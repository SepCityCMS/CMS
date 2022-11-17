<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="celebrities_profile_view.aspx.cs" inherits="wwwroot.celebrities_profile_view" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <input type="hidden" id="UserID" runat="server" />
        <input type="hidden" id="UserName" runat="server" />
        <div class="celebrities">
            <div class="heading_div">
                <hr class="hr4" />
                <span class="about_heading">Celebrities</span>
                <hr class="hr3" />
            </div>
            <div class="clr"></div>
            <div class="celebrities_white">
                <sep:ContentImages ID="ProfilePics" runat="server" />
                <h1>
                    <span ID="FirstName" runat="server"></span>
                    <span ID="LastName" runat="server"></span>
                </h1>
                <div class="mb-3">
                    <span ID="AboutMe" runat="server"></span>
                </div>
                <div class="clr"></div>
            </div>
            <div class="clr"></div>
            <div class="celebrities_white-left">
                <h1>Charities & Foundations Supported.</h1>
                <h4>Following are the charities that are supported by
                    <span ID="FirstName2" runat="server"></span>
                    <span ID="LastName2" runat="server"></span>:</h4>
                <div class="mb-3">
                    <span ID="CharitiesSupported" runat="server"></span>
                </div>
            </div>
            <!--left end -->
            <div class="celebrities_white-right">
                <h1>Causes Supported:</h1>
                <h4>
                    <span ID="FirstName3" runat="server"></span>
                    <span ID="LastName3" runat="server"></span>
                    has supported the following causes:</h4>
                <div class="mb-3">
                    <span ID="CausesSupported" runat="server"></span>
                </div>
            </div>
        </div>
        <asp:Button ID="DonateNow" runat="server" Text="Donate Now" OnClick="DonateButton_Click" />
        <!--content end -->
    </div>
</asp:content>