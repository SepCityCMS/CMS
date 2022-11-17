<%@ page title="Category" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="category_modify.aspx.cs" inherits="wwwroot.category_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        skipRestyling = true;

        function openGeneralOptions() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabPermissions a').removeClass('btn-info');
            $('#tabSEO a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#Permissions").hide();
            $("#SEO").hide();
            $("#Advanced").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openPermissions() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPermissions a').addClass('btn-info');
            $('#tabSEO a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#SEO").hide();
            $("#Advanced").hide();
            $("#Permissions").show();
            restyleFormElements('#Permissions');
        }

        function openSEO() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPermissions a').removeClass('btn-info');
            $('#tabSEO a').addClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#Permissions").hide();
            $("#Advanced").hide();
            $("#SEO").show();
            restyleFormElements('#SEO');
        }

        function openAdvanced() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPermissions a').removeClass('btn-info');
            $('#tabSEO a').removeClass('btn-info');
            $('#tabAdvanced a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#Permissions").hide();
            $("#SEO").hide();
            $("#Advanced").show();
            restyleFormElements('#Advanced');
        }

        $(document).ready(function () {
            $('#tabGeneral a').addClass('btn-info');
            restyleFormElements('#GeneralOptions');
            if (parent.$('#CategoryMenuTree').is(":visible")) {
                $('.col-md-12').removeClass('pagecontentsave');
            }
            $('html').css('height', 'calc(100% + 300px)');
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Category</h4>
                <input type="hidden" runat="server" ID="CatID" ClientIDMode="Static" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabPermissions" runat="server">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openPermissions();">Permissions</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabSEO">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openSEO();">SEO</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabAdvanced">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openAdvanced();">Advanced</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="GeneralOptions">
                            <div class="mb-3" id="CategoryRow">
                                <label ID="ListUnderLabel" runat="server" for="ListUnder" ClientIDMode="Static">List this category as sub-category of a category below:</label>
                                <sep:CategoryDropdown ID="ListUnder" runat="server" ClientIDMode="Static" Management="true" />
                            </div>
                            <div class="mb-3">
                                <label ID="CategoryNameLabel" clientidmode="Static" runat="server" for="CategoryName">Category Name:</label>
                                <input type="text" ID="CategoryName" runat="server"  class="form-control" MaxLength="100" ClientIDMode="Static" />
                                <asp:CustomValidator ID="CategoryNameRequired" runat="server" ControlToValidate="CategoryName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Category Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                                <textarea ID="Description" runat="server"  class="form-control"></textarea>
                            </div>
                            <div class="mb-3" ID="CategoryTypeRow" runat="server">
                                <label ID="CategoryTypeLabel" clientidmode="Static" runat="server" for="CategoryType">Category Type:</label>
                                <select runat="server" ID="CategoryType" class="form-control">
                                    <option value="Audio">Audio</option>
                                    <option value="Document">Document</option>
                                    <option value="Image">Image</option>
                                    <option value="Software">Software</option>
                                    <option value="Video">Video</option>
                                </select>
                            </div>
                            <div class="mb-3" id="ModeratorRow" runat="server">
                                <label ID="ModeratorLabel" clientidmode="Static" runat="server" for="Moderator">Moderator:</label>
                                <input type="text" ID="Moderator" runat="server"  class="form-control" />
                            </div>
                            <div class="mb-3" runat="server" id="ModulesRow">
                                <label ID="ModulesLabel" clientidmode="Static" runat="server" for="Modules">Modules to Show Category In:</label>
                                <sep:ModuleSelection ID="Modules" runat="server" ModuleType="Categories" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="ImageLabel" clientidmode="Static" runat="server" for="ImageUpload">Select an Image to Upload:</label>
                                <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control"></asp:FileUpload>
                                <asp:Image ID="CatImage" runat="server" Visible="false" CssClass="imageEntry" />
                                <asp:HyperLink ID="CatImageDelete" runat="server" Visible="false" Text="Delete Image" />
                            </div>
                        </div>
                        <div id="Permissions" style="display: none">
                            <div class="mb-3">
                                <label ID="AccessKeysLabel" ClientIDMode="Static" runat="server">Access keys required to access this category:</label>
                                <sep:AccessKeySelection ID="AccessKeysSelection" runat="server" text="|1|,|2|,|3|,|4|" ClientIDMode="Static" />
                                <br />
                                <asp:CheckBox runat="server" ID="AccessKeysHide" /> Hide if users does not have a selected access key from above.
                            </div>
                            <div class="mb-3">
                                <label ID="WriteKeysLabel" ClientIDMode="Static" runat="server">Access keys required to upload/write content this category:</label>
                                <sep:AccessKeySelection ID="WriteKeysSelection" runat="server" text="|2|,|3|,|4|" ClientIDMode="Static" />
                                <br />
                                <asp:CheckBox runat="server" ID="WriteKeysHide" /> Hide if users does not have a selected access key from above.
                            </div>
                            <div class="mb-3">
                                <label ID="ManageKeysLabel" ClientIDMode="Static" runat="server">Access keys required to manage content this category:</label>
                                <sep:AccessKeySelection ID="ManageKeysSelection" runat="server" text="|2|" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div id="SEO" style="display: none">
                            <div class="mb-3">
                                <label ID="PageTitleLabel" clientidmode="Static" runat="server" for="PageTitle">Page Title:</label>
                                <input type="text" ID="PageTitle" runat="server"  class="form-control" />
                            </div>
                            <div class="mb-3">
                                <label ID="MetaDescriptionLabel" clientidmode="Static" runat="server" for="MetaTagDescription">Meta Description:</label>
                                <textarea ID="MetaTagDescription" runat="server"  class="form-control"></textarea>
                            </div>
                            <div class="mb-3">
                                <label ID="MetaKeywordsLabel" clientidmode="Static" runat="server" for="MetaTagKeywords">Meta Keywords:</label>
                                <textarea ID="MetaTagKeywords" runat="server"  class="form-control"></textarea>
                            </div>
                        </div>
                        <div id="Advanced" style="display: none">
                            <div class="mb-3" id="PortalsRow" runat="server">
                                <label ID="PortalSelectionLabel" ClientIDMode="Static" runat="server">Select the portals to show this category in:</label>
                                <sep:PortalSelection ID="PortalSelection" runat="server" text="|0|" />
                                <br />
                                <asp:CheckBox runat="server" ID="ShareContent" /> Share the content this category with the selected portals from above.
                                <br />
                                <asp:CheckBox runat="server" ID="ExcludePortalSecurity" /> Overwrite portal manager security.
                            </div>
                            <div class="mb-3">
                                <label ID="WeightLabel" clientidmode="Static" runat="server" for="Weight" Text="0">Weight:</label>
                                <input type="text" ID="Weight" runat="server"  class="form-control" />
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