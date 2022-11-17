<%@ page language="C#" viewstatemode="Enabled" codebehind="elearning_presentation.aspx.cs" inherits="wwwroot.elearning_presentation" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <sep:videoplayer id="ViewPresentation" runat="server" width="480" height="360" />
    </form>
</body>
</html>