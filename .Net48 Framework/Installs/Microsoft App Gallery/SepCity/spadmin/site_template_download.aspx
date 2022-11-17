<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_download.aspx.cs" inherits="wwwroot.site_template_download" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span class="successNotification" id="successNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>
    </asp:Panel>
</asp:content>