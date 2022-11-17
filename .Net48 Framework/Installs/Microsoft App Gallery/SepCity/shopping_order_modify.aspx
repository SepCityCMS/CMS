﻿<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_order_modify.aspx.cs" inherits="wwwroot.shopping_order_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
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
                    <li class="nav-item" role="presentation" id="tabProducts"><a class="nav-link" href="shopping_my_products.aspx">My Products</a></li>
                    <li class="nav-item" role="presentation" id="tabOrders"><a class="nav-link btn-info" href="shopping_my_orders.aspx">My Orders</a></li>
                    <li class="nav-item" role="presentation" id="tabShipping"><a class="nav-link" href="shopping_shipping.aspx">Shipping Methods</a></li>
                    <li class="nav-item" role="presentation" id="tabAnalytics"><a class="nav-link" href="shopping_analytics.aspx">Analytics</a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            <div id="sectionOrders">

                <div class="ModFormDiv">

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
                        <span id="NoProductsAdded" runat="server"></span>
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
        </div>
</asp:content>