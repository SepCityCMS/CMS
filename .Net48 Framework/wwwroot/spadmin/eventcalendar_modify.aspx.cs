// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="eventcalendar_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class eventcalendar_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class eventcalendar_modify : Page
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
                    RecurringCycle.Items[0].Text = SepFunctions.LangText("Days");
                    RecurringCycle.Items[1].Text = SepFunctions.LangText("Weeks");
                    RecurringCycle.Items[2].Text = SepFunctions.LangText("Months");
                    RecurringCycle.Items[3].Text = SepFunctions.LangText("Years");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Event");
                    EventTypeLabel.InnerText = SepFunctions.LangText("Event Type:");
                    EventDateLabel.InnerText = SepFunctions.LangText("Event Date:");
                    StartTimeLabel.InnerText = SepFunctions.LangText("Start Time:");
                    EndTimeLabel.InnerText = SepFunctions.LangText("End Time:");
                    RecurringLabel.InnerText = SepFunctions.LangText("Recurring Options:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Select Pictures:");
                    SubjectLabel.InnerText = SepFunctions.LangText("Subject:");
                    EventTypeRequired.ErrorMessage = SepFunctions.LangText("~~Event Type~~ is required.");
                    SubjectRequired.ErrorMessage = SepFunctions.LangText("~~Subject~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
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

            GlobalVars.ModuleID = 46;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("EventsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("EventsAdmin"), true) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            if (!Page.IsPostBack)
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT TypeID,TypeName FROM EventTypes WHERE Status <> -1 AND PortalID=@PortalID ORDER BY TypeName", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                                while (RS.Read())
                                    EventType.Items.Add(new ListItem(SepFunctions.openNull(RS["TypeName"]), SepFunctions.openNull(RS["TypeID"])));
                        }
                    }
                }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("EventID")))
            {
                var jEvents = SepCommon.DAL.Events.Event_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("EventID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jEvents.EventID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Event~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Event");
                    EventID.Value = SepCommon.SepCore.Request.Item("EventID");
                    EventType.Value = Strings.ToString(jEvents.TypeID);
                    EventDate.Value = Strings.FormatDateTime(jEvents.EventDate, Strings.DateNamedFormat.ShortDate);
                    BegTime.Value = Strings.FormatDateTime(SepFunctions.toDate(jEvents.BegTime), Strings.DateNamedFormat.ShortTime);
                    EndTime.Value = Strings.FormatDateTime(SepFunctions.toDate(jEvents.EndTime), Strings.DateNamedFormat.ShortTime);
                    Subject.Value = jEvents.Subject;
                    EventContent.Text = jEvents.EventContent;
                    ShareEvent.Checked = jEvents.ShareEvent;
                    Pictures.ContentID = Strings.ToString(jEvents.EventID);
                    Pictures.UserID = jEvents.UserID;
                    Recurring.Value = Strings.ToString(jEvents.Recurring);
                    RecurringCycle.Value = jEvents.RecurringCycle;
                    Duration.Value = Strings.ToString(jEvents.Duration);
                    if (jEvents.EventOnlinePrice > 0 || jEvents.EventDoorPrice > 0)
                    {
                        EnablePricing.Checked = true;
                        OnlinePrice.Value = SepFunctions.Format_Currency(jEvents.EventOnlinePrice);
                        DoorPrice.Value = SepFunctions.Format_Currency(jEvents.EventDoorPrice);
                    }

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("EventID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(EventID.Value)) EventID.Value = Strings.ToString(SepFunctions.GetIdentity());
                Pictures.ContentID = EventID.Value;
                if (!Page.IsPostBack)
                {
                    EventDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    if (SepFunctions.CompareKeys(SepFunctions.Security("EventsShared"), true)) ShareEvent.Checked = true;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.Events.Event_Save(SepFunctions.toLong(EventID.Value), EventType.Value, Subject.Value, SepFunctions.Session_User_ID(), SepFunctions.toDate(EventDate.Value), BegTime.Value, EndTime.Value, SepFunctions.toInt(Recurring.Value), SepFunctions.toLong(Duration.Value), RecurringCycle.Value, EventContent.Text, ShareEvent.Checked, Convert.ToDecimal(EnablePricing.Checked ? SepFunctions.toDecimal(OnlinePrice.Value) : 0), Convert.ToDecimal(EnablePricing.Checked ? SepFunctions.toDecimal(DoorPrice.Value) : 0), SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}