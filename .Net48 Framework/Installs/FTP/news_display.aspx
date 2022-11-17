<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="news_display.aspx.cs" inherits="wwwroot.news_display" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <span class="ListingTitle">
            <span ID="NewsTitle" runat="server"></span></span>
        <br />
        Posted on
        <span ID="DatePosted" runat="server"></span>
        <br />
        <br />
        <span ID="NewsArticle" runat="server"></span>
        <br />
        <br />
        <sep:ContentImages ID="NewsImages" runat="server" />
    </div>
</asp:content>