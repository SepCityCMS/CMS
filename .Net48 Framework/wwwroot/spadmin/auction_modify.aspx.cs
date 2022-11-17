// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="auction_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class auction_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class auction_modify : Page
    {
        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Auction");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below where to list your item:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    AdTitleLabel.InnerText = SepFunctions.LangText("Title:");
                    StartingBidLabel.InnerText = SepFunctions.LangText("Starting Bid:");
                    IncreaseBidsLabel.InnerText = SepFunctions.LangText("Money To Increase Bids By:");
                    PicturesLabel.InnerText = SepFunctions.LangText("Pictures:");
                    ExpirationDateLabel.InnerText = SepFunctions.LangText("Expiration Date:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    AdTitleRequired.ErrorMessage = SepFunctions.LangText("~~Title~~ is required.");
                    StartingBidRequired.ErrorMessage = SepFunctions.LangText("~~Starting Bid~~ is required.");
                    IncreaseBidsRequired.ErrorMessage = SepFunctions.LangText("~~Increase Bids~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
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

            GlobalVars.ModuleID = 31;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AuctionAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AuctionAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                var jAuctions = SepCommon.DAL.Auctions.Auction_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jAuctions.AdID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Ad~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Auction");
                    AdID.Value = SepCommon.SepCore.Request.Item("AdID");
                    Pictures.ContentID = SepCommon.SepCore.Request.Item("AdID");
                    Pictures.UserID = jAuctions.UserID;
                    Category.CatID = Strings.ToString(jAuctions.CatID);
                    AdTitle.Value = jAuctions.Title;
                    StartingBid.Value = jAuctions.StartBid;
                    IncreaseBids.Value = jAuctions.BidIncrease;
                    FullDescription.Text = jAuctions.Description;
                    ExpirationDate.Value = Strings.ToString(jAuctions.EndDate);

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("AdID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    sUserID = jAuctions.UserID;

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                    FullDescription.Text = FullDescription.Text;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(AdID.Value)) AdID.Value = Strings.ToString(SepFunctions.GetIdentity());
                }

                Pictures.ContentID = AdID.Value;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                Pictures.showTemp = true;
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            if (string.IsNullOrWhiteSpace(Portal.Text))
            {
                Portal.Text = Strings.ToString(SepFunctions.Get_Portal_ID());
            }
            var intReturn = SepCommon.DAL.Auctions.Auction_Save(SepFunctions.toLong(AdID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), AdTitle.Value, FullDescription.Text, SepFunctions.toDouble(StartingBid.Value), SepFunctions.toDouble(IncreaseBids.Value), DateAndTime.DateAdd(DateAndTime.DateInterval.Day, SepFunctions.toInt(SepFunctions.Setup(GlobalVars.ModuleID, "AuctionDeleteDays")), SepFunctions.toDate(ExpirationDate.Value)), true, SepFunctions.toLong(Portal.Text));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}