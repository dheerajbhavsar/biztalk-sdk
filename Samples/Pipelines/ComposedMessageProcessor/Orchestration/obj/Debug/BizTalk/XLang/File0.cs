
#pragma warning disable 162

namespace CMP.Orchestration
{

    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "TypeAgnosticMessageOperation",
        new System.Type[]{
            typeof(CMP.Orchestration.__messagetype_System_Xml_XmlDocument)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeAttribute(Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal, "")]
    [System.SerializableAttribute]
    sealed internal class TypeAgnosticMessage_PortType : Microsoft.BizTalk.XLANGs.BTXEngine.BTXPortBase
    {
        public TypeAgnosticMessage_PortType(int portInfo, Microsoft.XLANGs.Core.IServiceProxy s)
            : base(portInfo, s)
        { }
        public TypeAgnosticMessage_PortType(TypeAgnosticMessage_PortType p)
            : base(p)
        { }

        public override Microsoft.XLANGs.Core.PortBase Clone()
        {
            TypeAgnosticMessage_PortType p = new TypeAgnosticMessage_PortType(this);
            return p;
        }

        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        #region port reflection support
        static public Microsoft.XLANGs.Core.OperationInfo TypeAgnosticMessageOperation = new Microsoft.XLANGs.Core.OperationInfo
        (
            "TypeAgnosticMessageOperation",
            System.Web.Services.Description.OperationFlow.OneWay,
            typeof(TypeAgnosticMessage_PortType),
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
                h[ "TypeAgnosticMessageOperation" ] = TypeAgnosticMessageOperation;
                return h;
            }
        }
        #endregion // port reflection support
    }
    //#line 368 "C:\Program Files (x86)\Microsoft BizTalk Server 2016\SDK\Samples\Pipelines\ComposedMessageProcessor\Orchestration\CMP.odx"
    [Microsoft.XLANGs.BaseTypes.StaticSubscriptionAttribute(
        0, "Port_Input", "TypeAgnosticMessageOperation", -1, -1, true
    )]
    [Microsoft.XLANGs.BaseTypes.ServicePortsAttribute(
        new Microsoft.XLANGs.BaseTypes.EXLangSParameter[] {
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements,
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses,
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        },
        new System.Type[] {
            typeof(CMP.Orchestration.TypeAgnosticMessage_PortType),
            typeof(CMP.Orchestration.TypeAgnosticMessage_PortType),
            typeof(CMP.Orchestration.TypeAgnosticMessage_PortType)
        },
        new System.String[] {
            "Port_Input",
            "SendFailedInterchange",
            "Port_Output"
        },
        new System.Type[] {
            null,
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
        Microsoft.XLANGs.BaseTypes.EXLangSServiceInfo.eNone|Microsoft.XLANGs.BaseTypes.EXLangSServiceInfo.eLongRunning
    )]
    [System.SerializableAttribute]
    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(false)]
    sealed internal class CMProcessor : Microsoft.BizTalk.XLANGs.BTXEngine.BTXService
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
        private static System.Guid _serviceId = Microsoft.XLANGs.Core.HashHelper.HashServiceType(typeof(CMProcessor));
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

        static CMProcessor()
        {
            Microsoft.BizTalk.XLANGs.BTXEngine.BTXService.CacheStaticState( _serviceId );
        }

        private void ConstructorHelper()
        {
            _segments = new Microsoft.XLANGs.Core.Segment[] {
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment0), 0, 0, 0),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment1), 1, 1, 1),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment2), 1, 2, 2),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment3), 1, 3, 3),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment4), 1, 3, 4),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment5), 1, 4, 5),
                new Microsoft.XLANGs.Core.Segment( new Microsoft.XLANGs.Core.Segment.SegmentCode(this.segment6), 1, 4, 6)
            };

            _Locks = 3;
            _rootContext = new __CMProcessor_root_0(this);
            _stateMgrs = new Microsoft.XLANGs.Core.IStateManager[5];
            _stateMgrs[0] = _rootContext;
            FinalConstruct();
        }

        public CMProcessor(System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXSession session, Microsoft.BizTalk.XLANGs.BTXEngine.BTXEvents tracker)
            : base(instanceId, session, "CMProcessor", tracker)
        {
            ConstructorHelper();
        }

        public CMProcessor(int callIndex, System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXService parent)
            : base(callIndex, instanceId, parent, "CMProcessor")
        {
            ConstructorHelper();
        }

        private const string _symInfo = @"
<XsymFile>
<ProcessFlow xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>      <shapeType>RootShape</shapeType>      <ShapeID>d7fe053e-e644-479c-86f5-17ddc8f9f00b</ShapeID>      
<children>                          
<ShapeInfo>      <shapeType>ReceiveShape</shapeType>      <ShapeID>77b1ecf5-cb22-4b46-a551-90d0d77abbf5</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>Receive_Interchange</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>ScopeShape</shapeType>      <ShapeID>c8e43366-c855-4ee8-b1df-cd4874658270</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>Scope_MapInterchange</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>VariableDeclarationShape</shapeType>      <ShapeID>15087cfe-8af9-4ea5-ad7b-874343e05695</ShapeID>      <ParentLink>Scope_VariableDeclaration</ParentLink>                <shapeText>RcvPipeOutput</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>AtomicTransactionShape</shapeType>      <ShapeID>9c08a79f-cf99-4f8e-b262-64b792ada31a</ShapeID>      <ParentLink>Scope_Transaction</ParentLink>                <shapeText>Transaction</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>ScopeShape</shapeType>      <ShapeID>ca21b67b-6293-4a51-9dfa-eaec75e5ebda</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>Scope_ExecuteReceivePipeline</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>VariableAssignmentShape</shapeType>      <ShapeID>c1e7dc64-4a82-4eb8-ba61-6a5a34b508ca</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ExecuteRcvPipe</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>CatchShape</shapeType>      <ShapeID>d41e0e51-cac0-4824-af9b-1d52f1ec19ae</ShapeID>      <ParentLink>Scope_Catch</ParentLink>                <shapeText>CatchPipelineException</shapeText>                      <ExceptionType>Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException</ExceptionType>            
<children>                          
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>cf686824-067f-444d-87e9-3cd72cb1e020</ShapeID>      <ParentLink>Catch_Statement</ParentLink>                <shapeText>ConstructFailedInterchangeMessage</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>433b6f99-855e-4b6b-b9ce-4c08afce0369</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageAssignmentShape</shapeType>      <ShapeID>ade5893d-2827-43ef-bfc8-57bd40abf989</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>SetErrorDetails</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>SendShape</shapeType>      <ShapeID>10d9421d-2415-4186-94a5-d841b5a5f416</ShapeID>      <ParentLink>Catch_Statement</ParentLink>                <shapeText>Send_FailedInterchange</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>TerminateShape</shapeType>      <ShapeID>dee8c70a-76cc-4fe0-b564-7fd21da63631</ShapeID>      <ParentLink>Catch_Statement</ParentLink>                <shapeText>TerminateOrchestrationOnRcv</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>WhileShape</shapeType>      <ShapeID>5ce2c101-cdba-4bd5-b0f3-e4ed52466ee6</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>Loop_WhileThereAreMessages</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>ee062bff-d953-44d4-9589-09dc73bf9a9f</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ConstructTmpMessageIn</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessageAssignmentShape</shapeType>      <ShapeID>3bf317d0-e603-4974-a631-35cc540c725c</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>GetTmpMessage</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>93b634dc-2934-4776-99cd-65a9845083ff</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>ea94f894-e19f-4271-9207-ac51ed42ee33</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ConstructTmpMessageOut</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>TransformShape</shapeType>      <ShapeID>721c877e-8831-4667-8dd6-cc2ea2129618</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>Transform_PO2Invoice</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessagePartRefShape</shapeType>      <ShapeID>58c008d4-008d-41e0-bf5e-23344bd2f490</ShapeID>      <ParentLink>Transform_OutputMessagePartRef</ParentLink>                <shapeText>MessagePartReference_2</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessagePartRefShape</shapeType>      <ShapeID>7f73d290-88db-4009-8dcf-0992258f6ca6</ShapeID>      <ParentLink>Transform_InputMessagePartRef</ParentLink>                <shapeText>MessagePartReference_1</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>da3febd0-1be9-404c-869e-a2b85edc496b</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>VariableAssignmentShape</shapeType>      <ShapeID>114b998a-a7af-4b26-9b07-ab051f0657cb</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>AddMessageToBatch</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>TransactionAttributeShape</shapeType>      <ShapeID>977dda33-9b44-412a-a38e-470b32c8ebad</ShapeID>      <ParentLink>Statement_CLRAttribute</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageDeclarationShape</shapeType>      <ShapeID>c853f88d-8141-40b2-830b-f899a6135daa</ShapeID>      <ParentLink>Scope_MessageDeclaration</ParentLink>                <shapeText>TmpMessageOut</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageDeclarationShape</shapeType>      <ShapeID>66b45428-5eaa-4512-a59e-ae6018713d92</ShapeID>      <ParentLink>Scope_MessageDeclaration</ParentLink>                <shapeText>TmpMessageIn</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>ScopeShape</shapeType>      <ShapeID>e06c3f5d-056c-4e44-a0a9-bed9bd1b61d8</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>Scope_ExecuteSendPipeline</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>876bd879-5cfc-4f21-9e39-e7b0399f9afb</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ConstructOutputInterchange</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessageAssignmentShape</shapeType>      <ShapeID>1dd68d91-079c-4952-81e9-641388b9fc9c</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>ExecuteSendPipeline</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>2af41a7f-05fb-4fc7-88ff-d51ddc19970a</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>SendShape</shapeType>      <ShapeID>7c88b1c6-d356-47ec-93c7-8a7465fb6440</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>Send_OutputInterchange</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>CatchShape</shapeType>      <ShapeID>8dfe9e2a-813c-4200-ac51-e150774b2504</ShapeID>      <ParentLink>Scope_Catch</ParentLink>                <shapeText>CatchPipelineException</shapeText>                      <ExceptionType>Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException</ExceptionType>            
<children>                          
<ShapeInfo>      <shapeType>ConstructShape</shapeType>      <ShapeID>09f5dae4-30ef-4aea-a2e4-34915db42995</ShapeID>      <ParentLink>Catch_Statement</ParentLink>                <shapeText>ConstructFailedInterchangeMessage</shapeText>                  
<children>                          
<ShapeInfo>      <shapeType>MessageRefShape</shapeType>      <ShapeID>859c7989-b56f-4de1-b477-20347ccb995f</ShapeID>      <ParentLink>Construct_MessageRef</ParentLink>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>MessageAssignmentShape</shapeType>      <ShapeID>263ba1f0-58eb-43e3-9862-da2b0928bce3</ShapeID>      <ParentLink>ComplexStatement_Statement</ParentLink>                <shapeText>SetErrorDetails</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>SendShape</shapeType>      <ShapeID>0479a2f4-4783-4216-96b7-3c6234b0a47e</ShapeID>      <ParentLink>Catch_Statement</ParentLink>                <shapeText>Send_FailedMessage</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>TerminateShape</shapeType>      <ShapeID>677d9ef2-80d4-442a-8d17-4240a7043697</ShapeID>      <ParentLink>Catch_Statement</ParentLink>                <shapeText>Terminate_Orchestration</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                  </children>
  </ShapeInfo>
                  </children>
  </ProcessFlow><Metadata>

<TrkMetadata>
<ActionName>'CMProcessor'</ActionName><IsAtomic>0</IsAtomic><Line>368</Line><Position>14</Position><ShapeID>'e211a116-cb8b-44e7-a052-0de295aa0001'</ShapeID>
</TrkMetadata>

