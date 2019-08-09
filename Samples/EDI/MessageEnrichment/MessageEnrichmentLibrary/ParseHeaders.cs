//---------------------------------------------------------------------
// File: ParseHeaders.cs
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
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace MessageEnrichmentLibrary
{
    /// <summary>
    /// This class is used to parse control headers for EDI messages
    /// </summary>
    public class EDIHeaders
    {
        // Header node representation
        const string _headersPrefix = "ns0";
        const string _headersName= "Headers";
        const string _headersNamespace = "http://EDI/Headers";

        // Edi document variables
        const string _EDIDocumentStartPrefix = "ns1";
        const string _EDIDocumentTransactionSetNode = "TransactionSet";

        static XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces();

        static List<char> _regexEscapedCharacters = new List<char>(new char[] { '.', '$', '^', '{', '[', '(', '|', ')', '*', '+', '?', '\\' });
        const string _UNBGenericPattern = @"UNB{0}(?<SyntaxIdentifier>({1}|[a-zA-Z]){{4}}){2}(?<SyntaxVersionNumber>[0-9]){0}(?<SenderIdentification>{1}|[a-zA-Z0-9]{{1,35}})({2}(?<SenderIdentificationCodeQualifier>({1}|[a-zA-Z0-9]){{0,4}}))?({2}(?<AddressForReverseRouting>({1}|[a-zA-Z0-9]){{0,14}}))?{0}(?<RecipientIdentification>{1}|[a-zA-Z0-9]{{1,35}})({2}(?<RecipientIdentificationCodeQualifier>({1}|[a-zA-Z0-9]){{0,4}}))?({2}(?<RoutingAddress>({1}|[a-zA-Z0-9]){{0,14}}))?{0}(?<Date>[0-9]{{6}}){2}(?<Time>[0-9]{{4}}){0}(?<InterchangeControlReference>{1}|[a-zA-Z0-9]{{1,14}})({0}((?<RecipientPassword>{1}|[a-zA-Z0-9]{{1,14}}|#{{16}})({2}(?<RecipientPasswordQualifier>({1}|[a-zA-Z0-9]){{2}}))?)?)({0}(?<ApplicationReference>({1}|[a-zA-Z0-9]){{1,14}})?)?({0}(?<ProcessingPriorityCode>({1}|[a-zA-Z]))?)?({0}(?<AcknowledgementRequest>[0-9])?)?({0}(?<CommunicationsAgreementID>({1}|[a-zA-Z0-9]){{1,35}})?)?({0}(?<TestIndicator>[0-9])?)?";
        const string _UNGGenericPattern = @"UNG{0}(?<FunctionalGroupIdentification>{1}|[a-zA-Z0-9]{{1,6}}){0}(?<SenderIdentification>{1}|[a-zA-Z0-9]{{1,35}})({2}(?<SenderIdentificationCodeQualifier>{1}|[a-zA-Z0-9]{{1,4}}))?{0}(?<RecipientIdentification>{1}|[a-zA-Z0-9]{{1,35}})({2}(?<RecipientIdentificationCodeQualifier>{1}|[a-zA-Z0-9]{{1,4}}))?{0}(?<Date>[0-9]{{6}}){2}(?<Time>[0-9]{{4}}){0}(?<FunctionalGroupReferenceNumber>{1}|[a-zA-Z0-9]{{1,14}}){0}(?<ControllingAgency>{1}|[a-zA-Z0-9]{{1,2}}){0}(?<MessageVersionNumber>{1}|[a-zA-Z0-9]{{1,3}}){2}(?<MessageReleaseNumber>{1}|[a-zA-Z0-9]{{1,3}})({2}(?<AssociationAssignedCode>{1}|[a-zA-Z0-9]{{1,6}}))({2}(?<ApplicationPassword>{1}|[a-zA-Z0-9]{{1,6}}))?";

        static EDIHeaders()
        {
            _namespaces.Add(_headersPrefix, _headersNamespace);
        }

        /// <summary>
        /// Parse X12 ISA header
        /// </summary>
        /// <param name="input">Input string to parse</param>
        /// <param name="_segmentSeparator">Segment separator</param>
        /// <returns>XML document with structure from headers.xsd X12 ISA header description</returns>
        public static HeadersISA ParseX12ISAHeader(string input, out string _segmentSeparator)
        {
            _segmentSeparator = "";

            if (input == null) return null;

            int _fieldQuantity = 17;

            HeadersISA _headersISA = new HeadersISA();

            char _elementSeparator = input[3];

            string[] _splittedInput = input.Trim('\r', '\n').Split(_elementSeparator);

            try
            {
                if (_splittedInput.Length == _fieldQuantity)
                {
                    // If headers.xsd was changed, change appropriate fields for new values
                    _headersISA.DataElementSeparator = _elementSeparator.ToString();
                    _headersISA.ISA01 = _splittedInput[1];
                    _headersISA.ISA02 = _splittedInput[2];
                    _headersISA.ISA03 = _splittedInput[3];
                    _headersISA.ISA04 = _splittedInput[4];
                    _headersISA.ISA05 = _splittedInput[5];
                    _headersISA.ISA06 = _splittedInput[6];
                    _headersISA.ISA07 = _splittedInput[7];
                    _headersISA.ISA08 = _splittedInput[8];
                    _headersISA.ISA09 = _splittedInput[9];
                    _headersISA.ISA10 = _splittedInput[10];
                    _headersISA.ISA11 = _splittedInput[11];
                    _headersISA.ISA12 = _splittedInput[12];
                    _headersISA.ISA13 = _splittedInput[13];
                    _headersISA.ISA14 = _splittedInput[14];
                    _headersISA.ISA15 = _splittedInput[15];
                    _headersISA.ISA16 = _splittedInput[16].Substring(0, 1);

                    if (_splittedInput[16].Length == 2)
                    {
                        _headersISA.SegmentTerminator = _splittedInput[16].Substring(1, 1);
                        _segmentSeparator = _splittedInput[16].Substring(1, 1);
                    }

                    return _headersISA;
                }
                else
                    throw new ApplicationException("Can't parse a message. Not enough parsing elements found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to parse UNB header. Reason: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Parse X12 GS header
        /// </summary>
        /// <param name="input">Input string to parse</param>
        /// <param name="_segmentSeparator">Segment separator</param>
        /// <returns>XML document with structure from headers.xsd X12 GS header description</returns>
        public static HeadersGS ParseX12GSHeader(string input, string _segmentSeparator)
        {
            int _fieldQuantity = 8;

            if (input == null) return null;

            HeadersGS _headerGS = new HeadersGS();

            char _elementSeparator = input[2];
            char[] _segmentSeparators;

            if (string.IsNullOrEmpty(_segmentSeparator))
                _segmentSeparators = new char[] { '\r', '\n' };
            else if (_segmentSeparator.Length == 1)
                _segmentSeparators = new char[] { '\r', '\n', _segmentSeparator[0] };
            else
                return null;

            string[] _splittedInput = input.Trim(_segmentSeparators).Split(_elementSeparator);

            try
            {
                if (_splittedInput.Length == _fieldQuantity + 1)
                {
                    // If headers.xsd was changed, change appropriate fields for new values
                    _headerGS.GS01 = _splittedInput[1];
                    _headerGS.GS02 = _splittedInput[2];
                    _headerGS.GS03 = _splittedInput[3];
                    _headerGS.GS04 = _splittedInput[4];
                    _headerGS.GS05 = _splittedInput[5];
                    _headerGS.GS06 = _splittedInput[6];
                    _headerGS.GS07 = _splittedInput[7];
                    _headerGS.GS08 = _splittedInput[8];

                    return _headerGS;
                }
                else
                    throw new ApplicationException("Can't parse a message. Not enough parsing elements found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to parse ISA header. Reason: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Parse EDIFACT UNA header
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="dataElementSeparator">Data element separator</param>
        /// <param name="releaseIndicator">Release indicator</param>
        /// <param name="componentDataElementSeparator">Component data element separator</param>
        /// <param name="segmentTerminator">Segment terminator</param>
        /// <returns>XML document with structure from headers.xsd EDIFACT UNA header description</returns>
        public static HeadersUNA ParseEDIFACTUNAHeader(
            string input,
            out string dataElementSeparator,
            out string releaseIndicator,
            out string componentDataElementSeparator,
            out string segmentTerminator
            )
        {
            dataElementSeparator = "";
            releaseIndicator = "";
            componentDataElementSeparator = "";
            segmentTerminator = "";

            if (input == null) return null;

            HeadersUNA _headerUNA = new HeadersUNA();

            try
            {
                if (input.Length == 9)
                {
                    // If headers.xsd was changed, change appropriate fields for new values
                    _headerUNA.UNA1 = input[3].ToString();
                    _headerUNA.UNA2 = input[4].ToString();
                    _headerUNA.UNA3 = input[5].ToString();
                    _headerUNA.UNA4 = input[6].ToString();
                    _headerUNA.UNA5 = input[7].ToString();
                    _headerUNA.UNA6 = input[8].ToString();

                    dataElementSeparator = input[4].ToString();
                    releaseIndicator = input[6].ToString();
                    componentDataElementSeparator = input[3].ToString();
                    segmentTerminator = input[8].ToString();

                    return _headerUNA;
                }
                else
                    throw new ApplicationException("Can't parse a message. Not enough parsing elements found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to parse GS header. Reason: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Parse EDIFACT UNB header
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="dataElementSeparator">Data element separator</param>
        /// <param name="releaseIndicator">Release indicator</param>
        /// <param name="componentDataElementSeparator">Component data element separator</param>
        /// <param name="segmentTerminator">Segment terminator</param>
        /// <returns>XML document with structure from headers.xsd EDIFACT UNA header description</returns>
        public static HeadersUNB ParseEDIFACTUNBHeader(
            string input,
            string dataElementSeparator,
            string releaseIndicator,
            string componentDataElementSeparator,
            string segmentTerminator
            )
        {
            if (input == null) return null;

            HeadersUNB _UNBHeader = new HeadersUNB();

            Regex _UNBRegex = new Regex(CreateParsePattern(_UNBGenericPattern, dataElementSeparator, releaseIndicator, componentDataElementSeparator, segmentTerminator));
            Match _UNBMatch = _UNBRegex.Match(input);

            string _escapedReleaseIndicator = EscapeRegexCharacter(releaseIndicator);

            try
            {
                if (_UNBMatch != null)
                {
                    // If headers.xsd was changed, change appropriate fields for new values
                    _UNBHeader.UNB1 = new HeadersUNBUNB1();
                    _UNBHeader.UNB1.UNB11 = _UNBMatch.Groups["SyntaxIdentifier"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB1.UNB12 = _UNBMatch.Groups["SyntaxVersionNumber"].ToString();
                    _UNBHeader.UNB2 = new HeadersUNBUNB2();
                    _UNBHeader.UNB2.UNB21 = _UNBMatch.Groups["SenderIdentification"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB2.UNB22 = _UNBMatch.Groups["SenderIdentificationCodeQualifier"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB2.UNB23 = _UNBMatch.Groups["AddressForReverseRouting"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB3 = new HeadersUNBUNB3();
                    _UNBHeader.UNB3.UNB31 = _UNBMatch.Groups["RecipientIdentification"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB3.UNB32 = _UNBMatch.Groups["RecipientIdentificationCodeQualifier"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB3.UNB33 = _UNBMatch.Groups["RoutingAddress"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB4 = new HeadersUNBUNB4();
                    _UNBHeader.UNB4.UNB41 = _UNBMatch.Groups["Date"].ToString();
                    _UNBHeader.UNB4.UNB42 = _UNBMatch.Groups["Time"].ToString();
                    _UNBHeader.UNB5 = _UNBMatch.Groups["InterchangeControlReference"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB6 = new HeadersUNBUNB6();
                    _UNBHeader.UNB6.UNB61 = _UNBMatch.Groups["RecipientPassword"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB6.UNB62 = _UNBMatch.Groups["RecipientPasswordQualifier"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB7 = _UNBMatch.Groups["ApplicationReference"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB8 = _UNBMatch.Groups["ProcessingPriorityCode"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB9 = _UNBMatch.Groups["AcknowledgementRequest"].ToString();
                    _UNBHeader.UNB10 = _UNBMatch.Groups["CommunicationsAgreementId"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNBHeader.UNB11 = _UNBMatch.Groups["TestIndicator"].ToString();

                    return _UNBHeader;
                }
                else
                    throw new ApplicationException("Can't parse a message. Not enough parsing elements found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to parse UNB header. Reason: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Parse EDIFACT UNG header
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="dataElementSeparator">Data element separator</param>
        /// <param name="releaseIndicator">Release indicator</param>
        /// <param name="componentDataElementSeparator">Component data element separator</param>
        /// <param name="segmentTerminator">Segment terminator</param>
        /// <returns>XML document with structure from headers.xsd EDIFACT UNA header description</returns>
        public static HeadersUNG ParseEDIFACTUNGHeader(
            string input,
            string dataElementSeparator,
            string releaseIndicator,
            string componentDataElementSeparator,
            string segmentTerminator
            )
        {
            if (input == null) return null;

            HeadersUNG _UNGHeader = new HeadersUNG();

            Regex _UNGRegex = new Regex(CreateParsePattern(_UNGGenericPattern, dataElementSeparator, releaseIndicator, componentDataElementSeparator, segmentTerminator));
            Match _UNGMatch = _UNGRegex.Match(input);

            string _escapedReleaseIndicator = EscapeRegexCharacter(releaseIndicator);

            try
            {
                if (_UNGMatch != null)
                {
                    // If headers.xsd was changed, change appropriate fields for new values
                    _UNGHeader.UNG1 = _UNGMatch.Groups["FunctionalGroupIdentification"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG2 = new HeadersUNGUNG2();
                    _UNGHeader.UNG2.UNG21 = _UNGMatch.Groups["SenderIdentification"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG2.UNG22 = _UNGMatch.Groups["SenderIdentificationCodeQualifier"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG3 = new HeadersUNGUNG3();
                    _UNGHeader.UNG3.UNG31 = _UNGMatch.Groups["RecipientIdentification"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG3.UNG32 = _UNGMatch.Groups["RecipientIdentificationCodeQualifier"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG4 = new HeadersUNGUNG4();
                    _UNGHeader.UNG4.UNG41 = _UNGMatch.Groups["Date"].ToString();
                    _UNGHeader.UNG4.UNG42 = _UNGMatch.Groups["Time"].ToString();
                    _UNGHeader.UNG5 = _UNGMatch.Groups["FunctionalGroupReferenceNumber"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG6 = _UNGMatch.Groups["ControllingAgency"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG7 = new HeadersUNGUNG7();
                    _UNGHeader.UNG7.UNG71 = _UNGMatch.Groups["MessageVersionNumber"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG7.UNG72 = _UNGMatch.Groups["MessageReleaseNumber"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG7.UNG73 = _UNGMatch.Groups["AssociationAssignedCode"].ToString().Replace(_escapedReleaseIndicator, String.Empty);
                    _UNGHeader.UNG8 = _UNGMatch.Groups["ApplicationPassword"].ToString().Replace(_escapedReleaseIndicator, String.Empty);

                    return _UNGHeader;
                }
                else
                    throw new ApplicationException("Can't parse a message. Not enough parsing elements found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to parse UNB header. Reason: {0}", ex.Message), ex);
            }
        }


        /// <summary>
        /// This method serializes EDIFACT headers UNA, UNB and UNG to an XML document, so each segment is an element and is wrapped around header elements
        /// </summary>
        /// <param name="stringHeaderUNA">String with a UNA segment</param>
        /// <param name="stringHeaderUNB">String with a UNA segment</param>
        /// <param name="stringHeaderUNG">String with a UNA segment</param>
        /// <returns>XmlDocument representation of a UNA, UNB and UNG headers</returns>
        public static XmlDocument SerializeEDIFACTHeaders(string stringHeaderUNA, string stringHeaderUNB, string stringHeaderUNG)
        {
            HeadersUNA headerUNA = null;
            HeadersUNB headerUNB = null;
            HeadersUNG headerUNG = null;

            string DataElementSeparator = string.Empty;
            string ReleaseIndicator = string.Empty;
            string ComponentDataElementSeparator = string.Empty;
            string SegmentSeparator = string.Empty;

            if (!string.IsNullOrEmpty(stringHeaderUNA))
                headerUNA = MessageEnrichmentLibrary.EDIHeaders.ParseEDIFACTUNAHeader(
                stringHeaderUNA,
                out DataElementSeparator,
                out ReleaseIndicator,
                out ComponentDataElementSeparator,
                out SegmentSeparator
                );
            else
            {
                headerUNA = new HeadersUNA();
                headerUNA.UNA2 = "+";
                headerUNA.UNA4 = "?";
                headerUNA.UNA1 = ":";
                headerUNA.UNA6 = "'";
                headerUNA.UNA3 = ",";
                headerUNA.UNA5 = "*";

                DataElementSeparator = headerUNA.UNA2;
                ReleaseIndicator = headerUNA.UNA4;
                ComponentDataElementSeparator = headerUNA.UNA1;
                SegmentSeparator = headerUNA.UNA6;
            }

            headerUNB = MessageEnrichmentLibrary.EDIHeaders.ParseEDIFACTUNBHeader(
            stringHeaderUNB,
            DataElementSeparator,
            ReleaseIndicator,
            ComponentDataElementSeparator,
            SegmentSeparator
            );

            if (!string.IsNullOrEmpty(stringHeaderUNG))
            {
                headerUNG = MessageEnrichmentLibrary.EDIHeaders.ParseEDIFACTUNGHeader(
                stringHeaderUNG,
                DataElementSeparator,
                ReleaseIndicator,
                ComponentDataElementSeparator,
                SegmentSeparator
                );
            }

            // Skip xml declaration for a newly created documents
            StringBuilder _newXML = new StringBuilder();
            XmlWriterSettings _settings = new XmlWriterSettings();
            _settings.OmitXmlDeclaration = true;

            // Compose result document
            XmlWriter _writer = XmlWriter.Create(_newXML, _settings);
            _writer.WriteStartElement(_headersPrefix, _headersName, _headersNamespace);
            _writer.WriteRaw(XmlToString(Serialize(headerUNA)));
            _writer.WriteRaw(XmlToString(Serialize(headerUNB)));
            if (headerUNG != null)
                _writer.WriteRaw(XmlToString(Serialize(headerUNG)));
            _writer.WriteEndElement();
            _writer.Flush();

            XmlDocument _newDocument = new XmlDocument();
            _newDocument.LoadXml(_newXML.ToString());
            return _newDocument;
        }

        public static XmlDocument SerializeX12Headers(string stringHeaderISA, string stringHeaderGS)
        {
            HeadersISA headerISA;
            HeadersGS headerGS;

            string SegmentSeparator;

            headerISA = MessageEnrichmentLibrary.EDIHeaders.ParseX12ISAHeader(stringHeaderISA, out SegmentSeparator);
            headerGS = MessageEnrichmentLibrary.EDIHeaders.ParseX12GSHeader(stringHeaderGS, SegmentSeparator);

            StringBuilder _newXML = new StringBuilder();
            XmlWriterSettings _settings = new XmlWriterSettings();
            _settings.OmitXmlDeclaration = true;
            
            XmlWriter _writer = XmlWriter.Create(_newXML, _settings);
            _writer.WriteStartElement(_headersPrefix, _headersName, _headersNamespace);
            _writer.WriteRaw(XmlToString(Serialize(headerISA)));
            _writer.WriteRaw(XmlToString(Serialize(headerGS)));
            _writer.WriteEndElement();
            _writer.Flush();

            XmlDocument _newDocument = new XmlDocument();
            _newDocument.LoadXml(_newXML.ToString());
            return _newDocument;
        }

        /// <summary>
        /// Utility method to serialize objects
        /// </summary>
        /// <param name="serializableObject">Object to serialize. Object needs to be serializable.</param>
        /// <returns>Xml representation of an object to be serialized</returns>
        public static XmlDocument Serialize(object serializableObject)
        {
            StringWriter _writer = new StringWriter();
            XmlSerializer _serializer = new XmlSerializer(serializableObject.GetType());
            _serializer.Serialize(_writer, serializableObject, _namespaces);
            XmlDocument _result = new XmlDocument();
            _result.PreserveWhitespace = true;
            _result.LoadXml(_writer.ToString());
            _result.RemoveChild(_result.ChildNodes[0]);
            return _result;
        }

        /// <summary>
        /// Creates concrete pattern for parsing EDIFACT segments using given separator values
        /// </summary>
        /// <param name="genericPattern">Generic pattern to be changed to concrete one</param>
        /// <param name="dataElementSeparator">Data element separator</param>
        /// <param name="releaseIndicator">Release indicator</param>
        /// <param name="componentDataElementSeparator">Component data element separator</param>
        /// <param name="segmentTerminator">Segment terminator</param>
        /// <returns>Concrete regex pattern to parse EDIFACT headers</returns>
        static string CreateParsePattern(
                                        string genericPattern,
                                        string dataElementSeparator,
                                        string releaseIndicator,
                                        string componentDataElementSeparator,
                                        string segmentTerminator
                                        )
        {
            string _escapedDataElementSeparator = EscapeRegexCharacter(dataElementSeparator);
            string _escapedReleaseIndicator = EscapeRegexCharacter(releaseIndicator);
            string _escapedComponentDataElementSeparator = EscapeRegexCharacter(componentDataElementSeparator);

            string _escapeSequence = string.Format(
                "{0}{0}|{0}{1}|{0}{2}|{0}{3}",
                _escapedReleaseIndicator,
                EscapeRegexCharacter(segmentTerminator),
                _escapedComponentDataElementSeparator,
                _escapedDataElementSeparator
                );

            return string.Format(
                                genericPattern,
                                _escapedDataElementSeparator,
                                _escapeSequence,
                                _escapedComponentDataElementSeparator
                                );

        }

        /// <summary>
        /// Escapes regex character if needed
        /// </summary>
        /// <param name="character">Character to be escaped</param>
        /// <returns>Escaped character or character itself depenging on character value</returns>
        static string EscapeRegexCharacter(string character)
        {
            if (character == null || character.Length != 1) return null;

            if (!_regexEscapedCharacters.Contains(character[0]))
                return character;
            else
                return "\\" + character;
        }


        /// <summary>
        /// Converts xml document to a string
        /// </summary>
        /// <param name="document">Document to convert</param>
        /// <returns>String representation of a given xml document</returns>
        static string XmlToString(XmlDocument document)
        {
            StringWriter _sw = new StringWriter();
            XmlTextWriter _xw = new XmlTextWriter(_sw);
            document.WriteTo(_xw);
            return _sw.ToString();
        }

        /// <summary>
        /// Creates message by wrapping original document with new document node using given name and namespace, and prepending extra XmlDocuments to it.
        /// </summary>
        /// <param name="originalDocument">EDI payload representation of an EDI document</param>
        /// <param name="documentName">New root node name</param>
        /// <param name="documentNamespace">New root node namespace</param>
        /// <param name="prependDocuments">Extra document to prepend to new document</param>
        /// <returns>Xml document prepended with extra documents as nodes and wrapped around a new root node</returns>
        public static XmlDocument CreateEDIMessage(XmlDocument originalDocument, string documentName, string documentNamespace, params XmlDocument[] prependDocuments)
        {
            StringWriter _sw = null;
            XmlTextWriter _xw = null;

            StringBuilder _newXML = new StringBuilder();
            XmlWriter _writer = XmlWriter.Create(_newXML);
            _writer.WriteStartElement(_EDIDocumentStartPrefix, documentName, documentNamespace);

            //Write prepend documents
            foreach (XmlDocument doc in prependDocuments)
            {
                _sw = new StringWriter();
                _xw = new XmlTextWriter(_sw);

                doc.WriteTo(_xw);
                _writer.WriteRaw(_sw.ToString());
            }

            _writer.WriteStartElement(_EDIDocumentTransactionSetNode);
            //Write original document
            _sw = new StringWriter();
            _xw = new XmlTextWriter(_sw);

            originalDocument.WriteTo(_xw);
            _writer.WriteRaw(_sw.ToString());
            _writer.WriteEndElement();
            _writer.WriteEndElement();
            _writer.Flush();

            XmlDocument _newDocument = new XmlDocument();
            _newDocument.LoadXml(_newXML.ToString());
            return _newDocument;
        }
    }
}
