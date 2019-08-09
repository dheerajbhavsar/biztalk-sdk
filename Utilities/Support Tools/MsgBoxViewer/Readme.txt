============
DESCRIPTION:
============


OBJECTIVE:
==========


MBVCONSOLE.EXE and MSGBOXVIEWER.EXE (Console and GUI version) generate both health check reports for a BizTalk Group.
They share a common Health Check engine and process a unique query repository targeting a BizTalk Group (BizTalk 2K4/2K6./2k9/2K10/2K13).
They are commonly used by Microsoft CSS at first troubleshooting stage to collect all important info from a BizTalk Group.

Prefer to execute these tools on a BizTalk Server to get automatically Mgmt db location/name and BTS version info, but you can also start them
on any type of server (SQL Servers, Non BT Servers, any type of Windows clients,etc...)

The folder MBVMMC contains the MBV MMC snapin which allow in an MMC console to generate some MBV reports and display their output.
This MMC snapin makes more easy the analyze of an MBV report and can be fusioned for example with the BizTalk Admin MMC.
See MBVMMC\readme.txt for more details to setup and use that MMC snapin  



QUERIES:
========

you have 4 types of queries which can executed  :


- SQL Queries

- WMI Queries

- .VBS Queries

- .BAT Queries

- .CMD Queries

- CUSTOM queries



These queries are organized in different "categories" :

- one "Important" category  : contained queries are ALWAYS executed

- several Optional categories : only the checked contained queries will be executed



Note: By default, only a part of the optional categories queries are selected for execution





OUTPUT FILES:
=============

Both tools generate output files in different format :


- HTML files contain all reports in HTML format => This is the first file to open to see well formated result of the health check.

- XML files contain all reports in XML format => can be usen then  with a parsing tool.

- TXT files contain all reports in plain Text.

- 1 Status log file which contain a detailed status of each query sent and possibly errors met (You can purge it inside the GUI Tool).
  This status log file is important to read if some errors were met during the collect process; 


Both tools generate 4 important type of reports (in the HTMl or XML) :


QUERY  REPORTS:
===============

This section list per category all the queries and their output report based on the queries you decided to execute.


TOPOLOGY REPORT:
================

This report contains a per server list (BizTalk and SQL Server hosting BT DBs) and each list contains main items hosted in the corresponding 
server : Dbs+Jobs for a SQL one, Configured Hosts+Running Hosts+adapters used+Ports+orch for a Biztalk one).

- if you select in the GUI tool the optional WMI queries , it will complete each server list with more info :

   Server Date/time, Server CPU info, Server CPU Usage, Mem Usage, CPU and Mem of running host instances, CPU and Mem of running SQLserv.exe...


- This report show you so quickly the topology of the BizTalk group with type of ports/host/db hosted on each server 



WARNINGS AND SUMMARY REPORT:
============================

The Warnings Report is the first List to focus on !

It display all the incompatibilities in the Msgbox or DTA Db and will  flag them in yellow or red depending of their severity.
=> ALL red warnings are critical to solve for a good health of BizTalk dbs !


SUMMARY REPORT:
==============


This report list a summary of useful info like Hosts/running hosts instances/running tracking hosts/ports disalled or stopped/largest number of msg in suspend Q...




OPTIONS
=======

- By default the console and GUI tool will execute mandatory queries but you can select in the GUI tool optional queries to execute 
(or use the /ALL option in the Console version) to get ALL lists.

- GUI tool will also display in some integrated ListViews the SUMMARY REPORT, HEADER REPORT and ERRORS.




RECOMMENDATIONS:
================

I would recommend in general to try the first collect with ALL optional selected. 
Even if you have some errors like Access Denied, they will appear in the html file and the status & errors file but they will NOT prevent the collections to stop.
Doing that allow you to get the max info the tool can try to have including the NON-SQL queries (WMI, VBS, BAT, or CUSTOM)

These NON-SQL queries can require some special user rights in additional to the BTS admin one  because they can target all servers belonging to the checked BTS group  and not only the Biztalk DBs ones on only SQL Servers.
So sometimes BTS admin user right is not enough but once again, if you have access denied errors with the NON-SQL queries, it is not critical because you should have already lot of info collected and lot of rules evaluated. 

Furthermore the TOPOLOGY REPORT is populated with additional info from these NON-SQL queries (CPU type, CPU usage, mem, running BTSNTSVC.EXE pids, SQLSERV.EXE pid, etc…)  and some additional warning can be raised also based on these NON-SQL  queries, like the throttling ones : In Biztalk 2006, except a biztalk trace  (and mayve eventlog, not sure ! )  only perf counters can show you if you have throttling, so if you don’t select the query “BTS perf counters” in the tool, you will not have this rule evaluated and no possibility to have the corresponding warning.

If you notice really lot of problems when ALL queries are cehecked, well, just uncheck them and make a collect  with the base queries ! :-:).

Finally, Each time you ask for the output files, please do not forget the status/errors file also, it allow me to analyze it when you report me some problems & errors and I can try quickly to determine the root cause. 

Thanks again to use it and feel free to ping me if you have any questions regarding these comments 

JP
(jpierauc@microsoft.com)






================
BUILDS/VERSIONS:
================


Version 13.40
============

- New "Topology graph" feature
- Global option to have report index HTML pages horizontal or vertical
- MBV UI reorganized
- Option to send by mail the dashboard and the Warnings reports 

Version 13.20
============

- New "Dashboard" section in the output report with key indicators
- Predefined profiles "Minimum" and "Full" selectable in both MBV UI and console
- Easy profiles switch
- Additional queries
- fixed issue with Perfmon query showing empty values
- format of the output report changed


Version 13.1
============

- New UI for MsgBoxwViewer tool
- Additional queries and rules (CUs,SQL,BAM,ESB,EDI,etc...)
- Full compatible with BizTalk 2013 
- New format of the output report for a better analyze


Version 12.00
=============


This build is a major new version providing now the possibility to split the Report output into multiple report files containing each one a section of the full report.

In previous versions one single HTML or XML file was generated and the resulting file could be huge depending of the analyzed BizTalk group configuration and so it generates some latency when opening it in IE.
This new build allows now, just using a global option in the GUI tool, to switch to Multiple files.
By default that option is set to False so MBV will keep generating a single report file but if you select to generate multiple files, MBV will create now a sub folder for each collect with the current date/time as the name and will generate in this folder several .HTML files (and .TXT or .XML if you selected to also produce .TXT or .XML files), each file corresponding to a section of a full MBV report.
The generated file MASTETRPAGE.html is the one to open to analyze the report and contains 3 frames : one for an index section, one which can change depending of which index item you select, and the third one which never change and which contains the “Query Reports“.
By default the Warnings report is displayed in the middle frame and when we click on the "Query Report" hyper link of a warning, it expands the corresponding query in the bottom frame so it make the analyze more easy now !. 

