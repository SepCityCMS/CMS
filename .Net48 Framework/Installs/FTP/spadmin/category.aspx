<%@ page title="Categories" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="category.aspx.cs" inherits="wwwroot.category" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/jquery/jquery.FileTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                loadCategoryTree();

                // Adjust Heights for the Category Tree and IFrame
                $('#categorynav')
                    .bind('load',
                        function () {
                            $('#categorynav').height($(window).height() - 108);
                        });

                $('#CategoryMenuTree').height(($(window).height() - 130) + 'px');

                window.onresize = function () {
                    $('#CategoryMenuTree').height(($(window).height() - 130) + 'px');
                    $('#categorynav').height($(window).height() - 108);
                };
            });

        function loadCategoryTree() {
            $('#CategoryMenuTree')
                .fileTree({
                    root: '',
                    script:
                        'menu_categories.aspx?ModuleID=<%=SepCommon.SepCore.Request.Item("ModuleID")%>&PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>',
                    returnFolderHref: true
                },
                    function (sID) {
                        document.getElementById("categorynav").src = document.getElementById(sID).href;
                    });
        }

        function deleteCategories(force) {
            if (force == false) {
                confirm('Are you sure you want to delete the selected categories?', function () { deleteCategories(true); });
                return false;
            } else {
                $('#RunAction').removeAttr("onclick");
                $('#RunAction').click();
                return true;
            }
        }
    </script>
    <style type="text/css">
        div.pagecontent {
            overflow: hidden;
        }
    </style>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            if (SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0)
            {
                var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
                cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                Response.Write(cAdminModuleMenu.Render()); 
            }
        %>

		<div class="col-md-12" id="modPageContent" runat="server">

        <h2>Categories</h2>

        <span class="successNotification" id="successNotification">
			<span ID="DeleteResult" runat="server"></span>
		</span>

        <table id="maintable" width="100%">
            <tr>
                <td width="190" valign="top" nowrap="nowrap">
					<div class="input-group">
                        <select id="FilterDoAction" runat="server" ClientIDMode="Static" width="130" class="form-control">
                            <option value="">Select an Action</option>
                            <option value="DeleteCategories">Delete Categories</option>
                            <option value="MassUpdate">Mass Update</option>
                        </select>
					    <span class="input-group-btn">
                            <asp:Button ID="RunAction" runat="server" cssclass="btn btn-light" Text="GO" onclick="RunAction_Click" OnClientClick="if (document.getElementById('FilterDoAction').value == 'DeleteCategories') { return deleteCategories(false); } else { return true; };" ClientIDMode="static" />
				        </span>
			        </div>
                    <div id="CategoryMenuTree" style="overflow:auto;">
                    </div>
                </td>
                <td valign="top" width="100%">
                    <iframe scrolling="auto" frameborder="0" style="border: medium none; height: 600px; width: 100%;" id="categorynav" runat="server" clientidmode="Static" name="categorynav" src="blank.html"></iframe>
                </td>
            </tr>
        </table>
            </div>
    </asp:Panel>
</asp:content>