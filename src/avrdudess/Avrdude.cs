/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace avrdudess
{
    class DetectedMCUEventArgs : EventArgs
    {
        public MCU mcu { get; set; }

        public DetectedMCUEventArgs(MCU m)
        {
            mcu = m;
        }
    }

    class Avrdude : Executable
    {
        public class UsbAspFreq
        {
            public string name { get; private set; }
            public string bitClock { get; private set; }
            public int freq { get; private set; }

            public UsbAspFreq(string name, string bitClock, int freq)
            {
                this.name       = name;
                this.bitClock   = bitClock;
                this.freq       = freq;
            }
        }

        private const string FILE_AVRDUDE = "avrdude";
        private const string FILE_AVRDUDECONF = "avrdude.conf";

        public static readonly List<UsbAspFreq> USBaspFreqs = new List<UsbAspFreq>()
        {
            // Must be in order from highest to lowest
            new UsbAspFreq("1.5 MHz", "0.5", 1500000),
            new UsbAspFreq("750 KHz", "1.0", 750000),
            new UsbAspFreq("375 KHz", "2.0", 375000),
            new UsbAspFreq("187.5 KHz", "4.0", 187500),
            new UsbAspFreq("93.75 KHz", "8.0", 93750),
            new UsbAspFreq("32 KHz", "20.96", 32000),
            new UsbAspFreq("16 KHz", "46.88", 16000),
            new UsbAspFreq("8 KHz", "93.75", 8000),
            new UsbAspFreq("4 KHz", "187.5", 4000),
            new UsbAspFreq("2 KHz", "375.0", 2000),
            new UsbAspFreq("1 KHz", "750.0", 1000),
            new UsbAspFreq("500 Hz", "1500.0", 500),
        };

        public static readonly List<FileFormat> fileFormats = new List<FileFormat>()
        {
            new FileFormat("a", "Auto (writing only)"),
            new FileFormat("i", "Intel Hex"),
            new FileFormat("s", "Motorola S-record"),
            new FileFormat("r", "Raw binary"),
            new FileFormat("d", "Decimal (reading only)"),
            new FileFormat("h", "Hexadecimal (reading only)"),
            new FileFormat("b", "Binary (reading only)")
        };

        private enum ParseMemType
        {
            None,
            Flash,
            Eeprom
        }

        private readonly List<Programmer> _programmers;
        private readonly List<MCU> _mcus;
        public string version { get; private set; }
        public event EventHandler OnVersionChange;
        public event EventHandler<DetectedMCUEventArgs> OnDetectedMCU;
        
        #region Getters and setters

        public List<Programmer> programmers
        {
            get { return _programmers; }
        }

        public List<MCU> mcus
        {
            get { return _mcus; }
        }

        public string log
        {
            get { return outputLog; }
        }

        #endregion

        public Avrdude()
        {
            _programmers    = new List<Programmer>();
            _mcus           = new List<MCU>();
            version         = "";
        }

        public void load()
        {
            base.load(FILE_AVRDUDE, Config.Prop.avrdudeLoc);

            getVersion();

            _programmers.Clear();
            _mcus.Clear();

            loadConfig(Config.Prop.avrdudeConfLoc);

            // Sort alphabetically
            _programmers.Sort();
            _mcus.Sort();

            // Add default
            _programmers.Insert(0, new Programmer("", "Select a programmer..."));
            _mcus.Insert(0, new MCU("", "Select an MCU..."));
        }

        // Get AVRDUDE version
        // Credits:
        // Simone Chifari (Getting AVRDUDE version and displaying in window title)
        private void getVersion()
        {
            version = "";

            launch("", OutputTo.Log);
            waitForExit();

            if (outputLog != null)
            {
                string log = outputLog;
                int pos = log.IndexOf("avrdude version");
                if (pos > -1)
                {
                    log = log.Substring(pos);
                    version = log.Substring(0, log.IndexOf(','));
                }
            }

            if (OnVersionChange != null)
                OnVersionChange(this, EventArgs.Empty);
        }

        // Basic parsing of avrdude.conf to get programmers & MCUs
        private void loadConfig(string confLoc)
        {
            string conf_loc = null;

            if (!String.IsNullOrEmpty(confLoc))
                conf_loc = Path.Combine(confLoc, FILE_AVRDUDECONF);
            else
            {
                // If on Unix check /etc/ and /usr/local/etc/ first
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    conf_loc = "/etc/" + FILE_AVRDUDECONF;
                    if (!File.Exists(conf_loc))
                    {
                        conf_loc = "/usr/local/etc/" + FILE_AVRDUDECONF;
                        if (!File.Exists(conf_loc))
                            conf_loc = null;
                    }
                }

                if (conf_loc == null)
                {
                    conf_loc = Path.Combine(AssemblyData.directory, FILE_AVRDUDECONF);
                    if (!File.Exists(conf_loc))
                        conf_loc = Path.Combine(Directory.GetCurrentDirectory(), FILE_AVRDUDECONF);
                }
            }

            // Config file not found
            if (String.IsNullOrEmpty(conf_loc) || !File.Exists(conf_loc))
            {
                MsgBox.error(FILE_AVRDUDECONF + " is missing!");
                //throw new System.IO.FileNotFoundException("File is missing", FILE_AVRDUDECONF);
                return;
            }

            // Load config
            string[] lines;
            try
            {
                lines = File.ReadAllLines(conf_loc);
            }
            catch (Exception ex)
            {
                MsgBox.error("Error reading " + FILE_AVRDUDECONF, ex);
                return;
            }

            // Credits:
            // Uwe Tanger (New parse code for changed avrdude.conf)
            // Simone Chifari (New parse code for changed avrdude.conf & getting MCU signature)

            char[] trimChars = new char[3] { ' ', '"', ';' };

            for (int i = 0; i < lines.Length - 3; i++)
            {
                string s = lines[i].Trim();

                bool isProgrammer = s.StartsWith("programmer");
                bool isPart = s.StartsWith("part");
                if (!isPart && !isProgrammer)
                    continue;

                // Get parent ID
                string partentId = null;
                if (isPart && s.Contains("parent"))
                {
                    string[] parts = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 2)
                        partentId = parts[2].Trim(trimChars);
                }

                i++; // next line
                // Does line have id key?
                int pos = lines[i].IndexOf('=');
                if (pos < 0 || lines[i].Substring(1, pos - 1).Trim() != "id")
                    continue;

                // Get ID value
                string id = lines[i].Substring(pos + 1).Trim(trimChars);

                i++; // next line
                // Does line have desc key?
                pos = lines[i].IndexOf('=');
                if (pos < 0 || lines[i].Substring(1, pos - 1).Trim() != "desc")
                    continue;

                // Get description value
                string desc = lines[i].Substring(pos + 1).Trim(trimChars);

                // If its a programmer then add to programmers and go back to the top
                if (isProgrammer)
                {
                    _programmers.Add(new Programmer(id, desc));
                    continue;
                }

                // Otherwise its an MCU

                // Part is a common value thing or deprecated
                if (id.StartsWith(".") || desc.StartsWith("deprecated"))
                    continue;

                // Here we get the MCU signature, flash and EEPROM sizes

                string signature = "";
                int flash = -1;
                int eeprom = -1;
                ParseMemType memType = ParseMemType.None;
                
                // Loop through lines looking for "signature" and "memory"
                // Abort if "part" or "programmer" is found
                for (; i < lines.Length; i++)
                {
                    s = lines[i].Trim();

                    // Too far
                    if (s.StartsWith("part") || s.StartsWith("programmer"))
                    {
                        i--;
                        break;
                    }

                    // Found memory section
                    if (s.StartsWith("memory"))
                    {
                        pos = lines[i].IndexOf('"');
                        if (pos > -1)
                        {
                            // What type of memory is this?
                            string mem = lines[i].Substring(pos - 1).Trim(trimChars).ToLower();
                            if (mem == "flash")
                                memType = ParseMemType.Flash;
                            else if (mem == "eeprom")
                                memType = ParseMemType.Eeprom;
                        }
                    }
                    else if (memType != ParseMemType.None)
                    {
                        // See if this line defines the memory size
                        pos = lines[i].IndexOf('=');
                        if (pos > -1 && lines[i].Substring(1, pos - 1).Trim() == "size")
                        {
                            // Get size value
                            string memStr = lines[i].Substring(pos + 1).Trim(trimChars);

                            // Parse to int
                            int memTmp = 0;
                            if (!int.TryParse(memStr, out memTmp))
                            {
                                // Probably hex
                                if(memStr.StartsWith("0x"))
                                    memStr = memStr.Substring(2); // Remove 0x
                                int.TryParse(memStr, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out memTmp);
                            }

                            if (memType == ParseMemType.Flash)
                                flash = memTmp;
                            else if (memType == ParseMemType.Eeprom)
                                eeprom = memTmp;

                            memType = ParseMemType.None;
                        }
                    }

                    // Does line have signature key?
                    pos = lines[i].IndexOf('=');
                    if (pos > -1 && lines[i].Substring(1, pos - 1).Trim() == "signature")
                    {
                        // Get signature value
                        signature = lines[i].Substring(pos + 1).Trim(trimChars);

                        // Remove 0x and spaces from signature (0xAA 0xAA 0xAA -> AAAAAA)
                        signature = signature.Replace("0x", "").Replace(" ", "");
                    }
                }

                // Some formatting
                desc = desc.ToUpper().Replace("XMEGA", "xmega").Replace("MEGA", "mega").Replace("TINY", "tiny");

                // Find parent
                MCU parent = null;
                if (partentId != null)
                    parent = _mcus.Find(m => m.name == partentId);

                // Add to MCUs
                _mcus.Add(new MCU(id, desc, signature, flash, eeprom, parent));
            }
        }

        public new void launch(string args, Action<object> onFinish, object param, OutputTo outputTo = OutputTo.Console)
        {
            if (args.Trim().Length > 0)
            {
                // Add -u to command line (disables safe mode)
                args = "-u " + args;

                // Set conf file to use
                string confLoc = Config.Prop.avrdudeConfLoc;
                if (confLoc != "")
                    args = "-C \"" + Path.Combine(confLoc, FILE_AVRDUDECONF) + "\" " + args;
            }

            if (outputTo == OutputTo.Console)
                Util.consoleWrite("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ " + Environment.NewLine);

            base.launch(args, onFinish, param, outputTo);
        }

        public void launch(string args, OutputTo outputTo = OutputTo.Console)
        {
            launch(args, null, null, outputTo);
        }

        public void detectMCU(string args)
        {
            launch(args, detectComplete, null, OutputTo.Log);
        }

        // Got MCU info
        private void detectComplete(object param)
        {
            string log = outputLog.ToLower();

            // Look for string
            int pos = log.IndexOf("device signature");
            if (pos > -1)
            {
                // Remove upto "device signature" line
                log = log.Substring(pos);

                int sigStart = log.IndexOf("0x"); // Look for signature hex value
                if (sigStart > -1)
                {
                    // Get the 6 hex digits
                    string detectedSignature = log.Substring(sigStart + 2, 6);

                    // Look for MCU with same signature
                    MCU m = mcus.Find(s => s.signature == detectedSignature);

                    if (m != null) // Found
                    {
                        if (OnDetectedMCU != null)
                            OnDetectedMCU(this, new DetectedMCUEventArgs(m));
                    }
                    else // Not found
                    {
                        // TODO: dont write to console here
                        //m = new MCU(null, null, detectedSignature);
                        Util.consoleWrite("Unknown signature " + detectedSignature + Environment.NewLine);
                    }

                    return;
                }
            }

            if (OnDetectedMCU != null)
                OnDetectedMCU(this, new DetectedMCUEventArgs(null));
        }
    }
}
