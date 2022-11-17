using SepCommon;
using SepCommon.SepCore;
using System;
using System.Web.UI.HtmlControls;

namespace wwwroot
{
    public partial class conference_meeting : System.Web.UI.Page
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
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder(true);
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

            GlobalVars.ModuleID = 69;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "VideoConferenceEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("VideoConferenceCreateKeys"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0] + " - " + SepFunctions.LangText("Schedule Meeting");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("MeetingID")))
            {
                var jSchedule = SepCommon.DAL.VideoConference.VideoSchedule_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("MeetingID")));

                if (jSchedule.MeetingID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Scheduled Item~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Meeting");
                    MeetingID.Value = SepCommon.SepCore.Request.Item("MeetingID");
                    UserName.Value = SepFunctions.GetUserInformation(jSchedule.UserID);
                    MeetingDate.Value = Strings.FormatDateTime(jSchedule.StartDate, Strings.DateNamedFormat.ShortDate);
                    MeetingTime.Value = Strings.FormatDateTime(jSchedule.StartDate, Strings.DateNamedFormat.ShortTime);
                    Subject.Value = jSchedule.Subject;
                    Message.Text = jSchedule.Message;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(MeetingID.Value)) MeetingID.Value = Strings.ToString(SepFunctions.GetIdentity());
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

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.VideoConference.VideoSchedule_Save(SepFunctions.toLong(MeetingID.Value), SepFunctions.toDate(MeetingDate.Value + " " + MeetingTime.Value), Subject.Value, Message.Text, false, DateTime.Now, "", SepFunctions.Session_User_ID(), SepFunctions.GetUserID(UserName.Value));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, MeetingID.Value);

            ModFormDiv.Visible = false;
        }
    }
}