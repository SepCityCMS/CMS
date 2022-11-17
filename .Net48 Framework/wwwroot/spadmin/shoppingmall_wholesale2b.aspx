<%@ page title="Shopping Mall" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shoppingmall_wholesale2b.aspx.cs" inherits="wwwroot.shoppingmall_wholesale2b" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 41;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Wholesale2b Feeds"></span>
        </h2>

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle" id="PageManageGridView" runat="server">
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                    <select id="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                        <option value="">Select an Action</option>
                        <option value="DeleteFeeds">Delete Feed(s)</option>
                    </select>
                    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'FeedID') == false) {return false} else">GO</button>
                </div>
                <div class="GridViewFilterRight">
                </div>
            </div>

            <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                          CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="FeedID<%#
                Eval("FeedID").ToString()%>" value="<%#
                Eval("FeedID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield ItemStyle-Width="20px">
                        <itemtemplate>
                            <a href="shoppingmall_wholesale2b_modify.aspx?FeedID=<%#
                Eval("FeedID").ToString()%>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Feed Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FeedtName">
                        <ItemTemplate>
                            <%#
                Eval("FeedName").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feed URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FeedURL">
                        <ItemTemplate>
                            <%#
                Eval("FeedURL").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>