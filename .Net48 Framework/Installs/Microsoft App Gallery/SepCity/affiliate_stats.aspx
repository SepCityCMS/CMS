<%@ page title="Affiliate Statistics" language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="affiliate_stats.aspx.cs" inherits="wwwroot.affiliate_stats" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="affiliateChart" class="ui-widget ui-widget-content ui-corner-all" style="height: 400px; width: 600px;"></div>

    <script type="text/javascript">
        $.ajax({
            dataType: "json",
            url: config.imageBase + "api/affiliate/affiliatetotals",
            jsonp: "$callback",
            success: affiliateChart
        });
    </script>
</asp:content>