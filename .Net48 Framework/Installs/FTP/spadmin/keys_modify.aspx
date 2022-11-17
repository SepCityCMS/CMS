<%@ page title="Classes/Keys" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="keys_modify.aspx.cs" inherits="wwwroot.keys_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 989;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Access Key</h4>
                <input type="hidden" runat="server" ID="KeyID" />
                <div class="mb-3">
                    <label ID="KeyNameLabel" clientidmode="Static" runat="server" for="KeyName">Key Name:</label>
                    <input type="text" ID="KeyName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="KeyNameRequired" runat="server" ControlToValidate="KeyName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Key Name is required."
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