// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2019-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Icon = AssemblyData.icon;
        }

        private void FormAbout_Load(object sender, System.EventArgs e)
        {
            Language.Translation.Apply(this);

            string about = "";
            about += AssemblyData.title + Environment.NewLine;
            about += $"Version {AssemblyData.version} ({getBuildDate()}){Environment.NewLine}";
            about += AssemblyData.copyright + Environment.NewLine;

            lblAbout.Text = about;
        }

        private void lnkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.openURL(((LinkLabel)sender).Text);
        }

        private void pbDonate_Click(object sender, EventArgs e)
        {
            Util.openURL("https://donate.zakkemble.net/avrdudess/");
        }

        private static string getBuildDate() // Based on John Leidegren's way on StackOverflow
        {
            var version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            var buildDate = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build));
            return buildDate.ToString("dd-MMM-yyyy").Split(' ')[0];
        }
    }
}
