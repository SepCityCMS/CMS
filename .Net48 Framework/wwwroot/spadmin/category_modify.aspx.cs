// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="category_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class category_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class category_modify : Page
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
                    CategoryType.Items[0].Text = SepFunctions.LangText("Audio");
                    CategoryType.Items[1].Text = SepFunctions.LangText("Document");
                    CategoryType.Items[2].Text = SepFunctions.LangText("Image");
                    CategoryType.Items[3].Text = SepFunctions.LangText("Software");
                    CategoryType.Items[4].Text = SepFunctions.LangText("Video");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Category");
                    ListUnderLabel.InnerText = SepFunctions.LangText("List this category as sub-category of a category below:");
                    CategoryNameLabel.InnerText = SepFunctions.LangText("Category Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    CategoryTypeLabel.InnerText = SepFunctions.LangText("Category Type:");
                    ModeratorLabel.InnerText = SepFunctions.LangText("Moderator:");
                    ModulesLabel.InnerText = SepFunctions.LangText("Modules to Show Category In:");
                    ImageLabel.InnerText = SepFunctions.LangText("Select an Image to Upload:");
                    AccessKeysLabel.InnerText = SepFunctions.LangText("Access keys required to access this category:");
                    WriteKeysLabel.InnerText = SepFunctions.LangText("Access keys required to upload/write content this category:");
                    ManageKeysLabel.InnerText = SepFunctions.LangText("Access keys required to manage content this category:");
                    PageTitleLabel.InnerText = SepFunctions.LangText("Page Title:");
                    MetaDescriptionLabel.InnerText = SepFunctions.LangText("Meta Description:");
                    MetaKeywordsLabel.InnerText = SepFunctions.LangText("Meta Keywords:");
                    PortalSelectionLabel.InnerText = SepFunctions.LangText("Select the portals to show this category in:");
                    WeightLabel.InnerText = SepFunctions.LangText("Weight:");
                    CategoryNameRequired.ErrorMessage = SepFunctions.LangText("~~Category Name~~ is required.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), true) == false)
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

            ListUnder.ModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));

            if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) != 10) CategoryTypeRow.Visible = false;
            if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) != 12) ModeratorRow.Visible = false;

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), false) == false) tabPermissions.Visible = false;

            if (SepFunctions.Get_Portal_ID() > 0) PortalSelection.Text = "|" + SepFunctions.Get_Portal_ID() + "|";

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("CatID")))
            {
                if (SepCommon.SepCore.Request.Item("DoAction") == "DeleteImage")
                {
                    var sReturn = SepCommon.DAL.Categories.Category_Delete_Image(SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")));

                    if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
                    else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

                    CatImage.Visible = false;
                    CatImageDelete.Visible = false;
                }

                var jCategories = SepCommon.DAL.Categories.Category_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")));

                if (jCategories.CatID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Category~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Category");
                    CatID.Value = SepCommon.SepCore.Request.Item("CatID");
                    ListUnder.CatID = Strings.ToString(jCategories.ListUnder);
                    CategoryName.Value = jCategories.CategoryName;
                    Description.Value = jCategories.Description;
                    CategoryType.Value = jCategories.CatType;
                    Moderator.Value = jCategories.Moderator;
                    Modules.Text = jCategories.ModuleIDs;
                    AccessKeysSelection.Text = jCategories.AccessKeys;
                    AccessKeysHide.Checked = jCategories.AccessHide;
                    WriteKeysSelection.Text = jCategories.WriteKeys;
                    WriteKeysHide.Checked = jCategories.WriteHide;
                    ManageKeysSelection.Text = jCategories.ManageKeys;
                    PageTitle.Value = jCategories.SEOPageTitle;
                    MetaTagDescription.Value = jCategories.SEODescription;
                    MetaTagKeywords.Value = jCategories.Keywords;
                    PortalSelection.Text = jCategories.PortalIDs;
                    ShareContent.Checked = jCategories.Sharing;
                    ExcludePortalSecurity.Checked = jCategories.ExcPortalSecurity;
                    Weight.Value = Strings.ToString(jCategories.Weight);

                    if (!string.IsNullOrWhiteSpace(jCategories.ImageData))
                    {
                        CatImage.Visible = true;
                        CatImageDelete.Visible = true;
                        CatImage.ImageUrl = "show_image.aspx?CatID=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
                        CatImageDelete.NavigateUrl = "category_modify.aspx?DoAction=DeleteImage&CatID=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
                        ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                    }
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    ListUnder.CatID = Request.Form["ListUnder"];
                    Modules.Text = Request.Form["Modules"];
                    AccessKeysSelection.Text = Request.Form["AccessKeysSelection"];
                    WriteKeysSelection.Text = Request.Form["WriteKeysSelection"];
                    ManageKeysSelection.Text = Request.Form["ManageKeysSelection"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(CatID.Value)) CatID.Value = Strings.ToString(SepFunctions.GetIdentity());

                    if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0) Modules.Text = "|" + SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) + "|";
                    PortalSelection.Text = "|" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")) + "|";
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
            string sReturn;

            var imageType = string.Empty;
            var imageData = string.Empty;

            if (ImageUpload.PostedFile == null || string.IsNullOrWhiteSpace(ImageUpload.PostedFile.FileName))
            {
            }
            else
            {
                var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(ImageUpload.PostedFile.InputStream.Length)) + 1];
                ImageUpload.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                imageType = ImageUpload.PostedFile.ContentType;
                imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));
            }

            sReturn = SepCommon.DAL.Categories.Category_Save(SepFunctions.toLong(CatID.Value), ListUnder.CatID, CategoryName.Value, Description.Value, Modules.Text, AccessKeysSelection.Text, AccessKeysHide.Checked, WriteKeysSelection.Text, AccessKeysHide.Checked, ManageKeysSelection.Text, PageTitle.Value, MetaTagDescription.Value, MetaTagKeywords.Value, PortalSelection.Text, CategoryType.Value, Moderator.Value, ShareContent.Checked, ExcludePortalSecurity.Checked, SepFunctions.toLong(Weight.Value), imageData, imageType, 0);

            if (!string.IsNullOrWhiteSpace(imageData))
            {
                CatImage.Visible = true;
                CatImageDelete.Visible = true;
                CatImage.ImageUrl = "show_image.aspx?CatID=" + SepFunctions.toLong(CatID.Value);
                CatImageDelete.NavigateUrl = "category_modify.aspx?DoAction=DeleteImage&CatID=" + SepFunctions.toLong(CatID.Value);
                ImageLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
            }

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

            CategoryNameLabel.InnerText = SepFunctions.LangText("Category Name:");
            ModFormDiv.Visible = false;
        }
    }
}