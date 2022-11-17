// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Saving.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using Newtonsoft.Json;
    using SepCommon.Core.Models;
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using System.Net;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Adds the points to users.
        /// </summary>
        /// <param name="Points">The points.</param>
        /// <param name="sUserID">Identifier for the user.</param>
        /// <param name="Conn">The connection.</param>
        public static void Add_Points_To_Users(long Points, string sUserID, SqlConnection Conn)
        {
            using (var cmd = new SqlCommand("UPDATE Members SET UserPoints=UserPoints+" + Points + ", Status=1 WHERE UserID='" + FixWord(sUserID) + "'", Conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Additional data delete.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="RecordID">Identifier for the record.</param>
        public static void Additional_Data_Delete(int iModuleID, string RecordID)
        {
            var cLucene = new LuceneDelete();

            // Delete indexes
            switch (iModuleID)
            {
                case 9:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;

                case 10:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;

                case 12:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;

                case 20:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;

                case 35:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;

                case 44:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;

                case 63:
                    cLucene.DeleteText(iModuleID, toLong(RecordID));
                    break;
            }

            // Delete change log history
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM ChangeLog WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                    cmd.Parameters.AddWithValue("@UniqueID", RecordID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Additional data save.
        /// </summary>
        /// <param name="isNewRecord">True if is new record, false if not.</param>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="RecordID">Identifier for the record.</param>
        /// <param name="sUserID">Identifier for the user.</param>
        /// <param name="Username">The username.</param>
        /// <param name="actType">Type of the act.</param>
        /// <param name="actDescription">Information describing the act.</param>
        /// <param name="PointVariable">The point variable.</param>
        /// <returns>An int.</returns>
        public static int Additional_Data_Save(bool isNewRecord, int iModuleID, string RecordID, string sUserID, string Username, string actType, string actDescription, string PointVariable)
        {
            var intReturn = 0;

            // Save Activity
            var sActDesc = string.Empty;
            var sActType = string.Empty;

            if (isNewRecord)
            {
                sActDesc = LangText(actDescription + " has been successfully added.") + Environment.NewLine + Environment.NewLine;
                sActType = "Add" + actType;
            }
            else
            {
                sActDesc = LangText(actDescription + " has been successfully updated.") + Environment.NewLine + Environment.NewLine;
                sActType = "Edit" + actType;
            }

            sActDesc += LangText("Written By: ~~" + Username + "~~") + Environment.NewLine;
            sActDesc += LangText("Date/Time: ~~" + DateTime.Now + "~~") + Environment.NewLine;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("INSERT INTO Activities (ActivityID,UserID,DatePosted,ActType,IPAddress,Description,ModuleID,UniqueID,Status) VALUES (@ActivityID,@UserID,@DatePosted,@ActType,@IPAddress,@Description,@ModuleID,@UniqueID,'1')", conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", GetIdentity());
                    cmd.Parameters.AddWithValue("@UserID", sUserID);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ActType", sActType);
                    cmd.Parameters.AddWithValue("@IPAddress", GetUserIP());
                    cmd.Parameters.AddWithValue("@Description", sActDesc);
                    cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                    cmd.Parameters.AddWithValue("@UniqueID", RecordID);
                    cmd.ExecuteNonQuery();
                }
            }

            // End Save Activity

            // Save File Upload
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                if (iModuleID == 28)
                {
                    using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE ModuleID=@ModuleID AND UserID=@UserID AND isTemp='1'", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                        cmd.Parameters.AddWithValue("@UserID", sUserID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                long iGetPoints = 0;
                                long iPostPoints = 0;

                                iGetPoints = Points_Setup("GetUploadPicture");
                                iPostPoints = Points_Setup("PostUploadPicture");

                                if (!string.IsNullOrWhiteSpace(sUserID) && (iGetPoints != 0 || iPostPoints != 0))
                                {
                                    using (var cmd2 = new SqlCommand("UPDATE Members SET UserPoints=UserPoints+" + iGetPoints + "-" + iPostPoints + " WHERE UserID=@UserID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserID", sUserID);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }

                using (var cmd = new SqlCommand("UPDATE Uploads SET isTemp='0' WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID AND isTemp='1'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                    cmd.Parameters.AddWithValue("@UniqueID", RecordID);
                    cmd.ExecuteNonQuery();
                }
            }

            // End Save File Upload

            // Save Custom Field
            if (iModuleID == 18 || iModuleID == 20 || iModuleID == 29 || iModuleID == 35 || iModuleID == 44 || iModuleID == 63)
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    var customXML = string.Empty;

                    using (var cmd = new SqlCommand("SELECT FieldID FROM CustomFields WHERE ModuleIDs LIKE '%|" + iModuleID + "|%' AND (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0)", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(Get_Portal_ID()));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                customXML += "<Custom" + openNull(RS["FieldID"]) + ">" + HTMLEncode(Request.Item("Custom" + openNull(RS["FieldID"]))) + "</Custom" + openNull(RS["FieldID"]) + ">";
                            }
                        }
                    }

                    DAL.CustomFields.Answers_Save(sUserID, iModuleID, toLong(RecordID), 0, customXML);
                }
            }

            // End Save Custom Fields

            // Save Custom Field Answers
            var sValue = string.Empty;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT FieldID,AnswerType FROM CustomFields WHERE ModuleIDs LIKE '%|" + iModuleID + "|%' AND PortalIDs LIKE '%|' + @PortalIDs + '|%'", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(Get_Portal_ID()));
                    using (SqlDataReader CustomRS = cmd.ExecuteReader())
                    {
                        while (CustomRS.Read())
                        {
                            if (openNull(CustomRS["AnswerType"]) == "Image")
                            {
                                sValue = Request.Item("FileName");
                            }
                            else
                            {
                                sValue = Request.Item("Custom" + openNull(CustomRS["FieldID"]));
                            }

                            if (!string.IsNullOrWhiteSpace(sValue))
                            {
                                using (var cmd2 = new SqlCommand("SELECT UniqueID FROM CustomFieldUsers WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID AND FieldID=@FieldID AND PortalID=@PortalID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@UniqueID", RecordID);
                                    cmd2.Parameters.AddWithValue("@ModuleID", iModuleID);
                                    cmd2.Parameters.AddWithValue("@FieldID", openNull(CustomRS["FieldID"]));
                                    cmd2.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
                                    using (SqlDataReader CustomRS2 = cmd2.ExecuteReader())
                                    {
                                        if (CustomRS2.HasRows)
                                        {
                                            using (var updatecmd = new SqlCommand("UPDATE CustomFieldUsers SET FieldValue=@FieldValue WHERE UserID=@UserID AND UniqueID=@UniqueID AND ModuleID=@ModuleID AND FieldID=@FieldID AND PortalID=@PortalID", conn))
                                            {
                                                updatecmd.Parameters.AddWithValue("@FieldValue", sValue);
                                                updatecmd.Parameters.AddWithValue("@UserID", sUserID);
                                                updatecmd.Parameters.AddWithValue("@UniqueID", RecordID);
                                                updatecmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                                                updatecmd.Parameters.AddWithValue("@FieldID", openNull(CustomRS["FieldID"]));
                                                updatecmd.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
                                                updatecmd.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            using (var updatecmd = new SqlCommand("INSERT INTO CustomFieldUsers (UserFieldID,UniqueID,FieldID,UserID,ModuleID,FieldValue,PortalID) VALUES(@UserFieldID,@UniqueID,@FieldID,@UserID,@ModuleID,@FieldValue,@PortalID)", conn))
                                            {
                                                updatecmd.Parameters.AddWithValue("@UserFieldID", GetIdentity());
                                                updatecmd.Parameters.AddWithValue("@UniqueID", RecordID);
                                                updatecmd.Parameters.AddWithValue("@FieldID", openNull(CustomRS["FieldID"]));
                                                updatecmd.Parameters.AddWithValue("@UserID", sUserID);
                                                updatecmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                                                updatecmd.Parameters.AddWithValue("@FieldValue", sValue);
                                                updatecmd.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
                                                updatecmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // End Save Custom Field Answers

            // Update Member Points
            if (isNewRecord && !string.IsNullOrWhiteSpace(PointVariable))
            {
                long iGetPoints = 0;
                long iPostPoints = 0;

                iGetPoints = Points_Setup("Get" + PointVariable);
                iPostPoints = Points_Setup("Post" + PointVariable);

                if (!string.IsNullOrWhiteSpace(sUserID) && (iGetPoints != 0 || iPostPoints != 0))
                {
                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("UPDATE Members SET UserPoints=UserPoints+" + iGetPoints + "-" + iPostPoints + " WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", sUserID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // End Update Member Points

            // Check for Approval Chain
            var sStatus = "1";
            var approveContent = false;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT AE.EmailID,AE.EmailAddress,AP.ApproveID FROM Approvals AS AP,ApprovalEmails AS AE WHERE AP.ApproveID=AE.ApproveID AND AP.ModuleIDs LIKE '%|" + iModuleID + "|%' AND (AP.PortalIDs LIKE '%|' + @PortalIDs + '|%' OR AP.PortalIDs LIKE '%|-1|%') AND AP.Status <> -1 ORDER BY Weight,FullName,EmailAddress", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(Get_Portal_ID()));
                    using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                    {
                        if (ApproveRS.HasRows)
                        {
                            if (CompareKeys("2", true) == false)
                            {
                                ApproveRS.Read();
                                Approval_Chain_Start(iModuleID, openNull(ApproveRS["ApproveID"]), openNull(ApproveRS["EmailAddress"]), RecordID, openNull(ApproveRS["EmailID"]), sUserID);
                                approveContent = true;
                                intReturn = 1;
                            }
                        }
                    }
                }
            }

            if (approveContent)
            {
                switch (iModuleID)
                {
                    case 10:
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Status=1 WHERE FileID=@FileID", conn))
                            {
                                cmd.Parameters.AddWithValue("@FileID", RecordID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        break;

                    case 18:
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE MatchMaker SET Status=1 WHERE ProfileID=@ProfileID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ProfileID", RecordID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        break;

                    case 19:
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE LinksWebSites SET Status=1 WHERE LinkID=@LinkID", conn))
                            {
                                cmd.Parameters.AddWithValue("@LinkID", RecordID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        break;

                    case 20:
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE BusinessListings SET Status=1 WHERE LinkID=@LinkID", conn))
                            {
                                cmd.Parameters.AddWithValue("@LinkID", RecordID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        break;

                    case 35:
                        if (isNewRecord)
                        {
                            if (!string.IsNullOrWhiteSpace(Request.Item("Status")))
                            {
                                sStatus = Request.Item("Status");
                            }

                            using (var conn = new SqlConnection(Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("UPDATE Articles SET Status='" + sStatus + "' WHERE ArticleID=@ArticleID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ArticleID", RecordID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        break;

                    case 63:
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE Profiles SET Status=1 WHERE ProfileID=@ProfileID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ProfileID", RecordID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        break;

                    case 65:
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE Vouchers SET Status=1 WHERE VoucherID=@VoucherID", conn))
                            {
                                cmd.Parameters.AddWithValue("@VoucherID", RecordID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        break;
                }
            }

            // End Approval Chain Check

            // Update pricing for modules that have pricing options
            if (isNewRecord)
            {
                Pricing_Options_Save(iModuleID, RecordID);
            }

            // Index data
            if (approveContent)
            {
                IndexRecord(iModuleID, toLong(RecordID));
            }

            return intReturn;
        }

        /// <summary>
        /// Approval chain check.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="GetUniqueID">Unique identifier for the get.</param>
        /// <param name="forceEmail">(Optional) True to force email.</param>
        public static void Approval_Chain_Check(int iModuleID, string GetUniqueID, bool forceEmail = false)
        {
            var sStatus = "1";
            var SqlStr = string.Empty;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT AE.EmailID,AE.EmailAddress,AP.ApproveID FROM Approvals AS AP,ApprovalEmails AS AE WHERE AP.ApproveID=AE.ApproveID AND AP.ModuleIDs LIKE '%|" + iModuleID + "|%' AND (AP.PortalIDs LIKE '%|' + @PortalIDs + '|%' OR AP.PortalIDs LIKE '%|-1|%') ORDER BY Weight,FullName,EmailAddress", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(Get_Portal_ID()));
                    using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                    {
                        if (ApproveRS.HasRows && (CompareKeys("2", true) == false || forceEmail))
                        {
                            ApproveRS.Read();
                            Approval_Chain_Start(iModuleID, openNull(ApproveRS["ApproveID"]), openNull(ApproveRS["EmailAddress"]), GetUniqueID, openNull(ApproveRS["EmailID"]), Session_User_ID());
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(Request.Item("Status")))
                            {
                                sStatus = Request.Item("Status");
                            }

                            switch (iModuleID)
                            {
                                case 10:
                                    SqlStr = "UPDATE LibrariesFiles SET Status=1 WHERE FileID='" + FixWord(GetUniqueID) + "'";
                                    break;

                                case 18:
                                    SqlStr = "UPDATE MatchMaker SET Status=1 WHERE ProfileID='" + FixWord(GetUniqueID) + "'";
                                    break;

                                case 19:
                                    SqlStr = "UPDATE LinksWebSites SET Status=1 WHERE LinkID='" + FixWord(GetUniqueID) + "'";
                                    break;

                                case 20:
                                    SqlStr = "UPDATE BusinessListings SET Status=1 WHERE LinkID='" + FixWord(GetUniqueID) + "'";
                                    break;

                                case 35:
                                    SqlStr = "UPDATE Articles SET Status='" + sStatus + "' WHERE ArticleID='" + FixWord(GetUniqueID) + "'";
                                    break;

                                case 63:
                                    SqlStr = "UPDATE Profiles SET Status=1 WHERE ProfileID='" + FixWord(GetUniqueID) + "'";
                                    break;

                                case 65:
                                    SqlStr = "UPDATE Vouchers SET Status='" + sStatus + "' WHERE VoucherID='" + FixWord(GetUniqueID) + "'";
                                    break;
                            }

                            if (!string.IsNullOrWhiteSpace(SqlStr))
                            {
                                using (var cmd2 = new SqlCommand(SqlStr, conn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Approval chain start.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="strApproveID">Identifier for the approve.</param>
        /// <param name="strEmail">The email.</param>
        /// <param name="strUniqueID">Unique identifier.</param>
        /// <param name="strEmailID">Identifier for the email.</param>
        /// <param name="sUserID">Identifier for the user.</param>
        public static void Approval_Chain_Start(int intModuleID, string strApproveID, string strEmail, string strUniqueID, string strEmailID, string sUserID)
        {
            var strAdditional = string.Empty;
            var strXML = string.Empty;
            var strSubject = string.Empty;
            var strBody = string.Empty;
            var strXMLID = Strings.ToString(GetIdentity());

            var GetAdminEmail = Setup(991, "AdminEmailAddress");

            var GetDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Hour, toInt(Setup(992, "TimeOffset")), DateTime.Now);

            if (!string.IsNullOrWhiteSpace(Request.Item("XMLID")))
            {
                strXMLID = Request.Item("XMLID");
            }

            switch (intModuleID)
            {
                case 10:

                    // Downloads
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<CATID>" + HTMLEncode(Request.Item("CatID")) + "</CATID>";
                        strXML += "<CATTYPE>" + HTMLEncode(Request.Item("CatType")) + "</CATTYPE>";
                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                        strXML += "<FIELD1>" + HTMLEncode(Request.Item("Field1")) + "</FIELD1>";
                        strXML += "<FIELD2>" + HTMLEncode(Request.Item("Field2")) + "</FIELD2>";
                        strXML += "<FIELD3>" + HTMLEncode(Request.Item("Field3")) + "</FIELD3>";
                        strXML += "<FIELD4>" + HTMLEncode(Request.Item("Field4")) + "</FIELD4>";
                        strXML += "<FILENAME>" + HTMLEncode(Request.Item("FileName")) + "</FILENAME>";
                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                        strXML += "<DOWNLOADS>0</DOWNLOADS>";
                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                    }

                    strSubject = LangText("File Waiting for Approval");
                    strBody = LangText("There is a file waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 18:

                    // MatchMaker
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<ABOUTME>" + HTMLEncode(Request.Item("AboutMe")) + "</ABOUTME>";
                        strXML += "<ABOUTMYMATCH>" + HTMLEncode(Request.Item("AboutMyMatch")) + "</ABOUTMYMATCH>";
                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                    }

                    strSubject = LangText("Profile Waiting for Approval");
                    strBody = LangText("There is a profile waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 19:

                    // Link Directory
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<CATID>" + HTMLEncode(Request.Item("CatID")) + "</CATID>";
                        strXML += "<LINKNAME>" + HTMLEncode(Request.Item("LinkName")) + "</LINKNAME>";
                        strXML += "<LINKURL>" + HTMLEncode(Request.Item("LinkURL")) + "</LINKURL>";
                        strXML += "<DESCRIPTION>" + HTMLEncode(Request.Item("Description")) + "</DESCRIPTION>";
                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                    }

                    strSubject = LangText("Website Waiting for Approval");
                    strBody = LangText("There is a website waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 20:

                    // Business Directory
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<CATID>" + HTMLEncode(Request.Item("CatID")) + "</CATID>";
                        strXML += "<BUSINESSNAME>" + HTMLEncode(Request.Item("BusinessName")) + "</BUSINESSNAME>";
                        strXML += "<CONTACTEMAIL>" + HTMLEncode(Request.Item("ContactEmail")) + "</CONTACTEMAIL>";
                        strXML += "<PHONENUMBER>" + HTMLEncode(Request.Item("PhoneNumber")) + "</PHONENUMBER>";
                        strXML += "<SITEURL>" + HTMLEncode(Request.Item("SiteURL")) + "</SITEURL>";
                        strXML += "<DESCRIPTION>" + HTMLEncode(Request.Item("Description")) + "</DESCRIPTION>";
                        strXML += "<ADDRESS>" + HTMLEncode(Request.Item("Address")) + "</ADDRESS>";
                        strXML += "<CITY>" + HTMLEncode(Request.Item("City")) + "</CITY>";
                        strXML += "<STATE>" + HTMLEncode(Request.Item("State")) + "</STATE>";
                        strXML += "<ZIPCODE>" + HTMLEncode(Request.Item("ZipCode")) + "</ZIPCODE>";
                        strXML += "<COUNTRY>" + HTMLEncode(Request.Item("Country")) + "</COUNTRY>";
                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                        strXML += "<FULLDESCRIPTION>" + HTMLEncode(Request.Item("FullDescription")) + "</FULLDESCRIPTION>";
                        strXML += "<ALBUMID>" + HTMLEncode(Request.Item("AlbumID")) + "</ALBUMID>";
                    }

                    strSubject = LangText("Business Waiting for Approval");
                    strBody = LangText("There is a business waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 35:

                    // Articles
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT * FROM Articles WHERE ArticleID='" + FixWord(strUniqueID) + "'", conn))
                            {
                                using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                                {
                                    if (ApproveRS.HasRows)
                                    {
                                        ApproveRS.Read();
                                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                                        strXML += "<CATID>" + HTMLEncode(openNull(ApproveRS["CatID"])) + "</CATID>";
                                        strXML += "<HEADLINE>" + HTMLEncode(openNull(ApproveRS["Headline"])) + "</HEADLINE>";
                                        strXML += "<HEADLINE_DATE>" + HTMLEncode(openNull(ApproveRS["Headline_Date"])) + "</HEADLINE_DATE>";
                                        strXML += "<AUTHOR>" + HTMLEncode(openNull(ApproveRS["Author"])) + "</AUTHOR>";
                                        strXML += "<SOURCE>" + HTMLEncode(openNull(ApproveRS["Source"])) + "</SOURCE>";
                                        strXML += "<SUMMARY>" + HTMLEncode(openNull(ApproveRS["Summary"])) + "</SUMMARY>";
                                        strXML += "<START_DATE>" + HTMLEncode(openNull(ApproveRS["Start_Date"])) + "</START_DATE>";
                                        strXML += "<END_DATE>" + HTMLEncode(openNull(ApproveRS["End_Date"])) + "</END_DATE>";
                                        strXML += "<STATUS>" + HTMLEncode(openNull(ApproveRS["Status"])) + "</STATUS>";
                                        strXML += "<ARTICLE_URL>" + HTMLEncode(openNull(ApproveRS["Article_URL"])) + "</ARTICLE_URL>";
                                        strXML += "<FULL_ARTICLE>" + HTMLEncode(openNull(ApproveRS["Full_Article"])) + "</FULL_ARTICLE>";
                                        strXML += "<META_KEYWORDS>" + HTMLEncode(openNull(ApproveRS["Meta_Keywords"])) + "</META_KEYWORDS>";
                                        strXML += "<META_DESCRIPTION>" + HTMLEncode(openNull(ApproveRS["Meta_Description"])) + "</META_DESCRIPTION>";
                                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                                    }
                                }
                            }
                        }
                    }

                    strSubject = LangText("Article Waiting for Approval");
                    strBody = LangText("There is an article waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 63:

                    // User Profiles
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<ABOUTME>" + HTMLEncode(Request.Item("AboutMe")) + "</ABOUTME>";
                        strXML += "<ABOUTMYMATCH>" + HTMLEncode(Request.Item("AboutMyMatch")) + "</ABOUTMYMATCH>";
                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                    }

                    strSubject = LangText("Profile Waiting for Approval");
                    strBody = LangText("There is a profile waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 65:

                    // Vouchers
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<CATID>" + HTMLEncode(Request.Item("CatID")) + "</CATID>";
                        strXML += "<BUYTITLE>" + HTMLEncode(Request.Item("BuyTitle")) + "</BUYTITLE>";
                        strXML += "<SHORTDESCRIPTION>" + HTMLEncode(Request.Item("ShortDescription")) + "</SHORTDESCRIPTION>";
                        strXML += "<LONGDESCRIPTION>" + HTMLEncode(Request.Item("LongDescription")) + "</LONGDESCRIPTION>";
                        strXML += "<SALEPRICE>" + HTMLEncode(Request.Item("SalePrice")) + "</SALEPRICE>";
                        strXML += "<REGULARPRICE>" + HTMLEncode(Request.Item("RegularPrice")) + "</REGULARPRICE>";
                        strXML += "<QUANTITY>" + HTMLEncode(Request.Item("Quantity")) + "</QUANTITY>";
                        strXML += "<REDEMPTIONSTART>" + HTMLEncode(Request.Item("RedemptionStart")) + "</REDEMPTIONSTART>";
                        strXML += "<REDEMPTIONEND>" + HTMLEncode(Request.Item("RedemptionEnd")) + "</REDEMPTIONEND>";
                        strXML += "<PURCHASECODE>" + HTMLEncode(Request.Item("PurchaseCode")) + "</PURCHASECODE>";
                        strXML += "<SALESTAX>" + HTMLEncode(Request.Item("SalesTax")) + "</SALESTAX>";
                        strXML += "<BUSINESSNAME>" + HTMLEncode(Request.Item("BusinessName")) + "</BUSINESSNAME>";
                        strXML += "<ADDRESS>" + HTMLEncode(Request.Item("Address")) + "</ADDRESS>";
                        strXML += "<CITY>" + HTMLEncode(Request.Item("City")) + "</CITY>";
                        strXML += "<STATE>" + HTMLEncode(Request.Item("State")) + "</STATE>";
                        strXML += "<ZIPCODE>" + HTMLEncode(Request.Item("ZipCode")) + "</ZIPCODE>";
                        strXML += "<COUNTRY>" + HTMLEncode(Request.Item("Country")) + "</COUNTRY>";
                        strXML += "<CONTACTEMAIL>" + HTMLEncode(Request.Item("ContactEmail")) + "</CONTACTEMAIL>";
                        strXML += "<CONTACTNAME>" + HTMLEncode(Request.Item("ContactName")) + "</CONTACTNAME>";
                        strXML += "<PHONENUMBER>" + HTMLEncode(Request.Item("PhoneNumber")) + "</PHONENUMBER>";
                        strXML += "<DAYSTOEXPIRE>" + HTMLEncode(Request.Item("DaysToExpire")) + "</DAYSTOEXPIRE>";
                        strXML += "<FINEPRINT></FINEPRINT>";
                        strXML += "<SENDEMAILTEMP></SENDEMAILTEMP>";
                        strXML += "<SENDEMAILTEMPORDER></SENDEMAILTEMPORDER>";
                        strXML += "<DATEPOSTED>" + HTMLEncode(Strings.ToString(GetDate)) + "</DATEPOSTED>";
                        strXML += "<PORTALID>" + HTMLEncode(Strings.ToString(Get_Portal_ID())) + "</PORTALID>";
                        strXML += "<USERID>" + HTMLEncode(sUserID) + "</USERID>";
                    }

                    strSubject = LangText("Voucher Waiting for Approval");
                    strBody = LangText("There is a voucher waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;

                case 999:

                    // Web Pages
                    if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
                    {
                        strXML += "<UNIQUEID>" + HTMLEncode(strUniqueID) + "</UNIQUEID>";
                        strXML += "<LINKTEXT>" + HTMLEncode(Request.Item("LinkText")) + "</LINKTEXT>";
                        strXML += "<DESCRIPTION>" + HTMLEncode(Request.Item("Description")) + "</DESCRIPTION>";
                        strXML += "<KEYWORDS>" + HTMLEncode(Request.Item("Keywords")) + "</KEYWORDS>";
                        strXML += "<PAGETEXT>" + HTMLEncode(Request.Item("txtContent")) + "</PAGETEXT>";
                        strXML += "<MENUID>" + HTMLEncode(Request.Item("MenuID")) + "</MENUID>";
                        strXML += "<ENABLE>" + HTMLEncode(Request.Item("Enable")) + "</ENABLE>";
                    }

                    strSubject = LangText("New Web Page Waiting for Approval");
                    strBody = LangText("There is a web page waiting to be added or updated on your web site.") + "<br/><br/>";
                    break;
            }

            if (string.IsNullOrWhiteSpace(Request.Item("XMLID")))
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("INSERT INTO ApprovalXML (XMLID,ApproveID,ModuleID,UniqueID,XMLData,Status) VALUES(@XMLID,@ApproveID,@ModuleID,@UniqueID,@XMLData,'1')", conn))
                    {
                        cmd.Parameters.AddWithValue("@XMLID", strXMLID);
                        cmd.Parameters.AddWithValue("@ApproveID", strApproveID);
                        cmd.Parameters.AddWithValue("@ModuleID", intModuleID);
                        cmd.Parameters.AddWithValue("@UniqueID", strUniqueID);
                        cmd.Parameters.AddWithValue("@XMLData", strXML);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            strBody += "<a href=\"" + GetMasterDomain(false) + "spadmin/approve_content.aspx?ApproveID=" + UrlEncode(strApproveID) + "&XMLID=" + UrlEncode(strXMLID) + "&EmailID=" + UrlEncode(strEmailID) + "&ModuleID=" + intModuleID + "&PortalID=" + Get_Portal_ID() + strAdditional + "\" target=\"_blank\">" + LangText("Click here to check approval status") + "</a>";

            Send_Email(strEmail, GetAdminEmail, strSubject, strBody, intModuleID);
        }

        /// <summary>
        /// Gets change log.
        /// </summary>
        /// <param name="changeId">Identifier for the change.</param>
        /// <returns>The change log.</returns>
        public static string Get_Change_Log(long changeId)
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ChangedData FROM ChangeLog WHERE ChangeID='" + changeId + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            return openNull(RS["ChangedData"]);
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Pricing options save.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="strUniqueID">Unique identifier.</param>
        public static void Pricing_Options_Save(int intModuleID, string strUniqueID)
        {
            var inidata = string.Empty;

            decimal intPrice = 0;

            var strFeatured = "0";
            var strBoldTitle = "0";
            var strHighlight = "0";

            var sInstallFolder = GetInstallFolder();

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT PriceID FROM PricingOptions WHERE ModuleID='" + intModuleID + "' AND UniqueID='" + FixWord(strUniqueID) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            return;
                        }
                    }
                }
            }

            if (CompareKeys("2") == false)
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT Description FROM ShopProducts WHERE ModuleID='" + intModuleID + "' AND ProductName='Posting Price'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                inidata = openNull(RS["Description"]);
                                if (Format_Currency(ParseXML("NEWLISTING", inidata)) != Format_Currency("0") || Format_Currency(ParseXML("FEATURELISTING", inidata)) != Format_Currency("0") || Format_Currency(ParseXML("BOLDTITLE", inidata)) != Format_Currency("0") || Format_Currency(ParseXML("HIGHLIGHTLISTING", inidata)) != Format_Currency("0"))
                                {
                                    if (Format_Currency(ParseXML("NEWLISTING", inidata)) != Format_Currency("0"))
                                    {
                                        intPrice = intPrice + toDecimal(ParseXML("NEWLISTING", inidata));
                                    }

                                    if (Format_Currency(ParseXML("FEATURELISTING", inidata)) != Format_Currency("0") && toInt(Request.Item("PFeature")) == 1)
                                    {
                                        intPrice = intPrice + toDecimal(ParseXML("FEATURELISTING", inidata));
                                        strFeatured = "1";
                                    }

                                    if (Format_Currency(ParseXML("BOLDTITLE", inidata)) != Format_Currency("0") && toInt(Request.Item("PBold")) == 1)
                                    {
                                        intPrice = intPrice + toDecimal(ParseXML("BOLDTITLE", inidata));
                                        strBoldTitle = "1";
                                    }

                                    if (Format_Currency(ParseXML("HIGHLIGHTLISTING", inidata)) != Format_Currency("0") && toInt(Request.Item("PHighlight")) == 1)
                                    {
                                        intPrice = intPrice + toDecimal(ParseXML("HIGHLIGHTLISTING", inidata));
                                        strHighlight = "1";
                                    }

                                    if (intPrice > 0)
                                    {
                                        using (var cmd2 = new SqlCommand("INSERT INTO PricingOptions (UniqueID, ModuleID, PriceID, Featured, BoldTitle, Highlight) VALUES(@UniqueID, @ModuleID, @PriceID, @Featured, @BoldTitle, @Highlight)", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@UniqueID", strUniqueID);
                                            cmd2.Parameters.AddWithValue("@ModuleID", intModuleID);
                                            cmd2.Parameters.AddWithValue("@PriceID", GetIdentity());
                                            cmd2.Parameters.AddWithValue("@Featured", strFeatured);
                                            cmd2.Parameters.AddWithValue("@BoldTitle", strBoldTitle);
                                            cmd2.Parameters.AddWithValue("@Highlight", strHighlight);
                                            cmd2.ExecuteNonQuery();
                                        }

                                        switch (intModuleID)
                                        {
                                            case 20:
                                                using (var cmd2 = new SqlCommand("UPDATE BusinessListings SET Status='0' WHERE LinkID='" + strUniqueID + "'", conn))
                                                {
                                                    cmd2.ExecuteNonQuery();
                                                }

                                                DAL.Invoices.Invoice_Save(toLong(Session_Invoice_ID()), Session_User_ID(), 0, DateTime.Now, intModuleID, Strings.ToString(GetIdentity()), "1", string.Empty, string.Empty, false, "Business Posting", Strings.ToString(intPrice), string.Empty, string.Empty, string.Empty, toLong(strUniqueID), 0, Get_Portal_ID());
                                                Redirect(sInstallFolder + "viewcart.aspx");
                                                break;

                                            case 31:
                                                using (var cmd2 = new SqlCommand("UPDATE AuctionAds SET Status='0' WHERE LinkID='" + strUniqueID + "'", conn))
                                                {
                                                    cmd2.ExecuteNonQuery();
                                                }

                                                DAL.Invoices.Invoice_Save(toLong(Session_Invoice_ID()), Session_User_ID(), 0, DateTime.Now, intModuleID, Strings.ToString(GetIdentity()), "1", string.Empty, string.Empty, false, "Auction Listing", Strings.ToString(intPrice), string.Empty, string.Empty, string.Empty, toLong(strUniqueID), 0, Get_Portal_ID());
                                                Redirect(sInstallFolder + "viewcart.aspx");
                                                break;

                                            case 44:
                                                using (var cmd2 = new SqlCommand("UPDATE ClassifiedsAds SET Status='0' WHERE LinkID='" + strUniqueID + "'", conn))
                                                {
                                                    cmd2.ExecuteNonQuery();
                                                }

                                                DAL.Invoices.Invoice_Save(toLong(Session_Invoice_ID()), Session_User_ID(), 0, DateTime.Now, intModuleID, Strings.ToString(GetIdentity()), "1", string.Empty, string.Empty, false, "Classified Listing", Strings.ToString(intPrice), string.Empty, string.Empty, string.Empty, toLong(strUniqueID), 0, Get_Portal_ID());
                                                Redirect(sInstallFolder + "viewcart.aspx");
                                                break;

                                            case 66:
                                                using (var cPCR = new PCRecruiter())
                                                {
                                                    HttpWebRequest WRequest = null;
                                                    var cJobs = new PCRecruiterJobs();
                                                    cJobs.Status = "Pending";

                                                    var jCustom = new PCRPositionCustom();
                                                    var jCollection = new Collection<PCRPositionCustom>();
                                                    jCustom.FieldName = "Highlighted Listing";
                                                    jCustom.FieldType = "2";
                                                    string[] arrList = null;
                                                    Array.Resize(ref arrList, 1);
                                                    arrList[0] = strHighlight;
                                                    jCustom.Values = arrList;
                                                    jCollection.Add(jCustom);
                                                    jCustom = new PCRPositionCustom();
                                                    jCustom.FieldName = "Bold Listing";
                                                    jCustom.FieldType = "17";
                                                    Array.Resize(ref arrList, 1);
                                                    arrList[0] = strBoldTitle;
                                                    jCustom.Values = arrList;
                                                    jCollection.Add(jCustom);
                                                    jCustom = new PCRPositionCustom();
                                                    jCustom.FieldName = "Feature Listing";
                                                    jCustom.FieldType = "17";
                                                    Array.Resize(ref arrList, 1);
                                                    arrList[0] = strFeatured;
                                                    jCustom.Values = arrList;
                                                    jCollection.Add(jCustom);
                                                    cJobs.CustomFields = jCollection;

                                                    var postData = JsonConvert.SerializeObject(cJobs);
                                                    var byteArray = Encoding.UTF8.GetBytes(postData);
                                                    var sessionId = cPCR.GetSessionId();

                                                    WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions/" + strUniqueID);
                                                    WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                                                    WRequest.Method = "PUT";
                                                    WRequest.ContentLength = byteArray.Length;
                                                    WRequest.ContentType = "application/json";
                                                    WRequest.Accept = "application/json";
                                                    using (var dataStream = WRequest.GetRequestStream())
                                                    {
                                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                                    }
                                                    DAL.Invoices.Invoice_Save(toLong(Session_Invoice_ID()), Session_User_ID(), 0, DateTime.Now, intModuleID, Strings.ToString(GetIdentity()), "1", string.Empty, string.Empty, false, "Job Listing", Strings.ToString(intPrice), string.Empty, string.Empty, string.Empty, toLong(strUniqueID), 0, Get_Portal_ID());
                                                }
                                                Redirect(sInstallFolder + "viewcart.aspx");
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("INSERT INTO PricingOptions (UniqueID, ModuleID, PriceID, Featured, BoldTitle, Highlight) VALUES(@UniqueID, @ModuleID, @PriceID, @Featured, @BoldTitle, @Highlight)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", strUniqueID);
                        cmd.Parameters.AddWithValue("@ModuleID", intModuleID);
                        cmd.Parameters.AddWithValue("@PriceID", GetIdentity());
                        cmd.Parameters.AddWithValue("@Featured", toLong(Request.Item("PFeature")));
                        cmd.Parameters.AddWithValue("@BoldTitle", toLong(Request.Item("PBold")));
                        cmd.Parameters.AddWithValue("@Highlight", toLong(Request.Item("PHighlight")));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Pub compose email.
        /// </summary>
        /// <param name="strEmail">The email.</param>
        /// <param name="sFile">The file.</param>
        public static void PUB_Compose_Email(string strEmail, string sFile)
        {
            var sBody = Setup(41, "ShopMallElectronic");
            var GetAdminEmail = Setup(991, "AdminEmailAddress");
            var sURL = GetMasterDomain(false) + "shopping_download.aspx?FileID=" + sFile;

            if (string.IsNullOrWhiteSpace(sBody))
            {
                sBody += "<p>" + LangText("Thank you for purchasing an electronic file from us, below is a download link") + "</p>";
            }

            sBody += "<a href=\"" + sURL + "\">" + sURL + "</a>";

            Send_Email(strEmail, GetAdminEmail, LangText("Thank you for your purchase"), sBody, 41);
        }

        /// <summary>
        /// Pub mark order paid.
        /// </summary>
        /// <param name="sInvoiceID">Identifier for the invoice.</param>
        /// <param name="sTransaction">The transaction.</param>
        /// <param name="sMethod">The method.</param>
        /// <param name="sCustEmail">The customer email.</param>
        public static void PUB_Mark_Order_Paid(string sInvoiceID, string sTransaction, string sMethod, string sCustEmail)
        {
            string[] arrSplit = null;
            var sRecurringAdd = 0;

            var GetEmailSubject = string.Empty;
            var GetEmailBody = string.Empty;

            var sNewInvoiceID = Strings.ToString(GetIdentity());

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT IP.*,(SELECT ModelNumber FROM ShopProducts WHERE ProductID=IP.ProductID) AS ModelNumber,(SELECT ShipOption FROM ShopProducts WHERE ProductID=IP.ProductID) AS ShipOption,INV.UserID,INV.RecurringID,INV.AffiliateID,INV.OrderDate FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND INV.InvoiceID=@InvoiceID AND INV.Status='0'", conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", sInvoiceID);
                    using (SqlDataReader PaidRS = cmd.ExecuteReader())
                    {
                        if (PaidRS.HasRows)
                        {
                            using (var cmd2 = new SqlCommand("UPDATE Invoices SET Status=1,TransactionID=@TransactionID,PaymentMethod=@PaymentMethod,inCart='0',DatePaid=@DatePaid WHERE InvoiceID=@InvoiceID", conn))
                            {
                                cmd2.Parameters.AddWithValue("@TransactionID", sTransaction);
                                cmd2.Parameters.AddWithValue("@PaymentMethod", sMethod);
                                cmd2.Parameters.AddWithValue("@DatePaid", DateTime.Now);
                                cmd2.Parameters.AddWithValue("@InvoiceID", sInvoiceID);
                                cmd2.ExecuteNonQuery();
                            }

                            while (PaidRS.Read())
                            {
                                if (toDouble(openNull(PaidRS["RecurringPrice"])) > 0)
                                {
                                    switch (openNull(PaidRS["RecurringCycle"]))
                                    {
                                        case "3m":
                                            sRecurringAdd = 3;
                                            break;

                                        case "6m":
                                            sRecurringAdd = 6;
                                            break;

                                        case "1y":
                                            sRecurringAdd = 12;
                                            break;

                                        default:
                                            sRecurringAdd = 1;
                                            break;
                                    }

                                    using (var cmd2 = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE isRecurring='0' AND InvoiceID=@InvoiceID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@InvoiceID", sNewInvoiceID);
                                        using (SqlDataReader PaidRS2 = cmd2.ExecuteReader())
                                        {
                                            if (!PaidRS.HasRows)
                                            {
                                                using (var cmd3 = new SqlCommand("INSERT INTO Invoices (InvoiceID,UserID,OrderDate,DatePaid,Status,TransactionID,PaymentMethod,AffiliateID,DiscountCode,isRecurring,RecurringID,inCart,PortalID) VALUES (@InvoiceID,@UserID,@OrderDate,@DatePaid,@Status,@TransactionID,@PaymentMethod,@AffiliateID,@DiscountCode,@isRecurring,@RecurringID,@inCart,@PortalID)", conn))
                                                {
                                                    cmd3.Parameters.AddWithValue("@InvoiceID", sNewInvoiceID);
                                                    cmd3.Parameters.AddWithValue("@UserID", openNull(PaidRS["UserID"]));
                                                    cmd3.Parameters.AddWithValue("@OrderDate", DateAndTime.DateAdd(DateAndTime.DateInterval.Month, sRecurringAdd, toDate(openNull(PaidRS["OrderDate"]))));
                                                    cmd3.Parameters.AddWithValue("@DatePaid", string.Empty);
                                                    cmd3.Parameters.AddWithValue("@Status", "0");
                                                    cmd3.Parameters.AddWithValue("@TransactionID", string.Empty);
                                                    cmd3.Parameters.AddWithValue("@PaymentMethod", string.Empty);
                                                    cmd3.Parameters.AddWithValue("@AffiliateID", openNull(PaidRS["AffiliateID"]));
                                                    cmd3.Parameters.AddWithValue("@DiscountCode", string.Empty);
                                                    cmd3.Parameters.AddWithValue("@isRecurring", "1");
                                                    cmd3.Parameters.AddWithValue("@RecurringID", openNull(PaidRS["RecurringID"]));
                                                    cmd3.Parameters.AddWithValue("@inCart", "0");
                                                    cmd3.Parameters.AddWithValue("@PortalID", openNull(PaidRS["PortalID"]));
                                                    cmd3.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }

                                    using (var cmd2 = new SqlCommand("INSERT INTO Invoices_Products (InvoiceProductID,InvoiceID,LinkID,ProductID,ModuleID,ProductName,UnitPrice,RecurringPrice,RecurringCycle,isRecurring,Quantity,AffiliateUnitPrice,AffiliateRecurringPrice,ExcludeAffiliate,PortalID,Handling) VALUES (@InvoiceProductID,@InvoiceID,@LinkID,@ProductID,@ModuleID,@ProductName,@UnitPrice,@RecurringPrice,@RecurringCycle,@isRecurring,@Quantity,@AffiliateUnitPrice,@AffiliateRecurringPrice,@ExcludeAffiliate,@PortalID,@Handling)", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@InvoiceProductID", GetIdentity());
                                        cmd2.Parameters.AddWithValue("@InvoiceID", sNewInvoiceID);
                                        cmd2.Parameters.AddWithValue("@LinkID", openNull(PaidRS["LinkID"]));
                                        cmd2.Parameters.AddWithValue("@ProductID", openNull(PaidRS["ProductID"]));
                                        cmd2.Parameters.AddWithValue("@ModuleID", openNull(PaidRS["ModuleID"]));
                                        cmd2.Parameters.AddWithValue("@ProductName", openNull(PaidRS["ProductName"]));
                                        cmd2.Parameters.AddWithValue("@UnitPrice", openNull(PaidRS["UnitPrice"]));
                                        cmd2.Parameters.AddWithValue("@RecurringPrice", openNull(PaidRS["RecurringPrice"]));
                                        cmd2.Parameters.AddWithValue("@RecurringCycle", openNull(PaidRS["RecurringCycle"]));
                                        cmd2.Parameters.AddWithValue("@isRecurring", "1");
                                        cmd2.Parameters.AddWithValue("@Quantity", openNull(PaidRS["Quantity"]));
                                        cmd2.Parameters.AddWithValue("@AffiliateUnitPrice", openNull(PaidRS["AffiliateUnitPrice"]));
                                        cmd2.Parameters.AddWithValue("@AffiliateRecurringPrice", openNull(PaidRS["AffiliateRecurringPrice"]));
                                        cmd2.Parameters.AddWithValue("@ExcludeAffiliate", openNull(PaidRS["ExcludeAffiliate"]));
                                        cmd2.Parameters.AddWithValue("@PortalID", openNull(PaidRS["PortalID"]));
                                        cmd2.Parameters.AddWithValue("@Handling", "0");
                                        cmd2.ExecuteNonQuery();
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(openNull(PaidRS["ProductID"])))
                                {
                                    using (var cmd2 = new SqlCommand("UPDATE ShopProducts SET Inventory=Inventory-@Inventory WHERE ProductID=@ProductID AND UseInventory='1' AND Inventory > '0'", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@Inventory", openNull(PaidRS["Quantity"]));
                                        cmd2.Parameters.AddWithValue("@ProductID", openNull(PaidRS["ProductID"]));
                                        cmd2.ExecuteNonQuery();
                                    }
                                }

                                switch (toInt(openNull(PaidRS["ModuleID"])))
                                {
                                    case 2:

                                        // Advertising Server Module
                                        using (var cmd2 = new SqlCommand("UPDATE Advertisements SET Status=1 WHERE Status='0' AND AdID=@LinkID AND UserID=@UserID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LinkID", openNull(PaidRS["LinkID"]));
                                            cmd2.Parameters.AddWithValue("@UserID", openNull(PaidRS["UserID"]));
                                            cmd2.ExecuteNonQuery();
                                        }

                                        break;

                                    case 20:

                                        // Business listings price to post
                                        using (var cmd2 = new SqlCommand("UPDATE BusinessListings SET Status=1 WHERE Status='0' AND LinkID=@LinkID AND UserID=@UserID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LinkID", openNull(PaidRS["LinkID"]));
                                            cmd2.Parameters.AddWithValue("@UserID", openNull(PaidRS["UserID"]));
                                            cmd2.ExecuteNonQuery();
                                        }

                                        IndexRecord(20, toLong(openNull(PaidRS["LinkID"])));
                                        break;

                                    case 31:

                                        // Auction price to Post
                                        using (var cmd2 = new SqlCommand("UPDATE AuctionAds SET Status=1 WHERE Status='0' AND LinkID=@LinkID AND UserID=@UserID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LinkID", openNull(PaidRS["LinkID"]));
                                            cmd2.Parameters.AddWithValue("@UserID", openNull(PaidRS["UserID"]));
                                            cmd2.ExecuteNonQuery();
                                        }

                                        IndexRecord(31, toLong(openNull(PaidRS["LinkID"])));
                                        break;

                                    case 37:

                                        // E-Learning Module
                                        using (var cmd2 = new SqlCommand("UPDATE ELearnStudents SET Active='1' WHERE Active='0' AND StudentID=@StudentID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@StudentID", openNull(PaidRS["UserID"]));
                                            cmd2.ExecuteNonQuery();
                                        }

                                        break;

                                    case 38:
                                        arrSplit = Strings.Split(openNull(PaidRS["ModelNumber"]), "-");
                                        Array.Resize(ref arrSplit, 2);
                                        PUB_Update_Class(arrSplit[1], openNull(PaidRS["UserID"]));
                                        break;

                                    case 44:

                                        // Classified Ad Pricing to Post
                                        using (var cmd2 = new SqlCommand("UPDATE ClassifiedsAds SET Status=1 WHERE Status='0' AND LinkID=@LinkID AND UserID=@UserID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LinkID", openNull(PaidRS["LinkID"]));
                                            cmd2.Parameters.AddWithValue("@UserID", openNull(PaidRS["UserID"]));
                                            cmd2.ExecuteNonQuery();
                                        }

                                        IndexRecord(44, toLong(openNull(PaidRS["LinkID"])));
                                        break;

                                    case 60:

                                        // Portals Module
                                        using (var cmd2 = new SqlCommand("UPDATE Portals SET Status=1 WHERE Status='0' AND PortalID=@LinkID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LinkID", openNull(PaidRS["LinkID"]));
                                            cmd2.ExecuteNonQuery();
                                        }

                                        break;

                                    case 65:
                                        Voucher_Send(openNull(PaidRS["LinkID"]), openNull(PaidRS["UserID"]), openNull(PaidRS["InvoiceProductID"]), toLong(openNull(PaidRS["Quantity"])), conn);
                                        break;

                                    case 66:

                                        // Job Board
                                        using (var cPCR = new PCRecruiter())
                                        {
                                            HttpWebRequest WRequest = null;
                                            var cJobs = new PCRecruiterJobs();
                                            cJobs.Status = "Available";

                                            var postData = JsonConvert.SerializeObject(cJobs);
                                            var byteArray = Encoding.UTF8.GetBytes(postData);
                                            var sessionId = cPCR.GetSessionId();

                                            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions/" + openNull(PaidRS["LinkID"]));
                                            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                                            WRequest.Method = "PUT";
                                            WRequest.ContentLength = byteArray.Length;
                                            WRequest.ContentType = "application/json";
                                            WRequest.Accept = "application/json";
                                            using (var dataStream = WRequest.GetRequestStream())
                                            {
                                                dataStream.Write(byteArray, 0, byteArray.Length);
                                            }
                                        }
                                        break;

                                    case 993:
                                        Add_Points_To_Users(toLong(openNull(PaidRS["Inventory"])), openNull(PaidRS["UserID"]), conn);
                                        break;
                                }

                                if (openNull(PaidRS["ShipOption"]) == "electronic" && !string.IsNullOrWhiteSpace(openNull(PaidRS["ProductID"])) && !string.IsNullOrWhiteSpace(sCustEmail))
                                {
                                    using (var cmd2 = new SqlCommand("SELECT UploadID FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID='41' AND ControlID='SelectFile' AND isTemp='0'", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UniqueID", openNull(PaidRS["ProductID"]));
                                        using (SqlDataReader PaidRS2 = cmd2.ExecuteReader())
                                        {
                                            if (PaidRS2.HasRows)
                                            {
                                                PaidRS2.Read();
                                                PUB_Compose_Email(sCustEmail, openNull(PaidRS2["UploadID"]));
                                            }
                                        }
                                    }
                                }

                                if (openBoolean(PaidRS["isRecurring"]) == false)
                                {
                                    if (!string.IsNullOrWhiteSpace(Setup(995, "EmailTempAdmin")))
                                    {
                                        using (var cmd2 = new SqlCommand("SELECT EmailSubject,EmailBody FROM EmailTemplates WHERE TemplateID=@TemplateID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@TemplateID", Setup(995, "EmailTempAdmin"));
                                            using (SqlDataReader PaidRS2 = cmd2.ExecuteReader())
                                            {
                                                if (PaidRS2.HasRows)
                                                {
                                                    PaidRS2.Read();
                                                    GetEmailSubject = openNull(PaidRS2["EmailSubject"]);
                                                    GetEmailBody = openNull(PaidRS2["EmailBody"]);
                                                    Send_Email(Setup(991, "AdminEmailAddress"), Setup(991, "AdminEmailAddress"), GetEmailSubject, GetEmailBody, 41);
                                                }
                                            }
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(Setup(995, "EmailTempCust")))
                                    {
                                        using (var cmd2 = new SqlCommand("SELECT EmailSubject,EmailBody FROM EmailTemplates WHERE TemplateID=@TemplateID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@TemplateID", Setup(995, "EmailTempCust"));
                                            using (SqlDataReader PaidRS2 = cmd2.ExecuteReader())
                                            {
                                                if (PaidRS2.HasRows)
                                                {
                                                    PaidRS2.Read();
                                                    GetEmailSubject = openNull(PaidRS2["EmailSubject"]);
                                                    GetEmailBody = openNull(PaidRS2["EmailBody"]);
                                                    Send_Email(GetUserInformation("EmailAddress", openNull(PaidRS["UserID"])), Setup(991, "AdminEmailAddress"), GetEmailSubject, GetEmailBody, 41);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pub update class.
        /// </summary>
        /// <param name="sModelNumber">The model number.</param>
        /// <param name="sUserID">Identifier for the user.</param>
        public static void PUB_Update_Class(string sModelNumber, string sUserID)
        {
            var sActDesc = string.Empty;
            var sClassName = string.Empty;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE AccessClass='2' AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", sUserID);
                    using (SqlDataReader ClassRS = cmd.ExecuteReader())
                    {
                        if (ClassRS.HasRows)
                        {
                            return;
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT ACLASS.ClassName FROM Members AS M,AccessClasses AS ACLASS WHERE M.AccessClass=ACLASS.ClassID AND M.UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", sUserID);
                    using (SqlDataReader ClassRS = cmd.ExecuteReader())
                    {
                        if (ClassRS.HasRows)
                        {
                            ClassRS.Read();
                            sClassName = openNull(ClassRS["ClassName"]);
                        }
                        else
                        {
                            sClassName = "N/A";
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT * FROM AccessClasses WHERE ClassID=@ClassID", conn))
                {
                    cmd.Parameters.AddWithValue("@ClassID", sModelNumber);
                    using (SqlDataReader ClassRS = cmd.ExecuteReader())
                    {
                        if (ClassRS.HasRows)
                        {
                            sActDesc = LangText("[[Username]] access class has been changed") + Environment.NewLine;
                            sActDesc += LangText("From Class: ~~" + sClassName + "~~") + Environment.NewLine;
                            sActDesc += LangText("To Class: ~~" + openNull(ClassRS["ClassName"]) + "~~") + Environment.NewLine;
                            Activity_Write("CHANGECLASS", sActDesc, 989, string.Empty, sUserID);
                            using (var cmd2 = new SqlCommand("UPDATE Members SET AccessClass=@AccessClass, AccessKeys=@AccessKeys, ClassChanged=getDate() WHERE UserID=@UserID", conn))
                            {
                                cmd2.Parameters.AddWithValue("@AccessClass", sModelNumber);
                                cmd2.Parameters.AddWithValue("@AccessKeys", openNull(ClassRS["KeyIDs"]));
                                cmd2.Parameters.AddWithValue("@UserID", sUserID);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the change log.
        /// </summary>
        /// <param name="moduleId">Identifier for the module.</param>
        /// <param name="uniqueId">Unique identifier.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="XML">The XML.</param>
        public static void Update_Change_Log(int moduleId, string uniqueId, string Subject, string XML)
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("INSERT INTO ChangeLog (ChangeID, UniqueID, ModuleID, PortalID, Subject, ChangedData, DateChanged) VALUES(@ChangeID, @UniqueID, @ModuleID, @PortalID, @Subject, @ChangedData, @DateChanged)", conn))
                {
                    cmd.Parameters.AddWithValue("@ChangeID", GetIdentity());
                    cmd.Parameters.AddWithValue("@UniqueID", uniqueId);
                    cmd.Parameters.AddWithValue("@ModuleID", moduleId);
                    cmd.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@ChangedData", XML);
                    cmd.Parameters.AddWithValue("@DateChanged", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Voucher send.
        /// </summary>
        /// <param name="sVoucherId">Identifier for the voucher.</param>
        /// <param name="sUserID">Identifier for the user.</param>
        /// <param name="sInvoiceProductID">Identifier for the invoice product.</param>
        /// <param name="iQuantity">Zero-based index of the quantity.</param>
        /// <param name="Conn">The connection.</param>
        public static void Voucher_Send(string sVoucherId, string sUserID, string sInvoiceProductID, long iQuantity, SqlConnection Conn)
        {
            var random = new Random();

            var purchaseCode = string.Empty;

            var GetEmailSubject = string.Empty;
            var GetEmailBody = string.Empty;

            using (var cmd = new SqlCommand("DELETE FROM VouchersPurchased WHERE VoucherID='" + FixWord(sVoucherId) + "' AND UserID='" + FixWord(sUserID) + "' AND CartID='" + FixWord(sInvoiceProductID) + "'", Conn))
            {
                cmd.ExecuteNonQuery();
            }

            for (var i = 1; i <= iQuantity; i++)
            {
                using (var cmd = new SqlCommand("SELECT * FROM Vouchers WHERE VoucherID='" + FixWord(sVoucherId) + "'", Conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            purchaseCode = openNull(RS["PurchaseCode"]) + "-" + random.Next(1, 9) + Convert.ToInt32(26 * random.NextDouble() + 65) + random.Next(1, 9) + Convert.ToInt32(26 * random.NextDouble() + 65) + random.Next(1, 9) + Convert.ToInt32(26 * random.NextDouble() + 65);
                            using (var cmd2 = new SqlCommand("INSERT INTO VouchersPurchased (PurchaseID,VoucherID,PortalID,UserID,PurchaseCode,Redeemed,CartID) VALUES('" + GetIdentity() + "','" + FixWord(sVoucherId) + "','" + FixWord(Strings.ToString(Get_Portal_ID())) + "','" + FixWord(sUserID) + "','" + FixWord(purchaseCode) + "','0','" + FixWord(sInvoiceProductID) + "')", Conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }

                            using (var cmd2 = new SqlCommand("UPDATE Vouchers SET TotalPurchases=TotalPurchases+1 WHERE VoucherID='" + FixWord(sVoucherId) + "'", Conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }

                            using (var cmd2 = new SqlCommand("UPDATE Vouchers SET Quantity=Quantity-1 WHERE VoucherID='" + FixWord(sVoucherId) + "'", Conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            using (var cmd = new SqlCommand("SELECT BuyEmailID,AdminEmailID FROM Vouchers WHERE VoucherID='" + FixWord(sVoucherId) + "'", Conn))
            {
                using (SqlDataReader RS = cmd.ExecuteReader())
                {
                    if (RS.HasRows)
                    {
                        RS.Read();
                        if (!string.IsNullOrWhiteSpace(openNull(RS["AdminEmailID"])))
                        {
                            using (var cmd2 = new SqlCommand("SELECT EmailSubject,EmailBody FROM EmailTemplates WHERE TemplateID='" + FixWord(openNull(RS["AdminEmailID"])) + "'", Conn))
                            {
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    if (RS2.HasRows)
                                    {
                                        RS2.Read();
                                        GetEmailSubject = openNull(RS2["EmailSubject"]);
                                        GetEmailBody = Replace_Fields(openNull(RS2["EmailBody"]), sUserID, sInvoiceProductID);
                                    }
                                    else
                                    {
                                        GetEmailSubject = LangText("You voucher purchase information");
                                        GetEmailBody = Voucher_Send_Default(sUserID, sInvoiceProductID);
                                    }
                                }
                                Send_Email(Setup(991, "AdminEmailAddress"), Setup(991, "AdminEmailAddress"), GetEmailSubject, GetEmailBody, 65);
                            }
                        }
                        else
                        {
                            GetEmailSubject = LangText("You voucher purchase information");
                            GetEmailBody = Voucher_Send_Default(sUserID, sInvoiceProductID);
                            Send_Email(GetUserInformation("EmailAddress", sUserID), Setup(991, "AdminEmailAddress"), GetEmailSubject, GetEmailBody, 65);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Voucher send default.
        /// </summary>
        /// <param name="sUserID">Identifier for the user.</param>
        /// <param name="sInvoiceProductID">Identifier for the invoice product.</param>
        /// <returns>A string.</returns>
        public static string Voucher_Send_Default(string sUserID, string sInvoiceProductID)
        {
            var str = new StringBuilder();

            str.Append(LangText("Greetings ~~" + GetUserInformation("FirstName", sUserID) + " " + GetUserInformation("LastName", sUserID) + "~~,") + "<br/><br/>");
            str.Append(LangText("Thank you for purchasing a \"Power Buy Deal\" from ~~" + Setup(992, "WebsiteName") + "~~.") + "<br/><br/>");
            str.Append(Vouchers_Print(sInvoiceProductID));
            str.Append(LangText("Thank You") + "<br/>");

            return Strings.ToString(str);
        }
    }
}