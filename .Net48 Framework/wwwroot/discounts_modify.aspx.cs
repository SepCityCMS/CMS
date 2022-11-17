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
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class discounts_modify1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class discounts_modify1 : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 5;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "DiscountsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("DiscountsPost"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("DiscountID")))
            {
                var jDiscounts = SepCommon.DAL.Discounts.Discount_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("DiscountID")));

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
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(DiscountID.Value)) DiscountID.Value = Strings.ToString(SepFunctions.GetIdentity());
                ProductImageUpload.ContentID = DiscountID.Value;
                BarCodeUpload.ContentID = DiscountID.Value;
                if (!Page.IsPostBack)
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostAddCoupon", "GetAddCoupon", DiscountID.Value, true) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    City.Value = SepFunctions.GetUserInformation("City");
                    State.Text = SepFunctions.GetUserInformation("State");
                    State.Country = SepFunctions.GetUserInformation("Country");
                    PostalCode.Value = SepFunctions.GetUserInformation("ZipCode");
                    Country.Text = SepFunctions.GetUserInformation("Country");
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
            var intReturn = SepCommon.DAL.Discounts.Discount_Save(SepFunctions.toLong(DiscountID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), DiscountCode.Value, CompanyName.Value, LabelText.Value, Disclaimer.Value, City.Value, State.Text, PostalCode.Value, Country.Text, SepFunctions.toLong(Quantity.Value), MarkOffPrice.Value, SepFunctions.toInt(MarkOffType.Value), SepFunctions.toDate(ExpirationDate.Value), SepFunctions.Get_Portal_ID());

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, LabelText.Value);
        }
    }
}