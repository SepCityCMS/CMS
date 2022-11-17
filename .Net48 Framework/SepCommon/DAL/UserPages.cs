// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UserPages.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class UserPages.
    /// </summary>
    public static class UserPages
    {
        /// <summary>
        /// Deletes the web site.
        /// </summary>
        /// <param name="SiteIDs">The site i ds.</param>
        /// <returns>System.String.</returns>
        public static string Delete_Web_Site(string SiteIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                string[] arrSiteID = null;
                arrSiteID = Strings.Split(SiteIDs, ",");

                if (arrSiteID != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSiteID); i++)
                    {
                        using (var cmd = new SqlCommand("SELECT M.UserID FROM UPagesSites AS U, Members AS M WHERE U.UserID=M.UserID AND U.SiteID=@SiteID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SiteID", arrSiteID[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    using (var cmd2 = new SqlCommand("DELETE FROM UPagesSites WHERE SiteID=@SiteID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@SiteID", arrSiteID[i]);
                                        cmd2.ExecuteNonQuery();
                                    }

                                    using (var cmd2 = new SqlCommand("DELETE FROM UPagesPages WHERE UserID=@UserID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserID", SepFunctions.openNull(RS["UserID"]));
                                        cmd2.ExecuteNonQuery();
                                    }

                                    using (var cmd2 = new SqlCommand("DELETE FROM UPagesGuestbook WHERE UserID=@UserID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserID", SepFunctions.openNull(RS["UserID"]));
                                        cmd2.ExecuteNonQuery();
                                    }

                                    using (var cmd2 = new SqlCommand("DELETE FROM CustomFieldUsers WHERE UserID=@UserID AND ModuleID='7'", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserID", SepFunctions.openNull(RS["UserID"]));
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var sReturn = SepFunctions.LangText("Web site has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Gets the user pages.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.UserPages&gt;.</returns>
        public static List<Models.UserPages> GetUserPages(long CategoryId = -1, string SortExpression = "UserName", string SortDirection = "ASC", string searchWords = "", string StartDate = "")
        {
            var lUserPages = new List<Models.UserPages>();
            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "UserName";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            string wClause = "Mod.UserID=M.UserID";
            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause += " AND M.UserName LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (CategoryId >= 0)
            {
                wClause += " AND Mod.CatID='" + SepFunctions.FixWord(Strings.ToString(CategoryId)) + "'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND Mod.DareCreated > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Mod.SiteID,Mod.SiteName,M.UserID,M.UserName,Mod.Description,Mod.Visits,(SELECT Count(CommentID) FROM Comments WHERE ModuleID='7' AND UniqueID=Mod.SiteID) AS TotalComments FROM UPagesSites AS Mod, Members AS M" + SepFunctions.Category_SQL_Manage_Select(CategoryId, wClause) + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dUserPages = new Models.UserPages { SiteID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteID"])) };
                    dUserPages.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dUserPages.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserName"]);
                    dUserPages.SiteName = SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteName"]);
                    dUserPages.Description = SepFunctions.openNull(ds.Tables[0].Rows[i]["Description"]);
                    dUserPages.TotalComments = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalComments"]));
                    dUserPages.Visits = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Visits"]));
                    lUserPages.Add(dUserPages);
                }
            }

            return lUserPages;
        }

        /// <summary>
        /// Gets the user pages guestbook.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.UserPageGuestbook&gt;.</returns>
        public static List<UserPageGuestbook> GetUserPagesGuestbook(string UserID, string SortExpression = "DatePosted", string SortDirection = "DESC", string searchWords = "")
        {
            var lUserPagesGuestbook = new List<UserPageGuestbook>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DatePosted";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND M.EmailAddress LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM UPagesGuestbook WHERE UserID='" + SepFunctions.FixWord(UserID) + "'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dUserPagesGuestbook = new Models.UserPageGuestbook { EntryID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["EntryID"])) };
                    dUserPagesGuestbook.UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]);
                    dUserPagesGuestbook.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dUserPagesGuestbook.SiteURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["SiteURL"]);
                    dUserPagesGuestbook.Message = SepFunctions.openNull(ds.Tables[0].Rows[i]["Message"]);
                    dUserPagesGuestbook.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    lUserPagesGuestbook.Add(dUserPagesGuestbook);
                }
            }

            return lUserPagesGuestbook;
        }

        /// <summary>
        /// Gets the user pages pages.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.UserPagesPages&gt;.</returns>
        public static List<UserPagesPages> GetUserPagesPages(string UserID, string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "")
        {
            var lUserPages = new List<UserPagesPages>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Weight";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "ASC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND M.PageTitle LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM UPagesPages WHERE UserID='" + SepFunctions.FixWord(UserID) + "'" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dUserPages = new Models.UserPagesPages { PageID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PageID"])) };
                    dUserPages.PageName = SepFunctions.openNull(ds.Tables[0].Rows[i]["PageName"]);
                    dUserPages.PageTitle = SepFunctions.openNull(ds.Tables[0].Rows[i]["PageTitle"]);
                    dUserPages.ImageFolder = SepFunctions.GetInstallFolder(true);
                    dUserPages.Weight = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                    dUserPages.RowNumber = i + 1;
                    lUserPages.Add(dUserPages);
                }
            }

            return lUserPages;
        }

        /// <summary>
        /// Guestbooks the delete.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="EntryIDs">The entry i ds.</param>
        /// <returns>System.String.</returns>
        public static string Guestbook_Delete(string UserID, string EntryIDs)
        {
            var arrEntryIDs = Strings.Split(EntryIDs, ",");

            if (arrEntryIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrEntryIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM UPagesGuestbook WHERE EntryID=@EntryID AND UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@EntryID", arrEntryIDs[i]);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Entry(s) has been successfully deleted.");
        }

        /// <summary>
        /// Guestbooks the save.
        /// </summary>
        /// <param name="EntryID">The entry identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="SiteURL">The site URL.</param>
        /// <param name="Message">The message.</param>
        /// <returns>System.String.</returns>
        public static string Guestbook_Save(long EntryID, string UserID, string EmailAddress, string SiteURL, string Message)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (EntryID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT EntryID FROM UPagesGuestbook WHERE EntryID=@EntryID", conn))
                    {
                        cmd.Parameters.AddWithValue("@EntryID", EntryID);
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
                    EntryID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE UPagesGuestbook SET UserID=@UserID, EmailAddress=@EmailAddress, SiteURL=@SiteURL, Message=@Message WHERE EntryID=@EntryID";
                }
                else
                {
                    SqlStr = "INSERT INTO UPagesGuestbook (EntryID, UserID, EmailAddress, SiteURL, Message, DatePosted) VALUES (@EntryID, @UserID, @EmailAddress, @SiteURL, @Message, @DatePosted)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@EntryID", EntryID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                    cmd.Parameters.AddWithValue("@SiteURL", SiteURL);
                    cmd.Parameters.AddWithValue("@Message", Message);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("Entry has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Entry has been successfully saved.");
            }

            return sReturn;
        }

        /// <summary>
        /// Pages the delete.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="PageIDs">The page i ds.</param>
        /// <returns>System.String.</returns>
        public static string Page_Delete(string UserID, string PageIDs)
        {
            var arrPageIDs = Strings.Split(PageIDs, ",");

            if (arrPageIDs != null)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    for (var i = 0; i <= Information.UBound(arrPageIDs); i++)
                    {
                        using (var cmd = new SqlCommand("DELETE FROM UPagesPages WHERE PageID=@PageID AND UserID=@UserID AND (PageName <> 'default.aspx' AND PageName <> 'guestbook.aspx')", conn))
                        {
                            cmd.Parameters.AddWithValue("@PageID", arrPageIDs[i]);
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("Web Page(s) has been successfully deleted.");
        }

        /// <summary>
        /// Pages the get.
        /// </summary>
        /// <param name="PageID">The page identifier.</param>
        /// <returns>Models.UserPagesPages.</returns>
        public static UserPagesPages Page_Get(long PageID)
        {
            var returnXML = new Models.UserPagesPages();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM UPagesPages WHERE PageID=@PageID", conn))
                {
                    cmd.Parameters.AddWithValue("@PageID", PageID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.PageID = SepFunctions.toLong(SepFunctions.openNull(RS["PageID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.MenuID = SepFunctions.toInt(SepFunctions.openNull(RS["MenuID"]));
                            returnXML.PageName = SepFunctions.openNull(RS["PageName"]);
                            returnXML.PageTitle = SepFunctions.openNull(RS["PageTitle"]);
                            returnXML.PageText = SepFunctions.openNull(RS["PageText"]);
                            returnXML.TemplateID = SepFunctions.toLong(SepFunctions.openNull(RS["TemplateID"]));
                            returnXML.Weight = SepFunctions.toLong(SepFunctions.openNull(RS["Weight"]));
                            returnXML.Password = SepFunctions.openNull(RS["Password"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Saves the web page.
        /// </summary>
        /// <param name="PageID">The page identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="MenuID">The menu identifier.</param>
        /// <param name="PageTitle">The page title.</param>
        /// <param name="PageText">The page text.</param>
        /// <param name="TemplateID">The template identifier.</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="PagePassword">The page password.</param>
        /// <returns>System.Int32.</returns>
        public static int Save_Web_Page(long PageID, string UserID, int MenuID, string PageTitle, string PageText, long TemplateID, long Weight, string PagePassword)
        {
            var bUpdate = false;
            var isNewRecord = false;
            string PageName = Strings.LCase(SepFunctions.ReplaceSpecial(SepFunctions.RemoveHTML(Strings.Replace(PageTitle, " ", string.Empty)))) + ".aspx";
            if (PageName == "homepage.aspx")
            {
                PageName = "default.aspx";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (PageID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT PageID FROM UPagesPages WHERE PageID=@PageID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PageID", PageID);
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
                    PageID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE UPagesPages SET UserID=@UserID, MenuID=@MenuID, PageName=@PageName, PageTitle=@PageTitle, PageText=@PageText, TemplateID=@TemplateID, Password=@Password WHERE PageID=@PageID";
                }
                else
                {
                    SqlStr = "INSERT INTO UPagesPages (PageID, UserID, MenuID, PageName, PageTitle, PageText, TemplateID, Weight, Password) VALUES (@PageID, @UserID, @MenuID, @PageName, @PageTitle, @PageText, @TemplateID, @Weight, @Password)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@PageID", PageID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@MenuID", MenuID);
                    cmd.Parameters.AddWithValue("@PageName", PageName);
                    cmd.Parameters.AddWithValue("@PageTitle", PageTitle);
                    cmd.Parameters.AddWithValue("@PageText", PageText);
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.Parameters.AddWithValue("@Password", !string.IsNullOrWhiteSpace(PagePassword) ? PagePassword : string.Empty);
                    cmd.ExecuteNonQuery();
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 7, Strings.ToString(PageID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "WebPage", "Web Page", string.Empty);

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
        /// Saves the web site.
        /// </summary>
        /// <param name="SiteID">The site identifier.</param>
        /// <param name="CatID">The cat identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="SiteSlogan">The site slogan.</param>
        /// <param name="Description">The description.</param>
        /// <param name="TemplateID">The template identifier.</param>
        /// <param name="ShowList">if set to <c>true</c> [show list].</param>
        /// <param name="InviteOnly">if set to <c>true</c> [invite only].</param>
        /// <param name="EnableGuestbook">if set to <c>true</c> [enable guestbook].</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Save_Web_Site(long SiteID, long CatID, string UserID, string SiteName, string SiteSlogan, string Description, long TemplateID, bool ShowList, bool InviteOnly, bool EnableGuestbook, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SiteID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT SiteID FROM UPagesSites WHERE SiteID=@SiteID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SiteID", SiteID);
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
                    SiteID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE UPagesSites SET SiteName=@SiteName, CatID=@CatID, InviteOnly=@InviteOnly, ShowList=@ShowList, SiteSlogan=@SiteSlogan, Description=@Description, TemplateID=@TemplateID, UserID=@UserID, Guestbook=@Guestbook, PortalID=@PortalID WHERE SiteID=@SiteID";
                }
                else
                {
                    SqlStr = "INSERT INTO UPagesSites (SiteID, CatID, SiteName, InviteOnly, ShowList, SiteSlogan, Description, TemplateID, UserID, Guestbook, PortalID, Visits, DateCreated, Status) VALUES (@SiteID, @CatID, @SiteName, @InviteOnly, @ShowList, @SiteSlogan, @Description, @TemplateID, @UserID, @Guestbook, @PortalID, '0', @DateCreated, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@SiteID", SiteID);
                    cmd.Parameters.AddWithValue("@CatID", CatID);
                    cmd.Parameters.AddWithValue("@SiteName", SiteName);
                    cmd.Parameters.AddWithValue("@InviteOnly", InviteOnly);
                    cmd.Parameters.AddWithValue("@ShowList", ShowList);
                    cmd.Parameters.AddWithValue("@SiteSlogan", SiteSlogan);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Guestbook", EnableGuestbook);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                if (bUpdate == false)
                {
                    if (EnableGuestbook)
                    {
                        Save_Web_Page(SepFunctions.GetIdentity(), UserID, 0, "Guestbook", "<h1>Guestbook</h1>", TemplateID, 0, string.Empty);
                    }

                    Save_Web_Page(SepFunctions.GetIdentity(), UserID, 0, "Home Page", "<h1>Home Page</h1>", TemplateID, 0, string.Empty);
                }
                else
                {
                    if (EnableGuestbook)
                    {
                        using (var cmd = new SqlCommand("SELECT UserID FROM UPagesPages WHERE UserID=@UserID AND PageTitle='Guestbook'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (!RS.HasRows)
                                {
                                    Save_Web_Page(SepFunctions.GetIdentity(), UserID, 0, "Guestbook", "<h1>Guestbook</h1>", TemplateID, 0, string.Empty);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("DELETE FROM UPagesPages WHERE UserID=@UserID AND PageTitle='Guestbook'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Write Additional Data
            if (bUpdate == false)
            {
                isNewRecord = true;
            }

            SepFunctions.Cache_Remove();

            int intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 7, Strings.ToString(SiteID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "WebSite", "Web Site", "CreateSite");
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
        /// Sites the get.
        /// </summary>
        /// <param name="SiteID">The site identifier.</param>
        /// <returns>Models.UserPages.</returns>
        public static Models.UserPages Site_Get(long SiteID)
        {
            var returnXML = new Models.UserPages();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM UPagesSites WHERE SiteID=@SiteID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@SiteID", SiteID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.SiteID = SepFunctions.toLong(SepFunctions.openNull(RS["SiteID"]));
                            returnXML.CatID = SepFunctions.toLong(SepFunctions.openNull(RS["CatID"]));
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.UserName = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                            returnXML.SiteName = SepFunctions.openNull(RS["SiteName"]);
                            returnXML.Slogan = SepFunctions.openNull(RS["SiteSlogan"]);
                            returnXML.Description = SepFunctions.openNull(RS["Description"]);
                            returnXML.TemplateID = SepFunctions.toLong(SepFunctions.openNull(RS["TemplateID"]));
                            returnXML.InviteOnly = SepFunctions.openBoolean(SepFunctions.openNull(RS["InviteOnly"]));
                            returnXML.ShowList = SepFunctions.openBoolean(SepFunctions.openNull(RS["ShowList"]));
                            returnXML.Visits = SepFunctions.toInt(SepFunctions.openNull(RS["Visits"]));
                            returnXML.Guestbook = SepFunctions.toBoolean(SepFunctions.openNull(RS["Guestbook"]));
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Users the identifier to site identifier.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.Int64.</returns>
        public static long UserID_to_SiteID(string UserID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT SiteID FROM UPagesSites WHERE UserID=@UserID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            return SepFunctions.toLong(SepFunctions.openNull(RS["SiteID"]));
                        }
                    }
                    return 0;
                }
            }
        }
    }
}