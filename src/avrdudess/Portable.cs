// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.IO;

namespace avrdudess
{
    public static class Portable
    {
        private const string FILE_PORTABLE = "portable.txt";
        private static bool hasReadFile = false;
        private static bool _isPortable = false;

        public static bool IsPortable
        {
            get
            {
                if (hasReadFile)
                    return _isPortable;

                hasReadFile = true;
                string path = Path.Combine(AssemblyData.directory, FILE_PORTABLE);
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[1];
                        int n = fs.Read(buffer, 0, 1);
                        if (n == 1 && buffer[0] == 'Y')
                            _isPortable = true;
                    }
                }
                catch (Exception)
                {
                    // Failed to open or something, run in non-portable mode
                }

                return _isPortable;
            }
        }
    }
}
