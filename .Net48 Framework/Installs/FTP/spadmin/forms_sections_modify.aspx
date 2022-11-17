<%@ page title="Forms" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="forms_sections_modify.aspx.cs" inherits="wwwroot.forms_sections_modify" %>

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

                <h4 id="ModifyLegend" runat="server">Add Section</h4>
                <input type="hidden" runat="server" ID="FormID" />
                <input type="hidden" runat="server" ID="SectionID" />

                <div class="mb-3">
                    <label ID="SectionNameLabel" clientidmode="Static" runat="server" for="SectionName">Section Name:</label>
                    <input type="text" ID="SectionName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="SectionNameRequired" runat="server" ControlToValidate="SectionName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Section Name is required."
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