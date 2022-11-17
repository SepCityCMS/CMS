<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="order_stats.aspx.cs" inherits="wwwroot.order_stats" %>

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

    <span class="successNotification" id="successNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
        OnSorting="ManageGridView_Sorting" EnableViewState="True">
        <Columns>
            <asp:TemplateField HeaderText="Invoice ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="InvoiceID">
                <ItemTemplate>
                    <a href='<%= this.GetInstallFolder() %>order_view.aspx?InvoiceID=<%#
                this.Eval("InvoiceID") %>'><%#
                this.Eval("InvoiceID") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Order Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="OrderDate">
                <ItemTemplate>
                    <%#
                this.Eval("OrderDate") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Order Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Status">
                <ItemTemplate>
                    <%#
                this.Eval("StatusText") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Paid" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="TotalPaid">
                <ItemTemplate>
                    <%#
                this.Eval("TotalPaid") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>