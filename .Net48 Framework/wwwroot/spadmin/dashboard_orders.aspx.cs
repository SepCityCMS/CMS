// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="dashboard_orders.aspx.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class dashboard_orders.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class dashboard_orders : Page
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
                    RecentInvoicesGridView.Columns[1].HeaderText = SepFunctions.LangText("User Name");
                    RecentInvoicesGridView.Columns[2].HeaderText = SepFunctions.LangText("Full Name");
                    RecentInvoicesGridView.Columns[3].HeaderText = SepFunctions.LangText("Date Ordered");
                    RecentInvoicesGridView.Columns[4].HeaderText = SepFunctions.LangText("Amount Paid");
                    MonthlyTotalsGridView.Columns[0].HeaderText = SepFunctions.LangText("Month");
                    MonthlyTotalsGridView.Columns[1].HeaderText = SepFunctions.LangText("Total Recurring");
                    MonthlyTotalsGridView.Columns[2].HeaderText = SepFunctions.LangText("Total One-Time");
                    MonthlyTotalsGridView.Columns[3].HeaderText = SepFunctions.LangText("Handling Charges");
                    MonthlyTotalsGridView.Columns[4].HeaderText = SepFunctions.LangText("Total Amount");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) == false)
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

            var dRecentInvoices = SepCommon.DAL.Invoices.GetInvoices();

            RecentInvoicesGridView.DataSource = dRecentInvoices.Take(10);
            RecentInvoicesGridView.DataBind();

            var dMonthlyIncome = SepCommon.DAL.Invoices.GetInvoicesMonthlyTotals();

            MonthlyTotalsGridView.DataSource = dMonthlyIncome.Take(12);
            MonthlyTotalsGridView.DataBind();
        }
    }
}