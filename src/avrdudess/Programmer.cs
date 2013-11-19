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
    public class Programmer : Part
    {
        public Programmer(string name, string fullName)
            : base(name, fullName)
        {

        }
    }
}
