@if "%_echo%"=="" echo off

@rem
@rem Manage BizTalk 2006 Release Bits Tracing
@rem

echo trace 2.0 - Manage BizTalk 2006 Release Bits Tracing.
echo trace.cmd %1 %2 %3 %4 %5 %6
@rem
@rem Jump to where we handle usage
@rem 
if /I "%1" == "help" goto Usage
if /I "%1" == "-help" goto Usage
if /I "%1" == "/help" goto Usage
if /I "%1" == "-h" goto Usage
if /I "%1" == "/h" goto Usage
if /I "%1" == "-?" goto Usage
if /I "%1" == "/?" goto Usage

@rem
@rem Set Tools environment variable
@rem
if /I %1. == -tools. shift&goto SetToolsPath
if /I %1. == /tools. shift&goto SetToolsPath
goto EndSetToolsPath
:SetToolsPath
if /I not "%~1" == "" goto :DoSetToolPath
echo ERROR: Argument '-tools' specified without argument for Trace Tools folder.
echo Usage example: trace -tools "C:\Program Files\Common Files\BizTalk Adapter Trace Utility"
goto :eof
:DoSetToolPath
if not exist %1\tracelog.exe goto ToolsNotFound
if not exist %1\tracefmt.exe goto ToolsNotFound
echo Setting TRACE_TOOL_SEARCH_PATH to %1
set TRACE_TOOL_SEARCH_PATH=%~1&shift
goto :eof
:ToolsNotFound
echo ERROR: Could not find trace tools in folder %1
goto :eof
:EndSetToolsPath

@rem
@rem Set TraceFormat environment variable
@rem
if /I "%1" == "-path" shift&goto SetPath
if /I "%1" == "/path" shift&goto SetPath
goto EndSetPath
:SetPath
if /I not "%~1" == "" goto DoSetPath
echo ERROR: Argument '-path' specified without argument for TraceFormat folder.
echo Usage example: trace -path x:\symbols.pri\TraceFormat
goto :eof
:DoSetPath
echo Setting TRACE_FORMAT_SEARCH_PATH to '%1'
set TRACE_FORMAT_SEARCH_PATH=%1&shift
goto :eof
:EndSetPath

@rem
@rem Set Tool paths
@rem
setlocal
if defined TRACE_TOOL_SEARCH_PATH goto SetToolsPathExplicit
set MZRT_TRACELOG_EXE=tracelog.exe
set MZRT_TRACEFMT_EXE=tracefmt.exe
goto EndSetToolsPath
:SetToolsPathExplicit
set MZRT_TRACELOG_EXE="%TRACE_TOOL_SEARCH_PATH%\tracelog.exe"
echo Using %MZRT_TRACELOG_EXE% to log traces
set MZRT_TRACEFMT_EXE="%TRACE_TOOL_SEARCH_PATH%\tracefmt.exe"
echo Using %MZRT_TRACEFMT_EXE% to format traces
:EndSetToolsPath

@rem
@rem Format binary log file to text file
@rem
if /I "%1" == "-format" shift&goto FormatFile
if /I "%1" == "/format" shift&goto FormatFile
goto EndFormatFile
:FormatFile
if defined TRACE_FORMAT_SEARCH_PATH goto DoFormatFile
echo ERROR: Argument '-format' specified without running 'trace -path' first.
echo Usage example: trace -path x:\symbols.pri\TraceFormat
echo                trace -format ('btstrace.bin' to text file 'btstrace.txt')
goto :eof
:DoFormatFile
set mqBinaryLog=btstrace
if /I not "%1" == "" set mqBinaryLog=%1&shift
echo Formatting binary log file '%mqBinaryLog%' to 'btstrace.txt'.
call %MZRT_TRACEFMT_EXE% %mqBinaryLog% -o btstrace.txt
set mqBinaryLog=
goto :eof
:EndFormatFile


@rem
@rem Process the tracing change
@rem 
if /I "%1" == "-change" shift&goto ChangeTrace
if /I "%1" == "/change" shift&goto ChangeTrace
goto EndChangeTrace

