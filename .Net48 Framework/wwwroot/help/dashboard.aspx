<%@ page title="Dashboard" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="dashboard.aspx.cs" inherits="wwwroot.dashboard1" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Dashboard</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>