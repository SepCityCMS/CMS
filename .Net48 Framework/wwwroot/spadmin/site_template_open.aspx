<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_open.aspx.cs" inherits="wwwroot.site_template_open" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <br />
        <div id="TemplateSelection" runat="server">
            Select a Template: <select ID="TemplateID" runat="server" Width="210px" /> <asp:Button ID="OpenButton" runat="server" Text="Open" onclick="OpenButton_Click" />
        </div>
    </asp:Panel>
</asp:content>