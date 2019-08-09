//---------------------------------------------------------------------
// File: MessageEnrichmentORchestration.odx.cs
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




#pragma warning disable 162

namespace Microsoft.Samples.BizTalk.MessageEnrichment
{

    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "MessageReceiveOperation",
        new System.Type[]{
            typeof(Microsoft.Samples.BizTalk.MessageEnrichment.__messagetype_System_Xml_XmlDocument)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeAttribute(Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal, "")]
    [System.SerializableAttribute]
    sealed internal class ReceiveMessageType : Microsoft.BizTalk.XLANGs.BTXEngine.BTXPortBase
    {
        public ReceiveMessageType(int portInfo, Microsoft.XLANGs.Core.IServiceProxy s)
            : base(portInfo, s)
        { }
        public ReceiveMessageType(ReceiveMessageType p)
            : base(p)
        { }

        public override Microsoft.XLANGs.Core.PortBase Clone()
        {
            ReceiveMessageType p = new ReceiveMessageType(this);
            return p;
        }

        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        #region port reflection support
        static public Microsoft.XLANGs.Core.OperationInfo MessageReceiveOperation = new Microsoft.XLANGs.Core.OperationInfo
        (
            "MessageReceiveOperation",
            System.Web.Services.Description.OperationFlow.OneWay,
            typeof(ReceiveMessageType),
            typeof(__messagetype_System_Xml_XmlDocument),
            null,
            new System.Type[]{},
            new string[]{}
        );
        static public System.Collections.Hashtable OperationsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[ "MessageReceiveOperation" ] = MessageReceiveOperation;
                return h;
            }
        }
        #endregion // port reflection support
    }

    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "MessageSendOperation",
        new System.Type[]{
            typeof(Microsoft.Samples.BizTalk.MessageEnrichment.__messagetype_System_Xml_XmlDocument)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeAttribute(Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal, "")]
    [System.SerializableAttribute]
    sealed internal class SendMessagePortType : Microsoft.BizTalk.XLANGs.BTXEngine.BTXPortBase
    {
        public SendMessagePortType(int portInfo, Microsoft.XLANGs.Core.IServiceProxy s)
            : base(portInfo, s)
        { }
        public SendMessagePortType(SendMessagePortType p)
            : base(p)
        { }

        public override Microsoft.XLANGs.Core.PortBase Clone()
        {
            SendMessagePortType p = new SendMessagePortType(this);
            return p;
        }

        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        #region port reflection support
        static public Microsoft.XLANGs.Core.OperationInfo MessageSendOperation = new Microsoft.XLANGs.Core.OperationInfo
        (
            "MessageSendOperation",
            System.Web.Services.Description.OperationFlow.OneWay,
            typeof(SendMessagePortType),
            typeof(__messagetype_System_Xml_XmlDocument),
            null,
            new System.Type[]{},
            new string[]{}
        );
        static public System.Collections.Hashtable OperationsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[ "MessageSendOperation" ] = MessageSendOperation;
                return h;
            }
        }
        #endregion // port reflection support
    }
    [Microsoft.XLANGs.BaseTypes.CorrelationTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal,
        new string[] {
            "BTS.ReceivePortName"
        }
    )]
    sealed internal class ReceivePortNameCorrelationType : Microsoft.XLANGs.Core.CorrelationType
    {
        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        private static Microsoft.XLANGs.BaseTypes.PropertyBase[] _properties = new Microsoft.XLANGs.BaseTypes.PropertyBase[] {
            new BTS.ReceivePortName()
         };
        public override Microsoft.XLANGs.BaseTypes.PropertyBase[] Properties { get { return _properties; } }
        public static Microsoft.XLANGs.BaseTypes.PropertyBase[] PropertiesList { get { return _properties; } }
    }
    //#line 279 "C:\Program Files\Microsoft BizTalk Server 2006\SDK\Samples\EDI\MessageEnrichment\MessageEnrichment\MessageEnrichmentOrchestration.odx"
    [Microsoft.XLANGs.BaseTypes.StaticSubscriptionAttribute(
        0, "ReceiveMessagePort", "MessageReceiveOperation", 0, -1, true
    )]
    [Microsoft.XLANGs.BaseTypes.ActivationPredicateAttribute(
        0,
        new bool[] {
            true
        },
        new System.Type[] {
            typeof(BTS.ReceivePortName)
        },
        new Microsoft.XLANGs.BaseTypes.EXLangPredicateOperator[] {
            Microsoft.XLANGs.BaseTypes.EXLangPredicateOperator.eOpEqual
        },
        new System.Object[] {
            "ReceiveEDIPort"
        }
    )]
    [Microsoft.XLANGs.BaseTypes.ServicePortsAttribute(
        new Microsoft.XLANGs.BaseTypes.EXLangSParameter[] {
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements,
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        },
        new System.Type[] {
            typeof(Microsoft.Samples.BizTalk.MessageEnrichment.ReceiveMessageType),
            typeof(Microsoft.Samples.BizTalk.MessageEnrichment.SendMessagePortType)
        },
        new System.String[] {
            "ReceiveMessagePort",
            "SendMessagePort"
        },
        new System.Type[] {
            null,
            null
        }
    )]
    [Microsoft.XLANGs.BaseTypes.ServiceCallTreeAttribute(
        new System.Type[] {
        },
        new System.Type[] {
        },
        new System.Type[] {
        }
    )]
    [Microsoft.XLANGs.BaseTypes.ServiceAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal,
        Microsoft.XLANGs.BaseTypes.EXLangSServiceInfo.eNone
    )]
    [System.SerializableAttribute]
    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(false)]
    sealed internal class MessageEnrichmentOrchestration : Microsoft.BizTalk.XLANGs.BTXEngine.BTXService
    {
        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        public static readonly bool __execable = false;
        [Microsoft.XLANGs.BaseTypes.CallCompensationAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSCallCompensationInfo.eNone,
            new System.String[] {
            },
            new System.String[] {
            }
        )]
        public static void __bodyProxy()
        {
        }
        private static System.Guid _serviceId = Microsoft.XLANGs.Core.HashHelper.HashServiceType(typeof(MessageEnrichmentOrchestration));
        private static volatile System.Guid[] _activationSubIds;

        private static new object _lockIdentity = new object();

        public static System.Guid UUID { get { return _serviceId; } }
        public override System.Guid ServiceId { get { return UUID; } }

        protected override System.Guid[] ActivationSubGuids
        {
            get { return _activationSubIds; }
            set { _activationSubIds = value; }
        }

        protected override object StaleStateLock
        {
            get { return _lockIdentity; }
        }

        protected override bool HasActivation { get { return true; } }

        internal bool IsExeced = false;

        static MessageEnrichmentOrchestration()
        {
            Microsoft.BizTalk.XLANGs.BTXEngine.BTXService.CacheStaticState( _serviceId );
        }

        private void ConstructorHelper()
        {
            _segments = new Microsoft.XLANGs.Core.Segment[] {
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment0), 0, 0, 0),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment1), 1, 1, 1)
            };

            _Locks = 0;
            _rootContext = new __MessageEnrichmentOrchestration_root_0(this);
            _stateMgrs = new Microsoft.XLANGs.Core.IStateManager[2];
            _stateMgrs[0] = _rootContext;
            FinalConstruct();
        }

        public MessageEnrichmentOrchestration(System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXSession session, Microsoft.BizTalk.XLANGs.BTXEngine.BTXEvents tracker)
            : base(instanceId, session, "MessageEnrichmentOrchestration", tracker)
        {
            ConstructorHelper();
        }

        public MessageEnrichmentOrchestration(int callIndex, System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXService parent)
            : base(callIndex, instanceId, parent, "MessageEnrichmentOrchestration")
        {
            ConstructorHelper();
        }

        private const string _symInfo = @"