<TrkMetadata>
<Line>383</Line><Position>22</Position><ShapeID>'77b1ecf5-cb22-4b46-a551-90d0d77abbf5'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<ActionName>'Transaction'</ActionName><IsAtomic>1</IsAtomic><Line>387</Line><Position>13</Position><ShapeID>'c8e43366-c855-4ee8-b1df-cd4874658270'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<ActionName>'??__scope33'</ActionName><IsAtomic>0</IsAtomic><Line>395</Line><Position>21</Position><ShapeID>'ca21b67b-6293-4a51-9dfa-eaec75e5ebda'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>400</Line><Position>43</Position><ShapeID>'c1e7dc64-4a82-4eb8-ba61-6a5a34b508ca'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>405</Line><Position>29</Position><ShapeID>'d41e0e51-cac0-4824-af9b-1d52f1ec19ae'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>408</Line><Position>33</Position><ShapeID>'cf686824-067f-444d-87e9-3cd72cb1e020'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>415</Line><Position>33</Position><ShapeID>'10d9421d-2415-4186-94a5-d841b5a5f416'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>417</Line><Position>33</Position><ShapeID>'dee8c70a-76cc-4fe0-b564-7fd21da63631'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>422</Line><Position>21</Position><ShapeID>'5ce2c101-cdba-4bd5-b0f3-e4ed52466ee6'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>425</Line><Position>25</Position><ShapeID>'ee062bff-d953-44d4-9589-09dc73bf9a9f'</ShapeID>
<Messages>
	<MsgInfo><name>TmpMessageIn</name><part>part</part><schema>CMP.PipelinesAndSchemas.PO</schema><direction>Out</direction></MsgInfo>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>432</Line><Position>25</Position><ShapeID>'ea94f894-e19f-4271-9207-ac51ed42ee33'</ShapeID>
<Messages>
	<MsgInfo><name>TmpMessageOut</name><part>part</part><schema>CMP.PipelinesAndSchemas.Invoice</schema><direction>Out</direction></MsgInfo>
	<MsgInfo><name>TmpMessageIn</name><part>part</part><schema>CMP.PipelinesAndSchemas.PO</schema><direction>In</direction></MsgInfo>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>438</Line><Position>42</Position><ShapeID>'114b998a-a7af-4b26-9b07-ab051f0657cb'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<ActionName>'??__scope35'</ActionName><IsAtomic>0</IsAtomic><Line>443</Line><Position>13</Position><ShapeID>'e06c3f5d-056c-4e44-a0a9-bed9bd1b61d8'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>448</Line><Position>21</Position><ShapeID>'876bd879-5cfc-4f21-9e39-e7b0399f9afb'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>455</Line><Position>21</Position><ShapeID>'7c88b1c6-d356-47ec-93c7-8a7465fb6440'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>460</Line><Position>21</Position><ShapeID>'8dfe9e2a-813c-4200-ac51-e150774b2504'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>463</Line><Position>25</Position><ShapeID>'09f5dae4-30ef-4aea-a2e4-34915db42995'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>470</Line><Position>25</Position><ShapeID>'0479a2f4-4783-4216-96b7-3c6234b0a47e'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>472</Line><Position>25</Position><ShapeID>'677d9ef2-80d4-442a-8d17-4240a7043697'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>
</Metadata>
</XsymFile>";

        public override string odXml { get { return _symODXML; } }

        private const string _symODXML = @"
