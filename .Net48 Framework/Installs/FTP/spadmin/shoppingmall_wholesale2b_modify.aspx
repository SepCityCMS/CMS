<%@ page title="Shopping Mall" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shoppingmall_wholesale2b_modify.aspx.cs" inherits="wwwroot.shoppingmall_wholesale2b_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        skipRestyling = true;

        function openGeneralOptions() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabPermissions a').removeClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#Permissions").hide();
            $("#Advanced").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openPermissions() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPermissions a').addClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#Advanced").hide();
            $("#Permissions").show();
            restyleFormElements('#Permissions');
        }

        function openAdvanced() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabPermissions a').removeClass('btn-info');
            $('#tabAdvanced a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#Permissions").hide();
            $("#Advanced").show();
            restyleFormElements('#Advanced');
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
            cAdminModuleMenu.ModuleID = 41;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Feed</h4>
                <input type="hidden" runat="server" ID="FeedID" ClientIDMode="Static" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabPermissions">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openPermissions();">Permissions</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabAdvanced">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openAdvanced();">Advanced</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="GeneralOptions">
                            <div class="mb-3">
                                <label ID="FeedNameLabel" clientidmode="Static" runat="server" for="FeedName">Feed Name:</label>
                                <input type="text" ID="FeedName" runat="server"  class="form-control" MaxLength="100" />
                                <asp:CustomValidator ID="FeedNameRequired" runat="server" ControlToValidate="FeedName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Feed Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="FeedURLLabel" clientidmode="Static" runat="server" for="FeedURL">Feed URL:</label>
                                <input type="text" ID="FeedURL" runat="server"  class="form-control" MaxLength="2048" />
                                <asp:CustomValidator ID="FeedURLRequired" runat="server" ControlToValidate="FeedURL"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Feed URL is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                        </div>
                        <div id="Permissions" style="display: none">
                            <div class="mb-3">
                                <label ID="AccessKeysLabel" ClientIDMode="Static" runat="server">Access keys required to access this category:</label>
                                <sep:AccessKeySelection ID="AccessKeysSelection" runat="server" text="|1|,|2|,|3|,|4|" ClientIDMode="Static" />
                                <br />
                                <asp:CheckBox runat="server" ID="AccessKeysHide" /> Hide if users does not have a selected access key from above.
                            </div>
                        </div>
                        <div id="Advanced" style="display: none">
                            <div class="mb-3">
                                <label ID="PortalSelectionLabel" ClientIDMode="Static" runat="server">Select the portals to show this category in:</label>
                                <sep:PortalSelection ID="PortalSelection" runat="server" text="|-1|" />
                                <br />
                                <asp:CheckBox runat="server" ID="ShareContent" /> Share the content this category with the selected portals from above.
                                <br />
                                <asp:CheckBox runat="server" ID="ExcludePortalSecurity" /> Overwrite portal manager security.
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