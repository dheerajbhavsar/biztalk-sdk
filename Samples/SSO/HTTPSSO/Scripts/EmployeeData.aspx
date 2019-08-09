<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Page Language="C#" Debug="true" %>

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
<script language="C#" runat="server">
	
	string user=null;
	DataSet Ds=new DataSet();

	void Page_Load(Object sender, EventArgs e) 
	{
		// Get user name
		user=Context.User.Identity.Name;
		int backslash=user.IndexOf("\\");
		
		// Remove domain name
		if(backslash!=-1)
		{
			user=user.Substring(backslash+1, user.Length-backslash-1);
		}
		
		// Create a connection
		SqlConnection conn=new SqlConnection("SERVER=localhost;DATABASE=Northwind;Integrated Security=SSPI");
		
		// Build the query
		SqlDataAdapter cmd=new SqlDataAdapter("SELECT * from Employees WHERE LastName='"+user+"'", conn);
		
		// Create and fill a data set
		cmd.Fill(Ds);
	}
</script>
<body>

<%	
	// Check if empty
	if(Ds.Tables[0].Rows.Count==0)
	{
%>
		<div style="font:12pt verdana;color:darkred">
			<i><b>
				User (<% =user %>) is unknown to the system. <br>
				Contact your system administrator.
			</i></b>
		</div>
<%
	}
	else
	{
		// Bind fields list to the data set
		data.DataSource=Ds;
		data.DataBind();
	}
%>

<form runat="server">
   <ASP:DataList id="data" runat="server">
      <ItemTemplate>
         <div style="padding:15,15,15,15;font-size:10pt;font-family:Verdana">
            <div style="font:12pt verdana;color:darkred">
               <i><b>Requested user (<%# user %>) information:
               </i></b>
            </div>
            <br>
            <b>First name: </b>
            <%# DataBinder.Eval(Container.DataItem, "FirstName") %><br>
            <b>Last name: </b>
            <%# DataBinder.Eval(Container.DataItem, "LastName") %><br>
            <b>Title: </b>
            <%# DataBinder.Eval(Container.DataItem, "Title")%><br>
            <b>Home phone: </b>
            <%# DataBinder.Eval(Container.DataItem, "HomePhone") %><br>
            <b>Address: </b>
            <%# DataBinder.Eval(Container.DataItem, "Address") %><br>
            <b>Region: </b>
            <%# DataBinder.Eval(Container.DataItem, "Region") %><br>
            <b>Postal code: </b>
            <%# DataBinder.Eval(Container.DataItem, "PostalCode") %><br>
            <b>Country: </b>
            <%# DataBinder.Eval(Container.DataItem, "Country") %><br>
            <p>
         </div>
      </ItemTemplate>
   </ASP:DataList>
  </form>

</body>
</html>
