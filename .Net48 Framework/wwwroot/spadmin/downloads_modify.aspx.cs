// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="downloads_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class downloads_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class downloads_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Upload File");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    SongTitleLabel.InnerText = SepFunctions.LangText("Song Title:");
                    AlbumLabel.Text = SepFunctions.LangText("Album Name:");
                    DocumentTitleLabel.InnerText = SepFunctions.LangText("Document Title:");
                    DescriptionLabel.Text = SepFunctions.LangText("Description:");
                    VideoTitleLabel.InnerText = SepFunctions.LangText("Video Title:");
                    VideoDescLabel.InnerText = SepFunctions.LangText("Description:");
                    CaptionLabel.InnerText = SepFunctions.LangText("Caption:");
                    ApplicationNameLabel.InnerText = SepFunctions.LangText("Application Name:");
                    VersionLabel.InnerText = SepFunctions.LangText("Version:");
                    PriceLabel.InnerText = SepFunctions.LangText("Price:");
                    AppDescLabel.InnerText = SepFunctions.LangText("Description:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
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
            var sInstallFolder = SepFunctions.GetInstallFolder(true);

            TranslatePage();

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("LibraryAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("LibraryAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FileID")))
            {
                var jDownloads = SepCommon.DAL.Downloads.Download_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FileID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jDownloads.FileID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~File~~ does not exist.") + "</div>";
                    FormContent.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("View Download");
                    FileID.Value = SepCommon.SepCore.Request.Item("FileID");
                    Category.CatID = Strings.ToString(jDownloads.CatID);
                    Category.FileID = SepCommon.SepCore.Request.Item("FileID");
                    Portal.Text = Strings.ToString(jDownloads.PortalID);
                    switch (jDownloads.FileType)
                    {
                        case "Audio":
                            AudioRows.Visible = true;
                            SongTitle.Value = jDownloads.Field1;
                            Album.Value = jDownloads.Field2;
                            break;

                        case "Document":
                            DocumentRows.Visible = true;
                            DocumentTitle.Value = jDownloads.Field1;
                            Description.Value = jDownloads.Field2;
                            break;

                        case "Image":
                            ImageRows.Visible = true;
                            Caption.Value = jDownloads.Field1;
                            break;

                        case "Software":
                            SoftwareRows.Visible = true;
                            ApplicationName.Value = jDownloads.Field1;
                            Version.Value = jDownloads.Field2;
                            Price.Value = jDownloads.Field3;
                            AppDesc.Value = jDownloads.Field4;
                            break;

                        case "Video":
                            VideoRows.Visible = true;
                            VideoTitle.Value = jDownloads.Field1;
                            VideoDesc.Value = jDownloads.Field2;
                            break;
                    }

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("FileID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    Category.Disabled = true;
                    UploadFileRow.InnerHtml = "<a href=\"" + sInstallFolder + "downloads/" + jDownloads.FileName + "\" target=\"_blank\">" + SepFunctions.GetMasterDomain(false) + "downloads/" + jDownloads.FileName + "</a>";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(FileID.Value)) FileID.Value = Strings.ToString(SepFunctions.GetIdentity());
                Category.FileID = FileID.Value;
            }

            if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes") CategoryRequired.ErrorMessage = SepFunctions.LangText("You must select the lowest level of category available.");
        }
    }
}