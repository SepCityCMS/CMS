// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="vouchers_purchased.aspx.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class vouchers_purchased.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class vouchers_purchased : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

        /// <summary>
        /// Gets or sets the grid view sort direction.
        /// </summary>
        /// <value>The grid view sort direction.</value>
        private string GridViewSortDirection
        {
            get => ViewState["SortDirection"] == null ? "ASC" : ViewState["SortDirection"].ToString();
            set => ViewState["SortDirection"] = value;
        }

        /// <summary>
        /// Gets or sets the grid view sort expression.
        /// </summary>
        /// <value>The grid view sort expression.</value>
        private string GridViewSortExpression
        {
            get => ViewState["SortExpression"] == null ? string.Empty : ViewState["SortExpression"].ToString();
            set => ViewState["SortExpression"] = value;
        }

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
                    PageHeader.InnerHtml = SepFunctions.LangText("Purchased Vouchers");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("Purchase Code");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("Redeemed");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Bought By");
                    ManageGridView.Columns[4].HeaderText = SepFunctions.LangText("Sold By");
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
        /// Handles the PageIndexChanging event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetSortDirection();
            ManageGridView.PageIndex = e.NewPageIndex;
            ManageGridView.DataSource = BindData();
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the Sorting event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var imgArrowUp = "<i class=\"glyphicon glyphicon-arrow-up\">";
            var imgArrowDown = "<i class=\"glyphicon glyphicon-arrow-down\">";
            for (var i = 0; i <= ManageGridView.Columns.Count - 1; i++)
            {
                var iconPosition = ManageGridView.Columns[i].HeaderText.IndexOf("<i class=\"", StringComparison.OrdinalIgnoreCase);
                if (iconPosition > 0) ManageGridView.Columns[i].HeaderText = ManageGridView.Columns[i].HeaderText.Substring(0, iconPosition);
                if (ManageGridView.Columns[i].SortExpression == e.SortExpression)
                {
                    GetSortDirection();

                    if (GetSortDirection() == "ASC") ManageGridView.Columns[i].HeaderText += imgArrowUp;
                    else ManageGridView.Columns[i].HeaderText += imgArrowDown;
                }
            }

            GridViewSortExpression = e.SortExpression;

            DeleteResult.InnerHtml = string.Empty;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the Click event of the ModuleSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ModuleSearchButton_Click(object sender, EventArgs e)
        {
            DeleteResult.InnerHtml = string.Empty;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your search has returned no results.") + "</div>";
                PageManageGridView.Visible = false;
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

            GlobalVars.ModuleID = 65;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("VoucherAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("VoucherAdmin"), true) == false)
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

            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No vouchers have been purchased.") + "</div>";
                PageManageGridView.Visible = false;
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dVouchers = SepCommon.DAL.Vouchers.GetVouchersPurchased(GridViewSortExpression, GetSortDirection(), ModuleSearch.Value);

            dv = new DataView(SepFunctions.ListToDataTable(dVouchers));
            return dv;
        }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetSortDirection()
        {
            switch (Strings.ToString(GridViewSortDirection))
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;

                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }
    }
}