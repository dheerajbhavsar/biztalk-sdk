net start entsso
REM - issue a few pings to give SSO time to initialize
ping localhost -n 30

"%CommonProgramFiles%\Enterprise Single Sign-On\ssomanage.exe" -disablesso
"%CommonProgramFiles%\Enterprise Single Sign-On\ssomanage.exe" -updatedb %1
"%CommonProgramFiles%\Enterprise Single Sign-On\ssomanage.exe" -enablesso

echo "Starts the SSO service and reconfigures the master secret server using a provided xml answer file"
echo "Usage: UpdateSSO.cmd <UpdateInfo.xml>"