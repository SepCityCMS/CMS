<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_view_store.aspx.cs" inherits="wwwroot.shopping_view_store" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
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

        function orderProduct(ProductID) {
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
                this.Response.Write("ProductsObj[\"Quantity\"] = 1;");
                this.Response.Write("Products.push(ProductsObj);");
                this.Response.Write("Invoice.Products = Products;");
                this.Response.Write("Invoice.EmailInvoice = false;");
                this.Response.Write("Invoice.LinkID = 0;");
                this.Response.Write("Invoice.StoreID = 0;");
                this.Response.Write("Invoice.PortalID = " + SepFunctions.Get_Portal_ID() + ";");
            %>

            $.ajax({
                type: "POST",
                headers: { "Content-Type": "application/json", "Accept": "application/json" },
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

            return false;
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="AddWishList" style="display: none;" title="Add to Wish List">
        <div id="AddWishListMsg">
        </div>
    </div>

    <asp:GridView ID="ListProducts" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" Width="100%" BorderWidth="0" ShowHeader="false"
        AllowPaging="true" OnPageIndexChanging="ListProducts_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100" ItemStyle-BorderWidth="0">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>shopping/<%#
                this.Eval("ProductID") %>/<%#
                this.Format_ISAPI(this.Eval("ProductName")) %>/">
                        <img src='<%#
                this.Eval("DefaultPicture") %>'
                            border="0" alt='<%#
                this.Eval("ProductName") %>' />
                    </a>
                    <br />
                    <br />
                    <a href="<%= this.GetInstallFolder() %>shopping/<%#
                this.Eval("ProductID") %>/<%#
                this.Format_ISAPI(this.Eval("ProductName")) %>/">See Details</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-BorderWidth="0">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>shopping/<%#
                this.Eval("ProductID") %>/<%#
                this.Format_ISAPI(this.Eval("ProductName")) %>/"
                        class="SMProductTitle"><%#
                this.Eval("ProductName") %></a>
                    <%#
                Convert.ToString(this.Eval("ShortDescription").ToString() != "" ? "<br />" + this.Eval("ShortDescription") : "") %>
                    <br />
                    <br />
                    <span class="SMUnitPrice">Price: <%#
                this.Format_Price(this.Eval("SalePrice").ToString(), this.Eval("UnitPrice").ToString(), this.Eval("RecurringPrice").ToString(), this.Eval("RecurringCycle").ToString()) %></span>
                    <br />
                    <div id="SaleRow" runat="server" visible='<%#
                this.Show_Sale_Row(this.Eval("SalePrice").ToString(), SepCommon.SepCore.Request.Item("StoreID")) %>'>
                        <span class="SMSalePrice">Sale Price: <%#
                this.Format_Sale_Price(this.Eval("SalePrice").ToString(), this.Eval("RecurringPrice").ToString(), this.Eval("RecurringCycle").ToString()) %></span>
                        <%#
                this.Get_Sale_Percentage(this.Eval("SalePrice").ToString(), this.Eval("UnitPrice").ToString(), this.Eval("RecurringPrice").ToString()) %>
                    </div>
                    <br />
                    <asp:Button ID="AddCartButton" runat="server" Text="Add to Cart" OnClientClick='<%#
                this.orderProduct(this.Eval("ProductID").ToString(), SepCommon.SepCore.Request.Item("StoreID")) %>'
                        UseSubmitBehavior="false" />
                    <asp:Button ID="WishListButton" runat="server" Text="Add to Wish List" OnClientClick='<%#
                Convert.ToString(this.Session_User_ID() != "" ? "saveWishList(\"" + this.Eval("ProductID") + "\");return false;" : "document.location.href=\"login.aspx\";return false;") %>'
                        UseSubmitBehavior="false" />
                    <br />
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>