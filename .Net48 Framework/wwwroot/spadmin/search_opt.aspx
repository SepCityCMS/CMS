<%@ page title="Search Optimization" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="search_opt.aspx.cs" inherits="wwwroot.search_opt" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 3;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Search Optimization</h4>
                <div class="mb-3">
                    XML Sitemap | <a href="sitemap.zip">Download Sitemap Generator</a> | <a href="http://www.google.com/support/webmasters/" target="_blank">Help</a> | <a href="http://www.google.com/webmasters/" target="_blank">Submit to Google</a>
                    <br />
                    Your Google sitemap file is located in <span id="SitemapDirectory" runat="server"></span>
                    <br />
                    Copy and paste your google sitemap file into the field below.
                </div>

                <div class="mb-3">
                    <textarea ID="SiteMap" runat="server" style="height:calc(100vh - 270px);" class="form-control"></textarea>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>

        <div class="mb-3">Help improve your backlinks to your website by using AutoSubmit to submit your website to hundreds of link directories.<br /><a href="autosubmit.zip">Download AutoSubmit</a>
        </div>
    </asp:Panel>
</asp:content>