﻿<%@ page title="Real Estate" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="realestate.aspx.cs" inherits="wwwroot.realestate" %>

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
            cAdminModuleMenu.ModuleID = 32;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Manage the Real Estate"></span>
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
									<option value="DeleteProperties">Delete Properties</option>
                        <option value="MarkActive">Mark as Active</option>
                        <option value="MarkPending">Mark as Pending</option>
                    </select>
							<span class="input-group-btn">
							<button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'PropertyID') == false) {return false} else">Go!</button>
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
                            <input type="checkbox" id="PropertyID<%#
                Eval("PropertyID").ToString()%>" value="<%#
                Eval("PropertyID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield ItemStyle-Width="20px">
                        <itemtemplate>
                            <a href="realestate_modify.aspx?PropertyID=<%#
                Eval("PropertyID").ToString()%>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Title">
                        <ItemTemplate>
                            <%#
                Eval("Title").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Property Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="PropertyType">
                        <ItemTemplate>
                            <%#
                Get_Property_Type(Eval("PropertyType").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="City">
                        <ItemTemplate>
                            <%#
                Eval("City").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="State">
                        <ItemTemplate>
                            <%#
                Eval("State").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Username">
                        <ItemTemplate>
                            <a href="members_modify.aspx?UserID=<%#Eval("UserID").ToString()%>"><%#Eval("UserName").ToString()%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Status">
                        <ItemTemplate>
                            <%#
                Translate_Status(Eval("Status").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>