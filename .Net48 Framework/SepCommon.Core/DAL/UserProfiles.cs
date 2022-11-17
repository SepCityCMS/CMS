// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UserProfiles.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class UserProfiles.
    /// </summary>
    public static class UserProfiles
    {
        /// <summary>
        /// Gets the user profiles.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="StartDate">The start date.</param>
        /// <returns>List&lt;Models.UserProfiles&gt;.</returns>
        public static List<Models.UserProfiles> GetUserProfiles(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "", int ModuleID = 63, string StartDate = "")
        {
            var lUserProfiles = new List<Models.UserProfiles>();

            var wClause = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

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
                wClause = " AND Username LIKE '" + SepFunctions.FixWord(searchWords) + "%'";
            }

            if (Information.IsDate(StartDate))
            {
                wClause += " AND M.CreateDate > '" + StartDate + "'";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT P.ProfileID,P.UserID,M.Username,P.Views,P.Status,P.DatePosted,M.LastLogin,M.FirstName,M.LastName,M.Male,M.City,M.State,CONVERT(int,DATEDIFF(hour,M.BirthDate,GETDATE())/8766.0,0) AS Age,M.ZipCode,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='63' AND UniqueID=P.ProfileID AND Uploads.isTemp='0' AND Uploads.Approved='1' AND (Right(FileName,4) = '.png' OR Right(FileName,4) = '.gif' OR Right(FileName,4) = '.jpg' OR Right(FileName,5) = '.jpeg') ORDER BY Weight) AS UploadID, (SELECT TOP 1 UserID FROM OnlineUsers WHERE Location <> 'Logout' AND (CurrentStatus <> 'DELETED' OR CurrentStatus IS NULL OR CurrentStatus <> '') AND UserID=M.UserID) AS isOnline FROM Members AS M,Profiles AS P WHERE P.UserID=M.UserID AND P.PortalID=@PortalID AND P.Status <> -1 AND M.Status=1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dUserProfiles = new Models.UserProfiles { UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]) };
                    dUserProfiles.ProfileID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ProfileID"]));
                    dUserProfiles.Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dUserProfiles.Views = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Views"]));
                    dUserProfiles.Age = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Age"]));
                    dUserProfiles.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dUserProfiles.LastLogin = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["LastLogin"]));
                    dUserProfiles.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dUserProfiles.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    if (SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Male"]))
                    {
                        dUserProfiles.Sex = SepFunctions.LangText("Male");
                    }
                    else
                    {
                        dUserProfiles.Sex = SepFunctions.LangText("Female");
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"])))
                    {
                        dUserProfiles.DefaultPicture = sImageFolder + "spadmin/show_image.aspx?ModuleID=63&Size=thumb&UploadID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UploadID"]);
                    }
                    else
                    {
                        if (SepFunctions.openBoolean(ds.Tables[0].Rows[i]["Male"]))
                        {
                            dUserProfiles.DefaultPicture = sImageFolder + "images/public/no-photo-male.jpg";
                        }
                        else
                        {
                            dUserProfiles.DefaultPicture = sImageFolder + "images/public/no-photo-female.jpg";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("ZipCode")) && SepFunctions.Setup(3, "SearchRadius") != "No")
                    {
                        dUserProfiles.Distance = SepFunctions.PostalCodeDistance(SepFunctions.GetUserInformation("ZipCode"), SepFunctions.openNull(ds.Tables[0].Rows[i]["ZipCode"])) + " ";
                        if (SepFunctions.GetUserCountry() == "us")
                        {
                            dUserProfiles.Distance += SepFunctions.LangText("Miles");
                        }
                        else
                        {
                            dUserProfiles.Distance += SepFunctions.LangText("Kilometers");
                        }
                    }

                    var sLocationSeperator = !string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["City"])) && !string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["State"])) ? ", " : string.Empty;

                    dUserProfiles.Location = SepFunctions.openNull(ds.Tables[0].Rows[i]["City"]) + sLocationSeperator + SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    dUserProfiles.DatePosted = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DatePosted"]));
                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["isOnline"])))
                    {
                        dUserProfiles.isOnline = true;
                    }
                    else
                    {
                        dUserProfiles.isOnline = false;
                    }

                    if (ModuleID == 64)
                    {
                        var jCustom1 = CustomFields.Answer_Get(117193851274426, SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                        dUserProfiles.Charities = Information.UBound(Strings.Split(jCustom1.FieldValue, Environment.NewLine)) > 0 ? Information.UBound(Strings.Split(jCustom1.FieldValue, Environment.NewLine)) + Strings.ToString(1) : "0";
                        var jCustom2 = CustomFields.Answer_Get(179115432969858, SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                        dUserProfiles.Causes = Information.UBound(Strings.Split(jCustom2.FieldValue, Environment.NewLine)) > 0 ? Information.UBound(Strings.Split(jCustom2.FieldValue, Environment.NewLine)) + Strings.ToString(1) : "0";
                        var jCustom3 = CustomFields.Answer_Get(906800028608884, SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]));
                        dUserProfiles.Occupation = jCustom3.FieldValue;
                    }

                    lUserProfiles.Add(dUserProfiles);
                }
            }

            return lUserProfiles;
        }

        /// <summary>
        /// Profiles the change status.
        /// </summary>
        /// <param name="ProfileIDs">The profile i ds.</param>
        /// <param name="Status">The status.</param>
        /// <returns>System.String.</returns>
        public static string Profile_Change_Status(string ProfileIDs, int Status)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrProfileIDs = Strings.Split(ProfileIDs, ",");

                if (arrProfileIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrProfileIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Profiles SET Status=@Status WHERE ProfileID=@ProfileID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ProfileID", arrProfileIDs[i]);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error change status:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Profile(s) status has been successfully saved.");

            return sReturn;
        }

        /// <summary>
        /// Profiles the delete.
        /// </summary>
        /// <param name="ProfileIDs">The profile i ds.</param>
        /// <returns>System.String.</returns>
        public static string Profile_Delete(string ProfileIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrProfileIDs = Strings.Split(ProfileIDs, ",");

                if (arrProfileIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrProfileIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Profiles SET Status='-1', DateDeleted=@DateDeleted WHERE ProfileID=@ProfileID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ProfileID", arrProfileIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.Additional_Data_Delete(63, arrProfileIDs[i]);
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deleting ID's:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Profile(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Profiles the get.
        /// </summary>
        /// <param name="ProfileID">The profile identifier.</param>
        /// <param name="ChangeID">The change identifier.</param>
        /// <returns>Models.UserProfiles.</returns>
        public static Models.UserProfiles Profile_Get(long ProfileID, long ChangeID = 0)
        {
            var returnXML = new Models.UserProfiles();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Profiles SET Views=Views+1 WHERE ProfileID=@ProfileID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ProfileID", ProfileID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT P.*,M.UserName,M.Male,M.CreateDate AS MemberSince,M.BirthDate,M.City,M.State,M.LastLogin,(SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='63' AND UserID=P.UserID AND Uploads.isTemp='0' AND Uploads.Approved='1') AS UploadID, (SELECT TOP 1 UserID FROM OnlineUsers WHERE Location <> 'Logout' AND CurrentStatus <> 'DELETED' AND UserID=M.UserID) AS isOnline FROM Profiles AS P,Members AS M WHERE P.ProfileID=@ProfileID AND P.UserID=M.UserID AND P.Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@ProfileID", ProfileID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var logData = string.Empty;
                            if (ChangeID > 0)
                            {
                                logData = SepFunctions.Get_Change_Log(ChangeID);
                            }

                            if (ChangeID > 0 && !string.IsNullOrWhiteSpace(logData))
                            {
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    var fieldName = RS.GetName(i);
                                    var fieldType = RS.GetFieldType(i);
                                    var fieldValue = SepFunctions.openNull(RS[i]);
                                    if (Strings.InStr(logData, "<" + fieldName + ">") > 0)
                                    {
                                        var xmlNode = SepFunctions.ParseXML(fieldName, logData);
                                        if (!string.IsNullOrWhiteSpace(xmlNode))
                                        {
                                            fieldValue = xmlNode;
                                        }
                                    }

                                    if (fieldName == "isOnline")
                                    {
                                        if (!string.IsNullOrWhiteSpace(fieldValue))
                                        {
                                            returnXML.isOnline = true;
                                        }
                                        else
                                        {
                                            returnXML.isOnline = false;
                                        }
                                    }
                                    else
                                    {
                                        var prop = returnXML.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
                                        if (null != prop && prop.CanWrite)
                                        {
                                            switch (fieldType.Name)
                                            {
                                                case "Double":
                                                    prop.SetValue(returnXML, SepFunctions.toLong(fieldValue), null);
                                                    break;

                                                case "DateTime":
                                                    prop.SetValue(returnXML, SepFunctions.toDate(fieldValue), null);
                                                    break;

                                                case "Boolean":
                                                    prop.SetValue(returnXML, SepFunctions.toBoolean(fieldValue), null);
                                                    break;

                                                case "Int32":
                                                    prop.SetValue(returnXML, SepFunctions.toInt(fieldValue), null);
                                                    break;

                                                case "Decimal":
                                                    prop.SetValue(returnXML, SepFunctions.toDecimal(fieldValue), null);
                                                    break;

                                                default:
                                                    prop.SetValue(returnXML, fieldValue, null);
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                returnXML.ProfileID = SepFunctions.toLong(SepFunctions.openNull(RS["ProfileID"]));
                                returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                                returnXML.Username = SepFunctions.openNull(RS["UserName"]);
                                returnXML.EnableComment = SepFunctions.toBoolean(SepFunctions.openNull(RS["EnableComment"]));
                                returnXML.AboutMe = SepFunctions.openNull(RS["AboutMe"]);
                                returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                                returnXML.Views = SepFunctions.toLong(SepFunctions.openNull(RS["Views"]));
                                returnXML.ProfileType = SepFunctions.toInt(SepFunctions.openNull(RS["ProfileType"]));
                                returnXML.HotOrNot = SepFunctions.openBoolean(RS["HotOrNot"]);
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["UploadID"])))
                                {
                                    returnXML.DefaultPicture = "/spadmin/show_image.aspx?ModuleID=63&Size=thumb&UploadID=" + SepFunctions.openNull(RS["UploadID"]);
                                }
                                else
                                {
                                    if (SepFunctions.openBoolean(RS["Male"]))
                                    {
                                        returnXML.DefaultPicture = "/images/public/no-photo-male.jpg";
                                    }
                                    else
                                    {
                                        returnXML.DefaultPicture = "/images/public/no-photo-female.jpg";
                                    }
                                }

                                if (SepFunctions.openBoolean(RS["Male"]))
                                {
                                    returnXML.Sex = "Male";
                                }
                                else
                                {
                                    returnXML.Sex = "Female";
                                }

                                var sLocationSeperator = !string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["City"])) && !string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["State"])) ? ", " : string.Empty;

                                returnXML.Location = SepFunctions.openNull(RS["City"]) + sLocationSeperator + SepFunctions.openNull(RS["State"]);
                                returnXML.LastLogin = SepFunctions.toDate(SepFunctions.openNull(RS["LastLogin"]));
                                returnXML.DatePosted = SepFunctions.toDate(SepFunctions.openNull(RS["DatePosted"]));
                                returnXML.MemberSince = SepFunctions.toDate(SepFunctions.openNull(RS["MemberSince"]));
                                returnXML.Age = DateAndTime.DateDiff(DateAndTime.DateInterval.Year, SepFunctions.toDate(SepFunctions.openNull(RS["BirthDate"])), DateTime.Now);
                                returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["isOnline"])))
                                {
                                    returnXML.isOnline = true;
                                }
                                else
                                {
                                    returnXML.isOnline = false;
                                }

                                returnXML.BGColor = SepFunctions.openNull(RS["BGColor"]);
                                returnXML.TextColor = SepFunctions.openNull(RS["TextColor"]);
                                returnXML.LinkColor = SepFunctions.openNull(RS["LinkColor"]);
                            }
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Profiles the save.
        /// </summary>
        /// <param name="ProfileID">The profile identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="AboutMe">The about me.</param>
        /// <param name="EnableComment">if set to <c>true</c> [enable comment].</param>
        /// <param name="HotOrNot">if set to <c>true</c> [hot or not].</param>
        /// <param name="ProfileType">Type of the profile.</param>
        /// <param name="BGColor">Color of the bg.</param>
        /// <param name="TextColor">Color of the text.</param>
        /// <param name="LinkColor">Color of the link.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Profile_Save(long ProfileID, string UserID, string AboutMe, bool EnableComment, bool HotOrNot, int ProfileType, string BGColor, string TextColor, string LinkColor, bool Approved, long PortalID)
        {
            var bUpdate = false;
            var isNewRecord = false;
            var intReturn = 0;

            var oldValues = new Hashtable();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (ProfileID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Profiles WHERE ProfileID=@ProfileID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProfileID", ProfileID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    oldValues.Add(RS.GetName(i), SepFunctions.openNull(RS[i]));
                                }

                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    ProfileID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Profiles SET EnableComment=@EnableComment,AboutMe=@AboutMe,HotOrNot=@HotOrNot,ProfileType=@ProfileType,BGColor=@BGColor,TextColor=@TextColor,LinkColor=@LinkColor WHERE ProfileID=@ProfileID";
                }
                else
                {
                    SqlStr = "INSERT INTO Profiles (ProfileID,UserID,EnableComment,AboutMe,PortalID,HotOrNot,BGColor,TextColor,LinkColor,Views,Status,DatePosted,ProfileType) VALUES(@ProfileID,@UserID,@EnableComment,@AboutMe,@PortalID,@HotOrNot,@BGColor,@TextColor,@LinkColor,'0',@Status,@DatePosted,@ProfileType)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ProfileID", ProfileID);
                    cmd.Parameters.AddWithValue("@EnableComment", EnableComment);
                    cmd.Parameters.AddWithValue("@HotOrNot", HotOrNot);
                    cmd.Parameters.AddWithValue("@AboutMe", AboutMe);
                    cmd.Parameters.AddWithValue("@ProfileType", ProfileType);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@BGColor", BGColor);
                    cmd.Parameters.AddWithValue("@TextColor", TextColor);
                    cmd.Parameters.AddWithValue("@LinkColor", LinkColor);
                    cmd.Parameters.AddWithValue("@Status", Approved ? "1" : "0");
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM Profiles WHERE UserID=@UserID AND Status='-1'", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }

                // Write Additional Data
                if (bUpdate == false)
                {
                    isNewRecord = true;
                }

                intReturn = SepFunctions.Additional_Data_Save(isNewRecord, 63, Strings.ToString(ProfileID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "Profile", "User Profiles", "AddProfile");

                if (oldValues.Count > 0)
                {
                    var changedValues = new Hashtable();
                    using (var cmd = new SqlCommand("SELECT * FROM Profiles WHERE ProfileID=@ProfileID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProfileID", ProfileID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                for (var i = 0; i < RS.FieldCount; i++)
                                {
                                    if (oldValues.ContainsKey(RS.GetName(i)))
                                    {
                                        if (SepFunctions.openNull(RS[i]) != SepFunctions.openNull(oldValues[RS.GetName(i)]))
                                        {
                                            changedValues.Add(RS.GetName(i), SepFunctions.openNull(oldValues[RS.GetName(i)]));
                                        }
                                    }
                                }

                                bUpdate = true;
                            }
                        }
                    }

                    if (changedValues.Count > 0)
                    {
                        var writeLog = new StringBuilder();
                        writeLog.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\" ?>");
                        writeLog.AppendLine("<root>");
                        for (var e = changedValues.GetEnumerator(); e.MoveNext();)
                        {
                            writeLog.AppendLine("<" + e.Key + ">" + SepFunctions.HTMLEncode(Strings.ToString(e.Value)) + "</" + e.Key + ">");
                        }

                        writeLog.AppendLine("</root>");
                        SepFunctions.Update_Change_Log(63, Strings.ToString(ProfileID), SepFunctions.GetUserInformation("UserName", UserID), Strings.ToString(writeLog));
                    }
                }
            }

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
        /// Profiles the user identifier to profile identifier.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.Int64.</returns>
        public static long Profile_UserID_To_ProfileID(string UserID)
        {
            var returnValue = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ProfileID FROM Profiles WHERE UserID=@UserID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnValue = SepFunctions.openNull(RS["ProfileID"]);
                        }
                    }
                }
            }

            return SepFunctions.toLong(returnValue);
        }
    }
}