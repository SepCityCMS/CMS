<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_property_modify.aspx.cs" inherits="wwwroot.realestate_property_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Property</h4>
        <input type="hidden" runat="server" id="PropertyID" />
        <input type="hidden" runat="server" id="AgentID" />

        <div class="mb-3">
            <label id="PropertyTitleLabel" clientidmode="Static" runat="server" for="PropertyTitle">Title:</label>
            <input type="text" id="PropertyTitle" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="TitleRequired" runat="server" ControlToValidate="PropertyTitle"
                ClientValidationFunction="customFormValidator" ErrorMessage="Title is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
            <input type="text" id="Price" runat="server" ckass="form-control inline-block" width="59%" maxlength="30" />
            <select id="RecurringCycle" runat="server" class="form-control inline-block" width="20%">
                <option value="1m">Monthly</option>
                <option value="3m">3 Months</option>
                <option value="6m">6 Months</option>
                <option value="1y">Yearly</option>
            </select>
            <asp:DropDownList ID="ForSale" runat="server" CssClass="form-control inline-block" AutoPostBack="True" clientidmode="Static" EnableViewState="True" width="20%" OnSelectedIndexChanged="ForSale_SelectedIndex">
                <asp:ListItem Text="For Sale" Value="1" />
                <asp:ListItem Text="For Rent" Value="0" />
            </asp:DropDownList>
        </div>
        <div class="mb-3" id="MLSNumberRow" runat="server">
            <label id="MLSNumberLabel" clientidmode="Static" runat="server" for="MLSNumber">MLS Number:</label>
            <input type="text" id="MLSNumber" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <sep:UploadFiles ID="Pictures" runat="server" ModuleID="32" Mode="MultipleFiles" FileType="Images" />
        </div>
        <div class="mb-3">
            <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
            <sep:WYSIWYGEditor ID="Description" runat="server" />
        </div>
        <div class="mb-3">
            <label id="PropertyTypeLabel" clientidmode="Static" runat="server" for="PropertyType">Property Type:</label>
            <select id="PropertyType" runat="server" class="form-control">
                <option value="1">Apartment</option>
                <option value="2">Condo</option>
                <option value="3">House</option>
                <option value="4">Land/Lot</option>
                <option value="5">Town House</option>
                <option value="11">Commercial Land/Lot</option>
                <option value="12">Commercial Building (Buy)</option>
                <option value="13">Commercial Building (Rent)</option>
                <option value="15">Furnished Rooms (Rent)</option>
            </select>
        </div>
        <div class="mb-3">
            <label id="StatusLabel" clientidmode="Static" runat="server" for="Status">Status:</label>
            <select id="Status" runat="server" class="form-control">
                <option value="1">Available</option>
                <option value="2">Not Available</option>
                <option value="3">Available (Show on Site)</option>
            </select>
        </div>
        <div class="mb-3" id="CountryRow" runat="server">
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
        <div class="mb-3" id="StateRow" runat="server">
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
        <div class="mb-3" id="CountyRow" runat="server">
            <label id="CountyLabel" clientidmode="Static" runat="server" for="County">County:</label>
            <input type="text" id="County" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="YearBuiltRow" runat="server">
            <label id="YearBuiltLabel" clientidmode="Static" runat="server" for="YearBuilt">Year Built:</label>
            <input type="text" id="YearBuilt" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <label id="NumBedroomsLabel" clientidmode="Static" runat="server" for="NumBedrooms"># of Bedrooms:</label>
            <input type="text" id="NumBedrooms" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <label id="NumBathroomsLabel" clientidmode="Static" runat="server" for="NumBathrooms"># of Bathrooms:</label>
            <input type="text" id="NumBathrooms" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <label id="NumRoomsLabel" clientidmode="Static" runat="server" for="NumRooms"># of Rooms:</label>
            <input type="text" id="NumRooms" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="SQFeetRow" runat="server">
            <label id="SQFeetLabel" clientidmode="Static" runat="server" for="SQFeet">SQ Feet:</label>
            <input type="text" id="SQFeet" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="TypeRow" runat="server">
            <label id="TypeLabel" clientidmode="Static" runat="server" for="Status">Type:</label>
            <select id="Type" runat="server" class="form-control">
                <option value="1">Ready to Move In</option>
                <option value="2">Fixer-Upper</option>
                <option value="3">Furnished</option>
            </select>
        </div>
        <div class="mb-3">
            <label id="StyleLabel" clientidmode="Static" runat="server" for="Style">Style:</label>
            <select id="Style" runat="server" class="form-control">
                <option value="1">Beach House</option>
                <option value="2">Bungalow</option>
                <option value="3">Cabin</option>
                <option value="4">Colonial</option>
                <option value="5">Commercial</option>
                <option value="6">Farmhouse</option>
                <option value="7">Multi-Level</option>
                <option value="8">Multi-Unit</option>
                <option value="9">One-Story</option>
                <option value="10">Ranch</option>
                <option value="11">Two-Story</option>
            </select>
        </div>
        <div class="mb-3" id="SizeMBedroomRow" runat="server">
            <label id="SizeMBedroomLabel" clientidmode="Static" runat="server" for="SizeMBedroom">Master Bedroom Size:</label>
            <input type="text" id="SizeMBedroom" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="SizeLivingRoomRow" runat="server">
            <label id="SizeLivingRoomLabel" clientidmode="Static" runat="server" for="SizeLivingRoom">Living Room Size:</label>
            <input type="text" id="SizeLivingRoom" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="SizeDiningRoomRow" runat="server">
            <label id="SizeDiningRoomLabel" clientidmode="Static" runat="server" for="SizeDiningRoom">Dining Room Size:</label>
            <input type="text" id="SizeDiningRoom" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="SizeKitchenRow" runat="server">
            <label id="SizeKitchenLabel" clientidmode="Static" runat="server" for="SizeKitchen">Kitchen Size:</label>
            <input type="text" id="SizeKitchen" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3" id="SizeLotRow" runat="server">
            <label id="SizeLotLabel" clientidmode="Static" runat="server" for="SizeLot">Land/Lot Size:</label>
            <input type="text" id="SizeLot" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <label id="GarageLabel" clientidmode="Static" runat="server" for="Garage">Garage:</label>
            <input type="text" id="Garage" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <label id="HeatingLabel" clientidmode="Static" runat="server" for="Heating">Heating:</label>
            <input type="text" id="Heating" runat="server" class="form-control" maxlength="100" />
        </div>
        <div class="mb-3">
            <label id="FeatureInteriorLabel" clientidmode="Static" runat="server" for="FeatureInterior">Interior Features:</label>
            <sep:WYSIWYGEditor ID="FeatureInterior" runat="server" />
        </div>
        <div class="mb-3">
            <label id="FeatureExteriorLabel" clientidmode="Static" runat="server" for="FeatureExterior">Exterior Features:</label>
            <sep:WYSIWYGEditor ID="FeatureExterior" runat="server" />
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 32;
            cCustomFields.FieldUniqueID = this.PropertyID.Value;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>