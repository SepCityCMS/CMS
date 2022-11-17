<%@ Page title="API Calls" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" 
    CodeBehind="api_calls.aspx.cs" Inherits="wwwroot.api_calls" %>

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
            cAdminModuleMenu.ModuleID = 988;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

            <h2>
                <span ID="PageHeader" runat="server" Text="Manage API Calls"></span>
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
									    <option value="DeleteChains">Delete API Calls</option>
                        </select>
							    <span class="input-group-btn">
							    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'APIID') == false) {return false} else">Go!</button>
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
                                <input type="checkbox" id="ChainID<%#
                    Eval("APIID").ToString()%>" value="<%#
                    Eval("APIID").ToString()%>" onclick="gridviewSelectRow(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:templatefield ItemStyle-Width="20px">
                            <itemtemplate>
                                <a href="api_calls_modify.aspx?APIID=<%#
                    Eval("APIID").ToString()%>">
                                    <img src="../images/public/edit.png" alt="Edit" />
                                </a>
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:TemplateField HeaderText="Method" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Method">
                            <ItemTemplate>
                                <%#
                    Eval("Method").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="ApiURL">
                            <ItemTemplate>
                                <%#
                    Eval("ApiURL").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:content>