<%@ page title="Forms" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="forms_submissions_view.aspx.cs" inherits="wwwroot.forms_submissions_view" %>

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

                <h4 id="ModifyLegend" runat="server"></h4>
                <input type="hidden" runat="server" ID="SubmissionID" />
                <input type="hidden" runat="server" ID="FormID" />

                <div class="mb-3">
                    <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                    <input type="text" ID="EmailAddress" runat="server"  class="form-control" MaxLength="100" ClientIDMode="Static" />
                    <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>

                <asp:Panel ID="QuestionsPanel" runat="server" ClientIDMode="Static"></asp:Panel>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>