// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="forgot_password.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class forgot_password.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class forgot_password : Page
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
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
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

            GlobalVars.ModuleID = 11;

            Page.Title = SepFunctions.LangText("Reset Password") + " : " + SepFunctions.Setup(992, "WebSiteName");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.ModuleActivated(68))
            {
                idLoginErrorMsg.InnerHtml = SepFunctions.SendGenericError(404);
                LoginDiv.Visible = false;
            }

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();
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
        /// Handles the Click event of the PasswordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PasswordButton_Click(object sender, EventArgs e)
        {
            var GetPasswordID = Strings.ToString(SepFunctions.GetIdentity());
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT Username,EmailAddress,Password,UserID,PasswordResetID FROM Members WHERE EmailAddress=@EmailAddress AND Status=1", conn))
                {
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            string sUserID = SepFunctions.openNull(RS["UserID"]);

                            using (var cmd2 = new SqlCommand("UPDATE Members SET PasswordResetID=@PasswordResetID, PasswordResetDate=@PasswordResetDate WHERE UserID=@UserID", conn))
                            {
                                cmd2.Parameters.AddWithValue("@PasswordResetID", GetPasswordID);
                                cmd2.Parameters.AddWithValue("@PasswordResetDate", DateTime.Now);
                                cmd2.Parameters.AddWithValue("@UserID", sUserID);
                                cmd2.ExecuteNonQuery();
                            }

                            string sUsername;
                            if (SepFunctions.Setup(997, "LoginEmail") == "Yes")
                                sUsername = SepFunctions.openNull(RS["EmailAddress"]);
                            else
                                sUsername = SepFunctions.openNull(RS["UserName"]);

                            string EmailSubject = SepFunctions.LangText("Password Reset Request at ~~" + sSiteName + "~~");
                            string EmailBody = SepFunctions.LangText("Dear ~~" + sUsername + "~~,") + "<br/><br/>";

                            EmailBody += SepFunctions.LangText("You have requested a password reset from ~~" + sSiteName + "~~, please click on the link below to complete the Password reset process.") + "<br/>";
                            EmailBody += SepFunctions.LangText("NOTE: The link below has a 24 hours expiration; so you must completed this process within 24 hours to successfully complete the password reset process.") + "<br/>";
                            EmailBody += SepFunctions.LangText("if you do not click the link below withing 24 hours time period, you will need to restart the password reset process.") + "<br/><br/>";

                            string sResetURL = SepFunctions.GetSiteDomain() + "reset_password.aspx?UserID=" + sUserID + "&ResetID=" + GetPasswordID;

                            EmailBody += sResetURL + "<br/><br/>";

                            EmailBody += SepFunctions.LangText("If you have not requested a password reset and you are reading this email, then please contact ~~" + sSiteName + "~~ support immediately") + "<br/>";

                            EmailBody += SepFunctions.LangText("Sincerely,") + "<br/>";
                            EmailBody += SepFunctions.LangText("~~" + sSiteName + "~~ Support");

                            SepFunctions.Send_Email(EmailAddress.Value, SepFunctions.Setup(991, "AdminEmailAddress"), EmailSubject, EmailBody, 0);

                            string sActDesc = SepFunctions.LangText("[[Username]] successfully request their password.") + Environment.NewLine;
                            sActDesc += SepFunctions.LangText("Password sent to: ~~" + EmailAddress.Value + "~~") + Environment.NewLine;
                            SepFunctions.Activity_Write("PASSWORD", sActDesc, GlobalVars.ModuleID, string.Empty, sUserID);

                            idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("We have sent you an email with a link to reset your password.") + "</div>";

                            LoginDiv.Visible = false;
                            BackLiteral.InnerHtml = "<p><a href=\"login.aspx\">" + SepFunctions.LangText("Back to the Login Form") + "</a></p>";
                        }
                        else
                        {
                            idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("No such user found in our database.");
                        }

                    }
                }
            }
        }
    }
}