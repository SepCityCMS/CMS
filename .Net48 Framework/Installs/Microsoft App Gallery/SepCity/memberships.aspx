<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="memberships.aspx.cs" inherits="wwwroot.memberships" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function orderMembership(sid) {
            <%
                this.Response.Write("var Invoice = new Object();");
                this.Response.Write("var Products = [];");
                this.Response.Write("var ProductsObj = new Object();");
                this.Response.Write("Invoice.InvoiceID = " + SepFunctions.Session_Invoice_ID() + ";");
                this.Response.Write("Invoice.UserID = \"" + SepFunctions.Session_User_ID() + "\";");
                this.Response.Write("Invoice.Status = 0;");
                this.Response.Write("Invoice.OrderDate = \"" + DateTime.Today + "\";");
                this.Response.Write("Invoice.ModuleID = 38;");
                this.Response.Write("ProductsObj[\"ProductID\"] = document.getElementById(sid).value;");
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

    <span ID="PageText" runat="server"></span>

    <asp:PlaceHolder runat="server" ID="ListClasses" />

    <div style="clear: both;"></div>
</asp:content>