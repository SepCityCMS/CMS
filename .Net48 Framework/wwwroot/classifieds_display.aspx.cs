// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="classifieds_display.aspx.cs" company="SepCity, Inc.">
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
    /// Class classifieds_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class classifieds_display : Page
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
        /// Handles the Click event of the BuyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BuyButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "classifieds_display.aspx?AdID=" + SepCommon.SepCore.Request.Item("AdID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            SepFunctions.Redirect(sInstallFolder + "classifieds_buy.aspx?AdID=" + SepCommon.SepCore.Request.Item("AdID") + "&Quantity=1");
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

            long Rating = 0;
            long RatingCount = 0;
            GlobalVars.ModuleID = 44;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ClassifiedAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                if (SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedBuy") == "Yes") BuyButton.Visible = false;

                var jAds = SepCommon.DAL.Classifieds.Classified_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")));

                if (jAds.AdID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Classified Ad~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewAd", "GetViewAd", SepCommon.SepCore.Request.Item("AdID"), false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    RatingText.InnerHtml = SepFunctions.LangText("This user has not been rated yet.");

                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jAds.Title);
                    AdTitle.InnerHtml = jAds.Title;
                    sUserID = jAds.UserID;
                    UserName.InnerHtml = SepFunctions.GetUserInformation("UserName", jAds.UserID);
                    AdID.InnerHtml = Strings.ToString(jAds.AdID);
                    UnitPrice.InnerHtml = Strings.ToString(SepFunctions.Format_Currency(jAds.Price));
                    UnitPrice2.InnerHtml = Strings.ToString(SepFunctions.Format_Currency(jAds.Price)); // -V3086
                    Quantity.InnerHtml = Strings.ToString(jAds.Quantity);
                    Location.InnerHtml = jAds.Location;
                    DatePosted.InnerHtml = Strings.ToString(jAds.DatePosted);
                    Visits.InnerHtml = Strings.ToString(jAds.Visits);
                    Description.InnerHtml = jAds.Description;

                    // Show Images
                    ClassifiedImages.ContentUniqueID = Strings.ToString(jAds.AdID);
                    ClassifiedImages.ModuleID = GlobalVars.ModuleID;
                    ClassifiedImages.UserID = jAds.UserID;

                    if (!string.IsNullOrWhiteSpace(jAds.UserID))
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();

                            using (var cmd = new SqlCommand("SELECT Rating FROM ClassifiedsFeedback WHERE ToUserID=@ToUserID", conn))
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
                        long YourRating = SepFunctions.toLong(Strings.FormatNumber(Rating / RatingCount));
                        RatingText.InnerHtml = SepFunctions.LangText("User Rating:") + " " + Strings.ToString(YourRating);
                    }
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Classified Ad~~ does not exist.") + "</div>";
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