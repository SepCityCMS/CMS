<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_view_candidate.aspx.cs" inherits="wwwroot.careers_view_candidate" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
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

    <h1 id="FullName" runat="server"></h1>

    <div id="CandidateInfo" runat="server" class="CandidateInfo"></div>

    <p class="CandidateRow"></p>

    <span ID="ScreenHTML" runat="server"></span>
</asp:content>