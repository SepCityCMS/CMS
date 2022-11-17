// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="account_edit.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.DAL;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class account_edit.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class account_edit : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
        }

        /// <summary>
        /// Saves the facebook token.
        /// </summary>
        public void Save_Facebook_Token()
        {
            var sResult = SepCommon.DAL.Members.Facebook_Token_Save(SepFunctions.Session_User_ID(), SepFunctions.Session_User_Name(), SepFunctions.Session_Password(), Facebook_Token.Value, Facebook_Id.Value, Facebook_User.Value);

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sResult + "</div>";
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
                    Gender.Items[0].Text = SepFunctions.LangText("Male");
                    Gender.Items[1].Text = SepFunctions.LangText("Female");
                    Friends.Items[0].Text = SepFunctions.LangText("Yes");
                    Friends.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Edit Account");
                    FacebookLoginLabel.InnerText = SepFunctions.LangText("Login below with facebook to associate your account with your FaceBook Account:");
                    PasswordLabel.InnerText = SepFunctions.LangText("Enter a Password:");
                    RePasswordLabel.InnerText = SepFunctions.LangText("Re-enter a Password:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    FirstNameLabel.InnerText = SepFunctions.LangText("First Name:");
                    LastNameLabel.InnerText = SepFunctions.LangText("Last Name:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    GenderLabel.InnerText = SepFunctions.LangText("Gender:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    FriendsLabel.InnerText = SepFunctions.LangText("Friends:");
                    PayPalLabel.InnerText = SepFunctions.LangText("PayPal Email Address (In case you intent buyers to pay you with credit card):");
                    NewslettersLabel.InnerText = SepFunctions.LangText("Select the Newsletters you wish to join:");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
                    FirstNameRequired.ErrorMessage = SepFunctions.LangText("~~First Name~~ is required.");
                    LastNameRequired.ErrorMessage = SepFunctions.LangText("~~Last Name~~ is required.");
                    StreetAddressRequired.ErrorMessage = SepFunctions.LangText("~~Street Address~~ is required.");
                    CityRequired.ErrorMessage = SepFunctions.LangText("~~City~~ is required.");
                    PostalCodeRequired.ErrorMessage = SepFunctions.LangText("~~Zip/Postal Code~~ is required.");
                    PhoneNumberRequired.ErrorMessage = SepFunctions.LangText("~~Phone Number~~ is required.");
                    PayPalRequired.ErrorMessage = SepFunctions.LangText("~~PayPal Email Address~~ is required.");
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

            GlobalVars.ModuleID = 33;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                SepFunctions.RequireLogin("|2|");

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.ModuleActivated(68))
            {
                PageText.InnerHtml = SepFunctions.SendGenericError(404);
                SignupFormDiv.Visible = false;
                return;
            }

            if (IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Facebook_Token.Value))
                {
                    Save_Facebook_Token();
                    var jMembers = SepCommon.DAL.Members.Member_Get(SepFunctions.Session_User_ID());

                    if (string.IsNullOrWhiteSpace(jMembers.UserID))
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Member~~ does not exist.") + "</div>";
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(jMembers.Facebook_Id)) FacebookRow.InnerHtml = "<a href=\"" + sInstallFolder + "account_edit.aspx?DoAction=RemoveFacebook\">" + SepFunctions.LangText("Remove FaceBook association from my account.") + "</a>";
                    }
                }
            }
            else
            {
                if (SepCommon.SepCore.Request.Item("DoAction") == "RemoveFacebook")
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("UPDATE Members SET Facebook_Token='',Facebook_Id='',Facebook_User='' WHERE UserID=@UserID AND Status = '1'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "FacebookAPIKey")))
                {
                    FacebookRow.Visible = false;
                    Facebook_Token.Visible = false;
                    Facebook_Id.Visible = false;
                    Facebook_User.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskCountry") != "Yes") CountryRow.Visible = false;
                if (SepFunctions.Setup(994, "AskStreetAddress") != "Yes") StreetAddressRow.Visible = false;
                if (SepFunctions.Setup(994, "AskCity") != "Yes") CityRow.Visible = false;
                if (SepFunctions.Setup(994, "AskState") != "Yes") StateRow.Visible = false;
                if (SepFunctions.Setup(994, "AskZipCode") != "Yes") PostalCodeRow.Visible = false;
                if (SepFunctions.Setup(994, "AskGender") != "Yes") GenderRow.Visible = false;
                if (SepFunctions.Setup(994, "AskPhoneNumber") != "Yes") PhoneNumberRow.Visible = false;
                if (SepFunctions.Setup(33, "FriendsEnable") != "Yes" || SepFunctions.Setup(994, "AskFriends") != "Yes") FriendsRow.Visible = false;
                if (SepFunctions.Setup(994, "AskPayPal") != "Yes") PayPalRow.Visible = false;

                var jMembers = SepCommon.DAL.Members.Member_Get(SepFunctions.Session_User_ID());

                if (string.IsNullOrWhiteSpace(jMembers.UserID))
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Member~~ does not exist.") + "</div>";
                }
                else
                {
                    if (SepFunctions.Setup(33, "ShowPoints") == "Yes") UserPoints.InnerHtml = SepFunctions.LangText("You currently have ~~" + jMembers.UserPoints + "~~ points remaining on your account.") + "<br/>";
                    FirstName.Value = jMembers.FirstName;
                    LastName.Value = jMembers.LastName;
                    StreetAddress.Value = jMembers.StreetAddress;
                    City.Value = jMembers.City;
                    State.Text = jMembers.State;
                    PostalCode.Value = jMembers.PostalCode;
                    Country.Text = jMembers.Country;
                    EmailAddress.Value = jMembers.EmailAddress;
                    PhoneNumber.Value = jMembers.PhoneNumber;
                    PayPalEmail.Value = jMembers.PayPal;
                    BirthDate.Value = Strings.FormatDateTime(jMembers.BirthDate, Strings.DateNamedFormat.ShortDate);
                    Gender.Value = jMembers.Gender;
                    if (!string.IsNullOrWhiteSpace(jMembers.Facebook_Id)) FacebookRow.InnerHtml = "<a href=\"" + sInstallFolder + "account_edit.aspx?DoAction=RemoveFacebook\">" + SepFunctions.LangText("Remove FaceBook association from my account.") + "</a>";
                }

                var sLetters = SepFunctions.Show_Newsletters(SepFunctions.Session_User_ID());
                if (string.IsNullOrWhiteSpace(sLetters))
                {
                    NewslettersRow.Visible = false;
                }
                else
                {
                    Newsletters.InnerHtml = sLetters;
                    NewslettersRow.Visible = true;
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
            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            var sUserID = SepFunctions.Session_User_ID();

            SepCommon.DAL.Members.Member_Save(sUserID, SepFunctions.GetUserInformation("UserName"), !string.IsNullOrWhiteSpace(Password.Value) ? Password.Value : string.Empty, string.Empty, string.Empty, FirstName.Value, LastName.Value, StreetAddress.Value, City.Value, State.Text, PostalCode.Value, Country.Text, EmailAddress.Value, PhoneNumber.Value, PayPalEmail.Value, SepFunctions.toLong(SepFunctions.GetUserInformation("UserPoints")), SepFunctions.toLong(SepFunctions.GetUserInformation("AccessClass")), SepFunctions.GetUserInformation("AccessKeys"), SepFunctions.toDate(SepFunctions.GetUserInformation("BirthDate")), SepFunctions.toInt(Gender.Value), SepFunctions.GetUserInformation("ReferralID"), SepFunctions.GetUserInformation("WebsiteURL"), SepFunctions.Get_Portal_ID(), SepFunctions.GetUserInformation("ApproveFriends"), Request.Form["LetterIDs"], 1, SepFunctions.GetUserInformation("Facebook_Token"), SepFunctions.GetUserInformation("Facebook_Id"), SepFunctions.GetUserInformation("Facebook_User"), false, SepFunctions.toLong(SepFunctions.GetUserInformation("SiteID")), string.Empty);

            // Save Custom Fields
            var customXML = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FieldID FROM CustomFields WHERE ModuleIDs LIKE '%|29|%' AND (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR datalength(PortalIDs) = 0)", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read()) customXML += "<Custom" + SepFunctions.openNull(RS["FieldID"]) + ">" + SepFunctions.HTMLEncode(SepCommon.SepCore.Request.Item("Custom" + SepFunctions.openNull(RS["FieldID"]))) + "</Custom" + SepFunctions.openNull(RS["FieldID"]) + ">";
                    }
                }
            }

            CustomFields.Answers_Save(sUserID, 29, 29, 0, customXML);

            // End Custom Fields
            Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Updated", SepFunctions.Session_User_Name());

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your account has been successfully saved.") + "</div>";

            var sLetters = SepFunctions.Show_Newsletters(SepFunctions.Session_User_ID());
            if (string.IsNullOrWhiteSpace(sLetters))
            {
                NewslettersRow.Visible = false;
            }
            else
            {
                Newsletters.InnerHtml = sLetters;
                NewslettersRow.Visible = true;
            }
        }
    }
}