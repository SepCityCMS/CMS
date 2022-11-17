<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_modify.aspx.cs" inherits="wwwroot.site_template_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/site_template.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 984;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="TemplateForm" runat="server">

                <h4 id="ModifyLegend" runat="server">Template Options</h4>
                <input type="hidden" runat="server" ID="TemplateID" />

                <div class="mb-3">
                    <label ID="TemplateNameLabel" clientidmode="Static" runat="server" for="TemplateName">Template Name:</label>
                    <input type="text" ID="TemplateName" runat="server"  class="form-control" ReadOnly="true" MaxLength="100" />
                    <asp:CustomValidator ID="TemplateNameRequired" runat="server" ControlToValidate="TemplateName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Template Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="AccessKeysRow" runat="server">
                    <label ID="AccessKeysLabel" clientidmode="Static" runat="server" for="AccessKeys">Access Keys to use this template in the portals and user pages module:</label>
                    <sep:AccessKeySelection ID="AccessKeys" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
                </div>
                <div class="mb-3" id="UserPagesRow" runat="server">
                    <label ID="EnableUserPagesLabel" clientidmode="Static" runat="server" for="EnableUserPages">Allow template to be used in User Pages:</label>
                    <select ID="EnableUserPages" runat="server" class="form-control">
                        <option value="1">Yes</option>
                        <option value="0">No</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea ID="Description" runat="server"  class="form-control"></textarea>
                </div>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>