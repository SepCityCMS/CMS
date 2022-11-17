<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_my_resume.aspx.cs" inherits="wwwroot.careers_my_resume" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">My Resume</h4>
        <div class="mb-3" id="ResumeFileRow" runat="server">
            <label id="ResumeFileLabel" clientidmode="Static" runat="server" for="ResumeFile">Resume File Name:</label>
            <span ID="ResumeFile" runat="server"></span>
        </div>
        <div class="mb-3">
            <label id="UploadResumeLabel" clientidmode="Static" runat="server" for="UploadResume">Upload a Resume:</label>
            <sep:UploadFiles ID="UploadResume" runat="server" FileType="Document" ModuleID="66" Mode="SingleFile" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>