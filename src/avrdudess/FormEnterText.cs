// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2018-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

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
