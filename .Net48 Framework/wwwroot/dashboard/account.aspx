<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" codebehind="account.aspx.cs" inherits="wwwroot.account1" %>

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
                        <h2>Login / Signup</h2>
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
                        <!-- Activity Trend -->
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Monthly Signup Trend</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content2">
                                    <div id="signup_trend" style="height: 300px; width: 100%;"></div>
                                </div>
                            </div>
                        </div>
                        <!-- /Activity Trend -->
                        <!-- Top 10 Used Activities -->
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Monthly Login Trend</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content2">
                                    <div id="login_trend" style="height: 300px; width: 100%;"></div>
                                </div>
                            </div>
                        </div>
                        <!-- /Top 10 Used Activities -->
                        <div style="clear: both"></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="GridViewStyle">
                                <asp:GridView ID="RecentSignupsGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                    CssClass="GridViewStyle" Caption="Most Recent Signups">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <a href="../spadmin/members_modify.aspx?UserID=<%#
                this.Eval("UserID") %>">
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
                                        <asp:TemplateField HeaderText="Signup Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("CreateDate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("City") %>, <%#
                this.Eval("State") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <br />

                            <div class="GridViewStyle">
                                <asp:GridView ID="RecentLoginsGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                    CssClass="GridViewStyle" Caption="Most Recent Logins">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <a href="../spadmin/members_modify.aspx?UserID=<%#
                this.Eval("UserID") %>">
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
                                        <asp:TemplateField HeaderText="Last Login Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("LastLogin") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("City") %>, <%#
                this.Eval("State") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="GridViewStyle">
                                <asp:GridView ID="ActiveMembersGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                    CssClass="GridViewStyle" Caption="Most Active Members">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <a href="../spadmin/members_modify.aspx?UserID=<%#
                this.Eval("UserID") %>">
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
                                        <asp:TemplateField HeaderText="# Of Activities" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("ActCount") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <%#
                this.Eval("City") %>, <%#
                this.Eval("State") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <!-- content ends here -->
                    </div>
                </div>
            </div>
        </div>

        <span ID="TrendData" runat="server"></span>

        <script type="text/javascript">
            restyleGridView("#RecentSignupsGridView");
            restyleGridView("#RecentLoginsGridView");
            restyleGridView("#ActiveMembersGridView");
        </script>
    </asp:Panel>
</asp:content>