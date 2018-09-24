/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace avrdudess
{
    // Contains all configuration stuff
    [XmlType(TypeName = "ConfigData")] // For backwards compatability with old (<v2.2) config.xml
    public sealed partial class Config
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
        public List<string> hiddenMCUs; // List of MCU IDs to hide from drop down list
        public List<string> hiddenProgrammers; // List of programmer IDs to hide from drop down list
        public PresetData previousSettings; // Settings from when the program was last closed

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
        private Config()
            : base()
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
            hiddenMCUs = new List<string>();
            hiddenProgrammers = new List<string>();
            previousSettings = new PresetData();
        }

        public new void save()
        {
            configVersion = CONFIG_VERSION;
            base.save();
        }

        public new void load()
        {
            base.load();

            // NOTE: When calling load() the XML deserializer creates a new Config object, which replaces this current object.
            // So to get and set things in the latest Config object we have to access it through the Prop property, which is static.
            // Once this method ends the garbage collector will clean up the old Config object.

            // Check config file version
            // No translation here since we've not loaded them yet
            if (Prop.configVersion == 0)
            {
                // TODO
                // Probably failed to load config file
            }
            else if (Prop.configVersion > CONFIG_VERSION)
                MsgBox.warning("Configuration file version ({0}) is newer than expected ({1}), things might not work properly...", Prop.configVersion, CONFIG_VERSION);
        }
    }
}
