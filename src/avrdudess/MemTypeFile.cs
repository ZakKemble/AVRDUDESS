// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2014-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Windows.Forms;

namespace avrdudess
{
    class MemTypeFile
    {
        private readonly TextBox txtFileLocation;
        private readonly Avrsize avrsize;
        public int size { get; private set; }
        public event EventHandler sizeChanged;
        public string location
        {
            get { return txtFileLocation.Text; }
        }

        public MemTypeFile(TextBox txtFileLocation, Avrsize avrsize)
        {
            this.txtFileLocation = txtFileLocation;
            this.avrsize = avrsize;
            size = Avrsize.INVALID;

            this.txtFileLocation.TextChanged += txtMemFile_TextChanged;
        }

        public void updateSize()
        {
            txtMemFile_TextChanged(txtFileLocation, EventArgs.Empty);
        }

        private void txtMemFile_TextChanged(object sender, EventArgs e)
        {
            int newSize = avrsize.getSize(location);
            if (newSize != size)
            {
                size = newSize;
                sizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
