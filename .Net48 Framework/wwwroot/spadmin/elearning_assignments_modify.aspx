<%@ page title="E-Learning" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="elearning_assignments_modify.aspx.cs" inherits="wwwroot.elearning_assignments_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(DueDate.ClientID, "false", "true", "")%>;
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

                <h4 id="ModifyLegend" runat="server">Add Assignment</h4>
                <input type="hidden" runat="server" ID="HomeID" />
                <div class="mb-3">
                    <label ID="CourseIDLabel" clientidmode="Static" runat="server" for="CourseID">Select a Course:</label>
                    <select ID="CourseID" runat="server" class="form-control">
                        <option value="">Select a Course</option>
                    </select>
                    <asp:CustomValidator ID="CourseIDRequired" runat="server" ControlToValidate="CourseID"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Course is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="VideoFileLabel" clientidmode="Static" runat="server" for="VideoFile">Select a Presentation Video:</label>
                    <sep:UploadFiles ID="VideoFile" runat="server" ModuleID="37" Mode="SingleFile" FileType="Video" />
                </div>
                <div class="mb-3">
                    <label ID="TitleLabel" clientidmode="Static" runat="server" for="CourseTitle">Title:</label>
                    <input type="text" ID="CourseTitle" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="TitleRequired" runat="server" ControlToValidate="CourseTitle"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Title is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="DueDateLabel" clientidmode="Static" runat="server" for="DueDate">Due Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="DueDateDiv">
                            <input type="text" id="DueDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea ID="Description" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="AttachmentLabel" clientidmode="Static" runat="server" for="Attachment">Assignment Attachment:</label>
                    <sep:UploadFiles ID="Attachment" runat="server" ModuleID="37" Mode="SingleFile" FileType="Software" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>