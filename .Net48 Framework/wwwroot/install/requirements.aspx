<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="requirements.aspx.cs" inherits="wwwroot.requirements" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentintro">

        <span ID="installErrors" runat="server"></span>

        <p align="center">
            <asp:Button ID="RescanButton" runat="server" CssClass="btn btn-info" Text="Check Requirements" OnClick="RescanButton_Click" />
            <asp:Button ID="ContinueButton" runat="server" CssClass="btn btn-primary" Text="Continue" OnClick="ContinueButton_Click" />
        </p>
    </div>
</asp:content>