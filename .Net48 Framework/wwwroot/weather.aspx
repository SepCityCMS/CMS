<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="weather.aspx.cs" inherits="wwwroot.weather" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageContent" runat="server"></span>
    
    <span ID="ShowWeather" runat="server"></span>
</asp:content>