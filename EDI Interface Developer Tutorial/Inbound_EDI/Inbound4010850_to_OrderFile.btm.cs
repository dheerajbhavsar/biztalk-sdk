namespace Inbound_EDI {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Inbound_EDI.X12_00401_850", typeof(global::Inbound_EDI.X12_00401_850))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Inbound_EDI.OrderFile", typeof(global::Inbound_EDI.OrderFile))]
    public sealed class Inbound4010850_to_OrderFile : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0 userCSharp"" version=""1.0"" xmlns:ns0=""http://Inbound_EDI.OrderFile"" xmlns:s0=""http://schemas.microsoft.com/BizTalk/EDI/X12/2006"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:X12_00401_850"" />
  </xsl:template>
  <xsl:template match=""/s0:X12_00401_850"">
    <ns0:OrderFile>
      <Order>
        <xsl:for-each select=""s0:PER"">
          <xsl:variable name=""var:v1"" select=""userCSharp:LogicalEq(string(PER01/text()) , &quot;AA&quot;)"" />
          <xsl:variable name=""var:v3"" select=""userCSharp:LogicalEq(string(PER03/text()) , &quot;TE&quot;)"" />
          <Header>
            <PODate>
              <xsl:value-of select=""../s0:BEG/BEG05/text()"" />
            </PODate>
            <PONumber>
              <xsl:value-of select=""../s0:BEG/BEG03/text()"" />
            </PONumber>
            <xsl:if test=""../s0:BEG/BEG08"">
              <CustomerID>
                <xsl:value-of select=""../s0:BEG/BEG08/text()"" />
              </CustomerID>
            </xsl:if>
            <xsl:if test=""string($var:v1)='true'"">
              <xsl:variable name=""var:v2"" select=""PER02/text()"" />
              <CustomerContactName>
                <xsl:value-of select=""$var:v2"" />
              </CustomerContactName>
            </xsl:if>
            <xsl:if test=""string($var:v3)='true'"">
              <xsl:variable name=""var:v4"" select=""PER04/text()"" />
              <CustomerContactPhone>
                <xsl:value-of select=""$var:v4"" />
              </CustomerContactPhone>
            </xsl:if>
          </Header>
        </xsl:for-each>
        <xsl:for-each select=""s0:PO1Loop1"">
          <xsl:variable name=""var:v5"" select=""userCSharp:MathMultiply(string(s0:PO1/PO104/text()) , string(s0:PO1/PO102/text()))"" />
          <LineItems>
            <PONumber>
              <xsl:value-of select=""../s0:BEG/BEG03/text()"" />
            </PONumber>
            <xsl:if test=""s0:PO1/PO107"">
              <ItemOrdered>
                <xsl:value-of select=""s0:PO1/PO107/text()"" />
              </ItemOrdered>
            </xsl:if>
            <xsl:if test=""s0:PO1/PO102"">
              <Quantity>
                <xsl:value-of select=""s0:PO1/PO102/text()"" />
              </Quantity>
            </xsl:if>
            <xsl:if test=""s0:PO1/PO103"">
              <UOM>
                <xsl:value-of select=""s0:PO1/PO103/text()"" />
              </UOM>
            </xsl:if>
            <xsl:if test=""s0:PO1/PO104"">
              <Price>
                <xsl:value-of select=""s0:PO1/PO104/text()"" />
              </Price>
            </xsl:if>
            <ExtendedPrice>
              <xsl:value-of select=""$var:v5"" />
            </ExtendedPrice>
            <xsl:if test=""s0:PIDLoop1/s0:PID_2/PID05"">
              <Description>
                <xsl:value-of select=""s0:PIDLoop1/s0:PID_2/PID05/text()"" />
              </Description>
            </xsl:if>
          </LineItems>
        </xsl:for-each>
      </Order>
    </ns0:OrderFile>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public bool LogicalEq(string val1, string val2)
{
	bool ret = false;
	double d1 = 0;
	double d2 = 0;
	if (IsNumeric(val1, ref d1) && IsNumeric(val2, ref d2))
	{
		ret = d1 == d2;
	}
	else
	{
		ret = String.Compare(val1, val2, StringComparison.Ordinal) == 0;
	}
	return ret;
}


public string MathMultiply(string param0, string param1)
{
	System.Collections.ArrayList listValues = new System.Collections.ArrayList();
	listValues.Add(param0);
	listValues.Add(param1);
	double ret = 1;
	bool first = true;
	foreach (string obj in listValues)
	{
		double d = 0;
		if (IsNumeric(obj, ref d))
		{
			if (first)
			{
				first = false;
				ret = d;
			}
			else
			{
				ret *= d;
			}
		}
		else
		{
			return """";
		}
	}
	return ret.ToString(System.Globalization.CultureInfo.InvariantCulture);
}


public bool IsNumeric(string val)
{
	if (val == null)
	{
		return false;
	}
	double d = 0;
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}

public bool IsNumeric(string val, ref double d)
{
	if (val == null)
	{
		return false;
	}
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}


]]></msxsl:script>
</xsl:stylesheet>";
        
        private const int _useXSLTransform = 0;
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"Inbound_EDI.X12_00401_850";
        
        private const global::Inbound_EDI.X12_00401_850 _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"Inbound_EDI.OrderFile";
        
        private const global::Inbound_EDI.OrderFile _trgSchemaTypeReference0 = null;
        
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
                _SrcSchemas[0] = @"Inbound_EDI.X12_00401_850";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"Inbound_EDI.OrderFile";
                return _TrgSchemas;
            }
        }
    }
}