<?xml version='1.0' encoding='utf-8' standalone='yes'?>
<om:MetaModel MajorVersion='1' MinorVersion='3' Core='2b131234-7959-458d-834f-2dc0769ce683' ScheduleModel='66366196-361d-448d-976f-cab5e87496d2' xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>
    <om:Element Type='Module' OID='e8ec94bc-d8c6-4d77-9bd6-97a96ca24e0b' LowerBound='1.1' HigherBound='123.1'>
        <om:Property Name='ReportToAnalyst' Value='True' />
        <om:Property Name='Name' Value='CMP.Orchestration' />
        <om:Property Name='Signal' Value='False' />
        <om:Element Type='PortType' OID='4f0ffac8-d8c8-4904-a1c0-ff653b96554b' ParentLink='Module_PortType' LowerBound='4.1' HigherBound='11.1'>
            <om:Property Name='Synchronous' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='TypeAgnosticMessage_PortType' />
            <om:Property Name='Signal' Value='True' />
            <om:Element Type='OperationDeclaration' OID='edaf0cd3-ae41-4fe1-80ce-06ca716e2cdd' ParentLink='PortType_OperationDeclaration' LowerBound='6.1' HigherBound='10.1'>
                <om:Property Name='OperationType' Value='OneWay' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='TypeAgnosticMessageOperation' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='MessageRef' OID='98c1fe7d-ccd1-4c94-8a38-dafa92376f62' ParentLink='OperationDeclaration_RequestMessageRef' LowerBound='8.13' HigherBound='8.35'>
                    <om:Property Name='Ref' Value='System.Xml.XmlDocument' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Request' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
        <om:Element Type='ServiceDeclaration' OID='4df1857a-90c3-4717-afb2-7615c78d17d9' ParentLink='Module_ServiceDeclaration' LowerBound='11.1' HigherBound='122.1'>
            <om:Property Name='InitializedTransactionType' Value='True' />
            <om:Property Name='IsInvokable' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='CMProcessor' />
            <om:Property Name='Signal' Value='True' />
            <om:Element Type='VariableDeclaration' OID='e092ea04-ca0c-4e17-9006-6ce5a1896871' ParentLink='ServiceDeclaration_VariableDeclaration' LowerBound='23.1' HigherBound='24.1'>
                <om:Property Name='UseDefaultConstructor' Value='True' />
                <om:Property Name='Type' Value='Microsoft.XLANGs.Pipeline.SendPipelineInputMessages' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='SendPipeInput' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='LongRunningTransaction' OID='3e997f6f-bace-433d-bb29-303862f57b96' ParentLink='ServiceDeclaration_Transaction' LowerBound='12.21' HigherBound='12.57'>
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='Transaction_1' />
                <om:Property Name='Signal' Value='False' />
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='0410f52f-fa52-4b99-a5aa-eea076574a44' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='20.1' HigherBound='21.1'>
                <om:Property Name='Type' Value='System.Xml.XmlDocument' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='InputInterchange' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='edef04d7-2da2-4fe7-970e-26ab08e4d2d5' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='21.1' HigherBound='22.1'>
                <om:Property Name='Type' Value='System.Xml.XmlDocument' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='OutputInterchange' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='a7425240-aee6-405a-8fd8-b5f30bbf9457' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='22.1' HigherBound='23.1'>
                <om:Property Name='Type' Value='System.Xml.XmlDocument' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='FailedInterchange' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='ServiceBody' OID='d7fe053e-e644-479c-86f5-17ddc8f9f00b' ParentLink='ServiceDeclaration_ServiceBody'>
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='Receive' OID='77b1ecf5-cb22-4b46-a551-90d0d77abbf5' ParentLink='ServiceBody_Statement' LowerBound='26.1' HigherBound='29.1'>
                    <om:Property Name='Activate' Value='True' />
                    <om:Property Name='PortName' Value='Port_Input' />
                    <om:Property Name='MessageName' Value='InputInterchange' />
                    <om:Property Name='OperationName' Value='TypeAgnosticMessageOperation' />
                    <om:Property Name='OperationMessageName' Value='Request' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Receive_Interchange' />
                    <om:Property Name='Signal' Value='True' />
                </om:Element>
                <om:Element Type='Scope' OID='c8e43366-c855-4ee8-b1df-cd4874658270' ParentLink='ServiceBody_Statement' LowerBound='29.1' HigherBound='86.1'>
                    <om:Property Name='InitializedTransactionType' Value='True' />
                    <om:Property Name='IsSynchronized' Value='False' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Scope_MapInterchange' />
                    <om:Property Name='Signal' Value='True' />
                    <om:Element Type='VariableDeclaration' OID='15087cfe-8af9-4ea5-ad7b-874343e05695' ParentLink='Scope_VariableDeclaration' LowerBound='35.1' HigherBound='36.1'>
                        <om:Property Name='UseDefaultConstructor' Value='False' />
                        <om:Property Name='Type' Value='Microsoft.XLANGs.Pipeline.ReceivePipelineOutputMessages' />
                        <om:Property Name='ParamDirection' Value='In' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='RcvPipeOutput' />
                        <om:Property Name='Signal' Value='True' />
                    </om:Element>
                    <om:Element Type='AtomicTransaction' OID='9c08a79f-cf99-4f8e-b262-64b792ada31a' ParentLink='Scope_Transaction' LowerBound='31.18' HigherBound='31.49'>
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='Transaction' />
                        <om:Property Name='Signal' Value='False' />
                    </om:Element>
                    <om:Element Type='Scope' OID='ca21b67b-6293-4a51-9dfa-eaec75e5ebda' ParentLink='ComplexStatement_Statement' LowerBound='38.1' HigherBound='65.1'>
                        <om:Property Name='InitializedTransactionType' Value='True' />
                        <om:Property Name='IsSynchronized' Value='False' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='Scope_ExecuteReceivePipeline' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='VariableAssignment' OID='c1e7dc64-4a82-4eb8-ba61-6a5a34b508ca' ParentLink='ComplexStatement_Statement' LowerBound='43.1' HigherBound='45.1'>
                            <om:Property Name='Expression' Value='RcvPipeOutput = Microsoft.XLANGs.Pipeline.XLANGPipelineManager.ExecuteReceivePipeline(typeof(CMP.PipelinesAndSchemas.FFReceivePipeline), InputInterchange);' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='ExecuteRcvPipe' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                        <om:Element Type='Catch' OID='d41e0e51-cac0-4824-af9b-1d52f1ec19ae' ParentLink='Scope_Catch' LowerBound='48.1' HigherBound='63.1'>
                            <om:Property Name='ExceptionName' Value='pEx' />
                            <om:Property Name='ExceptionType' Value='Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException' />
                            <om:Property Name='IsFaultMessage' Value='False' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='CatchPipelineException' />
                            <om:Property Name='Signal' Value='True' />
                            <om:Element Type='Construct' OID='cf686824-067f-444d-87e9-3cd72cb1e020' ParentLink='Catch_Statement' LowerBound='51.1' HigherBound='58.1'>
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Name' Value='ConstructFailedInterchangeMessage' />
                                <om:Property Name='Signal' Value='True' />
                                <om:Element Type='MessageRef' OID='433b6f99-855e-4b6b-b9ce-4c08afce0369' ParentLink='Construct_MessageRef' LowerBound='52.43' HigherBound='52.60'>
                                    <om:Property Name='Ref' Value='FailedInterchange' />
                                    <om:Property Name='ReportToAnalyst' Value='True' />
                                    <om:Property Name='Signal' Value='False' />
                                </om:Element>
                                <om:Element Type='MessageAssignment' OID='ade5893d-2827-43ef-bfc8-57bd40abf989' ParentLink='ComplexStatement_Statement' LowerBound='54.1' HigherBound='57.1'>
                                    <om:Property Name='Expression' Value='FailedInterchange = InputInterchange;&#xD;&#xA;FailedInterchange(CMP.PipelinesAndSchemas.ErrorDescription) = pEx.Message;&#xD;&#xA;' />
                                    <om:Property Name='ReportToAnalyst' Value='False' />
                                    <om:Property Name='Name' Value='SetErrorDetails' />
                                    <om:Property Name='Signal' Value='True' />
                                </om:Element>
                            </om:Element>
                            <om:Element Type='Send' OID='10d9421d-2415-4186-94a5-d841b5a5f416' ParentLink='Catch_Statement' LowerBound='58.1' HigherBound='60.1'>
                                <om:Property Name='PortName' Value='SendFailedInterchange' />
                                <om:Property Name='MessageName' Value='FailedInterchange' />
                                <om:Property Name='OperationName' Value='TypeAgnosticMessageOperation' />
                                <om:Property Name='OperationMessageName' Value='Request' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Name' Value='Send_FailedInterchange' />
                                <om:Property Name='Signal' Value='True' />
                            </om:Element>
                            <om:Element Type='Terminate' OID='dee8c70a-76cc-4fe0-b564-7fd21da63631' ParentLink='Catch_Statement' LowerBound='60.1' HigherBound='62.1'>
                                <om:Property Name='ErrorMessage' Value='&quot;Receive pipeline execution failed with the following error:&quot; + pEx.Message;' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Name' Value='TerminateOrchestrationOnRcv' />
                                <om:Property Name='Signal' Value='True' />
                            </om:Element>
                        </om:Element>
                    </om:Element>
                    <om:Element Type='While' OID='5ce2c101-cdba-4bd5-b0f3-e4ed52466ee6' ParentLink='ComplexStatement_Statement' LowerBound='65.1' HigherBound='84.1'>
                        <om:Property Name='Expression' Value='RcvPipeOutput.MoveNext()' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='Loop_WhileThereAreMessages' />
                        <om:Property Name='Signal' Value='False' />
                        <om:Element Type='Construct' OID='ee062bff-d953-44d4-9589-09dc73bf9a9f' ParentLink='ComplexStatement_Statement' LowerBound='68.1' HigherBound='75.1'>
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='ConstructTmpMessageIn' />
                            <om:Property Name='Signal' Value='True' />
                            <om:Element Type='MessageAssignment' OID='3bf317d0-e603-4974-a631-35cc540c725c' ParentLink='ComplexStatement_Statement' LowerBound='71.1' HigherBound='74.1'>
                                <om:Property Name='Expression' Value='TmpMessageIn = null;&#xD;&#xA;RcvPipeOutput.GetCurrent(TmpMessageIn);' />
                                <om:Property Name='ReportToAnalyst' Value='False' />
                                <om:Property Name='Name' Value='GetTmpMessage' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                            <om:Element Type='MessageRef' OID='93b634dc-2934-4776-99cd-65a9845083ff' ParentLink='Construct_MessageRef' LowerBound='69.35' HigherBound='69.47'>
                                <om:Property Name='Ref' Value='TmpMessageIn' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                        </om:Element>
                        <om:Element Type='Construct' OID='ea94f894-e19f-4271-9207-ac51ed42ee33' ParentLink='ComplexStatement_Statement' LowerBound='75.1' HigherBound='81.1'>
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='ConstructTmpMessageOut' />
                            <om:Property Name='Signal' Value='True' />
                            <om:Element Type='Transform' OID='721c877e-8831-4667-8dd6-cc2ea2129618' ParentLink='ComplexStatement_Statement' LowerBound='78.1' HigherBound='80.1'>
                                <om:Property Name='ClassName' Value='CMP.PipelinesAndSchemas.PO2Invoice' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Name' Value='Transform_PO2Invoice' />
                                <om:Property Name='Signal' Value='True' />
                                <om:Element Type='MessagePartRef' OID='58c008d4-008d-41e0-bf5e-23344bd2f490' ParentLink='Transform_OutputMessagePartRef' LowerBound='79.40' HigherBound='79.53'>
                                    <om:Property Name='MessageRef' Value='TmpMessageOut' />
                                    <om:Property Name='ReportToAnalyst' Value='True' />
                                    <om:Property Name='Name' Value='MessagePartReference_2' />
                                    <om:Property Name='Signal' Value='False' />
                                </om:Element>
                                <om:Element Type='MessagePartRef' OID='7f73d290-88db-4009-8dcf-0992258f6ca6' ParentLink='Transform_InputMessagePartRef' LowerBound='79.93' HigherBound='79.105'>
                                    <om:Property Name='MessageRef' Value='TmpMessageIn' />
                                    <om:Property Name='ReportToAnalyst' Value='True' />
                                    <om:Property Name='Name' Value='MessagePartReference_1' />
                                    <om:Property Name='Signal' Value='False' />
                                </om:Element>
                            </om:Element>
                            <om:Element Type='MessageRef' OID='da3febd0-1be9-404c-869e-a2b85edc496b' ParentLink='Construct_MessageRef' LowerBound='76.35' HigherBound='76.48'>
                                <om:Property Name='Ref' Value='TmpMessageOut' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                        </om:Element>
                        <om:Element Type='VariableAssignment' OID='114b998a-a7af-4b26-9b07-ab051f0657cb' ParentLink='ComplexStatement_Statement' LowerBound='81.1' HigherBound='83.1'>
                            <om:Property Name='Expression' Value='SendPipeInput.Add(TmpMessageOut);' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='AddMessageToBatch' />
                            <om:Property Name='Signal' Value='True' />
                        </om:Element>
                    </om:Element>
                    <om:Element Type='TransactionAttribute' OID='977dda33-9b44-412a-a38e-470b32c8ebad' ParentLink='Statement_CLRAttribute' LowerBound='30.1' HigherBound='31.1'>
                        <om:Property Name='Batch' Value='True' />
                        <om:Property Name='Retry' Value='False' />
                        <om:Property Name='Timeout' Value='60' />
                        <om:Property Name='Isolation' Value='Serializable' />
                        <om:Property Name='Signal' Value='False' />
                    </om:Element>
                    <om:Element Type='MessageDeclaration' OID='c853f88d-8141-40b2-830b-f899a6135daa' ParentLink='Scope_MessageDeclaration' LowerBound='33.1' HigherBound='34.1'>
                        <om:Property Name='Type' Value='CMP.PipelinesAndSchemas.Invoice' />
                        <om:Property Name='ParamDirection' Value='In' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='TmpMessageOut' />
                        <om:Property Name='Signal' Value='True' />
                    </om:Element>
                    <om:Element Type='MessageDeclaration' OID='66b45428-5eaa-4512-a59e-ae6018713d92' ParentLink='Scope_MessageDeclaration' LowerBound='34.1' HigherBound='35.1'>
                        <om:Property Name='Type' Value='CMP.PipelinesAndSchemas.PO' />
                        <om:Property Name='ParamDirection' Value='In' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='TmpMessageIn' />
                        <om:Property Name='Signal' Value='True' />
                    </om:Element>
                </om:Element>
                <om:Element Type='Scope' OID='e06c3f5d-056c-4e44-a0a9-bed9bd1b61d8' ParentLink='ServiceBody_Statement' LowerBound='86.1' HigherBound='120.1'>
                    <om:Property Name='InitializedTransactionType' Value='True' />
                    <om:Property Name='IsSynchronized' Value='False' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='Scope_ExecuteSendPipeline' />
                    <om:Property Name='Signal' Value='False' />
                    <om:Element Type='Construct' OID='876bd879-5cfc-4f21-9e39-e7b0399f9afb' ParentLink='ComplexStatement_Statement' LowerBound='91.1' HigherBound='98.1'>
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='ConstructOutputInterchange' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='MessageAssignment' OID='1dd68d91-079c-4952-81e9-641388b9fc9c' ParentLink='ComplexStatement_Statement' LowerBound='94.1' HigherBound='97.1'>
                            <om:Property Name='Expression' Value='OutputInterchange = null;&#xD;&#xA;Microsoft.XLANGs.Pipeline.XLANGPipelineManager.ExecuteSendPipeline(typeof(CMP.PipelinesAndSchemas.FFSendPipeline), SendPipeInput, OutputInterchange);' />
                            <om:Property Name='ReportToAnalyst' Value='False' />
                            <om:Property Name='Name' Value='ExecuteSendPipeline' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                        <om:Element Type='MessageRef' OID='2af41a7f-05fb-4fc7-88ff-d51ddc19970a' ParentLink='Construct_MessageRef' LowerBound='92.31' HigherBound='92.48'>
                            <om:Property Name='Ref' Value='OutputInterchange' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                    </om:Element>
                    <om:Element Type='Send' OID='7c88b1c6-d356-47ec-93c7-8a7465fb6440' ParentLink='ComplexStatement_Statement' LowerBound='98.1' HigherBound='100.1'>
                        <om:Property Name='PortName' Value='Port_Output' />
                        <om:Property Name='MessageName' Value='OutputInterchange' />
                        <om:Property Name='OperationName' Value='TypeAgnosticMessageOperation' />
                        <om:Property Name='OperationMessageName' Value='Request' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='Send_OutputInterchange' />
                        <om:Property Name='Signal' Value='True' />
                    </om:Element>
                    <om:Element Type='Catch' OID='8dfe9e2a-813c-4200-ac51-e150774b2504' ParentLink='Scope_Catch' LowerBound='103.1' HigherBound='118.1'>
                        <om:Property Name='ExceptionName' Value='pEx' />
                        <om:Property Name='ExceptionType' Value='Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException' />
                        <om:Property Name='IsFaultMessage' Value='False' />
                        <om:Property Name='ReportToAnalyst' Value='True' />
                        <om:Property Name='Name' Value='CatchPipelineException' />
                        <om:Property Name='Signal' Value='True' />
                        <om:Element Type='Construct' OID='09f5dae4-30ef-4aea-a2e4-34915db42995' ParentLink='Catch_Statement' LowerBound='106.1' HigherBound='113.1'>
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='ConstructFailedInterchangeMessage' />
                            <om:Property Name='Signal' Value='True' />
                            <om:Element Type='MessageRef' OID='859c7989-b56f-4de1-b477-20347ccb995f' ParentLink='Construct_MessageRef' LowerBound='107.35' HigherBound='107.52'>
                                <om:Property Name='Ref' Value='FailedInterchange' />
                                <om:Property Name='ReportToAnalyst' Value='True' />
                                <om:Property Name='Signal' Value='False' />
                            </om:Element>
                            <om:Element Type='MessageAssignment' OID='263ba1f0-58eb-43e3-9862-da2b0928bce3' ParentLink='ComplexStatement_Statement' LowerBound='109.1' HigherBound='112.1'>
                                <om:Property Name='Expression' Value='FailedInterchange = InputInterchange;&#xD;&#xA;FailedInterchange(CMP.PipelinesAndSchemas.ErrorDescription) = pEx.Message;&#xD;&#xA;' />
                                <om:Property Name='ReportToAnalyst' Value='False' />
                                <om:Property Name='Name' Value='SetErrorDetails' />
                                <om:Property Name='Signal' Value='True' />
                            </om:Element>
                        </om:Element>
                        <om:Element Type='Send' OID='0479a2f4-4783-4216-96b7-3c6234b0a47e' ParentLink='Catch_Statement' LowerBound='113.1' HigherBound='115.1'>
                            <om:Property Name='PortName' Value='SendFailedInterchange' />
                            <om:Property Name='MessageName' Value='FailedInterchange' />
                            <om:Property Name='OperationName' Value='TypeAgnosticMessageOperation' />
                            <om:Property Name='OperationMessageName' Value='Request' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='Send_FailedMessage' />
                            <om:Property Name='Signal' Value='True' />
                        </om:Element>
                        <om:Element Type='Terminate' OID='677d9ef2-80d4-442a-8d17-4240a7043697' ParentLink='Catch_Statement' LowerBound='115.1' HigherBound='117.1'>
                            <om:Property Name='ErrorMessage' Value='&quot;Send pipeline execution failed with the following error:&quot; + pEx.Message;' />
                            <om:Property Name='ReportToAnalyst' Value='True' />
                            <om:Property Name='Name' Value='Terminate_Orchestration' />
                            <om:Property Name='Signal' Value='False' />
                        </om:Element>
                    </om:Element>
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='3d4848b4-107f-4049-a425-eb6d8c824ad2' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='14.1' HigherBound='16.1'>
                <om:Property Name='PortModifier' Value='Implements' />
                <om:Property Name='Orientation' Value='Left' />
                <om:Property Name='PortIndex' Value='2' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='CMP.Orchestration.TypeAgnosticMessage_PortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='Port_Input' />
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='LogicalBindingAttribute' OID='cc353eb2-b9e8-4755-82cc-82b80b6ce559' ParentLink='PortDeclaration_CLRAttribute' LowerBound='14.1' HigherBound='15.1'>
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='6aa33d74-dad0-4ee2-bebd-dd12e344c1f8' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='16.1' HigherBound='18.1'>
                <om:Property Name='PortModifier' Value='Uses' />
                <om:Property Name='Orientation' Value='Left' />
                <om:Property Name='PortIndex' Value='89' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='CMP.Orchestration.TypeAgnosticMessage_PortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='Port_Output' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='LogicalBindingAttribute' OID='2c3c6e61-2e41-4c2c-99cc-29102a757b45' ParentLink='PortDeclaration_CLRAttribute' LowerBound='16.1' HigherBound='17.1'>
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='30bc8e47-3ff5-4ac3-b6b4-2a20b7dd0ff2' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='18.1' HigherBound='20.1'>
                <om:Property Name='PortModifier' Value='Uses' />
                <om:Property Name='Orientation' Value='Right' />
                <om:Property Name='PortIndex' Value='60' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='CMP.Orchestration.TypeAgnosticMessage_PortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='SendFailedInterchange' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='DirectBindingAttribute' OID='ebdd22a9-3406-450e-9742-9a301e473568' ParentLink='PortDeclaration_CLRAttribute' LowerBound='18.1' HigherBound='19.1'>
                    <om:Property Name='PartnerPort' Value='ReceiveFailedMessage' />
                    <om:Property Name='PartnerService' Value='CMP.Orchestration.SuspendMessage' />
                    <om:Property Name='DirectBindingType' Value='PartnerPort' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
    </om:Element>
