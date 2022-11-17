<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="horoscopes.aspx.cs" inherits="wwwroot.horoscopes" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageContent" runat="server"></span>

    <span ID="ShowHoroscopes" runat="server"></span>
</asp:content>