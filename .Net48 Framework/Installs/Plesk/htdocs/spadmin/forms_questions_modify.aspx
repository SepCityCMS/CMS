<%@ page title="Forms" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="forms_questions_modify.aspx.cs" inherits="wwwroot.forms_questions_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/forms_questions_modify.js" type="text/javascript"></script>
    <style type="text/css">
        #OptionPanel {
            margin-left: 30px;
            max-height: 360px;
            overflow: auto;
            width: 310px;
        }

        #OptionAdd {
            margin-left: 30px;
        }
    </style>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 13;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Question</h4>
                <input type="hidden" runat="server" ID="FormID" />
                <input type="hidden" runat="server" ID="QuestionID" />

                <div class="mb-3">
                    <label ID="QuestionTypeLabel" clientidmode="Static" runat="server" for="QuestionType">Question Type:</label>
                    <select ID="QuestionType" runat="server" ClientIDMode="Static" class="form-control" AutoPostBack="false">
                        <option value="YN">Yes/No</option>
                        <option value="TF">True/False</option>
                        <option value="CB">Checkboxes</option>
                        <option value="RB">Radio Buttons</option>
                        <option value="SA">Short Answer</option>
                        <option value="LA">Long Answer</option>
                        <option value="FU">File Upload</option>
                        <option value="HE">HTML Editor</option>
                        <option value="DD">Dropdown</option>
                    </select>
                    <asp:CustomValidator ID="QuestionTypeRequired" runat="server" ControlToValidate="QuestionType"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Question Type is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <asp:Panel ID="OptionPanel" runat="server" ClientIDMode="Static">
                </asp:Panel>
                <div class="mb-3" id="OptionAdd" style="display: none">
                    <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="AddOption" runat="server" Text="Add Option" UseSubmitBehavior="false" OnClientClick="return addCustomOption();" /></div>
                    <input type="hidden" ID="NumOptions" runat="server" Value="0" ClientIDMode="Static" />
                </div>

                <div class="mb-3">
                    <label ID="RequiredLabel" clientidmode="Static" runat="server" for="Required">Required:</label>
                    <select ID="Required" runat="server" class="form-control">
                        <option value="true">Yes</option>
                        <option value="false">No</option>
                    </select>
                    <asp:CustomValidator ID="RequiredRequired" runat="server" ControlToValidate="Required"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Required is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="SectionRow" runat="server">
                    <label ID="SectionIDLabel" clientidmode="Static" runat="server" for="SectionID">List question under the following section:</label>
                    <select ID="SectionID" runat="server" class="form-control">
                        <option value="">None</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="QuestionLabel" clientidmode="Static" runat="server" for="Question">Question Text:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="Question" Width="99%" Height="450" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>