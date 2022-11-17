using SepCommon;
using SepCommon.SepCore;
using System;
using System.Web.UI.HtmlControls;

namespace wwwroot
{
    public partial class conference_config : System.Web.UI.Page
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
            SepFunctions.RequireLogin(SepFunctions.Security("VideoConferenceAcceptKeys"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0] + " - " + SepFunctions.LangText("Configuration");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                var jConfig = SepCommon.DAL.VideoConference.VideoConfig_Get(SepFunctions.Session_User_ID());

                ContactOnline.Value = Strings.ToString(jConfig.ContactOnline);
                SundayAvailableStart.Value = jConfig.SundayAvailableStart;
                SundayAvailableEnd.Value = jConfig.SundayAvailableEnd;

                MondayAvailableStart.Value = jConfig.MondayAvailableStart;
                MondayAvailableEnd.Value = jConfig.MondayAvailableEnd;

                TuesdayAvailableStart.Value = jConfig.TuesdayAvailableStart;
                TuesdayAvailableEnd.Value = jConfig.TuesdayAvailableEnd;

                WednesdayAvailableStart.Value = jConfig.WednesdayAvailableStart;
                WednesdayAvailableEnd.Value = jConfig.WednesdayAvailableEnd;

                ThursdayAvailableStart.Value = jConfig.ThursdayAvailableStart;
                ThursdayAvailableEnd.Value = jConfig.ThursdayAvailableEnd;

                FridayAvailableStart.Value = jConfig.FridayAvailableStart;
                FridayAvailableEnd.Value = jConfig.FridayAvailableEnd;

                SaturdayAvailableStart.Value = jConfig.SaturdayAvailableStart;
                SaturdayAvailableEnd.Value = jConfig.SaturdayAvailableEnd;
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
            var intReturn = SepCommon.DAL.VideoConference.VideoConfig_Save(SepFunctions.Session_User_ID(), SepFunctions.toBoolean(ContactOnline.Value), SundayAvailableStart.Value, SundayAvailableEnd.Value,
                MondayAvailableStart.Value, MondayAvailableEnd.Value,
                TuesdayAvailableStart.Value, TuesdayAvailableEnd.Value,
                WednesdayAvailableStart.Value, WednesdayAvailableEnd.Value,
                ThursdayAvailableStart.Value, ThursdayAvailableEnd.Value,
                FridayAvailableStart.Value, FridayAvailableEnd.Value,
                SaturdayAvailableStart.Value, SaturdayAvailableEnd.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, SepFunctions.Session_User_ID());
        }
    }
}