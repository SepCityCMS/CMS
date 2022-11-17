<%@ page title="Email Templates" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="email_templates_modify.aspx.cs" inherits="wwwroot.email_templates_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 987;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Email Template</h4>
                <input type="hidden" runat="server" ID="TemplateID" />

                <div class="mb-3">
                    <label ID="TemplateNameLabel" clientidmode="Static" runat="server" for="TemplateName">Template Name:</label>
                    <input type="text" ID="TemplateName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="TemplateNameRequired" runat="server" ControlToValidate="TemplateName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Template Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="EmailSubjectLabel" clientidmode="Static" runat="server" for="EmailSubject">Email Subject:</label>
                    <input type="text" ID="EmailSubject" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="EmailSubjectRequired" runat="server" ControlToValidate="EmailSubject"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Subject is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="EmailBodyLabel" clientidmode="Static" runat="server" for="EmailBody">Email Body:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="EmailBody" Width="99%" Height="450" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>