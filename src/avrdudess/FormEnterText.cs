/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2018 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormEnterText : Form
    {
        public string inputText
        {
            get { return txtText.Text; }
            set { txtText.Text = value; }
        }

        public FormEnterText(string title, string prefillText)
        {
            InitializeComponent();
            Icon = AssemblyData.icon;
            Text = title;
            txtText.Text = prefillText;
        }

        private void FormEnterText_Load(object sender, System.EventArgs e)
        {
            Language.Translation.apply(this);
        }
    }
}
