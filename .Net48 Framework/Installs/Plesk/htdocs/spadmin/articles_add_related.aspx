<%@ page language="C#" viewstatemode="Enabled" codebehind="articles_add_related.aspx.cs" inherits="wwwroot.articles_add_related" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head runat="server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Add Related Article</title>
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <link type="text/css" rel="stylesheet" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js" integrity="sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI=" crossorigin="anonymous"></script>

    <script src="../js/bootbox.min.js" type="text/javascript"></script>
    <script src="../js/main.js" type="text/javascript"></script>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>

    <script type="text/javascript">
        function assignArticle(iRowOffset, articleID, headlineText, author, articleStatus, headlineDate) {
            var newRowOffset = parseInt(iRowOffset) + 1;
            var row = document.createElement("tr");

            var tdHeadlineText = document.createElement("td");
            var tdHeadlineDate = document.createElement("td");

            tdHeadlineText.innerHTML = '<input type=\"hidden\" name=\"ArticleID' +
                articleID +
                '\" id=\"ArticleID' +
                articleID +
                '\" value=\"' +
                articleID +
                '\" />' +
                headlineText;
            tdHeadlineDate.innerHTML = headlineDate;

            row.appendChild(tdHeadlineText);
            row.appendChild(tdHeadlineDate);

            window.parent.$("#RelatedArticles").val(window.parent.$("#RelatedArticles").val() + ',' + articleID);

            window.parent.$("#ManageGridView").append(row);

            window.parent.$("#AddArticle").attr("href", "javascript:addRelatedArticle('" + newRowOffset + "');");

            parent.closeDialog("addArticle");
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">

        <span class="successNotification" id="successNotification">
            <span id="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle" id="FilterGrid" runat="server">
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                </div>
                <div class="GridViewFilterRight">
                    <input type="text" id="ModuleSearch" runat="server" class="GridViewSearch" onkeypress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}" />
                    <button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Search</button>
                </div>
            </div>

            <asp:gridview id="ManageGridView" runat="server" autogeneratecolumns="False" allowsorting="True" clientidmode="Static" showheader="false"
                cssclass="GridViewStyle" allowpaging="true" onpageindexchanging="ManageGridView_PageIndexChanging" pagesize="20" pagersettings-mode="NumericFirstLast">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="javascript:assignArticle('<%= SepCommon.SepCore.Request.Item("RowOffset") %>', '<%#
                this.Eval("ArticleID") %>', '<%#
                this.Eval("Headline") %>', '<%#
                this.Eval("Author") %>', '<%#
                this.Eval("Status") %>', '<%#
                this.Format_Date(this.Eval("Headline_Date").ToString()) %>')"><%#
                this.Eval("Headline") %></a>
                            <br />
                            <%#
                this.Format_Date(this.Eval("Headline_Date").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:gridview>
        </div>
    </form>
</body>
</html>