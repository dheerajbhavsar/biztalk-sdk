<%@ Page Language="C#"  Debug="true" %>

<!---------------------------------------------------------------------
-- This file is part of the Microsoft BizTalk Server 2016 SDK
--
-- Copyright (c) Microsoft Corporation. All rights reserved.
--
-- This source code is intended only as a supplement to Microsoft BizTalk
-- Server 2016 release and/or on-line documentation. See these other
-- materials for detailed information regarding Microsoft code samples.
--
-- THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
-- KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
-- IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
-- PURPOSE.
----------------------------------------------------------------------->

<html>
<head>
	<title>Enterprise Single Sign On Test</title>
</head>
<script language="C#" runat="server">

	public void Page_Load(object sender, System.EventArgs e)
	{
		try
		{
			Response.Write("Test Passed. Welcome '" + this.Context.User.Identity.Name.ToString() + "'");
		}

		catch( Exception ex )
		{
			Response.Write(ex.Message);
		}
	
	}

</script>
<body>
<form runat="server">


</form>

</body>
</html>
