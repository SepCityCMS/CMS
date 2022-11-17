<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shop_product_view.aspx.cs" inherits="wwwroot.shop_product_view" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        var skipRestyling = true;

        function saveWishList(ProductID) {
            $.ajax({
                url: config.siteBase + 'favorites_add.aspx?ModuleID=41&PageURL=WISHLIST:' + ProductID,
                success: function (data) {
                    openModal('AddWishList', 300, 200);
                    $('#AddWishListMsg').html(data);
                }
            });
            return false;
        }

        function orderProduct(ProductID, StoreID) {
            if (ValidateCustomOptions() === true) {
                if (hasCustomOptions() !== '') {
                    <%
                        this.Response.Write("var params = new Object();");
                        this.Response.Write("params.UserID = \"" + SepFunctions.Session_User_ID() + "\";");
                        this.Response.Write("params.ModuleID = \"41\";");
                        this.Response.Write("params.UniqueID = ProductID;");
                        this.Response.Write("params.ProductID = ProductID;");
                        this.Response.Write("params.customData = hasCustomOptions();");
                    %>

                    $.ajax({
                        type: "POST",
                        data: JSON.stringify(params),
                        url: config.imageBase + "api/customfields/answers",
                        dataType: "json",
                        contentType: "application/json"
                    });
                }

                <%
                        this.Response.Write("var Invoice = new Object();");
                        this.Response.Write("var Products = [];");
                        this.Response.Write("var ProductsObj = new Object();");
                        this.Response.Write("Invoice.InvoiceID = " + SepFunctions.Session_Invoice_ID() + ";");
                        this.Response.Write("Invoice.UserID = \"" + SepFunctions.Session_User_ID() + "\";");
                        this.Response.Write("Invoice.Status = 0;");
                        this.Response.Write("Invoice.OrderDate = \"" + DateTime.Today + "\";");
                        this.Response.Write("Invoice.ModuleID = 41;");
                        this.Response.Write("ProductsObj[\"ProductID\"] = ProductID;");
                        this.Response.Write("ProductsObj[\"Quantity\"] = $('#Quantity').val();");
                        this.Response.Write("Products.push(ProductsObj);");
                        this.Response.Write("Invoice.Products = Products;");
                        this.Response.Write("Invoice.EmailInvoice = false;");
                        this.Response.Write("Invoice.LinkID = 0;");
                        this.Response.Write("Invoice.StoreID = StoreID;");
                        this.Response.Write("Invoice.PortalID = " + SepFunctions.Get_Portal_ID() + ";");
                %>

                $.ajax({
                    type: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    },
                    data: JSON.stringify(Invoice),
                    url: config.imageBase + "api/invoices",
                    dataType: "json",
                    contentType: "application/json",

                    error: function (xhr) {
                        alert("There has been an error loading data." + debugMsg("\n\n" + xhr.responseText));
                    },

                    success: function () {
                        document.location.href = "<%= this.GetInstallFolder() %>viewcart.aspx";
                    }
                });
            }

            return false;
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">

        <div id="AddWishList" style="display: none;" title="Add to Wish List">
            <div id="AddWishListMsg">
            </div>
        </div>

        <table width="100%">
            <caption>
                <span ID="ProductName" runat="server"></span>
            </caption>
        </table>

        <table width="100%" cellpadding="5" align="center" id="ProductDetTable">
            <tbody>
                <tr>
                    <td valign="top" id="ProductDetImages">
                        <sep:ContentImages ID="Images" runat="server" />
                    </td>
                    <td width="100%" valign="top">
                        <span ID="FullDescription" runat="server"></span>
                        <br>
                        <br>
                        <span class="DetailCaption">Details</span><br>
                        <hr width="100%" size="1">
                        <table width="100%" align="center">
                            <tbody>
                                <tr id="ManufacturerData" runat="server">
                                    <td width="30%" valign="top">Manufacturer</td>
                                    <td width="70%" valign="top">
                                        <span ID="Manufacturer" runat="server"></span>
                                    </td>
                                </tr>
                                <tr id="ModelData" runat="server">
                                    <td width="30%" valign="top">Model</td>
                                    <td width="70%" valign="top">
                                        <span ID="Model" runat="server"></span>
                                    </td>
                                </tr>
                                <tr id="WeightData" runat="server">
                                    <td width="30%" valign="top">Weight</td>
                                    <td width="70%" valign="top">
                                        <span ID="Weight" runat="server"></span>
                                    </td>
                                </tr>
                                <tr id="DimensionsData" runat="server">
                                    <td width="30%" valign="top">Dimensions (L+W+H)</td>
                                    <td width="70%" valign="top">
                                        <span ID="Dimensions" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" valign="top">Price</td>
                                    <td width="70%" valign="top">
                                        <span ID="Price" runat="server"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />

                        <span ID="LitQty" runat="server"></span>

                        <div id="CustomOptions" runat="server">
                            <span class="DetailCaption">Options:</span>
                            <hr />
                            <table width="100%" align="center">
                                <tbody>
                                    <%
                                        var cCustomFields = new SepCityControls.CustomFields();
                                        cCustomFields.ModuleID = 41;
                                        cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("ProductID");
                                        cCustomFields.UserID = SepFunctions.Session_User_ID();
                                        this.Response.Write(cCustomFields.Render());
                                    %>
                                    <tr id="QuantityRow" runat="server" visible="true">
                                        <td width="30%" valign="top" style="padding: 5px 0 5px 0">Quantity</td>
                                        <td width="70%" valign="top" style="padding: 5px 0 5px 0">
                                            <select id="Quantity" runat="server" clientidmode="Static" class="form-control">
                                            </select>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                        </div>

                        <table cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Button ID="AddCartButton" cssclass="btn btn-success" runat="server" Text="Add to Cart" UseSubmitBehavior="false" />
                                    </td>
                                    <td style="padding-left: 20px;">
                                        <asp:Button ID="WishListButton" cssclass="btn btn-light" runat="server" Text="Add to Wish List" UseSubmitBehavior="false" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td valign="top" id="SaleColumn" runat="server">
                        <div style="color: #ffffff; height: 57px; text-align: center; width: 57px;" id="SavePercent" runat="server"></div>
                    </td>
                </tr>
            </tbody>
        </table>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </div>
</asp:content>