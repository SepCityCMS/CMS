// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="realestate_review_post.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class realestate_review_post.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class realestate_review_post : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 32;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "RStateEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("RStateTenants"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack)
            {
                TenantID.Value = SepCommon.SepCore.Request.Item("TenantID");
                Rating.LookupID = TenantID.Value;
                var dRealEstateReviews = SepCommon.DAL.RealEstate.Review_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PropertyID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("TenantID")));
                if (dRealEstateReviews.ReviewID > 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have already reviewed this tenant") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    var dRealEstateProperties = SepCommon.DAL.RealEstate.GetRealEstateProperties(UserID: SepFunctions.Session_User_ID(), ForSale: "0");
                    for (var i = 0; i <= dRealEstateProperties.Count - 1; i++) PropertyID.Items.Add(new ListItem(dRealEstateProperties[i].Title, Strings.ToString(dRealEstateProperties[i].PropertyID)));

                    Rating.disableAJAX = true;
                }
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sReturn = SepCommon.DAL.RealEstate.Review_Save(SepFunctions.toLong(TenantID.Value), SepFunctions.toLong(PropertyID.Value), SepFunctions.toInt(Rating.Text), Complaints.Value, Compliments.Value);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, Globals.GoogleAnalyticsActType(3), Rating.Text);
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
            }

            ModFormDiv.Visible = false;
        }
    }
}