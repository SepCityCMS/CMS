// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="vouchers_display.aspx.cs" company="SepCity, Inc.">
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
    /// Class vouchers_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class vouchers_display : Page
    {
        /// <summary>
        /// The s voucher identifier
        /// </summary>
        public static string sVoucherID = string.Empty;

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
            GlobalVars.ModuleID = 65;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "VoucherEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("VoucherAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("VoucherID")))
            {
                var jVouchers = SepCommon.DAL.Vouchers.Voucher_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("VoucherID")));

                if (jVouchers.VoucherID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Voucher~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jVouchers.BuyTitle);
                    sVoucherID = SepCommon.SepCore.Request.Item("VoucherID");
                    BuyTitle.InnerHtml = jVouchers.BuyTitle;
                    ShortDescription.InnerHtml = jVouchers.ShortDescription;
                    LongDescription.InnerHtml = jVouchers.LongDescription;
                    SalePrice.InnerHtml = SepFunctions.Format_Currency(jVouchers.SalePrice);
                    RegularPrice.InnerHtml = SepFunctions.Format_Currency(jVouchers.RegularPrice);
                    BusinessName.InnerHtml = jVouchers.BusinessName;
                    Disclaimer.InnerHtml = jVouchers.Disclaimer;
                    QuantityRemaining.InnerHtml = Strings.ToString((jVouchers.Quantity - jVouchers.DealsBought));
                    SavePercent.InnerHtml = jVouchers.SavePercent;
                    SavePercent2.InnerHtml = jVouchers.SavePercent;
                    SavePrice.InnerHtml = jVouchers.SavePrice;
                    DealsBought.InnerHtml = Strings.ToString(jVouchers.DealsBought);

                    string TimeLeft = "<script language=\"JavaScript\">";
                    TimeLeft += "TargetDate = '" + jVouchers.BuyEndDate + "';";
                    TimeLeft += "BackColor = '';";
                    TimeLeft += "ForeColor = '';";
                    TimeLeft += "CountActive = true;";
                    TimeLeft += "CountStepper = -1;";
                    TimeLeft += "LeadingZero = true;";
                    if (DateAndTime.DateDiff(DateAndTime.DateInterval.Day, DateTime.Now, jVouchers.BuyEndDate) < 1)
                        TimeLeft += "DisplayFormat = '%%H%%:%%M%%:%%S%%';";
                    else
                        TimeLeft += "DisplayFormat = '%%D%% D, %%H%%:%%M%%:%%S%%';";
                    TimeLeft += "FinishMessage = unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("This deal is no longer available")) + "');";
                    TimeLeft += "</script>";
                    TimeLeft += "<script language=\"JavaScript\" src=\"" + SepFunctions.GetInstallFolder(true) + "js/countdown.js\"></script>";
                    TimeRemaining.InnerHtml = TimeLeft;
                    LogoImageUpload.InnerHtml = !string.IsNullOrWhiteSpace(jVouchers.LogoImage) ? "<br/><img src=\"" + jVouchers.LogoImage + "\" border=\"0\" />" : string.Empty;
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid Voucher") + "</div>";
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