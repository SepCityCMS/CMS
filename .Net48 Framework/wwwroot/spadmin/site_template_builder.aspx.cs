// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_builder.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Xml;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class site_template_builder.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_builder : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The s template identifier
        /// </summary>
        private readonly string sTemplateID = "1";

        /// <summary>
        /// The s temporary folder
        /// </summary>
        private string sTempFolder;

        /// <summary>
        /// The s template configuration XML
        /// </summary>
        private string sTemplateConfigXML;

        /// <summary>
        /// Adms the replace defaults.
        /// </summary>
        /// <param name="sdata">The sdata.</param>
        /// <returns>System.String.</returns>
        public static string ADM_Replace_Defaults(string sdata)
        {
            var str = sdata;

            var sBorderAll = " style=\"cursor:pointer;margin:auto;\" onmouseover=\"this.style.border='1px black solid';\" onmouseout=\"this.style.border='0px black solid';\"";

            if (Strings.InStr(str, "[[FieldSet]]") > 0)
                str = Strings.Replace(str, "[[FieldSet]]", " onclick=\"changeColor('Fieldset',event);\"" + sBorderAll);
            if (Strings.InStr(str, "[[EventCalendar]]") > 0)
                str = Strings.Replace(str, "[[EventCalendar]]", " onclick=\"changeColor('Events',event);\"" + sBorderAll);
            if (Strings.InStr(str, "[[Tables]]") > 0)
                str = Strings.Replace(str, "[[Tables]]", " onclick=\"changeColor('Tables',event);\"" + sBorderAll);

            return str;
        }

        /// <summary>
        /// Adms the clickable functions.
        /// </summary>
        /// <param name="pageText">The page text.</param>
        /// <returns>System.String.</returns>
        public string ADM_Clickable_Functions(string pageText)
        {
            var sBorderTopBottom = " style=\"cursor:pointer;margin:auto;\" onmouseover=\"this.style.borderBottom='1px black solid';this.style.borderTop='2px black solid';\" onmouseout=\"this.style.borderBottom='0px black solid';this.style.borderTop='0px black solid';\"";
            var sBorderAll = " style=\"cursor:pointer;margin:auto;\" onmouseover=\"this.style.border='1px black solid';\" onmouseout=\"this.style.border='0px black solid';\"";

            var sMenu = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (Strings.InStr(pageText, "[[SelectHeader1]]") > 0)
                pageText = Strings.Replace(pageText, "[[SelectHeader1]]", " onclick=\"changeColor('HeaderHR',event);\"" + sBorderTopBottom);
            if (Strings.InStr(pageText, "[[SelectHeader2]]") > 0)
                pageText = Strings.Replace(pageText, "[[SelectHeader2]]", " onclick=\"changeColor('Header',event);\"" + sBorderTopBottom);
            if (Strings.InStr(pageText, "[[TopMenu]]") > 0)
                pageText = Strings.Replace(pageText, "[[TopMenu]]", " onclick=\"changeColor('LayoutTopMenu',event);\"" + sBorderTopBottom);
            if (Strings.InStr(pageText, "[[ContentGrad1]]") > 0)
                pageText = Strings.Replace(pageText, "[[ContentGrad1]]", " onclick=\"changeColor('ContentTopGrad',event);\"" + sBorderTopBottom);
            if (Strings.InStr(pageText, "[[ContentGrad2]]") > 0)
                pageText = Strings.Replace(pageText, "[[ContentGrad2]]", " onclick=\"changeColor('ContentBottomGrad',event);\"" + sBorderTopBottom);
            if (Strings.InStr(pageText, "[[ContentBody]]") > 0)
                pageText = Strings.Replace(pageText, "[[ContentBody]]", " onclick=\"changeColor('Body',event);\"" + sBorderAll);
            if (Strings.InStr(pageText, "[[ContentRight]]") > 0)
                pageText = Strings.Replace(pageText, "[[ContentRight]]", " onclick=\"changeColor('Body',event);\"" + sBorderAll);
            if (Strings.InStr(pageText, "[[ContentSpacer]]") > 0)
                pageText = Strings.Replace(pageText, "[[ContentSpacer]]", " onclick=\"changeColor('ContentSpacer',event);\"" + sBorderAll);
            if (Strings.InStr(pageText, "[[Footer]]") > 0)
                pageText = Strings.Replace(pageText, "[[Footer]]", " onclick=\"changeColor('Footer',event);\"" + sBorderTopBottom);
            if (Strings.InStr(pageText, "[[ModuleTopMenu]]") > 0)
            {
                sMenu += "<ul id=\"nav-topmenu\" onclick=\"changeColor('ModuleTopMenu',event);\"" + sBorderTopBottom + ">";
                sMenu += "<li><span><a href=\"#\"><img src=\"" + sImageFolder + "skins/public/icons/topmenu/management.png\" alt=\"Management Icon\" class=\"LinkIcon\" border=\"0\" style=\"veritical-align:middle;\"/> " + SepFunctions.LangText("Management") + "</a></span></li>";
                sMenu += "<li><span><a href=\"#\"><img src=\"" + sImageFolder + "skins/public/icons/topmenu/mainpage.png\" alt=\"Main Page Icon\" class=\"LinkIcon\" border=\"0\" style=\"veritical-align:middle;\"/> " + SepFunctions.LangText("Main Page") + "</a></span></li>";
                sMenu += "<li><span><a href=\"#\"><img src=\"" + sImageFolder + "skins/public/icons/topmenu/search.png\" alt=\"Search Icon\" class=\"LinkIcon\" border=\"0\" style=\"veritical-align:middle;\"/> " + SepFunctions.LangText("Search") + "</a></span></li>";
                sMenu += "<li><span><a href=\"#\"><img src=\"" + sImageFolder + "skins/public/icons/topmenu/rss.png\" alt=\"RSS Feed Icon\" class=\"LinkIcon\" border=\"0\" style=\"veritical-align:middle;\"/> " + SepFunctions.LangText("RSS Feed") + "</a></span></li>";
                sMenu += "<li><span><a href=\"#\"><img src=\"" + sImageFolder + "skins/public/icons/topmenu/email.png\" alt=\"Refer a Friend Icon\" class=\"LinkIcon\" border=\"0\" style=\"veritical-align:middle;\"/> " + SepFunctions.LangText("Refer a Friend") + "</a></span></li>";
                sMenu += "<li><span><a href=\"#\"><img src=\"" + sImageFolder + "skins/public/icons/topmenu/favorites.png\" alt=\"Add to Favorites Icon\" class=\"LinkIcon\" border=\"0\" style=\"veritical-align:middle;\"/> " + SepFunctions.LangText("Add to Favorites") + "</a></span></li>";
                sMenu += "</ul>";
                pageText = Strings.Replace(pageText, "[[ModuleTopMenu]]", sMenu);
            }

            return pageText;
        }

        /// <summary>
        /// Adms the replace CSS colors.
        /// </summary>
        /// <param name="sCSS">The s CSS.</param>
        /// <param name="sFileName">Name of the s file.</param>
        /// <returns>System.String.</returns>
        public string ADM_Replace_CSS_Colors(string sCSS, string sFileName)
        {
            XmlDocument m_xmld = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(sTempFolder + "template-colors.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    m_xmld.Load(reader);

                    var arrNames = new ArrayList
                    {
                        "BGColor",
                        "BGForeColor",
                        "HyperlinkColor",
                        "HyperlinkHoverCover",
                        "TopMenuButBG",
                        "TopMenuButForeColor",
                        "TopMenuButBGHover",
                        "TopMenuButForeColorHover",
                        "MenuLabelBG1",
                        "MenuLabelBG2",
                        "MenuLabelColor",
                        "TopMenuBG1",
                        "TopMenuBG2",
                        "ContentTopGradBG1",
                        "ContentTopGradBG2",
                        "ContentBotGradBG1",
                        "ContentBotGradBG2",
                        "HeaderHRBG",
                        "HeaderBG",
                        "HeaderColor",
                        "ContentSpacerBG",
                        "FooterBG",
                        "FooterColor",
                        "LegendColor",
                        "LegendBG",
                        "FieldsetColor",
                        "FieldsetBG",
                        "FieldsetBorder",
                        "FieldsetReqText",
                        "EventBorder",
                        "EventTitleBG",
                        "EventTitleColor",
                        "EventDaysBG",
                        "EventDaysColor",
                        "EventOnDaysBG",
                        "EventOnDaysColor",
                        "EventBlankDays",
                        "EventTodayBG",
                        "EventTodayColor",
                        "TableBorder",
                        "TableTitleBG",
                        "TableTitleColor",
                        "TableHeaderBG",
                        "TableHeaderColor",
                        "TableBody1BG",
                        "TableBody1Color",
                        "TableBody2BG",
                        "TableBody2Color",
                        "TableBodyHighlightBG",
                        "TableBodyHighlightColor",
                        "TopMenuBG",
                        "TopMenuColor",
                        "TopMenuBGHover",
                        "TopMenuColorHover",
                        "TopMenuBorderColor"
                    };

                    for (var i = 0; i <= arrNames.Count - 1; i++)
                        try
                        {
                            sCSS = Strings.Replace(sCSS, "[[" + Strings.ToString(arrNames[i]) + "]]", Strings.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item(Strings.ToString(arrNames[i]))) ? "#" + SepCommon.SepCore.Request.Item(Strings.ToString(arrNames[i])) : m_xmld.SelectSingleNode("//root/Name[@Type='" + Strings.ToString(arrNames[i]) + "']/HexCode").InnerText));
                        }
                        catch
                        {
                        }
                }
            }

            using (var objWriter = new StreamWriter(sTempFolder + sFileName))
            {
                objWriter.Write(sCSS);
            }

            return sCSS;
        }

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Loads the folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Load_Folder()
        {
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ModTemp")))
                if (Directory.Exists(SepFunctions.GetDirValue("App_Data") + "templates\\saved\\" + SepCommon.SepCore.Request.Item("ModTemp") + "\\"))
                    return SepFunctions.GetDirValue("App_Data") + "templates\\saved\\" + SepCommon.SepCore.Request.Item("ModTemp") + "\\";

            if (Directory.Exists(SepFunctions.GetDirValue("App_Data") + "templates\\saved\\CurrentTemp\\") && SepCommon.SepCore.Request.Item("ModTemp") != "NewTemp")
                return SepFunctions.GetDirValue("App_Data") + "templates\\saved\\CurrentTemp\\";
            return sTempFolder;
        }

        /// <summary>
        /// Replaces the template master.
        /// </summary>
        /// <param name="sHTML">The s HTML.</param>
        /// <returns>System.String.</returns>
        public string Replace_Template_Master(string sHTML)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                sHTML = Strings.Replace(sHTML, "[[PageText]]", "<form id=\"Form1\" runat=\"server\">" + Environment.NewLine + "<asp:ContentPlaceHolder ID=\"SiteContent\" runat=\"server\"/>" + Environment.NewLine + "</form>" + Environment.NewLine);

                if (Strings.InStr(sHTML, "[[AccountMenu]]") > 0) sHTML = Strings.Replace(sHTML, "[[AccountMenu]]", "<div class=\"accountmenu\"><% var cAccountMenu = new SepCityControls.AccountMenu();this.Response.Write(cAccountMenu.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[MemberStats]]") > 0) sHTML = Strings.Replace(sHTML, "[[MemberStats]]", "<div class=\"memberstatistics\"><% var cMemberStatistics = new SepCityControls.MemberStatistics();this.Response.Write(cMemberStatistics.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[Members]]") > 0) sHTML = Strings.Replace(sHTML, "[[Members]]", "<div class=\"listnewmembers\"><% var cListNewMembers = new SepCityControls.ListNewMembers();this.Response.Write(cListNewMembers.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[Calendar]]") > 0) sHTML = Strings.Replace(sHTML, "[[Calendar]]", "<div class=\"eventcalendar\"><% var cEventCalendar = new SepCityControls.EventCalendar();this.Response.Write(cEventCalendar.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[Friends]]") > 0) sHTML = Strings.Replace(sHTML, "[[Friends]]", "<div class=\"friendlist\"><% var cFriendList = new SepCityControls.FriendList();this.Response.Write(cFriendList.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[Polls]]") > 0) sHTML = Strings.Replace(sHTML, "[[Polls]]", "<div class=\"randompoll\"><% var cRandomPoll = new SepCityControls.RandomPoll();this.Response.Write(cRandomPoll.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[Stocks]]") > 0) sHTML = Strings.Replace(sHTML, "[[Stocks]]", "<div class=\"stockquotes\"><% var cStockQuotes = new SepCityControls.StockQuotes();this.Response.Write(cStockQuotes.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[WhosOn]]") > 0) sHTML = Strings.Replace(sHTML, "[[WhosOn]]", "<div class=\"whosonline\"><% var cWhosOnline = new SepCityControls.WhosOnline();this.Response.Write(cWhosOnline.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteLogo]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteLogo]]", "<div class=\"sitelogo\"><% var cSiteLogo = new SepCityControls.SiteLogo();this.Response.Write(cSiteLogo.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[UnreadMessages]]") > 0) sHTML = Strings.Replace(sHTML, "[[UnreadMessages]]", "<div class=\"unreadmessages\"><% var cUnreadMessages = new SepCityControls.UnreadMessages();this.Response.Write(cUnreadMessages.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[ViewCart]]") > 0) sHTML = Strings.Replace(sHTML, "[[ViewCart]]", "<div class=\"shoppingcart\"><% var cShoppingCart = new SepCityControls.ShoppingCart();this.Response.Write(cShoppingCart.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[Newsletters]]") > 0) sHTML = Strings.Replace(sHTML, "[[Newsletters]]", "<div class=\"newsletters\"><% var cNewsletters = new SepCityControls.Newsletters();this.Response.Write(cNewsletters.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu1]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu1]]", "<div class=\"sitemenu1v\"><% var cSiteMenu1 = new SepCityControls.SiteMenu();cSiteMenu1.MenuID = 1;cSiteMenu1.CssClass = \"flex-column\";this.Response.Write(cSiteMenu1.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu2]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu2]]", "<div class=\"sitemenu2v\"><% var cSiteMenu2 = new SepCityControls.SiteMenu();cSiteMenu2.MenuID = 2;cSiteMenu2.CssClass = \"flex-column\";this.Response.Write(cSiteMenu2.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu3]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu3]]", "<div class=\"sitemenu3v\"><% var cSiteMenu3 = new SepCityControls.SiteMenu();cSiteMenu3.MenuID = 3;cSiteMenu3.CssClass = \"flex-column\";this.Response.Write(cSiteMenu3.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu4]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu4]]", "<div class=\"sitemenu4v\"><% var cSiteMenu4 = new SepCityControls.SiteMenu();cSiteMenu4.MenuID = 4;cSiteMenu4.CssClass = \"flex-column\";this.Response.Write(cSiteMenu4.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu5]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu5]]", "<div class=\"sitemenu5v\"><% var cSiteMenu5 = new SepCityControls.SiteMenu();cSiteMenu5.MenuID = 5;cSiteMenu5.CssClass = \"flex-column\";this.Response.Write(cSiteMenu5.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu6]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu6]]", "<div class=\"sitemenu6v\"><% var cSiteMenu6 = new SepCityControls.SiteMenu();cSiteMenu6.MenuID = 6;cSiteMenu6.CssClass = \"flex-column\";this.Response.Write(cSiteMenu6.Render()); %></div>") + Environment.NewLine;

                if (Strings.InStr(sHTML, "[[SiteMenu7]]") > 0) sHTML = Strings.Replace(sHTML, "[[SiteMenu7]]", "<div class=\"sitemenu7v\"><% var cSiteMenu7 = new SepCityControls.SiteMenu();cSiteMenu7.MenuID = 7;cSiteMenu7.CssClass = \"flex-column\";this.Response.Write(cSiteMenu7.Render()); %></div>") + Environment.NewLine;

                if (SepFunctions.Setup(2, "AdsEnable") == "Enable")
                    using (var cmd = new SqlCommand("SELECT ZoneID,ZoneName FROM TargetZones WHERE ModuleID='2' ORDER BY ZoneName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                                while (RS.Read())
                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ZoneName"])))
                                        if (Strings.InStr(sHTML, "[[Ads|" + SepFunctions.openNull(RS["ZoneName"]) + "]]") > 0)
                                            sHTML = Strings.Replace(sHTML, "[[Ads|" + SepFunctions.openNull(RS["ZoneName"]) + "]]", "<span class=\"adserver\"><% var cAdServer = new SepCityControls.AdServer();" + Environment.NewLine + "cAdServer.ZoneID = \"" + SepFunctions.openNull(RS["ZoneID"]) + "\";" + Environment.NewLine + "this.Response.Write(cAdServer.Render()); %></span>") + Environment.NewLine;
                        }
                    }

                if (SepFunctions.Setup(1, "CREnable") == "Yes")
                    using (var cmd = new SqlCommand("SELECT ZoneID,ZoneName FROM TargetZones WHERE ModuleID='1' ORDER BY ZoneName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                                while (RS.Read())
                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ZoneName"])))
                                        if (Strings.InStr(sHTML, "[[CR|" + SepFunctions.openNull(RS["ZoneName"]) + "]]") > 0)
                                            sHTML = Strings.Replace(sHTML, "[[CR|" + SepFunctions.openNull(RS["ZoneName"]) + "]]", "<span class=\"contentrotator\"><% var cContentRotator = new SepCityControls.ContentRotator();" + Environment.NewLine + "cContentRotator.ZoneID = \"" + SepFunctions.openNull(RS["ZoneID"]) + "\";" + Environment.NewLine + "this.Response.Write(cContentRotator.Render()); %></span>") + Environment.NewLine;
                        }
                    }

                sHTML = Strings.Replace(sHTML, "[[SelectHeader1]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[SelectHeader2]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[TopMenu]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[ContentGrad1]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[ContentGrad2]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[ContentBody]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[ContentRight]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[ContentSpacer]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[Footer]]", string.Empty);
                sHTML = Strings.Replace(sHTML, "[[ModuleTopMenu]]", string.Empty);
            }

            return sHTML;
        }

        /// <summary>
        /// Writes the template master.
        /// </summary>
        /// <param name="sHTML">The s HTML.</param>
        public void Write_Template_Master(string sHTML)
        {
            var sMaster = new StringBuilder();

            var sOrganinalHTML = sHTML;

            // Write template.master file
            sMaster.Append("<%@ Master Language=\"VB\" %>" + Environment.NewLine);
            sMaster.Append("<%@ Register TagPrefix=\"sep\" Namespace=\"SepControls\" Assembly=\"SepControls\" %>" + Environment.NewLine);
            sMaster.Append("<!DOCTYPE html>" + Environment.NewLine);
            sMaster.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\">" + Environment.NewLine);
            sMaster.Append("<head id=\"PageHead\" runat=\"server\">" + Environment.NewLine);
            sMaster.Append("<title></title>" + Environment.NewLine);
            sMaster.Append("<asp:ContentPlaceHolder ID=\"EmbeddedScripts\" runat=\"server\"></asp:ContentPlaceHolder>" + Environment.NewLine);
            sMaster.Append("<asp:ContentPlaceHolder ID=\"HeadContent\" runat=\"server\"></asp:ContentPlaceHolder>" + Environment.NewLine);
            sMaster.Append("<link rel=\"stylesheet\" href=\"~/skins/template-colors.css\" type=\"text/css\"/>" + Environment.NewLine);
            sMaster.Append("<link rel=\"stylesheet\" href=\"~/skins/template-menus.css\" type=\"text/css\"/>" + Environment.NewLine);
            sMaster.Append("<link rel=\"stylesheet\" href=\"~/skins/template-layout.css\" type=\"text/css\"/>" + Environment.NewLine);
            sMaster.Append("</head>" + Environment.NewLine);
            sMaster.Append("<body>" + Environment.NewLine + Environment.NewLine);

            sHTML = Replace_Template_Master(sOrganinalHTML);

            sMaster.Append(sHTML + Environment.NewLine);

            sMaster.Append("</body>" + Environment.NewLine);
            sMaster.Append("</html>" + Environment.NewLine);

            using (var sw = new StreamWriter(sTempFolder + "template.master"))
            {
                sw.Write(Strings.ToString(sMaster));
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSiteLooks")))
            {
                UpdatePanel.Visible = false;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSiteLooks"), false) == false)
            {
                UpdatePanel.Visible = false;
                return;
            }

            Response.Buffer = true;
            Response.Expires = -1;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            sTemplateConfigXML = SepFunctions.GetDirValue("App_Data") + "templates\\template.xml";
            sTempFolder = SepFunctions.GetDirValue("App_Data") + "templates\\temp\\";

            var cReplace = new Replace();

            var sDefData = string.Empty;

            XmlDocument m_xmld = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(sTemplateConfigXML))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    m_xmld.Load(reader);
                    var sFileList = m_xmld.SelectSingleNode("//root/template" + sTemplateID + "/HTMLFiles").InnerText;
                    string[] sFileArray = null;

                    string[] sRightColumnArray = null;
                    var sRightColumn = m_xmld.SelectSingleNode("//root/template" + sTemplateID + "/RightColumn").InnerText;
                    var sRightColumnHeader = m_xmld.SelectSingleNode("//root/template" + sTemplateID + "/RightColumnHeader").InnerText;
                    var sRightColumnFooter = m_xmld.SelectSingleNode("//root/template" + sTemplateID + "/RightColumnFooter").InnerText;
                    var sModuleName = string.Empty;

                    var strCSS = string.Empty;
                    var HTMLData = string.Empty;
                    var HTMLRightColumnData = string.Empty;

                    var sMasterFile = string.Empty;
                    var sBodyContent = string.Empty;

                    if (SepCommon.SepCore.Request.Item("DoAction") != "Refresh")
                    {
                        if (!Directory.Exists(sTempFolder)) Directory.CreateDirectory(sTempFolder);

                        if (!File.Exists(sTempFolder + "template-colors.xml")) File.Copy(Load_Folder() + "template-colors.xml", sTempFolder + "template-colors.xml");
                    }

                    XmlDocument m_xmld_cfg = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader2 = new StreamReader(Load_Folder() + "template-config.xml"))
                    {
                        using (XmlReader reader3 = XmlReader.Create(sreader2, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            m_xmld_cfg.Load(reader3);

                            using (var sr = new StreamReader(SepFunctions.GetDirValue("App_Data") + "templates\\css\\template" + sTemplateID + "-layout.css"))
                            {
                                strCSS += ADM_Replace_CSS_Colors(sr.ReadToEnd(), "layout.css");
                            }

                            using (var sr = new StreamReader(SepFunctions.GetDirValue("App_Data") + "templates\\css\\template" + sTemplateID + "-colors.css"))
                            {
                                strCSS += ADM_Replace_CSS_Colors(sr.ReadToEnd(), "colors.css");
                            }

                            using (var sr = new StreamReader(SepFunctions.GetDirValue("App_Data") + "templates\\css\\template" + sTemplateID + "-menus.css"))
                            {
                                strCSS += ADM_Replace_CSS_Colors(sr.ReadToEnd(), "menus.css");
                            }

                            using (var sr = new StreamReader(SepFunctions.GetDirValue("App_Data") + "templates\\default-data.html"))
                            {
                                sDefData = ADM_Replace_Defaults(sr.ReadToEnd());
                            }

                            Stylesheet.InnerHtml = "<style type=\"text/css\" media=\"all\">";
                            Stylesheet.InnerHtml += strCSS;
                            Stylesheet.InnerHtml += "</style>";

                            // Get Body Content
                            sRightColumnArray = Strings.Split(sRightColumn, ",");
                            if (sRightColumnArray != null)
                            {
                                for (var j = 0; j <= Information.UBound(sRightColumnArray); j++)
                                {
                                    sModuleName = string.Empty;
                                    switch (sRightColumnArray[j])
                                    {
                                        case "[[MemberStats]]":
                                            sModuleName = "Members Statistics";
                                            break;

                                        case "[[AffiliateInfo]]":
                                            sModuleName = "Affiliate Information";
                                            break;

                                        case "[[Members]]":
                                            sModuleName = "Latest Members";
                                            break;

                                        case "[[AccountMenu]]":
                                            sModuleName = "Account Menu";
                                            break;

                                        case "[[Calendar]]":
                                            sModuleName = "Event Calendar";
                                            break;

                                        case "[[Friends]]":
                                            sModuleName = "Friends List";
                                            break;

                                        case "[[Polls]]":
                                            sModuleName = "Random Poll";
                                            break;

                                        case "[[Stocks]]":
                                            sModuleName = "Stock Quotes";
                                            break;

                                        case "[[WhosOn]]":
                                            sModuleName = "Who's Online";
                                            break;

                                        case "[[UnreadMessages]]":
                                            sModuleName = "Unread Messages";
                                            break;

                                        case "[[AffiliateStats]]":
                                            sModuleName = "Affiliate Statistics";
                                            break;

                                        case "[[ViewCart]]":
                                            sModuleName = "View Shopping Cart";
                                            break;

                                        case "[[Newsletters]]":
                                            sModuleName = "Newsletters";
                                            break;

                                        case "[[PortalList]]":
                                            sModuleName = "Portal Listing";
                                            break;

                                        case "[[SiteMenu1]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu1Text")) ? SepFunctions.Setup(993, "Menu1Text") : SepFunctions.LangText("Site Menu ~~1~~"));
                                            break;

                                        case "[[SiteMenu2]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu2Text")) ? SepFunctions.Setup(993, "Menu2Text") : SepFunctions.LangText("Site Menu ~~2~~"));
                                            break;

                                        case "[[SiteMenu3]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu3Text")) ? SepFunctions.Setup(993, "Menu3Text") : SepFunctions.LangText("Site Menu ~~3~~"));
                                            break;

                                        case "[[SiteMenu4]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu4Text")) ? SepFunctions.Setup(993, "Menu4Text") : SepFunctions.LangText("Site Menu ~~4~~"));
                                            break;

                                        case "[[SiteMenu5]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu5Text")) ? SepFunctions.Setup(993, "Menu5Text") : SepFunctions.LangText("Site Menu ~~5~~"));
                                            break;

                                        case "[[SiteMenu6]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu6Text")) ? SepFunctions.Setup(993, "Menu6Text") : SepFunctions.LangText("Site Menu ~~6~~"));
                                            break;

                                        case "[[SiteMenu7]]":
                                            sModuleName = Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu7Text")) ? SepFunctions.Setup(993, "Menu7Text") : SepFunctions.LangText("Site Menu ~~7~~"));
                                            break;
                                    }

                                    HTMLRightColumnData += SepFunctions.HTMLDecode(Strings.ToString(!string.IsNullOrWhiteSpace(sModuleName) ? Strings.Replace(sRightColumnHeader, "[[ModuleName]]", sModuleName) : string.Empty)) + sRightColumnArray[j] + SepFunctions.HTMLDecode(sRightColumnFooter);
                                }
                            }

                            sFileArray = Strings.Split(sFileList, "|");
                            if (sFileArray != null)
                            {
                                for (var i = 0; i <= Information.UBound(sFileArray); i++)
                                    using (var sr = new StreamReader(SepFunctions.GetDirValue("App_Data") + "templates\\html\\template" + sTemplateID + "-" + sFileArray[i] + ".html"))
                                    {
                                        HTMLData = Strings.Replace(sr.ReadToEnd(), "[[RightColumn]]", HTMLRightColumnData);
                                        sMasterFile += HTMLData + Environment.NewLine;
                                    }
                            }

                            Write_Template_Master(sMasterFile);

                            sBodyContent = Strings.Replace(sMasterFile, "[[PageText]]", sDefData);

                            sBodyContent = cReplace.Replace_Widgets(sBodyContent, 0, true);

                            TemplateContent.InnerHtml = ADM_Clickable_Functions(sBodyContent);
                            cReplace.Dispose();
                        }
                    }
                }
            }
        }
    }
}