This version also updates the SQL versions rules and rules checking for CUs, but provides also new queries or rules proposed by BizTalk Support and Field engineers.

As requested by a major customer using MBV to monitor their Biztalk group, I also updated the MBV rule engine to implement Event IDs for each warning generated as an event.
MBV provides indeed a global option (in its GUI version) to generate Application events for its warnings, but until now all the events generated had 0 for their ID  value; this MBV update will generate now for each warning raised a unique ID with a base event ID range that you can choose  in the GUI version of MBV.

Thsi is below a summary of the main changes made in that MBV version :

-  Updated the BizTalk CUs rule
-  Updated the BizTalk BAP CUs rule
-  Added  rukes to check for  HIS kinstalltion and HIS Cumulative Updates
-  Added more  KB rules in the events query (select the ”Max Events..”  query to see all its rules)
-  The “Max events.. »  query targets now ALL the BTS in the group and not only the  server where MBV is running
-  The “Network drivers »  query targets now also the BTS servers
-  Added a query to get the operation history of BAM
-  Added a rule checking for the failure of the BizTalk monitor job and suggesting to install CU 1 if not installed
-  Updated the query to get versions of important dlls to get version of MQSeries, mqagent2, mqac.sys, etc… 
-  Adding two optional queries (in the “BT DBs Details” category)  to get DTA tracking info per orch and per port 
-  Generate now a unique ID for each rule raising a warning in the Event log
-  MBV MMC Snapin was fixed





Version 11.10
=============

This build  brings two major changes about the MBV Architecture :
 
1) MBV is compiled now to target .NET 2
   It means so that it can not run on a BT 2004 box with ONLY .NET 1.X installed, but honestly do we meet frequently such configs now  ? :-) 
 
 
2) MBV is not anymore a single .EXE and it is composed now of the following files: 

o   MYHC.DLL : Implement my custom Health Check engine
o   MYHCQUERIES_MBVQUERIES.DLL : Contains the entire MBV repository and custom Query functions to analyze a BizTalk Group       
o   MSGBOXVIEWER.EXE : GUI MBV Client tool
o   MBVCONSOLE.EXE : Console MBV Client tool
 
Reasons for this split are :

o   Query repository grow too quickly, need so to separate it from the clients .exe
o   Health Check engine should be updated w/o the client tools using it
o   Need to update easily MBV Repository w/o recompiling the client tools 
o   As MBV is just a client tool of my generic health Check engine, it can load any type of Query repository (DLL or XML) and not only the BizTalk one
 
You have the right to prefer the single binary version so in this case I can only suggest you to keep using  the old build :-) but you will not benefit of additional rules and queries.
 

New Features :
--------------

-          Compatible for incoming BizTalk 2010
-          Add additional query to get retrieve part of Biztalk 2010 Dashboard Settings
-          Detect if CU3 is installed on each BT Server 
-          Get Perfmon counters for ALL BT Servers (and not only the local server)
-          Get Perfmon counters for MsgBox Db SQL Servers (SQL General statisitcs, SQL locks, SQL Databases)
-          Check for installation of BAP 2010 beta and BAP SDK 2010 beta 
-          Check for installation of BT 2010 beta
-          Add order delivery and other port settings in the "Send ports" query
-          Add query to get custom settings of each Send Port: Not checked by default
-          Add query to get custom settings of each Receive Location: Not checked by default
-          Check for small or default value in WCF ReceiveTimeout  binding property 
-          Check for MQS port configured w/o Tx
-          Check for WMI prop “ClearAfter” too large value : Error CLSID_PropertBag
-          Format better BAM config info report output
-          Added Latest new MSDTC /COM+ packages version info (Suggested by Niklas) 
-          Get Last X rows from Spool and Instances  tables : rows number is configurable as a query property
-          Raise a warning in MBV if the CustomSD key is found under HKLM\System\Current Control Set\Services\Eventlog\Application (Suggested by Mike)
-          Rule to check for KB  980560
-          Rule to  check for KB  979709
-          Rule to check for KB  979095
-          Add more info in the “BizTalk Group” category of the SUMMARY REPORT section
-          Improved query to get sucessfully all GAC assemblies 
-          Updated  Latest BT Mgmt Pack version check
-          Get Pipeline ID in Pipeline Query Report
-          Get the Transport Type used in “WCF Custom” port
-          Redesigned the DialogBox UI to add custom Rules and be able to raise different type of actions
-          Redesigned the main dialogbox of MsgBoxViewer.exe (gui version)

 


Version 10.15.9078
==================

- Fixed MSDTC/COM+ package Version reported for W7 and W2K8 R2

- Fixed warning reporting incorrectly that Mgmt pack version installed was too old

- Check on ALL BT Servers belonging to a BizTalk 2006 R2 group for R2 SP1 presence and raise yellow warning suggesting it if not present

- Raise yellow warning if TDDS_FailedTrackingData rows > 50 000 (possiblr known issue described in KB 977289) 

- Added a rule in "BizTalk Eventlog" query checking for occurrence ofissue described in KB 950048 (MSQC with msg size > 4 MB)

- Added a rule in "Receive Locations" query checking for occurrence of issue described in KB 976306 (MSQC Receive Location using  “Request MSg Body Before” tracking option)

- Addedd mention to KB 978796 when DTA orphaned issue warning is raised 

 

Version 10.15.8978
==================


- Fixed issue where all  TCP settings were not entirely retrieved

- Fixed issue preventing properties of Web sites to be displayed in the "Web Sites" Query



Version 10.15.8878
==================

- Fixed issue where TEMP DB datasource is incorrectly retrieved in cluster configuration

- Fixed issue where KB 954814 is suggested in a warning for BizTalk 2009 (known issue only for 2006 and 2006 R2)

- New query to check for the presence of BizTalk Mgmt Pack and its version on the server running MBV 

- New query to get GAC assemblies and their version on the server running MBV

- New query to get current identity nID of MessageZeroSum and raise warning suggesting to install KB 970231 if identity 1,8 billions

- New query to check if row count in HostQ table > 2 billions and raise warning of possible Arythmetic error on BizTalk 2004 and 2006 Systems (known issue)



Version 10.15.8678
==================

- Fixed issue where child reports in some VBS queries are not closed correctly leting HTML report inconsistent.


Version 10.15.8578
==================

- Fixed "BizTalk COnfile files" query which was not able to show contain of the config files

- Fixed "BT Mgmt Pack Info" query to check for only Microogft BizTalk MP


Version 10.15.8478
==================

New Features and fixes:
----------------------

