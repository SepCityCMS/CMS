function showComments(FeedID, sInstallFolder) {
    $.ajax({
        url: sInstallFolder + "my_feeds_ajax.aspx?DoAction=ShowComments&FeedID=" + FeedID,
        error: function() {
            alert("There has been an error loading comments.");
        },
        success: function(data) {
            var sComment = "";
            sComment += '<div class="input-group">';
            sComment += '<input type="text" class="form-control" id="CommentText' + FeedID + '">';
            sComment += '<span class="input-group-btn">';
            sComment += '<button class="btn btn-default" type="button" onclick="postComment(\'' +
                FeedID +
                "', '" +
                sInstallFolder +
                '\');">Post Comment</button>';
            sComment += "</span>";
            sComment += "</div>";
            $("#ViewComments" + FeedID)
                .html('<div style="margin-top:5px;">' +
                    sComment +
                    '</div><div id="ViewComments2' +
                    FeedID +
                    '">' +
                    data +
                    "</div>");
        }
    });
}

function postComment(FeedID, sInstallFolder) {
    if ($("#CommentText" + FeedID).val() === "") {
        alert("Comment text is required.");
    } else {
        $.ajax({
            url: sInstallFolder +
                "my_feeds_ajax.aspx?DoAction=SaveComment&FeedID=" +
                FeedID +
                "&CommentText=" +
                $("#CommentText" + FeedID).val(),
            error: function() {
                alert("There has been an error saving your comment.");
            },
            success: function() {
                showComments(FeedID, sInstallFolder);
                var newNum = (parseInt(updateFeedCount($("#Comments" + FeedID).text())) + 1);
                $("#Comments" + FeedID).text("Comment (" + newNum + ")");
            }
        });
    }
}

function addFavorite(FeedID, sInstallFolder) {
    $.ajax({
        url: sInstallFolder + "my_feeds_ajax.aspx?DoAction=AddFavorite&FeedID=" + FeedID,
        error: function() {
            alert("There has been an error saving favorite.");
        },
        success: function() {
            reloadFeeds();
            $("#Favorite" + FeedID).text("Favorite (Y)");
        }
    });
}

function addLike(FeedID, sInstallFolder) {
    $.ajax({
        url: sInstallFolder + "my_feeds_ajax.aspx?DoAction=AddLike&FeedID=" + FeedID,
        error: function() {
            alert("There has been an error saving favorite.");
        },
        success: function(data) {
            reloadFeeds();
            if (data.indexOf("You have already liked this.") === -1) {
                var newNum = (parseInt(updateFeedCount($("#Like" + FeedID).text())) + 1);
                $("#Like" + FeedID).text("Like (" + newNum + ")");
            }
        }
    });
}

function addDislike(FeedID, sInstallFolder) {
    $.ajax({
        url: sInstallFolder + "my_feeds_ajax.aspx?DoAction=AddDislike&FeedID=" + FeedID,
        error: function() {
            alert("There has been an error saving favorite.");
        },
        success: function(data) {
            reloadFeeds();
            if (data.indexOf("You have already disliked this.") === -1) {
                var newNum = (parseInt(updateFeedCount($("#Dislike" + FeedID).text())) + 1);
                $("#Dislike" + FeedID).text("Dislike (" + newNum + ")");
            }
        }
    });
}

function deletePost(FeedID, sInstallFolder) {
    $.ajax({
        url: sInstallFolder + "my_feeds_ajax.aspx?DoAction=Delete&FeedID=" + FeedID,
        error: function() {
            alert("There has been an error saving favorite.");
        },
        success: function() {
            reloadFeeds();
            $("#FeedID" + FeedID).hide();
        }
    });
}

function reloadFeeds() {

    // ajax call to fetch next set of rows
    $.ajax({
        type: "GET",
        url: config.imageBase + "api/feeds",
        dataType: "json",
        contentType: "application/json",
        complete: function() {
        },

        error: function(xhr) {
            alert("There has been an error loading data." + debugMsg("\n\n" + xhr.responseText));
        },

        success: function(response) {
            var Feeds = $.parseJSON(response.d);

            // populate the rows
            for (var i = 0; i < Feeds.length; i++) {

                if ($("#FeedID" + Feeds[i].FeedID).length > 0) {
                    break;
                }

                var row = document.createElement("tr");
                var rowTd = document.createElement("td");

                rowTd.innerHTML += '<div id="FeedID' + Feeds[i].FeedID + '">';
                if (Feeds[i].MoreLink !== null) {
                    rowTd.innerHTML += '<a href="' + Feeds[i].MoreLink + '">';
                }
                rowTd.innerHTML += Feeds[i].Title;
                if (Feeds[i].MoreLink !== null) {
                    rowTd.innerHTML += "</a>";
                }
                rowTd.innerHTML += "<br />";
                rowTd.innerHTML += Feeds[i].TimeAgo;
                rowTd.innerHTML += "<br />";
                if (Feeds[i].Thumbnail !== null) {
                    rowTd.innerHTML += '<img src="' +
                        Feeds[i].Thumbnail +
                        '" border="0" alt="" align="Left" hspace="5" />';
                }
                if (Feeds[i].Description !== null) {
                    rowTd.innerHTML += Feeds[i].Description;
                }
                rowTd.innerHTML += "<br />";
                rowTd.innerHTML += '<button type="button" id="Comments' +
                    Feeds[i].FeedID +
                    '" class="btn btn-default" onclick="addComment(\'' +
                    Feeds[i].FeedID +
                    '\', \'<%= GetInstallFolder()%>\');">Comment (' +
                    Feeds[i].NumComments +
                    ")</button> ";
                rowTd.innerHTML += '<button type="button" id="Favorite' +
                    Feeds[i].FeedID +
                    '" class="btn btn-default" onclick="addFavorite(addComment(\'' +
                    Feeds[i].FeedID +
                    '\', \'<%= GetInstallFolder()%>\');">Favorite';
                if (Feeds[i].isFavorite === true) {
                    rowTd.innerHTML += " (&#10004;)";
                }
                rowTd.innerHTML += "</button> ";
                rowTd.innerHTML += '<button type="button" id="Like' +
                    Feeds[i].FeedID +
                    '" class="btn btn-default" onclick="addLike(addComment(\'' +
                    Feeds[i].FeedID +
                    '\', \'<%= GetInstallFolder()%>\');">Like (' +
                    Feeds[i].NumLikes +
                    ")</button> ";
                rowTd.innerHTML += '<button type="button" id="Dislike' +
                    Feeds[i].FeedID +
                    '" class="btn btn-default" onclick="addDislike(addComment(\'' +
                    Feeds[i].FeedID +
                    '\', \'<%= GetInstallFolder()%>\');">Dislike (' +
                    Feeds[i].NumDislikes +
                    ")</button> ";
                rowTd.innerHTML += '<button type="button" id="Delete' +
                    Feeds[i].FeedID +
                    '" class="btn btn-default" onclick="deletePost(addComment(\'' +
                    Feeds[i].FeedID +
                    '\', \'<%= GetInstallFolder()%>\');">Delete</button>';
                rowTd.innerHTML += "</div>";

                rowTd.innerHTML += '<div id="ViewComments' + Feeds[i].FeedID + '"></div>';

                row.appendChild(rowTd);

                $("#ManageGridView").prepend(row);
            }
        }
    });

}

function updateFeedCount(str) {
    var numbers = str.match(/[0-9]+/g)
        .map(function(n) {
            return +(n);
        });
    return numbers;
}