// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System.Collections.Generic;

namespace avrdudess
{
    public class Programmer : Part
    {
        private string _type;

		//
		// PROGRAMMER TYPE NOT YET IMPLEMENTED
		//
		
        /*
            arduino          = Arduino programmer
            avr910           = Serial programmers using protocol described in application note AVR910
            avrftdi = Interface to the MPSSE Engine of FTDI Chips using libftdi.
            buspirate        = Using the Bus Pirate's SPI interface for programming
            buspirate_bb     = Using the Bus Pirate's bitbang interface for programming
            butterfly        = Atmel Butterfly evaluation board; Atmel AppNotes AVR109, AVR911
            butterfly_mk = Mikrokopter.de Butterfly
            dragon_dw = Atmel AVR Dragon in debugWire mode
            dragon_hvsp      = Atmel AVR Dragon in HVSP mode
            dragon_isp       = Atmel AVR Dragon in ISP mode
            dragon_jtag      = Atmel AVR Dragon in JTAG mode
            dragon_pdi       = Atmel AVR Dragon in PDI mode
            dragon_pp        = Atmel AVR Dragon in PP mode
            flip1            = FLIP USB DFU protocol version 1 (doc7618)
            flip2            = FLIP USB DFU protocol version 2 (AVR4023)
            ftdi_syncbb      = FT245R/FT232R Synchronous BitBangMode Programmer
            jtagmki          = Atmel JTAG ICE mkI
            jtagmkii         = Atmel JTAG ICE mkII
            jtagmkii_avr32   = Atmel JTAG ICE mkII in AVR32 mode
            jtagmkii_dw      = Atmel JTAG ICE mkII in debugWire mode
            jtagmkii_isp     = Atmel JTAG ICE mkII in ISP mode
            jtagmkii_pdi     = Atmel JTAG ICE mkII in PDI mode
            jtagice3         = Atmel JTAGICE3
            jtagice3_pdi     = Atmel JTAGICE3 in PDI mode
            jtagice3_dw      = Atmel JTAGICE3 in debugWire mode
            jtagice3_isp     = Atmel JTAGICE3 in ISP mode
            linuxgpio        = GPIO bitbanging using the Linux sysfs interface (not available)
            par              = Parallel port bitbanging
            pickit2 = Microchip's PICkit2 Programmer
            serbb            = Serial port bitbanging
            stk500 = Atmel STK500 Version 1.x firmware
            stk500generic    = Atmel STK500, autodetect firmware version
            stk500v2         = Atmel STK500 Version 2.x firmware
            stk500hvsp       = Atmel STK500 V2 in high-voltage serial programming mode
            stk500pp         = Atmel STK500 V2 in parallel programming mode
            stk600 = Atmel STK600
            stk600hvsp = Atmel STK600 in high-voltage serial programming mode
            stk600pp         = Atmel STK600 in parallel programming mode
            usbasp = USBasp programmer, see http://www.fischl.de/usbasp/
            usbtiny          = Driver for "usbtiny"-type programmers
            wiring           = http://wiring.org.co/, Basically STK500v2 protocol, with some glue to trigger the bootloader.
*/

        private const string MCU_ISP = "m8";
        private const string MCU_JTAG = "m32";
        private const string MCU_TPI = "t10";
        private const string MCU_PDI = "";

        // debugWire, HVSP, ISP, JTAG, PDI, PP, TPI
        // "avrdude.exe -c ?type"
        // Don't do bootloader types (arduino, avr910, etc...)
        public static readonly Dictionary<string, List<string>> progInterfaces = new Dictionary<string, List<string>>()
        {
            {
                "avrftdi",
                new List<string>() { MCU_ISP, MCU_JTAG }
            },
            {
                "buspirate",
                new List<string>() { MCU_ISP }
            },
            {
                "buspirate_bb",
                new List<string>() { MCU_ISP, MCU_TPI }
            },
            {
                "usbasp",
                new List<string>() { MCU_ISP, MCU_TPI }
            }
        };

        public string type
        {
            get
            {
                return _type ?? ((Programmer)parent)?.type ?? "?";
            }
            private set
            {
                _type = value;
            }
        }

        public bool hide
        {
            get
            {
                return ignore || Config.Prop.hiddenProgrammers.Find(x => x == id) != null;
            }
        }

        public Programmer(string id, string desc = null, Programmer parent = null)
            : base(id, desc, parent)
        {

        }

        public List<string> getInterfaces()
        {
            return null;
            //return progInterfaces[type];
        } 
    }
}
