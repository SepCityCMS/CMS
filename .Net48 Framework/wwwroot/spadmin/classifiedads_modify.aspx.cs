// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="classifiedads_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class classifiedads_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class classifiedads_modify : Page
    {
        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Classified Ad");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below where to list your item:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    AdTitleLabel.InnerText = SepFunctions.LangText("Title:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Pictures:");
                    QuantityLabel.InnerText = SepFunctions.LangText("Quantity:");
                    PriceLabel.InnerText = SepFunctions.LangText("Price:");
                    ExpirationDateLabel.InnerText = SepFunctions.LangText("Expiration Date:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    AdTitleRequired.ErrorMessage = SepFunctions.LangText("~~Title~~ is required.");
                    QuantityRequired.ErrorMessage = SepFunctions.LangText("~~Quantity~~ is required.");
                    PriceRequired.ErrorMessage = SepFunctions.LangText("~~Price~~ is required.");
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

            GlobalVars.ModuleID = 44;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ClassifiedAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ClassifiedAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                var jClassifieds = SepCommon.DAL.Classifieds.Classified_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jClassifieds.AdID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Classified Ad~~ does not exist.") + "</div>";
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Classified Ad");
                    AdID.Value = SepCommon.SepCore.Request.Item("AdID");
                    Pictures.ContentID = SepCommon.SepCore.Request.Item("AdID");
                    Pictures.UserID = jClassifieds.UserID;
                    Category.CatID = Strings.ToString(jClassifieds.CatID);
                    Portal.Text = Strings.ToString(jClassifieds.PortalID);
                    AdTitle.Value = jClassifieds.Title;
                    Quantity.Value = Strings.ToString(jClassifieds.Quantity);
                    Price.Value = Strings.ToString(jClassifieds.Price);
                    FullDescription.Text = jClassifieds.Description;
                    ExpirationDate.Value = Strings.ToString(jClassifieds.EndDate);

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("AdID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }

                sUserID = jClassifieds.UserID;
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                    Portal.Text = Request.Form["Portal"];
                    FullDescription.Text = FullDescription.Text;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(AdID.Value)) AdID.Value = Strings.ToString(SepFunctions.GetIdentity());
                }

                Pictures.ContentID = AdID.Value;
            }

            if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes") CategoryRequired.ErrorMessage = SepFunctions.LangText("You must select the lowest level of category available.");
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                Pictures.showTemp = true;
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var intReturn = SepCommon.DAL.Classifieds.Classified_Save(SepFunctions.toLong(AdID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), AdTitle.Value, FullDescription.Text, SepFunctions.toLong(Quantity.Value), SepFunctions.toDouble(Price.Value), SepFunctions.toDate(ExpirationDate.Value), true, SepFunctions.toLong(Request.Form["Portal"]));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}