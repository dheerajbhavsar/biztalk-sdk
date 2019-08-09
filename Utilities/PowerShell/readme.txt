ReadMe for SOAFACTORY POWERSHELL PROVIDER BIZTALK

SOAFACTORY POWERSHELL PROVIDER BIZTALK allows you to manage your BizTalk environment. The original source of the SOAFACTORY POWERSHELL PROVIDER BIZTALK can be found at http://psbiztalk.codeplex.com/.

Before getting started, please ensure Powershell 3.0 is installed in the system.

Please follow the steps below to get started
1. Open a command prompt as an administrator
2. Change directory to the folder containing the binaries - \SDK\Utilities\PowerShell under the base install location
3. Run the following command to install/register the dlls
    %windir%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe .\BizTalkFactory.PowerShell.Extensions.dll
4. Open a powershell console as admin in x86 mode and set execution policy as remote-signed:
    Set-ExecutionPolicy –ExecutionPolicy RemoteSigned
5. Add the PS snap-in using the usual approach:
    Add-PSSnapin –Name BiztalkFactory.Powershell.Extensions

The following cmdlets are available in this provider:
Add-ApplicationReference
Remove-ApplicationReference
Add-ApplicationResource
Set-DefaultApplication
Start-Application
Stop-Application
Start-HostInstance
Stop-HostInstance
Reset-HostInstance
Start-Orchestration
Stop-Orchestration
Enlist-Orchestration
Unenlist-Orchestration
Disable-ReceiveLocation
Enable-ReceiveLocation
Start-SendPort
Stop-SendPort
Enlist-SendPort
Unenlist-SendPort
Start-SendPortGroup
Stop-SendPortGroup
Enlist-SendPortGroup
Unenlist-SendPortGroup
Import-Application
Import-Bindings
Import-Policy
Get-ApplicationResourceSpec
Export-Application
Export-Bindings
Export-Policy
Deploy-Policy
Undeploy-Policy