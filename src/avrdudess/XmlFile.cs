/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;
using System.Xml.Serialization;

namespace avrdudess
{
    public abstract class XmlFile<T>
    {
        protected string fileLocation { get; private set; }
        private string name;
        abstract protected object data { get; set; }

        public XmlFile(string fileName, string name)
        {
            this.name = name;

            // Where the file should be
            fileLocation = makePath(Environment.SpecialFolder.ApplicationData, fileName);
            
            // File exists, don't need to copy template
            if (File.Exists(fileLocation))
                return;

            // Copy template if found

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
                MsgBox.error("Failed to copy " + name + " template to AppData", ex);
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
                MsgBox.error("An error occurred trying to save " + name, ex);
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
                MsgBox.error("An error occurred trying to load " + name, ex);
            }

            if (tr != null)
                tr.Close();
        }
    }
}
