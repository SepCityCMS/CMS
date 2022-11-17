<%@ page title="E-Learning" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="elearning_students_modify.aspx.cs" inherits="wwwroot.elearning_students_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(DateEnrolled.ClientID, "false", "true", "")%>;
            restyleGridView("#AssignmentGrid");
            restyleGridView("#ExamGrid");
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

                <h4 id="ModifyLegend" runat="server">Add Student</h4>
                <input type="hidden" runat="server" ID="StudentID" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />
                <div class="mb-3">
                    <label ID="CourseIDLabel" clientidmode="Static" runat="server" for="CourseID">Select a Course:</label>
                    <select ID="CourseID" runat="server" class="form-control">
                        <option value="">Select a Course</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                    <input type="text" ID="UserName" runat="server"  class="form-control" ReadOnly="true" />
                </div>
                <div class="mb-3">
                    <label ID="DateEnrolledLabel" clientidmode="Static" runat="server" for="DateEnrolled">Date Enrolled:</label>
                    <div class="form-group">
                        <div class="input-group date" id="DateEnrolledDiv">
                            <input type="text" id="DateEnrolled" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="ActiveLabel" clientidmode="Static" runat="server" for="Active">Active:</label>
                    <select ID="Active" runat="server" class="form-control">
                        <option value="1">Yes</option>
                        <option value="0">No</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="UserNotesLabel" clientidmode="Static" runat="server" for="UserNotes">User Notes:</label>
                    <textarea ID="UserNotes" runat="server"  class="form-control"></textarea>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>

        <br />

        <asp:GridView ID="AssignmentGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
                      CssClass="GridViewStyle" AllowPaging="false" Caption="Assignments">
            <Columns>
                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <a href="elearning_assignments_grade.aspx?AssignmentID=<%#
                Eval("AssignmentID").ToString()%>&SubmitID=<%#
                Eval("SubmitID").ToString()%>&StudentID=<%=SepCommon.SepCore.Request.Item("StudentID")%>"><%#
                Eval("Title").ToString()%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("Description").ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Submitted" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Format_Date(Eval("DateSubmitted").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Format_Date(Eval("DueDate").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("Grade").ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />

        <asp:GridView ID="ExamGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
                      CssClass="GridViewStyle" AllowPaging="false" Caption="Exams">
            <Columns>
                <asp:TemplateField HeaderText="Exam Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("ExamName").ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:content>