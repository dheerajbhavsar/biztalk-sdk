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
call %MZRT_TRACELOG_EXE% %mqStartLogger% btstrace %mqRealTime% -flags @agent %mqOpenLogfile% output\%mqBinaryLog%%mqSeqCode%%mqSeqNumber%.bin  %mqLogFileType% %mqLogFileSize% -guid #29da55e6-e688-425f-a52c-79f840cb8C89  & @rem Agent

::Pause

call %MZRT_TRACELOG_EXE% -enable btstrace -flags @xlangcsharp    -guid #186cc4a4,abab,abab,abab,abababababab >nul & @rem    XLANG
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @processcontainer -guid #186cc4a5,abab,abab,abab,abababababab >nul & @rem    ProcessContainer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @ntsvc    -guid #186cc4a6,abab,abab,abab,abababababab >nul & @rem    NTSVC
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @messaging    -guid #9d0e4402,4cce,4536,83fa,4a5040674ad6 >nul & @rem    Messaging
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @httpreceive    -guid #ff1bf348,eff3,4e72,baa4,0bb90a2a58ad >nul & @rem    HTTPReceive
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @soaptransmitter    -guid #6796973A,3ABB,30F7,A30A,A48797B885BD >nul & @rem    SOAPTransmitter
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @soapreceive    -guid #C8F975BF,5C97,4f17,9CA1,F894BC258761 >nul & @rem    SOAPReceive
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @dbaccessor    -guid #442B65E4,5BA1,4e09,B620,5AE937F02995 >nul & @rem    DBAccessor
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @schemacache    -guid #9d0e4403,4cce,4536,83fa,4a5040674ad6 >nul & @rem    SchemaCache
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @util    -guid #9d0e4406,4cce,4536,83fa,4a5040674ad6 >nul & @rem    UTIL
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @mime    -guid #9d0e4408,4cce,4536,83fa,4a5040674ad6 >nul & @rem    MIME
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @errorhandler    -guid #9d0e4409,4cce,4536,83fa,4a5040674ad6 >nul & @rem    ERRORHANDLER
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @btsticket    -guid #2bfdd646,b8a0,4731,8e5a,9a9ddb84381a >nul & @rem    BTSTicket
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @mgdcomps    -guid #689E4209,67A2,409b,8303,0905CE67A931 >nul & @rem    MGDCOMPS
call %MZRT_TRACELOG_EXE% -enable btstrace -flags 0x7FFFFFFF    -guid #9C48C5A8,C4EF,4112,8F75,6342415B70F0 >nul & @rem    WMI
call %MZRT_TRACELOG_EXE% -enable btstrace -flags 0x7FFFFFFF    -guid #0765FD8D,45B2,41a9,9BCE,E0A80CF6B911 >nul & @rem    AdminLib
@rem call %MZRT_TRACELOG_EXE% -enable btstrace -flags %msgFlags%    -guid #251CFF54,B1EC,44c4,A97B,83BBA42A4E14 >nul & @rem    MMC

call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a00 >nul & @rem    InfoCache
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a01 >nul & @rem    SSOAdmin
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a02 >nul & @rem    SSOAdminServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a03 >nul & @rem    SSOLookup
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a04 >nul & @rem    SSOLookupServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a05 >nul & @rem    SSOMapper
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a06 >nul & @rem    SSOMappingServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a07 >nul & @rem    SSOSS
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a08 >nul & @rem    BTSSSO
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a09 >nul & @rem    ssocheck
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a0a >nul & @rem    ssoclient
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a0b >nul & @rem    ssoconfig
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a0c >nul & @rem    ssomanage
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d9901 >nul & @rem    SSOConfigStore
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d3a99 >nul & @rem    SSOCSServer
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d9902 >nul & @rem    SSOCSTX
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #4100fd26,a656,45c6,9b81,f7da488d9903 >nul & @rem    ImportExport
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @sso -guid #B156D071,308C,4931,93C3,199E167C4834 >nul & @rem    SSOConfiguration
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @wssadapter -guid #BA7DAD66,5FC8,4a24,A27E,D9F68FD67C3A >nul & @rem    WindowsSharepointServicesAdapter
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @bamtddsservice -guid #FDA0D9B6,CF63,4096,8912,C008E42A3B86 >nul & @rem    BamTddsService
call %MZRT_TRACELOG_EXE% -enable btstrace -flags @baminterceptors -guid #7DED31DE,B123,42A6,889C,090A60ED3CBB >nul & @rem    BAM Interceptors

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
echo	trace [^<Action^>] [^<FileSize^>] 
echo		(^<Action^> = -start , -stop; ^<FileSize^> = -cir ^<MB^>, -seq ^<MB^>, -rollover ^<MB^;)
echo	trace -path ^<TraceFormat folder^>
echo		(Set env var for TraceFormat folder, needed for rt, format)
echo	trace -tools ^<Trace Tools folder^>
echo		(Set env var for Trace Tools folder, default will search the current path)
echo	trace -format [^<Binary log file^>]
echo		(Format binary log file to text file 'btstrace.txt')
echo.
echo Examples: 
echo 	trace (start/update logging to 'btstrace.bin' at Error level)
echo 	trace -path x:\Symbols.pri\TraceFormat
echo 	trace -tools "C:\Program Files\Common Files\Biztalk Adapter Trace Utility"
echo 	trace -format (format 'btstrace.bin' to 'btstrace.txt')
echo 	trace -stop (stop logging)
echo.

