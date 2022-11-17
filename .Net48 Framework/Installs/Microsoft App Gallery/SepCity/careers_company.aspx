<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_company.aspx.cs" inherits="wwwroot.careers_company" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .CompanyIndustry {
            font-size: 15px;
            font-weight: bold;
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

    <h1 id="CompanyName" runat="server"></h1>

    <p class="CandidateRow"></p>

    <span ID="ScreenHTML" runat="server"></span>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="Position Search Results">
        <Columns>
        </Columns>
    </asp:GridView>
</asp:content>