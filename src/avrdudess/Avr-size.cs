/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;

namespace avrdudess
{
    class Avrsize : Executable
    {
        private const string FILE_AVR_SIZE = "avr-size";
        public const int INVALID = -1;

        public void load()
        {
            base.load(FILE_AVR_SIZE, Config.Prop.avrSizeLoc, false);
        }

        // Get size of flash/EEPROM file
        public int getSize(string file)
        {
            int totalSize = INVALID;
            if (File.Exists(file))
            {
                file = "\"" + file + "\"";
                if (launch(file, null, null, OutputTo.Log))
                {
                    waitForExit();
                    totalSize = parse();
                }
            }
            return totalSize;
        }

        // Parse out size
        private int parse()
        {
            if (outputLog == null)
                return INVALID;

            // Split into lines
            string[] data = outputLog.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 2)
                return INVALID;

            // Split line 2
            data = data[1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 2)
                return INVALID;

            // Parse data
            int textSize;
            int dataSize;
            int.TryParse(data[0], out textSize);
            int.TryParse(data[1], out dataSize);

            int totalSize = textSize + dataSize;

            return totalSize;
        }
    }
}
