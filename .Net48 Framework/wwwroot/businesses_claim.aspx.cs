// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="businesses_claim.aspx.cs" company="SepCity, Inc.">
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
    /// Class businesses_claim.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class businesses_claim : Page
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
            var sClaimID = Strings.ToString(SepFunctions.GetIdentity());
            if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("BusinessAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "business_claim.aspx?AdID=" + SepCommon.SepCore.Request.Item("BusinessID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("BusinessID")))
            {
                var jBusiness = SepCommon.DAL.Businesses.Business_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("BusinessID")));

                if (jBusiness.BusinessID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Business~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ClaimID")) && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
                    {
                        if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("UserName", SepCommon.SepCore.Request.Item("UserID"))))
                        {
                            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (var cmd = new SqlCommand("UPDATE BusinessListings SET UserID=@UserID WHERE ClaimID=@ClaimID AND BusinessID=@BusinessID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                                    cmd.Parameters.AddWithValue("ClaimID", sClaimID);
                                    cmd.Parameters.AddWithValue("@BusinessID", SepCommon.SepCore.Request.Item("BusinessID"));
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully claimed this business listing.") + "</div>";
                        }
                    }
                    else
                    {
                        string href = SepFunctions.GetSiteDomain() + "businesses_claim.aspx?BusinessID=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("BusinessID")) + "&ClaimID=" + SepFunctions.UrlEncode(sClaimID) + "&UserID=" + SepFunctions.Session_User_ID();
                        string sEmailBody = SepFunctions.LangText("To claim this business listing please click the link below:") + "<br/><br/>";
                        sEmailBody += "<a href=\"" + href + "\" target=\"_blank\">" + href + "</a>";
                        SepFunctions.Send_Email(jBusiness.ContactEmail, SepFunctions.Setup(991, "AdminEmailAddress"), SepFunctions.LangText("Claim a Business Listing"), sEmailBody, GlobalVars.ModuleID);
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE BusinessListings SET ClaimID=@ClaimID WHERE BusinessID=@BusinessID", conn))
                            {
                                cmd.Parameters.AddWithValue("ClaimID", sClaimID);
                                cmd.Parameters.AddWithValue("@BusinessID", SepCommon.SepCore.Request.Item("BusinessID"));
                                cmd.ExecuteNonQuery();
                            }
                        }

                        ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Email has been successfully sent to ~~" + jBusiness.ContactEmail + "~~. Please check your email and click on the link provided in the email to claim this business listing.") + "</div>";
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