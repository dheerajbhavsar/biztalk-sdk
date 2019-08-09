//---------------------------------------------------------------------
// File: OrchestrationBinding.cs
// 
// Sample: ExplorerOM
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





#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

#endregion

namespace OrchestrationBinding
{
    class OrchestrationBinding
    {

        static void Main(string[] args)
        {
            OrchestrationBinding ob = new OrchestrationBinding();

            Console.WriteLine("\nMake sure the \"HelloWorld\" sample application is deployed and running.");
            Console.WriteLine("By default it will be listed in the BizTalk Server Administration Console");
            Console.WriteLine("with the application name: \"BizTalk.Application.1\".\n");
            Console.WriteLine("Orchestration status should be \"Started\".");
            Console.WriteLine("In the BizTalk Administration Console, view the orchestration's status.");
            Console.WriteLine("Press <Enter> now to Stop the Orchestration using ExplorerOM...");
            Console.ReadLine();

            Console.WriteLine("=== Please wait, stopping the orchestration... ===");
            ob.StopOrchestration();
            Console.WriteLine("\nOrchestration status should now be \"Stopped\".");
            Console.WriteLine("Press F5 to refresh and verify in the BizTalk Server Administration Console");
            Console.WriteLine("Pressing <Enter> now will Unenlist the orchestration using ExplorerOM...");
            Console.ReadLine();

            Console.WriteLine("=== Please wait, unenlisting the orchestration... ===");
            ob.UnenlistOrchestration();
            Console.WriteLine("\nOrchestration status should now be \"Unenlisted\".");
            Console.WriteLine("Press F5 to refresh and verify in the BizTalk Server Administration Console");
            Console.WriteLine("Pressing <Enter> now will Unbind the orchestration using ExplorerOM...");
            Console.ReadLine();

            Console.WriteLine("=== Please wait, unbinding the orchestration... ===");
            ob.UnbindOrchestration();
            Console.WriteLine("\nOrchestration status should now be \"Unenlisted(Unbound)\".");
            Console.WriteLine("Press F5 to refresh and verify in the BizTalk Server Administration Console");
            Console.WriteLine("Pressing <Enter> now will re-bind the orchestration using ExplorerOM...");
            Console.ReadLine();

            Console.WriteLine("=== Please wait, re-binding the orchestration... ===");
            ob.BindOrchestration();
            Console.WriteLine("\nOrchestration status should now be \"Unenlisted\" again.");
            Console.WriteLine("Press F5 to refresh and verify in the BizTalk Server Administration Console");
            Console.WriteLine("Pressing <Enter> now will enlist the orchestration using ExplorerOM...");
            Console.ReadLine();

            Console.WriteLine("=== Please wait, re-enlisting the orchestration... ===");
            ob.EnlistOrchestration();
            Console.WriteLine("\nOrchestration status should now be \"Stopped\".");
            Console.WriteLine("Press F5 to refresh and verify in the BizTalk Server Administration Console");
            Console.WriteLine("Pressing <Enter> now will restart the orchestration using ExplorerOM...");
            Console.ReadLine();

            Console.WriteLine("=== Please wait, restarting the orchestration... ===");
            ob.StartOrchestration();
            Console.WriteLine("\nOrchestration status should now be \"Started\" again.");
            Console.WriteLine("Press F5 to refresh and verify in the BizTalk Server Administration Console");
        }


        public void BindOrchestration()
        {
            // Create the root object and set the connection string
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

            try
            {
                BtsOrchestrationCollection orchestrations = catalog.Assemblies["HelloWorld"].Orchestrations;

                // Specify either a sendport or a sendportgroup for the orchestration outbound port
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Ports["SendInvoicePort"].SendPort = catalog.SendPorts["HelloWorldSendPort"];

                // Not using SendPortGroup for HelloWorld Sample *** //
                //orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Ports["SendInvoicePort"].SendPortGroup = catalog.SendPortGroups["SendPortGroup_1"];

                // Specify a receiveport for orchestration inbound port
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Ports["ReceivePOPort"].ReceivePort = catalog.ReceivePorts["HelloWorldReceivePort"];

                // Specify a host by index or name.
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Host = catalog.Hosts["BizTalkServerApplication"];

                // Commit the changes
                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }
        }


        public void EnlistOrchestration()
        {
            // Create the root object and set the connection string
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

            try
            {
                BtsOrchestrationCollection orchestrations = catalog.Assemblies["HelloWorld"].Orchestrations;
                // Set the orchestration status to enlisted
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Status = OrchestrationStatus.Enlisted;
                // Commit the changes
                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }
        }


        public void StartOrchestration()
        {
            // Create the root object and set the connection string
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

            try
            {
                BtsOrchestrationCollection orchestrations = catalog.Assemblies["HelloWorld"].Orchestrations;
                // Set the orchestration status to started
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Status = OrchestrationStatus.Started;
                // Commit the changes
                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }
        }


        public void StopOrchestration()
        {
            // Create the root object and set the connection string
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

            try
            {
                BtsOrchestrationCollection orchestrations = catalog.Assemblies["HelloWorld"].Orchestrations;
                // set the orchestration status to enlisted
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Status = OrchestrationStatus.Enlisted;
                // commit the changes
                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }
        }


        public void UnenlistOrchestration()
        {
            // Create the root object and set the connection string
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

            try
            {
                BtsOrchestrationCollection orchestrations = catalog.Assemblies["HelloWorld"].Orchestrations;
                // Set the orchestration status to Unenlisted
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Status = OrchestrationStatus.Unenlisted;
                // Commit the changes
                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }
        }


        public void UnbindOrchestration()
        {
            // Create the root object and set the connection string
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

            try
            {
                BtsOrchestrationCollection orchestrations = catalog.Assemblies["HelloWorld"].Orchestrations;

                // Reset the bindings
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Ports["SendInvoicePort"].SendPort = null;
                // Not using SendPortGroup for HelloWorld Sample *** //
                //orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Ports["SendInvoicePort"].SendPortGroup = null;
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Ports["ReceivePOPort"].ReceivePort = null;
                orchestrations["Microsoft.Samples.BizTalk.HelloWorld.HelloSchedule"].Host = null;

                // Commit the changes
                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }
        }
    }
}