:ChangeTrace
@rem
@rem Process the module
@rem 
set ModuleGuid=

if /I "%1" == "XLANG" set ModuleGuid=186cc4a4,abab,abab,abab,abababababab
if /I "%1" == "ProcessContainer" set ModuleGuid=186cc4a5,abab,abab,abab,abababababab
if /I "%1" == "NTSVC" set ModuleGuid=186cc4a6,abab,abab,abab,abababababab
if /I "%1" == "Agent" set ModuleGuid=29da55e6,e688,425f,a52c,79f840b8C89
if /I "%1" == "Messaging" set ModuleGuid=9d0e4402,4cce,4536,83fa,4a5040674ad6
if /I "%1" == "HTTPReceive" set ModuleGuid=ff1bf348,eff3,4e72,baa4,0bb90a2a58ad
if /I "%1" == "SOAPTransmitter" set ModuleGuid=6796973A,3ABB,30F7,A30A,A48797B885BD
if /I "%1" == "SOAPReceive" set ModuleGuid=C8F975BF,5C97,4f17,9CA1,F894BC258761
if /I "%1" == "DBAccessor" set ModuleGuid=442B65E4,5BA1,4e09,B620,5AE937F02995
if /I "%1" == "SchemaCache" set ModuleGuid=9d0e4403,4cce,4536,83fa,4a5040674ad6
if /I "%1" == "UTIL" set ModuleGuid=9d0e4406,4cce,4536,83fa,4a5040674ad6
if /I "%1" == "ERRORHANDLER" set ModuleGuid=9d0e4409,4cce,4536,83fa,4a5040674ad6
if /I "%1" == "MGDCOMPS" set ModuleGuid=689E4209,67A2,409b,8303,0905CE67A931
if /I "%1" == "MIME" set ModuleGuid=9d0e4408,4cce,4536,83fa,4a5040674ad6
if /I "%1" == "MsmqtGENERAL" set ModuleGuid=E20CAC3D,9697,4c1c,B3CB,8C04F6677B9E   
if /I "%1" == "MsmqtAPPSTREAM" set ModuleGuid=8DA99B95,9FCC,4c78,8279,F907531777B5   
if /I "%1" == "MsmqtOUTSTREAM" set ModuleGuid=9A421B40,5CA0,48c9,870B,31B3BEBF1BC4   
if /I "%1" == "MsmqtINSTREAM" set ModuleGuid=A0BC8355,D6EE,4133,9ABB,31FBB0EBDF9F   
if /I "%1" == "MsmqtNETWORKING" set ModuleGuid=0D03E3E7,528C,4f08,97BB,38146CDF9461   
if /I "%1" == "MsmqtMESSAGES" set ModuleGuid=EC75918C,E7A4,4b5a,8A20,6FCAB06D6D3A   
if /I "%1" == "MsmqtTRACE" set ModuleGuid=1DC21FBF,4C4A,4bb2,94D4,AC8A2DA161C5   
if /I "%1" == "MsmqtSECURITY" set ModuleGuid=90e950bb,6ace,4676,98e0,f6cdc1403670  
if /I "%1" == "MsmqtDS" set ModuleGuid=5DC62C8C,BDf2,45A1,A06F,0C38CD5AF627  
if /I "%1" == "BTSTicket" set ModuleGuid=2bfdd646,b8a0,4731,8e5a,9a9ddb84381a 
if /I "%1" == "InfoCache" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a00
if /I "%1" == "SSOAdmin" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a01  
if /I "%1" == "SSOAdminServer" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a02   
if /I "%1" == "SSOLookup" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a03
if /I "%1" == "SSOLookupServer" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a04
if /I "%1" == "SSOMapper" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a05
if /I "%1" == "SSOMappingServer" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a06
if /I "%1" == "SSOSS" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a07
if /I "%1" == "BTSSSO" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a08
if /I "%1" == "ssocheck" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a09
if /I "%1" == "ssoclient" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a0a
if /I "%1" == "ssoconfig" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a0b
if /I "%1" == "ssomanage" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a0c
if /I "%1" == "SSOConfigStore" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d9901
if /I "%1" == "SSOCSServer" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d3a99
if /I "%1" == "SSOCSTX" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d9902
if /I "%1" == "ImportExport" set ModuleGuid=4100fd26,a656,45c6,9b81,f7da488d9903
if /I "%1" == "SSOConfiguration" set ModuleGuid=B156D071,308C,4931,93C3,199E167C4834
if /I "%1" == "WindowsSharepointServicesAdapter" set ModuleGuid=BA7DAD66,5FC8,4a24,A27E,D9F68FD67C3A
if /I "%1" == "BamTddsService" set ModuleGuid=FDA0D9B6,CF63,4096,8912,C008E42A3B86
if /I "%1" == "BamInterceptors" set ModuleGuid=7DED31DE,B123,42A6,889C,090A60ED3CBB
shift

