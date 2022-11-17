// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="viewpage.aspx.cs" company="SepCity, Inc.">
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
    /// Class viewpage.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class viewpage : Page
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

            GlobalVars.ModuleID = 999;
            var sAccessKeys = string.Empty;
            long isEnabled = 1;


            long PageId;
            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("UniqueID")) > 0)
                PageId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("UniqueID"));
            else
                PageId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ID"));

            if (SepFunctions.Get_Portal_ID() == 0)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT AccessKeys,Status,Visits FROM ModulesNPages WHERE UniqueID='" + PageId + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                using (var cmd2 = new SqlCommand("UPDATE ModulesNPages SET Visits='" + (SepFunctions.toLong(SepFunctions.openNull(RS["Visits"])) + 1) + "' WHERE UniqueID='" + PageId + "'", conn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }

                                sAccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                                isEnabled = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                            }
                        }
                    }
                }

                if (isEnabled < 1 && Response.IsClientConnected)
                    SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
                SepFunctions.RequireLogin(sAccessKeys);
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(PageId), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(PageId);

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
    }
}