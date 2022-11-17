// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="celebrities_schedule_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class conference_schedule_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class conference_schedule_view : Page
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    EventDateLabel.InnerText = SepFunctions.LangText("Event Date:");
                    BeginTimeLabel.InnerText = SepFunctions.LangText("Request Call Between Time:");
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
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("EventID")))
                    {
                        var jEvents = SepCommon.DAL.Events.Event_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("EventID")));

                        if (jEvents.EventID == 0)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Event~~ does not exist.") + "</div>";
                            DisplayContent.Visible = false;
                        }
                        else
                        {
                            Subject.InnerHtml = jEvents.Subject;
                            BeginTime.InnerHtml = jEvents.BegTime;
                            EndTime.InnerHtml = jEvents.EndTime;
                            EventDate.InnerHtml = Strings.ToString(jEvents.EventDate);
                            Viewed.InnerHtml = Strings.ToString(jEvents.Hits);
                            EventContent.InnerHtml = jEvents.EventContent;
                        }
                    }
                    else
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Item~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
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