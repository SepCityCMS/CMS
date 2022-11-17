// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="affiliate_image_upload.aspx.cs" company="SepCity, Inc.">
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
    /// Class affiliate_image_upload.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class affiliate_image_upload : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Upload Image");
                    ImageNameLabel.InnerText = SepFunctions.LangText("Image Name:");
                    PortalIDLabel.InnerText = SepFunctions.LangText("Target Ad to Portal:");
                    ImageNameRequired.ErrorMessage = SepFunctions.LangText("You must select an image.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AffiliateAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AffiliateAdmin"), false) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ImageID")))
            {
                var jAffiliate = SepCommon.DAL.Affiliate.Image_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ImageID")));

                if (jAffiliate.ImageID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Affiliate Image~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Image");
                    ImageID.Value = SepCommon.SepCore.Request.Item("ImageID");
                    PortalID.Text = Strings.ToString(jAffiliate.PortalID);
                    if (!string.IsNullOrWhiteSpace(jAffiliate.ImageData))
                    {
                        AffiliateImage.Visible = true;
                        AffiliateImage.ImageUrl = "show_image.aspx?ModuleID=39&ImageID=" + SepCommon.SepCore.Request.Item("ImageID");
                        ImageNameLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ImageID.Value)) ImageID.Value = Strings.ToString(SepFunctions.GetIdentity());
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var imageType = string.Empty;
            var imageData = string.Empty;

            var sFileExt = Strings.LCase(Path.GetExtension(ImageName.PostedFile.FileName)); // -V3095
            if (sFileExt != ".jpg" && sFileExt != ".jpeg" && sFileExt != ".gif" && sFileExt != ".png")
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid image file format. (Only jpg, gif and png files are supported)") + "</div>";
                return;
            }

            if (ImageName.PostedFile == null || string.IsNullOrWhiteSpace(ImageName.PostedFile.FileName))
            {
            }
            else
            {
                var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(ImageName.PostedFile.InputStream.Length)) + 1];
                ImageName.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                imageType = ImageName.PostedFile.ContentType;
                imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));
            }

            var sFileName = Path.GetFileName(ImageName.PostedFile.FileName); // -V3125

            var sReturn = SepCommon.DAL.Affiliate.Image_Save(SepFunctions.toLong(ImageID.Value), SepFunctions.Session_User_ID(), 39, sFileName, imageData, imageType, SepFunctions.toLong(PortalID.Text));

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

            AffiliateImage.Visible = true;
            AffiliateImage.ImageUrl = "show_image.aspx?ModuleID=39&ImageID=" + ImageID.Value;
            ImageNameLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
            ModFormDiv.Visible = false;
        }
    }
}