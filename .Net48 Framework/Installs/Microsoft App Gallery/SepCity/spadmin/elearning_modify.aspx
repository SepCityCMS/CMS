<%@ page title="E-Learning" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="elearning_modify.aspx.cs" inherits="wwwroot.elearning_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
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
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 37;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Course</h4>
                <input type="hidden" runat="server" ID="CourseID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="37" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="37" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="InstructorLabel" clientidmode="Static" runat="server" for="Instructor">Instructor:</label>
                    <input type="text" ID="Instructor" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="InstructorRequired" runat="server" ControlToValidate="Instructor"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Instructor is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="CourseNameLabel" clientidmode="Static" runat="server" for="CourseName">Course Name:</label>
                    <input type="text" ID="CourseName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="CourseNameRequired" runat="server" ControlToValidate="CourseName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Course Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Start Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="StartDateDiv">
                            <input type="text" id="StartDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><asp:checkbox id="DisableStartDate" runat="server" text="Disable Start Date" AutoPostBack="true" OnCheckedChanged="DisableStartDate_Clicked" /></span>
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="EndDateLabel" clientidmode="Static" runat="server" for="EndDate">End Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="EndDateDiv">
                            <input type="text" id="EndDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><asp:checkbox id="DisableEndDate" runat="server" text="Disable End Date" AutoPostBack="true" OnCheckedChanged="DisableEndDate_Clicked" /></span>
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="CreditsLabel" clientidmode="Static" runat="server" for="Credits">Credits:</label>
                    <input type="text" ID="Credits" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="CreditsRequired" runat="server" ControlToValidate="Credits"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Credits is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ImageNameLabel" clientidmode="Static" runat="server" for="ThumbnailImage">Thumbnail Image:</label>
                    <asp:FileUpload ID="ThumbnailImage" runat="server" CssClass="form-control" />
                    <asp:Image ID="ThumbnailImagePreview" runat="server" Visible="false" CssClass="img-fluid img-thumbnail" style="width:200px;display:block;" />
                    <asp:HyperLink ID="CatImageDelete" runat="server" Visible="false" Text="Delete Image" />
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor Runat="server" ID="CourseDescription" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="CoursePriceLabel" clientidmode="Static" runat="server" for="CoursePrice">Course Price:</label>
                    <input type="text" ID="CoursePrice" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="CoursePriceRequired" runat="server" ControlToValidate="CoursePrice"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Course Price is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="RecurringPriceLabel" clientidmode="Static" runat="server" for="RecurringPrice">Recurring Price:</label>
                    <input type="text" ID="RecurringPrice" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="RecurringPriceRequired" runat="server" ControlToValidate="RecurringPrice"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Recurring Price is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="RecurringCycleLabel" clientidmode="Static" runat="server" for="RecurringCycle">Recurring Cycle:</label>
                    <select ID="RecurringCycle" runat="server" class="form-control">
                        <option value="1m">Monthly</option>
                        <option value="3m">3 Months</option>
                        <option value="6m">6 Months</option>
                        <option value="1y">Yearly</option>
                    </select>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>