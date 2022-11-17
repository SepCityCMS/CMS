<%@ page title="Real Estate" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="realestate_agents_modify.aspx.cs" inherits="wwwroot.realestate_agents_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 32;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Agent</h4>
                <input type="hidden" runat="server" ID="AgentID" />

                <div class="mb-3">
                    <label ID="BrokerLabel" clientidmode="Static" runat="server" for="Broker">Broker:</label>
                    <select ID="Broker" runat="server" class="form-control">
                    </select>
                    <asp:CustomValidator ID="BrokerRequired" runat="server" ControlToValidate="Broker"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Broker is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                    <input type="text" ID="UserName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
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