// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2018-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace avrdudess
{
    [XmlRoot("languages")]
    public class LanguagesMeta
    {
        public struct SupportedEntry
        {
            [XmlAttribute] public string name;
            [XmlAttribute] public string ename;
            [XmlAttribute] public string file;
        }

#if DEBUG
        public struct KeyEntry
        {
            [XmlAttribute] public string name;
        }
#endif

        [XmlArray("supported")]
        [XmlArrayItem("x")]
        public List<SupportedEntry> Supported = new List<SupportedEntry>();

#if DEBUG
        [XmlArray("keys")]
        [XmlArrayItem("k")]
        public List<KeyEntry> Expectedkeys = new List<KeyEntry>();
#endif
    }

    [XmlRoot("translation")]
    public class TranslationData
    {
        public struct TranslationEntry
        {
            [XmlAttribute] public string name;
            [XmlText] public string str;
        }

        [XmlArray("data")]
        [XmlArrayItem("string")]
        public List<TranslationEntry> Translations = new List<TranslationEntry>();
    }

    class Language
    {
        private const string FILE_META = "_meta.xml";

        public static readonly Language Translation = new Language();

        private readonly Dictionary<string, string> languages = new Dictionary<string, string>();
        private readonly Dictionary<string, string> translations = new Dictionary<string, string>();
#if DEBUG
        private readonly HashSetD<string> expectedKeys = new HashSetD<string>();
#endif

        public Dictionary<string, string> Languages
        {
            get => languages;
        }

        public string this[string key]
        {
            get => get(key);
        }

        private Language()
        {

        }

        public void Apply(Control root, bool isFirst = true)
        {
            if (isFirst)
                root.Text = Translation.get(root.Text);

            foreach (Control control in root.Controls)
            {
                control.Text = Translation[control.Text];
                if (control.Controls != null)
                    Apply(control, false);
            }
        }

        private void LoadMeta(string langsDir)
        {
            var metaFile = Path.Combine(langsDir, FILE_META);
            var metaData = new XmlFile<LanguagesMeta>(metaFile).Read();
            metaData.Supported.ForEach(t => languages.Add(t.file, $"{t.name} ({t.ename})"));
#if DEBUG
            metaData.Expectedkeys.ForEach(t => expectedKeys.Add(t.name));
#endif
        }

        private void LoadLanguage(string langsDir, string language)
        {
            var langFile = Path.Combine(langsDir, language + ".xml");
            var langData = new XmlFile<TranslationData>(langFile).Read();
            langData.Translations.ForEach(t =>
            {
                if (translations.ContainsKey(t.name))
                    throw new Exception($"Duplicate translation key: {t.name}");
                translations.Add(t.name, t.str);
            });
#if DEBUG
            foreach (var k in expectedKeys.Keys)
            {
                if (!translations.ContainsKey(k))
                    Util.consoleWarning($"Missing translation key: {k}");
            }

            foreach (var k in translations.Keys)
            {
                if (!expectedKeys.Contains(k))
                    Util.consoleWarning($"Unexpected translation key: {k}");
            }
#endif
        }

        public void Load()
        {
            var langsDir = Path.Combine(AssemblyData.directory, "Languages");
            try
            {
                LoadMeta(langsDir);
                LoadLanguage(langsDir, Config.Prop.language);
            }
            catch (Exception ex)
            {
                MsgBox.error($"Error loading languages:{Environment.NewLine}{ex.Message}");
            }
        }

        public string get(string key) // TODO get -> Get (use indexer instead?)
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
    }
}
