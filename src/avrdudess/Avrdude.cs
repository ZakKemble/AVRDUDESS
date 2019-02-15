/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

    class ReadFuseLockEventArgs : EventArgs
    {
        public Avrdude.FuseLockType type { get; set; }
        public string value { get; set; }

        public ReadFuseLockEventArgs(Avrdude.FuseLockType t, string v)
        {
            type = t;
            value = v;
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
            new FileFormat("a", Language.Translation.get("_FILEFMT_AUTO")),
            new FileFormat("i", Language.Translation.get("_FILEFMT_HEX")),
            new FileFormat("s", Language.Translation.get("_FILEFMT_SREC")),
            new FileFormat("r", Language.Translation.get("_FILEFMT_BIN")),
            new FileFormat("d", Language.Translation.get("_FILEFMT_DECR")),
            new FileFormat("h", Language.Translation.get("_FILEFMT_HEXR")),
            new FileFormat("b", Language.Translation.get("_FILEFMT_BINR"))
        };
        
        public enum FuseLockType
        {
            [Description("")]
            None,
            [Description("hfuse")]
            Hfuse,
            [Description("lfuse")]
            Lfuse,
            [Description("efuse")]
            Efuse,
            [Description("lock")]
            Lock
        }

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
        public event EventHandler<ReadFuseLockEventArgs> OnReadFuseLock;

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
            get { return outputLogStdErr; }
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
        }

        // Get AVRDUDE version
        // Credits:
        // Simone Chifari (Getting AVRDUDE version and displaying in window title)
        private void getVersion()
        {
            version = "";

            launch("", OutputTo.Memory);
            waitForExit(); // TODO REMOVE? use callback

            if (outputLogStdErr != null)
            {
                string log = outputLogStdErr;
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

        private void savePart(bool isProgrammer, string parentId, string id, string desc, string signature, int flash, int eeprom)
        {
            if (id != null)
            {
                if (isProgrammer)
                {
                    // Find parent
                    Programmer parent = null;
                    if (parentId != null)
                        parent = _programmers.Find(m => m.id == parentId);

                    _programmers.Add(new Programmer(id, desc, parent));
                }
                else
                {
                    if (!id.StartsWith(".") && !desc.StartsWith("deprecated")) // Part is a common value thing or deprecated
                    {
                        // Some formatting
                        desc = desc.ToUpper().Replace("XMEGA", "xmega").Replace("MEGA", "mega").Replace("TINY", "tiny");

                        // Find parent
                        MCU parent = null;
                        if (parentId != null)
                            parent = _mcus.Find(m => m.id == parentId);

                        // Add to MCUs
                        _mcus.Add(new MCU(id, desc, signature, flash, eeprom, parent));
                    }
                }
            }
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
                Util.consoleError("_AVRCONFMISSING", FILE_AVRDUDECONF);
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
                Util.consoleError("_AVRCONFREADERROR", FILE_AVRDUDECONF, ex.Message);
                return;
            }

            char[] trimChars = new char[3] { ' ', '"', ';' };

            string parentId = null;
            string id = null;
            string desc = null;
            string signature = null;
            int flash = -1;
            int eeprom = -1;
       
            ParseMemType memType = ParseMemType.None;
            bool isProgrammer = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string s = lines[i].Trim();

                bool lineProgrammer = s.StartsWith("programmer");
                bool linePart = s.StartsWith("part");

                if(lineProgrammer || linePart)
                {
                    savePart(isProgrammer, parentId, id, desc, signature, flash, eeprom);

                    parentId = null;
                    id = null;
                    desc = null;
                    signature = null;
                    flash = -1;
                    eeprom = -1;
                    memType = ParseMemType.None;

                    // Get parent ID
                    string[] parts = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 2 && parts[1].Trim(trimChars) == "parent")
                        parentId = parts[2].Trim(trimChars);

                    isProgrammer = lineProgrammer;
                }
                else if (s == ";")
                    memType = ParseMemType.None;
                else
                {
                    int pos = s.IndexOf('=');
                    if (pos > -1)
                    {
                        string key = s.Substring(0, pos - 1).Trim();
                        string val = s.Substring(pos + 1).Trim(trimChars);

                        if (key == "id")
                            id = val;
                        else if (key == "desc")
                            desc = val;
                        else if (key == "signature")
                        {
                            signature = val;

                            // Remove 0x and spaces from signature (0xAA 0xAA 0xAA -> AAAAAA)
                            signature = signature.Replace("0x", "").Replace(" ", "");
                        }
                        else if (key == "size" && memType != ParseMemType.None)
                        {
                            // Parse to int
                            int memTmp = 0;
                            if (!int.TryParse(val, out memTmp))
                            {
                                // Probably hex
                                if (val.StartsWith("0x"))
                                    val = val.Substring(2); // Remove 0x
                                int.TryParse(val, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out memTmp);
                            }

                            if (memType == ParseMemType.Flash)
                                flash = memTmp;
                            else if (memType == ParseMemType.Eeprom)
                                eeprom = memTmp;

                            memType = ParseMemType.None;
                        }
                    }
                    else if (s.StartsWith("memory")) // Found memory section
                    {
                        pos = s.IndexOf('"');
                        if (pos > -1)
                        {
                            // Figure out memory type
                            string mem = s.Substring(pos - 1).Trim(trimChars).ToLower();
                            if (mem == "flash")
                                memType = ParseMemType.Flash;
                            else if (mem == "eeprom")
                                memType = ParseMemType.Eeprom;
                        }
                    }
                }
            }

            savePart(isProgrammer, parentId, id, desc, signature, flash, eeprom);
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
                Util.consoleWriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");

            Util.consoleWriteLine(">>>: {0} {1}", Color.Aquamarine, FILE_AVRDUDE, args);

            base.launch(args, onFinish, param, outputTo);
        }

        public void launch(string args, OutputTo outputTo = OutputTo.Console)
        {
            launch(args, null, null, outputTo);
        }

        public void detectMCU(string args)
        {
            launch(args, detectComplete, null, OutputTo.Memory);
        }

        // Got MCU info
        private void detectComplete(object param)
        {
            string log = outputLogStdErr.ToLower();

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
                        // TODO: dont write to console here, run event callback and let that deal with it
                        //m = new MCU(null, null, detectedSignature);
                        Util.consoleError("_UNKNOWNSIG", detectedSignature);
                    }

                    return;
                }
            }

            if (OnDetectedMCU != null)
                OnDetectedMCU(this, new DetectedMCUEventArgs(null));
        }

        public void readFusesLock(string args, FuseLockType[] types)
        {
            launch(args, readFusesLockComplete, types, OutputTo.Memory);
        }

        private void readFusesLockComplete(object param)
        { 
            FuseLockType[] types = param as FuseLockType[];
            if (types == null)
                return;

            string log = outputLogStdErr.ToLower();
            if (log.IndexOf("error") > -1 || log.IndexOf("fail") > -1)
            {
                if (OnReadFuseLock != null)
                    OnReadFuseLock(this, new ReadFuseLockEventArgs(FuseLockType.None, ""));
                return;
            }

            string[] values = outputLogStdOut.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            if (values.Length != types.Length)
            {
                if (OnReadFuseLock != null)
                    OnReadFuseLock(this, new ReadFuseLockEventArgs(FuseLockType.None, ""));
                return;
            }

            for(int i =0;i<types.Length;i++)
            {
                if (OnReadFuseLock != null)
                    OnReadFuseLock(this, new ReadFuseLockEventArgs(types[i], values[i].Trim()));
            }
        }
    }
}
