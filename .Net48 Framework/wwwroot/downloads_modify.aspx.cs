// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 06-12-2019
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
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class downloads_modify1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class downloads_modify1 : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 10;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "LibraryEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("LibraryUpload"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FileID")))
            {
                var jDownloads = SepCommon.DAL.Downloads.Download_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FileID")));

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

                    UploadFileRow.InnerHtml = "<a href=\"" + sInstallFolder + "downloads/" + jDownloads.FileName + "\" target=\"_blank\">" + SepFunctions.GetMasterDomain(false) + "downloads/" + jDownloads.FileName + "</a>";
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~File~~ does not exist.") + "</div>";
                FormContent.Visible = false;
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }
    }
}