<%@ page title="Activities" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="activities_modify.aspx.cs" inherits="wwwroot.activities_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/filters.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">
        
        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 998;
            if (SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0) cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID"))) cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Activity</h4>
                <input type="hidden" runat="server" ID="ActivityID" />
                <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />
                <div class="mb-3">
                    <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                    <input type="text" name="UserName" id="UserName" runat="server" class="form-control" placeholder="Click to select a user name" onclick="openUserSearch($(this).attr('id'), 'UserID')" />
                    <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ActivityTypeLabel" clientidmode="Static" runat="server" for="ActivityType">Activity Type:</label>
                    <sep:ActivityTypeDropdown ID="ActivityType" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea ID="Description" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>