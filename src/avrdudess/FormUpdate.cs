/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormUpdate : Form
    {
        private string address;
        private UpdateCheck updater;

        public FormUpdate()
        {
            InitializeComponent();

            Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Text = "Update available";
        }

        public void doUpdateMsg(string currentVersion, string newVersion, string msg, string address, UpdateCheck updater)
        {
            // Make sure end lines are in the correct format
            msg = msg.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);

            lblCurrentVersion.Text = currentVersion;
            lblNewVersion.Text = newVersion;
            txtUpdateInfo.Text = msg;

            this.address = address;
            this.updater = updater;

            ShowDialog();
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
            updater.skipVersion();
            Close();
        }
    }
}
