<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="elearning_questions_modify.aspx.cs" inherits="wwwroot.elearning_questions_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <%
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 37;
            this.Response.Write(cAdminModuleMenu.Render());
        %>

		<div class="col-md-12 pagecontent">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">Add Question</h4>
            <input type="hidden" runat="server" id="QuestionID" />
            <input type="hidden" runat="server" id="ExamID" />
            <div class="mb-3">
                <label id="QuestionTypeLabel" clientidmode="Static" runat="server" for="QuestionType">Question Type:</label>
                <select id="QuestionType" runat="server" class="form-control">
                    <option value="MC">Multiple choice</option>
                    <option value="MS">Multiple choice /w footer</option>
                    <option value="MCB">Multiple choice fill in the blank</option>
                    <option value="SP">Correct the sentence</option>
                    <option value="ABV1">Long abbreviation</option>
                    <option value="ABV2">Short abbreviation</option>
                    <option value="FB">Fill in the blank</option>
                </select>
                <asp:CustomValidator ID="QuestionTypeRequired" runat="server" ControlToValidate="QuestionType"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Question Type is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="QuestionNoLabel" clientidmode="Static" runat="server" for="QuestionNo">Question Order:</label>
                <input type="text" id="QuestionNo" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="QuestionHeaderLabel" clientidmode="Static" runat="server" for="QuestionHeader">Question Header:</label>
                <input type="text" id="QuestionHeader" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="QuestionFooterLabel" clientidmode="Static" runat="server" for="QuestionFooter">Question Footer:</label>
                <input type="text" id="QuestionFooter" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                Fill out the following if you have a multiple choice question.
            </div>
            <div class="mb-3">
                <label id="Answer1Label" clientidmode="Static" runat="server" for="Answer1">Answer 1:</label>
                <input type="text" id="Answer1" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="Answer2Label" clientidmode="Static" runat="server" for="Answer2">Answer 2:</label>
                <input type="text" id="Answer2" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="Answer3Label" clientidmode="Static" runat="server" for="Answer3">Answer 3:</label>
                <input type="text" id="Answer3" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="Answer4Label" clientidmode="Static" runat="server" for="Answer4">Answer 4:</label>
                <input type="text" id="Answer4" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="Answer5Label" clientidmode="Static" runat="server" for="Answer5">Answer 5:</label>
                <input type="text" id="Answer5" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="CorrectAnswerLabel" clientidmode="Static" runat="server" for="CorrectAnswer">Correct Answer:</label>
                <input type="text" id="CorrectAnswer" runat="server" class="form-control" />
            </div>

            <div class="row ModSaveButton">
                <div class="mb-3">
                    <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
                </div>
                <div class="mb-3">
                    <asp:Button CssClass="btn btn-primary" ID="BackButton" runat="server" Text="Back to Exam" OnClick="BackButton_Click" />
                </div>
            </div>
        </div>
            </div>
    </asp:Panel>
</asp:content>