<%@ page title="Forms" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="forms_modify.aspx.cs" inherits="wwwroot.forms_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 13;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Form</h4>
                <input type="hidden" runat="server" ID="FormID" />

                <div class="mb-3">
                    <label ID="FormNameLabel" clientidmode="Static" runat="server" for="FormName">Form Name:</label>
                    <input type="text" ID="FormName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="FormNameRequired" runat="server" ControlToValidate="FormName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Form Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="PortalsRow" runat="server">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" />
                </div>
                <div class="mb-3">
                    <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email address(es) to send form submissions to (Seperate by semicolons):</label>
                    <input type="text" ID="EmailAddress" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="EmailSubjectRequired" runat="server" ControlToValidate="EmailAddress"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PageBodyLabel" clientidmode="Static" runat="server" for="PageBody">Page Description:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="PageBody" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="CompletionURLLabel" clientidmode="Static" runat="server" for="CompletionURL">Completion URL:</label>
                    <input type="text" ID="CompletionURL" runat="server"  class="form-control" MaxLength="100" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>