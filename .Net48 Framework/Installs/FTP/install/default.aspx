<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="default.aspx.cs" inherits="wwwroot._default1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentintro">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Introduction</h4>

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <p>Welcome to the SepCity CMS Software installation wizard! This wizard will guide you through the installation process in a user-friendly manner.</p>
            <p>
                <b>The Web Server Requirements Are:</b>
            </p>
            <ul>
                <li>Windows 2003/2008/2012 Server with IIS installed</li>
                <li>Microsoft .NET Framework 4.0</li>
                <li>Microsoft SQL Server 2005/2008/2012 with a "Blank Database" already created</li>
                <li>Access to set security permissions on a few of your website directories</li>
            </ul>

            <h2>License Agreement</h2>
            <div style="border-color: #000000; border-style: solid; border-width: 1px; height: 200px; overflow: auto; width: 100%;" runat="server" id="LicenseAgreement"></div>
            <br />
            <asp:CheckBox ID="LicenceAgree" runat="server" ClientIDMode="Static" />
            I Agree to the licence agreement above.

            <p align="center">
                <div class="mb-3">
                    <asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue" OnClick="ContinueButton_Click" /></div>
            </p>
        </div>
    </div>
</asp:content>