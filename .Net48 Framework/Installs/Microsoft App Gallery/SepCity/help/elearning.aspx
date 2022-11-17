<%@ page title="E-Learning" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="elearning.aspx.cs" inherits="wwwroot.elearning2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">E-Learning</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>