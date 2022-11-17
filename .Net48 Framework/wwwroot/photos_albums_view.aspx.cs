// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="photos_albums_view.aspx.cs" company="SepCity, Inc.">
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
    /// Class photos_albums_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class photos_albums_view : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 28;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PhotosEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PhotosAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AlbumID")))
            {
                var jPhotos = SepCommon.DAL.PhotoAlbums.Album_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AlbumID")));

                if (jPhotos.AlbumID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Album~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jPhotos.AlbumName);
                    AlbumID.Value = Strings.ToString(jPhotos.AlbumID);
                    AlbumName.InnerHtml = jPhotos.AlbumName;

                    if (!string.IsNullOrWhiteSpace(jPhotos.Password) && jPhotos.UserID != SepFunctions.Session_User_ID())
                    {
                        ShowPassword.Visible = true;
                        DisplayContent.Visible = false;
                    }

                    // Show Images
                    AlbumImages.ContentUniqueID = Strings.ToString(jPhotos.AlbumID);
                    AlbumImages.ModuleID = GlobalVars.ModuleID;
                    AlbumImages.UserID = jPhotos.UserID;
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Album~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
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

        /// <summary>
        /// Handles the Click event of the PassButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PassButton_Click(object sender, EventArgs e)
        {
            var jPhotos = SepCommon.DAL.PhotoAlbums.Album_Get(SepFunctions.toLong(AlbumID.Value));

            if (jPhotos.AlbumID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Album~~ does not exist.") + "</div>";
                ShowPassword.Visible = false;
                DisplayContent.Visible = false;
            }
            else
            {
                if (jPhotos.Password == Password.Value)
                {
                    ErrorMessage.InnerHtml = string.Empty;
                    AlbumID.Value = Strings.ToString(jPhotos.AlbumID);
                    AlbumName.InnerHtml = jPhotos.AlbumName;

                    ShowPassword.Visible = false;
                    DisplayContent.Visible = true;

                    // Show Images
                    AlbumImages.ContentUniqueID = Strings.ToString(jPhotos.AlbumID);
                    AlbumImages.ModuleID = GlobalVars.ModuleID;
                    AlbumImages.UserID = jPhotos.UserID;
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid password.") + "</div>";
                    ShowPassword.Visible = true;
                    DisplayContent.Visible = false;
                }
            }
        }
    }
}