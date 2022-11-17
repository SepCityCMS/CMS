// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="menu_points.aspx.cs" company="SepCity, Inc.">
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
    /// Class menu_points.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class menu_points : Page
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
            switch (Context.Request.Form["folder"])
            {
                case "Modules":
                    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(35)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID35\" id=\"ModuleID35\">" + SepFunctions.LangText("Articles") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(31)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID31\" id=\"ModuleID31\">" + SepFunctions.LangText("Auction") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(61)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID61\" id=\"ModuleID61\">" + SepFunctions.LangText("Blogger") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(20)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID20\" id=\"ModuleID20\">" + SepFunctions.LangText("Business Directory") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(44)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID44\" id=\"ModuleID44\">" + SepFunctions.LangText("Classified Ads") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(64)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID64\" id=\"ModuleID64\">" + SepFunctions.LangText("Conference Center") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(5)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID5\" id=\"ModuleID5\">" + SepFunctions.LangText("Discount System") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(46)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID46\" id=\"ModuleID46\">" + SepFunctions.LangText("Event Calendar") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(10)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID10\" id=\"ModuleID10\">" + SepFunctions.LangText("Downloads") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(13)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID13\" id=\"ModuleID13\">" + SepFunctions.LangText("Form Creator") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(12)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID12\" id=\"ModuleID12\">" + SepFunctions.LangText("Forums") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(14)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID14\" id=\"ModuleID14\">" + SepFunctions.LangText("Guestbook") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(40)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID40\" id=\"ModuleID40\">" + SepFunctions.LangText("Hot or Not") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(66)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID66\" id=\"ModuleID66\">" + SepFunctions.LangText("Job Board") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(48)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID48\" id=\"ModuleID48\">" + SepFunctions.LangText("Job Listings") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(19)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID19\" id=\"ModuleID19\">" + SepFunctions.LangText("Links") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(18)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID18\" id=\"ModuleID18\">" + SepFunctions.LangText("Match Maker") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID17\" id=\"ModuleID17\">" + SepFunctions.LangText("Messenger") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(28)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID28\" id=\"ModuleID28\">" + SepFunctions.LangText("Photo Albums") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(25)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID25\" id=\"ModuleID25\">" + SepFunctions.LangText("Polls") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(32)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID32\" id=\"ModuleID32\">" + SepFunctions.LangText("Real Estate") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(43)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID43\" id=\"ModuleID43\">" + SepFunctions.LangText("Refer a Friend") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(7)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID7\" id=\"ModuleID7\">" + SepFunctions.LangText("User Pages") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(63)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID63\" id=\"ModuleID63\">" + SepFunctions.LangText("User Profiles") + "</a></li>" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(36)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID36\" id=\"ModuleID36\">" + SepFunctions.LangText("User Reviews") + "</a></li>" + Environment.NewLine);
                    Response.Write("</ul>" + Environment.NewLine);
                    break;

                case "Website":
                    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    if (SepFunctions.ModuleActivated(989)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989\" id=\"ModuleID989\">" + SepFunctions.LangText("Access Class") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID8\" id=\"ModuleID8\">" + SepFunctions.LangText("Comments & Ratings") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID997\" id=\"ModuleID997\">" + SepFunctions.LangText("Signup Setup") + "</a></li>" + Environment.NewLine);
                    Response.Write("</ul>" + Environment.NewLine);
                    break;

                default:
                    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Modules\" id=\"Modules\">" + SepFunctions.LangText("Modules") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Website\" id=\"Website\">" + SepFunctions.LangText("Website") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"pricing\"><a href=\"#\" rel=\"PricingOptions\" id=\"Pricing\">" + SepFunctions.LangText("Pricing Options") + "</a></li>" + Environment.NewLine);
                    Response.Write("</ul>" + Environment.NewLine);
                    break;
            }
        }
    }
}