<XsymFile>
<ProcessFlow xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>      <shapeType>RootShape</shapeType>      <ShapeID>954fd763-22e0-4694-add0-92cdee9b7772</ShapeID>      
<children>                          
<ShapeInfo>      <shapeType>ReceiveShape</shapeType>      <ShapeID>8139f4d5-0789-4562-9506-ed3b9e633030</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>ReceiveEDIDocument</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>DNFPredicateShape</shapeType>      <ShapeID>2befa467-5723-425d-8cd2-96e2bf4130ed</ShapeID>      <ParentLink>Receive_DNFPredicate</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>DecisionShape</shapeType>      <ShapeID>7a38e9d5-9995-44cf-8a5d-fb9b76f2d883</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>DetermineMessageType</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>DecisionBranchShape</shapeType>      <ShapeID>47926e05-6eea-4b55-8421-d3a87ea5ff4e</ShapeID>      <ParentLink>ReallyComplexStatement_Branch</ParentLink>                <shapeText>IsMessageEDIFACT</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>VariableAssignmentShape</shapeType>      <ShapeID>6773c142-860c-4cae-8202-381558c69c9e</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>UNA and UNG segments detection</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>006ac18c-173f-4764-ae90-81439d8d4315</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ConstructEnrichedEDIFACTMessage</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessageAssignmentShape</shapeType>      <ShapeID>aa634ab4-80df-457f-83cd-19c0ddbbc5cb</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>EnrichedMessageAssignment</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>f760e4e3-36d7-4954-a26c-a8b2593b618c</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>DecisionBranchShape</shapeType>      <ShapeID>2f9627e9-889f-4d6c-8938-af87536ea8d7</ShapeID>      <ParentLink>ReallyComplexStatement_Branch</ParentLink>                <shapeText>IsMessageX12</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>410cd074-f3b9-4d4a-8773-0951701d051c</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ConstructEnrichedX12Message</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>8d62a33e-d45a-4f0f-be73-3300f0716ff7</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageAssignmentShape</shapeType>      <ShapeID>2f34bcba-2af6-4024-a3b6-668d4405eaa7</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>EnrichedMessageAssignment</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>DecisionBranchShape</shapeType>      <ShapeID>8b71aafa-bc0c-425f-a261-75ac81c57343</ShapeID>      <ParentLink>ReallyComplexStatement_Branch</ParentLink>                <shapeText>Else</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>TerminateShape</shapeType>      <ShapeID>8fd7d0ff-a18f-4b1a-aff4-777dc6b61d91</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>TerminateOrchestration</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>SendShape</shapeType>      <ShapeID>6e2b77b6-c84c-4a0e-a9ed-35f67a272888</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>SendEmrichedMessage</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ProcessFlow>
<Metadata>

<TrkMetadata>
<ActionName>'MessageEnrichmentOrchestration'</ActionName><IsAtomic>0</IsAtomic><Line>279</Line><Position>14</Position><ShapeID>'e211a116-cb8b-44e7-a052-0de295aa0001'</ShapeID>
</TrkMetadata>

<TrkMetadata>
<Line>293</Line><Position>65</Position><ShapeID>'8139f4d5-0789-4562-9506-ed3b9e633030'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>297</Line><Position>13</Position><ShapeID>'7a38e9d5-9995-44cf-8a5d-fb9b76f2d883'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>300</Line><Position>17</Position><ShapeID>'6773c142-860c-4cae-8202-381558c69c9e'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>310</Line><Position>17</Position><ShapeID>'006ac18c-173f-4764-ae90-81439d8d4315'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>335</Line><Position>17</Position><ShapeID>'410cd074-f3b9-4d4a-8773-0951701d051c'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>356</Line><Position>17</Position><ShapeID>'8fd7d0ff-a18f-4b1a-aff4-777dc6b61d91'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>359</Line><Position>13</Position><ShapeID>'6e2b77b6-c84c-4a0e-a9ed-35f67a272888'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>
</Metadata>
</XsymFile>";

        public override string odXml { get { return _symODXML; } }

        private const string _symODXML = @"
