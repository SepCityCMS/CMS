﻿<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="discounts_manage.aspx.cs" inherits="wwwroot.discounts_manage" %>

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

    <h2>
        <span ID="PageHeader" runat="server" Text="My Coupons"></span>
    </h2>

    <span class="successNotification" id="successNotification">
        <span ID="DeleteResult" runat="server"></span>
    </span>

    <div class="panel panel-default" id="PageManageGridView" runat="server">
        <div class="panel-heading">
            <div class="row">
                <div class="col-lg-6">
                    <div class="input-group">
                        <select id="FilterDoAction" runat="server" class="form-control" clientidmode="Static">
                            <option value="">Select an Action</option>
                            <option value="DeleteDiscounts">Delete Discounts</option>
                        </select>
                        <span class="input-group-btn">
                            <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'DiscountID') == false) {return false} else">Go!</button>
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
                        <input type="checkbox" id="DiscountID<%#
                this.Eval("DiscountID").ToString() %>"
                            value="<%#
                this.Eval("DiscountID").ToString() %>"
                            onclick="gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="discounts_modify.aspx?CatID=<%#
                this.Eval("CatID").ToString() %>&DiscountID=<%#
                this.Eval("DiscountID").ToString() %>">
                            <img src="<%= this.GetInstallFolder(true) + "images/" %>public/edit.png" alt="Edit" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Label Text" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="LabelText">
                    <ItemTemplate>
                        <%#
                this.Eval("LabelText").ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="CompanyName">
                    <ItemTemplate>
                        <%#
                this.Eval("CompanyName").ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="PriceOff">
                    <ItemTemplate>
                        <%#
                this.Format_Price(this.Eval("MarkOffPrice").ToString(), this.Eval("MarkOffType").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>
</asp:content>