<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shopstores_orders_view.aspx.cs" inherits="wwwroot.shopstores_orders_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <%
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 41;
            this.Response.Write(cAdminModuleMenu.Render());
        %>

		<div class="col-md-12 pagecontent">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">View Invoice</h4>
            <div class="mb-3">
                <label id="InvoiceNumberLabel" clientidmode="Static" runat="server" for="InvoiceNumber">Invoice Number:</label>
                <input type="text" id="InvoiceNumber" runat="server" class="form-control" maxlength="100" readonly="true" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                <input type="text" name="UserName" id="UserName" runat="server" class="form-control" readonly="readonly" />
            </div>
            <div class="mb-3">
                <label id="StatusLabel" clientidmode="Static" runat="server" for="Status">Order Status:</label>
                <input type="text" name="Status" id="Status" runat="server" class="form-control" readonly="readonly" />
            </div>
            <div class="mb-3">
                <label id="OrderDateLabel" clientidmode="Static" runat="server" for="OrderDate">Order Date:</label>
                <input type="text" name="OrderDate" id="OrderDate" runat="server" class="form-control" readonly="readonly" />
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
</div>
    </asp:Panel>
</asp:content>