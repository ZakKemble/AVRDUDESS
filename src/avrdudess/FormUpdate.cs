/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormUpdate : Form
    {
        private string address;
        private Action onSkipVersion;
        private Timer tmr;

        public FormUpdate(UpdateData updateData, Action onSkipVersion)
        {
            InitializeComponent();

            Icon = AssemblyData.icon;

            btnUpdate.Enabled = false;
            btnSkip.Enabled = false;
            btnLater.Enabled = false;

            lblCurrentVersion.Text = updateData.currentVersion.ToString();

            // Make sure end lines are in the correct format (probably not needed...)
            txtUpdateInfo.Text = updateData.updateInfo.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);

            address = updateData.updateAddr;
            this.onSkipVersion = onSkipVersion;

            lblNewVersion.Text = string.Format(
                "{0} ({1})",
                updateData.newVersion.ToString(),
                new DateTime(1970, 1, 1).AddSeconds(updateData.newVersionDate).ToLocalTime().ToLongDateString()
                );
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            Language.Translation.ApplyTranslation(this);

            // 2 second timer so that a button is not accidentally clicked when the form pops up
            tmr = new Timer();
            tmr.Interval = 2000;
            tmr.Tick += Tmr_Tick;
            tmr.Start();
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            btnUpdate.Enabled = true;
            btnSkip.Enabled = true;
            btnLater.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(address);
            Close();
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            if(onSkipVersion != null)
                onSkipVersion();
            Close();
        }

        private void FormUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tmr.Enabled)
                e.Cancel = true;
        }
    }
}
