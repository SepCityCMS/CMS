// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_upload.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Ionic.Zip;
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class site_template_upload.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_upload : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Upload Template");
                    zipFileNameLabel.InnerText = SepFunctions.LangText("Select a File:");
                    zipFileNameRequired.ErrorMessage = SepFunctions.LangText("~~Select a file~~ is required.");
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
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(SepFunctions.GetDirValue("app_data") + "temp\\")) Directory.CreateDirectory(SepFunctions.GetDirValue("app_data") + "temp\\");

                var FileID = Strings.ToString(SepFunctions.GetIdentity());
                var FileInfo = zipFileName;
                var FileSize = Convert.ToInt16(FileInfo.PostedFile.InputStream.Length / 1024);

                byte[] Content = null;
                Content = new byte[FileSize + 1];

                FileInfo.PostedFile.InputStream.Read(Content, 0, FileSize);

                var fileName = FileID + Path.GetExtension(FileInfo.FileName);
                var fileExt = Path.GetExtension(FileInfo.FileName);
                if (string.IsNullOrWhiteSpace(fileExt)) fileExt = "." + Strings.Split(fileName, ".")[1];
                if (Strings.LCase(fileExt) == ".zip")
                {
                    // Save file to temp folder
                    FileInfo.SaveAs(SepFunctions.GetDirValue("app_data") + "temp\\" + fileName);

                    // Extract install.xml file from zip
                    var hasInstallXML = false;
                    using (var zip = ZipFile.Read(SepFunctions.GetDirValue("app_data") + "temp\\" + fileName))
                    {
                        ZipEntry entry = null;
                        foreach (var entry_loopVariable in zip)
                        {
                            entry = entry_loopVariable;
                            if (entry.FileName == "install.xml")
                            {
                                entry.Extract(SepFunctions.GetDirValue("app_data") + "temp\\", ExtractExistingFileAction.OverwriteSilently);
                                Thread.Sleep(20);
                                hasInstallXML = true;
                                break;
                            }
                        }

                        if (hasInstallXML)
                            try
                            {
                                using (var sr = new StreamReader(SepFunctions.GetDirValue("app_data") + "temp\\install.xml"))
                                {
                                    string line = null;
                                    line = sr.ReadToEnd();
                                    if (!Directory.Exists(SepFunctions.GetDirValue("skins") + SepFunctions.ParseXML("installFolder", line)))
                                    {
                                        foreach (var entry_loopVariable in zip)
                                        {
                                            entry = entry_loopVariable;
                                            if (entry.FileName != "install.xml")
                                            {
                                                entry.Extract(SepFunctions.GetDirValue("skins"), ExtractExistingFileAction.OverwriteSilently);
                                                Thread.Sleep(20);
                                            }
                                        }

                                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                                        {
                                            conn.Open();
                                            using (var cmd = new SqlCommand("INSERT INTO SiteTemplates (TemplateID, TemplateName, Description, FolderName, DatePosted, DateUpdated, useTemplate, AccessKeys, enableUserPage, Status) VALUES(@TemplateID, @TemplateName, @Description, @FolderName, @DatePosted, @DateUpdated, '0', '|2|', 0, '1')", conn))
                                            {
                                                cmd.Parameters.AddWithValue("@TemplateID", FileID);
                                                cmd.Parameters.AddWithValue("@TemplateName", SepFunctions.ParseXML("templateName", line));
                                                cmd.Parameters.AddWithValue("@Description", SepFunctions.ParseXML("description", line));
                                                cmd.Parameters.AddWithValue("@FolderName", SepFunctions.ParseXML("installFolder", line));
                                                cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                                                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                                                cmd.ExecuteNonQuery();
                                            }
                                        }

                                        ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Template has been successfully installed on your web site.") + "</div>";
                                    }
                                    else
                                    {
                                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This template is already installed on your site.") + "</div>";
                                    }
                                }
                            }
                            catch
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid skin file upload. (Error reading install.xml file)") + "</div>";
                            }
                        else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid skin file upload. (Missing install.xml file)") + "</div>";
                    }

                    try
                    {
                        File.Delete(SepFunctions.GetDirValue("app_data") + "temp\\install.xml");
                        File.Delete(SepFunctions.GetDirValue("app_data") + "temp\\" + fileName);
                    }
                    catch
                    {
                        // Could not delete temp files
                    }
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid skin file upload. (Not a valid zip file)") + "</div>";
                }
            }
            catch
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid skin file upload. (Can not read zip file)") + "</div>";
            }
        }
    }
}