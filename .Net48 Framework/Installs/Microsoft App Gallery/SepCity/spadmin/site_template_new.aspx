<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_new.aspx.cs" inherits="wwwroot.site_template_new" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link href="../js/jquery/jquery.miniColors.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery/jquery.miniColors.js" type="text/javascript"></script>
    <script src="../js/site_template.js" type="text/javascript"></script>
    <span ID="HeaderScript" runat="server"></span>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 984;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <div id="DivDynHrefFrame" style="display: none; overflow: hidden;" title="Insert Dyanmic Functions"></div>
        <div id="SaveHrefFrame" style="display: none; overflow: hidden;" title="Save Template"></div>

        <div id="TemplateToolbar" style="background-color: #dddddd; height: 140px; width: 100%;">
            <span ID="Toolbar" runat="server"></span>
        </div>

        <iframe style="height: 540px; width: 100%;" name="TemplateFrame" id="TemplateFrame" frameborder="0" src="site_template_builder.aspx"></iframe>
            </div>
    </asp:Panel>
</asp:content>