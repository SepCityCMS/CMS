// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Members.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.DAL
{
    using SepCommon.Core.Models;
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Class Members.
    /// </summary>
    public static class Members
    {
        /// <summary>
        /// Dailies the signups.
        /// </summary>
        /// <returns>List&lt;Models.ChartData&gt;.</returns>
        public static List<ChartData> DailySignups()
        {
            var lData = new List<ChartData>();
            int iDays = 7;

            long Total = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                // Populate series data
                for (var i = 0; i <= 6; i++)
                {
                    iDays = iDays - 1;
                    using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Day(CreateDate) = '" + DateAndTime.Day(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -iDays, DateTime.Now)) + "' AND Month(CreateDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -iDays, DateTime.Now)) + "' AND Year(CreateDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -iDays, DateTime.Now)) + "' AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            Total = Total + SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                        }
                    }
                }

                iDays = 7;

                for (var i = 0; i <= 6; i++)
                {
                    iDays = iDays - 1;
                    using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Day(CreateDate) = '" + DateAndTime.Day(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -iDays, DateTime.Now)) + "' AND Month(CreateDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -iDays, DateTime.Now)) + "' AND Year(CreateDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -iDays, DateTime.Now)) + "' AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            var dData = new Models.ChartData { DayName = DateAndTime.WeekdayName(DateAndTime.Day(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -i, DateTime.Now))) };
                            dData.NumSignups = Math.Round(SepFunctions.toDouble(SepFunctions.openNull(RS["Counter"])), 0);
                            if (Total > 0)
                            {
                                dData.Percentage = SepFunctions.toDecimal(SepFunctions.openNull(RS["Counter"])) / Total * 100;
                            }
                            else
                            {
                                dData.Percentage = 0;
                            }

                            lData.Add(dData);
                        }
                    }
                }
            }

            return lData;
        }

        /// <summary>
        /// Facebooks the token save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Username">The username.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Facebook_Token">The facebook token.</param>
        /// <param name="Facebook_Id">The facebook identifier.</param>
        /// <param name="Facebook_User">The facebook user.</param>
        /// <returns>System.String.</returns>
        public static string Facebook_Token_Save(string UserID, string Username, string Password, string Facebook_Token, string Facebook_Id, string Facebook_User)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Members SET Facebook_Token=@Facebook_Token,Facebook_Id=@Facebook_Id,Facebook_User=@Facebook_User WHERE UserID=@UserID AND Username=@Username AND Password=@Pass", conn))
                {
                    cmd.Parameters.AddWithValue("@Facebook_Token", Facebook_Token);
                    cmd.Parameters.AddWithValue("@Facebook_Id", Facebook_Id);
                    cmd.Parameters.AddWithValue("@Facebook_User", Facebook_User);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Pass", Password);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = SepFunctions.LangText("You can now login with your Facebook account in the future.");

            return sReturn;
        }

        /// <summary>
        /// Gets the member errors.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.Members&gt;.</returns>
        public static List<Models.Members> GetMemberErrors(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "")
        {
            var lMembers = new List<Models.Members>();

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
                wClause = " AND (Username LIKE '%" + SepFunctions.FixWord(searchWords) + "%')";
            }

            // Check Username
            wClause += " AND ((Username LIKE '%~%'";
            wClause += " OR Username LIKE '%`%'";
            wClause += " OR Username LIKE '%@%'";
            wClause += " OR Username LIKE '%!%'";
            wClause += " OR Username LIKE '%#%'";
            wClause += " OR Username LIKE '%$%'";
            wClause += " OR Username LIKE '%\\%%'";
            wClause += " OR Username LIKE '%^%'";
            wClause += " OR Username LIKE '%&%'";
            wClause += " OR Username LIKE '%*%'";
            wClause += " OR Username LIKE '%(%'";
            wClause += " OR Username LIKE '%)%'";
            wClause += " OR Username LIKE '%+%'";
            wClause += " OR Username LIKE '%=%'";
            wClause += " OR Username LIKE '%{%'";
            wClause += " OR Username LIKE '%}%'";
            wClause += " OR Username LIKE '%|%'";
            wClause += " OR Username LIKE '%[%'";
            wClause += " OR Username LIKE '%]%'";
            wClause += " OR Username LIKE '%\\%'";
            wClause += " OR Username LIKE '%:%'";
            wClause += " OR Username LIKE '%\"%'";
            wClause += " OR Username LIKE '%;%'";
            wClause += " OR Username LIKE '%''%'";
            wClause += " OR Username LIKE '%<%'";
            wClause += " OR Username LIKE '%>%'";
            wClause += " OR Username LIKE '%?%'";
            wClause += " OR Username LIKE '%,%'";
            wClause += " OR Username LIKE '%/%')";

            // Check Email
            wClause += " OR (EmailAddress NOT LIKE '%@%' AND EmailAddress NOT LIKE '%.%'))";

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT UserID,Username,FirstName,LastName,EmailAddress,Status FROM Members WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dMembers = new Models.Members { UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]) };
                    dMembers.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dMembers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dMembers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dMembers.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dMembers.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    if (Strings.InStr(SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]), "@") == 0 || Strings.InStr(SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]), ".") == 0)
                    {
                        dMembers.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]) + " (" + SepFunctions.LangText("Invalid Email Address") + ")";
                    }
                    else
                    {
                        dMembers.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]) + " (" + SepFunctions.LangText("Invalid Characters in User Name") + ")";
                    }

                    lMembers.Add(dMembers);
                }
            }

            return lMembers;
        }

        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>List&lt;Models.Members&gt;.</returns>
        public static List<Models.Members> GetMembers(string SortExpression = "Username", string SortDirection = "ASC", string searchWords = "", long PortalID = -1)
        {
            var lMembers = new List<Models.Members>();

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
                wClause = " AND (Username LIKE '%" + SepFunctions.FixWord(searchWords) + "%' OR FirstName LIKE '%" + SepFunctions.FixWord(searchWords) + "%' OR LastName LIKE '%" + SepFunctions.FixWord(searchWords) + "%' OR EmailAddress LIKE '%" + SepFunctions.FixWord(searchWords) + "%')";
            }

            if (PortalID >= 0)
            {
                wClause = " AND (PortalID='" + SepFunctions.FixWord(Strings.ToString(PortalID)) + "')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT UserID,Username,FirstName,LastName,EmailAddress,City,State,Status,CreateDate,LastLogin FROM Members WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
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

                    var dMembers = new Models.Members { UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]) };
                    dMembers.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dMembers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dMembers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dMembers.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dMembers.City = SepFunctions.openNull(ds.Tables[0].Rows[i]["City"]);
                    dMembers.State = SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    dMembers.Status = SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["Status"]));
                    dMembers.CreateDate = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["CreateDate"]));
                    dMembers.LastLogin = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["LastLogin"]));
                    lMembers.Add(dMembers);
                }
            }

            return lMembers;
        }

        /// <summary>
        /// Gets the members most active.
        /// </summary>
        /// <returns>List&lt;Models.Members&gt;.</returns>
        public static List<Models.Members> GetMembersMostActive()
        {
            var lMembers = new List<Models.Members>();

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT M.UserID,M.Username,M.FirstName,M.LastName,M.EmailAddress,M.City,M.State,Count(ACT.UserID) AS ActCount FROM Members AS M,Activities AS ACT WHERE M.UserID=ACT.UserID AND M.PortalID=@PortalID GROUP BY M.UserID,M.Username,M.FirstName,M.LastName,M.EmailAddress,M.City,M.State ORDER BY ActCount DESC", conn))
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

                    var dMembers = new Models.Members { UserID = SepFunctions.openNull(ds.Tables[0].Rows[i]["UserID"]) };
                    dMembers.UserName = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]);
                    dMembers.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dMembers.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dMembers.EmailAddress = SepFunctions.openNull(ds.Tables[0].Rows[i]["EmailAddress"]);
                    dMembers.City = SepFunctions.openNull(ds.Tables[0].Rows[i]["City"]);
                    dMembers.State = SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    dMembers.ActCount = SepFunctions.openNull(ds.Tables[0].Rows[i]["ActCount"]);
                    lMembers.Add(dMembers);
                }
            }

            return lMembers;
        }

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Facebook_Token">The facebook token.</param>
        /// <param name="Facebook_Id">The facebook identifier.</param>
        /// <param name="Facebook_User">The facebook user.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="encryptedPassword">if set to <c>true</c> [encrypted password].</param>
        /// <param name="LinkedInId">The linked in identifier.</param>
        /// <returns>System.String.</returns>
        public static string Login(string Username, string Password, string Facebook_Token, string Facebook_Id, string Facebook_User, long PortalID, bool encryptedPassword, string LinkedInId)
        {
            var LoginField = "UserName";

            var sUserID = string.Empty;
            var sUsername = string.Empty;
            var sPassword = string.Empty;
            var sAccessKeys = string.Empty;

            var SqlStr = string.Empty;

            if (SepFunctions.ModuleActivated(68))
            {
                using (var LD = new LDAP())
                {
                    return LD.Login(Username, Password);
                }
            }

            Hashtable settings = SepFunctions.SetupArray("LoginEmail");

            if (settings["LoginEmail"].ToString() == "Yes")
            {
                LoginField = "EmailAddress";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(Facebook_Id) && !string.IsNullOrWhiteSpace(Facebook_User) && string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
                {
                    SqlStr = "SELECT UserID,UserName,EmailAddress,Password,AccessKeys,Status FROM Members WHERE Facebook_Id=@Facebook_Id AND Facebook_User=@Facebook_User AND Status <> -1";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(LinkedInId) && LinkedInId == Password)
                    {
                        SqlStr = "SELECT UserID,UserName,EmailAddress,Password,AccessKeys,Status FROM Members WHERE LinkedInId=@LinkedInId AND EmailAddress=@EmailAddress AND Status <> -1";
                    }
                    else
                    {
                        SqlStr = "SELECT UserID,UserName,EmailAddress,Password,AccessKeys,Status FROM Members WHERE " + LoginField + "=@Username AND Status <> -1";
                    }
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    if (!string.IsNullOrWhiteSpace(Facebook_Id) && !string.IsNullOrWhiteSpace(Facebook_User) && string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
                    {
                        cmd.Parameters.AddWithValue("@Facebook_Id", Facebook_Id);
                        cmd.Parameters.AddWithValue("@Facebook_User", Facebook_User);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(LinkedInId) && LinkedInId == Password)
                        {
                            cmd.Parameters.AddWithValue("@LinkedInId", LinkedInId);
                            cmd.Parameters.AddWithValue("@EmailAddress", Username);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Username", Username);
                        }
                    }

                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            if (!string.IsNullOrWhiteSpace(Facebook_Token))
                            {
                                return SepFunctions.LangText("You must have an account on our system or allow Facebook to login to your account by editing your account.");
                            }

                            if (!string.IsNullOrWhiteSpace(LinkedInId))
                            {
                                return SepFunctions.LangText("You must have an account on our system or allow LinkedIn to login to your account by editing your account.");
                            }

                            return SepFunctions.LangText("Invalid user name / password.");
                        }

                        RS.Read();
                        var isValid = false;
                        sUserID = SepFunctions.openNull(RS["UserID"]);
                        if (settings["LoginEmail"].ToString() == "Yes")
                        {
                            sUsername = SepFunctions.openNull(RS["EmailAddress"]);
                        }
                        else
                        {
                            sUsername = SepFunctions.openNull(RS["UserName"]);
                        }

                        if (string.IsNullOrWhiteSpace(Facebook_Token) && string.IsNullOrWhiteSpace(LinkedInId))
                        {
                            if (SepFunctions.Verify_Password(SepFunctions.openNull(RS["Password"]), Password) == false)
                            {
                                if (encryptedPassword && Strings.UCase(Password) == Strings.UCase(SepFunctions.openNull(RS["Password"])))
                                {
                                    isValid = true;
                                }
                                else
                                {
                                    if (Strings.UCase(SepFunctions.MD5Hash_Encrypt(Password)) == Strings.UCase(SepFunctions.openNull(RS["Password"])))
                                    {
                                        isValid = true;
                                    }
                                }

                                if (isValid == false)
                                {
                                    return SepFunctions.LangText("Invalid user name / password.");
                                }

                                sPassword = SepFunctions.Save_Password(Password);
                                using (var cmd2 = new SqlCommand("UPDATE Members SET Password=@Password WHERE UserName=@Username AND Status <> -1", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@Username", Username);
                                    cmd2.Parameters.AddWithValue("@Password", sPassword);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                sPassword = SepFunctions.openNull(RS["Password"]);
                            }
                        }

                        sAccessKeys = SepFunctions.openNull(RS["AccessKeys"]);

                        if (SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 0)
                        {
                            return SepFunctions.LangText("User name has not been activated.");
                        }

                        if (SepFunctions.Get_Portal_ID() > 0)
                        {
                            using (var cmd2 = new SqlCommand("SELECT LoginKeys FROM Portals WHERE PortalID=@PortalID", conn))
                            {
                                cmd2.Parameters.AddWithValue("@PortalID", PortalID);
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    if (RS2.HasRows)
                                    {
                                        RS2.Read();
                                        if (SepFunctions.CompareKeys(SepFunctions.openNull(RS2["LoginKeys"]), true, sUserID) == false)
                                        {
                                            return SepFunctions.LangText("Access denied to login to this portal.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return "USERID:" + sUserID + "||" + sUsername + "||" + sPassword + "||" + sAccessKeys + "||" + DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Logins the validate API session.
        /// </summary>
        /// <param name="SessionId">The session identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool LoginValidateAPISession(string SessionId)
        {
            var sXML = string.Empty;
            var licenseKey = string.Empty;
            var sResponse = string.Empty;
            var Username = string.Empty;
            var Password = string.Empty;

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "license.xml"))
            {
                try
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "license.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            // Select the book node with the matching attribute value.
                            var root = doc.DocumentElement;

                            sXML = root.InnerXml;
                        }
                    }

                    licenseKey = SepFunctions.ParseXML("LicenseKey", sXML);
                }
                catch
                {
                    licenseKey = "sepcity";
                }
            }
            else
            {
                licenseKey = "sepcity";
            }

            try
            {
                sResponse = SepFunctions.AES_Decrypt(SessionId, licenseKey);
                Username = Strings.Split(sResponse, "||")[1];
                Password = Strings.Split(sResponse, "||")[2];

                sResponse = Login(Username, Password, string.Empty, string.Empty, string.Empty, SepFunctions.Get_Portal_ID(), true, string.Empty);

                if (Strings.Left(sResponse, 7) == "USERID:")
                {
                    if (DateTime.Now <= SepFunctions.toDate(Strings.Replace(Strings.Split(sResponse, "||")[4], "|", string.Empty)).AddMinutes(20))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Members the add access key.
        /// </summary>
        /// <param name="KeyID">The key identifier.</param>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Add_Access_Key(long KeyID, string UserIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                string[] arrKeys = null;
                var sKeyIDs = string.Empty;

                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        sKeyIDs = string.Empty;

                        using (var cmd = new SqlCommand("SELECT AccessKeys FROM Members WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    arrKeys = Strings.Split(Strings.Replace(SepFunctions.openNull(RS["AccessKeys"]), "|", string.Empty), ",");

                                    if (arrKeys != null)
                                    {
                                        for (var j = 0; j <= Information.UBound(arrKeys); j++)
                                        {
                                            if (Strings.ToString(KeyID) != Strings.Replace(arrKeys[j], "|", string.Empty))
                                            {
                                                sKeyIDs += "|" + arrKeys[j] + "|,";
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        sKeyIDs += "|" + KeyID + "|";

                        using (var cmd = new SqlCommand("UPDATE Members SET AccessKeys=@AccessKeys WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AccessKeys", sKeyIDs);
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error activating:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Access key has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Members the add group.
        /// </summary>
        /// <param name="ListID">The list identifier.</param>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Add_Group(long ListID, string UserIDs)
        {
            var bError = string.Empty;

            var recordExists = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        recordExists = false;

                        using (var cmd = new SqlCommand("SELECT ListID FROM GroupListsUsers WHERE ListID=@ListID AND UserID=@UserID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@ListID", ListID);
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    recordExists = true;
                                }
                            }
                        }

                        if (recordExists == false)
                        {
                            using (var cmd = new SqlCommand("INSERT INTO GroupListsUsers (ListUserID, ListID, UserID, DatePosted, Status) VALUES(@ListUserID, @ListID, @UserID, @DatePosted, '1')", conn))
                            {
                                cmd.Parameters.AddWithValue("@ListUserID", SepFunctions.GetIdentity());
                                cmd.Parameters.AddWithValue("@ListID", ListID);
                                cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                                cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error activating:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Member(s) has been successfully added to a group list.");

            return sReturn;
        }

        /// <summary>
        /// Members the delete.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Delete(string UserIDs)
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
                        using (var cmd = new SqlCommand("UPDATE Members SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Activities SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Advertisements SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Articles SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE AuctionAds SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Blog SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE BusinessListings SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ClassifiedsAds SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE CustomFieldUsers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE DiscountSystem SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ELearnStudents SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE EventCalendar SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE FormAnswers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE ForumsMessages SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE GroupLists SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE GroupListsUsers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Guestbook SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE LibrariesFiles SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE LinksWebSites SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE MatchMaker SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE NewslettersUsers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE PhotoAlbums SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Portals SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Profiles SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE RStateAgents SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE RStateBrokers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE RStateProperty SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Vouchers SET Status='-1', DateDeleted=@DateDeleted WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM UPagesSites WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
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

            sReturn = SepFunctions.LangText("Member(s) has been successfully deleted.");

            return sReturn;
        }

        /// <summary>
        /// Members the get.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.Members.</returns>
        public static Models.Members Member_Get(string UserID)
        {
            var returnXML = new Models.Members();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT M.*,(SELECT TOP 1 UserName FROM Members WHERE AffiliateID=M.ReferralID) AS ReferralUserName FROM Members AS M WHERE M.UserID=@UserID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.UserID = SepFunctions.openNull(RS["UserID"]);
                            returnXML.UserName = SepFunctions.openNull(RS["UserName"]);
                            returnXML.Password = SepFunctions.openNull(RS["Password"]);
                            returnXML.Secret_Question = SepFunctions.openNull(RS["Secret_Question"]);
                            returnXML.Secret_Answer = SepFunctions.openNull(RS["Secret_Answer"]);
                            returnXML.PasswordResetID = SepFunctions.toLong(SepFunctions.openNull(RS["PasswordResetID"]));
                            returnXML.PasswordResetDate = SepFunctions.toDate(SepFunctions.openNull(RS["PasswordResetDate"]));
                            returnXML.EmailAddress = SepFunctions.openNull(RS["EmailAddress"]);
                            returnXML.FirstName = SepFunctions.openNull(RS["FirstName"]);
                            returnXML.LastName = SepFunctions.openNull(RS["LastName"]);
                            if (SepFunctions.openBoolean(RS["Male"]))
                            {
                                returnXML.Gender = "1";
                            }
                            else
                            {
                                returnXML.Gender = "0";
                            }

                            returnXML.BirthDate = SepFunctions.toDate(SepFunctions.openNull(RS["BirthDate"]));
                            returnXML.StreetNumber = SepFunctions.openNull(RS["StreetNumber"]);
                            returnXML.StreetAddress = SepFunctions.openNull(RS["StreetAddress"]);
                            returnXML.City = SepFunctions.openNull(RS["City"]);
                            returnXML.State = SepFunctions.openNull(RS["State"]);
                            returnXML.PostalCode = SepFunctions.openNull(RS["ZipCode"]);
                            returnXML.Country = Strings.UCase(SepFunctions.openNull(RS["Country"]));
                            returnXML.PhoneNumber = SepFunctions.openNull(RS["PhoneNumber"]);
                            returnXML.PayPal = SepFunctions.openNull(RS["PayPal"]);
                            returnXML.PortalID = SepFunctions.toLong(SepFunctions.openNull(RS["PortalID"]));
                            returnXML.AccessClass = SepFunctions.toLong(SepFunctions.openNull(RS["AccessClass"]));
                            returnXML.AccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                            returnXML.ClassChanged = SepFunctions.toDate(SepFunctions.openNull(RS["ClassChanged"]));
                            returnXML.UserNotes = SepFunctions.openNull(RS["UserNotes"]);
                            returnXML.LastLogin = SepFunctions.toDate(SepFunctions.openNull(RS["LastLogin"]));
                            returnXML.CreateDate = SepFunctions.toDate(SepFunctions.openNull(RS["CreateDate"]));
                            returnXML.ApproveFriends = SepFunctions.toBoolean(SepFunctions.openNull(RS["ApproveFriends"]));
                            returnXML.IPAddress = SepFunctions.openNull(RS["IPAddress"]);
                            returnXML.AffiliateID = SepFunctions.toLong(SepFunctions.openNull(RS["AffiliateID"]));
                            returnXML.ReferralID = SepFunctions.toLong(SepFunctions.openNull(RS["ReferralID"]));
                            returnXML.ReferralUserName = SepFunctions.openNull(RS["ReferralUserName"]);
                            returnXML.WebsiteURL = SepFunctions.openNull(RS["WebsiteURL"]);
                            returnXML.AffiliatePaid = SepFunctions.toDate(SepFunctions.openNull(RS["AffiliatePaid"]));
                            returnXML.UserPoints = SepFunctions.toLong(SepFunctions.openNull(RS["UserPoints"]));
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            returnXML.Facebook_Id = SepFunctions.openNull(RS["Facebook_Id"]);
                            returnXML.HideTips = SepFunctions.openBoolean(RS["HideTips"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Members the invite.
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="JoinClass">The join class.</param>
        /// <returns>System.String.</returns>
        public static string Member_Invite(string EmailAddress, string FirstName, string LastName, string JoinClass)
        {
            var InviteId = string.Empty;
            var addRecord = false;
            var EmailMessage = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT InviteID FROM MembersInvite WHERE EmailAddress=@EmailAddress", conn))
                {
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            InviteId = SepFunctions.openNull(RS["InviteID"]);
                        }
                        else
                        {
                            InviteId = Strings.ToString(SepFunctions.GetIdentity());
                            addRecord = true;
                        }
                    }
                }

                if (addRecord)
                {
                    using (var cmd = new SqlCommand("INSERT INTO MembersInvite (InviteID, EmailAddress, FirstName, LastName, ClassID, DatePosted) VALUES(@InviteID, @EmailAddress, @FirstName, @LastName, @ClassID, @DatePosted)", conn))
                    {
                        cmd.Parameters.AddWithValue("@InviteID", InviteId);
                        cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@ClassID", JoinClass);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }

                EmailMessage = "Greetings,<br/><br/>";
                EmailMessage += "You have been invited from the Administrator to join " + SepFunctions.Setup(992, "WebSiteName") + "! To join our site simply click the link below to fill out our simple signup form. Once the form is submitted you will be a new member on our web site.<br/><br/>";
                EmailMessage += "<a href=\"" + SepFunctions.GetMasterDomain(true) + "/signup.aspx?InviteID=" + InviteId + "\" target=\"_blank\">" + SepFunctions.GetMasterDomain(true) + "/signup.aspx?InviteID=" + InviteId + "</a><br/><br/>";
                EmailMessage += "Thank you for your time!";

                SepFunctions.Send_Email(EmailAddress, SepFunctions.Setup(991, "AdminEmailAddress"), "Invitation to join " + SepFunctions.Setup(992, "WebSiteName"), EmailMessage, 986);
            }

            var sReturn = string.Empty;

            sReturn = SepFunctions.LangText("User invite has been sent successfully.");

            return sReturn;
        }

        /// <summary>
        /// Members the mark active.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Mark_Active(string UserIDs)
        {
            var bError = string.Empty;
            var strBody = string.Empty;

            var GetAdminEmail = SepFunctions.Setup(991, "AdminEmailAddress");

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Members SET Status=1 WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("SELECT EmailAddress,UserName FROM Members WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    strBody = SepFunctions.LangText("Thank you for registering with us!") + "<br/><br/>";
                                    strBody = strBody + SepFunctions.LangText("Username") + ": " + SepFunctions.openNull(RS["Username"]) + "<br/>";
                                    strBody = strBody + "<br/><br/>" + SepFunctions.LangText("If you have any questions regarding this email or your account, please feel free to email us by responding to this message.");
                                    SepFunctions.Send_Email(SepFunctions.openNull(RS["EmailAddress"]), GetAdminEmail, SepFunctions.LangText("Your account has been approved"), strBody, 986);
                                }
                            }
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error activating:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Member(s) has been successfully activated.");

            return sReturn;
        }

        /// <summary>
        /// Members the mark not active.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Mark_Not_Active(string UserIDs)
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
                        using (var cmd = new SqlCommand("UPDATE Members SET Status='0' WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error deactivating:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Member(s) has been successfully deactivated.");

            return sReturn;
        }

        /// <summary>
        /// Members the move class.
        /// </summary>
        /// <param name="ClassID">The class identifier.</param>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Move_Class(long ClassID, string UserIDs)
        {
            var bError = string.Empty;

            var sKeyIDs = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT KeyIDs FROM AccessClasses WHERE ClassID=@ClassID", conn))
                {
                    cmd.Parameters.AddWithValue("@ClassID", ClassID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sKeyIDs = SepFunctions.openNull(RS["KeyIDs"]);
                        }
                    }
                }

                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE Members SET AccessClass=@ClassID, AccessKeys=@AccessKeys WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ClassID", ClassID);
                            cmd.Parameters.AddWithValue("@AccessKeys", sKeyIDs);
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error activating:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Member(s) has been successfully moved to another class.");

            return sReturn;
        }

        /// <summary>
        /// Members the remove access key.
        /// </summary>
        /// <param name="KeyID">The key identifier.</param>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public static string Member_Remove_Access_Key(long KeyID, string UserIDs)
        {
            var bError = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                string[] arrKeys = null;
                var sKeyIDs = string.Empty;

                var arrUserIDs = Strings.Split(UserIDs, ",");

                if (arrUserIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrUserIDs); i++)
                    {
                        sKeyIDs = string.Empty;

                        using (var cmd = new SqlCommand("SELECT AccessKeys FROM Members WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    arrKeys = Strings.Split(SepFunctions.openNull(RS["AccessKeys"]), ",");

                                    if (arrKeys != null)
                                    {
                                        for (var j = 0; j <= Information.UBound(arrKeys); j++)
                                        {
                                            if (Strings.ToString(KeyID) != Strings.Replace(arrKeys[j], "|", string.Empty))
                                            {
                                                sKeyIDs += "|" + arrKeys[j] + "|,";
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        sKeyIDs += Strings.Left(sKeyIDs, Strings.Len(sKeyIDs) - 1);

                        using (var cmd = new SqlCommand("UPDATE Members SET AccessKeys=@AccessKeys WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AccessKeys", sKeyIDs);
                            cmd.Parameters.AddWithValue("@UserID", arrUserIDs[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(bError))
            {
                sReturn = SepFunctions.LangText("There has been an error activating:") + " " + bError;
            }

            sReturn = SepFunctions.LangText("Access key has been successfully removed.");

            return sReturn;
        }

        /// <summary>
        /// Members the save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="MemberUserName">Name of the member user.</param>
        /// <param name="MemberPassword">The member password.</param>
        /// <param name="Secret_Question">The secret question.</param>
        /// <param name="Secret_Answer">The secret answer.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="Country">The country.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="PayPalEmail">The pay pal email.</param>
        /// <param name="Points">The points.</param>
        /// <param name="AccessClass">The access class.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="BirthDate">The birth date.</param>
        /// <param name="Gender">The gender.</param>
        /// <param name="ReferralID">The referral identifier.</param>
        /// <param name="WebsiteURL">The website URL.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <param name="ApproveFriends">The approve friends.</param>
        /// <param name="LetterIDs">The letter i ds.</param>
        /// <param name="Status">The status.</param>
        /// <param name="Facebook_Token">The facebook token.</param>
        /// <param name="Facebook_Id">The facebook identifier.</param>
        /// <param name="Facebook_User">The facebook user.</param>
        /// <param name="HideTips">if set to <c>true</c> [hide tips].</param>
        /// <param name="SiteId">The site identifier.</param>
        /// <param name="LinkedInID">The linked in identifier.</param>
        /// <returns>System.String.</returns>
        public static string Member_Save(string UserID, string MemberUserName, string MemberPassword, string Secret_Question, string Secret_Answer, string FirstName, string LastName, string StreetAddress, string City, string State, string PostalCode, string Country, string EmailAddress, string PhoneNumber, string PayPalEmail, long Points, long AccessClass, string AccessKeys, DateTime BirthDate, int Gender, string ReferralID, string WebsiteURL, long PortalID, string ApproveFriends, string LetterIDs, int Status, string Facebook_Token, string Facebook_Id, string Facebook_User, bool HideTips, long SiteId, string LinkedInID)
        {
            var bUpdate = false;
            var ReferralEmail = string.Empty;
            var ReferralUserName = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SiteId > 0)
                {
                    using (var cmd = new SqlCommand("SELECT Members.AffiliateID, Members.EmailAddress, Members.UserName FROM Members, UPagesSites WHERE Members.UserID=UPagesSites.UserID AND UPagesSites.SiteID=@SiteID AND Members.Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@SiteID", SiteId);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                ReferralID = SepFunctions.openNull(RS["AffiliateID"]);
                                ReferralEmail = SepFunctions.openNull(RS["EmailAddress"]);
                                ReferralUserName = SepFunctions.openNull(RS["UserName"]);
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(ReferralID))
                    {
                        using (var cmd = new SqlCommand("SELECT AffiliateID, EmailAddress, UserName FROM Members WHERE AffiliateID=@AffiliateID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@AffiliateID", ReferralID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    ReferralID = SepFunctions.openNull(RS["AffiliateID"]);
                                    ReferralEmail = SepFunctions.openNull(RS["EmailAddress"]);
                                    ReferralUserName = SepFunctions.openNull(RS["UserName"]);
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(UserID))
                {
                    using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
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
                    UserID = SepFunctions.Generate_GUID();
                }

                // Send affiliate email
                if (bUpdate == false && !string.IsNullOrWhiteSpace(ReferralID))
                {
                    SepFunctions.Send_Email(ReferralEmail, EmailAddress, SepFunctions.LangText("~~" + MemberUserName + "~~ has signed up under your username - ") + ReferralUserName, SepFunctions.LangText("You have a new referral on ~~" + SepFunctions.Setup(992, "WebSiteName") + "~~"), 29);
                }

                if (!Information.IsDate(BirthDate))
                {
                    BirthDate = DateTime.Today;
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE Members SET UserName=@MemberUserName" + Strings.ToString(!string.IsNullOrWhiteSpace(MemberPassword) ? ", Password=@MemberPassword" : string.Empty) + string.Empty + Strings.ToString(!string.IsNullOrWhiteSpace(LinkedInID) ? ", LinkedInID=@LinkedInID" : string.Empty) + ", FirstName=@FirstName, LastName=@LastName, StreetAddress=@StreetAddress, City=@City, State=@State, ZipCode=@PostalCode, Country=@Country, EmailAddress=@EmailAddress, PhoneNumber=@PhoneNumber, PayPal=@PayPalEmail, UserPoints=@Points, AccessClass=@AccessClass, AccessKeys=@AccessKeys, BirthDate=@BirthDate, Male=@Gender" + Strings.ToString(!string.IsNullOrWhiteSpace(ReferralID) ? ", ReferralID=@ReferralID" : string.Empty) + ", WebsiteURL=@WebsiteURL, ApproveFriends=@ApproveFriends, PortalID=@PortalID, HideTips=@HideTips, Status=@Status WHERE UserID=@UserID";
                }
                else
                {
                    SqlStr = "INSERT INTO Members (UserID, UserName" + Strings.ToString(!string.IsNullOrWhiteSpace(MemberPassword) ? ", Password" : string.Empty) + Strings.ToString(!string.IsNullOrWhiteSpace(Secret_Answer) && !string.IsNullOrWhiteSpace(Secret_Question) ? ", Secret_Question, Secret_Answer" : string.Empty) + ", FirstName, LastName, StreetAddress, City, State, ZipCode, Country, EmailAddress, PhoneNumber, PayPal, UserPoints, AccessClass, AccessKeys, BirthDate, Male, ReferralID, WebsiteURL, PortalID, ClassChanged, LastLogin, CreateDate, ApproveFriends, AffiliateID, IPAddress, Facebook_Token, Facebook_Id, Facebook_User, LinkedInID, HideTips, Status, SiteID) VALUES (@UserID, @MemberUserName" + Strings.ToString(!string.IsNullOrWhiteSpace(MemberPassword) ? ", @MemberPassword" : string.Empty) + Strings.ToString(!string.IsNullOrWhiteSpace(Secret_Answer) && !string.IsNullOrWhiteSpace(Secret_Question) ? ", @Secret_Question, @Secret_Answer" : string.Empty) + ", @FirstName, @LastName, @StreetAddress, @City, @State, @PostalCode, @Country, @EmailAddress, @PhoneNumber, @PayPalEmail, @Points, @AccessClass, @AccessKeys, @BirthDate, @Gender, @ReferralID, @WebsiteURL, @PortalID, @ClassChanged, @LastLogin, @CreateDate, @ApproveFriends, @AffiliateID, @IPAddress, @Facebook_Token, @Facebook_Id, @Facebook_User, @LinkedInID, @HideTips, @Status, @SiteID)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@MemberUserName", MemberUserName);
                    if (!string.IsNullOrWhiteSpace(MemberPassword))
                    {
                        cmd.Parameters.AddWithValue("@MemberPassword", SepFunctions.Save_Password(MemberPassword));
                    }

                    if (!string.IsNullOrWhiteSpace(Secret_Question))
                    {
                        cmd.Parameters.AddWithValue("@Secret_Question", Secret_Question);
                    }

                    if (!string.IsNullOrWhiteSpace(Secret_Answer))
                    {
                        cmd.Parameters.AddWithValue("@Secret_Answer", Secret_Answer);
                    }

                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@StreetAddress", !string.IsNullOrWhiteSpace(StreetAddress) ? StreetAddress : string.Empty);
                    cmd.Parameters.AddWithValue("@City", !string.IsNullOrWhiteSpace(City) ? City : string.Empty);
                    cmd.Parameters.AddWithValue("@State", !string.IsNullOrWhiteSpace(State) ? State : string.Empty);
                    cmd.Parameters.AddWithValue("@PostalCode", !string.IsNullOrWhiteSpace(PostalCode) ? PostalCode : string.Empty);
                    cmd.Parameters.AddWithValue("@Country", !string.IsNullOrWhiteSpace(Country) ? Country : string.Empty);
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", !string.IsNullOrWhiteSpace(PhoneNumber) ? PhoneNumber : string.Empty);
                    cmd.Parameters.AddWithValue("@PayPalEmail", !string.IsNullOrWhiteSpace(PayPalEmail) ? PayPalEmail : string.Empty);
                    cmd.Parameters.AddWithValue("@Points", Points);
                    cmd.Parameters.AddWithValue("@AccessClass", AccessClass);
                    cmd.Parameters.AddWithValue("@AccessKeys", !string.IsNullOrWhiteSpace(AccessKeys) ? AccessKeys : string.Empty);
                    cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                    if (Gender == 1)
                    {
                        cmd.Parameters.AddWithValue("@Gender", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Gender", false);
                    }

                    cmd.Parameters.AddWithValue("@ReferralID", !string.IsNullOrWhiteSpace(ReferralID) ? ReferralID : string.Empty);
                    cmd.Parameters.AddWithValue("@WebsiteURL", !string.IsNullOrWhiteSpace(WebsiteURL) ? WebsiteURL : string.Empty);
                    cmd.Parameters.AddWithValue("@ApproveFriends", ApproveFriends);
                    cmd.Parameters.AddWithValue("@AffiliateID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@ClassChanged", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                    cmd.Parameters.AddWithValue("@Facebook_Token", !string.IsNullOrWhiteSpace(Facebook_Token) ? Facebook_Token : string.Empty);
                    cmd.Parameters.AddWithValue("@Facebook_Id", !string.IsNullOrWhiteSpace(Facebook_Id) ? Facebook_Id : string.Empty);
                    cmd.Parameters.AddWithValue("@Facebook_User", !string.IsNullOrWhiteSpace(Facebook_User) ? Facebook_User : string.Empty);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@LinkedInID", LinkedInID);
                    cmd.Parameters.AddWithValue("@HideTips", HideTips);
                    cmd.Parameters.AddWithValue("@SiteID", SiteId > 0 ? Strings.ToString(SiteId) : string.Empty);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM NewslettersUsers WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }

                if (LetterIDs != null)
                {
                    var arrLetterIDs = Strings.Split(LetterIDs, ",");

                    if (arrLetterIDs != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrLetterIDs); i++)
                        {
                            using (var cmd = new SqlCommand("INSERT INTO NewslettersUsers (NUserID, LetterID, UserID, EmailAddress, PortalID, Status) VALUES (@NUserID, @LetterID, @UserID, @EmailAddress, @PortalID, '1')", conn))
                            {
                                cmd.Parameters.AddWithValue("@NUserID", SepFunctions.GetIdentity());
                                cmd.Parameters.AddWithValue("@LetterID", arrLetterIDs[i]);
                                cmd.Parameters.AddWithValue("@UserID", UserID);
                                cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                                cmd.Parameters.AddWithValue("@PortalID", PortalID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            var sReturn = string.Empty;

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("Member has been successfully updated.");
            }

            sReturn = SepFunctions.LangText("Member has been successfully added.");

            return sReturn;
        }

        /// <summary>
        /// Monthlies the signups.
        /// </summary>
        /// <returns>List&lt;Models.ChartData&gt;.</returns>
        public static List<ChartData> MonthlySignups()
        {
            var lData = new List<ChartData>();
            int iMonths = 12;

            long Total = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                // Populate series data
                for (var i = 0; i <= 11; i++)
                {
                    iMonths = iMonths - 1;
                    using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Month(CreateDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND Year(CreateDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            Total = Total + SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                        }
                    }
                }

                iMonths = 12;

                for (var i = 0; i <= 11; i++)
                {
                    iMonths = iMonths - 1;
                    using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Month(CreateDate) = '" + DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND Year(CreateDate) = '" + DateAndTime.Year(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -iMonths, DateTime.Now)) + "' AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            RS.Read();
                            var dData = new Models.ChartData { MonthName = DateAndTime.MonthName(DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -i, DateTime.Now))) };
                            dData.NumSignups = Math.Round(SepFunctions.toDouble(SepFunctions.openNull(RS["Counter"])), 0);
                            if (Total > 0)
                            {
                                dData.Percentage = SepFunctions.toDecimal(SepFunctions.openNull(RS["Counter"])) / Total * 100;
                            }
                            else
                            {
                                dData.Percentage = 0;
                            }

                            lData.Add(dData);
                        }
                    }
                }
            }

            return lData;
        }

        /// <summary>
        /// Noteses the get.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>Models.Members.</returns>
        public static Models.Members Notes_Get(string UserID)
        {
            var returnXML = new Models.Members();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserNotes FROM Members WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.UserID = UserID;
                            returnXML.UserNotes = SepFunctions.openNull(RS["UserNotes"]);
                        }
                    }
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Noteses the save.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Notes">The notes.</param>
        /// <returns>System.String.</returns>
        public static string Notes_Save(string UserID, string Notes)
        {
            var sUserNotes = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserNotes FROM Members WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sUserNotes = SepFunctions.openNull(RS["UserNotes"]);
                        }
                    }
                }

                using (var cmd = new SqlCommand("UPDATE Members SET UserNotes=@Notes WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Notes", "*** " + SepFunctions.LangText("Added by username:") + " " + SepFunctions.GetUserInformation("UserName", UserID) + " ***" + Environment.NewLine + "*** " + SepFunctions.LangText("Date added:") + " " + DateTime.Now + " ***" + Environment.NewLine + Notes + Environment.NewLine + Environment.NewLine + sUserNotes);
                    cmd.ExecuteNonQuery();
                }
            }

            var sReturn = string.Empty;

            sReturn = SepFunctions.LangText("Notes has been successfully saved.");

            return sReturn;
        }
    }
}