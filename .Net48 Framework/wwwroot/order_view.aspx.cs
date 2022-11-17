// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="order_view.aspx.cs" company="SepCity, Inc.">
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
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class order_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class order_view : Page
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
        /// Formats the currency.
        /// </summary>
        /// <param name="sNum">The s number.</param>
        /// <returns>System.String.</returns>
        public string Format_Currency(object sNum)
        {
            return SepFunctions.Format_Currency(sNum);
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
                    ManageGridView.Columns[0].HeaderText = SepFunctions.LangText("Product Name");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("Unit Price");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("Quantity");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Total Price");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("View Invoice");
                    InvoiceNumberLabel.InnerText = SepFunctions.LangText("Invoice Number:");
                    DiscountCodeLabel.InnerText = SepFunctions.LangText("Discount Code:");
                    StatusLabel.InnerText = SepFunctions.LangText("Order Status:");
                    OrderDateLabel.InnerText = SepFunctions.LangText("Order Date:");
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

            GlobalVars.ModuleID = 33;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "order_view.aspx?InvoiceID=" + SepCommon.SepCore.Request.Item("InvoiceID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("InvoiceID")))
            {
                string sInvoiceID = SepCommon.SepCore.Request.Item("InvoiceID");
                var dInvoicesProducts = SepCommon.DAL.Invoices.GetInvoicesProducts(SepFunctions.toLong(sInvoiceID));

                ManageGridView.DataSource = dInvoicesProducts;
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0)
                {
                    NoProductsAdded.InnerHtml = SepFunctions.LangText("No products/services have been added to this invoice.");
                    ManageGridView.Visible = false;
                }

                var jInvoices = SepCommon.DAL.Invoices.Invoice_Get(SepFunctions.toLong(sInvoiceID));

                if (jInvoices.InvoiceID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Invoice~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    InvoiceNumber.Value = sInvoiceID;
                    if (!string.IsNullOrWhiteSpace(jInvoices.DiscountCode)) DiscountCode.Value = jInvoices.DiscountCode;
                    else DiscountCode.Value = SepFunctions.LangText("N/A");
                    Status.Value = jInvoices.StatusText;
                    OrderDate.Value = Strings.FormatDateTime(jInvoices.OrderDate, Strings.DateNamedFormat.ShortDate);
                }

                SqlDataAdapter da = null;
                var ds = new DataSet();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Count(IP.ProductID) AS ProductCount FROM Invoices_Products AS IP WHERE IP.InvoiceID='" + SepFunctions.FixWord(sInvoiceID) + "'", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                }

                ds.Dispose();
                da.Dispose();
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid invoice") + "</div>";
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