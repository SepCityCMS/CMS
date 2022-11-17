<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="viewcart.aspx.cs" inherits="wwwroot.viewcart" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
	<script type="text/javascript">
	function clearCart(force) {
		if(force == false) {
			confirm('Are you sure you want to empty the shopping cart?', function() {clearCart(true);});
			return false;
		} else {
			$('#EmptyCartButton').removeAttr("onclick");
			$('#EmptyCartButton').click();
			return true;
		}
	}
	</script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span class="successNotification" id="successNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="GridViewStyle">
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
            CssClass="GridViewStyle" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(DataBinder.Eval(Container.DataItem, "UnitPrice") != "xxblankxx" ? this.Eval("ProductName") : "") %>
                        <asp:Literal ID="CustomOptions" runat="server" Text='<%#
                this.GetCustomOptions(this.Eval("ProductID").ToString()) %>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(DataBinder.Eval(Container.DataItem, "UnitPrice") != "xxblankxx" ? this.Eval("UnitPrice") : "") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(DataBinder.Eval(Container.DataItem, "UnitPrice") != "xxblankxx" ? "<input type=\"text\" name=\"Qty" + this.Eval("InvoiceProductID") + "\" id=\"Qty" + this.Eval("InvoiceProductID") + "\" value=\"" + this.Eval("Quantity") + "\" class=\"CartQty\" />" : this.Eval("ProductName")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("TotalPriceNoHandling") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <br />

    <div class="CartButtons">
        <asp:Button ID="ContinueButton" runat="server" Text="Continue Shopping" CssClass="btn btn-dark" OnClick="ContinueButton_Click" />
        <asp:Button ID="EmptyCartButton" runat="server" Text="Empty Cart" ClientIdMode="static" CssClass="btn btn-danger" OnClick="EmptyCartButton_Click" onClientClick="return clearCart(false);" />
        <asp:Button ID="UpdateCartButton" runat="server" Text="Update Cart" CssClass="btn btn-success" OnClick="UpdateCartButton_Click" />
        <asp:Button ID="CheckoutButton" runat="server" Text="Checkout" CssClass="btn btn-primary" OnClick="CheckoutButton_Click" />
    </div>
</asp:content>