if /I "%ModuleGuid%" == "" goto Usage

@rem
@rem Process the level (-none / -low / -medium / -high / -all / -flag
@rem 
set TraceLevel=
if /I "%1" == "-none" set TraceLevel=0x0
if /I "%1" == "-low" set TraceLevel=0x1
if /I "%1" == "-medium" set TraceLevel=0x3
if /I "%1" == "-high" set TraceLevel=0x7
if /I "%1" == "-all" set TraceLevel=0x7FFFFFFF
if /I "%1" == "-flag" shift & set TraceLevel=%1
 
shift
if /I "%TraceLevel%" == "" goto Usage

@rem
@rem Query if btstrace logger is running. If not, echo and exit
@rem 
echo Querying if btstrace logger is currently running...
call %MZRT_TRACELOG_EXE% -q btstrace
if ERRORLEVEL 1 goto LoggerNotRunning
echo btstrace logger is currently running, changing trace level...
goto EndQueryLogger


:LoggerNotRunning
echo.
echo.
echo btstrace logger is not currently running
echo Please invoke TRACE -start to start tracing 
goto :eof


:EndQueryLogger
@rem
@rem At this point if we have any argument it's an error
@rem 
if /I not "%1" == "" goto Usage


@rem
@rem Make the change
@rem 
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %TraceLevel% -guid #%ModuleGuid%
goto :eof
:EndChangeTrace


@rem
@rem Consume the -rt argument
@rem
set mqRealTime=
if /I "%1" == "-rt" shift&goto ConsumeRealTimeArgument
if /I "%1" == "/rt" shift&goto ConsumeRealTimeArgument
goto EndConsumeRealTimeArgument
:ConsumeRealTimeArgument
if defined TRACE_FORMAT_SEARCH_PATH goto DoConsumeRealTimeArgument
echo ERROR: Argument '-rt' specified without running 'trace -path' first.
echo Usage example: trace -path x:\symbols.pri\TraceFormat
echo                trace -rt (start RealTime logging/formatting at Error level)
goto :eof
:DoConsumeRealTimeArgument
echo Running btstrace trace in Real Time mode...
set mqRealTime=-rt -ft 1
:EndConsumeRealTimeArgument

@rem
@rem Handle the -stop argument
@rem 
if /I "%1" == "-stop" shift&goto HandleStopArgument
if /I "%1" == "/stop" shift&goto HandleStopArgument
goto EndHandleStopArgument
:HandleStopArgument
echo Stopping btstrace trace...
call %MZRT_TRACELOG_EXE% -stop btstrace
@rem
@rem  Now that btstrace has been -stopped, let's see if default tracing has started. If so reenable those components
@rem
call %MZRT_TRACELOG_EXE% -q BizTalkDefaultTrace
if ERRORLEVEL 1 goto DefaultLoggerNotRunning
echo BizTalkDefaultTracelogger is currently running, re-enable default providers...
set msgFlags=0x1
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #29da55e6-e688-425f-a52c-79f840cb8C89 >nul & @rem Agent
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #186cc4a4,abab,abab,abab,abababababab >nul & @rem    XLANG
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #186cc4a5,abab,abab,abab,abababababab >nul & @rem    ProcessContainer
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #186cc4a6,abab,abab,abab,abababababab >nul & @rem    NTSVC
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #9d0e4402,4cce,4536,83fa,4a5040674ad6 >nul & @rem    Messaging
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #ff1bf348,eff3,4e72,baa4,0bb90a2a58ad >nul & @rem    HTTPReceive
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #C8F975BF,5C97,4f17,9CA1,F894BC258761 >nul & @rem    SOAPReceive
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #442B65E4,5BA1,4e09,B620,5AE937F02995 >nul & @rem    DBAccessor
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #9d0e4403,4cce,4536,83fa,4a5040674ad6 >nul & @rem    SchemaCache
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #9d0e4406,4cce,4536,83fa,4a5040674ad6 >nul & @rem    UTIL
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #9d0e4408,4cce,4536,83fa,4a5040674ad6 >nul & @rem    MIME
call %MZRT_TRACELOG_EXE% -enable BizTalkDefaultTrace -flags %msgFlags%    -guid #9d0e4409,4cce,4536,83fa,4a5040674ad6 >nul & @rem    ERRORHANDLER
goto :eof

:DefaultLoggerNotRunning
echo.
echo BizTalkDefaultTrace logger is not currently running
goto :eof
:EndHandleStopArgument

@rem
@rem Consume the "-start" argument if it exists. Default is to start.
@rem 
echo Starting btstrace trace logging to 'btstrace.bin'...
if /I "%1" == "-start" shift
if /I "%1" == "/start" shift

@rem
@rem Process the file size argument if it exists. Default is error sequential no limit.
@rem 

set mqLogFileType=
set mqLogFileSize=
Set mqBinaryLog=BTSTrace
Set mqSeqNumber=
Set mqSeqCode=

if /I "%1" == "-cir" shift&goto ConsumeCircularArgument
if /I "%1" == "/cir" shift&goto ConsumeCircularArgument
goto EndConsumeCircularArgument
:ConsumeCircularArgument
set mqLogFileType=-cir
set mqLogFileSize=%1
shift
goto EndConsumeFileSizeArgument
:EndConsumeCircularArgument



if /I "%1" == "-rollover" shift&goto ConsumeRolloverArgument
if /I "%1" == "/rollover" shift&goto ConsumeRolloverArgument
goto EndConsumeRolloverArgument
:ConsumeRolloverArgument
set mqLogFileType=-newfile
set mqLogFileSize=%1
Set mqSeqCode=%%
Set mqSeqNumber=%%d
shift
goto EndConsumeFileSizeArgument
:EndConsumeRolloverArgument



if /I "%1" == "-seq" shift&goto ConsumeSequentialArgument
if /I "%1" == "/seq" shift&goto ConsumeSequentialArgument
goto EndConsumeSequentialArgument
:ConsumeSequentialArgument
set mqLogFileType=-seq
set mqLogFileSize=%1
shift
goto EndConsumeFileSizeArgument
:EndConsumeSequentialArgument

:EndConsumeFileSizeArgument


@rem
@rem Process the noise level argument if it exists. Default is error level.
@rem 

@
@rem Level for the peripherial components (not needed usually)
@
set OtherFlags=0x3
set SuperFlags=0x0

if /I "%1" == "-high" shift&goto ConsumeInfoArgument
if /I "%1" == "/high" shift&goto ConsumeInfoArgument
goto EndConsumeInfoArgument
:ConsumeInfoArgument
echo btstrace trace noise level is INFORMATION...
set mqFlags=0x7
set btFlags=0xBF
set msgFlags=0x7
set dbAccessorFlags=0x3
set mgdFlags=0xF
set ssoFlags=0x3
set wssFlags=0x1f
set admFlags=0x3
set tddsFlags=0x1f
set interceptorFlags=0x1f
goto EndConsumeNoiseLevelArgument
:EndConsumeInfoArgument

if /I "%1" == "-all" shift&goto ConsumeSuperArgument
if /I "%1" == "/all" shift&goto ConsumeSuperArgument
goto EndConsumeSuperArgument
:ConsumeSuperArgument
echo btstrace trace noise level is SUPER...
set mqFlags=0x7
set btFlags=0x7FFFFFFF
set SuperFlags=0x7
set msgFlags=0x7FFFFFFF
set dbAccessorFlags=0x7FFFFFFF
set mgdFlags=0x7FFFFFFF
set ssoFlags=0x7
set wssFlags=0x1f
set admFlags=0x7FFFFFFF
set tddsFlags=0x1f
set interceptorFlags=0x1f
goto EndConsumeNoiseLevelArgument
:EndConsumeSuperArgument

if /I "%1" == "-medium" shift&goto ConsumeWarningArgument
if /I "%1" == "/medium" shift&goto ConsumeWarningArgument
goto EndConsumeWarningArgument
:ConsumeWarningArgument
echo btstrace trace noise level is WARNING...
set mqFlags=0x3
set btFlags=0xB
set msgFlags=0x3
set dbAccessorFlags=0x3
set mgdFlags=0x3
set ssoFlags=0x3
set wssFlags=0x3
set admFlags=0x3
set tddsFlags=0x3
set interceptorFlags=0x3
goto EndConsumeNoiseLevelArgument
:EndConsumeWarningArgument

if /I "%1" == "-flag" shift&goto ConsumeFlagArgument
if /I "%1" == "/flag" shift&goto ConsumeFlagArgument
goto EndConsumeFlagArgument
:ConsumeFlagArgument
shift
echo btstrace trace noise level is %1...
set mqFlags=%1
set btFlags=%1
set msgFlags=%1
set dbAccessorFlags=%1
set mgdFlags=%1
set ssoFlags=%1
set wssFlags=%1
set admFlags=%1
set tddsFlags=%1
set interceptorFlags=%1
:EndConsumeFlagArgument

if /I "%1" == "-low" shift&goto ConsumeErrorArgument
if /I "%1" == "/low" shift&goto ConsumeErrorArgument
goto EndConsumeErrorArgument
:ConsumeErrorArgument
echo btstrace trace noise level is ERROR...
set mqFlags=0x1
set btFlags=0x9
set msgFlags=0x1
set dbAccessorFlags=0x1
set mgdFlags=0x1
set ssoFlags=0x1
set wssFlags=0x1
set admFlags=0x1
set tddsFlags=0x1
set interceptorFlags=0x1
goto EndConsumeNoiseLevelArgument
:EndConsumeErrorArgument

echo Default btstrace trace noise level is ERROR...
set mqFlags=0x1
set btFlags=0x9
set msgFlags=0x1
set dbAccessorFlags=0x1
set mgdFlags=0x1
set ssoFlags=0x1
set wssFlags=0x1
set admFlags=0x1
set tddsFlags=0x1
set interceptorFlags=0x1
goto EndConsumeNoiseLevelArgument

:EndConsumeNoiseLevelArgument

@rem
@rem At this point if we have any argument it's an error
@rem 
if /I not "%1" == "" goto Usage

@rem
@rem Query if btstrace logger is running. If so only update the flags and append to logfile.
@rem 
echo Querying if btstrace logger is currently running...
call %MZRT_TRACELOG_EXE% -q btstrace
if ERRORLEVEL 1 goto LoggerNotRunning
echo btstrace logger is currently running, appending new trace output...
set mqStartLogger=-enable
set mqOpenLogfile=-append
goto EndQueryLogger
:LoggerNotRunning
echo btstrace logger is not currently running, starting new logger...
set mqStartLogger=-start
set mqOpenLogfile=-f
:EndQueryLogger

@rem
@rem Start a new btstrace logger or update the existing one
@rem 

@rem Echo on

if not exist output mkdir output
call %MZRT_TRACELOG_EXE% %mqStartLogger% btstrace %mqRealTime% -flags %btFlags% %mqOpenLogfile% output\%mqBinaryLog%%mqSeqCode%%mqSeqNumber%.bin  %mqLogFileType% %mqLogFileSize% -guid #29da55e6-e688-425f-a52c-79f840cb8C89  & @rem Agent

::Pause

call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #186cc4a4,abab,abab,abab,abababababab >nul & @rem    XLANG
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #186cc4a5,abab,abab,abab,abababababab >nul & @rem    ProcessContainer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #186cc4a6,abab,abab,abab,abababababab >nul & @rem    NTSVC
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #9d0e4402,4cce,4536,83fa,4a5040674ad6 >nul & @rem    Messaging
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #ff1bf348,eff3,4e72,baa4,0bb90a2a58ad >nul & @rem    HTTPReceive
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #6796973A,3ABB,30F7,A30A,A48797B885BD >nul & @rem    SOAPTransmitter
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #C8F975BF,5C97,4f17,9CA1,F894BC258761 >nul & @rem    SOAPReceive
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %dbAccessorFlags%    -guid #442B65E4,5BA1,4e09,B620,5AE937F02995 >nul & @rem    DBAccessor
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #9d0e4403,4cce,4536,83fa,4a5040674ad6 >nul & @rem    SchemaCache
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #9d0e4406,4cce,4536,83fa,4a5040674ad6 >nul & @rem    UTIL
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #9d0e4408,4cce,4536,83fa,4a5040674ad6 >nul & @rem    MIME
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #9d0e4409,4cce,4536,83fa,4a5040674ad6 >nul & @rem    ERRORHANDLER
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #2bfdd646,b8a0,4731,8e5a,9a9ddb84381a >nul & @rem    BTSTicket
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mgdFlags%    -guid #689E4209,67A2,409b,8303,0905CE67A931 >nul & @rem    MGDCOMPS
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %admFlags%    -guid #9C48C5A8,C4EF,4112,8F75,6342415B70F0 >nul & @rem    WMI
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %admFlags%    -guid #0765FD8D,45B2,41a9,9BCE,E0A80CF6B911 >nul & @rem    AdminLib
@rem call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #251CFF54,B1EC,44c4,A97B,83BBA42A4E14 >nul & @rem    MMC

call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mqFlags%    -guid #E20CAC3D,9697,4c1c,B3CB,8C04F6677B9E >nul & @rem    MsmqtGENERAL
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mqFlags%    -guid #8DA99B95,9FCC,4c78,8279,F907531777B5 >nul & @rem    MsmqtAPPSTREAM - application stream processing
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mqFlags%    -guid #9A421B40,5CA0,48c9,870B,31B3BEBF1BC4 >nul & @rem    MsmqtOUTSTREAM - Outgoing stream processing
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mqFlags%    -guid #A0BC8355,D6EE,4133,9ABB,31FBB0EBDF9F >nul & @rem    MsmqtINSTREAM  - incoming stream processing
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mqFlags%    -guid #EC75918C,E7A4,4b5a,8A20,6FCAB06D6D3A >nul & @rem    MsmqtMESSAGES  - trace main message properties 
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %SuperFlags% -guid #1DC21FBF,4C4A,4bb2,94D4,AC8A2DA161C5 >nul & @rem    MsmqtTRACE     - tracing every routine entry and exit
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %mqFlags%    -guid #0D03E3E7,528C,4f08,97BB,38146CDF9461 >nul & @rem    MsmqtNETWORKING- networking operations
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %OtherFlags% -guid #90e950bb,6ace,4676,98e0,f6cdc1403670 >nul & @rem    MsmqtSECURITTY - security libraries
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %OtherFlags% -guid #5DC62C8C,BDf2,45A1,A06F,0C38CD5AF627 >nul & @rem    MsmqtDS - directory service libraries
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a00 >nul & @rem    InfoCache
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a01 >nul & @rem    SSOAdmin
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a02 >nul & @rem    SSOAdminServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a03 >nul & @rem    SSOLookup
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a04 >nul & @rem    SSOLookupServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a05 >nul & @rem    SSOMapper
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a06 >nul & @rem    SSOMappingServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a07 >nul & @rem    SSOSS
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a08 >nul & @rem    BTSSSO
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a09 >nul & @rem    ssocheck
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a0a >nul & @rem    ssoclient
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a0b >nul & @rem    ssoconfig
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a0c >nul & @rem    ssomanage
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d9901 >nul & @rem    SSOConfigStore
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d3a99 >nul & @rem    SSOCSServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d9902 >nul & @rem    SSOCSTX
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #4100fd26,a656,45c6,9b81,f7da488d9903 >nul & @rem    ImportExport
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %ssoFlags% -guid #B156D071,308C,4931,93C3,199E167C4834 >nul & @rem    SSOConfiguration
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %wssFlags% -guid #BA7DAD66,5FC8,4a24,A27E,D9F68FD67C3A >nul & @rem    WindowsSharepointServicesAdapter
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %tddsFlags% -guid #FDA0D9B6,CF63,4096,8912,C008E42A3B86 >nul & @rem    BamTddsService
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %interceptorFlags% -guid #7DED31DE,B123,42A6,889C,090A60ED3CBB >nul & @rem    BAM Interceptors

@ MQSAgent added 09/2016
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags% -guid #2F7FEF13,7A25,44B0,90F1,840E94FC5A9F >nul & @rem MQSAgent
call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags% -guid #689E4209,67A2,409b,8303,0905CE67A931 >nul & @rem    MGDCOMPS

set mqFlags=
set OtherFlags=
set SuperFlags=
set set btFlags=
set mqStartLogger=
set mqOpenLogfile=

@rem
@rem In real time mode, start formatting
@rem 
if /I "%mqRealTime%" == "" goto EndRealTimeFormat
echo Starting btstrace real time formatting...
call %MZRT_TRACEFMT_EXE% -display -rt btstrace -o btstrace.txt
:EndRealTimeFormat
set mqRealTime=
goto :eof

:Usage
echo.
echo Usage: 
echo.
echo	trace [^<Action^>] [^<FileSize^>] [^<Level^>]
echo		(^<Action^> = -start , -stop; ^<FileSize^> = -cir ^<MB^>, -seq ^<MB^>, -rollover ^<MB^; 
echo		 ^<level^> = -low , -medium, -high, -all, -flag 0xfff)
echo	trace -path ^<TraceFormat folder^>
echo		(Set env var for TraceFormat folder, needed for rt, format)
echo	trace -tools ^<Trace Tools folder^>
echo		(Set env var for Trace Tools folder, default will search the current path)
echo	trace -rt [^<Action^>] [^<Level^>]
echo		(Start trace logger and formatter in Real Time mode, additionally to 
echo		 btstrace.bin)
echo	trace -format [^<Binary log file^>]
echo		(Format binary log file to text file 'btstrace.txt')
echo	trace -change ^<Module^> ^<Level^>
echo		(Change the trace level for each module)
echo.
echo ^<Module^>: The module for which to change the debug level, one of:
echo           NTSVC, XLANG,   ProcessContainer, Agent,         Messaging,    HTTPReceive, 
echo           SOAPReceive     SOAPTransmitter, DBAccessor,     SchemaCache, UTIL, MIME, ERRORHANDLER,
ECHO           MsmqtGENERAL,   MsmqtNETWORKING, MsmqtDS,        MsmqtSECURITY,
echo           MsmqtAPPSTREAM, MsmqtINSTREAM,   MsmqtOUTSTREAM, MsmqtMESSAGES,  MsmqtTRACE,  
echo           InfoCache, BTSTicket, SSOAdmin, SSOAdminServer, SSOLookup, SSOLookupServer, SSOMapper,
echo           SSOMappingServer, SSOSS, BTSSSO, ssocheck, ssoclient, ssoconfig, ssomanage, 
echo           SSOConfigStore, SSOCSServer, SSOCSTX, ImportExport, SSOConfiguration,
echo           WindowsSharepointServicesAdapter, BamTddsService, BamInterceptors
echo.
echo Examples: 
echo 	trace (start/update logging to 'btstrace.bin' at Error level)
echo 	trace -path x:\Symbols.pri\TraceFormat
echo 	trace -tools "C:\Program Files\Common Files\Biztalk Adapter Trace Utility"
echo 	trace -rt -high (start real time logging at High level)
echo 	trace -change Agent medium
echo 	trace -format (format 'btstrace.bin' to 'btstrace.txt')
echo 	trace -stop (stop logging)
echo.

