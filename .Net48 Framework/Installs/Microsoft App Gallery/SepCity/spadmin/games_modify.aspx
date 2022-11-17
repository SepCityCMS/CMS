<%@ page title="Games" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="games_modify.aspx.cs" inherits="wwwroot.games_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 47;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Edit Game</h4>
                <input type="hidden" runat="server" ID="GameID" />

                <div class="mb-3">
                    <label ID="GameNameLabel" clientidmode="Static" runat="server" for="GameName">Game Name:</label>
                    <input type="text" ID="GameName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="GameNameRequired" runat="server" ControlToValidate="GameName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Game Name is required."
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