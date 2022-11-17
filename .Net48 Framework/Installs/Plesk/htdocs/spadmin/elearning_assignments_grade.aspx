<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="elearning_assignments_grade.aspx.cs" inherits="wwwroot.elearning_assignments_grade" %>

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

        <div class="ModFormDiv" id="GradeContents" runat="server">

            <h4 id="ModifyLegend" runat="server">Grade Assignment</h4>
            <input type="hidden" runat="server" id="SubmitID" clientidmode="Static" />
            <input type="hidden" runat="server" id="StudentID" clientidmode="Static" />
            <input type="hidden" runat="server" id="AssignmentID" clientidmode="Static" />
            <input type="hidden" runat="server" id="UserID" clientidmode="Static" />

            <div class="mb-3">
                <label id="GradeLabel" clientidmode="Static" runat="server" for="Grade">Grade:</label>
                <input type="text" id="Grade" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="NotesLabel" clientidmode="Static" runat="server" for="Notes">Notes:</label>
                <textarea id="Notes" runat="server" class="form-control"></textarea>
            </div>
            <div class="mb-3">
                <span ID="AssignmentDownload" runat="server"></span>
            </div>

            <div class="row ModSaveButton">
                <div class="mb-3">
                    <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
                </div>
                <div class="mb-3">
                    <asp:Button CssClass="btn btn-primary" ID="BackButton" runat="server" Text="Back to Student" OnClick="BackButton_Click" />
                </div>
            </div>
        </div>
            </div>
    </asp:Panel>
</asp:content>