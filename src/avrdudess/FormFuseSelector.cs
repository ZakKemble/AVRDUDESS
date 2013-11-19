/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace avrdudess
{
    // Credits:
    // Simone Chifari (Fuse selector)
    public partial class FormFuseSelector : Form
    {
        private FusesList fl = new FusesList();
        private CheckBox[] cbLBits;
        private Button[] btnLFuse;
        private Button[] btnHFuse;
        private Button[] btnEFuse;
        private Label[] lbLB;
        private Label[] lbLF;
        private Label[] lbHF;
        private Label[] lbEF;

        private string[] newFuses;
        private bool closeOk = false;

        public FormFuseSelector()
        {
            InitializeComponent();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Icon = Icon.ExtractAssociatedIcon(assembly.Location);
        }

        private void FormFusesAndLocks_Load(object sender, EventArgs e)
        {
            Left += 200;
        }

        public string[] editFuseAndLocks(MCU mcu, string[] fuses)
        {
            Text = "Fuse & lock bits: " + mcu.fullName + " (" + mcu.signature.ToUpper() + ")";

            cbLBits = new CheckBox[] { cbLB0, cbLB1, cbLB2, cbLB3, cbLB4, cbLB5, cbLB6, cbLB7 };
            btnLFuse = new Button[] { btnLF0, btnLF1, btnLF2, btnLF3, btnLF4, btnLF5, btnLFuse6, btnLFuse7 };
            btnHFuse = new Button[] { btnHF0, btnHF1, btnHF2, btnHF3, btnHF4, btnHF5, btnHF6, btnHF7 };
            btnEFuse = new Button[] { btnEF0, btnEF1, btnEF2, btnEF3, btnEF4, btnEF5, btnEF6, btnEF7 };
            lbLB = new Label[] { lbLB0, lbLB1, lbLB2, lbLB3, lbLB4, lbLB5, lbLB6, lbLB7 };
            lbLF = new Label[] { lbLFuse0, lbLFuse1, lbLFuse2, lbLFuse3, lbLFuse4, lbLFuse5, lbLFuse6, lbLFuse7 };
            lbHF = new Label[] { lbHFuse0, lbHFuse1, lbHFuse2, lbHFuse3, lbHFuse4, lbHFuse5, lbHFuse6, lbHFuse7 };
            lbEF = new Label[] { lbEFuse0, lbEFuse1, lbEFuse2, lbEFuse3, lbEFuse4, lbEFuse5, lbEFuse6, lbEFuse7 };

            lblCarefulNow.Visible = !fl.isSupported(mcu.signature);

            string[] lfd = fl.getLfuse(mcu.signature).Split(',');
            string[] hfd = fl.getHfuse(mcu.signature).Split(',');
            string[] efd = fl.getEfuse(mcu.signature).Split(',');
            string[] lbd = fl.getLockBits(mcu.signature).Split(',');

            string lf = hex2binary(fuses[0]);
            string hf = hex2binary(fuses[1]);
            string ef = hex2binary(fuses[2]);
            string lb = hex2binary(fuses[3]);

            for (int i = 7; i >= 0; i--)
            {
                btnLFuse[i].Click += new EventHandler(bits_Click);
                btnHFuse[i].Click += new EventHandler(bits_Click);
                btnEFuse[i].Click += new EventHandler(bits_Click);
                cbLBits[i].Click += new EventHandler(bits_Click);

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

            ShowDialog();

            if (closeOk)
                return newFuses;

            return null;
        }

        private void bits_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                b.Text = (b.Text == "0") ? "1" : "0";
            }

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
            
            lbLFuse.Text = "0x" + lf;
            lbHFuse.Text = "0x" + hf;
            lbEFuse.Text = "0x" + ef;
            lbLBits.Text = "0x" + lb;

            newFuses = new string[] { lf, hf, ef, lb };
        }

        private string binaryToHex(string value)
        {
            return String.Format("{0:X2}", Convert.ToInt32(value, 2));
        }

        private string hex2binary(string hexvalue)
        {
            int value;
            try
            {
                value = Convert.ToInt32(hexvalue, 16);
            }
            catch (Exception)
            {
                value = 0xFF;
            }

            string binary = Convert.ToString(value, 2);
            binary = binary.PadLeft(8, '0');

            return binary;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            closeOk = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
