// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace avrdudess
{
	// https://stackoverflow.com/questions/453161/best-practice-to-save-application-settings-in-a-windows-forms-application
	// https://stackoverflow.com/questions/401232/static-indexers

    public static class Config
    {
        private const string FILE_CONFIG = "config.xml";
        private static readonly XmlFile<ConfigData> xmlFile = new XmlFile<ConfigData>(FILE_CONFIG);
        public static ConfigData Prop;

        public static void Save()
        {
            Prop.configVersion = ConfigData.CONFIG_VERSION;
            try
            {
                xmlFile.Write(Prop);
            }
            catch(Exception ex)
            {
                MsgBox.error("_XMLWRITEERROR", "configuration", ex.Message);
            }
        }

        public static void Load()
        {
            try
            {
                Prop = xmlFile.Read();
            }
            catch(Exception ex)
            {
                if (ex.GetType() != typeof(FileNotFoundException))
                    MsgBox.error($"An error occurred trying to load configuration:{Environment.NewLine}{ex.Message}");
            }

            if(Prop == null)
                Prop = new ConfigData();

            // Check config file version
            // No translation here since we've not loaded them yet
            if (Prop.configVersion == 0)
            {
                // TODO
                // Probably failed to load config file
            }
            else if (Prop.configVersion > ConfigData.CONFIG_VERSION)
                MsgBox.warning($"Configuration file version ({Prop.configVersion}) is newer than expected ({ConfigData.CONFIG_VERSION}), things might not work properly...");
        }
    }

    public class ConfigData
    {
        // This usually only needs to be incremented if a field has been renamed or removed
        public const uint CONFIG_VERSION = 1;

        // Version isn't serializable so use this struct instead
        public struct SkipVersion
        {
            public int Major;
            public int Minor;
            //public int Build;
            //public int Revision;
        };

        public uint configVersion; // Config file version
        public long updateCheck; // Time of last update check

        [XmlElement(ElementName = "skipVersion")]
        public SkipVersion _skipVersion; // Version to skip

        public bool toolTips; // Tool tips enabled
        public string avrdudeLoc; // avrdude location
        public string avrdudeConfLoc; // avrdude.conf location
        public string avrSizeLoc; // avr-size location
        public Point windowLocation; // For persistent window location across sessions
        public string language; // Language to use
        public HashSetD<string> hiddenMCUs; // List of MCU IDs to hide from drop down list
        public HashSetD<string> hiddenProgrammers; // List of programmer IDs to hide from drop down list
        public PresetData previousSettings; // Settings from when the program was last closed
        public bool usePreviousSettings; // Enable saving settings when closing
        public Size windowSize; // For persistent window size across sessions
        public bool checkForUpdates; // Check for updates on startup

        [XmlIgnore]
        public Version skipVersion
        {
            get { return new Version(_skipVersion.Major, _skipVersion.Minor); }//, config.skipVersion.Build, config.skipVersion.Revision); }
            set
            {
                _skipVersion.Major = value.Major;
                _skipVersion.Minor = value.Minor;
                //skipVersion.Build = value.Build;
                //skipVersion.Revision = value.Revision;
            }
        }

        // Defaults
        public ConfigData()
        {
            configVersion = 0;
            updateCheck = 0;
            _skipVersion.Major = 0;
            _skipVersion.Minor = 0;
            //_skipVersion.Build = 0;
            //_skipVersion.Revision = 0;
            toolTips = true;
            avrdudeLoc = "";
            avrdudeConfLoc = "";
            avrSizeLoc = "";
            language = "english";
            hiddenMCUs = new HashSetD<string>();
            hiddenProgrammers = new HashSetD<string>();
            previousSettings = new PresetData();
            usePreviousSettings = true;
            checkForUpdates = true;
        }
    }
}
