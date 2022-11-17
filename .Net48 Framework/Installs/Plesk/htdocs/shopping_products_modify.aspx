<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_products_modify.aspx.cs" inherits="wwwroot.shopping_products_modify" %>

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
            $('#tabAdvanced a').removeClass('btn-info');
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
            $("#Advanced").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openInventory() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').addClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
            $("#Advanced").hide();
            $("#InventoryDiv").show();
            restyleFormElements('#InventoryDiv');
        }

        function openAffiliate() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').addClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Custom").hide();
            $("#Advanced").hide();
            $("#Affiliate").show();
            restyleFormElements('#Affiliate');
        }

        function openCustom() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').addClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Advanced").hide();
            $("#Custom").show();
            restyleFormElements('#Custom');
        }

        function openAdvanced() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabInventory a').removeClass('btn-info');
            $('#tabAffiliate a').removeClass('btn-info');
            $('#tabCustom a').removeClass('btn-info');
            $('#tabAdvanced a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#InventoryDiv").hide();
            $("#Affiliate").hide();
            $("#Custom").hide();
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
                '<iframe style="width:100%; height: 350px;" id="UserSearchFrame" name="UserSearchFrame" src="<%= this.GetInstallFolder(true) %>spadmin/shoppingmall_custom_field_modify.aspx?RowOffset=' + irowOffset + '&FieldID=' + fieldId + '" frameborder="0" />';
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

            document.getElementById("AddCustomField")
                .setAttribute("href", "javascript:openCustomField('" + tableIndex + "', '');");
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="ModFormDiv" runat="server">
        <div class="panel panel-default">
            <div class="panel-body">
                <ul class="nav nav-pills">
                    <li class="nav-item" role="presentation" id="tabConfigure"><a class="nav-link" href="shopping_my_store.aspx">Configure Store</a></li>
                    <li class="nav-item" role="presentation" id="tabProducts"><a class="nav-link btn-info" href="shopping_my_products.aspx">My Products</a></li>
                    <li class="nav-item" role="presentation" id="tabOrders"><a class="nav-link" href="shopping_my_orders.aspx">My Orders</a></li>
                    <li class="nav-item" role="presentation" id="tabShipping"><a class="nav-link" href="shopping_shipping.aspx">Shipping Methods</a></li>
                    <li class="nav-item" role="presentation" id="tabAnalytics"><a class="nav-link" href="shopping_analytics.aspx">Analytics</a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            <div id="sectionConfigure">
                
                <ul class="nav nav-tabs">
                    <li class="nav-item"><a class="nav-link" href="shopping_my_products.aspx">Manage</a></li>
                    <li class="nav-item"><a class="nav-link active" href="shopping_products_modify.aspx">Add Product</a></li>
                </ul>

                <asp:Panel ID="CustomPanel" runat="server">
                    <div class="ModFormDiv">

                        <h4 id="ModifyLegend" runat="server">Add Product</h4>
                        <input type="hidden" runat="server" id="ProductID" />
                        <input type="hidden" runat="server" id="StoreID" />

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

                        <div class="col-md-9">
                            <div class="panel-body">
                                <div id="GeneralOptions">
                                    <div class="mb-3">
                                        <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                                        <sep:CategoryDropdown ID="Category" runat="server" ModuleID="41" ClientIDMode="Static" />
                                        <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                                        </asp:CustomValidator>
                                    </div>
                                    <div class="mb-3">
                                        <label id="ProductNameLabel" clientidmode="Static" runat="server" for="ProductName">Product Name:</label>
                                        <input type="text" id="ProductName" runat="server" class="form-control" maxlength="200" />
                                        <asp:CustomValidator ID="ProductNameRequired" runat="server" ControlToValidate="ProductName"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Product Name is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                                        </asp:CustomValidator>
                                    </div>
                                    <div class="mb-3">
                                        <label id="ManufacturerLabel" clientidmode="Static" runat="server" for="Manufacturer">Manufacturer:</label>
                                        <input type="text" id="Manufacturer" runat="server" class="form-control" maxlength="50" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="ModelNumberLabel" clientidmode="Static" runat="server" for="ModelNumber">Model Number:</label>
                                        <input type="text" id="ModelNumber" runat="server" class="form-control" maxlength="25" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="ShortDescriptionLabel" clientidmode="Static" runat="server" for="ShortDescription">Short Description:</label>
                                        <textarea id="ShortDescription" runat="server" class="form-control"></textarea>
                                        <asp:CustomValidator ID="ShortDescriptionRequired" runat="server" ControlToValidate="ShortDescription"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Short Description is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                                        </asp:CustomValidator>
                                    </div>
                                    <div class="mb-3">
                                        <label id="ImagesLabel" clientidmode="Static" runat="server" for="Images">Images:</label>
                                        <sep:UploadFiles ID="Images" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="41" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="FullDescriptionLabel" clientidmode="Static" runat="server" for="FullDescription">Full Description:</label>
                                        <sep:WYSIWYGEditor ID="FullDescription" runat="server" Width="99%" Height="450" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="UnitPriceLabel" clientidmode="Static" runat="server" for="UnitPrice">Unit Price:</label>
                                        <input type="text" id="UnitPrice" runat="server" class="form-control" maxlength="100" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="SalePriceLabel" clientidmode="Static" runat="server" for="SalePrice">Sale Price:</label>
                                        <input type="text" id="SalePrice" runat="server" class="form-control" maxlength="100" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="RecurringPriceLabel" clientidmode="Static" runat="server" for="RecurringPrice">Recurring Price:</label>
                                        <input type="text" id="RecurringPrice" runat="server" class="form-control" maxlength="100" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="RecurringCycleLabel" clientidmode="Static" runat="server" for="RecurringCycle">Recurring Cycle:</label>
                                        <select id="RecurringCycle" runat="server" class="form-control">
                                            <option value="1m">Monthly</option>
                                            <option value="3m">3 Months</option>
                                            <option value="6m">6 Months</option>
                                            <option value="1y">Yearly</option>
                                        </select>
                                    </div>
                                </div>
                                <div id="InventoryDiv" style="display: none">
                                    <div class="mb-3">
                                        <label id="ShippingOptionLabel" clientidmode="Static" runat="server" for="ShippingOption">Shipping Option:</label>
                                        <asp:DropDownList ID="ShippingOption" runat="server" CssClass="form-control" AutoPostBack="True" EnableViewState="True" OnSelectedIndexChanged="ShippingOption_SelectedIndexChanged">
                                            <asp:ListItem Text="Disabled" Value="disabled" />
                                            <asp:ListItem Text="Shipping" Value="shipping" />
                                            <asp:ListItem Text="Electronic" Value="electronic" />
                                        </asp:DropDownList>
                                    </div>
                                    <div id="ShippingOptions" runat="server" visible="false">
                                        <div class="mb-3">
                                            <label id="ItemWeightLabel" clientidmode="Static" runat="server" for="ItemWeight">Item Weight:</label>
                                            <input type="text" id="ItemWeight" runat="server" ckass="form-control inline-block" width="100" maxlength="100" />
                                            <select id="WeightType" runat="server" class="form-control inline-block" width="100">
                                                <option value="lbs">lbs</option>
                                                <option value="oz">oz</option>
                                            </select>
                                        </div>
                                        <div class="mb-3">
                                            <label id="DimensionsLabel" clientidmode="Static" runat="server" for="DimL">Dimensions (L x W x H):</label>
                                            Length
                                            <input type="text" id="DimL" runat="server" ckass="form-control inline-block" width="40" maxlength="15" />
                                            Width
                                            <input type="text" id="DimW" runat="server" ckass="form-control inline-block" width="40" maxlength="15" />
                                            Height
                                            <input type="text" id="DimH" runat="server" ckass="form-control inline-block" width="40" maxlength="15" />
                                        </div>
                                        <div class="mb-3">
                                            <label id="HandlingLabel" clientidmode="Static" runat="server" for="Handling">Handling:</label>
                                            <input type="text" id="Handling" runat="server" class="form-control" maxlength="100" />
                                        </div>
                                        <div class="mb-3">
                                            <label id="InventoryLabel" clientidmode="Static" runat="server" for="Inventory">Inventory:</label>
                                            <input type="text" id="Inventory" runat="server" class="form-control" maxlength="100" />
                                        </div>
                                        <div class="mb-3">
                                            <label id="MinOrderQtyLabel" clientidmode="Static" runat="server" for="MinOrderQty">Min Order Quantity:</label>
                                            <input type="text" id="MinOrderQty" runat="server" class="form-control" maxlength="100" />
                                        </div>
                                        <div class="mb-3">
                                            <label id="MaxOrderQtyLabel" clientidmode="Static" runat="server" for="MaxOrderQty">Max Order Quantity:</label>
                                            <input type="text" id="MaxOrderQty" runat="server" class="form-control" maxlength="100" />
                                        </div>
                                        <div class="mb-3">
                                            <label id="SKULabel" clientidmode="Static" runat="server" for="SKU">SKU:</label>
                                            <input type="text" id="SKU" runat="server" class="form-control" maxlength="100" />
                                        </div>
                                    </div>
                                    <div id="ElectronicOptions" runat="server" visible="false">
                                        <div class="mb-3">
                                            <label id="SelectFileLabel" clientidmode="Static" runat="server" for="SelectFile">Select File:</label>
                                            <sep:UploadFiles ID="SelectFile" runat="server" ModuleID="41" Mode="SingleFile" FileType="Any" />
                                        </div>
                                    </div>
                                </div>
                                <div id="Affiliate" style="display: none">
                                    <div class="mb-3">
                                        <asp:CheckBox ID="ExcludeAffiliate" runat="server" Text="Exclude Affiliate" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="AffiliatePriceLabel" clientidmode="Static" runat="server" for="AffiliateUnitPrice">Affiliate Price:</label>
                                        <input type="text" id="AffiliateUnitPrice" runat="server" class="form-control" maxlength="100" />
                                    </div>
                                    <div class="mb-3">
                                        <label id="AffiliateRecurringLabel" clientidmode="Static" runat="server" for="AffiliateRecurringPrice">Affiliate Recurring:</label>
                                        <input type="text" id="AffiliateRecurringPrice" runat="server" class="form-control" maxlength="100" />
                                    </div>
                                </div>
                                <div id="Custom" style="display: none">

                                    <asp:GridView ID="CustomFields" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                        CssClass="GridViewStyle" ShowHeaderWhenEmpty="true" Width="500">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Field Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <a href="javascript:openCustomField('<%#
                this.Eval("Offset") %>', '<%#
                this.Eval("FieldID") %>')"><%#
                this.Eval("FieldName") %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answer Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <input type="hidden" name="FieldID" id="FieldID<%#
                this.Eval("FieldID") %>"
                                                        value="<%#
                this.Eval("FieldID") %>" />
                                                    <%#
                this.Eval("AnswerType") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <a href="javascript:deleteField('<%#
                this.Eval("Offset") %>', '<%#
                this.Eval("FieldID") %>')">Delete</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <input type="hidden" id="FieldIDs" runat="server" clientidmode="static" />
                                    <input type="hidden" id="FieldNames" runat="server" clientidmode="static" />
                                    <input type="hidden" id="AnswerTypes" runat="server" clientidmode="static" />
                                    <input type="hidden" id="CustomFieldOptions" runat="server" clientidmode="static" />
                                    <input type="hidden" id="Orders" runat="server" clientidmode="static" />
                                    <input type="hidden" id="Requires" runat="server" clientidmode="static" />
                                    <input type="hidden" id="DeleteCustomFieldIds" runat="server" clientidmode="static" />
                                    <input type="hidden" id="DeleteCustomFieldOptions" runat="server" clientidmode="static" />

                                    <div class="mb-3">
                                        <asp:HyperLink ID="AddCustomField" runat="server" NavigateUrl="javascript:openCustomField('0', '')" ClientIDMode="Static" Text="Add Field" />
                                    </div>
                                </div>
                                <div id="Advanced" style="display: none">
                                    <div class="mb-3">
                                        <asp:CheckBox ID="DisableProduct" runat="server" Text="Disable Product?" />
                                    </div>
                                    <div class="mb-3">
                                        <asp:CheckBox ID="TaxExempt" runat="server" Text="Tax Exempt?" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr class="mb-4" />
                        <div class="mb-3">
                            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:content>