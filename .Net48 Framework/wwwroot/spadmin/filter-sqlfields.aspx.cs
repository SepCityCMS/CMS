// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="filter-sqlfields.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;

    /// <summary>
    /// Class filter_sqlfields.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class filter_sqlfields : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), true) == false)
            {
                Response.Write("<div align=\"center\" style=\"margin-top:50px\">");
                Response.Write("<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>");
                Response.Write(SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>");
                Response.Write("</div>");
                return;
            }

            string sSQLStatement;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SQLStatement")) && Strings.UCase(Strings.Left(SepCommon.SepCore.Request.Item("SQLStatement"), 6)) == "SELECT")
                sSQLStatement = SepFunctions.UrlDecode(SepCommon.SepCore.Request.Item("SQLStatement"));
            else
                sSQLStatement = "SELECT * FROM Members";

            using (var tbl = new DataTable())
            {
                var col = new DataColumn("FieldName", typeof(string));
                tbl.Columns.Add(col);
                var row = tbl.NewRow();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sSQLStatement, conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            for (var i = 0; i <= RS.FieldCount - 1; i++)
                            {
                                row["FieldName"] = SepFunctions.openNull(RS.GetName(i));
                                tbl.Rows.Add(row);
                                row = tbl.NewRow();
                            }

                        }
                    }
                }

                ManageGridView.DataSource = tbl;
                ManageGridView.DataBind();
            }
        }
    }
}