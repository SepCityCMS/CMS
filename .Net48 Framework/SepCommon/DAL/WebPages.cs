// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="WebPages.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class WebPages.
    /// </summary>
    public static class WebPages
    {
        /// <summary>
        /// Gets the web pages.
        /// </summary>
        /// <param name="MenuID">The menu identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.WebPages&gt;.</returns>
        public static List<Models.WebPages> GetWebPages(long MenuID = 3, string SortExpression = "Weight", string SortDirection = "ASC", string searchWords = "")
        {
            var lWebPages = new List<Models.WebPages>();

            var rowCount = 0;

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
                wClause = " AND (LinkText LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT [UniqueID],[PageID],[MenuID],[ModuleID],[LinkText],[Weight],[Status] FROM ModulesNPages WHERE MenuID=" + MenuID + " AND Activated=1 AND PageID <> 0 AND PageID <> 977 AND Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    if (SepFunctions.ModuleActivated(SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]))))
                    {
                        rowCount += 1;
                        var dWebPages = new Models.WebPages { UniqueID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"])) };
                        dWebPages.PageID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["PageID"]));
                        dWebPages.MenuID = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["MenuID"]));
                        dWebPages.LinkText = SepFunctions.openNull(ds.Tables[0].Rows[i]["LinkText"]);
                        dWebPages.Weight = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Weight"]));
                        if (SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"])) == 1)
                        {
                            dWebPages.Enable = true;
                        }
                        else
                        {
                            dWebPages.Enable = false;
                        }

                        dWebPages.RowNumber = rowCount;
                        lWebPages.Add(dWebPages);
                    }
                }
            }

            return lWebPages;
        }

        /// <summary>
        /// Saves the external link.
        /// </summary>
        /// <param name="PageID">The page identifier.</param>
        /// <param name="MenuID">The menu identifier.</param>
        /// <param name="LinkText">The link text.</param>
        /// <param name="PageURL">The page URL.</param>
        /// <param name="TargetWindow">The target window.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.String.</returns>
        public static string Save_External_Link(long PageID, long MenuID, string LinkText, string PageURL, string TargetWindow, long PortalID)
        {
            var bUpdate = false;
            long GetNewWeight = 0;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();


                string SqlStr;
                if (PortalID == 0)
                {
                    using (var cmd = new SqlCommand("SELECT UniqueID FROM ModulesNPages WHERE UniqueID=@UniqueID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", PageID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                bUpdate = true;
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(TargetWindow))
                    {
                        TargetWindow = "_parent";
                    }

                    if (bUpdate)
                    {
                        SqlStr = "UPDATE ModulesNPages SET LinkText=@LinkText, UserPageName=@PageURL, TargetWindow=@TargetWindow, MenuID=@MenuID WHERE UniqueID=@UniqueID";
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT Count(UniqueID) AS Counter FROM ModulesNPages WHERE MenuID=@MenuID AND Status=1", conn))
                        {
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetNewWeight = SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                                }
                            }
                        }

                        SqlStr = "INSERT INTO ModulesNPages (UniqueID, LinkText, ModuleID, PageText, UserPageName, TargetWindow, MenuID, Status, PageID, Activated, Weight, Description, Keywords, AdminPageName, AccessKeys, EditKeys) VALUES(@UniqueID, @LinkText, '0', '', @PageURL, @TargetWindow, @MenuID, 1, '201', '1', @NewWeight, '', '', '', '', '')";
                    }

                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", PageID);
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
                        cmd.Parameters.AddWithValue("@LinkText", LinkText);
                        cmd.Parameters.AddWithValue("@PageURL", PageURL);
                        cmd.Parameters.AddWithValue("@TargetWindow", TargetWindow);
                        cmd.Parameters.AddWithValue("@NewWeight", GetNewWeight);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("SELECT UniqueID FROM PortalPages WHERE UniqueID=@UniqueID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", PageID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                bUpdate = true;
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(TargetWindow))
                    {
                        TargetWindow = "_parent";
                    }

                    if (bUpdate)
                    {
                        SqlStr = "UPDATE PortalPages SET LinkText=@LinkText, UserPageName=@PageURL, TargetWindow=@TargetWindow, MenuID=@MenuID, PortalIDs=@PortalIDs WHERE UniqueID=@UniqueID AND PortalID=@PortalID";
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT Count(UniqueID) AS Counter FROM PortalPages WHERE MenuID=@MenuID AND Status=1 AND PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetNewWeight = SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                                }
                            }
                        }

                        SqlStr = "INSERT INTO PortalPages (UniqueID, LinkText, PageText, UserPageName, TargetWindow, MenuID, Status, PageID, Weight, Description, Keywords, PortalID, PortalIDs) VALUES(@UniqueID, @LinkText, '', @PageURL, @TargetWindow, @MenuID, 1, '201', @NewWeight, '', '', @PortalID, @PortalIDs)";
                    }

                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", PageID);
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
                        cmd.Parameters.AddWithValue("@LinkText", LinkText);
                        cmd.Parameters.AddWithValue("@PageURL", PageURL);
                        cmd.Parameters.AddWithValue("@TargetWindow", TargetWindow);
                        cmd.Parameters.AddWithValue("@NewWeight", GetNewWeight);
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        if (PortalID == -1)
                        {
                            cmd.Parameters.AddWithValue("@PortalIDs", "|-1|");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PortalIDs", string.Empty);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            SepFunctions.Cache_Remove();

            var sReturn = SepFunctions.LangText("External link has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Saves the web page.
        /// </summary>
        /// <param name="PageID">The page identifier.</param>
        /// <param name="MenuID">The menu identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="LinkText">The link text.</param>
        /// <param name="PageText">The page text.</param>
        /// <param name="SEOPageTitle">The seo page title.</param>
        /// <param name="SEODescription">The seo description.</param>
        /// <param name="SEOKeywords">The seo keywords.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="ManageKeys">The manage keys.</param>
        /// <param name="Status">The status.</param>
        /// <param name="CategoryID">The category identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        /// <returns>System.String.</returns>
        public static string Save_Web_Page(long PageID, long MenuID, string UserID, string LinkText, string PageText, string SEOPageTitle, string SEODescription, string SEOKeywords, string AccessKeys, string ManageKeys, long Status, long CategoryID, int ModuleID, long PortalID, string PortalIDs)
        {
            var bUpdate = false;
            var PageEditKeys = string.Empty;
            var GetNewWeight = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (CategoryID > 0 && ModuleID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT CatID FROM CategoriesPages WHERE CatID=@CatID AND ModuleID=@ModuleID AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", CategoryID);
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }

                    var SqlStr = string.Empty;
                    if (bUpdate)
                    {
                        SqlStr = "UPDATE CategoriesPages SET PageText=@PageText WHERE CatID=@CategoryID AND ModuleID=@ModuleID AND PortalID=@PortalID";
                    }
                    else
                    {
                        SqlStr = "INSERT INTO CategoriesPages (UniqueID,CatID,ModuleID,PortalID,PageText) VALUES(@UniqueID,@CategoryID,@ModuleID,@PortalID,@PageText)";
                    }

                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        cmd.Parameters.AddWithValue("@PageText", PageText);
                        cmd.ExecuteNonQuery();
                    }

                    return SepFunctions.LangText("Web page has been successfully saved.");
                }

                if (PortalID == 0)
                {
                    if (PageID > 0)
                    {
                        if (ModuleID > 0)
                        {
                            using (var cmd = new SqlCommand("SELECT UniqueID FROM ModulesNPages WHERE ModuleID=@ModuleID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        bUpdate = true;
                                        PageID = SepFunctions.toLong(SepFunctions.openNull(RS["UniqueID"]));
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (var cmd = new SqlCommand("SELECT PageID,EditKeys FROM ModulesNPages WHERE UniqueID=@UniqueID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UniqueID", PageID);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) == 200)
                                        {
                                            PageEditKeys = SepFunctions.openNull(RS["EditKeys"]);
                                        }
                                        else
                                        {
                                            PageEditKeys = SepFunctions.Security("AdminEditPage");
                                        }

                                        bUpdate = true;
                                    }
                                    else
                                    {
                                        PageEditKeys = SepFunctions.Security("AdminEditPage");
                                        bUpdate = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        PageID = SepFunctions.GetIdentity();
                        PageEditKeys = SepFunctions.Security("AdminEditPage");
                        bUpdate = false;
                    }

                    if (SepFunctions.CompareKeys(PageEditKeys, false, UserID) == false)
                    {
                        return SepFunctions.LangText("Sorry you do not have access to edit this web page.");
                    }

                    if (bUpdate)
                    {
                        using (var cmd = new SqlCommand("UPDATE ModulesNPages SET LinkText=@LinkText, PageTitle=@PageTitle, PageText=@PageText, Description=@Description, Keywords=@Keywords, AccessKeys=@AccessKeys, EditKeys=@ManageKeys, Status=@Status, MenuID=@MenuID, Activated='1' WHERE UniqueID=@UniqueID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", PageID);
                            cmd.Parameters.AddWithValue("@LinkText", LinkText);
                            cmd.Parameters.AddWithValue("@PageTitle", SEOPageTitle);
                            cmd.Parameters.AddWithValue("@Description", SEODescription);
                            cmd.Parameters.AddWithValue("@Keywords", SEOKeywords);
                            cmd.Parameters.AddWithValue("@PageText", PageText);
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            cmd.Parameters.AddWithValue("@AccessKeys", !string.IsNullOrWhiteSpace(AccessKeys) ? AccessKeys : string.Empty);
                            cmd.Parameters.AddWithValue("@ManageKeys", !string.IsNullOrWhiteSpace(ManageKeys) ? ManageKeys : string.Empty);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.ExecuteNonQuery();
                        }

                        if (Status == 1)
                        {
                            SepFunctions.IndexRecord(995, PageID);
                        }
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT Count(UniqueID) AS Counter FROM ModulesNPages WHERE MenuID=@MenuID AND Activated='1'", conn))
                        {
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetNewWeight = SepFunctions.openNull(RS["Counter"]);
                                }
                            }
                        }

                        using (var cmd = new SqlCommand("INSERT INTO ModulesNPages (UniqueID, PageID, PageTitle, LinkText, UserPageName, Description, Keywords, PageText, MenuID, Status, AccessKeys, EditKeys, Activated, Weight, ModuleID, TargetWindow, AdminPageName, Visits) VALUES(@UniqueID, '200', @PageTitle, @LinkText, 'viewpage.aspx', @Description, @Keywords, @PageText, @MenuID, '1', @AccessKeys, @ManageKeys, '1', @GetNewWeight, '0', '', '', '0')", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", PageID);
                            cmd.Parameters.AddWithValue("@LinkText", LinkText);
                            cmd.Parameters.AddWithValue("@PageTitle", SEOPageTitle);
                            cmd.Parameters.AddWithValue("@Description", SEODescription);
                            cmd.Parameters.AddWithValue("@Keywords", SEOKeywords);
                            cmd.Parameters.AddWithValue("@PageText", PageText);
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                            cmd.Parameters.AddWithValue("@ManageKeys", ManageKeys);
                            cmd.Parameters.AddWithValue("@GetNewWeight", GetNewWeight);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.IndexRecord(995, PageID);
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("SELECT UniqueID FROM PortalPages WHERE UniqueID=@UniqueID AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", PageID);
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                bUpdate = true;
                                PageID = SepFunctions.toLong(SepFunctions.openNull(RS["UniqueID"]));
                            }
                        }
                    }

                    if (bUpdate)
                    {
                        using (var cmd = new SqlCommand("UPDATE PortalPages SET LinkText=@LinkText, PageTitle=@PageTitle, PageText=@PageText, Description=@Description, Keywords=@Keywords, Status=@Status, MenuID=@MenuID, PortalIDs=@PortalIDs WHERE UniqueID=@UniqueID AND PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", PageID);
                            cmd.Parameters.AddWithValue("@LinkText", LinkText);
                            cmd.Parameters.AddWithValue("@PageTitle", SEOPageTitle);
                            cmd.Parameters.AddWithValue("@Description", SEODescription);
                            cmd.Parameters.AddWithValue("@Keywords", SEOKeywords);
                            cmd.Parameters.AddWithValue("@PageText", PageText);
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            cmd.Parameters.AddWithValue("@PortalIDs", PortalIDs);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("INSERT INTO PortalPages (UniqueID, PageID, LinkText, PageTitle, PageText, Description, Keywords, Status, MenuID, PortalID, Weight, UserPageName, Visits, ViewPage, PortalIDs) VALUES(@UniqueID, '200', @LinkText, @PageTitle, @PageText, @Description, @Keywords, @Status, @MenuID, @PortalID, '0', 'viewpage.aspx', '0', '1', @PortalIDs)", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", PageID);
                            cmd.Parameters.AddWithValue("@LinkText", LinkText);
                            cmd.Parameters.AddWithValue("@PageTitle", SEOPageTitle);
                            cmd.Parameters.AddWithValue("@Description", SEODescription);
                            cmd.Parameters.AddWithValue("@Keywords", SEOKeywords);
                            cmd.Parameters.AddWithValue("@PageText", PageText);
                            cmd.Parameters.AddWithValue("@MenuID", MenuID);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            cmd.Parameters.AddWithValue("@PortalIDs", PortalIDs);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            SepFunctions.Cache_Remove();

            var sReturn = SepFunctions.LangText("Web page has been successfully saved.");
            return sReturn;
        }
    }
}