-	Include "Tempdb" info  on the "Dbs infos", "Dbs Files", and "Dbs Space" queries
-	Query to get list of IIS App pools with their owning Applications and check if an Application is a BizTalk Receive Location
-	Query to get list of Web sites with all their propertes App pools with their owning Applications and check if an application is a BizTalk Receive Location (suggested by Alex)
-	Get list of active global SQL Trace flags
-	Query to get the SQL agent log
-	Changed the KB linked to the warning message when AllowOnlySecureRpcCalls  is not set correctly on cluster
-	Fixed HotFix queries to get also the 32 bits installed on a X64 machine
-	Display now HotFix file(s) with their version
-	Fixed the .NET config files to have also the ones from 64 bits version of the Framework
-	Get also BTSNTSVC64.exe.Config if found
-	Order by DESC the Most recent records in TDDSFailedTRRackingData table for both DTA and BAM dbs
-	Fixed an HTML display issue (report can be expanded correctly ) when we fail to connect to  some Dbs
-  	Added Autogrow rules for the "DBs File" query to check the current auto grow of BizTalk Dbs against our recommendations for MDF and LOG files
-	Get info about BizTalk Mgmt Pack if installed     



Version 10.15.8078
==================

New Features:
------------

- New query in “Server Info” category: “NET Config files” on all BT servers - Not Checked by default
- New query in “Server Info”: “Running Processes” on all servers
- New query in “Advanced DB Info” category: “Tracking tables Sequence Number”  -  Not Checked by default
- New query in “Advanced DB Info” category:  “Tracking tables Sequence Number gaps” - Not Checked by default
- New query in “Advanced DB Info” category : “Tracking tables Sequence Large BLOBs” - Not Checked by default
- Provides a rule raising a warning if large table “NotEqualPredicateTables” found (known issue)
- Provides additional EventsLog rules
- Improves MsgBox Database naming convention in some Summary Report categories to be unique in multiple MsgBox scenarios
- Provides “Total Q rows” entry in the “MsgBox Table” Summary Report category
- "Current Error Log" query is modified to list entries in Descendant order
- Provides some rules to the "Current Error Log" query to check for Disk space errors, Integrity errors,Media Failure,or Fatal errors and raise red warnings if found
- "Error Log.1" request in now in its dedicated query  - Not Checked by default
- Host instance "Start time" is now added in Topology Report "Running Host instance" category
- System Variables are added in Topology Report
- Changed Severity from yellow to Red when Backup job is disabled 




Version 10.15
=============

New Features:
------------

- Can export into MBV Extension File custom rules created in MBV gui ("Export" button present in the Rule Dialog in MBV gui)
- Get Artefacts per Host
- Get  COM+ rollup version from COM+ & MSDTC modules loaded in BizTalk instances and from the “Important software Layer” query
  and add it in Topology Report
- Get SQL ERROR LOGs
- Get System Environment Variables 
- Get LanManWorkstations parameters
- Get Config File of ALL BT servers
- Get List of Stopped services on all servers and raise warning if some BizTalk related services are stopped (EDI, Rules update, etc…)
- Get BAM Portal  config file
- Get BAMQueryService  config file
- Get BAMMgmtService Config File
- Get RPC Settings and Internet Port range if present
- Get File list of each BT HotFixe installed on ALL BT Servers
- Include in the Perfmon Log query “Process” counters for ALL running BTSNTSVC.EXE and RUNTIMEAGENT.EXE 
- Get by default both Errors and Warnings in the Event Log Query
- Changed DTC Settings and CID duplication queries to be a single VBS query and so target this time correctly x64 registries
- Check for “FailureActions” changes in BizTalk Services (default  is “restart Service”)
- Check when max size of a DB file is soon reached (based on his growth) 
- Add  SQL Servers Disk Infos in Topology with Disk  types, Capacity, and FreeSpace
- Populate more statistics in the “BizTalk group” category in the Summary Report to reflect very quickly number of BT Servers, SQL ones, DBs, 
  MsgBoxes, RL, send Ports, pipelines,  ports, etc…
- Identify and add more software versions in Topology Report  (WCF adapter, MQS client layer, Oracle client layer, SAP client layer, etc..) 
- For BT 2009 add UDDI Db in list of Dbs analyzed  if its location is found in registry in the BTS running MBV
- Detect when SMS agent is running and raise warning suggesting to use latest version (or risk of 100 CPU%)
- Detect if some BizTalk related  services were stopped on BT servers (MSDTC, ENTSSO, EDI, Rule Engine)
- Raise non support warning if W2K8 R2 is detected
- Fixed bug when generating VBS file with path having spaces
- Fixed bug whene generating BAT file with a long filename
- Implemented Timeout for .VBS, .CMD, and .BAT queries  (default is set to 30 Secs)
- Checks for non unique document namespaces



Version 10.14
=============


New features:
-------------

- 	Collect process can now be interrupted before the end and output report consistency will be maintained

-       Profiles support - you can so now keep your MBV settings (Global properties, Queries Properties, Rules) in different profiles and select at any time the current profile

-	When building a custom rule in MBV GUI , we can now update also the Topology report as a possible  action to trigger when rule is fired 

-	When building a custom rule in MBV GUI, we can now launch a process with its own args  as a possible action to trigger when rule is fired

-	Topology Report is again simplified showing clearly attributes of entry  in its own column

-	Added 3 queries to get  maps info and maps linked to send port and receive ports

-	Added a query to retrieve  Network adapters + ip address for each server

-	Added a query to get Kerberos parameters from registry of each server and raise warning if PAC is turn on

-	Added a DMV query to get SQL perf info on each SQL server 

-	Added a DMV query to get SQL connections on  each SQL server 

-	Added query chedcking for BizTalk dbs on Mirrors (check my previous mail)


Fixes :
-------


- Fixed a regression issue where Subscriptions query return always empty output



Version 10.13.6772
==================


New Features:
------------

- collapse Query Categories by default

- Update Terminaor cleanupscript XML file when orphaned caches are detected

- Changed the GUI to add icons


Fixes:
------


- Fixed a regression issue where columns name are not anymore visible in "Perfmon counters" query

- Fixed issue where macro %GLOBALPROP_TARGETEDSERVER% was not replaced correcty with its value in query bodies




Version 10.13
=============


Major new feature:
-----------------

- Add Support in MyHC framework of Query Categories

- Improved the Summary Report by allowing each category to be expandable/collapsible




Version 10.12
=============


Major new feature:
-----------------


Added major feature in MBV GUI tool to add very easely your own Custom rules and save them of course in MBV Settings.Xml file !. 
you can so run now MBV to monitor for example specific tables size in the MsgBox "Table size" query or specific counters in the "BizTalk Perfmon" query and raise you own warning. 


These are the teps below to try this feature in MBV GUI tool :

-	Select a query in one list 
-	On the right pane showing its Properties and Rules, select the tab ”Rules”
-	You will see buttons “add a custom Rule” and  “Edit or show a Rule” (you can not change native MBV rules)
-	To see a sample of native rule with its conditions, select the “Max 500 BizTalk Errors Events” last query, select a Rule and  click on the button “Show Rule Details”
-	When you add a custom rule, a dialog box allow you to create your own Rule, add your own conditions and your own entry in the Summary Report section
-	You can then use a Macro like %VALUECOL5% in a condition to represent a value in the report at column 5 for the current row evaluated - You can use also a macro like %ROWCOUNT% - I will  list possible macros to use in final release 
-	Click on “Update rule” button to save the conditions and Summary Report entry in the Rule
-	And that’s it  !
-	The custom rule will appear then in blue specifying that it is a Custom Rule






