<%@ Page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master" 
    CodeBehind="sq_checkout.aspx.cs" Inherits="wwwroot.sq_checkout" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

</asp:content>