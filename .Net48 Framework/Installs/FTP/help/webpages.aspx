<%@ page title="Web Pages" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="webpages.aspx.cs" inherits="wwwroot.webpages1" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Web Pages</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>