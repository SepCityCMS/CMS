<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="elearning_submit_assignment.aspx.cs" inherits="wwwroot.elearning_submit_assignment" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:Panel ID="UpdatePanel" runat="server">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Submit Assignment</h4>
            <input type="hidden" runat="server" id="SubmitID" />
            <input type="hidden" runat="server" id="AssignmentID" />
            <div class="mb-3">
                <label id="AssignmentAttachmentLabel" clientidmode="Static" runat="server" for="AssignmentAttachment">Assignment Attachment:</label>
                <sep:UploadFiles ID="AssignmentAttachment" runat="server" ModuleID="37" Mode="SingleFile" FileType="Software" />
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Submit" />
            </div>
        </div>
    </asp:Panel>
</asp:content>