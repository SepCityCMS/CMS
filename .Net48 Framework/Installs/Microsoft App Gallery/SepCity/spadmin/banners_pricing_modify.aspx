<%@ page title="Advertising" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="advertising_pricing_modify.aspx.cs" inherits="wwwroot.advertising_pricing_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 2;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Price</h4>
                <input type="hidden" runat="server" ID="PriceID" />
                <div class="mb-3">
                    <label ID="PlanNameLabel" clientidmode="Static" runat="server" for="PlanName">Plan Name:</label>
                    <input type="text" ID="PlanName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="PlanNameRequired" runat="server" ControlToValidate="PlanName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Plan Name is required."
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
                <div class="mb-3">
                    <label ID="OnetimePriceLabel" clientidmode="Static" runat="server" for="OnetimePrice">One-time Price:</label>
                    <input type="text" ID="OnetimePrice" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="OnetimePriceRequired" runat="server" ControlToValidate="OnetimePrice"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="One-time Price is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="RecurringPriceLabel" clientidmode="Static" runat="server" for="RecurringPrice">Recurring Price:</label>
                    <input type="text" ID="RecurringPrice" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="RecurringPriceRequired" runat="server" ControlToValidate="RecurringPrice"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Recurring Price is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="RecurringCycleLabel" clientidmode="Static" runat="server" for="RecurringCycle">Recurring Cycle:</label>
                    <select ID="RecurringCycle" runat="server" class="form-control">
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
                    <label ID="MaximumClicksLabel" clientidmode="Static" runat="server" for="MaximumClicks">Maximum Clicks (Enter "-1" for Unlimited):</label>
                    <input type="text" ID="MaximumClicks" runat="server"  class="form-control" MaxLength="100" Text="-1" />
                    <asp:CustomValidator ID="MaximumClicksRequired" runat="server" ControlToValidate="MaximumClicks"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Maximum Clicks is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="MaximumExposuresLabel" clientidmode="Static" runat="server" for="MaximumExposures">Maximum Exposures (Enter "-1" for Unlimited):</label>
                    <input type="text" ID="MaximumExposures" runat="server"  class="form-control" MaxLength="100" Text="-1" />
                    <asp:CustomValidator ID="MaximumExposuresRequired" runat="server" ControlToValidate="MaximumExposures"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Maximum Exposures is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ZonesLabel" clientidmode="Static" runat="server" for="Zones">Target Pricing to Zones:</label>
                    <sep:ZoneSelection ID="Zones" runat="server" ClientIDMode="Static" ModuleID="2" />
                </div>
                <div class="mb-3" runat="server" id="CategoriesRow">
                    <label ID="CategoriesLabel" clientidmode="Static" runat="server" for="Categories">Target Pricing to Categories:</label>
                    <sep:CategorySelection ID="Categories" runat="server" ClientIDMode="Static" />
                </div>
                <div class="mb-3" runat="server" id="WebPagesRow">
                    <label ID="PagesLabel" clientidmode="Static" runat="server" for="Pages">Target Pricing to Pages:</label>
                    <sep:PageSelection ID="Pages" runat="server" ClientIDMode="Static" text="|-1|" />
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalsLabel" clientidmode="Static" runat="server" for="Portals">Target Pricing to Portals:</label>
                    <sep:PortalSelection ID="Portals" runat="server" ClientIDMode="Static" text="|0|" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>