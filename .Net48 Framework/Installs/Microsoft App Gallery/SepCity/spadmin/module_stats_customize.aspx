<%@ page title="Module Statistics" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="module_stats_customize.aspx.cs" inherits="wwwroot.module_stats_customize" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function showModules(objField) {
            if (document.getElementById('FieldFilterDiv') === null) {
                var searchIFrame =
                    '<iframe style="width:100%; height: 310px;" id="UserSearchFrame" src="filter-modules.aspx?PopulateField=' + objField + '&include=Members" frameborder="0" />';
                var $searchDiv = $('<div title="Select a Module" id="FieldFilterDiv" style="display:none;">' +
                    searchIFrame +
                    '</div>');

                $('body').append($searchDiv);
                openDialog('FieldFilterDiv', 270, 400);
            } else {
                $('#FieldFilterDiv').remove();
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

<asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 981;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Customize the Module Statistics</h4>

            <table width="500">
                <tr>
                    <td valign="top">
                        <input type="text" id="Field1" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field2" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field3" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field4" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field5" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field1');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field2');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field3');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field4');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field5');return false;" /></div>
                    </td>
                </tr><tr>
                    <td valign="top">
                        <input type="text" id="Field6" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field7" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field8" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field9" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field10" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field6');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field7');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field8');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field9');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field10');return false;" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <input type="text" id="Field11" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field12" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field13" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field14" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field15" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field11');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field12');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field13');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field14');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field15');return false;" /></div>
                    </td>
                </tr><tr>
                    <td valign="top">
                        <input type="text" id="Field16" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field17" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field18" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field19" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field20" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field16');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field17');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field18');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field19');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field20');return false;" /></div>
                    </td>
                </tr><tr>
                    <td valign="top">
                        <input type="text" id="Field21" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field22" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field23" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field24" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field25" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field21');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field22');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field23');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field24');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field25');return false;" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <input type="text" id="Field26" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field27" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field28" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field29" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                    <td valign="top">
                        <input type="text" id="Field30" runat="server" class="form-control" style="width:100px;" ClientIDMode="Static" />
                    </td>
                </tr><tr>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field26');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field27');return false;" /></div>
                    </td>
                    <td valign="top">
                        <asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="
        showModules('Field28');return false;" />
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field29');return false;" /></div>
                    </td>
                    <td valign="top">
                        <div class="mb-3"><asp:Button CssClass="btn btn-primary" runat="server" Text="Change" OnClientClick="showModules('Field30');return false;" /></div>
                    </td>
                </tr>
            </table>
        </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>