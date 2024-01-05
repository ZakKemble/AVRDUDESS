// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System.Collections.Generic;

namespace avrdudess
{
    public class MCU : Part
    {
        private int _flash;
        private int _eeprom;
        private string _signature;
        private readonly List<string> _memoryTypes;

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
                return (_flash != -1) ? _flash : ((MCU)parent)?.flash ?? 0;
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
                return (_eeprom != -1) ? _eeprom : ((MCU)parent)?.eeprom ?? 0;
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
                return _signature ?? ((MCU)parent)?.signature ?? "?";
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
            this.signature = signature?.ToLower();
            this.flash = flash;
            this.eeprom = eeprom;
            _memoryTypes = memoryTypes ?? new List<string>();
        }
    }
}
