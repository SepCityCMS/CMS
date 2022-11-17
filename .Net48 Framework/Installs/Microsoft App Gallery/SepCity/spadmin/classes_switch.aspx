<%@ page title="Classes/Keys" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="classes_switch.aspx.cs" inherits="wwwroot.classes_switch" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 989;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Switch Users to Another Class</h4>
                <div class="mb-3">
                    <label ID="FromClassIDLabel" clientidmode="Static" runat="server" for="FromClassID">From Class Name:</label>
                    <sep:AccessClassDropdown ID="FromClassID" runat="server" CssClass="form-control" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="ToClassIDLabel" clientidmode="Static" runat="server" for="ToClassID">To Class Name:</label>
                    <sep:AccessClassDropdown ID="ToClassID" runat="server" CssClass="form-control" ClientIDMode="Static" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>