<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="my_feeds.aspx.cs" inherits="wwwroot.my_feeds" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="<%= this.GetInstallFolder() %>js/my_feeds.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder() %>js/jquery/jquery.endless-scroll.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
                <%
                    switch (SepCommon.SepCore.Request.Item("DoAction"))
                    {
                        case "Friends":
                            this.Response.Write("$('#FriendsLI').addClass('active');");
                            break;

                        case "Favorites":
                            this.Response.Write("$('#FavoritesLI').addClass('active');");
                            break;

                        default:
                            this.Response.Write("$('#AllMembersLI').addClass('active');");
                            break;
                    }
                %>
        });

        $(function () {
            $(window).scrollTop(101);
            $(window)
                .endlessScroll({
                    pagesToKeep: 5,
                    inflowPixels: 100,
                    fireDelay: 10,
                    content: function (i) {

                        // ajax call to fetch next set of rows
                        $.ajax({
                            type: "GET",
                            url: config.imageBase + "api/feeds",
                            dataType: "json",
                            contentType: "application/json",
                            error: function (xhr) {
                                alert("There has been an error loading data." + debugMsg("\n\n" + xhr.responseText));
                            },

                            success: function (response) {
                                var Feeds = $.parseJSON(response.d);

                                // populate the rows
                                for (var i = 0; i < Feeds.length; i++) {

                                    var row = document.createElement("tr");
                                    var rowTd = document.createElement("td");

                                    rowTd.innerHTML += '<div id="FeedID' + Feeds[i].FeedID + '">';
                                    if (Feeds[i].MoreLink != null) {
                                        rowTd.innerHTML += '<a href="' + Feeds[i].MoreLink + '">';
                                    }
                                    rowTd.innerHTML += Feeds[i].Title;
                                    if (Feeds[i].MoreLink != null) {
                                        rowTd.innerHTML += '</a>';
                                    }
                                    rowTd.innerHTML += '<br />';
                                    rowTd.innerHTML += Feeds[i].TimeAgo;
                                    rowTd.innerHTML += '<br />';
                                    if (Feeds[i].Thumbnail != null) {
                                        rowTd.innerHTML += '<img src="' +
                                            Feeds[i].Thumbnail +
                                            '" border="0" alt="" align="Left" hspace="5" />';
                                    }
                                    if (Feeds[i].Description != null) {
                                        rowTd.innerHTML += Feeds[i].Description;
                                    }
                                    rowTd.innerHTML += '<br />';
                                    rowTd.innerHTML += '<button type="button" id="Comments' +
                                        Feeds[i].FeedID +
                                        '" class="btn btn-default" onclick="addComment(\'' +
                                        Feeds[i].FeedID +
                                        '\', \'<%= this.GetInstallFolder() %>\');">Comment (' +
                                        Feeds[i].NumComments +
                                        ')</button> ';
                                    rowTd.innerHTML += '<button type="button" id="Favorite' +
                                        Feeds[i].FeedID +
                                        '" class="btn btn-default" onclick="addFavorite(addComment(\'' +
                                        Feeds[i].FeedID +
                                        '\', \'<%= this.GetInstallFolder() %>\');">Favorite';
                                    if (Feeds[i].isFavorite == true) {
                                        rowTd.innerHTML += ' (&#10004;)';
                                    }
                                    rowTd.innerHTML += '</button> ';
                                    rowTd.innerHTML += '<button type="button" id="Like' +
                                        Feeds[i].FeedID +
                                        '" class="btn btn-default" onclick="addLike(addComment(\'' +
                                        Feeds[i].FeedID +
                                        '\', \'<%= this.GetInstallFolder() %>\');">Like (' +
                                        Feeds[i].NumLikes +
                                        ')</button> ';
                                    rowTd.innerHTML += '<button type="button" id="Dislike' +
                                        Feeds[i].FeedID +
                                        '" class="btn btn-default" onclick="addDislike(addComment(\'' +
                                        Feeds[i].FeedID +
                                        '\', \'<%= this.GetInstallFolder() %>\');">Dislike (' +
                                        Feeds[i].NumDislikes +
                                        ')</button> ';
                                    rowTd.innerHTML += '<button type="button" id="Delete' +
                                        Feeds[i].FeedID +
                                        '" class="btn btn-default" onclick="deletePost(addComment(\'' +
                                        Feeds[i].FeedID +
                                        '\', \'<%= this.GetInstallFolder() %>\');">Delete</button>';
                                    rowTd.innerHTML += '</div>';

                                    rowTd.innerHTML += '<div id="ViewComments' + Feeds[i].FeedID + '"></div>';

                                    row.appendChild(rowTd);

                                    $("#ManageGridView").append(row);
                                }
                            }
                        });
                        return '';
                    }
                });
        });

        var timeout = null;

        $(document)
            .on('mousemove',
                function () {
                    clearTimeout(timeout);

                    timeout = setTimeout(function () {
                        reloadFeeds();
                    },
                        30000);
                });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <div class="mb-3">
            <asp:Label ID="WhatsNewLabel" runat="server" AssociatedControlID="WhatsNew">What's new,
                    <span ID="FirstName" runat="server"></span>?</asp:Label>
            <input type="text" id="WhatsNew" runat="server" class="form-control" />
            <asp:CustomValidator ID="WhatsNewRequired" runat="server" ControlToValidate="WhatsNew"
                ClientValidationFunction="customFormValidator" ErrorMessage="What's new is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="PostButton" runat="server" Text="Post Update" OnClick="PostButton_Click" />
        </div>
    </div>

    <ul class="nav nav-tabs">
        <li class="nav-item" role="presentation" id="AllMembersLI">
            <a class="nav-link" href="my_feeds.aspx">All Members</a>
        </li>
        <li class="nav-item" role="presentation" id="FriendsLI">
            <a class="nav-link" href="my_feeds.aspx?DoAction=Friends">My Friends</a>
        </li>
        <li class="nav-item" role="presentation" id="FavoritesLI">
            <a class="nav-link" href="my_feeds.aspx?DoAction=Favorites">My Favorites</a>
        </li>
    </ul>

    <span class="successNotification" id="successNotification">
        <span ID="DeleteResult" runat="server"></span>
    </span>

    <div class="GridViewStyle">
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
            CssClass="GridViewStyle" AllowPaging="false">
            <Columns>
                <asp:TemplateField HeaderText="Photo" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <div id="FeedID<%#
                this.Eval("FeedID") %>">
                            <%#
                Convert.ToString(!DBNull.Value.Equals(this.Eval("MoreLink")) ? "<a href=\"" + this.Eval("MoreLink") + "\">" + this.Eval("Title") + "</a>" : this.Eval("Title")) %>
                            <br />
                            <%#
                this.Eval("TimeAgo") %>
                            <br />
                            <%#
                Convert.ToString(!DBNull.Value.Equals(this.Eval("Thumbnail")) ? "<img src=\"" + this.Eval("Thumbnail") + "\" border=\"0\" alt=\"\" align=\"Left\" hspace=\"5\" />" : "") %>
                            <%#
                this.Eval("Description") %>
                            <br />
                            <button type="button" id="Comments<%#
                this.Eval("FeedID") %>" class="btn btn-light" onclick="showComments('<%#
                this.Eval("FeedID") %>', '<%#
                this.GetInstallFolder() %>');">Comment (<%#
                this.Eval("NumComments") %>)</button>
                            <button type="button" id="Favorite<%#
                this.Eval("FeedID") %>" class="btn btn-light" onclick="addFavorite('<%#
                this.Eval("FeedID") %>', '<%#
                this.GetInstallFolder() %>');">Favorite<%# Convert.ToString(Convert.ToBoolean(this.Eval("isFavorite")) ? " (&#10004;)" : "") %></button>
                            <button type="button" id="Like<%#
                this.Eval("FeedID") %>" class="btn btn-light" onclick="addLike('<%#
                this.Eval("FeedID") %>', '<%#
                this.GetInstallFolder() %>');">Like (<%#
                this.Eval("NumLikes") %>)</button>
                            <button type="button" id="Dislike<%#
                this.Eval("FeedID") %>" class="btn btn-light" onclick="addDislike('<%#
                this.Eval("FeedID") %>', '<%#
                this.GetInstallFolder() %>');">Dislike (<%#
                this.Eval("NumDislikes") %>)</button>
                            <button type="button" id="Delete<%#
                this.Eval("FeedID") %>" class="btn btn-light" onclick="deletePost('<%#
                this.Eval("FeedID") %>', '<%#
                this.GetInstallFolder() %>');">Delete</button>
                        </div>
                        <div id="ViewComments<%#
                this.Eval("FeedID") %>">
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>