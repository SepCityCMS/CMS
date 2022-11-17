// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AdminModuleMenu.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class AdminModuleMenu.
    /// </summary>
    public class AdminModuleMenu
    {
        /// <summary>
        /// The m menu identifier
        /// </summary>
        private string m_MenuID;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public string MenuID
        {
            get => Strings.ToString(m_MenuID);

            set => m_MenuID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (Request.Item("TopMenu") == "False")
            {
                return output.ToString();
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) == false)
            {
                return output.ToString();
            }

            if (ModuleID == 0)
            {
                return output.ToString();
            }

            var showSecurity = true;
            var showSetup = true;
            var showWebPages = true;
            var showCategory = false;
            var showCustom = false;
            var showPricing = false;
            var showReviews = false;
            var showZones = false;
            var showPoints = false;
            var showRecycle = false;
            var showActivities = false;
            var showChangeLog = false;

            var helpId = 0;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var GetMenuText = string.Empty;

            output.AppendLine("<nav class=\"navbar navbar-expand-lg navbar-dark bg-dark menu-to-top\">");
            output.AppendLine("<a class=\"navbar-brand\" href=\"#\"></a>");
            output.AppendLine("<button class=\"navbar-toggler\" type=\"button\" data-toggle=\"collapse\" data-target=\"#" + ID + "\" aria-controls=\"" + ID + "\" aria-expanded=\"false\" aria-label=\"Toggle navigation\">");
            output.AppendLine("<span class=\"navbar-toggler-icon\"></span>");
            output.AppendLine("</button>");

            output.AppendLine("<div class=\"collapse navbar-collapse\" id=\"" + ID + "\">");
            output.AppendLine("<ul class=\"navbar-nav\">");
            switch (ModuleID)
            {
                case 1:
                    showSecurity = false;
                    showSetup = false;
                    showWebPages = false;
                    showZones = true;
                    showRecycle = true;
                    helpId = 60;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Content") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"contentrotator.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"contentrotator_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Content") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 2:
                    showZones = true;
                    showRecycle = true;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Advertisements") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"banners.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"banners_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Advertisement") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuPrices" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Pricing") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuPrices" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"banners_pricing.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"banners_pricing_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Price") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 3:
                    showSecurity = false;
                    showSetup = false;
                    showWebPages = false;
                    helpId = 18;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"search_opt.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Google Sitemap") + "</a></li>");

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Additional Meta Tags") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"search_meta_tags.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"search_meta_tags_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Meta Tag") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 5:
                    showCategory = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 27;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Discounts") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"discounts.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"discounts_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Discount") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 7:
                    showCustom = true;
                    showPoints = true;
                    showCategory = true;
                    showActivities = true;
                    helpId = 53;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"userpages.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Sites") + "</a></li>");
                    break;

                case 9:
                    showCategory = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 30;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("FAQs") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"faq.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"faq_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add FAQ") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 10:
                    showCategory = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 31;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Files") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"downloads.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"downloads_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add File") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 12:
                    showCategory = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 33;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"forums.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Topics") + "</a></li>");
                    break;

                case 13:
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 32;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Forms") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"forms.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"forms_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Form") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"forms_submissions.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Submissions") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    if (!string.IsNullOrWhiteSpace(Request.Item("FormID")))
                    {
                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuForms" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Form Options") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuForms" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"forms_modify.aspx?FormID=" + Request.Item("FormID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Edit Form") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"forms_questions.aspx?FormID=" + Request.Item("FormID") + "&ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Questions") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"forms_questions_modify.aspx?FormID=" + Request.Item("FormID") + "&ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Question") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"forms_sections.aspx?FormID=" + Request.Item("FormID") + "&ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Sections") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"forms_sections_modify.aspx?FormID=" + Request.Item("FormID") + "&ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Section") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");
                    }

                    break;

                case 14:
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 34;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Entries") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"guestbook.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"guestbook_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Entry") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 18:
                    showCustom = true;
                    showReviews = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 39;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"matchmaker.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Profiles") + "</a></li>");
                    break;

                case 19:
                    showCategory = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 37;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Websites") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"linkdirectory.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"linkdirectory_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Website") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 20:
                    showCategory = true;
                    showCustom = true;
                    showPricing = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;

                    helpId = 25;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Businesses") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"business.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"business_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Business") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 23:
                    showRecycle = true;
                    showCategory = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 40;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("News") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"news.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"news_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add News") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 24:
                    showRecycle = true;
                    showActivities = true;
                    showWebPages = false;
                    helpId = 41;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Newsletters") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"newsletters.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"newsletters_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Newsletter") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"newsletters_send.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Send Newsletter") + "</a></li>");
                    break;

                case 25:
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 45;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Polls") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"polls.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"polls_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Poll") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 28:
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 43;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"photoalbums.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Photo Albums") + "</a></li>");
                    break;

                case 29:
                    showSetup = false;
                    showSecurity = false;
                    showWebPages = false;
                    showCustom = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 71;
                    break;

                case 31:
                    showCategory = true;
                    showCustom = true;
                    showPricing = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 23;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Ads") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"auction.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"auction_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Ad") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 32:
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 48;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Real Estate") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"realestate.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Properties") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"realestate_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Property") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"realestate_brokers.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Brokers") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"realestate_brokers_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Broker") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"realestate_agents.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Agents") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"realestate_agents_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Agent") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 35:
                    showCategory = true;
                    showCustom = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 22;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Articles") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"articles.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"articles_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Article") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 37:
                    showCategory = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 28;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Courses") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"elearning.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"elearning_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Course") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuExams" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Exams") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuExams" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"elearning_exams.aspx?ModuleID=" + ModuleID + "&CourseID=" + Request.Item("CourseID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"elearning_exams_modify.aspx?ModuleID=" + ModuleID + "&CourseID=" + Request.Item("CourseID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Exams") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuAssignment" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Assignments") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuAssignment" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"elearning_assignments.aspx?ModuleID=" + ModuleID + "&CourseID=" + Request.Item("CourseID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"elearning_assignments_modify.aspx?ModuleID=" + ModuleID + "&CourseID=" + Request.Item("CourseID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Assignment") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"elearning_students.aspx?ModuleID=" + ModuleID + "&CourseID=" + Request.Item("CourseID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Students") + "</a></li>");
                    break;

                case 39:
                    showWebPages = false;
                    helpId = 20;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Members") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"affiliate.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"affiliate_images.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Images") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"affiliate_image_upload.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Upload Image") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 40:
                    showPoints = true;
                    showActivities = true;
                    helpId = 35;

                    if (SepFunctions.Get_Portal_ID() == 0)
                    {
                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Ratings") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"hotornot.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"hotornot.aspx?DoAction=Reset&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Reset Ratings") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");
                    }
                    else
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"hotornot.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Ratings") + "</a></li>");
                    }

                    break;

                case 41:
                    showCategory = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 50;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Products") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"shoppingmall.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage Products") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"shoppingmall_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Product") + "</a>");
                    if (SepFunctions.Get_Portal_ID() == 0)
                    {
                        output.AppendLine("<a class=\"dropdown-item\" href=\"shopstores.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage Stores") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"shoppingmall_wholesale2b.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Wholesale2b Feeds") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"shoppingmall_wholesale2b_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Wholesale2b Feed") + "</a>");
                    }

                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 44:
                    showCategory = true;
                    showCustom = true;
                    showPricing = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 26;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Ads") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"classifiedads.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"classifiedads_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Ad") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 46:
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 29;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Events") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"eventcalendar.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"eventcalendar_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Event") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuTypes" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Event Types") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuTypes" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"events_types.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"events_types_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Event Type") + "</a>");
                    if (SepFunctions.Get_Portal_ID() == 0)
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ID FROM Scripts WHERE ScriptType='DEFETYPE' AND ModuleIDs LIKE '%|46|%'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<a class=\"dropdown-item\" href=\"" + sInstallFolder + "spadmin/events_types_default.aspx?DoAction=DelDefaultData&SQL=event-types&SType=DEFETYPE&ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Remove Default Types") + "</a>");
                                    }
                                    else
                                    {
                                        output.AppendLine("<a class=\"dropdown-item\" href=\"" + sInstallFolder + "spadmin/events_types_default.aspx?DoAction=AddDefaultData&SQL=event-types&SType=DEFETYPE&ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Default Types") + "</a>");
                                    }
                                }
                            }
                        }
                    }

                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 47:
                    helpId = 42;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"games.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Games") + "</a></li>");
                    break;

                case 50:
                    showRecycle = true;
                    showActivities = true;
                    helpId = 51;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Speakers") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"speakerbureau.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"speakerbureau_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Speaker") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"speakerbureau_topics.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Topics") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"speakerbureau_topics_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Topic") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 60:
                    showCategory = true;
                    showRecycle = true;
                    showActivities = true;
                    helpId = 46;
                    if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), false))
                    {
                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Portals") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals.aspx?PortalID=0\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_modify.aspx?ModuleID=" + ModuleID + "&PortalID=0\">" + SepFunctions.LangText("Add Portal") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_default_pages.aspx?ModuleID=" + ModuleID + "&PortalID=0\">" + SepFunctions.LangText("Default Pages") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");

                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuPricing" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Pricing Plans") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuPricing" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pricing.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage Plans") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pricing_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Plan") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");

                        if (SepFunctions.Get_Portal_ID() > 0)
                        {
                            output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuSet" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Current Portal") + "</a>");
                            output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuSet" + ModuleID + "\">");
                            output.AppendLine("<a class=\"dropdown-item\" href=\"portals_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Edit Portal") + "</a>");
                            output.AppendLine("<a class=\"dropdown-item\" href=\"portals_members.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Members") + "</a>");
                            output.AppendLine("<a class=\"dropdown-item\" href=\"portals_setup.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Settings") + "</a>");
                            output.AppendLine("<a class=\"dropdown-item\" href=\"portals_payment_gateways.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Payment Gateways") + "</a>");
                            output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pages.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Web Pages") + "</a>");
                            output.AppendLine("<a class=\"dropdown-item\" href=\"" + sImageFolder + "spadmin/default.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\" target=\"_blank\">" + SepFunctions.LangText("Admin Console") + "</a>");
                            output.AppendLine("</div>");
                            output.AppendLine("</li>");
                        }
                        else
                        {
                            var sPageName = Path.GetFileName(Request.PhysicalPath());

                            if (sPageName == "portals_default_pages.aspx" || sPageName == "portals_pages_modify.aspx" || sPageName == "portals_pages_link_modify.aspx")
                            {
                                output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuSet" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Default Pages Menu") + "</a>");
                                output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuSet" + ModuleID + "\">");
                                output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pages_modify.aspx?PortalID=-1\">" + SepFunctions.LangText("Add Web Page") + "</a>");
                                output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pages_link_modify.aspx?PortalID=-1\">" + SepFunctions.LangText("Add External Link") + "</a>");
                                for (var i = 1; i <= 7; i++)
                                {
                                    switch (i)
                                    {
                                        case 8:
                                            GetMenuText = SepFunctions.LangText("Account Info Top Menu");
                                            break;

                                        case 10:
                                            GetMenuText = SepFunctions.LangText("Account Info Main Menu");
                                            break;

                                        default:
                                            GetMenuText = SepFunctions.Setup(993, "Menu" + i + "Text");
                                            break;
                                    }

                                    if (string.IsNullOrWhiteSpace(GetMenuText))
                                    {
                                        GetMenuText = SepFunctions.LangText("Site Menu ~~" + i + "~~");
                                    }

                                    output.AppendLine("<a class=\"dropdown-item\" href=\"portals_default_pages.aspx?MenuID=" + i + "&PortalID=-1\">" + GetMenuText + "</a>");
                                }

                                output.AppendLine("</div>");
                                output.AppendLine("</li>");
                            }
                        }
                    }

                    break;

                case 61:
                    showPoints = true;
                    showRecycle = true;
                    showCategory = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 24;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"blogs.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Blogs") + "</a></li>");
                    break;

                case 63:
                    showCustom = true;
                    showReviews = true;
                    showPoints = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 54;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"userprofiles.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Profiles") + "</a></li>");
                    break;

                case 64:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    break;

                case 65:
                    showCategory = true;
                    showRecycle = true;
                    showActivities = true;
                    showChangeLog = true;
                    helpId = 75;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Vouchers") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"vouchers.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"vouchers_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Voucher") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"vouchers_purchased.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Purchased Vouchers") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 66:
                    showPricing = true;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Configure") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=CanDetail&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Candidate Detail Fields") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=CanSearch&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Candidate Search Result Fields") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=ComDetail&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Company Detail Fields") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=ComPost&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Company Posting Fields") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=JobDetail&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Job Detail Fields") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=JobPost&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Job Posting Fields") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"careers.aspx?DoAction=JobSearch&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Job Search Result Fields") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 980:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 61;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"dashboard.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Members") + "</a></li>");

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"dashboard_orders.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Invoices") + "</a></li>");
                    break;

                case 981:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 66;

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"module_stats.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Statistics") + "</a>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"module_stats_customize.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Customize") + "</a>");
                    output.AppendLine("</li>");
                    break;

                case 982:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 11;

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"custom_reports.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Print/Export") + "</a>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Manage Reports") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"custom_reports_manage.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"custom_reports_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Report") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 983:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    showRecycle = true;
                    helpId = 58;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Approval Chains") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"approval_chains.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"approval_chains_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Approval Chain") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"approval_chains_waiting.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Waiting Approval") + "</a>");
                    output.AppendLine("</li>");
                    break;

                case 984:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    showRecycle = true;
                    helpId = 72;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Site Templates") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"site_template.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"site_template_upload.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Upload Template") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    // output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuTemp" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Template Designer") + "</a>");
                    // output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuTemp" + ModuleID + "\">");
                    // output.AppendLine("<a class=\"dropdown-item\" href=\"site_template_new.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("New Template") + "</a>");
                    // output.AppendLine("<a class=\"dropdown-item\" href=\"#\" onclick=\"openTemplate();return false;\">" + SepFunctions.LangText("Open Template") + "</a>");
                    // output.AppendLine("</div>");
                    // output.AppendLine("</li>");
                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"site_template_directory.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Advance File Manager") + "</a>");
                    output.AppendLine("</li>");
                    break;

                case 985:
                    showRecycle = true;
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 64;

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"invoices.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Invoices") + "</a>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"invoices_stats.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Monthly Stats") + "</a>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item\">");
                    output.AppendLine("<a class=\"nav-link\" href=\"invoices_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Invoice") + "</a>");
                    output.AppendLine("</li>");
                    break;

                case 986:
                    showRecycle = true;
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 65;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Members") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"members.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"members_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("New Member") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"members_invite.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Invite Member") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    if (!string.IsNullOrWhiteSpace(Request.Item("UserID")))
                    {
                        output.AppendLine("<li class=\"nav-item\">");
                        output.AppendLine("<a class=\"nav-link\" href=\"members_modify.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Edit User") + "</a>");
                        output.AppendLine("</li>");

                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuActivity" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Activities") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuActivity" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"activities.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"activities_modify.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("New Activity") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"notes.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Notes") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");

                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuEmail" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Other") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuEmail" + ModuleID + "\">");
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AffiliateAdmin"), true) && SepFunctions.Get_Portal_ID() == 0 && SepFunctions.ModuleActivated(39))
                        {
                            output.AppendLine("<a class=\"dropdown-item\" href=\"affiliate_downline.aspx?AffiliateID=" + SepFunctions.GetUserInformation("AffiliateID", Request.Item("UserID")) + "&UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Affiliate Downline") + "</a>");
                        }

                        output.AppendLine("<a class=\"dropdown-item\" href=\"email_user.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Email User") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");

                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuInvoices" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Invoices") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuInvoices" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"invoices.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"invoices_modify.aspx?UserID=" + Request.Item("UserID") + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("New Invoice") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");
                    }

                    break;

                case 987:
                    showRecycle = true;
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 62;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Email Templates") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"email_templates.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"email_templates_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Email Template") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 989:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    showRecycle = true;
                    helpId = 59;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Classes") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"classes_keys.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"classes_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Class") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuKeys" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Keys") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuKeys" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"keys.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"keys_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Key") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"classes_switch.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Switch Users to Another Class") + "</a></li>");
                    break;

                case 990:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    showRecycle = true;
                    helpId = 63;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Group Lists") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"group_lists.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"group_lists_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Group List") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 995:
                    showWebPages = false;
                    showRecycle = true;
                    helpId = 49;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"shoppingcart.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Payment Gateways") + "</a></li>");

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Taxes") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"taxes.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"taxes_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Tax") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");

                    if (SepFunctions.isShippingEnabled(0))
                    {
                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"MenuShipping" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Shipping") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"MenuShipping" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"shopshipping.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"shopshipping_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Method") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");
                    }

                    break;

                case 998:
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    showRecycle = true;
                    helpId = 57;

                    output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Activities") + "</a>");
                    output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"activities.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"activities_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Activity") + "</a>");
                    output.AppendLine("</div>");
                    output.AppendLine("</li>");
                    break;

                case 999:
                    showRecycle = true;
                    showWebPages = false;
                    showSecurity = false;
                    showSetup = false;
                    helpId = 74;

                    var intMenuID = SepFunctions.toLong(MenuID);

                    if (SepFunctions.Get_Portal_ID() == 0)
                    {
                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Main") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"webpages.aspx?MenuID=" + intMenuID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"webpages_modify.aspx?MenuID=" + intMenuID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Web Page") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"webpages_link_modify.aspx?MenuID=" + intMenuID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add External Link") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");

                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"webpages.aspx?MenuID=0&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Not on a Menu") + "</a></li>");

                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            for (var i = 1; i <= 10; i++)
                            {
                                using (SqlCommand cmd = new SqlCommand("SELECT PageID FROM ModulesNPages WHERE Activated='1' AND UserPageName <> '' AND MenuID=@MenuID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@MenuID", i);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            switch (i)
                                            {
                                                case 8:
                                                    GetMenuText = SepFunctions.LangText("Account Info Top Menu");
                                                    break;

                                                case 10:
                                                    GetMenuText = SepFunctions.LangText("Account Info Main Menu");
                                                    break;

                                                default:
                                                    GetMenuText = SepFunctions.Setup(993, "Menu" + i + "Text");
                                                    break;
                                            }

                                            if (string.IsNullOrWhiteSpace(GetMenuText))
                                            {
                                                GetMenuText = SepFunctions.LangText("Site Menu ~~" + i + "~~");
                                            }

                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"webpages.aspx?MenuID=" + i + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + GetMenuText + "</a></li>");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Menu" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Main") + "</a>");
                        output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Menu" + ModuleID + "\">");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pages.aspx?MenuID=" + intMenuID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pages_modify.aspx?MenuID=" + intMenuID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Web Page") + "</a>");
                        output.AppendLine("<a class=\"dropdown-item\" href=\"portals_pages_link_modify.aspx?MenuID=" + intMenuID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add External Link") + "</a>");
                        output.AppendLine("</div>");
                        output.AppendLine("</li>");

                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"portals_pages.aspx?MenuID=0&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Not on a Menu") + "</a></li>");

                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            for (var i = 1; i <= 10; i++)
                            {
                                using (SqlCommand cmd = new SqlCommand("SELECT PageID FROM PortalPages WHERE UserPageName <> '' AND MenuID=@MenuID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@MenuID", i);
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            switch (i)
                                            {
                                                case 8:
                                                    GetMenuText = SepFunctions.LangText("Account Info Top Menu");
                                                    break;

                                                case 10:
                                                    GetMenuText = SepFunctions.LangText("Account Info Main Menu");
                                                    break;

                                                default:
                                                    GetMenuText = SepFunctions.PortalSetup("SiteMenu" + i, SepFunctions.Get_Portal_ID());
                                                    break;
                                            }

                                            if (string.IsNullOrWhiteSpace(GetMenuText))
                                            {
                                                GetMenuText = SepFunctions.LangText("Site Menu ~~" + i + "~~");
                                            }

                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"portals_pages.aspx?MenuID=" + i + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + GetMenuText + "</a></li>");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
            }

            if (showZones)
            {
                output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Zones" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Zones") + "</a>");
                output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Zones" + ModuleID + "\">");
                output.AppendLine("<a class=\"dropdown-item\" href=\"zones.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                output.AppendLine("<a class=\"dropdown-item\" href=\"zones_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Zone") + "</a>");
                output.AppendLine("</div>");
                output.AppendLine("</li>");
            }

            if (showCategory)
            {
                output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Cat" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Categories") + "</a>");
                output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Cat" + ModuleID + "\">");
                output.AppendLine("<a class=\"dropdown-item\" href=\"category.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                output.AppendLine("<a class=\"dropdown-item\" href=\"category_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Category") + "</a>");
                output.AppendLine("</div>");
                output.AppendLine("</li>");
            }

            if (showCustom)
            {
                output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Custom" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Custom Fields") + "</a>");
                output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Custom" + ModuleID + "\">");
                output.AppendLine("<a class=\"dropdown-item\" href=\"customfields.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                output.AppendLine("<a class=\"dropdown-item\" href=\"customfields_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Custom Field") + "</a>");
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    output.AppendLine("<a class=\"dropdown-item\" href=\"customfields_sections.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Sections") + "</a>");
                    output.AppendLine("<a class=\"dropdown-item\" href=\"customfields_sections_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Section") + "</a>");
                }

                output.AppendLine("</div>");
                output.AppendLine("</li>");
            }

            if (showReviews)
            {
                output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Reviews" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Reviews") + "</a>");
                output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Reviews" + ModuleID + "\">");
                output.AppendLine("<a class=\"dropdown-item\" href=\"reviews.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Manage") + "</a>");
                output.AppendLine("<a class=\"dropdown-item\" href=\"reviews_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Add Review") + "</a>");
                output.AppendLine("</div>");
                output.AppendLine("</li>");
            }

            if ((SepFunctions.Get_Portal_ID() == 0 || ModuleID == 60) && showWebPages)
            {
                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"webpages_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + Strings.ToString(showCategory ? "&ShowCat=True" : string.Empty) + "\">" + SepFunctions.LangText("Web Pages") + "</a></li>");
            }

            if (SepFunctions.Get_Portal_ID() > 0 && showWebPages)
            {
                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"portals_pages_modify.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + Strings.ToString(showCategory ? "&ShowCat=True" : string.Empty) + "\">" + SepFunctions.LangText("Web Pages") + "</a></li>");
            }

            if ((SepFunctions.Get_Portal_ID() == 0 || ModuleID == 60) && showActivities)
            {
                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"activities.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Activities") + "</a></li>");
            }

            if (SepFunctions.Get_Portal_ID() == 0 && showChangeLog)
            {
                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"changelog.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Change Log") + "</a></li>");
            }

            if ((SepFunctions.Get_Portal_ID() == 0 || ModuleID == 60) && showRecycle)
            {
                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"recyclebin.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Recycle Bin") + "</a></li>");
            }

            if (SepFunctions.Get_Portal_ID() == 0 && (showPricing || showSetup || showSecurity || showPoints || ModuleID == 60))
            {
                output.AppendLine("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle\" id=\"Settings" + ModuleID + "\" aria-haspopup=\"true\" aria-expanded=\"false\" data-toggle=\"dropdown\" href=\"#\">" + SepFunctions.LangText("Settings") + "</a>");
                output.AppendLine("<div class=\"dropdown-menu\" aria-labelledby=\"Settings" + ModuleID + "\">");
                if (showPricing)
                {
                    output.AppendLine("<a class=\"dropdown-item\" href=\"pricing.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Pricing") + "</a>");
                }

                if ((SepFunctions.Get_Portal_ID() == 0 || ModuleID == 60) && showPoints)
                {
                    output.AppendLine("<a class=\"dropdown-item\" href=\"points.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Credit System") + "</a>");
                }

                if ((SepFunctions.Get_Portal_ID() == 0 || ModuleID == 60) && showSetup)
                {
                    output.AppendLine("<a class=\"dropdown-item\" href=\"setup.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("General Setup") + "</a>");
                }

                if ((SepFunctions.Get_Portal_ID() == 0 || ModuleID == 60) && showSecurity)
                {
                    output.AppendLine("<a class=\"dropdown-item\" href=\"security.aspx?ModuleID=" + ModuleID + "&PortalID=" + SepFunctions.Get_Portal_ID() + "\">" + SepFunctions.LangText("Security Roles") + "</a>");
                }

                output.AppendLine("</div>");
                output.AppendLine("</li>");
            }

            if (helpId > 0)
            {
                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"javascript:void(0)\" onclick=\"loadHelp();return false;\">" + SepFunctions.LangText("Help") + "</a></li>");
            }

            output.AppendLine("</ul>");
            output.AppendLine("</div>");
            output.AppendLine("</nav>");

            if (helpId > 0)
            {
                output.AppendLine("<div id=\"helpDialog\" style=\"display:none;overflow:hidden;\" title=\"" + SepFunctions.LangText("Help") + "\"><b>" + SepFunctions.LangText("What do you need help with?") + "</b><br/><br/><ul class=\"navbar-nav\"><li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "help/default.aspx?HelpId=" + helpId + "\" target=\"_blank\">" + SepFunctions.LangText("Help for this Page") + "</a></li><li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "help/default.aspx\" target=\"_blank\">" + SepFunctions.LangText("Help Topics") + "</a></li></ul><br/><span class=\"helpCopyright\">Version " + SepFunctions.GetVersion() + "<br/>&copy; 2002-" + DateAndTime.Year(DateTime.Today) + "</span></div>");
            }

            if (ModuleID == 984)
            {
                output.AppendLine("<div id=\"OpenTempFrame\" style=\"display:none;overflow:hidden;\" title=\"" + SepFunctions.LangText("Open Template") + "\"><iframe style=\"width:90%;height:100px\" name=\"OpenTemplateFrame\" id=\"OpenTemplateFrame\" frameborder=\"0\" src=\"about:blank\"></iframe></div>");
            }

            return output.ToString();
        }
    }
}