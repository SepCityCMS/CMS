<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_my_properties.aspx.cs" inherits="wwwroot.realestate_my_properties" %>

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

    <h1>My Properties</h1>

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
                            <option value="DeleteProperties">Delete Properties</option>
                        </select>
                        <span class="input-group-btn">
                            <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'PropertyID') == false) {return false} else">Go!</button>
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
                        <input type="checkbox" id="PropertyID<%#
                this.Eval("PropertyID").ToString() %>"
                            value="<%#
                this.Eval("PropertyID").ToString() %>"
                            onclick="gridviewSelectRow(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>realestate_property_modify.aspx?PropertyID=<%#
                this.Eval("PropertyID").ToString() %>">
                            <img src="<%= this.GetInstallFolder(true) + "images/" %>public/edit.png" alt="Edit" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Title">
                    <ItemTemplate>
                        <%#
                this.Eval("Title").ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Property Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="PropertyType">
                    <ItemTemplate>
                        <%#
                this.Get_Property_Type(this.Eval("PropertyType").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="City">
                    <ItemTemplate>
                        <%#
                this.Eval("City").ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="State">
                    <ItemTemplate>
                        <%#
                this.Eval("State").ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tenants" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="ForSale">
                    <ItemTemplate>
                        <%#
                Convert.ToString(Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ForSale")) == false ? "<a href=\"" + this.GetInstallFolder(false) + "realestate_tenants.aspx?PropertyID=" + this.Eval("PropertyID") + "\">View Tenants</a>" : "") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>
</asp:content>