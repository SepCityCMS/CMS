<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userpages_pages.aspx.cs" inherits="wwwroot.userpages_pages" %>

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

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="GridViewStyle">
        <div class="GridViewFilter">
            <div class="GridViewFilterLeft">
                <select id="FilterDoAction" runat="server" class="GridViewAction" clientidmode="Static">
                    <option value="">Select an Action</option>
                    <option value="DeletePages">Delete Pages</option>
                </select>
                <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'PageID') == false) {return false} else">GO</button>
            </div>
            <div class="GridViewFilterRight">
            </div>
        </div>

        <input type="hidden" id="UniqueIDs" runat="server" clientidmode="Static" value="" />

        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="true"
            CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
            EnableViewState="True" OnRowCommand="ManageGridView_RowCommand">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20px">
                    <HeaderTemplate>
                        <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" id="PageID<%#
                this.Eval("PageID").ToString() %>"
                            value="<%#
                this.Eval("PageID").ToString() %>"
                            onclick="gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="userpages_pages_modify.aspx?PageID=<%#
                this.Eval("PageID").ToString() %>">
                            <img src="<%= this.GetInstallFolder(true) + "images/" %>public/edit.png" alt="Edit" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("PageTitle") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <p align="center">
                            <asp:ImageButton ID="btnUp" runat="Server" ImageUrl="/spadmin/images/moveup.png" CommandName="MoveUp" CommandArgument='<%#
                this.Eval("PageID") + "||" + this.Eval("RowNumber") %>' />
                            <asp:ImageButton ID="btnDown" runat="Server" ImageUrl="/spadmin/images/movedown.png" CommandName="MoveDown" CommandArgument='<%#
                this.Eval("PageID") + "||" + this.Eval("RowNumber") %>' />
                        </p>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>
</asp:content>