Version 10.11
=============



Major new feature:
-----------------

Version number is now 10.11 as I changed a little bit the MBV GUI interface to add now the possibility to unselect some “mandatory” queries :

Maybe this feature can surprise first if we consider these queries as mandatory but I found the need
to be able to select sometimes only few queries to have very quickly result of a report, like the jobs one, or MsgBox Integrity one,
or only send ports, etc.. 

That’s why I also changed the List name “Mandatory queries” to “Important Queries”

To be able to do that, there is now a global option in MBV named ”Can unselect some important queries” that you have to turn to “True”- it is “False” by default.
When you set it to True and display  the Important Queries List, you will notice checkboxes in the list and you will be able so to
select /unselect the ones you want !


NOTE: By default, behaviour is same than before and no check boxes are visible in this first Queries list,
so by default this feature is invisible !

BE CAREFUL: Topology Report and Warnings Report can  be impacted if you unselect some important queries as some optional queries need some
info returned by some important queries so you can have some errors or incomplete Topology and Warning reports depending of  which important queries you have unselected



Minor new features:
-------------------

- Add a query to list assemblies deployed

- Report now a warning if Job Last Run Date & Last Run Time is tool old (>2 days or >1 hour) for critical jobs

- Get Pipeline "FullQualifiedName"  in Receive locations, send ports, and Pipelines

- Change LMT and LMFS Queries to target  a multiple SQL Servers configuration like DTA, MsgBox, or Mgmt located on different servers 

- Change Status Port to reflect exactly the one we have in the Admin console

- Report now the last 50 rows in TDDS_FailedTrackingData tables of DTA and BAM Db

- Report now contain of  TDDS_StreamStatus tables of both DTA and BAM Db

- Include "Application" field in Send poirts and Receive Locations Query Reports

- Insert all hosts with their hosted items in the BizTalk Hosts category of "Summary Report"






Version 10.10
=============

New Features :
-------------

- Group now all global tool properties in a “Property grid”

- Group all type of Queries tabs in a unique parent “Queries” tab 

- Added a global property to disable update of “History” and “Status” text files

- Added a global property to log or not all red or yellow warnings in the App journal or to let each  Query  decide: you can use so MBV also as some sort of Monitor tool selecting which query can generate its warnings in the Journal

- Added a tab page in the Query properties pane (right side of MBV window) to show  a Checked List  of Rules embedded in each query selected.
with this build, you can know quickly now which query generate which warning or information in Topology Report and you can disable the Rules you want.This information was requested by some customers.
I added also a global property  in the MBV interface named “ALL Rules can be Evaluated” that is set to “True” by default.
If this option is set to “True”, even if a rule was disabled it will be evaluated !
If this option is set to “Depend of Each Query Rule”, disabled rules  will NOT be evaluated ! and in this case I raise a red msg in the Summary Report to notify that.

- Add an “Errors” ListView listing all the errors met during the collect. You can consult this list during the Collect .

- Added query to get “SSOmanage –displaydb”  output

- Added query "SSO Global info table"

- Added query to get TDDS_StreamStatus tables on both BAM and DTA dbs

- Get (via WMI) versions of important layers on ALL servers  in the BizTalk group (including SQL ones): COM+, MSDTC, MXSML, .NET FRWK

- Get (via WMI) Running Services on ALL BT Servers (not selected by default)

- Get (via WMI) Host name, PID, Mem, and CPU of ALL BTSNTSVC.EXE running on ALL BT Servers

- Added an editable property in ”MsgBox table size” and “DTA table size” queries to chose the method to get size : ”Sp_SpaceUsed (default)”   or” Select count(*)”

- Split the “WMI global info” query into multiple queries

- Added a property in the Application EventLog query to chose for Errors events, Warnings, or Both 

- Added a "Svc Instance" section in the Summary Report

- Added a link to EE blog in the Summary Report link column  for each warning needing a cleanup/repair script: “Get More Info on 'TERMINATOR' Tool to clean or repair this » and update correctly the generated “MBVCleanupscript.xml “.g

- Added some warnings rules checking versions of SP for SQL and OS

- Added many warnings rules for the Eventlog QUery

- get also FullyQualifedName for pipelines

- Improve Jobs query

- Check if last run date & time for critical job is not too old



Fixes:
-----

- Fixed a bug preventing sometimes the Topology Report and Summary Report to be visible in the HTML if error occur in some queries (plan text file remain  correct however).

- Fixed an important bug when calling “ChangeDatabase“ on a closed connection






Version 10.00
=============

New Features :


- Add Support of Query custom properties

- Gui of MBV tool redesigned to be more simple,intuitive and easy to use

- Add Additional queries : 

	- rows count for dehydrated instances
        - rows/month for some MsgBox and DTA tables
        - network drivers info on all SQSL servers


Fixes:


- Fix important Acces Denied issue when the tool user is not sysadmin on BizTalk Mgmt db preventing then the Analyze to really start

- changed "Acced Denied" Warning message in Summary Report saying that SysAdmin is mandatory to have all queries chance to succeed



Version 9.27.3030
=================

New Features :


- Add Start time in "Host Instance" reports

- Raise a yellow warning when a server is running either XP or Vista

- Add more Post-Query rules on the Application Journal Query report

Fixes:

- Fix minor bugs in HTML presentation layer




Version 9.27.2322
=================

New Features :


- Add queries to log frament parts info and tunning

- Add query and warning  to check for Orphaned DTA SvcInstanceExceptions

- check if map on send port

- Add WMI Query Timeout


Fixes:

- Improved MBV Extensibility and Rules engine




Version 9.27.2020
=================

New Features :


- Add Support for MBV Query Extension XML

- Extend Query Extensibility fetaure to WMI queries

- Order Pipelines by Name


Fixes:

"ALL BizTalk Servers: BTS Version" query was not visible anymore in the list



Version 9.25
===============

New Features :


- Add the option to sort tables size by Row Count

- Add options to choose sort order for Receive Locations, Send Ports, and orchestrations (see Option tab in Gui tool and "/?" for args in Console one)

- Add options to not include Query Reports and Topology Report (included by default - see Option tab in Gui tool and "/?" for args in Console one)

- Add Warnings indicator in the status bar

- Add an option to generate "mini" HTML report focused only on BizTalk DB Sizes (see Option tab in GUI tool and "/dbgrowth" in Console one)

- Generate "MBVCleanupScripts.xml" in XML/HTML folder keeping list of possible proposed cleanups scripts

- Add Support for MBV Extension dll


Fixes:


- Increase max size of reports (Topology and Summary Reports) to manage very large configs and avoid "out of range" exceptions

- Fix sporadic "not declared variable" issue in sp_who3 script

