
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Options;

    public class PagesController : ControllerBase
    {
        private readonly IOptions<SepCityCMS.Models.Config.Jwt.Root> _Jwt;
        private readonly IOptions<SepCityCMS.Models.Config.License.Root> _License;
        private readonly IOptions<SepCityCMS.Models.Config.Points.Root> _Points;
        private readonly IOptions<SepCityCMS.Models.Config.Security.Root> _Security;
        private readonly IOptions<SepCityCMS.Models.Config.Settings.Root> _Settings;
        private readonly IOptions<SepCityCMS.Models.Config.System.Root> _System;

        public PagesController(IOptions<SepCityCMS.Models.Config.Jwt.Root> Jwt, IOptions<SepCityCMS.Models.Config.License.Root> License, IOptions<SepCityCMS.Models.Config.Points.Root> Points,
            IOptions<SepCityCMS.Models.Config.Security.Root> Security, IOptions<SepCityCMS.Models.Config.Settings.Root> Settings, IOptions<SepCityCMS.Models.Config.System.Root> System)
        {
            _Jwt = Jwt;
            _License = License;
            _Points = Points;
            _Security = Security;
            _Settings = Settings;
            _System = System;
        }

        //[CheckOption("username", "Everyone")]
        [Route("api/pages/{id:long}/{PortalID:long?}")]
        [HttpGet]
        public Models.WebPages Get(long id, long PortalID)
        {
            var JwtValues = _Jwt.Value;
            var LicenseValues = _License.Value;
            var PointsValues = _Points.Value;
            var SecurityValues = _Security.Value;
            var SettingsValues = _Settings.Value;
            var SystemValues = _System.Value;

            if (!string.IsNullOrEmpty(JwtValues.JwtInfo.Key))
            {
            }

            var strSql = "";
            var sReturn = new Models.WebPages();
            if (PortalID == 0)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    strSql = "SELECT LinkText,PageText,ModuleID,MenuID FROM ModulesNPages WHERE PageID=" + id;
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
                    strSql = "SELECT LinkText,PageText,PageID,MenuID FROM PortalPages WHERE PortalID=@PortalID AND ModuleID=" + id + " AND Status=1";
                    using (var cmd = new SqlCommand(strSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
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

        [CheckOption("username", "AdminSiteLooks")]
        [Route("api/pages/WidgetsHtml")]
        [HttpGet]
        public Widgets WidgetsHtml([FromQuery] string PageID, [FromQuery] string ModuleID, [FromQuery] string PortalID)
        {
            var cReplace = new SepCityCMS.Server.BusinessObjects.Replace();
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
                                if (SepFunctions.Setup(SepFunctions.toInt(ModuleID), "Menu" + SepFunctions.openNull(RS["MenuID"]) + "Sitemap") == "Yes")
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
                                                Name = SepFunctions.Setup(SepFunctions.toInt(ModuleID), "Menu" + SepFunctions.openNull(RS["MenuID"]) + "Text")
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
                        cmd.Parameters.AddWithValue("@PortalIDs", Server.SepCore.Strings.ToString(SepFunctions.Get_Portal_ID()));
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

        [CheckOption("username", "AdminSiteLooks")]
        [Route("api/pages/Save")]
        [HttpPost]
        public ResponseMessage Save([FromBody] PageContent PageInfo)
        {
            ResponseMessage sReturn = new ResponseMessage();
            Server.DAL.WebPages.Save_Web_Page(PageInfo.PageID, PageInfo.MenuID, "", PageInfo.LinkText, PageInfo.PageText, "", "", "", "", "", 1, 0, PageInfo.ModuleID, PageInfo.PortalID, "");
            return sReturn;
        }
    }

    public class ResponseMessage
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class PageContent
    {
        public long PageID { get; set; }
        public long MenuID { get; set; }
        public int ModuleID { get; set; }
        public string LinkText { get; set; }
        public string PageText { get; set; }
        public long PortalID { get; set; }
    }
    public class Widgets
    {
        public string AccountMenu { get; set; }

        public string AdServer { get; set; }

        public string Breadcrumbs { get; set; }

        public string CompanySlogan { get; set; }

        public string EventCalendar { get; set; }

        public string FriendList { get; set; }

        public string MemberStats { get; set; }

        public string NewestMembers { get; set; }

        public string Newsletters { get; set; }

        public string Polls { get; set; }

        public string SearchEngine { get; set; }

        public string SiteLogo { get; set; }

        public string SiteMenu1 { get; set; }

        public string SiteMenu1V { get; set; }

        public string SiteMenu2 { get; set; }

        public string SiteMenu2V { get; set; }

        public string SiteMenu3 { get; set; }

        public string SiteMenu3V { get; set; }

        public string SiteMenu4 { get; set; }

        public string SiteMenu4V { get; set; }

        public string SiteMenu5 { get; set; }

        public string SiteMenu5V { get; set; }

        public string SiteMenu6 { get; set; }

        public string SiteMenu6V { get; set; }

        public string SiteMenu7 { get; set; }

        public string SiteMenu7V { get; set; }

        public string SiteName { get; set; }

        public string Sponsors { get; set; }

        public string Stocks { get; set; }

        public string UserTopMenu { get; set; }

        public string WhosOn { get; set; }

        public string PageText { get; set; }

        public List<MenuPageHeaders> PageHeaders { get; set; }
    }

    public class MenuPageHeaders
    {
        public string Name { get; set; }
        public List<MenuPages> Pages { get; set; }
    }

    public class MenuPages
    {
        public string PageID { get; set; }

        public string ModuleID { get; set; }

        public string PageTitle { get; set; }
    }
}