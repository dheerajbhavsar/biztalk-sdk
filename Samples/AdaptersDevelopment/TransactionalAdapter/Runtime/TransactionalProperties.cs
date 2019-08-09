
//---------------------------------------------------------------------
// File: TransactionalProperties.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework transactional adapter.
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
using System.IO;
using System.Xml;
using System.Net;
using System.Collections;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.Transactional
{
    internal class TransactionalTransmitProperties : ConfigProperties
    {
        private string uri;
        private string connectionString;
        private string cmdText;

        public string Uri { get { return uri; } }
        public string ConnectionString { get { return connectionString; } }
        public string CmdText { get { return cmdText; } }

        public TransactionalTransmitProperties (string uri)
        {
            this.uri = uri;
            this.connectionString = "";
            this.cmdText = "";
        }

        public virtual void LocationConfiguration (XmlDocument configDOM)
        {
            try
            {
                XmlNode nodeConnectionString = configDOM.SelectSingleNode("Config/connectionString");
                this.connectionString = nodeConnectionString.InnerText;

                XmlNode nodeCmdText = configDOM.SelectSingleNode("Config/cmdText");
                this.cmdText = nodeCmdText.InnerText;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public void DynamicSendConfiguration (string dynamicUri)
        {
            try
            {
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
    }

    public enum SizeUnit { Bytes, KBytes, MBytes }

    // //////////////////////////////////////////////////////////////////////////////////////////////

    internal class TransactionalReceiveProperties : ConfigProperties
    {
        private string uri;
        private int pollingInterval;
        private string connectionString;
        private string cmdText;

        public string Uri { get { return uri; } }
        public int PollingInterval { get { return pollingInterval; } }
        public string ConnectionString { get { return connectionString; } }
        public string CmdText { get { return cmdText; } }

        public TransactionalReceiveProperties (string uri)
        {
            try
            {
                this.uri = uri;
                this.pollingInterval = 3000;
                this.connectionString = "";
                this.cmdText = "";
            }
            finally
            {
            }
        }

        public virtual void HandlerConfiguration (XmlDocument configDOM)
        {
        }

        public virtual void LocationConfiguration (XmlDocument configDOM)
        {
            try
            {
                int pollingIntervalMultiplier = 1;

                XmlNode nodePollingIntervalUnit = configDOM.SelectSingleNode("Config/pollingIntervalUnit");
                switch (nodePollingIntervalUnit.InnerText.ToLower())
                {
                    case "milliseconds":
                        pollingIntervalMultiplier = 1;
                        break;
                    case "seconds":
                        pollingIntervalMultiplier = 1000;
                        break;
                    case "minutes":
                        pollingIntervalMultiplier = 1000 * 60;
                        break;
                }

                XmlNode nodePollingInterval = configDOM.SelectSingleNode("Config/pollingInterval");
                int pollingIntervalValue = int.Parse(nodePollingInterval.InnerText);

                this.pollingInterval = pollingIntervalMultiplier * pollingIntervalValue;

                XmlNode nodeConnectionString = configDOM.SelectSingleNode("Config/connectionString");
                this.connectionString = nodeConnectionString.InnerText;

                XmlNode nodeCmdText = configDOM.SelectSingleNode("Config/cmdText");
                this.cmdText = nodeCmdText.InnerText;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
    }
}
