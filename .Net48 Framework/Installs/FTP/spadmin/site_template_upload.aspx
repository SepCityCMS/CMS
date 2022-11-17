<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_upload.aspx.cs" inherits="wwwroot.site_template_upload" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
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

                <h4 id="ModifyLegend" runat="server">Upload Template</h4>

                <div class="mb-3">
                    <label ID="zipFileNameLabel" clientidmode="Static" runat="server" for="zipFileName">Select a File:</label>
                    <asp:FileUpload ID="zipFileName" runat="server" CssClass="form-control" />
                    <asp:CustomValidator ID="zipFileNameRequired" runat="server" ControlToValidate="zipFileName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Select a file is required."
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