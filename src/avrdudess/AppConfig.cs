/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Drawing;
using System.Xml.Serialization;

namespace avrdudess
{
    // Contains all configuration stuff
    [XmlType(TypeName = "ConfigData")] // For backwards compatability with old (<v2.2) config.xml
    public sealed partial class Config
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

        [XmlElement(ElementName = "skipVersion")]
        public SkipVersion _skipVersion; // Version to skip

        public bool toolTips;       // Tool tips enabled
        public string avrdudeLoc;   // avrdude location
        public string avrdudeConfLoc;   // avrdude.conf location
        public string avrSizeLoc;   // avr-size location
        public Point windowLocation; // For persistant window location across sessions

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
            configVersion = CONFIG_VERSION;
            preset = "Default";
            updateCheck = 0;
            _skipVersion.Major = 0;
            _skipVersion.Minor = 0;
            //_skipVersion.Build = 0;
            //_skipVersion.Revision = 0;
            toolTips = true;
            avrdudeLoc = "";
            avrdudeConfLoc = "";
            avrSizeLoc = "";
        }

        public new void load()
        {
            base.load();

            // Check config file version
            if (configVersion > CONFIG_VERSION)
                MsgBox.warning(String.Format("Configuration file version ({0}) is newer than expected ({1}), things might not work properly...", configVersion, CONFIG_VERSION));
        }
    }
}
