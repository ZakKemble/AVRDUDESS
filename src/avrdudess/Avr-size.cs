// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2014-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

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
            load(FILE_AVR_SIZE, Config.Prop.avrSizeLoc, false);
        }

        // Get size of flash/EEPROM file
        public int getSize(string file)
        {
            int totalSize = INVALID;
            if (File.Exists(file) && launch($"\"{file}\"", null, null, OutputTo.Memory))
            {
                waitForExit(); // TODO remove? use callback
                totalSize = parse();
            }
            return totalSize;
        }

        // Parse out size
        private int parse()
        {
            if (outputLogStdOut == null)
                return INVALID;

            // Split into lines
            string[] data = outputLogStdOut.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 2)
                return INVALID;

            // Split line 2
            data = data[1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 2)
                return INVALID;

            // Parse data
            int.TryParse(data[0], out int textSize);
            int.TryParse(data[1], out int dataSize);

            int totalSize = textSize + dataSize;

            return totalSize;
        }
    }
}
