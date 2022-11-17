<%@ page title="Link Directory" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="linkdirectory_modify.aspx.cs" inherits="wwwroot.linkdirectory_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 19;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Website</h4>
                <input type="hidden" runat="server" ID="LinkID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="19" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="19" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="SiteNameLabel" clientidmode="Static" runat="server" for="SiteName">Site Name:</label>
                    <input type="text" ID="SiteName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="SiteNameRequired" runat="server" ControlToValidate="SiteName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Site Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="SiteURLLabel" clientidmode="Static" runat="server" for="SiteURL">Site URL:</label>
                    <input type="text" ID="SiteURL" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="SiteURLRequired" runat="server" ControlToValidate="SiteURL"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Site URL is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
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