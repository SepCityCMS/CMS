// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="menu_setup.aspx.cs" company="SepCity, Inc.">
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
    /// Class menu_setup.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class menu_setup : Page
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
            var additionalModules = string.Empty;

            if (SepFunctions.ModuleActivated(68) == false) additionalModules += ", 994, 997";

            switch (Context.Request.Form["folder"])
            {
                case "Modules":

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true))
                    {
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();

                            Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                            using (var cmd = new SqlCommand("SELECT ModuleID,LinkText,Status FROM ModulesNPages WHERE Activated='1' AND ModuleID NOT IN (0, 22, 29, 996, 34, 33, 991, 8, 979, 980, 981, 982, 983, 984, 993, 992, 26, 973, 974, 975, 976, 977, 985, 986, 987, 989, 990, 995, 998, 999" + additionalModules + ") ORDER BY LinkText", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                        while (RS.Read())
                                            if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) < 500)
                                            {
                                                if (SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])))) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a>" + SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 1 ? string.Empty : " - <b>" + SepFunctions.LangText("Disabled") + "</b>") + "</li>" + Environment.NewLine);
                                            }
                                            else
                                            {
                                                Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a></li>" + Environment.NewLine);
                                            }

                                }
                            }

                            Response.Write("</ul>" + Environment.NewLine);
                        }
                    }

                    break;

                case "Website":

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true))
                    {
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();

                            Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                            using (var cmd = new SqlCommand("SELECT ModuleID,LinkText,Status FROM ModulesNPages WHERE Activated='1' AND ModuleID IN (33, 991, 8, 993, 992, 26, 995, 989" + additionalModules + ") ORDER BY LinkText", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                        while (RS.Read())
                                            if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) < 500)
                                            {
                                                Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a>" + SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 1 ? string.Empty : " - <b>" + SepFunctions.LangText("Disabled") + "</b>") + "</li>" + Environment.NewLine);
                                            }
                                            else
                                            {
                                                if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) == 995) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a>" + SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 1 ? string.Empty : " - <b>" + SepFunctions.LangText("Disabled") + "</b>") + "</li>" + Environment.NewLine);
                                                else if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) == 989) Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Integrations\" id=\"Integrations\">" + SepFunctions.LangText("Integrations") + "</a></li>" + Environment.NewLine);
                                                else Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\" id=\"ModuleID" + SepFunctions.openNull(RS["ModuleID"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a></li>" + Environment.NewLine);
                                            }

                                }
                            }

                            Response.Write("</ul>" + Environment.NewLine);
                        }
                    }

                    break;

                case "Integrations":
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminModuleMan"), true))
                    {
                        Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989E\" id=\"ModuleID989E\">CloudFlare</a></li>" + Environment.NewLine);
                        if (SepFunctions.ModuleActivated(67)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID67\" id=\"ModuleID67\">CRM/Support</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989A\" id=\"ModuleID989A\">Facebook</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989G\" id=\"ModuleID989G\">FedEx Shipping</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989C\" id=\"ModuleID989C\">Google Analytics</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989B\" id=\"ModuleID989B\">Google reCAPTCHA</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989J\" id=\"ModuleID989J\">Google Maps</a></li>" + Environment.NewLine);
                        if (SepFunctions.ModuleActivated(68)) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID68\" id=\"ModuleID68\">LDAP</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989F\" id=\"ModuleID989F\">LinkedIn</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989L\" id=\"ModuleID989L\">PayPal Business</a></li>" + Environment.NewLine);
                        if (SepFunctions.isHosted() == false) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID70\" id=\"ModuleID70\">Radius/IP2Location</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989D\" id=\"ModuleID989D\">Twilio</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989H\" id=\"ModuleID989H\">UPS Shipping</a></li>" + Environment.NewLine);
                        Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989I\" id=\"ModuleID989I\">USPS Shipping</a></li>" + Environment.NewLine);
                        if (SepFunctions.isProfessionalEdition()) Response.Write("<li class=\"setupmodule\"><a href=\"#\" rel=\"ModuleID989K\" id=\"ModuleID989K\">Zoom</a></li>" + Environment.NewLine);
                        Response.Write("</ul>" + Environment.NewLine);
                    }

                    break;

                default:
                    Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">" + Environment.NewLine);
                    Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Modules\" id=\"Modules\">" + SepFunctions.LangText("Modules") + "</a></li>" + Environment.NewLine);
                    Response.Write("<li class=\"directoryplus collapsed\"><a href=\"#\" rel=\"Website\" id=\"Website\">" + SepFunctions.LangText("Website") + "</a></li>" + Environment.NewLine);
                    Response.Write("</ul>" + Environment.NewLine);
                    break;
            }
        }
    }
}