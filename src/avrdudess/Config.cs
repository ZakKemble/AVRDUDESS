/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;

namespace avrdudess
{
    class Config : XmlFile<ConfigData>
    {
        private const string FILE_CONFIG = "config.xml";

        private ConfigData config;

        protected override object data
        {
            get { return config; }
            set { config = (ConfigData)value; }
        }

        #region Getters and setters

        public string preset
        {
            get { return config.preset; }
            set { config.preset = value; }
        }

        public long updateCheck
        {
            get { return config.updateCheck; }
            set { config.updateCheck = value; }
        }

        public Version skipVersion
        {
            get { return new Version(config.skipVersion.Major, config.skipVersion.Minor); }//, config.skipVersion.Build, config.skipVersion.Revision); }
            set
            {
                config.skipVersion.Major = value.Major;
                config.skipVersion.Minor = value.Minor;
                //config.skipVersion.Build = value.Build;
                //config.skipVersion.Revision = value.Revision;
            }
        }

        public bool toolTips
        {
            get { return config.toolTips; }
            set { config.toolTips = value; }
        }

        #endregion

        public Config()
            : base(FILE_CONFIG, "configuration")
        {
            config = new ConfigData();
        }

        // Save config
        public void save()
        {
            write();
        }

        // Load config
        public void load()
        {
            // If file doesn't exist then make it
            if (!File.Exists(fileLocation))
                save();

            // Load config from XML
            read();
            if (config == null)
                config = new ConfigData();

            // Check config file version
            if (config.configVersion > ConfigData.CONFIG_VERSION)
                MsgBox.warning(String.Format("Configuration file version ({0}) is newer than expected ({1}), things might not work properly...", config.configVersion, ConfigData.CONFIG_VERSION));
        }
    }

    [Serializable]
    public class ConfigData
    {
        public const uint CONFIG_VERSION = 1;

        // Version isn't serializable so use this struct instead
        public struct SkipVersion
        {
            public int Major;
            public int Minor;
            //public int Build;
            //public int Revision;
        };

        public uint configVersion;  // Config file version
        public string preset;       // Last preset used
        public long updateCheck;    // Time of last update check
        public SkipVersion skipVersion; // Version to skip
        public bool toolTips;       // Tool tips enabled

        public ConfigData()
        {
            configVersion = CONFIG_VERSION;
            preset = "Default";
            updateCheck = 0;
            skipVersion.Major = 0;
            skipVersion.Minor = 0;
            //skipVersion.Build = 0;
            //skipVersion.Revision = 0;
            toolTips = true;
        }
    }
}