<?xml version='1.0' encoding='utf-8' standalone='yes'?>
<om:MetaModel MajorVersion='1' MinorVersion='3' Core='2b131234-7959-458d-834f-2dc0769ce683' ScheduleModel='66366196-361d-448d-976f-cab5e87496d2' xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>
    <om:Element Type='Module' OID='8cb6538f-1e7d-4798-be6e-5a37fc97f4d7' LowerBound='1.1' HigherBound='107.1'>
        <om:Property Name='ReportToAnalyst' Value='True' />
        <om:Property Name='Name' Value='Microsoft.Samples.BizTalk.MessageEnrichment' />
        <om:Property Name='Signal' Value='False' />
        <om:Element Type='CorrelationType' OID='c2c175fc-87d5-47fa-9d47-3aacd6921d0c' ParentLink='Module_CorrelationType' LowerBound='18.1' HigherBound='22.1'>
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='ReceivePortNameCorrelationType' />
            <om:Property Name='Signal' Value='True' />
            <om:Element Type='PropertyRef' OID='77b230fc-346c-4564-83f9-21ffc4e43d82' ParentLink='CorrelationType_PropertyRef' LowerBound='20.9' HigherBound='20.28'>
                <om:Property Name='Ref' Value='BTS.ReceivePortName' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='PropertyRef_1' />
                <om:Property Name='Signal' Value='False' />
            </om:Element>
        </om:Element>
        <om:Element Type='ServiceDeclaration' OID='ceae2f28-0c35-4a8d-b55c-40ebfdb6604f' ParentLink='Module_ServiceDeclaration' LowerBound='22.1' HigherBound='106.1'>
            <om:Property Name='InitializedTransactionType' Value='False' />
            <om:Property Name='IsInvokable' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='MessageEnrichmentOrchestration' />
            <om:Property Name='Signal' Value='True' />
            <om:Element Type='VariableDeclaration' OID='b0a4479c-0200-49eb-8000-6911a00e8878' ParentLink='ServiceDeclaration_VariableDeclaration' LowerBound='32.1' HigherBound='33.1'>
                <om:Property Name='UseDefaultConstructor' Value='False' />
                <om:Property Name='Type' Value='System.String' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='UNASegment' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='VariableDeclaration' OID='9026e3f8-e9c2-4f99-b7a8-ec127d9f6232' ParentLink='ServiceDeclaration_VariableDeclaration' LowerBound='33.1' HigherBound='34.1'>
                <om:Property Name='UseDefaultConstructor' Value='False' />
                <om:Property Name='Type' Value='System.String' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='UNGSegment' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='CorrelationDeclaration' OID='0875e73a-5869-48eb-a8d4-3c542f88ff4c' ParentLink='ServiceDeclaration_CorrelationDeclaration' LowerBound='29.1' HigherBound='30.1'>
                <om:Property Name='Type' Value='Microsoft.Samples.BizTalk.MessageEnrichment.ReceivePortNameCorrelationType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='ReceivePortNameCorrelation' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='StatementRef' OID='4032ceae-659b-4172-8da7-96b88895650c' ParentLink='CorrelationDeclaration_StatementRef' LowerBound='103.74' HigherBound='103.111'>
                    <om:Property Name='Initializes' Value='True' />
                    <om:Property Name='Ref' Value='6e2b77b6-c84c-4a0e-a9ed-35f67a272888' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='StatementRef_1' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='503a01ad-fe63-4f28-9d9d-54b607705b5d' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='30.1' HigherBound='31.1'>
                <om:Property Name='Type' Value='System.Xml.XmlDocument' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='EnrichedMessage' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='8552c79b-b695-44ab-994b-e521a292ea6f' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='31.1' HigherBound='32.1'>
                <om:Property Name='Type' Value='System.Xml.XmlDocument' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='EDIMessage' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='ServiceBody' OID='954fd763-22e0-4694-add0-92cdee9b7772' ParentLink='ServiceDeclaration_ServiceBody'>
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='Receive' OID='8139f4d5-0789-4562-9506-ed3b9e633030' ParentLink='ServiceBody_Statement' LowerBound='36.1' HigherBound='40.1'>
                    <om:Property Name='Activate' Value='True' />
                    <om:Property Name='PortName' Value='ReceiveMessagePort' />
                    <om:Property Name='MessageName' Value='EDIMessage' />
                    <om:Property Name='OperationName' Value='MessageReceiveOperation' />
                    <om:Property Name='OperationMessageName' Value='Request' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='AnalystComments' Value='This receive is used to receive an EDIFACT or X12 message for it&apos;s further enrichment.' />
                    <om:Property Name='Name' Value='ReceiveEDIDocument' />
                    <om:Property Name='Signal' Value='True' />
                    <om:Element Type='DNFPredicate' OID='2befa467-5723-425d-8cd2-96e2bf4130ed' ParentLink='Receive_DNFPredicate'>
                        <om:Property Name='LHS' Value='BTS.ReceivePortName' />
                        <om:Property Name='RHS' Value='&quot;ReceiveEDIPort&quot;' />
                        <om:Property Name='Grouping' Value='AND' />
                        <om:Property Name='Operator' Value='Equals' />
                        <om:Property Name='Signal' Value='False' />
                    </om:Element>
                </om:Element>
                <om:Element Type='Decision' OID='7a38e9d5-9995-44cf-8a5d-fb9b76f2d883' ParentLink='ServiceBody_Statement' LowerBound='40.1' HigherBound='102.1'>
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='DetermineMessageType' />
                    <om:Property Name='Signal' Value='True' />
                    <om:Element Type='DecisionBranch' OID='47926e05-6eea-4b55-8421-d3a87ea5ff4e' ParentLink='ReallyComplexStatement_Branch' LowerBound='41.13' HigherBound='76.1'>
                        <om:Property Name='Expression' Value='MessageEnrichmentLibrary.OrchestrationUtilities.IsMessageEDIFACT(EDIMessage(BTS.MessageType)) == true' />
                        <om:Property Name='IsGhostBranch' Value='True' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='AnalystComments' Value='Process EDIFACT message' />
                        <om:Property Name='Name' Value='IsMessageEDIFACT' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='VariableAssignment' OID='6773c142-860c-4cae-8202-381558c69c9e' ParentLink='ComplexStatement_Statement' LowerBound='43.1' HigherBound='53.1'>
                            <om:Property Name='Expression' Value='if (MessageEnrichmentLibrary.OrchestrationUtilities.IsHeaderExist(EDIMessage, typeof(EDI.UNA_Segment)))&#xD;&#xA;    {UNASegment = EDIMessage(EDI.UNA_Segment);}&#xD;&#xA;else&#xD;&#xA;    {UNASegment = null;}&#xD;&#xA;&#xD;&#xA;if (MessageEnrichmentLibrary.OrchestrationUtilities.IsHeaderExist(EDIMessage, typeof(EDI.UNG_Segment)))&#xD;&#xA;    {UNGSegment = EDIMessage(EDI.UNG_Segment);}&#xD;&#xA;else&#xD;&#xA;    {UNGSegment = null;}' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='AnalystComments' Value='Determine if UNA and UNG headers exist and assign variables appropriately' />
                            <om:Property Name='Name' Value='UNA and UNG segments detection' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                        <om:Element Type='Construct' OID='006ac18c-173f-4764-ae90-81439d8d4315' ParentLink='ComplexStatement_Statement' LowerBound='53.1' HigherBound='75.1'>
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='AnalystComments' Value='Create enriched EDIFACT message' />
                            <om:Property Name='Name' Value='ConstructEnrichedEDIFACTMessage' />
                            <om:Property Name='Signal' Value='True' />
                            <om:Element Type='MessageAssignment' OID='aa634ab4-80df-457f-83cd-19c0ddbbc5cb' ParentLink='ComplexStatement_Statement' LowerBound='56.1' HigherBound='74.1'>
                                <om:Property Name='Expression' Value='EnrichedMessage = EDIMessage;&#xD;&#xA;&#xD;&#xA;EnrichedMessage = &#xD;&#xA;MessageEnrichmentLibrary.EDIHeaders.CreateEDIMessage(&#xD;&#xA;EDIMessage,&#xD;&#xA;&quot;EDIFACTEnrichedMessage&quot;,&#xD;&#xA;&quot;http://schemas.microsoft.com/BizTalk/EDI/EDIFACT/2006/EnrichedMessageXML&quot;,&#xD;&#xA;MessageEnrichmentLibrary.EDIHeaders.SerializeEDIFACTHeaders(&#xD;&#xA;                                UNASegment, &#xD;&#xA;                                EDIMessage(EDI.UNB_Segment), &#xD;&#xA;                                UNGSegment)&#xD;&#xA;);&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;// A set of properties which will be promoted is assigned here. &#xD;&#xA;// This properties should also be in appropriate correlation type&#xD;&#xA;EnrichedMessage(BTS.ReceivePortName) = &quot;EnrichmentOrchestration&quot;;' />
                                <om:Property Name='ReportToAnalyst' Value='False' />
                                <om:Property Name='Name' Value='EnrichedMessageAssignment' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                            <om:Element Type='MessageRef' OID='f760e4e3-36d7-4954-a26c-a8b2593b618c' ParentLink='Construct_MessageRef' LowerBound='54.27' HigherBound='54.42'>
                                <om:Property Name='Ref' Value='EnrichedMessage' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                        </om:Element>
                    </om:Element>
                    <om:Element Type='DecisionBranch' OID='2f9627e9-889f-4d6c-8938-af87536ea8d7' ParentLink='ReallyComplexStatement_Branch' LowerBound='76.18' HigherBound='97.1'>
                        <om:Property Name='Expression' Value='MessageEnrichmentLibrary.OrchestrationUtilities.IsMessageX12(EDIMessage(BTS.MessageType)) == true' />
                        <om:Property Name='IsGhostBranch' Value='True' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='AnalystComments' Value='Process X12 message' />
                        <om:Property Name='Name' Value='IsMessageX12' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='Construct' OID='410cd074-f3b9-4d4a-8773-0951701d051c' ParentLink='ComplexStatement_Statement' LowerBound='78.1' HigherBound='96.1'>
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='AnalystComments' Value='Create enriched X12 message' />
                            <om:Property Name='Name' Value='ConstructEnrichedX12Message' />
                            <om:Property Name='Signal' Value='True' />
                            <om:Element Type='MessageRef' OID='8d62a33e-d45a-4f0f-be73-3300f0716ff7' ParentLink='Construct_MessageRef' LowerBound='79.27' HigherBound='79.42'>
                                <om:Property Name='Ref' Value='EnrichedMessage' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                            <om:Element Type='MessageAssignment' OID='2f34bcba-2af6-4024-a3b6-668d4405eaa7' ParentLink='ComplexStatement_Statement' LowerBound='81.1' HigherBound='95.1'>
                                <om:Property Name='Expression' Value='EnrichedMessage = EDIMessage;&#xD;&#xA;&#xD;&#xA;EnrichedMessage = &#xD;&#xA;MessageEnrichmentLibrary.EDIHeaders.CreateEDIMessage(&#xD;&#xA;EDIMessage,&#xD;&#xA;&quot;X12EnrichedMessage&quot;,&#xD;&#xA;&quot;http://schemas.microsoft.com/BizTalk/EDI/EDIFACT/2006/EnrichedMessageXML&quot;,&#xD;&#xA;MessageEnrichmentLibrary.EDIHeaders.SerializeX12Headers(EDIMessage(EDI.ISA_Segment), EDIMessage(EDI.GS_Segment))&#xD;&#xA;);&#xD;&#xA;&#xD;&#xA;// A set of properties which will be promoted is assigned here. &#xD;&#xA;// This properties should also be in appropriate correlation type&#xD;&#xA;EnrichedMessage(BTS.ReceivePortName) = &quot;EnrichmentOrchestration&quot;;' />
                                <om:Property Name='ReportToAnalyst' Value='False' />
                                <om:Property Name='Name' Value='EnrichedMessageAssignment' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                        </om:Element>
                    </om:Element>
                    <om:Element Type='DecisionBranch' OID='8b71aafa-bc0c-425f-a261-75ac81c57343' ParentLink='ReallyComplexStatement_Branch'>
                        <om:Property Name='IsGhostBranch' Value='True' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='AnalystComments' Value='Process non-EDI message' />
                        <om:Property Name='Name' Value='Else' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='Terminate' OID='8fd7d0ff-a18f-4b1a-aff4-777dc6b61d91' ParentLink='ComplexStatement_Statement' LowerBound='99.1' HigherBound='101.1'>
                            <om:Property Name='ErrorMessage' Value='&quot;Message type is not recognized&quot;;' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='AnalystComments' Value='Finish processing' />
                            <om:Property Name='Name' Value='TerminateOrchestration' />
                            <om:Property Name='Signal' Value='True' />
                        </om:Element>
                    </om:Element>
                </om:Element>
                <om:Element Type='Send' OID='6e2b77b6-c84c-4a0e-a9ed-35f67a272888' ParentLink='ServiceBody_Statement' LowerBound='102.1' HigherBound='104.1'>
                    <om:Property Name='PortName' Value='SendMessagePort' />
                    <om:Property Name='MessageName' Value='EnrichedMessage' />
                    <om:Property Name='OperationName' Value='MessageSendOperation' />
                    <om:Property Name='OperationMessageName' Value='Request' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='AnalystComments' Value='This send is used to send enriched EDIFACT or X12 message into message box.' />
                    <om:Property Name='Name' Value='SendEmrichedMessage' />
                    <om:Property Name='Signal' Value='True' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='8bd2d87c-2a58-404a-b5b3-7ff76d4e1d52' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='25.1' HigherBound='27.1'>
                <om:Property Name='PortModifier' Value='Implements' />
                <om:Property Name='Orientation' Value='Right' />
                <om:Property Name='PortIndex' Value='-1' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='Microsoft.Samples.BizTalk.MessageEnrichment.ReceiveMessageType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='AnalystComments' Value='This port is used to receive an EDIFACT or X12 message for it&apos;s further enrichment.' />
                <om:Property Name='Name' Value='ReceiveMessagePort' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='DirectBindingAttribute' OID='ec4ac7a5-088b-455c-b900-2198e03892da' ParentLink='PortDeclaration_CLRAttribute' LowerBound='25.1' HigherBound='26.1'>
                    <om:Property Name='DirectBindingType' Value='MessageBox' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='d6bdca72-c383-4c61-b46c-074709584730' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='27.1' HigherBound='29.1'>
                <om:Property Name='PortModifier' Value='Uses' />
                <om:Property Name='Orientation' Value='Right' />
                <om:Property Name='PortIndex' Value='26' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='Microsoft.Samples.BizTalk.MessageEnrichment.SendMessagePortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='AnalystComments' Value='This port is used to send enriched EDIFACT or X12 message into message box.' />
                <om:Property Name='Name' Value='SendMessagePort' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='DirectBindingAttribute' OID='b89a463d-9ca5-418d-9914-3433fadd7f36' ParentLink='PortDeclaration_CLRAttribute' LowerBound='27.1' HigherBound='28.1'>
                    <om:Property Name='DirectBindingType' Value='MessageBox' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
        <om:Element Type='PortType' OID='9f094c26-ed2f-454f-b486-202faa55de6f' ParentLink='Module_PortType' LowerBound='4.1' HigherBound='11.1'>
            <om:Property Name='Synchronous' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='ReceiveMessageType' />
            <om:Property Name='Signal' Value='False' />
            <om:Element Type='OperationDeclaration' OID='e40996e7-523d-4089-af51-1a4b1a0ce2e8' ParentLink='PortType_OperationDeclaration' LowerBound='6.1' HigherBound='10.1'>
                <om:Property Name='OperationType' Value='OneWay' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='MessageReceiveOperation' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='MessageRef' OID='85f800ab-ab90-49bf-b069-764031bea96f' ParentLink='OperationDeclaration_RequestMessageRef' LowerBound='8.13' HigherBound='8.35'>
                    <om:Property Name='Ref' Value='System.Xml.XmlDocument' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Request' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
        <om:Element Type='PortType' OID='b9214835-5e2f-4e0c-8805-9bf7ac3593fc' ParentLink='Module_PortType' LowerBound='11.1' HigherBound='18.1'>
            <om:Property Name='Synchronous' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='SendMessagePortType' />
            <om:Property Name='Signal' Value='False' />
            <om:Element Type='OperationDeclaration' OID='b45f04f3-f31a-4ac7-acc9-6acd695225fe' ParentLink='PortType_OperationDeclaration' LowerBound='13.1' HigherBound='17.1'>
                <om:Property Name='OperationType' Value='OneWay' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='MessageSendOperation' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='MessageRef' OID='be3b647e-7c22-48cd-82e1-9fc19631d36c' ParentLink='OperationDeclaration_RequestMessageRef' LowerBound='15.13' HigherBound='15.35'>
                    <om:Property Name='Ref' Value='System.Xml.XmlDocument' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Request' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
    </om:Element>
