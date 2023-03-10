/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;
using System.Xml.Serialization;

namespace avrdudess
{
    public abstract class XmlFile<T>
    {
        private const string FILE_PORTABLE = "portable.txt";

        protected string fileLocation { get; private set; }
        private string name;
        abstract protected object data { get; set; }
        
        public XmlFile(string fileName, string name, bool customFileLocation)
        {
            this.name = name;

            if (customFileLocation) // Used for importing/exporting presets XML (a bit hacky)
            {
                fileLocation = fileName;
                return;
            }

            var portableFileLocation = Path.Combine(AssemblyData.directory, fileName);
            if (File.Exists(portableFileLocation)) // Portable mode will only read/write from the application directory
            {
                fileLocation = portableFileLocation;
                return;
            }

            
            // Where the file should be
            fileLocation = makePath(Environment.SpecialFolder.ApplicationData, fileName);

            // If file exists then we don't need to copy the template
            if (!File.Exists(fileLocation))
            {
                // Copy template if we can find it

                string[] locations = new string[]
                {
                    makePath(Environment.SpecialFolder.CommonApplicationData, fileName), // CommonAppData
                    Path.Combine(AssemblyData.directory, fileName), // Program .exe directory
                    Path.Combine(Directory.GetCurrentDirectory(), fileName) // Working directory
                };

                foreach (string location in locations)
                {
                    if (File.Exists(location))
                    {
                        copyTemplate(location);
                        break;
                    }
                }

                // If template wasn't found then a new file will be created later
            }
        }

        private string makePath(Environment.SpecialFolder folder, string fileName)
        {
            string path = Path.Combine(Environment.GetFolderPath(folder), AssemblyData.title);
            path = Path.Combine(path, fileName);
            return path;
        }

        private void copyTemplate(string source)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileLocation));
                File.Copy(source, fileLocation);
            }
            catch (Exception ex)
            {
                MsgBox.error("_XMLCOPYERROR", name, ex.Message);
            }
        }

        protected void write()
        {
            TextWriter tw = null;

            // TODO: move these try-catches somewhere outside this class?
            try
            {
                // Make sure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(fileLocation));

                tw = new StreamWriter(fileLocation, false);
                new XmlSerializer(data.GetType()).Serialize(tw, data);
            }
            catch (Exception ex)
            {
                MsgBox.error("_XMLWRITEERROR", name, ex.Message);
            }

            if (tw != null)
                tw.Close();
        }

        protected void read()
        {
            data = default(T);
            TextReader tr = null;
            try
            {
                tr = new StreamReader(fileLocation);
                data = (T)new XmlSerializer(typeof(T)).Deserialize(tr);
            }
            catch (Exception ex)
            {
                // No translation here since we might have a read error while loading config.xml or the translation file itself
                MsgBox.error("An error occurred trying to load {0}:{1}{2}", name, Environment.NewLine, ex.Message);
            }

            if (tr != null)
                tr.Close();
        }
    }
}
