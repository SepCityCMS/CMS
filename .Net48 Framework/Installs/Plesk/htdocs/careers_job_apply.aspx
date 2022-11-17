<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_job_apply.aspx.cs" inherits="wwwroot.careers_job_apply" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Send Resume</h4>
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
            <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" runat="server" class="form-control" maxlength="30" clientidmode="Static" />
            <asp:CustomValidator ID="PhoneNumberRequired" runat="server" ControlToValidate="PhoneNumber"
                ClientValidationFunction="customFormValidator" ErrorMessage="Phone Number is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="UploadResumeLabel" clientidmode="Static" runat="server" for="UploadResume">Upload a Resume:</label>
            <sep:UploadFiles ID="UploadResume" runat="server" FileType="Document" ModuleID="66" Mode="SingleFile" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="ApplyButton" runat="server" Text="Apply Now" OnClick="ApplyButton_Click" />
        </div>
    </div>
</asp:content>