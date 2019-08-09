namespace CMP.PipelinesAndSchemas {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"CMP.PipelinesAndSchemas.PO", typeof(global::CMP.PipelinesAndSchemas.PO))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"CMP.PipelinesAndSchemas.Invoice", typeof(global::CMP.PipelinesAndSchemas.Invoice))]
    public sealed class PO2Invoice : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0"" version=""1.0"" xmlns:s0=""http://CMP.PO"" xmlns:ns0=""http://CMP.Invoice"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:PO"" />
  </xsl:template>
  <xsl:template match=""/s0:PO"">
    <ns0:Invoice>
      <xsl:if test=""@Date"">
        <xsl:attribute name=""Date"">
          <xsl:value-of select=""@Date"" />
        </xsl:attribute>
      </xsl:if>
      <BillTo>
        <County>
          <xsl:value-of select=""BillTo/Country/text()"" />
        </County>
        <Name>
          <xsl:value-of select=""BillTo/Name/text()"" />
        </Name>
        <Street>
          <xsl:value-of select=""BillTo/Street/text()"" />
        </Street>
        <City>
          <xsl:value-of select=""BillTo/City/text()"" />
        </City>
        <State>
          <xsl:value-of select=""BillTo/State/text()"" />
        </State>
        <Zip>
          <xsl:value-of select=""BillTo/Zip/text()"" />
        </Zip>
      </BillTo>
      <xsl:for-each select=""Items/Item"">
        <Item>
          <ID>
            <xsl:value-of select=""ID/text()"" />
          </ID>
          <Name>
            <xsl:value-of select=""Name/text()"" />
          </Name>
          <Quantity>
            <xsl:value-of select=""Quantity/text()"" />
          </Quantity>
          <Price>
            <xsl:value-of select=""Price/text()"" />
          </Price>
          <Comment>
            <xsl:value-of select=""Comment/text()"" />
          </Comment>
          <xsl:value-of select=""./text()"" />
        </Item>
      </xsl:for-each>
    </ns0:Invoice>
  </xsl:template>
</xsl:stylesheet>";
        
        private const int _useXSLTransform = 0;
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"CMP.PipelinesAndSchemas.PO";
        
        private const global::CMP.PipelinesAndSchemas.PO _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"CMP.PipelinesAndSchemas.Invoice";
        
        private const global::CMP.PipelinesAndSchemas.Invoice _trgSchemaTypeReference0 = null;
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override int UseXSLTransform {
            get {
                return _useXSLTransform;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"CMP.PipelinesAndSchemas.PO";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"CMP.PipelinesAndSchemas.Invoice";
                return _TrgSchemas;
            }
        }
    }
}
