<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="whos_online.aspx.cs" inherits="wwwroot.whos_online" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
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
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <img src='<%#
                this.Eval("DefaultPicture") %>'
                        border="0" alt="" align="left" style="margin-right: 10px;" />
                    <a href="<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>"><%#
                this.Eval("Username") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Location") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>