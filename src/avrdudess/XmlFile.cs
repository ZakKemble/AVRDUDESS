// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.IO;
using System.Xml.Serialization;

namespace avrdudess
{
    public class XmlFile<T>
    {
        public string FilePath { get; private set; }

        public XmlFile(string fileName, bool isFullPath = false)
        {
            if (isFullPath) // For importing/exporting presets
                FilePath = fileName;
            else if (Portable.IsPortable) // Use same directory as .exe when in portable mode
                FilePath = Path.Combine(AssemblyData.directory, fileName);
            else
            {
                // Users\[USERNAME]\AppData\Roaming\AVRDUDESS\
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = Path.Combine(path, AssemblyData.title);
                FilePath = Path.Combine(path, fileName);
            }
        }

        public void Write(T obj)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            using (TextWriter tw = new StreamWriter(FilePath, false))
                new XmlSerializer(typeof(T)).Serialize(tw, obj);
        }

        public T Read()
        {
            T obj = default;
            using (TextReader tr = new StreamReader(FilePath))
                obj = (T)new XmlSerializer(typeof(T)).Deserialize(tr);
            return obj;
        }
    }
}
