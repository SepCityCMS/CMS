<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="signup.aspx.cs" inherits="wwwroot.usersignup" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.BirthDate.ClientID, "false", "true", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="SignupFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Register Now</h4>
        <div class="row FacebookImgRow" id="FacebookRow" runat="server" clientidmode="Static">
            <asp:HyperLink ID="FacebookSignup" runat="server" NavigateUrl="javascript:fbSignupLogin()" CssClass="SignupFacebookImg" ImageUrl="/images/admin/signupFacebookButton.png"></asp:HyperLink>
        </div>
        <div class="row LinkedInImgRow" id="LinkedInRow" runat="server" clientidmode="Static">
            <script type="in/Login"></script>
        </div>
        <input type="hidden" id="FacebookToken" runat="server" clientidmode="Static" />
        <input type="hidden" id="FacebookId" runat="server" clientidmode="Static" />
        <input type="hidden" id="FacebookUser" runat="server" clientidmode="Static" />
        <input type="hidden" id="LinkedInID" runat="server" clientidmode="Static" />
        <span ID="SignupFormText" runat="server"></span>
        <span ID="MembershipSelection" runat="server"></span>
        <span ID="CreditSelection" runat="server"></span>

        <div class="mb-3" id="SiteIdRow" runat="server">
            <label id="SiteIdLabel" clientidmode="Static" runat="server" for="SiteId">Select a Member Group:</label>
            <select runat="server" id="SiteId" clientidmode="Static" class="form-control">
                <option value="">Select a User Site</option>
            </select>
        </div>
        <div class="mb-3" id="UserNameRow" runat="server">
            <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
            <input type="text" id="UserName" runat="server" class="form-control" maxlength="25" clientidmode="Static" />
            <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="PasswordLabel" clientidmode="Static" runat="server" for="Password">Enter a Password:</label>
            <input type="password" runat="server" id="Password" class="form-control" maxlength="21" autocomplete="off" />
            <asp:RegularExpressionValidator ID="PasswordRegularExpression1" runat="server" Display="dynamic"
                ControlToValidate="Password"
                ErrorMessage="Password must contain one of @#$%^&*/!."
                ValidationExpression=".*[@#$%^&*/!].*" />
            <asp:RegularExpressionValidator ID="PasswordRegularExpression2" runat="server" Display="dynamic"
                ControlToValidate="Password"
                ErrorMessage="Password must be between 4-20 characters."
                ValidationExpression="[^\s]{4,20}" />
            <asp:CustomValidator ID="RePasswordRequired" runat="server" ControlToValidate="Password"
                ClientValidationFunction="customFormValidator" ErrorMessage="Password is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="RePasswordLabel" clientidmode="Static" runat="server" for="RePassword">Re-enter a Password:</label>
            <input type="password" runat="server" id="RePassword" class="form-control" maxlength="21" autocomplete="off" />
            <asp:CompareValidator ID="RePasswordCompare" runat="server"
                ControlToValidate="RePassword"
                ControlToCompare="Password"
                ErrorMessage="Passwords do not match." />
        </div>
        <div class="mb-3">
            <label id="SecretQuestionLabel" clientidmode="Static" runat="server" for="SecretQuestion">Secret Question:</label>
            <select id="SecretQuestion" runat="server" class="form-control">
                <option value="Name of your favorite pet?">Name of your favorite pet?</option>
                <option value="In what city were you born?">In what city were you born?</option>
                <option value="What high school did you attend?">What high school did you attend?</option>
                <option value="Your favorite movie?">Your favorite movie?</option>
                <option value="Your mother's maiden name?">Your mother's maiden name?</option>
                <option value="What street did you grow up on?">What street did you grow up on?</option>
                <option value="Make of your first car?">Make of your first car?</option>
                <option value="When is your anniversary?">When is your anniversary?</option>
                <option value="What is your favorite color?">What is your favorite color?</option>
            </select>
            <asp:CustomValidator ID="SecretQuestionRequired" runat="server" ControlToValidate="SecretQuestion"
                ClientValidationFunction="customFormValidator" ErrorMessage="Secret Question is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SecretAnswerLabel" clientidmode="Static" runat="server" for="SecretAnswer">Secret Answer:</label>
            <input type="text" id="SecretAnswer" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="SecretAnswerRequired" runat="server" ControlToValidate="SecretAnswer"
                ClientValidationFunction="customFormValidator" ErrorMessage="Secret Answer is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
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
        <div class="mb-3" id="BirthDateRow" runat="server">
            <label id="BirthDateLabel" clientidmode="Static" runat="server" for="BirthDate">Birth Date:</label>
            <div class="form-group">
                <div class="input-group date" id="datetimepicker1">
                    <input type="text" id="BirthDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
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
            <label id="FriendsLabel" clientidmode="Static" runat="server" for="Friends">Allow your authorization before others can add you to their friend list:</label>
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
        <div class="mb-3" id="ReferralRow" runat="server">
            <label id="ReferralLabel" clientidmode="Static" runat="server" for="Referral">Referral Options:</label>
            <select runat="server" id="ReferralOptions" clientidmode="Static" class="form-control">
                <option value="Username">Referral Username</option>
                <option value="AffiliateID">Referral ID Number</option>
                <option value="EmailAddress">Referral Email Address</option>
            </select>
            <br />
            <input type="text" id="Referral" runat="server" class="form-control" maxlength="25" clientidmode="Static" />
            <asp:CustomValidator ID="ReferralRequired" runat="server" ControlToValidate="Referral"
                ClientValidationFunction="customFormValidator" ErrorMessage="Referral is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 29;
            cCustomFields.FieldUniqueID = "29";
            cCustomFields.UserID = SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>
        <div class="mb-3" id="NewslettersRow" runat="server">
            <label id="NewslettersLabel" clientidmode="Static" runat="server">Select the Newsletters you wish to join:</label>
            <span ID="Newsletters" runat="server"></span>
        </div>
        <div class="mb-3" id="AgreementRow" runat="server">
            <iframe id="SignupAgreementFrame" runat="server" width="500" height="100" src="signup_agreement.aspx"></iframe>
            <br />
            <asp:CheckBox ID="SignupAgreement" runat="server" Text="I Agree to the signup agreement above" />
        </div>
        <div class="mb-3" id="CaptchaRow" runat="server">
            <sep:Captcha ID="Recaptcha1" runat="server" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SignupButton" runat="server" Text="Register Now" OnClick="SignupButton_Click" />
        </div>
    </div>
</asp:content>