- To avoid confusion, added Duration format (HMMSS) in Col name in the Jobs Report 

- Fix issue when having ndf files reporting incorrect growth value

- Fix issue where Orchestration name was not shown anymore in the "Orchestrations Instances" report (regression of 9.24)

- Read/save in the xml settings file the output files path & folders





Version 9.24
===============

New Features :

- Both Gui and Console versions now use a config file named "MsgBoxViewer_Settings.xml" and "btsdbcollect_settings.xml" to store tool options
and optional queries selected.

=> This file is loaded at tool startup when found in the Tool's folder and saved/updated when tool exits.
=> if you use the Console version, you can so update manually the "btsdbcollect_settings.xml" config file to select only the optional queries you want to keep executed for example
=> The Console version will generate a config xml file ONLY if there was no config file present before in the tool's folder.
=> Any options passed to the Console version will overllap the ones stored in config file 

- Add "Last 100 Recent Svc instances" optional query (original query of HAT)

- Log now in the HTML and History files the title of ALL queries available in the tool but gray out the unselected or disabled  ones.
  We can see quickly so which query was unselected or disabled because of role missing

- Modify the XML Summary Report section to be used easely in future parsing tools


Changes:

- Table size order for DTA and MsgBox db is now done in DESCENDING mode so the largest table will be the first one in the report if this order choice is selected (by default).

- Unselect by default the first 4 DTA optional queries




Version 9.23.51
===============

New Features :

- Each category of the SUMMARY REPORT is now collapsable and the SUMMARY REPORT can be expanded/collapsed in one click

- Name Order of DTA and Msgbox tables is now Descending

- Renamed some Categrories in the SUMMARY REPORT to be more explicit



Version 9.23.5
==============

New Features :

- Change format of the SUMMARY REPORT section.

  Now this report is composed of 3 sub-reports :

	* "Critical Warnings" : report only the red warnings
	* "Non Critical Warnings" : report only the yellow warnings
	* "Full Summary Report : represent the complete Summary Report

- Add the total Collect Duration at the begining of the Full Summary Report 





Version 9.23.42
==============

New Features :

- Get high fragmentation info for MsgBox and DTA DBs
  Note:  this query can be long to execute and so the default query timeout of 30s can expire. It can be changed in both the Gui and Console  tools

- Retrieve now also dynamic send ports in the Send ports detailed report

- Included TwoWay  settings in send ports and Receive Locations Detailed reports (for their port)

- Include now the RulesEngine Db in the list of databases checked

- Add hyperlinks to the Summary Report Categories  in the Summary Report header


Fixes:

- Decreased the severity of "Time Difference" warning from red to yellow and improved statements to get the time of each server to avoid if possible to have false alarm on that.
Now I raise a warning if the time difference between a BTS box and SQL box hosting a MsgBox db is >=2 minutes.

- Fixed an issue when reading the db schema version  in btsdbversion table of each BizTalk Db. Until now only the first record was read and starting with this build I enumerate all possible records in this table and take in account the most recent db schema version found 



Version 9.23.4
==============

Change:

- Keep only the name of Stored procs in the  "Most CPU Queries (SQL 2K5 only)" instead of the complet text



Version 9.23.32
==============

Fix:

- Use DB server virtual name in the Detailed Report instead of the physical server name
Keep only the physical server name in the Topology Report



Version 9.23.31
==============

Fix:

- report incorrectly yellow warning about "MS VM" and corresponding Policy Support url instead of WMWARE one (even if this info in the Topology Report remains correct)



Version 9.23.3
==============

New features :
 
- Add an optional query to get the 100 max BizTalk Application error events in last 24 hours.
I retrieve ONLY the errors msg from following source : "BAM","BIZTALK SERVER","ENTSSO","TDDS","XLANG","EDI","RULEENGINE","MSDTC"


- GUI tool: execute now the collections in a separate thread than the form so the form is always responsive now and not
appearing anymore as blank window during a pending long query
 


Version 9.23.2
==============

New feature :
 
- option to save also the Collect output in an XML file including the corresponding schema.
This opton is avail in the GUI tool in main DialogBox via the chekbox :
"Generate also Collect output in an XML file (in HTLM file folder)" 
This option is avail in the Console tool via the Option -XML

if this option is selected, ALL reports will be included in the XML file : 
the Header, all the Detailed ones, the Msg Flows, and the Summary one


This option can useful if you want to develop a parsing tool analyzing this Collect file and making its own reporting



Version 9.23.1
==============

New features :
 
- Check if HotFix KB 943165 is installed on x64 BTS and raise a yellow warning if not


Fixes/changes :
 

- fixed some invalid tracking reporting in some specific configurations of orchs


Version 9.23
============


New features :


- Implement now User Roles: the tool let the user choose which permissions he has among 4, and depending of his choice, some queries will be disabled (grayed in both queries listboxes).
The disabled queries will NOT be executed during the collect and, for each disabled query, a status message will be inserted in the status log file mentioning that the query is “disabled”.
The User roles selected are present in the HEADER REPORT so you can know quickly what is the profile of the user running the tool.

- Add a optional query getting ALL cluster nodes discovered in relation with BizTalk group and add them in the Topology Report

- Reorganize for more clarity the output in  the status File 



Fixes/changes :
 

- Select by default ALL Optional Queries
 
- Replace in some warnings SOX number link with the url lof "BizTalk EE blog" created by Mike
 
- List of queries are redesigned
 
- Reorganize a little bit the Status File output to be more readabble

- Add links to pages number in the BizTalk Operation Guide





Version 9.22
============


New features :


- Enumerate  also now 64 bits BizTalk host instances running

- Get also cluster prop in host table

- Add more info in the Topology Report for BizTalk hosts, dbs, jobs,etc.. 

- Allow to sort db tables size reports by table name or size

- Split MsgBox reports in sub-reports when we have multiple msgbox 

- Collect db infos for ALL bizTalk dbs (in the db info report)

- Check if found collation on bizTalk dbs are supported in BizTalk 2K4

- Added two columns “Running Status” and “State”’ in the jobs  table and put the value in red or yellow depending of the severity

- if the first method to find the SQLAgent fails, a second method is tried looking for ‘SQLAgent’ in ‘proc_name’ of SysProcesses

- Add a  'Assembly' column  in ‘Orchestrations Deployed’ Report to make distinction between multiple identical orchestration names (but of version different) 

- Raise a warning if SQL 2005 SP0 or SP1 is installed as they are not supported anymore

- Raise a warning if Windows 2003 SP0 is installed on a server as SP0 is not supported anymore

- change the title of output files produced to reflect exactly the tool name used

- Add the tool version in the title of HTML files produced

- Add the type of each query at the end of each query title in the listboxes : can be either "SQL query", "REGISTRY query", or "WMI query" 


Version 9.21.01
===============


New features :


- Added now attributes for some entries in the Topology Report : for hosts instances, Databases, jobs)

