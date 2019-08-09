@REM
@REM File: RemoveApp.bat
@REM
@REM This samples indicates the 3 steps needed to remove a Biztalk application from a
@REM single machine. Note that the post processing script in the application does the
@REM third post processing step of cleaning up the machine, which is left empty in this
@REM script
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

echo off

setlocal

REM #####################################################################################
REM ####          Step 1: Delete the application from the config db
REM #####################################################################################

REM #### Attempt to remove the application. If the application is running, this step will
REM #### fail. This will cause the script to fail.
BTSTask RemoveApp -ApplicationName:CreateApplicationSample
if NOT %errorlevel%==0 goto :end

REM #####################################################################################
REM ####          Step 2: Uninstall the application from the local machine
REM #####################################################################################

REM #### Since the removal succeeded, uninstall the application. Note that using msiexec
REM #### is not a supported method to unistall applications, always use BTSTask. If 
REM #### multiple MSI's were installed for an application, BTSTask will uninstall them all.
REM #### Also, if this script is run on multiple machines, the previous section needs to be
REM #### removed on all other machines, as the application would already have been removed
REM #### the first time the script ran.
BTSTask UninstallApp -ApplicationName:CreateApplicationSample

REM #####################################################################################
REM ####          Step 3: Reminder: Clean any left over components
REM #####################################################################################

REM This portion is a reminder to the user of the third step which is 
REM accomplished by a post uninstall script in the application MSI. This step cleans up 
REM anything the MSI is not designed to clean up, but should be cleaned up.

setlocal
:end