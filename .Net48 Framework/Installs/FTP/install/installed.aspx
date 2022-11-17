<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="installed.aspx.cs" inherits="wwwroot.installed" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentintro">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Already Installed</h4>

            <div class="mb-3">
                <span ID="InstallText" runat="server" Text="You have already installed SepCity, please delete the install directory from your web server."></span>
            </div>

            <div class="mb-3" align="center">
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue to your Web Site" OnClick="ContinueButton_Click" /></div>
            </div>
        </div>
    </div>
</asp:content>