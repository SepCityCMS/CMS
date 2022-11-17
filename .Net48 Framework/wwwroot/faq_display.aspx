<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="faq_display.aspx.cs" inherits="wwwroot.faq_display" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <h1>
            <span ID="Question" runat="server"></span>
        </h1>

        <sep:RatingStars ID="RatingStars" runat="server" ModuleID="9" />

        <span ID="Answer" runat="server"></span>

        <br />

        <p align="center">
            <sep:RatingGraph ID="RatingGraph" runat="server" ModuleID="9" />
        </p>
    </div>
</asp:content>