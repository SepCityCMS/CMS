// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopping_my_orders.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class shopping_my_orders.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopping_my_orders : Page
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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
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
                ManageGridView.Visible = false;
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

            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));

            var StoreID = SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID());
            if (StoreID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Access Denied. You do not have a store setup.") + "</div>";
                return;
            }

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No invoices have been added.") + "</div>";
                ManageGridView.Visible = false;
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
            var sStoreId = SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID());

            var dInvoices = SepCommon.DAL.Invoices.GetInvoices(GridViewSortExpression, GetSortDirection(), ModuleSearch.Value, SepCommon.SepCore.Request.Item("UserID"), RequireUserID: true, StoreID: sStoreId);

            dv = new DataView(SepFunctions.ListToDataTable(dInvoices));
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