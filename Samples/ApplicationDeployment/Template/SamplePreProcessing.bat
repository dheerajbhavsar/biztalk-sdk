@setlocal

REM ### For testing, set BTAD_* environment variables.
REM set LogFile=C:\ScriptLog.txt
REM set BTAD_ChangeRequestAction=Create
REM set BTAD_InstallMode=Import
REM set BTAD_HostClass=ConfigurationDb

REM ### For verifying BTAD_* environment variables when script is called.
REM echo Script log %DATE% %TIME% > "%LogFile%"
REM echo BTAD_ChangeRequestAction=%BTAD_ChangeRequestAction% >> "%LogFile%"
REM echo BTAD_InstallMode=%BTAD_InstallMode% >> "%LogFile%"
REM echo BTAD_HostClass=%BTAD_HostClass% >> "%LogFile%"

set LogFile=c:\SampleLogOut.txt

REM ### Pre import part of the script called for a new application.
if "%BTAD_ChangeRequestAction%"=="Create" (
  if "%BTAD_InstallMode%"=="Import" (
    if "%BTAD_HostClass%"=="ConfigurationDb" (
	REM ### Remove the following line, and add your code here 
	echo "Pre import part of the script called for a new application" >> %LogFile%
    )
  )
)

REM ### Pre import part of the script called for an existing application
if "%BTAD_ChangeRequestAction%"=="Update" (
  if "%BTAD_InstallMode%"=="Import" (
    if "%BTAD_HostClass%"=="ConfigurationDb" (
	REM ### Remove the following line, and add your code here 
	echo "Pre import part of the script called for an existing application" >> %LogFile%
    )
  )
)

REM ### Pre install part of the script called for a new or existing application
if "%BTAD_ChangeRequestAction%"=="Update" (
  if "%BTAD_InstallMode%"=="Install" (
    if "%BTAD_HostClass%"=="BizTalkHostInstance" (
	REM ### Remove the following line, and add your code here 
	echo "Pre install part of the script called for an existing application" >> %LogFile%
    )
  )
)

REM ### Pre uninstall part of the script called for an existing application
if "%BTAD_ChangeRequestAction%"=="Delete" (
  if "%BTAD_InstallMode%"=="Uninstall" (
    if "%BTAD_HostClass%"=="BizTalkHostInstance" (
	REM ### Remove the following line, and add your code here 
	echo "Pre uninstall part of the script called for an existing application" >> %LogFile%
    )
  )
)

@endlocal
