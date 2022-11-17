<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_open_file.aspx.cs" inherits="wwwroot.site_template_open_file" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/site_template.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/textarea_editor/edit_area_full.js"></script>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                $("#EditBox").height($(document).height() - 220);
                $("#EditBox").width($(document).width() - 100);
            });

        window.onresize = function () {
            $("#EditBox").height($(document).height() - 220);
            $("#EditBox").width($(document).width() - 100);
        };
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 984;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">File Editor</h4>
                <input type="hidden" ID="FilePath" runat="server" />

                <div class="mb-3">
                    <textarea ID="EditBox" runat="server" class="form-control"></textarea>
                </div>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>