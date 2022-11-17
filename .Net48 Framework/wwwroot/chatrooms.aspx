<%@ page title="Chat Rooms" language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="chatrooms.aspx.cs" inherits="wwwroot.chatrooms1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="PageText" Runat="server"></span>

    <div ID="ChatWindow" runat="server">
        <span ID="chat_script" runat="server"></span>
    </div>
</asp:content>