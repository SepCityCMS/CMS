<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="discounts_modify.aspx.cs" inherits="wwwroot.discounts_modify1" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.ExpirationDate.ClientID, "false", "true", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Discount</h4>
        <input type="hidden" runat="server" id="DiscountID" />
        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="5" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="LabelTextLabel" clientidmode="Static" runat="server" for="LabelText">Label Text:</label>
            <input type="text" id="LabelText" runat="server" class="form-control" />
            <asp:CustomValidator ID="LabelTextRequired" runat="server" ControlToValidate="LabelText"
                ClientValidationFunction="customFormValidator" ErrorMessage="Label Text is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Company Name:</label>
            <input type="text" id="CompanyName" runat="server" class="form-control" />
            <asp:CustomValidator ID="CompanyNameRequired" runat="server" ControlToValidate="CompanyName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Company Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="DisclaimerLabel" clientidmode="Static" runat="server" for="Disclaimer">Disclaimer:</label>
            <input type="text" id="Disclaimer" runat="server" class="form-control" />
            <asp:CustomValidator ID="DisclaimerRequired" runat="server" ControlToValidate="Disclaimer"
                ClientValidationFunction="customFormValidator" ErrorMessage="Disclaimer is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="DiscountCodeLabel" clientidmode="Static" runat="server" for="DiscountCode">Discount Code:</label>
            <input type="text" id="DiscountCode" runat="server" class="form-control" />
            <asp:CustomValidator ID="DiscountCodeRequired" runat="server" ControlToValidate="DiscountCode"
                ClientValidationFunction="customFormValidator" ErrorMessage="Discount Code is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="MarkOffTypeLabel" clientidmode="Static" runat="server" for="MarkOffType">Mark-off Price:</label>
            <select id="MarkOffType" runat="server" class="form-control inline-block" width="10%">
                <option value="0">Dollars</option>
                <option value="1">Percent</option>
                <option value="2">Fixed-Price</option>
            </select>
            <input type="text" id="MarkOffPrice" runat="server" ckass="form-control inline-block" width="89%" />
        </div>
        <div class="mb-3">
            <label id="QuantityLabel" clientidmode="Static" runat="server" for="Quantity">Quantity:</label>
            <input type="text" id="Quantity" runat="server" class="form-control" />
            <asp:CustomValidator ID="QuantityRequired" runat="server" ControlToValidate="Quantity"
                ClientValidationFunction="customFormValidator" ErrorMessage="Quantity is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="ExpirationDateLabel" clientidmode="Static" runat="server" for="ExpirationDate">Expiration Date:</label>
            <div class="form-group">
                <div class="input-group date" id="ExpirationDateDiv">
                    <input type="text" id="ExpirationDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
            <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
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
            <label id="ProductImageLabel" clientidmode="Static" runat="server" for="ProductImageUpload">Product Image:</label>
            <sep:UploadFiles ID="ProductImageUpload" runat="server" ModuleID="5" Mode="SingleFile" FileType="Images" />
        </div>
        <div class="mb-3">
            <label id="BarCodeUploadLabel" clientidmode="Static" runat="server" for="BarCodeUpload">Bar Code Image:</label>
            <sep:UploadFiles ID="BarCodeUpload" runat="server" ModuleID="5" Mode="SingleFile" FileType="Images" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>