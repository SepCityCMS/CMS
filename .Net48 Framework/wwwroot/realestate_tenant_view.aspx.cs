// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="realestate_tenant_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
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
    /// Class realestate_tenant_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class realestate_tenant_view : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

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

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TenantID")))
                {
                    var sTenantID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("TenantID"));
                    var sPropertyID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("PropertyID"));

                    var jTenants = SepCommon.DAL.RealEstate.Tenant_Get(sTenantID, sPropertyID);

                    if (jTenants.TenantID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Tenant~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                    else
                    {
                        Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", "Tenant: " + jTenants.TenantName);
                        RatingStars.InnerHtml = SepFunctions.Rating_Stars_Display(jTenants.AverageRating);

                        if (!string.IsNullOrWhiteSpace(jTenants.DefaultPicture)) Photo.InnerHtml = "<img src=\"" + jTenants.DefaultPicture + "\" border=\"0\" /><br/>";
                        TenantName.InnerText = jTenants.TenantName;
                        MovedIn.InnerHtml = Strings.FormatDateTime(jTenants.MovedIn, Strings.DateNamedFormat.ShortDate);
                        MovedOut.InnerHtml = Strings.FormatDateTime(jTenants.MovedOut, Strings.DateNamedFormat.ShortDate);
                        TenantNumber.InnerHtml = jTenants.TenantNumber;
                        BirthDate.InnerHtml = Strings.FormatDateTime(jTenants.BirthDate, Strings.DateNamedFormat.ShortDate);

                        dv = BindData(sPropertyID, sTenantID);
                        ReviewList.DataSource = dv;
                        ReviewList.DataBind();

                        dv = BindDataAttachments(sTenantID);
                        AttachmentList.DataSource = dv;
                        AttachmentList.DataBind();
                    }
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Property~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
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
        /// Handles the ItemDataBound event of the ReviewList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs" /> instance containing the event data.</param>
        protected void ReviewList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lUserRating = (Literal)e.Item.FindControl("UserRating");
            lUserRating.Text = SepFunctions.Rating_Stars_Display(SepFunctions.toDouble(lUserRating.Text.Split('|')[0])) + "<span style=\"padding-left:25px;\">Reviewed " + SepFunctions.TimeAgo(SepFunctions.toDate(lUserRating.Text.Split('|')[1])) + "</span>";
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="PropertyID">The property identifier.</param>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <returns>DataView.</returns>
        private DataView BindData(long PropertyID, long TenantID)
        {
            var dRealEstateReviews = SepCommon.DAL.RealEstate.GetRealEstateReviews(PropertyID: PropertyID, TenantID: TenantID);

            dv = new DataView(SepFunctions.ListToDataTable(dRealEstateReviews));
            return dv;
        }

        /// <summary>
        /// Binds the data attachments.
        /// </summary>
        /// <param name="TenantID">The tenant identifier.</param>
        /// <returns>DataView.</returns>
        private DataView BindDataAttachments(long TenantID)
        {
            var dRealEstateAttachments = SepCommon.DAL.RealEstate.GetRealEstateAttachments(TenantID: TenantID);

            dv = new DataView(SepFunctions.ListToDataTable(dRealEstateAttachments));
            return dv;
        }
    }
}