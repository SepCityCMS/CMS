<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="messenger.aspx.cs" inherits="wwwroot.messenger" %>

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

    <span ID="PageText" runat="server"></span>

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
                            <option value="DeleteMessages">Delete Message(s)</option>
                        </select>
                        <span class="input-group-btn">
                            <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'MessageID') == false) {return false} else">Go!</button>
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
                        <input type="checkbox" id="MessageID<%#
                this.Eval("MessageID") %>"
                            value="<%#
                this.Eval("MessageID") %>"
                            onclick="gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Photo" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <img border="0" src='<%#
                this.Eval("DefaultPicture") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Subject">
                    <ItemTemplate>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("ReadNew")) ? "<b>" : "") %>
                        <a href="messenger_display.aspx?MessageID=<%#
                this.Eval("MessageID") %>"><%#
                this.Eval("Subject") %></a>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("ReadNew")) ? "</b>" : "") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FromUsername">
                    <ItemTemplate>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("ReadNew")) ? "<b>" : "") %>
                        <%#
                this.Eval("FromUsername") %>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("ReadNew")) ? "</b>" : "") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Sent" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="DateSent">
                    <ItemTemplate>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("ReadNew")) ? "<b>" : "") %>
                        <%#
                this.Eval("DateSent") %>
                        <%#
                Convert.ToString(Convert.ToBoolean(this.Eval("ReadNew")) ? "</b>" : "") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>
</asp:content>