// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="MyFeeds.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class MyFeeds.
    /// </summary>
    public static class MyFeeds
    {
        /// <summary>
        /// Feeds the add favorite.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>System.String.</returns>
        public static string Feed_Add_Favorite(string UserID, string FeedID)
        {
            var bUpdate = false;
            var sReturn = new Models.MyFeeds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FeedID FROM FeedsUsers WHERE FeedID=@FeedID AND UserID=@UserID AND (DownVote='1' OR UpVote='1')", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bUpdate = true;
                        }
                    }
                }

                if (bUpdate == false)
                {
                    using (var cmd = new SqlCommand("INSERT INTO FeedsUsers (FeedID, UserID, isFavorite, DatePosted, isDeleted) VALUES (@FeedID, @UserID, @isFavorite, @DatePosted, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@isFavorite", 1);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("UPDATE FeedsUsers SET isFavorite=@isFavorite, DatePosted=@DatePosted, isDeleted='0' WHERE FeedID=@FeedID AND UserID=@UserID AND (DownVote='1' OR UpVote='1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@isFavorite", 1);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            sReturn.JsonMessage = SepFunctions.LangText("Feed has been successfully added to your favorites.");

            return JsonConvert.SerializeObject(sReturn);
        }

        /// <summary>
        /// Feeds the delete.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>System.String.</returns>
        public static string Feed_Delete(string UserID, string FeedID)
        {
            var bUpdate = false;
            var sReturn = new Models.MyFeeds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FeedID FROM FeedsUsers WHERE FeedID=@FeedID AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bUpdate = true;
                        }
                    }
                }

                if (bUpdate == false)
                {
                    using (var cmd = new SqlCommand("INSERT INTO FeedsUsers (FeedID, UserID, DownVote, UpVote, DatePosted, isDeleted) VALUES (@FeedID, @UserID, @DownVote, @UpVote, @DatePosted, '1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@DownVote", 0);
                        cmd.Parameters.AddWithValue("@UpVote", 0);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("UPDATE FeedsUsers SET isDeleted='1' WHERE FeedID=@FeedID AND UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            sReturn.JsonMessage = SepFunctions.LangText("Feed has been successfully deleted.");

            return JsonConvert.SerializeObject(sReturn);
        }

        /// <summary>
        /// Feeds the dislike save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>System.String.</returns>
        public static string Feed_Dislike_Save(string UserID, string FeedID)
        {
            var bUpdate = false;
            var sReturn = new Models.MyFeeds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FeedID FROM FeedsUsers WHERE FeedID=@FeedID AND UserID=@UserID AND (DownVote='1' OR UpVote='1')", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bUpdate = true;
                        }
                    }
                }

                if (bUpdate == false)
                {
                    using (var cmd = new SqlCommand("INSERT INTO FeedsUsers (FeedID, UserID, DownVote, UpVote, DatePosted, isDeleted) VALUES (@FeedID, @UserID, @DownVote, @UpVote, @DatePosted, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@DownVote", 1);
                        cmd.Parameters.AddWithValue("@UpVote", 0);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("UPDATE FeedsUsers SET DownVote=@DownVote, UpVote=@UpVote, DatePosted=@DatePosted, isDeleted='0' WHERE FeedID=@FeedID AND UserID=@UserID AND (DownVote='1' OR UpVote='1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@DownVote", 1);
                        cmd.Parameters.AddWithValue("@UpVote", 0);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (bUpdate == false)
            {
                sReturn.JsonMessage = SepFunctions.LangText("Dislike has been successfully saved.");
            }
            else
            {
                sReturn.JsonMessage = SepFunctions.LangText("You have already disliked this.");
            }

            return JsonConvert.SerializeObject(sReturn);
        }

        /// <summary>
        /// Feeds the like save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <returns>System.String.</returns>
        public static string Feed_Like_Save(string UserID, string FeedID)
        {
            var bUpdate = false;
            var sReturn = new Models.MyFeeds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FeedID FROM FeedsUsers WHERE FeedID=@FeedID AND UserID=@UserID AND (DownVote='1' OR UpVote='1')", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedID", FeedID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bUpdate = true;
                        }
                    }
                }

                if (bUpdate == false)
                {
                    using (var cmd = new SqlCommand("INSERT INTO FeedsUsers (FeedID, UserID, DownVote, UpVote, DatePosted, isDeleted) VALUES (@FeedID, @UserID, @DownVote, @UpVote, @DatePosted, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@DownVote", 0);
                        cmd.Parameters.AddWithValue("@UpVote", 1);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("UPDATE FeedsUsers SET DownVote=@DownVote, UpVote=@UpVote, DatePosted=@DatePosted, isDeleted='0' WHERE FeedID=@FeedID AND UserID=@UserID AND (DownVote='1' OR UpVote='1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedID", FeedID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@DownVote", 0);
                        cmd.Parameters.AddWithValue("@UpVote", 1);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (bUpdate == false)
            {
                sReturn.JsonMessage = SepFunctions.LangText("Like has been successfully saved.");
            }
            else
            {
                sReturn.JsonMessage = SepFunctions.LangText("You have already liked this.");
            }

            return JsonConvert.SerializeObject(sReturn);
        }

        /// <summary>
        /// Gets my feeds.
        /// </summary>
        /// <param name="onlyFavorites">if set to <c>true</c> [only favorites].</param>
        /// <param name="onlyFriends">if set to <c>true</c> [only friends].</param>
        /// <returns>List&lt;Models.MyFeeds&gt;.</returns>
        public static List<Models.MyFeeds> GetMyFeeds(bool onlyFavorites = false, bool onlyFriends = false)
        {
            var lMyFeeds = new List<Models.MyFeeds>();

            var wClause = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (onlyFavorites)
            {
                wClause += " AND Feeds.FeedID IN (SELECT FeedID FROM FeedsUsers WHERE isFavorite='1' AND UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "' AND FeedID=Feeds.FeedID)";
            }

            if (onlyFriends)
            {
                wClause += " AND Feeds.UserID IN (SELECT AddedUserID FROM FriendsList WHERE UserID='" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "' AND Approved='1')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Feeds.*,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID=Feeds.ModuleID AND UniqueID=Feeds.UniqueID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID,(SELECT Count(CommentID) FROM Comments WHERE ModuleID='62' AND ReplyID='0' AND UniqueID=Feeds.FeedID) AS NumComments,(SELECT Count(FeedID) FROM FeedsUsers WHERE UpVote='1' AND FeedID=Feeds.FeedID) AS NumLikes,(SELECT Count(FeedID) FROM FeedsUsers WHERE DownVote='1' AND FeedID=Feeds.FeedID) AS NumDislikes,(SELECT Count(FeedID) FROM FeedsUsers WHERE isFavorite='1' AND FeedID=Feeds.FeedID) AS isFavorite FROM Feeds, Members WHERE Members.UserID=Feeds.UserID AND Members.Status=1 AND Feeds.UserID NOT IN (SELECT UserID FROM FeedsUsers WHERE FeedID=Feeds.FeedID AND isDeleted='1' AND UserID=Feeds.UserID)" + wClause + " ORDER BY DatePosted DESC", conn))
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

                    if (SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"])) < SepFunctions.toDate(SepFunctions.GetUserInformation("CreateDate")))
                    {
                        break;
                    }

                    var dMyFeeds = new Models.MyFeeds { FeedID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["FeedID"])) };
                    dMyFeeds.UniqueID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]));
                    dMyFeeds.ModuleID = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]));
                    dMyFeeds.Title = SepFunctions.openNull(ds.Tables[0].Rows[i]["Title"]);
                    dMyFeeds.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dMyFeeds.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dMyFeeds.UserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                    dMyFeeds.MoreLink = SepFunctions.openNull(ds.Tables[0].Rows[i]["MoreLink"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dMyFeeds.Thumbnail = sImageFolder + "spadmin/show_image.aspx?ModuleID=" + SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"])) + "&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        var sProfileImage = SepFunctions.userProfileImage(SepFunctions.openNull(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"])));
                        if (!string.IsNullOrWhiteSpace(sProfileImage))
                        {
                            dMyFeeds.Thumbnail = sProfileImage;
                        }
                    }

                    dMyFeeds.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dMyFeeds.TimeAgo = SepFunctions.TimeAgo(SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"])));
                    dMyFeeds.NumComments = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["NumComments"]));
                    dMyFeeds.NumLikes = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["NumLikes"]));
                    dMyFeeds.NumDislikes = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["NumDislikes"]));
                    dMyFeeds.isFavorite = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["isFavorite"]));
                    lMyFeeds.Add(dMyFeeds);
                }
            }

            return lMyFeeds;
        }
    }
}