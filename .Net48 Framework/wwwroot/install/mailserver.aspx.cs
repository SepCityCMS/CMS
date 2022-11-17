// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="mailserver.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class mailserver.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class mailserver : Page
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
        /// Handles the Click event of the BackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BackButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect("personal.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            Session["SMTPServer"] = SMTPServer.Value;
            Session["SMTPUser"] = SMTPUser.Value;
            Session["SMTPPass"] = SMTPPass.Value;

            SepFunctions.Redirect("activation.aspx");
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
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml")) SepFunctions.Redirect("installed.aspx");
            Label menuLabel = (Label)Master.FindControl("SMTPSpan");
            if (menuLabel != null)
                menuLabel.Font.Bold = true;

            if (Session["DBAddress"] == null || Session["DBName"] == null || Session["DBUser"] == null || Session["DBPass"] == null) contentsmtp.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing Database Information, please run the install again.</div>";

            if (Session["PUserName"] == null || Session["PPassword"] == null || Session["PSecretQuestion"] == null || Session["PSecretAnswer"] == null || Session["PEmailAddress"] == null || Session["PFirstName"] == null || Session["PLastName"] == null || Session["PCountry"] == null || Session["PStreetAddress"] == null || Session["PCity"] == null || Session["PPostalCode"] == null || Session["PGender"] == null || Session["PBirthDate"] == null || Session["PPhoneNumber"] == null) contentsmtp.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing personal Information, please run the install again.</div>";
        }
    }
}