- Get Cluster property of each host


Version 9.21
============


New features :


- Identify basic message flows and report them in a 4th Report section ordered by Receive port and MesageType

- Order now each server topology by categories

- Changed captions of collections in the UI and the reports to reflect the targeted Db(s) or Server(s)

- Get the Message counts in DTA (reuse the HAT query)

- Get now the hotfixes installed for R2

- Include in the Host Report BizTalk Hosts which are 32 bits or 64 bits and report total in Summary Report 

- Check max job log history on master db and raise warning if greater than 1000 (red when > 5000)

- Get CID MSDTC reg key in Topology report for each server of the topology and raise a yellow warning if duplicated ones are found with link to a KB

- Insert also in the Topology Report of each server the MSDTC settings, some important TCP Settings  found and the BizTalk Hotfixes 

- Raise now a red warning if SP2 is not installed on a BizTalk 2004 group with link to the Lifecycle doc

- Raise a yellow warning about SNP possible effects if a W2K3 SP2 server is found and if the "Update" to turn it off is not found

- Add versions of important software layers in the topology report (ONLY for the server executing MBV)


Fixes:


- Get now correct value for perf counters/sec


- changed the case of some variable and field names in some query script to work fine with case sensitive collation dbs



Version 9.20.8
==============

New features :


- Report now the Activation Subscriptions per host (Optional query) 

- Check if Updategrams are used and raise a yellow warning

- Add BTS Version in the Topology Report

- Raise a yellow warning if rows are found in TDDS_FailedTrackingData DTA table.


Fix :

- show now correctly the filter of send ports in the HTLM Report

- Get the exact error msg when the tool can not connect to SQL Dbs



Version 9.20.7
==============

Fix :

- HTML Summary Report could not be generated sometimes if some specific warnings were raised (History Log file contains well the Summary Report however) 


Version 9.20.6
==============

New features :


- Report now if some TCIP Special settings are found on Servers of the BizTalk group ("TCPTimeWaitDelay","MaxUserPort","TcpWindowSize","SynAttackProtect")


Version 9.20.5
==============

New features :


- Report now the MSDTC settings found under 'Cluster'registry key if found for a server

- Report now the list of subscriptions w/o instances grouped by their classID (sub-Report of the "MsgBox Integrity Report")

- Check if Control messages are well present in the Spool table (1 msg in Biztalk 2K4 SP1/SP2,  4 msg in BizTalk 2K6)



Fix:

- fixed errors "Out of range" when too may throttling warnings or Registry throttling changes are raised in the Summary Report
  so normally now the tool should be more robust to analyze very complex BizTalk Config having large number of Servers + large number of hosts
  + 3 msgboxes for example  


Version 9.20.4
==============

New features :


- Can Report (optional info) the list of modules (name, version, path) loaded in each BTSNTSVC.EXE running (with the PID and host name) on the BTS 
  where the tool is excecuting.

Fix:

- No bugs found since 9.10


Version 9.20.3
==============

New features :

- Check if Servers have /3GB enabled (add this info in the Topology Report and raise a yellow warning if it is for a BizTalk Server)

- Add query to get hotfixes for ALL BTS (optional query) and not onty for the one running the tool

- Enumerate now only the send ports with their primary transport (some sendports having backup transport were previously duplicated in the 
"SendPorts"Detailed report) 

- Add the "MinCompletionPortThreads" check in the CLR Detailed Report


Version 9.20.2
==============

New features :

- Add more categories in the SUMMARY REPORT

- Report more Data tables rows in the SUMMARY REPORT using Select count(*) and raise warning if difference between sp_spaceused is > 1000

- Report the "Applications" found on a bts 2K6 Group

- Report now size of ALL BizTalk Databases

- Report in the summary Report the larger table of Msgbox and DTA databases

- raise red warning if suspended cache instances found > 5000

- raise warnings if large number or "Ready To Run" Svc Instances are found


Fixes:

no since 9.10



Version 9.20.
============

New features :


- The SummaryReport display now a third column containing, for most of the entries, a link to the the corresponding Detailed Report
and sometimes a URL link to MS KB or Tech Article on Internet.

- Add now the Server Domain, Server Domain role, and Server Model in the Topology Report

- Raise a warning if a server is running in MS Virtual Machine

- Raise a warning if a AutoClose is set for a Biztalk DB

- Report ALL reg values under each Host Svbc Registry key and raise warning if Throttling ones are found 


Fixes:


No Since 9.10



Version 9.10
============

New features :


- raise a warning if msg_cleanup job is well be disabled since BTS 2K6

- raise a warning if the schedule interval of some important jobs was changed from the default one


Fixes:

- fixed a bug when comparing drive of MsgBox LDF and MDF

- Fixed a bug : all detailed reports were not well expended when clicking on the expand button

- Detect in the "MSDTC Settings Report" if a machine is a cluster node and if yes do NOT get the node's DTC settings as they are not used
 (the Shared cluster registry contain the good ones in fact)


Version 9.09
============

Fixes:

- get now correctly ALL tracking flags for ReceivePorts & SendPorts (including two-ways ones), Orch and Pipelines 


Version 9.08
============

New feature:

- Add the corresponding Receive port for each Receive Location found

Fixes:

- "Out of Range" sometimes error when collecting MsgBoxTable Size


Version 9.07
============

New feature:

- The “SUMMARY REPORT” report now the row count  of “Spool”, “Instances” and “Subscription” tables using the query  :
  “Select count(*) from <table> with (nolock) “ and  a yellow warning is raised if these results are different than the “MsgBox Tables Size” 
  Detailed Report which use “sp_spaceused”.  This warning is intended to focus on possible SQL Statistics update issue. 

Fixes:

No new bugs reported


Version 9.06
============


New features:

- Add the time stamp of each query and error in the status log file

- Add the Connect Timeout and Query Timeout values in the header

- Decrease the default Query timeout to 30 secs


Fixes:

No new bugs reported


Version 9.05
============


New features:

- Allow to expand or collapse all Detailed Reports and Topology Reports

- Raise a red warning if a reg value 'MaxThreadPool' found is greater than 100 (can generate too many SQL connections)

- Collect the Btsntsvc.exe.config for the BTS running the tool (in the Optional list)

- Add the Service Pack version info in Topology for each server found


Fixes:

No new bugs reported



Version 9.04
============


New features:

- Raise a yellow warning if bts_cleanupmsgbox sp was updated with its cleanup code (as this sp is not really supported in prod because it empty the msgbox) 

- Display additional DTC settings for each Servers of the Biztalk group :

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSDTC]
"AllowOnlySecureRpcCalls"=dword:00000000
"FallbackToUnsecureRPCIfNecessary"=dword:00000001
"TurnOffRpcSecurity"=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSDTC\Security]
"AccountName"="NT Authority\\NetworkService"


Fixes:

No new bugs reported




Version 9.03
============


Fixes:

