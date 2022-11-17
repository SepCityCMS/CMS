<%@ page title="FAQ" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="faq.aspx.cs" inherits="wwwroot.faq2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">FAQ</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>