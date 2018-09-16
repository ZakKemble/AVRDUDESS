/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

namespace avrdudess
{
    public class MCU : Part
    {
        private int _flash;
        private int _eeprom;
        private string _signature;

        public int flash
        {
            get
            {
                int s = _flash;
                if (s == -1)
                    s = (parent != null) ? ((MCU)parent).flash : 0;
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
                    s = (parent != null) ? ((MCU)parent).eeprom : 0;
                return s;
            }
            private set
            {
                _eeprom = value;
            }
        }

        public string signature
        {
            get
            {
                string s = _signature;
                if (s == null)
                    s = (parent != null) ? ((MCU)parent).signature : "?";
                return s;
            }
            private set
            {
                _signature = value;
            }
        }

        public MCU(string id, string desc = null, string signature = null, int flash = 0, int eeprom = 0, MCU parent = null)
            : base(id, desc, parent)
        {
            if(signature != null)
                this.signature = signature.ToLower();
            this.flash = flash;
            this.eeprom = eeprom;
        }
    }
}
