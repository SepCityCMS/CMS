<%@ page title="Email Templates" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="email_templates.aspx.cs" inherits="wwwroot.email_templates1" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Email Templates</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>