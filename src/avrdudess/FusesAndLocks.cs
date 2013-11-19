/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections;

namespace avrdudess
{
    // Credits:
    // Simone Chifari (Fuse selector)
    class FusesList
    {
        private Hashtable lockbits = new Hashtable();
        private Hashtable fusebitslo = new Hashtable();
        private Hashtable fusebitshi = new Hashtable();
        private Hashtable fusebitsext = new Hashtable();

        public bool isSupported(string signature)
        {
            return (
                fusebitslo.ContainsKey(signature) &&
                fusebitshi.ContainsKey(signature) &&
                fusebitsext.ContainsKey(signature) &&
                lockbits.ContainsKey(signature)
                );
        }

        public string getLfuse(string signature)
        {
            if (fusebitslo.ContainsKey(signature))
                return fusebitslo[signature].ToString();

            return "?,?,?,?,?,?,?,?";
        }

        public string getHfuse(string signature)
        {
            if (fusebitshi.ContainsKey(signature))
                return fusebitshi[signature].ToString();

            return "?,?,?,?,?,?,?,?";
        }

        public string getEfuse(string signature)
        {
            if (fusebitsext.ContainsKey(signature))
                return fusebitsext[signature].ToString();

            return "?,?,?,?,?,?,?,?";
        }

        public string getLockBits(string signature)
        {
            if (lockbits.ContainsKey(signature))
                return lockbits[signature].ToString();

            return "?,?,?,?,?,?,?,?";
        }

        public FusesList()
        {
            string signature;

            // TODO: Move this stuff to a file

            // AT90
            signature="1e9001"; // AT90S1200
            lockbits.Add(signature, ",LB1,LB2,,,,,");
            fusebitslo.Add(signature, ",,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9101"; // AT90S2313
            lockbits.Add(signature, ",LB1,LB2,,,,,");
            fusebitslo.Add(signature, ",,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9102"; // AT90S2323
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, "FSTRT,,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9103"; // AT90S2343
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, "RCEN,,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9105"; // AT90S2333
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,BODEN,BODLVL,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9201"; // AT90S4414
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, ",,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9202"; // AT90S4433
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,BODEN,BODLVL,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9203"; // AT90S4434
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, ",,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9301"; // AT90S8515
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, ",,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9303"; // AT90S8535
            lockbits.Add(signature, ",LB2,LB1,,,,,");
            fusebitslo.Add(signature, ",,,,,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9381"; // AT90PWM2/3
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,PSCRV,PSC0RB,PSC1RB,PSC2RB");

            signature="1e9383"; // AT90PWM2B/3B
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,PSCRV,PSC0RB,,PSC2RB");

            signature="1e9482"; // AT90USB82/162
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,RSTDSBL,DWEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,HWBE,,,,");

            signature="1e9581"; // AT90CAN32
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "TA0SEL,BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,");

            signature="1e9681"; // AT90CAN64
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "TA0SEL,BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,");

            signature="1e9682"; // AT90USB646/647
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,HWBE,,,,");

            signature="1e9781"; // AT90CAN128
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "TA0SEL,BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,");

            signature="1e9782"; // AT90USB1286/1287
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,HWBE,,,,");

            //ATmega
            signature="1e9205"; // ATmega48
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e920a"; // ATmega48P
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9306"; // ATmega8515
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,WDTON,S8515C");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9307"; // ATmega8
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,WDTON,RSTDISBL");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9308"; // ATmega8535
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,WDTON,S8535C");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e930a"; // ATmega88
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");

            signature="1e930f"; // ATmega88P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");

            signature="1e9401"; // ATmega161
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,,SUT,,BOOTRST,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9402"; // ATmega163
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,,,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9403"; // ATmega16
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9404"; // ATmega162
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, ",BODLEVEL0,BODLEVEL1,BODLEVEL2,M161C,,,");

            signature="1e9405"; // ATmega165/169
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, ",BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,");

            signature="1e9406"; // ATmega168
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");

            signature="1e9407"; // ATmega165P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,");

            signature="1e940a"; // ATmega164P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e940b"; // ATmega168P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");

            signature="1e9501"; // ATmega323
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,,,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9502"; // ATmega32
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9503"; // ATmega329
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9504"; // ATmega3290
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9505"; // ATmega325
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9506"; // ATmega3250
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9507"; // ATmega406
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL,SUT0,SUT1,BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON");
            fusebitshi.Add(signature, "JTAGEN,OCDEN,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9508"; // ATmega324P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e950f"; // ATmega328P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");

            signature = "1e9514"; // ATmega328
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,,,,,");

            signature="1e9602"; // ATmega64
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "WDTON,M103C,,,,,,");

            signature="1e9603"; // ATmega649
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9604"; // ATmega6490
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9605"; // ATmega645
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9606"; // ATmega6450
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,,,,,");

            signature="1e9608"; // ATmega640
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e9609"; // ATmega644
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e960a"; // ATmega644P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e9702"; // ATmega128
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,BODEN,BODLEVEL");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,CKOPT,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "WDTON,M103C,,,,,,");

            signature="1e9703"; // ATmega1280
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e9704"; // ATmega1281
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e9705"; // ATmega1284P
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e9801"; // ATmega2560
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            signature="1e9802"; // ATmega2561
            lockbits.Add(signature, "LB1,LB2,BLB01,BLB02,BLB11,BLB12,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BOOTRST,BOOTSZ0,BOOTSZ1,EESAVE,WDTON,,JTAGEN,OCDEN");
            fusebitsext.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,,,,,");

            //ATtiny
            signature="1e9004"; // ATtiny11
            lockbits.Add(signature, ",LB1,LB2,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,RSTDISBL,FSTRT,,,");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9005"; // ATtiny12
            lockbits.Add(signature, ",LB1,LB2,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,RSTDISBL,SPIEN,BODEN,BODLEVEL");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9006"; // ATtiny15
            lockbits.Add(signature, ",LB1,LB2,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,,,RSTDISBL,SPIEN,BODEN,BODLEVEL");
            fusebitshi.Add(signature, ",,,,,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9007"; // ATtiny13
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,SUT0,SUT1,CKDIV8,WDTON,EESAVE,");
            fusebitshi.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,DWEN,SELFPRGEN,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e9108"; // ATtiny25
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9109"; // ATtiny26
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOPT,PLLCK");
            fusebitshi.Add(signature, "BODEN,BODLEVEL,EESAVE,,RSTDISBL,,,");
            fusebitsext.Add(signature, ",,,,,,,");

            signature="1e910a"; // ATtiny2313
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "RSTDISBL,BODLEVEL0,BODLEVEL1,BODLEVEL2,WDTON,,EESAVE,DWEN");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e910b"; // ATtiny24
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e910c"; // ATtiny261
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9206"; // ATtiny45
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9207"; // ATtiny44
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9208"; // ATtiny461
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9209"; // ATtiny48
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,,,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e930b"; // ATtiny85
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e930c"; // ATtiny84
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e930d"; // ATtiny861
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,CKSEL2,CKSEL3,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");

            signature="1e9311"; // ATtiny88
            lockbits.Add(signature, "LB1,LB2,,,,,,");
            fusebitslo.Add(signature, "CKSEL0,CKSEL1,,,SUT0,SUT1,CKOUT,CKDIV8");
            fusebitshi.Add(signature, "BODLEVEL0,BODLEVEL1,BODLEVEL2,EESAVE,WDTON,,DWEN,RSTDISBL");
            fusebitsext.Add(signature, "SELFPRGEN,,,,,,,");
        }
    }
}