</om:MetaModel>
";

        [System.SerializableAttribute]
        public class __CMProcessor_root_0 : Microsoft.XLANGs.Core.ServiceContext
        {
            public __CMProcessor_root_0(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "CMProcessor")
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
                CMProcessor __svc__ = (CMProcessor)_service;
                __CMProcessor_root_0 __ctx0__ = (__CMProcessor_root_0)(__svc__._stateMgrs[0]);

                if (__svc__.Port_Input != null)
                {
                    __svc__.Port_Input.Close(this, null);
                    __svc__.Port_Input = null;
                }
                if (__svc__.SendFailedInterchange != null)
                {
                    __svc__.SendFailedInterchange.Close(this, null);
                    __svc__.SendFailedInterchange = null;
                }
                if (__svc__.Port_Output != null)
                {
                    __svc__.Port_Output.Close(this, null);
                    __svc__.Port_Output = null;
                }
                base.Finally();
            }

            internal Microsoft.XLANGs.Core.SubscriptionWrapper __subWrapper0;
        }


        [System.SerializableAttribute]
        public class __CMProcessor_1 : Microsoft.XLANGs.Core.LongRunningTransaction
        {
            public __CMProcessor_1(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "CMProcessor")
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
                CMProcessor __svc__ = (CMProcessor)_service;
                __Transaction_2 __ctx2__ = (__Transaction_2)(__svc__._stateMgrs[2]);
                __CMProcessor_1 __ctx1__ = (__CMProcessor_1)(__svc__._stateMgrs[1]);

                if (__ctx1__ != null && __ctx1__.__InputInterchange != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__InputInterchange);
                    __ctx1__.__InputInterchange = null;
                }
                if (__ctx1__ != null && __ctx1__.__FailedInterchange != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__FailedInterchange);
                    __ctx1__.__FailedInterchange = null;
                }
                if (__ctx1__ != null)
                    __ctx1__.__SendPipeInput = null;
                if (__ctx2__ != null && __ctx2__.__FailedInterchange != null)
                {
                    __ctx2__.UnrefMessage(__ctx2__.__FailedInterchange);
                    __ctx2__.__FailedInterchange = null;
                }
                if (__ctx2__ != null)
                    __ctx2__.__SendPipeInput = null;
                base.Finally();
            }

            [Microsoft.XLANGs.Core.UserVariableAttribute("InputInterchange")]
            public __messagetype_System_Xml_XmlDocument __InputInterchange;  // lock index = 0
            [Microsoft.XLANGs.Core.UserVariableAttribute("OutputInterchange")]
            public __messagetype_System_Xml_XmlDocument __OutputInterchange;
            [Microsoft.XLANGs.Core.UserVariableAttribute("FailedInterchange")]
            public __messagetype_System_Xml_XmlDocument __FailedInterchange;  // lock index = 1
            [Microsoft.XLANGs.Core.UserVariableAttribute("SendPipeInput")]
            internal Microsoft.XLANGs.Pipeline.SendPipelineInputMessages __SendPipeInput;  // lock index = 2
        }


        [System.SerializableAttribute]
        public class __Transaction_2 : Microsoft.XLANGs.Core.AtomicTransaction
        {
            public __Transaction_2(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "Transaction")
            {
                Retry = false;
                Batch = true;
                Timeout = 60;
                TranIsolationLevel = System.Data.IsolationLevel.Serializable;
            }

            public override int Index { get { return 2; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[2]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[2]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Finally();
                return false;
            }

            public override void Finally()
            {
                CMProcessor __svc__ = (CMProcessor)_service;
                __Transaction_2 __ctx2__ = (__Transaction_2)(__svc__._stateMgrs[2]);
                __CMProcessor_1 __ctx1__ = (__CMProcessor_1)(__svc__._stateMgrs[1]);

                if (this.Succeeded)
                {
                    if (__ctx1__.__FailedInterchange != null)
                        __ctx1__.UnrefMessage(__ctx1__.__FailedInterchange);
                    __ctx1__.__FailedInterchange = __ctx2__.__FailedInterchange;
                    if (__ctx2__.__FailedInterchange != null)
                        __ctx1__.RefMessage(__ctx1__.__FailedInterchange);
                    __ctx1__.__SendPipeInput = __ctx2__.__SendPipeInput;
                }
                else if (_PrologueCompleted)
                {
                    __ctx1__.__SendPipeInput = (Microsoft.XLANGs.Pipeline.SendPipelineInputMessages)__ctx2__.RestoreObject(__ctx2__.__SendPipeInput, 0);
                }
                if (__ctx2__ != null)
                    __ctx2__.__RcvPipeOutput = null;
                if (__ctx2__ != null && __ctx2__.__TmpMessageOut != null)
                {
                    __ctx2__.UnrefMessage(__ctx2__.__TmpMessageOut);
                    __ctx2__.__TmpMessageOut = null;
                }
                if (__ctx2__ != null && __ctx2__.__TmpMessageIn != null)
                {
                    __ctx2__.UnrefMessage(__ctx2__.__TmpMessageIn);
                    __ctx2__.__TmpMessageIn = null;
                }
                base.Finally();
            }

            [Microsoft.XLANGs.Core.UserVariableAttribute("FailedInterchange")]
            public __messagetype_System_Xml_XmlDocument __FailedInterchange;
            [Microsoft.XLANGs.Core.UserVariableAttribute("TmpMessageOut")]
            public __messagetype_CMP_PipelinesAndSchemas_Invoice __TmpMessageOut;
            [Microsoft.XLANGs.Core.UserVariableAttribute("TmpMessageIn")]
            public __messagetype_CMP_PipelinesAndSchemas_PO __TmpMessageIn;
            [Microsoft.XLANGs.Core.UserVariableAttribute("SendPipeInput")]
            internal Microsoft.XLANGs.Pipeline.SendPipelineInputMessages __SendPipeInput;
            [System.NonSerializedAttribute]
            [Microsoft.XLANGs.Core.UserVariableAttribute("RcvPipeOutput")]
            internal Microsoft.XLANGs.Pipeline.ReceivePipelineOutputMessages __RcvPipeOutput;
        }


        [System.SerializableAttribute]
        public class ____scope33_3 : Microsoft.XLANGs.Core.ExceptionHandlingContext
        {
            public ____scope33_3(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "??__scope33")
            {
            }

            public override int Index { get { return 3; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[3]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[3]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Microsoft.XLANGs.Core.Segment __seg__;
                Microsoft.XLANGs.Core.FaultReceiveException __fault__;

                __exv__ = _exception;
                if (!(__exv__ is Microsoft.XLANGs.Core.UnknownException))
                {
                    __fault__ = __exv__ as Microsoft.XLANGs.Core.FaultReceiveException;
                    if ((__fault__ == null) && (__exv__ is Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException))
                    {
                        __seg__ = _service._segments[4];
                        __seg__.Reset(1);
                        __seg__.PredecessorDone(_service);
                        return true;
                    }
                }

                Finally();
                return false;
            }

            public override void Finally()
            {
                CMProcessor __svc__ = (CMProcessor)_service;
                ____scope33_3 __ctx3__ = (____scope33_3)(__svc__._stateMgrs[3]);

                if (__ctx3__ != null)
                    __ctx3__.__pEx_0 = null;
                base.Finally();
            }

            internal object __exv__;
            internal Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException __pEx_0
            {
                get { return (Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException)__exv__; }
                set { __exv__ = value; }
            }
        }


        [System.SerializableAttribute]
        public class ____scope35_4 : Microsoft.XLANGs.Core.ExceptionHandlingContext
        {
            public ____scope35_4(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "??__scope35")
            {
            }

            public override int Index { get { return 4; } }

            public override bool CombineParentCommit { get { return true; } }

            public override Microsoft.XLANGs.Core.Segment InitialSegment
            {
                get { return _service._segments[5]; }
            }
            public override Microsoft.XLANGs.Core.Segment FinalSegment
            {
                get { return _service._segments[5]; }
            }

            public override int CompensationSegment { get { return -1; } }
            public override bool OnError()
            {
                Microsoft.XLANGs.Core.Segment __seg__;
                Microsoft.XLANGs.Core.FaultReceiveException __fault__;

                __exv__ = _exception;
                if (!(__exv__ is Microsoft.XLANGs.Core.UnknownException))
                {
                    __fault__ = __exv__ as Microsoft.XLANGs.Core.FaultReceiveException;
                    if ((__fault__ == null) && (__exv__ is Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException))
                    {
                        __seg__ = _service._segments[6];
                        __seg__.Reset(1);
                        __seg__.PredecessorDone(_service);
                        return true;
                    }
                }

                Finally();
                return false;
            }

            public override void Finally()
            {
                CMProcessor __svc__ = (CMProcessor)_service;
                ____scope35_4 __ctx4__ = (____scope35_4)(__svc__._stateMgrs[4]);
                __CMProcessor_1 __ctx1__ = (__CMProcessor_1)(__svc__._stateMgrs[1]);

                if (__ctx4__ != null)
                    __ctx4__.__pEx_0 = null;
                if (__ctx1__ != null && __ctx1__.__OutputInterchange != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__OutputInterchange);
                    __ctx1__.__OutputInterchange = null;
                }
                base.Finally();
            }

            internal object __exv__;
            internal Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException __pEx_0
            {
                get { return (Microsoft.XLANGs.Pipeline.XLANGPipelineManagerException)__exv__; }
                set { __exv__ = value; }
            }
        }

        private static Microsoft.XLANGs.Core.CorrelationType[] _correlationTypes = null;
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

        [Microsoft.XLANGs.BaseTypes.LogicalBindingAttribute()]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("Port_Input")]
        internal TypeAgnosticMessage_PortType Port_Input;
        [Microsoft.XLANGs.BaseTypes.DirectBindingAttribute(typeof(SuspendMessage), "ReceiveFailedMessage")]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("SendFailedInterchange")]
        internal TypeAgnosticMessage_PortType SendFailedInterchange;
        [Microsoft.XLANGs.BaseTypes.LogicalBindingAttribute()]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eUses
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("Port_Output")]
        internal TypeAgnosticMessage_PortType Port_Output;

        public static Microsoft.XLANGs.Core.PortInfo[] _portInfo = new Microsoft.XLANGs.Core.PortInfo[] {
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {TypeAgnosticMessage_PortType.TypeAgnosticMessageOperation},
                                               typeof(CMProcessor).GetField("Port_Input", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.implements,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(CMProcessor), "Port_Input"),
                                               null),
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {TypeAgnosticMessage_PortType.TypeAgnosticMessageOperation},
                                               typeof(CMProcessor).GetField("SendFailedInterchange", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.uses,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(CMProcessor), "SendFailedInterchange"),
                                               null),
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {TypeAgnosticMessage_PortType.TypeAgnosticMessageOperation},
                                               typeof(CMProcessor).GetField("Port_Output", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.uses,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(CMProcessor), "Port_Output"),
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
                h[_portInfo[2].Name] = _portInfo[2];
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
                     new object[5] { _portInfo[0], 0, null , -1, true }
                };
            }
        }

        public static Microsoft.XLANGs.RuntimeTypes.Location[] __eventLocations = new Microsoft.XLANGs.RuntimeTypes.Location[] {
            new Microsoft.XLANGs.RuntimeTypes.Location(0, "00000000-0000-0000-0000-000000000000", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(1, "77b1ecf5-cb22-4b46-a551-90d0d77abbf5", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(2, "77b1ecf5-cb22-4b46-a551-90d0d77abbf5", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(3, "00000000-0000-0000-0000-000000000000", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(4, "c8e43366-c855-4ee8-b1df-cd4874658270", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(5, "00000000-0000-0000-0000-000000000000", 2, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(6, "ca21b67b-6293-4a51-9dfa-eaec75e5ebda", 2, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(7, "00000000-0000-0000-0000-000000000000", 3, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(8, "c1e7dc64-4a82-4eb8-ba61-6a5a34b508ca", 3, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(9, "c1e7dc64-4a82-4eb8-ba61-6a5a34b508ca", 3, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(10, "d41e0e51-cac0-4824-af9b-1d52f1ec19ae", 4, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(11, "cf686824-067f-444d-87e9-3cd72cb1e020", 4, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(12, "cf686824-067f-444d-87e9-3cd72cb1e020", 4, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(13, "10d9421d-2415-4186-94a5-d841b5a5f416", 4, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(14, "10d9421d-2415-4186-94a5-d841b5a5f416", 4, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(15, "dee8c70a-76cc-4fe0-b564-7fd21da63631", 4, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(16, "d41e0e51-cac0-4824-af9b-1d52f1ec19ae", 4, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(17, "ca21b67b-6293-4a51-9dfa-eaec75e5ebda", 2, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(18, "5ce2c101-cdba-4bd5-b0f3-e4ed52466ee6", 2, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(19, "ee062bff-d953-44d4-9589-09dc73bf9a9f", 2, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(20, "ee062bff-d953-44d4-9589-09dc73bf9a9f", 2, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(21, "ea94f894-e19f-4271-9207-ac51ed42ee33", 2, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(22, "ea94f894-e19f-4271-9207-ac51ed42ee33", 2, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(23, "114b998a-a7af-4b26-9b07-ab051f0657cb", 2, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(24, "114b998a-a7af-4b26-9b07-ab051f0657cb", 2, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(25, "5ce2c101-cdba-4bd5-b0f3-e4ed52466ee6", 2, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(26, "c8e43366-c855-4ee8-b1df-cd4874658270", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(27, "e06c3f5d-056c-4e44-a0a9-bed9bd1b61d8", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(28, "00000000-0000-0000-0000-000000000000", 5, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(29, "876bd879-5cfc-4f21-9e39-e7b0399f9afb", 5, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(30, "876bd879-5cfc-4f21-9e39-e7b0399f9afb", 5, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(31, "7c88b1c6-d356-47ec-93c7-8a7465fb6440", 5, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(32, "7c88b1c6-d356-47ec-93c7-8a7465fb6440", 5, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(33, "8dfe9e2a-813c-4200-ac51-e150774b2504", 6, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(34, "09f5dae4-30ef-4aea-a2e4-34915db42995", 6, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(35, "09f5dae4-30ef-4aea-a2e4-34915db42995", 6, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(36, "0479a2f4-4783-4216-96b7-3c6234b0a47e", 6, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(37, "0479a2f4-4783-4216-96b7-3c6234b0a47e", 6, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(38, "677d9ef2-80d4-442a-8d17-4240a7043697", 6, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(39, "8dfe9e2a-813c-4200-ac51-e150774b2504", 6, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(40, "e06c3f5d-056c-4e44-a0a9-bed9bd1b61d8", 1, false)
        };

        public override Microsoft.XLANGs.RuntimeTypes.Location[] EventLocations
        {
            get { return __eventLocations; }
        }

        public static Microsoft.XLANGs.RuntimeTypes.EventData[] __eventData = new Microsoft.XLANGs.RuntimeTypes.EventData[] {
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Body),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Receive),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Scope),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Expression),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Expression),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Catch),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Construct),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Send),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Terminate),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Catch),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Scope),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.WhileBody),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.While),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.While),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.WhileBody),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Body)
        };

        public static int[] __progressLocation0 = new int[] { 0,0,0,3,3,};
        public static int[] __progressLocation1 = new int[] { 0,0,1,1,2,2,4,4,4,26,27,27,27,40,3,3,3,3,};
        public static int[] __progressLocation2 = new int[] { 6,6,6,6,6,6,6,17,18,18,18,19,19,20,21,21,22,23,23,24,25,25,25,25,25,25,};
        public static int[] __progressLocation3 = new int[] { 8,8,8,9,9,9,9,};
        public static int[] __progressLocation4 = new int[] { 10,10,11,11,12,13,13,13,14,15,15,16,16,};
        public static int[] __progressLocation5 = new int[] { 29,29,29,30,31,31,31,32,32,32,32,};
        public static int[] __progressLocation6 = new int[] { 33,33,34,34,35,36,36,36,37,38,38,39,39,};

        public static int[][] __progressLocations = new int[7] [] {__progressLocation0,__progressLocation1,__progressLocation2,__progressLocation3,__progressLocation4,__progressLocation5,__progressLocation6};
        public override int[][] ProgressLocations {get {return __progressLocations;} }

        public Microsoft.XLANGs.Core.StopConditions segment0(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[0];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[0];
            __CMProcessor_root_0 __ctx0__ = (__CMProcessor_root_0)_stateMgrs[0];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                Port_Input = new TypeAgnosticMessage_PortType(0, this);
                Port_Output = new TypeAgnosticMessage_PortType(2, this);
                SendFailedInterchange = new TypeAgnosticMessage_PortType(1, this);
                __ctx__.PrologueCompleted = true;
                __ctx0__.__subWrapper0 = new Microsoft.XLANGs.Core.SubscriptionWrapper(ActivationSubGuids[0], Port_Input, this);
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.Initialized) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.Initialized;
                goto case 1;
            case 1:
                __ctx1__ = new __CMProcessor_1(this);
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
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[1];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[1];
            ____scope35_4 __ctx4__ = (____scope35_4)_stateMgrs[4];
            __CMProcessor_root_0 __ctx0__ = (__CMProcessor_root_0)_stateMgrs[0];
            __Transaction_2 __ctx2__ = (__Transaction_2)_stateMgrs[2];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                __ctx1__.__SendPipeInput = default(Microsoft.XLANGs.Pipeline.SendPipelineInputMessages);
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
                if (!Port_Input.GetMessageId(__ctx0__.__subWrapper0.getSubscription(this), __seg__, __ctx1__, out __msgEnv__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if (__ctx1__.__InputInterchange != null)
                    __ctx1__.UnrefMessage(__ctx1__.__InputInterchange);
                __ctx1__.__InputInterchange = new __messagetype_System_Xml_XmlDocument("InputInterchange", __ctx1__);
                __ctx1__.RefMessage(__ctx1__.__InputInterchange);
                Port_Input.ReceiveMessage(0, __msgEnv__, __ctx1__.__InputInterchange, null, (Microsoft.XLANGs.Core.Context)_stateMgrs[1], __seg__);
                if (Port_Input != null)
                {
                    Port_Input.Close(__ctx1__, __seg__);
                    Port_Input = null;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Receive);
                    __edata.Messages.Add(__ctx1__.__InputInterchange);
                    __edata.PortName = @"Port_Input";
                    Tracker.FireEvent(__eventLocations[2],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                __ctx1__.__SendPipeInput = new Microsoft.XLANGs.Pipeline.SendPipelineInputMessages();
                if ( !PostProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 6;
            case 6:
                if ( !PreProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[4],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 7;
            case 7:
                __ctx2__ = new __Transaction_2(this);
                _stateMgrs[2] = __ctx2__;
                if ( !PostProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 8;
            case 8:
                __ctx1__.StartContext(__seg__, __ctx2__);
                if ( !PostProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                return Microsoft.XLANGs.Core.StopConditions.Blocked;
            case 9:
                if ( !PreProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[26],__eventData[10],_stateMgrs[1].TrackDataStream );
                __ctx2__.Finally();
                if (__ctx2__ != null)
                    __ctx2__.__SendPipeInput = null;
                if (__ctx2__ != null && __ctx2__.__FailedInterchange != null)
                {
                    __ctx2__.UnrefMessage(__ctx2__.__FailedInterchange);
                    __ctx2__.__FailedInterchange = null;
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 10;
            case 10:
                if ( !PreProgressInc( __seg__, __ctx__, 11 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[27],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 11;
            case 11:
                __ctx4__ = new ____scope35_4(this);
                _stateMgrs[4] = __ctx4__;
                if ( !PostProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 12;
            case 12:
                __ctx1__.StartContext(__seg__, __ctx4__);
                if ( !PostProgressInc( __seg__, __ctx__, 13 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                return Microsoft.XLANGs.Core.StopConditions.Blocked;
            case 13:
                if ( !PreProgressInc( __seg__, __ctx__, 14 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if (__ctx1__ != null)
                    __ctx1__.__SendPipeInput = null;
                if (__ctx1__ != null && __ctx1__.__FailedInterchange != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__FailedInterchange);
                    __ctx1__.__FailedInterchange = null;
                }
                if (__ctx1__ != null && __ctx1__.__InputInterchange != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__InputInterchange);
                    __ctx1__.__InputInterchange = null;
                }
                if (SendFailedInterchange != null)
                {
                    SendFailedInterchange.Close(__ctx1__, __seg__);
                    SendFailedInterchange = null;
                }
                if (Port_Output != null)
                {
                    Port_Output.Close(__ctx1__, __seg__);
                    Port_Output = null;
                }
                Tracker.FireEvent(__eventLocations[40],__eventData[10],_stateMgrs[1].TrackDataStream );
                __ctx4__.Finally();
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 14;
            case 14:
                if ( !PreProgressInc( __seg__, __ctx__, 15 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[3],__eventData[15],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 15;
            case 15:
                if (!__ctx1__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 16 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 16;
            case 16:
                if ( !PreProgressInc( __seg__, __ctx__, 17 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx1__.OnCommit();
                goto case 17;
            case 17:
                __seg__.SegmentDone();
                _segments[0].PredecessorDone(this);
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment2(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            bool __condition__;
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[2];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[2];
            ____scope33_3 __ctx3__ = (____scope33_3)_stateMgrs[3];
            __Transaction_2 __ctx2__ = (__Transaction_2)_stateMgrs[2];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                if (__ctx2__.LockRead(0, _segments[2]) == false)  // __CMProcessor_1.__InputInterchange
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if (__ctx2__.LockWrite(1, _segments[2]) == false)  // __CMProcessor_1.__FailedInterchange
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 2;
            case 2:
                if (__ctx2__.LockWrite(2, _segments[2]) == false)  // __CMProcessor_1.__SendPipeInput
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 3;
            case 3:
                __ctx2__.__RcvPipeOutput = default(Microsoft.XLANGs.Pipeline.ReceivePipelineOutputMessages);
                __ctx2__.__TmpMessageOut = null;
                __ctx2__.__TmpMessageIn = null;
                __ctx2__.__FailedInterchange = __ctx1__.__FailedInterchange;
                if (__ctx2__.__FailedInterchange != null)
                    __ctx2__.RefMessage(__ctx2__.__FailedInterchange);
                __ctx2__.__SendPipeInput = (Microsoft.XLANGs.Pipeline.SendPipelineInputMessages)__ctx2__.SaveObject(__ctx1__.__SendPipeInput, 0);
                __ctx__.PrologueCompleted = true;
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[6],__eventData[2],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                __ctx3__ = new ____scope33_3(this);
                _stateMgrs[3] = __ctx3__;
                if ( !PostProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 6;
            case 6:
                __ctx2__.StartContext(__seg__, __ctx3__);
                if ( !PostProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                return Microsoft.XLANGs.Core.StopConditions.Blocked;
            case 7:
                if ( !PreProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[17],__eventData[10],_stateMgrs[2].TrackDataStream );
                __ctx3__.Finally();
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 8;
            case 8:
                if ( !PreProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[18],__eventData[11],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 9;
            case 9:
                __condition__ = __ctx2__.__RcvPipeOutput.MoveNext();
                if (!__condition__)
                {
                    if ( !PostProgressInc( __seg__, __ctx__, 22 ) )
                        return Microsoft.XLANGs.Core.StopConditions.Paused;
                    goto case 22;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 10;
            case 10:
                if ( !PreProgressInc( __seg__, __ctx__, 11 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[18],__eventData[12],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 11;
            case 11:
                if ( !PreProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[19],__eventData[6],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 12;
            case 12:
                {
                    __messagetype_CMP_PipelinesAndSchemas_PO __TmpMessageIn = new __messagetype_CMP_PipelinesAndSchemas_PO("TmpMessageIn", __ctx2__);

                    __TmpMessageIn.part.LoadFrom(null);
                    __ctx2__.__RcvPipeOutput.GetCurrent(CreateMessageWrapperForUserCode(__TmpMessageIn));

                    if (__ctx2__.__TmpMessageIn != null)
                        __ctx2__.UnrefMessage(__ctx2__.__TmpMessageIn);
                    __ctx2__.__TmpMessageIn = __TmpMessageIn;
                    __ctx2__.RefMessage(__ctx2__.__TmpMessageIn);
                }
                __ctx2__.__TmpMessageIn.ConstructionCompleteEvent(true);
                if ( !PostProgressInc( __seg__, __ctx__, 13 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 13;
            case 13:
                if ( !PreProgressInc( __seg__, __ctx__, 14 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx2__.__TmpMessageIn);
                    Tracker.FireEvent(__eventLocations[20],__edata,_stateMgrs[2].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 14;
            case 14:
                if ( !PreProgressInc( __seg__, __ctx__, 15 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[21],__eventData[6],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 15;
            case 15:
                {
                    __messagetype_CMP_PipelinesAndSchemas_Invoice __TmpMessageOut = new __messagetype_CMP_PipelinesAndSchemas_Invoice("TmpMessageOut", __ctx2__);

                    ApplyTransform(typeof(CMP.PipelinesAndSchemas.PO2Invoice), new object[] {__TmpMessageOut.part}, new object[] {__ctx2__.__TmpMessageIn.part});

                    if (__ctx2__.__TmpMessageOut != null)
                        __ctx2__.UnrefMessage(__ctx2__.__TmpMessageOut);
                    __ctx2__.__TmpMessageOut = __TmpMessageOut;
                    __ctx2__.RefMessage(__ctx2__.__TmpMessageOut);
                }
                __ctx2__.__TmpMessageOut.ConstructionCompleteEvent(true);
                if ( !PostProgressInc( __seg__, __ctx__, 16 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 16;
            case 16:
                if ( !PreProgressInc( __seg__, __ctx__, 17 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx2__.__TmpMessageOut);
                    __edata.Messages.Add(__ctx2__.__TmpMessageIn);
                    Tracker.FireEvent(__eventLocations[22],__edata,_stateMgrs[2].TrackDataStream );
                }
                if (__ctx2__ != null && __ctx2__.__TmpMessageIn != null)
                {
                    __ctx2__.UnrefMessage(__ctx2__.__TmpMessageIn);
                    __ctx2__.__TmpMessageIn = null;
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 17;
            case 17:
                if ( !PreProgressInc( __seg__, __ctx__, 18 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[23],__eventData[3],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 18;
            case 18:
                __ctx2__.__SendPipeInput.Add(CreateMessageWrapperForUserCode(__ctx2__.__TmpMessageOut));
                if (__ctx2__ != null && __ctx2__.__TmpMessageOut != null)
                {
                    __ctx2__.UnrefMessage(__ctx2__.__TmpMessageOut);
                    __ctx2__.__TmpMessageOut = null;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 19 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 19;
            case 19:
                if ( !PreProgressInc( __seg__, __ctx__, 20 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[24],__eventData[4],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 20;
            case 20:
                if ( !PreProgressInc( __seg__, __ctx__, 21 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[25],__eventData[13],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 21;
            case 21:
                if ( !PostProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 9;
            case 22:
                if ( !PreProgressInc( __seg__, __ctx__, 23 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if (__ctx2__ != null)
                    __ctx2__.__RcvPipeOutput = null;
                Tracker.FireEvent(__eventLocations[25],__eventData[14],_stateMgrs[2].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 23;
            case 23:
                if (!__ctx2__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 24 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 24;
            case 24:
                if ( !PreProgressInc( __seg__, __ctx__, 25 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx2__.OnCommit();
                goto case 25;
            case 25:
                __seg__.SegmentDone();
                _segments[1].PredecessorDone(this);
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment3(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[3];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[3];
            ____scope33_3 __ctx3__ = (____scope33_3)_stateMgrs[3];
            __Transaction_2 __ctx2__ = (__Transaction_2)_stateMgrs[2];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                __ctx__.PrologueCompleted = true;
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if ( !PreProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[8],__eventData[3],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 2;
            case 2:
                __ctx2__.__RcvPipeOutput = Microsoft.XLANGs.Pipeline.XLANGPipelineManager.ExecuteReceivePipeline(typeof(CMP.PipelinesAndSchemas.FFReceivePipeline), CreateMessageWrapperForUserCode(__ctx1__.__InputInterchange));
                if ( !PostProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 3;
            case 3:
                if ( !PreProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[9],__eventData[4],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 4;
            case 4:
                if (!__ctx3__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 5;
            case 5:
                if ( !PreProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx3__.OnCommit();
                goto case 6;
            case 6:
                __seg__.SegmentDone();
                _segments[2].PredecessorDone(this);
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment4(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[4];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[3];
            ____scope33_3 __ctx3__ = (____scope33_3)_stateMgrs[3];
            __CMProcessor_root_0 __ctx0__ = (__CMProcessor_root_0)_stateMgrs[0];
            __Transaction_2 __ctx2__ = (__Transaction_2)_stateMgrs[2];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                OnBeginCatchHandler(3);
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if ( !PreProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[10],__eventData[5],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 2;
            case 2:
                if ( !PreProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[11],__eventData[6],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 3;
            case 3:
                {
                    __messagetype_System_Xml_XmlDocument __FailedInterchange = new __messagetype_System_Xml_XmlDocument("FailedInterchange", __ctx2__);

                    __FailedInterchange.CopyFrom(__ctx1__.__InputInterchange);
                    __FailedInterchange.SetPropertyValue(typeof(CMP.PipelinesAndSchemas.ErrorDescription), __ctx3__.__pEx_0.Message);

                    if (__ctx2__.__FailedInterchange != null)
                        __ctx2__.UnrefMessage(__ctx2__.__FailedInterchange);
                    __ctx2__.__FailedInterchange = __FailedInterchange;
                    __ctx2__.RefMessage(__ctx2__.__FailedInterchange);
                }
                __ctx2__.__FailedInterchange.ConstructionCompleteEvent(false);
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx2__.__FailedInterchange);
                    Tracker.FireEvent(__eventLocations[12],__edata,_stateMgrs[3].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                if ( !PreProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[13],__eventData[7],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 6;
            case 6:
                if (!__ctx3__.PrepareToPendingCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 7;
            case 7:
                if ( !PreProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                SendFailedInterchange.SendMessage(0, __ctx2__.__FailedInterchange, null, null, __ctx3__, __seg__ , Microsoft.XLANGs.Core.ActivityFlags.None );
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.OutgoingRqst) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.OutgoingRqst;
                goto case 8;
            case 8:
                if ( !PreProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Send);
                    __edata.Messages.Add(__ctx2__.__FailedInterchange);
                    __edata.PortName = @"SendFailedInterchange";
                    Tracker.FireEvent(__eventLocations[14],__edata,_stateMgrs[3].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 9;
            case 9:
                if ( !PreProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[15],__eventData[8],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 10;
            case 10:
                RequestTerminate(__ctx3__,"Receive pipeline execution failed with the following error:" + __ctx3__.__pEx_0.Message);
                __seg__.SegmentDone();
                if (__ctx3__ != null)
                    __ctx3__.__pEx_0 = null;
                break;
            case 11:
                if ( !PreProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[16],__eventData[9],_stateMgrs[3].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 12;
            case 12:
                __ctx3__.__exv__ = null;
                OnEndCatchHandler(3, __seg__);
                __seg__.SegmentDone();
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment5(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[5];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[4];
            ____scope35_4 __ctx4__ = (____scope35_4)_stateMgrs[4];
            __CMProcessor_root_0 __ctx0__ = (__CMProcessor_root_0)_stateMgrs[0];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                __ctx__.PrologueCompleted = true;
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if ( !PreProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[29],__eventData[6],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 2;
            case 2:
                {
                    __messagetype_System_Xml_XmlDocument __OutputInterchange = new __messagetype_System_Xml_XmlDocument("OutputInterchange", __ctx1__);

                    __OutputInterchange.part.LoadFrom(null);
                    Microsoft.XLANGs.Pipeline.XLANGPipelineManager.ExecuteSendPipeline(typeof(CMP.PipelinesAndSchemas.FFSendPipeline), __ctx1__.__SendPipeInput, CreateMessageWrapperForUserCode(__OutputInterchange));

                    if (__ctx1__.__OutputInterchange != null)
                        __ctx1__.UnrefMessage(__ctx1__.__OutputInterchange);
                    __ctx1__.__OutputInterchange = __OutputInterchange;
                    __ctx1__.RefMessage(__ctx1__.__OutputInterchange);
                }
                __ctx1__.__OutputInterchange.ConstructionCompleteEvent(true);
                if ( !PostProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 3;
            case 3:
                if ( !PreProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx1__.__OutputInterchange);
                    Tracker.FireEvent(__eventLocations[30],__edata,_stateMgrs[4].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[31],__eventData[7],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                if (!__ctx4__.PrepareToPendingCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 6;
            case 6:
                if ( !PreProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Port_Output.SendMessage(0, __ctx1__.__OutputInterchange, null, null, __ctx4__, __seg__ , Microsoft.XLANGs.Core.ActivityFlags.NextActivityPersists );
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.OutgoingRqst) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.OutgoingRqst;
                goto case 7;
            case 7:
                if ( !PreProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Send);
                    __edata.Messages.Add(__ctx1__.__OutputInterchange);
                    __edata.PortName = @"Port_Output";
                    Tracker.FireEvent(__eventLocations[32],__edata,_stateMgrs[4].TrackDataStream );
                }
                if (__ctx1__ != null && __ctx1__.__OutputInterchange != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__OutputInterchange);
                    __ctx1__.__OutputInterchange = null;
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 8;
            case 8:
                if (!__ctx4__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 9;
            case 9:
                if ( !PreProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx4__.OnCommit();
                goto case 10;
            case 10:
                __seg__.SegmentDone();
                _segments[1].PredecessorDone(this);
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }

        public Microsoft.XLANGs.Core.StopConditions segment6(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[6];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[4];
            ____scope35_4 __ctx4__ = (____scope35_4)_stateMgrs[4];
            __CMProcessor_root_0 __ctx0__ = (__CMProcessor_root_0)_stateMgrs[0];
            __CMProcessor_1 __ctx1__ = (__CMProcessor_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                OnBeginCatchHandler(4);
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 1;
            case 1:
                if ( !PreProgressInc( __seg__, __ctx__, 2 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[33],__eventData[5],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 2;
            case 2:
                if ( !PreProgressInc( __seg__, __ctx__, 3 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[34],__eventData[6],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 3;
            case 3:
                {
                    __messagetype_System_Xml_XmlDocument __FailedInterchange = new __messagetype_System_Xml_XmlDocument("FailedInterchange", __ctx1__);

                    __FailedInterchange.CopyFrom(__ctx1__.__InputInterchange);
                    __FailedInterchange.SetPropertyValue(typeof(CMP.PipelinesAndSchemas.ErrorDescription), __ctx4__.__pEx_0.Message);

                    if (__ctx1__.__FailedInterchange != null)
                        __ctx1__.UnrefMessage(__ctx1__.__FailedInterchange);
                    __ctx1__.__FailedInterchange = __FailedInterchange;
                    __ctx1__.RefMessage(__ctx1__.__FailedInterchange);
                }
                __ctx1__.__FailedInterchange.ConstructionCompleteEvent(false);
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Construct);
                    __edata.Messages.Add(__ctx1__.__FailedInterchange);
                    Tracker.FireEvent(__eventLocations[35],__edata,_stateMgrs[4].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                if ( !PreProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[36],__eventData[7],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 6;
            case 6:
                if (!__ctx4__.PrepareToPendingCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 7;
            case 7:
                if ( !PreProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                SendFailedInterchange.SendMessage(0, __ctx1__.__FailedInterchange, null, null, __ctx4__, __seg__ , Microsoft.XLANGs.Core.ActivityFlags.None );
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.OutgoingRqst) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.OutgoingRqst;
                goto case 8;
            case 8:
                if ( !PreProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Send);
                    __edata.Messages.Add(__ctx1__.__FailedInterchange);
                    __edata.PortName = @"SendFailedInterchange";
                    Tracker.FireEvent(__eventLocations[37],__edata,_stateMgrs[4].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 9;
            case 9:
                if ( !PreProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[38],__eventData[8],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 10;
            case 10:
                RequestTerminate(__ctx4__,"Send pipeline execution failed with the following error:" + __ctx4__.__pEx_0.Message);
                __seg__.SegmentDone();
                if (__ctx4__ != null)
                    __ctx4__.__pEx_0 = null;
                break;
            case 11:
                if ( !PreProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[39],__eventData[9],_stateMgrs[4].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 12;
            case 12:
                __ctx4__.__exv__ = null;
                OnEndCatchHandler(4, __seg__);
                __seg__.SegmentDone();
                break;
            }
            return Microsoft.XLANGs.Core.StopConditions.Completed;
        }
    }
    //#line 89 "C:\Program Files (x86)\Microsoft BizTalk Server 2016\SDK\Samples\Pipelines\ComposedMessageProcessor\Orchestration\SuspendMessage.odx"
    [Microsoft.XLANGs.BaseTypes.StaticSubscriptionAttribute(
        0, "ReceiveFailedMessage", "TypeAgnosticMessageOperation", -1, -1, true
    )]
    [Microsoft.XLANGs.BaseTypes.ServicePortsAttribute(
        new Microsoft.XLANGs.BaseTypes.EXLangSParameter[] {
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.ePort|Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements
        },
        new System.Type[] {
            typeof(CMP.Orchestration.TypeAgnosticMessage_PortType)
        },
        new System.String[] {
            "ReceiveFailedMessage"
        },
        new System.Type[] {
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
    sealed internal class SuspendMessage : Microsoft.BizTalk.XLANGs.BTXEngine.BTXService
    {
        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.eInternal;
        public static readonly bool __execable = false;
        [Microsoft.XLANGs.BaseTypes.CallCompensationAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSCallCompensationInfo.eHasSuspend
,
            new System.String[] {
            },
            new System.String[] {
            }
        )]
        public static void __bodyProxy()
        {
        }
        private static System.Guid _serviceId = Microsoft.XLANGs.Core.HashHelper.HashServiceType(typeof(SuspendMessage));
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

        static SuspendMessage()
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
            _rootContext = new __SuspendMessage_root_0(this);
            _stateMgrs = new Microsoft.XLANGs.Core.IStateManager[2];
            _stateMgrs[0] = _rootContext;
            FinalConstruct();
        }

        public SuspendMessage(System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXSession session, Microsoft.BizTalk.XLANGs.BTXEngine.BTXEvents tracker)
            : base(instanceId, session, "SuspendMessage", tracker)
        {
            ConstructorHelper();
        }

        public SuspendMessage(int callIndex, System.Guid instanceId, Microsoft.BizTalk.XLANGs.BTXEngine.BTXService parent)
            : base(callIndex, instanceId, parent, "SuspendMessage")
        {
            ConstructorHelper();
        }

        private const string _symInfo = @"
<XsymFile>
<ProcessFlow xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>      <shapeType>RootShape</shapeType>      <ShapeID>4e43a804-b3b3-42b4-9505-8743e2b1d340</ShapeID>      
<children>                          
<ShapeInfo>      <shapeType>ReceiveShape</shapeType>      <ShapeID>2a34699f-0ea7-489a-8476-dd39b3fae8e7</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>ReceiveMessageToSuspend</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>VariableAssignmentShape</shapeType>      <ShapeID>57823b43-0814-45f9-b866-011524177a0d</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>BuildErrorDetailsString</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>SuspendShape</shapeType>      <ShapeID>6a06bbdc-8d76-4100-8400-b87b642ba396</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>SuspendInstance</shapeText>                  
<children>                </children>
  </ShapeInfo>
                            
<ShapeInfo>      <shapeType>VariableAssignmentShape</shapeType>      <ShapeID>ed9f86e8-9f75-460a-9531-fcafee5b8b05</ShapeID>      <ParentLink>ServiceBody_Statement</ParentLink>                <shapeText>DummyExpression</shapeText>                  
<children>                </children>
  </ShapeInfo>
                  </children>
  </ProcessFlow><Metadata>

<TrkMetadata>
<ActionName>'SuspendMessage'</ActionName><IsAtomic>0</IsAtomic><Line>89</Line><Position>14</Position><ShapeID>'e211a116-cb8b-44e7-a052-0de295aa0001'</ShapeID>
</TrkMetadata>

<TrkMetadata>
<Line>98</Line><Position>22</Position><ShapeID>'2a34699f-0ea7-489a-8476-dd39b3fae8e7'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>101</Line><Position>23</Position><ShapeID>'57823b43-0814-45f9-b866-011524177a0d'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>103</Line><Position>13</Position><ShapeID>'6a06bbdc-8d76-4100-8400-b87b642ba396'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>

<TrkMetadata>
<Line>105</Line><Position>23</Position><ShapeID>'ed9f86e8-9f75-460a-9531-fcafee5b8b05'</ShapeID>
<Messages>
</Messages>
</TrkMetadata>
</Metadata>
</XsymFile>";

        public override string odXml { get { return _symODXML; } }

        private const string _symODXML = @"
<?xml version='1.0' encoding='utf-8' standalone='yes'?>
<om:MetaModel MajorVersion='1' MinorVersion='3' Core='2b131234-7959-458d-834f-2dc0769ce683' ScheduleModel='66366196-361d-448d-976f-cab5e87496d2' xmlns:om='http://schemas.microsoft.com/BizTalk/2003/DesignerData'>
    <om:Element Type='Module' OID='3050aeaa-bf4c-48cd-b91e-31c35e826cdd' LowerBound='1.1' HigherBound='25.1'>
        <om:Property Name='ReportToAnalyst' Value='True' />
        <om:Property Name='Name' Value='CMP.Orchestration' />
        <om:Property Name='Signal' Value='False' />
        <om:Element Type='ServiceDeclaration' OID='48ff180a-0189-46d2-a2fc-8644be06ff00' ParentLink='Module_ServiceDeclaration' LowerBound='4.1' HigherBound='24.1'>
            <om:Property Name='InitializedTransactionType' Value='True' />
            <om:Property Name='IsInvokable' Value='False' />
            <om:Property Name='TypeModifier' Value='Internal' />
            <om:Property Name='ReportToAnalyst' Value='True' />
            <om:Property Name='Name' Value='SuspendMessage' />
            <om:Property Name='Signal' Value='True' />
            <om:Element Type='VariableDeclaration' OID='1fc694eb-7c0b-4b18-bbeb-b5e952e46294' ParentLink='ServiceDeclaration_VariableDeclaration' LowerBound='10.1' HigherBound='11.1'>
                <om:Property Name='UseDefaultConstructor' Value='False' />
                <om:Property Name='Type' Value='System.String' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='ErrorText' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='MessageDeclaration' OID='062202e1-2789-4d6a-875f-bf3abc5e0813' ParentLink='ServiceDeclaration_MessageDeclaration' LowerBound='9.1' HigherBound='10.1'>
                <om:Property Name='Type' Value='System.Xml.XmlDocument' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='MessageToSuspend' />
                <om:Property Name='Signal' Value='True' />
            </om:Element>
            <om:Element Type='ServiceBody' OID='4e43a804-b3b3-42b4-9505-8743e2b1d340' ParentLink='ServiceDeclaration_ServiceBody'>
                <om:Property Name='Signal' Value='False' />
                <om:Element Type='Receive' OID='2a34699f-0ea7-489a-8476-dd39b3fae8e7' ParentLink='ServiceBody_Statement' LowerBound='13.1' HigherBound='16.1'>
                    <om:Property Name='Activate' Value='True' />
                    <om:Property Name='PortName' Value='ReceiveFailedMessage' />
                    <om:Property Name='MessageName' Value='MessageToSuspend' />
                    <om:Property Name='OperationName' Value='TypeAgnosticMessageOperation' />
                    <om:Property Name='OperationMessageName' Value='Request' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='ReceiveMessageToSuspend' />
                    <om:Property Name='Signal' Value='True' />
                </om:Element>
                <om:Element Type='VariableAssignment' OID='57823b43-0814-45f9-b866-011524177a0d' ParentLink='ServiceBody_Statement' LowerBound='16.1' HigherBound='18.1'>
                    <om:Property Name='Expression' Value='ErrorText = &quot;Error Description: &quot; + MessageToSuspend(CMP.PipelinesAndSchemas.ErrorDescription);' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='BuildErrorDetailsString' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
                <om:Element Type='Suspend' OID='6a06bbdc-8d76-4100-8400-b87b642ba396' ParentLink='ServiceBody_Statement' LowerBound='18.1' HigherBound='20.1'>
                    <om:Property Name='ErrorMessage' Value='ErrorText;' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='SuspendInstance' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
                <om:Element Type='VariableAssignment' OID='ed9f86e8-9f75-460a-9531-fcafee5b8b05' ParentLink='ServiceBody_Statement' LowerBound='20.1' HigherBound='22.1'>
                    <om:Property Name='Expression' Value='ErrorText = MessageToSuspend(CMP.PipelinesAndSchemas.ErrorDescription);' />
                    <om:Property Name='ReportToAnalyst' Value='True' />
                    <om:Property Name='Name' Value='DummyExpression' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
            <om:Element Type='PortDeclaration' OID='d5e8580c-19d3-4115-9b76-f5a62b19b724' ParentLink='ServiceDeclaration_PortDeclaration' LowerBound='7.1' HigherBound='9.1'>
                <om:Property Name='PortModifier' Value='Implements' />
                <om:Property Name='Orientation' Value='Left' />
                <om:Property Name='PortIndex' Value='5' />
                <om:Property Name='IsWebPort' Value='False' />
                <om:Property Name='OrderedDelivery' Value='False' />
                <om:Property Name='DeliveryNotification' Value='None' />
                <om:Property Name='Type' Value='CMP.Orchestration.TypeAgnosticMessage_PortType' />
                <om:Property Name='ParamDirection' Value='In' />
                <om:Property Name='ReportToAnalyst' Value='True' />
                <om:Property Name='Name' Value='ReceiveFailedMessage' />
                <om:Property Name='Signal' Value='True' />
                <om:Element Type='DirectBindingAttribute' OID='cf87981f-21df-4486-bcef-6eeffa41af89' ParentLink='PortDeclaration_CLRAttribute' LowerBound='7.1' HigherBound='8.1'>
                    <om:Property Name='PartnerPort' Value='ReceiveFailedMessage' />
                    <om:Property Name='PartnerService' Value='CMP.Orchestration.SuspendMessage' />
                    <om:Property Name='DirectBindingType' Value='PartnerPort' />
                    <om:Property Name='Signal' Value='False' />
                </om:Element>
            </om:Element>
        </om:Element>
    </om:Element>
</om:MetaModel>
";

        [System.SerializableAttribute]
        public class __SuspendMessage_root_0 : Microsoft.XLANGs.Core.ServiceContext
        {
            public __SuspendMessage_root_0(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "SuspendMessage")
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
                SuspendMessage __svc__ = (SuspendMessage)_service;
                __SuspendMessage_root_0 __ctx0__ = (__SuspendMessage_root_0)(__svc__._stateMgrs[0]);

                if (__svc__.ReceiveFailedMessage != null)
                {
                    __svc__.ReceiveFailedMessage.Close(this, null);
                    __svc__.ReceiveFailedMessage = null;
                }
                base.Finally();
            }

            internal Microsoft.XLANGs.Core.SubscriptionWrapper __subWrapper0;
        }


        [System.SerializableAttribute]
        public class __SuspendMessage_1 : Microsoft.XLANGs.Core.ExceptionHandlingContext
        {
            public __SuspendMessage_1(Microsoft.XLANGs.Core.Service svc)
                : base(svc, "SuspendMessage")
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
                SuspendMessage __svc__ = (SuspendMessage)_service;
                __SuspendMessage_1 __ctx1__ = (__SuspendMessage_1)(__svc__._stateMgrs[1]);

                if (__ctx1__ != null && __ctx1__.__MessageToSuspend != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__MessageToSuspend);
                    __ctx1__.__MessageToSuspend = null;
                }
                if (__ctx1__ != null)
                    __ctx1__.__ErrorText = null;
                base.Finally();
            }

            [Microsoft.XLANGs.Core.UserVariableAttribute("MessageToSuspend")]
            public __messagetype_System_Xml_XmlDocument __MessageToSuspend;
            [Microsoft.XLANGs.Core.UserVariableAttribute("ErrorText")]
            internal System.String __ErrorText;
        }

        private static Microsoft.XLANGs.Core.CorrelationType[] _correlationTypes = null;
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

        [Microsoft.XLANGs.BaseTypes.DirectBindingAttribute(typeof(SuspendMessage), "ReceiveFailedMessage")]
        [Microsoft.XLANGs.BaseTypes.PortAttribute(
            Microsoft.XLANGs.BaseTypes.EXLangSParameter.eImplements
        )]
        [Microsoft.XLANGs.Core.UserVariableAttribute("ReceiveFailedMessage")]
        internal TypeAgnosticMessage_PortType ReceiveFailedMessage;

        public static Microsoft.XLANGs.Core.PortInfo[] _portInfo = new Microsoft.XLANGs.Core.PortInfo[] {
            new Microsoft.XLANGs.Core.PortInfo(new Microsoft.XLANGs.Core.OperationInfo[] {TypeAgnosticMessage_PortType.TypeAgnosticMessageOperation},
                                               typeof(SuspendMessage).GetField("ReceiveFailedMessage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                                               Microsoft.XLANGs.BaseTypes.Polarity.implements,
                                               false,
                                               Microsoft.XLANGs.Core.HashHelper.HashPort(typeof(SuspendMessage), "ReceiveFailedMessage"),
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
                     new object[5] { _portInfo[0], 0, null , -1, true }
                };
            }
        }

        public static Microsoft.XLANGs.RuntimeTypes.Location[] __eventLocations = new Microsoft.XLANGs.RuntimeTypes.Location[] {
            new Microsoft.XLANGs.RuntimeTypes.Location(0, "00000000-0000-0000-0000-000000000000", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(1, "2a34699f-0ea7-489a-8476-dd39b3fae8e7", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(2, "2a34699f-0ea7-489a-8476-dd39b3fae8e7", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(3, "00000000-0000-0000-0000-000000000000", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(4, "57823b43-0814-45f9-b866-011524177a0d", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(5, "57823b43-0814-45f9-b866-011524177a0d", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(6, "6a06bbdc-8d76-4100-8400-b87b642ba396", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(7, "6a06bbdc-8d76-4100-8400-b87b642ba396", 1, false),
            new Microsoft.XLANGs.RuntimeTypes.Location(8, "ed9f86e8-9f75-460a-9531-fcafee5b8b05", 1, true),
            new Microsoft.XLANGs.RuntimeTypes.Location(9, "ed9f86e8-9f75-460a-9531-fcafee5b8b05", 1, false)
        };

        public override Microsoft.XLANGs.RuntimeTypes.Location[] EventLocations
        {
            get { return __eventLocations; }
        }

        public static Microsoft.XLANGs.RuntimeTypes.EventData[] __eventData = new Microsoft.XLANGs.RuntimeTypes.EventData[] {
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Body),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Receive),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Expression),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Expression),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.Start | Microsoft.XLANGs.RuntimeTypes.Operation.Suspend),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Suspend),
            new Microsoft.XLANGs.RuntimeTypes.EventData( Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Body)
        };

        public static int[] __progressLocation0 = new int[] { 0,0,0,3,3,};
        public static int[] __progressLocation1 = new int[] { 0,0,1,1,2,2,4,4,5,6,6,7,8,8,9,3,3,3,3,};

        public static int[][] __progressLocations = new int[2] [] {__progressLocation0,__progressLocation1};
        public override int[][] ProgressLocations {get {return __progressLocations;} }

        public Microsoft.XLANGs.Core.StopConditions segment0(Microsoft.XLANGs.Core.StopConditions stopOn)
        {
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[0];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[0];
            __SuspendMessage_root_0 __ctx0__ = (__SuspendMessage_root_0)_stateMgrs[0];
            __SuspendMessage_1 __ctx1__ = (__SuspendMessage_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                ReceiveFailedMessage = new TypeAgnosticMessage_PortType(0, this);
                __ctx__.PrologueCompleted = true;
                __ctx0__.__subWrapper0 = new Microsoft.XLANGs.Core.SubscriptionWrapper(ActivationSubGuids[0], ReceiveFailedMessage, this);
                if ( !PostProgressInc( __seg__, __ctx__, 1 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                if ((stopOn & Microsoft.XLANGs.Core.StopConditions.Initialized) != 0)
                    return Microsoft.XLANGs.Core.StopConditions.Initialized;
                goto case 1;
            case 1:
                __ctx1__ = new __SuspendMessage_1(this);
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
            Microsoft.XLANGs.Core.Segment __seg__ = _segments[1];
            Microsoft.XLANGs.Core.Context __ctx__ = (Microsoft.XLANGs.Core.Context)_stateMgrs[1];
            __SuspendMessage_root_0 __ctx0__ = (__SuspendMessage_root_0)_stateMgrs[0];
            __SuspendMessage_1 __ctx1__ = (__SuspendMessage_1)_stateMgrs[1];

            switch (__seg__.Progress)
            {
            case 0:
                __ctx1__.__ErrorText = default(System.String);
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
                if (!ReceiveFailedMessage.GetMessageId(__ctx0__.__subWrapper0.getSubscription(this), __seg__, __ctx1__, out __msgEnv__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if (__ctx1__.__MessageToSuspend != null)
                    __ctx1__.UnrefMessage(__ctx1__.__MessageToSuspend);
                __ctx1__.__MessageToSuspend = new __messagetype_System_Xml_XmlDocument("MessageToSuspend", __ctx1__);
                __ctx1__.RefMessage(__ctx1__.__MessageToSuspend);
                ReceiveFailedMessage.ReceiveMessage(0, __msgEnv__, __ctx1__.__MessageToSuspend, null, (Microsoft.XLANGs.Core.Context)_stateMgrs[1], __seg__);
                if (ReceiveFailedMessage != null)
                {
                    ReceiveFailedMessage.Close(__ctx1__, __seg__);
                    ReceiveFailedMessage = null;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 4 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 4;
            case 4:
                if ( !PreProgressInc( __seg__, __ctx__, 5 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                {
                    Microsoft.XLANGs.RuntimeTypes.EventData __edata = new Microsoft.XLANGs.RuntimeTypes.EventData(Microsoft.XLANGs.RuntimeTypes.Operation.End | Microsoft.XLANGs.RuntimeTypes.Operation.Receive);
                    __edata.Messages.Add(__ctx1__.__MessageToSuspend);
                    __edata.PortName = @"ReceiveFailedMessage";
                    Tracker.FireEvent(__eventLocations[2],__edata,_stateMgrs[1].TrackDataStream );
                }
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 5;
            case 5:
                __ctx1__.__ErrorText = "";
                if ( !PostProgressInc( __seg__, __ctx__, 6 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 6;
            case 6:
                if ( !PreProgressInc( __seg__, __ctx__, 7 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[4],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 7;
            case 7:
                __ctx1__.__ErrorText = "Error Description: " + (System.String)__ctx1__.__MessageToSuspend.GetPropertyValueThrows(typeof(CMP.PipelinesAndSchemas.ErrorDescription));
                if ( !PostProgressInc( __seg__, __ctx__, 8 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 8;
            case 8:
                if ( !PreProgressInc( __seg__, __ctx__, 9 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[5],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 9;
            case 9:
                if ( !PreProgressInc( __seg__, __ctx__, 10 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[6],__eventData[4],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 10;
            case 10:
                RequestSuspend(__seg__, __ctx1__,__ctx1__.__ErrorText);
                if ( !PostProgressInc( __seg__, __ctx__, 11 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                return Microsoft.XLANGs.Core.StopConditions.Blocked;
            case 11:
                if ( !PreProgressInc( __seg__, __ctx__, 12 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[7],__eventData[5],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 12;
            case 12:
                if ( !PreProgressInc( __seg__, __ctx__, 13 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[8],__eventData[2],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 13;
            case 13:
                __ctx1__.__ErrorText = (System.String)__ctx1__.__MessageToSuspend.GetPropertyValueThrows(typeof(CMP.PipelinesAndSchemas.ErrorDescription));
                if (__ctx1__ != null)
                    __ctx1__.__ErrorText = null;
                if (__ctx1__ != null && __ctx1__.__MessageToSuspend != null)
                {
                    __ctx1__.UnrefMessage(__ctx1__.__MessageToSuspend);
                    __ctx1__.__MessageToSuspend = null;
                }
                if ( !PostProgressInc( __seg__, __ctx__, 14 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 14;
            case 14:
                if ( !PreProgressInc( __seg__, __ctx__, 15 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[9],__eventData[3],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 15;
            case 15:
                if ( !PreProgressInc( __seg__, __ctx__, 16 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                Tracker.FireEvent(__eventLocations[3],__eventData[6],_stateMgrs[1].TrackDataStream );
                if (IsDebugged)
                    return Microsoft.XLANGs.Core.StopConditions.InBreakpoint;
                goto case 16;
            case 16:
                if (!__ctx1__.CleanupAndPrepareToCommit(__seg__))
                    return Microsoft.XLANGs.Core.StopConditions.Blocked;
                if ( !PostProgressInc( __seg__, __ctx__, 17 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                goto case 17;
            case 17:
                if ( !PreProgressInc( __seg__, __ctx__, 18 ) )
                    return Microsoft.XLANGs.Core.StopConditions.Paused;
                __ctx1__.OnCommit();
                goto case 18;
            case 18:
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

    [System.SerializableAttribute]
    sealed public class __CMP_PipelinesAndSchemas_Invoice__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static CMP.PipelinesAndSchemas.Invoice _schema = new CMP.PipelinesAndSchemas.Invoice();

        public __CMP_PipelinesAndSchemas_Invoice__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eThirdKind,
        "CMP.PipelinesAndSchemas.Invoice",
        new System.Type[]{
            typeof(CMP.PipelinesAndSchemas.Invoice)
        },
        new string[]{
            "part"
        },
        new System.Type[]{
            typeof(__CMP_PipelinesAndSchemas_Invoice__)
        },
        0,
        @"http://CMP.Invoice#Invoice"
    )]
    [System.SerializableAttribute]
    sealed public class __messagetype_CMP_PipelinesAndSchemas_Invoice : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __CMP_PipelinesAndSchemas_Invoice__ part;

        private void __CreatePartWrappers()
        {
            part = new __CMP_PipelinesAndSchemas_Invoice__(this, "part", 0);
            this.AddPart("part", 0, part);
        }

        public __messagetype_CMP_PipelinesAndSchemas_Invoice(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [System.SerializableAttribute]
    sealed public class __CMP_PipelinesAndSchemas_PO__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static CMP.PipelinesAndSchemas.PO _schema = new CMP.PipelinesAndSchemas.PO();

        public __CMP_PipelinesAndSchemas_PO__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eThirdKind,
        "CMP.PipelinesAndSchemas.PO",
        new System.Type[]{
            typeof(CMP.PipelinesAndSchemas.PO)
        },
        new string[]{
            "part"
        },
        new System.Type[]{
            typeof(__CMP_PipelinesAndSchemas_PO__)
        },
        0,
        @"http://CMP.PO#PO"
    )]
    [System.SerializableAttribute]
    sealed public class __messagetype_CMP_PipelinesAndSchemas_PO : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __CMP_PipelinesAndSchemas_PO__ part;

        private void __CreatePartWrappers()
        {
            part = new __CMP_PipelinesAndSchemas_PO__(this, "part", 0);
            this.AddPart("part", 0, part);
        }

        public __messagetype_CMP_PipelinesAndSchemas_PO(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(false)]
    sealed public class _MODULE_PROXY_ { }
}
