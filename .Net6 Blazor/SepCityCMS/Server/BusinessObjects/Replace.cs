// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Replace.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.BusinessObjects
{
    using Server;
    using Server.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class Replace.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class Replace : IDisposable
    {
        // -V3073
        /// <summary>
        /// To detect redundant calls.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the page text.
        /// </summary>
        /// <param name="GetPageID">The get page identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns>System.String.</returns>
        public string GetPageText(long GetPageID, long ModuleID = 0)
        {
            var str = new StringBuilder();

            var strPageText = string.Empty;

            if (GetPageID == 38)
                return string.Empty;

            var currentURL = Strings.LCase(Request.Path());

            if (Response.IsClientConnected())
            {
                if (Strings.InStr(currentURL, "/viewpage.aspx") > 0 || ModuleID == 0)
                {
                    if (SepFunctions.Get_Portal_ID() == 0)
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT PageText,Visits FROM ModulesNPages WHERE UniqueID='" + GetPageID + "'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        using (var cmd2 = new SqlCommand("UPDATE ModulesNPages SET Visits='" + (SepFunctions.toLong(SepFunctions.openNull(RS["Visits"])) + 1) + "' WHERE UniqueID='" + GetPageID + "'", conn))
                                        {
                                            cmd2.ExecuteNonQuery();
                                        }

                                        strPageText = SepFunctions.openNull(RS["PageText"]);
                                    }
                                }
                            }
                        }
                    else
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT PageText,Visits FROM PortalPages WHERE (PortalID=@PortalID OR PortalID = -1) AND UniqueID='" + GetPageID + "' AND Status=1", conn))
                            {
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        using (var cmd2 = new SqlCommand("UPDATE PortalPages SET Visits='" + (SepFunctions.toLong(SepFunctions.openNull(RS["Visits"])) + 1) + "' WHERE (PortalID=@PortalID OR PortalID = -1) AND UniqueID='" + GetPageID + "' AND Status=1", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                            cmd2.ExecuteNonQuery();
                                        }

                                        strPageText = SepFunctions.openNull(RS["PageText"]);
                                    }
                                }
                            }
                        }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Request.Item("CatID")) && Strings.InStr(Request.Item("CatID"), ",") == 0)
                    {
                        if (string.IsNullOrWhiteSpace(Request.Item("DoAction")))
                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("SELECT PageText FROM CategoriesPages WHERE ModuleID='" + GetPageID + "' AND CatID='" + SepFunctions.FixWord(Request.Item("CatID")) + "' AND PortalID=@PortalID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            strPageText = SepFunctions.openNull(RS["PageText"]);
                                        }

                                    }
                                }
                            }
                    }
                    else
                    {
                        if (SepFunctions.Get_Portal_ID() == 0)
                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("SELECT PageText,Visits FROM ModulesNPages WHERE PageID='" + GetPageID + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            using (var cmd2 = new SqlCommand("UPDATE ModulesNPages SET Visits='" + (SepFunctions.toLong(SepFunctions.openNull(RS["Visits"])) + 1) + "' WHERE PageID='" + GetPageID + "'", conn))
                                            {
                                                cmd2.ExecuteNonQuery();
                                            }

                                            strPageText = SepFunctions.openNull(RS["PageText"]);
                                        }
                                    }
                                }
                            }
                        else
                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("SELECT PageText,Visits FROM PortalPages WHERE (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%') AND PageID='" + GetPageID + "' AND PortalID=0 AND Status=1", conn))
                                {
                                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            using (var cmd2 = new SqlCommand("UPDATE PortalPages SET Visits='" + (SepFunctions.toLong(SepFunctions.openNull(RS["Visits"])) + 1) + "' WHERE (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%') AND PageID='" + GetPageID + "' AND PortalID=0 AND Status=1", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                                                cmd2.ExecuteNonQuery();
                                            }

                                            strPageText = SepFunctions.openNull(RS["PageText"]);
                                        }
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(strPageText))
                                    using (var cmd = new SqlCommand("SELECT PageText,Visits FROM PortalPages WHERE PortalID=@PortalID AND PageID='" + GetPageID + "' AND Status=1", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                using (var cmd2 = new SqlCommand("UPDATE PortalPages SET Visits='" + (SepFunctions.toLong(SepFunctions.openNull(RS["Visits"])) + 1) + "' WHERE PortalID=@PortalID AND PageID='" + GetPageID + "' AND Status=1", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                                    cmd2.ExecuteNonQuery();
                                                }

                                                strPageText = SepFunctions.openNull(RS["PageText"]);
                                            }
                                        }
                                    }
                            }
                    }
                }

                if (!string.IsNullOrWhiteSpace(strPageText))
                {
                    if (Strings.InStr(strPageText, "[RSS]") > 0 && Strings.InStr(strPageText, "[/RSS]") > 0)
                        str.Append(SepFunctions.RSS_Feed_Parse(strPageText));
                    else
                        str.Append(strPageText);
                }
            }

            return Replace_Widgets(Strings.ToString(str), GetPageID);
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// Replaces the widgets.
        /// </summary>
        /// <param name="strPageText">The string page text.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="isLatestNews">if set to <c>true</c> [is latest news].</param>
        /// <returns>System.String.</returns>
        public string Replace_Widgets(string strPageText, long ModuleID, bool isLatestNews = false)
        {
            if (Strings.InStr(strPageText, "[") == 0 && Strings.InStr(strPageText, "]") == 0)
                return strPageText;
            strPageText = SepFunctions.Replace_Widgets(strPageText, ModuleID, isLatestNews);


            string GetZoneName;
            if (Strings.InStr(strPageText, "[[Ads|") > 0)
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT ZoneID,ZoneName FROM TargetZones WHERE ModuleID='2' AND Status <> -1", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                GetZoneName = SepFunctions.openNull(RS["ZoneName"]);
                                if (Strings.InStr(strPageText, "[[Ads|" + GetZoneName) > 0)
                                {
                                    var cAds = new Server.Controls.AdServer
                                    {
                                        ZoneID = SepFunctions.openNull(RS["ZoneID"])
                                    };
                                    strPageText = Strings.Replace(strPageText, "[[Ads|" + GetZoneName + "]]", Strings.ToString(cAds.Render()));
                                }
                            }

                        }
                    }
                }

            if (Strings.InStr(strPageText, "[[CR|") > 0)
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT ZoneID,ZoneName FROM TargetZones WHERE ModuleID='1' AND Status <> -1", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                GetZoneName = SepFunctions.openNull(RS["ZoneName"]);
                                if (Strings.InStr(strPageText, "[[CR|" + GetZoneName) > 0)
                                {
                                    var cCR = new Server.Controls.ContentRotator
                                    {
                                        ZoneID = SepFunctions.openNull(RS["ZoneID"])
                                    };
                                    strPageText = Strings.Replace(strPageText, "[[CR|" + GetZoneName + "]]", Strings.ToString(cCR.Render()));
                                }
                            }

                        }
                    }
                }

            // replace Dyanmic Functions
            if (Strings.InStr(strPageText, "]]") > 0 && Strings.InStr(strPageText, "[[") > 0)
            {
                if (Strings.InStr(strPageText, "[[Year]]") > 0)
                    strPageText = Strings.Replace(strPageText, "[[Year]]", Strings.ToString(DateTime.Now.Year));

                if (Strings.InStr(strPageText, "[[CompanySlogan]]") > 0)
                    strPageText = Strings.Replace(strPageText, "[[CompanySlogan]]", SepFunctions.Setup(991, "CompanySlogan"));

                if (Strings.InStr(strPageText, "[[Breadcrumbs]]") > 0)
                {
                    var cBreadcrumbs = new Server.Controls.Breadcrumbs();
                    strPageText = Strings.Replace(strPageText, "[[Breadcrumbs]]", Strings.ToString(cBreadcrumbs.Render()));
                }

                if (Strings.InStr(strPageText, "[[UserTopMenu]]") > 0)
                {
                    var cUserTopMenu = new Server.Controls.UserTopMenu();
                    strPageText = Strings.Replace(strPageText, "[[UserTopMenu]]", Strings.ToString(cUserTopMenu.Render()));
                }

                if (Strings.InStr(strPageText, "[[Newsletters]]") > 0)
                {
                    var cNewsletters = new Server.Controls.Newsletters();
                    strPageText = Strings.Replace(strPageText, "[[Newsletters]]", Strings.ToString(cNewsletters.Render()));
                }

                if (Strings.InStr(strPageText, "[[SiteLogo]]") > 0)
                {
                    var cSiteLogo = new Server.Controls.SiteLogo();
                    strPageText = Strings.Replace(strPageText, "[[SiteLogo]]", Strings.ToString(cSiteLogo.Render()));
                }

                if (Strings.InStr(strPageText, "[[SearchEngine]]") > 0)
                {
                    var cSearch = new Server.Controls.SearchBox();
                    strPageText = Strings.Replace(strPageText, "[[SearchEngine]]", Strings.ToString(cSearch.Render()));
                }

                if (Strings.InStr(strPageText, "[[SiteName]]") > 0)
                {
                    var cSiteName = new Server.Controls.CompanyName();
                    strPageText = Strings.Replace(strPageText, "[[SiteName]]", Strings.ToString(cSiteName.Render()));
                }

                if (ModuleID != 7)
                {
                    // If ModuleID is 7 then hyperlinks will be going to
                    // /members/username/modulepage.aspx so modules with hyperlinks will be disabled
                    if (Strings.InStr(strPageText, "[[MemberStats]]") > 0)
                    {
                        var cMemberStatistics = new Server.Controls.MemberStatistics();
                        strPageText = Strings.Replace(strPageText, "[[MemberStats]]", Strings.ToString(cMemberStatistics.Render()));
                    }

                    if (Strings.InStr(strPageText, "[[NewestMembers]]") > 0)
                    {
                        var cListNewMembers = new Server.Controls.ListNewMembers();
                        strPageText = Strings.Replace(strPageText, "[[NewestMembers]]", Strings.ToString(cListNewMembers.Render()));
                    }

                    if (Strings.InStr(strPageText, "[[AccountMenu]]") > 0)
                    {
                        var cAccountMenu = new Server.Controls.AccountMenu();
                        strPageText = Strings.Replace(strPageText, "[[AccountMenu]]", cAccountMenu.Render());
                    }

                    if (Strings.InStr(strPageText, "[[EventCalendar]]") > 0)
                    {
                        var cEventCalendar = new Server.Controls.EventCalendar();
                        strPageText = Strings.Replace(strPageText, "[[EventCalendar]]", Strings.ToString(cEventCalendar.Render()) + "<div style=\"clear:both\"></div>");
                    }

                    if (Strings.InStr(strPageText, "[[FriendList]]") > 0)
                    {
                        var cFriendList = new Server.Controls.FriendList();
                        strPageText = Strings.Replace(strPageText, "[[FriendList]]", Strings.ToString(cFriendList.Render()));
                    }

                    if (Strings.InStr(strPageText, "[[Polls]]") > 0)
                    {
                        var cRandomPoll = new Server.Controls.RandomPoll();
                        strPageText = Strings.Replace(strPageText, "[[Polls]]", Strings.ToString(cRandomPoll.Render()));
                    }

                    if (Strings.InStr(strPageText, "[[Stocks]]") > 0)
                    {
                        var cStockQuotes = new Server.Controls.StockQuotes();
                        strPageText = Strings.Replace(strPageText, "[[Stocks]]", Strings.ToString(cStockQuotes.Render()));
                    }

                    if (Strings.InStr(strPageText, "[[WhosOn]]") > 0)
                    {
                        var cWhosOnline = new Server.Controls.WhosOnline();
                        strPageText = Strings.Replace(strPageText, "[[WhosOn]]", Strings.ToString(cWhosOnline.Render()));
                    }

                    if (Strings.InStr(strPageText, "[[UnreadMessages]]") > 0)
                    {
                        var cUnreadMessages = new Server.Controls.UnreadMessages();
                        strPageText = Strings.Replace(strPageText, "[[UnreadMessages]]", Strings.ToString(cUnreadMessages.Render()));
                    }

                    // Web Site Menus
                    for (int i = 1; i <= 7; i++)
                    {
                        if (Strings.InStr(strPageText, "[[SiteMenu" + i + "]]") > 0 || Strings.InStr(strPageText, "[[SiteMenu" + i + "V]]") > 0)
                        {
                            var cMenu = new Server.Controls.SiteMenu
                            {
                                MenuID = i
                            };
                            if (Strings.InStr(strPageText, "[[SiteMenu" + i + "V]]") > 0)
                                cMenu.CssClass = "flex-column";
                            if (Strings.InStr(strPageText, "[[SiteMenu" + i + "V]]") > 0)
                            {
                                strPageText = Strings.Replace(strPageText, "[[SiteMenu" + i + "V]]", Strings.ToString(cMenu.Render()));
                            }
                            else
                            {
                                strPageText = Strings.Replace(strPageText, "[[SiteMenu" + i + "]]", Strings.ToString(cMenu.Render()));
                            }
                        }
                    }

                }
            }

            return strPageText;
        }

        /// <summary>
        /// IDisposable.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
                if (disposing)
                {
                }

            disposedValue = true;
        }
    }
}