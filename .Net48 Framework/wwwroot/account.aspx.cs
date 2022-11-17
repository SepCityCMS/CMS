// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="account.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class account.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class account : Page
    {
        /// <summary>
        /// The i portal identifier
        /// </summary>
        public static long iPortalID = 0;

        /// <summary>
        /// The s install folder
        /// </summary>
        public static string sInstallFolder = string.Empty;

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
            sInstallFolder = SepFunctions.GetInstallFolder();

            var sRedirect = string.Empty;

            GlobalVars.ModuleID = 33;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "account.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
            {
                switch (SepFunctions.Setup(1, "PPDefaultPage"))
                {
                    case "Memberships":
                        sRedirect = "memberships.aspx";
                        break;

                    case "Orders":
                        sRedirect = "order_stats.aspx";
                        break;

                    case "AdStats":
                        sRedirect = "advertising_stats.aspx";
                        break;

                    case "Friends":
                        sRedirect = "friends.aspx";
                        break;

                    case "Favorites":
                        sRedirect = "favorites.aspx";
                        break;

                    case "Affiliate":
                        sRedirect = "affiliate_tree.aspx";
                        break;

                    case "Messenger":
                        sRedirect = "memberships.aspx";
                        break;

                    case "Profile":
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT UserID FROM Profiles WHERE UserID='" + SepFunctions.Session_User_ID() + "'" + Strings.ToString(SepFunctions.Setup(60, "PortalProfiles") == "Yes" ? string.Empty : " AND PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty), conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                        sRedirect = "profile/" + SepFunctions.userProfileID(SepFunctions.Session_User_ID()) + "/" + SepFunctions.Session_User_Name() + "/";
                                    else
                                        sRedirect = "profiles_modify.aspx";
                                }
                            }
                        }

                        break;

                    case "Blogs":
                        sRedirect = "blogs.aspx";
                        break;

                    case "Forums":
                        sRedirect = "forums.aspx";
                        break;
                }

                if (!string.IsNullOrWhiteSpace(sRedirect)) SepFunctions.Redirect(sInstallFolder + sRedirect);
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
    }
}