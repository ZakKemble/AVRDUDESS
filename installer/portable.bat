@echo off
cd /d %~dp0

SET source="..\src\avrdudess\bin\Release\"
SET dest="portable\"

mkdir %dest%Languages

copy %source%avrdudess.exe %dest%
copy %source%avrdudess.exe.config %dest%
copy %source%avrdude.exe %dest%
copy %source%avr-size.exe %dest%
copy %source%avrdude.conf %dest%
:: copy %source%config.xml %dest%
copy %source%presets.xml %dest%
copy %source%bits.xml %dest%
:: copy %source%portable.txt %dest%
copy %source%Languages\*.xml %dest%Languages
copy "..\Changelog.txt" %dest%
copy "..\Credits.txt" %dest%
copy "..\License.txt" %dest%
copy "..\README.md" %dest%
copy "..\TODO.txt" %dest%

:: copy /y NUL %dest%portable >NUL
echo Y> %dest%portable.txt

echo Done

pause
