<%@ page title="Content Rotator" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="contentrotator.aspx.cs" inherits="wwwroot.contentrotator1" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Content Rotator</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>