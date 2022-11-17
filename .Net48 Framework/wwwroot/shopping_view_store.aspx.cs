// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopping_view_store.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class shopping_view_store.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopping_view_store : Page
    {
        /// <summary>
        /// The s store identifier
        /// </summary>
        public static string sStoreID = string.Empty;

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

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
        /// Formats the ISAPI.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public string Format_ISAPI(object sText)
        {
            return SepFunctions.Format_ISAPI(Strings.ToString(sText));
        }

        /// <summary>
        /// Formats the price.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sUnitPrice">The s unit price.</param>
        /// <param name="sRecurringPrice">The s recurring price.</param>
        /// <param name="sRecurringCycle">The s recurring cycle.</param>
        /// <returns>System.String.</returns>
        public string Format_Price(string sSalePrice, string sUnitPrice, string sRecurringPrice, string sRecurringCycle)
        {
            var str = string.Empty;

            var toSalePrice = SepFunctions.toDecimal(sSalePrice);
            var toUnitPrice = SepFunctions.toDecimal(sUnitPrice);
            var toRecurringPrice = SepFunctions.toDecimal(sRecurringPrice);

            if (toSalePrice < toUnitPrice && toSalePrice > 0)
                str += "<s>";
            str += SepFunctions.Pricing_Long_Price(toUnitPrice, toRecurringPrice, sRecurringCycle);
            if (toSalePrice < toUnitPrice && toSalePrice > 0)
                str += "</s>";

            return str;
        }

        /// <summary>
        /// Formats the sale price.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sRecurringPrice">The s recurring price.</param>
        /// <param name="sRecurringCycle">The s recurring cycle.</param>
        /// <returns>System.String.</returns>
        public string Format_Sale_Price(string sSalePrice, string sRecurringPrice, string sRecurringCycle)
        {
            return SepFunctions.Pricing_Long_Price(SepFunctions.toDecimal(sSalePrice), SepFunctions.toDecimal(sRecurringPrice), sRecurringCycle);
        }

        /// <summary>
        /// Gets the sale percentage.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sUnitPrice">The s unit price.</param>
        /// <param name="sRecurringPrice">The s recurring price.</param>
        /// <returns>System.String.</returns>
        public string Get_Sale_Percentage(string sSalePrice, string sUnitPrice, string sRecurringPrice)
        {
            decimal sSavePrice = SepFunctions.toDecimal(sUnitPrice) - SepFunctions.toDecimal(sSalePrice);
            double sSavePercent = SepFunctions.Format_Double(Strings.FormatNumber(100 - SepFunctions.toDecimal(sSalePrice) / SepFunctions.toDecimal(sUnitPrice) * 100, 0));

            var sSaveSetup = SepFunctions.toDouble(sRecurringPrice) > 0 ? " " + SepFunctions.LangText("on the setup") : string.Empty;

            return "(-" + SepFunctions.Format_Currency(sSavePrice) + "), " + SepFunctions.LangText("save") + " " + sSavePercent + "%" + sSaveSetup;
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
        /// Orders the product.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>System.String.</returns>
        public string orderProduct(string ProductID, string StoreID)
        {
            return "orderProduct('" + ProductID + "', '" + StoreID + "');return false;";
        }

        /// <summary>
        /// Sessions the user identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Session_User_ID()
        {
            return SepFunctions.Session_User_ID();
        }

        /// <summary>
        /// Shows the sale row.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sUnitPrice">The s unit price.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Show_Sale_Row(string sSalePrice, string sUnitPrice)
        {
            if (SepFunctions.toDecimal(sSalePrice) < SepFunctions.toDecimal(sUnitPrice) && SepFunctions.toDecimal(sSalePrice) > 0)
                return true;
            return false;
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
                if (dv != null)
                {
                    dv.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ListProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ListProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListProducts.PageIndex = e.NewPageIndex;
            ListProducts.DataSource = BindData();
            ListProducts.DataBind();
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 41;

            if ((SepFunctions.Setup(GlobalVars.ModuleID, "ShopMallEnable") != "Enable" || SepFunctions.isUserPage()) && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ShopMallStore"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            sStoreID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("StoreID")).ToString();

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ListProducts.DataSource = dv;
                ListProducts.DataBind();
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
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var cProducts = SepCommon.DAL.ShoppingMall.GetShopProducts(CategoryId: SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")), showSalesOnly: false, ShowOnlyAvailable: true);

            if (cProducts.Count > 0)
            {
                ListProducts.Visible = true;

                dv = new DataView(SepFunctions.ListToDataTable(cProducts));
                return dv;
            }

            return null;
        }
    }
}