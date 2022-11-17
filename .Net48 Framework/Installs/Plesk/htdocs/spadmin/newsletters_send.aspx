<%@ page title="Newsletters" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="newsletters_send.aspx.cs" inherits="wwwroot.newsletters_send" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        skipRestyling = true;

        function openGeneralOptions() {
            $('#tabGeneral a').addClass('btn-info');
            $('#tabAdvanced a').removeClass('btn-info');
            $("#AdvancedOptions").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openAdvancedOptions() {
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabAdvanced a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#AdvancedOptions").show();
            restyleFormElements('#AdvancedOptions');
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
            cAdminModuleMenu.ModuleID = 24;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <div class="ModFormDiv" ID="SendLetterForm" runat="server">

                <h4 id="ModifyLegend" runat="server">Send Newsletter</h4>

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabAdvanced">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openAdvancedOptions();">Advanced Options</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="panel-body">
                        <div id="AdvancedOptions" style="display: none">
                            <div class="mb-3" runat="server" id="PortalsRow">
                                <label ID="PortalLabel" ClientIDMode="Static" runat="server">Portals a user must signup in to receive this newsletter:</label>
                                <sep:PortalDropdown ID="PortalID" runat="server" text="|-1|" ClientIDMode="Static" CssClass="form-control" ShowAllPortals="true" />
                            </div>
                            <div class="mb-3" runat="server" id="KeyRow">
                                <label ID="AccessKeysLabel" ClientIDMode="Static" runat="server">Access Keys a user must have to receive this newsletter:</label>
                                <sep:AccessKeySelection ID="AccessKeys" runat="server" ClientIDMode="Static" text="|1|,|2|,|3|,|4|" />
                            </div>
                        </div>
                        <div id="GeneralOptions">
                            <div class="mb-3">
                                <label ID="EmailFromLavel" clientidmode="Static" runat="server" for="EmailFrom">Send Email From:</label>
                                <select id="EmailFrom" runat="server" class="form-control">
                                </select>
                            </div>
                            <div class="mb-3">
                                <label ID="EmailToLabel" clientidmode="Static" runat="server" for="EmailTo">Send Email To:</label>
                                <select id="EmailTo" runat="server" class="form-control">
                                    <option value="All">All Newsletters</option>
                                    <option value="AllMembers">All Members</option>
                                    <option value="AllNonCustomers">All Non-Customers</option>
                                    <option value="AllPaidCustomers">All Paid Customers</option>
                                </select>
                            </div>
                            <div class="mb-3">
                                <label ID="EmailSubjectLabel" clientidmode="Static" runat="server" for="EmailSubject">Email Subject:</label>
                                <input type="text" ID="EmailSubject" runat="server"  class="form-control" MaxLength="100" />
                                <asp:CustomValidator ID="EmailSubjectRequired" runat="server" ControlToValidate="EmailSubject"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Email Subject is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="EmailBodyLabel" clientidmode="Static" runat="server" for="EmailBody">Email Body:</label>
                                <sep:WYSIWYGEditor Runat="server" ID="EmailBody" Mode="advanced" Width="99%" Height="450" RelativeURLs="true" />
                            </div>
                        </div>
                    </div>
            </div>
            <div class="button-to-bottom">
                <asp:Button CssClass="btn btn-primary" ID="SendButton" runat="server" Text="Send Newsletter" onclick="SendButton_Click" />
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>