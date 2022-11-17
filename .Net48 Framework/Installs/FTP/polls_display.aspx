<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="polls_display.aspx.cs" inherits="wwwroot.polls_display" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <!-- morris.js -->
    <link href="/dashboard/vendors/morris.js/morris.css" rel="stylesheet">
    <script src="/dashboard/vendors/raphael/raphael.min.js"></script>
    <script src="/dashboard/vendors/morris.js/morris.min.js"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1 runat="server" id="PollQuestion"></h1>

    <sep:ContentImages ID="PollPHoto" runat="server" />

    <div id="PollOptions" runat="server" clientidmode="Static"></div>

    <div id="PollResults" class="ui-widget ui-widget-content ui-corner-all" style="display: none; height: auto; width: 420px;"></div>

    <br />

    <div class="mb-3">
        <asp:HyperLink ID="resultsLink" runat="server" ClientIDMode="Static" Text="View Results" />
    </div>

    <span ID="TrendData" runat="server"></span>
    <%
        var cSocialShare = new SepCityControls.SocialShare();
        this.Response.Write(cSocialShare.Render());
    %>
</asp:content>