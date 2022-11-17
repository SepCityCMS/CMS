// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="realestate.aspx.cs" company="SepCity, Inc.">
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
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class realestate1.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class realestate1 : Page
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
        /// Formats the ISAPI.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public string Format_ISAPI(object sText)
        {
            return SepFunctions.Format_ISAPI(Strings.ToString(sText));
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
            ManageGridView.PageIndex = e.NewPageIndex;
            ManageGridView.DataSource = BindData();
            ManageGridView.DataBind();
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
            SepFunctions.RequireLogin(SepFunctions.Security("RStateAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.GetUserCountry() == "us") MilesText.InnerHtml = "miles";
            else MilesText.InnerHtml = "kilometers";

            if (SepFunctions.Setup(GlobalVars.ModuleID, "SearchRadius") == "No") RadiusSearching.Visible = false;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "SearchCountry") == "No")
            {
                SearchCountry1.Visible = false;
                SearchCountry2.Visible = false;
            }

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            if (!Page.IsPostBack)
            {
                var sUserId = string.Empty;
                if (SepFunctions.isUserPage() && SepFunctions.Setup(7, "UPagesTop10") == "Yes")
                    sUserId = SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"));

                if (SepFunctions.Setup(GlobalVars.ModuleID, "RStateDisplayNewest") == "Yes")
                {
                    NewestProperties.Visible = true;
                    var cDownloads = SepCommon.DAL.RealEstate.GetRealEstateProperties(UserID: sUserId);
                    NewestProperties.DataSource = cDownloads.Take(10);
                    NewestProperties.DataBind();
                }

                if (SepFunctions.Setup(GlobalVars.ModuleID, "RStateNewestRent") == "Yes")
                {
                    NewestPropertiesRent.Visible = true;
                    var cDownloads = SepCommon.DAL.RealEstate.GetRealEstateProperties(UserID: sUserId, ForSale: "0");
                    NewestPropertiesRent.DataSource = cDownloads.Take(10);
                    NewestPropertiesRent.DataBind();
                }

                if (SepFunctions.Setup(GlobalVars.ModuleID, "RStateNewestSale") == "Yes")
                {
                    NewestPropertiesSale.Visible = true;
                    var cDownloads = SepCommon.DAL.RealEstate.GetRealEstateProperties(UserID: sUserId, ForSale: "1");
                    NewestPropertiesSale.DataSource = cDownloads.Take(10);
                    NewestPropertiesSale.DataBind();
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
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            NewestProperties.Visible = false;
            NewestPropertiesSale.Visible = false;
            NewestPropertiesRent.Visible = false;
            SearchForm.Visible = false;
            PageText.InnerHtml = "<h1>" + SepFunctions.LangText("Search Results") + "</h1>";

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Sorry, no results found in our database.") + "</div>";
                ManageGridView.Visible = false;
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dRealEstateProperties = SepCommon.DAL.RealEstate.GetRealEstateProperties(StartPrice: StartPrice.Value, EndPrice: EndPrice.Value, PropertyTypes: SepCommon.SepCore.Request.Item("PropertyType"), BedRooms: SepFunctions.toDouble(Beds.Value), BathRooms: SepFunctions.toDouble(Baths.Value), Distance: Distance.Value, PostalCode: PostalCode.Value, State: State.Text, Country: Country.Text);

            dv = new DataView(SepFunctions.ListToDataTable(dRealEstateProperties));
            return dv;
        }
    }
}