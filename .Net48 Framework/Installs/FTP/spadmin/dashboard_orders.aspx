<%@ page title="Dashboard" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="dashboard_orders.aspx.cs" inherits="wwwroot.dashboard_orders" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function monthlyOrdersChart(result) {

            var outputHTML = '';

            $.each(result,
                function (i) {
                    outputHTML += result[i].MonthName + '<br />';
                    outputHTML += '<div class="progress">';
                    outputHTML += '<div class="progress-bar" role="progressbar" aria-valuenow="' +
                        result[i].Percentage +
                        '" aria-valuemin="0" aria-valuemax="100" style="width: ' +
                        result[i].Percentage +
                        '%;">' +
                        result[i].NumOrders +
                        '</div>';
                    outputHTML += '</div>';
                });

            $("#monthlyChart").css('padding', '10px');
            $("#monthlyChart").html(outputHTML);

        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">
        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 980;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Dashboard (Invoice Statistics)"></span>
        </h2>

        <table width="100%">
            <tr>
                <td valign="top">
                    <div class="GridViewStyle">
                        <asp:GridView ID="RecentInvoicesGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                      CssClass="GridViewStyle" Caption="Most Recent Invoices">
                            <Columns>
                                <asp:templatefield ItemStyle-Width="20px">
                                    <itemtemplate>
                                        <a href="invoices_modify.aspx?InvoiceID=<%#
                Eval("InvoiceID")%>">
                                            <img src="../images/public/edit.png" alt="Edit" />
                                        </a>
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("UserName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("FirstName")%> <%#
                Eval("LastName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Ordered" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("OrderDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount Paid" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("TotalPaid")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <br />

                    <div class="GridViewStyle">
                        <asp:GridView ID="MonthlyTotalsGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                      CssClass="GridViewStyle" Caption="Monthly Income Totals">
                            <Columns>
                                <asp:TemplateField HeaderText="Month" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("MonthName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Recurring" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("TotalRecurring")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total One-Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("TotalUnitPrice")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Handling Charges" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("TotalHandlingPrice")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("TotalPaid")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td valign="top">
                    <div id="monthlyChart" class="ui-widget ui-widget-content ui-corner-all" style="height: 750px; width: 420px;"></div>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            restyleGridView("#RecentInvoicesGridView");
            restyleGridView("#MonthlyTotalsGridView");

            var monthlyUrl = config.imageBase + "api/invoices/monthlyorders";

            $.ajax({
                dataType: "jsonp",
                url: monthlyUrl,
                jsonp: "$callback",
                success: monthlyOrdersChart
            });
        </script>
            </div>
    </asp:Panel>
</asp:content>