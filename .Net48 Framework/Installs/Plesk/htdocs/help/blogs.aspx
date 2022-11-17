<%@ page title="Blogs" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="blogs.aspx.cs" inherits="wwwroot.blogs2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Blogs</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>