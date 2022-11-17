<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="vouchers_purchase.aspx.cs" inherits="wwwroot.vouchers_purchase" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">

        <div style="height: 200px; overflow: auto; width: 100%;">
            <span ID="SignupAgreement" runat="server"></span>
        </div>
        <p align="center">
            <asp:CheckBox ID="Agreement" runat="server" Text="I accept the agreement that is stated above." />
        </p>

        <p align="center">
            <asp:Button ID="SaveButton" runat="server" Text="Continue" OnClick="SaveButton_Click" />
        </p>
    </div>
</asp:content>