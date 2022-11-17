<%@ page title="Advertising" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="advertising.aspx.cs" inherits="wwwroot.advertising" %>

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
            cAdminModuleMenu.ModuleID = 2;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Manage the Advertising"></span>
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
									<option value="DeleteAds">Delete Ads</option>
                            </select>
							<span class="input-group-btn">
							<button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'AdID') == false) {return false} else">Go!</button>
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
                          OnSorting="ManageGridView_Sorting" EnableViewState="True">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="AdID<%#
                Eval("AdID").ToString()%>" value="<%#
                Eval("AdID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield ItemStyle-Width="20px">
                        <itemtemplate>
                            <a href="banners_modify.aspx?AdID=<%#
                Eval("AdID").ToString()%>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Site URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="SiteURL">
                        <ItemTemplate>
                            <%#
                Eval("SiteURL").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Target Zone" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="ZoneName">
                        <ItemTemplate>
                            <%#
                Eval("ZoneName").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Description">
                        <ItemTemplate>
                            <%#
                Eval("Description").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Advertisement Preview" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("AdvertisementPreview").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <img src="<%#
                                            Convert.ToString(DataBinder.Eval(Container.DataItem, "Status") == "ME" || DataBinder.Eval(Container.DataItem, "Status") == "MC" ? "images/icon_red.png" :
                                            Convert.ToString(DataBinder.Eval(Container.DataItem, "Status") == "SD" ? "images/icon_red.png" :
                                            Convert.ToString(DataBinder.Eval(Container.DataItem, "Status") == "ED" ? "images/icon_orange.png" : "images/icon_green.png")))
                                        %>" border="0" alt="Advertisement Status" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>

        <br />

        <table align="center" width="270">
            <tr>
                <td colspan="2">
                    <b>Status Legend:</b>
                </td>
            </tr><tr>
                <td width="20">
                    <img src="images/icon_green.png" alt="Active" title="Active" border="0" />
                </td>
                <td width="250">Ad is currently running (Active)</td>
            </tr><tr>
                <td width="20">
                    <img src="images/icon_yellow.png" alt="Start Date" title="Start Date" border="0" />
                </td>
                <td width="250">Start Date not reached (Pending)</td>
            </tr><tr>
                <td width="20">
                    <img src="images/icon_red.png" alt="Max Clicks / Exposures Reached" title="Max Clicks / Exposures Reached" border="0" />
                </td>
                <td width="250">Max Clicks / Exposures Reached (Ended)</td>
            </tr><tr>
                <td width="20">
                    <img src="images/icon_orange.png" alt="End Date Reached" title="End Date Reached" border="0" />
                </td>
                <td width="250">End Date Reached (Expired)</td>
            </tr>
        </table>
            </div>
    </asp:Panel>
</asp:content>