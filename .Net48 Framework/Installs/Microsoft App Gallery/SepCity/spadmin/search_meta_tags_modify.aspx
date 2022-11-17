<%@ page title="Search Optimization" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="search_meta_tags_modify.aspx.cs" inherits="wwwroot.search_meta_tags_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 3;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Meta Tag</h4>
                <input type="hidden" runat="server" ID="TagID" />
                <div class="mb-3">
                    <label ID="PageURLLabel" clientidmode="Static" runat="server" for="PageURL">Page URL:</label>
                    <input type="text" ID="PageURL" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="PageURLRequired" runat="server" ControlToValidate="PageURL"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Page URL is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PageTitleLabel" clientidmode="Static" runat="server" for="PageTitle">Page Title:</label>
                    <input type="text" ID="PageTitle" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="PageTitleRequired" runat="server" ControlToValidate="PageTitle"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Page Title is required."
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