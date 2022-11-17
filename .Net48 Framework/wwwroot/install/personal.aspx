<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="personal.aspx.cs" inherits="wwwroot.personal" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentpersonal" runat="server">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Personal Information</h4>

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <div class="mb-3">
                Enter your personal information that will be created for your Administrator account down below.
            </div>
            <div class="mb-3">
                <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                <input type="text" id="UserName" runat="server" class="form-control" maxlength="25" clientidmode="Static" />
                <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="PasswordLabel" clientidmode="Static" runat="server" for="Password">Enter a Password:</label>
                <input type="password" runat="server" id="Password" class="form-control" maxlength="21" />
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
                <input type="password" runat="server" id="RePassword" class="form-control" maxlength="21" />
                <asp:CompareValidator ID="RePasswordCompare" runat="server"
                    ControlToValidate="RePassword"
                    ControlToCompare="Password"
                    ErrorMessage="Passwords do not match." />
            </div>
            <div class="mb-3">
                <label id="SecretQuestionLabel" clientidmode="Static" runat="server" for="SecretQuestion">Secret Question:</label>
                <select ID="SecretQuestion" runat="server" class="form-control">
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
            <div class="mb-3">
                <label id="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
                <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
            </div>
            <div class="mb-3">
                <label id="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                <input type="text" id="StreetAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
                <asp:CustomValidator ID="StreetAddressRequired" runat="server" ControlToValidate="StreetAddress"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Street Address is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
                <input type="text" id="City" runat="server" class="form-control" maxlength="50" clientidmode="Static" />
                <asp:CustomValidator ID="CityRequired" runat="server" ControlToValidate="City"
                    ClientValidationFunction="customFormValidator" ErrorMessage="City is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="StateLabel" clientidmode="Static" runat="server" for="Country">State/Province:</label>
                <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
            </div>
            <div class="mb-3">
                <label id="PostalCodeLabel" clientidmode="Static" runat="server" for="PostalCode">Zip/Postal Code:</label>
                <input type="text" id="PostalCode" runat="server" class="form-control" maxlength="10" clientidmode="Static" />
                <asp:CustomValidator ID="PostalCodeRequired" runat="server" ControlToValidate="PostalCode"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Zip/Postal Code is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="GenderLabel" clientidmode="Static" runat="server" for="Gender">Gender:</label>
                <select runat="server" ID="Gender" ClientIDMode="Static" class="form-control">
                    <option value="1">Male</option>
                    <option value="0">Female</option>
                </select>
            </div>
            <div class="mb-3">
                <label id="BirthDateLabel" clientidmode="Static" runat="server" for="BirthDate">Birth Date:</label>
                <input type="text" id="BirthDate" runat="server" class="form-control" maxlength="10" clientidmode="Static" />
                <asp:CustomValidator ID="BirthDateRequired" runat="server" ControlToValidate="BirthDate"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Birth Date is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
                <input type="text" id="PhoneNumber" runat="server" class="form-control" maxlength="30" clientidmode="Static" />
                <asp:CustomValidator ID="PhoneNumberRequired" runat="server" ControlToValidate="PhoneNumber"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Phone Number is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>

            <div class="mb-3" align="center">
                <asp:Button CssClass="btn btn-secondary" ID="BackButton" runat="server" Text="Back" OnClick="BackButton_Click" />
                <asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue" OnClick="ContinueButton_Click" />
            </div>
        </div>
    </div>
</asp:content>