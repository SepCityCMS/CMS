// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="report_listing.aspx.cs" company="SepCity, Inc.">
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
    /// Class report_listing.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class report_listing : Page
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

            GlobalVars.ModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "report_listing.aspx?URL=" + SepCommon.SepCore.Request.Item("URL") + "&UniqueID=" + SepCommon.SepCore.Request.Item("UniqueID") + "&ModuleID=" + SepCommon.SepCore.Request.Item("ModuleID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID, "Report Listing"), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("URL")) && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UniqueID")) && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ModuleID")))
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ActivityID FROM Activities WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID AND IPAddress=@IPAddress AND ActType='REPORTLISTING'", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", SepCommon.SepCore.Request.Item("UniqueID"));
                        cmd.Parameters.AddWithValue("@ModuleID", SepCommon.SepCore.Request.Item("ModuleID"));
                        cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Sorry, you have already reported this listing.") + "</div>";
                            }
                            else
                            {
                                string sActDesc = SepFunctions.LangText("[[Username]] has reported a listing.") + Environment.NewLine;
                                sActDesc += SepFunctions.LangText("URL: ~~" + SepCommon.SepCore.Request.Item("URL") + "~~") + Environment.NewLine;
                                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name())) sActDesc += SepFunctions.LangText("Username: ~~" + SepFunctions.Session_User_Name() + "~~") + Environment.NewLine;
                                SepFunctions.Activity_Write("REPORTLISTING", sActDesc, SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")), SepCommon.SepCore.Request.Item("UniqueID"));
                                SepFunctions.Send_Email(SepFunctions.Setup(991, "AdminEmailAddress"), SepFunctions.Setup(991, "AdminEmailAddress"), SepFunctions.LangText("A user has reported a bad listing to you."), SepFunctions.LangText("The listing below has been reported from your website.") + "<br/><br/><a href=\"" + SepFunctions.GetMasterDomain(true) + SepCommon.SepCore.Request.Item("URL") + "\">" + SepFunctions.GetMasterDomain(true) + SepCommon.SepCore.Request.Item("URL") + "</a>", GlobalVars.ModuleID);
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully reported this listing to the administrator.") + "</div>";
                            }

                        }
                    }
                }
            else
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=400");
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