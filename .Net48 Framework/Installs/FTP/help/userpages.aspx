<%@ page title="User Pages" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="userpages.aspx.cs" inherits="wwwroot.userpages2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">User Pages</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>