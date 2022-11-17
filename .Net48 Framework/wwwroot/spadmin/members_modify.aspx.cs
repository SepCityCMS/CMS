// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="members_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class members_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class members_modify : Page
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
                    Gender.Items[0].Text = SepFunctions.LangText("Male");
                    Gender.Items[1].Text = SepFunctions.LangText("Female");
                    Friends.Items[0].Text = SepFunctions.LangText("Yes");
                    Friends.Items[1].Text = SepFunctions.LangText("No");
                    Status.Items[0].Text = SepFunctions.LangText("Active");
                    Status.Items[1].Text = SepFunctions.LangText("Not Active");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Member");
                    UserNameLabel.InnerText = SepFunctions.LangText("User Name:");
                    PasswordLabel.InnerText = SepFunctions.LangText("Enter a Password:");
                    RePasswordLabel.InnerText = SepFunctions.LangText("Re-enter a Password:");
                    FirstNameLabel.InnerText = SepFunctions.LangText("First Name:");
                    LastNameLabel.InnerText = SepFunctions.LangText("Last Name:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City / Town:");
                    StateLabel.InnerText = SepFunctions.LangText("State / Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    PayPalEmailLabel.InnerText = SepFunctions.LangText("PayPal Email Address (In case you intent buyers to pay you with credit card):");
                    PointsLabel.InnerText = SepFunctions.LangText("Credits:");
                    AccessClassLabel.InnerText = SepFunctions.LangText("Access Class:");
                    AccessKeysLabel.InnerText = SepFunctions.LangText("Access Keys:");
                    BirthDateLabel.InnerText = SepFunctions.LangText("Birth Date:");
                    GenderLabel.InnerText = SepFunctions.LangText("Gender:");
                    ReferralLabel.InnerText = SepFunctions.LangText("Referral (User Name):");
                    WebsiteURLLabel.InnerText = SepFunctions.LangText("Website URL:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    FriendsLabel.InnerText = SepFunctions.LangText("Require authorization before others can add this user to their friend list:");
                    NewslettersLabel.InnerText = SepFunctions.LangText("Select the Newsletters you wish to join:");
                    StatusLabel.InnerText = SepFunctions.LangText("User Status:");
                    UserNameRequired.ErrorMessage = SepFunctions.LangText("~~User Name~~ is required.");
                    RePasswordRequired.ErrorMessage = SepFunctions.LangText("~~Password~~ is required.");
                    FirstNameRequired.ErrorMessage = SepFunctions.LangText("~~First Name~~ is required.");
                    LastNameRequired.ErrorMessage = SepFunctions.LangText("~~Last Name~~ is required.");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminUserMan")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminUserMan"), false) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
            {
                var jMembers = SepCommon.DAL.Members.Member_Get(SepCommon.SepCore.Request.Item("UserID"));

                if (string.IsNullOrWhiteSpace(jMembers.UserID))
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Member~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Member");
                    RePasswordRequired.Visible = false;
                    UserID.Value = SepCommon.SepCore.Request.Item("UserID");
                    UserName.Value = jMembers.UserName;
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
                    Points.Value = Strings.ToString(jMembers.UserPoints);
                    AccessClass.Text = Strings.ToString(jMembers.AccessClass);
                    AccessKeys.Text = jMembers.AccessKeys;
                    BirthDate.Value = jMembers.BirthDate.ToShortDateString();
                    Gender.Value = jMembers.Gender;
                    ReferralID.Value = Strings.ToString(jMembers.ReferralID);
                    if (!string.IsNullOrWhiteSpace(jMembers.ReferralUserName)) Referral.Disabled = true;
                    Referral.Value = jMembers.ReferralUserName;
                    Portal.Text = Strings.ToString(jMembers.PortalID);
                    WebsiteURL.Value = jMembers.WebsiteURL;
                    Status.Value = Strings.ToString(jMembers.Status);
                    HideTips.Checked = jMembers.HideTips;

                    if (SepFunctions.TwilioActivated())
                    {
                        PhoneNumber.Attributes.Remove("class");
                        PhoneNumber.Attributes.Add("class", "form-control phoneEntry");
                    }
                }

                var sLetters = SepFunctions.Show_Newsletters(SepCommon.SepCore.Request.Item("UserID"));
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
            else
            {
                if (Page.IsPostBack)
                {
                    AccessClass.Text = Request.Form["AccessClass"];
                    AccessKeys.Text = Request.Form["AccessKeys"];
                    Portal.Text = Request.Form["Portal"];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(UserID.Value)) UserID.Value = SepFunctions.Generate_GUID();

                    AccessClass.Text = SepFunctions.Setup(997, "StartupClass");

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT KeyIDs FROM AccessClasses WHERE ClassID=@ClassID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ClassID", SepFunctions.toLong(SepFunctions.Setup(997, "StartupClass")));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    AccessKeys.Text = SepFunctions.openNull(RS["KeyIDs"]);
                                }
                                else
                                {
                                    AccessKeys.Text = "|1|";
                                }

                            }
                        }
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
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            if (!string.IsNullOrWhiteSpace(Password.Value))
            {
                if (Password.Value != RePassword.Value)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Passwords do not match.") + "</div>";
                    return;
                }

                if (Regex.IsMatch(Password.Value, ".*[@#$%^&*/!].*") == false)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password must contain one of @#$%^&*/!.") + "</div>";
                    return;
                }

                if (Regex.IsMatch(Password.Value, "[^\\s]{4,20}") == false)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password must be between 4-20 characters.") + "</div>";
                    return;
                }
            }

            var sReturn = SepCommon.DAL.Members.Member_Save(UserID.Value, UserName.Value, !string.IsNullOrWhiteSpace(Password.Value) ? Password.Value : string.Empty, string.Empty, string.Empty, FirstName.Value, LastName.Value, StreetAddress.Value, City.Value, State.Text, PostalCode.Value, Country.Text, EmailAddress.Value, PhoneNumber.Value, PayPalEmail.Value, SepFunctions.toInt(Points.Value), SepFunctions.toLong(Request.Form["AccessClass"]), Request.Form["AccessKeys"], SepFunctions.toDate(BirthDate.Value), SepFunctions.toInt(Gender.Value), SepFunctions.GetUserInformation("AffiliateID", ReferralID.Value), WebsiteURL.Value, SepFunctions.toLong(Request.Form["Portal"]), Friends.Value, Request.Form["LetterIDs"], SepFunctions.toInt(Status.Value), string.Empty, string.Empty, string.Empty, HideTips.Checked, SepFunctions.toLong(SepFunctions.GetUserInformation("SiteID", UserID.Value)), string.Empty);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

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

            SepCommon.DAL.CustomFields.Answers_Save(UserID.Value, 29, 29, 0, customXML);

            // End Custom Fields
            var sLetters = SepFunctions.Show_Newsletters(UserID.Value);
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