// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopping_order_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class shopping_order_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopping_order_modify : Page
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

            var StoreID = SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID());
            if (StoreID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Access Denied. You do not have a store setup.") + "</div>";
                return;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("InvoiceID")))
            {
                var sInvoiceID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("InvoiceID"));

                var dInvoicesProducts = SepCommon.DAL.Invoices.GetInvoicesProducts(sInvoiceID, StoreID);

                ManageGridView.DataSource = dInvoicesProducts;
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0) NoProductsAdded.InnerHtml = SepFunctions.LangText("No products/services have been added to this invoice.");

                var jInvoices = SepCommon.DAL.Invoices.Invoice_Get(sInvoiceID);

                if (jInvoices.InvoiceID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Invoice~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    InvoiceNumber.Value = Strings.ToString(sInvoiceID);
                    UserName.Value = jInvoices.UserName;
                    Status.Value = Strings.ToString(jInvoices.Status);
                    OrderDate.Value = Strings.FormatDateTime(jInvoices.OrderDate, Strings.DateNamedFormat.ShortDate);
                }

                SqlDataAdapter da = null;
                var ds = new DataSet();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT Count(IP.ProductID) AS ProductCount FROM Invoices_Products AS IP WHERE IP.InvoiceID='" + SepFunctions.FixWord(Strings.ToString(sInvoiceID)) + "' AND IP.StoreID='" + SepFunctions.FixWord(Strings.ToString(StoreID)) + "'", conn))
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
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid Invoice") + "</div>";
            }

            if (ManageGridView.Rows.Count == 0)
            {
                var tbl = new DataTable();
                var col1 = new DataColumn("ProductName", typeof(string));
                var col2 = new DataColumn("ProductID", typeof(string));
                var col3 = new DataColumn("Handling", typeof(string));
                var col4 = new DataColumn("Quantity", typeof(string));
                var col5 = new DataColumn("TotalPrice", typeof(string));
                var col6 = new DataColumn("UnitPrice", typeof(string));
                tbl.Columns.Add(col1);
                tbl.Columns.Add(col2);
                tbl.Columns.Add(col3);
                tbl.Columns.Add(col4);
                tbl.Columns.Add(col5);
                tbl.Columns.Add(col6);
                var row = tbl.NewRow();
                row["ProductName"] = string.Empty;
                row["ProductID"] = string.Empty;
                row["Handling"] = string.Empty;
                row["Quantity"] = string.Empty;
                row["TotalPrice"] = string.Empty;
                row["UnitPrice"] = string.Empty;
                tbl.Rows.Add(row);
                ManageGridView.DataSource = tbl;
                ManageGridView.DataBind();
                ManageGridView.Rows[0].Visible = false;
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