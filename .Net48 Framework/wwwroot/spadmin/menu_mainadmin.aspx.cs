// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="menu_mainadmin.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Web.UI;

    /// <summary>
    /// Class mainmenu.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class mainmenu : Page
    {
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
        /// The c common
        /// </summary>
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
            var iPortalID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"));

            var sInstallFolder = SepFunctions.GetInstallFolder();

            switch (SepCommon.SepCore.Request.Item("folder"))
            {
                case "Modules":
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true))
                    {
                        Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdsAdmin"), true) && iPortalID == 0) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID02\" rel=\"banners.aspx\">" + SepFunctions.LangText("Advertising") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AffiliateAdmin"), true) && iPortalID == 0 && SepFunctions.ModuleActivated(39)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID39\" rel=\"affiliate.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Affiliate Program") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("ArticlesAdmin"), true) && SepFunctions.ModuleActivated(35)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID35\" rel=\"articles.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Articles") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AuctionAdmin"), true) && SepFunctions.ModuleActivated(31)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID31\" rel=\"auction.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Auction") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("BlogsAdmin"), true) && SepFunctions.ModuleActivated(61)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID61\" rel=\"blogs.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Blogs") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("BusinessAdmin"), true) && SepFunctions.ModuleActivated(20)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID20\" rel=\"business.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Business Directory") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("ClassifiedAdmin"), true) && SepFunctions.ModuleActivated(44)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID44\" rel=\"classifiedads.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Classified Ads") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) && iPortalID == 0) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID01\" rel=\"contentrotator.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Content Rotator") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("DiscountsAdmin"), true) && SepFunctions.ModuleActivated(5)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID05\" rel=\"discounts.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Discounts") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("LibraryAdmin"), true) && SepFunctions.ModuleActivated(10)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID10\" rel=\"downloads.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Downloads") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("ELearningAdmin"), true) && SepFunctions.ModuleActivated(37)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID37\" rel=\"elearning.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("E-Learning") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("EventsAdmin"), true) && SepFunctions.ModuleActivated(46)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID46\" rel=\"eventcalendar.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Event Calendar") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("FAQAdmin"), true) && SepFunctions.ModuleActivated(9)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID09\" rel=\"faq.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("FAQ") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("FormsAdmin"), true) && SepFunctions.ModuleActivated(13)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID13\" rel=\"forms.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Forms") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("ForumsAdmin"), true) && SepFunctions.ModuleActivated(12)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID12\" rel=\"forums.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Forums") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("GuestbookAdmin"), true) && SepFunctions.ModuleActivated(14)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID14\" rel=\"guestbook.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Guestbook") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("HotNotAdmin"), true) && SepFunctions.ModuleActivated(40)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID40\" rel=\"hotornot.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Hot or Not") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("LinksAdmin"), true) && SepFunctions.ModuleActivated(19)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID19\" rel=\"linkdirectory.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Link Directory") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("MatchAdmin"), true) && SepFunctions.ModuleActivated(18)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID18\" rel=\"matchmaker.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Match Maker") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("NewsAdmin"), true) && SepFunctions.ModuleActivated(23)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID23\" rel=\"news.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("News") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("GamesAdmin"), true) && SepFunctions.ModuleActivated(47)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID47\" rel=\"games.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Online Games") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("PCRAdmin"), true) && SepFunctions.ModuleActivated(66)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID66\" rel=\"careers.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Job Board") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("PhotosAdmin"), true) && SepFunctions.ModuleActivated(28)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID28\" rel=\"photoalbums.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Photo Albums") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("PollsAdmin"), true) && SepFunctions.ModuleActivated(25)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID25\" rel=\"polls.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Polls") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), true) && SepFunctions.ModuleActivated(60) && iPortalID == 0) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID60\" rel=\"portals.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Portals") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("RStateAdmin"), true) && SepFunctions.ModuleActivated(32)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID32\" rel=\"realestate.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Real Estate") + "</a></li>" + Environment.NewLine);
                        if (iPortalID == 0)
                        {
                            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin"), true)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID995\" rel=\"shoppingcart.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Shopping Cart") + "</a></li>" + Environment.NewLine);
                        }
                        else
                        {
                            Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID995\" rel=\"portals_payment_gateways.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Shopping Cart") + "</a></li>" + Environment.NewLine);
                        }

                        if (SepFunctions.CompareKeys(SepFunctions.Security("ShopMallAdmin"), true) && SepFunctions.ModuleActivated(41)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID41\" rel=\"shoppingmall.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Shopping Mall") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) && SepFunctions.ModuleActivated(68) == false)
                        {
                            if (SepCommon.SepCore.Request.Item("DoAction") == "FromHelp") Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID0\" rel=\"signup.aspx?ModuleID=29&PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Signup") + "</a></li>" + Environment.NewLine);
                            else Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID0\" rel=\"customfields.aspx?ModuleID=29&PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Signup") + "</a></li>" + Environment.NewLine);
                        }

                        if (SepFunctions.CompareKeys(SepFunctions.Security("SpeakerAdmin"), true) && SepFunctions.ModuleActivated(50)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID50\" rel=\"speakerbureau.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Speakers Bureau") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("UPagesManage"), true) && SepFunctions.ModuleActivated(7)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID07\" rel=\"userpages.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("User Pages") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("ProfilesAdmin"), true) && SepFunctions.ModuleActivated(63)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID63\" rel=\"userprofiles.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("User Profiles") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("VoucherAdmin"), true) && SepFunctions.ModuleActivated(65)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" id=\"ModuleID65\" rel=\"vouchers.aspx?PortalID=" + iPortalID + "\">" + SepFunctions.LangText("Vouchers") + "</a></li>" + Environment.NewLine);
                        Response.Write("</ul>" + Environment.NewLine);
                    }

                    break;

                case "Memberships":
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminUserMan"), true) || SepFunctions.CompareKeys(SepFunctions.Security("AffiliateAdmin")) || SepFunctions.CompareKeys(SepFunctions.Security("GroupLists")) || SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity")) || SepFunctions.CompareKeys(SepFunctions.Security("NewsletAdmin")) || SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin")))
                    {
                        Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AffiliateAdmin")) && iPortalID == 0 && SepFunctions.ModuleActivated(39)) Response.Write("<li class=\"affiliate\"><a href=\"#\" rel=\"affiliate.aspx?PortalID=" + iPortalID + "\"id=\"Affiliate\">" + SepFunctions.LangText("Affiliate") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("GroupLists")) && iPortalID == 0) Response.Write("<li class=\"grouplists\"><a href=\"#\" rel=\"group_lists.aspx?PortalID=" + iPortalID + "\" id=\"GroupLists\">" + SepFunctions.LangText("Group Lists") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity")) && iPortalID == 0 && SepFunctions.isProfessionalEdition())
                            if (SepFunctions.ModuleActivated(68) == false)
                                Response.Write("<li class=\"classeskeys\"><a href=\"#\" rel=\"classes_keys.aspx?PortalID=" + iPortalID + "\" id=\"ClassesKeys\">" + SepFunctions.LangText("Classes/Keys") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("NewsletAdmin")) && iPortalID == 0) Response.Write("<li class=\"newsletters\"><a href=\"#\" rel=\"newsletters.aspx?PortalID=" + iPortalID + "\" id=\"Newsletters\">" + SepFunctions.LangText("Newsletters") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin"))) Response.Write("<li class=\"invoices\"><a href=\"#\" rel=\"invoices.aspx?PortalID=" + iPortalID + "\" id=\"Invoices\">" + SepFunctions.LangText("Invoices") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminUserMan")))
                        {
                            if (iPortalID == 0) Response.Write("<li class=\"members\"><a href=\"#\" rel=\"members.aspx?PortalID=" + iPortalID + "\" id=\"Members\">" + SepFunctions.LangText("Members") + "</a></li>" + Environment.NewLine);
                            else Response.Write("<li class=\"members\"><a href=\"#\" rel=\"portals_members.aspx?PortalID=" + iPortalID + "\" id=\"Members\">" + SepFunctions.LangText("Members") + "</a></li>" + Environment.NewLine);
                        }

                        Response.Write("</ul>" + Environment.NewLine);
                    }

                    break;

                case "Utilities":
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false) && iPortalID == 0)
                    {
                        Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                        if (SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"approve\"><a href=\"#\" rel=\"approval_chains.aspx?PortalID=" + iPortalID + "\" id=\"ApprovalChains\">" + SepFunctions.LangText("Approval Chains") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.showCategories()) Response.Write("<li class=\"category\"><a href=\"#\" rel=\"category.aspx?PortalID=" + iPortalID + "\" id=\"CategorySetup\">" + SepFunctions.LangText("Category Setup") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"database\"><a href=\"#\" rel=\"database_tools.aspx?PortalID=" + iPortalID + "\" id=\"DatabaseTools\">" + SepFunctions.LangText("Database Tools") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"email\"><a href=\"#\" rel=\"email_templates.aspx?PortalID=" + iPortalID + "\" id=\"EmailTemplates\">" + SepFunctions.LangText("Email Templates") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"changedomain\"><a href=\"#\" rel=\"change_domain.aspx?PortalID=" + iPortalID + "\" id=\"ChangeDomain\">" + SepFunctions.LangText("Change Domain") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.ModuleActivated(35) || SepFunctions.ModuleActivated(31) || SepFunctions.ModuleActivated(20) || SepFunctions.ModuleActivated(44) || SepFunctions.ModuleActivated(10) || SepFunctions.ModuleActivated(46) || SepFunctions.ModuleActivated(18) || SepFunctions.ModuleActivated(23) || SepFunctions.ModuleActivated(28) || SepFunctions.ModuleActivated(52) || SepFunctions.ModuleActivated(51) || SepFunctions.ModuleActivated(32) || SepFunctions.ModuleActivated(41) || SepFunctions.ModuleActivated(50) || SepFunctions.ModuleActivated(45) || SepFunctions.ModuleActivated(63)) Response.Write("<li class=\"imagesizing\"><a href=\"#\" rel=\"image_sizing.aspx?PortalID=" + iPortalID + "\" id=\"ImageSizing\">" + SepFunctions.LangText("Image Sizing") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"import\"><a href=\"#\" rel=\"import_utility.aspx?PortalID=" + iPortalID + "\" id=\"ImportUtility\">" + SepFunctions.LangText("Import Utility") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"search\"><a href=\"#\" rel=\"search_opt.aspx?PortalID=" + iPortalID + "\" id=\"SearchOptimization\">" + SepFunctions.LangText("Search Optimization") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminActivities"), false)) Response.Write("<li class=\"activities\"><a href=\"#\" rel=\"activities.aspx?PortalID=" + iPortalID + "\" id=\"Activities\">" + SepFunctions.LangText("Activities") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"import\"><a href=\"#\" rel=\"api.aspx?PortalID=" + iPortalID + "\" id=\"ImportUtility\">" + SepFunctions.LangText("OAuth / API") + "</a></li>" + Environment.NewLine);
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false)) Response.Write("<li class=\"import\"><a href=\"#\" rel=\"api_calls.aspx?PortalID=" + iPortalID + "\"  id=\"ImportUtility\">" + SepFunctions.LangText("API Calls") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"systemdiag\"><a href=\"#\" rel=\"system_diagnostics.aspx?PortalID=" + iPortalID + "\" id=\"SystemDiagnostics\">" + SepFunctions.LangText("System Diagnostics") + "</a></li>" + Environment.NewLine);
                        Response.Write("</ul>" + Environment.NewLine);
                    }

                    break;

                case "Analytics":
                    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    Response.Write("<li class=\"members\"><a href=\"#\" rel=\"../dashboard/default.aspx\" id=\"Dashboard\">" + SepFunctions.LangText("Members") + "</a>" + Environment.NewLine);
                    Response.Write("<li class=\"activities\"><a href=\"#\" rel=\"../dashboard/activities.aspx\" id=\"DashActivity\">" + SepFunctions.LangText("Activities") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"invoices\"><a href=\"#\" rel=\"../dashboard/sales.aspx\" id=\"DashSales\">" + SepFunctions.LangText("Sales") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"classeskeys\"><a href=\"#\" rel=\"../dashboard/account.aspx\" id=\"DashActivity\">" + SepFunctions.LangText("Login / Signup") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"../dashboard/modules.aspx\" id=\"DashActivity\">" + SepFunctions.LangText("Modules") + "</a></li>" + Environment.NewLine);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleAnalyticsID")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleAnalyticsClientID"))) Response.Write("<li class=\"sitestats\"><a href=\"#\" rel=\"site_stats.aspx?PortalID=" + iPortalID + "\" id=\"SiteStats\">" + SepFunctions.LangText("Site Statistics") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), false)) Response.Write("<li class=\"modulestats\"><a href=\"#\" rel=\"module_stats.aspx?DoAction=ModuleStats&PortalID=" + iPortalID + "\"  id=\"ModuleStats\">" + SepFunctions.LangText("Module Stats") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"reports\"><a href=\"#\" rel=\"custom_reports.aspx?PortalID=" + iPortalID + "\" id=\"CustomReports\">" + SepFunctions.LangText("Custom Reports") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminErrorLogs"), false))
                    {
                        Response.Write("<li class=\"members\"><a href=\"#\" rel=\"member_errors.aspx?PortalID=" + iPortalID + "\" id=\"MemberErrors\">" + SepFunctions.LangText("Member Errors") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"logerror\"><a href=\"#\" rel=\"log_errors.aspx?PortalID=" + iPortalID + "\" id=\"LogErrors\">" + SepFunctions.LangText("Log File Errors") + "</a></li>" + Environment.NewLine);
                    }

                    Response.Write("</ul>" + Environment.NewLine);
                    break;

                default:
                    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    Response.Write("<li class=\"directoryplus expanded\"><a href=\"#\" rel=\"Analytics\" id=\"Analytics\">" + SepFunctions.LangText("Analytics") + "</a>");
                    Response.Write("<ul class=\"jqueryFileTree\">" + Environment.NewLine);
                    Response.Write("<li class=\"members\"><a href=\"#\" rel=\"../dashboard/default.aspx\" id=\"Dashboard\">" + SepFunctions.LangText("Members") + "</a>" + Environment.NewLine);
                    Response.Write("<li class=\"activities\"><a href=\"#\" rel=\"../dashboard/activities.aspx\" id=\"DashActivity\">" + SepFunctions.LangText("Activities") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"invoices\"><a href=\"#\" rel=\"../dashboard/sales.aspx\" id=\"DashSales\">" + SepFunctions.LangText("Sales") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"classeskeys\"><a href=\"#\" rel=\"../dashboard/account.aspx\" id=\"DashActivity\">" + SepFunctions.LangText("Login / Signup") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"../dashboard/modules.aspx\" id=\"DashActivity\">" + SepFunctions.LangText("Modules") + "</a></li>" + Environment.NewLine);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleAnalyticsID")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleAnalyticsClientID"))) Response.Write("<li class=\"sitestats\"><a href=\"#\" rel=\"site_stats.aspx?PortalID=" + iPortalID + "\" id=\"SiteStats\">" + SepFunctions.LangText("Site Statistics") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), false)) Response.Write("<li class=\"modulestats\"><a href=\"#\" rel=\"module_stats.aspx?DoAction=ModuleStats&PortalID=" + iPortalID + "\"  id=\"ModuleStats\">" + SepFunctions.LangText("Module Stats") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"reports\"><a href=\"#\" rel=\"custom_reports.aspx?PortalID=" + iPortalID + "\" id=\"CustomReports\">" + SepFunctions.LangText("Custom Reports") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminErrorLogs"), false))
                    {
                        Response.Write("<li class=\"members\"><a href=\"#\" rel=\"member_errors.aspx?PortalID=" + iPortalID + "\" id=\"MemberErrors\">" + SepFunctions.LangText("Member Errors") + "</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"logerror\"><a href=\"#\" rel=\"log_errors.aspx?PortalID=" + iPortalID + "\" id=\"LogErrors\">" + SepFunctions.LangText("Log File Errors") + "</a></li>" + Environment.NewLine);
                    }

                    Response.Write("</ul>" + Environment.NewLine);
                    Response.Write("</li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true)) Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Modules\" id=\"Modules\">" + SepFunctions.LangText("Modules") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminUserMan"), true)) Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Memberships\" id=\"Memberships\">" + SepFunctions.LangText("Memberships") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminEditPage")))
                    {
                        if (iPortalID == 0 || SepCommon.SepCore.Request.Item("DoAction") == "FromHelp") Response.Write("<li class=\"webpages\"><a href=\"#\" rel=\"webpages.aspx?PortalID=" + iPortalID + "\" id=\"WebPages\">" + SepFunctions.LangText("Web Pages") + "</a></li>" + Environment.NewLine);
                        else Response.Write("<li class=\"webpages\"><a href=\"#\" rel=\"portals_pages.aspx?PortalID=" + iPortalID + "\" id=\"WebPages\">" + SepFunctions.LangText("Web Pages") + "</a></li>" + Environment.NewLine);
                    }

                    if ((SepFunctions.Setup(60, "PortalSiteLooks") == "Yes" || iPortalID == 0) && SepFunctions.CompareKeys(SepFunctions.Security("AdminSiteLooks"), true))
                    {
                        if (iPortalID == 0) Response.Write("<li class=\"template\"><a href=\"#\" rel=\"site_template.aspx?PortalID=" + iPortalID + "\" id=\"SiteTemplate\">" + SepFunctions.LangText("Site Template") + "</a></li>" + Environment.NewLine);
                        else Response.Write("<li class=\"template\"><a href=\"#\" rel=\"portals_template.aspx?PortalID=" + iPortalID + "\" id=\"SiteTemplate\">" + SepFunctions.LangText("Site Template") + "</a></li>" + Environment.NewLine);
                    }

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false) && iPortalID == 0) Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Utilities\" id=\"Utilities\">" + SepFunctions.LangText("Utilities") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminRecycleBin"), false) && iPortalID == 0) Response.Write("<li class=\"recyclebin\"><a href=\"#\" rel=\"recyclebin.aspx?PortalID=" + iPortalID + "\" id=\"RecycleBin\">" + SepFunctions.LangText("Recycle Bin") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSetup")))
                    {
                        if (iPortalID == 0 || SepCommon.SepCore.Request.Item("DoAction") == "FromHelp") Response.Write("<li class=\"setup\"><a href=\"#\" rel=\"setup.aspx?PortalID=" + iPortalID + "\" id=\"GeneralSetup\">" + SepFunctions.LangText("General Setup") + "</a></li>" + Environment.NewLine);
                        else Response.Write("<li class=\"setup\"><a href=\"#\" rel=\"portals_setup.aspx?PortalID=" + iPortalID + "\"  id=\"GeneralSetup\">" + SepFunctions.LangText("General Setup") + "</a></li>" + Environment.NewLine);
                    }

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), false) && iPortalID == 0) Response.Write("<li class=\"security\"><a href=\"#\" rel=\"security.aspx?PortalID=" + iPortalID + "\" id=\"SecurityRoles\">" + SepFunctions.LangText("Security Roles") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminPointSys"), false) && iPortalID == 0 && SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"pointing\"><a href=\"#\" rel=\"points.aspx?PortalID=" + iPortalID + "\" id=\"PointingSystem\">" + SepFunctions.LangText("Credit System") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("SepCityStore"), false) && iPortalID == 0 && SepFunctions.isProfessionalEdition() == false && SepCommon.SepCore.Request.Item("DoAction") != "FromHelp") Response.Write("<li class=\"sepcitystore\"><a href=\"#\" id=\"SepCityStore\" rel=\"http://www.sepcity.com/\">" + SepFunctions.LangText("SepCity Store") + "</a></li>");
                    if (SepFunctions.CompareKeys("|2|", false) && iPortalID == 0) Response.Write("<li class=\"activation\"><a href=\"#\" id=\"Activation\" rel=\"activation.aspx\">" + SepFunctions.LangText("Activation") + "</a></li>");
                    if (SepCommon.SepCore.Request.Item("DoAction") != "FromHelp") Response.Write("<li class=\"help\"><a href=\"#\" rel=\"" + sInstallFolder + "help/default.aspx\" target=\"_blank\" id=\"HelpManual\">" + SepFunctions.LangText("Help Manual") + "</a></li>" + Environment.NewLine);
                    if (SepCommon.SepCore.Request.Item("DoAction") == "FromHelp")
                    {
                        Response.Write("<li class=\"webservices\"><a href=\"#\" rel=\"" + sInstallFolder + "apidocs/index\" id=\"WebServices\">" + SepFunctions.LangText("Web Services (API)") + "</a></li>" + Environment.NewLine);
                    }

                    Response.Write("</ul>" + Environment.NewLine);
                    break;
            }
        }
    }
}