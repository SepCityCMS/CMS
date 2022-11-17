<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="account_edit.aspx.cs" inherits="wwwroot.account_edit" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="<%= this.GetInstallFolder(true) %>js/country.js" type="text/javascript"></script>
    <script type="text/javascript">
        function changePassword() {
            if (document.getElementById("PasswordRow1").style.display == "none") {
                document.getElementById("PasswordRow1").style.display = "";
                document.getElementById("PasswordRow2").style.display = "";
            } else {
                document.getElementById("PasswordRow1").style.display = "none";
                document.getElementById("PasswordRow2").style.display = "none";
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="fb-root"></div>

    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="SignupFormDiv" runat="server">
        <input type="hidden" id="Facebook_Token" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_Id" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_User" runat="server" clientidmode="Static" />
        <input type="hidden" id="BirthDate" runat="server" clientidmode="Static" />

        <h4 id="ModifyLegend" runat="server">Edit Account</h4>
        <span ID="UserPoints" runat="server"></span>
        <div class="row FacebookImgRow" id="FacebookRow" runat="server">
            <label id="FacebookLoginLabel" clientidmode="Static" runat="server">Login below with facebook to associate your account with your FaceBook Account:</label>
            <br />
            <a onclick="fbAssociate();" href="javascript:void(0)" class="fb_button fb_button_medium">
                <span class="fb_button_text">Sign in with Facebook</span></a>
        </div>
        <div class="mb-3">
            <a href="javascript:changePassword()">Change Password</a>
        </div>
        <div class="mb-3" id="PasswordRow1" style="display: none">
            <label id="PasswordLabel" clientidmode="Static" runat="server" for="Password">Enter a Password:</label>
            <input type="password" runat="server" id="Password" class="form-control" maxlength="21" />
        </div>
        <div class="mb-3" id="PasswordRow2" style="display: none">
            <label id="RePasswordLabel" clientidmode="Static" runat="server" for="RePassword">Re-enter a Password:</label>
            <input type="password" runat="server" id="RePassword" class="form-control" maxlength="21" />
            <asp:CompareValidator ID="RePasswordCompare" runat="server"
                ControlToValidate="RePassword"
                ControlToCompare="Password"
                ErrorMessage="Passwords do not match." />
        </div>
        <div class="mb-3">
            <label id="EmailAddressLabel" runat="server" clientidmode="static" for="EmailAddress">Email Address:</label>
            <input type="text" id="EmailAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="FirstNameLabel" clientidmode="Static" runat="server" for="FirstName">First Name:</label>
            <input type="text" id="FirstName" runat="server" class="form-control" maxlength="50" clientidmode="Static" />
            <asp:CustomValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName"
                ClientValidationFunction="customFormValidator" ErrorMessage="First Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="LastNameLabel" clientidmode="Static" runat="server" for="LastName">Last Name:</label>
            <input type="text" id="LastName" runat="server" class="form-control" maxlength="50" clientidmode="Static" />
            <asp:CustomValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Last Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="CountryRow" runat="server">
            <label id="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
            <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
        </div>
        <div class="mb-3" id="StreetAddressRow" runat="server">
            <label id="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
            <input type="text" id="StreetAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="StreetAddressRequired" runat="server" ControlToValidate="StreetAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Street Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="CityRow" runat="server">
            <label id="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
            <input type="text" id="City" runat="server" class="form-control" maxlength="50" clientidmode="Static" />
            <asp:CustomValidator ID="CityRequired" runat="server" ControlToValidate="City"
                ClientValidationFunction="customFormValidator" ErrorMessage="City is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="StateRow" runat="server">
            <label id="StateLabel" clientidmode="Static" runat="server" for="Country">State/Province:</label>
            <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
        </div>
        <div class="mb-3" id="PostalCodeRow" runat="server">
            <label id="PostalCodeLabel" clientidmode="Static" runat="server" for="PostalCode">Zip/Postal Code:</label>
            <input type="text" id="PostalCode" runat="server" class="form-control" maxlength="10" clientidmode="Static" />
            <asp:CustomValidator ID="PostalCodeRequired" runat="server" ControlToValidate="PostalCode"
                ClientValidationFunction="customFormValidator" ErrorMessage="Zip/Postal Code is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="GenderRow" runat="server">
            <label id="GenderLabel" clientidmode="Static" runat="server" for="Gender">Gender:</label>
            <select runat="server" id="Gender" clientidmode="Static" class="form-control">
                <option value="1">Male</option>
                <option value="0">Female</option>
            </select>
        </div>
        <div class="mb-3" id="PhoneNumberRow" runat="server">
            <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" runat="server" class="form-control" maxlength="30" clientidmode="Static" />
            <asp:CustomValidator ID="PhoneNumberRequired" runat="server" ControlToValidate="PhoneNumber"
                ClientValidationFunction="customFormValidator" ErrorMessage="Phone Number is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="FriendsRow" runat="server">
            <label id="FriendsLabel" clientidmode="Static" runat="server" for="Friends">Friends:</label>
            <select runat="server" id="Friends" clientidmode="Static" class="form-control">
                <option value="Yes">Yes</option>
                <option value="No">No</option>
            </select>
        </div>
        <div class="mb-3" id="PayPalRow" runat="server">
            <label id="PayPalLabel" clientidmode="Static" runat="server" for="PayPalEmail">PayPal Email Address (In case you intent buyers to pay you with credit card):</label>
            <input type="text" id="PayPalEmail" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="PayPalRequired" runat="server" ControlToValidate="PayPalEmail"
                ClientValidationFunction="customFormValidator" ErrorMessage="PayPal Email Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 29;
            cCustomFields.FieldUniqueID = "29";
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>
        <div class="mb-3" id="NewslettersRow" runat="server">
            <label id="NewslettersLabel" clientidmode="Static" runat="server">Select the Newsletters you wish to join:</label>
            <span ID="Newsletters" runat="server"></span>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save Changes</button>
        </div>
    </div>
</asp:content>