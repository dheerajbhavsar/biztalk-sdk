//---------------------------------------------------------------------
// File: RegisterAdapter.cs
// 
// Summary: BizTalk messaging adapter registration.
//
// Sample: SubmitDirect SDK sample 
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using Microsoft.Win32;
using System.Management;

namespace Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils
{
	/// <summary>
	/// Register an adapter
	/// </summary>
	class AdapterRegistration
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Check command-line parameters
			if (0 < args.Length)
			{
				if (("/?" == args[0]) || ("-?" == args[0]))
				{
					PrintUsage();
					return;
				}
				else if (("/u" == args[0]) || ("-u" == args[0]))
				{
					Uninstall();
					return;
				}
				else
				{
					Console.WriteLine("Incorrect command-line parameter.");
					PrintUsage();
					return;
				}
			}

			// Check if the BizTalk Server is present on the machine
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0");
			if (null == rk)
			{
				Console.WriteLine("Microsoft BizTalk Server is not installed on this machine.");
				return;
			}

			// Get install directory path
			string InstallPath = (String)rk.GetValue("InstallPath");
			string InboundAssemblyPath = InstallPath + 
				   @"SDK\Samples\AdaptersDevelopment\SubmitDirect\TransportProxyUtils\bin\Debug\Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils.dll";
			string AdapterMgmtAssemblyPath = InstallPath +
				   @"SDK\Samples\AdaptersDevelopment\SubmitDirect\TransportProxyUtilsUI\bin\Debug\Microsoft.Samples.BizTalk.Adapters.TransportProxyUtilsMgmt.dll";
			
			// Check if the adapter has been already registered
			rk = Registry.ClassesRoot.OpenSubKey(@"CLSID\{CFD15534-83BB-4a13-9F82-50F51E200E04}");
			if (null == rk)
			{
				Console.WriteLine("Updating the registry...");
				rk = Registry.ClassesRoot.OpenSubKey("CLSID", true);
				rk = rk.CreateSubKey("{CFD15534-83BB-4a13-9F82-50F51E200E04}");
				rk.SetValue("", "Submit Adapter Class");
				rk.SetValue("AppID", "{12A6EBAA-CF68-4B58-B36E-A5A19B22C04E}");
			
				RegistryKey rkBizTalk = rk.CreateSubKey("BizTalk");
				rk = rk.CreateSubKey("Implemented Categories");
				rk.CreateSubKey("{7F46FC3E-3C2C-405B-A47F-8D17942BA8F9}");

				rkBizTalk.SetValue("", "BizTalk");

				rkBizTalk.SetValue("TransportType", "Submit");
				rkBizTalk.SetValue("Constraints", 0x00000081);
				rkBizTalk.SetValue("ReceiveLocation_PageProv", "{2DE93EE6-CB01-4007-93E9-C3D71689A280}");
				rkBizTalk.SetValue("InboundEngineCLSID", "{11E62A28-D6C8-41ed-AF7F-369EFBFDA2BE}");
				rkBizTalk.SetValue("InboundTypeName", "Microsoft.Samples.BizTalk.TransportProxyUtils.BizTalkMessaging");
				rkBizTalk.SetValue("InboundAssemblyPath", InboundAssemblyPath);
			
				rkBizTalk.SetValue("PropertyNameSpace", "http://schemas.microsoft.com/BizTalk/2003/SDK_Samples/Messaging/Transports/submit-properties");

				rkBizTalk.SetValue("AdapterMgmtTypeName", "Microsoft.Samples.BizTalk.Adapters.TransportProxyUtilsMgmt.TPUtilsManagement");
				rkBizTalk.SetValue("AdapterMgmtAssemblyPath", AdapterMgmtAssemblyPath);
				rkBizTalk.SetValue("AliasesXML", "<AdapterAliasList><AdapterAlias>Submit://</AdapterAlias></AdapterAliasList>");
				rkBizTalk.SetValue("ReceiveLocationPropertiesXML", "<CustomProps><AdapterConfig vt=\"8\"/></CustomProps>");
				rkBizTalk.SetValue("ReceiveHandlerPropertiesXML", "<CustomProps><AdapterConfig vt=\"8\"/></CustomProps>");

				Console.WriteLine("Registry has been updated.");

			}
			else
			{
				Console.WriteLine("Submit adapter has been already registered.");
			}

