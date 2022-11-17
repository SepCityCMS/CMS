// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_open_file.aspx.cs" company="SepCity, Inc.">
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
    /// Class site_template_open_file.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_open_file : Page
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("File Editor");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSiteLooks")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSiteLooks"), false) == false)
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

            try
            {
                var jScript = string.Empty;
                var editorSyntax = "basic";

                var sFileName = SepFunctions.GetDirValue("skins") + SepFunctions.CleanFileName(SepFunctions.UrlDecode(SepCommon.SepCore.Request.Item("File")));

                if (!Page.IsPostBack)
                {
                    FilePath.Value = sFileName;

                    using (var objReader = new StreamReader(sFileName))
                    {
                        EditBox.Value = objReader.ReadToEnd();
                    }
                }

                switch (Strings.LCase(sFileName.Substring(sFileName.IndexOf(".", StringComparison.OrdinalIgnoreCase) + 1)))
                {
                    case ".html":
                    case ".htm":
                    case ".master":
                        editorSyntax = "html";
                        break;

                    case ".js":
                        editorSyntax = "js";
                        break;

                    case ".css":
                        editorSyntax = "css";
                        break;

                    case ".sql":
                        editorSyntax = "sql";
                        break;

                    case ".php":
                        editorSyntax = "php";
                        break;

                    case ".xml":
                        editorSyntax = "xml";
                        break;

                    default:
                        editorSyntax = "basic";
                        break;
                }

                jScript += "<script type=\"text/javascript\">";
                jScript += "editAreaLoader.init({";
                jScript += "id: 'EditBox'";
                jScript += ", start_highlight: true";
                jScript += ", allow_resize: 'both'";
                jScript += ", allow_toggle: false";
                jScript += ", word_wrap: true";
                jScript += ", language: 'en'";
                jScript += ", toolbar: 'search, go_to_line, fullscreen, |, undo, redo'";
                jScript += ", syntax: '" + editorSyntax + "'";
                jScript += "});";
                jScript += "</script>";

                var cstype = GetType();

                Page.ClientScript.RegisterClientScriptBlock(cstype, "ButtonClickScript", jScript);
            }
            catch
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Error loading file.") + "</div>";
                EditBox.Visible = false;
                SaveButton.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sFileName = FilePath.Value;

            if (File.Exists(sFileName))
                try
                {
                    using (var objWriter = new StreamWriter(sFileName))
                    {
                        objWriter.Write(EditBox.Value);
                    }

                    ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("File has been successfully saved.") + "</div>";
                }
                catch
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error saving file.") + "</div>";
                }
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error saving file.") + "</div>";
        }
    }
}