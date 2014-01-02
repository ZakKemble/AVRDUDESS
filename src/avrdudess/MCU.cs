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
    public class MCU : Part
    {
        private int _flash;
        private int _eeprom;
        public string signature { get; private set; }
        private MCU parent;

        public int flash
        {
            get
            {
                int s = _flash;
                if (s == -1)
                    s = (parent != null) ? parent.flash : 0;
                return s;
            }
            private set
            {
                _flash = value;
            }
        }

        public int eeprom
        {
            get
            {
                int s = _eeprom;
                if (s == -1)
                    s = (parent != null) ? parent.eeprom : 0;
                return s;
            }
            private set
            {
                _eeprom = value;
            }
        }

        public MCU(string name, string fullName = "", string signature = "", int flash = 0, int eeprom = 0, MCU parent = null)
            : base(name, fullName)
        {
            this.signature = signature.ToLower();
            this.flash = flash;
            this.eeprom = eeprom;
            this.parent = parent;
        }
    }
}
