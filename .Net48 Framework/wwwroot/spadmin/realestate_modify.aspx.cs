// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="realestate_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class realestate_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class realestate_modify : Page
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
        /// Handles the SelectedIndex event of the ForSale control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void ForSale_SelectedIndex(object sender, EventArgs e)
        {
            if (ForSale.SelectedValue == "0")
            {
                MLSNumberRow.Visible = false;
                CountyRow.Visible = false;
                YearBuiltRow.Visible = false;
                TypeRow.Visible = false;
                SizeMBedroomRow.Visible = false;
                SizeLivingRoomRow.Visible = false;
                SizeDiningRoomRow.Visible = false;
                SizeKitchenRow.Visible = false;
                SizeLotRow.Visible = false;
                SQFeetRow.Visible = false;
                RecurringCycle.Visible = true;
            }
            else
            {
                MLSNumberRow.Visible = true;
                CountyRow.Visible = true;
                YearBuiltRow.Visible = true;
                TypeRow.Visible = true;
                SizeMBedroomRow.Visible = true;
                SizeLivingRoomRow.Visible = true;
                SizeDiningRoomRow.Visible = true;
                SizeKitchenRow.Visible = true;
                SizeLotRow.Visible = true;
                SQFeetRow.Visible = true;
                RecurringCycle.Visible = false;
            }
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
                    ForSale.Items[0].Text = SepFunctions.LangText("For Sale");
                    ForSale.Items[1].Text = SepFunctions.LangText("For Rent");
                    PropertyType.Items[0].Text = SepFunctions.LangText("Apartment");
                    PropertyType.Items[1].Text = SepFunctions.LangText("Condo");
                    PropertyType.Items[2].Text = SepFunctions.LangText("House");
                    PropertyType.Items[3].Text = SepFunctions.LangText("Land/Lot");
                    PropertyType.Items[4].Text = SepFunctions.LangText("Town House");
                    PropertyType.Items[5].Text = SepFunctions.LangText("Commercial Land/Lot");
                    PropertyType.Items[6].Text = SepFunctions.LangText("Commercial Building (Buy)");
                    PropertyType.Items[7].Text = SepFunctions.LangText("Commercial Building (Rent)");
                    PropertyType.Items[8].Text = SepFunctions.LangText("Furnished Rooms (Rent)");
                    Status.Items[0].Text = SepFunctions.LangText("Available");
                    Status.Items[1].Text = SepFunctions.LangText("Not Available");
                    Status.Items[2].Text = SepFunctions.LangText("Available (Show on Site)");
                    Type.Items[0].Text = SepFunctions.LangText("Ready to Move In");
                    Type.Items[1].Text = SepFunctions.LangText("Fixer-Upper");
                    Type.Items[2].Text = SepFunctions.LangText("Furnished");
                    Style.Items[0].Text = SepFunctions.LangText("Beach House");
                    Style.Items[1].Text = SepFunctions.LangText("Bungalow");
                    Style.Items[2].Text = SepFunctions.LangText("Cabin");
                    Style.Items[3].Text = SepFunctions.LangText("Colonial");
                    Style.Items[4].Text = SepFunctions.LangText("Commercial");
                    Style.Items[5].Text = SepFunctions.LangText("Farmhouse");
                    Style.Items[6].Text = SepFunctions.LangText("Multi-Level");
                    Style.Items[7].Text = SepFunctions.LangText("Multi-Unit");
                    Style.Items[8].Text = SepFunctions.LangText("One-Story");
                    Style.Items[9].Text = SepFunctions.LangText("Ranch");
                    Style.Items[10].Text = SepFunctions.LangText("Two-Story");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Property");
                    PropertyTitleLabel.InnerText = SepFunctions.LangText("Title:");
                    PriceLabel.InnerText = SepFunctions.LangText("Price:");
                    MLSNumberLabel.InnerText = SepFunctions.LangText("MLS Number:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    PropertyTypeLabel.InnerText = SepFunctions.LangText("Property Type:");
                    StatusLabel.InnerText = SepFunctions.LangText("Status:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    CountyLabel.InnerText = SepFunctions.LangText("County:");
                    YearBuiltLabel.InnerText = SepFunctions.LangText("Year Built:");
                    NumBedroomsLabel.InnerText = SepFunctions.LangText("# of Bedrooms:");
                    NumBathroomsLabel.InnerText = SepFunctions.LangText("# of Bathrooms:");
                    NumRoomsLabel.InnerText = SepFunctions.LangText("# of Rooms:");
                    SQFeetLabel.InnerText = SepFunctions.LangText("SQ Feet:");
                    TypeLabel.InnerText = SepFunctions.LangText("Type:");
                    StyleLabel.InnerText = SepFunctions.LangText("Style:");
                    SizeMBedroomLabel.InnerText = SepFunctions.LangText("Master Bedroom Size:");
                    SizeLivingRoomLabel.InnerText = SepFunctions.LangText("Living Room Size:");
                    SizeDiningRoomLabel.InnerText = SepFunctions.LangText("Dining Room Size:");
                    SizeKitchenLabel.InnerText = SepFunctions.LangText("Kitchen Size:");
                    SizeLotLabel.InnerText = SepFunctions.LangText("Land/Lot Size:");
                    GarageLabel.InnerText = SepFunctions.LangText("Garage:");
                    HeatingLabel.InnerText = SepFunctions.LangText("Heating:");
                    FeatureInteriorLabel.InnerText = SepFunctions.LangText("Interior Features:");
                    FeatureExteriorLabel.InnerText = SepFunctions.LangText("Exterior Features:");
                    TitleRequired.ErrorMessage = SepFunctions.LangText("~~Title~~ is required.");
                    StreetAddressRequired.ErrorMessage = SepFunctions.LangText("~~Street Address~~ is required.");
                    CityRequired.ErrorMessage = SepFunctions.LangText("~~City~~ is required.");
                    PostalCodeRequired.ErrorMessage = SepFunctions.LangText("~~Zip/Postal Code~~ is required.");
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

            GlobalVars.ModuleID = 32;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("RStateAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("RStateAdmin"), true) == false)
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

            if (SepFunctions.Setup(32, "RStateStateDrop") == "No") StateRow.Visible = false;

            if (SepFunctions.Setup(32, "RStateCountryDrop") == "No") CountryRow.Visible = false;

            RecurringCycle.Visible = false;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PropertyID")))
            {
                var jProperties = SepCommon.DAL.RealEstate.Property_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PropertyID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jProperties.PropertyID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Property~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Property");
                    PropertyID.Value = SepCommon.SepCore.Request.Item("PropertyID");
                    Pictures.ContentID = SepCommon.SepCore.Request.Item("PropertyID");
                    Pictures.UserID = jProperties.UserID;
                    PropertyTitle.Value = jProperties.Title;
                    Price.Value = jProperties.Price;
                    RecurringCycle.Value = jProperties.RecurringCycle;
                    if (jProperties.ForSale) ForSale.SelectedValue = "1";
                    else ForSale.SelectedValue = "0";
                    MLSNumber.Value = jProperties.MLSNumber;
                    Description.Text = jProperties.Description;
                    PropertyType.Value = Strings.ToString(jProperties.PropertyType);
                    Status.Value = Strings.ToString(jProperties.Status);
                    Country.Text = jProperties.Country;
                    StreetAddress.Value = jProperties.StreetAddress;
                    City.Value = jProperties.City;
                    State.Text = jProperties.State;
                    PostalCode.Value = jProperties.PostalCode;
                    County.Value = jProperties.County;
                    YearBuilt.Value = jProperties.YearBuilt;
                    NumBedrooms.Value = Strings.ToString(jProperties.NumBedrooms);
                    NumBathrooms.Value = Strings.ToString(jProperties.NumBathrooms);
                    NumRooms.Value = Strings.ToString(jProperties.NumRooms);
                    SQFeet.Value = jProperties.SQFeet;
                    Type.Value = Strings.ToString(jProperties.Type);
                    Style.Value = Strings.ToString(jProperties.Style);
                    SizeMBedroom.Value = jProperties.SizeMBedroom;
                    SizeLivingRoom.Value = jProperties.SizeLivingRoom;
                    SizeDiningRoom.Value = jProperties.SizeDiningRoom;
                    SizeKitchen.Value = jProperties.SizeKitchen;
                    SizeLot.Value = jProperties.SizeLot;
                    Garage.Value = jProperties.Garage;
                    Heating.Value = jProperties.Heating;
                    FeatureInterior.Text = jProperties.FeatureInterior;
                    FeatureExterior.Text = jProperties.FeatureExterior;

                    sUserID = jProperties.UserID;

                    if (ForSale.SelectedValue == "0")
                    {
                        MLSNumberRow.Visible = false;
                        CountyRow.Visible = false;
                        YearBuiltRow.Visible = false;
                        TypeRow.Visible = false;
                        SizeMBedroomRow.Visible = false;
                        SizeLivingRoomRow.Visible = false;
                        SizeDiningRoomRow.Visible = false;
                        SizeKitchenRow.Visible = false;
                        SizeLotRow.Visible = false;
                        SQFeetRow.Visible = false;
                        RecurringCycle.Visible = true;
                    }

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("PropertyID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(PropertyID.Value)) PropertyID.Value = Strings.ToString(SepFunctions.GetIdentity());

                if (!Page.IsPostBack)
                {
                    Pictures.ContentID = PropertyID.Value;
                    Pictures.UserID = SepFunctions.Session_User_ID();
                    if (ForSale.SelectedValue == "0")
                    {
                        MLSNumberRow.Visible = false;
                        CountyRow.Visible = false;
                        YearBuiltRow.Visible = false;
                        TypeRow.Visible = false;
                        SizeMBedroomRow.Visible = false;
                        SizeLivingRoomRow.Visible = false;
                        SizeDiningRoomRow.Visible = false;
                        SizeKitchenRow.Visible = false;
                        SizeLotRow.Visible = false;
                        SQFeetRow.Visible = false;
                        RecurringCycle.Visible = true;
                    }
                }
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

            var intReturn = SepCommon.DAL.RealEstate.Property_Save(SepFunctions.toLong(PropertyID.Value), SepFunctions.Session_User_ID(), SepFunctions.toLong(AgentID.Value), PropertyTitle.Value, Description.Text, Price.Value, ForSale.SelectedValue, MLSNumber.Value, PropertyType.Value, Status.Value, StreetAddress.Value, City.Value, State.Text, PostalCode.Value, County.Value, Country.Text, YearBuilt.Value, NumBedrooms.Value, NumBathrooms.Value, NumRooms.Value, SQFeet.Value, Type.Value, Style.Value, SizeMBedroom.Value, SizeLivingRoom.Value, SizeDiningRoom.Value, SizeKitchen.Value, SizeLot.Value, Garage.Value, Heating.Value, FeatureInterior.Text, FeatureExterior.Text, SepFunctions.Get_Portal_ID(), RecurringCycle.Value);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}