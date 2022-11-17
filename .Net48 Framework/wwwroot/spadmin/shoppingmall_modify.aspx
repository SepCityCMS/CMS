<%@ page title="Shopping Mall" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shoppingmall_modify.aspx.cs" inherits="wwwroot.shoppingmall_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/filters.js" type="text/javascript"></script>
    <script type="text/javascript">
        skipRestyling = true;

        function openGeneralOptions() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
            $("#Related").hide();
            $("#Advanced").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openInventory() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').addClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
            $("#Related").hide();
            $("#Advanced").hide();
            $("#InventoryDiv").show();
            restyleFormElements('#InventoryDiv');
        }

        function openAffiliate() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').addClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Custom").hide();
            $("#Related").hide();
            $("#Advanced").hide();
            $("#Affiliate").show();
            restyleFormElements('#Affiliate');
        }

        function openCustom() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').addClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Related").hide();
            $("#Advanced").hide();
            $("#Custom").show();
            restyleFormElements('#Custom');
        }

        function openRelated() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabRelated a').addClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
            $("#Advanced").hide();
            $("#Related").show();
            restyleFormElements('#Related');
        }

        function openAdvanced() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabRelated a').removeClass('btn-info');
            $('#tabAdvanced a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
            $("#Related").hide();
            $("#Advanced").show();
            restyleFormElements('#Advanced');
        }

        $(document)
            .ready(function () {
                restyleFormElements('#GeneralOptions');
                restyleGridView("#CustomFields");
                $('#tabGeneral a').addClass('btn-info');
            });

        function openCustomField(irowOffset, fieldId) {
            if ($('#FieldFilterDiv').length > 0) {
                $('#FieldFilterDiv').remove();
            }
            var searchIFrame =
                '<iframe style="width:100%; height: 350px;" id="UserSearchFrame" name="UserSearchFrame" src="shoppingmall_custom_field_modify.aspx?RowOffset=' + irowOffset + '&FieldID=' + fieldId + '" frameborder="0" />';
            var $searchDiv = $('<div id="FieldFilterDiv" style="width:100%;" title="Add Custom Field">' +
                searchIFrame +
                '</div>');

            $('body').append($searchDiv);
            openModal('FieldFilterDiv',
                400,
                450,
                '<button type="button" class="btn btn-primary" onclick="UserSearchFrame.applyField();">Save Field</button>');
        }

        function deleteField(offset, fieldId) {
            $("#CustomFields tr").eq(offset).remove();

            if ($("#DeleteCustomFieldIds").val() != '') {
                $("#DeleteCustomFieldIds").val($("#DeleteCustomFieldIds").val() + '|%|');
            };
            $("#DeleteCustomFieldIds").val($("#DeleteCustomFieldIds").val() + fieldId);

            // Fix table indexes
            var tableIndex = 0;
            var fieldId = "";
            var fieldName = "";
            $('#CustomFields tr')
                .each(function () {
                    if (tableIndex > 0) {
                        fieldId = $(this).find("td").eq(0).find("input").eq(0).val();
                        fieldName = $(this).find("td").eq(0).find("a").eq(0).html();
                        $(this)
                            .find("td")
                            .eq(0)
                            .html('<input type=\"hidden\" name=\"FieldID' +
                                fieldId +
                                '\" id=\"FieldID' +
                                fieldId +
                                '\" value=\"' +
                                fieldId +
                                '\" /><a href=\"javascript:openCustomField(\'' +
                                tableIndex +
                                '\', \'' +
                                fieldId +
                                '\')\">' +
                                fieldName +
                                '</a>');
                        $(this)
                            .find("td")
                            .eq(2)
                            .html('<a href=\"javascript:deleteField(\'' +
                                tableIndex +
                                '\', \'' +
                                fieldId +
                                '\')\">Delete</a>');
                    }
                    tableIndex = tableIndex + 1;
                });

            document.getElementById("AddCustomField").setAttribute("href", "javascript:openCustomField('" + tableIndex + "', '');");
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

<asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 41;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

<asp:Panel ID="CustomPanel" runat="server">
<div class="ModFormDiv" id="ModFormDiv" runat="server">

<h4 id="ModifyLegend" runat="server">Add Product</h4>
<input type="hidden" runat="server" ID="ProductID" />

    <div class="panel panel-default" id="PageManageGridView" runat="server">
        <div class="panel-body">
            <ul class="nav nav-pills">
                <li class="nav-item" role="presentation" id="tabGeneral">
                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                </li>
                <li class="nav-item" role="presentation" id="tabInventory">
                    <a class="nav-link" href="javascript:void(0)" onclick="openInventory();">Inventory / Shipping</a>
                </li>
                <li class="nav-item" role="presentation" id="tabAffiliate">
                    <a class="nav-link" href="javascript:void(0)" onclick="openAffiliate();">Affiliate</a>
                </li>
                <li class="nav-item" role="presentation" id="tabCustom">
                    <a class="nav-link" href="javascript:void(0)" onclick="openCustom();">Custom Fields</a>
                </li>
                <li class="nav-item" role="presentation" id="tabAdvanced">
                    <a class="nav-link" href="javascript:void(0)" onclick="openAdvanced();">Advanced</a>
                </li>
            </ul>
        </div>
    </div>

<div class="panel-body">
<div id="GeneralOptions">

    <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="41" CssClass="form-control"></sep:ChangeLogDropdown>

    <div class="mb-3">
        <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
        <sep:CategoryDropdown ID="Category" runat="server" ModuleID="41" ClientIDMode="Static" />
        <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                             ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                             ValidateEmptyText="true" Display="Dynamic">
        </asp:CustomValidator>
    </div>
    <div class="mb-3">
        <label ID="ProductNameLabel" clientidmode="Static" runat="server" for="ProductName">Product Name:</label>
        <input type="text" ID="ProductName" runat="server"  class="form-control" MaxLength="200" />
        <asp:CustomValidator ID="ProductNameRequired" runat="server" ControlToValidate="ProductName"
                             ClientValidationFunction="customFormValidator" ErrorMessage="Product Name is required."
                             ValidateEmptyText="true" Display="Dynamic">
        </asp:CustomValidator>
    </div>
    <div class="mb-3">
        <label ID="ManufacturerLabel" clientidmode="Static" runat="server" for="Manufacturer">Manufacturer:</label>
        <input type="text" ID="Manufacturer" runat="server"  class="form-control" MaxLength="50" />
    </div>
    <div class="mb-3">
        <label ID="ModelNumberLabel" clientidmode="Static" runat="server" for="ModelNumber">Model Number:</label>
        <input type="text" ID="ModelNumber" runat="server"  class="form-control" MaxLength="25" />
    </div>
    <div class="mb-3">
        <label ID="ShortDescriptionLabel" clientidmode="Static" runat="server" for="ShortDescription">Short Description:</label>
        <textarea ID="ShortDescription" runat="server"  class="form-control"></textarea>
        <asp:CustomValidator ID="ShortDescriptionRequired" runat="server" ControlToValidate="ShortDescription"
                             ClientValidationFunction="customFormValidator" ErrorMessage="Short Description is required."
                             ValidateEmptyText="true" Display="Dynamic">
        </asp:CustomValidator>
    </div>
    <div class="mb-3">
        <label ID="ImagesLabel" clientidmode="Static" runat="server" for="Images">Images:</label>
        <sep:UploadFiles ID="Images" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="41" />
    </div>
    <div class="mb-3">
        <label ID="FullDescriptionLabel" clientidmode="Static" runat="server" for="FullDescription">Full Description:</label>
        <sep:WYSIWYGEditor ID="FullDescription" runat="server" Width="99%" Height="450" />
    </div>
    <div class="mb-3">
        <label ID="UnitPriceLabel" clientidmode="Static" runat="server" for="UnitPrice">Unit Price:</label>
        <input type="text" ID="UnitPrice" runat="server"  class="form-control" MaxLength="100" />
    </div>
    <div class="mb-3">
        <label ID="SalePriceLabel" clientidmode="Static" runat="server" for="SalePrice">Sale Price:</label>
        <input type="text" ID="SalePrice" runat="server"  class="form-control" MaxLength="100" />
    </div>
    <div class="mb-3">
        <label ID="RecurringPriceLabel" clientidmode="Static" runat="server" for="RecurringPrice">Recurring Price:</label>
        <input type="text" ID="RecurringPrice" runat="server"  class="form-control" MaxLength="100" />
    </div>
    <div class="mb-3">
        <label ID="RecurringCycleLabel" clientidmode="Static" runat="server" for="RecurringCycle">Recurring Cycle:</label>
        <select ID="RecurringCycle" runat="server" class="form-control">
            <option value="1m">Monthly</option>
            <option value="3m">3 Months</option>
            <option value="6m">6 Months</option>
            <option value="1y">Yearly</option>
        </select>
    </div>
</div>
<div id="InventoryDiv" style="display: none">
    <div class="mb-3">
        <label ID="ShippingOptionLabel" clientidmode="Static" runat="server" for="ShippingOption">Shipping Option:</label>
        <asp:DropDownList ID="ShippingOption" runat="server" CssClass="form-control" AutoPostBack="True" EnableViewState="True" OnSelectedIndexChanged="ShippingOption_SelectedIndexChanged">
            <asp:ListItem Text="Disabled" Value="disabled" />
            <asp:ListItem Text="Shipping" Value="shipping" />
            <asp:ListItem Text="Electronic" Value="electronic" />
        </asp:DropDownList>
    </div>
    <div id="ApiCallRow" runat="server" visible="false">
        <div class="mb-3">
            <label ID="ApiCallLabel" clientidmode="Static" runat="server" for="ApiCall">Select API Call:</label>
            <asp:DropDownList ID="ApiCall" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
    </div>
    <div id="ShippingOptions" runat="server" visible="false">
        <div class="mb-3">
            <label ID="ItemWeightLabel" clientidmode="Static" runat="server" for="ItemWeight">Item Weight:</label>
            <input type="text" id="ItemWeight" runat="server" ckass="form-control inline-block" Width="100" MaxLength="100" />
            <select ID="WeightType" runat="server" Class="form-control inline-block" Width="100">
                <option value="lbs">lbs</option>
                <option value="oz">oz</option>
            </select>
        </div>
        <div class="mb-3">
            <label ID="DimensionsLabel" clientidmode="Static" runat="server" for="DimL">Dimensions (L x W x H):</label>
            Length <input type="text" id="DimL" runat="server" ckass="form-control inline-block" Width="40" MaxLength="15" />
            Width <input type="text" id="DimW" runat="server" ckass="form-control inline-block" Width="40" MaxLength="15" />
            Height <input type="text" id="DimH" runat="server" ckass="form-control inline-block" Width="40" MaxLength="15" />
        </div>
        <div class="mb-3">
            <label ID="HandlingLabel" clientidmode="Static" runat="server" for="Handling">Handling:</label>
            <input type="text" ID="Handling" runat="server"  class="form-control" MaxLength="100" />
        </div>
        <div class="mb-3">
            <label ID="InventoryLabel" clientidmode="Static" runat="server" for="Inventory">Inventory:</label>
            <input type="text" ID="Inventory" runat="server"  class="form-control" MaxLength="100" />
        </div>
        <div class="mb-3">
            <label ID="MinOrderQtyLabel" clientidmode="Static" runat="server" for="MinOrderQty">Min Order Quantity:</label>
            <input type="text" ID="MinOrderQty" runat="server"  class="form-control" MaxLength="100" />
        </div>
        <div class="mb-3">
            <label ID="MaxOrderQtyLabel" clientidmode="Static" runat="server" for="MaxOrderQty">Max Order Quantity:</label>
            <input type="text" ID="MaxOrderQty" runat="server"  class="form-control" MaxLength="100" />
        </div>
        <div class="mb-3">
            <label ID="SKULabel" clientidmode="Static" runat="server" for="SKU">SKU:</label>
            <input type="text" ID="SKU" runat="server"  class="form-control" MaxLength="100" />
        </div>
    </div>
    <div id="ElectronicOptions" runat="server" visible="false">
        <div class="mb-3">
            <label ID="SelectFileLabel" clientidmode="Static" runat="server" for="SelectFile">Select File:</label>
            <sep:UploadFiles ID="SelectFile" runat="server" ModuleID="41" Mode="SingleFile" FileType="Any" />
        </div>
    </div>
</div>
<div id="Affiliate" style="display: none">
    <div class="mb-3">
        <asp:CheckBox ID="ExcludeAffiliate" runat="server" Text="Exclude Affiliate" />
    </div>
    <div class="mb-3">
        <label ID="AffiliatePriceLabel" clientidmode="Static" runat="server" for="AffiliateUnitPrice">Affiliate Price:</label>
        <input type="text" ID="AffiliateUnitPrice" runat="server"  class="form-control" MaxLength="100" />
    </div>
    <div class="mb-3">
        <label ID="AffiliateRecurringLabel" clientidmode="Static" runat="server" for="AffiliateRecurringPrice">Affiliate Recurring:</label>
        <input type="text" ID="AffiliateRecurringPrice" runat="server"  class="form-control" MaxLength="100" />
    </div>
</div>
<div id="Custom" style="display: none">

    <asp:GridView ID="CustomFields" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                  CssClass="GridViewStyle" ShowHeaderWhenEmpty="true" Width="500">
        <Columns>
            <asp:TemplateField HeaderText="Field Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="javascript:openCustomField('<%#
                Eval("Offset")%>', '<%#
                Eval("FieldID")%>')"><%#
                Eval("FieldName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Answer Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <input type="hidden" name="FieldID" id="FieldID<%#
                Eval("FieldID")%>" value="<%#
                Eval("FieldID")%>" />
                    <%#
                Eval("AnswerType")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="javascript:deleteField('<%#
                Eval("Offset")%>', '<%#
                Eval("FieldID")%>')">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <input type="hidden" ID="FieldIDs" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="FieldNames" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="AnswerTypes" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="CustomFieldOptions" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="Orders" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="Requires" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="DeleteCustomFieldIds" runat="server" ClientIDMode="static" />
    <input type="hidden" ID="DeleteCustomFieldOptions" runat="server" ClientIDMode="static" />

    <div class="mb-3">
        <asp:HyperLink ID="AddCustomField" runat="server" NavigateUrl="javascript:openCustomField('0', '')" ClientIDMode="Static" Text="Add Field" />
    </div>
</div>
<div id="Related" style="display: none">

    <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                  CssClass="GridViewStyle" ShowHeaderWhenEmpty="true">
        <Columns>
            <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                Eval("ProductName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <input type="hidden" name="ProductID" id="ProductID<%#
                Eval("ProductID")%>" value="<%#
                Eval("ProductID")%>" />
                    <%#
                Eval("UnitPrice")%>
                    <%#
                        Convert.ToString(Format_Currency(DataBinder.Eval(Container.DataItem, "Handling")) != Format_Currency("0") ? "<br />Plus " + DataBinder.Eval(Container.DataItem, "Handling") + " handling fee" : "")
                    %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <input type="text" name="Qty" style="width: 50px" value="<%#
                Eval("Quantity")%>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                Eval("TotalPrice")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div class="mb-3">
        <asp:HyperLink ID="AddProduct" runat="server" NavigateUrl="javascript:openProductSearch('0')" ClientIDMode="Static" Text="Add Product" />
    </div>
</div>
<div id="Advanced" style="display: none">
    <div class="mb-3">
        <asp:CheckBox ID="DisableProduct" runat="server" Text="Disable Product?" />
    </div>
    <div class="mb-3">
        <asp:CheckBox ID="TaxExempt" runat="server" Text="Tax Exempt?" />
    </div>
    <div class="mb-3">
        <label ID="NewsletterLabel" clientidmode="Static" runat="server" for="Newsletter">Newsletter for user to join if they purchase this item:</label>
        <sep:NewsletterDropdown id="Newsletter" runat="server" CssClass="form-control" />
    </div>
    <div class="mb-3" id="PortalsRow" runat="server">
        <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
        <sep:PortalDropdown ID="Portal" runat="server" CssClass="form-control" />
    </div>
</div>
</div>
</div>
    <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
</div>
</asp:Panel>
</asp:Panel>
</asp:content>