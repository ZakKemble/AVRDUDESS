/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;

namespace avrdudess
{
    public class Part : IComparable
    {
        public string id { get; private set; }
        private string _desc;
        protected Part parent;
        public bool hide { get; private set; }

        public string desc
        {
            get
            {
                string s = _desc;
                if (s == null)
                    s = (parent != null) ? parent.desc : "?";
                return s;
            }
            private set
            {
                _desc = value;
            }
        }

        public Part(string id, string desc, Part parent)
        {
            // id must not be null

            this.id = id;
            this.desc = desc;
            this.parent = parent;

            // Part is a common value thing or deprecated
            hide = (id.StartsWith(".") || (desc != null && desc.ToLower().StartsWith("deprecated")));
        }

        public int CompareTo(object other)
        {
            return desc.CompareTo(((Part)other).desc);
        }
    }
}
