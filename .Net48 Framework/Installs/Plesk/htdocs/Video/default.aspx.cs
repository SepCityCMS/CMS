using SepCommon;
using SepCommon.SepCore;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using Twilio.Jwt.AccessToken;
using System.Data.SqlClient;

namespace wwwroot.Video
{
    /// <summary>
    /// 
    /// </summary>
    public partial class _default : System.Web.UI.Page
    {

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 69;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "video/default.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepCommon.SepCore.Request.Item("DoAction") == "MakeCall" && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
            {
                SepCommon.DAL.Messenger.Message_Send(SepFunctions.Session_User_ID(), SepFunctions.GetUserInformation("UserName", SepCommon.SepCore.Request.Item("UserID")), SepFunctions.Session_User_Name() + " is requesting a video chat.", "<a href=\"" + sInstallFolder + "video/default.aspx?UserID=" + SepCommon.SepCore.Request.Item("UserID") + "\">Click here</a> to accept the video chat.", false);
            }

            // Substitute your Twilio AccountSid and ApiKey details
            var AccountSid = SepFunctions.Setup(989, "TwilioAccountSID");
            var ApiKeySid = SepFunctions.Setup(989, "TwilioVideoSID");
            var ApiKeySecret = SepFunctions.Setup(989, "TwilioVideoSecret");

            var identity = SepFunctions.Session_User_Name();

            // Create a video grant for the token
            var grant = new VideoGrant();

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("MeetingID")))
            {
                grant.Room = SepCommon.DAL.VideoConference.VideoSchedule_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("MeetingID"))).ToUserName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
                {
                    grant.Room = SepFunctions.GetUserInformation("UserName", SepCommon.SepCore.Request.Item("UserID"));
                }
                else
                {
                    grant.Room = SepFunctions.Session_User_Name();
                }
            }

            var grants = new HashSet<IGrant> { grant };

            // Create an Access Token generator
            var token = new Token(AccountSid, ApiKeySid, ApiKeySecret, identity: identity, grants: grants);

            if(SepFunctions.Setup(64, "ConferenceEnable") == "Enable" && SepFunctions.toLong(SepFunctions.Setup(64, "PercentCelebrities")) > 0)
            {
                var jCustom = SepCommon.DAL.CustomFields.Answer_Get(847562837400918, grant.Room);

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT UnitPrice FROM ShopProducts WHERE ProductID=@ProductID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", jCustom.FieldValue);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                var donatePrice = (SepFunctions.toDecimal(SepFunctions.Setup(64, "PercentCelebrities")) / SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"]))) * 100;
                                PayPal.CreateOrder(grant.Room, SepFunctions.Generate_GUID(), Strings.ToString(donatePrice), "Video Chat");
                            }
                        }
                    }
                }
            }

            // Serialize the token as a JWT
            VideoID.Value = token.ToJwt();
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