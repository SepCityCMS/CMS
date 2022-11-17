<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="knowledgebase_view.aspx.cs" inherits="wwwroot.knowledgebase_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="PageContent" runat="server">
        <h1 id="ArticleSubject" runat="server">Knowledge Base</h1>

        <p><a href="javascript:history.back(1)">Go Back</a></p>

        <div id="ArticleContent" runat="server"></div>

        <br />
        <br />

        <span ID="ArticleFooter" runat="server"></span>
    </div>
</asp:content>