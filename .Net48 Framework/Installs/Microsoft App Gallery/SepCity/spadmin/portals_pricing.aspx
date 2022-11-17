<%@ page title="Portal Pricing Plans" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_pricing.aspx.cs" inherits="wwwroot.spadmin.portals_pricing" %>

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
            cAdminModuleMenu.ModuleID = 60;
            this.Response.Write(cAdminModuleMenu.Render());
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Manage Pricing Plans"></span>
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
                                <option value="DeletePortals">Delete Portals</option>
                            </select>
                            <span class="input-group-btn">
                                <button class="btn btn-light" id="RunAction" runat="server" onserverclick="RunAction_Click" onclick="if(ExecuteAction(this, 'PortalID') == false) {return false} else">Go!</button>
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

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
                CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <ItemTemplate>
                            <a href="portals_pricing_modify.aspx?PlanID=<%#
                this.Eval("PlanID").ToString() %>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Plan Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("PlanName").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>