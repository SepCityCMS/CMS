<%@ page title="Shopping Mall Orders" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shopstores_orders.aspx.cs" inherits="wwwroot.shopstores_orders" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
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
            <span ID="PageHeader" runat="server" Text="Stores Orders"></span>
        </h2>

            <span class="successNotification" id="successNotification">
                <span ID="DeleteResult" runat="server"></span>
            </span>

            <div class="panel panel-default" id="PageManageGridView" runat="server">
			    <div class="panel-heading">
				    <div class="mb-3">
				    <div class="col-lg-12">
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
                        <asp:templatefield HeaderText="View" ItemStyle-Width="20px">
                            <itemtemplate>
                                <a href="shopstores_orders_view.aspx?InvoiceID=<%#
                    Eval("InvoiceID").ToString()%>">
                                    View
                                </a>
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="FirstName">
                            <ItemTemplate>
                                <%#
                    Eval("FirstName").ToString()%> <%#
                    Eval("LastName").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Username">
                            <ItemTemplate>
                                <%#
                    Eval("Username").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Status">
                            <ItemTemplate>
                                <%#
                    Eval("StatusText").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="OrderDate">
                            <ItemTemplate>
                                <%#
                    Format_Date(Eval("OrderDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
</div>
    </asp:Panel>
</asp:content>