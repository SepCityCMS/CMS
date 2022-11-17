// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="RecycleBin.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    /// <summary>
    /// Class RecycleBin.
    /// </summary>
    public static class RecycleBin
    {
        /// <summary>
        /// Gets the recycle bin items.
        /// </summary>
        /// <param name="iModuleID">The i module identifier.</param>
        /// <returns>List&lt;Models.RecycleBinItems&gt;.</returns>
        public static List<RecycleBinItems> GetRecycleBinItems(int iModuleID)
        {
            var lRecycleBinItems = new List<RecycleBinItems>();

            GetRecycleBinItemsBuild(iModuleID);

            using (var ds = new DataSet())
            {
                SqlDataAdapter da = null;
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT UniqueID,ModuleID,ModuleName,Title,DateDeleted FROM RecycleBin ORDER BY DateDeleted DESC", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (da = new SqlDataAdapter(cmd))
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

                    var dRecycleBinItems = new Models.RecycleBinItems { UniqueID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) };
                    dRecycleBinItems.ModuleID = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]));
                    dRecycleBinItems.ModuleName = SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleName"]);
                    dRecycleBinItems.Title = SepFunctions.openNull(ds.Tables[0].Rows[i]["Title"]);
                    dRecycleBinItems.DateDeleted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateDeleted"]));
                    lRecycleBinItems.Add(dRecycleBinItems);
                }
            }

            return lRecycleBinItems;
        }

        /// <summary>
        /// Recycles the bin delete.
        /// </summary>
        /// <param name="UniqueIDs">The unique i ds.</param>
        /// <returns>System.String.</returns>
        public static string Recycle_Bin_Delete(string UniqueIDs)
        {
            var SqlStr = string.Empty;
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrUniqueIDs = Strings.Split(UniqueIDs, ",");

                if (arrUniqueIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUniqueIDs); i++)
                    {
                        int ModuleID = SepFunctions.toInt(Strings.Split(arrUniqueIDs[i], "||")[0]);
                        switch (ModuleID)
                        {
                            case 1:
                                SqlStr = "DELETE FROM ContentRotator WHERE Status='-1' AND ContentID=@UniqueID";
                                break;

                            case 2:
                                SqlStr = "DELETE FROM Advertisements WHERE Status='-1' AND AdID=@UniqueID";
                                break;

                            case 5:
                                SqlStr = "DELETE FROM DiscountSystem WHERE Status='-1' AND DiscountID=@UniqueID";
                                break;

                            case 9:
                                SqlStr = "DELETE FROM FAQ WHERE Status='-1' AND FAQID=@UniqueID";
                                break;

                            case 10:
                                SqlStr = "DELETE FROM LibrariesFiles WHERE Status='-1' AND FileID=@UniqueID";
                                break;

                            case 12:
                                SqlStr = "DELETE FROM ForumsMessages WHERE Status='-1' AND TopicID=@UniqueID";
                                break;

                            case 13:
                                SqlStr = "DELETE FROM Forms WHERE Status='-1' AND FormID=@UniqueID;DELETE FROM FormAnswers WHERE Status='-1' AND FormID=@UniqueID;DELETE FROM FormSections WHERE Status='-1' AND FormID=@UniqueID;DELETE FROM FormQuestions WHERE Status='-1' AND FormID=@UniqueID;";
                                break;

                            case 14:
                                SqlStr = "DELETE FROM Guestbook WHERE Status='-1' AND EntryID=@UniqueID";
                                break;

                            case 18:
                                SqlStr = "DELETE FROM MatchMaker WHERE Status='-1' AND ProfileID=@UniqueID";
                                break;

                            case 19:
                                SqlStr = "DELETE FROM LinksWebSites WHERE Status='-1' AND LinkID=@UniqueID";
                                break;

                            case 20:
                                SqlStr = "DELETE FROM BusinessListings WHERE Status='-1' AND LinkID=@UniqueID";
                                break;

                            case 23:
                                SqlStr = "DELETE FROM News WHERE Status='-1' AND NewsID=@UniqueID";
                                break;

                            case 24:
                                SqlStr = "DELETE FROM Newsletters WHERE Status='-1' AND LetterID=@UniqueID";
                                break;

                            case 25:
                                SqlStr = "DELETE FROM PNQQuestions WHERE Status='-1' AND PollID=@UniqueID;DELETE FROM PNQOptions WHERE Status='-1' AND PollID=@UniqueID;";
                                break;

                            case 28:
                                SqlStr = "DELETE FROM PhotoAlbums WHERE Status='-1' AND AlbumID=@UniqueID";
                                break;

                            case 31:
                                SqlStr = "DELETE FROM AuctionAds WHERE Status='-1' AND LinkID=@UniqueID";
                                break;

                            case 32:
                                SqlStr = "DELETE FROM RStateProperty WHERE Status='-1' AND PropertyID=@UniqueID;DELETE FROM RStateAgents WHERE Status='-1' AND AgentID=@UniqueID;DELETE FROM RStateBrokers WHERE Status='-1' AND BrokerID=@UniqueID;";
                                break;

                            case 35:
                                SqlStr = "DELETE FROM Articles WHERE Status='-1' AND ArticleID=@UniqueID";
                                break;

                            case 37:
                                SqlStr = "DELETE FROM ELearnCourses WHERE Status='-1' AND CourseID=@UniqueID;DELETE FROM ELearnExams WHERE Status='-1' AND ExamID=@UniqueID;DELETE FROM ELearnHomework WHERE Status='-1' AND HomeID=@UniqueID;";
                                break;

                            case 41:
                                SqlStr = "DELETE FROM ShopProducts WHERE Status='-1' AND ProductID=@UniqueID";
                                break;

                            case 44:
                                SqlStr = "DELETE FROM ClassifiedsAds WHERE Status='-1' AND LinkID=@UniqueID";
                                break;

                            case 46:
                                SqlStr = "DELETE FROM EventCalendar WHERE Status='-1' AND LinkID=@UniqueID;DELETE FROM EventTypes WHERE Status='-1' AND TypeID=@UniqueID";
                                break;

                            case 50:
                                SqlStr = "DELETE FROM Speakers WHERE Status='-1' AND SpeakerID=@UniqueID;DELETE FROM SpeakSpeeches WHERE Status='-1' AND SpeakerID=@UniqueID;DELETE FROM SpeakTopics WHERE Status='-1' AND TopicID=@UniqueID;";
                                break;

                            case 60:
                                SqlStr = "DELETE FROM Portals WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM PortalPages WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Articles WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM AuctionAds WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Blog WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM BusinessListings WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM ClassifiedsAds WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM CustomFieldUsers WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM DiscountSystem WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM ELearnCourses WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM EmailTemplates WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM EventTypes WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM EventCalendar WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM FAQ WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Forms WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM ForumsMessages WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Guestbook WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM LibrariesFiles WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM LinksWebSites WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM MatchMaker WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Members WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM News WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM PhotoAlbums WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Profiles WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM RStateBrokers WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM RStateAgents WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM RStateProperty WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM ShopProducts WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Invoices WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Invoices_Products WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM SpeakSpeeches WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM SpeakTopics WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Speakers WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Vouchers WHERE Status='-1' AND PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Associations WHERE PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM PortalScripts WHERE PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM Uploads WHERE PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM VouchersPurchased WHERE PortalID=@UniqueID;";
                                SqlStr += "DELETE FROM CustomFieldUsers WHERE PortalID=@UniqueID;";
                                if (File.Exists(SepFunctions.GetDirValue("app_data") + "settings-" + arrUniqueIDs[i] + ".xml"))
                                {
                                    File.Delete(SepFunctions.GetDirValue("app_data") + "settings-" + arrUniqueIDs[i] + ".xml");
                                }

                                // Categories
                                using (var cmd3 = new SqlCommand("SELECT CatID FROM CategoriesPortals WHERE PortalID='" + SepFunctions.FixWord(Strings.Split(arrUniqueIDs[i], "||")[1]) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd3.ExecuteReader())
                                    {
                                        if (RS2.RecordsAffected == 1)
                                        {
                                            while (RS2.Read())
                                            {
                                                using (var cmd4 = new SqlCommand("SELECT CatID FROM CategoriesPortals WHERE PortalID='" + SepFunctions.FixWord(Strings.Split(arrUniqueIDs[i], "||")[1]) + "'", conn))
                                                {
                                                    using (SqlDataReader RS3 = cmd4.ExecuteReader())
                                                    {
                                                        if (RS3.RecordsAffected == 1)
                                                        {
                                                            using (var cmd = new SqlCommand("DELETE FROM Categories WHERE CatID=@CatID", conn))
                                                            {
                                                                cmd.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS3["CatID"]));
                                                                cmd.ExecuteNonQuery();
                                                            }

                                                            using (var cmd = new SqlCommand("DELETE FROM CategoriesModules WHERE CatID=@CatID", conn))
                                                            {
                                                                cmd.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS3["CatID"]));
                                                                cmd.ExecuteNonQuery();
                                                            }

                                                            using (var cmd = new SqlCommand("DELETE FROM CategoriesPages WHERE CatID=@CatID", conn))
                                                            {
                                                                cmd.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS3["CatID"]));
                                                                cmd.ExecuteNonQuery();
                                                            }

                                                            using (var cmd = new SqlCommand("DELETE FROM CategoriesPortals WHERE CatID=@CatID", conn))
                                                            {
                                                                cmd.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS3["CatID"]));
                                                                cmd.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            using (var cmd = new SqlCommand("DELETE FROM CategoriesPortals WHERE PortalID=@PortalID", conn))
                                            {
                                                cmd.Parameters.AddWithValue("@PortalID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }

                                // Delete User Pages
                                var GetUserID = string.Empty;
                                using (var cmd3 = new SqlCommand("SELECT M.UserID,M.UserName FROM UPagesSites AS U, Members AS M WHERE U.UserID=M.UserID AND U.PortalID='" + SepFunctions.FixWord(Strings.Split(arrUniqueIDs[i], "||")[1]) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd3.ExecuteReader())
                                    {
                                        while (RS2.Read())
                                        {
                                            GetUserID = SepFunctions.openNull(RS2["UserID"]);
                                            if (Directory.Exists(SepFunctions.GetDirValue("members") + SepFunctions.openNull(RS2["Username"])))
                                            {
                                                Directory.Delete(SepFunctions.GetDirValue("members") + SepFunctions.openNull(RS2["Username"]));
                                            }

                                            using (var cmd = new SqlCommand("DELETE FROM UPagesSites WHERE PortalID=@PortalID", conn))
                                            {
                                                cmd.Parameters.AddWithValue("@PortalID", arrUniqueIDs[i]);
                                                cmd.ExecuteNonQuery();
                                            }

                                            using (var cmd = new SqlCommand("DELETE FROM UPagesPages WHERE UserID=@UserID", conn))
                                            {
                                                cmd.Parameters.AddWithValue("@UserID", GetUserID);
                                                cmd.ExecuteNonQuery();
                                            }

                                            using (var cmd = new SqlCommand("DELETE FROM UPagesGuestbook WHERE UserID=@UserID", conn))
                                            {
                                                cmd.Parameters.AddWithValue("@UserID", GetUserID);
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }

                                break;

                            case 61:
                                SqlStr = "DELETE FROM Blog WHERE Status='-1' AND BlogID=@UniqueID";
                                break;

                            case 63:
                                SqlStr = "DELETE FROM Profiles WHERE Status='-1' AND ProfileID=@UniqueID";
                                break;

                            case 65:
                                SqlStr = "DELETE FROM Vouchers WHERE Status='-1' AND VoucherID=@UniqueID";
                                break;

                            case 974:
                                SqlStr = "DELETE FROM CustomSections WHERE Status='-1' AND SectionID=@UniqueID";
                                break;

                            case 975:
                                SqlStr = "DELETE FROM CustomFields WHERE Status='-1' AND FieldID=@UniqueID;DELETE FROM CustomFieldOptions WHERE FieldID=@UniqueID;DELETE FROM CustomFieldUsers WHERE Status='-1' AND FieldID=@UniqueID";
                                break;

                            case 976:
                                SqlStr = "DELETE FROM Categories WHERE Status='-1' AND CatID=@UniqueID;DELETE FROM CategoriesModules WHERE Status='-1' AND CatID=@UniqueID;DELETE FROM CategoriesPages WHERE CatID=@UniqueID;DELETE FROM CategoriesPortals WHERE CatID=@UniqueID;";
                                SqlStr += "DELETE FROM FAQ WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM LibrariesFiles WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM ForumsMessages WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM BusinessListings WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM Articles WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM ClassifiedsAds WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM AuctionAds WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM ELearnCourses WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM ShopProducts WHERE Status='-1' AND CatID=@UniqueID;";
                                SqlStr += "DELETE FROM Vouchers WHERE Status='-1' AND CatID=@UniqueID;";
                                break;

                            case 979:
                                SqlStr = "DELETE FROM TargetZones WHERE Status='-1' AND ZoneID=@UniqueID;DELETE FROM Advertisements WHERE Status='-1' AND ZoneID=@UniqueID;DELETE FROM ContentRotator WHERE Status='-1' AND ZoneID=@UniqueID;";
                                break;

                            case 983:
                                SqlStr = "DELETE FROM Approvals WHERE Status='-1' AND ApproveID=@UniqueID;DELETE FROM ApprovalXML WHERE Status='-1' AND ApproveID=@UniqueID;DELETE FROM ApprovalEmails WHERE Status='-1' AND ApproveID=@UniqueID;";
                                break;

                            case 984:
                                var sSavedTemplateDir = SepFunctions.GetDirValue("App_Data") + "templates\\saved\\";
                                using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@TemplateID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (Directory.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + " (DELETED)\\"))
                                            {
                                                Directory.Delete(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + " (DELETED)\\", true);
                                            }

                                            if (Directory.Exists(sSavedTemplateDir + SepFunctions.openNull(RS["FolderName"]) + "\\"))
                                            {
                                                Directory.Delete(sSavedTemplateDir + SepFunctions.openNull(RS["FolderName"]) + "\\", true);
                                            }
                                        }
                                    }
                                }

                                SqlStr = "DELETE FROM SiteTemplates WHERE Status='-1' AND TemplateID=@UniqueID";
                                break;

                            case 985:
                                SqlStr = "DELETE FROM Invoices WHERE Status='-1' AND InvoiceID=@UniqueID;DELETE FROM Invoices_Products WHERE Status='-1' AND InvoiceID=@UniqueID;";
                                break;

                            case 986:
                                SqlStr = "DELETE FROM Members WHERE Status='-1' AND UserID=@UniqueID";
                                break;

                            case 987:
                                SqlStr = "DELETE FROM EmailTemplates WHERE Status='-1' AND TemplateID=@UniqueID";
                                break;

                            case 989:
                                SqlStr = "DELETE FROM AccessKeys WHERE Status='-1' AND KeyID=@UniqueID";
                                break;

                            case 990:
                                SqlStr = "DELETE FROM GroupLists WHERE Status='-1' AND ListID=@UniqueID";
                                break;

                            case 995:
                                SqlStr = "DELETE FROM TaxCalculator WHERE Status='-1' AND ID=@UniqueID";
                                break;

                            case 998:
                                SqlStr = "DELETE FROM Activities WHERE Status='-1' AND ActivityID=@UniqueID";
                                break;

                            case 999:
                                SqlStr = "DELETE FROM ModulesNPages WHERE Status='-1' AND UniqueID=@UniqueID";
                                break;
                        }

                        if (!string.IsNullOrWhiteSpace(SqlStr))
                        {
                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                cmd.Parameters.AddWithValue("@UniqueID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                cmd.ExecuteNonQuery();
                            }

                            if (ModuleID == 986)
                            {
                                SqlStr = "SELECT FileName FROM Uploads WHERE UserID=@UniqueID AND ModuleID=@ModuleID";
                            }
                            else
                            {
                                SqlStr = "SELECT FileName FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID";
                            }

                            // Delete from uploads table and file name in downloads and images folder
                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                                cmd.Parameters.AddWithValue("@UniqueID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        while (RS.Read())
                                        {
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["FileName"])))
                                            {
                                                if (ModuleID == 10)
                                                {
                                                    if (File.Exists(SepFunctions.GetDirValue("downloads") + SepFunctions.openNull(RS["FileName"])))
                                                    {
                                                        File.Delete(SepFunctions.GetDirValue("downloads") + SepFunctions.openNull(RS["FileName"]));
                                                    }
                                                }
                                                else
                                                {
                                                    if (File.Exists(SepFunctions.GetDirValue("images") + SepFunctions.openNull(RS["FileName"])))
                                                    {
                                                        File.Delete(SepFunctions.GetDirValue("images") + SepFunctions.openNull(RS["FileName"]));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (ModuleID == 986)
                            {
                                SqlStr = "DELETE FROM Uploads WHERE UserID=@UniqueID AND ModuleID=@ModuleID";
                            }
                            else
                            {
                                SqlStr = "DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID";
                            }

                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                                cmd.Parameters.AddWithValue("@UniqueID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                cmd.ExecuteNonQuery();
                            }

                            // end uploads table
                        }
                        else
                        {
                            bError = " (" + SepFunctions.LangText("Invalid ModuleID") + ")";
                        }
                    }
                }
            }

            string sReturn = SepFunctions.LangText("Recycled Item(s) has been successfully deleted.");

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            return sReturn;
        }

        /// <summary>
        /// Recycles the bin restore.
        /// </summary>
        /// <param name="UniqueIDs">The unique i ds.</param>
        /// <returns>System.String.</returns>
        public static string Recycle_Bin_Restore(string UniqueIDs)
        {
            var SqlStr = string.Empty;

            var bError = string.Empty;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrUniqueIDs = Strings.Split(UniqueIDs, ",");

                if (arrUniqueIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUniqueIDs); i++)
                    {
                        bool restoreCat = false;
                        switch (SepFunctions.toInt(Strings.Split(arrUniqueIDs[i], "||")[0]))
                        {
                            case 1:
                                SqlStr = "UPDATE ContentRotator SET Status=1 WHERE ContentID=@UniqueID";
                                break;

                            case 2:
                                SqlStr = "UPDATE Advertisements SET Status=1 WHERE AdID=@UniqueID";
                                break;

                            case 5:
                                SqlStr = "UPDATE DiscountSystem SET Status=1 WHERE DiscountID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 9:
                                SqlStr = "UPDATE FAQ SET Status=1 WHERE FAQID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 10:
                                SqlStr = "UPDATE LibrariesFiles SET Status=1 WHERE FileID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 12:
                                SqlStr = "UPDATE ForumsMessages SET Status=1 WHERE TopicID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 13:
                                SqlStr = "UPDATE Forms SET Status=1 WHERE FormID=@UniqueID;UPDATE FormSections SET Status=1 WHERE FormID=@UniqueID;UPDATE FormQuestions SET Status=1 WHERE FormID=@UniqueID;UPDATE FormAnswers SET Status=1 WHERE FormID=@UniqueID;";
                                break;

                            case 14:
                                SqlStr = "UPDATE Guestbook SET Status=1 WHERE EntryID=@UniqueID";
                                break;

                            case 18:
                                SqlStr = "UPDATE MatchMaker SET Status=1 WHERE ProfileID=@UniqueID";
                                break;

                            case 19:
                                SqlStr = "UPDATE LinksWebSites SET Status=1 WHERE LinkID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 20:
                                SqlStr = "UPDATE BusinessListings SET Status=1 WHERE LinkID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 23:
                                SqlStr = "UPDATE News SET Status=1 WHERE NewsID=@UniqueID";
                                break;

                            case 24:
                                SqlStr = "UPDATE Newsletters SET Status=1 WHERE LetterID=@UniqueID";
                                break;

                            case 25:
                                SqlStr = "UPDATE PNQQuestions SET Status=1 WHERE PollID=@UniqueID;UPDATE PNQOptions SET Status=1 WHERE PollID=@UniqueID;";
                                break;

                            case 28:
                                SqlStr = "UPDATE PhotoAlbums SET Status=1 WHERE AlbumID=@UniqueID";
                                break;

                            case 31:
                                SqlStr = "UPDATE AuctionAds SET Status=1 WHERE LinkID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 32:
                                SqlStr = "UPDATE RStateProperty SET Status=1 WHERE PropertyID=@UniqueID;UPDATE RStateAgents SET Status=1 WHERE AgentID=@UniqueID;UPDATE RStateBrokers SET Status=1 WHERE BrokerID=@UniqueID;";
                                break;

                            case 35:
                                SqlStr = "UPDATE Articles SET Status=1 WHERE ArticleID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 37:
                                SqlStr = "UPDATE ELearnCourses SET Status=1 WHERE CourseID=@UniqueID;UPDATE ELearnExams SET Status=1 WHERE ExamID=@UniqueID;UPDATE ELearnHomework SET Status=1 WHERE HomeID=@UniqueID;";
                                break;

                            case 41:
                                SqlStr = "UPDATE ShopProducts SET Status=1 WHERE ProductID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 44:
                                SqlStr = "UPDATE ClassifiedsAds SET Status=1 WHERE LinkID=@UniqueID";
                                restoreCat = true;
                                break;

                            case 46:
                                SqlStr = "UPDATE EventCalendar SET Status=1 WHERE LinkID=@UniqueID;UPDATE EventTypes SET Status=1 WHERE TypeID=@UniqueID";
                                break;

                            case 50:
                                SqlStr = "UPDATE Speakers SET Status=1 WHERE SpeakerID=@UniqueID;UPDATE SpeakSpeeches SET Status=1 WHERE SpeakerID=@UniqueID;UPDATE SpeakTopics SET Status=1 WHERE TopicID=@UniqueID;";
                                break;

                            case 60:
                                SqlStr = "UPDATE Portals SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE PortalPages SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Articles SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE AuctionAds SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Blog SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE BusinessListings SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE ClassifiedsAds SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE CustomFieldUsers SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE DiscountSystem SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE ELearnCourses SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE EmailTemplates SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE EventTypes SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE EventCalendar SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE FAQ SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Forms SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE ForumsMessages SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Guestbook SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE LibrariesFiles SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE LinksWebSites SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE MatchMaker SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Members SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE News SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE PhotoAlbums SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Profiles SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE RStateBrokers SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE RStateAgents SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE RStateProperty SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE ShopProducts SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Invoices SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Invoices_Products SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE SpeakSpeeches SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE SpeakTopics SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Speakers SET Status=1 WHERE PortalID=@UniqueID;";
                                SqlStr += "UPDATE Vouchers SET Status=1 WHERE PortalID=@UniqueID;";
                                break;

                            case 61:
                                SqlStr = "UPDATE Blog SET Status=1 WHERE BlogID=@UniqueID";
                                break;

                            case 63:
                                SqlStr = "UPDATE Profiles SET Status=1 WHERE ProfileID=@UniqueID";
                                break;

                            case 65:
                                SqlStr = "UPDATE Vouchers SET Status=1 WHERE VoucherID=@UniqueID";
                                break;

                            case 974:
                                SqlStr = "UPDATE CustomSections SET Status=1 WHERE SectionID=@UniqueID";
                                break;

                            case 975:
                                SqlStr = "UPDATE CustomFields SET Status=1 WHERE FieldID=@UniqueID";
                                break;

                            case 976:
                                SqlStr = "UPDATE Categories SET Status=1 WHERE CatID=@UniqueID";
                                break;

                            case 979:
                                SqlStr = "UPDATE TargetZones SET Status=1 WHERE ZoneID=@UniqueID;UPDATE Advertisements SET Status=1 WHERE ZoneID=@UniqueID;UPDATE ContentRotator SET Status=1 WHERE ZoneID=@UniqueID;";
                                break;

                            case 983:
                                SqlStr = "UPDATE Approvals SET Status=1 WHERE ApproveID=@UniqueID;UPDATE ApprovalXML SET Status=1 WHERE ApproveID=@UniqueID;UPDATE ApprovalEmails SET Status=1 WHERE ApproveID=@UniqueID;";
                                break;

                            case 984:
                                SqlStr = "UPDATE SiteTemplates SET Status=1 WHERE TemplateID=@UniqueID";

                                using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@TemplateID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (Directory.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + " (DELETED)\\"))
                                            {
                                                Directory.Move(SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + " (DELETED)\\", SepFunctions.GetDirValue("skins") + SepFunctions.openNull(RS["FolderName"]) + "\\");
                                                SqlStr = "UPDATE SiteTemplates SET Status='2' WHERE TemplateID=@UniqueID";
                                            }
                                        }
                                    }
                                }

                                break;

                            case 985:
                                SqlStr = "UPDATE Invoices SET Status='0' WHERE InvoiceID=@UniqueID;UPDATE Invoices_Products SET Status=1 WHERE InvoiceID=@UniqueID;";
                                break;

                            case 986:
                                SqlStr = "UPDATE Members SET Status=1 WHERE UserID=@UniqueID";
                                break;

                            case 987:
                                SqlStr = "UPDATE EmailTemplates SET Status=1 WHERE Template=@UniqueID";
                                break;

                            case 989:
                                SqlStr = "UPDATE AccessKeys SET Status=1 WHERE KeyID=@UniqueID";
                                break;

                            case 990:
                                SqlStr = "UPDATE GroupLists SET Status=1 WHERE ListID=@UniqueID";
                                break;

                            case 995:
                                SqlStr = "UPDATE TaxCalculator SET Status=1 WHERE ID=@UniqueID";
                                break;

                            case 998:
                                SqlStr = "UPDATE Activities SET Status=1 WHERE ActivityID=@UniqueID";
                                break;

                            case 999:
                                SqlStr = "UPDATE ModulesNPages SET Status=1 WHERE UniqueID=@UniqueID";
                                break;
                        }

                        if (!string.IsNullOrWhiteSpace(SqlStr))
                        {
                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                cmd.Parameters.AddWithValue("@UniqueID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                cmd.ExecuteNonQuery();
                            }

                            if (restoreCat)
                            {
                                using (var cmd = new SqlCommand(Strings.Replace(Strings.Replace(SqlStr, "UPDATE ", "SELECT CatID FROM "), "SET Status=1", string.Empty), conn))
                                {
                                    cmd.Parameters.AddWithValue("@UniqueID", Strings.Split(arrUniqueIDs[i], "||")[1]);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            Recycle_Bin_Restore("976||" + SepFunctions.openNull(RS["CatID"]));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            bError = " (" + SepFunctions.LangText("Invalid ModuleID") + ")";
                        }
                    }
                }
            }

            string sReturn = SepFunctions.LangText("Recycled Item(s) has been successfully restored.");

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            return sReturn;
        }

        /// <summary>
        /// Gets the recycle bin items add item.
        /// </summary>
        /// <param name="SqlStr">The SQL string.</param>
        /// <param name="conn">The connection.</param>
        private static void GetRecycleBinItemsAddItem(string SqlStr, SqlConnection conn)
        {
            using (var cmd = new SqlCommand("INSERT INTO RecycleBin " + SqlStr, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets the recycle bin items build.
        /// </summary>
        /// <param name="iModuleID">The i module identifier.</param>
        private static void GetRecycleBinItemsBuild(int iModuleID)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("DELETE FROM RecycleBin", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                if (iModuleID == 0)
                {
                    GetRecycleBinItemsAddItem("SELECT Cast(FieldID AS decimal) AS UniqueID,'975' AS ModuleID,'Custom Field' AS ModuleName,FieldName AS Title,DateDeleted FROM CustomFields WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(SectionID AS decimal) AS UniqueID,'974' AS ModuleID,'Custom Field Section' AS ModuleName,SectionName AS Title,DateDeleted FROM CustomSections WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(CatID AS decimal) AS UniqueID,'976' AS ModuleID,'Category' AS ModuleName,CategoryName AS Title,DateDeleted FROM Categories WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(ContentID AS decimal) AS UniqueID,'1' AS ModuleID,'Content Rotator' AS ModuleName,Description AS Title,DateDeleted FROM ContentRotator WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(AdID AS decimal) AS UniqueID,'2' AS ModuleID,'Advertisement' AS ModuleName,Description AS Title,DateDeleted FROM Advertisements WHERE Status='-1'", conn);
                    if (SepFunctions.ModuleActivated(5))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(DiscountID AS decimal) AS UniqueID,'5' AS ModuleID,'Discount Coupon' AS ModuleName,DiscountCode AS Title,DateDeleted FROM DiscountSystem WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(9))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(FAQID AS decimal) AS UniqueID,'9' AS ModuleID,'FAQ' AS ModuleName,Question AS Title,DateDeleted FROM FAQ WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(10))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(FileID AS decimal) AS UniqueID,'10' AS ModuleID,'Downloads' AS ModuleName,Field1 AS Title,DateDeleted FROM LibrariesFiles WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(12))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(TopicID AS decimal) AS UniqueID,'12' AS ModuleID,'Forums' AS ModuleName,Subject AS Title,DateDeleted FROM ForumsMessages WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(13))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(FormID AS decimal) AS UniqueID,'13' AS ModuleID,'Forms' AS ModuleName,Title AS Title,DateDeleted FROM Forms WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(14))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(EntryID AS decimal) AS UniqueID,'14' AS ModuleID,'Guestbook' AS ModuleName,EmailAddress AS Title,DateDeleted FROM Guestbook WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(18))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(ProfileID AS decimal) AS UniqueID,'18' AS ModuleID,'Match Maker Profile' AS ModuleName,M.UserName AS Title,MatchMaker.DateDeleted FROM MatchMaker,Members AS M WHERE MatchMaker.UserID=M.UserID AND MatchMaker.Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(19))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(LinkID AS decimal) AS UniqueID,'19' AS ModuleID,'Web Site' AS ModuleName,LinkName AS Title,DateDeleted FROM LinksWebSites WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(20))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(BusinessID AS decimal) AS UniqueID,'20' AS ModuleID,'Business Directory' AS ModuleName,BusinessName AS Title,DateDeleted FROM BusinessListings WHERE BusinessID=LinkID AND Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(23))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(NewsID AS decimal) AS UniqueID,'23' AS ModuleID,'News' AS ModuleName,Headline AS Title,DateDeleted FROM News WHERE Status='-1'", conn);
                    }

                    GetRecycleBinItemsAddItem("SELECT Cast(LetterID AS decimal) AS UniqueID,'24' AS ModuleID,'Newsletters' AS ModuleName,NewsletName AS Title,DateDeleted FROM Newsletters WHERE Status='-1'", conn);
                    if (SepFunctions.ModuleActivated(25))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(PollID AS decimal) AS UniqueID,'25' AS ModuleID,'Polls' AS ModuleName,Question AS Title,DateDeleted FROM PNQQuestions WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(28))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(AlbumID AS decimal) AS UniqueID,'28' AS ModuleID,'Photo Album' AS ModuleName,AlbumName AS Title,DateDeleted FROM PhotoAlbums WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(31))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(AdID AS decimal) AS UniqueID,'31' AS ModuleID,'Auction Ads' AS ModuleName,Title AS Title,DateDeleted FROM AuctionAds WHERE AdID=LinkID AND Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(32))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(PropertyID AS decimal) AS UniqueID,'32' AS ModuleID,'Real Estate Property' AS ModuleName,Title AS Title,DateDeleted FROM RStateProperty WHERE Status='-1'", conn);
                        GetRecycleBinItemsAddItem("SELECT Cast(AgentID AS decimal) AS UniqueID,'32' AS ModuleID,'Real Estate Agent' AS ModuleName,(SELECT UserName FROM Members WHERE UserID=RStateAgents.UserID) AS Title,DateDeleted FROM RStateAgents WHERE Status='-1'", conn);
                        GetRecycleBinItemsAddItem("SELECT Cast(BrokerID AS decimal) AS UniqueID,'32' AS ModuleID,'Real Estate Broker' AS ModuleName,BrokerName AS Title,DateDeleted FROM RStateBrokers WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(35))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(ArticleID AS decimal) AS UniqueID,'35' AS ModuleID,'Articles' AS ModuleName,Headline AS Title,DateDeleted FROM Articles WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(37))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(CourseID AS decimal) AS UniqueID,'37' AS ModuleID,'Elearning Courses' AS ModuleName,CourseName AS Title,DateDeleted FROM ELearnCourses WHERE Status='-1'", conn);
                        GetRecycleBinItemsAddItem("SELECT Cast(ExamID AS decimal) AS UniqueID,'37' AS ModuleID,'Elearning Exam' AS ModuleName,ExamName AS Title,DateDeleted FROM ELearnExams WHERE Status='-1'", conn);
                        GetRecycleBinItemsAddItem("SELECT Cast(HomeID AS decimal) AS UniqueID,'37' AS ModuleID,'Elearning Assignment' AS ModuleName,HWTitle AS Title,DateDeleted FROM ELearnHomework WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(41))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(ProductID AS decimal) AS UniqueID,'41' AS ModuleID,'Shopping Mall' AS ModuleName,ProductName AS Title,DateDeleted FROM ShopProducts WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(44))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(AdID AS decimal) AS UniqueID,'44' AS ModuleID,'Classified Ads' AS ModuleName,Title AS Title,DateDeleted FROM ClassifiedsAds WHERE AdID=LinkID AND Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(46))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(EventID AS decimal) AS UniqueID,'46' AS ModuleID,'Event Calendar' AS ModuleName,Subject AS Title,DateDeleted FROM EventCalendar WHERE EventID=LinkID AND Status='-1'", conn);
                        GetRecycleBinItemsAddItem("SELECT Cast(TypeID AS decimal) AS UniqueID,'46' AS ModuleID,'Event Calendar Type' AS ModuleName,TypeName AS Title,DateDeleted FROM EventTypes WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(50))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(SpeakerID AS decimal) AS UniqueID,'50' AS ModuleID,'Speakers Bureau - Speaker' AS ModuleName,FirstName + ' ' + LastName AS Title,DateDeleted FROM Speakers WHERE Status='-1'", conn);
                        GetRecycleBinItemsAddItem("SELECT Cast(TopicID AS decimal) AS UniqueID,'50' AS ModuleID,'Speakers Bureau - Topic' AS ModuleName,TopicName AS Title,DateDeleted FROM SpeakTopics WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(60))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(PortalID AS decimal) AS UniqueID,'60' AS ModuleID,'Portals' AS ModuleName,PortalTitle AS Title,DateDeleted FROM Portals WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(61))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(BlogID AS decimal) AS UniqueID,'61' AS ModuleID,'Blogs' AS ModuleName,BlogName AS Title,DateDeleted FROM Blog WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(63))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(ProfileID AS decimal) AS UniqueID,'63' AS ModuleID,'User Profiles' AS ModuleName,M.UserName AS Title,Profiles.DateDeleted FROM Profiles,Members AS M WHERE Profiles.UserID=M.UserID AND Profiles.Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(65))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(VoucherID AS decimal) AS UniqueID,'65' AS ModuleID,'Voucher' AS ModuleName,BuyTitle AS Title,DateDeleted FROM Vouchers WHERE Status='-1'", conn);
                    }

                    if (SepFunctions.ModuleActivated(983))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(ApproveID AS decimal) AS UniqueID,'983' AS ModuleID,'Approval Chain' AS ModuleName,ChainName AS Title,DateDeleted FROM Approvals WHERE Status='-1'", conn);
                    }

                    GetRecycleBinItemsAddItem("SELECT Cast(TemplateID AS decimal) AS UniqueID,'984' AS ModuleID,'Site Template' AS ModuleName,TemplateName AS Title,DateDeleted FROM SiteTemplates WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(InvoiceID AS decimal) AS UniqueID,'985' AS ModuleID,'Invoices' AS ModuleName,'Invoice ID: '+Cast(Cast(InvoiceID AS decimal) AS varchar)+' ordered by '+(SELECT UserName FROM Members WHERE UserID=Invoices.UserID)+' on '+Cast(OrderDate AS varchar) AS Title,DateDeleted FROM Invoices WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT UserID AS UniqueID,'986' AS ModuleID,'Members' AS ModuleName,UserName AS Title,DateDeleted FROM Members WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(TemplateID AS decimal) AS UniqueID,'987' AS ModuleID,'Email Template' AS ModuleName,TemplateName AS Title,DateDeleted FROM EmailTemplates WHERE Status='-1'", conn);
                    if (SepFunctions.ModuleActivated(989))
                    {
                        GetRecycleBinItemsAddItem("SELECT Cast(KeyID AS decimal) AS UniqueID,'989' AS ModuleID,'Access Key' AS ModuleName,KeyName AS Title,DateDeleted FROM AccessKeys WHERE Status='-1'", conn);
                    }

                    GetRecycleBinItemsAddItem("SELECT Cast(ListID AS decimal) AS UniqueID,'990' AS ModuleID,'Group List' AS ModuleName,ListName AS Title,DateDeleted FROM GroupLists WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(ID AS decimal) AS UniqueID,'995' AS ModuleID,'Taxes' AS ModuleName,TaxName AS Title,DateDeleted FROM TaxCalculator WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(ActivityID AS decimal) AS UniqueID,'998' AS ModuleID,'Activities' AS ModuleName,Description AS Title,DateDeleted FROM Activities WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(UniqueID AS decimal) AS UniqueID,'999' AS ModuleID,'Web Page' AS ModuleName,LinkText AS Title,DateDeleted FROM ModulesNPages WHERE Status='-1'", conn);
                    GetRecycleBinItemsAddItem("SELECT Cast(ZoneID AS decimal) AS UniqueID,'979' AS ModuleID,'Target Zone' AS ModuleName,ZoneName AS Title,DateDeleted FROM TargetZones WHERE Status='-1'", conn);
                }
                else
                {
                    switch (iModuleID)
                    {
                        case 1:
                            GetRecycleBinItemsAddItem("SELECT Cast(ContentID AS decimal) AS UniqueID,'1' AS ModuleID,'Content Rotator' AS ModuleName,Description AS Title,DateDeleted FROM ContentRotator WHERE Status='-1'", conn);
                            GetRecycleBinItemsAddItem("SELECT Cast(ZoneID AS decimal) AS UniqueID,'979' AS ModuleID,'Content Rotator Zone' AS ModuleName,ZoneName AS Title,DateDeleted FROM TargetZones WHERE ModuleID='1' AND Status='-1'", conn);

                            break;

                        case 2:
                            GetRecycleBinItemsAddItem("SELECT Cast(AdID AS decimal) AS UniqueID,'2' AS ModuleID,'Advertisement' AS ModuleName,Description AS Title,DateDeleted FROM Advertisements WHERE Status='-1'", conn);
                            GetRecycleBinItemsAddItem("SELECT Cast(ZoneID AS decimal) AS UniqueID,'979' AS ModuleID,'Advertisement Zone' AS ModuleName,ZoneName AS Title,DateDeleted FROM TargetZones WHERE ModuleID='2' AND Status='-1'", conn);

                            break;

                        case 5:
                            if (SepFunctions.ModuleActivated(5))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(DiscountID AS decimal) AS UniqueID,'5' AS ModuleID,'Discount Coupon' AS ModuleName,DiscountCode AS Title,DateDeleted FROM DiscountSystem WHERE Status='-1'", conn);
                            }

                            break;

                        case 9:
                            if (SepFunctions.ModuleActivated(9))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(FAQID AS decimal) AS UniqueID,'9' AS ModuleID,'FAQ' AS ModuleName,Question AS Title,DateDeleted FROM FAQ WHERE Status='-1'", conn);
                            }

                            break;

                        case 10:
                            if (SepFunctions.ModuleActivated(10))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(FileID AS decimal) AS UniqueID,'10' AS ModuleID,'Downloads' AS ModuleName,Field1 AS Title,DateDeleted FROM LibrariesFiles WHERE Status='-1'", conn);
                            }

                            break;

                        case 12:
                            if (SepFunctions.ModuleActivated(12))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(TopicID AS decimal) AS UniqueID,'12' AS ModuleID,'Forums' AS ModuleName,Subject AS Title,DateDeleted FROM ForumsMessages WHERE Status='-1'", conn);
                            }

                            break;

                        case 13:
                            if (SepFunctions.ModuleActivated(13))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(FormID AS decimal) AS UniqueID,'13' AS ModuleID,'Forms' AS ModuleName,Title AS Title,DateDeleted FROM Forms WHERE Status='-1'", conn);
                            }

                            break;

                        case 14:
                            if (SepFunctions.ModuleActivated(14))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(EntryID AS decimal) AS UniqueID,'14' AS ModuleID,'Guestbook' AS ModuleName,EmailAddress AS Title,DateDeleted FROM Guestbook WHERE Status='-1'", conn);
                            }

                            break;

                        case 18:
                            if (SepFunctions.ModuleActivated(18))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(ProfileID AS decimal) AS UniqueID,'18' AS ModuleID,'Match Maker Profile' AS ModuleName,M.UserName AS Title,MatchMaker.DateDeleted FROM MatchMaker,Members AS M WHERE MatchMaker.UserID=M.UserID AND MatchMaker.Status='-1'", conn);
                            }

                            break;

                        case 19:
                            if (SepFunctions.ModuleActivated(19))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(LinkID AS decimal) AS UniqueID,'19' AS ModuleID,'Web Site' AS ModuleName,LinkName AS Title,DateDeleted FROM LinksWebSites WHERE Status='-1'", conn);
                            }

                            break;

                        case 20:
                            if (SepFunctions.ModuleActivated(20))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(BusinessID AS decimal) AS UniqueID,'20' AS ModuleID,'Business Directory' AS ModuleName,BusinessName AS Title,DateDeleted FROM BusinessListings WHERE BusinessID=LinkID AND Status='-1'", conn);
                            }

                            break;

                        case 23:
                            if (SepFunctions.ModuleActivated(23))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(NewsID AS decimal) AS UniqueID,'23' AS ModuleID,'News' AS ModuleName,Headline AS Title,DateDeleted FROM News WHERE Status='-1'", conn);
                            }

                            break;

                        case 24:
                            GetRecycleBinItemsAddItem("SELECT Cast(LetterID AS decimal) AS UniqueID,'24' AS ModuleID,'Newsletters' AS ModuleName,NewsletName AS Title,DateDeleted FROM Newsletters WHERE Status='-1'", conn);
                            break;

                        case 25:
                            if (SepFunctions.ModuleActivated(25))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(PollID AS decimal) AS UniqueID,'25' AS ModuleID,'Polls' AS ModuleName,Question AS Title,DateDeleted FROM PNQQuestions WHERE Status='-1'", conn);
                            }

                            break;

                        case 28:
                            if (SepFunctions.ModuleActivated(28))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(AlbumID AS decimal) AS UniqueID,'28' AS ModuleID,'Photo Album' AS ModuleName,AlbumName AS Title,DateDeleted FROM PhotoAlbums WHERE Status='-1'", conn);
                            }

                            break;

                        case 29:
                            GetRecycleBinItemsAddItem("SELECT Cast(FieldID AS decimal) AS UniqueID,'975' AS ModuleID,'Custom Field' AS ModuleName,FieldName AS Title,DateDeleted FROM CustomFields WHERE ModuleIDs LIKE '%|29|%' AND Status='-1'", conn);
                            break;

                        case 31:
                            if (SepFunctions.ModuleActivated(31))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(AdID AS decimal) AS UniqueID,'31' AS ModuleID,'Auction Ads' AS ModuleName,Title AS Title,DateDeleted FROM AuctionAds WHERE AdID=LinkID AND Status='-1'", conn);
                            }

                            break;

                        case 32:
                            if (SepFunctions.ModuleActivated(32))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(PropertyID AS decimal) AS UniqueID,'32' AS ModuleID,'Real Estate Property' AS ModuleName,Title AS Title,DateDeleted FROM RStateProperty WHERE Status='-1'", conn);
                                GetRecycleBinItemsAddItem("SELECT Cast(AgentID AS decimal) AS UniqueID,'32' AS ModuleID,'Real Estate Agent' AS ModuleName,(SELECT UserName FROM Members WHERE UserID=RStateAgents.UserID) AS Title,DateDeleted FROM RStateAgents WHERE Status='-1'", conn);
                                GetRecycleBinItemsAddItem("SELECT Cast(BrokerID AS decimal) AS UniqueID,'32' AS ModuleID,'Real Estate Broker' AS ModuleName,BrokerName AS Title,DateDeleted FROM RStateBrokers WHERE Status='-1'", conn);
                            }

                            break;

                        case 35:
                            if (SepFunctions.ModuleActivated(35))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(ArticleID AS decimal) AS UniqueID,'35' AS ModuleID,'Articles' AS ModuleName,Headline AS Title,DateDeleted FROM Articles WHERE Status='-1'", conn);
                            }

                            break;

                        case 37:
                            if (SepFunctions.ModuleActivated(37))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(CourseID AS decimal) AS UniqueID,'37' AS ModuleID,'Elearning Course' AS ModuleName,CourseName AS Title,DateDeleted FROM ELearnCourses WHERE Status='-1'", conn);
                                GetRecycleBinItemsAddItem("SELECT Cast(ExamID AS decimal) AS UniqueID,'37' AS ModuleID,'Elearning Exam' AS ModuleName,ExamName AS Title,DateDeleted FROM ELearnExams WHERE Status='-1'", conn);
                                GetRecycleBinItemsAddItem("SELECT Cast(HomeID AS decimal) AS UniqueID,'37' AS ModuleID,'Elearning Assignment' AS ModuleName,HWTitle AS Title,DateDeleted FROM ELearnHomework WHERE Status='-1'", conn);
                            }

                            break;

                        case 41:
                            if (SepFunctions.ModuleActivated(41))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(ProductID AS decimal) AS UniqueID,'41' AS ModuleID,'Shopping Mall' AS ModuleName,ProductName AS Title,DateDeleted FROM ShopProducts WHERE Status='-1'", conn);
                            }

                            break;

                        case 44:
                            if (SepFunctions.ModuleActivated(44))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(AdID AS decimal) AS UniqueID,'44' AS ModuleID,'Classified Ads' AS ModuleName,Title AS Title,DateDeleted FROM ClassifiedsAds WHERE AdID=LinkID AND Status='-1'", conn);
                            }

                            break;

                        case 46:
                            if (SepFunctions.ModuleActivated(46))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(EventID AS decimal) AS UniqueID,'46' AS ModuleID,'Event Calendar' AS ModuleName,Subject AS Title,DateDeleted FROM EventCalendar WHERE EventID=LinkID AND Status='-1'", conn);
                                GetRecycleBinItemsAddItem("SELECT Cast(TypeID AS decimal) AS UniqueID,'46' AS ModuleID,'Event Calendar Type' AS ModuleName,TypeName AS Title,DateDeleted FROM EventTypes WHERE Status='-1'", conn);
                            }

                            break;

                        case 50:
                            if (SepFunctions.ModuleActivated(50))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(SpeakerID AS decimal) AS UniqueID,'50' AS ModuleID,'Speakers Bureau - Speaker' AS ModuleName,FirstName + ' ' + LastName AS Title,DateDeleted FROM Speakers WHERE Status='-1'", conn);
                                GetRecycleBinItemsAddItem("SELECT Cast(TopicID AS decimal) AS UniqueID,'50' AS ModuleID,'Speakers Bureau - Topic' AS ModuleName,TopicName AS Title,DateDeleted FROM SpeakTopics WHERE Status='-1'", conn);
                            }

                            break;

                        case 60:
                            if (SepFunctions.ModuleActivated(60))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(PortalID AS decimal) AS UniqueID,'60' AS ModuleID,'Portals' AS ModuleName,PortalTitle AS Title,DateDeleted FROM Portals WHERE Status='-1'", conn);
                            }

                            break;

                        case 61:
                            if (SepFunctions.ModuleActivated(61))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(BlogID AS decimal) AS UniqueID,'61' AS ModuleID,'Blogs' AS ModuleName,BlogName AS Title,DateDeleted FROM Blog WHERE Status='-1'", conn);
                            }

                            break;

                        case 63:
                            if (SepFunctions.ModuleActivated(63))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(ProfileID AS decimal) AS UniqueID,'63' AS ModuleID,'User Profiles' AS ModuleName,M.UserName AS Title,Profiles.DateDeleted FROM Profiles,Members AS M WHERE Profiles.UserID=M.UserID AND Profiles.Status='-1'", conn);
                            }

                            break;

                        case 65:
                            if (SepFunctions.ModuleActivated(65))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(VoucherID AS decimal) AS UniqueID,'65' AS ModuleID,'Voucher' AS ModuleName,BuyTitle AS Title,DateDeleted FROM Vouchers WHERE Status='-1'", conn);
                            }

                            break;

                        case 983:
                            if (SepFunctions.ModuleActivated(983))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(ApproveID AS decimal) AS UniqueID,'983' AS ModuleID,'Approval Chain' AS ModuleName,ChainName AS Title,DateDeleted FROM Approvals WHERE Status='-1'", conn);
                            }

                            break;

                        case 984:
                            GetRecycleBinItemsAddItem("SELECT Cast(TemplateID AS decimal) AS UniqueID,'984' AS ModuleID,'Site Template' AS ModuleName,TemplateName AS Title,DateDeleted FROM SiteTemplates WHERE Status='-1'", conn);
                            break;

                        case 985:
                            GetRecycleBinItemsAddItem("SELECT Cast(InvoiceID AS decimal) AS UniqueID,'985' AS ModuleID,'Invoices' AS ModuleName,'Invoice ID: '+Cast(Cast(InvoiceID AS decimal) AS varchar)+' ordered by '+(SELECT UserName FROM Members WHERE UserID=Invoices.UserID)+' on '+Cast(OrderDate AS varchar) AS Title,DateDeleted FROM Invoices WHERE Status='-1'", conn);
                            break;

                        case 986:
                            GetRecycleBinItemsAddItem("SELECT UserID AS UniqueID,'986' AS ModuleID,'Members' AS ModuleName,UserName AS Title,DateDeleted FROM Members WHERE Status='-1'", conn);
                            break;

                        case 987:
                            GetRecycleBinItemsAddItem("SELECT Cast(TemplateID AS decimal) AS UniqueID,'987' AS ModuleID,'Email Template' AS ModuleName,TemplateName AS Title,DateDeleted FROM EmailTemplates WHERE Status='-1'", conn);
                            break;

                        case 989:
                            if (SepFunctions.ModuleActivated(989))
                            {
                                GetRecycleBinItemsAddItem("SELECT Cast(KeyID AS decimal) AS UniqueID,'989' AS ModuleID,'Access Key' AS ModuleName,KeyName AS Title,DateDeleted FROM AccessKeys WHERE Status='-1'", conn);
                            }

                            break;

                        case 990:
                            GetRecycleBinItemsAddItem("SELECT Cast(ListID AS decimal) AS UniqueID,'990' AS ModuleID,'Group List' AS ModuleName,ListName AS Title,DateDeleted FROM GroupLists WHERE Status='-1'", conn);
                            break;

                        case 995:
                            GetRecycleBinItemsAddItem("SELECT Cast(ID AS decimal) AS UniqueID,'995' AS ModuleID,'Taxes' AS ModuleName,TaxName AS Title,DateDeleted FROM TaxCalculator WHERE Status='-1'", conn);
                            break;

                        case 998:
                            GetRecycleBinItemsAddItem("SELECT Cast(ActivityID AS decimal) AS UniqueID,'998' AS ModuleID,'Activities' AS ModuleName,Description AS Title,DateDeleted FROM Activities WHERE Status='-1'", conn);
                            break;

                        case 999:
                            GetRecycleBinItemsAddItem("SELECT Cast(UniqueID AS decimal) AS UniqueID,'999' AS ModuleID,'Web Page' AS ModuleName,LinkText AS Title,DateDeleted FROM ModulesNPages WHERE Status='-1'", conn);
                            break;
                    }
                }
            }
        }
    }
}