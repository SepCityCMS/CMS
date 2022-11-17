// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CategoryLayout.cs" company="SepCity, Inc.">
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
    /// Class CategoryLayout.
    /// </summary>
    public class CategoryLayout
    {
        /// <summary>
        /// The m category identifier
        /// </summary>
        private long m_CategoryID;

        /// <summary>
        /// The m page name
        /// </summary>
        private string m_PageName;

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        public long CategoryID
        {
            get => Convert.ToInt64(m_CategoryID);

            set => m_CategoryID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName
        {
            get => Strings.ToString(m_PageName);

            set => m_PageName = value;
        }

        /// <summary>
        /// Categories the contents.
        /// </summary>
        /// <param name="strPageName">Name of the string page.</param>
        /// <param name="iModuleID">The i module identifier.</param>
        /// <param name="acount">The acount.</param>
        /// <param name="iTotalRecords">The i total records.</param>
        /// <param name="sFileName">Name of the s file.</param>
        /// <param name="sCatID">The s cat identifier.</param>
        /// <param name="sCategoryName">Name of the s category.</param>
        /// <param name="sDescription">The s description.</param>
        /// <param name="CatCount">The cat count.</param>
        /// <returns>System.String.</returns>
        public string Category_Contents(ref string strPageName, int iModuleID, long acount, long iTotalRecords, string sFileName, string sCatID, string sCategoryName, string sDescription, long CatCount)
        {
            var str = new StringBuilder();
            var href = string.Empty;
            var sCatCount = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            sCatCount = " (" + CatCount + ")";

            if (acount % 2 == 0)
            {
                if (acount > 0)
                {
                    str.Append("</tr>");
                }

                str.Append("<tr>");
            }

            if (!string.IsNullOrWhiteSpace(sFileName))
            {
                str.Append("<td valign=\"top\" width=\"1px\"><img src=\"" + sImageFolder + "spadmin/show_image.aspx?ModuleID=" + iModuleID + "&CatID=" + sCatID + "\" border=\"0\" alt=\"\" /></td>");
            }
            else
            {
                str.Append("<td valign=\"top\" width=\"1px\"></td>");
            }

            href = strPageName;
            str.Append("<td valign=\"top\" width=\"50%\"><b><a href=\"" + href + "\" class=\"CategoryMain\">" + sCategoryName + "</a></b>" + sCatCount);
            if (!string.IsNullOrWhiteSpace(sDescription))
            {
                str.Append("<br/>" + sDescription);
            }

            str.Append("</td>");
            if (acount + 1 == iTotalRecords)
            {
                str.Append("</tr>");
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Categories the contents2.
        /// </summary>
        /// <param name="imageTag">The image tag.</param>
        /// <param name="strPageName">Name of the string page.</param>
        /// <param name="acount">The acount.</param>
        /// <param name="iTotalRecords">The i total records.</param>
        /// <param name="sCategoryName">Name of the s category.</param>
        /// <param name="CatCount">The cat count.</param>
        /// <returns>System.String.</returns>
        public string Category_Contents2(string imageTag, string strPageName, long acount, long iTotalRecords, string sCategoryName, long CatCount)
        {
            var str = new StringBuilder();
            var href = string.Empty;
            var sCatCount = string.Empty;

            sCatCount = " (" + CatCount + ")";

            if (acount % 2 == 0)
            {
                if (acount > 0)
                {
                    str.Append("</tr>");
                }

                str.Append("<tr>");
            }

            href = strPageName;
            str.Append("<li>");
            str.Append(imageTag);
            str.Append("<a href=\"" + href + "\">" + sCategoryName + sCatCount + "</a></li>");
            if (acount + 1 == iTotalRecords)
            {
                str.Append("</tr>");
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (ModuleID == 0)
            {
                output.AppendLine("ModuleID is Required");
                return output.ToString();
            }

            var intRecordCount = 0;
            var iTotalRecords = 0;

            var strGetPageURL = string.Empty;

            var SqlStr = string.Empty;
            var wc = string.Empty;

            string[] arrUserKeys = null;

            var ImageData = string.Empty;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var countTable = string.Empty;
            var countID = string.Empty;
            var countWC = string.Empty;
            var CountSqlStr = string.Empty;
            long iCount = 0;

            var UserAccessKeys = SepFunctions.GetUserInformation("AccessKeys");

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (string.IsNullOrWhiteSpace(PageName))
                {
                    PageName = SepFunctions.GetModulePageNames(ModuleID);
                }

                if (ModuleID != 60)
                {
                    wc += " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesPortals WHERE (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalID = -1) AND CatID=CAT.CatID)";
                }

                if (CategoryID > 0)
                {
                    wc += " AND CAT.ListUnder='" + SepFunctions.FixWord(Strings.ToString(CategoryID)) + "'";
                }
                else
                {
                    wc += " AND CAT.ListUnder='0'";
                }

                if (!string.IsNullOrWhiteSpace(UserAccessKeys))
                {
                    wc += " AND (";
                    arrUserKeys = Strings.Split(UserAccessKeys, ",");
                    if (arrUserKeys != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrUserKeys); i++)
                        {
                            if (i > 0)
                            {
                                wc += " OR ";
                            }

                            wc += "(CAT.AccessKeys LIKE '%|" + SepFunctions.FixWord(Strings.Replace(arrUserKeys[i], "|", string.Empty)) + "|%' OR CAT.AccessHide='0')";
                        }
                    }

                    wc += ")";
                }
                else
                {
                    wc += " AND CAT.AccessKeys LIKE '%|1|%' OR CAT.AccessHide='0'";
                }

                switch (ModuleID)
                {
                    case 5:
                        countTable = "DiscountSystem";
                        countID = "DiscountID";
                        countWC = " AND ExpireDate > getDate() AND Quantity > '0' AND ShowWeb='1' AND DiscountSystem.Status=1";
                        break;

                    case 7:
                        countTable = "UPagesSites";
                        countID = "SiteID";
                        countWC = " AND UPagesSites.Status=1";
                        break;

                    case 9:
                        countTable = "FAQ";
                        countID = "FAQID";
                        countWC = " AND FAQ.Status=1";
                        break;

                    case 10:
                        countTable = "LibrariesFiles";
                        countID = "FileID";
                        countWC = " AND LibrariesFiles.Status=1";
                        break;

                    case 12:
                        countTable = "ForumsMessages";
                        countID = "TopicID";
                        countWC = " AND ReplyID='0' AND ForumsMessages.Status=1";
                        break;

                    case 19:
                        countTable = "LinksWebsites";
                        countID = "LinkID";
                        countWC = " AND LinksWebsites.Status=1";
                        break;

                    case 20:
                        countTable = "BusinessListings";
                        countID = "DISTINCT LinkID";
                        countWC = " AND BusinessListings.Status=1";
                        break;

                    case 23:
                        countTable = "News";
                        countID = "NewsID";
                        countWC = " AND News.Status=1 AND News.StartDate <= GETDATE() AND News.EndDate >= GETDATE()";
                        break;

                    case 25:
                        countTable = "PNQQuestions";
                        countID = "PollID";
                        countWC = " AND PNQQuestions.Status=1 AND PNQQuestions.StartDate <= GETDATE() AND PNQQuestions.EndDate >= GETDATE()";
                        break;

                    case 31:
                        countTable = "AuctionAds";
                        countID = "AdID";
                        countWC = " AND AuctionAds.Status=1 AND EndDate > getDate() AND AdID=LinkID";
                        break;

                    case 32:
                        countTable = "RStateProperty";
                        countID = "PropertyID";
                        countWC = " AND RStateProperty.Status=1";
                        break;

                    case 35:
                        countTable = "Articles";
                        countID = "ArticleID";
                        countWC = " AND Articles.Status=1 AND Articles.Start_Date < GETDATE() AND Articles.End_Date > GETDATE()";
                        break;

                    case 37:
                        countTable = "ELearnCourses";
                        countID = "CourseID";
                        countWC = " AND ELearnCourses.StartDate > getDate() AND ELearnCourses.Status=1";
                        break;

                    case 41:
                        countTable = "ShopProducts";
                        countID = "ProductID";
                        countWC = " AND ShopProducts.Status=1";
                        break;

                    case 44:
                        countTable = "ClassifiedsAds";
                        countID = "AdID";
                        countWC = " AND SoldOut='0' AND ClassifiedsAds.Status=1 AND EndDate > getDate() AND AdID=LinkID";
                        break;

                    case 60:
                        countTable = "Portals";
                        countID = "PortalID";
                        countWC = " AND HideList='0' AND Portals.Status=1";
                        break;

                    case 61:
                        countTable = "Blog";
                        countID = "BlogID";
                        countWC = " AND Blog.Status=1 AND StartDate <= GETDATE() AND EndDate >= GETDATE()";
                        break;

                    case 65:
                        countTable = "Vouchers";
                        countID = "VoucherID";
                        countWC = " AND Vouchers.Status=1 AND BuyEndDate > getDate()";
                        break;
                }

                CountSqlStr = "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + " WHERE ";
                CountSqlStr += "CatID IN (SELECT CatID FROM Categories WHERE CatID=" + countTable + ".CatID AND CatID=CAT.CatID)" + countWC + ") AS Count1, ";
                CountSqlStr += "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + " WHERE ";
                CountSqlStr += "CatID IN (SELECT CatID FROM Categories WHERE CatID=" + countTable + ".CatID AND ListUnder=CAT.CatID)" + countWC + ") AS Count2, ";
                CountSqlStr += "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + ",Categories AS CCAT1 WHERE ";
                CountSqlStr += "CCAT1.ListUnder=CAT.CatID " + countWC + " AND " + countTable + ".CatID IN (SELECT CatID FROM Categories WHERE ListUnder=CCAT1.CatID)) AS Count3, ";
                if (CategoryID == 0)
                {
                    CountSqlStr += "(SELECT Count(" + countID + ") AS RecordCount FROM " + countTable + ",Categories AS CCAT1 WHERE ";
                    CountSqlStr += "CCAT1.ListUnder=CAT.CatID " + countWC + " AND " + countTable + ".CatID IN (SELECT ListUnder FROM Categories WHERE ListUnder=CCAT1.ListUnder)) AS Count4 ";
                }
                else
                {
                    CountSqlStr += "'0' AS Count4 ";
                }

                if (ModuleID == 12)
                {
                    SqlStr = "SELECT CAT.CatID,CAT.ListUnder,CAT.CategoryName,CAT.Description,Cat.ImageDatA FROM Categories AS CAT WHERE CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1) AND CAT.Status <> -1" + wc + " ORDER BY CAT.Weight,CAT.CategoryName";
                }
                else
                {
                    SqlStr = "SELECT CAT.CatID,CAT.ListUnder,CAT.CategoryName,CAT.Description,Cat.ImageData," + CountSqlStr + " FROM Categories AS CAT WHERE CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1) AND CAT.Status <> -1" + wc + " ORDER BY CAT.Weight,CAT.CategoryName";
                }

                using (SqlCommand cmd = new SqlCommand(SqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            if (ModuleID == 12)
                            {
                                output.AppendLine("<table width=\"100%\" class=\"forumCatTable\" id=\"Category\">");
                                while (RS.Read())
                                {
                                    output.AppendLine("<tr class=\"TableTitle\">");
                                    output.AppendLine("<td colspan=\"6\"><b>" + SepFunctions.openNull(RS["CategoryName"]) + "</b></td>");
                                    output.AppendLine("</tr>");
                                    output.AppendLine("<tr class=\"TableHeader\">");
                                    output.AppendLine("<td colspan=\"2\"><b>" + SepFunctions.LangText("Topic") + "</b></td>");
                                    output.AppendLine("<td width=\"60\" align=\"center\"><b>" + SepFunctions.LangText("Posts") + "</b></td>");
                                    output.AppendLine("<td width=\"60\" align=\"center\"><b>" + SepFunctions.LangText("Threads") + "</b></td>");
                                    output.AppendLine("<td width=\"90\" align=\"center\" nowrap=\"nowrap\"><b>" + SepFunctions.LangText("Last Post") + "</b></td>");
                                    output.AppendLine("<td align=\"center\"><b>" + SepFunctions.LangText("Moderator") + "</b></td>");
                                    output.AppendLine("</tr>");
                                    SqlStr = "SELECT CatID,ListUnder,CategoryName,Description,Moderator,Cat.ImageData," + SepFunctions.Upload_SQL_Select("CatID", 0) + ",(SELECT Count(F.TopicID) FROM ForumsMessages AS F,Members AS M WHERE F.UserID=M.UserID AND F.Status=1 AND F.CatID=CAT.CatID AND (F.CatID=CAT.CatID AND CAT.Sharing='1' OR F.CatID=CAT.CatID AND CAT.Sharing='0' AND F.PortalID=" + SepFunctions.Get_Portal_ID() + ")) AS TotalThreads,(SELECT Count(F.TopicID) FROM ForumsMessages AS F,Members AS M WHERE F.UserID=M.UserID AND F.ReplyID='0' AND F.Status=1 AND F.CatID=CAT.CatID AND Left(F.Subject,3) <> 'RE:' AND (F.CatID=CAT.CatID AND CAT.Sharing='1' OR F.CatID=CAT.CatID AND CAT.Sharing='0' AND F.PortalID=" + SepFunctions.Get_Portal_ID() + ")) AS ContentCount,(SELECT TOP 1 F.DatePosted FROM ForumsMessages AS F,Members AS M WHERE F.UserID=M.UserID AND F.CatID=CAT.CatID AND (F.CatID=CAT.CatID AND CAT.Sharing='1' OR F.CatID=CAT.CatID AND CAT.Sharing='0' AND F.PortalID=" + SepFunctions.Get_Portal_ID() + ") ORDER BY F.DatePosted DESC) AS LastPost FROM Categories AS CAT WHERE ListUnder='" + SepFunctions.openNull(RS["CatID"], true) + "' AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1) ORDER BY Weight,CategoryName";
                                    using (SqlCommand cmd2 = new SqlCommand(SqlStr, conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            while (RS2.Read())
                                            {
                                                intRecordCount = intRecordCount + 1;
                                                if (SepFunctions.OffSetRows(Convert.ToInt32(intRecordCount)))
                                                {
                                                    output.AppendLine("<tr class=\"TableBody1\">");
                                                }
                                                else
                                                {
                                                    output.AppendLine("<tr class=\"TableBody2\">");
                                                }

                                                strGetPageURL = sInstallFolder + "cat" + ModuleID + "/" + SepFunctions.openNull(RS2["CatID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS2["CategoryName"])) + "/";
                                                output.AppendLine("<td valign=\"top\" align=\"center\" width=\"20\" style=\"padding-right:10px;\">");
                                                if (Information.IsDBNull(RS2["ImageData"]))
                                                {
                                                    ImageData = string.Empty;
                                                }
                                                else
                                                {
                                                    ImageData = SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS2["ImageData"]));
                                                }

                                                if (!string.IsNullOrWhiteSpace(ImageData))
                                                {
                                                    output.AppendLine("<img src=\"" + sImageFolder + "spadmin/show_image.aspx?CatID=" + SepFunctions.openNull(RS2["CatID"]) + "\" border=\"0\" alt=\"\" />");
                                                }
                                                else
                                                {
                                                    output.AppendLine("<img src=\"" + sImageFolder + "images/admin/folder.gif\" alt=\"\" border=\"0\" />");
                                                }

                                                output.AppendLine("</td>");
                                                output.AppendLine("<td valign=\"top\" width=\"100%\"><a href=\"" + strGetPageURL + "\">" + SepFunctions.openNull(RS2["CategoryName"]) + "</a>");
                                                output.AppendLine("<br/>" + SepFunctions.openNull(RS2["Description"]) + "</td>");
                                                output.AppendLine("<td valign=\"top\" align=\"center\">" + SepFunctions.toDouble(SepFunctions.openNull(RS2["ContentCount"])) + "</td>");
                                                var sContentCount = SepFunctions.toLong(SepFunctions.openNull(RS2["ContentCount"])) == 0 ? "0" : SepFunctions.toLong(SepFunctions.openNull(RS2["TotalThreads"])).ToString();
                                                output.AppendLine("<td valign=\"top\" align=\"center\">" + Strings.ToString(sContentCount) + "</td>");
                                                if (Information.IsDate(SepFunctions.openNull(RS2["LastPost"])))
                                                {
                                                    output.AppendLine("<td valign=\"top\" align=\"center\" nowrap=\"nowrap\">" + Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS2["LastPost"])), Strings.DateNamedFormat.ShortDate) + "</td>");
                                                }
                                                else
                                                {
                                                    output.AppendLine("<td valign=\"top\" align=\"center\">" + SepFunctions.LangText("N/A") + "</td>");
                                                }

                                                output.AppendLine("<td valign=\"top\" align=\"center\" nowrap=\"nowrap\">" + SepFunctions.openNull(RS2["Moderator"]) + "</td>");
                                                output.AppendLine("</tr>");
                                            }
                                        }
                                    }

                                    output.AppendLine("<tr class=\"TableSplit\">");
                                    output.AppendLine("<td colspan=\"6\"></td>");
                                    output.AppendLine("</tr>");
                                }

                                output.AppendLine("</table>");
                                output.AppendLine("<br/>");
                            }
                            else
                            {
                                output.AppendLine("<div class=\"list-style1\"><ul>");
                                intRecordCount = 0;
                                iTotalRecords = RS.RecordsAffected;
                                while (RS.Read())
                                {
                                    iCount = SepFunctions.toLong(SepFunctions.openNull(RS["Count1"])) + SepFunctions.toLong(SepFunctions.openNull(RS["Count2"])) + SepFunctions.toLong(SepFunctions.openNull(RS["Count3"])) + SepFunctions.toLong(SepFunctions.openNull(RS["Count4"]));
                                    strGetPageURL = sInstallFolder + "cat" + ModuleID + "/" + SepFunctions.openNull(RS["CatID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["CategoryName"]) + "/");
                                    if (Information.IsDBNull(RS["ImageData"]))
                                    {
                                        ImageData = string.Empty;
                                    }
                                    else
                                    {
                                        ImageData = SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["ImageData"]));
                                    }

                                    if (!string.IsNullOrWhiteSpace(ImageData))
                                    {
                                        ImageData = "<img src=\"" + sImageFolder + "spadmin/show_image.aspx?CatID=" + SepFunctions.openNull(RS["CatID"]) + "\" border=\"0\" alt=\"\" />";
                                    }

                                    output.Append(Category_Contents2(ImageData, strGetPageURL, intRecordCount, iTotalRecords, SepFunctions.openNull(RS["CategoryName"]), iCount));
                                    intRecordCount = intRecordCount + 1;
                                }

                                output.AppendLine("</ul></div>");
                                output.AppendLine("<div style=\"clear:both;\"></div>");
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}