<%@ page title="Dashboard" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="dashboard.aspx.cs" inherits="wwwroot.dashboard" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function dailySignupsChart(result) {

            var outputHTML = '';

            $.each(result,
                function (i) {
                    outputHTML += result[i].DayName + '<br />';
                    outputHTML += '<div class="progress">';
                    outputHTML += '<div class="progress-bar" role="progressbar" aria-valuenow="' +
                        result[i].Percentage +
                        '" aria-valuemin="0" aria-valuemax="100" style="width: ' +
                        result[i].Percentage +
                        '%;">' +
                        result[i].NumSignups +
                        '</div>';
                    outputHTML += '</div>';
                });

            $("#dailyChart").css('padding', '10px');
            $("#dailyChart").html(outputHTML);
        }

        function monthlySignupsChart(result) {

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
                        result[i].NumSignups +
                        '</div>';
                    outputHTML += '</div>';
                });

            $("#monthlyChart").css('padding', '10px');
            $("#monthlyChart").html(outputHTML);
        }

        function monthlyActivityChart(result) {

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
                        result[i].NumSignups +
                        '</div>';
                    outputHTML += '</div>';
                });

            $("#activityChart").css('padding', '10px');
            $("#activityChart").html(outputHTML);

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
            <span ID="PageHeader" runat="server" Text="Dashboard (Member Statistics)"></span>
        </h2>

        <table width="100%">
            <tr>
                <td valign="top">
                    <div class="GridViewStyle">
                        <asp:GridView ID="RecentSignupsGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                      CssClass="GridViewStyle" Caption="Most Recent Signups">
                            <Columns>
                                <asp:templatefield ItemStyle-Width="20px">
                                    <itemtemplate>
                                        <a href="members_modify.aspx?UserID=<%#
                Eval("UserID")%>">
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
                                <asp:TemplateField HeaderText="Signup Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("CreateDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("City")%>, <%#
                Eval("State")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <br />

                    <div class="GridViewStyle">
                        <asp:GridView ID="RecentLoginsGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                      CssClass="GridViewStyle" Caption="Most Recent Logins">
                            <Columns>
                                <asp:templatefield ItemStyle-Width="20px">
                                    <itemtemplate>
                                        <a href="members_modify.aspx?UserID=<%#
                Eval("UserID")%>">
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
                                <asp:TemplateField HeaderText="Last Login Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("LastLogin")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("City")%>, <%#
                Eval("State")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <br />

                    <div class="GridViewStyle">
                        <asp:GridView ID="ActiveMembersGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                      CssClass="GridViewStyle" Caption="Most Active Members">
                            <Columns>
                                <asp:templatefield ItemStyle-Width="20px">
                                    <itemtemplate>
                                        <a href="members_modify.aspx?UserID=<%#
                Eval("UserID")%>">
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
                                <asp:TemplateField HeaderText="# Of Activities" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("ActCount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%#
                Eval("City")%>, <%#
                Eval("State")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td valign="top">
                    <h4>Daily Signups</h4>
                    <div id="dailyChart" class="ui-widget ui-widget-content ui-corner-all" style="height: 450px; width: 420px;"></div>

                    <br />

                    <h4>Monthly Signups</h4>
                    <div id="monthlyChart" class="ui-widget ui-widget-content ui-corner-all" style="height: 750px; width: 420px;"></div>

                    <br />

                    <h4>Monthly Activities</h4>
                    <div id="activityChart" class="ui-widget ui-widget-content ui-corner-all" style="height: 750px; width: 420px;"></div>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            restyleGridView("#RecentSignupsGridView");
            restyleGridView("#RecentLoginsGridView");
            restyleGridView("#ActiveMembersGridView");

            var dailyUrl = config.imageBase + "api/members/dailysignups";
            var monthlyUrl = config.imageBase + "api/members/monthlysignups";
            var activityUrl = config.imageBase + "api/activities/monthlyactivities";

            $.ajax({
                dataType: "jsonp",
                url: dailyUrl,
                jsonp: "$callback",
                success: dailySignupsChart
            });

            $.ajax({
                dataType: "jsonp",
                url: monthlyUrl,
                jsonp: "$callback",
                success: monthlySignupsChart
            });

            $.ajax({
                dataType: "jsonp",
                url: activityUrl,
                jsonp: "$callback",
                success: monthlyActivityChart
            });
        </script>
            </div>
    </asp:Panel>
</asp:content>