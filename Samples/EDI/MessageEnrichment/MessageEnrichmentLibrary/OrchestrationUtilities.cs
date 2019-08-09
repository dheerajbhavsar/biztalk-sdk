//---------------------------------------------------------------------
// File: OrchestrationUtilities.cs
//
// Sample: MessageEnrichment
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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using Microsoft.XLANGs.BaseTypes;

namespace MessageEnrichmentLibrary
{
    public class OrchestrationUtilities
    {
        const string _EDIFACT = "EDIFACT";
        const string _X12 = "X12";

        public static bool IsMessageEDIFACT(string messageType)
        {
            return messageType.ToUpper().Contains(_EDIFACT);
        }

        public static bool IsMessageX12(string messageType)
        {
            return messageType.ToUpper().Contains(_X12);
        }

        public static bool IsHeaderExist(XLANGMessage message, Type type)
        {
            return message.GetPropertyValue(type) != null;
        }
    }
}
