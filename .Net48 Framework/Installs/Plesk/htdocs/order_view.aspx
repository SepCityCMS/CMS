<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="order_view.aspx.cs" inherits="wwwroot.order_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">View Invoice</h4>

        <div class="mb-3">
            <label id="InvoiceNumberLabel" clientidmode="Static" runat="server" for="InvoiceNumber">Invoice Number:</label>
            <input type="text" id="InvoiceNumber" runat="server" class="form-control" maxlength="100" readonly="true" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="DiscountCodeLabel" clientidmode="Static" runat="server" for="DiscountCode">Discount Code:</label>
            <input type="text" id="DiscountCode" runat="server" class="form-control" maxlength="100" readonly="true" />
        </div>
        <div class="mb-3">
            <label id="StatusLabel" clientidmode="Static" runat="server" for="Status">Order Status:</label>
            <input type="text" id="Status" runat="server" class="form-control" maxlength="100" readonly="true" />
        </div>
        <div class="mb-3">
            <label id="OrderDateLabel" clientidmode="Static" runat="server" for="OrderDate">Order Date:</label>
            <input type="text" id="OrderDate" runat="server" class="form-control" maxlength="100" readonly="true" />
        </div>

        <span class="successNotification">
            <span ID="NoProductsAdded" runat="server"></span>
        </span>

        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
            CssClass="GridViewStyle" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("ProductName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <input type="hidden" name="ProductID" id="ProductID<%#
                this.Eval("ProductID") %>"
                            value="<%#
                this.Eval("ProductID") %>" />
                        <%#
                this.Eval("UnitPrice") %>
                        <%#
                Convert.ToString(this.Format_Currency(DataBinder.Eval(Container.DataItem, "Handling")) != this.Format_Currency("0") ? "<br />Plus " + DataBinder.Eval(Container.DataItem, "Handling") + " handling fee" : "") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Quantity") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("TotalPrice") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>