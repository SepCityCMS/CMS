// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="viewcart.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class viewcart.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class viewcart : Page
    {
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
        /// Formats the currency.
        /// </summary>
        /// <param name="sNum">The s number.</param>
        /// <returns>System.String.</returns>
        public string Format_Currency(object sNum)
        {
            return SepFunctions.Format_Currency(sNum);
        }

        /// <summary>
        /// Gets the custom options.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns>System.String.</returns>
        public string GetCustomOptions(string ProductId)
        {
            var str = string.Empty;

            var cCustomFields = SepCommon.DAL.CustomFields.GetCustomFields(ModuleID: 41, uniqueIds: "|" + ProductId + "|");
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                for (var i = 0; i <= cCustomFields.Count - 1; i++)
                {
                    using (var cmd = new SqlCommand("SELECT FieldValue FROM CustomFieldUsers WHERE FieldID=@FieldID AND UserID=@UserID AND ModuleID='41' AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@FieldID", cCustomFields[i].FieldID);
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (Strings.InStr(SepFunctions.openNull(RS["FieldValue"]), "||") > 0)
                                    str += "<br/>" + cCustomFields[i].FieldName + ": " + Strings.Split(SepFunctions.openNull(RS["FieldValue"]), "||")[0] + " (+" + SepFunctions.Format_Currency(Strings.Split(SepFunctions.openNull(RS["FieldValue"]), "||")[1]) + ")";
                                else
                                    str += "<br/>" + cCustomFields[i].FieldName + ": " + SepFunctions.openNull(RS["FieldValue"]);
                            }

                        }
                    }
                }
            }

            return str;
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
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CheckoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CheckoutButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect(SepFunctions.GetInstallFolder() + "viewcart_checkout.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect(SepCommon.SepCore.Request.Item("ContinueURL"));
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
        /// Handles the Click event of the EmptyCartButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void EmptyCartButton_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("DELETE FROM Invoices WHERE InvoiceID=@InvoiceID AND inCart='1'", conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", SepFunctions.Session_Invoice_ID());
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM Invoices_Products WHERE InvoiceID=@InvoiceID", conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", SepFunctions.Session_Invoice_ID());
                    cmd.ExecuteNonQuery();
                }
            }

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            if (ManageGridView.Rows.Count == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your shopping cart is empty.") + "</div>";
                ManageGridView.Visible = false;
                EmptyCartButton.Visible = false;
                UpdateCartButton.Visible = false;
                CheckoutButton.Visible = false;
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

            GlobalVars.ModuleID = 995;

            if (!IsPostBack)
            {
                var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID, "View Shopping Cart"), "|$$|");
                Array.Resize(ref arrHeader, 3);
                Page.Title = arrHeader[0];
            }

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", SepFunctions.Session_User_Name());

            if (ManageGridView.Rows.Count == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your shopping cart is empty.") + "</div>";
                ManageGridView.Visible = false;
                EmptyCartButton.Visible = false;
                UpdateCartButton.Visible = false;
                CheckoutButton.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ContinueURL"))) ContinueButton.Visible = false;
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
        /// Handles the Click event of the UpdateCartButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void UpdateCartButton_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var InvoiceProducts = SepCommon.DAL.Invoices.GetInvoicesProducts(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()));

                for (var i = 0; i <= InvoiceProducts.Count - 1; i++)
                    if (SepFunctions.toLong(Request.Form["Qty" + InvoiceProducts[i].InvoiceProductID]) == 0)
                        using (var cmd = new SqlCommand("DELETE FROM Invoices_Products WHERE InvoiceProductID=@InvoiceProductID AND InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceProductID", InvoiceProducts[i].InvoiceProductID);
                            cmd.Parameters.AddWithValue("@InvoiceID", SepFunctions.Session_Invoice_ID());
                            cmd.ExecuteNonQuery();
                        }
                    else
                        using (var cmd = new SqlCommand("UPDATE Invoices_Products SET Quantity=@Quantity WHERE InvoiceProductID=@InvoiceProductID AND InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", Request.Form["Qty" + InvoiceProducts[i].InvoiceProductID]);
                            cmd.Parameters.AddWithValue("@InvoiceProductID", InvoiceProducts[i].InvoiceProductID);
                            cmd.Parameters.AddWithValue("@InvoiceID", SepFunctions.Session_Invoice_ID());
                            cmd.ExecuteNonQuery();
                        }
            }

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            if (ManageGridView.Rows.Count == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your shopping cart is empty.") + "</div>";
                ManageGridView.Visible = false;
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            decimal totalHandling = 0;
            decimal TotalPrice = 0;
            decimal totalTaxes = 0;

            DataView dvShipping;
            List<InvoicesProducts> dShopProducts = SepCommon.DAL.Invoices.GetInvoicesProducts(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()));

            dv = new DataView(SepFunctions.ListToDataTable(dShopProducts));

            for (var i = 0; i <= dShopProducts.Count - 1; i++)
            {
                totalHandling += SepFunctions.toDecimal(dShopProducts[i].Handling);
                TotalPrice += SepFunctions.toDecimal(dShopProducts[i].TotalPriceNoHandling);
            }

            if (dShopProducts.Count > 0)
            {
                var tbl = new DataTable();
                var col1 = new DataColumn("ProductName", typeof(string));
                var col2 = new DataColumn("TotalPriceNoHandling", typeof(string));
                var col3 = new DataColumn("UnitPrice", typeof(string));

                tbl.Columns.Add(col1);
                tbl.Columns.Add(col2);
                tbl.Columns.Add(col3);

                var row1 = tbl.NewRow();
                row1["ProductName"] = "Sub-Total";
                row1["TotalPriceNoHandling"] = SepFunctions.Format_Currency(TotalPrice);
                row1["UnitPrice"] = "xxblankxx";
                tbl.Rows.Add(row1);

                if (totalHandling > 0)
                {
                    var row2 = tbl.NewRow();
                    row2["ProductName"] = "Shipping & Handling";
                    row2["TotalPriceNoHandling"] = SepFunctions.Format_Currency(totalHandling);
                    row2["UnitPrice"] = "xxblankxx";
                    tbl.Rows.Add(row2);
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("State")))
                {
                    Taxes dTaxes = SepCommon.DAL.ShoppingCart.Tax_Get_By_State(SepFunctions.GetUserInformation("State"));
                    if (!string.IsNullOrWhiteSpace(dTaxes.TaxPercent))
                    {
                        if (SepFunctions.toDecimal(dTaxes.TaxPercent) > 0)
                        {
                            totalTaxes = (SepFunctions.toDecimal(dTaxes.TaxPercent) / 100) * (TotalPrice + totalHandling);
                            var row3 = tbl.NewRow();
                            row3["ProductName"] = "Sales Tax (" + dTaxes.TaxPercent + "%)";
                            row3["TotalPriceNoHandling"] = SepFunctions.Format_Currency(totalTaxes);
                            row3["UnitPrice"] = "xxblankxx";
                            tbl.Rows.Add(row3);
                        }
                    }
                }

                var row4 = tbl.NewRow();
                row4["ProductName"] = "Total Price";
                row4["TotalPriceNoHandling"] = SepFunctions.Format_Currency((totalHandling + TotalPrice + totalTaxes));
                row4["UnitPrice"] = "xxblankxx";
                tbl.Rows.Add(row4);

                dvShipping = new DataView(tbl);
                dv.Table.Merge(dvShipping.Table);
                dvShipping.Dispose();
                tbl.Dispose();
            }

            return dv;
        }
    }
}