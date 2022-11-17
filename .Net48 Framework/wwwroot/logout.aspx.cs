// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="logout.aspx.cs" company="SepCity, Inc.">
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
    using System.Web;
    using System.Web.UI;

    /// <summary>
    /// Class logout.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class logout : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            GlobalVars.ModuleID = 21;

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Expires = 0;
            SepCommon.SepCore.Response.AddHeader("pragma", "no-cache");
            SepCommon.SepCore.Response.AddHeader("cache-control", "private");
            HttpContext.Current.Response.CacheControl = "no-cache";

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM OnlineUsers WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.ExecuteNonQuery();
                    }
                }

            SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", string.Empty);
            SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "UserID", string.Empty);
            SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Password", string.Empty);
            SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "AccessKeys", string.Empty);

            SepCommon.SepCore.Session.removeCookie("returnUrl");

            Response.Cookies["UserInfo"].Value = string.Empty;

            SepFunctions.Redirect(sInstallFolder + "default.aspx");
        }
    }
}