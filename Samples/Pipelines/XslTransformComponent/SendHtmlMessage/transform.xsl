<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
    <html>
		<head>
			<meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
			<title>Northwind Inc Purchase Order Confirmation</title>
			<style></style>
		</head>
		<body bgcolor="#FFFFFF">
			<table border="20" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#6699FF" width="100%" id="AutoNumber1" height="336">
				<tr>
					<td width="50%" bgcolor="#6699FF" height="106">
						<i>
							<font size="7" face="Arial Black">Northwind Inc</font>
						</i>
						<font face="Arial"></font>
						<i>
							<b>
								<font face="Arial Black">Powered By</font>
								<font color="#FFFFFF" face="Arial Black"></font>
							</b>
							<font size="5" color="#FFFFFF" face="Arial Black">BizTalk Server 2016</font>
						</i>
					</td>
				</tr>
				<tr>
					<td width="50%" height="223" bgcolor="#FFFFFF">
						<font face="Arial">Dear 
							<b><xsl:value-of select="//Name"/></b>!
							<p>Thank you for shopping with Northwind Inc. We would like to confirm that your order has been received.</p>
							<p>On <xsl:value-of select="//Date"/> you have ordered:</p>
							<p>Item: <b><xsl:value-of select="//Item"/></b></p>
							<p>Quantity: <b><xsl:value-of select="//Quantity"/></b> </p>
							<p>Price: <b>$<xsl:value-of select="//Price"/></b></p>
						</font>
					</td>
				</tr>
				<tr>
					<td width="50%" height="46" bgcolor="#FFFFFF">
						<p align="center">
							<font face="Arial">To check the status of your order go to :
								<a href="HTTP://www.microsoft.com/BizTalk/SDKSamples">HTTP://www.microsoft.com/BizTalk/SDKSamples</a>
							</font>
						</p>
					</td>
				</tr>
				<tr>
					<td width="50%" height="19" bgcolor="#6699FF">
						<p align="center">
							<font face="Arial">
								<i>
									<b>
										<font size="2">Thank You For Ordering From Northwind Inc</font>
									</b>
								</i>
							</font>
						</p>
					</td>
				</tr>
			</table>
		</body>
	</html>
</xsl:template>
</xsl:stylesheet>
