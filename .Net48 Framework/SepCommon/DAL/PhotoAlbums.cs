// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PhotoAlbums.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class PhotoAlbums.
    /// </summary>
    public static class PhotoAlbums
    {
        /// <summary>
        /// Albums the delete.
        /// </summary>
        /// <param name="AlbumIDs">The album i ds.</param>
        /// <returns>System.String.</returns>
        public static string Album_Delete(string AlbumIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAlbumIDs = Strings.Split(AlbumIDs, ",");

                if (arrAlbumIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAlbumIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE PhotoAlbums SET Status='-1', DateDeleted=@DateDeleted WHERE AlbumID=@AlbumID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AlbumID", arrAlbumIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(28, arrAlbumIDs[i]);
                    }
                }
            }

            return SepFunctions.LangText("Album(s) has been successfully deleted.");
        }

        /// <summary>
        /// Albums the get.
        /// </summary>
        /// <param name="AlbumID">The album identifier.</param>
        /// <returns>Models.PhotoAlbums.</returns>
        public static Models.PhotoAlbums Album_Get(long AlbumID)
        {
            var returnXML = new Models.PhotoAlbums();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM PhotoAlbums WHERE AlbumID=@AlbumID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.AlbumID = SepFunctions.toLong(SepFunctions.openNull(RS["AlbumID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.AlbumName = SepFunctions.openNull(RS["AlbumName"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.SharedAlbum = SepFunctions.toBoolean(SepFunctions.openNull(RS["SharedAlbum"]));
                            returnXML.Password = SepFunctions.openNull(RS["Password"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Albums the save.
        /// </summary>
        /// <param name="AlbumID">The album identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="AlbumName">Name of the album.</param>
        /// <param name="Description">The description.</param>
        /// <param name="SharedAlbum">if set to <c>true</c> [shared album].</param>
        /// <param name="AlbumPassword">The album password.</param>
        /// <returns>System.Int32.</returns>
        public static int Album_Save(long AlbumID, string UserID, string AlbumName, string Description, bool SharedAlbum, string AlbumPassword)
        {
            var bUpdate = false;
            var isNewRecord = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (AlbumID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT AlbumID FROM PhotoAlbums WHERE AlbumID=@AlbumID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
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
                    AlbumID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE PhotoAlbums SET AlbumName=@AlbumName, Description=@Description, SharedAlbum=@SharedAlbum, Password=@Password WHERE AlbumID=@AlbumID";
                }
                else
                {
                    SqlStr = "INSERT INTO PhotoAlbums (AlbumID, AlbumName, Description, SharedAlbum, Password, UserID, PortalID, Status) VALUES (@AlbumID, @AlbumName, @Description, @SharedAlbum, @Password, @UserID, @PortalID, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
                    cmd.Parameters.AddWithValue("@AlbumName", AlbumName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@SharedAlbum", SharedAlbum);
                    cmd.Parameters.AddWithValue("@Password", AlbumPassword);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 28, Strings.ToString(AlbumID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Album", "Photo Album", "CreateAlbum");
            if (intReturn == 0)
            {
                if (bUpdate)
                {
                    return 2;
                }

                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Gets the photo albums.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="onlyShared">if set to <c>true</c> [only shared].</param>
        /// <returns>List&lt;Models.PhotoAlbums&gt;.</returns>
        public static List<Models.PhotoAlbums> GetPhotoAlbums(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "", string UserID = "", bool onlyShared = false)
        {
            var lPhotoAlbums = new List<Models.PhotoAlbums>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Username";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND M.Username LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND P.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (onlyShared)
            {
                wClause += " AND P.SharedAlbum='1'";
            }

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT P.AlbumID,M.Username,P.AlbumName,P.Description,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='28' AND UniqueID=P.AlbumID AND Uploads.isTemp='0' AND Uploads.Approved='1' ORDER BY Weight) AS UploadID FROM PhotoAlbums AS P,Members AS M WHERE P.UserID=M.UserID AND P.PortalID=@PortalID AND P.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
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

                    var dPhotoAlbums = new Models.PhotoAlbums { AlbumID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["AlbumID"])) };
                    dPhotoAlbums.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dPhotoAlbums.AlbumName = SepFunctions.openNull(ds.Tables[0].Rows[i]["AlbumName"]);
                    dPhotoAlbums.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dPhotoAlbums.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=28&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        dPhotoAlbums.DefaultPicture = sImageFolder + "images/public/no-photo.jpg";
                    }

                    lPhotoAlbums.Add(dPhotoAlbums);
                }
            }

            return lPhotoAlbums;
        }

        /// <summary>
        /// Gets the photo albums users.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="onlyShared">if set to <c>true</c> [only shared].</param>
        /// <returns>List&lt;Models.PhotoAlbums&gt;.</returns>
        public static List<Models.PhotoAlbums> GetPhotoAlbumsUsers(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "", string UserID = "", bool onlyShared = false)
        {
            var lPhotoAlbums = new List<Models.PhotoAlbums>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Username";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND M.Username LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wClause += " AND M.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            if (onlyShared)
            {
                wClause += " AND P.SharedAlbum='1'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT DISTINCT M.Username, M.UserID FROM PhotoAlbums AS P,Members AS M WHERE P.UserID=M.UserID AND P.PortalID=@PortalID AND P.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
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

                    var dPhotoAlbums = new Models.PhotoAlbums { Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]) };
                    dPhotoAlbums.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    lPhotoAlbums.Add(dPhotoAlbums);
                }
            }

            return lPhotoAlbums;
        }
    }
}