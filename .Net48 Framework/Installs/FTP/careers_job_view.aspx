<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_job_view.aspx.cs" inherits="wwwroot.careers_job_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .JobInfo {
            text-align: left;
        }

        .CandidateInfoLabel {
            display: inline-block;
            font-weight: bold;
            width: 160px;
        }

        .CandidateRow {
            border-bottom: 1px solid #cccccc;
            text-align: left;
        }
    </style>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:Panel ID="JobDetails" runat="server" Visible="true">

        <h1 id="JobTitle" runat="server"></h1>

        <span ID="ScreenHTML" runat="server"></span>

        <p align="center">
            <asp:Button Text="Apply with LinkedIn" ID="LinkedInButton" runat="server" OnClick="Authorize" />
            <asp:Button Text="Send Us Your Resume" ID="ResumeButton" runat="server" OnClick="SendResume" />
        </p>
        
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </asp:Panel>
</asp:content>