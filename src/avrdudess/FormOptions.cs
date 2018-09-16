/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
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

        public string language
        {
            get { return (string)cbLanguage.SelectedValue; }
            set { cbLanguage.SelectedValue = value; }
        }

        #endregion

        public FormOptions()
        {
            InitializeComponent();

            Icon = AssemblyData.icon;

            cbLanguage.Items.Clear();

            // TODO a
            Dictionary<string, string> a = Language.Translation.getLanguages();

            cbLanguage.DataSource = new BindingSource(a, null);
            cbLanguage.DisplayMember = "Value";
            cbLanguage.ValueMember = "Key";
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            Language.Translation.ApplyTranslation(this);
        }

        private void btnBrowseAvrdude_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = Language.Translation.get("_AVRDUDE_LOCATION");
            browse(txtAvrdudeLocation);
        }

        private void btnBrowseAvrdudeConf_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = Language.Translation.get("_AVRDUDECONF_LOCATION");
            browse(txtAvrdudeConfLocation);
        }

        private void btnBrowseAvrSize_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = Language.Translation.get("_AVRSIZE_LOCATION");
            browse(txtAvrSizeLocation);
        }

        private void browse(TextBox txt)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txt.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
