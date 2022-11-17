<%@ page title="FAQ" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="faq_modify.aspx.cs" inherits="wwwroot.faq_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 9;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add FAQ</h4>
                <input type="hidden" runat="server" ID="FAQID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="9" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="9" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="QuestionLabel" clientidmode="Static" runat="server" for="Question">Question:</label>
                    <textarea ID="Question" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Question is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="AnswerLabel" ClientIDMode="Static" runat="server">Answer to your question:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="Answer" Width="99%" Height="450" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>