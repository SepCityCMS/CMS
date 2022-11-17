<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="classifieds_buy.aspx.cs" inherits="wwwroot.classifieds_buy" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <b>Total Price:</b>
        <span ID="TotalAdPrice" runat="server"></span>
        <br />
        <b>Note:</b> Shipping charges may apply depending on the seller and item you are ordering
        <br />
        <br />
        <asp:Button ID="OrderButton" runat="server" Text="Proceed with the order" OnClick="OrderButton_Click" />
    </div>
</asp:content>