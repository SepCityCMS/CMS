<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_my_store.aspx.cs" inherits="wwwroot.shopping_my_store" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="panel panel-default">
        <div class="panel-body">
            <ul class="nav nav-pills">
                <li class="nav-item" role="presentation" id="tabConfigure"><a class="nav-link btn-info" href="shopping_my_store.aspx">Configure Store</a></li>
                <li class="nav-item" role="presentation" id="tabProducts"><a class="nav-link" href="shopping_my_products.aspx">My Products</a></li>
                <li class="nav-item" role="presentation" id="tabOrders"><a class="nav-link" href="shopping_my_orders.aspx">My Orders</a></li>
                <li class="nav-item" role="presentation" id="tabShipping"><a class="nav-link" href="shopping_shipping.aspx">Shipping Methods</a></li>
                <li class="nav-item" role="presentation" id="tabAnalytics"><a class="nav-link" href="shopping_analytics.aspx">Analytics</a></li>
            </ul>
        </div>
    </div>

    <div class="panel-body">
        <div id="sectionConfigure">

            <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">My Store</h4>
                <input type="hidden" runat="server" id="StoreID" />

                <div class="mb-3">
                    <label id="StoreNameLabel" clientidmode="Static" runat="server" for="StoreName">Store Name:</label>
                    <input type="text" id="StoreName" runat="server" class="form-control" maxlength="100" />
                    <asp:CustomValidator ID="StoreNameRequired" runat="server" ControlToValidate="StoreName"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Store Name is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea id="Description" runat="server" class="form-control"></textarea>
                    <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label id="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Company Name:</label>
                    <input type="text" id="CompanyName" runat="server" class="form-control" maxlength="100" />
                </div>
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
                    <label id="ContactEmailLabel" clientidmode="Static" runat="server" for="ContactEmail">Contact Email:</label>
                    <input type="text" id="ContactEmail" runat="server" class="form-control" />
                </div>

                <hr class="mb-4" />
                <div class="mb-3">
                    <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
                </div>
            </div>
        </div>
    </div>
</asp:content>