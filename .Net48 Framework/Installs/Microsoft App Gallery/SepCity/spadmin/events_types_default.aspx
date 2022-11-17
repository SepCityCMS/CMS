<%@ page title="Default Event Types" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="events_types_default.aspx.cs" inherits="wwwroot.events_types_default" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 46;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">
            <h2>
                <span ID="PageHeader" runat="server">Add Default Event Types</span>
            </h2>

            <span id="failureNotification">
                <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>
        </div>
    </asp:Panel>
</asp:content>