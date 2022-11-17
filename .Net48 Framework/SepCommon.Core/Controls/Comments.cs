// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Comments.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class Comments.
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// The m content unique identifier
        /// </summary>
        private string m_ContentUniqueID;

        /// <summary>
        /// The m reply user identifier
        /// </summary>
        private string m_ReplyUserID;

        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

        /// <summary>
        /// Gets or sets the content unique identifier.
        /// </summary>
        /// <value>The content unique identifier.</value>
        public string ContentUniqueID
        {
            get => Strings.ToString(m_ContentUniqueID);

            set => m_ContentUniqueID = value;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the reply user identifier.
        /// </summary>
        /// <value>The reply user identifier.</value>
        public string ReplyUserID
        {
            get => Strings.ToString(m_ReplyUserID);

            set => m_ReplyUserID = value;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => Strings.ToString(m_UserID);

            set => m_UserID = value;
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(DateTime sDate)
        {
            string sReturn = string.Empty;

            if (DateAndTime.DateDiff(DateAndTime.DateInterval.Hour, sDate, DateTime.Now) >= 1)
            {
                if (DateAndTime.DateDiff(DateAndTime.DateInterval.Day, sDate, DateTime.Now) >= 1)
                {
                    if (DateAndTime.DateDiff(DateAndTime.DateInterval.Month, sDate, DateTime.Now) >= 1)
                    {
                        if (DateAndTime.DateDiff(DateAndTime.DateInterval.Year, sDate, DateTime.Now) >= 1)
                        {
                            var sSuffix = DateAndTime.DateDiff(DateAndTime.DateInterval.Year, sDate, DateTime.Now) > 1 ? "s" : string.Empty;
                            sReturn = SepFunctions.LangText("~~" + DateAndTime.DateDiff(DateAndTime.DateInterval.Year, sDate, DateTime.Now) + "~~ year" + sSuffix + " ago");
                        }
                        else
                        {
                            var sSuffix = DateAndTime.DateDiff(DateAndTime.DateInterval.Month, sDate, DateTime.Now) > 1 ? "s" : string.Empty;
                            sReturn = SepFunctions.LangText("~~" + DateAndTime.DateDiff(DateAndTime.DateInterval.Month, sDate, DateTime.Now) + "~~ month" + sSuffix + " ago");
                        }
                    }
                    else
                    {
                        var sSuffix = DateAndTime.DateDiff(DateAndTime.DateInterval.Day, sDate, DateTime.Now) > 1 ? "s" : string.Empty;
                        sReturn = SepFunctions.LangText("~~" + DateAndTime.DateDiff(DateAndTime.DateInterval.Day, sDate, DateTime.Now) + "~~ day" + sSuffix + " ago");
                    }
                }
                else
                {
                    var sSuffix = DateAndTime.DateDiff(DateAndTime.DateInterval.Hour, sDate, DateTime.Now) > 1 ? "s" : string.Empty;
                    sReturn = SepFunctions.LangText("~~" + DateAndTime.DateDiff(DateAndTime.DateInterval.Hour, sDate, DateTime.Now) + "~~ hour" + sSuffix + " ago");
                }
            }
            else
            {
                var sSuffix = DateAndTime.DateDiff(DateAndTime.DateInterval.Minute, sDate, DateTime.Now) > 1 ? "s" : string.Empty;
                sReturn = SepFunctions.LangText("~~" + DateAndTime.DateDiff(DateAndTime.DateInterval.Minute, sDate, DateTime.Now) + "~~ minute" + sSuffix + " ago");
            }

            return sReturn;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ContentUniqueID))
            {
                return output.ToString();
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);
            Session.setCookie("returnUrl", SepFunctions.GetPageName() + "?" + Request.ServerVariables("QUERY_STRING"));
            var loginUrl = sInstallFolder + "login.aspx";

            if (SepFunctions.Setup(8, "CNRCEnable") == "Yes")
            {
                output.AppendLine("<script type=\"text/javascript\">");
                output.AppendLine("function VerifyComment(replyId) {");
                output.AppendLine("if(($('#" + ID + "').val() == '' && replyId == '0') || ($('#txt'+replyId).val() == '' && replyId > '0')) {");
                output.AppendLine("  openModal('CommentBoxDialog');");
                output.AppendLine("} else {");
                output.AppendLine("  var params = new Object();");
                output.AppendLine("  var sCommentId = getIdentity();");
                output.AppendLine("  var sContents = '';");
                output.AppendLine("  params.UserID = '" + SepFunctions.Session_User_ID() + "';");
                output.AppendLine("  params.CommentID = sCommentId;");
                output.AppendLine("  params.ReplyID = replyId;");
                output.AppendLine("  params.ModuleID = '" + ModuleID + "';");
                output.AppendLine("  params.UniqueID = '" + ContentUniqueID + "';");
                output.AppendLine("  if(replyId == '0') {");
                output.AppendLine("    sContents = $('#" + ID + "').val();");
                output.AppendLine("  } else {");
                output.AppendLine("    sContents = $('#txt'+replyId).val();");
                output.AppendLine("  }");
                output.AppendLine("  params.Message = sContents;");
                output.AppendLine("  $.ajax({");
                output.AppendLine("    type: 'POST',");
                output.AppendLine("    data: JSON.stringify(params),");
                output.AppendLine("    url: '" + sImageFolder + "api/comments',");
                output.AppendLine("    dataType: 'json',");
                output.AppendLine("    contentType: 'application/json',");
                output.AppendLine("    error: function (xhr, ajaxOptions, thrownError) {");
                output.AppendLine("      alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("There has been an error saving your comment.")) + "'));");
                output.AppendLine("    },");
                var highlightClass = string.Empty;
                if (SepFunctions.Session_User_ID() == ReplyUserID)
                {
                    highlightClass = " PHighlight";
                }

                output.AppendLine("    success: function (response) {");
                output.AppendLine("      if(replyId == '0') {");
                output.AppendLine("        var htmlContent = '<div class=\"CommentBox\" id=\"Comment'+sCommentId+'\">';");
                output.AppendLine("        htmlContent += '<div class=\"UserCommentName" + highlightClass + "\"><a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.Session_User_ID() + "\">" + SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName") + "</a> <span>" + Format_Date(DateTime.Now) + "</span></div>';");
                output.AppendLine("        htmlContent += '<div class=\"UserCommentMsg\">'+sContents+'</div>';");
                output.AppendLine("        htmlContent += '<div class=\"UserCommentReply\"><span class=\"ThumbsSpan\"><span class=\"Likes\" id=\"Likes'+sCommentId+'\">0</span><img src=\"" + sImageFolder + "images/public/thumbs_up.png\" border=\"0\" onclick=\"likeComment(\\''+sCommentId+'\\')\" /></span><span class=\"ThumbsSpan\"><span class=\"Dislikes\" id=\"Dislikes'+sCommentId+'\">0</span><img src=\"" + sImageFolder + "images/public/thumbs_down.png\" border=\"0\" onclick=\"dislikeComment(\\''+sCommentId+'\\')\" /></span> <a href=\"javascript:void(0)\" onclick=\"commentReply(\\''+sCommentId+'\\');return false;\">" + SepFunctions.LangText("Reply") + "</a></div>';");
                output.AppendLine("        htmlContent += '</div>';");
                output.AppendLine("        htmlContent += '<div class=\"ReplyContents\" id=\"ReplyContents'+sCommentId+'\"></div>';");
                output.AppendLine("        $('#CommentContents').after(htmlContent);");
                output.AppendLine("      } else {");
                output.AppendLine("        var htmlContent = '<div class=\"ReplyBox\" style=\"margin-left:40px;\" id=\"Comment'+sCommentId+'\">';");
                output.AppendLine("        htmlContent += '<div class=\"UserCommentName" + highlightClass + "\"><a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.Session_User_ID() + "\">" + SepFunctions.GetUserInformation("FirstName") + " " + SepFunctions.GetUserInformation("LastName") + "</a> <span>" + Format_Date(DateTime.Now) + "</span></div>';");
                output.AppendLine("        htmlContent += '<div class=\"UserCommentMsg\">'+sContents+'</div>';");
                output.AppendLine("        htmlContent += '<div class=\"UserCommentReply\"><span class=\"ThumbsSpan\"><span class=\"Likes\" id=\"Likes'+sCommentId+'\">0</span><img src=\"" + sImageFolder + "images/public/thumbs_up.png\" border=\"0\" onclick=\"likeComment(\\''+sCommentId+'\\')\" /></span><span class=\"ThumbsSpan\"><span class=\"Dislikes\" id=\"Dislikes'+sCommentId+'\">0</span><img src=\"" + sImageFolder + "images/public/thumbs_down.png\" border=\"0\" onclick=\"dislikeComment(\\''+sCommentId+'\\')\" /></span></div>';");
                output.AppendLine("        htmlContent += '</div>';");
                output.AppendLine("        $('#Reply'+replyId).remove();");
                output.AppendLine("        $('#ReplyContents'+replyId).after(htmlContent);");
                output.AppendLine("      }");
                output.AppendLine("      $('#" + ID + "').val('');");
                output.AppendLine("    }");
                output.AppendLine("  });");
                output.AppendLine("}");
                output.AppendLine("}");

                output.AppendLine("function likeComment(commentId) {");
                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    output.AppendLine("var params = new Object();");
                    output.AppendLine("params.LikeID = getIdentity();");
                    output.AppendLine("params.CommentID = commentId;");
                    output.AppendLine("params.ModuleID = '" + ModuleID + "';");
                    output.AppendLine("params.UserLikes = true;");
                    output.AppendLine("params.UserDislikes = false;");
                    output.AppendLine("params.UserID = '" + SepFunctions.Session_User_ID() + "';");
                    output.AppendLine("$.ajax({");
                    output.AppendLine("type: 'POST',");
                    output.AppendLine("data: JSON.stringify(params),");
                    output.AppendLine("url: '" + sImageFolder + "api/comments/like',");
                    output.AppendLine("dataType: 'json',");
                    output.AppendLine("contentType: 'application/json',");
                    output.AppendLine("error: function (xhr, ajaxOptions, thrownError) {");
                    output.AppendLine("alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("There has been an error saving your comment.")) + "'));");
                    output.AppendLine("},");
                    output.AppendLine("success: function (data) {");
                    output.AppendLine("var obj = jQuery.parseJSON(JSON.stringify(data));");
                    output.AppendLine("if(obj.d != 'Failed') {");
                    output.AppendLine("$('#Likes'+commentId).text((parseInt($('#Likes'+commentId).text()) + 1));");
                    output.AppendLine("} else {");
                    output.AppendLine("openModal('alreadyRated');");
                    output.AppendLine("}");
                    output.AppendLine("}");
                    output.AppendLine("});");
                }
                else
                {
                    output.AppendLine("document.location.href='" + loginUrl + "';");
                }

                output.AppendLine("}");

                output.AppendLine("function dislikeComment(commentId) {");
                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    output.AppendLine("var params = new Object();");
                    output.AppendLine("params.LikeID = getIdentity();");
                    output.AppendLine("params.CommentID = commentId;");
                    output.AppendLine("params.ModuleID = '" + ModuleID + "';");
                    output.AppendLine("params.UserLikes = false;");
                    output.AppendLine("params.UserDislikes = true;");
                    output.AppendLine("params.UserID = '" + SepFunctions.Session_User_ID() + "';");
                    output.AppendLine("$.ajax({");
                    output.AppendLine("type: 'POST',");
                    output.AppendLine("data: JSON.stringify(params),");
                    output.AppendLine("url: '" + sImageFolder + "api/comments/like',");
                    output.AppendLine("dataType: 'json',");
                    output.AppendLine("contentType: 'application/json',");
                    output.AppendLine("error: function (xhr, ajaxOptions, thrownError) {");
                    output.AppendLine("alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("There has been an error saving your comment.")) + "'));");
                    output.AppendLine("},");
                    output.AppendLine("success: function (data) {");
                    output.AppendLine("var obj = jQuery.parseJSON(JSON.stringify(data));");
                    output.AppendLine("if(obj.d != 'Failed') {");
                    output.AppendLine("$('#Dislikes'+commentId).text((parseInt($('#Dislikes'+commentId).text()) + 1));");
                    output.AppendLine("} else {");
                    output.AppendLine("openModal('alreadyRated');");
                    output.AppendLine("}");
                    output.AppendLine("}");
                    output.AppendLine("});");
                }
                else
                {
                    output.AppendLine("document.location.href='" + loginUrl + "';");
                }

                output.AppendLine("}");

                output.AppendLine("function commentReply(commentId) {");
                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    output.AppendLine("if($('#Reply'+commentId).length == 0) {");
                    output.AppendLine("  $('#Comment'+commentId).append('<div class=\"CommentBoxNewReply\" id=\"Reply'+commentId+'\" style=\"margin-left:40px;\"><textarea name=\"txt'+commentId+'\" id=\"txt'+commentId+'\" onkeyup=\"$(\\'#CommentBoxChars'+commentId+'\\').text((500 - parseInt(this.value.replace(/{.*}/g, \\'\\').length)) + \\' characters remaining\\');\" class=\"pb-cmnt-textarea form-control\" style=\"height:60px;\"></textarea><br/><span id=\"CommentBoxChars'+commentId+'\" style=\"float:left;\">" + SepFunctions.LangText("500 characters remaining") + "</span><span id=\"CommentBoxPost\" style=\"float:right;\"><input type=\"button\" class=\"btn btn-secondary\" value=\"" + SepFunctions.LangText("Reply") + "\" onclick=\"VerifyComment(\\''+commentId+'\\');\" /></span><div style=\"clear:both;\"></div></div>');");
                    output.AppendLine("} else {");
                    output.AppendLine("  $('#Reply'+commentId).remove();");
                    output.AppendLine("}");
                }
                else
                {
                    output.AppendLine("document.location.href='" + loginUrl + "';");
                }

                output.AppendLine("}");

                output.AppendLine("$(document).ready(function() {");
                output.AppendLine("$('#" + ID + "').keyup(function() {");
                output.AppendLine("if(parseInt(this.value.replace(/{.*}/g, '').length) > 500) {");
                output.AppendLine("$('#" + ID + "').val(Left($('#" + ID + "').val(), 500));");
                output.AppendLine("$('#CommentBoxChars').text('0 characters remaining');");
                output.AppendLine("var psconsole = $('#" + ID + "');");
                output.AppendLine("psconsole.scrollTop(psconsole[0].scrollHeight - psconsole.height());");
                output.AppendLine("} else {");
                output.AppendLine("$('#CommentBoxChars').text((500 - parseInt(this.value.replace(/{.*}/g, '').length)) + ' characters remaining');");
                output.AppendLine("}");
                output.AppendLine("});");
                output.AppendLine("});");
                output.AppendLine("</script>");

                var requiredText = ModuleID == 20 ? SepFunctions.LangText("You must enter a review before you can post.") : SepFunctions.LangText("You must enter a comment before you can post.");
                var alreadyRatedText = ModuleID == 20 ? SepFunctions.LangText("You have already rated this review.") : SepFunctions.LangText("You have already rated this comment.");

                output.AppendLine("<div id=\"CommentBoxDialog\" title=\"Required Field\" style=\"display:none\"><p>" + requiredText + "</p></div>");
                output.AppendLine("<div id=\"alreadyRated\" title=\"Already Rated\" style=\"display:none\"><p>" + alreadyRatedText + "</p></div>");
                output.AppendLine("<div style=\"clear:both;\"></div>");

                var sReviewText = ModuleID == 20 ? SepFunctions.LangText("Write your review here!") : SepFunctions.LangText("Write your comment here!");

                //////////////////////////////////////////////////////////////////////////////////////////////////
                string buttonAction = "document.location.href='" + loginUrl + "';";
                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    buttonAction = "VerifyComment('0');";
                }

                output.AppendLine("<div class=\"container pb-cmnt-container\">");
                output.AppendLine("    <div class=\"row\">");
                output.AppendLine("        <div style=\"width:100%;\">");
                output.AppendLine("            <div class=\"card-block\">");
                output.AppendLine("                  <textarea name=\"" + ID + "\" id=\"" + ID + "\" placeholder=\"" + sReviewText + "\" class=\"pb-cmnt-textarea\"></textarea>");
                output.AppendLine("			        <span id=\"CommentBoxChars\" style=\"float:left;\">" + SepFunctions.LangText("500 characters remaining") + "</span>");
                output.AppendLine("			        <button class=\"btn btn-primary float-right\" onclick=\"" + buttonAction + "\" type=\"button\">" + SepFunctions.LangText("Comment") + "</button>");
                output.AppendLine("            </div>");
                output.AppendLine("        </div>");
                output.AppendLine("    </div>");
                output.AppendLine("</div>");

                //////////////////////////////////////////////////////////////////////////////////////////////////
                output.AppendLine("<div class=\"ModuleComments\">");

                output.AppendLine("<div class=\"CommentsList\">");
                output.AppendLine("<div id=\"CommentContents\"></div>");
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 50 CommentID,UserID,FullName,Message,DatePosted,(SELECT SUM(UserLike) FROM CommentsLikes WHERE CommentID=Com.CommentID) AS UserLikes,(SELECT SUM(UserDislike) FROM CommentsLikes WHERE CommentID=Com.CommentID) AS UserDislikes FROM Comments AS Com WHERE ReplyID='0' AND UniqueID=@ContentUniqueID AND ModuleID=@ModuleID ORDER BY DatePosted DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@ContentUniqueID", ContentUniqueID);
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                while (RS.Read())
                                {
                                    highlightClass = string.Empty;
                                    if (SepFunctions.openNull(RS["UserID"]) == ReplyUserID)
                                    {
                                        highlightClass = " PHighlight";
                                    }

                                    output.AppendLine("<div class=\"CommentBox\" id=\"Comment" + SepFunctions.openNull(RS["CommentID"]) + "\">");
                                    output.AppendLine("<div class=\"UserCommentName" + highlightClass + "\"><a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.openNull(RS["UserID"]) + "\">" + SepFunctions.openNull(RS["FullName"]) + "</a> <span>" + Format_Date(SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]))) + "</span></div>");
                                    output.AppendLine("<div class=\"UserCommentMsg\">" + SepFunctions.openNull(RS["Message"]) + "</div>");
                                    output.AppendLine("<div class=\"UserCommentReply\"><span class=\"ThumbsSpan\"><span class=\"Likes\" id=\"Likes" + SepFunctions.openNull(RS["CommentID"]) + "\">" + SepFunctions.toLong(SepFunctions.openNull(RS["UserLikes"])) + "</span><img src=\"" + sImageFolder + "images/public/thumbs_up.png\" border=\"0\" onclick=\"likeComment('" + SepFunctions.openNull(RS["CommentID"]) + "')\" /></span><span class=\"ThumbsSpan\"><span class=\"Dislikes\" id=\"Dislikes" + SepFunctions.openNull(RS["CommentID"]) + "\">" + SepFunctions.toLong(SepFunctions.openNull(RS["UserDislikes"])) + "</span><img src=\"" + sImageFolder + "images/public/thumbs_down.png\" border=\"0\" onclick=\"dislikeComment('" + SepFunctions.openNull(RS["CommentID"]) + "')\" /></span> <a href=\"javascript:void(0)\" onclick=\"commentReply('" + SepFunctions.openNull(RS["CommentID"]) + "');return false;\">" + SepFunctions.LangText("Reply") + "</a></div>");
                                    output.AppendLine("</div>");
                                    output.AppendLine("<div class=\"ReplyContents\" id=\"ReplyContents" + SepFunctions.openNull(RS["CommentID"]) + "\">");
                                    output.AppendLine("</div>");
                                    using (SqlCommand cmd2 = new SqlCommand("SELECT TOP 50 CommentID,UserID,FullName,Message,DatePosted,(SELECT SUM(UserLike) FROM CommentsLikes WHERE CommentID=Com.CommentID) AS UserLikes,(SELECT SUM(UserDislike) FROM CommentsLikes WHERE CommentID=Com.CommentID) AS UserDislikes FROM Comments AS Com WHERE ReplyID=@ReplyID ORDER BY DatePosted DESC", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@ReplyID", SepFunctions.openNull(RS["CommentID"]));
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            while (RS2.Read())
                                            {
                                                highlightClass = string.Empty;
                                                if (SepFunctions.openNull(RS2["UserID"]) == ReplyUserID)
                                                {
                                                    highlightClass = " PHighlight";
                                                }

                                                output.AppendLine("<div class=\"ReplyBox\" style=\"margin-left:40px;\" id=\"Comment" + SepFunctions.openNull(RS2["CommentID"]) + "\">");
                                                output.AppendLine("<div class=\"UserCommentName" + highlightClass + "\"><a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + SepFunctions.openNull(RS2["UserID"]) + "\">" + SepFunctions.openNull(RS2["FullName"]) + "</a> <span>" + Format_Date(SepFunctions.toDate(SepFunctions.openNull(RS2["DatePosted"]))) + "</span></div>");
                                                output.AppendLine("<div class=\"UserCommentMsg\">" + SepFunctions.openNull(RS2["Message"]) + "</div>");
                                                output.AppendLine("<div class=\"UserCommentReply\"><span class=\"ThumbsSpan\"><span class=\"Likes\" id=\"Likes" + SepFunctions.openNull(RS2["CommentID"]) + "\">" + SepFunctions.toLong(SepFunctions.openNull(RS2["UserLikes"])) + "</span><img src=\"" + sImageFolder + "images/public/thumbs_up.png\" border=\"0\" onclick=\"likeComment('" + SepFunctions.openNull(RS2["CommentID"]) + "')\" /></span><span class=\"ThumbsSpan\"><span class=\"Dislikes\" id=\"Dislikes" + SepFunctions.openNull(RS2["CommentID"]) + "\">" + SepFunctions.toLong(SepFunctions.openNull(RS2["UserDislikes"])) + "</span><img src=\"" + sImageFolder + "images/public/thumbs_down.png\" border=\"0\" onclick=\"dislikeComment('" + SepFunctions.openNull(RS2["CommentID"]) + "')\" /></span></div>");
                                                output.AppendLine("</div>");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                output.AppendLine("</div>");
                output.AppendLine("</div>");
            }

            return output.ToString();
        }
    }
}