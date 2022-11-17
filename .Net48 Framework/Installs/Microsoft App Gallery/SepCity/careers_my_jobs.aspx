<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_my_jobs.aspx.cs" inherits="wwwroot.careers_my_jobs" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#ListContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="My Positions" AllowCustomPaging="true">
        <Columns>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>