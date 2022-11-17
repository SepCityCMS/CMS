<%@ page title="Manage Web Pages" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_pages.aspx.cs" inherits="wwwroot.portals_pages" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>
    <script type="text/javascript">
        function popupURL(url) {
            document.getElementById("JQueryDialog").setAttribute("title", "Web Page URL");
            document.getElementById("JQueryDialog").innerHTML = "<p align=\"center\">" + url + "</p>";
            openModal('JQueryDialog', 400, 250);
        }

        function clickEnabled(id, enabled) {
            if (enabled == 'Yes') {
                document.getElementById(id).innerHTML = 'No';
                document.getElementById(id).setAttribute("onclick", "clickEnabled('" + id + "', 'No'); return false;");
            } else {
                document.getElementById(id).innerHTML = 'Yes';
                document.getElementById(id).setAttribute("onclick", "clickEnabled('" + id + "', 'Yes'); return false;");
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 999;
            cAdminModuleMenu.MenuID = SepCommon.SepCore.Strings.ToString(MenuID);
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Manage Web Pages"></span>
        </h2>

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="panel panel-default" id="PageManageGridView" runat="server">
			<div class="panel-heading">
				<div class="row">
					<div class="col-lg-6">
						<div class="input-group">
							<select id="FilterDoAction" runat="server" class="form-control" ClientIDMode="Static">
								<option value="">Select an Action</option>
									<option value="DeletePages">Delete Pages</option>
                    </select>
							<span class="input-group-btn">
							<button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'PageID') == false) {return false} else">Go!</button>
						</span>
					</div>
				</div>
				<div class="col-lg-6">
					<div class="input-group">
						<input type="text" ID="ModuleSearch" runat="server" placeholder="Search for..." onKeyPress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}"  class="form-control" />
						<span class="input-group-btn">
							<button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Go!</button>
						</span>
					</div>
				</div>
			</div>
		</div>

            <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                          CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
                          OnSorting="ManageGridView_Sorting" EnableViewState="True" OnRowCommand="ManageGridView_RowCommand">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="PageID<%#
                Eval("UniqueID").ToString()%>" value="<%#
                Eval("UniqueID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield ItemStyle-Width="20px">
                        <itemtemplate>
                            <a href="<%#
                "portals_pages_" + Convert.ToString(Eval("PageID").ToString() == "201" ? "link_" : "") +
                "modify.aspx?UniqueID=" + Eval("UniqueID").ToString() + "&MenuID=" +
                Convert.ToString(SepCommon.SepCore.Request.Item("MenuID") != "" ? SepCommon.SepCore.Request.Item("MenuID") : "3") +
                "&PortalID=" + SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:templatefield ItemStyle-Width="20px">
                        <itemtemplate>
                            <button type="button" class="btn btn-success" onclick="window.open('templatedesigner/default.aspx?TemplateID=<%= sTemplateID %>&PageID=<%#Eval("UniqueID").ToString()%>&PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>');">Designer</button>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Page Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="LinkText">
                        <ItemTemplate>
                            <%#
                Eval("LinkText").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield HeaderText="Order" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="70px">
                        <itemtemplate>
                            <p align="center">
                                <asp:ImageButton ID="btnUp" runat="Server" ImageUrl="images/moveup.png" CommandName="MoveUp" CommandArgument='<%#
                SepCommon.SepCore.Request.Item("MenuID") + "||" + SepCommon.SepCore.Request.Item("ModuleID") + "||" + Eval("UniqueID").ToString() +
                "||" + Eval("RowNumber").ToString() + "||" + Eval("MenuID").ToString()%>' />
                                <asp:ImageButton ID="btnDown" runat="Server" ImageUrl="images/movedown.png" CommandName="MoveDown" CommandArgument='<%#
                SepCommon.SepCore.Request.Item("MenuID") + "||" + SepCommon.SepCore.Request.Item("ModuleID") + "||" + Eval("UniqueID").ToString() +
                "||" + Eval("RowNumber").ToString() + "||" + Eval("MenuID").ToString()%>' />
                            </p>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Enabled" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="70px" SortExpression="Enabled">
                        <ItemTemplate>
                            <p align="center">
                                <asp:LinkButton ID="EnableLink" Text='<%#
                Convert.ToString(Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Enable")) == true ? "Yes" : "No")%>' runat="server" CommandName="EnableLink" CommandArgument='<%#
                SepCommon.SepCore.Request.Item("MenuID") + "||" + SepCommon.SepCore.Request.Item("ModuleID") + "||" +
                Convert.ToString(Convert.ToBoolean(Eval("Enable")) == true ? "No" : "Yes") + "||" + Eval("UniqueID").ToString()%>'></asp:LinkButton>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield HeaderText="URL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="30px">
                        <itemtemplate>
                            <%# Convert.ToString(Eval("PageID").ToString() == "200" ? "<center><a href=\"javascript:void(0)\" onclick=\"popupURL('" + GetInstallFolder() + "viewpage.aspx?UniqueID=" + Eval("UniqueID").ToString() + "');return false;\">URL</a></center>" : "")%>
                        </itemtemplate>
                    </asp:templatefield>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>