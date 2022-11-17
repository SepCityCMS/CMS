<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="knowledgebase.aspx.cs" inherits="wwwroot.knowledgebase" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Knowledge Base</h1>

    <p id="PageBrowse" runat="server">
        Use this view to search the knowledge base for a specific topic. You can also <a href="knowledgebase_browse.aspx">browse the knowledge base</a> by folder.
    </p>

    <table class="KBSearchTable">
        <tbody>
            <tr>
                <th>Search</th>
                <td>
                    <input type="text" id="Keywords" runat="server" clientidmode="Static" class="form-control" /></td>
            </tr>
            <tr id="FoldersRow" runat="server">
                <th>Folder</th>
                <td>
                    <select id="Folders" runat="server" clientidmode="Static" class="form-control" /></td>
            </tr>
        </tbody>
    </table>

    <div style="height: 30px;">
        <asp:Button ID="resetForm" runat="server" UseSubmitBehavior="false" OnClientClick="document.getElementById('Keywords').value='';document.getElementById('Folders').value='0';return false;" Text="Clear" CssClass="btn btn-default" />
        <asp:Button ID="SearchFormButton" runat="server" Text="Search" CssClass="btn btn-success btn-default" OnClick="SearchFormButton_Click" />
    </div>

    <br />

    <div class="panel panel-default" id="PageManageGridView" runat="server">
        <div class="panel-heading">Search Results</div>
    </div>

    <div style="clear: both"></div>

    <span ID="KBContent" runat="server" Text="No items to show in this list."></span>
</asp:content>