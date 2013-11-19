/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;

namespace avrdudess
{
    public class Part : IComparable
    {
        public string name { get; private set; }
        public string fullName { get; private set; }

        public Part(string name, string fullName)
        {
            this.name = name;
            this.fullName = fullName;
        }

        public int CompareTo(object other)
        {
            return fullName.CompareTo(((Part)other).fullName);
        }
    }
}
