<%@ page title="Site Statistics" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="site_stats.aspx.cs" inherits="wwwroot.site_stats1" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Site Statistics</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>