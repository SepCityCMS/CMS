<%@ page title="Administration Console" language="C#" masterpagefile="Site.Master"
    codebehind="Default.aspx.cs" inherits="wwwroot._Default" %>
<%@ import namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/jquery/jquery.FileTree.js" type="text/javascript"></script>
    <style type="text/css">
        #DialogBox {
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">
        function openVersion(sVersion) {
            $('#DialogBox').attr('title', 'New Version Available');
            $('#DialogBox')
                .html("<iframe src=\"https://www.sepcity.com/new-version.aspx?Version=" +
                    sVersion +
                    "\" width=\"100%\" height=\"240\" frameborder=\"0\" />");
            openModal('DialogBox', 300, 330);
        }

        function openTip(title, description) {
            $('#DialogBox').attr('title', 'Tip: ' + title);
            $('#DialogBox')
                .html(description +
                    '<br /><br /><div class="form-group"><input type="checkbox" name="hidetip" onclick="hideTip()" /> Never show this again.</div>');
            openModal('DialogBox', 300, 330);
        }

        function hideTip() {
            $.ajax({
                url: "default_tips.aspx",
                cache: false
            })
            .done(function (html) {
                alert(html);
                closeDialog('DialogBox');
            });
        }

        function drawMenuTree() {
            $('#AdminMenuTree').fileTree({
                root: '',
                folderEvent: 'click',
                expandSpeed: 500,
                collapseSpeed: 500,
                multiFolder: true,
                script: 'menu_mainadmin.aspx?PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>'
            },
            function (sID) {
                if (sID == '/help/default.aspx') {
                    window.open("../help/default.aspx", "_blank");
                } else if (sID == 'http://www.sepcity.com/') {
                    window.open("http://www.sepcity.com/", "_blank");
                } else {
                    $('#adminnav').attr('src', sID);
                }
            });
        }

        $(document).ready(function () {
            drawMenuTree();

            // Adjust Heights for the Admin Tree and IFrame
            $('#adminnav').bind('load',
                    function () {
                        $('#adminnav').height($(window).height() - 48);
                    });

            $('#AdminMenuTree').height(($(window).height() - 58) + 'px');

            window.onresize = function () {
                $('#AdminMenuTree').height(($(window).height() - 58) + 'px');
                $('#adminnav').height($(window).height() - 48);
            };
        });

        function makeFullScreen() {
            if (document.getElementById('idTree').style.display == 'none') {
                document.getElementById('idTree').style.display = '';
                document.getElementById('idMoveImg').src = 'images/moveleft.gif';
            } else {
                document.getElementById('idTree').style.display = 'none';
                document.getElementById('idMoveImg').src = 'images/moveright.gif';
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div id="DialogBox" title="New Version Available" style="display: none"></div>
            <table id="maintable" width="100%">
                <tr style="height: 35px">
                    <td bgcolor="#222222" width="100%" colspan="3" align="center" id="AdminTopBar">
                        <table align="center" width="100%">
                            <tbody>
                            <tr>
                                <td width="100%">
                                    <span style="color: #ffffff; display: block; font-size: 16px; margin: 5px 0 0 20px;"><b>Administration Console</b></span></td>
                                <td align="right" valign="bottom" nowrap="nowrap">
                                    <span style="color: #ffffff; display: block; margin: 5px 20px 0 0;">Version: <span ID="CWVersion" runat="server"></span> [ <a href="javascript:void(0)" onclick="confirm('Are you sure you wish to logout?', function() {document.location.href='<%=SepFunctions.GetInstallFolder()%>logout.aspx';})" style="color:#ffffff;">Logout</a> ]</span></td>
                            </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="190" id="idTree" style="border-right: 1px solid #4A4A4A;">
                        <div id="AdminMenuTree"></div>
                    </td>
                    <td width="8" style="vertical-align: middle;" bgcolor="#4A4A4A">
                        <img alt="Move Left" onclick="makeFullScreen()" id="idMoveImg" src="images/moveleft.gif">
                    </td>
                    <td width="100%">
                        <iframe scrolling="auto" frameborder="0" style="border: medium none; height: 600px; width: 100%;" id="adminnav" name="adminnav" src="<%=Convert.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("DoPageURL")) ? SepFunctions.UrlDecode(SepCommon.SepCore.Request.Item("DoPageURL")) : "../dashboard/default.aspx?PortalID=" + SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")))%>"></iframe>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>