<%@ page language="C#" viewstatemode="Enabled" codebehind="site_template_save.aspx.cs" inherits="wwwroot.site_template_save" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Save Template</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" style="width: 95%">

            <h4 id="ModifyLegend" runat="server">Save Template</h4>

            <div class="mb-3">
                <label id="TemplateNameLabel" clientidmode="Static" runat="server" for="TemplateName">Template Name:</label>
                <input type="text" id="TemplateName" runat="server" maxlength="50" class="form-control" />
            </div>

            <div class="mb-3">
                <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                <textarea id="Description" runat="server" class="form-control"></textarea>
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </form>
</body>
</html>