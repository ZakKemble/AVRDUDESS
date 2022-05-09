/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System.IO;

namespace avrdudess
{
	// https://stackoverflow.com/questions/453161/best-practice-to-save-application-settings-in-a-windows-forms-application
	// https://stackoverflow.com/questions/401232/static-indexers

    public sealed partial class Config : AppConfig
    {
        public static Config Prop = new Config();

        protected override object data
        {
            get { return Prop; }
            set { Prop = (value != null) ? (Config)value : new Config(); }
        }
    }

    public abstract class AppConfig : XmlFile<Config>
    {
        private const string FILE_CONFIG = "config.xml";

        protected AppConfig(string xmlFile = FILE_CONFIG)
            : base(xmlFile, "configuration", false)
        {
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
        }
    }
}
