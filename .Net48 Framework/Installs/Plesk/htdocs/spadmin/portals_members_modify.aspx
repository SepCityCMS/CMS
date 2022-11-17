<%@ page title="Edit Member" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_members_modify.aspx.cs" inherits="wwwroot.portals_members_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/filters.js" type="text/javascript"></script>
    <script type="text/javascript">
        skipRestyling = true;

        function openGeneralInfo() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').addClass('btn-info');
            $('#tabContact a').removeClass('btn-info');
            $('#tabOther a').removeClass('btn-info');
            $("#ContactInfo").hide();
            $("#MoreInfo").hide();
            $("#GeneralInfo").show();
            restyleFormElements('#GeneralInfo');
        }

        function openContactInfo() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabContact a').addClass('btn-info');
            $('#tabOther a').removeClass('btn-info');
            $("#GeneralInfo").hide();
            $("#MoreInfo").hide();
            $("#ContactInfo").show();
            restyleFormElements('#ContactInfo');
        }

        function openMoreInfo() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabContact a').removeClass('btn-info');
            $('#tabOther a').addClass('btn-info');
            $("#GeneralInfo").hide();
            $("#ContactInfo").hide();
            $("#MoreInfo").show();
            restyleFormElements('#MoreInfo');
        }

        $(document)
            .ready(function () {
                restyleFormElements('#GeneralInfo');
                $('#tabGeneral a').addClass('btn-info');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 60;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AffiliateID"))) cAdminModuleMenu.ModuleID = 39;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Member</h4>
                <input type="hidden" runat="server" ID="UserID" />
                <input type="hidden" runat="server" ID="ReferralID" ClientIDMode="Static" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralInfo();">General Info</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabContact">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openContactInfo();">Contact Info</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabOther">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openMoreInfo();">More Info</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="GeneralInfo">
                            <div class="mb-3">
                                <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                                <input type="text" ID="UserName" runat="server"  class="form-control" MaxLength="25" ClientIDMode="Static" />
                                <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="PasswordLabel" clientidmode="Static" runat="server" for="Password">Enter a Password:</label>
                                <input type="text" ID="Password" runat="server" TextMode="Password" AutoComplete="off" MaxLength="12"  class="form-control" />
                                <asp:CustomValidator ID="RePasswordRequired" runat="server" ControlToValidate="Password"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Password is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="RePasswordLabel" clientidmode="Static" runat="server" for="RePassword">Re-enter a Password:</label>
                                <input type="text" ID="RePassword" runat="server" TextMode="Password" AutoComplete="off" MaxLength="12"  class="form-control" />
                                <asp:CompareValidator ID="RePasswordCompare" runat="server"
                                                      controltovalidate="RePassword"
                                                      controltocompare="Password"
                                                      errormessage="Passwords do not match." />
                            </div>
                            <div class="mb-3">
                                <label ID="FirstNameLabel" clientidmode="Static" runat="server" for="FirstName">First Name:</label>
                                <input type="text" ID="FirstName" runat="server"  class="form-control" MaxLength="50" ClientIDMode="Static" />
                                <asp:CustomValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="First Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="LastNameLabel" clientidmode="Static" runat="server" for="LastName">Last Name:</label>
                                <input type="text" ID="LastName" runat="server"  class="form-control" MaxLength="50" ClientIDMode="Static" />
                                <asp:CustomValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Last Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                                <input type="text" ID="EmailAddress" runat="server"  class="form-control" MaxLength="100" ClientIDMode="Static" />
                                <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                                <input type="text" ID="StreetAddress" runat="server"  class="form-control" MaxLength="100" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="CityLabel" clientidmode="Static" runat="server" for="City">City / Town:</label>
                                <input type="text" ID="City" runat="server"  class="form-control" MaxLength="50" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="StateLabel" clientidmode="Static" runat="server" for="State">State / Province:</label>
                                <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="PostalCodeLabel" clientidmode="Static" runat="server" for="PostalCode">Zip/Postal Code:</label>
                                <input type="text" ID="PostalCode" runat="server"  class="form-control" MaxLength="10" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
                                <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
                            </div>
                        </div>
                        <div id="ContactInfo" style="display: none">
                            <div class="mb-3">
                                <label ID="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
                                <input type="text" ID="PhoneNumber" runat="server"  class="form-control" MaxLength="30" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="PayPalEmailLabel" clientidmode="Static" runat="server" for="PayPalEmail">PayPal Email Address (In case you intent buyers to pay you with credit card):</label>
                                <input type="text" ID="PayPalEmail" runat="server"  class="form-control" MaxLength="100" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div id="MoreInfo" style="display: none">
                            <div class="mb-3">
                                <label ID="BirthDateLabel" clientidmode="Static" runat="server" for="BirthDate">Birth Date:</label>
                                <input type="text" ID="BirthDate" runat="server"  class="form-control" MaxLength="10" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="GenderLabel" clientidmode="Static" runat="server" for="Gender">Gender:</label>
                                <select runat="server" ID="Gender" ClientIDMode="Static" class="form-control">
                                    <option value="1">Male</option>
                                    <option value="0">Female</option>
                                </select>
                            </div>
                            <div class="mb-3">
                                <label ID="ReferralLabel" clientidmode="Static" runat="server" for="Referral">Referral (User Name):</label>
                                <input type="text" name="Referral" id="Referral" runat="server" class="form-control" placeholder="Click to select a user name" onclick="openUserSearch($(this).attr('id'), 'ReferralID')" />
                            </div>
                            <div class="mb-3">
                                <label ID="WebsiteURLLabel" clientidmode="Static" runat="server" for="WebsiteURL">Website URL:</label>
                                <input type="text" ID="WebsiteURL" runat="server"  class="form-control" MaxLength="150" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3" id="FriendsRow" runat="server">
                                <label ID="FriendsLabel" clientidmode="Static" runat="server" for="Friends">Require authorization before others can add this user to their friend list:</label>
                                <select runat="server" ID="Friends" ClientIDMode="Static" class="form-control">
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                            <% 
                                var cCustomFields = new SepCityControls.CustomFields();
                                cCustomFields.ModuleID = 29;
                                cCustomFields.FieldUniqueID = "29";
                                cCustomFields.UserID = SepCommon.SepCore.Request.Item("UserID");
                                Response.Write(cCustomFields.Render()); 
                            %>
                            <div class="mb-3" id="NewslettersRow" runat="server">
                                <label ID="NewslettersLabel" ClientIDMode="Static" runat="server">Select the Newsletters you wish to join:</label>
                                <span ID="Newsletters" runat="server"></span>
                            </div>
                            <div class="mb-3">
                                <label ID="StatusLabel" clientidmode="Static" runat="server" for="Gender">User Status:</label>
                                <select runat="server" ID="Status" ClientIDMode="Static" class="form-control">
                                    <option value="1">Active</option>
                                    <option value="0">Not Active</option>
                                </select>
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