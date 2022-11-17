// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Friends.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.Models;
    using SepCommon.Core.SepCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class Friends.
    /// </summary>
    public static class Friends
    {
        /// <summary>
        /// Friendses the approve.
        /// </summary>
        /// <param name="FriendID">The friend identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.String.</returns>
        public static string Friends_Approve(string FriendID, string UserID)
        {
            var alreadyAdded = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var sFriendUserID = string.Empty;

                using (var cmd = new SqlCommand("SELECT UserID FROM FriendsList WHERE ID=@FriendID", conn))
                {
                    cmd.Parameters.AddWithValue("@FriendID", FriendID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sFriendUserID = SepFunctions.openNull(RS["UserID"]);
                        }
                        else
                        {
                            return SepFunctions.LangText("User does not exist on our web site.");
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT UserID FROM FriendsList WHERE UserID=@UserID AND AddedUserID=@AddedUserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@AddedUserID", sFriendUserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            alreadyAdded = true;
                        }
                    }
                }

                if (alreadyAdded == false)
                {
                    using (var cmd = new SqlCommand("UPDATE FriendsList SET Approved='1' WHERE UserID=@UserID AND AddedUserID=@AddedUserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", sFriendUserID);
                        cmd.Parameters.AddWithValue("@AddedUserID", UserID);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("INSERT INTO FriendsList (UserID, AddedUserID, Approved) VALUES (@UserID, @AddedUserID, @Approved)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@AddedUserID", sFriendUserID);
                        cmd.Parameters.AddWithValue("@Approved", true);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            var sReturn = string.Empty;

            if (alreadyAdded == false)
            {
                sReturn = SepFunctions.LangText("Friend has been successfully approved.");
            }

            sReturn = SepFunctions.LangText("Friend has been already approved.");
            return sReturn;
        }

        /// <summary>
        /// Friendses the delete.
        /// </summary>
        /// <param name="FriendIDs">The friend i ds.</param>
        /// <returns>System.String.</returns>
        public static string Friends_Delete(string FriendIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrFriendIDs = Strings.Split(FriendIDs, ",");

                if (arrFriendIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrFriendIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM FriendsList WHERE ID=@FriendID", conn))
                        {
                            cmd.Parameters.AddWithValue("@FriendID", arrFriendIDs[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Friend(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Friendses the save.
        /// </summary>
        /// <param name="FriendUserName">Name of the friend user.</param>
        /// <param name="Username">The username.</param>
        /// <returns>System.String.</returns>
        public static string Friends_Save(string FriendUserName, string Username)
        {
            var alreadyAdded = false;
            var Approved = "1";

            if (Strings.LCase(Strings.Trim(Username)) == Strings.LCase(FriendUserName))
            {
                return SepFunctions.LangText("You cannot add yourself as a friend.");
            }

            var UserID = SepFunctions.GetUserID(Username);

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var sFriendUserID = string.Empty;
                using (var cmd = new SqlCommand("SELECT UserID,ApproveFriends,EmailAddress FROM Members WHERE UserName=@FriendUserName AND Status=1", conn))
                {
                    cmd.Parameters.AddWithValue("@FriendUserName", FriendUserName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sFriendUserID = SepFunctions.openNull(RS["UserID"]);
                            if (SepFunctions.openNull(RS["ApproveFriends"]) == "Yes")
                            {
                                Approved = "0";
                                var EmailSubject = SepFunctions.LangText("A friend is waiting approval on ~~" + SepFunctions.Setup(992, "WebSiteName") + "~~");
                                var EmailBody = SepFunctions.LangText("~~" + SepFunctions.Session_User_Name() + "~~ is trying to add you to their friends list!") + "<br/><br/>" + SepFunctions.LangText("To approve this friend please visit ~~<a href=\"" + SepFunctions.GetSiteDomain() + "friends.aspx\" target=\"_blank\">" + SepFunctions.GetSiteDomain() + "friends.aspx</a>~~.") + "<br/><br/><b>" + SepFunctions.LangText("Username") + "</b> " + SepFunctions.Session_User_Name() + "<br/><br/>" + SepFunctions.LangText("If you have any questions regarding this email or your account, please feel free to email us by responding to this message.");
                                SepFunctions.Send_Email(SepFunctions.openNull(RS["EmailAddress"]), SepFunctions.Setup(991, "AdminEmailAddress"), EmailSubject, EmailBody, 0);
                            }
                        }
                        else
                        {
                            return SepFunctions.LangText("User does not exist on our web site.");
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT UserID FROM FriendsList WHERE UserID=@UserID AND AddedUserID=@AddedUserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@AddedUserID", sFriendUserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            alreadyAdded = true;
                        }
                    }
                }

                if (alreadyAdded == false)
                {
                    using (var cmd = new SqlCommand("INSERT INTO FriendsList (UserID, AddedUserID, Approved) VALUES (@UserID, @AddedUserID, @Approved)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@AddedUserID", sFriendUserID);
                        cmd.Parameters.AddWithValue("@Approved", Approved);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            var sReturn = string.Empty;

            if (alreadyAdded)
            {
                sReturn = SepFunctions.LangText("User is already on your friends list.");
            }

            sReturn = SepFunctions.LangText("User has been successfully added to your friends list.");
            return sReturn;
        }

        /// <summary>
        /// Gets the friends.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="ApprovedFriends">if set to <c>true</c> [approved friends].</param>
        /// <returns>List&lt;Models.FriendsList&gt;.</returns>
        public static List<FriendsList> GetFriends(string SortExpression = "UserName", string SortDirection = "ASC", string searchWords = "", bool ApprovedFriends = true)
        {
            var lFriendsList = new List<FriendsList>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "UserName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (M.UserName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (ApprovedFriends)
            {
                wClause += " AND FL.UserID='" + SepFunctions.Session_User_ID() + "' AND M.UserID=FL.AddedUserID";
            }
            else
            {
                wClause += " AND FL.AddedUserID='" + SepFunctions.Session_User_ID() + "' AND FL.Approved='0' AND M.UserID=FL.UserID";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT FL.ID,FL.AddedUserID,M.Username,M.BirthDate,M.Male,FL.Approved," + SepFunctions.Upload_SQL_Select(string.Empty, 63, "FileName", "M.UserID") + ", (SELECT TOP 1 UserID FROM OnlineUsers WHERE Location <> 'Logout' AND (CurrentStatus <> 'DELETED' OR CurrentStatus IS NULL OR CurrentStatus <> '') AND UserID=M.UserID) AS isOnline FROM FriendsList AS FL,Members AS M WHERE M.Status=1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dFriendsList = new Models.FriendsList { FriendID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ID"])) };
                    dFriendsList.AddedUserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["AddedUserID"]);
                    dFriendsList.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dFriendsList.BirthDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["BirthDate"]));
                    dFriendsList.Gender = Strings.ToString(SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["Male"])) ? "Male" : "Female");
                    dFriendsList.Approved = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["Approved"]));
                    dFriendsList.isOnline = SepFunctions.toBoolean(SepFunctions.openNull(ds.Tables[0].Rows[i]["isOnline"]));
                    lFriendsList.Add(dFriendsList);
                }
            }

            return lFriendsList;
        }
    }
}