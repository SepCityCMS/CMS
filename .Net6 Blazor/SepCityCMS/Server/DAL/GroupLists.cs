// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="GroupLists.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using Models;
    using SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class GroupLists.
    /// </summary>
    public static class GroupLists
    {
        /// <summary>
        /// Gets the group lists.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.GroupLists&gt;.</returns>
        public static List<Models.GroupLists> GetGroupLists(string SortExpression = "ListName", string SortDirection = "ASC", string searchWords = "")
        {
            var lGroupLists = new List<Models.GroupLists>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "ListName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ListName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT GroupLists.ListID,GroupLists.UserID,GroupLists.ListName,GroupLists.Description,GroupLists.DatePosted,Members.UserName FROM GroupLists, Members WHERE GroupLists.UserID=Members.UserID AND Members.Status <> -1 AND GroupLists.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dGroupLists = new Models.GroupLists { ListID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ListID"])) };
                    dGroupLists.ListName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ListName"]);
                    dGroupLists.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dGroupLists.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    dGroupLists.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    lGroupLists.Add(dGroupLists);
                }
            }

            return lGroupLists;
        }

        /// <summary>
        /// Gets the group lists users.
        /// </summary>
        /// <param name="ListID">The list identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.GroupListsUsers&gt;.</returns>
        public static List<GroupListsUsers> GetGroupListsUsers(long ListID, string SortExpression = "UserName", string SortDirection = "ASC", string searchWords = "")
        {
            var lGroupListsUsers = new List<GroupListsUsers>();

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
                wClause = " AND (UserName LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT GL.ListUserID,M.UserID,M.Username,M.FirstName,M.LastName,M.City,M.State,GL.DatePosted FROM GroupListsUsers AS GL,Members AS M WHERE M.Status <> -1 AND GL.Status <> -1 AND GL.UserID=M.UserID AND GL.ListID='" + SepFunctions.FixWord(Strings.ToString(ListID)) + "'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dGroupListsUsers = new Models.GroupListsUsers { ListUserID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ListUserID"])) };
                    dGroupListsUsers.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dGroupListsUsers.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dGroupListsUsers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dGroupListsUsers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dGroupListsUsers.City = SepFunctions.openNull(ds.Tables[0].Rows[i]["City"]);
                    dGroupListsUsers.State = SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    dGroupListsUsers.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lGroupListsUsers.Add(dGroupListsUsers);
                }
            }

            return lGroupListsUsers;
        }

        /// <summary>
        /// Groups the users delete.
        /// </summary>
        /// <param name="ListID">The list identifier.</param>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Group_Users_Delete(long ListID, string UserIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE GroupListsUsers SET Status='-1', DateDeleted=@DateDeleted WHERE ListID=@ListID AND UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@ListID", ListID);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
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

            sReturn = SepFunctions.LangText("Users(s) has been successfully deleted from this group list.");

            return sReturn;
        }

        /// <summary>
        /// Lists the delete.
        /// </summary>
        /// <param name="ListIDs">The list i ds.</param>
        /// <returns>System.String.</returns>
        public static string List_Delete(string ListIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrListIDs = Strings.Split(ListIDs, ",");

                if (arrListIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrListIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE GroupLists SET Status='-1', DateDeleted=@DateDeleted WHERE ListID=@ListID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ListID", arrListIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE GroupListsUsers SET Status='-1', DateDeleted=@DateDeleted WHERE ListID=@ListID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ListID", arrListIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
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

            sReturn = SepFunctions.LangText("Group List(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Lists the get.
        /// </summary>
        /// <param name="ListID">The list identifier.</param>
        /// <returns>Models.GroupLists.</returns>
        public static Models.GroupLists List_Get(long ListID)
        {
            var returnXML = new Models.GroupLists();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM GroupLists WHERE ListID=@ListID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ListID", ListID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.ListID = SepFunctions.toLong(SepFunctions.openNull(RS["ListID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.Username = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.ListName = SepFunctions.openNull(RS["ListName"]);
                            returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Lists the save.
        /// </summary>
        /// <param name="ListID">The list identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ListName">Name of the list.</param>
        /// <param name="Description">The description.</param>
        /// <returns>System.String.</returns>
        public static string List_Save(long ListID, string UserID, string ListName, string Description)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (ListID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT ListID FROM GroupLists WHERE ListID=@ListID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ListID", ListID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    ListID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE GroupLists SET ListName=@ListName, Description=@Description, UserID=@UserID WHERE ListID=@ListID";
                }
                else
                {
                    SqlStr = "INSERT INTO GroupLists (ListID, ListName, Description, UserID, DatePosted, Status) VALUES (@ListID, @ListName, @Description, @UserID, @DatePosted, '0')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ListID", ListID);
                    cmd.Parameters.AddWithValue("@ListName", ListName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Group list has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Group list has been successfully added.");

            return sReturn;
        }
    }
}