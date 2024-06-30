// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

namespace avrdudess
{
    public class Part
    {
        public string id { get; private set; }
        private string _desc;
        protected Part parent;
        public bool ignore { get; private set; }

        public string desc
        {
            get => _desc ?? parent?.desc ?? "?";
            private set => _desc = value;
        }

        public Part(string id, string desc, Part parent)
        {
            // id must not be null

            this.id = id;
            this.desc = desc;
            this.parent = parent;

            // Part is a common value thing or deprecated
            ignore = id.StartsWith(".") || (desc?.ToLower().StartsWith("deprecated") ?? false);
        }
    }
}
