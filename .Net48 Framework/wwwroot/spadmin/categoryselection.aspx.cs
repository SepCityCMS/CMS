// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="categoryselection.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;

    /// <summary>
    /// Class categoryselection.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class categoryselection : Page
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
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), true) == false)
            {
                Response.Write("<div align=\"center\" style=\"margin-top:50px\">");
                Response.Write("<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>");
                Response.Write(SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>");
                Response.Write("</div>");
                return;
            }

            long aa = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT CatID,CategoryName,(SELECT CategoryName FROM Categories WHERE CatID=Categories.ListUnder) AS SubCategory FROM Categories WHERE CategoryName LIKE '" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("Keywords")) + "%'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            Response.Write("<table width=\"100%\" class=\"Table\" id=\"availableItems\">");
                            while (RS.Read())
                            {
                                if (aa % 2 == 0) Response.Write("<tr class=\"TableBody1\" id=\"avail" + SepFunctions.openNull(RS["CatID"]) + "\">");
                                else Response.Write("<tr class=\"TableBody2\" id=\"avail" + SepFunctions.openNull(RS["CatID"]) + "\">");
                                Response.Write("<td style=\"cursor:pointer;\" onclick=\"addCategory('" + SepFunctions.openNull(RS["CatID"]) + "', '" + SepCommon.SepCore.Request.Item("FieldID") + "');\" id=\"availtd" + SepFunctions.openNull(RS["CatID"]) + "\"><b>");
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["SubCategory"]))) Response.Write(SepFunctions.openNull(RS["SubCategory"]) + " / ");
                                Response.Write(SepFunctions.openNull(RS["CategoryName"]));
                                Response.Write("</b></td>");
                                Response.Write("</tr>");
                                aa += 1;
                            }

                            Response.Write("</table>");
                        }

                    }
                }
            }
        }
    }
}