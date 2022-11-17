// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="businesses_display.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class businesses_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class businesses_display : Page
    {
        /// <summary>
        /// The s address
        /// </summary>
        public static string sAddress = string.Empty;

        /// <summary>
        /// The s business identifier
        /// </summary>
        public static string sBusinessID = string.Empty;

        /// <summary>
        /// The s site URL
        /// </summary>
        public static string sSiteUrl = string.Empty;

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
        /// <param name="excludePortal">if set to <c>true</c> [exclude portal].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortal = false)
        {
            return SepFunctions.GetInstallFolder(excludePortal);
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

            GlobalVars.ModuleID = 20;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("BusinessAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            sBusinessID = SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(SepCommon.SepCore.Request.Item("BusinessID")));

            if (!string.IsNullOrWhiteSpace(sBusinessID))
            {
                var jBusiness = SepCommon.DAL.Businesses.Business_Get(SepFunctions.toLong(sBusinessID));

                if (jBusiness.BusinessID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Business~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewBusiness", "GetViewBusiness", sBusinessID, false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jBusiness.BusinessName);
                    BusinessName.InnerHtml = jBusiness.BusinessName;
                    sUserID = jBusiness.UserID;
                    var sLocationSeperator1 = !string.IsNullOrWhiteSpace(jBusiness.Address) ? ", " : string.Empty;
                    var sLocationSeperator2 = !string.IsNullOrWhiteSpace(jBusiness.City) && !string.IsNullOrWhiteSpace(jBusiness.State) ? ", " : string.Empty;
                    sAddress = jBusiness.Address + sLocationSeperator1 + jBusiness.City + sLocationSeperator2 + jBusiness.State + " " + jBusiness.ZipCode;
                    if (jBusiness.IncludeMap == false || string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleMapsAPI")) || string.IsNullOrWhiteSpace(sAddress)) MapCol.Visible = false;
                    else GoogleMap.InnerHtml = "<script async defer src=\"https://maps.googleapis.com/maps/api/js?key=" + SepFunctions.Setup(989, "GoogleMapsAPI") + "&callback=initMap\"></script>";
                    sSiteUrl = SepFunctions.GetMasterDomain(true);
                    FullDescription.InnerHtml = jBusiness.FullDescription;
                    StreetAddress.InnerHtml = jBusiness.Address;
                    City.InnerHtml = jBusiness.City;
                    State.InnerHtml = jBusiness.State;
                    PostalCode.InnerHtml = jBusiness.ZipCode;
                    PhoneNumber.InnerHtml = jBusiness.PhoneNumber;
                    DatePosted.InnerHtml = Strings.ToString(jBusiness.DatePosted);
                    Visits.InnerHtml = Strings.ToString(jBusiness.Visits);
                    if (jBusiness.SiteURL != "http://")
                    {
                        WebSiteURL.Text = jBusiness.SiteURL;
                        WebSiteURL.NavigateUrl = jBusiness.SiteURL;
                    }

                    RatingStars.LookupID = sBusinessID;

                    // Claim a business
                    if (!string.IsNullOrWhiteSpace(jBusiness.UserID))
                    {
                        UserName.InnerHtml = SepFunctions.GetUserInformation("UserName", jBusiness.UserID);
                        ClaimBusiness.Visible = false;
                    }
                    else
                    {
                        ContactMember.Visible = false;
                        if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessClaim") == "Yes" && !string.IsNullOrWhiteSpace(jBusiness.ContactEmail))
                        {
                            ClaimBusiness.Visible = true;
                            ClaimBusiness.InnerHtml = "<a href=\"" + sInstallFolder + "businesses_claim.aspx?BusinessID=" + sBusinessID + "\">" + SepFunctions.LangText("Claim this Business") + "</a><br/>";
                        }
                    }
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Business~~ does not exist.") + "</div>";
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