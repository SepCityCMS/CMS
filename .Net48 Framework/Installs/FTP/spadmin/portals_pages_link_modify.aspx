<%@ page title="Edit External Link" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_pages_link_modify.aspx.cs" inherits="wwwroot.portals_pages_link_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 999;
            if (SepCommon.SepFunctions.Get_Portal_ID() == -1) cAdminModuleMenu.ModuleID = 60;
            cAdminModuleMenu.MenuID = SepCommon.SepCore.Request.Item("MenuID");
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Add External Link</h4>
                <input type="hidden" runat="server" ID="LinkID" />
                <div class="mb-3">
                    <label ID="MenuIDLabel" clientidmode="Static" runat="server" for="MenuID">Select a Menu:</label>
                    <sep:MenuDropdown ID="MenuID" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="LinkTextLabel" clientidmode="Static" runat="server" for="LinkText">Link Text:</label>
                    <input type="text" ID="LinkText" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="LinkTextRequired" runat="server" ControlToValidate="LinkText"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Link Text is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PageURLLabel" clientidmode="Static" runat="server" for="PageURL">Page URL:</label>
                    <input type="text" ID="PageURL" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="PageURLRequired" runat="server" ControlToValidate="PageURL"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Page URL is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="TargetWindowLabel" clientidmode="Static" runat="server" for="TargetWindowLabel">Target Window:</label>
                    <input type="text" ID="TargetWindow" runat="server"  class="form-control" MaxLength="100" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>