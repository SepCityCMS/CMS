﻿<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="site_template.aspx.cs" inherits="wwwroot.site_template1" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Site Template</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>