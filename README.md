AVRDUDESS - A GUI for AVRDUDE
=============================

This is a GUI for AVRDUDE (http://savannah.nongnu.org/projects/avrdude).

    http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/

Windows:
--------
Requires .NET Framework 2.0 SP1 or newer (http://www.microsoft.com/en-gb/download/details.aspx?id=16614).

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