// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="photos.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class photos.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class photos : Page
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

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            dynamic dPhotoAlbums;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
                dPhotoAlbums = SepCommon.DAL.PhotoAlbums.GetPhotoAlbumsUsers(UserID: SepCommon.SepCore.Request.Item("UserID"), onlyShared: false);
            else
                dPhotoAlbums = SepCommon.DAL.PhotoAlbums.GetPhotoAlbumsUsers(onlyShared: true);

            var str = new StringBuilder();
            var intCount = 0;

            str.Append("<table width=\"95%\" border=\"0\" align=\"center\">");
            for (var i = 0; i <= dPhotoAlbums.Count - 1; i++)
            {
                str.Append("<tr>");
                str.Append("<td width=\"180\" valign=\"top\">");
                str.Append("<img src=\"" + SepFunctions.userProfileImage(dPhotoAlbums[i].UserID) + "\" border=\"0\" />");
                str.Append("<br/><b>" + dPhotoAlbums[i].Username + "</b><br/>");
                str.Append("<a href=\"" + sInstallFolder + "messenger_compose.aspx?UserID=" + dPhotoAlbums[i].UserID + "\">" + SepFunctions.LangText("Send a Message") + "</a><br/>");
                str.Append("<a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + dPhotoAlbums[i].UserID + "\">" + SepFunctions.LangText("User Information") + "</a>");
                str.Append("</td>");
                str.Append("<td valign=\"top\"><table width=\"98%\" align=\"center\">");
                dynamic dPhotos;
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
                    dPhotos = SepCommon.DAL.PhotoAlbums.GetPhotoAlbums(UserID: dPhotoAlbums[i].UserID, onlyShared: false);
                else
                    dPhotos = SepCommon.DAL.PhotoAlbums.GetPhotoAlbums(UserID: dPhotoAlbums[i].UserID, onlyShared: true);
                for (var j = 0; j <= dPhotos.Count - 1; j++)
                {
                    if (SepFunctions.OffSetRows(intCount) == false) str.Append("<tr>");
                    str.Append("<td width=\"50%\" valign=\"top\">");
                    str.Append("<a href=\"" + sInstallFolder + "albums/" + dPhotos[j].AlbumID + "/" + SepFunctions.Format_ISAPI(dPhotos[j].AlbumName) + "/\"><img src=\"" + dPhotos[j].DefaultPicture + "\" border=\"0\" align=\"left\" /></a>");
                    str.Append("<a href=\"" + sInstallFolder + "albums/" + dPhotos[j].AlbumID + "/" + SepFunctions.Format_ISAPI(dPhotos[j].AlbumName) + "/\">" + dPhotos[j].AlbumName + "</a>");
                    str.Append("<br/>" + dPhotos[j].Description + "</td>");
                    if (SepFunctions.OffSetRows(intCount)) str.Append("</tr>");
                    intCount += 1;
                }

                str.Append("</table></td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"2\"><hr /></td>");
                str.Append("</tr>");
            }

            str.Append("</table>");

            PhotoContent.InnerHtml = Strings.ToString(str);
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