// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ContentRotator.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using SepCore;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ContentRotator.
    /// </summary>
    public class ContentRotator
    {
        /// <summary>
        /// The m zone identifier
        /// </summary>
        private string m_ZoneID;

        /// <summary>
        /// Gets or sets the zone identifier.
        /// </summary>
        /// <value>The zone identifier.</value>
        public string ZoneID
        {
            get => Strings.ToString(m_ZoneID);

            set => m_ZoneID = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ZoneID))
            {
                output.AppendLine("ZoneID is Required");
                return output.ToString();
            }

            var iModuleID = 0;

            iModuleID = SepFunctions.toInt(Session.getSession("ModuleID"));

            var wc = string.Empty;
            var GetContentID = string.Empty;

            if (!string.IsNullOrWhiteSpace(Request.Item("CatID")))
            {
                wc += " AND (CR.CatIDs LIKE '%|" + SepFunctions.FixWord(Request.Item("CatID")) + "|%' OR CR.PageIDs LIKE '%|" + iModuleID + "|%' OR CR.PageIDs LIKE '%|-1|%')";
            }
            else
            {
                wc += " AND (CR.CatIDs LIKE '%|0|%' OR CR.CatIDs IS NULL OR datalength(CR.CatIDs) = 0)";
                if (iModuleID > 0)
                {
                    wc += " AND (CR.PageIDs LIKE '%|" + iModuleID + "|%'";
                }
                else
                {
                    wc += " AND (CR.PageIDs LIKE '%|" + SepFunctions.FixWord(Request.Item("UniqueID")) + "|%'";
                }

                wc += " OR CR.PageIDs LIKE '%|-1|%')";
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 ContentID FROM ContentRotator AS CR,TargetZones AS TZ WHERE (TZ.ZoneID=CR.ZoneID AND TZ.ZoneID='" + SepFunctions.FixWord(ZoneID) + "' AND (CR.PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(CR.PortalIDs) = 0)) AND CR.Status <> -1" + wc + " ORDER BY NEWID()", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            GetContentID = SepFunctions.openNull(RS["ContentID"]);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(GetContentID))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT HTMLContent FROM ContentRotator WHERE ContentID='" + SepFunctions.FixWord(GetContentID) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                output.Append(Replace_Widgets(SepFunctions.openNull(RS["HTMLContent"]), iModuleID));
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Replaces the widgets.
        /// </summary>
        /// <param name="strPageText">The string page text.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns>System.String.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Replace_Widgets(string strPageText, int ModuleID)
        {
            if (Strings.InStr(strPageText, "[") == 0 && Strings.InStr(strPageText, "]") == 0)
            {
                return strPageText;
            }

            var posa = 0;
            var strNum = string.Empty;

            var GetZoneName = string.Empty;

            strPageText = SepFunctions.Replace_Widgets(strPageText, ModuleID, false);

            if (Strings.InStr(strPageText, "[[Ads|") > 0)
            {
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
                                    var cAds = new AdServer();
                                    cAds.ZoneID = SepFunctions.openNull(RS["ZoneID"]);
                                    strPageText = Strings.Replace(strPageText, "[[Ads|" + GetZoneName + "]]", cAds.Render());
                                }
                            }
                        }
                    }
                }
            }

            if (Strings.InStr(strPageText, "[[CR|") > 0)
            {
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
                                    var cCR = new ContentRotator();
                                    cCR.ZoneID = SepFunctions.openNull(RS["ZoneID"]);
                                    strPageText = Strings.Replace(strPageText, "[[CR|" + GetZoneName + "]]", cCR.Render());
                                }
                            }
                        }
                    }
                }
            }

            if (Strings.InStr(strPageText, "[[Categories||") > 0)
            {
                posa = Strings.InStr(strPageText, "[[Categories||") + Strings.Len("[[Categories||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cCat = new CategoryLayout();
                cCat.ModuleID = SepFunctions.toInt(strNum);
                strPageText = Strings.Replace(strPageText, "[[Categories||" + strNum + "]]", Strings.ToString(cCat.Render()));
            }

            // replace Dyanmic Functions
            if (Strings.InStr(strPageText, "]]") > 0 && Strings.InStr(strPageText, "[[") > 0)
            {
                if (Strings.InStr(strPageText, "[[SiteLogo]]") > 0)
                {
                    var cSiteLogo = new SiteLogo();
                    strPageText = Strings.Replace(strPageText, "[[SiteLogo]]", cSiteLogo.Render());
                }

                if (Strings.InStr(strPageText, "[[SearchEngine]]") > 0)
                {
                    var cSearch = new SearchBox();
                    strPageText = Strings.Replace(strPageText, "[[SearchEngine]]", cSearch.Render());
                }

                if (Strings.InStr(strPageText, "[[SiteName]]") > 0)
                {
                    var cSiteName = new CompanyName();
                    strPageText = Strings.Replace(strPageText, "[[SiteName]]", cSiteName.Render());
                }

                if (ModuleID != 7)
                {
                    // If ModuleID is 7 then hyperlinks will be going to
                    // /members/username/modulepage.aspx so modules with hyperlinks will be disabled
                    if (Strings.InStr(strPageText, "[[MemberStats]]") > 0)
                    {
                        var cMemberStatistics = new MemberStatistics();
                        strPageText = Strings.Replace(strPageText, "[[MemberStats]]", cMemberStatistics.Render());
                    }

                    if (Strings.InStr(strPageText, "[[NewestMembers]]") > 0)
                    {
                        var cListNewMembers = new ListNewMembers();
                        strPageText = Strings.Replace(strPageText, "[[NewestMembers]]", cListNewMembers.Render());
                    }

                    if (Strings.InStr(strPageText, "[[AccountMenu]]") > 0)
                    {
                        var cAccountMenu = new AccountMenu();
                        strPageText = Strings.Replace(strPageText, "[[AccountMenu]]", cAccountMenu.Render());
                    }

                    if (Strings.InStr(strPageText, "[[EventCalendar]]") > 0)
                    {
                        var cEventCalendar = new EventCalendar();
                        strPageText = Strings.Replace(strPageText, "[[EventCalendar]]", cEventCalendar.Render());
                    }

                    if (Strings.InStr(strPageText, "[[FriendList]]") > 0)
                    {
                        var cFriendList = new FriendList();
                        strPageText = Strings.Replace(strPageText, "[[FriendList]]", cFriendList.Render());
                    }

                    if (Strings.InStr(strPageText, "[[Polls]]") > 0)
                    {
                        var cRandomPoll = new RandomPoll();
                        strPageText = Strings.Replace(strPageText, "[[Polls]]", cRandomPoll.Render());
                    }

                    if (Strings.InStr(strPageText, "[[Stocks]]") > 0)
                    {
                        var cStockQuotes = new StockQuotes();
                        strPageText = Strings.Replace(strPageText, "[[Stocks]]", cStockQuotes.Render());
                    }

                    if (Strings.InStr(strPageText, "[[WhosOn]]") > 0)
                    {
                        var cWhosOnline = new WhosOnline();
                        strPageText = Strings.Replace(strPageText, "[[WhosOn]]", cWhosOnline.Render());
                    }

                    if (Strings.InStr(strPageText, "[[UnreadMessages]]") > 0)
                    {
                        var cUnreadMessages = new UnreadMessages();
                        strPageText = Strings.Replace(strPageText, "[[UnreadMessages]]", cUnreadMessages.Render());
                    }

                    if (Strings.InStr(strPageText, "[[ViewCart]]") > 0)
                    {
                        var cShoppingCart = new ShoppingCart();
                        strPageText = Strings.Replace(strPageText, "[[ViewCart]]", cShoppingCart.Render());
                    }

                    if (Strings.InStr(strPageText, "[[Newsletters]]") > 0)
                    {
                        var cNewsletters = new Newsletters();
                        strPageText = Strings.Replace(strPageText, "[[Newsletters]]", cNewsletters.Render());
                    }

                    if (Strings.InStr(strPageText, "[[PortalDropdown]]") > 0)
                    {
                        var cPortalDropdown = new PortalDropdown();
                        strPageText = Strings.Replace(strPageText, "[[PortalDropdown]]", cPortalDropdown.Render());
                    }

                    // Web Site Menus
                    if (Strings.InStr(strPageText, "[[SiteMenu1]]") > 0)
                    {
                        SiteMenu cMenu1 = new SiteMenu();
                        cMenu1.MenuID = 1;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu1]]", cMenu1.Render());
                    }

                    if (Strings.InStr(strPageText, "[[SiteMenu2]]") > 0)
                    {
                        SiteMenu cMenu2 = new SiteMenu();
                        cMenu2.MenuID = 2;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu2]]", cMenu2.Render());
                    }

                    if (Strings.InStr(strPageText, "[[SiteMenu3]]") > 0)
                    {
                        SiteMenu cMenu3 = new SiteMenu();
                        cMenu3.MenuID = 3;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu3]]", cMenu3.Render());
                    }

                    if (Strings.InStr(strPageText, "[[SiteMenu4]]") > 0)
                    {
                        SiteMenu cMenu4 = new SiteMenu();
                        cMenu4.MenuID = 4;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu4]]", cMenu4.Render());
                    }

                    if (Strings.InStr(strPageText, "[[SiteMenu5]]") > 0)
                    {
                        SiteMenu cMenu5 = new SiteMenu();
                        cMenu5.MenuID = 5;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu5]]", cMenu5.Render());
                    }

                    if (Strings.InStr(strPageText, "[[SiteMenu6]]") > 0)
                    {
                        SiteMenu cMenu6 = new SiteMenu();
                        cMenu6.MenuID = 6;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu6]]", cMenu6.Render());
                    }

                    if (Strings.InStr(strPageText, "[[SiteMenu7]]") > 0)
                    {
                        SiteMenu cMenu7 = new SiteMenu();
                        cMenu7.MenuID = 7;
                        strPageText = Strings.Replace(strPageText, "[[SiteMenu7]]", cMenu7.Render());
                    }
                }
            }

            return strPageText;
        }
    }
}