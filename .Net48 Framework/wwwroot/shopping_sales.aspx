<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_sales.aspx.cs" inherits="wwwroot.shopping_sales" %>
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

        function orderProduct(ProductID, StoreID) {
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

            return false;
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Current Sales</h1>

    <asp:ListView ID="ListProducts" runat="server" ItemPlaceholderID="itemPlaceholder">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="shoping-product">
                <ul>
                    <li>
                        <div class="pro-image">
                            <a href="<%= this.GetInstallFolder() %>shopping/<%#
                this.Eval("ProductID") %>/<%#
                this.Format_ISAPI(this.Eval("ProductName")) %>/">
                                <img src='<%#
                this.Eval("DefaultPicture") %>' border="0" alt='<%#
                this.Eval("ProductName") %>' class="img-fluid" />
                            </a>
                        </div>
                        <div class="pro-text">
                            <h3 class="pro-name"><a href="<%= this.GetInstallFolder() %>shopping/<%#
                this.Eval("ProductID") %>/<%#
                this.Format_ISAPI(this.Eval("ProductName")) %>/"><%#
                this.Eval("ProductName") %></a></h3>
                            <p>
                                <%# Convert.ToString(this.Eval("ShortDescription").ToString() != "" ? "<br />" + this.Eval("ShortDescription") : "") %>
                            </p>
                            <h4 class="main-price">Price: <%#
                this.Format_Price(this.Eval("SalePrice").ToString(), this.Eval("UnitPrice").ToString(), this.Eval("RecurringPrice").ToString(), this.Eval("RecurringCycle").ToString()) %>
                                <span id="SaleRow" runat="server" visible='<%#
                this.Show_Sale_Row(this.Eval("SalePrice").ToString(), this.Eval("UnitPrice").ToString()) %>'><%#
                this.Format_Sale_Price(this.Eval("SalePrice").ToString(), this.Eval("RecurringPrice").ToString(), this.Eval("RecurringCycle").ToString()) %></span></h4>
                            <a class="btn btn-success" href="javascript:void(0)" onclick="<%#
                this.orderProduct(this.Eval("ProductID").ToString(), this.Eval("StoreID").ToString()) %>">Add to Cart</a>
                            <a class="btn btn-secondary" href="javascript:void(0)" onclick="<%#
                Convert.ToString(this.Session_User_ID() != "" ? "saveWishList('" + this.Eval("ProductID") + "');return false;" : "document.location.href='" + this.GetInstallFolder() + "login.aspx';return false;") %>">Add to Wish List</a>
                        </div>
                    </li>
                </ul>
            </div>
            <hr>
        </itemtemplate>
    </asp:ListView>
</asp:content>