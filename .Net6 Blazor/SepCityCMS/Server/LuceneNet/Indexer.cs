// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Indexer.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.LuceneNet
{
    using SepCore;
    using System.Data.SqlClient;
    using System.IO;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Index record.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="RecordID">Identifier for the record.</param>
        public static void IndexRecord(int iModuleID, long RecordID)
        {
            var indexSQL = string.Empty;
            var moduleUrl = string.Empty;
            var indexUrl = string.Empty;
            var indexIsHtml = false;
            var trimDescription = false;
            var sKeywords = string.Empty;

            switch (iModuleID)
            {
                case 5:
                    indexSQL = "SELECT DiscountID AS RecordId,LabelText AS Title,UserID AS UserID,Disclaimer AS Description,LabelText + ' ' + Disclaimer + ' ' + DiscountCode + ' ' + CompanyName AS Keywords,Disclaimer AS HTML,PostalCode AS PostalCode, PortalID FROM DiscountSystem WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND DiscountID=@RecordId";
                    }

                    moduleUrl = "/discounts/";
                    indexIsHtml = true;
                    break;

                case 9:
                    indexSQL = "SELECT FAQID AS RecordId,Question AS Title,'' AS UserID,Answer AS Description,'' AS Keywords,Answer AS HTML,'' AS PostalCode, PortalID FROM FAQ WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND FAQID=@RecordId";
                    }

                    moduleUrl = "/faq/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 10:
                    indexSQL = "SELECT FileID AS RecordId,Field1 AS Title,UserID,Field2 AS Description,'' AS Keywords,Field2 AS HTML,'' AS PostalCode, PortalID FROM LibrariesFiles WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND FileID=@RecordId";
                    }

                    moduleUrl = "/downloads_view.aspx?FileID=";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 12:
                    indexSQL = "SELECT TopicID AS RecordId,Subject AS Title,UserID,Message AS Description,'' AS Keywords,Message AS HTML,'' AS PostalCode, PortalID FROM ForumsMessages WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND TopicID=@RecordId";
                    }

                    moduleUrl = "/forum/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 19:
                    indexSQL = "SELECT LinkID AS RecordId,LinkName AS Title,UserID,Description,'' AS Keywords,Description AS HTML,'' AS PostalCode, PortalID FROM LinksWebSites WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND LinkID=@RecordId";
                    }

                    moduleUrl = "/linkdir/";
                    trimDescription = true;
                    break;

                case 18:
                    indexSQL = "SELECT P.ProfileID AS RecordId,M.UserName AS Title,M.UserID,P.AboutMe AS Description,'' AS Keywords,P.AboutMe AS HTML,(SELECT ZipCode FROM Members WHERE UserID=P.UserID) AS PostalCode, P.PortalID FROM MatchMaker AS P,Members AS M WHERE P.UserID=M.UserID";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND P.ProfileID=@RecordId";
                    }

                    moduleUrl = "/match/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 20:
                    indexSQL = "SELECT BusinessID AS RecordId,BusinessName AS Title,UserID,Description,'' AS Keywords,FullDescription AS HTML,ZipCode AS PostalCode, PortalID FROM BusinessListings WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND BusinessID=@RecordId";
                    }

                    moduleUrl = "/business/";
                    indexIsHtml = true;
                    break;

                case 31:
                    indexSQL = "SELECT AdID AS RecordId,Title,UserID,Description,'' AS Keywords,Description AS HTML,(SELECT ZipCode FROM Members WHERE UserID=AuctionAds.UserID) AS PostalCode, PortalID FROM AuctionAds WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND AdID=@RecordId";
                    }

                    moduleUrl = "/auction/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 35:
                    indexSQL = "SELECT ArticleID AS RecordId,Headline AS Title,UserID,Summary AS Description,Meta_Keywords AS Keywords,Full_Article AS HTML,'' AS PostalCode, PortalID FROM Articles WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND ArticleId=@RecordId";
                    }

                    moduleUrl = "/article/";
                    indexIsHtml = true;
                    break;

                case 37:
                    indexSQL = "SELECT CourseID AS RecordId,CourseName AS Title,'' AS UserID,'' AS Description,'' AS Keywords,'' AS HTML,'' AS PostalCode, PortalID FROM ELearnCourses WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND CourseID=@RecordId";
                    }

                    moduleUrl = "/course/";
                    indexIsHtml = true;
                    break;

                case 41:
                    indexSQL = "SELECT ProductID AS RecordId,ProductName AS Title,'' AS UserID,ShortDesc AS Description,'' AS Keywords,Description AS HTML,'' AS PostalCode, PortalID FROM ShopProducts WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND ProductID=@RecordId";
                    }

                    moduleUrl = "/shopping/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 44:
                    indexSQL = "SELECT AdID AS RecordId,Title,UserID,Description,'' AS Keywords,Description AS HTML,(SELECT ZipCode FROM Members WHERE UserID=ClassifiedsAds.UserID) AS PostalCode, PortalID FROM ClassifiedsAds WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND AdID=@RecordId";
                    }

                    moduleUrl = "/classified/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 46:
                    indexSQL = "SELECT EventID AS RecordId,Subject AS Title,UserID,Notes AS Description,'' AS Keywords,Notes AS HTML,'' AS PostalCode, PortalID FROM EventCalendar WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND EventID=@RecordId";
                    }

                    moduleUrl = "/event/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 61:
                    indexSQL = "SELECT BlogID AS RecordId,BlogName AS Title,UserID,Message AS Description,'' AS Keywords,Message AS HTML,'' AS PostalCode, PortalID FROM Blog WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND BlogID=@RecordId";
                    }

                    moduleUrl = "/blogs/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 63:
                    indexSQL = "SELECT P.ProfileID AS RecordId,M.UserName AS Title,M.UserID,P.AboutMe AS Description,'' AS Keywords,P.AboutMe AS HTML,(SELECT ZipCode FROM Members WHERE UserID=P.UserID) AS PostalCode, P.PortalID FROM Profiles AS P,Members AS M WHERE P.UserID=M.UserID";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND P.ProfileID=@RecordId";
                    }

                    moduleUrl = "/profile/";
                    indexIsHtml = true;
                    trimDescription = true;
                    break;

                case 999:
                    indexSQL = "SELECT UniqueID AS RecordId,LinkText AS Title,'' AS UserID,Description,Keywords,PageText AS HTML,'' AS PostalCode, '0' AS PortalID FROM ModulesNPages WHERE Status=1";
                    if (RecordID > 0)
                    {
                        indexSQL += " AND UniqueID=@RecordId";
                    }

                    moduleUrl = "/page/";
                    indexIsHtml = true;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(indexSQL))
            {
                using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(indexSQL, conn))
                    {
                        cmd.CommandTimeout = 600;
                        if (RecordID > 0)
                        {
                            cmd.Parameters.AddWithValue("@RecordId", RecordID);
                            using (SqlDataReader IndexRS = cmd.ExecuteReader())
                            {
                                if (IndexRS.HasRows)
                                {
                                    IndexRS.Read();
                                    if (Server.SepFunctions.toLong(Server.SepFunctions.openNull(IndexRS["PortalID"])) > 0)
                                    {
                                        indexUrl = "/portals/" + Server.SepFunctions.toLong(Server.SepFunctions.openNull(IndexRS["PortalID"]));
                                    }
                                    else
                                    {
                                        indexUrl = string.Empty;
                                    }

                                    switch (iModuleID)
                                    {
                                        case 10:
                                            var jDownloads = DAL.Downloads.Download_Get(RecordID);
                                            switch (Path.GetExtension(jDownloads.FileName))
                                            {
                                                case ".docx":
                                                    sKeywords = Server.SepFunctions.DocX2Text(Server.SepFunctions.GetDirValue("downloads") + jDownloads.FileName);
                                                    break;

                                                case ".pdf":
                                                    sKeywords = Server.SepFunctions.PDF2Text(Server.SepFunctions.GetDirValue("downloads") + jDownloads.FileName);
                                                    break;
                                            }

                                            indexUrl += moduleUrl + Server.SepFunctions.openNull(IndexRS["RecordId"]);
                                            break;

                                        case 18:
                                            sKeywords = Server.SepFunctions.GetUserInformation("Sex", Server.SepFunctions.openNull(IndexRS["UserID"]));
                                            indexUrl += moduleUrl + Server.SepFunctions.openNull(IndexRS["RecordId"]) + "/" + Server.SepFunctions.Format_ISAPI(Server.SepFunctions.openNull(IndexRS["Title"])) + "/";
                                            break;

                                        default:
                                            sKeywords = Server.SepFunctions.openNull(IndexRS["Keywords"]);
                                            indexUrl += moduleUrl + Server.SepFunctions.openNull(IndexRS["RecordId"]) + "/" + Server.SepFunctions.Format_ISAPI(Server.SepFunctions.openNull(IndexRS["Title"])) + "/";
                                            break;
                                    }

                                    AddText(iModuleID, RecordID, Server.SepFunctions.openNull(IndexRS["Title"]), trimDescription ? Strings.Left(Server.SepFunctions.RemoveHTML(Server.SepFunctions.openNull(IndexRS["Description"])), 300) : Server.SepFunctions.openNull(IndexRS["Description"]), sKeywords, Server.SepFunctions.openNull(IndexRS["HTML"]), indexUrl, indexIsHtml, Server.SepFunctions.openNull(IndexRS["PostalCode"]), Server.SepFunctions.openNull(IndexRS["UserID"]));
                                }
                            }
                        }
                        else
                        {
                            using (SqlDataReader IndexRS = cmd.ExecuteReader())
                            {
                                while (IndexRS.Read())
                                {
                                    if (Server.SepFunctions.toLong(Server.SepFunctions.openNull(IndexRS["PortalID"])) > 0)
                                    {
                                        indexUrl = "/portals/" + Server.SepFunctions.toLong(Server.SepFunctions.openNull(IndexRS["PortalID"]));
                                    }
                                    else
                                    {
                                        indexUrl = string.Empty;
                                    }

                                    switch (iModuleID)
                                    {
                                        case 10:
                                            var jDownloads = DAL.Downloads.Download_Get(Server.SepFunctions.toLong(Server.SepFunctions.openNull(IndexRS["RecordId"])));
                                            switch (Strings.LCase(Path.GetExtension(jDownloads.FileName)))
                                            {
                                                case ".docx":
                                                    sKeywords = Server.SepFunctions.DocX2Text(Server.SepFunctions.GetDirValue("downloads") + jDownloads.FileName);
                                                    break;

                                                case ".pdf":
                                                    sKeywords = Server.SepFunctions.PDF2Text(Server.SepFunctions.GetDirValue("downloads") + jDownloads.FileName);
                                                    break;
                                            }

                                            indexUrl += moduleUrl + Server.SepFunctions.openNull(IndexRS["RecordId"]);
                                            break;

                                        case 18:
                                            sKeywords = Server.SepFunctions.GetUserInformation("Sex", Server.SepFunctions.openNull(IndexRS["UserID"]));
                                            indexUrl += moduleUrl + Server.SepFunctions.openNull(IndexRS["RecordId"]) + "/" + Server.SepFunctions.Format_ISAPI(Server.SepFunctions.openNull(IndexRS["Title"])) + "/";
                                            break;

                                        default:
                                            sKeywords = Server.SepFunctions.openNull(IndexRS["Keywords"]);
                                            indexUrl += moduleUrl + Server.SepFunctions.openNull(IndexRS["RecordId"]) + "/" + Server.SepFunctions.Format_ISAPI(Server.SepFunctions.openNull(IndexRS["Title"])) + "/";
                                            break;
                                    }

                                    AddText(iModuleID, Server.SepFunctions.toLong(Server.SepFunctions.openNull(IndexRS["RecordId"])), Server.SepFunctions.openNull(IndexRS["Title"]), trimDescription ? Strings.Left(Server.SepFunctions.RemoveHTML(Server.SepFunctions.openNull(IndexRS["Description"])), 300) : Server.SepFunctions.openNull(IndexRS["Description"]), sKeywords, Server.SepFunctions.openNull(IndexRS["HTML"]), indexUrl, indexIsHtml, Server.SepFunctions.openNull(IndexRS["PostalCode"]), Server.SepFunctions.openNull(IndexRS["UserID"]));
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reindex database.
        /// </summary>
        public static void ReindexDatabase()
        {
            if (Server.SepFunctions.ModuleActivated(5))
            {
                IndexRecord(5, 0);
            }

            if (Server.SepFunctions.ModuleActivated(9))
            {
                IndexRecord(9, 0);
            }

            if (Server.SepFunctions.ModuleActivated(10))
            {
                IndexRecord(10, 0);
            }

            if (Server.SepFunctions.ModuleActivated(12))
            {
                IndexRecord(12, 0);
            }

            if (Server.SepFunctions.ModuleActivated(18))
            {
                IndexRecord(18, 0);
            }

            if (Server.SepFunctions.ModuleActivated(19))
            {
                IndexRecord(19, 0);
            }

            if (Server.SepFunctions.ModuleActivated(20))
            {
                IndexRecord(20, 0);
            }

            if (Server.SepFunctions.ModuleActivated(31))
            {
                IndexRecord(31, 0);
            }

            if (Server.SepFunctions.ModuleActivated(35))
            {
                IndexRecord(35, 0);
            }

            if (Server.SepFunctions.ModuleActivated(37))
            {
                IndexRecord(37, 0);
            }

            if (Server.SepFunctions.ModuleActivated(41))
            {
                IndexRecord(41, 0);
            }

            if (Server.SepFunctions.ModuleActivated(44))
            {
                IndexRecord(44, 0);
            }

            if (Server.SepFunctions.ModuleActivated(61))
            {
                IndexRecord(61, 0);
            }

            if (Server.SepFunctions.ModuleActivated(63))
            {
                IndexRecord(63, 0);
            }

            IndexRecord(999, 0);
        }
    }
}