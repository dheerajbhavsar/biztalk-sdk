Dim WshShell, sInstallPath
Dim source, destination
Dim L_ErrMsg_Text
L_ErrMsg_Text = "Unable to read registry:"

Set WshShell = WScript.CreateObject("WScript.Shell")
regstr="HKLM\SOFTWARE\Microsoft\BizTalk Server\3.0\InstallPath"
sInstallPath = WshShell.RegRead(regstr)
if sInstallPath = "" Then
	MsgBox L_ErrMsg_Text  & regstr
else
	'Copy the required dlls to the "Developer Tools" folder
	i = InStrRev(sInstallPath, "\")
	if(i <> len(sInstallPath)) then
		source1 = sInstallPath & "\" & "SDK\Utilities\Schema Generator\Microsoft.BizTalk.WFXToXSDGenerator.dll"
		destination = sInstallPath & "\" & "Developer Tools\Schema Editor Extensions"
	else
		source1 = sInstallPath & "SDK\Utilities\Schema Generator\Microsoft.BizTalk.WFXToXSDGenerator.dll"
		destination = sInstallPath & "Developer Tools\Schema Editor Extensions"
	end if
	cmd = "cmd.exe /c copy """ & source1 & """ """ & destination & """"
	WshShell.Run cmd
end if


