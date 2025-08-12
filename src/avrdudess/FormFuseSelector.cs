// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2025, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace avrdudess
{
    // Credits:
    // Simone Chifari (Fuse selector)
    public partial class FormFuseSelector : Form
    {
        private readonly Button[] btnLockBits;
        private readonly Button[] btnLFuse;
        private readonly Button[] btnHFuse;
        private readonly Button[] btnEFuse;
        private readonly Label[] lbLB;
        private readonly Label[] lbLF;
        private readonly Label[] lbHF;
        private readonly Label[] lbEF;
        private string[] newFuses;

        public FormFuseSelector()
        {
            InitializeComponent();

            Icon = AssemblyData.icon;

            btnLockBits = new Button[] { btnLockBit0, btnLockBit1, btnLockBit2, btnLockBit3, btnLockBit4, btnLockBit5, btnLockBit6, btnLockBit7 };
            btnLFuse = new Button[] { btnLF0, btnLF1, btnLF2, btnLF3, btnLF4, btnLF5, btnLFuse6, btnLFuse7 };
            btnHFuse = new Button[] { btnHF0, btnHF1, btnHF2, btnHF3, btnHF4, btnHF5, btnHF6, btnHF7 };
            btnEFuse = new Button[] { btnEF0, btnEF1, btnEF2, btnEF3, btnEF4, btnEF5, btnEF6, btnEF7 };
            lbLB = new Label[] { lbLB0, lbLB1, lbLB2, lbLB3, lbLB4, lbLB5, lbLB6, lbLB7 };
            lbLF = new Label[] { lbLFuse0, lbLFuse1, lbLFuse2, lbLFuse3, lbLFuse4, lbLFuse5, lbLFuse6, lbLFuse7 };
            lbHF = new Label[] { lbHFuse0, lbHFuse1, lbHFuse2, lbHFuse3, lbHFuse4, lbHFuse5, lbHFuse6, lbHFuse7 };
            lbEF = new Label[] { lbEFuse0, lbEFuse1, lbEFuse2, lbEFuse3, lbEFuse4, lbEFuse5, lbEFuse6, lbEFuse7 };

            for (int i = 7; i >= 0; i--)
            {
                btnLFuse[i].Click += bits_Click;
                btnHFuse[i].Click += bits_Click;
                btnEFuse[i].Click += bits_Click;
                btnLockBits[i].Click += bits_Click;
            }
        }

        private void FormFusesAndLocks_Load(object sender, EventArgs e)
        {
            Language.Translation.Apply(this);

            Left += 200;
        }

        public string[] editFuseAndLocks(MCU mcu, string[] fuses)
        {
            Text = string.Format(Language.Translation.get("_TITLE_FUSEANDLOCKBITS"), mcu.desc, mcu.signature.ToUpper());

            var supported = FusesList.fl.Items.ContainsKey(mcu.signature);
            var bitNames = supported ? FusesList.fl.Items[mcu.signature] : new FusesList.FuseBitNames();
            lblCarefulNow.Visible = !supported;

            string lf = hex2binary(fuses[0]);
            string hf = hex2binary(fuses[1]);
            string ef = hex2binary(fuses[2]);
            string lb = hex2binary(fuses[3]);

            for (int i = 7; i >= 0; i--)
            {
                btnLFuse[i].Enabled = (bitNames.lfd[i] != "");
                btnHFuse[i].Enabled = (bitNames.hfd[i] != "");
                btnEFuse[i].Enabled = (bitNames.efd[i] != "");
                btnLockBits[i].Enabled = (bitNames.lbd[i] != "");

                SetBtnState(btnLFuse[i], lf.Substring(7 - i, 1) == "1");
                SetBtnState(btnHFuse[i], hf.Substring(7 - i, 1) == "1");
                SetBtnState(btnEFuse[i], ef.Substring(7 - i, 1) == "1");
                SetBtnState(btnLockBits[i], lb.Substring(7 - i, 1) == "1");

                lbLB[i].Text = bitNames.lbd[i];
                lbLF[i].Text = bitNames.lfd[i];
                lbHF[i].Text = bitNames.hfd[i];
                lbEF[i].Text = bitNames.efd[i];
            }

            generateFusesAndLocks();

            if(ShowDialog() == DialogResult.OK)
                return newFuses;

            return null;
        }

        private void SetBtnState(Button btn, bool state)
        {
            btn.Text = state ? "1" : "0";
            btn.BackColor = state && btn.Enabled ? Color.LightGreen : default;
        }

        private void bits_Click(object sender, EventArgs e)
        {
            if (sender is Button b)
                SetBtnState(b, b.Text != "1");

            generateFusesAndLocks();
        }

        private void generateFusesAndLocks()
        {
            string lf = "";
            string hf = "";
            string ef = "";
            string lb = "";

            for (int i = 7; i >= 0; i--)
            {
                lf += btnLFuse[i].Text;
                hf += btnHFuse[i].Text;
                ef += btnEFuse[i].Text;
                lb += btnLockBits[i].Text;
            }

            lf = binaryToHex(lf);
            hf = binaryToHex(hf);
            ef = binaryToHex(ef);
            lb = binaryToHex(lb);
            
            lbLFuse.Text = $"0x{lf}";
            lbHFuse.Text = $"0x{hf}";
            lbEFuse.Text = $"0x{ef}";
            lbLBits.Text = $"0x{lb}";

            newFuses = new string[] { lf, hf, ef, lb };
        }

        private string binaryToHex(string value)
        {
            return $"{Convert.ToInt32(value, 2):X2}";
        }

        private string hex2binary(string hexValue)
        {
            int value;
            if (!int.TryParse(hexValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                value = 0xFF;
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }
    }
}
