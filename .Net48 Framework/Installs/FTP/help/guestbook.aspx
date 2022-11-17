<%@ page title="Guestbook" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="guestbook.aspx.cs" inherits="wwwroot.guestbook2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Guestbook</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>