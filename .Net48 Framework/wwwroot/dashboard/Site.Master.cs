// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="Site.Master.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class Site1.
    /// Implements the <see cref="System.Web.UI.MasterPage" />
    /// </summary>
    /// <seealso cref="System.Web.UI.MasterPage" />
    public partial class Site1 : MasterPage
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
        /// Handles the User event of the Login control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void Login_User(object sender, EventArgs e)
        {
            var sResponse = SepCommon.DAL.Members.Login(UserName.Text, Password.Text, string.Empty, string.Empty, string.Empty, SepFunctions.Get_Portal_ID(), false, string.Empty);
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            if (Strings.Left(sResponse, 7) == "USERID:")
            {
                sResponse = Strings.Replace(sResponse, "USERID:", string.Empty);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "UserID", Strings.Split(sResponse, "||")[0]);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", Strings.Split(sResponse, "||")[1]);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Password", Strings.Split(sResponse, "||")[2]);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "AccessKeys", Strings.Split(sResponse, "||")[3]);

                if (RememberMe.Checked)
                {
                    Response.Cookies["UserInfo"].Value = Strings.Split(sResponse, "||")[0] + "||" + Strings.Split(sResponse, "||")[1] + "||" + Strings.Split(sResponse, "||")[2] + "||" + Strings.Split(sResponse, "||")[3];
                    Response.Cookies["UserInfo"].Expires = DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, DateTime.Today);
                }

                idLoginErrorMsg.Text = SepFunctions.LangText("You have successfully logged in.");
                SepFunctions.Redirect(Request.RawUrl);
            }
            else
            {
                idLoginErrorMsg.Text = "<div class=\"alert alert-danger\" role=\"alert\">" + sResponse + "</div>";
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
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
            if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang))) UserNameRequired.ErrorMessage = SepFunctions.LangText("~~User Name~~ is required.");

            if (SepFunctions.Setup(997, "LoginEmail") == "Yes")
            {
                UserNameLabel.Text = SepFunctions.LangText("Email Address:");
                UserNameRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
            }

            if (Strings.InStr(Context.Request.RawUrl, ".aspx") == 0) aspnetForm.Action = "/dashboard/default.aspx";
            else aspnetForm.Action = Context.Request.RawUrl;
        }
    }
}