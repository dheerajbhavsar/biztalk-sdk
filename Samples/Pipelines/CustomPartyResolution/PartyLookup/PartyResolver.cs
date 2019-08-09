//---------------------------------------------------------------------
// File: PartyResolver.cs
// 
// Sample: CustomPartyResolution
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
using System.Data;
using System.Data.SqlClient;
using System.Management;

#endregion

namespace Microsoft.Samples.BizTalk.PartyLookup
{
	public struct BizTalkParty
	{
		public string Name;
		public string SID;
	}
	
	public class PartyResolver
	{
		private static string MgmtDBConnString = String.Empty;
		static private object syncRoot = new object();

		public PartyResolver()
		{
			
		}

		public BizTalkParty GetPartyFromAlias(string aliasName, string aliasQualifier, string aliasValue)
		{
			BizTalkParty result;

			SqlConnection connection = new SqlConnection(GetMgmtDBConnectionString());
			SqlCommand partyResolveCMD = new SqlCommand("admsvr_GetPartyByAliasNameValue", connection);
			partyResolveCMD.CommandType = CommandType.StoredProcedure;

			if (aliasName.Length > 256) throw new ArgumentException("Length of the alias name exceed 256 characters");
			SqlParameter param = partyResolveCMD.Parameters.Add("@nvcAliasName", SqlDbType.NVarChar, 256);
			param.Value = aliasName;

			if (aliasQualifier.Length > 64) throw new ArgumentException("Length of the alias qualifier exceeds 64 characters");
			param = partyResolveCMD.Parameters.Add("@nvcAliasQualifier", SqlDbType.NVarChar, 64);
			param.Value = aliasQualifier;

			if ( aliasValue.Length > 256) throw new ArgumentException("Length of the alias value exceeds 256 characters");
			param = partyResolveCMD.Parameters.Add("@nvcAliasValue", SqlDbType.NVarChar, 256);
			param.Value = aliasValue;

			param = partyResolveCMD.Parameters.Add("@nvcSID", SqlDbType.NVarChar, 256);
			param.Direction = ParameterDirection.Output;
			param.Value = string.Empty;

			param = partyResolveCMD.Parameters.Add("@nvcName", SqlDbType.NVarChar, 256);
			param.Direction = ParameterDirection.Output;
			param.Value = string.Empty;


			connection.Open();
			SqlDataReader reader = partyResolveCMD.ExecuteReader();
		
			result.Name = partyResolveCMD.Parameters["@nvcName"].Value.ToString();
			result.SID = partyResolveCMD.Parameters["@nvcSID"].Value.ToString();

			if (result.Name.Length == 0) result.Name = "Guest";
			if (result.SID.Length == 0) result.SID = "s-1-5-7";
			
			reader.Close();
			connection.Close();

			return result;
		}

		/// <summary>
		///  Looks in the registry to determine the database connection string for the
		///  BizTalk Management Database for the BizTalk Group that this machine belongs
		///  to.
		/// </summary>
		/// <returns>Database connection string to BTS Management Database</returns>
		private string GetMgmtDBConnectionString()
		{
			string BTSMgmtDBName = String.Empty, BTSMgmtDBServerName = String.Empty;
			
			//check to see if the string has already been retrieved.  If not, get it and store it.
			if( MgmtDBConnString.Length == 0 ) 
			{
				lock( syncRoot )
				{
					if( MgmtDBConnString.Length == 0)
					{
						ManagementObjectSearcher searcher =
							new ManagementObjectSearcher(@"root\MicrosoftBizTalkServer", "SELECT * FROM MSBTS_GroupSetting");
						foreach (ManagementObject Group in searcher.Get())
						{
							if(Group != null)
							{
								Group.Get();
								BTSMgmtDBName = Group["MgmtDbName"].ToString();
								BTSMgmtDBServerName = Group["MgmtDbServerName"].ToString();

							}						
						}

						if( BTSMgmtDBName.Length == 0 || BTSMgmtDBServerName.Length == 0 )
							throw new ApplicationException("Unable to find Management Database Name or Management Database Server Name");

						// Assuming Integrated Security is being used for database connection.
						MgmtDBConnString = string.Format("SERVER={0};DATABASE={1};Integrated Security=SSPI", BTSMgmtDBServerName, BTSMgmtDBName);
					}
				}
			}
		
			return MgmtDBConnString;
		}
	}
}
