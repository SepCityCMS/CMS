<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="vouchers_modify.aspx.cs" inherits="wwwroot.vouchers_modify1" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="'text/javascript">
		function getSavings() {
			var price1 = 0;
			var price2 = 0;
			try{if(isNaN(parseFloat(document.getElementById('SalePrice').value))){price1 = 0}else{price1 = parseFloat(document.getElementById('SalePrice').value);}}catch(e){price1 = 0;};
			try{if(isNaN(parseFloat(document.getElementById('RegularPrice').value))){price2 = 0}else{price2 = parseFloat(document.getElementById('RegularPrice').value);}}catch(e){price2 = 0;};
			if(price2 == 0)
			{
				document.getElementById('Savings').value = '0%';
			} else {
				document.getElementById('Savings').value = Math.round((((price2 - price1) / price2) * 100)) + '%';
			}
		}
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.RedemptionStart.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.RedemptionEnd.ClientID, "false", "true", "$('#RedemptionStart.ClientID').val()") %>;
            $('#<%= this.RedemptionStart.ClientID %>')
                .on("dp.change",
                    function (e) {
                        $('#<%= this.RedemptionEnd.ClientID %>').data("DateTimePicker").minDate(e.date);
                });
            $('#<%= this.RedemptionEnd.ClientID %>')
                .on("dp.change",
                    function (e) {
                        $('#<%= this.RedemptionStart.ClientID %>').data("DateTimePicker").maxDate(e.date);
                });
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Voucher</h4>
        <input type="hidden" runat="server" id="VoucherID" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server">Select a Category in the box below where to list your item:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="65" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="BuyTitleLabel" clientidmode="Static" runat="server" for="BuyTitle">Buy Title:</label>
            <input type="text" id="BuyTitle" runat="server" class="form-control" />
            <asp:CustomValidator ID="BuyTitleRequired" runat="server" ControlToValidate="BuyTitle"
                ClientValidationFunction="customFormValidator" ErrorMessage="Buy Title is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="ShortDescriptionLabel" clientidmode="Static" runat="server" for="ShortDescription">Short Description (Ex. Today Only! Get 65% savings on ice cream!):</label>
            <textarea id="ShortDescription" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="ShortDescriptionRequired" runat="server" ControlToValidate="ShortDescription"
                ClientValidationFunction="customFormValidator" ErrorMessage="Short Description is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="LongDescriptionLabel" clientidmode="Static" runat="server" for="LongDescription">Long Description (Describe options available to the customer under this deal):</label>
            <textarea id="LongDescription" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="LongDescriptionRequired" runat="server" ControlToValidate="LongDescription"
                ClientValidationFunction="customFormValidator" ErrorMessage="Long Description is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="LogoImageLabel" clientidmode="Static" runat="server" for="LogoImageUpload">Logo Image (Upload one logo or image descriptive of your deal or your business. File must be no larger than 250px x 250px and must be optimized for the web in .png or .jpg format):</label>
            <sep:UploadFiles ID="LogoImageUpload" runat="server" ModuleID="65" Mode="SingleFile" FileType="Images" />
        </div>
        <div class="mb-3">
            <label id="BarCodeUploadLabel" clientidmode="Static" runat="server" for="BarCodeUpload">Bar Code Image (If you have a graphic bar code that you want to use to scan the voucher at your point of sale, you can upload it here. It is not required and we track all purchased vouchers with a special random code to help prevent fraud. However, it is your responsibility to reconcile your vouchers to be sure a specific voucher has not already been redeemed.):</label>
            <sep:UploadFiles ID="BarCodeUpload" runat="server" ModuleID="65" Mode="SingleFile" FileType="Images" />
        </div>
        <div class="mb-3">
            <label id="SalePriceLabel" clientidmode="Static" runat="server" for="SalePrice">Sale Price:</label>
            <input type="text" id="SalePrice" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="SalePriceRequired" runat="server" ControlToValidate="SalePrice"
                ClientValidationFunction="customFormValidator" ErrorMessage="Sale Price is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="RegularPriceLabel" clientidmode="Static" runat="server" for="RegularPrice">Original Value:</label>
            <input type="text" id="RegularPrice" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="RegularPriceRequired" runat="server" ControlToValidate="RegularPrice"
                ClientValidationFunction="customFormValidator" ErrorMessage="Original Value is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="SavingsRow" runat="server">
            <label id="SavingsLabel" clientidmode="Static" runat="server" for="Savings">Savings (Savings are automatically calculated based on the values you have entered in the fields above):</label>
            <input type="text" id="Savings" runat="server" class="form-control" readonly="true" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="QuantityLabel" clientidmode="Static" runat="server" for="Quantity">Max Quantity Available (This is the maximum number of vouchers you want to make available for sale at this price. EX. 500):</label>
            <input type="text" id="Quantity" runat="server" class="form-control" />
            <asp:CustomValidator ID="QuantityRequired" runat="server" ControlToValidate="Quantity"
                ClientValidationFunction="customFormValidator" ErrorMessage="Quantity is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="MaxNumPerUserLabel" clientidmode="Static" runat="server" for="MaxNumPerUser">Max Quantity Per User (This field is for entering the maximum quantity of vouchers a single person may purchase under this deal. Example: you may want to limit a single person to purchase 3 vouchers maximum so that there are enough to go around for others to purchase.):</label>
            <input type="text" id="MaxNumPerUser" runat="server" class="form-control" />
            <asp:CustomValidator ID="MaxNumPerUserRequired" runat="server" ControlToValidate="MaxNumPerUser"
                ClientValidationFunction="customFormValidator" ErrorMessage="Max Quantity Per User is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="RedemptionStartLabel" clientidmode="Static" runat="server" for="RedemptionStart">Redemption Start Date (Redemption dates are the dates that the customer can redeem a voucher at your business or a discount code at your website):</label>
            <div class="form-group">
                <div class="input-group date" id="datetimepicker1">
                    <input type="text" id="RedemptionStart" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="RedemptionEndLabel" clientidmode="Static" runat="server" for="RedemptionEnd">Redemption End Date (Redemption dates are the dates that the customer can redeem a voucher at your business or a discount code at your website):</label>
            <div class="form-group">
                <div class="input-group date" id="datetimepicker2">
                    <input type="text" id="RedemptionEnd" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="PurchaseCodeLabel" clientidmode="Static" runat="server" for="PurchaseCode">Purchase Code:</label>
            <input type="text" id="PurchaseCode" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="PurchaseCodeRequired" runat="server" ControlToValidate="PurchaseCode"
                ClientValidationFunction="customFormValidator" ErrorMessage="Purchase Code is required."
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
            <label id="ContactEmailLabel" clientidmode="Static" runat="server" for="ContactEmail">Contact Email:</label>
            <input type="text" id="ContactEmail" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="ContactNameLabel" clientidmode="Static" runat="server" for="ContactName">Contact Name:</label>
            <input type="text" id="ContactName" runat="server" class="form-control" />
        </div>
        <div class="mb-3">
            <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" runat="server" class="form-control" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="DisclaimerLabel" clientidmode="Static" runat="server" for="Disclaimer">Disclaimer:</label>
            <textarea id="Disclaimer" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="DisclaimerRequired" runat="server" ControlToValidate="Disclaimer"
                ClientValidationFunction="customFormValidator" ErrorMessage="Disclaimer is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="AgreementRow" runat="server">
            <iframe id="SignupAgreementFrame" runat="server" width="500" height="100" src="vouchers_agreement.aspx"></iframe>
            <br />
            <asp:CheckBox ID="SignupAgreement" runat="server" Text="I Agree to the signup agreement above" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>