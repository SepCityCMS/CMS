// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="vouchers_purchase.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class vouchers_purchase.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class vouchers_purchase : Page
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

            GlobalVars.ModuleID = 65;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "VoucherEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("VoucherAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "vouchers_purchase.aspx?VoucherID=" + SepCommon.SepCore.Request.Item("VoucherID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT VoucherID FROM Vouchers WHERE BuyEndDate > getDate() AND VoucherID=@VoucherID", conn))
                {
                    cmd.Parameters.AddWithValue("@VoucherID", SepCommon.SepCore.Request.Item("VoucherID"));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(GlobalVars.ModuleID, "VoucherAgreementBuy")))
                            {
                                SignupAgreement.InnerHtml = SepFunctions.HTMLDecode(SepFunctions.Setup(GlobalVars.ModuleID, "VoucherAgreementBuy"));
                            }
                            else
                            {
                                var jVouchers = SepCommon.DAL.Vouchers.Voucher_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("VoucherID")));

                                if (jVouchers.VoucherID == 0)
                                {
                                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Voucher~~ does not exist.") + "</div>";
                                    DisplayContent.Visible = false;
                                }
                                else
                                {
                                    SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, 65, string.Empty, "1", string.Empty, string.Empty, false, jVouchers.BuyTitle, Strings.ToString(jVouchers.SalePrice), string.Empty, string.Empty, string.Empty, jVouchers.VoucherID, 0, SepFunctions.Get_Portal_ID());
                                    SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
                                }
                            }
                        }
                        else
                        {
                            ErrorMessage.InnerHtml = SepFunctions.LangText("Invalid Voucher");
                            DisplayContent.Visible = false;
                        }
                    }
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (Agreement.Checked == false)
            {
                ErrorMessage.InnerHtml = SepFunctions.LangText("You must accept our agreement before purchasing this voucher.");
            }
            else
            {
                var jVouchers = SepCommon.DAL.Vouchers.Voucher_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("VoucherID")));

                if (jVouchers.VoucherID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Voucher~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, 65, string.Empty, "1", string.Empty, string.Empty, false, jVouchers.BuyTitle, Strings.ToString(jVouchers.SalePrice), string.Empty, string.Empty, string.Empty, jVouchers.VoucherID, 0, SepFunctions.Get_Portal_ID());
                }

                SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
            }
        }
    }
}