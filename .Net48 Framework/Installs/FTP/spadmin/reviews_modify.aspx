<%@ page title="Reviews" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="reviews_modify.aspx.cs" inherits="wwwroot.reviews_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Review</h4>
                <input type="hidden" runat="server" ID="ReviewID" />

                <div class="mb-3">
                    <label ID="QuestionLabel" clientidmode="Static" runat="server" for="Question">Question:</label>
                    <input type="text" ID="Question" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Question is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>

                <div class="mb-3">
                    <label ID="ModulesLabel" clientidmode="Static" runat="server" for="Question">Modules:</label>
                    <sep:ModuleSelection ID="Modules" runat="server" ModuleType="Reviews" ClientIDMode="Static" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>