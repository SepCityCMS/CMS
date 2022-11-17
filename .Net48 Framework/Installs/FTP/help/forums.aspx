<%@ page title="Forums" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="forums.aspx.cs" inherits="wwwroot.forums2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Forums</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>