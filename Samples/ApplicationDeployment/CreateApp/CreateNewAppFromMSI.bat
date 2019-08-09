@REM
@REM File: CreateNewAppFromMSI.bat
@REM
@REM Summary: This script demonstrates the set up of a new application on a single box.
@REM
@REM --------------------------------------------------------------------
@REM This file is part of the Microsoft BizTalk Server SDK
@REM
@REM Copyright (c) Microsoft Corporation. All rights reserved.
@REM
@REM This source code is intended only as a supplement to Microsoft
@REM BizTalk Server release and/or on-line documentation. See these
@REM other materials for detailed information regarding Microsoft code samples.
@REM
@REM THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
@REM KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
@REM IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
@REM PARTICULAR PURPOSE.
@REM --------------------------------------------------------------------

@setlocal

@echo off

set LOG_LOCATION=.\LogFile.txt

echo ##################################################################################### >> %LOG_LOCATION%
echo         %DATE% %TIME% Running CreateNewAppFromMSI.bat >> %LOG_LOCATION%
echo ##################################################################################### >> %LOG_LOCATION%

REM #### Install the MSI onto the local box
msiexec /i "%~dp0CreateApplicationSample.msi" TARGETDIR="%cd%" /qn >> %LOG_LOCATION%

REM #### Import the MSI into the default configuration database. 

REM #### Create the application
BTSTask AddApp -ApplicationName:CreateApplicationSample >> %LOG_LOCATION%

REM #### Import the MSI into the application
BTSTask ImportApp -Package:"%~dp0CreateApplicationSample.msi" -ApplicationName:CreateApplicationSample >> %LOG_LOCATION%

@endlocal