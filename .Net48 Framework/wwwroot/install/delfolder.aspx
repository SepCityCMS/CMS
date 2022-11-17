<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="delfolder.aspx.cs" inherits="wwwroot.delfolder" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentintro">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Delete Install Folder</h4>

            <div class="mb-3">
                <span ID="InstallText" runat="server" Text="For security reasons you must remove the /install/ folder after your website has already been installed."></span>
            </div>

            <div class="mb-3" align="center">
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue to your Web Site" OnClick="ContinueButton_Click" /></div>
            </div>
        </div>
    </div>
</asp:content>