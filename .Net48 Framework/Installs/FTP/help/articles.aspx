<%@ page title="Articles" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="articles.aspx.cs" inherits="wwwroot.articles2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Articles</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>