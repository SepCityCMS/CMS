<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="businesses_modify.aspx.cs" inherits="wwwroot.businesses_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Post a Business</h4>
        <input type="hidden" runat="server" id="BusinessID" />

        <sep:PostPrice ID="PostPricing" runat="server" ModuleID="20" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="20" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="BusinessNameLabel" clientidmode="Static" runat="server" for="BusinessName">Business Name:</label>
            <input type="text" id="BusinessName" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="BusinessNameRequired" runat="server" ControlToValidate="BusinessName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Business Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="ContactEmailLabel" clientidmode="Static" runat="server" for="ContactEmail">Contact Email:</label>
            <input type="text" id="ContactEmail" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" runat="server" class="form-control" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="FaxNumberLabel" clientidmode="Static" runat="server" for="FaxNumber">Fax Number:</label>
            <input type="text" id="FaxNumber" runat="server" class="form-control" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="SiteURLLabel" clientidmode="Static" runat="server" for="SiteURL">Site URL:</label>
            <input type="text" id="SiteURL" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Short Description:</label>
            <textarea id="Description" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <asp:CheckBox ID="IncludeProfile" runat="server" OnCheckedChanged="IncludeProfile_Clicked" AutoPostBack="True" Text="Show your account address on your business listing"></asp:CheckBox>
        </div>
        <div id="BusinessAddress" runat="server" visible="false">
            <div class="mb-3">
                <label id="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                <input type="text" id="StreetAddress" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
                <input type="text" id="City" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
                <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
            </div>
            <div class="mb-3">
                <label id="StateLabel" clientidmode="Static" runat="server" for="State">State/Province:</label>
                <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
            </div>
            <div class="mb-3">
                <label id="PostalCodeLabel" clientidmode="Static" runat="server" for="PostalCode">Zip/Postal Code:</label>
                <input type="text" id="PostalCode" runat="server" class="form-control" />
            </div>
        </div>
        <div class="mb-3">
            <label id="FullDescriptionLabel" clientidmode="Static" runat="server" for="FullDescription">Full Description:</label>
            <sep:WYSIWYGEditor runat="server" ID="FullDescription" Width="99%" Height="450" />
        </div>
        <div class="mb-3">
            <label id="TwitterLinkLabel" clientidmode="Static" runat="server" for="TwitterLink">Twitter Link:</label>
            <input type="text" id="TwitterLink" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="FacebookLinkLabel" clientidmode="Static" runat="server" for="FacebookLink">Facebook Link:</label>
            <input type="text" id="FacebookLink" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="LinkedInLinkLabel" clientidmode="Static" runat="server" for="LinkedInLink">LinkedIn Link:</label>
            <input type="text" id="LinkedInLink" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="OfficeHoursLabel" clientidmode="Static" runat="server" for="OfficeHours">Office Hours:</label>
            <input type="text" id="OfficeHours" runat="server" class="form-control" />
        </div>
        <div class="mb-3" runat="server" id="GoogleMapRow">
            <asp:CheckBox ID="IncludeMap" runat="server" Text="Show the Google map to your business address"></asp:CheckBox>
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 20;
            cCustomFields.FieldUniqueID = this.BusinessID.Value;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>