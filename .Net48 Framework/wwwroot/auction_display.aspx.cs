// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="auction_display.aspx.cs" company="SepCity, Inc.">
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
    /// Class auction_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class auction_display : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
        }

        /// <summary>
        /// Handles the Click event of the BidButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BidButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "auction_display.aspx?AdID=" + SepCommon.SepCore.Request.Item("AdID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var AdId = SepCommon.SepCore.Request.Item("AdID");
            var maxBid = SepFunctions.toDouble(UnitPrice2.Value);
            ErrorMessage.InnerHtml = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM AuctionAds WHERE AdID=@AdID AND OldAd='0'", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdId);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This item has been sold.") + "</div>";
                            DisplayContent.Visible = false;
                        }
                        else
                        {
                            RS.Read();

                            if (SepFunctions.openNull(RS["UserID"]) == SepFunctions.Session_User_ID())
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You cannot bid on your own item.") + "</div>";
                                DisplayContent.Visible = false;
                                return;
                            }

                            string sTitle = SepFunctions.openNull(RS["Title"]);
                            string sActDesc = SepFunctions.LangText("[[Username]] has bidded for an auction") + Environment.NewLine;
                            sActDesc += SepFunctions.LangText("Auction title: ~~" + Title + "~~") + Environment.NewLine;
                            SepFunctions.Activity_Write("BidAuction", sActDesc, GlobalVars.ModuleID, AdId);
                            if (SepFunctions.toDate(SepFunctions.openNull(RS["EndDate"])) < DateTime.Now)
                            {
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Sorry, this auction has ended.") + "</div>";
                                DisplayContent.Visible = false;
                            }
                            else
                            {
                                double CurrentBid = SepFunctions.toDouble(SepFunctions.openNull(RS["CurrentBid"]));
                                if (Math.Abs(CurrentBid) < 1) CurrentBid = SepFunctions.toDouble(SepFunctions.openNull(RS["StartBid"]));
                                CurrentBid += SepFunctions.toDouble(SepFunctions.openNull(RS["BidIncrease"]));
                                if (maxBid < CurrentBid || maxBid <= SepFunctions.toDouble(SepFunctions.openNull(RS["MaxBid"])))
                                {
                                    if (maxBid >= CurrentBid)
                                        using (var cmd2 = new SqlCommand("UPDATE AuctionAds SET CurrentBid='" + SepFunctions.FixWord(Strings.ToString(maxBid)) + "', TotalBids='" + SepFunctions.toDouble(SepFunctions.openNull(RS["TotalBids"])) + 1 + "' WHERE AdID='" + SepFunctions.FixWord(AdId) + "' AND OldAd='0'", conn))
                                        {
                                            cmd2.ExecuteNonQuery();
                                        }

                                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Sorry, you have been out bidded.") + "</div>";
                                    UnitPrice.InnerHtml = SepFunctions.Format_Currency(CurrentBid);
                                    UnitPrice2.Value = SepFunctions.Format_Currency(CurrentBid + SepFunctions.toDouble(SepFunctions.openNull(RS["BidIncrease"])));
                                }
                                else
                                {
                                    ConfirmTitle.InnerHtml = sTitle;
                                    ConfirmMaxBid.InnerHtml = SepFunctions.Format_Currency(maxBid);
                                    ConfirmCurrentBid.InnerHtml = SepFunctions.Format_Currency(CurrentBid);
                                    ConfirmNote.InnerHtml = SepFunctions.LangText("Shipping charges may apply depending on the seller and item you are ordering");
                                    BidContent.Visible = true;
                                    DisplayContent.Visible = false;
                                }
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ConfirmButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "auction_display.aspx?AdID=" + SepCommon.SepCore.Request.Item("AdID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var AdId = SepCommon.SepCore.Request.Item("AdID");
            var maxBid = SepFunctions.toDouble(UnitPrice2.Value);

            ErrorMessage.InnerHtml = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TotalBids,BidIncrease FROM AuctionAds WHERE OldAd='0' AND AdID='" + SepFunctions.FixWord(AdId) + "' AND OldAd='0'", conn))
                {
                    cmd.Parameters.AddWithValue("@AdID", AdId);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This item has been sold.") + "</div>";
                            DisplayContent.Visible = false;
                            BidContent.Visible = false;
                        }
                        else
                        {
                            RS.Read();
                            using (var cmd2 = new SqlCommand("UPDATE AuctionAds SET MaxBid='" + SepFunctions.FixWord(Strings.ToString(maxBid)) + "', CurrentBid='" + SepFunctions.toDecimal(ConfirmCurrentBid.InnerHtml) + "', BidUserID='" + SepFunctions.Session_User_ID() + "', TotalBids='" + SepFunctions.toLong(SepFunctions.openNull(RS["TotalBids"])) + 1 + "' WHERE AdID='" + SepFunctions.FixWord(AdId) + "' AND OldAd='0'", conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }

                            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully bidded on an item.") + "</div>";
                            UnitPrice.InnerHtml = SepFunctions.Format_Currency(ConfirmCurrentBid.InnerHtml);
                            UnitPrice2.Value = SepFunctions.Format_Currency(SepFunctions.toDouble(ConfirmCurrentBid.InnerHtml) + SepFunctions.toDouble(SepFunctions.openNull(RS["BidIncrease"])));
                            DisplayContent.Visible = false;
                            BidContent.Visible = false;
                        }

                    }
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            long Rating = 0;
            long RatingCount = 0;
            GlobalVars.ModuleID = 31;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "AuctionEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("AuctionAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                if (!Page.IsPostBack)
                {
                    var jAds = SepCommon.DAL.Auctions.Auction_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")));

                    if (jAds.AdID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Ad~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                    else
                    {
                        if (jAds.EndDate > DateTime.Now)
                        {
                            if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewAd", "GetViewAd", SepCommon.SepCore.Request.Item("AdID"), false) == false)
                            {
                                SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                                return;
                            }

                            Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jAds.Title);
                            RatingText.InnerHtml = SepFunctions.LangText("This user has not been rated yet.");

                            AdTitle.InnerHtml = jAds.Title;
                            sUserID = jAds.UserID;
                            UserName.InnerHtml = SepFunctions.GetUserInformation("UserName", jAds.UserID);
                            AdID.InnerHtml = Strings.ToString(jAds.AdID);
                            UnitPrice.InnerHtml = jAds.CurrentBid;
                            UnitPrice2.Value = SepFunctions.Format_Currency(SepFunctions.toDouble(jAds.CurrentBid) + SepFunctions.toDouble(jAds.BidIncrease));
                            Location.InnerHtml = jAds.Location;
                            DatePosted.InnerHtml = Strings.ToString(jAds.DatePosted);
                            Visits.InnerHtml = Strings.ToString(jAds.Visits);
                            Description.InnerHtml = jAds.Description;

                            // Show Images
                            AuctionImages.ContentUniqueID = Strings.ToString(jAds.AdID);
                            AuctionImages.ModuleID = GlobalVars.ModuleID;
                            AuctionImages.UserID = jAds.UserID;

                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();

                                using (var cmd = new SqlCommand("SELECT Rating FROM AuctionFeedback WHERE ToUserID=@ToUserID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@ToUserID", jAds.UserID);
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

                            if (Rating > 0)
                            {
                                long YourRating = Rating / RatingCount;
                                RatingText.InnerHtml = SepFunctions.LangText("User Rating:") + " " + Strings.ToString(YourRating);
                            }
                        }
                        else
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This auction has already ended.") + "</div>";
                            DisplayContent.Visible = false;
                        }
                    }
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Sorry, this ad~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
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