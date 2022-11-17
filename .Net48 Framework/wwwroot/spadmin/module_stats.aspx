<%@ page title="Module Stats" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="module_stats.aspx.cs" inherits="wwwroot.module_stats" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 981;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Module Stats"></span>
        </h2>

        <span ID="ModuleStats" runat="server"></span>
            </div>
    </asp:Panel>
</asp:content>