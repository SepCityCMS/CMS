<%@ page title="System Diagnostics" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="system_diagnostics.aspx.cs" inherits="wwwroot.system_diagnostics" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <h1>System Diagnostics</h1>

        <div style="margin-left: 30px; margin-top: 20px;">
            <span ID="installErrors" runat="server"></span>
        </div>
    </asp:Panel>
</asp:content>