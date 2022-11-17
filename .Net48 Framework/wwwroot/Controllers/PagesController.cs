// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 12-10-2019
//
// Last Modified By : spink
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="PagesController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class PagesController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class PagesController : ApiController
    {
        /// <summary>
        /// Gets the specified page identifier.
        /// </summary>
        /// <param name="PageID">The page identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>SepCommon.Models.WebPages.</returns>
        [Route("api/pages/Get")]
        [HttpGet]
        public SepCommon.Models.WebPages Get([FromUri] string PageID, [FromUri] string PortalID)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminSiteLooks");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var strSql = "";
                var sReturn = new SepCommon.Models.WebPages();
                if (SepFunctions.toLong(PortalID) == 0)
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        strSql = "SELECT LinkText,PageText,ModuleID,MenuID FROM ModulesNPages WHERE UniqueID='" + SepFunctions.toLong(PageID) + "'";
                        using (var cmd = new SqlCommand(strSql, conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    sReturn.LinkText = SepFunctions.openNull(RS["LinkText"]);
                                    sReturn.PageText = SepFunctions.openNull(RS["PageText"]);
                                    sReturn.ModuleID = SepFunctions.openNull(RS["ModuleID"]);
                                    sReturn.MenuID = SepFunctions.toInt(SepFunctions.openNull(RS["MenuID"]));
                                }

                            }
                        }
                    }
                }
                else
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        strSql = "SELECT LinkText,PageText,PageID,MenuID FROM PortalPages WHERE PortalID=@PortalID AND UniqueID='" + SepFunctions.toLong(PageID) + "' AND Status=1";
                        using (var cmd = new SqlCommand(strSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.toLong(PortalID));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    sReturn.LinkText = SepFunctions.openNull(RS["LinkText"]);
                                    sReturn.PageText = SepFunctions.openNull(RS["PageText"]);
                                    sReturn.ModuleID = SepFunctions.openNull(RS["PageID"]);
                                    sReturn.MenuID = SepFunctions.toInt(SepFunctions.openNull(RS["MenuID"]));
                                }

                            }
                        }
                    }
                }
                return sReturn;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Widgetses the HTML.
        /// </summary>
        /// <param name="PageID">The page identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="PortalID">The portal identifier.</param>
        /// <returns>Widgets.</returns>
        [Route("api/pages/WidgetsHtml")]
        [HttpGet]
        public Widgets WidgetsHtml([FromUri] string PageID, [FromUri] string ModuleID, [FromUri] string PortalID)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminSiteLooks");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var cReplace = new BusinessObjects.Replace();
                var WidgetHtml = new Widgets
                {
                    AccountMenu = cReplace.Replace_Widgets("[[AccountMenu]]", 0),
                    CompanySlogan = cReplace.Replace_Widgets("[[CompanySlogan]]", 0),
                    Breadcrumbs = cReplace.Replace_Widgets("[[Breadcrumbs]]", 4),
                    UserTopMenu = cReplace.Replace_Widgets("[[UserTopMenu]]", 17),
                    Newsletters = cReplace.Replace_Widgets("[[Newsletters]]", 0),
                    SiteLogo = cReplace.Replace_Widgets("[[SiteLogo]]", 0),
                    SearchEngine = cReplace.Replace_Widgets("[[SearchEngine]]", 0),
                    SiteName = cReplace.Replace_Widgets("[[SiteName]]", 0),
                    MemberStats = cReplace.Replace_Widgets("[[MemberStats]]", 0),
                    NewestMembers = cReplace.Replace_Widgets("[[NewestMembers]]", 0),
                    EventCalendar = cReplace.Replace_Widgets("[[EventCalendar]]", 0),
                    FriendList = cReplace.Replace_Widgets("[[FriendList]]", 0),
                    Polls = cReplace.Replace_Widgets("[[Polls]]", 0),
                    Stocks = cReplace.Replace_Widgets("[[Stocks]]", 0),
                    SiteMenu1 = cReplace.Replace_Widgets("[[SiteMenu1]]", 0),
                    SiteMenu2 = cReplace.Replace_Widgets("[[SiteMenu2]]", 0),
                    SiteMenu3 = cReplace.Replace_Widgets("[[SiteMenu3]]", 0),
                    SiteMenu4 = cReplace.Replace_Widgets("[[SiteMenu4]]", 0),
                    SiteMenu5 = cReplace.Replace_Widgets("[[SiteMenu5]]", 0),
                    SiteMenu6 = cReplace.Replace_Widgets("[[SiteMenu6]]", 0),
                    SiteMenu7 = cReplace.Replace_Widgets("[[SiteMenu7]]", 0),
                    SiteMenu1V = cReplace.Replace_Widgets("[[SiteMenu1V]]", 0),
                    SiteMenu2V = cReplace.Replace_Widgets("[[SiteMenu2V]]", 0),
                    SiteMenu3V = cReplace.Replace_Widgets("[[SiteMenu3V]]", 0),
                    SiteMenu4V = cReplace.Replace_Widgets("[[SiteMenu4V]]", 0),
                    SiteMenu5V = cReplace.Replace_Widgets("[[SiteMenu5V]]", 0),
                    SiteMenu6V = cReplace.Replace_Widgets("[[SiteMenu6V]]", 0),
                    SiteMenu7V = cReplace.Replace_Widgets("[[SiteMenu7V]]", 0),
                    WhosOn = cReplace.Replace_Widgets("[[WhosOn]]", 0),
                    AdServer = cReplace.Replace_Widgets("[[Ads|Banner]]", 0),
                    Sponsors = cReplace.Replace_Widgets("[[Ads|Sponsor]]", 0),
                    PageText = cReplace.GetPageText(SepFunctions.toLong(PageID), SepFunctions.toLong(ModuleID))
                };
                cReplace.Dispose();

                long tmpMenuID = 0;
                var showPage = false;

                if (SepFunctions.toLong(PortalID) == 0)
                {
                    List<MenuPageHeaders> HeadersWidgetListHtml = new List<MenuPageHeaders>();
                    MenuPageHeaders HeadersWidgetHtml = new MenuPageHeaders();
                    List<MenuPages> PagesWidgetListHtml = new List<MenuPages>();
                    MenuPages PagesWidgetHtml = new MenuPages();
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT UniqueID,ModuleID,MenuID,PageID,UserPageName,LinkText,TargetWindow FROM ModulesNPages WHERE MenuID <> '0' AND Status=1 AND Activated='1' AND UserPageName <> '' AND PageID < 201 ORDER BY MenuID, Weight, LinkText", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                tmpMenuID = 10;
                                while (RS.Read())
                                    if (SepFunctions.Setup(GlobalVars.ModuleID, "Menu" + SepFunctions.openNull(RS["MenuID"]) + "Sitemap") == "Yes")
                                    {
                                        showPage = false;
                                        if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) > 0)
                                        {
                                            if (SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])))) showPage = true;
                                        }
                                        else
                                        {
                                            showPage = true;
                                        }

                                        if (showPage)
                                        {
                                            if (tmpMenuID != SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"])))
                                            {
                                                HeadersWidgetHtml.Pages = PagesWidgetListHtml;
                                                PagesWidgetListHtml = new List<MenuPages>();
                                                HeadersWidgetHtml = new MenuPageHeaders
                                                {
                                                    Name = SepFunctions.Setup(GlobalVars.ModuleID, "Menu" + SepFunctions.openNull(RS["MenuID"]) + "Text")
                                                };
                                                HeadersWidgetListHtml.Add(HeadersWidgetHtml);
                                            }

                                            PagesWidgetHtml = new MenuPages
                                            {
                                                PageID = SepFunctions.openNull(RS["UniqueID"]),
                                                ModuleID = SepFunctions.openNull(RS["ModuleID"]),
                                                PageTitle = SepFunctions.openNull(RS["LinkText"])
                                            };
                                            PagesWidgetListHtml.Add(PagesWidgetHtml);
                                        }

                                        tmpMenuID = SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"]));
                                    }
                            }
                        }
                    }
                    HeadersWidgetHtml.Pages = PagesWidgetListHtml;
                    WidgetHtml.PageHeaders = HeadersWidgetListHtml;
                }
                else
                {
                    List<MenuPageHeaders> HeadersWidgetListHtml = new List<MenuPageHeaders>();
                    MenuPageHeaders HeadersWidgetHtml = new MenuPageHeaders();
                    List<MenuPages> PagesWidgetListHtml = new List<MenuPages>();
                    MenuPages PagesWidgetHtml = new MenuPages();
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT UniqueID,PageID,MenuID,UserPageName,LinkText,TargetWindow FROM PortalPages WHERE MenuID <> 0 AND Status='1' AND UserPageName <> '' AND (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%') AND PageID < 201 ORDER BY MenuID, Weight, LinkText", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                tmpMenuID = 10;
                                while (RS.Read())
                                {
                                    if (tmpMenuID != SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"])))
                                    {
                                        HeadersWidgetHtml.Pages = PagesWidgetListHtml;
                                        PagesWidgetListHtml = new List<MenuPages>();
                                        HeadersWidgetHtml = new MenuPageHeaders
                                        {
                                            Name = SepFunctions.PortalSetup("SiteMenu" + SepFunctions.openNull(RS["MenuID"]))
                                        };
                                        HeadersWidgetListHtml.Add(HeadersWidgetHtml);
                                    }

                                    PagesWidgetHtml = new MenuPages
                                    {
                                        PageID = SepFunctions.openNull(RS["UniqueID"]),
                                        ModuleID = SepFunctions.openNull(RS["PageID"]),
                                        PageTitle = SepFunctions.openNull(RS["LinkText"])
                                    };
                                    PagesWidgetListHtml.Add(PagesWidgetHtml);

                                    tmpMenuID = SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"]));
                                }
                            }
                        }
                    }
                    HeadersWidgetHtml.Pages = PagesWidgetListHtml;
                    WidgetHtml.PageHeaders = HeadersWidgetListHtml;
                }
                return WidgetHtml;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        [Route("api/pages/Save")]
        [HttpPost]
        public ResponseMessage Save([FromBody] PageContent PageInfo)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminSiteLooks");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                ResponseMessage sReturn = new ResponseMessage();
                SepCommon.DAL.WebPages.Save_Web_Page(PageInfo.PageID, PageInfo.MenuID, "", PageInfo.LinkText, PageInfo.PageText, "", "", "", "", "", 1, 0, PageInfo.ModuleID, PageInfo.PortalID, "");
                return sReturn;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }

    public class ResponseMessage
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Class PageContent.
    /// </summary>
    public class PageContent
    {
        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public long PageID { get; set; }
        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public long MenuID { get; set; }
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }
        /// <summary>
        /// Gets or sets the link text.
        /// </summary>
        /// <value>The link text.</value>
        public string LinkText { get; set; }
        /// <summary>
        /// Gets or sets the page text.
        /// </summary>
        /// <value>The page text.</value>
        public string PageText { get; set; }
        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }
    }
    /// <summary>
    /// Class Widgets.
    /// </summary>
    public class Widgets
    {
        /// <summary>
        /// Gets or sets the account menu.
        /// </summary>
        /// <value>The account menu.</value>
        public string AccountMenu { get; set; }

        /// <summary>
        /// Gets or sets the ad server.
        /// </summary>
        /// <value>The ad server.</value>
        public string AdServer { get; set; }

        /// <summary>
        /// Gets or sets the breadcrumbs.
        /// </summary>
        /// <value>The breadcrumbs.</value>
        public string Breadcrumbs { get; set; }

        /// <summary>
        /// Gets or sets the company slogan.
        /// </summary>
        /// <value>The company slogan.</value>
        public string CompanySlogan { get; set; }

        /// <summary>
        /// Gets or sets the event calendar.
        /// </summary>
        /// <value>The event calendar.</value>
        public string EventCalendar { get; set; }

        /// <summary>
        /// Gets or sets the friend list.
        /// </summary>
        /// <value>The friend list.</value>
        public string FriendList { get; set; }

        /// <summary>
        /// Gets or sets the member stats.
        /// </summary>
        /// <value>The member stats.</value>
        public string MemberStats { get; set; }

        /// <summary>
        /// Gets or sets the newest members.
        /// </summary>
        /// <value>The newest members.</value>
        public string NewestMembers { get; set; }

        /// <summary>
        /// Gets or sets the newsletters.
        /// </summary>
        /// <value>The newsletters.</value>
        public string Newsletters { get; set; }

        /// <summary>
        /// Gets or sets the polls.
        /// </summary>
        /// <value>The polls.</value>
        public string Polls { get; set; }

        /// <summary>
        /// Gets or sets the search engine.
        /// </summary>
        /// <value>The search engine.</value>
        public string SearchEngine { get; set; }

        /// <summary>
        /// Gets or sets the site logo.
        /// </summary>
        /// <value>The site logo.</value>
        public string SiteLogo { get; set; }

        /// <summary>
        /// Gets or sets the site menu1.
        /// </summary>
        /// <value>The site menu1.</value>
        public string SiteMenu1 { get; set; }

        /// <summary>
        /// Gets or sets the site menu1.
        /// </summary>
        /// <value>The site menu1.</value>
        public string SiteMenu1V { get; set; }

        /// <summary>
        /// Gets or sets the site menu2.
        /// </summary>
        /// <value>The site menu2.</value>
        public string SiteMenu2 { get; set; }

        /// <summary>
        /// Gets or sets the site menu2.
        /// </summary>
        /// <value>The site menu2.</value>
        public string SiteMenu2V { get; set; }

        /// <summary>
        /// Gets or sets the site menu3.
        /// </summary>
        /// <value>The site menu3.</value>
        public string SiteMenu3 { get; set; }

        /// <summary>
        /// Gets or sets the site menu3.
        /// </summary>
        /// <value>The site menu3.</value>
        public string SiteMenu3V { get; set; }

        /// <summary>
        /// Gets or sets the site menu4.
        /// </summary>
        /// <value>The site menu4.</value>
        public string SiteMenu4 { get; set; }

        /// <summary>
        /// Gets or sets the site menu4.
        /// </summary>
        /// <value>The site menu4.</value>
        public string SiteMenu4V { get; set; }

        /// <summary>
        /// Gets or sets the site menu5.
        /// </summary>
        /// <value>The site menu5.</value>
        public string SiteMenu5 { get; set; }

        /// <summary>
        /// Gets or sets the site menu5.
        /// </summary>
        /// <value>The site menu5.</value>
        public string SiteMenu5V { get; set; }

        /// <summary>
        /// Gets or sets the site menu6.
        /// </summary>
        /// <value>The site menu6.</value>
        public string SiteMenu6 { get; set; }

        /// <summary>
        /// Gets or sets the site menu6.
        /// </summary>
        /// <value>The site menu6.</value>
        public string SiteMenu6V { get; set; }

        /// <summary>
        /// Gets or sets the site menu7.
        /// </summary>
        /// <value>The site menu7.</value>
        public string SiteMenu7 { get; set; }

        /// <summary>
        /// Gets or sets the site menu7.
        /// </summary>
        /// <value>The site menu7.</value>
        public string SiteMenu7V { get; set; }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the sponsors.
        /// </summary>
        /// <value>The sponsors.</value>
        public string Sponsors { get; set; }

        /// <summary>
        /// Gets or sets the stocks.
        /// </summary>
        /// <value>The stocks.</value>
        public string Stocks { get; set; }

        /// <summary>
        /// Gets or sets the user top menu.
        /// </summary>
        /// <value>The user top menu.</value>
        public string UserTopMenu { get; set; }

        /// <summary>
        /// Gets or sets the whos on.
        /// </summary>
        /// <value>The whos on.</value>
        public string WhosOn { get; set; }

        /// <summary>
        /// Gets or sets the whos on.
        /// </summary>
        /// <value>The page text.</value>
        public string PageText { get; set; }

        /// <summary>
        /// Gets or sets the sponsors.
        /// </summary>
        /// <value>The sponsors.</value>
        public List<MenuPageHeaders> PageHeaders { get; set; }
    }

    /// <summary>
    /// Class MenuPageHeaders.
    /// </summary>
    public class MenuPageHeaders
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        public List<MenuPages> Pages { get; set; }
    }

    /// <summary>
    /// Class MenuPages.
    /// </summary>
    public class MenuPages
    {
        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>The page title.</value>
        public string PageID { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public string ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>The page title.</value>
        public string PageTitle { get; set; }
    }
}