				// Add the adapter to the server.
			Console.WriteLine("Adding the adapter to the server...");
			ManagementObject objInstance = null;

			try
			{
				ManagementClass objClass = new ManagementClass(
					@"root\MicrosoftBizTalkServer", 
					"MSBTS_AdapterSetting", 
					new ObjectGetOptions());
			
				objInstance = objClass.CreateInstance();

				objInstance["Name"] = "Submit";
				objInstance["MgmtCLSID"] = "{CFD15534-83BB-4a13-9F82-50F51E200E04}";
				objInstance["Comment"] = "Programmatic submission adapter";
				
				objInstance.Put();
                CreateAdapterHandler("Submit");
				Console.WriteLine("Adapter has been added.");
			}
			catch(Exception e)
			{
				Console.WriteLine("Failed to add adapter to the server, reason: {0}", e.Message);
			}
		}

        static void CreateAdapterHandler(string adapterName)
        {
            string hostName = null;
            try
            {
                PutOptions options = new PutOptions(); 
                options.Type = PutType.CreateOnly;

                //create a ManagementClass object and spawn a ManagementObject instance
                ManagementClass objReceiveHandlerClass = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_ReceiveHandler", null);
                ManagementObject objReceiveHandler  = objReceiveHandlerClass.CreateInstance();
                
                //set the properties for the Managementobject
                objReceiveHandler["AdapterName"] = adapterName;
                
                ManagementClass hostClass = new ManagementClass(
                   @"root\MicrosoftBizTalkServer",
                   "MSBTS_HostSetting",
                   new ObjectGetOptions());

                foreach (ManagementObject objInstance in hostClass.GetInstances())
                {
                    if ("2" == objInstance["HostType"].ToString())// "2" indicates isolated type
                    {
                        objReceiveHandler["HostName"] = objInstance["Name"];
                        break;
                    }
                }

                objReceiveHandler["CustomCfg"] = "<CustomProps><AdapterConfig vt=\"8\"/></CustomProps>";
                
                //create the Managementobject
                objReceiveHandler.Put(options);
                System.Console.WriteLine("ReceiveHandler - " + adapterName + " " +hostName + " - has been created successfully");
            }
            catch(Exception excep)
            {
                System.Console.WriteLine("CreateReceiveHandler - " + adapterName + " " + hostName + " - failed: " + excep.Message);
            }
        }

		static void Uninstall()
		{
			// Check if the BizTalk Server is present on the machine
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0");
			if (null == rk)
			{
				Console.WriteLine("Microsoft BizTalk Server is not installed on this machine.");
				return;
			}

			// Check if the adapter has been already registered
			rk = Registry.ClassesRoot.OpenSubKey(@"CLSID\{CFD15534-83BB-4a13-9F82-50F51E200E04}");
			if (null != rk)
			{
				Console.WriteLine("Removing the registry entery...");
				rk = Registry.ClassesRoot.OpenSubKey("CLSID", true);
				rk.DeleteSubKeyTree("{CFD15534-83BB-4a13-9F82-50F51E200E04}");
				Console.WriteLine("Registry entery removed.");
			}
			else
			{
				Console.WriteLine("Submit adapter is not registered.");
			}

			// Removing the adapter from the server.
			Console.WriteLine("Removing the adapter from the server...");

			try
			{
				ManagementClass objClass = new ManagementClass(
					@"root\MicrosoftBizTalkServer", 
					"MSBTS_AdapterSetting", 
					new ObjectGetOptions());

				foreach (ManagementObject objInstance in objClass.GetInstances())
				{
					if ("Submit" == objInstance["Name"].ToString())
					{
						objInstance.Delete();
						break;
					}
				}

				Console.WriteLine("Submit adapter removed from server.");
			}
			catch(Exception e)
			{
				Console.WriteLine("Failed to remove adapter from the server, reason: {0}", e.Message);
			}
		}

		static void PrintUsage()
		{
			Console.WriteLine("Submit Adapter Registration Utility");
			Console.WriteLine("Usage:");
			Console.WriteLine();
			Console.WriteLine( "RegisterAdapter.exe [/?]|[/u]");
			Console.WriteLine();
			Console.WriteLine(" Where: ");
			Console.WriteLine("  /? = Displays this help message.");
			Console.WriteLine("  /u = Will unregister the adapter.");
			Console.WriteLine();
		}
	}
}