- fixed the "Locked SPIDS" query to return all recordsets (previous query did not return last one).
  this output now is EXACTLY the sp_who3 of LeeG (tool is using this sp_who3).

- fixed procedure to re-establish automatically connection if it was closed. In previous versions, re-open was not really made. 



Version 9.02
===============

New features :

- Raise a yellow warning if a same host is found to run RPorts + SPorts or Orchs

- display the Installation Type for a Biztalk 2K6 minimum : "Fresh installation" or "Upgrade"


Fixes:


- do not find correctly the "CLR thread Pool" Settings for a BTS 2006

- do not search the good jobs if the Biztalk install is an upgrade from 2K4 to 2K6




Version 9.00
===============

New features :

- this release bring a new major feature concerning only the html report rendering as now all "DETAILED REPORTS" and Servers in the TOPOLOGY REPORT
  are now collapsible !




Version 8.24.00
===============

New features :


- Report Zombie msgs grouped by classID

- Raise a red warning if Zombies cache msgs are found

- Raise a red warning if Zombies routing failures msgs are found

Fixes :

- Fixed a critical bug in the Biztaklk 2006 integrity script preventing correct enumeration of Zombie msgs

- Fixed DB Schema version checks and SQL 20005 SP2 check for BizTalk 2006 R2




Version 8.23.01
===============

New feature :

- raise a red warning if R2 is installed on a pre-SP2 SQL 2005 box

Fixes:

- Check the DB connection before each query and try to reconnect if needed

- Fixed some queries to support now Sensitive case collation Biztalk dbs

- Fixed a naming problem with the msgbox_cleanup job



Version 8.23.00
===============

New Features :

- Report the list of Adapters installed

- Report the version found in registry on ALL Biztalk Servers of the group and raise a red warning if version found
  is different of the one which was assumed for the group

- Report now the db space of the BAM db also

- Raise  a yellow informational warning if an adapter installed in the group is not an MS one 

- Raise now a Red warning instead of a Yellow one when a cleanup job is too long (parts or msg cleanup)

Fixes:

- Moved the Collect Errors number from the HEADER REPORT to the SUMMARY REPORT to show a correct number.



Version 8.22.01
===============

New Features :

- Log in more details all the pre-collect steps in the status log file

Fixes:

- REGRESSION issue introcuced in 8.22 !!: BAM db absence cause the tool to stop collect process at begining
 


Version 8.22
============

New Features :


- For SQL 2K5 dbs, get now the physical cluster node where SQL instance is running to include in the TOPOLOGY REPORT  to have
  a more valuable TOPLOGY REPORT.

- Raise yellow warning if SSO DB is not clustered

- Raise yellow warning if CLR Thread Pool Settings are found in BTS registry (ONLY if this option was selected in the optional queries list)



Fixes:

- the "Running SPID'"tab in the gui tool displayed the wrong Report



Version 8.21.04
===============

New Features :


- New column in the job list to show the steps commands in details

- List the DTC settings of each server found (when reg remote connection is allowed) 

- List the CLR Thread Pool settings of each BTS found (when reg remote connection is allowed) 

- Raise a read warning if a Biztalk db is detected as suspect 

- Raise a read warning if a Biztalk 2004 db is installed on a SQL 2K5

- Raise a yellow warning if Biztalk System is Biztalk 2004 db RTM or SP1




Version 8.21.00
===============

New Features :


- add a button to select the folder for the HTML output file


Fixes:


- fixed a regression issue introduced in 8.20 where BTS 2K6 throttling settings are checked for a Biztalk 2004 msgbox

- prevent the Collect statement to stop if there is an error determining BizTalk Version from a DB Schema version



Version 8.20.00
===============

New Features :


- Check, as an optional info, the DTC Settings on all servers involved in the biztalk group (remote registries is targeted so the check can fail for permissions issues fo example but the collect will continue anyway) 

- Can select now which Biztalk Perf counters we want to collect

- Assume a BizTalk version from the schema of each BizTalk DB; assumed version can be on of these :

BizTalk 2004 RTM 
BizTalk 2004 SP1 
BizTalk 2004 SP2 
BizTalk 2006 RTM 
BizTalk 2006 R2

and raise a yellow warning if a BizTalk version assumed from a DB schema seem different than the one found in BTS registry if the tool was running on a BTS server referenced in the targeted Mgmt db. 

- To avoid confusion, do not report anymore the Biztalk info (version, edition,..) found in a BTS registry if the BTS server running the 
  tool is not referenced in the targeted Mgmt Db. 
  We have indeed some customers who run the tool on a bts but they target a mgmt db belonging to a different Biztalk group than the bts
  one.In this scenario or if the server running the tool is NOT a bts, the  Biztalk Version assumed is the one detected from the Biztalk   
  Mgmt db schema and is mentioned in the SUMMARY REPORT

- Mention now which server is the "SSO MASTER" in the TOPOLOGY REPORT 

- Report in the TOPOLOGY REPORT if dbs are clustered or not 

- Add an Error counter in the GUI tool near the status bar

- The GUI tool display now only critical errors (can not connect to Mgmt db, DTA db, or MsgBox db for ex...)

- Changed the GUI portion related to the info to collect : Add a button to select all optional choices and 
  add a button to select some specific BizTalk counters.


Fixes:


- Do not stop the collecting process if table 'adm_otherDatabases' is not found in Mgmt Db

- Do not check the Tracking_spool job on a BizTalk 2K4 SP2 or 2K6 because it is not used anymore in these versions 

 

Version 8.19.00
===============

New Feature :


- Retrieve, as optional info, the Biztalk Perf counters of the BTS running the tool (BizTalk:Messaging, BizTalk:Message Agent, BizTalk:Messaging Latency,XLANGs) and raise red warning
 if these perf counters show delivery or publishing throlling in place (for BTS 2K6)

- Get db name and location for SSO DB and other dbs (adm_otherdatabases)

- Get Biztalk User Group Info



Version 8.18.00
===============

New Feature :

- Report the top 100 parts ordered by NumFragments and raise a yellow warning if lot of large msg submited

- Raise a red warning if recovery Model=SIMPLE as default for Biztalk db is FULL



Fix :

- Report now correctly Total Server CPU usage and process (BTSNTSVC.EXE and SQLSERVR.EXE) CPU usage in the Topology Report


Version 8.17.06
===============

Fix :

- Incorrectly report that db files of MsgBox and DTA are on same drive if MsgBox Server is different than DTA Server


Version 8.17.05
===============

New Feature :

- Report a yellow warning if db Auto-grow settings for Log and Data have been changed from their default values


Version 8.17.04 
===============

Fix :

- an empty Topology List is incorrectly created when BAM db does not exist 



Version 8.17.03 
===============

New features :

- Report Pipelines deployed with their msgbody tracking info

- Report msgbody tracking info for orchs deployed

- raise a yellow warning if msgbody tracking is detected on RecPorts, SendPorts, orchs or Pipelines

Fix :

