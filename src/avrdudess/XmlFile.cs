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
using System.Windows.Forms;

namespace avrdudess
{
    class XmlFile
    {
        private string fileLocation;
        private string name;

        public string fileLoc
        {
            get { return fileLocation; }
        }

        public XmlFile(string fileName, string name)
        {
            fileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            this.name = name;
        }

        public void save(object data)
        {
            TextWriter tw = null;
            try
            {
                tw = new StreamWriter(fileLocation, false);
                new XmlSerializer(data.GetType()).Serialize(tw, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occored trying to save " + name + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (tw != null)
                tw.Close();
        }

        public T load<T>()
        {
            T data = default(T);
            TextReader tr = null;
            try
            {
                tr = new StreamReader(fileLocation);
                data = (T)new XmlSerializer(typeof(T)).Deserialize(tr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occored trying to load " + name + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (tr != null)
                tr.Close();

            return data;
        }
    }
}
