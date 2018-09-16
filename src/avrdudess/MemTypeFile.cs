/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

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
                if (sizeChanged != null)
                    sizeChanged(this, EventArgs.Empty);
            }
        }
    }
}
