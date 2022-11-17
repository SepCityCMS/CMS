// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="discounts_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class discounts_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class discounts_modify : Page
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
                    MarkOffType.Items[0].Text = SepFunctions.LangText("Dollars");
                    MarkOffType.Items[1].Text = SepFunctions.LangText("Percent");
                    MarkOffType.Items[2].Text = SepFunctions.LangText("Fixed-Price");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Discount");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    LabelTextLabel.InnerText = SepFunctions.LangText("Label Text:");
                    CompanyNameLabel.InnerText = SepFunctions.LangText("Company Name:");
                    DisclaimerLabel.InnerText = SepFunctions.LangText("Disclaimer:");
                    DiscountCodeLabel.InnerText = SepFunctions.LangText("Discount Code:");
                    MarkOffTypeLabel.InnerText = SepFunctions.LangText("Mark-off Price:");
                    QuantityLabel.InnerText = SepFunctions.LangText("Quantity:");
                    ExpirationDateLabel.InnerText = SepFunctions.LangText("Expiration Date:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    ProductImageLabel.InnerText = SepFunctions.LangText("Product Image:");
                    BarCodeUploadLabel.InnerText = SepFunctions.LangText("Bar Code Image:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    LabelTextRequired.ErrorMessage = SepFunctions.LangText("~~Label Text~~ is required.");
                    CompanyNameRequired.ErrorMessage = SepFunctions.LangText("~~Company Name~~ is required.");
                    DisclaimerRequired.ErrorMessage = SepFunctions.LangText("~~Disclaimer~~ is required.");
                    DiscountCodeRequired.ErrorMessage = SepFunctions.LangText("~~Discount Code~~ is required.");
                    QuantityRequired.ErrorMessage = SepFunctions.LangText("~~Quantity~~ is required.");
                    CityRequired.ErrorMessage = SepFunctions.LangText("~~City~~ is required.");
                    PostalCodeRequired.ErrorMessage = SepFunctions.LangText("~~Zip/Postal Code~~ is required.");
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

            GlobalVars.ModuleID = 5;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("DiscountsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("DiscountsAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("DiscountID")))
            {
                var jDiscounts = SepCommon.DAL.Discounts.Discount_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("DiscountID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jDiscounts.DiscountID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Discount coupon~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Discount");
                    DiscountID.Value = SepCommon.SepCore.Request.Item("DiscountID");
                    Category.CatID = Strings.ToString(jDiscounts.CatID);
                    Portal.Text = Strings.ToString(jDiscounts.PortalID);
                    LabelText.Value = jDiscounts.LabelText;
                    CompanyName.Value = jDiscounts.CompanyName;
                    Disclaimer.Value = jDiscounts.Disclaimer;
                    DiscountCode.Value = jDiscounts.DiscountCode;
                    MarkOffType.Value = jDiscounts.MarkOffType;
                    MarkOffPrice.Value = jDiscounts.MarkOffPrice;
                    Quantity.Value = Strings.ToString(jDiscounts.Quantity);
                    ExpirationDate.Value = jDiscounts.ExpireDate.ToShortDateString();
                    Country.Text = jDiscounts.Country;
                    City.Value = jDiscounts.City;
                    State.Text = jDiscounts.State;
                    PostalCode.Value = jDiscounts.PostalCode;

                    ProductImageUpload.ContentID = SepCommon.SepCore.Request.Item("DiscountID");
                    BarCodeUpload.ContentID = SepCommon.SepCore.Request.Item("DiscountID");

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("DiscountID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(DiscountID.Value)) DiscountID.Value = Strings.ToString(SepFunctions.GetIdentity());
                ProductImageUpload.ContentID = DiscountID.Value;
                BarCodeUpload.ContentID = DiscountID.Value;
                if (!Page.IsPostBack)
                {
                    City.Value = SepFunctions.GetUserInformation("City");
                    State.Text = SepFunctions.GetUserInformation("State");
                    State.Country = SepFunctions.GetUserInformation("Country");
                    PostalCode.Value = SepFunctions.GetUserInformation("ZipCode");
                    Country.Text = SepFunctions.GetUserInformation("Country");
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
            if (string.IsNullOrWhiteSpace(Portal.Text))
            {
                Portal.Text = Strings.ToString(SepFunctions.Get_Portal_ID());
            }
            var intReturn = SepCommon.DAL.Discounts.Discount_Save(SepFunctions.toLong(DiscountID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), DiscountCode.Value, CompanyName.Value, LabelText.Value, Disclaimer.Value, City.Value, State.Text, PostalCode.Value, Country.Text, SepFunctions.toLong(Quantity.Value), MarkOffPrice.Value, SepFunctions.toInt(MarkOffType.Value), SepFunctions.toDate(ExpirationDate.Value), SepFunctions.toLong(Portal.Text));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}