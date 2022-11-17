<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="games_play.aspx.cs" inherits="wwwroot.games_play" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <h1 id="GameName" runat="server"></h1>

    <span ID="GameContent" runat="server"></span>

    <br />

    <p align="center">
        <a href="<%= this.GetInstallFolder() %>games.aspx">Go Back</a>
    </p>
</asp:content>