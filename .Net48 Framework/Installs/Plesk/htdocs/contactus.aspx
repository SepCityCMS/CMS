<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="contactus.aspx.cs" inherits="wwwroot.contactus" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ContactDiv" runat="server">

        <input type="hidden" runat="server" id="UploadID" />
        <div class="form-row">
            <div class="form-group col-md-6">
                <label id="YourNameLabel" clientidmode="Static" runat="server" for="YourName">Your Name:</label>
                <input type="text" id="YourName" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
                <asp:CustomValidator ID="YourNameRequired" runat="server" ControlToValidate="YourName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Your Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="form-group col-md-6" runat="server" id="StreetAddressRow">
                <label id="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                <input type="text" id="StreetAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            </div>
            <div class="form-group col-md-6" runat="server" id="AddressRow">
                <label id="AddressLabel" clientidmode="Static" runat="server" for="Address">City/State:</label>
                <input type="text" id="Address" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            </div>
            <div class="form-group col-md-6" runat="server" id="PhoneNumberRow">
                <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
                <input type="text" id="PhoneNumber" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            </div>
            <div class="form-group col-md-6" runat="server" id="FaxNumberRow">
                <label id="FaxNumberLabel" clientidmode="Static" runat="server" for="FaxNumber">Fax Number:</label>
                <input type="text" id="FaxNumber" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            </div>
            <div class="form-group col-md-6" runat="server" id="ContactUploadRow">
                <label id="FileUploadLabel" clientidmode="Static" runat="server" for="FileUpload">Attach file(s) to your message to us:</label>
                <sep:UploadFiles ID="FileUpload" runat="server" ModuleID="4" ClientIDMode="Static" />
            </div>
            <div class="form-group col-md-6">
                <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                <input type="text" id="EmailAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
                <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-12">
                <label id="CommentsLabel" clientidmode="Static" runat="server" for="Comments">Comments:</label>
                <textarea id="Comments" runat="server" class="form-control"></textarea>
                <asp:CustomValidator ID="CommentsRequired" runat="server" ControlToValidate="Comments"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Comments is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
        </div>
        <div class="form-row" id="CaptchaRow" runat="server">
            <div class="form-group col-md-6">
                <sep:Captcha ID="Recaptcha1" runat="server" />
            </div>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SendButton" runat="server" onserverclick="SendButton_Click"><i class="fa fa-envelope-o"></i> Send Email</button>
        </div>
    </div>
</asp:content>