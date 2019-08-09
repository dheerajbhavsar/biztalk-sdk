<%@ Page language="c#" Inherits="Microsoft.Samples.BizTalk.RequestResponse.RequestResponseWebForm" CodeFile="default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title>RequestResponse</title>
        <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" Content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    </HEAD>
    <body>
        <H1>Please enter your purchase order details below:</H1>
        <form runat="server">
            Item to purchase:&nbsp;
            <asp:DropDownList id="item" runat="server">
                <asp:ListItem Value="A">A</asp:ListItem>
                <asp:ListItem Value="B">B</asp:ListItem>
                <asp:ListItem Value="C">C</asp:ListItem>
                <asp:ListItem Value="D">D</asp:ListItem>
            </asp:DropDownList>
            <br>
            Estimated Price:&nbsp; $<asp:TextBox id="estimatedPrice" runat="server" MaxLength="5" Width="72px" />
            <br>
            <br>
            <asp:Button id="placeOrder" Text="Place Order" Runat="server" onclick="placeOrder_Click" />
            <br>
            <br>
            <br>
            <asp:Label id="status" runat="server">
        Status:</asp:Label>
        </form>
    </body>
</HTML>
