// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
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
        [XmlRoot("fuseBits")]
        public class FuseBitNamesXML
        {
            public struct McuBitsXML
            {
                [XmlAttribute] public string signature;
                [XmlAttribute] public string name;
                public string high;
                public string low;
                public string ext;
                [XmlElement(ElementName = "lock")] public string lb;
            }

            [XmlElement("mcu")]
            public List<McuBitsXML> Mcus = new List<McuBitsXML>();
        }

        public class FuseBitNames
        {
            public string[] lfd;
            public string[] hfd;
            public string[] efd;
            public string[] lbd;

            public FuseBitNames() : 
                this (new FuseBitNamesXML.McuBitsXML())
            {

            }

            public FuseBitNames(FuseBitNamesXML.McuBitsXML bits)
            {
                lfd = GetBitNames(bits.low);
                hfd = GetBitNames(bits.high);
                efd = GetBitNames(bits.ext);
                lbd = GetBitNames(bits.lb);
            }

            private string[] GetBitNames(string str)
            {
                var b = str?.Split(',');
                if(b == null || b.Length != 8)
                    b = new string[8] { "?", "?", "?", "?", "?", "?", "?", "?", };
                return b;
            }
        }

        private const string FILE_BITS = "bits.xml";

        public static readonly FusesList fl = new FusesList();

        public readonly Dictionary<string, FuseBitNames> Items = new Dictionary<string, FuseBitNames>();

        private FusesList()
        {
            Load();
        }

        private void Load()
        {
            FuseBitNamesXML bits = null;
            try
            {
                string path = Path.Combine(AssemblyData.directory, FILE_BITS);
                bits = new XmlFile<FuseBitNamesXML>(path).Read();
            }
            catch (Exception ex)
            {
                MsgBox.error("_ERRORLOADFUSES", ex.Message);
            }

            if (bits == null)
                return;

            bits.Mcus.ForEach(m => Items.Add(m.signature, new FuseBitNames(m)));
        }
    }
}
