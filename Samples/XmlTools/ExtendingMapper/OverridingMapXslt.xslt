<?xml version="1.0" encoding="UTF-16"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates select="/*[local-name()='Root' and namespace-uri()='http://ExtendingMapper.Source']" />
  </xsl:template>
  <xsl:template match="/*[local-name()='Root' and namespace-uri()='http://ExtendingMapper.Source']">
    <ns0:Root xmlns:ns0="http://ExtendingMapper.Destination">
      <xsl:for-each select="*[local-name()='Child']">
        <Child>
          <xsl:if test="@*[local-name()='Field1']">
            <Record1><xsl:value-of select="@*[local-name()='Field1']" /></Record1>
			<Record2><xsl:value-of select="@*[local-name()='Field1']" /></Record2>
		<xsl:attribute name="Field1">
              <xsl:value-of select="@*[local-name()='Field1']" />
            </xsl:attribute>
			
          </xsl:if>
        </Child>
      </xsl:for-each>
    </ns0:Root>
  </xsl:template>
</xsl:stylesheet>