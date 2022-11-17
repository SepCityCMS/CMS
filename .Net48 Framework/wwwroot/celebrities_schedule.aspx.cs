// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="celebrities_schedule.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class conference_schedule.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class conference_schedule : Page
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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="sUserID">The s user identifier.</param>
        /// <returns>System.String.</returns>
        public string GetFullName(string sUserID)
        {
            return SepFunctions.GetUserInformation("FirstName", sUserID) + " " + SepFunctions.GetUserInformation("LastName", sUserID);
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ListContent.Columns[0].HeaderText = SepFunctions.LangText("Full Name");
                    ListContent.Columns[1].HeaderText = SepFunctions.LangText("Subject");
                    ListContent.Columns[2].HeaderText = SepFunctions.LangText("Date/Time");
                    ListContent.Columns[3].HeaderText = SepFunctions.LangText("Call Now");
                }
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
            TranslatePage();

            GlobalVars.ModuleID = 64;

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                SepFunctions.RequireLogin("|2|");

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                if (SepFunctions.GetUserInformation("AccessClass", SepFunctions.Session_User_ID()) == SepFunctions.Setup(64, "ModeratorClass"))
                {
                    var cEvents = new List<Events>();
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 5, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 4, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 3, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 2, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 1, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateTime.Now), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -2, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -3, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));
                    cEvents.AddRange(SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -4, DateTime.Now)), EventTypeID: SepFunctions.userProfileID(SepFunctions.Session_User_ID())));

                    ListContent.DataSource = cEvents.Take(SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage")));
                    ListContent.DataBind();

                    if (ListContent.Rows.Count == 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("There are currently no scheduled calls on your schedule.") + "</div>";
                }
                else
                {
                    PageContent.InnerHtml = "<h1>" + SepFunctions.LangText("Access Denied") + "</h1>";
                }
            }
            else
            {
                PageContent.InnerHtml = "<h1>" + SepFunctions.LangText("Access Denied") + "</h1>";
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }
    }
}