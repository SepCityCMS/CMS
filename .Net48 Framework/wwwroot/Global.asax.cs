// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Global.asax.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using CuteChat;
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;
    using ChatProvider = CuteChat.ChatProvider;
    using Strings = SepCommon.SepCore.Strings;

    /// <summary>
    /// Class Global_asax.
    /// Implements the <see cref="System.Web.HttpApplication" />
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class Global_asax : HttpApplication
    {
        /// <summary>
        /// The bg thread
        /// </summary>
        public Thread bgThread;

        /// <summary>
        /// Handles the AcquireRequestState event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null)
            {
                return;
            }

            var currentURL = Strings.LCase(Request.Path);

            if (Strings.InStr(currentURL, "install/") > 0 || Strings.InStr(currentURL, "help/") > 0 || Strings.InStr(currentURL, "spadmin/") > 0)
            {
                return;
            }

            var sInstallFolder = "";

            try
            {
                sInstallFolder = SepFunctions.GetInstallFolder();
            }
            catch
            {

            }

            if (!File.Exists(SepFunctions.GetDirValue("app_data") + "license.xml"))
            {
                SepFunctions.Redirect(sInstallFolder + "install/default.aspx");
                return;
            }

            if (Directory.Exists(HostingEnvironment.MapPath("~/install/")))
            {
                SepFunctions.Redirect(sInstallFolder + "install/delfolder.aspx");
                return;
            }

            var iModuleID = GlobalVars.ModuleID;
            var GetPageTitle = string.Empty;

            long iPageID = 0;

            var intPortalID = SepFunctions.Get_Portal_ID();

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("UniqueID")) > 0 || SepFunctions.toLong(SepCommon.SepCore.Request.Item("ID")) > 0)
            {
                iModuleID = 0;
                iPageID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("UniqueID")) > 0 ? SepFunctions.toLong(SepCommon.SepCore.Request.Item("UniqueID")) : SepFunctions.toLong(SepCommon.SepCore.Request.Item("ID"));
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                string adId = !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")) ? SepCommon.SepCore.Request.Item("AdID") : SepCommon.SepCore.Request.Item("ID");

                if (SepCommon.SepCore.Request.Item("DoAction") == "AdRedirect" && !string.IsNullOrWhiteSpace(adId))
                    using (var cmd = new SqlCommand("SELECT SiteURL,TotalClicks FROM Advertisements WHERE AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", adId);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                using (var cmd2 = new SqlCommand("UPDATE Advertisements SET TotalClicks='" + (SepFunctions.toLong(SepFunctions.openNull(RS["TotalClicks"])) + 1) + "' WHERE AdID=@AdID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@AdID", !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")) ? SepCommon.SepCore.Request.Item("AdID") : SepCommon.SepCore.Request.Item("ID"));
                                    cmd2.ExecuteNonQuery();
                                    SepFunctions.Redirect(SepFunctions.openNull(RS["SiteURL"]));
                                }
                            }
                        }
                    }

                if (intPortalID > 0 && string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ErrorID")))
                    using (var cmd = new SqlCommand("SELECT PortalID FROM Portals WHERE PortalID=@PortalID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", intPortalID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows) SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=989398917283746");
                        }
                    }

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AffiliateID")))
                {
                    Session["ReferralID"] = SepCommon.SepCore.Request.Item("AffiliateID");
                }
                else
                {
                    if (SepFunctions.isUserPage())
                        if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"))))
                        {
                            var userPageUserID = SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"));
                            if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("ReferralID", userPageUserID))) Session["ReferralID"] = SepFunctions.GetUserInformation("ReferralID", userPageUserID);
                        }
                }

                if (Context.Session["UserLoaded"] == null)
                {
                    if (Strings.InStr(Context.Request.Url.AbsolutePath, "error.aspx") == 0 && !string.IsNullOrWhiteSpace(SepFunctions.GetUserIP()))
                        using (var cmd = new SqlCommand("SELECT TOP 1 ActivityID FROM Activities WHERE IPAddress=@IPAddress AND ActType='BANNEDIP' AND ModuleID='999' AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows) SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=304");
                            }
                        }

                    using (var cmd = new SqlCommand("DELETE FROM OnlineUsers WHERE LastActive < @LastActive", conn))
                    {
                        cmd.Parameters.AddWithValue("@LastActive", DateAndTime.DateAdd(DateAndTime.DateInterval.Hour, -2, DateTime.Now));
                        cmd.ExecuteNonQuery();
                    }

                    if (SepFunctions.CheckPriSite())
                        if (Strings.InStr(Context.Request.ServerVariables["SCRIPT_NAME"], "default.aspx") == 0 && Strings.InStr(Context.Request.ServerVariables["SCRIPT_NAME"], "login.aspx") == 0 && Strings.InStr(Context.Request.ServerVariables["SCRIPT_NAME"], "contactus.aspx") == 0 && Strings.InStr(Context.Request.ServerVariables["SCRIPT_NAME"], "account.aspx") == 0 && Strings.InStr(Context.Request.ServerVariables["SCRIPT_NAME"], "signup.aspx") == 0)
                            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                            {
                                SepFunctions.Redirect(sInstallFolder + "login.aspx");
                                {
                                    return;
                                }
                            }
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {

                    string SqlStr;
                    if (intPortalID == 0)
                        try
                        {
                            if (iPageID == 0)
                                SqlStr = "SELECT TOP 1 LinkText FROM ModulesNPages WHERE ModuleID='" + iModuleID + "'";
                            else
                                SqlStr = "SELECT TOP 1 LinkText FROM ModulesNPages WHERE UniqueID='" + iPageID + "' AND PageID='200'";
                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        GetPageTitle = SepFunctions.openNull(RS["LinkText"]);
                                    }

                                }
                            }
                        }
                        catch
                        {
                        }
                    else
                        try
                        {
                            if (iPageID == 0)
                                SqlStr = "SELECT TOP 1 LinkText FROM PortalPages WHERE PageID='" + iModuleID + "' AND (PortalIDs LIKE '%|" + intPortalID + "|%' OR PortalIDs LIKE '%|-1|%')";
                            else
                                SqlStr = "SELECT TOP 1 LinkText FROM PortalPages WHERE PageID='" + iPageID + "' AND (PortalIDs LIKE '%|" + intPortalID + "|%' OR PortalIDs LIKE '%|-1|%')";
                            using (var cmd = new SqlCommand(SqlStr, conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        GetPageTitle = SepFunctions.openNull(RS["LinkText"]);
                                    }

                                }
                            }
                        }
                        catch
                        {
                        }

                    using (var cmd = new SqlCommand("SELECT UserID FROM OnlineUsers WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                                using (var cmd2 = new SqlCommand("INSERT INTO OnlineUsers(UserID, Location, LoginTime, LastActive, isChatting) VALUES ('" + SepFunctions.Session_User_ID() + "', 'Login', GetDate(), GetDate(), '0')", conn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }
                            else
                                using (var cmd2 = new SqlCommand("UPDATE OnlineUsers SET LastActive=GetDate(), CurrentStatus=''" + Strings.ToString(!string.IsNullOrWhiteSpace(GetPageTitle) ? ", Location='" + SepFunctions.FixWord(GetPageTitle) + "'" : string.Empty) + " WHERE UserID='" + SepFunctions.Session_User_ID() + "'", conn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReferralFrom")) && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReferralTo")))
                {
                    long AddNewHit = 0;
                    using (var cmd = new SqlCommand("SELECT Visited FROM ReferralAddresses WHERE ToEmailAddress=@ToEmailAddress", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToEmailAddress", SepCommon.SepCore.Request.Item("ReferralTo"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (SepFunctions.openBoolean(RS["Visited"]) == false)
                                    using (var cmd2 = new SqlCommand("SELECT Visitors FROM ReferralStats WHERE FromEmailAddress=@FromEmailAddress", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@FromEmailAddress", SepCommon.SepCore.Request.Item("ReferralFrom"));
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                AddNewHit = SepFunctions.toLong(SepFunctions.openNull(RS2["Visitors"])) + 1;
                                                using (var cmd3 = new SqlCommand("UPDATE ReferralStats SET Visitors=@Visitors WHERE FromEmailAddress=@FromEmailAddress", conn))
                                                {
                                                    cmd3.Parameters.AddWithValue("@Visitors", AddNewHit);
                                                    cmd3.Parameters.AddWithValue("@FromEmailAddress", SepCommon.SepCore.Request.Item("ReferralFrom"));
                                                    cmd3.ExecuteNonQuery();
                                                }

                                                using (var cmd3 = new SqlCommand("UPDATE ReferralAddresses SET Visited='1' Where ToEmailAddress=@ToEmailAddress", conn))
                                                {
                                                    cmd3.Parameters.AddWithValue("@ToEmailAddress", SepCommon.SepCore.Request.Item("ReferralTo"));
                                                    cmd3.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                            }

                        }
                    }
                }
            }

            SepCommon.SepCore.Session.setSession("UserLoaded", "1");
        }

        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var currentURL = Strings.LCase(HttpContext.Current.Request.Path);

            HttpApplication app = sender as HttpApplication;
            if (currentURL != "/downloads_view_video.aspx")
            {
                app.Context.Response.Headers.Add("X-Frame-Options", "sameorigin");
            }

            var rewritePage = Globals.RedirectURL(currentURL);

            if (!string.IsNullOrWhiteSpace(rewritePage)) HttpContext.Current.RewritePath(rewritePage);
        }

        /// <summary>
        /// Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Application_End(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(HostingEnvironment.MapPath("~/install/"))) return;

                bgThread.Abort();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Applications the post authorize request.
        /// </summary>
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest()) HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            if (Directory.Exists(HostingEnvironment.MapPath("~/install/"))) return;

            try
            {
                GlobalConfiguration.Configure(
                    config =>
                        {
                            AreaRegistration.RegisterAllAreas();
                            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                            WebApiConfig.Register(config, RouteTable.Routes);
                        });
            }
            catch
            {
            }

            try
            {
                ChatProvider.Instance = new SepCommon.ChatProvider();
                ChatSystem.Start(new AppSystem());
            }
            catch
            {
            }

            try
            {
                BackgroundProcesses cProcesses = new BackgroundProcesses();
                ThreadStart objThreadStart = new ThreadStart(cProcesses.Start);
                bgThread = new Thread(objThreadStart);
                bgThread.Start();
            }
            catch
            {
                bgThread.Abort();
            }
        }

        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Determines whether [is web API request].
        /// </summary>
        /// <returns><c>true</c> if [is web API request]; otherwise, <c>false</c>.</returns>
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative, StringComparison.OrdinalIgnoreCase);
        }
    }
}