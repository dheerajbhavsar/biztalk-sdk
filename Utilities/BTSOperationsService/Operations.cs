//---------------------------------------------------------------------
// File: Operations.cs
// 
// Summary: This class is invokes the ESB.BizTalkOperations Assembly. It is 
//          the wrapper that exposes the BizTalk Server Environment.
//          This is an Infrastructure Service.
//---------------------------------------------------------------------
// This file is part of the Microsoft ESB Guidance for BizTalk
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2009 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Web.Services;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Practices.ESB.BizTalkOperations;
using Microsoft.Practices.ESB.Exception.Management;

[WebService(Namespace = "http://Microsoft.Practices.ESB.BizTalkOperationsService/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Operations : WebService
{
    private BizTalkQuery bizTalkQuery;
    // error codes
    private const int BizTalkQueryServiceMessage = 251001;
    private const int BizTalkQueryServiceHostsMessage = 251002;
    private const int BizTalkQueryServiceReceiveLocationsMessage = 251003;
    private const int BizTalkQueryServiceReceiveLocationByDescMessage = 251004;
    private const int BizTalkQueryServiceSendPortsMessage = 251005;
    private const int BizTalkQueryServiceSendPortsByDescMessage = 251006;
    private const int BizTalkQueryServiceOrchestrationsMessage = 251007;
    private const int BizTalkQueryServiceSystemStatusMessage = 251008;
    private const int BizTalkQueryServiceApplicationStatusMessage = 251009;
    private const int BizTalkQueryServiceApplicationsMessage = 251010;
    private const int BizTalkQueryServiceStatusChangedMessage = 251011;
    private const int BizTalkQueryServiceMessageFlowMessage = 251012;
    private const int BizTalkQueryServiceTrackingBodyMessage = 251013;
    private const int BizTalkQueryServiceUpdateReceiveLocationDescMessage = 251014;
    private const int BizTalkQueryServiceUpdateSendPortDescMessage = 251015;
    private const int BizTalkQueryServiceLiveMessageBodyMessage = 251016;
    private const int BizTalkQueryServiceOrchestrationInstanceMessage = 251017;
    private const int BizTalkQueryServiceMessageInstanceMessage = 251018;


    /// <summary>
    /// looks up exceptions descriptions based on error codes. prepends "e"
    /// for lookup in resource file
    /// </summary>
    /// <param name="errorCode">code represented by constant</param>
    /// <returns>string representing error code</returns>
    private static string GetErrorDescription(int errorCode)
    {
        try
        {
            // Get the culture of the currently executing thread.
            // The value of ci will determine the culture of
            // the resources that the resource manager retrieves.
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            // Retrieve the value of the string resource and return it 
            string message = ESB.BizTalkOperationsService.Exceptions.ResourceManager.GetString("e" + System.Convert.ToString(errorCode, NumberFormatInfo.CurrentInfo), ci);
            return message;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// looks up exceptions descriptions based on error codes. prepends "e"
    /// for lookup in resource file
    /// </summary>
    /// <param name="errorCode">code represented by constant</param>
    /// <param name="arguments">optional arguments to format in message</param>
    /// <returns>string representing error code</returns>
    private static string GetErrorDescription(int errorCode,
        params Object[] arguments)
    {
        try
        {
            // Get the culture of the currently executing thread.
            // The value of ci will determine the culture of
            // the resources that the resource manager retrieves.
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            // Retrieve the value of the string resource and return it 
            string message = ESB.BizTalkOperationsService.Exceptions.ResourceManager.GetString("e" + System.Convert.ToString(errorCode, NumberFormatInfo.CurrentInfo), ci);
            return string.Format(ci, message, arguments);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// Default constructor. Initializes the BizTalkQuery class
    /// </summary>
    public Operations()
    {
        try
        {
            bizTalkQuery = new BizTalkQuery();
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceMessage), SoapException.ServerFaultCode, ex);
        }
    }


    /// <summary>
    /// Gets Host Instance information. Empty Host Name returns all BizTalk Server hosts.
    /// </summary>
    /// <param name="hostName">Biztalk Server Host Name: BizTalkServerApplication</param>
    /// <returns>Collection<BTQuery.BTHost></returns>
    [WebMethod]
    public Collection<BizTalkQuery.BTHost> Hosts(string name)
    {
        // the hostName parameter can be an empty string
        try
        {
            Collection<BizTalkQuery.BTHost> BTHosts = bizTalkQuery.Hosts(name);
            return BTHosts;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceHostsMessage), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets all the Receive Locations Information. 
    /// Empty Location Name returns all Receive Locations.
    /// </summary>
    /// <param name="name">Recieve Location NAme</param>
    /// <returns>BTQuery.BTSysStatus</returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus ReceiveLocations(string name)
    {
        // receive location parameter can be empty
        // A null return is a valid return value
        try
        {
            Collection<BizTalkQuery.BTReceiveLocation> retval = bizTalkQuery.ReceiveLocations(name);
            BizTalkQuery.BTSysStatus retvalStatus = new BizTalkQuery.BTSysStatus();
            if (retval != null)
                retvalStatus.ReceiveLocations = retval;
            return retvalStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);
            
            throw new SoapException(GetErrorDescription(BizTalkQueryServiceReceiveLocationsMessage), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets all the Receive Locations Information. 
    /// Empty Location description returns all Receive Locations.
    /// </summary>
    /// <param name="description">Recieve Location Description</param>
    /// <returns>BTQuery.BTSysStatus</returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus ReceiveLocationsByDescription(string description)
    {
        // description parameter may be null
        try
        {
            Collection<BizTalkQuery.BTReceiveLocation> retval = bizTalkQuery.ReceiveLocationsByDescription(description);
            BizTalkQuery.BTSysStatus retvalStatus = new BizTalkQuery.BTSysStatus();
            if (retval != null)
                retvalStatus.ReceiveLocations = retval;
            return retvalStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceReceiveLocationByDescMessage), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets all the Send Port Information. 
    /// Empty Port Name returns all Send Ports.
    /// </summary>
    /// <param name="name">Send Port Name</param>
    /// <returns>BTQuery.BTSysStatus</returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus SendPorts(string name)
    {
        // Send port parameter name may be empty
        try
        {
            Collection<BizTalkQuery.BTSendPort> retval = bizTalkQuery.SendPorts(name);
            BizTalkQuery.BTSysStatus retvalStatus = new BizTalkQuery.BTSysStatus();
            if (retval != null)
                retvalStatus.SendPorts = retval;
            return retvalStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceSendPortsMessage), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets all the Send Port Information. 
    /// Empty Port Description returns all Send Ports.
    /// </summary>
    /// <param name="description">Send Port Description</param>
    /// <returns>BTQuery.BTSysStatus</returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus SendPortsByDescription(string description)
    {
        // description parameter may be empty
        try
        {
            Collection<BizTalkQuery.BTSendPort> retval = bizTalkQuery.SendPortsByDescription(description);
            BizTalkQuery.BTSysStatus retvalStatus = new BizTalkQuery.BTSysStatus();
            if (retval != null)
                retvalStatus.SendPorts = retval;
            return retvalStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceSendPortsByDescMessage), SoapException.ServerFaultCode, ex);
        }
    }
    /// <summary>
    /// Gets all the Orchestration Information. 
    /// Empty Orchestration Name returns all Orchestrations.
    /// </summary>
    /// <param name="orchName">Orchestration Name</param>
    /// <returns>BTQuery.BTSysStatus </returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus Orchestrations(string name)
    {
        // orchestration name can by empty
        try
        {
            Collection<BizTalkQuery.BTOrchestration> retval = bizTalkQuery.Orchestrations(name);
            BizTalkQuery.BTSysStatus retvalStatus = new BizTalkQuery.BTSysStatus();
            if (retval != null)
                retvalStatus.Orchestrations = retval;
            
            return retvalStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceOrchestrationsMessage), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets Complete System Status.
    /// </summary>
    /// <returns>BTQuery.BTSysStatus </returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus SystemStatus()
    {
        try
        {
            BizTalkQuery.BTSysStatus BTStatus = bizTalkQuery.SystemStatus();
            return BTStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceSystemStatusMessage), SoapException.ServerFaultCode, ex);
        }
    }
    /// <summary>
    /// Returns information about specific BizTalk Server Application. 
    /// The input is the Application Name. 
    /// The Services returns information about Orchestrations, 
    /// Send Ports, Receive locations and Host Information to which the application belongs. 
    /// </summary>
    /// <param name="appName">BizTalk Application Name</param>
    /// <returns>BTQuery.BTSysStatus</returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus ApplicationStatus(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            BizTalkQuery.BTSysStatus BTStatus = bizTalkQuery.ApplicationStatus(name);
            return BTStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceApplicationStatusMessage, name), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Returns all Application Names and Description. 
    /// The BTQuery invokes the BizTalk Explorer, iterates through 
    /// the applications and returns the application name and description.
    /// </summary>
    /// <returns>Collection<BTQuery.BTApplication></returns>
    [WebMethod]
    public Collection<BizTalkQuery.BTApplication> Applications()
    {
        try
        {
            Collection<BizTalkQuery.BTApplication> apps = bizTalkQuery.Applications();
            return apps;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceApplicationsMessage), SoapException.ServerFaultCode, ex);
        }
    }
    /// <summary>
    /// Input is a timestamp. Retrieves BizTalk status 
    /// (ports, hosts, orchestrations), but only returns items that 
    /// have been modified after the timestamp parameter. 
    /// </summary>
    /// <param name="datetime">Time Stamp</param>
    /// <returns>BTQuery.BTSysStatus</returns>
    [WebMethod]
    public BizTalkQuery.BTSysStatus StatusChanged(DateTime datetime)
    {
        try
        {
            BizTalkQuery.BTSysStatus BTStatus;
            BTStatus = bizTalkQuery.StatusChanged(datetime);
            return BTStatus;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceStatusChangedMessage, datetime.ToString()), SoapException.ServerFaultCode, ex);
        }
    }

    //------------------------tracking methods-----------------------
    /// <summary>
    /// Gets the message flow for a given InstanceId.
    /// </summary>
    /// <param name="instanceId">Service/Instance Id to retreive the message flow</param>
    /// <returns>BTQuery.RouteTreeNode</returns>
    [WebMethod]
    public RouteTreeNode MessageFlowTree(string instanceId)
    {
        try
        {
            if (string.IsNullOrEmpty(instanceId))
                throw new ArgumentNullException("instanceId");

            RouteTreeNode root = bizTalkQuery.RouteTree(instanceId);
            return root;
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceMessageFlowMessage, instanceId), SoapException.ServerFaultCode, ex);
        }
    }


    /// <summary>
    /// Gets the message for a given Message ID after a message is processed. 
    /// method to return the body of a given message id
    /// </summary>
    /// <param name="msg">Message ID</param>
    /// <returns></returns>
    [WebMethod]
    public BizTalkQuery.BTMsgBody GetTrackedMessageBody(string messageId)
    {
        try
        {
            if (string.IsNullOrEmpty(messageId))
                throw new ArgumentNullException("messageId");

            return bizTalkQuery.GetTrackedMessageBody(messageId);
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceTrackingBodyMessage, messageId), SoapException.ServerFaultCode, ex);
        }
    }

   
    /// <summary>
    /// This updates the description of the receive location 
    /// with the given information. This takes a Application Name, 
    /// Receive Port Name, Receive Location Name and 
    /// Receive Location Description that needs to be updated. 
    /// </summary>
    /// <param name="ApplicationName">Application Name</param>
    /// <param name="RecievePortName">Recieve Port Name</param>
    /// <param name="RecieveLocationName">Recieve Location Name</param>
    /// <param name="RecieveLocationDescription">Recieve Location Description</param>
    /// <returns></returns>
    [WebMethod]
    public string UpdateReceiveLocationDescription(string applicationName,
            string receivePortName, string receiveLocationName, string receiveLocationDescription)
    {
        try
        {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException("applicationName");

            if (string.IsNullOrEmpty(receivePortName))
                throw new ArgumentNullException("receivePortName");

            if (string.IsNullOrEmpty(receiveLocationName))
                throw new ArgumentNullException("receiveLocationName");

            if (string.IsNullOrEmpty(receiveLocationDescription))
                throw new ArgumentNullException("receiveLocationDescription");

            return
                bizTalkQuery.UpdateReceiveLocationDescription(applicationName, receivePortName, receiveLocationName,
                                                    receiveLocationDescription);
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceUpdateReceiveLocationDescMessage), SoapException.ServerFaultCode, ex);
        }
    }

    [WebMethod]
    public string UpdateSendPortDescription(string sendPortName, string sendPortDescription)
    {
        try
        {
            if (string.IsNullOrEmpty(sendPortName))
                throw new ArgumentNullException("sendPortName");
            if (string.IsNullOrEmpty(sendPortDescription))
                throw new ArgumentNullException("sendPortDescription");

            return
                bizTalkQuery.UpdateSendPortDescription(sendPortName, sendPortDescription);
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceUpdateSendPortDescMessage), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets the message body with the given Message ID(GUID). 
    /// This is information from the live environment.  
    /// </summary>
    /// <param name="msg">Message Type</param>
    /// <param name="instanceID">Message GUID</param>
    /// <returns>BTQuery.BTMsgBody</returns>
    [WebMethod]
    public BizTalkQuery.BTMsgBody GetLiveMessageBody(string messageId, string instanceId)
    {
        try
        {
            if (string.IsNullOrEmpty(messageId))
                throw new ArgumentNullException("messageId");

            return bizTalkQuery.GetLiveMessageBody(messageId, instanceId);
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceLiveMessageBodyMessage, messageId), SoapException.ServerFaultCode, ex);
        }
    }

    /// <summary>
    /// Gets all the Service Instances Information for a given Orchestration Name. 
    /// </summary>
    /// <param name="orchName">Orchestration Name</param>
    /// <returns>Collection<BTQuery.BTOrchInstance></returns>
    [WebMethod]
    public Collection<BizTalkQuery.BTOrchestrationInstance> GetOrchestrationInstances(string name)
    {
        try
        {
            return bizTalkQuery.GetOrchestrationInstances(name);
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceOrchestrationInstanceMessage, name), SoapException.ServerFaultCode, ex);
        }
    }
 

    /// <summary>
    ///  Gets all messages for the Message Type entered in the BizTalk Server Environment.  
    /// </summary>
    /// <param name="msgName">Message Type. Ex: http://Schemas.Microsoft.Practices.ESB.AP/test#RetailOrder</param>
    /// <returns>Collection<BTQuery.BTMsgInstance></returns>
    [WebMethod]
    public Collection<BizTalkQuery.BTMsgInstance> GetMessageInstances(string messageType)
    {
        try
        {
            return bizTalkQuery.GetMessageInstances(messageType);
        }
        catch (System.Exception ex)
        {
            // log and throw new error
            EventLogger.LogMessage(EventFormatter.FormatEvent(MethodInfo.GetCurrentMethod(), ex),
                    EventLogEntryType.Error, (int)EventLogger.EventId.BizTalkQueryService);

            throw new SoapException(GetErrorDescription(BizTalkQueryServiceMessageInstanceMessage, messageType), SoapException.ServerFaultCode, ex);
        }
    }
}