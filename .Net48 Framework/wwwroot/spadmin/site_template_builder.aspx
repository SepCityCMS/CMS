<%@ page language="C#" viewstatemode="Enabled" codebehind="site_template_builder.aspx.cs" inherits="wwwroot.site_template_builder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Template</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <span id="Stylesheet" runat="server"></span>
    <script type="text/javascript">
        function changeColor(sFunc, e) {
            parent.document.getElementById('divBody').style.display = 'none';
            parent.document.getElementById('divLayoutTopMenu').style.display = 'none';
            parent.document.getElementById('divMenuLabel').style.display = 'none';
            parent.document.getElementById('divContentTopGrad').style.display = 'none';
            parent.document.getElementById('divContentBottomGrad').style.display = 'none';
            parent.document.getElementById('divHeaderHR').style.display = 'none';
            parent.document.getElementById('divHeader').style.display = 'none';
            parent.document.getElementById('divContentSpacer').style.display = 'none';
            parent.document.getElementById('divFooter').style.display = 'none';
            parent.document.getElementById('divFieldset').style.display = 'none';
            parent.document.getElementById('divEvents').style.display = 'none';
            parent.document.getElementById('divTables').style.display = 'none';
            parent.document.getElementById('divModuleTopMenu').style.display = 'none';
            parent.document.getElementById('div' + sFunc).style.display = '';
            parent.document.getElementById('idApplyChanges').style.display = '';
            e.cancelBubble = true;
        }

        function revealEvent(elem, evt) {
            var elemx;
            evt = (evt) ? evt : ((window.event) ? window.event : '');
            if (evt) {
                elemx = (evt.target) ? evt.target : evt.srcElement;
            }
            var elemv = (evt.currentTarget) ? evt.currentTarget.nodeName : document.activeElement.tagName;
            var msg = 'Event (from ' + elemx.tagName + ' | ' + elemv + ' at ';
            msg += evt.clientX + ',' + evt.clientY + ') is DateTime.Today at the <';
            msg += elem + '> element.';
            alert(msg);
        }

        function clickEvent(form, e) {
            revealEvent('button', e);
            // cancel button
            e.cancelBubble = true;
        }
    </script>
</head>
<body>

    <asp:panel id="UpdatePanel" runat="server">

        <form id="form1" runat="server">
            <span ID="TemplateContent" runat="server"></span>
        </form>

        <script type="text/javascript">
            var anchors = document.getElementsByTagName('a');
            for (var i = 0; i < anchors.length; i++)
                anchors[i].onclick = function () { return false };
            var anchors = document.getElementsByTagName('input');
            for (var i = 0; i < anchors.length; i++)
                anchors[i].onclick = function () { return false };
        </script>
    </asp:Panel>
</body>
</html>