</om:MetaModel>
";

        [System.SerializableAttribute]
        public class __MessageEnrichmentOrchestration_root_0 : Microsoft.XLANGs.Core.ServiceContext
        {
            public __MessageEnrichmentOrchestration_root_0(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "MessageEnrichmentOrchestration")
            {
            }

            public override int Index { get { return 0; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[0]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[0]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Finally();
                return false;
            }

            public override void Finally()
            {
                MessageEnrichmentOrchestration __svc__ = (MessageEnrichmentOrchestration)_service;
                __MessageEnrichmentOrchestration_root_0 __ctx0__ = (__MessageEnrichmentOrchestration_root_0)(__svc__._stateMgrs[0]);

                if (__svc__.SendMessagePort != null)
                {
                    __svc__.SendMessagePort.Close(this, null);
                    __svc__.SendMessagePort = null;
                }
                if (__svc__.ReceiveMessagePort != null)
                {
                    __svc__.ReceiveMessagePort.Close(this, null);
                    __svc__.ReceiveMessagePort = null;
                }
                base.Finally();
            }

            internal Microsoft.XLANGs.Core.SubscriptionWrapper __subWrapper0;
        }


        [System.SerializableAttribute]
        public class __MessageEnrichmentOrchestration_1 : Microsoft.XLANGs.Core.ExceptionHandlingContext
        {
            public __MessageEnrichmentOrchestration_1(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "MessageEnrichmentOrchestration")
            {
            }

            public override int Index { get { return 1; } }

            public override bool CombineParentCommit { get { return true; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[1]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[1]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Finally();
                return false;
            }

            public override void Finally()
            {
                MessageEnrichmentOrchestration __svc__ = (MessageEnrichmentOrchestration)_service;
                __MessageEnrichmentOrchestration_1 __ctx1__ = (__MessageEnrichmentOrchestration_1)(__svc__._stateMgrs[1]);

                if (__ctx1__ != null && __ctx1__.__ReceivePortNameCorrelation != null)
                    __ctx1__.__ReceivePortNameCorrelation = null;
                if (__ctx1__ != null && __ctx1__.__EDIMessage != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__EDIMessage);
                    __ctx1__.__EDIMessage = null;
                }
                if (__ctx1__ != null && __ctx1__.__EnrichedMessage != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__EnrichedMessage);
                    __ctx1__.__EnrichedMessage = null;
                }
                if (__ctx1__ != null)
                    __ctx1__.__UNASegment = null;
                if (__ctx1__ != null)
                    __ctx1__.__UNGSegment = null;
                base.Finally();
            }

            [Microsoft.XLANGs.Core.UserVariableAttribute("EnrichedMessage")]
            public __messagetype_System_Xml_XmlDocument __EnrichedMessage;
            [Microsoft.XLANGs.Core.UserVariableAttribute("EDIMessage")]
            public __messagetype_System_Xml_XmlDocument __EDIMessage;
            [Microsoft.XLANGs.Core.UserVariableAttribute("ReceivePortNameCorrelation")]
            internal Microsoft.XLANGs.Core.Correlation __ReceivePortNameCorrelation;
            [Microsoft.XLANGs.Core.UserVariableAttribute("UNASegment")]
            internal System.String __UNASegment;
            [Microsoft.XLANGs.Core.UserVariableAttribute("UNGSegment")]
            internal System.String __UNGSegment;
        }

        private static Microsoft.XLANGs.Core.CorrelationType[] _correlationTypes = new Microsoft.XLANGs.Core.CorrelationType[] { new ReceivePortNameCorrelationType() };
        public override Microsoft.XLANGs.Core.CorrelationType[] CorrelationTypes { get { return _correlationTypes; } }

        private static System.Guid[] _convoySetIds;

        public override System.Guid[] ConvoySetGuids
        {
            get { return _convoySetIds; }
            set { _convoySetIds = value; }
        }

        public static object[] StaticConvoySetInformation
        {
            get {
                return null;
            }
        }

        [Microsoft.XLANGs.BaseTypes.DirectBindingAttribute()]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("ReceiveMessagePort")]
        internal ReceiveMessageType ReceiveMessagePort;
        [Microsoft.XLANGs.BaseTypes.DirectBindingAttribute()]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("SendMessagePort")]
        internal SendMessagePortType SendMessagePort;
        private static BTS.ReceivePortName _prop_BTS_ReceivePortName = new BTS.ReceivePortName();

        sealed private class PredicateSet0_0 : Microsoft.XLANGs.Core.PredicateGroup
        {
            public PredicateSet0_0() : base(1)
            {
                Add(new Microsoft.XLANGs.Core.FullySpecifiedPredicate(_prop_BTS_ReceivePortName, Microsoft.XLANGs.Core.PredicateBase.Operators.eEqual, "ReceiveEDIPort"));
            }
        }


        private static Microsoft.XLANGs.Core.PredicateGroup[] _predicates0 = {
            new PredicateSet0_0()
        };


        public static Microsoft.XLANGs.Core.PortInfo[] _portInfo = new Microsoft.XLANGs.Core.PortInfo[] {
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {ReceiveMessageType.MessageReceiveOperation},
                                               typeof(MessageEnrichmentOrchestration).GetField("ReceiveMessagePort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.implements,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(MessageEnrichmentOrchestration), "ReceiveMessagePort"),
                                               null),
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {SendMessagePortType.MessageSendOperation},
                                               typeof(MessageEnrichmentOrchestration).GetField("SendMessagePort", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.uses,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(MessageEnrichmentOrchestration), "SendMessagePort"),
                                               null)
        };

        public override Microsoft.XLANGs.Core.PortInfo[] PortInformation
        {
            get { return _portInfo; }
        }

        static public System.Collections.Hashtable PortsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[_portInfo[0].Name] = _portInfo[0];
                h[_portInfo[1].Name] = _portInfo[1];
                return h;
            }
        }

        public static System.Type[] InvokedServicesTypes
        {
            get
            {
                return new System.Type[] {
                    // type of each service invoked by this service
                };
            }
        }

        public static System.Type[] CalledServicesTypes
        {
            get
            {
                return new System.Type[] {
                };
            }
        }

        public static System.Type[] ExecedServicesTypes
        {
            get
            {
                return new System.Type[] {
                };
            }
        }

        public static object[] StaticSubscriptionsInformation {
            get {
                return new object[1]{
                     new object[5] { _portInfo[0], 0, _predicates0 , -1, true }
                };
            }
        }

        public static Microsoft.XLANGs.RuntimeTypes.Location[] __eventLocations = new Microsoft.XLANGs.RuntimeTypes.Location[] {
            new Microsoft.XLANGs.RuntimeTypes.Location(0, "00000000-0000-0000-0000-000000000000", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(1, "8139f4d5-0789-4562-9506-ed3b9e633030", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(2, "8139f4d5-0789-4562-9506-ed3b9e633030", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(3, "00000000-0000-0000-0000-000000000000", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(4, "7a38e9d5-9995-44cf-8a5d-fb9b76f2d883", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(5, "6773c142-860c-4cae-8202-381558c69c9e", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(6, "6773c142-860c-4cae-8202-381558c69c9e", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(7, "006ac18c-173f-4764-ae90-81439d8d4315", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(8, "006ac18c-173f-4764-ae90-81439d8d4315", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(9, "410cd074-f3b9-4d4a-8773-0951701d051c", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(10, "410cd074-f3b9-4d4a-8773-0951701d051c", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(11, "8fd7d0ff-a18f-4b1a-aff4-777dc6b61d91", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(12, "7a38e9d5-9995-44cf-8a5d-fb9b76f2d883", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(13, "6e2b77b6-c84c-4a0e-a9ed-35f67a272888", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(14, "6e2b77b6-c84c-4a0e-a9ed-35f67a272888", 1, false)
        };

        public override Microsoft.XLANGs.RuntimeTypes.Location[] EventLocations
        {
            get { return __eventLocations; }
        }

        public static Microsoft.XLANGs.RuntimeTypes.EventData[] __eventData = new Microsoft.XLANGs.RuntimeTypes.EventData[] {
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Body),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Receive),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.If),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.If),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Construct),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Terminate),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Send),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Body)
        };

        public static int[] __progressLocation0 = new int[] { 0,0,0,3,3,};
        public static int[] __progressLocation1 = new int[] { 0,0,1,1,2,2,2,4,4,5,5,5,5,5,6,6,6,6,6,6,6,7,7,8,4,4,4,9,9,10,10,11,11,11,12,13,13,13,14,3,3,3,3,};

        public static int[][] __progressLocations = new int[2] [] {__progressLocation0,__progressLocation1};
        public override int[][] ProgressLocations {get {return __progressLocations;} }

        public Microsoft.XLANGs.Core.StopConditions segment0(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[0];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[0];
            __MessageEnrichmentOrchestration_root_0 __ctx0__ = (__MessageEnrichmentOrchestration_root_0)_stateMgrs[0];
            __MessageEnrichmentOrchestration_1 __ctx1__ = (__MessageEnrichmentOrchestration_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                ReceiveMessagePort = new ReceiveMessageType(0, this);
                SendMessagePort = new SendMessagePortType(1, this);
                __ctx__.PrologueCompleted = true;
                __ctx0__.__subWrapper0 = new Microsoft.XLANGs.Core.SubscriptionWrapper(ActivationSubGuids[0], ReceiveMessagePort, this);
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.Initialized) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.Initialized;
                goto case 1;
            case 1:
                __ctx1__ = new __MessageEnrichmentOrchestration_1(this);
                _stateMgrs[1] = __ctx1__;
                if ( !PostProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 2;
            case 2:
                __ctx0__.StartContext(__seg__, __ctx1__);
                if ( !PostProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                return Microsoft.XLANGs.Core.StopConditions.Blocked;
            case 3:
                if (!__ctx0__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                __ctx1__.Finally();
                ServiceDone(__seg__, (Microsoft.XLANGs.Core.Context)_stateMgrs[0]);
                __ctx0__.OnCommit();
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment1(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Envelope __msgEnv__ = null;
            bool __condition__;
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[1];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[1];
            __MessageEnrichmentOrchestration_root_0 __ctx0__ = (__MessageEnrichmentOrchestration_root_0)_stateMgrs[0];
            __MessageEnrichmentOrchestration_1 __ctx1__ = (__MessageEnrichmentOrchestration_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                __ctx1__.__UNASegment = default(System.String);
                __ctx1__.__UNGSegment = default(System.String);
                __ctx__.PrologueCompleted = true;
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if ( !PreProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[0],__eventData[0],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 2;
            case 2:
                if ( !PreProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[1],__eventData[1],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 3;
            case 3:
                if (!ReceiveMessagePort.GetMessageId(__ctx0__.__subWrapper0.getSubscription(this), __seg__, __ctx1__, out __msgEnv__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if (__ctx1__.__EDIMessage != null)
                    __ctx1__.UnrefMessage(__ctx1__.__EDIMessage);
                __ctx1__.__EDIMessage = new __messagetype_System_Xml_XmlDocument("EDIMessage", __ctx1__);
                __ctx1__.RefMessage(__ctx1__.__EDIMessage);
                ReceiveMessagePort.ReceiveMessage(0, __msgEnv__, __ctx1__.__EDIMessage, null, (Microsoft.XLANGs.Core.Context)_stateMgrs[1], __seg__);
                if (ReceiveMessagePort != null)
                {
                    ReceiveMessagePort.Close(__ctx1__, __seg__);
                    ReceiveMessagePort = null;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Receive);
                    __edata.Messages.Add(__ctx1__.__EDIMessage);
                    __edata.PortName = @"ReceiveMessagePort";
                    Tracker.FireEvent(__eventLocations[2],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                __ctx1__.__UNASegment = "";
                if ( !PostProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 6;
            case 6:
                __ctx1__.__UNGSegment = "";
                if ( !PostProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 7;
            case 7:
                if ( !PreProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[4],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 8;
            case 8:
                __condition__ = MessageEnrichmentLibrary.OrchestrationUtilities.IsMessageEDIFACT((System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(BTS.MessageType)));
                if (!__condition__)
                {
                    if ( !PostProgressInc( __seg__, __ctx__, 25 ) )
                        return Microsoft.XLANGs.Core.StopConditions.Paused;
                    goto case 25;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 9;
            case 9:
                if ( !PreProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[5],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 10;
            case 10:
                __condition__ = MessageEnrichmentLibrary.OrchestrationUtilities.IsHeaderExist(CreateMessageWrapperForUserCode(__ctx1__.__EDIMessage), typeof(EDI.UNA_Segment));
                if (!__condition__)
                {
                    if ( !PostProgressInc( __seg__, __ctx__, 13 ) )
                        return Microsoft.XLANGs.Core.StopConditions.Paused;
                    goto case 13;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 11 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 11;
            case 11:
                __ctx1__.__UNASegment = (System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(EDI.UNA_Segment));
                if ( !PostProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 12;
            case 12:
                if ( !PostProgressInc( __seg__, __ctx__, 14 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 14;
            case 13:
                __ctx1__.__UNASegment = null;
                if ( !PostProgressInc( __seg__, __ctx__, 14 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 14;
            case 14:
                if ( !PreProgressInc( __seg__, __ctx__, 15 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[6],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 15;
            case 15:
                if ( !PreProgressInc( __seg__, __ctx__, 16 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[0],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 16;
            case 16:
                __condition__ = MessageEnrichmentLibrary.OrchestrationUtilities.IsHeaderExist(CreateMessageWrapperForUserCode(__ctx1__.__EDIMessage), typeof(EDI.UNG_Segment));
                if (!__condition__)
                {
                    if ( !PostProgressInc( __seg__, __ctx__, 19 ) )
                        return Microsoft.XLANGs.Core.StopConditions.Paused;
                    goto case 19;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 17 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 17;
            case 17:
                __ctx1__.__UNGSegment = (System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(EDI.UNG_Segment));
                if ( !PostProgressInc( __seg__, __ctx__, 18 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 18;
            case 18:
                if ( !PostProgressInc( __seg__, __ctx__, 20 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 20;
            case 19:
                __ctx1__.__UNGSegment = null;
                if ( !PostProgressInc( __seg__, __ctx__, 20 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 20;
            case 20:
                if ( !PreProgressInc( __seg__, __ctx__, 21 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[3],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 21;
            case 21:
                if ( !PreProgressInc( __seg__, __ctx__, 22 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[7],__eventData[4],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 22;
            case 22:
                {
                    __messagetype_System_Xml_XmlDocument __EnrichedMessage = new __messagetype_System_Xml_XmlDocument("EnrichedMessage", __ctx1__);

                    __EnrichedMessage.CopyFrom(__ctx1__.__EDIMessage);
                    __EnrichedMessage.part.LoadFrom(MessageEnrichmentLibrary.EDIHeaders.CreateEDIMessage(__ctx1__.__EDIMessage.part.TypedValue, "EDIFACTEnrichedMessage", "http://schemas.microsoft.com/BizTalk/EDI/EDIFACT/2006/EnrichedMessageXML", MessageEnrichmentLibrary.EDIHeaders.SerializeEDIFACTHeaders(__ctx1__.__UNASegment, (System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(EDI.UNB_Segment)), __ctx1__.__UNGSegment)));
                    __EnrichedMessage.SetPropertyValue(typeof(BTS.ReceivePortName), "EnrichmentOrchestration");

                    if (__ctx1__.__EnrichedMessage != null)
                        __ctx1__.UnrefMessage(__ctx1__.__EnrichedMessage);
                    __ctx1__.__EnrichedMessage = __EnrichedMessage;
                    __ctx1__.RefMessage(__ctx1__.__EnrichedMessage);
                }
                __ctx1__.__EnrichedMessage.ConstructionCompleteEvent(true);
                if ( !PostProgressInc( __seg__, __ctx__, 23 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 23;
            case 23:
                if ( !PreProgressInc( __seg__, __ctx__, 24 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx1__.__EnrichedMessage);
                    Tracker.FireEvent(__eventLocations[8],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 24;
            case 24:
                if ( !PostProgressInc( __seg__, __ctx__, 34 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 34;
            case 25:
                if ( !PreProgressInc( __seg__, __ctx__, 26 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[0],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 26;
            case 26:
                __condition__ = MessageEnrichmentLibrary.OrchestrationUtilities.IsMessageX12((System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(BTS.MessageType)));
                if (!__condition__)
                {
                    if ( !PostProgressInc( __seg__, __ctx__, 31 ) )
                        return Microsoft.XLANGs.Core.StopConditions.Paused;
                    goto case 31;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 27 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 27;
            case 27:
                if ( !PreProgressInc( __seg__, __ctx__, 28 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[9],__eventData[4],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 28;
            case 28:
                {
                    __messagetype_System_Xml_XmlDocument __EnrichedMessage = new __messagetype_System_Xml_XmlDocument("EnrichedMessage", __ctx1__);

                    __EnrichedMessage.CopyFrom(__ctx1__.__EDIMessage);
                    __EnrichedMessage.part.LoadFrom(MessageEnrichmentLibrary.EDIHeaders.CreateEDIMessage(__ctx1__.__EDIMessage.part.TypedValue, "X12EnrichedMessage", "http://schemas.microsoft.com/BizTalk/EDI/EDIFACT/2006/EnrichedMessageXML", MessageEnrichmentLibrary.EDIHeaders.SerializeX12Headers((System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(EDI.ISA_Segment)), (System.String)__ctx1__.__EDIMessage.GetPropertyValueThrows(typeof(EDI.GS_Segment)))));
                    __EnrichedMessage.SetPropertyValue(typeof(BTS.ReceivePortName), "EnrichmentOrchestration");

                    if (__ctx1__.__EnrichedMessage != null)
                        __ctx1__.UnrefMessage(__ctx1__.__EnrichedMessage);
                    __ctx1__.__EnrichedMessage = __EnrichedMessage;
                    __ctx1__.RefMessage(__ctx1__.__EnrichedMessage);
                }
                __ctx1__.__EnrichedMessage.ConstructionCompleteEvent(true);
                if ( !PostProgressInc( __seg__, __ctx__, 29 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 29;
            case 29:
                if ( !PreProgressInc( __seg__, __ctx__, 30 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx1__.__EnrichedMessage);
                    Tracker.FireEvent(__eventLocations[10],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 30;
            case 30:
                if ( !PostProgressInc( __seg__, __ctx__, 33 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 33;
            case 31:
                if ( !PreProgressInc( __seg__, __ctx__, 32 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[11],__eventData[5],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 32;
            case 32:
                RequestTerminate(__ctx1__,"Message type is not recognized");
                __seg__.SegmentDone();
                break;
            case 33:
                if ( !PreProgressInc( __seg__, __ctx__, 34 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[3],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 34;
            case 34:
                if ( !PreProgressInc( __seg__, __ctx__, 35 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if (__ctx1__ != null)
                    __ctx1__.__UNGSegment = null;
                if (__ctx1__ != null)
                    __ctx1__.__UNASegment = null;
                if (__ctx1__ != null && __ctx1__.__EDIMessage != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__EDIMessage);
                    __ctx1__.__EDIMessage = null;
                }
                Tracker.FireEvent(__eventLocations[12],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 35;
            case 35:
                if ( !PreProgressInc( __seg__, __ctx__, 36 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[13],__eventData[6],_stateMgrs[1].TrackDataStream );
                __ctx1__.__ReceivePortNameCorrelation = new Microsoft.XLANGs.Core.Correlation(this, 0, 0);
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 36;
            case 36:
                if (!__ctx1__.PrepareToPendingCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 37 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 37;
            case 37:
                if ( !PreProgressInc( __seg__, __ctx__, 38 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                SendMessagePort.SendMessage(0, __ctx1__.__EnrichedMessage, new Microsoft.XLANGs.Core.Correlation[] { __ctx1__.__ReceivePortNameCorrelation }, null, __ctx1__, __seg__ , Microsoft.XLANGs.Core.ActivityFlags.NextActivityPersists );
                if (__ctx1__ != null && __ctx1__.__ReceivePortNameCorrelation != null)
                    __ctx1__.__ReceivePortNameCorrelation = null;
                if (SendMessagePort != null)
                {
                    SendMessagePort.Close(__ctx1__, __seg__);
                    SendMessagePort = null;
                }
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.OutgoingRqst) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.OutgoingRqst;
                goto case 38;
            case 38:
                if ( !PreProgressInc( __seg__, __ctx__, 39 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Send);
                    __edata.Messages.Add(__ctx1__.__EnrichedMessage);
                    __edata.PortName = @"SendMessagePort";
                    Tracker.FireEvent(__eventLocations[14],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (__ctx1__ != null && __ctx1__.__EnrichedMessage != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__EnrichedMessage);
                    __ctx1__.__EnrichedMessage = null;
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 39;
            case 39:
                if ( !PreProgressInc( __seg__, __ctx__, 40 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[3],__eventData[7],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 40;
            case 40:
                if (!__ctx1__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 41 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 41;
            case 41:
                if ( !PreProgressInc( __seg__, __ctx__, 42 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx1__.OnCommit();
                goto case 42;
            case 42:
                __seg__.SegmentDone();
                _segments[0].PredecessorDone(this);
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }
    }

    [System.SerializableAttribute]
    sealed public class __Microsoft_XLANGs_BaseTypes_Any__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static Microsoft.XLANGs.BaseTypes.Any _schema = new Microsoft.XLANGs.BaseTypes.Any();

        public __Microsoft_XLANGs_BaseTypes_Any__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eThirdKind,
        "System.Xml.XmlDocument",
        new System.Type[]{
            typeof(Microsoft.XLANGs.BaseTypes.Any)
        },
        new string[]{
            "part"
        },
        new System.Type[]{
            typeof(__Microsoft_XLANGs_BaseTypes_Any__)
        },
        0,
        Microsoft.XLANGs.Core.XMessage.AnyMessageTypeName
    )]
    [System.SerializableAttribute]
    sealed public class __messagetype_System_Xml_XmlDocument : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __Microsoft_XLANGs_BaseTypes_Any__ part;

        private void __CreatePartWrappers()
        {
            part = new __Microsoft_XLANGs_BaseTypes_Any__(this, "part", 0);
            this.AddPart("part", 0, part);
        }

        public __messagetype_System_Xml_XmlDocument(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(false)]
    sealed public class _MODULE_PROXY_ { }
}
