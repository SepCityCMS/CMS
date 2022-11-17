<%@ page title="Shopping Mall" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="shoppingmall.aspx.cs" inherits="wwwroot.shoppingmall2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Shopping Mall</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>