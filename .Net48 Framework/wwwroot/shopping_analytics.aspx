<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_analytics.aspx.cs" inherits="wwwroot.shopping_analytics" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div class="page-title">
        <div class="title_left">
            <h3></h3>
        </div>

        <div class="title_right">
            <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
            </div>
        </div>
    </div>

    <div class="clearfix"></div>

    <div class="panel panel-default">
        <div class="panel-body">
            <ul class="nav nav-pills">
                <li class="nav-item" role="presentation" id="tabConfigure"><a class="nav-link" href="shopping_my_store.aspx">Configure Store</a></li>
                <li class="nav-item" role="presentation" id="tabProducts"><a class="nav-link" href="shopping_my_products.aspx">My Products</a></li>
                <li class="nav-item" role="presentation" id="tabOrders"><a class="nav-link" href="shopping_my_orders.aspx">My Orders</a></li>
                <li class="nav-item" role="presentation" id="tabShipping"><a class="nav-link" href="shopping_shipping.aspx">Shipping Methods</a></li>
                <li class="nav-item" role="presentation" id="tabAnalytics"><a class="nav-link btn-info" href="shopping_analytics.aspx">Analytics</a></li>
            </ul>
        </div>
    </div>

    <div class="panel-body">
        <div id="sectionConfigure">

            <span class="successNotification" id="successNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <div class="mb-3">
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Analytics (<span id="DashYear" runat="server"></span>)</h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li><a href="javascript:void(0)" onclick="changeDashYear()">Change Year</a></li>
                                    </ul>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <!-- content starts here -->
                            <div class="GridViewStyle">
                                <asp:GridView ID="RecentInvoicesGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                    CssClass="GridViewStyle" Caption="Most Recent Invoices">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <a href="shopping_order_modify.aspx?InvoiceID=<%#
                this.Eval("InvoiceID") %>">View
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("UserName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("FirstName") %> <%#
                this.Eval("LastName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Ordered" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("OrderDate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount Paid" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("TotalPaid") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="mb-3">
                            <div class="col-md-12">
                                <div class="GridViewStyle">
                                    <asp:GridView ID="MonthlyTotalsGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                        CssClass="GridViewStyle" Caption="Monthly Income Totals">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Month" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <%#
                this.Eval("MonthName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Recurring" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <%#
                this.Eval("TotalRecurring") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total One-Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <%#
                this.Eval("TotalUnitPrice") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handling Charges" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <%#
                this.Eval("TotalHandlingPrice") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <%#
                this.Eval("TotalPaid") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            .
                            <!-- content ends here -->
                        </div>
                    </div>
                </div>
            </div>

            <span id="TrendData" runat="server"></span>

            <script type="text/javascript">
                restyleGridView("#RecentInvoicesGridView");
                restyleGridView("#MonthlyTotalsGridView");
            </script>
        </div>
    </div>
</asp:content>