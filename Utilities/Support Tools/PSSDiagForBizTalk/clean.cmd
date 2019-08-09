copy *Settings.xml output\
move BTSNetMon*.cap output\
del output\##*
@ECHO OFF
del BTSHAT.vbs
del BTSHAT0.vbs
del XLANGS.exe
del TerminateDebugger.exe
del TempWmicBatchFile.bat
set j=0;
:TOP1
PING 1.1.1.1 -n 1 -w 30000 >NUL
for /d %%a in (MBVOutput*) do move "%%a" output\
set /a j+=1
if %j%==36 exit
if exist MBVOutput* GOTO TOP1



