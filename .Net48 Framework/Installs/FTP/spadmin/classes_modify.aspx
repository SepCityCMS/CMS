<%@ page title="Classes/Keys" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="classes_modify.aspx.cs" inherits="wwwroot.classes_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function openGeneralOptions() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').addClass('btn-info');
            $('#tabPricing a').removeClass('btn-info');
            $('#tabAdvance a').removeClass('btn-info');
            $("#AdvanceOptions").hide();
            $("#PricingPlans").hide();
            $("#GeneralOptions").show();
        }

        function openPricingPlans() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPricing a').addClass('btn-info');
            $('#tabAdvance a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#AdvanceOptions").hide();
            $("#PricingPlans").show();
        }

        function openAdvanceOptions() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPricing a').removeClass('btn-info');
            $('#tabAdvance a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#PricingPlans").hide();
            $("#AdvanceOptions").show();
        }

        $(document)
            .ready(function () {
                $('#tabGeneral a').addClass('btn-info');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 989;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModifyClassDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Access Class</h4>
                <input type="hidden" runat="server" ID="ClassID" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabPricing">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openPricingPlans();">Pricing Plans</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabAdvance">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openAdvanceOptions();">Advance Options</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="GeneralOptions">
                            <div class="mb-3">
                                <label ID="ClassNameLabel" clientidmode="Static" runat="server" for="ClassName">Class Name:</label>
                                <input type="text" ID="ClassName" runat="server"  class="form-control" MaxLength="100" />
                                <asp:CustomValidator ID="ClassNameRequired" runat="server" ControlToValidate="ClassName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Class Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="AccessKeysLabel" ClientIDMode="Static" runat="server">Access Keys to assign users in this class:</label>
                                <sep:AccessKeySelection ID="AccessKeySelection" runat="server" text="|1|,|3|,|4|" />
                            </div>
                            <div class="mb-3">
                                <sep:WYSIWYGEditor Runat="server" ID="Description" Width="99%" Height="450" />
                            </div>
                        </div>

                        <div id="PricingPlans" style="display: none">
                            <strong>Pricing Plan One</strong>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="UnitPrice1">One-time Price</label>
									<input type="text" id="UnitPrice1" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
                            </div>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="RecurringPrice1">Recurring Price</label>
									<input type="text" id="RecurringPrice1" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
								<div class="form-group col-md-3">
									<label for="RecurringCycle1">Recurring Cycle</label>
									<select ID="RecurringCycle1" runat="server" Class="form-control">
										<option value="1m">1 Month</option>
										<option value="3m">3 Months</option>
										<option value="6m">6 Months</option>
										<option value="1y">1 Year</option>
									</select>
								</div>
                            </div>

                            <hr height="1" width="100%">

                            <strong>Pricing Plan Two</strong>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="UnitPrice1">One-time Price</label>
                                    <input type="text" id="UnitPrice2" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
                            </div>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="RecurringPrice1">Recurring Price</label>
                                    <input type="text" id="RecurringPrice2" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
								<div class="form-group col-md-3">
									<label for="RecurringCycle1">Recurring Cycle</label>
									<select ID="RecurringCycle2" runat="server" Class="form-control">
                                        <option value="1m">1 Month</option>
                                        <option value="3m">3 Months</option>
                                        <option value="6m">6 Months</option>
                                        <option value="1y">1 Year</option>
                                    </select>
								</div>
                            </div>

                            <hr height="1" width="100%">

                            <strong>Pricing Plan Three</strong>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="UnitPrice1">One-time Price</label>
                                    <input type="text" id="UnitPrice3" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
                            </div>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="RecurringPrice1">Recurring Price</label>
                                    <input type="text" id="RecurringPrice3" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
								<div class="form-group col-md-3">
									<label for="RecurringCycle1">Recurring Cycle</label>
									<select ID="RecurringCycle3" runat="server" Class="form-control">
                                        <option value="1m">1 Month</option>
                                        <option value="3m">3 Months</option>
                                        <option value="6m">6 Months</option>
                                        <option value="1y">1 Year</option>
                                    </select>
								</div>
                            </div>

                            <hr height="1" width="100%">

                            <strong>Pricing Plan Four</strong>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="UnitPrice1">One-time Price</label>
                                    <input type="text" id="UnitPrice4" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
                            </div>
                            <div class="form-row">
								<div class="form-group col-md-3">
									<label for="RecurringPrice1">Recurring Price</label>
                                    <input type="text" id="RecurringPrice4" runat="server" class="form-control" ClientIDMode="Static" />
								</div>
								<div class="form-group col-md-3">
									<label for="RecurringCycle1">Recurring Cycle</label>
									<select ID="RecurringCycle4" runat="server" Class="form-control">
                                        <option value="1m">1 Month</option>
                                        <option value="3m">3 Months</option>
                                        <option value="6m">6 Months</option>
                                        <option value="1y">1 Year</option>
                                    </select>
								</div>
                            </div>
                        </div>

                        <div id="AdvanceOptions" style="display: none">
                            <div class="mb-3">
                                Mark class as private <asp:CheckBox ID="PrivateClass" runat="server" />
                            </div>
                            
                            <hr height="1" width="100%">

                            <div class="mb-3">
                                If user has not logged in for <input type="text" id="LoggedDays" runat="server" style="width:50px;" class="form-control inline-block" /> days 
                             then switch to class <sep:AccessClassDropdown ID="LoggedSwitchTo" runat="server" showDeleteAccount="true" ClientIDMode="Static" CssClass="form-control inline-block" SelectWidth="300px" />
                            </div>
                            
                            <hr height="1" width="100%">

                            <div class="mb-3">
                                When user is in this class for  <input type="text" id="InDays" runat="server" style="width:50px;" class="form-control inline-block" /> days 
                                then switch to class <sep:AccessClassDropdown ID="InSwitchTo" runat="server" showDeleteAccount="true" ClientIDMode="Static" CssClass="form-control inline-block" SelectWidth="300px" />
                            </div>
                            
                            <hr height="1" width="100%">

                            <div class="mb-3" runat="server" id="PortalsRow">
                                <label ID="PortalLabel" ClientIDMode="Static" runat="server">Portals to Show Class In:</label>
                                <sep:PortalSelection ID="PortalSelection" runat="server" text="|0|" />
                            </div>
                        </div>
                    </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>