// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="menu_security.aspx.cs" company="SepCity, Inc.">
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
    /// Class menu_security.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class menu_security : Page
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
            switch (Context.Request.Form["folder"])
            {
                case "Modules":

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true))
                    {
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();

                            Context.Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                            using (var cmd = new SqlCommand("SELECT ModuleID,LinkText,Status FROM ModulesNPages WHERE Activated='1' AND ModuleID NOT IN ('0','26','29','22','992','993','55','15','991','8','4','56','57','1','997','3','33','973','974','975','976','977','979','983','984','985','986','987','989','990','994','996','995','998','999') ORDER BY LinkText", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                        while (RS.Read())
                                            if (SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"]))))
                                                Context.Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a>" + SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 1 ? string.Empty : " - <b>" + SepFunctions.LangText("Disabled") + "</b>") + "</li>" + Environment.NewLine);
                                }
                            }

                            Context.Response.Write("</ul>" + Environment.NewLine);
                        }
                    }

                    break;

                case "Website":

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true))
                    {
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();

                            Context.Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                            using (var cmd = new SqlCommand("SELECT ModuleID,LinkText,Status FROM ModulesNPages WHERE Activated='1' AND ModuleID IN ('994','996','995') ORDER BY LinkText", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                        while (RS.Read())
                                            Context.Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a>" + SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 1 ? string.Empty : " - <b>" + SepFunctions.LangText("Disabled") + "</b>") + "</li>" + Environment.NewLine);
                                }
                            }

                            Context.Response.Write("</ul>" + Environment.NewLine);
                        }
                    }

                    break;

                default:
                    Context.Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    Context.Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Modules\" id=\"Modules\">" + SepFunctions.LangText("Modules") + "</a></li>" + Environment.NewLine);
                    Context.Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Website\" id=\"Website\">" + SepFunctions.LangText("Website") + "</a></li>" + Environment.NewLine);
                    Context.Response.Write("</ul>" + Environment.NewLine);
                    break;
            }
        }
    }
}