echo off

REM ### USAGE: After app creation, please clean up all the binding files manually.

REM #######################################################################################
REM #######################################################################################
REM 			YOU HAVE TO EDIT THIS SECTION 
REM #######################################################################################
REM #######################################################################################

REM ### The folder where the bindings are located. Fill in the correct value of BINDING_LOC
REM ### for your application and then remove the REM.
SET BINDINGS_LOC=.

REM ### Name of the application to import the bindings to
set APPLICATION_NAME=SelectiveBindingImport

REM ### Error log name and location
REM SET ERROR_LOG=%temp%\BindingImportLog.txt
SET ERROR_LOG=.\BindingImportLog.txt

echo %DATE% %TIME% Starting Selective Binding Import Script >> %ERROR_LOG%

REM #######################################################################################
REM #######################################################################################
REM 			YOU TYPICALLY WONT NEED TO EDIT THIS SECTION
REM #######################################################################################
REM #######################################################################################

setlocal

REM ### For testing, set BTAD_* environment variables.
REM set BTAD_ChangeRequestAction=Create
REM set BTAD_InstallMode=Import
REM set BTAD_HostClass=ConfigurationDb
REM set ENVIRONMENT=PRODUCTION

REM ### If the user did not remove the REM in the SET BINDING_LOC line, the script should not 
REM ### do anything.
if not defined BINDINGS_LOC ( 
	echo %DATE% %TIME% script not initialised >> %ERROR_LOG%
	echo %DATE% %TIME% set the BINDING_LOC variable in the script >> %ERROR_LOG%
	goto end
)

if not defined APPLICATION_NAME ( 
	echo %DATE% %TIME%: script not initialised >> %ERROR_LOG%
	echo %DATE% %TIME%: set the APPLICATION_NAME variable in the script >> %ERROR_LOG%
	goto end
)

REM ### Default locations under %BINDING_LOC% where the binding files for each environment are
REM ### located.
SET DEVELOP=Develop
SET TEST=Test
SET STAGING=Staging
SET PRODUCTION=Production

REM ### This if statement executes post import. If the install has already happened, the 
REM ### binding files would have been installed in the expected location, and can be imported. 
REM ### If the install has not already happened, the binding files will not be found and this 
REM ### step will be skipped. There is a danger that there may be existing binding files in the 
REM ### folder - which will get applied. 
if "%BTAD_ChangeRequestAction%"=="Create" (
  if "%BTAD_InstallMode%"=="Import" (
    if "%BTAD_HostClass%"=="ConfigurationDb" (
      @echo %DATE% %TIME% Post import
      call :ImportBindingsBasedOnContext
    )
  )
)

REM ### This if statement executes post install. If the import has already happened, the application
REM ### will exist and the bindings import will succeed. If the import has not happened, the bindings
REM ### import will fail.
REM if "%BTAD_ChangeRequestAction%"=="Update" (
REM    if "%BTAD_InstallMode%"=="Install" (
REM     if "%BTAD_HostClass%"=="BizTalkHostInstance" (
REM       REM ### Remove the following line, and add your code here 
REM       @echo %DATE% %TIME% Post install
REM       call :ImportBindingsBasedOnContext	
REM     )
REM   )
REM )

goto :EOF

:ImportBindingsBasedOnContext

    if 	%ENVIRONMENT%==DEVELOP    ( call :ImportBindings %DEVELOP%) 
    if 	%ENVIRONMENT%==TEST       ( call :ImportBindings %TEST% )
    if 	%ENVIRONMENT%==STAGING    ( call :ImportBindings %STAGING% )
    if 	%ENVIRONMENT%==PRODUCTION ( call :ImportBindings %PRODUCTION% )

goto :EOF

REM ### Depending on the environment, location, we will load the correct binding file.
REM ###
REM ### Arg 1: %1	The environment (develop, test etc.)
REM ###
:ImportBindings
  @echo How do I test if %1 is defined??
  @echo Environment %1 >> %ERROR_LOG%

  if EXIST %BINDINGS_LOC%\%1% (
    REM ### Import Develop environment bindings
    for %%I in (%BINDINGS_LOC%\%1%\*.*) do (
      @echo %%I >> %ERROR_LOG%
      BTSTask ImportBindings -ApplicationName:%APPLICATION_NAME% -Source:"%%I" >> %ERROR_LOG%
    )
  ) ELSE (
      echo %DATE% %TIME%: Binding file folder not found >> %ERROR_LOG%
  )

goto :EOF

:end
setlocal
