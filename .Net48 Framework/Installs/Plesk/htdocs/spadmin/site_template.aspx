<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template.aspx.cs" inherits="wwwroot.site_template" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link type="text/css" rel="stylesheet" href="../skins/public/styles/prettyPhoto.css" />
    <script type="text/javascript" src="../js/jquery/jquery.prettyPhoto.js"></script>
    <script src="../js/site_template.js" type="text/javascript"></script>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("a[rel^='prettyPhoto']").prettyPhoto();
        });

        function deleteTemplate(id, force) {
            if (force == false) {
                confirm('Are you sure you want to delete this template?', function () { deleteTemplate(id, true); });
                return false;
            } else {
                $('#' + id).removeAttr("onclick");
                $('#' + id)[0].click();
                return true;
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 984;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

            <h2>
                <span ID="PageHeader" runat="server" Text="Site Templates"></span>
            </h2>

            <span class="successNotification" id="successNotification">
                <span ID="DeleteResult" runat="server"></span>
            </span>

            <asp:ListView ID="ListContent" runat="server">
                <LayoutTemplate>
                    <div id="siteTemplates">
                        <ul>
                            <li id="itemPlaceholder" runat="server"></li>
                        </ul>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <div><%# Eval("TemplateName")%></div>
                        <span class="btn-group" role="group" aria-label="Basic example">
                            <button type="button" class="btn btn-primary" onclick="document.location.href='site_template_modify.aspx?TemplateID=<%#
                    Eval("TemplateID")%>&PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>';">Options</button>
                            <button type="button" class="btn btn-success" onclick="window.open('templatedesigner/default.aspx?TemplateID=<%#
                    Eval("TemplateID")%>&PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>');">Designer</button>
                            <button type="button" class="btn btn-danger" onclick="if(deleteTemplate('Delete<%# Eval("TemplateID")%>', false) == true) {document.location.href='site_template.aspx?DoAction=Delete&TemplateID=<%#
                    Eval("TemplateID")%>&PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>" id="Delete<%# Eval("TemplateID")%>'};">Delete</button>
                        </span>
                        <p align="center">
                            <a href="<%# Eval("ScreenShotLarge")%>" rel="prettyPhoto" title="<%# Eval("TemplateName")%>">
                                <img alt="<%# Eval("TemplateName")%>" src="<%# Eval("ScreenShot")%>" border="0" width="220" height="175" />
                            </a>
                        </p>
                        <p align="center"><%# Convert.ToString(Convert.ToBoolean(Eval("useTemplate")) == true ? "Current Template" : "<a href=\"site_template.aspx?DoAction=ChangeActive&TemplateID=" + Eval("TemplateID") + "&PortalID=" + SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")) + "\">Use Template</a>") %></p>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </asp:Panel>
</asp:content>