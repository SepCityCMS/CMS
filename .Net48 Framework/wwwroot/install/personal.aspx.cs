// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="personal.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class personal.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class personal : Page
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
            SepFunctions.Redirect("dbinfo.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            if (Password.Value != RePassword.Value)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Passwords do not match.</div>";
                return;
            }

            if (Regex.IsMatch(Password.Value, ".*[@#$%^&*/!].*") == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Password must contain one of @#$%^&*/!.</div>";
                return;
            }

            if (Regex.IsMatch(Password.Value, "[^\\s]{4,20}") == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Password must be between 4-20 characters.</div>";
                return;
            }

            if (SepFunctions.blockedUser(UserName.Value))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Invalid user name</div>";
                return;
            }

            if (UserName.Value == "admin")
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">You cannot use \"admin\" as the user name</div>";
                return;
            }

            Session["PUserName"] = UserName.Value;
            Session["PPassword"] = Password.Value;
            Session["PSecretQuestion"] = SecretQuestion.Value;
            Session["PSecretAnswer"] = SecretAnswer.Value;
            Session["PEmailAddress"] = EmailAddress.Value;
            Session["PFirstName"] = FirstName.Value;
            Session["PLastName"] = LastName.Value;
            Session["PCountry"] = Request.Form["Country"];
            Session["PStreetAddress"] = StreetAddress.Value;
            Session["PCity"] = City.Value;
            Session["PState"] = Request.Form["State"];
            Session["PPostalCode"] = PostalCode.Value;
            Session["PGender"] = Gender.Value;
            Session["PBirthDate"] = BirthDate.Value;
            Session["PPhoneNumber"] = PhoneNumber.Value;

            SepFunctions.Redirect("mailserver.aspx");
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
            Label menuLabel = (Label)Master.FindControl("PersonalSpan");
            if (menuLabel != null)
                menuLabel.Font.Bold = true;

            if (Session["DBAddress"] == null || Session["DBName"] == null || Session["DBUser"] == null || Session["DBPass"] == null) contentpersonal.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing Database Information, please run the install again.</div>";
        }
    }
}