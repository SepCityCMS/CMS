<%@ page title="Discounts" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="discounts_modify.aspx.cs" inherits="wwwroot.discounts_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(ExpirationDate.ClientID, "false", "true", "")%>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 5;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Discount</h4>
                <input type="hidden" runat="server" ID="DiscountID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="5" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="5" ClientIDMode="Static" />
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
                    <label ID="LabelTextLabel" clientidmode="Static" runat="server" for="LabelText">Label Text:</label>
                    <input type="text" ID="LabelText" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="LabelTextRequired" runat="server" ControlToValidate="LabelText"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Label Text is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Company Name:</label>
                    <input type="text" ID="CompanyName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="CompanyNameRequired" runat="server" ControlToValidate="CompanyName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Company Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="DisclaimerLabel" clientidmode="Static" runat="server" for="Disclaimer">Disclaimer:</label>
                    <input type="text" ID="Disclaimer" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="DisclaimerRequired" runat="server" ControlToValidate="Disclaimer"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Disclaimer is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="DiscountCodeLabel" clientidmode="Static" runat="server" for="DiscountCode">Discount Code:</label>
                    <input type="text" ID="DiscountCode" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="DiscountCodeRequired" runat="server" ControlToValidate="DiscountCode"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Discount Code is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="MarkOffTypeLabel" clientidmode="Static" runat="server" for="MarkOffType">Mark-off Price:</label>
                    <select ID="MarkOffType" runat="server" Class="form-control inline-block" Width="10%">
                        <option value="0">Dollars</option>
                        <option value="1">Percent</option>
                        <option value="2">Fixed-Price</option>
                    </select>
                    <input type="text" id="MarkOffPrice" runat="server" ckass="form-control inline-block" Width="89%" />
                </div>
                <div class="mb-3">
                    <label ID="QuantityLabel" clientidmode="Static" runat="server" for="Quantity">Quantity:</label>
                    <input type="text" ID="Quantity" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="QuantityRequired" runat="server" ControlToValidate="Quantity"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Quantity is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ExpirationDateLabel" clientidmode="Static" runat="server" for="ExpirationDate">Expiration Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="ExpirationDateDiv">
                            <input type="text" id="ExpirationDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="CountryLabel" clientidmode="Static" runat="server" for="Country">Country:</label>
                    <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
                </div>
                <div class="mb-3">
                    <label ID="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
                    <input type="text" ID="City" runat="server"  class="form-control" MaxLength="50" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CityRequired" runat="server" ControlToValidate="City"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="City is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
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
                <div class="mb-3">
                    <label ID="ProductImageLabel" clientidmode="Static" runat="server" for="ProductImageUpload">Product Image:</label>
                    <sep:UploadFiles ID="ProductImageUpload" runat="server" ModuleID="5" Mode="SingleFile" FileType="Images" />
                </div>
                <div class="mb-3">
                    <label ID="BarCodeUploadLabel" clientidmode="Static" runat="server" for="BarCodeUpload">Bar Code Image:</label>
                    <sep:UploadFiles ID="BarCodeUpload" runat="server" ModuleID="5" Mode="SingleFile" FileType="Images" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>