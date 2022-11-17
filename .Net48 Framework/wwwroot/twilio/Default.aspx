<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="Default.aspx.cs" inherits="wwwroot.TwilioDefault" %>
<%@ import namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        #DialogBox {
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
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

        function clickNavigator(id) {
            $("li a").removeClass('active');
            $("li#" + id + " a").addClass('active');
        }

        function showSoftPhone() {
            if ($('#idSoftphone').is(":hidden")) {
                $('#idSoftphone').show();
                $('#button-call').val('Hide Phone');
            } else {
                $('#idSoftphone').hide();
                $('#button-call').val('Call Number');
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
                                <td width="260" nowrap="nowrap">
                                    <span style="color: #ffffff; display: block; font-size: 16px; margin: 5px 0 0 20px;"><b>Twilio Control Panel</b></span></td>
                                <td width="100%" align="left" valign="bottom" nowrap="nowrap">
                                    <input type="button" onclick="showSoftPhone()" id="button-call" class="btn btn-success btn-block flatbtn" style="width:150px;" value="Call Number" />
                                    <iframe scrolling="no" frameborder="0" style="display:none; border: medium none; height: 670px; width: 330px; position:absolute; top:35px; left:260px;" id="idSoftphone" name="idSoftphone" src="softphone.aspx"></iframe>
                                </td>
                                <td width="220" align="right" valign="bottom" nowrap="nowrap">
                                    <span style="color: #ffffff; display: block; margin: 5px 20px 0 0;">Version: <span ID="CWVersion" runat="server"></span> [ <a href="javascript:void(0)" onclick="confirm('Are you sure you wish to logout?', function() {document.location.href='<%=SepFunctions.GetInstallFolder()%>logout.aspx';})">Logout</a> ]</span></td>
                            </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="190" id="idTree" style="border-right: 1px solid #4A4A4A;">
                        <div id="AdminMenuTree">
                            <h4>Messages</h4>
                            <ul class="nav nav-pills flex-column">
                                <li role="presentation" id="inbox" class="nav-item"><a class="nav-link active" href="main.aspx" onclick="clickNavigator('inbox');" target="adminnav">Inbox</a></li>
                                <span id="ListGroups" runat="server"></span>
                            </ul>
                            <h4>Setup</h4>
                            <ul class="nav nav-pills flex-column">
                                <li class="nav-item" role="presentation" id="devices"><a class="nav-link" href="devices.aspx" onclick="clickNavigator('devices');" target="adminnav">Devices</a></li>
                                <li class="nav-item" role="presentation" id="voicemail"><a class="nav-link" href="voicemail.aspx" onclick="clickNavigator('voicemail');" target="adminnav">Voicemail</a></li>
                            </ul>
                            <h4>Admin</h4>
                            <ul class="nav nav-pills flex-column">
                                <li class="nav-item" role="presentation" id="flows"><a class="nav-link" href="flows.aspx" onclick="clickNavigator('flows');" target="adminnav">Flows</a></li>
                                <li class="nav-item" role="presentation" id="numbers"><a class="nav-link" href="numbers.aspx" onclick="clickNavigator('numbers');" target="adminnav">Numbers</a></li>
                                <li class="nav-item" role="presentation" id="users"><a class="nav-link" href="users.aspx" onclick="clickNavigator('users');" target="adminnav">Users / Groups</a></li>
                                <li class="nav-item" role="presentation" id="settings"><a class="nav-link" href="settings.aspx" onclick="clickNavigator('settings');" target="adminnav">Settings</a></li>
                            </ul>
                        </div>
                    </td>
                    <td width="8" style="vertical-align: middle;" bgcolor="#4A4A4A">
                        <img alt="Move Left" onclick="makeFullScreen()" id="idMoveImg" src="images/moveleft.gif">
                    </td>
                    <td width="100%">
                        <iframe scrolling="auto" frameborder="0" style="border: medium none; height: 600px; width: 100%;" id="adminnav" name="adminnav" src="main.aspx"></iframe>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>