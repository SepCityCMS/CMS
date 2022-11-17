// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="requirements.aspx.cs" company="SepCity, Inc.">
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
    /// Class requirements.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class requirements : Page
    {
        /// <summary>
        /// Checks the requirements.
        /// </summary>
        public void Check_Requirements()
        {
            var disableContinue = false;

            installErrors.InnerHtml = string.Empty;

            if (!File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml"))
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">1). Checking if SepCity is not already installed ... <span style=\"color: #006400;\">Success</span></div>";
            }
            else
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">1). Checking if SepCity is not already installed ... <span style=\"color: #ff0000;\">Failed</span></div>";
                disableContinue = true;
            }

            if (Strings.Left(Strings.ToString(Environment.Version), 3) == "4.0")
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">2). Checking if the .NET Framework Version 4.0 is installed ... <span style=\"color: #006400;\">Success</span></div>";
            }
            else
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">2). Checking if the .NET Framework Version 4.0 is installed ... <span style=\"color: #ff0000;\">Failed</span></div>";
                disableContinue = true;
            }

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
                disableContinue = true;
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
                disableContinue = true;
            }

            try
            {
                using (var outfile = new StreamWriter(HostingEnvironment.MapPath("~/images/") + "\\install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>0</percentage>" + Environment.NewLine + "</root>");
                }

                File.Delete(HostingEnvironment.MapPath("~/images/") + "\\install.xml");
                installErrors.InnerHtml += "<div class=\"requirementCheck\">5). Checking the \"images\" directory permissions ... <span style=\"color: #006400;\">Success</span></div>";
            }
            catch
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">5). Checking the \"images\" directory permissions ... <span style=\"color: #ff0000;\">Failed</span></div>";
                disableContinue = true;
            }

            try
            {
                using (var outfile = new StreamWriter(HostingEnvironment.MapPath("~/skins/") + "\\install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>0</percentage>" + Environment.NewLine + "</root>");
                }

                File.Delete(HostingEnvironment.MapPath("~/skins/") + "\\install.xml");
                installErrors.InnerHtml += "<div class=\"requirementCheck\">6). Checking the \"skins\" directory permissions ... <span style=\"color: #006400;\">Success</span></div>";
            }
            catch
            {
                installErrors.InnerHtml += "<div class=\"requirementCheck\">6). Checking the \"skins\" directory permissions ... <span style=\"color: #ff0000;\">Failed</span></div>";
                disableContinue = true;
            }

            if (disableContinue) ContinueButton.Enabled = false;
            else ContinueButton.Enabled = true;
        }

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
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect("dbinfo.aspx");
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
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml")) SepFunctions.Redirect("installed.aspx");
            Label menuLabel = (Label)Master.FindControl("RequirementsSpan");
            if (menuLabel != null)
                menuLabel.Font.Bold = true;

            Check_Requirements();
        }

        /// <summary>
        /// Handles the Click event of the RescanButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RescanButton_Click(object sender, EventArgs e)
        {
            Check_Requirements();
        }
    }
}