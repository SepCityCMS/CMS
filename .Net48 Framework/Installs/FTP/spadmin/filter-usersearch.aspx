﻿<%@ page language="C#" viewstatemode="Enabled" codebehind="filter-usersearch.aspx.cs" inherits="wwwroot.filter_usersearch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head runat="server">

    <script type="text/javascript">
        var config = {
            base: '<%= this.GetInstallFolder(true) %>'
        };
    </script>
    <link type="text/css" rel="stylesheet" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js" integrity="sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI=" crossorigin="anonymous"></script>
    
    <script src="../js/bootbox.min.js" type="text/javascript"></script>
    <script src="../js/main.js" type="text/javascript"></script>
    <script src="../js/country.js" type="text/javascript"></script>

    <script type="text/javascript">
        function assignUserValue(objUsername, objUserID, sUsername, sUserID) {
            parent.document.getElementById(objUsername).value = sUsername;
            parent.document.getElementById(objUserID).value = sUserID;
            parent.closeDialog('FieldFilterDiv');
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="input-group">
            <input type="text" id="ModuleSearch" runat="server" placeholder="Search for..." onkeypress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}" class="form-control" />
            <span class="input-group-btn">
                <button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Go!</button>
            </span>
        </div>

        <span class="successNotification" id="successNotification">
            <span id="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle">
            <asp:gridview id="ManageGridView" runat="server" autogeneratecolumns="False" allowsorting="True" clientidmode="Static"
                cssclass="GridViewStyle" allowpaging="true" onpageindexchanging="ManageGridView_PageIndexChanging" pagesize="20" pagersettings-mode="NumericFirstLast"
                enableviewstate="True">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="javascript:assignUserValue('<%= SepCommon.SepCore.Request.Item("PopulateUsername") %>', '<%= SepCommon.SepCore.Request.Item("PopulateUserID") %>', '<%#
                this.Eval("Username") %>', '<%#
                this.Eval("UserID") %>');"><%#
                this.Eval("Username") %></a> (<%#
                this.Eval("FirstName") %> <%#
                this.Eval("LastName") %>)
                            <br />
                            <%#
                this.Eval("EmailAddress") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:gridview>
        </div>
    </form>
</body>
</html>