<%@ page title="Recycle Bin" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="recyclebin.aspx.cs" inherits="wwwroot.recyclebin" %>

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
            if (SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0) {
                var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
                cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                Response.Write(cAdminModuleMenu.Render()); 
            }
        %>

		<div class="col-md-12" id="modPageContent" runat="server">

        <h2>
            <span ID="PageHeader" runat="server" Text="Recycle Bin"></span>
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
									<option value="DeleteContent">Delete Content</option>
                        <option value="RestoreContent">Restore Content</option>
                    </select>
							<span class="input-group-btn">
							<button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'UniqueID') == false) {return false} else">Go!</button>
						</span>
					</div>
				</div>
				<div class="col-lg-6">
				</div>
			</div>
		</div>

            <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" ClientIDMode="Static"
                          CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="UniqueID<%#
                Eval("UniqueID").ToString()%>" value="<%#
                Eval("ModuleID").ToString()%>||<%#
                Eval("UniqueID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Module Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("ModuleName").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("Title").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Deleted" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("DateDeleted").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>