<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="article_display.aspx.cs" inherits="wwwroot.article_display" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <% if (SepCommon.SepCore.Request.Item("DoAction") == "Print")
       { %>
    <style type="text/css" media="print">
        @media print {
            a[href]:after {
                content: none !important;
            }
        }
    </style>
    <% } %>
    <script type="text/javascript">
        function switchArticle(url) {
            document.location.href = url;
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <p id="PrintButtonRow" runat="server" align="center">
            <input type="button" name="PrintButton" value="Print Now" onclick="print();" />
        </p>

        <h1 class="ListingTitle">
            <span ID="Headline" runat="server"></span>
        </h1>

        <div class="row">
            <div class="col-sm-2">
                <span ID="ProfilePic" runat="server"></span>
            </div>
            <div class="col-sm-5">
                <span ID="Author" runat="server"></span>
                <br />
                <span id="CategoryColumn" runat="server">Category:
                    <span ID="CategoryName" runat="server"></span></span>
                <br />
                <span id="SourceInfo" runat="server">Source:
                    <span ID="ArticleURL" runat="server"></span></span>
                <br />
                <%
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 35;
                    cCustomFields.isReadOnly = true;
                    cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("ArticleID");
                    cCustomFields.UserID = sUserName;
                    this.Response.Write(cCustomFields.Render());
                %>
            </div>
            <div class="col-sm-5">
                <span class="pull-right">Viewed
                    <span ID="Visits" runat="server"></span></span>
                <br />
                <span class="pull-right">
                    <sep:RatingStars ID="RatingStars" runat="server" ModuleID="35" />
                </span>
            </div>
        </div>

        <hr />
        <br />

        <div class="mb-3">
            <div class="col-sm-3" id="ArticlePhotos">
                <sep:ContentImages ID="ArticleImages" runat="server" />
            </div>
            <div class="col-sm-9" id="ArticleContent">
                <span ID="Full_Article" runat="server"></span>
            </div>
        </div>

        <br />

        <div class="mb-3">
            <div class="col-sm-12">
                This article was published on:
				<br />
                <span ID="HeadlineDate" runat="server"></span>
                <%
                    var cSocialShare = new SepCityControls.SocialShare();
                    this.Response.Write(cSocialShare.Render());
                %>
            </div>
        </div>

        <br />

        <div class="mb-3">
            <div class="col-sm-6">
                <%
                    if (SepCommon.SepCore.Request.Item("DoAction") != "Print")
                    {
                        var cComments = new SepCityControls.Comments();
                        cComments.ModuleID = 35;
                        cComments.ID = "Comments";
                        cComments.ContentUniqueID = SepCommon.SepCore.Request.Item("ArticleID");
                        cComments.ReplyUserID = sUserName;
                        cComments.UserID = SepCommon.SepFunctions.Session_User_ID();
                        this.Response.Write(cComments.Render());
                    }
                %>
            </div>
            <div class="col-sm-6">
                <div id="ArtAuthorRow" runat="server">
                    <span class="pull-right"><b>Articles from This Author</b><br />
                        <select id="ArtAuthor" runat="server" width="230" onchange="switchArticle(this.value)" clientidmode="Static">
                        </select></span>
                </div>
                <div id="CatArticlesRow" runat="server">
                    <br />
                    <br />
                    <span class="pull-right"><b>Articles Under This Category</b><br />
                        <select id="CatArticles" runat="server" width="230" onchange="switchArticle(this.value)" clientidmode="Static">
                        </select></span>
                </div>
            </div>
        </div>

        <div class="mb-3">
            <div class="col-sm-12">
                <sep:RatingGraph ID="RatingGraph" runat="server" ModuleID="35" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        if ($('#ArticlePhotos img').length == 0) {
            $('#ArticlePhotos').hide();
            $('#ArticleContent').attr('class', 'col-sm-12');
        }
    </script>
</asp:content>