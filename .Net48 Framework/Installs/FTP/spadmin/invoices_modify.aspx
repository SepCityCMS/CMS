<%@ page title="Invoices" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="invoices_modify.aspx.cs" inherits="wwwroot.invoices_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script src="../js/filters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(OrderDate.ClientID, "false", "true", "")%>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 985;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID"))) cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Invoice</h4>
                <input type="hidden" runat="server" ID="InvoiceID" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="AddProductIDs" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="AddProductName" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="AddUnitPrice" ClientIDMode="Static" />

                <div class="mb-3">
                    <label ID="InvoiceNumberLabel" clientidmode="Static" runat="server" for="InvoiceNumber">Invoice Number:</label>
                    <input type="text" ID="InvoiceNumber" runat="server"  class="form-control" MaxLength="100" ReadOnly="true" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                    <input type="text" name="UserName" id="UserName" runat="server" class="form-control" placeholder="Click to select a user name" onclick="openUserSearch($(this).attr('id'), 'UserID')" />
                    <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="DiscountCodeLabel" clientidmode="Static" runat="server" for="DiscountCode">Discount Code:</label>
                    <input type="text" ID="DiscountCode" runat="server"  class="form-control" MaxLength="100" />
                </div>
                <div class="mb-3">
                    <label ID="StatusLabel" clientidmode="Static" runat="server" for="Status">Order Status:</label>
                    <sep:OrderStatusDropdown ID="Status" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="OrderDateLabel" clientidmode="Static" runat="server" for="OrderDate">Order Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="datetimepicker1">
                            <input type="text" id="OrderDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="EmailInvoice" runat="server" /> Email invoice to user when saved.
                </div>

                <span class="successNotification">
					<span ID="NoProductsAdded" runat="server"></span>
				</span>

                <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                              CssClass="GridViewStyle" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <%# Eval("ProductName")%>
                                <%# Convert.ToString(!string.IsNullOrWhiteSpace(Convert.ToString(Eval("StoreName"))) ? "<br />Bought from " + Eval("StoreName") : "")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <input type="hidden" name="ProductID" id="ProductID<%#
                Eval("ProductID")%>" value="<%#
                Eval("ProductID")%>" />
                                <%#
                Eval("UnitPrice")%>
                                <%#
                                    Convert.ToString(Format_Currency(DataBinder.Eval(Container.DataItem, "Handling")) != Format_Currency("0") ? "<br />Plus " + DataBinder.Eval(Container.DataItem, "Handling") + " handling fee" : "")
                                %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <input type="text" name="Qty" style="width: 50px" value="<%#
                Eval("Quantity")%>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <%#
                Eval("TotalPrice")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div class="mb-3">
                    <asp:HyperLink ID="AddProduct" runat="server" NavigateUrl="javascript:openProductSearch('0')" ClientIDMode="Static" Text="Add Product" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>