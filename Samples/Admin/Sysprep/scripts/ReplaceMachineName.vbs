'Usage: ReplaceMachineName.vbs <text file to open> <string to be replaced with the current machine name>

Dim sOutput, reader, readerStream, writer, writerStream, Wshell

set WshShell = WScript.CreateObject("WScript.Shell")

Set reader = CreateObject("Scripting.FileSystemObject")
set readerStream = reader.OpenTextFile(WScript.Arguments(0), 1, , -2)

sOutput = Replace(readerStream.ReadAll, WScript.Arguments(1), wshShell.ExpandEnvironmentStrings("%COMPUTERNAME%"))
sOutput = Replace(sOutput, UCase(WScript.Arguments(1)), wshShell.ExpandEnvironmentStrings("%COMPUTERNAME%"))
sOutput = Replace(sOutput, LCase(WScript.Arguments(1)), wshShell.ExpandEnvironmentStrings("%COMPUTERNAME%"))

readerStream.Close

Set writer = CreateObject("Scripting.FileSystemObject")
Set writerStream = writer.CreateTextFile(WScript.Arguments(0), true, False) ''Write the file in ASCII
writerStream.Write(sOutput)
writerStream.Close