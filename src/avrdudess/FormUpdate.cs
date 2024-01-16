// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormUpdate : Form
    {
        private readonly string address;
        private Timer tmr;

        public event EventHandler OnSkipVersion;

        public FormUpdate(UpdateData updateData)
        {
            InitializeComponent();

            Icon = AssemblyData.icon;

            btnUpdate.Enabled = false;
            btnSkip.Enabled = false;
            btnLater.Enabled = false;

            lblCurrentVersion.Text = updateData.currentVersion.ToString();

            string info = "";
            foreach (UpdateReleaseData release in updateData.releases)
            {
                //if (release.version.CompareTo(updateData.currentVersion) > 0) // Only show change logs for newer versions
                //{
                    info += string.Format(
                        "v{0} ({1}){2}{3}{4}{5}",
                        release.Version.ToString(),
                        release.Date.ToLocalTime().ToLongDateString(),
                        Environment.NewLine,
                        release.info,
                        Environment.NewLine,
                        Environment.NewLine
                        );
                //}
            }

            address = updateData.updateAddr;

            // Make sure end lines are in the correct format (probably not needed...)
            txtUpdateInfo.Text = info.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            lblNewVersion.Text = $"{updateData.Latest.Version} ({updateData.Latest.Date.ToLocalTime().ToLongDateString()})";
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            Language.Translation.apply(this);

            // 2 second timer so that a button is not accidentally clicked when the form pops up
            tmr = new Timer();
            tmr.Interval = 2000;
            tmr.Tick += Tmr_Tick;
            tmr.Start();

            txtUpdateInfo.Select(0, 0);
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
            Util.openURL(address);
            Close();
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            OnSkipVersion?.Invoke(this, new EventArgs());
            Close();
        }

        private void FormUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tmr.Enabled)
                e.Cancel = true;
        }
    }
}
