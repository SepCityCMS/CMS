<%@ page title="Real Estate" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="realestate_modify.aspx.cs" inherits="wwwroot.realestate_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 32;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Property</h4>
                <input type="hidden" runat="server" ID="PropertyID" />
                <input type="hidden" runat="server" ID="AgentID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="32" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="PropertyTitleLabel" clientidmode="Static" runat="server" for="PropertyTitle">Title:</label>
                    <input type="text" ID="PropertyTitle" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="TitleRequired" runat="server" ControlToValidate="PropertyTitle"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Title is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
                    <input type="text" id="Price" runat="server" ckass="form-control inline-block" Width="59%" MaxLength="30" />
                    <select ID="RecurringCycle" runat="server" Class="form-control inline-block" Width="20%">
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
                    <label ID="MLSNumberLabel" clientidmode="Static" runat="server" for="MLSNumber">MLS Number:</label>
                    <input type="text" ID="MLSNumber" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <sep:UploadFiles ID="Pictures" runat="server" ModuleID="32" Mode="MultipleFiles" FileType="Images" />
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <sep:WYSIWYGEditor ID="Description" runat="server" />
                </div>
                <div class="mb-3">
                    <label ID="PropertyTypeLabel" clientidmode="Static" runat="server" for="PropertyType">Property Type:</label>
                    <select ID="PropertyType" runat="server" class="form-control">
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
                    <label ID="StatusLabel" clientidmode="Static" runat="server" for="Status">Status:</label>
                    <select ID="Status" runat="server" class="form-control">
                        <option value="1">Available</option>
                        <option value="2">Not Available</option>
                        <option value="3">Available (Show on Site)</option>
                    </select>
                </div>
                <div class="mb-3" id="CountryRow" runat="server">
                    <label ID="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
                    <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
                </div>
                <div class="mb-3">
                    <label ID="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                    <input type="text" ID="StreetAddress" runat="server"  class="form-control" MaxLength="100" ClientIDMode="Static" />
                    <asp:CustomValidator ID="StreetAddressRequired" runat="server" ControlToValidate="StreetAddress"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Street Address is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
                    <input type="text" ID="City" runat="server"  class="form-control" MaxLength="50" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CityRequired" runat="server" ControlToValidate="City"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="City is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="StateRow" runat="server">
                    <label ID="StateLabel" clientidmode="Static" runat="server" for="Country">State/Province:</label>
                    <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="PostalCodeLabel" clientidmode="Static" runat="server" for="PostalCode">Zip/Postal Code:</label>
                    <input type="text" ID="PostalCode" runat="server"  class="form-control" MaxLength="10" ClientIDMode="Static" />
                    <asp:CustomValidator ID="PostalCodeRequired" runat="server" ControlToValidate="PostalCode"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Zip/Postal Code is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="CountyRow" runat="server">
                    <label ID="CountyLabel" clientidmode="Static" runat="server" for="County">County:</label>
                    <input type="text" ID="County" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="YearBuiltRow" runat="server">
                    <label ID="YearBuiltLabel" clientidmode="Static" runat="server" for="YearBuilt">Year Built:</label>
                    <input type="text" ID="YearBuilt" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="NumBedroomsLabel" clientidmode="Static" runat="server" for="NumBedrooms"># of Bedrooms:</label>
                    <input type="text" ID="NumBedrooms" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="NumBathroomsLabel" clientidmode="Static" runat="server" for="NumBathrooms"># of Bathrooms:</label>
                    <input type="text" ID="NumBathrooms" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="NumRoomsLabel" clientidmode="Static" runat="server" for="NumRooms"># of Rooms:</label>
                    <input type="text" ID="NumRooms" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="SQFeetRow" runat="server">
                    <label ID="SQFeetLabel" clientidmode="Static" runat="server" for="SQFeet">SQ Feet:</label>
                    <input type="text" ID="SQFeet" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="TypeRow" runat="server">
                    <label ID="TypeLabel" clientidmode="Static" runat="server" for="Status">Type:</label>
                    <select ID="Type" runat="server" class="form-control">
                        <option value="1">Ready to Move In</option>
                        <option value="2">Fixer-Upper</option>
                        <option value="3">Furnished</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="StyleLabel" clientidmode="Static" runat="server" for="Style">Style:</label>
                    <select ID="Style" runat="server" class="form-control">
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
                    <label ID="SizeMBedroomLabel" clientidmode="Static" runat="server" for="SizeMBedroom">Master Bedroom Size:</label>
                    <input type="text" ID="SizeMBedroom" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="SizeLivingRoomRow" runat="server">
                    <label ID="SizeLivingRoomLabel" clientidmode="Static" runat="server" for="SizeLivingRoom">Living Room Size:</label>
                    <input type="text" ID="SizeLivingRoom" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="SizeDiningRoomRow" runat="server">
                    <label ID="SizeDiningRoomLabel" clientidmode="Static" runat="server" for="SizeDiningRoom">Dining Room Size:</label>
                    <input type="text" ID="SizeDiningRoom" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="SizeKitchenRow" runat="server">
                    <label ID="SizeKitchenLabel" clientidmode="Static" runat="server" for="SizeKitchen">Kitchen Size:</label>
                    <input type="text" ID="SizeKitchen" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3" id="SizeLotRow" runat="server">
                    <label ID="SizeLotLabel" clientidmode="Static" runat="server" for="SizeLot">Land/Lot Size:</label>
                    <input type="text" ID="SizeLot" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="GarageLabel" clientidmode="Static" runat="server" for="Garage">Garage:</label>
                    <input type="text" ID="Garage" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="HeatingLabel" clientidmode="Static" runat="server" for="Heating">Heating:</label>
                    <input type="text" ID="Heating" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="FeatureInteriorLabel" clientidmode="Static" runat="server" for="FeatureInterior">Interior Features:</label>
                    <sep:WYSIWYGEditor ID="FeatureInterior" runat="server" />
                </div>
                <div class="mb-3">
                    <label ID="FeatureExteriorLabel" clientidmode="Static" runat="server" for="FeatureExterior">Exterior Features:</label>
                    <sep:WYSIWYGEditor ID="FeatureExterior" runat="server" />
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 32;
                    cCustomFields.FieldUniqueID = PropertyID.Value;
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