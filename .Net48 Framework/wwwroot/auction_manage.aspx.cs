// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="auction_manage.aspx.cs" company="SepCity, Inc.">
//     Copyright ? SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class auction_manage.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class auction_manage : Page
    {
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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
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
                    FilterDoAction.Items[0].Text = SepFunctions.LangText("Select an Action");
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Ads");
                    SellingContent.Columns[1].HeaderText = SepFunctions.LangText("Title");
                    SellingContent.Columns[2].HeaderText = SepFunctions.LangText("Current Bid");
                    SellingContent.Columns[3].HeaderText = SepFunctions.LangText("Date Posted");
                    SoldContent.Columns[0].HeaderText = SepFunctions.LangText("Title");
                    SoldContent.Columns[1].HeaderText = SepFunctions.LangText("Buyer");
                    SoldContent.Columns[2].HeaderText = SepFunctions.LangText("Sold Price");
                    SoldContent.Columns[3].HeaderText = SepFunctions.LangText("Date Sold");
                    SoldContent.Columns[4].HeaderText = SepFunctions.LangText("Leave Feedback");
                    BoughtContent.Columns[0].HeaderText = SepFunctions.LangText("Title");
                    BoughtContent.Columns[1].HeaderText = SepFunctions.LangText("Seller");
                    BoughtContent.Columns[2].HeaderText = SepFunctions.LangText("Sold Price");
                    BoughtContent.Columns[3].HeaderText = SepFunctions.LangText("Date Sold");
                    BoughtContent.Columns[4].HeaderText = SepFunctions.LangText("Leave Feedback");
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
        /// Handles the Sorting event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var dAuctions = SepCommon.DAL.Auctions.GetAuctionAds(GridViewSortExpression, GetSortDirection(), ModuleSearch.Value, SepFunctions.Session_User_ID());

            GridViewSortExpression = e.SortExpression;
            SellingContent.DataSource = dAuctions.Take(50);
            SellingContent.DataBind();
        }

        /// <summary>
        /// Handles the Click event of the ModuleSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ModuleSearchButton_Click(object sender, EventArgs e)
        {
            DeleteResult.InnerHtml = string.Empty;

            var dAuctions = SepCommon.DAL.Auctions.GetAuctionAds(GridViewSortExpression, GetSortDirection(), ModuleSearch.Value, SepFunctions.Session_User_ID());

            SellingContent.DataSource = dAuctions.Take(50);
            SellingContent.DataBind();

            if (SellingContent.Rows.Count == 0) DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your search has returned no results.") + "</div>";
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

            long Rating = 0;
            long RatingCount = 0;
            TranslatePage();

            GlobalVars.ModuleID = 31;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "AuctionEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("AuctionPost"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepCommon.SepCore.Request.Item("DoAction") == "Feedback") ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your feedback has been successfully saved.") + "</div>";

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM AuctionFeedback WHERE ToUserID=@ToUserID", conn))
                {
                    cmd.Parameters.AddWithValue("@ToUserID", SepFunctions.Session_User_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            Rating += SepFunctions.toLong(SepFunctions.openNull(RS["Rating"]));
                            RatingCount += 1;
                        }

                    }
                }
            }

            if (Rating == 0)
            {
                lblYourRating.InnerHtml = SepFunctions.LangText("N/A");
            }
            else
            {
                long YourRating = Rating / RatingCount;
                lblYourRating.InnerHtml = Strings.ToString(YourRating) + " out of 5";
            }

            var dAllAuctions = SepCommon.DAL.Auctions.GetAuctionAds(userId: SepFunctions.Session_User_ID(), availableItems: true);

            SellingContent.DataSource = dAllAuctions.Take(50);
            SellingContent.DataBind();

            var dSoldAuctions = SepCommon.DAL.Auctions.GetAuctionAds(userId: SepFunctions.Session_User_ID(), soldItems: true);
            SoldContent.DataSource = dSoldAuctions.Take(50);
            SoldContent.DataBind();

            var dBoughtAuctions = SepCommon.DAL.Auctions.GetAuctionAds(boughtUserID: SepFunctions.Session_User_ID());
            BoughtContent.DataSource = dBoughtAuctions.Take(50);
            BoughtContent.DataBind();

            if (SellingContent.Rows.Count == 0) DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No auction ads have been added.") + "</div>";
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
        /// Handles the Click event of the RunAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RunAction_Click(object sender, EventArgs e)
        {
            GetSortDirection();
            if (string.IsNullOrWhiteSpace(FilterDoAction.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                return;
            }

            var sIDs = UniqueIDs.Value;

            if (Strings.Len(sIDs) > 0)
            {
                try
                {
                    var sDeleteResult = SepCommon.DAL.Auctions.Auction_Delete(sIDs);
                    DeleteResult.InnerHtml = Strings.InStr(sDeleteResult, SepFunctions.LangText("Successfully")) > 0 ? "<div class=\"alert alert-success\" role=\"alert\">" + sDeleteResult + "</div>" : "<div class=\"alert alert-danger\" role=\"alert\">" + sDeleteResult + "</div>";
                }
                catch
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                var dAuctions = SepCommon.DAL.Auctions.GetAuctionAds(Strings.ToString(GridViewSortExpression), Strings.ToString(GridViewSortDirection), ModuleSearch.Value, SepFunctions.Session_User_ID());
                SellingContent.DataSource = dAuctions.Take(50);
                SellingContent.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
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