﻿<%@ page title="Edit Web Page" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_pages_modify.aspx.cs" inherits="wwwroot.portals_pages_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/jquery/jquery.FileTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                $('#AdminMenuTree')
                    .fileTree({ root: '', script: 'menu_mainadmin.aspx' },
                        function (sID) {
                            document.getElementById("adminnav").src = document.getElementById(sID).href;
                        });
                $('#tabGeneral a').addClass('btn-info');
            });

        function showPageMain() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabSEO a').removeClass('btn-info');
            document.getElementById('idPageMain').style.display = '';
            document.getElementById('idPageSEO').style.display = 'none';
        }

        function showPageSEO() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabSEO a').addClass('btn-info');
            document.getElementById('idPageMain').style.display = 'none';
            document.getElementById('idPageSEO').style.display = '';
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 999;
            if(GetModuleID > 0) {
                cAdminModuleMenu.ModuleID = GetModuleID;
            }
            cAdminModuleMenu.MenuID = SepCommon.SepCore.Request.Item("MenuID");
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Add Web Page</h4>
                <input type="hidden" runat="server" ID="PageID" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="ModuleID" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="ShowCat" ClientIDMode="Static" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="showPageMain();">Main</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabSEO">
                                    <a class="nav-link" href="javascript:void(0)" onclick="showPageSEO();">SEO</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="idPageSEO" style="display: none;">
                            <div class="mb-3">
                                <label ID="PageTitleLabel" clientidmode="Static" runat="server" for="SEOPageTitle">Page Title:</label>
                                <input type="text" ID="SEOPageTitle" runat="server"  class="form-control" MaxLength="100" />
                            </div>
                            <div class="mb-3">
                                <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="SEODescription">Meta Description:</label>
                                <textarea ID="SEODescription" runat="server"  class="form-control"></textarea>
                            </div>
                            <div class="mb-3">
                                <label ID="KeywordsLabel" clientidmode="Static" runat="server" for="SEOKeywords">Meta Keywords:</label>
                                <textarea ID="SEOKeywords" runat="server"  class="form-control"></textarea>
                            </div>
                        </div>
                        <div id="idPageMain">
                            <div class="mb-3">
                                <label ID="EnabledLabel" clientidmode="Static" runat="server" for="LinkText">Enable Web Page:</label>
                                <select runat="server" ID="Enabled" class="form-control">
                                    <option value="1">Yes</option>
                                    <option value="0">No</option>
                                </select>
                            </div>
                            <div class="mb-3">
                                <label ID="LinkTextLabel" clientidmode="Static" runat="server" for="LinkText">Link Text:</label>
                                <input type="text" ID="LinkText" runat="server"  class="form-control" MaxLength="100" />
                                <asp:CustomValidator ID="LinkTextRequired" runat="server" ControlToValidate="LinkText"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Link Text is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="MenuIDLabel" clientidmode="Static" runat="server" for="MenuID">Select a Menu:</label>
                                <sep:MenuDropdown ID="MenuID" runat="server" CssClass="form-control" ShowNotOnAMenu="true" />
                            </div>
                            <div class="mb-3" runat="server" id="CategorySelection">
                                <label ID="CatIDLabel" clientidmode="Static" runat="server" for="CatID">Select a Category:</label>
                                <sep:CategoryDropdown ID="CatID" runat="server" Management="true" />
                            </div>
                            <div class="mb-3" runat="server" id="PortalSelection">
                                <label ID="PortalIDsLabel" clientidmode="Static" runat="server" for="PortalIDs">List web pages in the following portals:</label>
                                <sep:PortalSelection ID="PortalIDs" runat="server" ExcludeMaster="true" />
                            </div>
                            <div class="mb-3">
                                <sep:WYSIWYGEditor Runat="server" ID="PageText" Width="99%" Height="450" Mode="advanced" />
                            </div>
                        </div>
                    </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>