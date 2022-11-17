<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_tenant_search.aspx.cs" inherits="wwwroot.realestate_tenant_search" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.BirthDate.ClientID, "false", "true", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Quick-Check Tenants</h1>

    <div class="mb-3">
        <div class="col-lg-6">
            <div class="input-group" style="width: 100%;">
                <input type="text" id="TenantNumber" runat="server" ckass="form-control" placeholder="Last 4 digits of ID / Social Security Number" onkeypress="if(checkEnter(event) == false){document.getElementById('SearchButton').click();return checkEnter(event);}" />
            </div>
        </div>
        <div class="col-lg-6">
            <div class="input-group">
                <input type="text" id="BirthDate" runat="server" ckass="form-control" placeholder="Date of Birth" onkeypress="if(checkEnter(event) == false){document.getElementById('SearchButton').click();return checkEnter(event);}" />
                <span class="input-group-btn">
                    <asp:Button ID="SearchButton" CssClass="btn btn-light" runat="server" Text="Search" OnClick="SearchButton_Click" />
                </span>
            </div>
        </div>
    </div>

    <br />

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="SearchResults" runat="server">
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
            CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast" EnableViewState="True">
            <Columns>
                <asp:TemplateField HeaderText="Tenant Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <a href="realestate_tenant_view.aspx?TenantID=<%#
                this.Eval("TenantID").ToString() %>">
                            <%#
                this.Eval("TenantName").ToString() %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tenant Number" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("TenantNumber").ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Birth Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%# SepCommon.SepCore.Strings.FormatDateTime(Convert.ToDateTime(this.Eval("BirthDate")), SepCommon.SepCore.Strings.DateNamedFormat.ShortDate) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>
</asp:content>