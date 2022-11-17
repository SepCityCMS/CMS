<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_shipping.aspx.cs" inherits="wwwroot.shopping_shipping" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div class="panel panel-default">
        <div class="panel-body">
            <ul class="nav nav-pills">
                <li class="nav-item" role="presentation" id="tabConfigure"><a class="nav-link" href="shopping_my_store.aspx">Configure Store</a></li>
                <li class="nav-item" role="presentation" id="tabProducts"><a class="nav-link" href="shopping_my_products.aspx">My Products</a></li>
                <li class="nav-item" role="presentation" id="tabOrders"><a class="nav-link" href="shopping_my_orders.aspx">My Orders</a></li>
                <li class="nav-item" role="presentation" id="tabShipping"><a class="nav-link btn-info" href="shopping_shipping.aspx">Shipping Methods</a></li>
                <li class="nav-item" role="presentation" id="tabAnalytics"><a class="nav-link" href="shopping_analytics.aspx">Analytics</a></li>
            </ul>
        </div>
    </div>

    <div class="panel-body">
        <div id="sectionConfigure">

            <h1>
                <span id="PageHeader" runat="server" text="Shipping Methods"></span>
            </h1>

            <ul class="nav nav-tabs">
                <li role="presentation" class="active"><a href="shopping_shipping.aspx">Manage</a></li>
                <li role="presentation"><a href="shopping_shipping_modify.aspx">Add Shipping Method</a></li>
                <li role="presentation"><a href="shopping_shipping_config.aspx">Configure</a></li>
            </ul>

            <span class="successNotification" id="successNotification">
                <span id="DeleteResult" runat="server"></span>
            </span>

            <div class="panel panel-default" id="PageManageGridView" runat="server">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="input-group">
                                <select id="FilterDoAction" runat="server" class="form-control" clientidmode="Static">
                                    <option value="">Select an Action</option>
                                    <option value="DeleteMethods">Delete Shipping Methods</option>
                                </select>
                                <span class="input-group-btn">
                                    <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'MethodID') == false) {return false} else">Go!</button>
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-6">
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
                        <asp:TemplateField ItemStyle-Width="20px">
                            <HeaderTemplate>
                                <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" id="MethodID<%#
                this.Eval("MethodID").ToString() %>"
                                    value="<%#
                this.Eval("MethodID").ToString() %>"
                                    onclick="gridviewSelectRow(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20px">
                            <ItemTemplate>
                                <a href="shopping_shipping_modify.aspx?MethodID=<%#
                this.Eval("MethodID").ToString() %>">
                                    <img src="<%= this.GetInstallFolder(true) + "images/" %>public/edit.png" alt="Edit" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Method Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="MethodName">
                            <ItemTemplate>
                                <%#
                this.Eval("MethodName").ToString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:content>