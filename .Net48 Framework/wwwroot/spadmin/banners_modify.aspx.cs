// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="banners_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class advertising_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class advertising_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Advertisement");
                    UserNameLabel.InnerText = SepFunctions.LangText("User Name:");
                    SiteURLLabel.InnerText = SepFunctions.LangText("Site URL (ex. http://www.google.com):");
                    ImageLabel.InnerText = SepFunctions.LangText("Select an Image to Upload:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description (Internal User Only):");
                    ZoneLabel.InnerText = SepFunctions.LangText("Select a zone to target the advertisement to:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Select a Start Date:");
                    Label1.InnerText = SepFunctions.LangText("Select a End Date:");
                    HTMLCodeLabel.InnerText = SepFunctions.LangText("HTML Code (This will overwrite the clicks/exposures, site URL and Image Upload):");
                    MaximumClicksLabel.InnerText = SepFunctions.LangText("Maximum Clicks (Enter \"-1\" for Unlimited):");
                    MaximumExposuresLabel.InnerText = SepFunctions.LangText("Maximum Exposures (Enter \"-1\" for Unlimited):");
                    TotalClicksLabel.InnerText = SepFunctions.LangText("Total Clicks:");
                    TotalExposuresLabel.InnerText = SepFunctions.LangText("Total Exposures:");
                    CategoriesLabel.InnerText = SepFunctions.LangText("Target Ad to Categories:");
                    PagesLabel.InnerText = SepFunctions.LangText("Target Ad to Pages:");
                    CountryLabel.InnerText = SepFunctions.LangText("Target to Country:");
                    StateLabel.InnerText = SepFunctions.LangText("Target to State / Province:");
                    PortalsLabel.InnerText = SepFunctions.LangText("Target Ad to Portals:");
                    UserNameRequired.ErrorMessage = SepFunctions.LangText("~~User Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
                    MaximumClicksRequired.ErrorMessage = SepFunctions.LangText("~~Maximum Clicks~~ is required.");
                    MaximumExposuresRequired.ErrorMessage = SepFunctions.LangText("~~Maximum Exposures~~ is required.");
                    TotalClicksRequired.ErrorMessage = SepFunctions.LangText("~~Total Clicks~~ is required.");
                    TotalExposuresRequired.ErrorMessage = SepFunctions.LangText("~~Total Exposures~~ is required.");
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

            GlobalVars.ModuleID = 2;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdsAdmin"), false) == false)
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

            if (SepFunctions.showCategories() == false) CategoriesRow.Visible = false;

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminEditPage"), true) == false) WebPagesRow.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                var jAdvertisements = SepCommon.DAL.Advertisements.Advertisement_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")));

                if (jAdvertisements.AdID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Advertisement~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Advertisement");
                    AdID.Value = SepCommon.SepCore.Request.Item("AdID");
                    UserName.Value = SepFunctions.GetUserInformation("UserName", jAdvertisements.UserID);
                    SiteURL.Value = jAdvertisements.SiteURL;
                    Description.Value = jAdvertisements.Description;
                    Zone.Text = Strings.ToString(jAdvertisements.ZoneID);
                    StartDate.Value = Strings.FormatDateTime(jAdvertisements.StartDate, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(jAdvertisements.EndDate, Strings.DateNamedFormat.ShortDate);
                    HTMLCode.Value = jAdvertisements.HTMLCode;
                    MaximumClicks.Value = Strings.ToString(jAdvertisements.MaximumClicks);
                    MaximumExposures.Value = Strings.ToString(jAdvertisements.MaximumExposures);
                    TotalClicks.Value = Strings.ToString(jAdvertisements.TotalClicks);
                    TotalExposures.Value = Strings.ToString(jAdvertisements.TotalExposures);
                    Categories.Text = jAdvertisements.CatIDs;
                    Pages.Text = jAdvertisements.PageIDs;
                    Country.Text = jAdvertisements.Country;
                    State.Text = jAdvertisements.State;
                    State.Country = jAdvertisements.Country;
                    Portals.Text = jAdvertisements.PortalIDs;

                    if (jAdvertisements.UseHTML && !string.IsNullOrWhiteSpace(jAdvertisements.HTMLCode))
                    {
                        AdImage.Visible = false;
                        ImageHTML.InnerHtml = jAdvertisements.HTMLCode;
                        ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(jAdvertisements.ImageData))
                        {
                            AdImage.Visible = true;
                            AdImage.ImageUrl = "show_image.aspx?ModuleID=2&AdID=" + SepCommon.SepCore.Request.Item("AdID");
                            ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(jAdvertisements.ImageURL))
                            {
                                AdImage.Visible = true;
                                AdImage.ImageUrl = jAdvertisements.ImageURL;
                                ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                            }
                        }
                    }
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Categories.Text = Request.Form["Categories"];
                    Pages.Text = Request.Form["Pages"];
                    Country.Text = Request.Form["Country"];
                    State.Text = Request.Form["State"];
                    Portals.Text = Request.Form["Portals"];
                    Zone.Text = Request.Form["Zone"];
                }
                else
                {
                    UserName.Value = SepFunctions.Session_User_Name();
                    StartDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 1, DateTime.Today), Strings.DateNamedFormat.ShortDate);
                    if (string.IsNullOrWhiteSpace(AdID.Value)) AdID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    TotalClicks.Value = "0";
                    TotalExposures.Value = "0";
                    Pages.Text = "|-1|";
                    Portals.Text = "|-1|";
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
            var imageType = string.Empty;
            var imageData = string.Empty;

            var sFileExt = Strings.LCase(Path.GetExtension(ImageUpload.PostedFile.FileName)); // -V3095

            string sReturn;

            if (ImageUpload.PostedFile == null || string.IsNullOrWhiteSpace(ImageUpload.PostedFile.FileName))
            {
            }
            else
            {
                if (sFileExt != ".jpg" && sFileExt != ".jpeg" && sFileExt != ".gif" && sFileExt != ".png")
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid image file format. (Only jpg, gif and png files are supported)") + "</div>";
                    return;
                }

                var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(ImageUpload.PostedFile.InputStream.Length)) + 1];
                ImageUpload.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                imageType = ImageUpload.PostedFile.ContentType;
                imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));
            }

            sReturn = SepCommon.DAL.Advertisements.Advertisement_Save(SepFunctions.toLong(AdID.Value), SepFunctions.Session_User_Name(), SiteURL.Value, Description.Value, SepFunctions.toLong(Request.Form["Zone"]), SepFunctions.toDate(StartDate.Value), SepFunctions.toDate(EndDate.Value), HTMLCode.Value, MaximumClicks.Value, MaximumExposures.Value, TotalClicks.Value, TotalExposures.Value, Request.Form["Categories"], Request.Form["Pages"], Request.Form["Country"], Request.Form["State"], Request.Form["Portals"], imageData, imageType, 1);

            var jAdvertisements = SepCommon.DAL.Advertisements.Advertisement_Get(SepFunctions.toLong(AdID.Value));

            if (jAdvertisements.AdID > 0)
            {
                if (jAdvertisements.UseHTML && !string.IsNullOrWhiteSpace(jAdvertisements.HTMLCode))
                {
                    AdImage.Visible = false;
                    ImageHTML.InnerHtml = jAdvertisements.HTMLCode;
                    ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(jAdvertisements.ImageData))
                    {
                        AdImage.Visible = true;
                        AdImage.ImageUrl = "show_image.aspx?ModuleID=2&AdID=" + AdID.Value;
                        ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(jAdvertisements.ImageURL))
                        {
                            AdImage.Visible = true;
                            AdImage.ImageUrl = jAdvertisements.ImageURL;
                            ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                        }
                    }
                }
            }

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}