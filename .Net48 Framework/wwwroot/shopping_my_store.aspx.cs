// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopping_my_store.aspx.cs" company="SepCity, Inc.">
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
    /// Class shopping_my_store.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopping_my_store : Page
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
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("My Store");
                    StoreNameLabel.InnerText = SepFunctions.LangText("Store Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    CompanyNameLabel.InnerText = SepFunctions.LangText("Company Name:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    FaxNumberLabel.InnerText = SepFunctions.LangText("Fax Number:");
                    SiteURLLabel.InnerText = SepFunctions.LangText("Site URL:");
                    ContactEmailLabel.InnerText = SepFunctions.LangText("Contact Email:");
                    StoreNameRequired.ErrorMessage = SepFunctions.LangText("~~Store Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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

            var sStoreID = SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID());

            if (!Page.IsPostBack && Information.IsNumeric(sStoreID))
            {
                var jStored = SepCommon.DAL.ShoppingMall.Store_Get(sStoreID);

                if (jStored.StoreID > 0)
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Store");
                    StoreID.Value = Strings.ToString(jStored.StoreID);
                    StoreName.Value = jStored.StoreName;
                    Description.Value = jStored.Description;
                    CompanyName.Value = jStored.CompanyName;
                    ContactEmail.Value = jStored.ContactEmail;
                    PhoneNumber.Value = jStored.PhoneNumber;
                    FaxNumber.Value = jStored.FaxNumber;
                    SiteURL.Value = jStored.SiteURL;
                    StreetAddress.Value = jStored.StreetAddress;
                    City.Value = jStored.City;
                    Country.Text = jStored.Country;
                    State.Text = jStored.State;
                    PostalCode.Value = jStored.PostalCode;
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    State.Text = Request.Form["State"];
                    Country.Text = Request.Form["Country"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(StoreID.Value)) StoreID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    StreetAddress.Value = SepFunctions.GetUserInformation("StreetAddress");
                    City.Value = SepFunctions.GetUserInformation("City");
                    State.Text = SepFunctions.GetUserInformation("State");
                    PostalCode.Value = SepFunctions.GetUserInformation("ZipCode");
                    Country.Text = SepFunctions.GetUserInformation("Country");
                    ContactEmail.Value = SepFunctions.GetUserInformation("EmailAddress");
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
            var sReturn = SepCommon.DAL.ShoppingMall.Store_Save(SepFunctions.toLong(StoreID.Value), SepFunctions.Session_User_ID(), StoreName.Value, Description.Value, CompanyName.Value, StreetAddress.Value, City.Value, State.Text, Country.Text, PostalCode.Value, PhoneNumber.Value, FaxNumber.Value, SiteURL.Value, ContactEmail.Value, SepFunctions.Get_Portal_ID());

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}