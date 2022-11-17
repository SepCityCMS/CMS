function showComments(b,a){$.ajax({url:a+"my_feeds_ajax.aspx?DoAction=ShowComments&FeedID="+b,error:function(){alert("There has been an error loading comments.")},success:function(c){var d="";d+='<div class="input-group">';d+='<input type="text" class="form-control" id="CommentText'+b+'">';d+='<span class="input-group-btn">';d+='<button class="btn btn-default" type="button" onclick="postComment(\''+b+"', '"+a+"');\">Post Comment</button>";d+="</span>";d+="</div>";$("#ViewComments"+b).html('<div style="margin-top:5px;">'+d+'</div><div id="ViewComments2'+b+'">'+c+"</div>")}})}function postComment(b,a){if($("#CommentText"+b).val()===""){alert("Comment text is required.")}else{$.ajax({url:a+"my_feeds_ajax.aspx?DoAction=SaveComment&FeedID="+b+"&CommentText="+$("#CommentText"+b).val(),error:function(){alert("There has been an error saving your comment.")},success:function(){showComments(b,a);var c=(parseInt(updateFeedCount($("#Comments"+b).text()))+1);$("#Comments"+b).text("Comment ("+c+")")}})}}function addFavorite(b,a){$.ajax({url:a+"my_feeds_ajax.aspx?DoAction=AddFavorite&FeedID="+b,error:function(){alert("There has been an error saving favorite.")},success:function(){reloadFeeds();$("#Favorite"+b).text("Favorite (Y)")}})}function addLike(b,a){$.ajax({url:a+"my_feeds_ajax.aspx?DoAction=AddLike&FeedID="+b,error:function(){alert("There has been an error saving favorite.")},success:function(c){reloadFeeds();if(c.indexOf("You have already liked this.")===-1){var d=(parseInt(updateFeedCount($("#Like"+b).text()))+1);$("#Like"+b).text("Like ("+d+")")}}})}function addDislike(b,a){$.ajax({url:a+"my_feeds_ajax.aspx?DoAction=AddDislike&FeedID="+b,error:function(){alert("There has been an error saving favorite.")},success:function(c){reloadFeeds();if(c.indexOf("You have already disliked this.")===-1){var d=(parseInt(updateFeedCount($("#Dislike"+b).text()))+1);$("#Dislike"+b).text("Dislike ("+d+")")}}})}function deletePost(b,a){$.ajax({url:a+"my_feeds_ajax.aspx?DoAction=Delete&FeedID="+b,error:function(){alert("There has been an error saving favorite.")},success:function(){reloadFeeds();$("#FeedID"+b).hide()}})}function reloadFeeds(){$.ajax({type:"GET",url:config.imageBase+"api/feeds",dataType:"json",contentType:"application/json",complete:function(){},error:function(a){alert("There has been an error loading data."+debugMsg("\n\n"+a.responseText))},success:function(a){var c=$.parseJSON(a.d);for(var b=0;b<c.length;b++){if($("#FeedID"+c[b].FeedID).length>0){break}var e=document.createElement("tr");var d=document.createElement("td");d.innerHTML+='<div id="FeedID'+c[b].FeedID+'">';if(c[b].MoreLink!==null){d.innerHTML+='<a href="'+c[b].MoreLink+'">'}d.innerHTML+=c[b].Title;if(c[b].MoreLink!==null){d.innerHTML+="</a>"}d.innerHTML+="<br />";d.innerHTML+=c[b].TimeAgo;d.innerHTML+="<br />";if(c[b].Thumbnail!==null){d.innerHTML+='<img src="'+c[b].Thumbnail+'" border="0" alt="" align="Left" hspace="5" />'}if(c[b].Description!==null){d.innerHTML+=c[b].Description}d.innerHTML+="<br />";d.innerHTML+='<button type="button" id="Comments'+c[b].FeedID+'" class="btn btn-default" onclick="addComment(\''+c[b].FeedID+"', '<%= GetInstallFolder()%>');\">Comment ("+c[b].NumComments+")</button> ";d.innerHTML+='<button type="button" id="Favorite'+c[b].FeedID+'" class="btn btn-default" onclick="addFavorite(addComment(\''+c[b].FeedID+"', '<%= GetInstallFolder()%>');\">Favorite";if(c[b].isFavorite===true){d.innerHTML+=" (&#10004;)"}d.innerHTML+="</button> ";d.innerHTML+='<button type="button" id="Like'+c[b].FeedID+'" class="btn btn-default" onclick="addLike(addComment(\''+c[b].FeedID+"', '<%= GetInstallFolder()%>');\">Like ("+c[b].NumLikes+")</button> ";d.innerHTML+='<button type="button" id="Dislike'+c[b].FeedID+'" class="btn btn-default" onclick="addDislike(addComment(\''+c[b].FeedID+"', '<%= GetInstallFolder()%>');\">Dislike ("+c[b].NumDislikes+")</button> ";d.innerHTML+='<button type="button" id="Delete'+c[b].FeedID+'" class="btn btn-default" onclick="deletePost(addComment(\''+c[b].FeedID+"', '<%= GetInstallFolder()%>');\">Delete</button>";d.innerHTML+="</div>";d.innerHTML+='<div id="ViewComments'+c[b].FeedID+'"></div>';e.appendChild(d);$("#ManageGridView").prepend(e)}}})}function updateFeedCount(b){var a=b.match(/[0-9]+/g).map(function(c){return +(c)});return a};