<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="vouchers.aspx.cs" inherits="wwwroot.vouchers1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            restyleGridView("#ListContent");
            restyleGridView("#NewestContent");
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 65;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="true"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>voucher/<%#
                this.Eval("VoucherID") %>/<%#
                this.Format_ISAPI(this.Eval("BuyTitle")) %>/"><%#
                this.Eval("BuyTitle") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Short Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("ShortDescription") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sale Price" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Format_Currency(this.Eval("SalePrice")) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="NewestContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="Latest Vouchers">
        <Columns>
            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>voucher/<%#
                this.Eval("VoucherID") %>/<%#
                this.Format_ISAPI(this.Eval("BuyTitle")) %>/"><%#
                this.Eval("BuyTitle") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Short Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("ShortDescription") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sale Price" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Format_Currency(this.Eval("SalePrice")) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>