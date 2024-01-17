// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Globalization;
using System.Windows.Forms;

namespace avrdudess
{
    // Credits:
    // Simone Chifari (Fuse selector)
    public partial class FormFuseSelector : Form
    {
        private readonly CheckBox[] cbLBits;
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

            cbLBits = new CheckBox[] { cbLB0, cbLB1, cbLB2, cbLB3, cbLB4, cbLB5, cbLB6, cbLB7 };
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
                cbLBits[i].Click += bits_Click;
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

            lblCarefulNow.Visible = !FusesList.fl.isSupported(mcu.signature);

            string[] lfd = FusesList.fl.getLfuse(mcu.signature).Split(',');
            string[] hfd = FusesList.fl.getHfuse(mcu.signature).Split(',');
            string[] efd = FusesList.fl.getEfuse(mcu.signature).Split(',');
            string[] lbd = FusesList.fl.getLockBits(mcu.signature).Split(',');

            string lf = hex2binary(fuses[0]);
            string hf = hex2binary(fuses[1]);
            string ef = hex2binary(fuses[2]);
            string lb = hex2binary(fuses[3]);

            for (int i = 7; i >= 0; i--)
            {
                btnLFuse[i].Text = lf.Substring(7 - i, 1);
                btnHFuse[i].Text = hf.Substring(7 - i, 1);
                btnEFuse[i].Text = ef.Substring(7 - i, 1);
                cbLBits[i].Checked = (lb.Substring(7 - i, 1) == "0");

                lbLB[i].Text = lbd[i];
                lbLF[i].Text = lfd[i];
                lbHF[i].Text = hfd[i];
                lbEF[i].Text = efd[i];

                btnLFuse[i].Enabled = (lfd[i] != "");
                btnHFuse[i].Enabled = (hfd[i] != "");
                btnEFuse[i].Enabled = (efd[i] != "");
                cbLBits[i].Enabled = (lbd[i] != "");
            }

            generateFusesAndLocks();

            if(ShowDialog() == DialogResult.OK)
                return newFuses;

            return null;
        }

        private void bits_Click(object sender, EventArgs e)
        {
            if (sender is Button b)
                b.Text = (b.Text == "0") ? "1" : "0";

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
                lb += (cbLBits[i].Checked == true) ? 0 : 1;
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
