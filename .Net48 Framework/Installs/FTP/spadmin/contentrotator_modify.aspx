<%@ page title="Content Rotator" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="contentrotator_modify.aspx.cs" inherits="wwwroot.contentrotator_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        skipRestyling = true;

        function openGeneralOptions() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabTarget a').removeClass('btn-info');
            $("#TargetOptions").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openTargetOptions() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabTarget a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#TargetOptions").show();
            restyleFormElements('#TargetOptions');
        }

        $(document)
            .ready(function () {
                $('#tabGeneral a').addClass('btn-info');
                restyleFormElements('#GeneralOptions');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 1;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Content</h4>
                <input type="hidden" runat="server" ID="ContentID" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabTarget">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openTargetOptions();">Target Options</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="GeneralOptions">
                            <div class="mb-3">
                                <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description (Internal User Only):</label>
                                <input type="text" ID="Description" runat="server"  class="form-control" />
                                <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="ZoneLabel" clientidmode="Static" runat="server" for="Zone">Select a zone to target the content to:</label>
                                <sep:ZoneDropdown ID="Zone" runat="server" ModuleID="1" ClientIDMode="Static" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label ID="HTMLCodeLabel" clientidmode="Static" runat="server" for="HTMLCode">HTML Code:</label>
                                <sep:WYSIWYGEditor Runat="server" ClientIDMode="Static" Mode="advanced" ID="HTMLCode" Width="99%" Height="450" />
                            </div>
                        </div>
                        <div id="TargetOptions" style="display: none">
                            <div class="mb-3">
                                <label ID="CategoriesLabel" clientidmode="Static" runat="server" for="Categories">Target Ad to Category:</label>
                                <sep:CategorySelection ID="Categories" runat="server" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3" runat="server" id="WebPagesRow">
                                <label ID="PagesLabel" clientidmode="Static" runat="server" for="Pages">Target Ad to Pages:</label>
                                <sep:PageSelection ID="Pages" runat="server" text="|-1|" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3" runat="server" id="PortalsRow">
                                <label ID="PortalsLabel" clientidmode="Static" runat="server" for="Portals">Target Ad to Portals:</label>
                                <sep:PortalSelection ID="Portals" runat="server" text="|-1|" ClientIDMode="Static" />
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