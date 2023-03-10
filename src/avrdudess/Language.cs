/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2018 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;

namespace avrdudess
{
    class Language
    {
        public static readonly Language Translation = new Language();

        private Dictionary<string, string> languages = new Dictionary<string, string>();
        private Dictionary<string, string> translations = new Dictionary<string, string>();

        private delegate bool Action<T1, T2>(T1 arg, T2 arg2);

        private Language()
        {

        }

        public void apply(Control root, bool isFirst = true)
        {
            if (isFirst)
                root.Text = Translation.get(root.Text);

            foreach (Control control in root.Controls)
            {
                control.Text = Translation.get(control.Text);
                if (control.Controls != null)
                    apply(control, false);
            }
        }

        public void load()
        {
            try
            {
                string location = AssemblyData.directory + "/Languages/";

                findLanguages(location);

                // Load the selected language xml
                string fileName = Config.Prop.language + ".xml";
                readThroughXML(Path.Combine(location, fileName), getTranslation, null);
            }
            catch (Exception ex)
            {
                MsgBox.error("Error loading languages:{0}{1}", Environment.NewLine, ex.Message);
            }
        }

        // Loop through all .xml files in directory and search for the <name> tag
        private void findLanguages(string location)
        {
            string[] languageFiles = Directory.GetFiles(location, "*.xml");

            foreach (string file in languageFiles)
            {
                string languageId = Path.GetFileNameWithoutExtension(file);
                readThroughXML(file, getLanguageInfo, languageId);
            }
        }

        private bool getLanguageInfo(XmlReader reader, object data)
        {
            string name = reader.Name;

            if (name == "name" && reader.Read())
            {
                string file = (string)data;
                string languageName = reader.ReadContentAsString();
                languages.Add(file, languageName);
                return true; // We've found what we're looking for, stop reading XML file
            }

            return false;
        }

        private bool getTranslation(XmlReader reader, object data)
        {
            string name = reader.Name;

            if (name == "string")
            {
                string translationId = reader.GetAttribute("name");
                if (reader.Read())
                {
                    string value = reader.ReadContentAsString();
                    try
                    {
                        translations.Add(translationId, value);
                    }
                    catch(ArgumentException)
                    {
                        throw new Exception(string.Format("Duplicate translation ID: {0}", translationId));
                    }
                }
            }

            return false;
        }

        private void readThroughXML(string file, Action<XmlReader, object> onElement, object data)
        {
            TextReader tr = new StreamReader(file);

            using (XmlReader reader = XmlReader.Create(tr))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (onElement(reader, data))
                            break;
                    }
                }
            }

            if (tr != null)
                tr.Close();
        }
        
        public string get(string key)
        {
            string str;

            if (!key.StartsWith("_")) // Only lookup translations for strings that start with an underscore
                str = key;
            else if (!translations.TryGetValue(key.Remove(0, 1), out str))
                str = key; //"<ERR>";

            // what about new lines?
            // \n, \r and \r\n all seem to work fine in message boxes and text boxes

            return str;
        }

        public Dictionary<string, string> getLanguages()
        {
            return languages;
        }
    }
}
