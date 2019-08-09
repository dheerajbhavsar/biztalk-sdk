PSSDiagForBizTalk is tool to help diagnose BizTalk issues. It captures the following data. 
 

1.  BizTalk trace
2.  MsgBoxViewer report
3.  Completed service instances.
4.  Inomplete service instances: including message body, message context, and service configuration.
5.  Event logs
6.  Performance logs
7.  WCF trace
8.  System.Net trace
9.  Network Monitor trace (Network Monitor must be installed beforehand)
10. Hang dump 
11. BizTalk registry entries.
12. System information like installed hotfixes and running processes etc.
13. List of assemblies in GAC and their file attributes
14. Group memberships of service accounts
15. Some important files: BTSNTSvc.exe.config, BTSNTSvc64.exe.config, machine.config, Mscorwks.dll, Mscordacwks.dll, and Sos.dll


Instructions:

If possible, perform this step so that you only need to run PSSDiagForBizTalk on one physical server.
In a multi-server environment, if the involved host has multiple host instances running on different physical servers, please shut down all but one host instance so that you can run PSSDiagForBizTalk only on one physical server. Otherwise, since the host instances are distributed on separate servers, you need to run PSSDiag on all BizTalk servers to capture all activities in the host. 

If possible, perform this step to reduce the size of captured data.
On the physical server you are going to run PSSDiagForBizTalk, make sure to shut down the host instances which do not belong to the involved host. Restrict the traffic which is not related to the issue, and use as few number of messages as possible to reproduce the issue.
 
1. Run Initialize.exe
2. Select tracing options and click Save and Close button to finish the initialization.
3. Open a command prompt and go to the folder, and run "PSSDiag.exe /g". 
4. Wait for this message in Console: PSSDIAG collection started. 
5. Reproduce the issue. 
6. Use a Ctrl C to stop PSSDiag and wait until you see this message in Console: PSSDIAG Collection complete. Do not use any other methods to stop PSSDiagForBizTalk so that PSSDiagForBizTak has the chance to finish capturing data and clean up. Also issue Ctrl C only once and be patient to wait for the tool to exit gracefully.
7. Captured data is saved in a subfolder called "output". Zip up the "output" folder and all of its subfolders. 
