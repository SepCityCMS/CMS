<%@ page title="Portal Pricing Plans" language="C#" autoeventwireup="true" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_pricing_modify.aspx.cs" inherits="wwwroot.spadmin.portals_pricing_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <%
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 60;
            this.Response.Write(cAdminModuleMenu.Render());
        %>

		<div class="col-md-12 pagecontent">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">Add Pricing Plan</h4>
            <input type="hidden" runat="server" id="PlanID" />

            <div class="mb-3">
                <label id="PlanNameLabel" clientidmode="Static" runat="server" for="PlanName">Plan Name:</label>
                <input type="text" id="PlanName" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="PlanNameRequired" runat="server" ControlToValidate="PlanName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Plan Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                <textarea id="Description" runat="server" class="form-control"></textarea>
                <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="OnetimePriceLabel" clientidmode="Static" runat="server" for="OnetimePrice">One-time Price:</label>
                <input type="text" id="OnetimePrice" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="OnetimePriceRequired" runat="server" ControlToValidate="OnetimePrice"
                    ClientValidationFunction="customFormValidator" ErrorMessage="One-time Price is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="RecurringPriceLabel" clientidmode="Static" runat="server" for="RecurringPrice">Recurring Price:</label>
                <input type="text" id="RecurringPrice" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="RecurringPriceRequired" runat="server" ControlToValidate="RecurringPrice"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Recurring Price is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="RecurringCycleLabel" clientidmode="Static" runat="server" for="RecurringCycle">Recurring Cycle:</label>
                <select id="RecurringCycle" runat="server" class="form-control">
                    <option value="1m">1 Month</option>
                    <option value="3m">3 Months</option>
                    <option value="6m">6 Months</option>
                    <option value="1y">1 Year</option>
                </select>
                <asp:CustomValidator ID="RecurringCycleRequired" runat="server" ControlToValidate="RecurringCycle"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Recurring Cycle is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="ModulesLabel" clientidmode="Static" runat="server" for="Modules">Modules to Show Category In:</label>
                <sep:ModuleSelection ID="Modules" runat="server" ModuleType="Portals" ClientIDMode="Static" />
            </div>
            </div>
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>