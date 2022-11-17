// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="runinstall.aspx.cs" company="SepCity, Inc.">
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
    /// Class runinstall.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class runinstall : Page
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
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml") && !File.Exists(SepFunctions.GetDirValue("app_data") + "install_temp.xml")) SepFunctions.Redirect("installed.aspx");
            Label menuLabel = (Label)Master.FindControl("InstallSpan");
            if (menuLabel != null)
                menuLabel.Font.Bold = true;

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "install_temp.xml")) hideNotes.Visible = false;

            if (Session["DBAddress"] == null || Session["DBName"] == null || Session["DBUser"] == null || Session["DBPass"] == null) contentinstall.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing Database Information, please run the install again.</div>";

            if (SepCommon.SepCore.Request.Item("DoAction") != "Upgrade")
                if (Session["PUserName"] == null || Session["PPassword"] == null || Session["PSecretQuestion"] == null || Session["PSecretAnswer"] == null || Session["PEmailAddress"] == null || Session["PFirstName"] == null || Session["PLastName"] == null || Session["PCountry"] == null || Session["PStreetAddress"] == null || Session["PCity"] == null || Session["PPostalCode"] == null || Session["PGender"] == null || Session["PBirthDate"] == null || Session["PPhoneNumber"] == null)
                    contentinstall.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing personal Information, please run the install again.</div>";

            if (Session["SMTPServer"] == null) contentinstall.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing SMTP Information, please run the install again.</div>";

            if (Session["LicenseUser"] == null || Session["LicensePass"] == null || Session["LicenseKey"] == null) contentinstall.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing license information, please run the install again.</div>";

            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            SepCommon.SepCore.Session.setSession(SepCommon.SepCore.Strings.Left(SepCommon.SepCore.Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", string.Empty);
            SepCommon.SepCore.Session.setSession(SepCommon.SepCore.Strings.Left(SepCommon.SepCore.Strings.Replace(sSiteName, " ", string.Empty), 5) + "UserID", string.Empty);
            SepCommon.SepCore.Session.setSession(SepCommon.SepCore.Strings.Left(SepCommon.SepCore.Strings.Replace(sSiteName, " ", string.Empty), 5) + "Password", string.Empty);
            SepCommon.SepCore.Session.setSession(SepCommon.SepCore.Strings.Left(SepCommon.SepCore.Strings.Replace(sSiteName, " ", string.Empty), 5) + "AccessKeys", string.Empty);

            Response.Cookies["UserInfo"].Value = string.Empty;
        }
    }
}