AVRDUDESS - A GUI for AVRDUDE
=============================

This is a GUI for AVRDUDE (http://savannah.nongnu.org/projects/avrdude).

http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/

![AVRDUDESS pic](https://github.com/zkemble/AVRDUDESS/raw/master/images/avrdudess.png "")

Windows:
--------
Requires .NET Framework 2.0 SP1. On Windows 10 and maybe 8 and 8.1 you'll probably get a prompt about installing .NET Framework 3.5 (which includes .NET 2.0), click install and it will do the rest for you.\
If you get some other error message then you can download .NET 3.5 from https://www.microsoft.com/en-gb/download/details.aspx?id=21

Linux & Mac OS X:
-----------------
Can be ran using Mono (http://www.mono-project.com).\
Has not been tested on OS X, but should work.

Installing on Ubuntu 18.04:
---------------------------
Install Mono (this is the minimum required, you can do mono-complete for a full install):

    sudo apt-get install libmono-system-windows-forms4.0-cil

On older versions of Ubuntu you might need to use `libmono-winforms2.0-cil` instead.

Install AVRDUDE and AVR-GCC (for avr-size):

    sudo apt-get install avrdude gcc-avr

Run AVRDUDESS with mono, you might have to run as root (sudo) so that AVRDUDE can access ports if you haven't changed any permissions or rules.d stuff:

    mono avrdudess.exe

--------

Zak Kemble

contact@zakkemble.net
