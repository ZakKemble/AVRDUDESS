﻿/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2019 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

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
            Language.Translation.ApplyTranslation(this);

            string about = "";
            about += AssemblyData.title + Environment.NewLine;
            about += "Version " + AssemblyData.version.ToString() + Environment.NewLine;
            about += AssemblyData.copyright + Environment.NewLine;

            lblAbout.Text = about;
        }

        private void lnkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://zakkemble.net");
        }
    }
}
