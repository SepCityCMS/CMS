<%@ page title="Database Tools" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" async="true"
    codebehind="database_tools.aspx.cs" inherits="wwwroot.database_tools" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function reindexDatabase(s) {
            var id = $(s).attr("id");
            document.getElementById('ProgressDiv').style.display = '';
            __doPostBack(id, id);
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <div style="padding: 10px;">
            <div style="background: #085ABE url('images/reindex_database.png') no-repeat center top; color: #ffffff; cursor: pointer; height: 106px; text-align: center; width: 96px;" runat="server" onclick="reindexDatabase(this)" id="ReindexData" clientidmode="Static">
                <div style="padding-top: 55px;">Reindex Database</div>
            </div>
        </div>

        <div id="ProgressDiv" clientidmode="Static" runat="server" style="display: none; padding: 10px;">Database is reindexing (This may take several minutes), please wait ...</div>
    </asp:Panel>
</asp:content>