/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

namespace avrdudess
{
    public class Programmer : Part
    {
        public Programmer(string id, string desc = null, Programmer parent = null)
            : base(id, desc, parent)
        {

        }
    }
}
