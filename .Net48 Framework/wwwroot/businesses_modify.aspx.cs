// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="businesses_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class businesses_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class businesses_modify : Page
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
        /// Handles the Clicked event of the IncludeProfile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void IncludeProfile_Clicked(object sender, EventArgs e)
        {
            if (IncludeProfile.Checked) BusinessAddress.Visible = false;
            else BusinessAddress.Visible = true;
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Post a Business");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    BusinessNameLabel.InnerText = SepFunctions.LangText("Business Name:");
                    ContactEmailLabel.InnerText = SepFunctions.LangText("Contact Email:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    FaxNumberLabel.InnerText = SepFunctions.LangText("Fax Number:");
                    SiteURLLabel.InnerText = SepFunctions.LangText("Site URL:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Short Description:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    FullDescriptionLabel.InnerText = SepFunctions.LangText("Full Description:");
                    TwitterLinkLabel.InnerText = SepFunctions.LangText("Twitter Link:");
                    FacebookLinkLabel.InnerText = SepFunctions.LangText("Facebook Link:");
                    LinkedInLinkLabel.InnerText = SepFunctions.LangText("LinkedIn Link:");
                    OfficeHoursLabel.InnerText = SepFunctions.LangText("Office Hours:");
                    IncludeProfile.Text = SepFunctions.LangText("Show your account address on your business listing");
                    IncludeMap.Text = SepFunctions.LangText("Show the Google map to your business address");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    BusinessNameRequired.ErrorMessage = SepFunctions.LangText("~~Business Name~~ is required.");
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

            GlobalVars.ModuleID = 20;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("BusinessPost"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleMapsAPI"))) GoogleMapRow.Visible = false;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "BusinessUserAddress") == "Yes") IncludeProfile.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("BusinessID")))
            {
                var jBusinesses = SepCommon.DAL.Businesses.Business_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("BusinessID")));

                if (jBusinesses.BusinessID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Business~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Business");
                    BusinessID.Value = SepCommon.SepCore.Request.Item("BusinessID");
                    Category.CatID = Strings.ToString(jBusinesses.CatID);
                    BusinessName.Value = jBusinesses.BusinessName;
                    ContactEmail.Value = jBusinesses.ContactEmail;
                    PhoneNumber.Value = jBusinesses.PhoneNumber;
                    FaxNumber.Value = jBusinesses.FaxNumber;
                    SiteURL.Value = jBusinesses.SiteURL;
                    Description.Value = jBusinesses.Description;
                    StreetAddress.Value = jBusinesses.Address;
                    City.Value = jBusinesses.City;
                    Country.Text = jBusinesses.Country;
                    State.Text = jBusinesses.State;
                    PostalCode.Value = jBusinesses.ZipCode;
                    FullDescription.Text = jBusinesses.FullDescription;
                    TwitterLink.Value = jBusinesses.TwitterLink;
                    FacebookLink.Value = jBusinesses.FacebookLink;
                    LinkedInLink.Value = jBusinesses.LinkedInLink;
                    OfficeHours.Value = jBusinesses.OfficeHours;
                    IncludeProfile.Checked = jBusinesses.IncludeProfile;
                    IncludeMap.Checked = jBusinesses.IncludeMap;
                    if (SepFunctions.Setup(20, "BusinessUserAddress") == "Yes")
                    {
                        BusinessAddress.Visible = false;
                    }
                    else
                    {
                        if (jBusinesses.IncludeProfile == false) BusinessAddress.Visible = true;
                    }

                    PostPricing.Visible = false;
                }
            }
            else
            {
                if (SepFunctions.Setup(20, "BusinessUserAddress") == "Yes") BusinessAddress.Visible = false;
                if (Page.IsPostBack)
                {
                    Category.CatID = Request.Form["Category"];
                    State.Text = Request.Form["State"];
                    Country.Text = Request.Form["Country"];
                    FullDescription.Text = FullDescription.Text;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(BusinessID.Value)) BusinessID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    IncludeProfile.Checked = true;
                    IncludeMap.Checked = true;
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostPostBusiness", "GetPostBusiness", BusinessID.Value, true) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    StreetAddress.Value = SepFunctions.GetUserInformation("StreetAddress");
                    City.Value = SepFunctions.GetUserInformation("City");
                    State.Text = SepFunctions.GetUserInformation("State");
                    PostalCode.Value = SepFunctions.GetUserInformation("ZipCode");
                    Country.Text = SepFunctions.GetUserInformation("Country");
                }
            }

            PostPricing.PriceUniqueID = BusinessID.Value;

            if (SepFunctions.Setup(992, "CatLowestLvl") == "Yes") CategoryRequired.ErrorMessage = SepFunctions.LangText("You must select the lowest level of category available.");
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
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var intReturn = SepCommon.DAL.Businesses.Business_Save(SepFunctions.toLong(BusinessID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(Category.CatID), BusinessName.Value, ContactEmail.Value, PhoneNumber.Value, FaxNumber.Value, SiteURL.Value, Description.Value, StreetAddress.Value, City.Value, Request.Form["State"], PostalCode.Value, Request.Form["Country"], FullDescription.Text, TwitterLink.Value, FacebookLink.Value, LinkedInLink.Value, OfficeHours.Value, IncludeProfile.Checked, IncludeMap.Checked, SepFunctions.Get_Portal_ID(), true);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, BusinessName.Value);

            SepFunctions.Pricing_Options_Save(20, BusinessID.Value);

            ModFormDiv.Visible = false;
        }
    }
}