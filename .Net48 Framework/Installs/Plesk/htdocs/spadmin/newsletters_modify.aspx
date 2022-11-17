<%@ page title="Newsletters" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="newsletters_modify.aspx.cs" inherits="wwwroot.newsletters_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 24;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Newsletter</h4>
                <input type="hidden" runat="server" ID="LetterID" />
                <div class="mb-3">
                    <label ID="LetterNameLabel" clientidmode="Static" runat="server" for="LetterName">Letter Name:</label>
                    <input type="text" ID="LetterName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="LetterNameRequired" runat="server" ControlToValidate="LetterName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Letter Name is required."
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
                <div class="mb-3" runat="server" id="KeyRow">
                    <label ID="AccessKeysLabel" ClientIDMode="Static" runat="server">Keys to be able to join this newsletter:</label>
                    <sep:AccessKeySelection ID="JoinKeys" runat="server" text="|1|,|2|,|3|,|4|" ClientIDMode="Static" />
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" ClientIDMode="Static" runat="server">Portals to show newsletter in:</label>
                    <sep:PortalSelection ID="Portals" runat="server" text="|0|" ClientIDMode="Static" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>