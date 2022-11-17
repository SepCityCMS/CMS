// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Functions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    //using SepCommon.Core.SepActivation;
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// True to enable debug mode, false to disable it.
        /// </summary>
        public static bool DebugMode = false;

        /// <summary>
        /// The points XML.
        /// </summary>
        private static string strPointsXml = string.Empty;

        /// <summary>
        /// The security XML.
        /// </summary>
        private static string strSecurityXML = string.Empty;

        /// <summary>
        /// The setup XML.
        /// </summary>
        private static string strSetupXml = string.Empty;

        /// <summary>
        /// Activity write.
        /// </summary>
        /// <param name="sActType">Type of the act.</param>
        /// <param name="sDescription">The description.</param>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="sUniqueID">(Optional) Unique identifier.</param>
        /// <param name="sUserID">(Optional) Identifier for the user.</param>
        /// <param name="sIPAddress">(Optional) The IP address.</param>
        public static void Activity_Write(string sActType, string sDescription, int iModuleID, string sUniqueID = "", string sUserID = "", string sIPAddress = "")
        {
            string[] arrVariable = null;
            string[] arrActType = null;
            long iGetPoints = 0;
            long iPostPoints = 0;
            SqlConnection ActConn = null;
            SqlCommand Actcmd = null;

            if (string.IsNullOrWhiteSpace(sUserID))
            {
                sUserID = Session_User_ID();
            }

            if (string.IsNullOrWhiteSpace(sIPAddress))
            {
                sIPAddress = GetUserIP();
            }

            sDescription += Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(Session_User_Name()))
            {
                sDescription += LangText("Written By: ~~" + Session_User_Name() + "~~") + Environment.NewLine;
            }

            sDescription += LangText("Date/Time: ~~" + DateTime.Today + "~~") + Environment.NewLine;

            if (toLong(sUniqueID) > 0)
            {
                using (ActConn = new SqlConnection(Database_Connection()))
                {
                    ActConn.Open();

                    using (Actcmd = new SqlCommand("INSERT INTO Activities (ActivityID, UserID, DatePosted, ActType, IPAddress, Description, ModuleID, UniqueID, Status) VALUES('" + GetIdentity() + "','" + FixWord(sUserID) + "',getDate(),'" + FixWord(sActType) + "','" + FixWord(sIPAddress) + "','" + FixWord(sDescription) + "','" + iModuleID + "','" + FixWord(sUniqueID) + "', '1')", ActConn))
                    {
                        Actcmd.ExecuteNonQuery();
                    }

                    arrVariable = Strings.Split(Pointing_Array(iModuleID, "Variable"), "||");
                    arrActType = Strings.Split(Pointing_Array(iModuleID, "ActType"), "||");

                    if (arrVariable != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrVariable); i++)
                        {
                            if (Strings.LCase(Strings.Trim(arrActType[i])) == Strings.LCase(Strings.Trim(sActType)))
                            {
                                iGetPoints = Points_Setup("Get" + arrVariable[i]);
                                iPostPoints = Points_Setup("Post" + arrVariable[i]);
                                break;
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(sUserID))
                    {
                        using (Actcmd = new SqlCommand("UPDATE Members SET UserPoints=UserPoints+" + iGetPoints + " WHERE UserID=@UserID", ActConn))
                        {
                            Actcmd.Parameters.AddWithValue("@UserID", sUserID);
                            Actcmd.ExecuteNonQuery();
                        }

                        using (Actcmd = new SqlCommand("UPDATE Members SET UserPoints=UserPoints-" + iPostPoints + " WHERE UserID=@UserID", ActConn))
                        {
                            Actcmd.Parameters.AddWithValue("@UserID", sUserID);
                            Actcmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Admin login required.
        /// </summary>
        /// <param name="ModuleKeys">The module keys.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool Admin_Login_Required(string ModuleKeys)
        {
            string[] RLoginArray = null;
            var DoLogin = true;

            RLoginArray = Strings.Split(ModuleKeys, ",");

            if (RLoginArray != null)
            {
                for (var i = 0; i <= Information.UBound(RLoginArray); i++)
                {
                    if (RLoginArray[i] == "1" || RLoginArray[i] == "|1|")
                    {
                        DoLogin = false;
                        break;
                    }
                }
            }

            if (DoLogin && string.IsNullOrWhiteSpace(Session_User_Name()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Admin menu height.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <returns>A string.</returns>
        public static string Admin_Menu_Height(string iModuleID)
        {
            if (iModuleID == "42")
            {
                return "110";
            }
            else
            {
                return "60";
            }
        }

        /// <summary>
        /// Affiliate join keys.
        /// </summary>
        /// <param name="sPrefix">(Optional) The prefix.</param>
        /// <returns>A string.</returns>
        public static string Affiliate_JoinKeys(string sPrefix = "")
        {
            var str = new StringBuilder();
            long aa = 0;
            var sJoinKeys = Security("AffiliateJoin");

            string[] arrAffiliate = null;

            if (!string.IsNullOrWhiteSpace(sJoinKeys))
            {
                arrAffiliate = Strings.Split(sJoinKeys, ",");

                str.Append(" AND (");
                if (arrAffiliate != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAffiliate); i++)
                    {
                        if (!string.IsNullOrWhiteSpace(arrAffiliate[i]))
                        {
                            aa = aa + 1;
                            if (aa > 1)
                            {
                                str.Append(" OR ");
                            }

                            str.Append(sPrefix + "AccessKeys LIKE '%" + arrAffiliate[i] + "%'");
                        }
                    }
                }

                str.Append(")");
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Blocked user.
        /// </summary>
        /// <param name="sUserName">Name of the user.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool blockedUser(string sUserName)
        {
            switch (Strings.LCase(sUserName))
            {
                case "admin":
                case "administrator":
                case "root":
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Cache remove.
        /// </summary>
        public static void Cache_Remove()
        {
            strPointsXml = string.Empty;
            strSecurityXML = string.Empty;
            strSetupXml = string.Empty;

            if (Directory.Exists(GetDirValue("App_Data") + "cache\\"))
            {
                try
                {
                    Directory.Delete(GetDirValue("App_Data") + "cache\\", true);
                    if (!string.IsNullOrWhiteSpace(Setup(989, "CloudFlareAPI")) && !string.IsNullOrWhiteSpace(Setup(989, "CloudFlareEmail")) && !string.IsNullOrWhiteSpace(Setup(989, "CloudFlareDomain")))
                    {
                        var sUrl = "https://www.cloudflare.com/api_json.html";
                        sUrl += "?a=fpurge_ts";
                        sUrl += "&tkn=" + UrlEncode(Setup(989, "CloudFlareAPI"));
                        sUrl += "&email=" + UrlEncode(Setup(989, "CloudFlareEmail"));
                        sUrl += "&z=" + UrlEncode(Setup(989, "CloudFlareDomain"));
                        sUrl += "&v=1";
                        Send_Get(sUrl);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Category SQL manage select.
        /// </summary>
        /// <param name="CatID">Identifier for the category.</param>
        /// <param name="wc">The wc.</param>
        /// <param name="forceCategory">(Optional) True to force category.</param>
        /// <returns>A string.</returns>
        public static string Category_SQL_Manage_Select(long CatID, string wc, bool forceCategory = false)
        {
            var strWC = string.Empty;

            if (Strings.Left(Strings.Trim(wc), 3) == "AND")
            {
                wc = Strings.Right(Strings.Trim(wc), Strings.Len(Strings.Trim(wc)) - 3);
            }

            if (CatID > 0 || forceCategory)
            {
                strWC = ",Categories AS CAT WHERE (Mod.CatID IN (SELECT CatID FROM CategoriesPortals WHERE (PortalID='" + Get_Portal_ID() + "' AND CAT.Sharing='0') OR ((PortalID='" + Get_Portal_ID() + "' OR PortalID = -1) AND CAT.Sharing='1') AND CatID=Mod.CatID))";
                strWC += " AND Mod.CatID=CAT.CatID ";
                if (!string.IsNullOrWhiteSpace(wc))
                {
                    strWC += " AND " + wc;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(wc))
                {
                    strWC = " WHERE " + wc;
                }
            }

            return strWC;
        }

        /// <summary>
        /// Check rating.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="intUniqueID">Unique identifier for the int.</param>
        /// <param name="sUserID">(Optional) Identifier for the user.</param>
        /// <param name="CheckVisitor">(Optional) True to check visitor.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool Check_Rating(int intModuleID, string intUniqueID, string sUserID = "", bool CheckVisitor = false)
        {
            if (string.IsNullOrWhiteSpace(sUserID))
            {
                sUserID = Session_User_ID();
            }

            if (string.IsNullOrWhiteSpace(sUserID))
            {
                if (Session.getSession("RATE" + intModuleID + intUniqueID) == "Yes")
                {
                    return true;
                }

                return false;
            }

            var sActType = "RATING";
            if (CheckVisitor)
            {
                sActType = "UVISITOR";
            }

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TOP 1 ActivityID FROM Activities WHERE ActType='" + sActType + "' AND ModuleID=@ModuleID AND UniqueID=@UniqueID AND IPAddress=@IPAddress AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", intModuleID);
                    cmd.Parameters.AddWithValue("@UniqueID", intUniqueID);
                    cmd.Parameters.AddWithValue("@IPAddress", GetUserIP());
                    cmd.Parameters.AddWithValue("@UserID", sUserID);
                    using (SqlDataReader UserRS = cmd.ExecuteReader())
                    {
                        if (UserRS.HasRows)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Check user points.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="sTake">The take.</param>
        /// <param name="sReceive">The receive.</param>
        /// <param name="UniqueID">Unique identifier.</param>
        /// <param name="DoNotUpdate">True to do not update.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool Check_User_Points(int iModuleID, string sTake, string sReceive, string UniqueID, bool DoNotUpdate)
        {
            var bReturn = true;
            long iPointsNeeded = 0;
            long iPointsReceive = 0;
            long iUserPoints = 0;

            iPointsNeeded = Points_Setup(sTake);
            iPointsReceive = Points_Setup(sReceive);

            if (iPointsReceive > 0)
            {
                if (!string.IsNullOrWhiteSpace(Session_User_ID()))
                {
                    if (toLong(GetUserInformation("AccessClass")) != 2)
                    {
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT UserPoints FROM Members WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session_User_ID());
                                using (SqlDataReader PointsRS = cmd.ExecuteReader())
                                {
                                    if (PointsRS.HasRows)
                                    {
                                        PointsRS.Read();
                                        iUserPoints = toLong(openNull(PointsRS["UserPoints"]));
                                    }
                                }
                            }

                            if (iPointsReceive > 0)
                            {
                                // -V3022
                                iUserPoints = iUserPoints + iPointsReceive;
                            }

                            if (DoNotUpdate == false)
                            {
                                if (Check_Rating(iModuleID + 100, UniqueID, Session_User_ID()) == false)
                                {
                                    using (var cmd = new SqlCommand("UPDATE Members SET UserPoints=@UserPoints WHERE UserID=@UserID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UserPoints", iUserPoints);
                                        cmd.Parameters.AddWithValue("@UserID", Session_User_ID());
                                        cmd.ExecuteNonQuery();
                                    }

                                    Write_Rating(iModuleID + 100, UniqueID, Session_User_ID());
                                }
                            }
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            if (iPointsNeeded > 0)
            {
                if (!string.IsNullOrWhiteSpace(Session_User_ID()))
                {
                    if (toLong(GetUserInformation("AccessClass")) != 2)
                    {
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT UserPoints FROM Members WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session_User_ID());
                                using (SqlDataReader PointsRS = cmd.ExecuteReader())
                                {
                                    if (PointsRS.HasRows)
                                    {
                                        PointsRS.Read();
                                        iUserPoints = toLong(openNull(PointsRS["UserPoints"]));
                                    }
                                }
                            }

                            if (iUserPoints >= iPointsNeeded)
                            {
                                iUserPoints = iUserPoints - iPointsNeeded;
                            }

                            if (DoNotUpdate == false)
                            {
                                if (Check_Rating(iModuleID + 200, UniqueID, Session_User_ID()) == false)
                                {
                                    using (var cmd = new SqlCommand("UPDATE Members SET UserPoints=@UserPoints WHERE UserID=@UserID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UserPoints", iUserPoints);
                                        cmd.Parameters.AddWithValue("@UserID", Session_User_ID());
                                        cmd.ExecuteNonQuery();
                                    }

                                    Write_Rating(iModuleID + 200, UniqueID, Session_User_ID());
                                }
                            }
                        }
                    }
                    else
                    {
                        return true;
                    }

                    if (iUserPoints < iPointsNeeded)
                    {
                        bReturn = false;
                    }
                }
            }

            return bReturn;
        }

        /// <summary>
        /// Determines if we can check pri site.
        /// </summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CheckPriSite()
        {
            var sStartClass = toLong(Setup(997, "StartupClass"));

            if (Get_Portal_ID() > 0)
            {
                if (toLong(PortalSetup("StartClass")) > 0)
                {
                    sStartClass = toLong(PortalSetup("StartClass"));
                }
            }

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ClassID FROM AccessClasses WHERE ClassID='" + FixWord(Strings.ToString(sStartClass)) + "' AND Privateclass='1'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Clean file name.
        /// </summary>
        /// <param name="sFile">The file.</param>
        /// <returns>A string.</returns>
        public static string CleanFileName(string sFile)
        {
            return Strings.Replace(Strings.Replace(sFile, "../", string.Empty), "..\\", string.Empty);
        }

        /// <summary>
        /// Compare keys.
        /// </summary>
        /// <param name="ModuleKeys">The module keys.</param>
        /// <param name="CheckPortals">(Optional) True to check portals.</param>
        /// <param name="userId">(Optional) Identifier for the user.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CompareKeys(string ModuleKeys, bool CheckPortals = true, string userId = "")
        {
            string[] ALoginArray = null;
            var K = 0;
            var L = 0;
            var strArr1 = string.Empty;
            var strArr2 = string.Empty;
            string[] Arr1 = null;
            string[] Arr2 = null;
            var GetAccessKeys = string.Empty;

            if ((string.IsNullOrWhiteSpace(userId) || userId == Session_User_ID()) && !string.IsNullOrWhiteSpace(Session_User_ID()))
            {
                if (string.IsNullOrWhiteSpace(Session.getSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "AccessKeys")))
                {
                    Session.setSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "AccessKeys", GetUserInformation("AccessKeys", userId));
                }

                GetAccessKeys = Session.getSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "AccessKeys");
            }
            else
            {
                GetAccessKeys = GetUserInformation("AccessKeys", userId);
            }

            if (CheckPortals && Get_Portal_ID() > 0)
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ManageKeys FROM Portals WHERE PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
                        using (SqlDataReader SecRS = cmd.ExecuteReader())
                        {
                            if (SecRS.HasRows)
                            {
                                SecRS.Read();
                                ModuleKeys += ",|" + openNull(SecRS["ManageKeys"]) + "|";
                            }
                        }
                    }
                }
            }

            ModuleKeys = Strings.Replace(ModuleKeys, "|", string.Empty);
            GetAccessKeys = Strings.Replace(GetAccessKeys, "|", string.Empty);

            if (!string.IsNullOrWhiteSpace(GetAccessKeys))
            {
                ALoginArray = Strings.Split(GetAccessKeys, ",");
                if (ALoginArray != null)
                {
                    for (K = 0; K <= Information.UBound(ALoginArray); K++)
                    {
                        if (toLong(ALoginArray[K]) == 2)
                        {
                            return true;
                        }
                    }
                }
            }

            strArr1 = ModuleKeys;
            strArr2 = GetAccessKeys;

            Arr1 = Strings.Split(strArr1, ",");
            Arr2 = Strings.Split(strArr2, ",");

            if (Arr1 != null)
            {
                for (K = 0; K <= Information.UBound(Arr1); K++)
                {
                    if (Arr2 != null)
                    {
                        for (L = 0; L <= Information.UBound(Arr2); L++)
                        {
                            if (toLong(Arr1[K]) == toLong(Arr2[L]) || toLong(Arr1[K]) == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Cookie return URL.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Cookie_ReturnUrl()
        {
            var returnCookie = Session.getCookie("returnUrl");
            if (!string.IsNullOrWhiteSpace(returnCookie))
            {
                return Strings.Replace(Strings.Replace(Strings.Replace(returnCookie, "signup.aspx", "default.aspx"), "DoAction=Logout", string.Empty), "LoginError=True&", string.Empty);
            }

            return string.Empty;
        }

        /// <summary>
        /// Database connection.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Database_Connection()
        {
            if (File.Exists(GetDirValue("app_data") + "system.xml"))
            {
                var doc = new XmlDocument();
                doc.Load(GetDirValue("app_data") + "system.xml");
                var root = doc.DocumentElement;
                return ParseXML("ConnectionString", root.InnerXml);
            }

            return string.Empty;
        }

        /// <summary>
        /// Draw calendar.
        /// </summary>
        /// <param name="intWidth">Width of the int.</param>
        /// <param name="selectedDate">The selected date.</param>
        /// <param name="EventType">(Optional) Type of the event.</param>
        /// <returns>A string.</returns>
        public static string Draw_Calendar(long intWidth, DateTime selectedDate, string EventType = "")
        {
            var str = new StringBuilder();

            var cell = new string[51];
            var bg = new string[51];

            var strCurDate = selectedDate;

            var Highlight = new string[50];

            DateTime daydone;
            double days = 0;
            var celldate = string.Empty;
            var stday = 0;

            if (!Information.IsDate(strCurDate))
            {
                strCurDate = DateTime.Today;
            }

            daydone = DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, strCurDate);
            days = DateAndTime.DateDiff(DateAndTime.DateInterval.Day, strCurDate, daydone);

            var startDate = new DateTime(strCurDate.Year, strCurDate.Month, 1);
            stday = (int)startDate.DayOfWeek;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT DISTINCT EventDate FROM EventCalendar WHERE (Month(EventDate)='" + DateAndTime.Month(strCurDate) + "' AND Year(EventDate)='" + DateAndTime.Year(strCurDate) + "' AND PortalID='" + Get_Portal_ID() + "' AND Status > '-1') AND (Shared='1' OR Shared='0' AND UserID='" + Session_User_ID() + "')", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            Highlight[DateAndTime.Day(toDate(openNull(RS["EventDate"])))] = "Yes";
                        }
                    }
                }
            }

            for (var i = 1; i <= 49; i++)
            {
                bg[i] = "EventBlank";
                cell[i] = "<td class=\"EventBlank\"></td>";
            }

            for (var i = 1; i <= days; i++)
            {
                celldate = Strings.ToString(Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, i - 1, DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -DateAndTime.Day(strCurDate) + 1, strCurDate)), Strings.DateNamedFormat.ShortDate));
                bg[i] = "EventBlank";
                if (celldate == Strings.ToString(Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate)))
                {
                    bg[i] = "EventToday";
                }
                else
                {
                    bg[i] = "EventDays";
                }

                if (Highlight[i] == "Yes")
                {
                    bg[i] = "EventOnDays";
                }

                cell[i + stday] = "<td class=\"" + bg[i] + "\" style=\"border:1px solid #cccccc;width: " + Strings.ToString(intWidth / 7) + "px; height: " + Strings.ToString(intWidth / 7) + "px; cursor:pointer;padding:5px;\" onclick=\"document.location.href='events.aspx?EventDate=" + UrlEncode(celldate) + "&EventType=" + UrlEncode(EventType) + "';\"><div class=\"caldiv\">" + i + "</div></td>";
            }

            str.Append("<table class=\"EventTable\" style=\"width:" + intWidth + "px;float:left;border:1px solid #cccccc;\">");
            str.Append("<tr>" + Environment.NewLine);
            str.Append("<td class=\"EventTitle\" style=\"text-align: left;padding-left:5px;\"><a href=\"events.aspx?EventDate=" + UrlEncode(Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -1, strCurDate))) + "&EventType=" + UrlEncode(EventType) + "\">" + Strings.Left(DateAndTime.MonthName(DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, -1, strCurDate))), 3) + "</a></td>");
            str.Append("<td class=\"EventTitle\" colspan=\"5\" style=\"text-align: center;\"><a href=\"events.aspx?EventDate=" + UrlEncode(Strings.ToString(strCurDate)) + "&EventType=" + UrlEncode(EventType) + "\">" + DateAndTime.MonthName(DateAndTime.Month(strCurDate)) + " " + DateAndTime.Year(strCurDate) + "</a></td>");
            str.Append("<td class=\"EventTitle\" style=\"text-align: right;padding-right:5px;\"><a href=\"events.aspx?EventDate=" + UrlEncode(Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, strCurDate))) + "&EventType=" + UrlEncode(EventType) + "\">" + Strings.Left(DateAndTime.MonthName(DateAndTime.Month(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, strCurDate))), 3) + "</a></td>");
            str.Append("</tr>" + Environment.NewLine);

            str.Append("<tr>");
            for (var i = 1; i <= 7; i++)
            {
                str.Append("<td style=\"text-align: center;width: " + Strings.ToString(intWidth / 7) + "px;\" class=\"EventDays\">" + Strings.ToString(intWidth > 210 ? DateAndTime.WeekdayName(i, true) : Strings.Left(DateAndTime.WeekdayName(i), 1)) + "</td>");
            }

            str.Append("</tr>");

            str.Append("<tr>" + cell[1] + cell[2] + cell[3] + cell[4] + cell[5] + cell[6] + cell[7] + "</tr>" + Environment.NewLine);
            str.Append("<tr>" + cell[8] + cell[9] + cell[10] + cell[11] + cell[12] + cell[13] + cell[14] + "</tr>" + Environment.NewLine);
            str.Append("<tr>" + cell[15] + cell[16] + cell[17] + cell[18] + cell[19] + cell[20] + cell[21] + "</tr>" + Environment.NewLine);
            str.Append("<tr>" + cell[22] + cell[23] + cell[24] + cell[25] + cell[26] + cell[27] + cell[28] + "</tr>" + Environment.NewLine);
            if (cell[29] != "<td class=\"EventBlank\"></td>")
            {
                str.Append("<tr>" + cell[29] + cell[30] + cell[31] + cell[32] + cell[33] + cell[34] + cell[35] + "</tr>" + Environment.NewLine);
            }

            if (cell[36] != "<td class=\"EventBlank\"></td>")
            {
                str.Append("<tr>" + cell[36] + cell[37] + cell[38] + cell[39] + cell[40] + cell[41] + cell[42] + "</tr>" + Environment.NewLine);
            }

            if (cell[43] != "<td class=\"EventBlank\"></td>")
            {
                str.Append("<tr>" + cell[43] + cell[44] + cell[45] + cell[46] + cell[47] + cell[48] + cell[49] + "</tr>" + Environment.NewLine);
            }

            str.Append("</table>");

            return Strings.ToString(str);
        }

        /// <summary>
        /// Filter bad words.
        /// </summary>
        /// <param name="sText">The text.</param>
        /// <returns>A string.</returns>
        public static string Filter_Bad_Words(string sText)
        {
            if (!string.IsNullOrWhiteSpace(Setup(992, "BadWordFilter")))
            {
                var AttachtoEnd = string.Empty;
                var arrWords = Strings.Split(Setup(992, "BadWordFilter"), ",");
                if (arrWords != null)
                {
                    for (var i = 0; i <= Information.UBound(arrWords); i++)
                    {
                        var eLength = arrWords[i].Length;
                        for (var x = 1; x <= eLength - 1; x++)
                        {
                            AttachtoEnd = AttachtoEnd + "*";
                        }

                        sText = Strings.Replace(sText, arrWords[i], arrWords[i].Substring(0, 1) + AttachtoEnd);
                        AttachtoEnd = string.Empty;
                    }
                }
            }

            return sText;
        }

        /// <summary>
        /// Fix word.
        /// </summary>
        /// <param name="StringWriteText">The string write text.</param>
        /// <returns>A string.</returns>
        public static string FixWord(string StringWriteText)
        {
            return Strings.Replace(StringWriteText, "'", "''");
        }

        /// <summary>
        /// Format currency.
        /// </summary>
        /// <param name="sNum">Number of.</param>
        /// <returns>The formatted currency.</returns>
        public static string Format_Currency(object sNum)
        {
            if (sNum == null)
            {
                return Strings.FormatCurrency("0");
            }

            var sNumOutput = toDecimal(Strings.ToString(sNum));
            return Strings.FormatCurrency(sNumOutput);
        }

        /// <summary>
        /// Format double.
        /// </summary>
        /// <param name="sDouble">The double.</param>
        /// <returns>The formatted double.</returns>
        public static double Format_Double(string sDouble)
        {
            return toDouble(Strings.FormatNumber(sDouble, 2));
        }

        /// <summary>
        /// Format isapi.
        /// </summary>
        /// <param name="sText">The text.</param>
        /// <returns>The formatted isapi.</returns>
        public static string Format_ISAPI(string sText)
        {
            return UrlEncode(Strings.Replace(ReplaceSpecial(Strings.Replace(Strings.Replace(Strings.Trim(sText), " & ", "_"), "&", "_")), " ", "_"));
        }

        /// <summary>
        /// Format long price.
        /// </summary>
        /// <param name="strUnitPrice">The unit price.</param>
        /// <param name="strRecurringPrice">The recurring price.</param>
        /// <param name="strRecurringCycle">The recurring cycle.</param>
        /// <returns>The formatted long price.</returns>
        public static string Format_Long_Price(decimal strUnitPrice, decimal strRecurringPrice, string strRecurringCycle)
        {
            var str = new StringBuilder();

            if (Math.Abs(strUnitPrice) > 0 && Math.Abs(strRecurringPrice) < 1)
            {
                str.Append(Format_Currency(strUnitPrice));
            }
            else if (Math.Abs(strUnitPrice) < 1 && Math.Abs(strRecurringPrice) > 0)
            {
                str.Append(Format_Currency(strRecurringPrice));
                if (strRecurringCycle == "1m")
                {
                    str.Append("/" + LangText("month"));
                }
                else if (strRecurringCycle == "3m")
                {
                    str.Append("/" + LangText("3 months"));
                }
                else if (strRecurringCycle == "6m")
                {
                    str.Append("/" + LangText("6 months"));
                }
                else
                {
                    str.Append("/" + LangText("year"));
                }
            }
            else
            {
                str.Append(Format_Currency(strUnitPrice));
                if (strRecurringPrice > 0)
                {
                    str.Append(" " + LangText("and") + " " + Format_Currency(strRecurringPrice) + string.Empty);
                    if (strRecurringCycle == "1m")
                    {
                        str.Append("/" + LangText("month"));
                    }
                    else if (strRecurringCycle == "3m")
                    {
                        str.Append("/" + LangText("3 months"));
                    }
                    else if (strRecurringCycle == "6m")
                    {
                        str.Append("/" + LangText("6 months"));
                    }
                    else
                    {
                        str.Append("/" + LangText("year"));
                    }
                }
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Format URL.
        /// </summary>
        /// <param name="strURL">STRURL of the resource.</param>
        /// <returns>The formatted URL.</returns>
        public static string Format_URL(string strURL)
        {
            if (Strings.Left(strURL, 7) == "http://" || Strings.Left(strURL, 8) == "https://")
            {
                return strURL;
            }

            return "http://" + strURL;
        }

        /// <summary>
        /// Format job type.
        /// </summary>
        /// <param name="jobType">Type of the job.</param>
        /// <returns>The formatted job type.</returns>
        public static string formatJobType(string jobType)
        {
            switch (jobType)
            {
                case "TempToPermanent":
                    return LangText("Temp To Permanent");

                case "FullTimeCoOp":
                    return LangText("Full Time CO-OP");

                case "PartTimeCoOp":
                    return LangText("Part Time CO-OP");

                case "FullTimeTemporary":
                    return LangText("Full Time (Temporary)");

                case "PartTimeRegular":
                    return LangText("Part Time");

                case "PartTimeTemporary":
                    return LangText("Part Time (Temporary)");

                case "ContractFullTime":
                    return LangText("Contractor (Full Time)");

                case "ContractPartTime":
                    return LangText("Contractor (Part Time)");

                case "FullTimeRegular":
                    return LangText("Full Time Regular");

                default:
                    return jobType;
            }
        }

        /// <summary>
        /// Format phone.
        /// </summary>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <returns>The formatted phone.</returns>
        public static string FormatPhone(string PhoneNumber)
        {
            if (Strings.Left(PhoneNumber, 2) == "+1")
            {
                return PhoneNumber;
            }

            PhoneNumber = Strings.Replace(Strings.Replace(Strings.Replace(Strings.Replace(PhoneNumber, " ", string.Empty), ")", string.Empty), "(", string.Empty), "-", string.Empty);
            if (Strings.Len(PhoneNumber) == 10)
            {
                PhoneNumber = "+1" + PhoneNumber;
            }

            if (Strings.Left(PhoneNumber, 1) != "+")
            {
                PhoneNumber = "+" + PhoneNumber;
            }

            return PhoneNumber;
        }

        /// <summary>
        /// Gets portal identifier.
        /// </summary>
        /// <returns>The portal identifier.</returns>
        public static long Get_Portal_ID()
        {
            long intReturn = 0;
            var LookupPortal = false;

            if (string.IsNullOrWhiteSpace(Session.getSession("PortalIDLoaded")))
            {
                LookupPortal = true;
            }
            else
            {
                intReturn = toLong(Session.getSession("PortalID"));
            }

            if (LookupPortal)
            {
                if (toLong(Request.Item("PortalID")) != 0)
                {
                    intReturn = toLong(Request.Item("PortalID"));
                }
                else
                {
                    if (isUserPage())
                    {
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT PortalID FROM UPagesSites WHERE UserID=@UserID AND Status <> -1", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", GetUserID(Request.Item("UserName")));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        intReturn = toLong(openNull(RS["PortalID"]));
                                    }
                                }
                            }
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(Request.Item("FriendlyPortalID")))
                    {
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE FriendlyName='" + FixWord(Request.Item("FriendlyPortalID")) + "'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        intReturn = toLong(openNull(RS["PortalID"]));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Request.ServerVariables("SERVER_NAME")))
                        {
                            using (var conn = new SqlConnection(Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE DomainName='" + FixWord(Strings.Replace(Request.ServerVariables("SERVER_NAME"), "www.", string.Empty)) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            intReturn = toLong(openNull(RS["PortalID"]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return intReturn;
        }

        /// <summary>
        /// Gets content type.
        /// </summary>
        /// <param name="FileExtension">The file extension.</param>
        /// <returns>The content type.</returns>
        public static string GetContentType(string FileExtension)
        {
            var d = new Dictionary<string, string>();

            // Images'
            d.Add(".bmp", "image/bmp");
            d.Add(".gif", "image/gif");
            d.Add(".jpeg", "image/jpeg");
            d.Add(".jpg", "image/jpeg");
            d.Add(".png", "image/png");
            d.Add(".tif", "image/tiff");
            d.Add(".tiff", "image/tiff");

            // Documents'
            d.Add(".doc", "application/msword");
            d.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            d.Add(".pdf", "application/pdf");
            d.Add(".rtf", "application/rtf");
            d.Add(".htm", "text/html");
            d.Add(".html", "text/html");

            // Slideshows'
            d.Add(".ppt", "application/vnd.ms-powerpoint");
            d.Add(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");

            // Data'
            d.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            d.Add(".xls", "application/vnd.ms-excel");
            d.Add(".csv", "text/csv");
            d.Add(".xml", "text/xml");
            d.Add(".txt", "text/plain");

            // Compressed Folders'
            d.Add(".zip", "application/zip");

            // Audio'
            d.Add(".ogg", "application/ogg");
            d.Add(".mp3", "audio/mpeg");
            d.Add(".wma", "audio/x-ms-wma");
            d.Add(".wav", "audio/x-wav");

            // Video'
            d.Add(".wmv", "audio/x-ms-wmv");
            d.Add(".swf", "application/x-shockwave-flash");
            d.Add(".avi", "video/avi");
            d.Add(".mp4", "video/mp4");
            d.Add(".mpeg", "video/mpeg");
            d.Add(".mpg", "video/mpeg");
            d.Add(".qt", "video/quicktime");
            return d[FileExtension];
        }

        /// <summary>
        /// Gets dir value.
        /// </summary>
        /// <param name="strValue">The value.</param>
        /// <param name="strUserValue">(Optional) True to user value.</param>
        /// <returns>The dir value.</returns>
        public static string GetDirValue(string strValue, bool strUserValue = false)
        {
            if (strUserValue)
            {
                var sInstallFolder = GetInstallFolder(true);
                return sInstallFolder + strValue + "/";
            }

            return HostingEnvironment.MapPath("~/" + strValue + "\\");
        }

        /// <summary>
        /// Gets domain name.
        /// </summary>
        /// <returns>The domain name.</returns>
        public static string GetDomainName()
        {
            try
            {
                return Request.Url.Host();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets encryption key.
        /// </summary>
        /// <returns>The encryption key.</returns>
        public static string GetEncryptionKey()
        {
            var doc = new XmlDocument();
            doc.Load(GetDirValue("app_data") + "system.xml");
            var root = doc.DocumentElement;
            var sXML = root.InnerXml;
            if (!string.IsNullOrWhiteSpace(ParseXML("EncryptionKey", sXML)))
            {
                return ParseXML("EncryptionKey", sXML);
            }

            var key = Strings.Left(Generate_GUID().Replace("-", string.Empty), 24);
            var node = doc.SelectSingleNode("//ROOTLEVEL/EncryptionKey");
            node.InnerText = key;
            node = doc.SelectSingleNode("//ROOTLEVEL/MailServerPass");
            node.InnerText = Encrypt(node.InnerText, key);
            doc.Save(GetDirValue("app_data") + "system.xml");

            doc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return key;
        }

        /// <summary>
        /// Gets install folder.
        /// </summary>
        /// <param name="excludePortal">(Optional) True to exclude, false to include the portal.</param>
        /// <returns>The install folder.</returns>
        public static string GetInstallFolder(bool excludePortal = false)
        {
            if (excludePortal == false && Get_Portal_ID() > 0)
            {
                if (!string.IsNullOrWhiteSpace(Session.getSession("FriendlyPortalID")))
                {
                    return Strings.ToString(Strings.Len(HttpRuntime.AppDomainAppVirtualPath()) > 1 ? HttpRuntime.AppDomainAppVirtualPath() : isUserPage() && !string.IsNullOrWhiteSpace(Request.Item("UserName")) ? "/!" + Request.Item("UserName") : "/" + "go/" + Session.getSession("FriendlyPortalID")) + "/";
                }

                return Strings.ToString(Strings.Len(HttpRuntime.AppDomainAppVirtualPath()) > 1 ? HttpRuntime.AppDomainAppVirtualPath() : isUserPage() && !string.IsNullOrWhiteSpace(Request.Item("UserName")) ? "/!" + Request.Item("UserName") : "/" + "portals/" + Get_Portal_ID()) + "/";
            }

            return Strings.ToString(Strings.Len(HttpRuntime.AppDomainAppVirtualPath()) > 1 ? HttpRuntime.AppDomainAppVirtualPath() : excludePortal == false && isUserPage() && !string.IsNullOrWhiteSpace(Request.Item("UserName")) ? "/!" + Request.Item("UserName") : string.Empty) + "/";
        }

        /// <summary>
        /// Gets master domain.
        /// </summary>
        /// <param name="checkPortalDomain">True to check portal domain.</param>
        /// <returns>The master domain.</returns>
        public static string GetMasterDomain(bool checkPortalDomain)
        {
            var sURL = string.Empty;

            if (Setup(60, "PortalsEnable") == "Enable" && Get_Portal_ID() > 0)
            {
                if (checkPortalDomain)
                {
                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT DomainName FROM Portals WHERE PortalID=@PortalID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
                            using (SqlDataReader PortalRS = cmd.ExecuteReader())
                            {
                                if (PortalRS.HasRows)
                                {
                                    PortalRS.Read();
                                    if (!string.IsNullOrWhiteSpace(openNull(PortalRS["DomainName"])))
                                    {
                                        sURL = "http://" + openNull(PortalRS["DomainName"]);
                                    }
                                    else
                                    {
                                        sURL = "http://" + Setup(60, "PortalMasterDomain");
                                    }
                                }
                                else
                                {
                                    sURL = "http://" + Setup(60, "PortalMasterDomain");
                                }
                            }
                        }
                    }
                }
                else
                {
                    sURL = "http://" + GetDomainName();
                }
            }
            else
            {
                sURL = "http://" + GetDomainName();
            }

            if (sURL == "http://")
            {
                sURL = "http://" + GetDomainName();
            }

            switch (Request.Url.Port())
            {
                case 80:
                    break;

                // Do Nothing
                case 443:
                    sURL = Strings.Replace(sURL, "http://", "https://");
                    break;

                default:
                    sURL += ":" + Request.Url.Port();
                    break;
            }

            if (checkPortalDomain == false)
            {
                sURL += GetInstallFolder(true);
            }
            else
            {
                sURL += GetInstallFolder();
            }

            return sURL;
        }

        /// <summary>
        /// Gets master page.
        /// </summary>
        /// <returns>The master page.</returns>
        public static string GetMasterPage()
        {
            var sTemplate = "~/skins/template.master";

            if (Request.Item("DoAction") != "Print")
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    if (isUserPage())
                    {
                        using (var cmd = new SqlCommand("SELECT SiteTemplates.FolderName FROM UPagesSites,SiteTemplates WHERE UPagesSites.TemplateID=SiteTemplates.TemplateID AND UPagesSites.UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", GetUserID(Request.Item("UserName")));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (File.Exists(GetDirValue("skins") + openNull(RS["FolderName"]) + "\\custom0.master"))
                                    {
                                        sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/custom0.master";
                                    }
                                    else
                                    {
                                        sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/template.master";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Get_Portal_ID() == 0)
                        {
                            using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE useTemplate='1'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        if (File.Exists(GetDirValue("skins") + openNull(RS["FolderName"]) + "\\custom0.master"))
                                        {
                                            sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/custom0.master";
                                        }
                                        else
                                        {
                                            sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/template.master";
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                            {
                                cmd.Parameters.AddWithValue("@TemplateID", PortalSetup("WEBSITELAYOUT", Get_Portal_ID()));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        if (File.Exists(GetDirValue("skins") + openNull(RS["FolderName"]) + "\\custom" + Get_Portal_ID() + ".master"))
                                        {
                                            sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/custom" + Get_Portal_ID() + ".master";
                                        }
                                        else if (File.Exists(GetDirValue("skins") + openNull(RS["FolderName"]) + "\\custom.master"))
                                        {
                                            sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/custom.master";
                                        }
                                        else
                                        {
                                            sTemplate = "~/skins/" + openNull(RS["FolderName"]) + "/template.master";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!File.Exists(GetDirValue("skins") + Strings.Replace(Strings.Replace(Strings.Replace(sTemplate, "/skins/", "/"), "/", "\\"), "~", string.Empty)))
            {
                sTemplate = "~/skins/template.master";
            }

            return sTemplate;
        }

        /// <summary>
        /// Gets module name.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <returns>The module name.</returns>
        public static string GetModuleName(int intModuleID)
        {
            var str = string.Empty;

            switch (intModuleID)
            {
                case 1:
                    str = "Content Rotator";
                    break;

                case 2:
                    str = "Advertising";
                    break;

                case 3:
                    str = "Search Engine";
                    break;

                case 4:
                    str = "Contact Us";
                    break;

                case 5:
                    str = "Discount System";
                    break;

                case 6:
                    str = "Instant Messenger";
                    break;

                case 7:
                    str = "User Pages";
                    break;

                case 8:
                    str = "Comments";
                    break;

                case 9:
                    str = "FAQ's";
                    break;

                case 10:
                    str = "Downloads";
                    break;

                case 11:
                    str = "Forgot Password";
                    break;

                case 12:
                    str = "Forums";
                    break;

                case 13:
                    str = "Forms";
                    break;

                case 14:
                    str = "Guestbook";
                    break;

                case 15:
                    str = "Stocks";
                    break;

                case 16:
                    str = "Home Page";
                    break;

                case 17:
                    str = "Messenger";
                    break;

                case 18:
                    str = "Match Maker";
                    break;

                case 19:
                    str = "Link Directory";
                    break;

                case 20:
                    str = "Business Directory";
                    break;

                case 21:
                    str = "Login";
                    break;

                case 22:
                    str = "Twilio Panel";
                    break;

                case 23:
                    str = "News";
                    break;

                case 24:
                    str = "Newsletters";
                    break;

                case 25:
                    str = "Polls";
                    break;

                case 26:
                    str = "Website Statistics";
                    break;

                case 27:
                    str = "Live Support";
                    break;

                case 28:
                    str = "Photo Albums";
                    break;

                case 29:
                    str = "Signup";
                    break;

                case 30:
                    str = "Sitemap";
                    break;

                case 31:
                    str = "Auctions";
                    break;

                case 32:
                    str = "Real Estate";
                    break;

                case 33:
                    str = "Account";
                    break;

                case 34:
                    str = "Who's Online";
                    break;

                case 35:
                    str = "Articles";
                    break;

                case 37:
                    str = "E-learning";
                    break;

                case 38:
                    str = "Memberships";
                    break;

                case 39:
                    str = "Affiliate Program";
                    break;

                case 40:
                    str = "Hot or Not";
                    break;

                case 41:
                    str = "Shopping Mall";
                    break;

                case 42:
                    str = "Chat Rooms";
                    break;

                case 43:
                    str = "Refer a Friend";
                    break;

                case 44:
                    str = "Classified Ads";
                    break;

                case 46:
                    str = "Event Calendar";
                    break;

                case 47:
                    str = "Online Games";
                    break;

                case 50:
                    str = "Speakers Bureau";
                    break;

                case 55:
                    str = "Weather Forecast";
                    break;

                case 56:
                    str = "International News";
                    break;

                case 57:
                    str = "Horoscopes";
                    break;

                case 58:
                    str = "PCRecruiter";
                    break;

                case 60:
                    str = "Portals";
                    break;

                case 61:
                    str = "Blogs";
                    break;

                case 62:
                    str = "My Feeds";
                    break;

                case 63:
                    str = "User Profiles";
                    break;

                case 65:
                    str = "Vouchers";
                    break;

                case 69:
                    str = "Video Conference";
                    break;

                case 985:
                    str = "Error on Page";
                    break;

                case 986:
                    str = "Report Listing";
                    break;

                case 987:
                    str = "Refer a Friend";
                    break;

                case 988:
                    str = "User Information";
                    break;

                case 989:
                    str = "Access Class";
                    break;

                case 990:
                    str = "Group Lists";
                    break;

                case 991:
                    str = "Administrator Information";
                    break;

                case 992:
                    str = "Website Setup";
                    break;

                case 993:
                    str = "Website Layout";
                    break;

                case 994:
                    str = "Account Management";
                    break;

                case 995:
                    str = "Shopping Cart";
                    break;

                case 996:
                    str = "Administration Console";
                    break;

                case 997:
                    str = "Signup Setup";
                    break;

                case 998:
                    str = "Activities";
                    break;

                case 999:
                    str = "Banned IP";
                    break;
            }

            return str;
        }

        /// <summary>
        /// Gets module page names.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <returns>The module page names.</returns>
        public static string GetModulePageNames(int intModuleID)
        {
            var str = string.Empty;

            switch (intModuleID)
            {
                case 5:
                    str = "discounts.aspx";
                    break;

                case 9:
                    str = "faq.aspx";
                    break;

                case 10:
                    str = "downloads.aspx";
                    break;

                case 12:
                    str = "forums.aspx";
                    break;

                case 19:
                    str = "links.aspx";
                    break;

                case 20:
                    str = "businesses.aspx";
                    break;

                case 31:
                    str = "auction.aspx";
                    break;

                case 35:
                    str = "articles.aspx";
                    break;

                case 37:
                    str = "elearning.aspx";
                    break;

                case 41:
                    str = "shopmall.aspx";
                    break;

                case 44:
                    str = "classifieds.aspx";
                    break;

                case 65:
                    str = "vouchers.aspx";
                    break;
            }

            return str;
        }

        /// <summary>
        /// Gets page name.
        /// </summary>
        /// <returns>The page name.</returns>
        public static string GetPageName()
        {
            return Path.GetFileName(Request.Url.AbsolutePath());
        }

        /// <summary>
        /// Gets site domain.
        /// </summary>
        /// <returns>The site domain.</returns>
        public static string GetSiteDomain()
        {
            var sPort = string.Empty;
            var sSuffix = "http://";

            switch (Request.Url.Port())
            {
                case 80:
                    break;

                // Do Nothing
                case 443:
                    sSuffix = "https://";
                    break;

                default:
                    sPort = ":" + Request.Url.Port();
                    break;
            }

            if (Get_Portal_ID() > 0)
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT DomainName FROM Portals AS P WHERE P.PortalID=" + Get_Portal_ID() + string.Empty, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (!string.IsNullOrWhiteSpace(openNull(RS["DomainName"])))
                                {
                                    return sSuffix + openNull(RS["DomainName"]) + sPort + "/";
                                }

                                return sSuffix + Request.ServerVariables("SERVER_NAME") + sPort + "/portals/" + Get_Portal_ID() + "/";
                            }
                        }

                        return sSuffix + Request.ServerVariables("SERVER_NAME") + sPort + "/portals/" + Get_Portal_ID() + "/";
                    }
                }
            }

            return sSuffix + Request.ServerVariables("SERVER_NAME") + sPort + "/";
        }

        /// <summary>
        /// Gets site language.
        /// </summary>
        /// <returns>The site language.</returns>
        public static string GetSiteLanguage()
        {
            var str = string.Empty;

            if (!string.IsNullOrWhiteSpace(Session.getSession("UserLang")))
            {
                str = Strings.UCase(Session.getSession("UserLang"));
            }
            else
            {
                if (Get_Portal_ID() == 0)
                {
                    str = Strings.UCase(Setup(992, "SiteLang"));
                }
                else
                {
                    str = Strings.UCase(PortalSetup("SITELANG"));
                }
            }

            if (str != "EN-US" && str != "PT-BR" && str != "ES-MX" && str != "ES-ES" && str != "FR-CA" && str != "FR-FR" && str != "NL-NL")
            {
                str = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(str))
            {
                return "EN-US";
            }

            return str;
        }

        /// <summary>
        /// Gets template folder.
        /// </summary>
        /// <returns>The template folder.</returns>
        public static string getTemplateFolder()
        {
            var sFolderName = string.Empty;

            if (string.IsNullOrWhiteSpace(Session.getSession("TemplateFolderLoaded")))
            {
                if (Request.Item("DoAction") != "Print")
                {
                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        if (isUserPage())
                        {
                            using (var cmd = new SqlCommand("SELECT SiteTemplates.FolderName FROM UPagesSites,SiteTemplates WHERE UPagesSites.TemplateID=SiteTemplates.TemplateID AND UPagesSites.UserID=@UserID", conn))
                            {
                                var sUserID = GetUserID(Request.Item("UserName"));
                                if (string.IsNullOrWhiteSpace(sUserID))
                                {
                                    cmd.Parameters.AddWithValue("@UserID", string.Empty);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@UserID", sUserID);
                                }

                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        sFolderName = openNull(RS["FolderName"]);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (Get_Portal_ID() == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE useTemplate='1'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            sFolderName = openNull(RS["FolderName"]);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@TemplateID", PortalSetup("WEBSITELAYOUT", Get_Portal_ID()));
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            sFolderName = openNull(RS["FolderName"]);
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
                sFolderName = Session.getSession("TemplateFolder");
            }

            return sFolderName;
        }

        /// <summary>
        /// Gets user country.
        /// </summary>
        /// <returns>The user country.</returns>
        public static string GetUserCountry()
        {
            if (string.IsNullOrWhiteSpace(Session_User_ID()))
            {
                var sCity = string.Empty;
                var sState = string.Empty;
                var sCountry = string.Empty;
                IP2Location(GetUserIP(), ref sCity, ref sState, ref sCountry);
                return sCountry;
            }

            return GetUserInformation("Country");
        }

        /// <summary>
        /// Gets user identifier.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <returns>The user identifier.</returns>
        public static string GetUserID(string UserName)
        {
            var sReturn = string.Empty;

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserName=@UserName AND Status=1", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sReturn = openNull(RS["UserID"]);
                            }

                        }
                    }
                }
            }

            return sReturn;
        }

        /// <summary>
        /// Gets user information.
        /// </summary>
        /// <param name="OptionValue">The option value.</param>
        /// <param name="strUserID">(Optional) Identifier for the user.</param>
        /// <returns>The user information.</returns>
        public static string GetUserInformation(string OptionValue, string strUserID = "")
        {
            var sReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(strUserID))
            {
                strUserID = Session_User_ID();
            }

            if (OptionValue == "AccessKeys" && string.IsNullOrWhiteSpace(strUserID))
            {
                return "1";
            }

            if (string.IsNullOrWhiteSpace(strUserID))
            {
                return string.Empty;
            }

            SqlConnection Userconn = null;
            SqlCommand Usercmd = null;
            SqlDataAdapter UserDA = null;
            var UserDS = new DataSet();

            switch (OptionValue)
            {
                case "Age":
                    using (Userconn = new SqlConnection(Database_Connection()))
                    {
                        using (Usercmd = new SqlCommand("SELECT BirthDate FROM Members WHERE UserID=@UserID", Userconn))
                        {
                            Usercmd.Parameters.AddWithValue("@UserID", strUserID);
                            Usercmd.CommandType = CommandType.Text;
                            Usercmd.Connection.Open();
                            UserDA = new SqlDataAdapter(Usercmd);
                            UserDA.Fill(UserDS);
                            if (UserDS.Tables[0].Rows.Count > 0)
                            {
                                if (Information.IsDate(openNull(UserDS.Tables[0].Rows[0]["BirthDate"])))
                                {
                                    sReturn = Strings.ToString(DateAndTime.DateDiff(DateAndTime.DateInterval.Year, Convert.ToDateTime(openNull(UserDS.Tables[0].Rows[0]["BirthDate"])), DateTime.Today));
                                }
                                else
                                {
                                    sReturn = "N/A";
                                }
                            }

                            UserDS.Dispose();
                            UserDA.Dispose();
                        }
                    }

                    break;

                case "Sex":
                    using (Userconn = new SqlConnection(Database_Connection()))
                    {
                        using (Usercmd = new SqlCommand("SELECT Male FROM Members WHERE UserID=@UserID", Userconn))
                        {
                            Usercmd.Parameters.AddWithValue("@UserID", strUserID);
                            Usercmd.CommandType = CommandType.Text;
                            Usercmd.Connection.Open();
                            UserDA = new SqlDataAdapter(Usercmd);
                            UserDA.Fill(UserDS);
                            if (UserDS.Tables[0].Rows.Count > 0)
                            {
                                if (openBoolean(UserDS.Tables[0].Rows[0]["Male"]))
                                {
                                    sReturn = LangText("Male");
                                }
                                else
                                {
                                    sReturn = LangText("Female");
                                }
                            }

                            UserDS.Dispose();
                            UserDA.Dispose();
                        }
                    }

                    break;

                case "AccessKeys":
                    using (Userconn = new SqlConnection(Database_Connection()))
                    {
                        using (Usercmd = new SqlCommand("SELECT AccessKeys FROM Members WHERE UserID=@UserID AND Status=1", Userconn))
                        {
                            Usercmd.Parameters.AddWithValue("@UserID", strUserID);
                            Usercmd.CommandType = CommandType.Text;
                            Usercmd.Connection.Open();
                            UserDA = new SqlDataAdapter(Usercmd);
                            UserDA.Fill(UserDS);
                            if (UserDS.Tables[0].Rows.Count == 0)
                            {
                                sReturn = "1";
                            }
                            else
                            {
                                sReturn = openNull(UserDS.Tables[0].Rows[0]["AccessKeys"]);
                            }

                            UserDS.Dispose();
                            UserDA.Dispose();
                        }
                    }

                    break;

                default:
                    using (Userconn = new SqlConnection(Database_Connection()))
                    {
                        using (Usercmd = new SqlCommand("SELECT " + OptionValue + " FROM Members WHERE UserID=@UserID AND Status <> -1", Userconn))
                        {
                            Usercmd.Parameters.AddWithValue("@UserID", strUserID);
                            Usercmd.CommandType = CommandType.Text;
                            Usercmd.Connection.Open();
                            UserDA = new SqlDataAdapter(Usercmd);
                            UserDA.Fill(UserDS);
                            if (UserDS.Tables[0].Rows.Count > 0)
                            {
                                if (OptionValue == "BirthDate")
                                {
                                    sReturn = Strings.ToString(toDate(openNull(UserDS.Tables[0].Rows[0][OptionValue])));
                                }
                                else
                                {
                                    sReturn = openNull(UserDS.Tables[0].Rows[0][OptionValue]);
                                }
                            }

                            UserDS.Dispose();
                            UserDA.Dispose();
                        }
                    }

                    break;
            }

            return sReturn;
        }

        /// <summary>
        /// Gets user IP.
        /// </summary>
        /// <returns>The user IP.</returns>
        public static string GetUserIP()
        {
            var ipAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR");

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return Request.ServerVariables("REMOTE_ADDR");
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns>The version.</returns>
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor;
        }

        /// <summary>
        /// Inserts an image.
        /// </summary>
        /// <param name="sURL">URL of the resource.</param>
        /// <param name="sAlt">The alternate.</param>
        /// <param name="sAlign">(Optional) The align.</param>
        /// <param name="sClass">(Optional) The class.</param>
        /// <param name="bLangText">(Optional) True to language text.</param>
        /// <param name="sID">(Optional) The identifier.</param>
        /// <param name="sAdditional">(Optional) The additional.</param>
        /// <returns>A string.</returns>
        public static string insImage(string sURL, string sAlt, string sAlign = "", string sClass = "", bool bLangText = true, string sID = "", string sAdditional = "")
        {
            if (bLangText)
            {
                sAlt = LangText(sAlt);
            }

            return "<img src=\"" + sURL + "\" alt=\"" + sAlt + "\"" + Strings.ToString(!string.IsNullOrWhiteSpace(sAlt) ? " title=\"" + sAlt + "\"" : string.Empty) + Strings.ToString(!string.IsNullOrWhiteSpace(sAlign) && sAlign != "absmiddle" ? " align=\"" + sAlign + "\"" : string.Empty) + Strings.ToString(!string.IsNullOrWhiteSpace(sClass) ? " class=\"" + sClass + "\"" : string.Empty) + Strings.ToString(!string.IsNullOrWhiteSpace(sID) ? " id=\"" + sID + "\"" : string.Empty) + " border=\"0\"" + Strings.ToString(sAlign == "absmiddle" ? " style=\"veritical-align:middle;\"" : string.Empty) + sAdditional + "/>";
        }

        /// <summary>
        /// Query if this object is hosted.
        /// </summary>
        /// <returns>True if hosted, false if not.</returns>
        public static bool isHosted()
        {
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in ipHostInfo.AddressList)
            {
                if (Strings.Left(Strings.ToString(item), 12) == "198.205.120.")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Query if this object is professional edition.
        /// </summary>
        /// <returns>True if professional edition, false if not.</returns>
        public static bool isProfessionalEdition()
        {
            // Approval Chains Import Affiliate Program Customize Classes/Keys Pointing System Custom Reports
            using (var readLicense = new StreamReader(GetDirValue("app_data") + "license.xml"))
            {
                if (ParseXML("Version", readLicense.ReadToEnd()) == "Enterprise")
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Query if this object is user page.
        /// </summary>
        /// <returns>True if user page, false if not.</returns>
        public static bool isUserPage()
        {
            var currentURL = Strings.LCase(Request.Path());

            if ((Strings.InStr(currentURL, "userpages_site_view.aspx") > 0 || Strings.InStr(Request.Item("UserPage"), "Yes") > 0) && !string.IsNullOrWhiteSpace(GetUserID(Request.Item("UserName"))))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Language text.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <param name="bAdd">(Optional) True to add.</param>
        /// <returns>A string.</returns>
        public static string LangText(string strText, bool bAdd = true)
        {
            var GetLangID = string.Empty;
            var posa = 0;
            var posb = 0;
            var strExcluded = string.Empty;
            var strLangText = strText;

            var siteLang = GetSiteLanguage();

            if (Strings.InStr(strLangText, "~~") > 0)
            {
                posa = Strings.InStr(strLangText, "~~") - 1;
                posb = Strings.InStr(Strings.Right(strLangText, Strings.Len(strLangText) - posa - 2), "~~") - 1;
                strExcluded = Strings.Mid(strLangText, posa + 3, posb);
                strLangText = Strings.Replace(strLangText, strExcluded, "#");
            }

            if (DebugMode == false && (Strings.UCase(siteLang) == "EN-US" || string.IsNullOrWhiteSpace(Strings.UCase(siteLang))))
            {
                return Strings.Replace(strText, "~~", string.Empty);
            }

            GetLangID = Strings.Replace(strLangText, "A", "01");
            GetLangID = Strings.Replace(GetLangID, "B", "02");
            GetLangID = Strings.Replace(GetLangID, "C", "03");
            GetLangID = Strings.Replace(GetLangID, "D", "04");
            GetLangID = Strings.Replace(GetLangID, "E", "05");
            GetLangID = Strings.Replace(GetLangID, "F", "06");
            GetLangID = Strings.Replace(GetLangID, "G", "07");
            GetLangID = Strings.Replace(GetLangID, "H", "08");
            GetLangID = Strings.Replace(GetLangID, "I", "09");
            GetLangID = Strings.Replace(GetLangID, "J", "10");
            GetLangID = Strings.Replace(GetLangID, "K", "11");
            GetLangID = Strings.Replace(GetLangID, "L", "12");
            GetLangID = Strings.Replace(GetLangID, "M", "13");
            GetLangID = Strings.Replace(GetLangID, "N", "14");
            GetLangID = Strings.Replace(GetLangID, "O", "15");
            GetLangID = Strings.Replace(GetLangID, "P", "16");
            GetLangID = Strings.Replace(GetLangID, "Q", "17");
            GetLangID = Strings.Replace(GetLangID, "R", "18");
            GetLangID = Strings.Replace(GetLangID, "S", "19");
            GetLangID = Strings.Replace(GetLangID, "T", "20");
            GetLangID = Strings.Replace(GetLangID, "U", "21");
            GetLangID = Strings.Replace(GetLangID, "V", "22");
            GetLangID = Strings.Replace(GetLangID, "W", "23");
            GetLangID = Strings.Replace(GetLangID, "X", "24");
            GetLangID = Strings.Replace(GetLangID, "Y", "25");
            GetLangID = Strings.Replace(GetLangID, "Z", "26");
            GetLangID = Strings.Replace(GetLangID, "a", "27");
            GetLangID = Strings.Replace(GetLangID, "b", "28");
            GetLangID = Strings.Replace(GetLangID, "c", "29");
            GetLangID = Strings.Replace(GetLangID, "d", "30");
            GetLangID = Strings.Replace(GetLangID, "e", "31");
            GetLangID = Strings.Replace(GetLangID, "f", "32");
            GetLangID = Strings.Replace(GetLangID, "g", "33");
            GetLangID = Strings.Replace(GetLangID, "h", "34");
            GetLangID = Strings.Replace(GetLangID, "i", "35");
            GetLangID = Strings.Replace(GetLangID, "j", "36");
            GetLangID = Strings.Replace(GetLangID, "k", "37");
            GetLangID = Strings.Replace(GetLangID, "l", "38");
            GetLangID = Strings.Replace(GetLangID, "m", "39");
            GetLangID = Strings.Replace(GetLangID, "n", "40");
            GetLangID = Strings.Replace(GetLangID, "o", "41");
            GetLangID = Strings.Replace(GetLangID, "p", "42");
            GetLangID = Strings.Replace(GetLangID, "q", "43");
            GetLangID = Strings.Replace(GetLangID, "r", "44");
            GetLangID = Strings.Replace(GetLangID, "s", "45");
            GetLangID = Strings.Replace(GetLangID, "t", "46");
            GetLangID = Strings.Replace(GetLangID, "u", "47");
            GetLangID = Strings.Replace(GetLangID, "v", "48");
            GetLangID = Strings.Replace(GetLangID, "w", "49");
            GetLangID = Strings.Replace(GetLangID, "x", "50");
            GetLangID = Strings.Replace(GetLangID, "y", "51");
            GetLangID = Strings.Replace(GetLangID, "z", "52");
            GetLangID = Strings.Replace(GetLangID, " ", "53");
            GetLangID = Strings.Replace(GetLangID, "'", "54");
            GetLangID = Strings.Replace(GetLangID, "~", "55");
            GetLangID = Strings.Replace(GetLangID, "`", "56");
            GetLangID = Strings.Replace(GetLangID, "!", "57");
            GetLangID = Strings.Replace(GetLangID, "@", "58");
            GetLangID = Strings.Replace(GetLangID, "#", "59");
            GetLangID = Strings.Replace(GetLangID, "$", "60");
            GetLangID = Strings.Replace(GetLangID, "%", "61");
            GetLangID = Strings.Replace(GetLangID, "^", "62");
            GetLangID = Strings.Replace(GetLangID, "&", "63");
            GetLangID = Strings.Replace(GetLangID, "*", "64");
            GetLangID = Strings.Replace(GetLangID, "(", "65");
            GetLangID = Strings.Replace(GetLangID, ")", "66");
            GetLangID = Strings.Replace(GetLangID, "_", "67");
            GetLangID = Strings.Replace(GetLangID, "-", "68");
            GetLangID = Strings.Replace(GetLangID, "+", "69");
            GetLangID = Strings.Replace(GetLangID, "=", "70");
            GetLangID = Strings.Replace(GetLangID, "{", "71");
            GetLangID = Strings.Replace(GetLangID, "}", "72");
            GetLangID = Strings.Replace(GetLangID, "|", "73");
            GetLangID = Strings.Replace(GetLangID, "[", "74");
            GetLangID = Strings.Replace(GetLangID, "]", "75");
            GetLangID = Strings.Replace(GetLangID, "\\", "76");
            GetLangID = Strings.Replace(GetLangID, ":", "77");
            GetLangID = Strings.Replace(GetLangID, ";", "78");
            GetLangID = Strings.Replace(GetLangID, "\"", "79");
            GetLangID = Strings.Replace(GetLangID, "<", "80");
            GetLangID = Strings.Replace(GetLangID, ">", "81");
            GetLangID = Strings.Replace(GetLangID, "?", "82");
            GetLangID = Strings.Replace(GetLangID, ",", "83");
            GetLangID = Strings.Replace(GetLangID, ".", "84");
            GetLangID = Strings.Replace(GetLangID, "?", "85");
            GetLangID = Strings.Replace(GetLangID, "/", "86");

            try
            {
                var objCI = new CultureInfo(siteLang);
                Thread.CurrentThread.CurrentCulture = objCI;
                Thread.CurrentThread.CurrentUICulture = objCI;

                var rm = ResourceManager.CreateFileBasedResourceManager("resource", GetDirValue("App_GlobalResources"), null);

                strLangText = rm.GetString(GetLangID, objCI);
            }
            catch
            {
            }

            if (Strings.Trim(strLangText) != Strings.Trim(GetLangID))
            {
                strLangText = Strings.Replace(strLangText, "~~#~~", strExcluded);
                if (string.IsNullOrWhiteSpace(strLangText))
                {
                    strLangText = Strings.Replace(strText, "~~", string.Empty);
                }
            }
            else
            {
                strLangText = Strings.Replace(strText, "~~", string.Empty);
            }

            try
            {
                if (bAdd && DebugMode)
                {
                    if (File.Exists(GetDirValue("skins") + "public\\xml\\resource.en-US.txt"))
                    {
                        var inidata = string.Empty;
                        using (var objReader = new StreamReader(GetDirValue("skins") + "public\\xml\\resource.en-US.txt"))
                        {
                            inidata = objReader.ReadToEnd();
                        }

                        if (Strings.InStr(inidata, "R" + GetLangID + "\t") == 0)
                        {
                            using (var outputLicense = new StreamWriter(GetDirValue("skins") + "public\\xml\\resource.en-US.txt"))
                            {
                                outputLicense.Write(inidata + "R" + GetLangID + "\t" + HTMLEncode(Strings.Replace(strText, "~~" + strExcluded + "~~", "~~#~~")) + Environment.NewLine);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return HTMLDecode(strLangText);
        }

        /// <summary>
        /// List to data table.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ListToDataTable<T>(IList<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var dt = new DataTable();
            for (var i = 0; i <= properties.Count - 1; i++)
            {
                var property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }

            var values = new object[properties.Count];
            foreach (var item in data)
            {
                for (var i = 0; i <= values.Length - 1; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dt.Rows.Add(values);
            }

            return dt;
        }

        /// <summary>
        /// Loads an image.
        /// </summary>
        /// <param name="sImage">The image.</param>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="iSize">(Optional) Zero-based index of the size.</param>
        /// <param name="allowResize">(Optional) True to allow, false to suppress the resize.</param>
        /// <param name="sLeftAlign">(Optional) The left align.</param>
        /// <param name="sImageName">(Optional) Name of the image.</param>
        /// <returns>The image.</returns>
        public static string LoadImage(string sImage, int iModuleID, int iSize = 1, bool allowResize = false, string sLeftAlign = "", string sImageName = "")
        {
            var sImgSuffix = string.Empty;
            var sDir = GetDirValue("images", true);

            if (iModuleID == 63 && Setup(63, "ProfilesEnable") != "Enable")
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(sImage))
            {
                switch (iSize)
                {
                    case 1:
                        sImgSuffix = "thumb_";
                        break;

                    case 2:
                        sImgSuffix = "large_";
                        break;

                    case 3:
                        sImgSuffix = string.Empty;
                        break;

                    case 4:
                        sImgSuffix = "slarge_";
                        break;
                }

                if (Strings.Left(sImage, 7) == "http://")
                {
                    allowResize = false;
                    sDir = string.Empty;
                    sImgSuffix = string.Empty;
                }

                if (allowResize)
                {
                    return "<a href=\"" + sDir + "slarge_" + sImage + "\" rel=\"lightbox" + Strings.ToString(iModuleID == 28 && Request.Item("From") != "AJAX" ? "[photoalbums]" : string.Empty) + "\" id=\"ResizeHref\">" + insImage(GetDirValue("images", true) + sImgSuffix + sImage, "Thumbnail Image", sLeftAlign, string.Empty, true, string.Empty, Strings.ToString(!string.IsNullOrWhiteSpace(sImageName) ? " name=\"" + HTMLEncode(sImageName) + "\"" : string.Empty)) + "</a>";
                }

                return insImage(sDir + sImgSuffix + sImage, "Thumbnail Image", sLeftAlign, string.Empty, true, string.Empty, Strings.ToString(!string.IsNullOrWhiteSpace(sImageName) ? " name=\"" + HTMLEncode(sImageName) + "\"" : string.Empty));
            }

            if (iModuleID == 63 || iModuleID == 18)
            {
                return insImage(sDir + "spadmin/nopicture.gif", "No Image Available", sLeftAlign, string.Empty, true, string.Empty, Strings.ToString(!string.IsNullOrWhiteSpace(sImageName) ? " name=\"" + HTMLEncode(sImageName) + "\"" : string.Empty));
            }

            return string.Empty;
        }

        /// <summary>
        /// Module activated.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool ModuleActivated(long iModuleID)
        {
            return true;
            // TODO
            //var bReturn = false;
            //switch (iModuleID)
            //{
            //    case 0:
            //        return true;

            //    case 1:
            //        return true;

            //    case 2:
            //        return true;

            //    case 3:
            //        return true;

            //    case 4:
            //        return true;

            //    case 8:
            //        return true;

            //    case 16:
            //        return true;

            //    case 17:
            //        return true;

            //    case 24:
            //        return true;

            //    case 29:
            //        return true;

            //    case 33:
            //        return true;

            //    case 34:
            //        return true;

            //    case 200:
            //        return true;

            //    case 201:
            //        return true;

            //    case 975:
            //        return true;

            //    case 976:
            //        return true;

            //    case 977:
            //        return true;

            //    case 979:
            //        return true;

            //    case 984:
            //        return true;

            //    case 985:
            //        return true;

            //    case 986:
            //        return true;

            //    case 987:
            //        return true;

            //    case 990:
            //        return true;

            //    case 995:
            //        return true;

            //    case 997:
            //        return true;

            //    case 998:
            //        return true;

            //    case 999:

            //        // Built in modules Newsletters Messenger Whos Online Advertisements Content
            //        // Rotator Contact Us Search Engine Group Lists Taxes Signup Activities Web Pages
            //        return true;

            //    case 38:
            //        return isProfessionalEdition();

            //    case 39:
            //        return isProfessionalEdition();

            //    case 973:
            //        return isProfessionalEdition();

            //    case 983:
            //        return isProfessionalEdition();

            //    case 989:

            //        // Professional edition modules
            //        return isProfessionalEdition();

            //    default:
            //        if (!File.Exists(GetDirValue("app_data") + "license.xml"))
            //        {
            //            return false;
            //        }
            //        else
            //        {
            //            var doc = new XmlDocument();
            //            var sXML = string.Empty;

            //            if (File.Exists(GetDirValue("app_data") + "license.xml"))
            //            {
            //                try
            //                {
            //                    doc.Load(GetDirValue("app_data") + "license.xml");

            //                    // Select the book node with the matching attribute value.
            //                    var root = doc.DocumentElement;

            //                    sXML = root.InnerXml;
            //                }
            //                catch
            //                {
            //                    sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            //                    sXML += "<ROOTLEVEL>";
            //                    sXML += "<Username></Username>";
            //                    sXML += "<Password></Password>";
            //                    sXML += "<LicenseKey></LicenseKey>";
            //                    sXML += "<LastChecked>" + Strings.FormatDateTime(DateTime.Now, Strings.DateNamedFormat.GeneralDate) + "</LastChecked>";
            //                    sXML += "<ModuleList></ModuleList>";
            //                    sXML += "<Version>Standard</Version>";
            //                    sXML += "</ROOTLEVEL>";
            //                    Debug_Log("Error loading license.xml");
            //                }
            //            }
            //            else
            //            {
            //                sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            //                sXML += "<ROOTLEVEL>";
            //                sXML += "<Username></Username>";
            //                sXML += "<Password></Password>";
            //                sXML += "<LicenseKey></LicenseKey>";
            //                sXML += "<LastChecked>" + Strings.FormatDateTime(DateTime.Now, Strings.DateNamedFormat.GeneralDate) + "</LastChecked>";
            //                sXML += "<ModuleList></ModuleList>";
            //                sXML += "<Version>Standard</Version>";
            //                sXML += "</ROOTLEVEL>";
            //                Debug_Log("Missing license.xml");
            //            }

            //            doc = null;
            //            GC.Collect();
            //            GC.WaitForPendingFinalizers();

            //            var bDownload = false;

            //            if (!Information.IsDate(ParseXML("LastChecked", sXML)) || toDate(ParseXML("LastChecked", sXML)) < toDate(Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate)))
            //            {
            //                bDownload = true;
            //            }

            //            if (bDownload)
            //            {
            //                var jActivation = new jActivation();
            //                try
            //                {
            //                    using (var soapClient = new activationSoapClient("activationSoap"))
            //                    {
            //                        jActivation = soapClient.Get_License_Details(ParseXML("Username", sXML), ParseXML("Password", sXML), ParseXML("LicenseKey", sXML));
            //                    }
            //                }
            //                catch
            //                {
            //                    if (Strings.InStr(ParseXML("ModuleList", sXML), "|" + iModuleID + "|") > 0)
            //                    {
            //                        bReturn = true;
            //                    }
            //                    else
            //                    {
            //                        bReturn = false;
            //                    }
            //                }

            //                try
            //                {
            //                    var sLicense = string.Empty;
            //                    var sModuleList = jActivation.ModuleList;
            //                    using (var readLicense = new StreamReader(GetDirValue("app_data") + "license.xml"))
            //                    {
            //                        sLicense = readLicense.ReadToEnd();
            //                        sLicense = Strings.Replace(sLicense, "<LastChecked>" + ParseXML("LastChecked", sLicense) + "</LastChecked>", "<LastChecked>" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate) + "</LastChecked>");
            //                        sLicense = Strings.Replace(sLicense, "<ModuleList>" + ParseXML("ModuleList", sLicense) + "</ModuleList>", "<ModuleList>" + sModuleList + "</ModuleList>");
            //                    }

            //                    using (var outputLicense = new StreamWriter(GetDirValue("app_data") + "license.xml"))
            //                    {
            //                        outputLicense.Write(sLicense);
            //                    }

            //                    Cache_Remove();
            //                    if (!string.IsNullOrWhiteSpace(sModuleList))
            //                    {
            //                        if (Strings.InStr(sModuleList, "|" + iModuleID + "|") > 0)
            //                        {
            //                            bReturn = true;
            //                        }
            //                        else
            //                        {
            //                            bReturn = false;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        bReturn = false;
            //                    }
            //                }
            //                catch
            //                {
            //                    bReturn = false;
            //                }
            //            }
            //            else
            //            {
            //                if (Strings.InStr(ParseXML("ModuleList", sXML), "|" + iModuleID + "|") > 0)
            //                {
            //                    bReturn = true;
            //                }
            //                else
            //                {
            //                    bReturn = false;
            //                }
            //            }
            //        }

            //        if (bReturn && Get_Portal_ID() > 0)
            //        {
            //            using (var conn = new SqlConnection(Database_Connection()))
            //            {
            //                conn.Open();
            //                using (var cmd = new SqlCommand("SELECT PageID FROM PortalPages WHERE PageID=@PageID AND PortalID=@PortalID AND Status > '-2'", conn))
            //                {
            //                    cmd.Parameters.AddWithValue("@PageID", iModuleID);
            //                    cmd.Parameters.AddWithValue("@PortalID", Get_Portal_ID());
            //                    using (SqlDataReader RS = cmd.ExecuteReader())
            //                    {
            //                        if (RS.HasRows)
            //                        {
            //                            bReturn = true;
            //                        }
            //                        else
            //                        {
            //                            bReturn = false;
            //                        }

            //                    }
            //                }
            //            }
            //        }

            //        return bReturn;
            //}
        }

        /// <summary>
        /// Number add ordinal.
        /// </summary>
        /// <param name="num">Number of.</param>
        /// <returns>The total number of add ordinal.</returns>
        public static string NumberAddOrdinal(int num)
        {
            if (num <= 0)
            {
                return Strings.ToString(num);
            }

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";

                case 2:
                    return num + "nd";

                case 3:
                    return num + "rd";

                default:
                    return num + "th";
            }
        }

        /// <summary>
        /// Off set rows.
        /// </summary>
        /// <param name="intCount">Number of ints.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool OffSetRows(int intCount)
        {
            if (Convert.ToInt32(Strings.Right(Strings.ToString(intCount), 1)) == 1 || Convert.ToInt32(Strings.Right(Strings.ToString(intCount), 1)) == 3 || Convert.ToInt32(Strings.Right(Strings.ToString(intCount), 1)) == 5 || Convert.ToInt32(Strings.Right(Strings.ToString(intCount), 1)) == 7 || Convert.ToInt32(Strings.Right(Strings.ToString(intCount), 1)) == 9)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Opens a boolean.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool openBoolean(object strText)
        {
            return Convert.ToBoolean(Information.IsDBNull(strText) ? false : strText);
        }

        /// <summary>
        /// Opens a null.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <param name="bFixWord">(Optional) True to fix word.</param>
        /// <returns>A string.</returns>
        public static string openNull(object strText, bool bFixWord = false)
        {
            if (bFixWord)
            {
                return FixWord(Strings.Replace(Strings.Replace(Strings.ToString(Information.IsDBNull(strText) ? null : strText), "&#39;", "'"), "#39;", "'"));
            }

            return Strings.Replace(Strings.Replace(Strings.ToString(Information.IsDBNull(strText) ? null : strText), "&#39;", "'"), "#39;", "'");
        }

        /// <summary>
        /// Page load.
        /// </summary>
        public static void Page_Load()
        {
            if (toLong(Request.Item("PortalID")) != 0)
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT PortalID,FriendlyName FROM Portals WHERE PortalID=@PortalID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", toLong(Request.Item("PortalID")));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                Session.setSession("FriendlyPortalID", openNull(RS["FriendlyName"]));
                                Session.setSession("PortalID", Strings.ToString(toLong(openNull(RS["PortalID"]))));
                                Session.setSession("PortalIDLoaded", "1");
                            }
                            else
                            {
                                Session.setSession("PortalID", Strings.ToString(toLong(Request.Item("PortalID"))));
                                Session.setSession("PortalIDLoaded", "1");
                            }

                        }
                    }
                }
            }
            else
            {
                if (isUserPage())
                {
                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT PortalID FROM UPagesSites WHERE UserID=@UserID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", GetUserID(Request.Item("UserName")));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    Session.setSession("PortalID", SepCore.Strings.ToString(toLong(openNull(RS["PortalID"]))));
                                    Session.setSession("PortalIDLoaded", "1");
                                }

                            }
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(Request.Item("FriendlyPortalID")))
                {
                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE FriendlyName='" + FixWord(Request.Item("FriendlyPortalID")) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    Session.setSession("FriendlyPortalID", Request.Item("FriendlyPortalID"));
                                    Session.setSession("PortalID", SepCore.Strings.ToString(toLong(openNull(RS["PortalID"]))));
                                    Session.setSession("PortalIDLoaded", "1");
                                }
                                else
                                {
                                    Session.setSession("PortalID", "0");
                                    Session.setSession("PortalIDLoaded", "1");
                                }

                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Request.ServerVariables("SERVER_NAME")))
                    {
                        using (var conn = new SqlConnection(Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE DomainName='" + FixWord(Strings.Replace(Request.ServerVariables("SERVER_NAME"), "www.", string.Empty)) + "'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        Session.setSession("PortalID", SepCore.Strings.ToString(toLong(openNull(RS["PortalID"]))));
                                        Session.setSession("PortalIDLoaded", "1");
                                    }
                                    else
                                    {
                                        Session.setSession("PortalID", "0");
                                        Session.setSession("PortalIDLoaded", "1");
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        Session.setSession("PortalID", "0");
                        Session.setSession("PortalIDLoaded", "1");
                    }
                }
            }

            if (Request.Item("DoAction") != "Print")
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    if (isUserPage())
                    {
                        using (var cmd = new SqlCommand("SELECT SiteTemplates.FolderName FROM UPagesSites,SiteTemplates WHERE UPagesSites.TemplateID=SiteTemplates.TemplateID AND UPagesSites.UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", GetUserID(Request.Item("UserName")));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    Session.setSession("TemplateFolder", openNull(RS["FolderName"]));
                                    Session.setSession("TemplateFolderLoaded", "1");
                                }

                            }
                        }
                    }
                    else
                    {
                        if (Get_Portal_ID() == 0)
                        {
                            using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE useTemplate='1'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        Session.setSession("TemplateFolder", openNull(RS["FolderName"]));
                                        Session.setSession("TemplateFolderLoaded", "1");
                                    }

                                }
                            }
                        }
                        else
                        {
                            using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE TemplateID=@TemplateID", conn))
                            {
                                cmd.Parameters.AddWithValue("@TemplateID", PortalSetup("WEBSITELAYOUT", Get_Portal_ID()));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        Session.setSession("TemplateFolder", openNull(RS["FolderName"]));
                                        Session.setSession("TemplateFolderLoaded", "1");
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Page unload.
        /// </summary>
        public static void Page_Unload()
        {
            if (!string.IsNullOrWhiteSpace(Session.getSession("PortalIDLoaded")))
            {
                Session.setSession("PortalIDLoaded", string.Empty);
            }

            if (!string.IsNullOrWhiteSpace(Session.getSession("TemplateFolderLoaded")))
            {
                Session.setSession("TemplateFolderLoaded", string.Empty);
            }
        }

        /// <summary>
        /// Page header.
        /// </summary>
        /// <param name="iModuleID">Identifier for the module.</param>
        /// <param name="OverwriteTitle">(Optional) The overwrite title.</param>
        /// <returns>A string.</returns>
        public static string PageHeader(long iModuleID, string OverwriteTitle = "")
        {
            var str = new StringBuilder();

            var GetPageTitle = string.Empty;
            var GetLinkText = string.Empty;
            var GetDescription = string.Empty;
            var GetKeywords = string.Empty;

            var SqlStr = string.Empty;

            long iPageID = 0;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                if (toLong(Request.Item("UniqueID")) > 0)
                {
                    iModuleID = 0;
                    iPageID = toLong(Request.Item("UniqueID"));
                }

                if (iModuleID > 0)
                {
                    if (!string.IsNullOrWhiteSpace(Request.Item("CatID")) && string.IsNullOrWhiteSpace(Request.Item("DoAction")))
                    {
                        using (var cmd = new SqlCommand("SELECT SEOPageTitle,SEODescription,Keywords FROM Categories WHERE CatID='" + FixWord(Request.Item("CatID")) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (!string.IsNullOrWhiteSpace(openNull(RS["SEOPageTitle"])))
                                    {
                                        GetPageTitle = openNull(RS["SEOPageTitle"]);
                                    }

                                    if (!string.IsNullOrWhiteSpace(openNull(RS["SEODescription"])))
                                    {
                                        GetDescription = openNull(RS["SEODescription"]);
                                    }

                                    if (!string.IsNullOrWhiteSpace(openNull(RS["Keywords"])))
                                    {
                                        GetKeywords = openNull(RS["Keywords"]);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT PageTitle,Description FROM SEOTitles WHERE PageURL='" + FixWord(Request.Url.AbsolutePath() + Request.Url.Query()) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (!string.IsNullOrWhiteSpace(openNull(RS["PageTitle"])))
                                    {
                                        GetLinkText = openNull(RS["PageTitle"]);
                                    }

                                    if (!string.IsNullOrWhiteSpace(openNull(RS["Description"])))
                                    {
                                        GetDescription = openNull(RS["Description"]);
                                    }
                                }

                            }
                        }
                    }

                    switch (iModuleID)
                    {
                        case 7:
                            if (!string.IsNullOrWhiteSpace(Request.Item("PageName")))
                            {
                                using (var cmd = new SqlCommand("SELECT PageTitle FROM UPagesPages WHERE PageName=@PageName AND UserID=@UserID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@PageName", Request.Item("PageName"));
                                    cmd.Parameters.AddWithValue("@UserID", GetUserID(Request.Item("UserName")));
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (!string.IsNullOrWhiteSpace(openNull(RS["PageTitle"])))
                                            {
                                                GetLinkText = openNull(RS["PageTitle"]);
                                            }
                                        }

                                    }
                                }
                            }

                            break;

                        case 9:
                            if (!string.IsNullOrWhiteSpace(Request.Item("FAQID")) && Strings.InStr(Request.Item("FAQID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Question FROM FAQ WHERE FAQID='" + FixWord(Request.Item("FAQID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Question"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 12:
                            if (!string.IsNullOrWhiteSpace(Request.Item("TopicID")) && Strings.InStr(Request.Item("TopicID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Subject,Message FROM ForumsMessages WHERE TopicID='" + FixWord(Request.Item("TopicID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (!string.IsNullOrWhiteSpace(RemoveHTML(openNull(RS["Message"]))))
                                            {
                                                GetPageTitle = openNull(RS["Subject"]);
                                                GetDescription = RemoveHTML(openNull(RS["Message"]));
                                            }
                                        }

                                    }
                                }
                            }

                            break;

                        case 13:
                            if (!string.IsNullOrWhiteSpace(Request.Item("FormID")) && Strings.InStr(Request.Item("FormID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Title,Description FROM Forms WHERE FormID='" + FixWord(Request.Item("FormID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Title"]);
                                            GetDescription = RemoveHTML(openNull(RS["Description"]));
                                        }

                                    }
                                }
                            }

                            break;

                        case 18:
                            if (Request.Item("DoAction") == "ViewMatch" && !string.IsNullOrWhiteSpace(Request.Item("UserID")))
                            {
                                using (var cmd = new SqlCommand("SELECT Username FROM Members WHERE UserID='" + FixWord(Request.Item("UserID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = LangText("Match Maker Profile for ~~" + openNull(RS["Username"]) + "~~");
                                        }

                                    }
                                }
                            }

                            break;

                        case 20:
                            if (!string.IsNullOrWhiteSpace(Request.Item("BusinessID")) && Strings.InStr(Request.Item("BusinessID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT BusinessName,Description FROM BusinessListings WHERE BusinessID='" + FixWord(Request.Item("BusinessID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (!string.IsNullOrWhiteSpace(RemoveHTML(openNull(RS["BusinessName"]))))
                                            {
                                                GetPageTitle = openNull(RS["BusinessName"]);
                                                GetDescription = RemoveHTML(openNull(RS["Description"]));
                                            }
                                        }

                                    }
                                }
                            }

                            break;

                        case 23:
                            if (!string.IsNullOrWhiteSpace(Request.Item("NewsID")) && Strings.InStr(Request.Item("NewsID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Topic,Headline FROM News WHERE NewsID='" + FixWord(Request.Item("NewsID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (!string.IsNullOrWhiteSpace(RemoveHTML(openNull(RS["Headline"]))))
                                            {
                                                GetPageTitle = openNull(RS["Topic"]);
                                                GetDescription = RemoveHTML(openNull(RS["Headline"]));
                                            }
                                        }

                                    }
                                }
                            }

                            break;

                        case 25:
                            if (!string.IsNullOrWhiteSpace(Request.Item("PollID")) && Strings.InStr(Request.Item("PollID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Question FROM PNQQuestions WHERE PollID='" + FixWord(Request.Item("PollID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Question"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 31:
                            if (!string.IsNullOrWhiteSpace(Request.Item("AdID")) && Strings.InStr(Request.Item("AdID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Title FROM AuctionAds WHERE AdID='" + FixWord(Request.Item("AdID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Title"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 32:
                            if (!string.IsNullOrWhiteSpace(Request.Item("PropertyID")) && Strings.InStr(Request.Item("PropertyID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Title FROM RStateProperty WHERE PropertyID='" + FixWord(Request.Item("PropertyID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Title"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 35:
                            if (!string.IsNullOrWhiteSpace(Request.Item("ArticleID")) && Strings.InStr(Request.Item("ArticleID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Headline,Author,Meta_Description,Meta_Keywords FROM Articles WHERE ArticleID='" + FixWord(Request.Item("ArticleID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Headline"]);
                                            if (!string.IsNullOrWhiteSpace(openNull(RS["Author"])))
                                            {
                                                GetPageTitle += " by " + openNull(RS["Author"]);
                                            }

                                            GetDescription = openNull(RS["Meta_Description"]);
                                            GetKeywords = openNull(RS["Meta_Keywords"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 37:
                            if (!string.IsNullOrWhiteSpace(Request.Item("CourseID")) && Strings.InStr(Request.Item("CourseID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT CourseName FROM ELearnCourses WHERE CourseID='" + FixWord(Request.Item("CourseID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["CourseName"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 41:
                            if (!string.IsNullOrWhiteSpace(Request.Item("ProductID")) && Strings.InStr(Request.Item("ProductID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT ProductName,Description FROM ShopProducts WHERE ProductID='" + FixWord(Request.Item("ProductID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["ProductName"]);
                                            GetDescription = RemoveHTML(openNull(RS["Description"]));
                                        }

                                    }
                                }
                            }

                            break;

                        case 44:
                            if (!string.IsNullOrWhiteSpace(Request.Item("AdID")) && Strings.InStr(Request.Item("AdID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Title FROM ClassifiedsAds WHERE AdID='" + FixWord(Request.Item("AdID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Title"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 46:
                            if (!string.IsNullOrWhiteSpace(Request.Item("EventID")) && Strings.InStr(Request.Item("EventID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT EC.Subject FROM EventCalendar AS EC WHERE EC.EventID='" + FixWord(Request.Item("EventID")) + "' AND EC.PortalID=" + Get_Portal_ID() + string.Empty, conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["Subject"]);
                                        }

                                    }
                                }
                            }

                            break;

                        case 61:
                            if (!string.IsNullOrWhiteSpace(Request.Item("BlogID")) && Strings.InStr(Request.Item("BlogID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT Blog.BlogName,Members.UserName FROM Blog,Members WHERE Blog.UserID=Members.UserID AND Blog.BlogID='" + FixWord(Request.Item("BlogID")) + "' AND Blog.PortalID=" + Get_Portal_ID() + string.Empty, conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["BlogName"]);
                                            if (!string.IsNullOrWhiteSpace(openNull(RS["UserName"])))
                                            {
                                                GetPageTitle += " by " + openNull(RS["UserName"]);
                                            }
                                        }

                                    }
                                }
                            }

                            break;

                        case 63:
                            if (Request.Item("DoAction") == "ViewMatch" && !string.IsNullOrWhiteSpace(Request.Item("UserID")))
                            {
                                using (var cmd = new SqlCommand("SELECT Username FROM Members WHERE UserID='" + FixWord(Request.Item("UserID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = LangText("Member Profile for ~~" + openNull(RS["Username"]) + "~~");
                                        }

                                    }
                                }
                            }

                            break;

                        case 65:
                            if (!string.IsNullOrWhiteSpace(Request.Item("VoucherID")) && Strings.InStr(Request.Item("VoucherID"), ",") == 0)
                            {
                                using (var cmd = new SqlCommand("SELECT BuyTitle,ShortDescription FROM Vouchers WHERE VoucherID='" + FixWord(Request.Item("VoucherID")) + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            GetPageTitle = openNull(RS["BuyTitle"]);
                                            GetDescription = openNull(RS["ShortDescription"]);
                                        }

                                    }
                                }
                            }

                            break;
                    }

                    if (string.IsNullOrWhiteSpace(GetLinkText) && string.IsNullOrWhiteSpace(GetDescription) && !string.IsNullOrWhiteSpace(Request.Item("CatID")))
                    {
                        using (var cmd = new SqlCommand("SELECT CategoryName,SEOPageTitle,SEODescription,Keywords FROM Categories WHERE CatID='" + FixWord(Request.Item("CatID")) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (string.IsNullOrWhiteSpace(GetPageTitle))
                                    {
                                        GetPageTitle = openNull(RS["SEOPageTitle"]);
                                    }

                                    if (string.IsNullOrWhiteSpace(GetPageTitle))
                                    {
                                        GetPageTitle = openNull(RS["CategoryName"]);
                                    }

                                    GetDescription = openNull(RS["SEODescription"]);
                                    GetKeywords = openNull(RS["Keywords"]);
                                }

                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(GetLinkText) && string.IsNullOrWhiteSpace(GetDescription))
                    {
                        switch (iModuleID)
                        {
                            case 3:
                                if (string.IsNullOrWhiteSpace(GetPageTitle))
                                {
                                    GetPageTitle = LangText("Search Engine");
                                }

                                break;

                            case 987:
                                if (string.IsNullOrWhiteSpace(GetPageTitle))
                                {
                                    GetPageTitle = LangText("Refer a Friend");
                                }

                                break;

                            case 988:
                                if (string.IsNullOrWhiteSpace(GetPageTitle))
                                {
                                    GetPageTitle = LangText("User Information");
                                    if (!string.IsNullOrWhiteSpace(Request.Item("UserID")))
                                    {
                                        using (var cmd = new SqlCommand("SELECT Username FROM Members WHERE UserID='" + FixWord(Request.Item("UserID")) + "'", conn))
                                        {
                                            using (SqlDataReader RS = cmd.ExecuteReader())
                                            {
                                                if (RS.HasRows)
                                                {
                                                    RS.Read();
                                                    GetPageTitle += " (" + openNull(RS["Username"]) + ")";
                                                }

                                            }
                                        }
                                    }
                                }

                                break;

                            case 995:
                                if (string.IsNullOrWhiteSpace(GetPageTitle))
                                {
                                    GetPageTitle = LangText("View Shopping Cart");
                                }

                                break;
                        }
                    }
                }

                if (iPageID == 0)
                {
                    if (Get_Portal_ID() < 1)
                    {
                        SqlStr = "SELECT LinkText,PageTitle,Description,Keywords FROM ModulesNPages WHERE PageID='" + iModuleID + "'";
                    }
                    else
                    {
                        SqlStr = "SELECT LinkText,PageTitle,Description,Keywords FROM PortalPages WHERE PageID='" + iModuleID + "' AND PortalID=" + Get_Portal_ID() + " AND Status=1";
                    }
                }
                else
                {
                    if (Get_Portal_ID() < 1)
                    {
                        SqlStr = "SELECT LinkText,PageTitle,Description,Keywords FROM ModulesNPages WHERE PageID='200' AND UniqueID='" + iPageID + "'";
                    }
                    else
                    {
                        SqlStr = "SELECT LinkText,PageTitle,Description,Keywords FROM PortalPages WHERE PageID='200' AND UniqueID='" + iPageID + "' AND (PortalID=" + Get_Portal_ID() + " OR PortalID = -1) AND Status=1";
                    }
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            if (string.IsNullOrWhiteSpace(GetPageTitle))
                            {
                                GetPageTitle = openNull(RS["PageTitle"]);
                            }

                            GetLinkText = openNull(RS["LinkText"]);
                            if (string.IsNullOrWhiteSpace(GetDescription))
                            {
                                GetDescription = openNull(RS["Description"]);
                            }

                            if (string.IsNullOrWhiteSpace(GetKeywords))
                            {
                                GetKeywords = openNull(RS["Keywords"]);
                            }
                        }

                    }
                }

                if (!string.IsNullOrWhiteSpace(OverwriteTitle))
                {
                    str.Append(LangText(OverwriteTitle) + " : " + WebsiteName(Get_Portal_ID()) + "|$$|");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(GetPageTitle))
                    {
                        str.Append(GetPageTitle + " : " + WebsiteName(Get_Portal_ID()) + "|$$|");
                    }
                    else
                    {
                        str.Append(GetLinkText + " : " + WebsiteName(Get_Portal_ID()) + "|$$|");
                    }
                }

                str.Append(GetDescription + "|$$|");
                str.Append(GetKeywords + "|$$|");
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Parse digits.
        /// </summary>
        /// <param name="strRawValue">The raw value.</param>
        /// <returns>A string.</returns>
        public static string ParseDigits(string strRawValue)
        {
            var strDigits = string.Empty;
            if (string.IsNullOrEmpty(strRawValue))
            {
                return strDigits;
            }

            foreach (var c in strRawValue)
            {
                if (char.IsNumber(c) || Strings.ToString(c) == "." || Strings.ToString(c) == ",")
                {
                    strDigits += c;
                }
            }

            return strDigits;
        }

        /// <summary>
        /// Parse XML.
        /// </summary>
        /// <param name="fieldname">The fieldname.</param>
        /// <param name="apstring">The apstring.</param>
        /// <returns>A string.</returns>
        public static string ParseXML(string fieldname, string apstring)
        {
            var posa = 0;
            var posb = 0;

            if (Strings.InStr(apstring, "<" + fieldname + ">") > 0 && Strings.InStr(apstring, "</" + fieldname + ">") > 0)
            {
                posa = Strings.InStr(apstring, "<" + fieldname + ">");
                posb = Strings.InStr(posa, apstring, "</" + fieldname + ">");

                return Strings.Trim(Strings.Mid(apstring, posa + Strings.Len(fieldname) + 2, posb - posa - Strings.Len(fieldname) - 2));
            }

            return string.Empty;
        }

        /// <summary>
        /// Pointing array.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns>A string.</returns>
        public static string Pointing_Array(int intModuleID, string ReturnValue)
        {
            string[] arrQuestion = null;
            string[] arrVariable = null;
            string[] arrActType = null;

            switch (intModuleID)
            {
                case 5:

                    // ============= Discount System =====================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Adding a discount coupon";

                    arrVariable[0] = "AddCoupon";

                    arrActType[0] = "ADDCOUPON";
                    break;

                case 7:

                    // ============= User Pages ==================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Creating a web site";

                    arrVariable[0] = "CreateSite";

                    arrActType[0] = "ADDSITE";
                    break;

                case 8:

                    // ============= Comments & Ratings ==================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Rating a listing";
                    arrQuestion[1] = "Leaving a comment";

                    arrVariable[0] = "RateList";
                    arrVariable[1] = "LeaveComment";

                    arrActType[0] = "ADDRATING";
                    arrActType[1] = "ADDCOMMENT";
                    break;

                case 10:

                    // ============= File Libraries ======================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Uploading a file";
                    arrQuestion[1] = "Downloading / view a download";

                    arrVariable[0] = "UploadFile";
                    arrVariable[1] = "DownloadFile";

                    arrActType[0] = "ADDFILE";
                    arrActType[1] = "DOWNLOADFILE";
                    break;

                case 12:

                    // ============= Forums ==============================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Posting a new topic";
                    arrQuestion[1] = "Replying to a topic";

                    arrVariable[0] = "PostTopic";
                    arrVariable[1] = "ReplyTopic";

                    arrActType[0] = "ADDTOPIC";
                    arrActType[1] = "REPLYTOPIC";
                    break;

                case 13:

                    // ============= Forms ===============================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Submitting a form";

                    arrVariable[0] = "SubmitForm";

                    arrActType[0] = "SUBMITFORM";
                    break;

                case 14:

                    // ============= Guestbook ===========================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Signing the guestbook";

                    arrVariable[0] = "SignGuestbook";

                    arrActType[0] = "SIGNGUESTBOOK";
                    break;

                case 17:

                    // ============= Messenger ===========================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Sending a message";

                    arrVariable[0] = "SendMessage";

                    arrActType[0] = "SENDMESSAGE";
                    break;

                case 18:

                    // ============= Match Maker =========================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Creating a profile";

                    arrVariable[0] = "CreateProfile";

                    arrActType[0] = "ADDPROFILE";
                    break;

                case 19:

                    // ============= Links ===============================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Posting a link";
                    arrQuestion[1] = "Viewing a link";

                    arrVariable[0] = "PostLink";
                    arrVariable[1] = "ViewLink";

                    arrActType[0] = "ADDLINK";
                    arrActType[1] = "VIEWLINK";
                    break;

                case 20:

                    // ============= Business Directory ====================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Posting a business";
                    arrQuestion[1] = "Viewing a business";

                    arrVariable[0] = "PostBusiness";
                    arrVariable[1] = "ViewBusiness";

                    arrActType[0] = "ADDBUSINESS";
                    arrActType[1] = "VIEWBUSINESS";
                    break;

                case 25:

                    // ============= Polls ================================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Voting for a poll";

                    arrVariable[0] = "VotePoll";

                    arrActType[0] = "VOTEPOLL";
                    break;

                case 28:

                    // ============= Photo Albums =========================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Creating an album";
                    arrQuestion[1] = "Uploading a picture";

                    arrVariable[0] = "CreateAlbum";
                    arrVariable[1] = "UploadPicture";

                    arrActType[0] = "ADDALBUM";
                    arrActType[1] = "ALBUMPICUPLOAD";
                    break;

                case 31:

                    // ============= Auction ==============================
                    Array.Resize(ref arrQuestion, 3);
                    Array.Resize(ref arrVariable, 3);
                    Array.Resize(ref arrActType, 3);

                    arrQuestion[0] = "Adding an auction";
                    arrQuestion[1] = "Viewing an auction";
                    arrQuestion[2] = "Bidding on an item";

                    arrVariable[0] = "AddAction";
                    arrVariable[1] = "ViewAuction";
                    arrVariable[2] = "BidItem";

                    arrActType[0] = "ADDAUCTION";
                    arrActType[1] = "VIEWAUCTION";
                    arrActType[2] = "BIDAUCTION";
                    break;

                case 32:

                    // ============= Real Estate ==============================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Posting a property";
                    arrQuestion[1] = "Viewing a property";

                    arrVariable[0] = "PostProperty";
                    arrVariable[1] = "ViewProperty";

                    arrActType[0] = "ADDPROPERTY";
                    arrActType[1] = "VIEWPROPERTY";
                    break;

                case 35:

                    // ============= Articles ==============================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Posting an article";
                    arrQuestion[1] = "Viewing an article";

                    arrVariable[0] = "PostArticle";
                    arrVariable[1] = "ViewArticle";

                    arrActType[0] = "ADDARTICLE";
                    arrActType[1] = "VIEWARTICLE";
                    break;

                case 39:

                    // ============= Affiliate Program ======================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Referring a user to the site";

                    arrVariable[0] = "ReferUser";

                    arrActType[0] = "AFFILIATEREFERRAL";
                    break;

                case 40:

                    // ============= Hot or Not ================================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Rating a picture";

                    arrVariable[0] = "RatePicture";

                    arrActType[0] = "RATEHOTORNOT";
                    break;

                case 43:

                    // ============= Refer a Friend ================================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Referring a user to the site";

                    arrVariable[0] = "ReferUser";

                    arrActType[0] = "REFERAFRIEND";
                    break;

                case 44:

                    // ============= Classified Ads ================================
                    Array.Resize(ref arrQuestion, 3);
                    Array.Resize(ref arrVariable, 3);
                    Array.Resize(ref arrActType, 3);

                    arrQuestion[0] = "Posting an ad";
                    arrQuestion[1] = "Viewing an ad";
                    arrQuestion[2] = "Purchasing an item";

                    arrVariable[0] = "PostAd";
                    arrVariable[1] = "ViewAd";
                    arrVariable[2] = "PurchaseItem";

                    arrActType[0] = "ADDCLASSAD";
                    arrActType[1] = "VIEWCLASSAD";
                    arrActType[2] = "BUYCLASSAD";
                    break;

                case 46:

                    // ============= Event Calendar ================================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Posting an event";
                    arrQuestion[1] = "Viewing an event";

                    arrVariable[0] = "PostEvent";
                    arrVariable[1] = "ViewEvent";

                    arrActType[0] = "ADDEVENT";
                    arrActType[1] = "VIEWEVENT";
                    break;

                case 48:

                    // ============= Job Listings =====================================
                    Array.Resize(ref arrQuestion, 3);
                    Array.Resize(ref arrVariable, 3);
                    Array.Resize(ref arrActType, 3);

                    arrQuestion[0] = "Adding a resume";
                    arrQuestion[1] = "Posting a job listing";
                    arrQuestion[2] = "Adding a company";

                    arrVariable[0] = "AddResume";
                    arrVariable[1] = "PostJob";
                    arrVariable[2] = "AddCompany";

                    arrActType[0] = "ADDRESUME";
                    arrActType[1] = "ADDPOSITION";
                    arrActType[2] = "ADDCOMPANY";
                    break;

                case 61:

                    // ============= Blogger ===================================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Adding a blog";
                    arrQuestion[1] = "Viewing a blog";

                    arrVariable[0] = "AddBlog";
                    arrVariable[1] = "ViewBlog";

                    arrActType[0] = "ADDBLOG";
                    arrActType[1] = "VIEWBLOG";
                    break;

                case 62:

                    // ============= Countdown Auction ===================================
                    Array.Resize(ref arrQuestion, 2);
                    Array.Resize(ref arrVariable, 2);
                    Array.Resize(ref arrActType, 2);

                    arrQuestion[0] = "Adding an auction";
                    arrQuestion[1] = "Bidding on an Item";

                    arrVariable[0] = "AddCAuction";
                    arrVariable[1] = "BidCAuction";

                    arrActType[0] = "ADDCAUCTION";
                    arrActType[1] = "BIDCAUCTION";
                    break;

                case 63:

                    // ============= User Profiles ================================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Adding a profile";

                    arrVariable[0] = "AddProfile";

                    arrActType[0] = "ADDPROFILE";
                    break;

                case 989:

                    // ============= Access Class ================================
                    var classCount = 0;

                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM AccessClasses ORDER BY ClassName", conn))
                        {
                            using (SqlDataReader SetupRS = cmd.ExecuteReader())
                            {
                                while (SetupRS.Read())
                                {
                                    Array.Resize(ref arrQuestion, classCount + 1);
                                    Array.Resize(ref arrVariable, classCount + 1);
                                    Array.Resize(ref arrActType, classCount + 1);

                                    arrQuestion[classCount] = LangText("Join the ~~" + openNull(SetupRS["ClassName"]) + "~~ class");

                                    arrVariable[classCount] = "JoinClass" + openNull(SetupRS["ClassID"]);

                                    arrActType[classCount] = "CHANGECLASS";
                                }
                            }
                        }
                    }

                    break;

                case 997:

                    // ============= Signup Setup ================================
                    Array.Resize(ref arrQuestion, 1);
                    Array.Resize(ref arrVariable, 1);
                    Array.Resize(ref arrActType, 1);

                    arrQuestion[0] = "Creating a new account";

                    arrVariable[0] = "CreateAccount";

                    arrActType[0] = "SIGNUP";
                    break;
            }

            switch (ReturnValue)
            {
                case "Question":
                    return Strings.Join(arrQuestion, "||");

                case "Variable":
                    return Strings.Join(arrVariable, "||");

                default:
                    return Strings.Join(arrActType, "||");
            }
        }

        /// <summary>
        /// Points setup.
        /// </summary>
        /// <param name="OptionValue">The option value.</param>
        /// <returns>A long.</returns>
        public static long Points_Setup(string OptionValue)
        {
            if (File.Exists(GetDirValue("app_data") + "points.xml"))
            {
                var doc = new XmlDocument();

                doc.Load(GetDirValue("app_data") + "points.xml");

                var root = doc.DocumentElement;

                strPointsXml = root.InnerXml;

                doc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return toLong(ParseXML(OptionValue, strPointsXml));
        }

        /// <summary>
        /// Portal setup.
        /// </summary>
        /// <param name="OptionValue">The option value.</param>
        /// <param name="iPortalID">(Optional) Identifier for the portal.</param>
        /// <returns>A string.</returns>
        public static string PortalSetup(string OptionValue, long iPortalID = 0)
        {
            var sXML = string.Empty;

            if (File.Exists(GetDirValue("app_data") + "settings-" + iPortalID + ".xml"))
            {
                var doc = new XmlDocument();
                doc.Load(GetDirValue("app_data") + "settings-" + iPortalID + ".xml");

                var root = doc.DocumentElement;

                sXML = root.InnerXml;

                doc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            if (!string.IsNullOrWhiteSpace(sXML))
            {
                return Strings.Trim(ParseXML(OptionValue, sXML));
            }

            return string.Empty;
        }

        /// <summary>
        /// Pricing long price.
        /// </summary>
        /// <param name="strUnitPrice">The unit price.</param>
        /// <param name="strRecurringPrice">The recurring price.</param>
        /// <param name="strRecurringCycle">The recurring cycle.</param>
        /// <returns>A string.</returns>
        public static string Pricing_Long_Price(decimal strUnitPrice, decimal strRecurringPrice, string strRecurringCycle)
        {
            var str = new StringBuilder();

            if (strUnitPrice > 0 && strRecurringPrice == 0)
            {
                str.Append(Format_Currency(strUnitPrice));
            }
            else if (strUnitPrice == 0 && strRecurringPrice > 0)
            {
                str.Append(Format_Currency(strRecurringPrice));
                if (strRecurringCycle == "1m")
                {
                    str.Append("/" + LangText("month"));
                }
                else if (strRecurringCycle == "3m")
                {
                    str.Append("/" + LangText("3 months"));
                }
                else if (strRecurringCycle == "6m")
                {
                    str.Append("/" + LangText("6 months"));
                }
                else
                {
                    str.Append("/" + LangText("year"));
                }
            }
            else
            {
                str.Append(Format_Currency(strUnitPrice));
                if (strRecurringPrice > 0)
                {
                    str.Append(" " + LangText("and") + " " + Format_Currency(strRecurringPrice) + string.Empty);
                    if (strRecurringCycle == "1m")
                    {
                        str.Append("/" + LangText("month"));
                    }
                    else if (strRecurringCycle == "3m")
                    {
                        str.Append("/" + LangText("3 months"));
                    }
                    else if (strRecurringCycle == "6m")
                    {
                        str.Append("/" + LangText("6 months"));
                    }
                    else
                    {
                        str.Append("/" + LangText("year"));
                    }
                }
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Rating check.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="intUniqueID">Unique identifier for the int.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool Rating_Check(int intModuleID, string intUniqueID)
        {
            var bReturn = false;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT IPAddress FROM Ratings WHERE ModuleID='" + intModuleID + "' AND UniqueID='" + FixWord(intUniqueID) + "' AND IPAddress='" + FixWord(GetUserIP()) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bReturn = true;
                        }

                    }
                }
            }

            return bReturn;
        }

        /// <summary>
        /// Rating stars display.
        /// </summary>
        /// <param name="TotalStars">The total stars.</param>
        /// <returns>A string.</returns>
        public static string Rating_Stars_Display(double TotalStars)
        {
            var sStars = string.Empty;

            var sImageFolder = GetInstallFolder(true);

            if (TotalStars >= 0.2 && TotalStars <= 0.7)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-half.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 0.8 && TotalStars <= 1.2)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 1.3 && TotalStars <= 1.7)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-half.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 1.8 && TotalStars <= 2.2)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 2.3 && TotalStars <= 2.7)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-half.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 2.8 && TotalStars <= 3.2)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 3.3 && TotalStars <= 3.7)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-half.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 3.8 && TotalStars <= 4.2)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 4.3 && TotalStars <= 4.7)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-half.png\" border=\"0\" alt=\"\" />";
            }
            else if (TotalStars >= 4.8)
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-yellow.png\" border=\"0\" alt=\"\" />";
            }
            else
            {
                sStars = "<img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" /><img src=\"" + sImageFolder + "images/public/star-grey.png\" border=\"0\" alt=\"\" />";
            }

            return sStars;
        }

        /// <summary>
        /// Redirects the given document.
        /// </summary>
        /// <param name="url">URL of the resource.</param>
        public static void Redirect(string url)
        {
            Response.Clear();
            Response.AddHeader("content-type", "text/html");
            Response.Redirect(url);
            Response.End();
        }

        /// <summary>
        /// Removes the HTML described by strText.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <returns>A string.</returns>
        public static string RemoveHTML(string strText)
        {
            var strContent = string.Empty;
            var mString = string.Empty;
            var mStartPos = 0;
            var mEndPos = 0;

            strContent = strText;

            // Start process
            mStartPos = Strings.InStr(strContent, "<");
            mEndPos = Strings.InStr(strContent, ">");
            while (mStartPos != 0 && mEndPos != 0 && mEndPos > mStartPos)
            {
                mString = Strings.Mid(strContent, mStartPos, mEndPos - mStartPos + 1);
                strContent = Strings.Replace(strContent, mString, string.Empty);
                mStartPos = Strings.InStr(strContent, "<");
                mEndPos = Strings.InStr(strContent, ">");
            }

            // Translate common escape sequence chars
            strContent = Strings.Replace(strContent, "&nbsp;", " ");
            strContent = Strings.Replace(strContent, "&amp;", "&");
            strContent = Strings.Replace(strContent, "&quot;", "'");
            strContent = Strings.Replace(strContent, "&#", "#");
            strContent = Strings.Replace(strContent, "&lt;", "<");
            strContent = Strings.Replace(strContent, "&gt;", ">");
            strContent = Strings.Replace(strContent, "%20", " ");
            strContent = Strings.LTrim(Strings.Trim(strContent));
            if (Strings.Len(strContent) > 0)
            {
                while (strContent.Substring(0, 1) == "\r" || strContent.Substring(0, 1) == "\n")
                {
                    strContent = strContent.Substring(1);
                }
            }

            return strContent;
        }

        /// <summary>
        /// Replace special.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <returns>A string.</returns>
        public static string ReplaceSpecial(string strText)
        {
            var sReturn = string.Empty;

            sReturn = Strings.Replace(strText, "~", string.Empty);
            sReturn = Strings.Replace(sReturn, "`", string.Empty);
            sReturn = Strings.Replace(sReturn, "!", string.Empty);
            sReturn = Strings.Replace(sReturn, "@", string.Empty);
            sReturn = Strings.Replace(sReturn, "#", string.Empty);
            sReturn = Strings.Replace(sReturn, "$", string.Empty);
            sReturn = Strings.Replace(sReturn, "%", string.Empty);
            sReturn = Strings.Replace(sReturn, "^", string.Empty);
            sReturn = Strings.Replace(sReturn, "&", string.Empty);
            sReturn = Strings.Replace(sReturn, "*", string.Empty);
            sReturn = Strings.Replace(sReturn, "(", string.Empty);
            sReturn = Strings.Replace(sReturn, ")", string.Empty);
            sReturn = Strings.Replace(sReturn, "+", string.Empty);
            sReturn = Strings.Replace(sReturn, "=", string.Empty);
            sReturn = Strings.Replace(sReturn, "{", string.Empty);
            sReturn = Strings.Replace(sReturn, "}", string.Empty);
            sReturn = Strings.Replace(sReturn, "|", string.Empty);
            sReturn = Strings.Replace(sReturn, "[", string.Empty);
            sReturn = Strings.Replace(sReturn, "]", string.Empty);
            sReturn = Strings.Replace(sReturn, "\\", string.Empty);
            sReturn = Strings.Replace(sReturn, ":", string.Empty);
            sReturn = Strings.Replace(sReturn, "\"", string.Empty);
            sReturn = Strings.Replace(sReturn, ";", string.Empty);
            sReturn = Strings.Replace(sReturn, "'", string.Empty);
            sReturn = Strings.Replace(sReturn, "<", string.Empty);
            sReturn = Strings.Replace(sReturn, ">", string.Empty);
            sReturn = Strings.Replace(sReturn, "?", string.Empty);
            sReturn = Strings.Replace(sReturn, "/", string.Empty);
            sReturn = Strings.Replace(sReturn, ".", string.Empty);
            sReturn = Strings.Replace(sReturn, ",", string.Empty);

            return sReturn;
        }

        /// <summary>
        /// Require login.
        /// </summary>
        /// <param name="ModuleKeys">The module keys.</param>
        /// <param name="fromAdmin">(Optional) True to from admin.</param>
        /// <param name="forceURL">(Optional) URL of the force.</param>
        public static void RequireLogin(string ModuleKeys, bool fromAdmin = false, string forceURL = "")
        {
            string[] RLoginArray = null;
            var DoLogin = false;

            var sURL = string.Empty;
            object Item = null;
            long intFormCount = 0;

            var adminPath = string.Empty;
            if (fromAdmin)
            {
                adminPath = "spadmin/";
            }

            var sInstallFolder = GetInstallFolder();

            RLoginArray = Strings.Split(ModuleKeys, ",");

            DoLogin = true;

            if (RLoginArray != null)
            {
                for (var i = 0; i <= Information.UBound(RLoginArray); i++)
                {
                    if (RLoginArray[i] == "1" || RLoginArray[i] == "|1|")
                    {
                        DoLogin = false;
                        break;
                    }
                }
            }

            if (DoLogin && string.IsNullOrWhiteSpace(Session_User_Name()))
            {
                if (!string.IsNullOrWhiteSpace(forceURL))
                {
                    Session.setCookie("returnUrl", forceURL);
                    Redirect(sInstallFolder + adminPath + "login.aspx");
                }
                else
                {
                    Session.setCookie("returnUrl", GetPageName() + "?" + Request.ServerVariables("QUERY_STRING"));
                    sURL = sInstallFolder + adminPath + "login.aspx";
                    Response.Write("<form action=\"" + sURL + "\" name=\"frmReqLogin\" id=\"frmReqLogin\">");
                    foreach (var Item_loopVariable in Request.Form())
                    {
                        Item = Item_loopVariable;
                        intFormCount = intFormCount + 1;
                        Response.Write("<input name=\"" + Strings.ToString(Item) + "\" id=\"" + Strings.ToString(Item) + "\" value=\"" + HTMLEncode(Request.Form(Strings.ToString(Item))) + "\"/>");
                    }

                    Response.Write("</form>");
                    if (intFormCount == 0)
                    {
                        Redirect(sURL);
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\" language=\"JavaScript\">document.frmReqLogin.submit();</script>");
                    }
                }
            }
            else
            {
                if (DoLogin && CompareKeys(ModuleKeys, userId: Session_User_ID()) == false)
                {
                    Redirect(sInstallFolder + "memberships.aspx?DoAction=Restricted");
                }
            }
        }

        /// <summary>
        /// RSS feed content.
        /// </summary>
        /// <param name="path">Full pathname of the file.</param>
        /// <param name="feedFormat">[in,out] The feed format.</param>
        /// <returns>A string.</returns>
        public static string RSS_Feed_Content(string path, ref string feedFormat)
        {
            string content = null;

            try
            {
                if (path.StartsWith("http:", StringComparison.CurrentCultureIgnoreCase) || path.StartsWith("https:", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var down = new WebClient())
                    {
                        down.Encoding = Encoding.UTF8;
                        down.Proxy = WebRequest.DefaultWebProxy;
                        var myUri = new Uri(path);
                        content = down.DownloadString(myUri);
                    }
                }
                else
                {
                    content = File.ReadAllText(path);
                }

                var lastTag = content.LastIndexOf("</", StringComparison.OrdinalIgnoreCase);
                if (lastTag > -1)
                {
                    feedFormat = content.Substring(lastTag + 2);
                }

                return content;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// RSS feed get.
        /// </summary>
        /// <param name="path">Full pathname of the file.</param>
        /// <param name="sTemplate">The template.</param>
        /// <returns>A string.</returns>
        public static string RSS_Feed_Get(string path, string sTemplate)
        {
            long aa = 0;
            long iRecords = 0;
            string[] arrURL = null;

            if (Strings.InStr(path, "||") > 0)
            {
                arrURL = Strings.Split(path, "||");
                Array.Resize(ref arrURL, 2);
                path = arrURL[0];
                iRecords = toLong(arrURL[1]);
            }

            var feedFormat = string.Empty;
            var content = RSS_Feed_Content(path, ref feedFormat);
            var str = new StringBuilder();
            var sSend = string.Empty;

            if (!string.IsNullOrWhiteSpace(content))
            {
                string container = null;
                string[] keys = null;
                string[] fields = null;
                ArrayList results;
                var maxRecords = 10;
                var isList = false;

                if (path == "http://www.astrology.com/us/offsite/rss/daily-extended.aspx")
                {
                    maxRecords = 12;
                }

                if (feedFormat.StartsWith("rss", StringComparison.OrdinalIgnoreCase) || feedFormat.StartsWith("rdf", StringComparison.OrdinalIgnoreCase))
                {
                    container = "item";
                    keys = new[] { "title", "url", "description", "date" };
                    fields = new[] { "title", "link", "description", "pubDate" };
                }
                else if (feedFormat.StartsWith("feed", StringComparison.OrdinalIgnoreCase))
                {
                    container = "entry";
                    keys = new[] { "title", "url", "description", "date" };
                    fields = new[] { "title", "link@href", "content", "(published|issued)" };
                }
                else if (feedFormat.StartsWith("opml", StringComparison.OrdinalIgnoreCase))
                {
                    container = "outline";
                    keys = new[] { "url" };
                    fields = new[] { "outline@xmlUrl" };
                    isList = true;
                }
                else
                {
                    str.Append(new string('x', 80));
                    str.Append("no implementation for " + feedFormat);
                    str.Append(new string('x', 80));
                    return Strings.ToString(str);
                }

                results = Parse(content, container, keys, fields, maxRecords, true);
                if (isList)
                {
                    foreach (NameValueCollection result in results)
                    {
                        RSS_Feed_Get(result["url"], sTemplate);
                    }
                }
                else
                {
                    foreach (NameValueCollection result in results)
                    {
                        aa = aa + 1;
                        if (!string.IsNullOrWhiteSpace(sTemplate))
                        {
                            if (string.IsNullOrWhiteSpace(result["url"]))
                            {
                                sSend = Strings.Replace(sTemplate, "[[LINK]]", UrlDecode(result["link"]));
                            }
                            else
                            {
                                sSend = Strings.Replace(sTemplate, "[[LINK]]", UrlDecode(result["url"]));
                            }

                            sSend = Strings.Replace(sSend, "[[TITLE]]", result["title"]);
                            sSend = Strings.Replace(sSend, "[[DATE]]", result["date"]);
                            sSend = Strings.Replace(sSend, "[[DESCRIPTION]]", HTMLDecode(result["description"]));
                            str.Append(sSend);
                        }
                        else
                        {
                            str.Append("<div class=\"rssfeed\">");
                            str.Append("<span class=\"rsstitle\">" + result["title"] + "</span><br/>");
                            str.Append("<span class=\"rssdate\">" + LangText("Date/Time:") + " " + result["date"] + "</span><br/>");
                            str.Append("<span class=\"rssdescription\">" + HTMLDecode(result["description"]) + "</span><br/>");
                            if (string.IsNullOrWhiteSpace(result["url"]))
                            {
                                if (!string.IsNullOrWhiteSpace(result["link"]))
                                {
                                    str.Append("<span class=\"rsslink\"><a href=\"" + UrlDecode(result["link"]) + "\" target=\"_blank\">" + LangText("Read More") + "</a></span><br/>");
                                }
                            }
                            else
                            {
                                str.Append("<span class=\"rsslink\"><a href=\"" + UrlDecode(Strings.Left(result["url"], 4) != "http" ? "http://" : string.Empty + result["url"]) + "\" target=\"_blank\">" + LangText("Read More") + "</a></span><br/>");
                            }

                            str.Append("</div>");
                        }

                        if (iRecords > 0 && aa == iRecords)
                        {
                            break;
                        }
                    }
                }
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// RSS feed parse.
        /// </summary>
        /// <param name="apstring">The apstring.</param>
        /// <returns>A string.</returns>
        public static string RSS_Feed_Parse(string apstring)
        {
            // ERROR: Not supported in C#: OnErrorStatement

            // -= LEAVE THIS =-
            string[] arrRSS = null;
            var PageText = string.Empty;

            var pos = 0;

            arrRSS = Strings.Split(apstring, "[RSS]");

            if (arrRSS != null)
            {
                for (var i = Information.LBound(arrRSS); i <= Information.UBound(arrRSS); i++)
                {
                    pos = Strings.InStr(arrRSS[i], "[/RSS]") - 1;
                    if (pos > 0)
                    {
                        PageText += RSS_Feed_Read(Strings.Left(arrRSS[i], pos));
                        PageText += Strings.Right(arrRSS[i], Strings.Len(arrRSS[i]) - pos - 6);
                    }
                    else
                    {
                        PageText += arrRSS[i];
                    }
                }
            }

            return PageText;
        }

        /// <summary>
        /// RSS feed read.
        /// </summary>
        /// <param name="sUrl">URL of the resource.</param>
        /// <param name="sTemplate">(Optional) The template.</param>
        /// <returns>A string.</returns>
        public static string RSS_Feed_Read(string sUrl, string sTemplate = "")
        {
            var GetRSS = string.Empty;
            var GetNewRSS = false;

            sUrl = UrlDecode(sUrl);

            if (Strings.Left(sUrl, 7) != "http://" && Strings.Left(sUrl, 8) != "https://")
            {
                sUrl = "http://" + sUrl;
            }

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT DatePosted, ScriptText FROM Scripts WHERE ScriptType='RSSFEED' AND ScriptText LIKE '%<RSSURL>" + sUrl + "</RSSURL>%'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            if (DateAndTime.DateDiff(DateAndTime.DateInterval.Hour, Convert.ToDateTime(openNull(RS["DatePosted"])), DateTime.Now) >= 1)
                            {
                                GetNewRSS = true;
                            }
                            else
                            {
                                GetRSS = openNull(RS["ScriptText"]);
                            }
                        }
                        else
                        {
                            GetNewRSS = true;
                        }

                    }
                }
            }

            if (GetNewRSS)
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM Scripts WHERE ScriptType='RSSFEED' AND ScriptText LIKE '%<RSSURL>" + sUrl + "</RSSURL>%'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                GetRSS = RSS_Feed_Get(sUrl, sTemplate);
            }

            return GetRSS;
        }

        /// <summary>
        /// Securities.
        /// </summary>
        /// <param name="OptionValue">The option value.</param>
        /// <returns>A string.</returns>
        public static string Security(string OptionValue)
        {
            if (string.IsNullOrWhiteSpace(strSecurityXML))
            {
                try
                {
                    if (File.Exists(GetDirValue("app_data") + "security.xml"))
                    {
                        var doc = new XmlDocument();

                        doc.Load(GetDirValue("app_data") + "security.xml");

                        var root = doc.DocumentElement;

                        strSecurityXML = root.InnerXml;

                        doc = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
                catch
                {
                    if (Strings.InStr(OptionValue, "Admin") > 0 || Strings.InStr(OptionValue, "Manage") > 0)
                    {
                        return "|2|";
                    }

                    return "|1|";
                }
            }

            if (!string.IsNullOrWhiteSpace(strSecurityXML))
            {
                return ParseXML(OptionValue, strSecurityXML);
            }

            return "|1|";
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="strTo">to.</param>
        /// <param name="strFrom">Source for the.</param>
        /// <param name="strSubject">The subject.</param>
        /// <param name="strMessage">The message.</param>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="strFile">(Optional) The file.</param>
        /// <param name="UniqueID">(Optional) Unique identifier.</param>
        public static void Send_Email(string strTo, string strFrom, string strSubject, string strMessage, int ModuleID, string strFile = "", long UniqueID = 0)
        {
            if (string.IsNullOrWhiteSpace(strTo) || Strings.InStr(strTo, "@") == 0 || Strings.InStr(strTo, ".") == 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(strFrom) || Strings.InStr(strFrom, "@") == 0 || Strings.InStr(strFrom, ".") == 0)
            {
                return;
            }

            var iPort = 25;
            var sServerIP = string.Empty;
            var sServerUser = string.Empty;
            var sServerPass = string.Empty;

            if (File.Exists(GetDirValue("app_data") + "system.xml"))
            {
                var doc = new XmlDocument();
                doc.Load(GetDirValue("app_data") + "system.xml");

                var root = doc.DocumentElement;
                var sXML = root.InnerXml;

                if (!string.IsNullOrWhiteSpace(ParseXML("MailServerPort", sXML)))
                {
                    iPort = toInt(ParseXML("MailServerPort", sXML));
                }

                if (iPort == 0)
                {
                    iPort = 25;
                }

                sServerIP = ParseXML("MailServerIP", sXML);
                sServerUser = ParseXML("MailServerUser", sXML);
                sServerPass = Decrypt(ParseXML("MailServerPass", sXML));

                doc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                iPort = 25;
                sServerIP = "127.0.0.1";
            }

            try
            {
                SmtpClient client = null;
                using (client = new SmtpClient(sServerIP, iPort))
                {
                    if (!string.IsNullOrWhiteSpace(sServerUser) && !string.IsNullOrWhiteSpace(sServerPass))
                    {
                        client.Credentials = new NetworkCredential(sServerUser, sServerPass);
                    }

                    MailMessage msg = null;
                    using (msg = new MailMessage(strFrom, strTo))
                    {
                        msg.Subject = strSubject;
                        msg.Body = HTMLDecode(strMessage);
                        msg.IsBodyHtml = true;
                        msg.Priority = MailPriority.Normal;

                        if (UniqueID > 0)
                        {
                            using (var conn = new SqlConnection(Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("SELECT FileName, FileData FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UniqueID", UniqueID);
                                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                                    using (SqlDataReader mailRS = cmd.ExecuteReader())
                                    {
                                        while (mailRS.Read())
                                        {
                                            var data = (byte[])mailRS["FileData"];
                                            var ms = new MemoryStream(data);
                                            msg.Attachments.Add(new Attachment(ms, openNull(mailRS["FileName"]), "text/plain"));
                                        }
                                    }
                                }

                                using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID='4'", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UniqueID", UniqueID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(strFile))
                        {
                            var MsgAttach = new Attachment(strFile);
                            msg.Attachments.Add(MsgAttach);
                        }

                        client.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug_Log("Error in Send_Email(). (" + ex.Message + ")");
            }
        }

        /// <summary>
        /// Sends an error.
        /// </summary>
        /// <param name="objErr">The object error.</param>
        /// <returns>A string.</returns>
        public static string SendError(Exception objErr)
        {
            return objErr.Message + "<br/><br/>" + objErr.StackTrace + "<br/><br/>" + objErr.TargetSite;
        }

        /// <summary>
        /// Sends a generic error.
        /// </summary>
        /// <param name="ErrorID">Identifier for the error.</param>
        /// <returns>A string.</returns>
        public static string SendGenericError(long ErrorID)
        {
            var errorTitle = "An Error Has Occurred";
            var errorMsg = "An unexpected error occurred on our website. The website administrator has been notified.";

            string sInstallFolder = GetInstallFolder();

            switch (ErrorID)
            {
                case 400:
                    errorTitle = "400 Bad Request";
                    errorMsg = "You have made an invalid request to this page.";
                    break;

                case 403:
                    if (string.IsNullOrWhiteSpace(Session_User_ID()))
                    {
                        if (Request.UrlReferrer() != null && Request.UrlReferrer_AbsolutePath() != null)
                        {
                            Session.setCookie("returnUrl", Request.UrlReferrer_AbsolutePath());
                        }

                        Redirect(sInstallFolder + "login.aspx");
                    }

                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ShopProducts,AccessClasses WHERE AccessClasses.ClassID=LTRIM(RTRIM(right(ShopProducts.ModelNumber, len(ShopProducts.ModelNumber)-charindex('-', ShopProducts.ModelNumber)))) AND ShopProducts.ModuleID='38' AND ShopProducts.Status <> -1 AND AccessClasses.Status <> -1 AND AccessClasses.PrivateClass='0' AND (AccessClasses.PortalIDs LIKE '%|" + Get_Portal_ID() + "|%' OR AccessClasses.PortalIDs LIKE '%|-1|%' OR datalength(AccessClasses.PortalIDs) = 0)", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    Redirect(sInstallFolder + "memberships.aspx?DoAction=Paying");
                                }
                                else
                                {
                                    errorTitle = "403 Forbidden / Access Denied";
                                    errorMsg = "You do not have access to this page.";
                                }

                            }
                        }
                    }

                    break;

                case 404:
                    errorTitle = "404 Not Found";
                    errorMsg = "The resource you are looking for might have been removed, had its name changed, or is temporarily unavailable.";
                    break;

                case 989398917283746L:
                    errorTitle = "Portal Disabled";
                    errorMsg = "This portal has been disabled by the Administrator.";
                    break;
            }

            return "<h1>" + errorTitle + "</h1><p>" + errorMsg + "</p>";
        }

        /// <summary>
        /// Sessions the clear invoice identifier.
        /// </summary>
        public static void Session_Clear_Invoice_ID()
        {
            if (!string.IsNullOrWhiteSpace(Session.getSession(Strings.Left(Setup(992, "WebSiteName"), 5) + "InvoiceID")))
            {
                Session.setSession(Strings.Left(Setup(992, "WebSiteName"), 5) + "InvoiceID", string.Empty);
            }
        }

        /// <summary>
        /// Session invoice identifier.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_Invoice_ID()
        {
            long sInvoiceId = 0;

            if (string.IsNullOrWhiteSpace(Session.getSession(Strings.Left(Setup(992, "WebSiteName"), 5) + "InvoiceID")))
            {
                if (!string.IsNullOrWhiteSpace(Session_User_ID()))
                {
                    using (var conn = new SqlConnection(Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE UserID=@UserID AND Status='0' AND inCart='1'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session_User_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    sInvoiceId = toLong(openNull(RS["InvoiceID"]));
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                sInvoiceId = toLong(Strings.Replace(Session.getSession(Strings.Left(Setup(992, "WebSiteName"), 5) + "InvoiceID"), "'", string.Empty));
            }

            if (sInvoiceId == 0)
            {
                sInvoiceId = GetIdentity();
            }

            Session.setSession(Strings.Left(Setup(992, "WebSiteName"), 5) + "InvoiceID", Strings.ToString(sInvoiceId));

            return Strings.ToString(sInvoiceId);
        }

        /// <summary>
        /// Session password.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_Password()
        {
            var sPassword = Strings.Replace(Session.getSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "Password"), "'", string.Empty);
            if (string.IsNullOrWhiteSpace(sPassword))
            {
                sPassword = Session_Password_Cookie();
            }

            return sPassword;
        }

        /// <summary>
        /// Session password cookie.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_Password_Cookie()
        {
            var sUserInfo = Strings.Split(Strings.Replace(Session.getCookie("UserInfo"), "'", string.Empty), "||");
            Array.Resize(ref sUserInfo, 3);
            if (!string.IsNullOrWhiteSpace(sUserInfo[2]))
            {
                Session.setSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "Password", sUserInfo[2]);
            }

            return sUserInfo[2];
        }

        /// <summary>
        /// Session user identifier.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_User_ID()
        {
            var sUserID = Strings.Replace(Session.getSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "UserID"), "'", string.Empty);
            if (string.IsNullOrWhiteSpace(sUserID))
            {
                sUserID = Session_User_ID_Cookie();
            }

            return sUserID;
        }

        /// <summary>
        /// Session user identifier cookie.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_User_ID_Cookie()
        {
            try
            {
                var sUserInfo = Strings.Split(Strings.Replace(Session.getCookie("UserInfo"), "'", string.Empty), "||");
                Array.Resize(ref sUserInfo, 3);
                if (!string.IsNullOrWhiteSpace(sUserInfo[0]))
                {
                    Session.setSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "UserID", sUserInfo[0]);
                }

                return sUserInfo[0];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Session user name.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_User_Name()
        {
            var sUsername = Strings.Replace(Session.getSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "Username"), "'", string.Empty);
            if (string.IsNullOrWhiteSpace(sUsername))
            {
                sUsername = Session_User_Name_Cookie();
            }

            return sUsername;
        }

        /// <summary>
        /// Session user name cookie.
        /// </summary>
        /// <returns>A string.</returns>
        public static string Session_User_Name_Cookie()
        {
            var sUserInfo = Strings.Split(Strings.Replace(Session.getCookie("UserInfo"), "'", string.Empty), "||");
            Array.Resize(ref sUserInfo, 3);
            if (!string.IsNullOrWhiteSpace(sUserInfo[1]))
            {
                Session.setSession(Strings.Left(Strings.Replace(Setup(992, "WebSiteName"), " ", string.Empty), 5) + "Username", sUserInfo[1]);
            }

            return sUserInfo[1];
        }

        /// <summary>
        /// Setups.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="OptionValue">The option value.</param>
        /// <returns>A string.</returns>
        public static string Setup(int intModuleID, string OptionValue)
        {
            if (Strings.InStr(OptionValue, "Enable") > 0)
            {
                if (ModuleActivated(intModuleID) == false)
                {
                    return "Disable";
                }
            }

            if (Strings.InStr(OptionValue, "Enable") > 0 && Get_Portal_ID() > 0 && OptionValue != "CNRCEnable" && OptionValue != "CNRREnable")
            {
                return "Enable";
            }

            if (string.IsNullOrWhiteSpace(strSetupXml))
            {
                if (File.Exists(GetDirValue("app_data") + "settings.xml"))
                {
                    var doc = new XmlDocument();
                    doc.Load(GetDirValue("app_data") + "settings.xml");

                    var root = doc.DocumentElement;

                    strSetupXml = root.InnerXml;

                    doc = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }

            if (!string.IsNullOrWhiteSpace(strSetupXml))
            {
                if (OptionValue == "RecPerAPage" && toLong(ParseXML(OptionValue, strSetupXml)) < 5)
                {
                    return "5";
                }

                return Strings.Trim(ParseXML(OptionValue, strSetupXml));
            }

            return string.Empty;
        }

        /// <summary>
        /// Sets up the array.
        /// </summary>
        /// <param name="OptionValue">The option value.</param>
        /// <returns>A Hashtable.</returns>
        public static Hashtable SetupArray(string OptionValue)
        {
            Hashtable htReturn = new Hashtable();

            if (string.IsNullOrWhiteSpace(strSetupXml))
            {
                if (File.Exists(GetDirValue("app_data") + "settings.xml"))
                {
                    var doc = new XmlDocument();
                    doc.Load(GetDirValue("app_data") + "settings.xml");

                    var root = doc.DocumentElement;

                    strSetupXml = root.InnerXml;

                    doc = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }

            string[] arrOptions = Strings.Split(OptionValue, "|");

            if (!string.IsNullOrWhiteSpace(strSetupXml))
            {
                for (var i = 0; i <= Information.UBound(arrOptions); i++)
                {
                    try
                    {
                        if (arrOptions[i] == "RecPerAPage" && toLong(ParseXML(arrOptions[i], strSetupXml)) < 5)
                        {
                            htReturn.Add(arrOptions[i], "5");
                        }

                        htReturn.Add(arrOptions[i], Strings.Trim(ParseXML(arrOptions[i], strSetupXml)));
                    }
                    catch
                    {
                    }
                }
            }

            return htReturn;
        }

        /// <summary>
        /// Shows the categories.
        /// </summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool showCategories()
        {
            if (ModuleActivated(5) || ModuleActivated(9) || ModuleActivated(10) || ModuleActivated(19) || ModuleActivated(20) || ModuleActivated(31) || ModuleActivated(35) || ModuleActivated(37) || ModuleActivated(41) || ModuleActivated(44) || ModuleActivated(52))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Time long difference.
        /// </summary>
        /// <param name="strDate">The date Date/Time.</param>
        /// <returns>A string.</returns>
        public static string Time_Long_Difference(DateTime strDate)
        {
            var str = new StringBuilder();

            var aa = strDate.Subtract(DateTime.Now);

            var intMonth = DateAndTime.DateDiff(DateAndTime.DateInterval.Month, strDate, DateTime.Now);
            var intDay = Convert.ToInt64(Strings.Replace(Strings.ToString(aa.Days), "-", string.Empty));
            var intHour = Convert.ToInt64(Strings.Replace(Strings.ToString(aa.Hours), "-", string.Empty));
            var intMinute = Convert.ToInt64(Strings.Replace(Strings.ToString(aa.Minutes), "-", string.Empty));

            if (intMonth > 0)
            {
                str.Append(Strings.FormatNumber(intMonth, 0) + " " + LangText("month" + Strings.ToString(intMonth == 1 ? string.Empty : "s")) + ", ");
            }

            if (intDay > 0)
            {
                if (intDay > 729)
                {
                    intDay = intDay - 730;
                }

                if (intDay > 364)
                {
                    intDay = intDay - 365;
                }

                if (intDay > 29)
                {
                    intDay = Convert.ToInt64(intDay / 30);
                }

                str.Append(Strings.FormatNumber(intDay, 0) + " " + LangText("day" + Strings.ToString(intDay == 1 ? string.Empty : "s")) + ", ");
            }

            if (intHour > 0)
            {
                str.Append(Strings.FormatNumber(intHour, 0) + " " + LangText("hour" + Strings.ToString(intHour == 1 ? string.Empty : "s")) + ", ");
            }

            str.Append(Strings.FormatNumber(intMinute, 0) + " " + LangText("minute" + Strings.ToString(intMinute == 1 || intMinute == 0 ? string.Empty : "s")));

            return Strings.Trim(Strings.ToString(str));
        }

        /// <summary>
        /// Time ago.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        public static string TimeAgo(DateTime value)
        {
            var span = DateTime.Now - Convert.ToDateTime(value);
            if (span.Days > 365)
            {
                var years = span.Days / 365;
                if (span.Days % 365 != 0)
                {
                    years += 1;
                }

                return string.Format("about {0} {1} ago", years, years == 1 ? "year" : "years");
            }

            if (span.Days > 30)
            {
                var months = span.Days / 30;
                if (span.Days % 31 != 0)
                {
                    months += 1;
                }

                return string.Format("about {0} {1} ago", months, months == 1 ? "month" : "months");
            }

            if (span.Days > 0)
            {
                return string.Format("about {0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");
            }

            if (span.Hours > 0)
            {
                return string.Format("about {0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");
            }

            if (span.Minutes > 0)
            {
                return string.Format("about {0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            }

            if (span.Seconds > 5)
            {
                return string.Format("about {0} seconds ago", span.Seconds);
            }

            if (span.Seconds <= 5)
            {
                // -V3022
                return "just now";
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts a value to a boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as a bool.</returns>
        public static bool toBoolean(string value)
        {
            if (!bool.TryParse(value, out bool outValue))
            {
                return outValue;
            }

            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Converts a value to a date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as a DateTime.</returns>
        public static DateTime toDate(string value)
        {
            var outValue = DateTime.Now;
            if (!DateTime.TryParse(value, out outValue))
            {
                return outValue;
            }

            return Convert.ToDateTime(value);
        }

        /// <summary>
        /// Converts a value to a decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as a decimal.</returns>
        public static decimal toDecimal(string value)
        {
            if (!decimal.TryParse(ParseDigits(value), out decimal outValue))
            {
                return outValue;
            }

            return Convert.ToDecimal(ParseDigits(value));
        }

        /// <summary>
        /// Converts a value to a double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as a double.</returns>
        public static double toDouble(string value)
        {
            if (!double.TryParse(ParseDigits(value), out double outValue))
            {
                return outValue;
            }

            return Convert.ToDouble(ParseDigits(value));
        }

        /// <summary>
        /// Converts a value to an int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as an int.</returns>
        public static int toInt(string value)
        {
            if (!int.TryParse(value, out int outValue))
            {
                return outValue;
            }

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts a value to a long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as a long.</returns>
        public static long toLong(string value)
        {
            if (!long.TryParse(value, out long outValue))
            {
                return outValue;
            }

            return Convert.ToInt64(value);
        }

        /// <summary>
        /// Converts a value to a percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Value as a string.</returns>
        public static string toPercent(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "0%";
            }

            return ParseDigits(value) + "%";
        }

        /// <summary>
        /// Determines if we can twilio activated.
        /// </summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool TwilioActivated()
        {
            if (string.IsNullOrWhiteSpace(Setup(989, "TwilioAccountSID")) && string.IsNullOrWhiteSpace(Setup(989, "TwilioAuthToken")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Uploads a SQL select.
        /// </summary>
        /// <param name="strUniqueID">Unique identifier.</param>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="strSQLReturn">(Optional) The SQL return.</param>
        /// <param name="sUserID">(Optional) Identifier for the user.</param>
        /// <returns>A string.</returns>
        public static string Upload_SQL_Select(string strUniqueID, int intModuleID, string strSQLReturn = "FileName", string sUserID = "")
        {
            if (!string.IsNullOrWhiteSpace(strUniqueID))
            {
                strUniqueID = " UniqueID=" + strUniqueID + " AND";
            }

            if (!string.IsNullOrWhiteSpace(sUserID))
            {
                sUserID = " UserID=" + sUserID + " AND";
            }

            return "(SELECT TOP 1 FileName FROM Uploads WHERE" + strUniqueID + sUserID + " ModuleID='" + intModuleID + "' AND Approved='1' AND isTemp='0') AS " + strSQLReturn;
        }

        /// <summary>
        /// User online status.
        /// </summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A string.</returns>
        public static string userOnlineStatus(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ID FROM OnlineUsers WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                return LangText("online");
                            }

                        }
                        return LangText("offline");
                    }
                }
            }

            return LangText("offline");
        }

        /// <summary>
        /// User profile identifier.
        /// </summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A long.</returns>
        public static long userProfileID(string userId)
        {
            long sReturn = 0;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT P.ProfileID FROM Members AS M,Profiles AS P WHERE P.UserID=M.UserID AND M.UserID=@UserID AND M.Status=1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sReturn = toLong(openNull(RS["ProfileID"]));
                        }

                    }
                }
            }

            return sReturn;
        }

        /// <summary>
        /// User profile image.
        /// </summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A string.</returns>
        public static string userProfileImage(string userId)
        {
            var sReturn = string.Empty;

            var sImageFolder = GetInstallFolder(true);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT Male, (SELECT TOP 1 UploadID FROM Uploads WHERE ModuleID='63' AND UniqueID=P.ProfileID AND Uploads.isTemp='0' AND Uploads.Approved='1' AND (Right(FileName,4) = '.png' OR Right(FileName,4) = '.gif' OR Right(FileName,4) = '.jpg' OR Right(FileName,5) = '.jpeg') ORDER BY Weight) AS UploadID FROM Members AS M, Profiles AS P WHERE P.UserID=M.UserID AND M.UserID=@UserID AND M.Status=1", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (!string.IsNullOrWhiteSpace(openNull(RS["UploadID"])))
                                {
                                    sReturn = sImageFolder + "spadmin/show_image.aspx?ModuleID=63&Size=Thumb&UploadID=" + openNull(RS["UploadID"]);
                                }
                                else
                                {
                                    if (openBoolean(RS["Male"]))
                                    {
                                        sReturn = sImageFolder + "images/public/no-photo-male.jpg";
                                    }
                                    else
                                    {
                                        sReturn = sImageFolder + "images/public/no-photo-female.jpg";
                                    }
                                }
                            }
                            else
                            {
                                if (GetUserInformation("Sex", userId) == LangText("Male"))
                                {
                                    sReturn = sImageFolder + "images/public/no-photo-male.jpg";
                                }
                                else
                                {
                                    sReturn = sImageFolder + "images/public/no-photo-female.jpg";
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                if (GetUserInformation("Sex", userId) == LangText("Male"))
                {
                    sReturn = sImageFolder + "images/public/no-photo-male.jpg";
                }
                else
                {
                    sReturn = sImageFolder + "images/public/no-photo-female.jpg";
                }
            }

            return sReturn;
        }

        /// <summary>
        /// User validated.
        /// </summary>
        /// <param name="sUser">The user.</param>
        /// <param name="sPass">The pass.</param>
        /// <param name="adminSecurity">[in,out] The admin security.</param>
        /// <param name="userSecurity">[in,out] The user security.</param>
        /// <returns>A string.</returns>
        public static string UserValidated(string sUser, string sPass, ref string adminSecurity, ref string userSecurity)
        {
            var LoginField = "UserName";
            var sUserID = string.Empty;
            var checkUserSecurity = false;
            var wc = string.Empty;

            if (adminSecurity != userSecurity)
            {
                checkUserSecurity = true;
            }

            if (adminSecurity == "|1|" || Strings.InStr(Security(adminSecurity), "|1|") > 0)
            {
                adminSecurity = "True";
            }

            if (userSecurity == "|1|" || Strings.InStr(Security(userSecurity), "|1|") > 0)
            {
                userSecurity = "True";
            }

            if (!string.IsNullOrWhiteSpace(sUser) && !string.IsNullOrWhiteSpace(sPass))
            {
                if (Setup(997, "LoginEmail") == "Yes")
                {
                    LoginField = "EmailAddress";
                }

                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE " + LoginField + "=@User AND Password=@Pass AND Status=1" + wc, conn))
                    {
                        cmd.Parameters.AddWithValue("@User", sUser);
                        cmd.Parameters.AddWithValue("@Pass", sPass);
                        using (SqlDataReader ValidateRS = cmd.ExecuteReader())
                        {
                            if (ValidateRS.HasRows)
                            {
                                ValidateRS.Read();
                                sUserID = openNull(ValidateRS["UserID"]);
                            }
                        }
                    }
                }
            }

            if (adminSecurity != "True" && userSecurity != "True")
            {
                if (!string.IsNullOrWhiteSpace(sUserID))
                {
                    if (adminSecurity == "|1|" || CompareKeys(Security(adminSecurity), true, sUserID))
                    {
                        adminSecurity = "True";
                    }
                    else
                    {
                        adminSecurity = string.Empty;
                    }

                    if (checkUserSecurity)
                    {
                        if (userSecurity == "|1|" || CompareKeys(Security(userSecurity), true, sUserID))
                        {
                            userSecurity = "True";
                        }
                    }
                    else
                    {
                        userSecurity = adminSecurity;
                    }
                }
            }

            return sUserID;
        }

        /// <summary>
        /// Validates the custom fields described by intModuleID.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <returns>A string.</returns>
        public static string Validate_Custom_Fields(int intModuleID)
        {
            var wc = string.Empty;
            var sReturn = string.Empty;

            if (intModuleID != 41)
            {
                wc += " AND (PortalIDs LIKE '%|" + Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0)";
            }

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT FieldID,Required,FieldName FROM CustomFields WHERE ModuleIDs LIKE '%|" + intModuleID + "|%' AND Status <> -1" + wc + " ORDER BY Weight,FieldName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                if (openBoolean(RS["Required"]))
                                {
                                    if (string.IsNullOrWhiteSpace(Request.Item("Custom" + openNull(RS["FieldID"]))))
                                    {
                                        sReturn += "<div class=\"alert alert-danger\" role=\"alert\">" + LangText("~~" + openNull(RS["FieldName"]) + "~~ is required.") + "</div>";
                                    }
                                }
                            }
                        }

                    }
                }
            }

            return sReturn;
        }

        /// <summary>
        /// Website name.
        /// </summary>
        /// <param name="iPortalID">Identifier for the portal.</param>
        /// <returns>A string.</returns>
        public static string WebsiteName(long iPortalID)
        {
            var sName = string.Empty;

            if (iPortalID < 1)
            {
                sName = Setup(992, "WebsiteName");
            }
            else
            {
                using (var conn = new SqlConnection(Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT PortalTitle FROM Portals WHERE PortalID='" + iPortalID + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sName = openNull(RS["PortalTitle"]);
                            }

                        }
                    }
                }
            }

            return sName;
        }

        /// <summary>
        /// Writes a rating.
        /// </summary>
        /// <param name="intModuleID">Identifier for the int module.</param>
        /// <param name="intUniqueID">Unique identifier for the int.</param>
        /// <param name="sUserID">(Optional) Identifier for the user.</param>
        /// <param name="CheckVisitor">(Optional) True to check visitor.</param>
        public static void Write_Rating(int intModuleID, string intUniqueID, string sUserID = "", bool CheckVisitor = false)
        {
            if (string.IsNullOrWhiteSpace(sUserID))
            {
                sUserID = Session_User_ID();
            }

            if (CheckVisitor)
            {
                Activity_Write("UVISITOR", LangText("[[Username]] has visited your web site") + Environment.NewLine, intModuleID, intUniqueID, sUserID);
            }
            else
            {
                Activity_Write("RATING", LangText("[[Username]] has rated a listing") + Environment.NewLine, intModuleID, intUniqueID, sUserID);
            }

            if (string.IsNullOrWhiteSpace(sUserID))
            {
                Session.setSession("RATE" + intModuleID + intUniqueID, "Yes");
            }
        }
    }
}