- The query "Active msg for Send port" fails when the msgbox db name is not the default one.


Version 8.17.02 
===============

Fix :

- retrieve now ALL results from locked SPIDS output (sp_who3 @blocked=1) instead of only the Head(s) of blocking chain  


Version 8.17.01 
===============

Fix :

- if BAM db is not installed, the tool report an "object  ref not set" when it query the version of dbs 


Version 8.17.00 
===============

New Features :

- Report "Throttling Settings" for a Biztalk 2006 System and raise a yellow warning if their values were changed from the default one



Version 8.16.05 
===============

New Features :

- Detect R2 version

- GUI version implements now a progress bar

- keep 2 digits for decimal part of the db spaces

- raise a yellow warning if msgbox db is located on a BTS Server



Version 8.16.04 
===============

New Features :

- Add more comments with warnings in the Summary Report for the jobs

- possibility now to specify a specific user/pwd for the WMI queries (last collect in the Optional List)

- Report now ONLY as a warning if LDF and MDF are on same drives 


Version 8.16.03 
===============

just added a try/catch around each query made to continue to collect if there is an error in a specific query


Version 8.16.02 
===============

Fixes:

I incorrectly checked the presence of Indexes CIS_trackingMessageReferences and CIS_MessageParts on a BTS 2K4 PRE SP2 Msgbox db 
(they do not exist on BTS 2K4 PRE SP2) - reported by Michael Koppenol
I don't check them so now for such version of Msgbox 


Version 8.16.01 
===============

Fixes:

SQL Agent execution was checked on ALL servers including BTS ones genrating so timeouts ! (reported by Everett Yang).
Now, I check its good execution only on SQL servers hosting Biztall dbs - make more sens :-)


Version 8.16.00 
===============


New Features :


- Add a  query at the end of the optional list (WMI query) to include in the TOPOLOGY REPORT for each server :

	* OS info
	* Processor info	
	* System Date & System Time
	* CPU Load
	* Tot Mem
	* Avail Mem
	* CPU Load and private Mem of running Btsntsvc.exe and SQLServ.exe

- Report read warning if bts group named was renamed from the default name (suggested by Mike Shea)

- Report red warning if time found on BTS servers is not the same than time on SQL Servers hsoting MSgbox (suggested by Everett Yang)



Fixes:

- "Server Type" is not always present in some Server Topology List

- "SQL Server" type value is present multiple times in a same Server Topology List 

- report now DOP value in SUMMARY REPORT  only if it is wrong (not supported)

- report now autocreate/update stats settings in SUMMARY REPORT  only if they are enabled (not supported)



Version 8.15.00 
===============


New Features :


- Generate now a TOPOLOGY REPORT displaying all the Servers found,their rôle, and what they host (DB, Jobs, hostinstances, RL, SP, Orch,...)

- Report status of msgbox tables indexes and raise a warning if "ignore dup key" is not present for some indexes

- Report a red warning if SQL SP3 is found for a BTS 2K4 SP2 or BTS 2K6

- Report the "Send ports Groups"

- Add pipeline in "Receive Locations" Report

- Report a yellow warning for jobs having no history

- Report a yellow warning if there is no dedicated tracking host (host which do NOT host RL, SP, or orchs)


Fixes:


Move the "DB Fragmentation" reports into the optional list 



Version 8.14.00 
===============


New Features :


- Report in Red in "Summary Report" section if SQL ServerAgent is not running on a SQL Server hosting Biztalk DBs

- Report Host Name for each RL, Send Ports, and Orchestration deployed

- Report Tracking info for each RL and Send Ports deployed (no tracking, msgbody berfore, msgbody after, msgsprops before, msgprops after,...) 



Version 8.13.01 
===============


New Features :


- Show the state of RL, Send Ports, and Orch Deployed (Started, Stopped, Bound)

- Report in the "Summary Report" Section if stopped RL, Send Ports, and Orch deployed are detected

- Show DTA and Msgbox table sizes in descending size order


Fix :

- status bar in GUI version is hidden in VISTA


Version 8.13.00 
===============


New Features :


- Add in the header of the html file an hyperlink to the Summary Report section and list of hyperlinks for each Report to go immediately to a Report

- Integrate now both updated MsgBox Integrity check scripts for BTS 2K4 and BTS 2K6 (provided by Lee).
  The tool wil select the good Msgbox integrity Check script (either bts 2K4 or bts 2K6) depending of the version of Msgbox detected.  

- Generate a "DBCC ShowConfig" Report for Msgbox and DTA dbs to show fragmentation info (suggested by Michael Shea)  



Fix :

- Integrate new sp_who3 for SQL 2K and 2K5 that Lee fixed (error when waittime is too large).
  The tool use the good sp_who3 script depending of the MsgBox SQL Server version detected .



Version 8.12.6 
==============


Fix : the tool crash when started under Vista because Vista do not recognize DataGrid Set_Caption method


Version 8.12.5 
==============


Fix : "ManageRefcount" job  is searched incorectly for a prior SP2 BTS 2004 MSgboxes



Version 8.12.4 
==============


Fix : Do not generate anymore for the moment Integrity check output for BTS 2K6 - new script need to be provided by PG for BTS 2K6




Version 8.12.3 
==============



New features:


- flag in Red the missing jobs in the Report depending of the version of Biztalk detected


- Report also now Total number of tracking Host configured 


Fixes :


- Fix a Limitation of 200 rows for the Q List buffer (thanks Everett to have reported that) . Nows it calculate the exact number  needed 



Version 8.12.2 
==============



New features:


- set in Yellow in the Report if no host instance is running


Fixes :


- Fixed : use now "DBCC FILESTATS" to caculate Data Space of a Db

- Fixed : Running tracking host number in the Report is wrong

- Fixed : for multiple msgbox, only jobs of the latest msgbox found are listed

- implemented a try/cath around the Output files opening to catch any problem of wrong path or permissions
  if the path is wrong or any error occurs during opening output files, collection will continue anyway and
  will populate the Report ListView in the GUI tool. 
 



Version 8.12 
=============



New features:



- Determine the version of Biztalk (2004 RTM, 2004 SP1, 2004 SP2, 2006 RTM) directly from the DB Msgbox schema and include it in the Report

- Get more info for the SendPorts

- Add in the Report if Global tracking is disabled or not



Fixes :


NO




Version 8.11 
=============



New features:



- Show a Tab "Report" in the GUI tool

- Collect Receive Locations

- Collect Send Ports

- Collect Orchs deployed

- Collect Svc Instance Status (optional)



Fixes :


- Fixed bug finding incorrect number of running tracking Host instances

- Fixed bug with the button "Collect" which remain sometimes disabled

- Fixed bug with the title "Running Spid" displayed instead of "integrity check"

- Add "MSgBox Server Name + DB Name" in first column for running Orch and Active Msgs Collections

- Remove bts edition & Edition props in the List Report if the tool is started  on a a different box than a BTS one

