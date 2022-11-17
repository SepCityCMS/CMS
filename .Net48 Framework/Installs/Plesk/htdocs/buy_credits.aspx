<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="buy_credits.aspx.cs" inherits="wwwroot.buy_credits" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#ListContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="Purchase Credits" ShowHeaderWhenEmpty="true" OnRowCommand="ListContent_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Credits" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("NumCredits") %> Credits
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Price") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Buy Now" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <asp:LinkButton ID="BuyNow" runat="server" Text="Buy Now" CommandName="AddCart" CommandArgument='<%#
                this.Eval("CreditID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>