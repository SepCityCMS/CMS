<%@ page title="Signup" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="signup.aspx.cs" inherits="wwwroot.signup" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 29;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Manage the Signup"></span>
        </h2>
            </div>
    </asp:Panel>
</asp:content>