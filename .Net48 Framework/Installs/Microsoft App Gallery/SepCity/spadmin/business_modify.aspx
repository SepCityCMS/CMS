<%@ page title="Business Directory" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="business_modify.aspx.cs" inherits="wwwroot.business_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 20;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Business</h4>
                <input type="hidden" runat="server" ID="BusinessID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="20" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="20" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="BusinessNameLabel" clientidmode="Static" runat="server" for="BusinessName">Business Name:</label>
                    <input type="text" ID="BusinessName" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="BusinessNameRequired" runat="server" ControlToValidate="BusinessName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Business Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ContactEmailLabel" clientidmode="Static" runat="server" for="ContactEmail">Contact Email:</label>
                    <input type="text" ID="ContactEmail" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
                    <input type="text" ID="PhoneNumber" runat="server"  class="form-control" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="FaxNumberLabel" clientidmode="Static" runat="server" for="FaxNumber">Fax Number:</label>
                    <input type="text" ID="FaxNumber" runat="server"  class="form-control" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="SiteURLLabel" clientidmode="Static" runat="server" for="SiteURL">Site URL:</label>
                    <input type="text" ID="SiteURL" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Short Description:</label>
                    <textarea ID="Description" runat="server"  class="form-control"></textarea>
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
                        <label ID="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                        <input type="text" ID="StreetAddress" runat="server"  class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label ID="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
                        <input type="text" ID="City" runat="server"  class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label ID="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
                        <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
                    </div>
                    <div class="mb-3">
                        <label ID="StateLabel" clientidmode="Static" runat="server" for="State">State/Province:</label>
                        <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <label ID="PostalCodeLabel" clientidmode="Static" runat="server" for="PostalCode">Zip/Postal Code:</label>
                        <input type="text" ID="PostalCode" runat="server"  class="form-control" />
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="FullDescriptionLabel" clientidmode="Static" runat="server" for="FullDescription">Full Description:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="FullDescription" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="TwitterLinkLabel" clientidmode="Static" runat="server" for="TwitterLink">Twitter Link:</label>
                    <input type="text" ID="TwitterLink" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="FacebookLinkLabel" clientidmode="Static" runat="server" for="FacebookLink">Facebook Link:</label>
                    <input type="text" ID="FacebookLink" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="LinkedInLinkLabel" clientidmode="Static" runat="server" for="LinkedInLink">LinkedIn Link:</label>
                    <input type="text" ID="LinkedInLink" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="OfficeHoursLabel" clientidmode="Static" runat="server" for="OfficeHours">Office Hours:</label>
                    <input type="text" ID="OfficeHours" runat="server"  class="form-control" />
                </div>
                <div class="mb-3" runat="server" id="GoogleMapRow">
                    <asp:CheckBox ID="IncludeMap" runat="server" Text="Show the Google map to your business address"></asp:CheckBox>
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 20;
                    cCustomFields.FieldUniqueID = BusinessID.Value;
                    if(sUserID != "") {
                        cCustomFields.UserID = sUserID;
                    } else {
                        cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
                    }
                    Response.Write(cCustomFields.Render()); 
                %>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>