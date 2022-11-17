<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_my_orders.aspx.cs" inherits="wwwroot.shopping_my_orders" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

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

            <h1>
                <span id="PageHeader" runat="server" text="My Orders"></span>
            </h1>

            <span class="successNotification" id="successNotification">
                <span id="DeleteResult" runat="server"></span>
            </span>

            <div class="panel panel-default" id="PageManageGridView" runat="server">
                <div class="panel-heading">
                    <div class="mb-3">
                        <div class="col-lg-12">
                            <div class="input-group">
                                <input type="text" id="ModuleSearch" runat="server" placeholder="Search for..." onkeypress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}" class="form-control" />
                                <span class="input-group-btn">
                                    <button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Go!</button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <input type="hidden" id="UniqueIDs" runat="server" clientidmode="Static" value="" />

                <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                    CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
                    OnSorting="ManageGridView_Sorting" EnableViewState="True">
                    <Columns>
                        <asp:TemplateField HeaderText="View" ItemStyle-Width="20px">
                            <ItemTemplate>
                                <a href="shopping_order_modify.aspx?InvoiceID=<%#
                this.Eval("InvoiceID").ToString() %>">View
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FirstName">
                            <ItemTemplate>
                                <%#
                this.Eval("FirstName").ToString() %> <%#
                this.Eval("LastName").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Username">
                            <ItemTemplate>
                                <%#
                this.Eval("Username").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Status">
                            <ItemTemplate>
                                <%#
                this.Eval("StatusText").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="OrderDate">
                            <ItemTemplate>
                                <%#
                this.Format_Date(this.Eval("OrderDate").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:content>