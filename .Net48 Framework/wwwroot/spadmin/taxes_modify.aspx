<%@ page title="Taxes" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="taxes_modify.aspx.cs" inherits="wwwroot.taxes_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 995;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Tax</h4>
                <input type="hidden" runat="server" ID="TaxID" />
                <div class="mb-3">
                    <label ID="TaxNameLabel" clientidmode="Static" runat="server" for="TaxName">Tax Name:</label>
                    <input type="text" ID="TaxName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="TaxNameRequired" runat="server" ControlToValidate="TaxName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Tax Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PercentageLabel" clientidmode="Static" runat="server" for="Percentage">Percentage:</label>
                    <input type="text" ID="Percentage" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="PercentageRequired" runat="server" ControlToValidate="Percentage"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Percentage is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="StateLabel" clientidmode="Static" runat="server" for="State">State/Province:</label>
                    <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
                </div>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>