strComputer = "."
set fo = CreateObject("Scripting.FileSystemObject")
'dim sCurPath
sCurPath=fo.GetAbsolutePathName(".")
Set objWMIService = GetObject("winmgmts:" & "{impersonationLevel=impersonate,(Backup)}!\\" & strComputer & "\root\cimv2")
Set colLogFiles = objWMIService.ExecQuery("SELECT * FROM Win32_NTEventLogFile WHERE LogFileName='Application'")
For Each objLogfile In colLogFiles
 errBackupLog = objLogFile.BackupEventLog(sCurPath & "\\output\\BTS_App_UDE.evt")
 'If errBackupLog <> 0 Then
  'Wscript.Echo "The Application event log could not be backed up: " & errBackupLog 
 'end if
Next

Set colLogFiles = objWMIService.ExecQuery("SELECT * FROM Win32_NTEventLogFile WHERE LogFileName='System'")
For Each objLogfile in colLogFiles
 errBackupLog = objLogFile.BackupEventLog(sCurPath & "\\output\\BTS_Sys_UDE.evt")
Next
Set colLogFiles = objWMIService.ExecQuery("SELECT * FROM Win32_NTEventLogFile WHERE LogFileName='Security'")
For Each objLogfile in colLogFiles
 errBackupLog = objLogFile.BackupEventLog(sCurPath & "\\output\\BTS_Sec_UDE.evt")
Next