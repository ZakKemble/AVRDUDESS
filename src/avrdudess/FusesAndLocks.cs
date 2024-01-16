// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace avrdudess
{
    // Credits:
    // Simone Chifari (Fuse selector)
    public class FusesList
    {
        private const string FILE_BITS = "bits.xml";

        public static readonly FusesList fl = new FusesList();
        private readonly Hashtable lockbits = new Hashtable();
        private readonly Hashtable fusebitslo = new Hashtable();
        private readonly Hashtable fusebitshi = new Hashtable();
        private readonly Hashtable fusebitsext = new Hashtable();

        private readonly string fileLocation;

        private readonly XmlFile<FuseBits> xmlFile;

        private FusesList()
        {
            fileLocation = Path.Combine(AssemblyData.directory, FILE_BITS);
            xmlFile = new XmlFile<FuseBits>(fileLocation);
            Load();
        }

        private void Load()
        {
            FuseBits bits = null;
            try
            {
                bits = xmlFile.Read();
            }
            catch (Exception ex)
            {
                MsgBox.error("_ERRORLOADFUSES", ex.Message);
            }

            if (bits == null)
                return;

            bits.bits.ForEach(m => {
                if (string.IsNullOrEmpty(m.signature))
                    return;
                if (m.lb != null)
                    lockbits.Add(m.signature, m.lb);
                if (m.low != null)
                    fusebitslo.Add(m.signature, m.low);
                if (m.high != null)
                    fusebitshi.Add(m.signature, m.high);
                if (m.ext != null)
                    fusebitsext.Add(m.signature, m.ext);
            });
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

    [XmlRoot("fuseBits")]
    public class FuseBits
    {
        public struct Bits
        {
            [XmlAttribute] public string signature;
            [XmlAttribute] public string name;
            public string high;
            public string low;
            public string ext;
            [XmlElement(ElementName = "lock")] public string lb;
        };

        [XmlElement("mcu")]
        public List<Bits> bits = new List<Bits>();
    }
}
