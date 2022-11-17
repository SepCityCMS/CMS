<%@ page title="E-Learning" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="elearning_exams_modify.aspx.cs" inherits="wwwroot.elearning_exams_modify" %>
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

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Exam</h4>
                <input type="hidden" runat="server" ID="ExamID" />
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
                    <label ID="ExamNameLabel" clientidmode="Static" runat="server" for="ExamName">Exam Name:</label>
                    <input type="text" ID="ExamName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="ExamNameRequired" runat="server" ControlToValidate="ExamName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Exam Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Start Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="StartDateDiv">
                            <input type="text" id="StartDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="EndDateLabel" clientidmode="Static" runat="server" for="EndDate">End Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="EndDateDiv">
                            <input type="text" id="EndDate" class="form-control" runat="server" />
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
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
	</div>
        </div>

        <br />

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle" id="QuestionSection" runat="server">
            <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="AddQuestionButton" runat="server" Text="Add Question" onclick="AddQuestionButton_Click" /></div>
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                    <select id="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                        <option value="">Select an Action</option>
                        <option value="DeleteQuestion">Delete Question</option>
                    </select>
                    <div class="button-to-bottom">
		<button class="btn btn-primary" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'QuestionID') == false) {return false} else">GO</button>
	</div>
                </div>
                <div class="GridViewFilterRight">
                     <input type="text" id="ModuleSearch" runat="server" Class="GridViewSearch" onKeyPress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}" /> <div class="mb-3"><asp:Button class="btn btn-primary" ID="ModuleSearchButton" runat="server" Text="Search" UseSubmitBehavior="false" ClientIDMode="Static" /></div>
                </div>
            </div>

            <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                          CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
                          OnSorting="ManageGridView_Sorting" EnableViewState="True">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="QuestionID<%#
                Eval("QuestionID").ToString()%>" value="<%#
                Eval("QuestionID").ToString()%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:templatefield ItemStyle-Width="20px">
                        <itemtemplate>
                            <a href="elearning_questions_modify.aspx?QuestionID=<%#
                Eval("QuestionID").ToString()%>">
                                <img src="../images/public/edit.png" alt="Edit" />
                            </a>
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:TemplateField HeaderText="Question" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("QuestionHeader").ToString()%> <%#
                Eval("QuestionFooter").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>