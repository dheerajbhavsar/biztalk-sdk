namespace CMP.PipelinesAndSchemas
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class FFSendPipeline : Microsoft.BizTalk.PipelineOM.SendPipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>8c6b051c-0ff5-4fc2-9ae5-5016cb726282</CategoryId>  <FriendlyName>Transmit</FriendlyName"+
">  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Pre-Assemble\" minO"+
"ccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4101-4cce-4536-83fa-4a5040674ad6\" />      <Co"+
"mponents />    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"2\" Name=\"Assemb"+
"le\" minOccurs=\"0\" maxOccurs=\"1\" execMethod=\"All\" stageId=\"9d0e4107-4cce-4536-83fa-4a5040674ad6\" />  "+
"    <Components>        <Component>          <Name>Microsoft.BizTalk.Component.FFAsmComp,Microsoft.B"+
"izTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</Name>"+
"          <ComponentName>Flat file assembler</ComponentName>          <Description>Flat file assembl"+
"er component.</Description>          <Version>1.0</Version>          <Properties>            <Proper"+
"ty Name=\"HeaderSpecName\">              <Value xsi:type=\"xsd:string\">CMP.PipelinesAndSchemas.InvoiceH"+
"eader</Value>            </Property>            <Property Name=\"DocumentSpecName\">              <Val"+
"ue xsi:type=\"xsd:string\">CMP.PipelinesAndSchemas.Invoice</Value>            </Property>            <"+
"Property Name=\"TrailerSpecName\" />            <Property Name=\"TargetCharset\">              <Value xs"+
"i:type=\"xsd:string\" />            </Property>            <Property Name=\"TargetCodePage\">           "+
"   <Value xsi:type=\"xsd:int\">0</Value>            </Property>            <Property Name=\"PreserveBom"+
"\">              <Value xsi:type=\"xsd:boolean\">false</Value>            </Property>          </Proper"+
"ties>          <CachedDisplayName>Flat file assembler</CachedDisplayName>          <CachedIsManaged>"+
"true</CachedIsManaged>        </Component>      </Components>    </Stage>    <Stage>      <PolicyFil"+
"eStage _locAttrData=\"Name\" _locID=\"3\" Name=\"Encode\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" st"+
"ageId=\"9d0e4108-4cce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>  </Stages></Document"+
">";
        
        private const string _versionDependentGuid = "9cd73bcd-413a-4269-b82b-391a1651141d";
        
        public FFSendPipeline()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4107-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Microsoft.BizTalk.Component.FFAsmComp,Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"HeaderSpecName\""+
">      <Value xsi:type=\"xsd:string\">CMP.PipelinesAndSchemas.InvoiceHeader</Value>    </Property>    "+
"<Property Name=\"DocumentSpecName\">      <Value xsi:type=\"xsd:string\">CMP.PipelinesAndSchemas.Invoice"+
"</Value>    </Property>    <Property Name=\"TrailerSpecName\" />    <Property Name=\"TargetCharset\">   "+
"   <Value xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"TargetCodePage\">      <Value xs"+
"i:type=\"xsd:int\">0</Value>    </Property>    <Property Name=\"PreserveBom\">      <Value xsi:type=\"xsd"+
":boolean\">false</Value>    </Property>  </Properties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}
