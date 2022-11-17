<%@ page title="Advertising" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="advertising_modify.aspx.cs" inherits="wwwroot.advertising_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script src="../js/filters.js" type="text/javascript"></script>
    <script type="text/javascript">
        skipRestyling = true;

        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(StartDate.ClientID, "false", "true", "")%>;
            <%=SepFunctions.Date_Picker(EndDate.ClientID, "false", "true", "$('#StartDate.ClientID').val()")%>;
            $('#<%=StartDate.ClientID%>')
                .on("dp.change",
                    function (e) {
                        $('#<%=EndDate.ClientID%>').data("DateTimePicker").minDate(e.date);
                });
            $('#<%=EndDate.ClientID%>')
                .on("dp.change",
                    function (e) {
                        $('#<%=StartDate.ClientID%>').data("DateTimePicker").maxDate(e.date);
                });
            $('#tabGeneral a').addClass('btn-info');
            restyleFormElements('#GeneralOptions');
        });

        function openGeneralOptions() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').addClass('btn-info');
            $('#tabClicks a').removeClass('btn-info');
            $('#tabTarget a').removeClass('btn-info');
            $("#ClicksExposures").hide();
            $("#TargetOptions").hide();
            $("#GeneralOptions").show();
            restyleFormElements('#GeneralOptions');
        }

        function openClicksExposures() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabClicks a').addClass('btn-info');
            $('#tabTarget a').removeClass('btn-info');
            $("#GeneralOptions").hide();
            $("#TargetOptions").hide();
            $("#ClicksExposures").show();
            restyleFormElements('#ClicksExposures');
        }

        function openTargetOptions() {
            $('#FieldFilterDiv').remove();
            $('#tabGeneral a').removeClass('btn-info');
            $('#tabClicks a').removeClass('btn-info');
            $('#tabTarget a').addClass('btn-info');
            $("#GeneralOptions").hide();
            $("#ClicksExposures").hide();
            $("#TargetOptions").show();
            restyleFormElements('#TargetOptions');
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 2;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Advertisement</h4>
                <input type="hidden" runat="server" ID="AdID" />
                <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />

                    <div class="panel panel-default" id="PageManageGridView" runat="server">
                        <div class="panel-body">
                            <ul class="nav nav-pills">
                                <li class="nav-item" role="presentation" id="tabGeneral">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openGeneralOptions();">General Options</a>
                                </li>
                                <li class="nav-item" role="presentation" id="tabClicks">
                                    <a class="nav-link" href="javascript:void(0)" onclick="openClicksExposures();">Clicks / Exposures</a>
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
                                <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                                <input type="text" name="UserName" id="UserName" runat="server" class="form-control" placeholder="Click to select a user name" onclick="openUserSearch($(this).attr('id'), 'UserID')" />
                                <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="SiteURLLabel" clientidmode="Static" runat="server" for="SiteURL">Site URL (ex. http://www.google.com):</label>
                                <input type="text" ID="SiteURL" runat="server"  class="form-control" MaxLength="100" Text="http://" />
                            </div>
                            <div class="mb-3">
                                <label ID="ImageLabel" clientidmode="Static" runat="server" for="ImageUpload">Select an Image to Upload:</label>
                                <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control"></asp:FileUpload>
                                <asp:Image ID="AdImage" runat="server" Visible="false" CssClass="imageEntry" />
                                <span id="ImageHTML" runat="server"></span>
                            </div>
                            <div class="mb-3">
                                <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description (Internal User Only):</label>
                                <textarea ID="Description" runat="server"  class="form-control"></textarea>
                                <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="ZoneLabel" clientidmode="Static" runat="server" for="Zone">Select a zone to target the advertisement to:</label>
                                <sep:ZoneDropdown ID="Zone" runat="server" ModuleID="2" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label ID="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Select a Start Date:</label>
                                <div class="form-group">
                                    <div class="input-group date" id="StartDateDiv">
                                        <input type="text" id="StartDate" class="form-control" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label ID="Label1" clientidmode="Static" runat="server" for="EndDate">Select a End Date:</label>
                                <div class="form-group">
                                    <div class="input-group date" id="EndDateDiv">
                                        <input type="text" id="EndDate" class="form-control" runat="server" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label ID="HTMLCodeLabel" clientidmode="Static" runat="server" for="HTMLCode">HTML Code (This will overwrite the clicks/exposures, site URL and Image Upload):</label>
                                <textarea ID="HTMLCode" runat="server"  class="form-control"></textarea>
                            </div>
                        </div>
                        <div id="ClicksExposures" style="display: none">
                            <div class="mb-3">
                                <label ID="MaximumClicksLabel" clientidmode="Static" runat="server" for="MaximumClicks">Maximum Clicks (Enter "-1" for Unlimited):</label>
                                <input type="text" ID="MaximumClicks" runat="server"  class="form-control" MaxLength="100" Text="-1" />
                                <asp:CustomValidator ID="MaximumClicksRequired" runat="server" ControlToValidate="MaximumClicks"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Maximum Clicks is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="MaximumExposuresLabel" clientidmode="Static" runat="server" for="MaximumExposures">Maximum Exposures (Enter "-1" for Unlimited):</label>
                                <input type="text" ID="MaximumExposures" runat="server"  class="form-control" MaxLength="100" Text="-1" />
                                <asp:CustomValidator ID="MaximumExposuresRequired" runat="server" ControlToValidate="MaximumExposures"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Maximum Exposures is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="TotalClicksLabel" clientidmode="Static" runat="server" for="TotalClicks">Total Clicks:</label>
                                <input type="text" ID="TotalClicks" runat="server"  class="form-control" MaxLength="100" Text="0" />
                                <asp:CustomValidator ID="TotalClicksRequired" runat="server" ControlToValidate="TotalClicks"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Total Clicks is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                            <div class="mb-3">
                                <label ID="TotalExposuresLabel" clientidmode="Static" runat="server" for="TotalExposures">Total Exposures:</label>
                                <input type="text" ID="TotalExposures" runat="server"  class="form-control" MaxLength="100" Text="0" />
                                <asp:CustomValidator ID="TotalExposuresRequired" runat="server" ControlToValidate="TotalExposures"
                                                     ClientValidationFunction="customFormValidator" ErrorMessage="Total Exposures is required."
                                                     ValidateEmptyText="true" Display="Dynamic">
                                </asp:CustomValidator>
                            </div>
                        </div>
                        <div id="TargetOptions" style="display: none">
                            <div class="mb-3" runat="server" id="CategoriesRow">
                                <label ID="CategoriesLabel" clientidmode="Static" runat="server" for="Categories">Target Ad to Categories:</label>
                                <sep:CategorySelection ID="Categories" runat="server" text="|0|" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3" runat="server" id="WebPagesRow">
                                <label ID="PagesLabel" clientidmode="Static" runat="server" for="Pages">Target Ad to Pages:</label>
                                <sep:PageSelection ID="Pages" runat="server" text="|-1|" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label ID="CountryLabel" clientidmode="Static" runat="server" for="Country">Target to Country:</label>
                                <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" AllowBlankSelection="true" />
                            </div>
                            <div class="mb-3">
                                <label ID="StateLabel" clientidmode="Static" runat="server" for="State">Target to State / Province:</label>
                                <sep:StateDropdown ID="State" runat="server" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3" runat="server" id="PortalsRow">
                                <label ID="PortalsLabel" clientidmode="Static" runat="server" for="Portals">Target Ad to Portals:</label>
                                <sep:PortalSelection ID="Portals" runat="server" text="|0|" ClientIDMode="Static" />
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