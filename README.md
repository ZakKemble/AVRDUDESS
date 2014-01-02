AVRDUDESS - A GUI for AVRDUDE
=============================

This is a GUI for AVRDUDE (http://savannah.nongnu.org/projects/avrdude).

http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/

Windows:
--------
Requires .NET Framework 2.0 SP1 or newer (http://www.microsoft.com/en-gb/download/details.aspx?id=16614).
Latest .NET can be found here - http://www.microsoft.com/net

AVRDUDE requires LibUSB
LibUSB should really be installed the normal way as a driver for a LibUSB device, but if you don't have any such devices then you will need to download this - http://downloads.sourceforge.net/libusb-win32/libusb-win32-bin-1.2.6.0.zip

Extract libusb-win32-bin-1.2.6.0/bin/x86/libusb_x86.dll to where you have avrdude.exe placed and rename libusb_x86.dll to libusb.dll

Linux & Mac OS X:
-----------------
Can be ran using Mono (http://www.mono-project.com).

Has not been tested on OS X, but should work.

Installing on Ubuntu 13.10:
---------------------------
Install Mono (this is the minimum required, you can do mono-complete for a full install)

    sudo apt-get install libmono-winforms2.0-cil

Install AVRDUDE

    sudo apt-get install avrdude

Run AVRDUDESS with mono, you might have to run as root (sudo) so AVRDUDE runs as root if you havn't changed any rules.d stuff

    mono avrdudess.exe

--------

Zak Kemble

contact@zakkemble.co.uk