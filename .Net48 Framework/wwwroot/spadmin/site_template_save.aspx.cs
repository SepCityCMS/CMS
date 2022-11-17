// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_save.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Web.UI;

    /// <summary>
    /// Class site_template_save.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_save : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The s temporary folder
        /// </summary>
        private string sTempFolder;

        /// <summary>
        /// Adms the template save preview.
        /// </summary>
        /// <param name="sFolderName">Name of the s folder.</param>
        public void ADM_Template_Save_Preview(string sFolderName)
        {
            var sSaveFolder = SepFunctions.GetDirValue("App_Data") + "templates\\saved\\" + sFolderName + "\\";

            if (Directory.Exists(sSaveFolder)) Directory.Delete(sSaveFolder, true);

            Directory.CreateDirectory(sSaveFolder);

            File.Copy(sTempFolder + "template.master", sSaveFolder + "template.master", true);
            File.Copy(sTempFolder + "colors.css", sSaveFolder + "colors.css", true);
            File.Copy(sTempFolder + "layout.css", sSaveFolder + "layout.css", true);
            File.Copy(sTempFolder + "menus.css", sSaveFolder + "menus.css", true);
            File.Copy(sTempFolder + "template-colors.xml", sSaveFolder + "template-colors.xml", true);
            File.Copy(sTempFolder + "template-config.xml", sSaveFolder + "template-config.xml", true);
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
        /// Gets the folder namme.
        /// </summary>
        /// <param name="sName">Name of the s.</param>
        /// <returns>System.String.</returns>
        public string GetFolderNamme(string sName)
        {
            return Strings.Left(SepFunctions.ReplaceSpecial(Strings.Replace(sName, " ", string.Empty)), 30);
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Save Template");
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

            sTempFolder = SepFunctions.GetDirValue("App_Data") + "templates\\temp\\";

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSiteLooks")))
            {
                form1.Visible = false;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSiteLooks"), false) == false)
            {
                form1.Visible = false;
                return;
            }

            if (!Page.IsPostBack)
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ModTemp")))
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("SELECT TemplateName,Description FROM SiteTemplates WHERE Status='2' AND FolderName=@FolderName", conn))
                        {
                            cmd.Parameters.AddWithValue("@FolderName", SepCommon.SepCore.Request.Item("ModTemp"));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    TemplateName.Value = SepFunctions.openNull(RS["TemplateName"]);
                                    Description.Value = SepFunctions.openNull(RS["Description"]);
                                }

                            }
                        }
                    }
                }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sTemplateName = TemplateName.Value;
            var sFolderName = GetFolderNamme(sTemplateName);

            var jScript = string.Empty;

            if (string.IsNullOrWhiteSpace(sTemplateName))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Template Name is required.") + "</div>";
                return;
            }

            var sSaveFolder = SepFunctions.GetDirValue("skins") + sFolderName + "\\";

            // Save to Predefined Folder
            if (!Directory.Exists(sSaveFolder))
            {
                Directory.CreateDirectory(sSaveFolder);
            }
            else
            {
                if (!File.Exists(sSaveFolder + "builder.txt"))
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid template name.") + "</div>";
                    return;
                }
            }

            Directory.CreateDirectory(sSaveFolder + "styles");
            Directory.CreateDirectory(sSaveFolder + "images");

            File.Copy(sTempFolder + "colors.css", sSaveFolder + "styles\\colors.css", true);
            File.Copy(sTempFolder + "layout.css", sSaveFolder + "styles\\layout.css", true);
            File.Copy(sTempFolder + "menus.css", sSaveFolder + "styles\\menus.css", true);

            File.Copy(sTempFolder + "template.master", sSaveFolder + "template.master", true);

            var sTemplateFile = string.Empty;
            using (var reader = File.OpenText(sSaveFolder + "template.master"))
            {
                sTemplateFile = Strings.Replace(reader.ReadToEnd(), "/skins/template-", "/skins/" + sFolderName + "/styles/");
            }

            using (var outfile = new StreamWriter(sSaveFolder + "template.master"))
            {
                outfile.Write(sTemplateFile);
            }

            // Save file so we know this is a generate template
            if (!Directory.Exists(sSaveFolder + "builder.txt"))
            {
                using (var fs = File.Create(sSaveFolder + "builder.txt", 1024))
                {
                }
            }

            var updateTemplate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FolderName FROM SiteTemplates WHERE FolderName=@FolderName", conn))
                {
                    cmd.Parameters.AddWithValue("@FolderName", sFolderName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows) updateTemplate = true;
                    }
                }

                if (updateTemplate)
                    using (var cmd = new SqlCommand("UPDATE SiteTemplates SET DateUpdated=@DateUpdated, Status='2' WHERE FolderName=@FolderName", conn))
                    {
                        cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@FolderName", sFolderName);
                        cmd.ExecuteNonQuery();
                    }
                else
                    using (var cmd = new SqlCommand("INSERT INTO SiteTemplates (TemplateID, TemplateName, Description, FolderName, DatePosted, DateUpdated, useTemplate, Status) VALUES(@TemplateID, @TemplateName, @Description, @FolderName, @DatePosted, @DateUpdated, '0', '2')", conn))
                    {
                        cmd.Parameters.AddWithValue("@TemplateID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@TemplateName", sTemplateName);
                        cmd.Parameters.AddWithValue("@Description", Description.Value);
                        cmd.Parameters.AddWithValue("@FolderName", sFolderName);
                        cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
            }

            // Save to Saved Folder for the template builder
            ADM_Template_Save_Preview(sFolderName);

            jScript += "<script type=\"text/javascript\">";
            jScript += "parent.document.getElementById('TemplateFrame').src='site_template_builder.aspx?ModTemp=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("ModTemp")) + "&PortalID=" + SepFunctions.Get_Portal_ID() + "';";
            jScript += "parent.closeDialog('SaveHrefFrame');";
            jScript += "</script>";

            var cstype = GetType();

            Page.ClientScript.RegisterClientScriptBlock(cstype, "ButtonClickScript", jScript);
        }
    }
}