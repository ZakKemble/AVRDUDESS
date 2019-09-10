/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System.Collections.Generic;

namespace avrdudess
{
    public class MCU : Part
    {
        private int _flash;
        private int _eeprom;
        private string _signature;
        private List<string> _memoryTypes;

        /*
        Memory types can be called anything, heres a list of all the types that currently appear in avrdude.conf

        efuse
        lfuse
        hfuse
        fuse
        lock
        signature
        flash
        eeprom
        calibration

        XMEGA stuff:
        prodsig
        fuse0
        fuse1
        fuse2
        fuse3
        fuse4
        fuse5
        data
        application
        apptable
        boot
        usersig
        */


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

        public List<string> memoryTypes
        {
            get
            {
                List<string> allTypes = new List<string>();
                allTypes.AddRange(_memoryTypes);
                if (parent != null)
                    allTypes.AddRange(((MCU)parent).memoryTypes);

                // NOTE: This list will have duplicate entries if the same memories are also defined in parent parts
                return allTypes;
            }
        }

        public MCU(string id, string desc = null, string signature = null, int flash = 0, int eeprom = 0, MCU parent = null, List<string> memoryTypes = null)
            : base(id, desc, parent)
        {
            if(signature != null)
                this.signature = signature.ToLower();
            this.flash = flash;
            this.eeprom = eeprom;
            _memoryTypes = (memoryTypes != null) ? memoryTypes : new List<string>();
        }
    }
}
