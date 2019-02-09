/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Text;

namespace avrdudess
{
    // Move this class to Avrdude class or somthing instead of it being on its own?
    // Maybe have stuff like setMCU("m328") in Avrdude class and do away with this one?
    // TODO: Improve this class
    class CmdLine
    {
        private Form1 mainForm;
        private StringBuilder sb = new StringBuilder();

        public CmdLine(Form1 mainForm)
        {
            this.mainForm = mainForm;
        }

        private void generateMain(bool addMCU = true)
        {
            //sb.Clear(); // .NET 4.0+ only
            sb.Length = 0;
            sb.Capacity = 0;

            if (mainForm.prog != null && mainForm.prog.id.Length > 0)
                cmdLineOption("c", mainForm.prog.id);

            if (mainForm.mcu != null && mainForm.mcu.id.Length > 0 && addMCU)
                cmdLineOption("p", mainForm.mcu.id);

            if (mainForm.port.Length > 0)
                cmdLineOption("P", mainForm.port);

            if (mainForm.baudRate.Length > 0)
                cmdLineOption("b", mainForm.baudRate);

            if (mainForm.bitClock.Length > 0)
                cmdLineOption("B", mainForm.bitClock);

            if (mainForm.force)
                cmdLineOption("F");

            for (byte i = 0; i < mainForm.verbosity; i++)
                cmdLineOption("v");
        }

        public string genReadSig()
        {
            generateMain(false);

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            // AVRDUDE needs -p defined to work, so just set m8
            cmdLineOption("p", "m8");

            return sb.ToString();
        }

        // TODO combine generateReadFuses and generateReadLock
        public string generateReadFuses(string lfuseFile, string hfuseFile, string efuseFile)
        {
            generateMain();

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            cmdLineOption("U", "lfuse:r:\"" + lfuseFile + "\":h");
            cmdLineOption("U", "hfuse:r:\"" + hfuseFile + "\":h");
            cmdLineOption("U", "efuse:r:\"" + efuseFile + "\":h");

            return sb.ToString();
        }

        public string generateReadLock(string lockFile)
        {
            generateMain();

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            cmdLineOption("U", "lock:r:\"" + lockFile + "\":h");

            return sb.ToString();
        }

        public string generateWriteFuses()
        {
            generateMain();

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            addWriteFuses();

            return sb.ToString();
        }

        public string generateWriteLock()
        {
            generateMain();

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            if (mainForm.lockSetting.Length > 0)
                cmdLineOption("U", "lock:w:" + mainForm.lockSetting + ":m");

            return sb.ToString();
        }

        public string generateFlash()
        {
            generateMain();

            if (mainForm.disableVerify)
                cmdLineOption("V");

            if (mainForm.disableFlashErase)
                cmdLineOption("D");

            if (mainForm.eraseFlashAndEEPROM)
                cmdLineOption("e");

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            if (mainForm.flashFile.Length > 0)
                cmdLineOption("U", "flash:" + mainForm.flashFileOperation + ":\"" + mainForm.flashFile + "\":" + mainForm.flashFileFormat);

            return sb.ToString();
        }

        public string generateEEPROM()
        {
            generateMain();

            if (mainForm.disableVerify)
                cmdLineOption("V");

            if (mainForm.disableFlashErase)
                cmdLineOption("D");

            if (mainForm.eraseFlashAndEEPROM)
                cmdLineOption("e");

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            if (mainForm.EEPROMFile.Length > 0)
                cmdLineOption("U", "eeprom:" + mainForm.EEPROMFileOperation + ":\"" + mainForm.EEPROMFile + "\":" + mainForm.EEPROMFileFormat);

            return sb.ToString();
        }

        public void generate()
        {
            if (!mainForm.ready)
                return;

            generateMain();

            if (mainForm.disableVerify)
                cmdLineOption("V");

            if (mainForm.disableFlashErase)
                cmdLineOption("D");

            if (mainForm.eraseFlashAndEEPROM)
                cmdLineOption("e");

            if (mainForm.doNotWrite)
                cmdLineOption("n");

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            if (mainForm.flashFile.Length > 0)
                cmdLineOption("U", "flash:" + mainForm.flashFileOperation + ":\"" + mainForm.flashFile + "\":" + mainForm.flashFileFormat);

            if (mainForm.EEPROMFile.Length > 0)
                cmdLineOption("U", "eeprom:" + mainForm.EEPROMFileOperation + ":\"" + mainForm.EEPROMFile + "\":" + mainForm.EEPROMFileFormat);

            if (mainForm.setFuses)
                addWriteFuses();

            if (mainForm.setLock && mainForm.lockSetting.Length > 0)
                cmdLineOption("U", "lock:w:" + mainForm.lockSetting + ":m");

            mainForm.cmdBox = sb.ToString();
        }

        private void addWriteFuses()
        {
            if (mainForm.lowFuse.Length > 0)
            {
                if (mainForm.lowFuse.Length == 2)
                    mainForm.lowFuse = "0x" + mainForm.lowFuse.ToUpper();
                cmdLineOption("U", "lfuse:w:" + mainForm.lowFuse + ":m");
            }

            if (mainForm.highFuse.Length > 0)
            {
                if (mainForm.highFuse.Length == 2)
                    mainForm.highFuse = "0x" + mainForm.highFuse.ToUpper();
                cmdLineOption("U", "hfuse:w:" + mainForm.highFuse + ":m");
            }

            if (mainForm.exFuse.Length > 0)
            {
                if (mainForm.exFuse.Length == 2)
                    mainForm.exFuse = "0x" + mainForm.exFuse.ToUpper();
                cmdLineOption("U", "efuse:w:" + mainForm.exFuse + ":m");
            }
        }

        private void cmdLineOption(string arg, string val)
        {
            sb.Append("-" + arg + " " + val + " ");
        }

        private void cmdLineOption(string arg)
        {
            sb.Append("-" + arg + " ");
        }
    }
}
