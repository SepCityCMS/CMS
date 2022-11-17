// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="photos_album_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class photos_album_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class photos_album_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Album");
                    AlbumNameLabel.InnerText = SepFunctions.LangText("Album Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    AlbumPasswordLabel.InnerText = SepFunctions.LangText("Password (Users will be required to enter this password to view photos in this album):");
                    PicturesLabel.InnerText = SepFunctions.LangText("Select Photos to Upload:");
                    LetterNameRequired.ErrorMessage = SepFunctions.LangText("~~Album Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 28;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PhotosEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PhotosCreate"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.CompareKeys(SepFunctions.Security("PhotosShared")) == false) ShareRow.Visible = false;

            if (SepFunctions.CompareKeys(SepFunctions.Security("PhotosPassword")) == false) AlbumPasswordRow.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AlbumID")))
            {
                var jPhotos = SepCommon.DAL.PhotoAlbums.Album_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AlbumID")));

                if (jPhotos.AlbumID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Album~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Album");
                    AlbumID.Value = SepCommon.SepCore.Request.Item("AlbumID");
                    AlbumName.Value = jPhotos.AlbumName;
                    Description.Value = jPhotos.Description;
                    ShareAlbum.Checked = jPhotos.SharedAlbum;
                    AlbumPassword.Attributes.Add("type", "password");
                    AlbumPassword.Value = jPhotos.Password;
                    Pictures.ContentID = SepCommon.SepCore.Request.Item("AlbumID");
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (string.IsNullOrWhiteSpace(AlbumID.Value)) AlbumID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    Pictures.ContentID = AlbumID.Value;
                    ShareAlbum.Checked = true;
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostCreateAlbum", "GetCreateAlbum", AlbumID.Value, true) == false) SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                }
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intReturn = SepCommon.DAL.PhotoAlbums.Album_Save(SepFunctions.toLong(AlbumID.Value), SepFunctions.Session_User_ID(), AlbumName.Value, Description.Value, ShareAlbum.Checked, AlbumPassword.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, AlbumName.Value);
        }
    }
}