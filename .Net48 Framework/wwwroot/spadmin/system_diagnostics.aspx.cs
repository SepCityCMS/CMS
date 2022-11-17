// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="system_diagnostics.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class system_diagnostics.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class system_diagnostics : Page
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
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            installErrors.InnerHtml = string.Empty;

            if (Strings.Left(Strings.ToString(Environment.Version), 3) == "4.0") installErrors.InnerHtml += "<div class=\"requirementCheck\">1). Checking if the .NET Framework Version 4.0 is installed ... <span style=\"color: #006400;\">Success</span></div>";
            else installErrors.InnerHtml += "<div class=\"requirementCheck\">1). Checking if the .NET Framework Version 4.0 is installed ... <span style=\"color: #ff0000;\">Failed</span></div>";

            if (!Directory.Exists(HostingEnvironment.MapPath("~/install/"))) installErrors.InnerHtml += "<div class=\"requirementCheck\">2). Checking if the install folder is removed ... <span style=\"color: #006400;\">Success</span></div>";
            else installErrors.InnerHtml += "<div class=\"requirementCheck\">2). Checking if the install folder is removed ... <span style=\"color: #ff0000;\">Failed</span></div>";

            try
            {
                using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>0</percentage>" + Environment.NewLine + "</root>");
                }

                File.Delete(SepFunctions.GetDirValue("app_data") + "install.xml");
                installErrors.InnerHtml += "<div class=\"requirementCheck\">3). Checking the \"app_data\" directory permissions ... <span style=\"color: #006400;\">Success</span></div>";
            }
            catch
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">3). Checking the \"app_data\" directory permissions ... <span style=\"color: #ff0000;\">Failed</span></div>";
            }

            try
            {
                using (var outfile = new StreamWriter(HostingEnvironment.MapPath("~/downloads/") + "\\install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>0</percentage>" + Environment.NewLine + "</root>");
                }

                File.Delete(HostingEnvironment.MapPath("~/downloads/") + "\\install.xml");
                installErrors.InnerHtml += "<div class=\"requirementCheck\">4). Checking the \"downloads\" directory permissions ... <span style=\"color: #006400;\">Success</span></div>";
            }
            catch
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">4). Checking the \"downloads\" directory permissions ... <span style=\"color: #ff0000;\">Failed</span></div>";
            }

            try
            {
                using (var outfile = new StreamWriter(HostingEnvironment.MapPath("~/skins/") + "\\install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>0</percentage>" + Environment.NewLine + "</root>");
                }

                File.Delete(HostingEnvironment.MapPath("~/skins/") + "\\install.xml");
                installErrors.InnerHtml += "<div class=\"requirementCheck\">5). Checking the \"skins\" directory permissions ... <span style=\"color: #006400;\">Success</span></div>";
            }
            catch
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">5). Checking the \"skins\" directory permissions ... <span style=\"color: #ff0000;\">Failed</span></div>";
            }
        }
    }
}