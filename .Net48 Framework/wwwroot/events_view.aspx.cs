// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="events_view.aspx.cs" company="SepCity, Inc.">
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
    /// Class events_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class events_view : Page
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
                    EventTypeLabel.InnerText = SepFunctions.LangText("Event Type:");
                    BeginTimeLabel.InnerText = SepFunctions.LangText("Begin Time:");
                    EndTimeLabel.InnerText = SepFunctions.LangText("End Time:");
                    EventDateLabel.InnerText = SepFunctions.LangText("Event Date:");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BuyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BuyButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var jEvents = SepCommon.DAL.Events.Event_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("EventID")));

            if (jEvents.EventID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Event~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
            }
            else
            {
                SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, 46, string.Empty, "1", string.Empty, string.Empty, false, jEvents.Subject, Strings.ToString(jEvents.EventOnlinePrice), string.Empty, string.Empty, Quantity.Value, jEvents.EventID, 0, SepFunctions.Get_Portal_ID());
            }

            SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 46;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "EventsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("EventsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

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
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewEvent", "GetViewEvent", SepCommon.SepCore.Request.Item("EventID"), false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jEvents.Subject);
                    Subject.InnerHtml = jEvents.Subject;
                    EventType.InnerHtml = jEvents.EventType;
                    BeginTime.InnerHtml = jEvents.BegTime;
                    EndTime.InnerHtml = jEvents.EndTime;
                    EventDate.InnerHtml = jEvents.EventDate.ToShortDateString();
                    Viewed.InnerHtml = Strings.ToString(jEvents.Hits);
                    EventContent.InnerHtml = jEvents.EventContent;

                    // Buy Tickets
                    if (!Page.IsPostBack)
                    {
                        if (jEvents.EventOnlinePrice > 0)
                        {
                            BuyButton.Text = SepFunctions.LangText("Buy On-line for ~~" + SepFunctions.Format_Currency(jEvents.EventOnlinePrice) + " ~~");
                        }
                        else
                        {
                            BuyButton.Visible = false;
                            QuantitySpan.Visible = false;
                        }

                        if (jEvents.EventDoorPrice > 0) DoorPrice.InnerHtml = "<br/>" + SepFunctions.LangText("- Or Purchase at the door for ~~" + SepFunctions.Format_Currency(jEvents.EventDoorPrice) + " ~~");
                        else DoorPrice.Visible = false;
                    }

                    // Show Images
                    EventPictures.ContentUniqueID = SepCommon.SepCore.Request.Item("EventID");
                    EventPictures.ModuleID = GlobalVars.ModuleID;
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Event~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
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