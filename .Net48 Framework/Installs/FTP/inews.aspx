<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="inews.aspx.cs" inherits="wwwroot.inews" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageContent" runat="server"></span>

    <span ID="ShowNews" runat="server"></span>
</asp:content>