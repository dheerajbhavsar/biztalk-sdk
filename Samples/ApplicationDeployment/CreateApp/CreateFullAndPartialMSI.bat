@REM
@REM File: CreateFullAndPartialMSI.bat
@REM
@REM Summary: Creates a full MSI and a partial MSI.
@REM
@REM This sample creates a new application, adds resources into it, and exports 
@REM the application into an MSI file. It also uses a "resource spec" and creates
@REM a partial MSI. 
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
echo on

@setlocal

set BASE_LOCATION=.
set LOG_LOCATION=.\LogFile.txt

echo ##################################################################################### >> %LOG_LOCATION%
echo         %DATE% %TIME% Running CreateFullAndPartialMSI.bat >> %LOG_LOCATION%
echo ##################################################################################### >> %LOG_LOCATION%

REM #### Create a new application - if this fails for some reason (maybe an application with
REM #### the same name already exists, in which case we dont want to import into it, and then
REM #### delete it (see last lines of code) ) terminate.
BTSTask AddApp -ApplicationName:CreateApplicationSample >> %LOG_LOCATION%
if NOT %errorlevel%==0 (
  echo Error Creating Application >> %LOG_LOCATION%
  goto :end
)

REM #### Add resources into the application

REM #### Add assemblies. If any of these steps fail, we terminate.
BTSTask AddResource -Source:.\dlls\Schemas.dll -ApplicationName:CreateApplicationSample -Type:BizTalkAssembly -Options:GacOnAdd,GacOnInstall >> %LOG_LOCATION%
BTSTask AddResource -Source:.\dlls\Maps.dll -ApplicationName:CreateApplicationSample -Type:BizTalkAssembly -Options:GacOnAdd,GacOnInstall >> %LOG_LOCATION%
BTSTask AddResource -Source:.\dlls\Orchestrations.dll -ApplicationName:CreateApplicationSample -Type:BizTalkAssembly -Options:GacOnAdd,GacOnInstall >> %LOG_LOCATION%

REM #### Add scripts
BTSTask AddResource -Source:.\Scripts\PreProcScript.bat -Destination:.\PreProcScript.bat -ApplicationName:CreateApplicationSample -Type:PreProcessingScript  >> %LOG_LOCATION%


REM #### Importing Bindings
BTSTask ImportBindings -Source:.\Bindings\CreateApplicationSampleBindings.xml -ApplicationName:CreateApplicationSample >> %LOG_LOCATION%

REM #### For record keeping purposes, create a manifest of the MSI
BTSTask ListApp -ApplicationName:CreateApplicationSample -ResourceSpec:.\ResourceSpecs\AppManifest.xml

REM #### Create a complete MSI
BTSTask ExportApp -ApplicationName:CreateApplicationSample -Package:.\CreateApplicationSample.msi >> %LOG_LOCATION%

REM #### Create a partial MSI with just the orchestration
BTSTask ExportApp -ApplicationName:CreateApplicationSample -Package:.\CreateApplicationSamplePartial.msi -ResourceSpec:.\ResourceSpecs\ResourceSpecPartial.xml >> %LOG_LOCATION%

REM #### Delete Application
BTSTask RemoveApp -ApplicationName:CreateApplicationSample >> %LOG_LOCATION%

:end