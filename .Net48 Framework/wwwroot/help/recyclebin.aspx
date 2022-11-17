<%@ page title="Recycle Bin" language="C#" viewstatemode="Enabled" masterpagefile="~/help/page.Master"
    codebehind="recyclebin.aspx.cs" inherits="wwwroot.recyclebin2" %>

<asp:content id="PageHeader" contentplaceholderid="PageHeader" runat="Server">Recycle Bin</asp:content>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Literal ID="HelpContent" runat="server" />
</asp:content>