@echo off

set /p apkName=Enter the name of the APK file to install (including the .apk extension): 

echo Installing %apkName%...
adb install %apkName%

echo Done!
pause
