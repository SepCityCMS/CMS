// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="vouchers_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class vouchers_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class vouchers_modify : Page
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
                    ApproveEmailID.Items[0].Text = SepFunctions.LangText("None");
                    BuyEmailID.Items[0].Text = SepFunctions.LangText("None");
                    AdminEmailID.Items[0].Text = SepFunctions.LangText("None");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Voucher");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below where to list your item:");
                    UsernameLabel.InnerText = SepFunctions.LangText("User Name:");
                    BuyTitleLabel.InnerText = SepFunctions.LangText("Buy Title:");
                    ShortDescriptionLabel.InnerText = SepFunctions.LangText("Short Description (Ex. Today Only! Get 65% savings on ice cream!):");
                    LongDescriptionLabel.InnerText = SepFunctions.LangText("Long Description (Describe options available to the customer under this deal):");
                    LogoImageLabel.InnerText = SepFunctions.LangText("Logo Image (Upload one logo or image descriptive of your deal or your business. File must be no larger than 250px x 250px and must be optimized for the web in .png or .jpg format):");
                    BarCodeUploadLabel.InnerText = SepFunctions.LangText("Bar Code Image (If you have a graphic bar code that you want to use to scan the voucher at your point of sale, you can upload it here. It is not required and we track all purchased vouchers with a special random code to help prevent fraud. However, it is your responsibility to reconcile your vouchers to be sure a specific voucher has not already been redeemed.):");
                    SalePriceLabel.InnerText = SepFunctions.LangText("Sale Price:");
                    RegularPriceLabel.InnerText = SepFunctions.LangText("Original Value:");
                    SavingsLabel.InnerText = SepFunctions.LangText("Savings (Savings are automatically calculated based on the values you have entered in the fields above):");
                    QuantityLabel.InnerText = SepFunctions.LangText("Max Quantity Available (This is the maximum number of vouchers you want to make available for sale at this price. EX. 500):");
                    MaxNumPerUserLabel.InnerText = SepFunctions.LangText("Max Quantity Per User (This field is for entering the maximum quantity of vouchers a single person may purchase under this deal. Example: you may want to limit a single person to purchase 3 vouchers maximum so that there are enough to go around for others to purchase.):");
                    RedemptionStartLabel.InnerText = SepFunctions.LangText("Redemption Start Date (Redemption dates are the dates that the customer can redeem a voucher at your business or a discount code at your website):");
                    RedemptionEndLabel.InnerText = SepFunctions.LangText("Redemption End Date (Redemption dates are the dates that the customer can redeem a voucher at your business or a discount code at your website):");
                    DaysToExpireLabel.InnerText = SepFunctions.LangText("Days to Run:");
                    PurchaseCodeLabel.InnerText = SepFunctions.LangText("Purchase Code:");
                    BusinessNameLabel.InnerText = SepFunctions.LangText("Business Name:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    ContactEmailLabel.InnerText = SepFunctions.LangText("Contact Email:");
                    ContactNameLabel.InnerText = SepFunctions.LangText("ContactName:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    DisclaimerLabel.InnerText = SepFunctions.LangText("Disclaimer:");
                    FinePrintLabel.InnerText = SepFunctions.LangText("Fine Print:");
                    ApproveEmailIDLabel.InnerText = SepFunctions.LangText("Auto Responder to Merchant:");
                    BuyEmailIDLabel.InnerText = SepFunctions.LangText("Auto Responder to Purchaser:");
                    AdminEmailIDLabel.InnerText = SepFunctions.LangText("Auto Responder to Admin When Purchase is Made:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    UsernameRequired.ErrorMessage = SepFunctions.LangText("~~User Name~~ is required.");
                    BuyTitleRequired.ErrorMessage = SepFunctions.LangText("~~Buy Title~~ is required.");
                    ShortDescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Short Description~~ is required.");
                    LongDescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Long Description~~ is required.");
                    SalePriceRequired.ErrorMessage = SepFunctions.LangText("~~Sale Price~~ is required.");
                    RegularPriceRequired.ErrorMessage = SepFunctions.LangText("~~Original Value~~ is required.");
                    QuantityRequired.ErrorMessage = SepFunctions.LangText("~~Quantity~~ is required.");
                    MaxNumPerUserRequired.ErrorMessage = SepFunctions.LangText("~~Max Quantity Per User~~ is required.");
                    DaysToExpireRequired.ErrorMessage = SepFunctions.LangText("~~Days to Run~~ is required.");
                    PurchaseCodeRequired.ErrorMessage = SepFunctions.LangText("~~Purchase Code~~ is required.");
                    BusinessNameRequired.ErrorMessage = SepFunctions.LangText("~~Business Name~~ is required.");
                    DisclaimerRequired.ErrorMessage = SepFunctions.LangText("~~Disclaimer~~ is required.");
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
            TranslatePage();

            GlobalVars.ModuleID = 65;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("VoucherAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("VoucherAdmin"), true) == false)
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

            if (!Page.IsPostBack)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT TemplateID,TemplateName FROM EmailTemplates ORDER BY EmailSubject", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                while (RS.Read())
                                {
                                    ApproveEmailID.Items.Add(new ListItem(SepFunctions.openNull(RS["TemplateName"]), SepFunctions.openNull(RS["TemplateID"])));
                                    BuyEmailID.Items.Add(new ListItem(SepFunctions.openNull(RS["TemplateName"]), SepFunctions.openNull(RS["TemplateID"])));
                                    AdminEmailID.Items.Add(new ListItem(SepFunctions.openNull(RS["TemplateName"]), SepFunctions.openNull(RS["TemplateID"])));
                                }

                                RS.Read();
                            }
                            else
                            {
                                ApproveEmailIDRow.Visible = false;
                                BuyEmailIDRow.Visible = false;
                                AdminEmailIDRow.Visible = false;
                            }

                        }
                    }
                }

                Username.Value = SepFunctions.Session_User_Name();
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("VoucherID")))
            {
                var jVouchers = SepCommon.DAL.Vouchers.Voucher_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("VoucherID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jVouchers.VoucherID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Voucher~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Voucher");
                    VoucherID.Value = SepCommon.SepCore.Request.Item("VoucherID");
                    Category.CatID = Strings.ToString(jVouchers.CatID);
                    BuyTitle.Value = jVouchers.BuyTitle;
                    ShortDescription.Value = jVouchers.ShortDescription;
                    LongDescription.Value = jVouchers.LongDescription;
                    SalePrice.Value = Strings.ToString(jVouchers.SalePrice);
                    RegularPrice.Value = Strings.ToString(jVouchers.RegularPrice);
                    Savings.Value = jVouchers.SavePrice;
                    MaxNumPerUser.Value = Strings.ToString(jVouchers.MaxNumPerUser);
                    RedemptionStart.Value = Strings.ToString(jVouchers.RedemptionStart);
                    RedemptionEnd.Value = Strings.ToString(jVouchers.RedemptionEnd);
                    BusinessName.Value = jVouchers.BusinessName;
                    StreetAddress.Value = jVouchers.Address;
                    City.Value = jVouchers.City;
                    State.Text = jVouchers.State;
                    Country.Text = jVouchers.Country;
                    PostalCode.Value = jVouchers.ZipCode;
                    ContactEmail.Value = jVouchers.ContactEmail;
                    ContactName.Value = jVouchers.ContactName;
                    PhoneNumber.Value = jVouchers.PhoneNumber;
                    Disclaimer.Value = jVouchers.Disclaimer;
                    FinePrint.Value = jVouchers.FinePrint;
                    ApproveEmailID.Value = Strings.ToString(jVouchers.ApproveEmailID);
                    BuyEmailID.Value = Strings.ToString(jVouchers.BuyEmailID);
                    AdminEmailID.Value = Strings.ToString(jVouchers.AdminEmailID);
                    DaysToExpire.Value = jVouchers.BuyEndDate.ToShortDateString();
                    PurchaseCode.Value = jVouchers.PurchaseCode;
                    Quantity.Value = Strings.ToString(jVouchers.Quantity);
                    LogoImageUpload.ContentID = SepCommon.SepCore.Request.Item("VoucherID");
                    BarCodeUpload.ContentID = SepCommon.SepCore.Request.Item("VoucherID");

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("VoucherID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(VoucherID.Value)) VoucherID.Value = Strings.ToString(SepFunctions.GetIdentity());
                LogoImageUpload.ContentID = VoucherID.Value;
                BarCodeUpload.ContentID = VoucherID.Value;
                if (!Page.IsPostBack)
                {
                    RedemptionStart.Value = Strings.FormatDateTime(DateTime.Now, Strings.DateNamedFormat.GeneralDate);
                    RedemptionEnd.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, DateTime.Now), Strings.DateNamedFormat.GeneralDate);
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
            var intReturn = SepCommon.DAL.Vouchers.Voucher_Save(SepFunctions.toLong(VoucherID.Value), SepFunctions.toLong(Category.CatID), BuyTitle.Value, ShortDescription.Value, LongDescription.Value, SepFunctions.toDecimal(SalePrice.Value), SepFunctions.toDecimal(RegularPrice.Value), SepFunctions.toLong(Quantity.Value), SepFunctions.toLong(MaxNumPerUser.Value), SepFunctions.toDate(RedemptionStart.Value), SepFunctions.toDate(RedemptionEnd.Value), PurchaseCode.Value, BusinessName.Value, StreetAddress.Value, City.Value, State.Text, PostalCode.Value, Country.Text, ContactEmail.Value, ContactName.Value, PhoneNumber.Value, Disclaimer.Value, SepFunctions.Get_Portal_ID(), SepFunctions.GetUserID(Username.Value), 0, DateAndTime.DateAdd(DateAndTime.DateInterval.Day, 30, DateTime.Now), FinePrint.Value, SepFunctions.toLong(BuyEmailID.Value), SepFunctions.toLong(ApproveEmailID.Value), SepFunctions.toLong(AdminEmailID.Value), string.Empty, string.Empty);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}