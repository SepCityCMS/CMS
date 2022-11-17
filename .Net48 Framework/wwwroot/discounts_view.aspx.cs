// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="discounts_view.aspx.cs" company="SepCity, Inc.">
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
    /// Class discounts_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class discounts_view : Page
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
        /// Formats the price.
        /// </summary>
        /// <param name="PriceType">Type of the price.</param>
        /// <param name="PriceOff">The price off.</param>
        /// <returns>System.String.</returns>
        public string FormatPrice(string PriceType, string PriceOff)
        {
            string GetPrice;
            switch (SepFunctions.toLong(PriceType))
            {
                case 0:
                    GetPrice = SepFunctions.Format_Currency(PriceOff) + " " + SepFunctions.LangText("Off");
                    break;

                case 1:
                    GetPrice = PriceOff + "%";
                    break;

                default:
                    GetPrice = SepFunctions.Format_Currency(PriceOff);
                    break;
            }

            return GetPrice;
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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

            GlobalVars.ModuleID = 5;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "DiscountsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("DiscountsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("DiscountID")))
            {
                var jDiscounts = SepCommon.DAL.Discounts.Discount_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("DiscountID")));

                if (jDiscounts.DiscountID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Discount coupon~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jDiscounts.LabelText);
                    DiscountCode.InnerHtml = jDiscounts.DiscountCode;
                    ExpireDate.InnerHtml = Strings.FormatDateTime(jDiscounts.ExpireDate, Strings.DateNamedFormat.ShortDate);
                    Quantity.InnerHtml = Strings.ToString(jDiscounts.Quantity);
                    CompanyName.InnerHtml = !string.IsNullOrWhiteSpace(jDiscounts.CompanyName) ? "</td><tr><td align=\"center\">" + jDiscounts.CompanyName + "</td></tr><td>" : string.Empty;
                    Disclaimer.InnerHtml = !string.IsNullOrWhiteSpace(jDiscounts.Disclaimer) ? "<td width=\"100%\" valign=\"Bottom\"><b>Disclaimer:</b>" + jDiscounts.Disclaimer + "</td>" : string.Empty;
                    LabelText.InnerHtml = jDiscounts.LabelText;
                    MarkOffPrice.InnerHtml = FormatPrice(jDiscounts.MarkOffType, jDiscounts.MarkOffPrice);
                    ProductImage.InnerHtml = !string.IsNullOrWhiteSpace(jDiscounts.ProductImage) ? "<td valign=\"top\" width=\"1\"><br/><img src=\"" + jDiscounts.ProductImage + "\" border=\"0\" /></td>" : string.Empty;
                    BarCodeImage.InnerHtml = !string.IsNullOrWhiteSpace(jDiscounts.BarCodeImage) ? "<img src=\"" + jDiscounts.BarCodeImage + "\" border=\"0\" />" : "<img src=\"" + GetInstallFolder() + "images/admin/barcode.gif\" border=\"0\" />";
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Discount coupon~~ does not exist.") + "</div>";
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
    }
}