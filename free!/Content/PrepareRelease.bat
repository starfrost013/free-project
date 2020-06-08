::PREPARE FOR RELEASE 
::© 2019-12-22 [Modified 2020-06-02] AVANT-GARDÉ EYES

@echo off
title Preparing free! for release...

echo Preparing free for release. Please stand by...this may take a few seconds.

mkdir FreeRelease

robocopy . .\FreeRelease\ /S /E /XD /MIR

echo Deleting not needed files...
echo.

del /s .\FreeRelease\Level1IntroConcept.png
del /s .\FreeRelease\Levels\Testdebugmodexmloutput2.xml
del /s .\FreeRelease\Levels\Testdebugmodexmloutput3.xml
del /s .\FreeRelease\Levels\Testdebugmodexmloutput4.xml
del /s .\FreeRelease\Levels\Testdebugmodexmloutput5.xml
del /s .\FreeRelease\*.txt
del /s .\FreeRelease\*.pdn
del /s .\FreeRelease\*.zip
del /s .\FreeRelease\*.pdb
del /s .\FreeRelease\*.bat
del /s .\FreeRelease\free!.exe.config
del /s /q .\FreeRelease\FreeRelease\*.*
rd .\FreeRelease\FreeRelease\Audio
rd .\FreeRelease\FreeRelease

echo Prepared for release. Press any key to exit.
pause >nul
exit
