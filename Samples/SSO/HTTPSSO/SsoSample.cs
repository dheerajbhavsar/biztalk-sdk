//---------------------------------------------------------------------
// File:	SsoSample.cs
// 
// Summary: 	None
//
// Sample: 	HTTP SSO
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
using System.IO;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.BizTalk.SSOClient.Interop;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Win32;
using COMAdmin;

namespace Microsoft.Samples.BizTalk.SSO
{
	#region IisConfigurator
	/// <summary>
	/// Creates IIS configuration
	/// </summary>
	/// <remarks>
	/// This code is supposed to work only on IIS 5.0, 5.1 and 6.0 (in IIS 5.0 isolation mode)
	/// </remarks>
	public class IisConfigurator
	{
		/// <summary>
		/// The IIS versions
		/// </summary>
		public enum IISVersion
		{
			unknown,
			v50,
			v51,
			v60
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public IisConfigurator()
		{
		}

		/// <summary>
		/// Gets the IIS version from the registry
		/// </summary>
		/// <returns>IIS version</returns>
		public static IISVersion GetIISVersion()
		{
			// Get the IIS parameters key
			RegistryKey inetKey=Registry.LocalMachine.OpenSubKey(
				@"SOFTWARE\Microsoft\InetMgr\Parameters");

			// Read versions
			int majorVersion=(int)inetKey.GetValue("MajorVersion");
			int minorVersion=(int)inetKey.GetValue("MinorVersion");
			
			if(majorVersion==6)
			{
				return IISVersion.v60;
			}
			else if(minorVersion==1)
			{
				return IISVersion.v51;
			}
			else if(minorVersion==0)
			{
				return IISVersion.v50;
			}
			else
			{
				return IISVersion.unknown;
			}
		}

		/// <summary>
		/// Determines if IIS is running in IIS 5.0 isolation mode
		/// </summary>
		/// <returns>True if IIS is running in the old isolation mode, otherwise false</returns>
		public static bool IsOldIsolationMode()
		{
			if(GetIISVersion()==IISVersion.v60)
			{
				// Get the IISWebServer object
				DirectoryEntry server=new DirectoryEntry("IIS://localhost/W3SVC");

				// Check mode
				return (bool)(server.Properties["IIs5IsolationModeEnabled"].Value);
			}

			return true;
		}

		/// <summary>
		/// Checks if virtual directory exists
		/// </summary>
		/// <param name="name">Virtual directory name</param>
		/// <returns>True if it exists, otherwise false</returns>
		public static bool Exists(string name)
		{
			// Check parameters
			if(name==null)
			{
				throw new ApplicationException("Invalid parameter: 'name' cannot be null");
			}

			// Get the root directory entry for the web site
			DirectoryEntry root=new DirectoryEntry("IIS://localhost/W3SVC/1/Root");
				
			foreach(DirectoryEntry dir in root.Children)
			{
				if(dir.Name.ToLower()==name.ToLower())
				{
					return true;
				}
			}

			// Not found
			return false;
		}

