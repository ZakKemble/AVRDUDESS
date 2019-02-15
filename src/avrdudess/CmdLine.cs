/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

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
        
        public string generateReadFusesLock(Avrdude.FuseLockType[] types)
        {
            generateMain();

            if (mainForm.additionalSettings.Length > 0)
                sb.Append(mainForm.additionalSettings + " ");

            for (int i = 0; i < types.Length; i++)
                cmdLineOption("U", types[i].GetDescription() + ":r:-:h");
            
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

            makeWriteFuseLock(Avrdude.FuseLockType.Lock, mainForm.lockSetting);

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
                cmdLineOption("U", string.Format("flash:{0}:\"{1}\":{2}", mainForm.flashFileOperation, mainForm.flashFile, mainForm.flashFileFormat));

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
                cmdLineOption("U", string.Format("eeprom:{0}:\"{1}\":{2}", mainForm.EEPROMFileOperation, mainForm.EEPROMFile, mainForm.EEPROMFileFormat));

            return sb.ToString();
        }

        public void generate()
        {
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
                cmdLineOption("U", string.Format("flash:{0}:\"{1}\":{2}", mainForm.flashFileOperation, mainForm.flashFile, mainForm.flashFileFormat));

            if (mainForm.EEPROMFile.Length > 0)
                cmdLineOption("U", string.Format("eeprom:{0}:\"{1}\":{2}", mainForm.EEPROMFileOperation, mainForm.EEPROMFile, mainForm.EEPROMFileFormat));

            if (mainForm.setFuses)
                addWriteFuses();

            if (mainForm.setLock)
                makeWriteFuseLock(Avrdude.FuseLockType.Lock, mainForm.lockSetting);

            mainForm.cmdBox = sb.ToString();
        }

        private void makeWriteFuseLock(Avrdude.FuseLockType fuseLockType, string value)
        {
            value = value.Trim();
            if (value.Length > 0)
                cmdLineOption("U", fuseLockType.GetDescription() + ":w:" + value + ":m");
        }

        private void addWriteFuses()
        {
            makeWriteFuseLock(Avrdude.FuseLockType.Lfuse, mainForm.lowFuse);
            makeWriteFuseLock(Avrdude.FuseLockType.Hfuse, mainForm.highFuse);
            makeWriteFuseLock(Avrdude.FuseLockType.Efuse, mainForm.exFuse);
        }

        private void cmdLineOption(string arg, string val)
        {
            sb.Append(string.Format("-{0} {1} ", arg, val));
        }

        private void cmdLineOption(string arg)
        {
            sb.Append(string.Format("-{0} ", arg));
        }
    }
}
