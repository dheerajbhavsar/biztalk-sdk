//---------------------------------------------------------------------
// File: OperationsSamples.cs
// 
// Sample: OperationsOM
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2016 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------





using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.Operations;
using OM = Microsoft.BizTalk.Operations;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace OperationsSamples
{
    /* Samples: 
     * (1) Search for a messagebox MSMQt instance
     *     - Look at its associated msgs
     *     - Look at its associated subscriptions
     * 
     * (2) Search for all msgs
     * 
     * (3) Suspend, terminate, resume an instance
     * 
     * (4) Search for a tracked db instance
     * 
     * (5) Message flow/routing details
     *   
     * [(6) EventLog traversal]
     */
    public class OperationsSamples
    {
        /* 
         * Rename the output directory of the HelloWorld Orchestration scenario 
         * and drop in a message. This will create a suspended (resumable) send port 
         * instance required for the OperationsOM sample. 
         */
        private static void SetUp()
        {
            Console.WriteLine("OperationsOM Sample SetUp -- Create a Suspended(Resumable) send port instance");
            //Get the HelloOrchestration directory path relative to the current directory.
            string helloOrchDir = Directory.GetCurrentDirectory() + @"\..\..\..\..\..\Orchestrations\HelloWorld\";
            if (!Directory.Exists(helloOrchDir + "Out2"))
            {
                try
                {
                    Directory.Move(helloOrchDir + "Out", helloOrchDir + "Out2");
                }
                catch (IOException ioexc)
                {
                    Console.WriteLine("The operation to change the directory name failed due to the following reason = " + ioexc.Message);
                }
            }
            //ensure that the Out directory is not present.
            if (Directory.Exists(helloOrchDir + "Out"))
            {
                Directory.Delete(helloOrchDir + "Out");
            }
            Console.WriteLine("Drop in a message for the HelloWorld scenario.");
            string inputFileName = "SamplePOInput.xml";
            //drop the message
            File.Copy(helloOrchDir + inputFileName, helloOrchDir + "In\\" + DateTime.Today.Second + inputFileName);
        }

        /* The test requires a dehydrated\suspended send port instance to 
         * be present in the database. The Setup.bat in the OperationsSamples
         * achieves this by renaming the output directory (Out) of the HelloOrchestrations
         * sample and dropping a message. 
         */
        public static void Main(string[] args)
        {
            SetUp();
            Samples examples = new Samples();

            //examples.AccessMsgBoxInstance();
            //examples.GetAllLiveMessages();
            //examples.SuspendResumeTerminateInstances();            

            string poNumber = "1234";
            Console.WriteLine("Get orchestration ID for purchase order # = " + poNumber);
            //get the orchestration id corresponding to this purchase order. 
            Guid orchestrationID = BAMWebService.GetOrchestrationID(poNumber);
            examples.AccessMessageFlow(orchestrationID);

            Console.WriteLine("Get the send port ID that follows this orchestration.");
            Guid sendPortID = examples.GetDestinationInstanceID(orchestrationID);
            Console.WriteLine("Get send port instance details from the message box database.");
            Console.WriteLine("Query the send port instance details before the resume operation.");
            examples.AccessMsgBoxInstance(sendPortID);

            Console.WriteLine("Fix the error causing the send port to fail."); 
            Console.WriteLine("We rename the 'Out2' to 'Out' and try to resume the send port instance");
            //Get the HelloOrchestration directory path relative to the current directory.
            string currentDir = Directory.GetCurrentDirectory();
            Directory.Move(currentDir + @"\..\..\..\..\..\Orchestrations\HelloWorld\Out2", currentDir + @"\..\..\..\..\..\Orchestrations\HelloWorld\Out");
            
            //try to resume the send port.
            examples.OperateOnInstance(Samples.InstanceOperation.Resume, sendPortID);
            Console.WriteLine("Query the send port instance details after the resume operation.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            examples.AccessTrackedDbInstance(sendPortID);
            
            /* The DTADbInterface interfcase class is provided as a guideline to query the 
             * DTA database against the ServiceFacts and MessageFacts views. They contain 
             * sample queries to search for completed service and message instances.
             */
            Console.WriteLine("Example: Query the DTADb for an instance guid and run it against OperationsOM APIs");
            Guid orchstrationID = DTADbInterface.GetLatestServiceInstance("Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule",
                                                    "Orchestration",
                                                    "Completed");
        }
    }

    public class BTSParameters
    {
        public static string DTADb = "BizTalkDTADb";
        public static string SQLServer = Environment.MachineName;
        public static string BAMDb = "BAMPrimaryImport";
    }

    public class Samples
    {
        BizTalkOperations _operations;

        static Samples()
        {
            Console.WriteLine("This sample is intended to be used on a system on which Microsoft BizTalk Server has been installed and configured.");
            Console.WriteLine("In particular, the MessageBox Database and/or the DTA Database as well as the Management Database are required for meaningful results.");
        }

        public Samples()
        {
            _operations = new BizTalkOperations();
        }

        //Retrieve the instance class -- orchestration/messaging from the DTA Db.
        public ServiceClass GetInstanceType(Guid instanceID)
        {
            Instance instance = _operations.GetServiceInstance(instanceID, new TrackingDatabase(BTSParameters.SQLServer, BTSParameters.DTADb));
            return instance.Class;
        }


        //USER: To be provided by the user; this can be obtained via the BAM APIs.
        public void AccessMsgBoxInstance(Guid msgBoxInstanceID)
        {
            PrintSampleName("AccessMsgBoxInstance");
            //note that this fn can return an instance from the msgbox or the DTADb. Different overloads allow the user
            //to specify the input database name.
            Instance inst = _operations.GetServiceInstance(msgBoxInstanceID);
            Console.WriteLine("Instance Class = " + inst.Class);
            Console.WriteLine("Instance ServiceType " + inst.ServiceType);
            Console.WriteLine("Instance Status = " + inst.InstanceStatus);
            Console.WriteLine("ErrorDescription = " + inst.ErrorDescription);
            
            if (inst is SendPortInstance)
            {
                Console.WriteLine("This is a SendPortInstance");
            }
            else if (inst is RoutingFailureInstance)
            {
                Console.WriteLine("This is a RoutingFailureInstance");
            }
            else if (inst is OrchestrationInstance)
            {
                Console.WriteLine("This is an OrchestrationInstance");
            }
            else if (inst is MSMQtInstance)
            {
                Console.WriteLine("This is a MSMQtInstance");
                AccessMSMQtInstance((MSMQtInstance)inst);
            }
	        else if (inst is TrackedServiceInstance)
	        {
                Console.WriteLine("This is a TrackedServiceInstance");
	        }
            else
            { 
                //This is an instance object
            }

        }

        private void PrintSampleName(string sampleName)
        {
            Console.WriteLine();
            Console.WriteLine("*********************************************");
            Console.WriteLine("*    " + sampleName + " Sample");
            Console.WriteLine("*********************************************");
            Console.WriteLine();
        }

        private void AccessMSMQtInstance(MSMQtInstance msmqtInst)
        {
            Console.WriteLine("These are a few of the properties available on the MSMQtInstance object. Other MessageBoxServiceInstance objects share some of these properties. ");
            Console.WriteLine("InstanceID: " + msmqtInst.ID);
            Console.WriteLine("HostName: " + msmqtInst.HostName);
            Console.WriteLine("Creation time: " + msmqtInst.CreationTime);
            Console.WriteLine("Instance state: " + msmqtInst.InstanceStatus);

            MSMQtOutboundQState outState = msmqtInst.OutboundUserState;

            if (outState != null)
            {
                Console.WriteLine("\n This object encapsulates an outbound MSMQt instance. Relevant fields are: ");
                Console.WriteLine("Unacknowledged message count: " + outState.UnacknowledgedMessageCount);
                Console.WriteLine("Unprocessed message count: " + outState.UnprocessedMessageCount);
                Console.WriteLine("MSMQt queue name: " + outState.QueueName);
                Console.WriteLine("Acknowledged message count: " + outState.AcknowledgedMessageCount);
            }

            MSMQtInboundQState inState = msmqtInst.InboundUserState;

            if (inState != null)
            {
                Console.WriteLine("\n This object encapsulates an inbound MSMQt instance. Relevant fields are: ");
                Console.WriteLine("Sender ID: " + inState.SenderId);
                Console.WriteLine("Incoming message count: " + inState.IncomingMessageCount);
                Console.WriteLine("MSMQT queue name: " + inState.QueueName);
            }

            Console.WriteLine(" /n Subscriptions associated with this instance: ");
            Console.WriteLine("These are a few of the properties available on the Subscription objects associated with this service instance. ");

            IEnumerable subscriptions = msmqtInst.Subscriptions;
            foreach (OM.Subscription subs in subscriptions)
            {                
                Console.WriteLine("SubscriptionID : " + subs.SubscriptionID);
                Console.WriteLine("Subscription state: " + subs.State);
                Console.WriteLine("Subscription priority: " + subs.Priority);
                //Predicate information is also available in the subscription object
            }

            Console.WriteLine(" /n Messages associated with this instance: ");
            Console.WriteLine("These are a few of the properties available on the Messages objects associated with this service instance. ");

            foreach (BizTalkMessage msg in msmqtInst.Messages)
            {
                Console.WriteLine("Message ID" + msg.MessageID);
                Console.WriteLine("Message state" + msg.MessageStatus);
            }
            
        }

        public void GetAllLiveMessages()
        {
            PrintSampleName("GetAllLiveMessages");

            IEnumerable messages = _operations.GetMessages();

            foreach (BizTalkMessage msg in messages)
            {
                ulong size;
                bool isImplemented;
                msg.GetSize(out size, out isImplemented);

                Console.WriteLine("The message ID, number of parts in the message, message type (ie, associated schema), body part name, size of the message and a bool (indicating whether the size is implemented) are: ");
                Console.WriteLine(msg.MessageID + ", " + msg.PartCount + ", " + msg.MessageType + ", " + msg.BodyPartName + ", " + size + ", " + isImplemented);

                Console.WriteLine("The messagebox name and server name on which this message exists are: ");
                Console.WriteLine(msg.MessageBox.DBName + ", " + msg.MessageBox.DBServer + "/n");
                Console.WriteLine();

                //Prints out the parts/property bag 
                Console.WriteLine("Parts associated with the message: ");
                int msgPartCount = msg.PartCount;
                for (int i = 0; i < msg.PartCount; i++)
                {
                    string name;
                    Microsoft.BizTalk.Message.Interop.IBaseMessagePart part = msg.GetPartByIndex(i, out name);
                    Console.WriteLine("Part number and part name are: " + i + ", " + name);
                    Console.WriteLine("Part charset, content type and partID are: " + part.Charset + ", " + part.ContentType + ", " + part.PartID);

                    Microsoft.BizTalk.Message.Interop.IBasePropertyBag bag = part.PartProperties;
                    Console.WriteLine("Properties associated with the part: ");
                    Console.WriteLine("Number of properties: " + bag.CountProperties);

                    uint propCount = bag.CountProperties;

                    for (int j = 0; j < propCount; j++)
                    {
                        string propName;
                        string nameSpace;
                        bag.ReadAt(j, out propName, out nameSpace);
                        Console.WriteLine("Part property number, name and namespace: " + j + ", " + propName + ", " + nameSpace);
                    }
                    Console.WriteLine();
                }

                //The message context can also be accessed in a similar way
                //Exception handling, ie, if a db you're querying against goes down                           
            }
        }

        public Guid GetDestinationInstanceID(Guid instanceID)
        {
            Console.WriteLine("Get instance following " + instanceID + " in the message flow");
            MessageFlow messageFlow = _operations.GetMessageFlow(instanceID);
            Guid destinationID = Guid.Empty;
            foreach (MessageEvent msgEvent in messageFlow.MessageEvents)
            {
                if (msgEvent.EventType == MessageEventType.Sent)
                {
                    foreach (MessageFlow outFlow in msgEvent.RoutingDetails.Destinations)
                    {
                        destinationID = outFlow.InstanceID;
                        break;
                    }
                }
            }
            Console.WriteLine("Destination ID = " + destinationID);
            return destinationID;
        }

        public void OperateOnInstance(InstanceOperation operation, Guid instanceID)
        {            
            CompletionStatus operationStatus;
            try
            {
                switch (operation)
                {
                    case (InstanceOperation.Suspend):
                        PrintSampleName("SuspendInstance");
                        operationStatus = _operations.SuspendInstance(instanceID);
                        PrintStateTransitionStatus(InstanceOperation.Suspend, operationStatus);
                        return;
                    case (InstanceOperation.Terminate):
                        PrintSampleName("TerminateInstance");
                        operationStatus = _operations.TerminateInstance(instanceID);
                        PrintStateTransitionStatus(InstanceOperation.Terminate, operationStatus);
                        return;
                    case (InstanceOperation.Resume):
                        PrintSampleName("ResumeInstance");
                        operationStatus = _operations.ResumeInstance(instanceID);
                        PrintStateTransitionStatus(InstanceOperation.Resume, operationStatus);
                        return;
                }                    
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("An argument exception occurred. It is possible that the input guid is an empty guid.");
                Console.WriteLine("Specific details are: " + ae.Message);
            }
            catch (SqlException se)
            {
                Console.WriteLine("A SQL exception occurred. It is possible that one of the databases is down or that the SQL service isn't running.");
                Console.WriteLine("Specific details are: " + se.Message);
            }
        }

        private void PrintStateTransitionStatus(InstanceOperation instOp, CompletionStatus operationStatus)
        {
            string operationType = ConvertInstanceOperation(instOp);

            switch (operationStatus)
            {
                case (CompletionStatus.Succeeded):
                    Console.WriteLine("The " + operationType + " operation succeeded.");
                    return;
                case (CompletionStatus.Failed):
                    Console.WriteLine("The " + operationType + " operation failed.");
                    return;
                case (CompletionStatus.Pending):
                    Console.WriteLine("The " + operationType + " operation is pending or was skipped.");
                    return;
            }
        }

        private string ConvertInstanceOperation(InstanceOperation instOp) 
        {
            switch (instOp)
            {
                case (InstanceOperation.Suspend):
                    return "suspend";
                case (InstanceOperation.Terminate):
                    return "terminate";
                case (InstanceOperation.Resume):
                    return "resume";
            }

            return "Error";
        }

        public enum InstanceOperation
        {
            Suspend = 1,
            Terminate = 2,
            Resume = 3
        }
        
        public void AccessTrackedDbInstance(Guid instanceID)
        {
            PrintSampleName("AccessTrackedDbInstance"); 
            TrackedServiceInstance trkInstance = (TrackedServiceInstance)_operations.GetServiceInstance(instanceID,
                                                                                new TrackingDatabase(BTSParameters.SQLServer, BTSParameters.DTADb));

            Console.WriteLine("Instance Class = " + trkInstance.Class);
            Console.WriteLine("Instance Status = " + trkInstance.InstanceStatus);
            Console.WriteLine("ErrorDescription = " + trkInstance.ErrorDescription);
        }

        public void AccessMessageFlow(Guid trackingInstID)
        {
            PrintSampleName("AccessMessageFlow");

            MessageFlow flow = _operations.GetMessageFlow(trackingInstID);

            Console.WriteLine("Messageflow's instanceID: " + flow.InstanceID);
            Console.WriteLine();
            IEnumerable msgEvents = flow.MessageEvents;

            foreach (MessageEvent msgevent in msgEvents)
            {
                //Some message event properties:
                Console.Write("Message event details - MessageID, MessageSize, TimeStamp, EventType are: ");
                Console.WriteLine(msgevent.MessageID + ", " + msgevent.MessageSize + ", " + msgevent.Timestamp + ", " + msgevent.EventType);
                Console.WriteLine("Message routing details for this event are: ");
                MessageRoutingDetails mrd = msgevent.RoutingDetails;
                GetRoutingDetails(mrd, msgevent.MessageID);
                Console.WriteLine("----------");
            }                       
        }

        //targetID is the messageID
        private void GetRoutingDetails(MessageRoutingDetails mrd, Guid targetID)
        {            
            MessageFlow src = mrd.Source; //the source
            ArrayList dests = (ArrayList)mrd.Destinations;
            if (src != null)
            {
                Console.WriteLine("Source flow - InstanceID, StartTime, EndTime, ServiceName: ");
                Console.WriteLine(src.InstanceID + ", " + src.StartTime + ", " + src.EndTime + ", " + src.ServiceName);
            }

            Console.WriteLine();
            Console.WriteLine("Destination flows - InstanceID, StartTime, EndTime, ServiceName for each destination:  ");
            
            foreach (MessageFlow dest in dests)
            {
                Console.WriteLine(dest.InstanceID + ", " + dest.StartTime + ", " + dest.EndTime + ", " + dest.ServiceName);
            } 
        }

    }

    /* This class queries the BAM web service to retrieve the activity ID.
     * It uses the BamManagementService web proxy class for the same.
     * Pre-requisite: you have BAM configured on the local machine, if not 
     * please update the BamManagementService class with the web-service url
     */
    public class BAMWebService
    {
        /* Retrieve the instanceID using the BAM GetReferences web-services API
         */
        public static Guid GetOrchestrationID(string poNumber)
        {
            BamManagementService bamMgmtService = new BamManagementService();
            bamMgmtService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //get the activity id
            Guid activityID = BAMDbInterface.GetActivityID(poNumber);
            //get instance guids
            Reference[] bamReferences = bamMgmtService.GetReferences("HelloOrchestration", "HelloOrchestration", activityID.ToString(), null);
            Guid orchestrationID = Guid.Empty;
            foreach (Reference reference in bamReferences)
            {
                Samples operationsOM = new Samples();
                if (reference.Type == "BizTalkService")
                {
                    if (operationsOM.GetInstanceType(new Guid(reference.Data)) == ServiceClass.Orchestration)
                    {
                        orchestrationID = new Guid(reference.Data);
                        break;
                    }

                }
            }
            Console.WriteLine("OrchestrationID retrieved from the BAM database = " + orchestrationID);
            return orchestrationID;
        }
    }

    /* The following classes are provided to suggests ways in which the 
     * Tracking database (DTA Db) and the BAM database can be queried against
     * the public views. They can be extended as required.
     */

    public class DataBaseHelper
    {
        private SqlConnection _dbConnection = null;
        private string _dbConnectString;

        public DataBaseHelper(string dbServerName, string dbName)
        {
            _dbConnectString = String.Format("Persist Security Info=False;Integrated Security=SSPI;Data Source={0};Initial Catalog={1};", dbServerName, dbName);
        }

        public DataBaseHelper(string connectionString)
        {
            _dbConnectString = connectionString;
        }

        private void OpenConnection()
        {
            if (_dbConnection == null || (_dbConnection.State != ConnectionState.Open))
            {
                _dbConnection = new SqlConnection();
                _dbConnection.ConnectionString = _dbConnectString;
                _dbConnection.Open();
            }
        }

        public SqlDataReader GetQueryResults(SqlCommand sqlCommand)
        {
            OpenConnection();
            sqlCommand.Connection = _dbConnection;
            return sqlCommand.ExecuteReader();
        }

        public void CleanUp()
        {
            _dbConnection.Close();
        }
    }


    public class DTADbInterface
    {
        private static DataBaseHelper _dbHelper;
        private static DataBaseHelper DBHelper
        {
            get
            {
                if (_dbHelper == null)
                {
                    _dbHelper = new DataBaseHelper(BTSParameters.SQLServer, BTSParameters.DTADb);
                }
                return _dbHelper;
            }
        }

        public static Guid GetLatestServiceInstance(string serviceName, string serviceType, string serviceState)
        {
            if (String.IsNullOrEmpty(serviceName))
            {
                throw new Exception("Pass in a valid serviceName value.");
            }
            SqlCommand cmd = new SqlCommand();

            StringBuilder sqlQuery = new StringBuilder("SELECT [ServiceInstance/InstanceID] FROM [dbo].[dtav_ServiceFacts] WHERE [Service/Name] = @ServiceName ");
            cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 512).Value = serviceName;

            if (!String.IsNullOrEmpty(serviceType))
            {
                sqlQuery.Append(" AND [Service/Type] = @ServiceType ");
                cmd.Parameters.Add("@ServiceType", SqlDbType.NVarChar, 512).Value = serviceType;
            }
            if (!String.IsNullOrEmpty(serviceState))
            {
                sqlQuery.Append(" AND	[ServiceInstance/State] = @ServiceState ");
                cmd.Parameters.Add("@ServiceState", SqlDbType.NVarChar, 512).Value = serviceState;
            }
            sqlQuery.Append(" ORDER BY [ServiceInstance/EndTime] DESC");
            cmd.CommandText = sqlQuery.ToString();
            Guid instanceID = Guid.Empty;
            using (SqlDataReader reader = DBHelper.GetQueryResults(cmd))
            {
                while (reader.Read())
                {
                    instanceID = reader.GetGuid(0);
                    break;
                }
            }
            if (instanceID == Guid.Empty)
            {
                throw new Exception("No service instances found matching the above criteria");
            }
            _dbHelper.CleanUp();
            return instanceID;
        }

        public static Guid GetLatestMessage(Guid serviceID)
        {
            if (serviceID == Guid.Empty)
            {
                throw new Exception("Pass in a valid serviceID value.");
            }
            String sqlQuery = "SELECT [MessageInstance/InstanceID] FROM [dbo].[dtav_MessageFacts] WHERE [ServiceInstance/InstanceID] = @ServiceID ORDER BY [Event/Timestamp] DESC";
            SqlCommand cmd = new SqlCommand(sqlQuery);
            cmd.Parameters.Add("@ServiceID", SqlDbType.UniqueIdentifier).Value = serviceID;
            Guid messageID = Guid.Empty;
            using (SqlDataReader reader = DBHelper.GetQueryResults(cmd))
            {
                while (reader.Read())
                {
                    messageID = reader.GetGuid(0);
                    break;
                }
            }
            if (messageID == Guid.Empty)
            {
                throw new Exception("No messages found matching the above criteria");
            }
            _dbHelper.CleanUp();
            return messageID;
        }
    }


    public class BAMDbInterface
    {
        private static DataBaseHelper _dbHelper;
        private static DataBaseHelper DBHelper
        {
            get
            {
                if (_dbHelper == null)
                {
                    _dbHelper = new DataBaseHelper(BTSParameters.SQLServer, BTSParameters.BAMDb);
                }
                return _dbHelper;
            }
        }

        
        public static Guid GetActivityID(string poNumber)
        {
            if (String.IsNullOrEmpty(poNumber))
            {
                throw new Exception("Pass in a valid PONumber.");
            }
            Console.WriteLine("Query the bam_HelloOrchestration_Active for the activiy id for purchase order# = " + poNumber);
            String sqlQuery = "SELECT Top 1 ActivityId from bam_HelloOrchestration_Active WHERE PONumber = @Number ORDER BY ReceivePortIn DESC";
            SqlCommand cmd = new SqlCommand(sqlQuery);
            cmd.Parameters.Add("@Number", SqlDbType.NVarChar, 100).Value = poNumber;
            Guid activityID = Guid.Empty;
            using (SqlDataReader reader = DBHelper.GetQueryResults(cmd))
            {
                while (reader.Read())
                {
                    activityID = new Guid(reader.GetString(0));
                    break;
                }
            }
            if (activityID == Guid.Empty)
            {
                throw new Exception("No activity was found matching the above criteria");
            }
            Console.WriteLine("Activiy ID retrieved from the database = " + activityID);
            _dbHelper.CleanUp();
            return activityID;
        }
    }
}
