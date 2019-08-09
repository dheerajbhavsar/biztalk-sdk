@echo off

REM echo  ^<font size=10^>^<b^>SYSTEM INFORMATION:  %1 ^<^/b^>^<font^>
wmic  cpu get systemname,manufacturer,deviceid,description,maxclockspeed  /format:htable
wmic  os get CSname,Name,version,CSDVersion,buildnumber,currenttimezone,localdatetime,systemdirectory,totalvirtualmemorysize,totalvisiblememorysize  /format:htable
wmic  memphysical list full  /format:htable
wmic  process get Caption,Commandline,Processid /format:htable 
wmic  qfe list full  /format:htable