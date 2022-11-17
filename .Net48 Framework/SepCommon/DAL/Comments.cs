// ***********************************************************************
// Assembly         : SepCommon
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

namespace SepCommon.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Comments.
    /// </summary>
    public static class Comments
    {
        /// <summary>
        /// Comments the like save.
        /// </summary>
        /// <param name="LikeID">The like identifier.</param>
        /// <param name="CommentID">The comment identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="UserLike">if set to <c>true</c> [user like].</param>
        /// <param name="UserDislike">if set to <c>true</c> [user dislike].</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.String.</returns>
        public static string Comment_Like_Save(long LikeID, long CommentID, int ModuleID, bool UserLike, bool UserDislike, string UserID)
        {
            string sReturn = SepFunctions.LangText("Failed");

            var alreadyRated = false;

            if (LikeID == 0)
            {
                LikeID = SepFunctions.GetIdentity();
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT LikeID FROM CommentsLikes WHERE CommentID=@CommentID AND IPAddress=@IPAddress AND ModuleID=@ModuleID", conn))
                {
                    cmd.Parameters.AddWithValue("@CommentID", CommentID);
                    cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            alreadyRated = true;
                        }
                    }
                }
            }

            if (alreadyRated == false)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("INSERT INTO CommentsLikes (LikeID, CommentID, UserID, ModuleID, UserLike, UserDislike, IPAddress, DatePosted) VALUES (@LikeID, @CommentID, @UserID, @ModuleID, @UserLike, @UserDislike, @IPAddress, @DatePosted)", conn))
                    {
                        cmd.Parameters.AddWithValue("@LikeID", LikeID);
                        cmd.Parameters.AddWithValue("@CommentID", CommentID);
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@UserLike", UserLike ? "1" : "0");
                        cmd.Parameters.AddWithValue("@UserDislike", UserDislike ? "1" : "0");
                        cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }

                sReturn = SepFunctions.LangText("You have successfully liked/disliked a comment.");
            }

            return sReturn;
        }

        /// <summary>
        /// Comments the save.
        /// </summary>
        /// <param name="CommentID">The comment identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="UniqueID">The unique identifier.</param>
        /// <param name="ReplyID">The reply identifier.</param>
        /// <param name="Comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Comment_Save(long CommentID, string UserID, int ModuleID, long UniqueID, long ReplyID, string Comment)
        {
            if (CommentID == 0)
            {
                CommentID = SepFunctions.GetIdentity();
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("INSERT INTO Comments (CommentID, ReplyID, UniqueID, UserID, ModuleID, Message, FullName, IPAddress, DatePosted) VALUES (@CommentID, @ReplyID, @UniqueID, @UserID, @ModuleID, @Message, @FullName, @IPAddress, @DatePosted)", conn))
                {
                    cmd.Parameters.AddWithValue("@CommentID", CommentID);
                    cmd.Parameters.AddWithValue("@UniqueID", UniqueID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    cmd.Parameters.AddWithValue("@ReplyID", ReplyID);
                    cmd.Parameters.AddWithValue("@Message", SepFunctions.Filter_Bad_Words(SepFunctions.RemoveHTML(Comment)));
                    cmd.Parameters.AddWithValue("@FullName", SepFunctions.GetUserInformation("FirstName", UserID) + " " + SepFunctions.GetUserInformation("LastName", UserID));
                    cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn;
            sReturn = SepFunctions.LangText("Comment has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="UniqueID">The unique identifier.</param>
        /// <param name="ReplyID">The reply identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns>List&lt;Models.Comments&gt;.</returns>
        public static List<Models.Comments> GetComments(int ModuleID, long UniqueID, long ReplyID = 0, string SortExpression = "DatePosted", string SortDirection = "DESC")
        {
            var lComments = new List<Models.Comments>();

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT CommentID,UserID,FullName,Message,UserLikes,UserDislikes,ReplyID,DatePosted FROM Comments WHERE ModuleID='" + SepFunctions.FixWord(SepCore.Strings.ToString(ModuleID)) + "' AND UniqueID='" + SepFunctions.FixWord(SepCore.Strings.ToString(UniqueID)) + "' AND ReplyID='" + SepFunctions.FixWord(SepCore.Strings.ToString(ReplyID)) + "' ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dComments = new Models.Comments { CommentID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["CommentID"])) };
                    dComments.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dComments.FullName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FullName"]);
                    dComments.Message = SepFunctions.openNull(ds.Tables[0].Rows[i]["Message"]);
                    dComments.UserLikes = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserLikes"]));
                    dComments.UserDislikes = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserDislikes"]));
                    dComments.ReplyID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ReplyID"]));
                    dComments.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lComments.Add(dComments);
                }
            }

            return lComments;
        }
    }
}