		/// <summary>
		/// Creates an IIS virtual directory. Does nothing if it already exists.
		/// </summary>
		/// <param name="name">The virtual name of this directory</param>
		/// <param name="path">The physical path this directory maps to</param>
		/// <param name="basicAuth">True to set Basic authentication, False to sent Kerberos authentication</param>
		/// <returns>True if successful, otherwise false</returns>
		public static bool CreateVirtualDirectory(string name, string path, bool basicAuth)
		{
			try
			{
				// Get the root web site entry
				DirectoryEntry root=new DirectoryEntry("IIS://localhost/W3SVC/1/Root");

				// Remove it if it already exists
				if(Exists(name))
				{
					foreach(DirectoryEntry d in root.Children)
					{
						if(d.Name.ToLower()==name.ToLower())
						{
							// Delete the application
							d.Invoke("AppDelete", null);

							// Delete the child node corresponding to this virtual directory
							root.Children.Remove(d);

							break;
						}
					}
				}	
			
				// Create it
				DirectoryEntry dir=root.Children.Add(name, "IIsWebVirtualDir");

				if(IsOldIsolationMode())
				{
					// High isolation mode
					object[] param={1};
					dir.Invoke("AppCreate2", param);
				}
				else
				{
					// Create application pool
					object[] param={1, name, true};
					dir.Invoke("AppCreate3", param);
				}

				// Set the path and the isolation level
				dir.Properties["Path"][0]=path;
				dir.Properties["AppIsolated"][0]=1;

				// Set the access flags (READ and EXECUTE)
				dir.Properties["AccessFlags"][0]=0x00000005; 

				// Set access flags directly for Win2K
				dir.Properties["AccessRead"][0]=true;
				dir.Properties["AccessExecute"][0]=true;

				// Set the authentication type
				dir.Properties["AuthFlags"][0]=basicAuth ? 0x2 : 0x4;

				// Set the authentication flags
				dir.Properties["AuthAnonymous"][0]=false;
				dir.Properties["AuthBasic"][0]=basicAuth;
				dir.Properties["AuthNTLM"][0]=!basicAuth;

				// Save changes
				dir.CommitChanges();
			}
			catch
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Configures a COM+ application associated with the specified IIS virtual directory 
		/// to run under the interactive user account instead of a system account.
		/// </summary>
		/// <param name="name">The virtual directory name</param>
		/// <returns>True if successful, otherwise false</returns>
		public static bool SetVirtualDirectoryHostIdentity(string name)
		{
			try
			{
				// Set application pool identity on IIS 6.0
				if(IsOldIsolationMode())
				{
					// Instantiate the admin object
					Type type=Type.GetTypeFromProgID("COMAdmin.COMAdminCatalog");
					object catalog=Activator.CreateInstance(type);
					object[] args=new object[] {"Applications"};

					// Get the COM+ applications collection
					COMAdminCatalogCollection apps=(COMAdminCatalogCollection)type.InvokeMember("GetCollection", 
						BindingFlags.InvokeMethod, null, catalog, args);
					apps.Populate();
			
					string appName="//root/"+name.ToLower()+"}";
			
					// Look for our COM+ application
					foreach(COMAdminCatalogObject app in apps)
					{
						if(app.Name.ToString().ToLower().EndsWith(appName))
						{	
							// Set logon to interactive user
							app.set_Value("Identity", "Interactive User");
							app.set_Value("Password", "");
							apps.SaveChanges();
						}
					}
				}
				else
				{
					// Get pool object
					DirectoryEntry pool=new DirectoryEntry("IIS://localhost/W3SVC/AppPools/"+name);

					// Set properties
					pool.Properties["AppPoolIdentityType"][0]=3;
					pool.Properties["WAMUserName"][0]="Interactive User";
					pool.Properties["WAMUserPass"][0]="";

					// Save changes
					pool.CommitChanges();
				}
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
	#endregion

	#region BtsConfigurator
	/// <summary>
	/// Creates BTS configuration
	/// </summary>
	public class BtsConfigurator
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public BtsConfigurator()
		{
		}

		/// <summary>
		/// Creates receive port with one request-response HTTP receive location
		/// </summary>
		/// <param name="virtualDirectory">Virtual directory name</param>
		/// <param name="portName">Receive port name</param>
		/// <param name="locationName">Receive location name</param>
		/// <returns>True if successful, otherwise false</returns>
		public static bool CreateHttpRequestResponsePort(string virtualDirectory, string portName, string locationName, ref string retMsg)
		{
			IBtsCatalogExplorer explorer=null;
			try
			{
				string mgmtDBName="BizTalkMgmtDb";
				string mgmtDBServer="localhost";

				ManagementClass groupSettings=new ManagementClass("root\\MicrosoftBizTalkServer:MSBTS_GroupSetting");
				foreach(ManagementObject obj in groupSettings.GetInstances())
				{
					mgmtDBName=(string)obj.Properties["MgmtDbName"].Value;
					mgmtDBServer=(string)obj.Properties["MgmtDbServerName"].Value;
				}

				// Get BizTalk Explorer object
				explorer=new BtsCatalogExplorer();
				explorer.ConnectionString="SERVER="+mgmtDBServer+";DATABASE="+mgmtDBName+";Integrated Security=SSPI";

				// Delete this port if it already exists
				foreach(IReceivePort port in explorer.GetCollection(CollectionType.ReceivePort))
				{
					if(port.Name.ToLower()==portName.ToLower())
					{
						explorer.RemoveReceivePort(port);
						break;
					}
				}
				explorer.SaveChanges();

				// Add new port
				IReceivePort receivePort=explorer.AddNewReceivePort(true);
				receivePort.Name=portName;

				// Add new location
				IReceiveLocation rxLocation=receivePort.AddNewReceiveLocation();
				rxLocation.Name=locationName;

				// Set the receive location
				rxLocation.Address="/"+virtualDirectory+"/BTSHttpReceive.dll";

				// Set the transport type
				foreach(IProtocolType protocol in explorer.GetCollection(CollectionType.ProtocolType))
				{
					if(protocol.Name=="HTTP")
						rxLocation.TransportType=protocol;
				}

				// Set the transport data
				rxLocation.TransportTypeData=
					"<CustomProps>"+
					"<Address vt=\"8\">"+rxLocation.Address+"</Address>"+
					"<LoopBack vt=\"11\">0</LoopBack>"+
					"<IsSynchronous vt=\"11\">1</IsSynchronous>"+
					"<ReturnCorrelationHandle vt=\"11\">0</ReturnCorrelationHandle>"+
					"<ResponseContentType vt=\"8\">text/html</ResponseContentType>"+
					"<URI vt=\"8\">"+rxLocation.Address+"</URI>"+
					"<UseSSO vt=\"11\">1</UseSSO>"+
					"</CustomProps>";

				// Set the receive pipeline
				foreach(IPipeline pipe in explorer.GetCollection(CollectionType.Pipeline))
				{
					if((pipe.Type==PipelineType.Receive) && (pipe.FullName=="Microsoft.BizTalk.DefaultPipelines.PassThruReceive"))
					{
						rxLocation.ReceivePipeline=pipe;
						break;
					}
				}

				// Set the receive handler
				foreach(IReceiveHandler handler in explorer.GetCollection(CollectionType.ReceiveHandler))
				{
					if(handler.TransportType==rxLocation.TransportType)
					{
						rxLocation.ReceiveHandler=handler;
						break;
					}
				}

				foreach(IPipeline pipe in explorer.GetCollection(CollectionType.Pipeline))
				{
					if((pipe.Type==PipelineType.Send) && (pipe.FullName=="Microsoft.BizTalk.DefaultPipelines.PassThruTransmit"))
					{
						receivePort.SendPipeline=pipe;
						break;
					}
				}

				// Enable this receive location
				rxLocation.Enable=true;

				// Save changes
				explorer.SaveChanges();
			}
			catch(Exception e)
			{
				retMsg=e.Message.ToString();
				if(explorer!=null)
				{
					explorer.DiscardChanges();
				}
				return false;
			}

			return true;
		}

		/// <summary>
		/// Creates solicit-response HTTP send port
		/// </summary>
		/// <param name="uri">Send port URI</param>
		/// <param name="portName">Send port name</param>
		/// <param name="receivePort">Name of the receive port to bind this send port to</param>
		/// <returns>True if successful, otherwise false</returns>
		public static bool CreateHttpSolicitResponsePort(string uri, string portName, string receivePort, string affiliateApplication, ref string retMsg)
		{
			IBtsCatalogExplorer explorer=null;
			try
			{
				string mgmtDBName="BizTalkMgmtDb";
				string mgmtDBServer="localhost";

				ManagementClass groupSettings=new ManagementClass("root\\MicrosoftBizTalkServer:MSBTS_GroupSetting");
				foreach(ManagementObject obj in groupSettings.GetInstances())
				{
					mgmtDBName=(string)obj.Properties["MgmtDbName"].Value;
					mgmtDBServer=(string)obj.Properties["MgmtDbServerName"].Value;
				}

				// Get BizTalk Explorer object
				explorer=new BtsCatalogExplorer();
				explorer.ConnectionString="SERVER="+mgmtDBServer+";DATABASE="+mgmtDBName+";Integrated Security=SSPI";

				// Delete this port if it already exists
				foreach(ISendPort port in explorer.GetCollection(CollectionType.SendPort))
				{
					if(port.Name.ToLower()==portName.ToLower())
					{
						port.Status=PortStatus.Bound;
						explorer.RemoveSendPort(port);
						break;
					}
				}
				explorer.SaveChanges();

				// Add send port
				ISendPort transmitPort=explorer.AddNewSendPort(false, true);
				transmitPort.Name=portName;

				ITransportInfo TransInfo=transmitPort.PrimaryTransport;

				TransInfo.Address=uri;

				// Set protocol type
				foreach(IProtocolType protocol in explorer.GetCollection(CollectionType.ProtocolType))
				{
					if(protocol.Name=="HTTP")
						TransInfo.TransportType=protocol;
				}

				// Set the transport data
				string transportTypeData=
					"<CustomProps>"+
					"<Address vt=\"8\">"+TransInfo.Address+"</Address>"+
					"<IsSolicitResponse vt=\"11\">1</IsSolicitResponse>"+
					"<RequestTimeout vt=\"3\">120</RequestTimeout>"+
					"<MaxRedirects vt=\"3\">0</MaxRedirects>"+
					"<ContentType vt=\"8\">text/xml</ContentType>"+
					"<URI vt=\"8\">"+TransInfo.Address+"</URI>"+
					"<UseSSO vt=\"11\">1</UseSSO>"+
					"<AffiliateApplicationName vt=\"8\">"+affiliateApplication+"</AffiliateApplicationName>"+
					"<AuthenticationScheme vt=\"8\">Basic</AuthenticationScheme>"+
					"</CustomProps>";

				// Set transport config
				TransInfo.TransportTypeData=transportTypeData;
				ITransportInfo transInfo=transmitPort.PrimaryTransport;

				// Set reference to transmit pipeline
				foreach(IPipeline pipe in explorer.GetCollection(CollectionType.Pipeline))
				{
					if((pipe.Type==PipelineType.Send) && (pipe.FullName=="Microsoft.BizTalk.DefaultPipelines.PassThruTransmit"))
					{
						transmitPort.SendPipeline=pipe;
						break;
					}
				}

				// For synchronous communications we should set up a receive pipeline
				foreach(IPipeline pipe in explorer.GetCollection(CollectionType.Pipeline))
				{
					if((pipe.Type==PipelineType.Receive) && (pipe.FullName=="Microsoft.BizTalk.DefaultPipelines.PassThruReceive"))
					{
						transmitPort.ReceivePipeline=pipe;
						break;
					}
				}

				// Set filter
				transmitPort.Filter=
					"<Filter>"+
					"<Group>"+
					"<Statement Property=\"BTS.ReceivePortName\" Operator=\"0\" Value=\""+receivePort+"\" />"+
					"</Group>"+
					"</Filter>";
			
				// Enlist and start send port
				transmitPort.Status=PortStatus.Started;

				// Remember this created send port
				explorer.SaveChanges();
			}
			catch(Exception e)
			{
				retMsg=e.Message.ToString();
				if(explorer!=null)
				{
					explorer.DiscardChanges();
				}
				return false;
			}

			return true;
		}
	}
	#endregion

	#region SsoConfigurator
	/// <summary>
	/// Creates SSO configuration
	/// </summary>
	public class SsoConfigurator
	{
		private static string[] Applications=null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public SsoConfigurator()
		{
		}

		/// <summary>
		/// Retrieves the list of existing SSO applications
		/// </summary>
		/// <returns>List of application names</returns>
		public static string[] GetApplications()
		{
			if(Applications==null)
			{
				string[] descs;
				string[] contacts;
				ISSOMapper mapper=new ISSOMapper();
				mapper.GetApplications(out Applications, out descs, out contacts);
			}

			return Applications;
		}

		/// <summary>
		/// Adds mapping
		/// </summary>
		/// <param name="application">Affiliate application</param>
		/// <param name="user">Windows user</param>
		/// <param name="XU">External user</param>
		/// <param name="XP">External password</param>
		/// <returns>True if successful, otherwise false</returns>
		public static bool AddMapping(string application, string user, string XU, string XP)
		{
			try
			{
				// Set mapping
				ISSOMapper mapper=new ISSOMapper();
				ISSOMapping mapping=new ISSOMapping();

				string username=user.Substring(user.IndexOf('\\')+1);
				string userdomain=user.Substring(0, user.IndexOf('\\'));

				mapping.WindowsDomainName=userdomain;
				mapping.WindowsUserName=username;
				mapping.ApplicationName=application;
				mapping.ExternalUserName=XU;

				mapping.Create(0);
		
				// Set credentials
				string[] credentials=new string[]{XP};
				mapper.SetExternalCredentials(application, XU, ref credentials);
			
				mapping.Enable(0);
			}
			catch
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Enables tickets
		/// </summary>
		/// <returns>True if successful, otherwise false</returns>
		public static bool EnableTickets()
		{
			try
			{
				ISSOAdmin admin=new ISSOAdmin();

				int flags=0;
				int appDeleteMax=-1;
				int mappingDeleteMax=-1;
				int ntpLookupMax=-1;
				int xplLookupMax=-1;
				int ticketTimeout=-1;
				int cacheTimeout=-1;
				string secretServer=null;
				string ssoAdminGroup=null;
				string affiliateAppMgrGroup=null;
			
				// Get current default settings
				admin.GetGlobalInfo(out flags, out appDeleteMax, out mappingDeleteMax, out ntpLookupMax,
					out xplLookupMax, out ticketTimeout, out cacheTimeout, out secretServer,
					out ssoAdminGroup, out affiliateAppMgrGroup);

				// Update global settings
				admin.UpdateGlobalInfo(SSOFlag.SSO_FLAG_ALLOW_TICKETS | SSOFlag.SSO_FLAG_VALIDATE_TICKETS, 
					SSOFlag.SSO_FLAG_ALLOW_TICKETS | SSOFlag.SSO_FLAG_VALIDATE_TICKETS, 
					ref appDeleteMax, ref mappingDeleteMax, ref ntpLookupMax, 
					ref xplLookupMax, ref ticketTimeout, ref cacheTimeout, null, null, null); 
			}
			catch
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Adds SSO application
		/// </summary>
		/// <param name="name">Application name</param>
		/// <param name="admins">Application admin group</param>
		/// <param name="users">Application users group</param>
		/// <returns>True if successful, otherwise false</returns>
		public static bool AddApplication(string name, string admins, string users)
		{
			try
			{
				ISSOAdmin admin=new ISSOAdmin();

				// Create application
				admin.CreateApplication(name, "SSO Sample Application", 
					"administrator@ssoaffiliateapplication.com", users, admins, 
					SSOFlag.SSO_WINDOWS_TO_EXTERNAL | SSOFlag.SSO_FLAG_ALLOW_TICKETS | SSOFlag.SSO_FLAG_VALIDATE_TICKETS, 2);

				// Add fields
				admin.CreateFieldInfo(name, "User Id", SSOFlag.SSO_FLAG_NONE);
				admin.CreateFieldInfo(name, "Password", SSOFlag.SSO_FLAG_FIELD_INFO_MASK);

				// Enable application
				admin.UpdateApplication(name, null, null, null, null, SSOFlag.SSO_FLAG_ENABLED, SSOFlag.SSO_FLAG_ENABLED);
			}
			catch
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Check whether given application already exists
		/// </summary>
		/// <param name="application">Application name</param>
		/// <returns>True if exists, otherwise false</returns>
		public static bool Exists(string application)
		{
			string[] apps=GetApplications();
			foreach(string app in apps)
			{
				if(app.ToLower()==application.ToLower())
				{
					return true;
				}
			}

			return false;
		}
	}
	#endregion

	#region SSOSample
	/// <summary>
	/// SSO Sample class
	/// </summary>
	public class SSOSample
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public SSOSample()
		{
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			System.Windows.Forms.Application.Run(new SsoSampleWizard());
		}
	}
	#endregion
}
