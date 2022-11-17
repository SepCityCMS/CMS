<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="events.aspx.cs" inherits="wwwroot.events" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function changeEventType() {
            document.location.href = '<%= this.GetInstallFolder() %>events.aspx?EventDate='+encodeURIComponent('<%= SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("EventDate")) %>')+'&EventType=' + encodeURIComponent($('#EventType').val());
        }
        $(document).ready(function () {
                restyleGridView("#ListContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <div style="margin: 0 auto; width: 500px;">
        <span ID="drawCalendar" runat="server"></span>
    </div>

    <br />

    <div style="clear: both"></div>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <p id="EventTypeRow" runat="server">
        Filter by Event Type:
        <select id="EventType" runat="server" class="form-control inline-block" width="250" onchange="changeEventType()" clientidmode="static">
            <option value="">All Event Types</option>
        </select>
    </p>

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
        Caption="Events" ShowHeaderWhenEmpty="true">
        <Columns>
            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>event/<%#
                this.Eval("EventID") %>/<%#
                this.Format_ISAPI(this.Eval("Subject")) %>/"><%#
                this.Eval("Subject") %></a> <%#
                Convert.ToString(this.Eval("UserID").ToString() == this.Session_UserID() ? " (<a href=\"" + this.GetInstallFolder() + "events_modify.aspx?EventID=" + this.Eval("EventID") + "\">Edit</a>)" : "") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Event Type" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("EventType") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Event Date / Time" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Format_Date(this.Eval("EventDate").ToString()) %> (<%#
                this.Eval("BegTime").ToString() %> - <%#
                this.Eval("EndTime").ToString() %>)
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>