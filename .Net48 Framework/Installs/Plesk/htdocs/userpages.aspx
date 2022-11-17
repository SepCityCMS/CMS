<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userpages.aspx.cs" inherits="wwwroot.userpages1" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            restyleGridView("#ListContent");
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageContent" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 7;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table width="95%" align="center" class="Table">
                        <tr class="TableHeader">
                            <td>
                                <a href="<%= this.GetInstallFolder() %>!<%#
                this.Eval("UserName") %>/"
                                    target="_blank"><%#
                this.Eval("SiteName") %></a>
                            </td>
                            <td align="right">Hits: <%#
                this.Eval("Visits") %></td>
                        </tr>
                        <tr class="TableBody1">
                            <td valign="top" width="100%"><%#
                this.Eval("Description") %></td>
                            <td align="right" valign="top" width="130">
                                <a href="javascript:void(0)" class="btn btn-primary" onclick="window.open('<%= this.GetInstallFolder() %>!<%#
                this.Eval("UserName") %>/')" style="width: 130px">Visit</a>
                                <a href="<%= this.GetInstallFolder() %>refer.aspx?URL=<%= this.UrlEncode(this.GetInstallFolder()) %>!<%#
                this.Eval("UserName") %>%2f" class="btn btn-secondary" style="width: 130px">Refer</a>
                            </td>
                        </tr>
                        <tr class="TableHeader">
                            <td colspan="2">
                                <table width="98%" align="center">
                                    <tr class="TableHeader">
                                        <td width="100%" align="right">
                                            <sep:RatingStars ID="Rating" runat="server" ModuleID="7" LookupID='<%#
                this.Eval("SiteID") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>