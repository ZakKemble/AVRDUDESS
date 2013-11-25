/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace avrdudess
{
    class Avrdude
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

        public enum OutputTo
        {
            Log,
            Console
        }

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

        private readonly List<Programmer> _programmers;
        private readonly List<MCU> _mcus;
        private Process p;
        private Action<object> onFinish;
        private readonly Action<string> consoleWriteFunc;
        private readonly Action consoleClearFunc;
        private readonly Action processStartFunc;
        private readonly Action processEndFunc;
        private object param;
        private readonly string binary;
        private string _version;
        private string outputLog;
        private bool enableConsoleUpdate;
        private bool processOutputStreamOpen;

        #region Getters and setters

        public List<Programmer> programmers
        {
            get { return _programmers; }
        }

        public List<MCU> mcus
        {
            get { return _mcus; }
        }

        public string version
        {
            get { return _version; }
        }

        public string log
        {
            get { return outputLog; }
        }

        #endregion

        public Avrdude()
            : this(null, null, null, null)
        {
        }

        public Avrdude(Action<string> consoleWriteFunc, Action consoleClearFunc, Action processStartFunc, Action processEndFunc)
        {
            // Maybe these Actions should be EventHandler things?
            this.consoleWriteFunc = consoleWriteFunc;
            this.consoleClearFunc = consoleClearFunc;
            this.processStartFunc = processStartFunc;
            this.processEndFunc = processEndFunc;

            binary = searchForBinary();

            if (binary == null)
                MsgBox.error(FILE_AVRDUDE + " is missing!");

            Thread t = new Thread(new ThreadStart(tConsoleUpdate));
            t.IsBackground = true;
            t.Start();

            _programmers    = new List<Programmer>();
            _mcus           = new List<MCU>();

            load();
        }

        private void load()
        {
            getVersion();

            _programmers.Clear();
            _mcus.Clear();
            
            loadConfig();

            // Sort alphabetically
            _programmers.Sort();
            _mcus.Sort();

            // Add default
            _programmers.Insert(0, new Programmer("", "Select a programmer..."));
            _mcus.Insert(0, new MCU("", "Select an MCU...", ""));
        }

        private string searchForBinary()
        {
            char pathSplit;
            string binaryName = FILE_AVRDUDE;

            // Get char that is used to split PATH entries
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    pathSplit = ':';
                    break;
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    pathSplit = ';';
                    binaryName += ".exe";
                    break;
                default:
                    pathSplit = ';';
                    break;
            }

            // File exists in application directory (mainly for Windows)
            string app = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, binaryName);
            if (File.Exists(app))
                return app;

            // Search PATHs
            var paths = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in paths.Split(new char[] {pathSplit}, StringSplitOptions.RemoveEmptyEntries))
            {
                var fullPath = Path.Combine(path, binaryName);
                if (File.Exists(fullPath))
                    return fullPath;
            }

            return null;
        }

        // Get AVRDUDE version
        // Credits:
        // Simone Chifari (Getting AVRDUDE version and displaying in window title)
        private void getVersion()
        {
            launch("", OutputTo.Log);
            waitForExit();

            if (outputLog == null)
                return;

            string log = outputLog;
            int pos = log.IndexOf("avrdude version");
            if (pos > -1)
            {
                log = log.Substring(pos);
                _version = log.Substring(0, log.IndexOf(','));
            }
        }

        // Basic parsing of avrdude.conf to get programmers & MCUs
        private void loadConfig()
        {
            string conf_loc = null;
            
            // If on Unix check /etc/ first
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                conf_loc = "/etc/" + FILE_AVRDUDECONF;
                if (!File.Exists(conf_loc))
                    conf_loc = null;
            }

            if (conf_loc == null)
            {
                conf_loc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILE_AVRDUDECONF);

                // Config file not found
                if (!File.Exists(conf_loc))
                {
                    MsgBox.error(FILE_AVRDUDECONF + " is missing!");
                    return;
                }
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
                if (!s.StartsWith("part") && !isProgrammer)
                    continue;

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

                // Here we get the MCU signature

                string signature = "";

                // Loop through lines looking for "signature"
                // Abort if "part" or "programmer" is found, "signature" is probably missing for this MCU
                for (; i < lines.Length; i++)
                {
                    s = lines[i].Trim();

                    // Too far
                    if (s.StartsWith("part") || s.StartsWith("programmer"))
                    {
                        i--;
                        break;
                    }

                    // Does line have signature key?
                    pos = lines[i].IndexOf('=');
                    if (pos > -1 && lines[i].Substring(1, pos - 1).Trim() == "signature")
                    {
                        // Get signature value
                        signature = lines[i].Substring(pos + 1).Trim(trimChars);

                        // Remove 0x and spaces from signature (0xAA 0xAA 0xAA -> AAAAAA)
                        signature = signature.Replace("0x", "").Replace(" ", "");

                        break;
                    }
                }

                // Some formatting
                desc = desc.ToUpper().Replace("XMEGA", "xmega").Replace("MEGA", "mega").Replace("TINY", "tiny");

                // Add to MCUs
                _mcus.Add(new MCU(id, desc, signature));
            }
        }

        public void launch(string arg, Action<object> onFinish, object param, OutputTo outputTo = OutputTo.Console)
        {
            if (isActive()) // Another process is active
                return;
            this.onFinish = onFinish;
            this.param = param;
            launch(arg, outputTo);
        }

        public void launch(string args, OutputTo outputTo = OutputTo.Console)
        {
            if (isActive()) // Another process is active
                return;
            else if (!File.Exists(binary)) // avrdude is missing
            {
                consoleWrite(FILE_AVRDUDE + " is missing!" + Environment.NewLine);
                return;
            }

            // Clear log
            outputLog = "";
            //consoleClear();

            if (outputTo == OutputTo.Console)
                consoleWrite("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ " + Environment.NewLine);

            // Add -u to command line (disables safe mode)
            if (args.Trim().Length > 0)
                args = "-u " + args;

            Process tmp = new Process();
            tmp.StartInfo.FileName = binary;
            tmp.StartInfo.Arguments = args;
            tmp.StartInfo.CreateNoWindow = true;
            tmp.StartInfo.UseShellExecute = false;
            tmp.StartInfo.RedirectStandardOutput = true;
            tmp.StartInfo.RedirectStandardError = true;
            tmp.EnableRaisingEvents = true;
            if (outputTo == OutputTo.Log)
            {
                //tmp.OutputDataReceived += new DataReceivedEventHandler(outputLogHandler);
                tmp.ErrorDataReceived += new DataReceivedEventHandler(outputLogHandler);
            }
            tmp.Exited += new EventHandler(p_Exited);

            try
            {
                tmp.Start();
            }
            catch (Exception ex)
            {
                MsgBox.error("Error starting AVRDUDE", ex);
                return;
            }

            if (processStartFunc != null)
                processStartFunc();

            enableConsoleUpdate = (outputTo == OutputTo.Console);
            p = tmp;

            if (outputTo == OutputTo.Log)
            {
                processOutputStreamOpen = true;
                //p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            }
            else
                processOutputStreamOpen = false;
        }

        private void p_Exited(object sender, EventArgs e)
        {
            if (processEndFunc != null)
                processEndFunc();

            if (onFinish != null)
                onFinish(param);
            onFinish = null;
        }

        // Progress bars don't work using async output, since it only fires when a new line is received
        // Problem: Slow if the process outputs a lot of text
        private void tConsoleUpdate()
        {
            while (true)
            {
                Thread.Sleep(25);
                if (!enableConsoleUpdate)
                    continue;
                try
                {
                    if (p != null)
                    {
                        char[] buff = new char[256];
                        if (p.StandardError.Read(buff, 0, buff.Length) > 0)
                        {
                            string s = new string(buff);
                            consoleWrite(s);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        // This method is needed to properly capture the process output for logging
        private void outputLogHandler(object sender, DataReceivedEventArgs e)
        {
            string s = e.Data;
            if (s != null)
                outputLog += s.Replace("\0", String.Empty) + Environment.NewLine;
            else // A null is sent when the stream is closed
                processOutputStreamOpen = false;
        }

        private bool isActive()
        {
            return (p != null && !p.HasExited);
        }
        
        public void kill()
        {
            if (p != null && !p.HasExited)
            {
                p.Kill();
                consoleWrite(Environment.NewLine + "AVRDUDE killed" + Environment.NewLine);
            }
        }

        public void waitForExit()
        {
            if (p != null && !p.HasExited)
                p.WaitForExit();

            // There might still be data in a buffer somewhere that needs to be read by the output handler even after the process has ended
            while (processOutputStreamOpen)
                Thread.Sleep(20);
        }

        private void consoleWrite(string text)
        {
            if (consoleWriteFunc != null)
                consoleWriteFunc(text);
        }

        private void consoleClear()
        {
            if (consoleClearFunc != null)
                consoleClearFunc();
        }
    }
}
