/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace avrdudess
{
    // Credits:
    // Simone Chifari (Fuse selector)
    sealed class FusesList
    {
        private const string FILE_BITS = "bits.xml";

        public static readonly FusesList fl = new FusesList();
        private Hashtable lockbits = new Hashtable();
        private Hashtable fusebitslo = new Hashtable();
        private Hashtable fusebitshi = new Hashtable();
        private Hashtable fusebitsext = new Hashtable();

        private string fileLocation;

        private FusesList()
        {
            fileLocation = AssemblyData.directory + "\\" + FILE_BITS;
            load();
        }

        private void load()
        {
            string signature = null;
            string high = null;
            string low = null;
            string ext = null;
            string lb = null;
            TextReader tr = null;

            try
            {
                tr = new StreamReader(fileLocation);

                using (XmlReader reader = XmlReader.Create(tr))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            string name = reader.Name;

                            if (name == "mcu")
                                signature = reader.GetAttribute("signature");

                            reader.Read();
                            switch (name)
                            {
                                case "high":
                                    high = reader.ReadContentAsString();
                                    break;
                                case "low":
                                    low = reader.ReadContentAsString();
                                    break;
                                case "ext":
                                    ext = reader.ReadContentAsString();
                                    break;
                                case "lock":
                                    lb = reader.ReadContentAsString();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (reader.Name == "mcu" && signature != null)
                            {
                                if (lb != null)
                                    lockbits.Add(signature, lb);
                                if (low != null)
                                    fusebitslo.Add(signature, low);
                                if (high != null)
                                    fusebitshi.Add(signature, high);
                                if (ext != null)
                                    fusebitsext.Add(signature, ext);

                                signature = null;
                                high = null;
                                low = null;
                                ext = null;
                                lb = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.error("_ERRORLOADFUSES", ex.Message);
            }

            if (tr != null)
                tr.Close();
        }

        public bool isSupported(string signature)
        {
            return (
                fusebitslo.ContainsKey(signature) &&
                fusebitshi.ContainsKey(signature) &&
                fusebitsext.ContainsKey(signature) &&
                lockbits.ContainsKey(signature)
                );
        }

        public string getLfuse(string signature)
        {
            return getBits(signature, fusebitslo);
        }

        public string getHfuse(string signature)
        {
            return getBits(signature, fusebitshi);
        }

        public string getEfuse(string signature)
        {
            return getBits(signature, fusebitsext);
        }

        public string getLockBits(string signature)
        {
            return getBits(signature, lockbits);
        }

        private string getBits(string signature, Hashtable table)
        {
            if (table.ContainsKey(signature))
                return table[signature].ToString();

            return "?,?,?,?,?,?,?,?";
        }
    }
}
