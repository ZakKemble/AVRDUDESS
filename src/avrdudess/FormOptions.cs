/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormOptions : Form
    {
        #region Control getters and setters

        public bool toolTips
        {
            get { return cbShowToolTips.Checked; }
            set { cbShowToolTips.Checked = value; }
        }

        public string avrdudeLocation
        {
            get { return txtAvrdudeLocation.Text; }
            set { txtAvrdudeLocation.Text = value; }
        }

        public string avrdudeConfLocation
        {
            get { return txtAvrdudeConfLocation.Text; }
            set { txtAvrdudeConfLocation.Text = value; }
        }

        public string avrSizeLocation
        {
            get { return txtAvrSizeLocation.Text; }
            set { txtAvrSizeLocation.Text = value; }
        }

        #endregion

        public FormOptions()
        {
            InitializeComponent();

            Icon = AssemblyData.icon;
        }

        private void btnBrowseAvrdude_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "avrdude folder location";
            browse(txtAvrdudeLocation);
        }

        private void btnBrowseAvrdudeConf_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "avrdude.conf folder location";
            browse(txtAvrdudeConfLocation);
        }

        private void btnBrowseAvrSize_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "avr-size folder location";
            browse(txtAvrSizeLocation);
        }

        private void browse(TextBox txt)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txt.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
