<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" codebehind="sales.aspx.cs" inherits="wwwroot.sales" %>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">
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

        <div class="mb-3">
            <div class="col-md-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Sales</h2>
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
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="GridViewStyle">
                                <asp:GridView ID="RecentInvoicesGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                    CssClass="GridViewStyle" Caption="Most Recent Invoices">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <a href="../spadmin/invoices_modify.aspx?InvoiceID=<%#
                this.Eval("InvoiceID") %>">
                                                    <img src="../images/public/edit.png" alt="Edit" />
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
                        <div class="col-md-6 col-sm-6 col-xs-12">
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

        <span ID="TrendData" runat="server"></span>

        <script type="text/javascript">
            restyleGridView("#RecentInvoicesGridView");
            restyleGridView("#MonthlyTotalsGridView");
        </script>
    </asp:Panel>
</asp:content>