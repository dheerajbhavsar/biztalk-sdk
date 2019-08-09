if "%1"=="" goto Usage

SqlCmd -s . -d Master -A -Q "select @@servername as 'Before Sql Server Name Change'" >> C:\Scripts\SqlServerNameChange.log

SqlCmd -s . -d Master -A -Q "EXEC sp_dropserver '%1'"

SqlCmd -s . -d Master -A -Q "EXEC sp_dropserver '%1\SQLEXPRESS'"

SqlCmd -s . -d Master -A -Q "EXEC sp_addserver '$(NEWCOMPUTERNAME)', local"

SqlCmd -s . -d Master -A -Q "EXEC sp_addserver '$(NEWCOMPUTERNAME)\SQLEXPRESS', local"

ping localhost -n 30

net stop BTSSvc$BizTalkServerApplication

REM - Unregister Bam Alerts
net stop NS$BAMAlerts
taskkill /f /im nsservice.exe
cd "%ProgramFiles%\Microsoft SQL Server\90\NotificationServices\9.0.242\bin\"
cd "%ProgramFiles(x86)%\Microsoft SQL Server\90\NotificationServices\9.0.242\bin\"
nscontrol unregister -name BamAlerts

net stop ENTSSO
taskkill /f /im entsso.exe

net stop SQLAgent$SQLEXPRESS
net stop sqlserveragent

net stop ReportServer$SQLEXPRESS
net stop ReportServer
taskkill /f /im ReportingServicesService*

net stop MSOLAP$SQLEXPRESS
net stop MSSQLServerOLAPService

net stop MSSQL$SQLEXPRESS
net stop mssqlserver

net start mssqlserver
net start MSSQL$SQLEXPRESS

net start ReportServer$SQLEXPRESS
net start ReportServer

net start MSOLAP$SQLEXPRESS
net start MSSQLServerOLAPService

net start sqlserveragent
net start SQLAgent$SQLEXPRESS

net start ENTSSO

REM - Reregister Bam Alerts service
nscontrol register -name BamAlerts -server $(NEWCOMPUTERNAME) -service -serviceusername "UPDATEME" -servicepassword "UPDATEME"
net start NS$BAMAlerts

net start BTSSvc$BizTalkServerApplication

SqlCmd -s . -d Master -A -Q "select @@servername as 'After Sql Server Name Change'" >> C:\Scripts\SqlServerNameChange.log

:Usage
echo "Updates SQL after a machine name change and cycles a number of dependent services"
echo "Usage: UpdateSqlServerAndInstanceName <oldDBServerName>"
