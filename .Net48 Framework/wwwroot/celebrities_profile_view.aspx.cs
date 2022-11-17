// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="celebrities_profile_view.aspx.cs" company="SepCity, Inc.">
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
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class celebrities_profile_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class celebrities_profile_view : Page
    {
        /// <summary>
        /// The s profile image
        /// </summary>
        public static string sProfileImage = string.Empty;

        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

        /// <summary>
        /// The s user name
        /// </summary>
        public static string sUserName = string.Empty;

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
        /// Handles the Click event of the DonateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void DonateButton_Click(object sender, EventArgs e)
        {
            var sProfileId = SepFunctions.userProfileID(SepFunctions.GetUserID(UserName.Value));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                SepFunctions.RequireLogin("|2|");

            var GetInvoiceID = SepFunctions.toLong(SepFunctions.Session_Invoice_ID());

            var jCustom = SepCommon.DAL.CustomFields.Answer_Get(847562837400918, UserID.Value);

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UnitPrice FROM ShopProducts WHERE ProductID=@ProductID", conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", jCustom.FieldValue);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            SepCommon.DAL.Invoices.Invoice_Save(GetInvoiceID, SepFunctions.Session_User_ID(), 0, DateTime.Now, 0, string.Empty, "1", string.Empty, string.Empty, false, "Donation", SepFunctions.openNull(RS["UnitPrice"]), string.Empty, string.Empty, string.Empty, 0, 0, SepFunctions.Get_Portal_ID());
                            var cPayPal = new Submit_PayPal();
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", cPayPal.SendPayPal(GetInvoiceID, SepFunctions.openNull(RS["UnitPrice"]), SepFunctions.GetMasterDomain(true) + "celebrities_schedule_call.aspx?ProfileID=" + sProfileId + "&UserID=" + UserID.Value));
                        }

                    }
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There was an error") + "</div>";
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
            var sProfileId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("ProfileID"));

            GlobalVars.ModuleID = 63;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserName")))
                if (SepFunctions.userProfileID(SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName"))) > 0)
                    sProfileId = SepFunctions.userProfileID(SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")));

            if (sProfileId > 0)
            {
                var jProfiles = SepCommon.DAL.UserProfiles.Profile_Get(sProfileId);

                if (jProfiles.ProfileID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Profile~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    sUserID = jProfiles.UserID;
                    sUserName = jProfiles.Username;
                    sProfileImage = jProfiles.DefaultPicture;
                    FirstName.InnerHtml = jProfiles.FirstName;
                    LastName.InnerHtml = jProfiles.LastName;
                    FirstName2.InnerHtml = jProfiles.FirstName;
                    LastName2.InnerHtml = jProfiles.LastName;
                    FirstName3.InnerHtml = jProfiles.FirstName;
                    LastName3.InnerHtml = jProfiles.LastName;
                    AboutMe.InnerHtml = jProfiles.AboutMe;

                    UserID.Value = sUserID;
                    UserName.Value = SepCommon.SepCore.Request.Item("UserName");

                    // Change Colors
                    if (!string.IsNullOrWhiteSpace(jProfiles.BGColor))
                    {
                        DisplayContent.Style.Add("background-color", "#" + jProfiles.BGColor);
                        var scriptKey = "bgColorScript";
                        var javaScript = "<script type=\"text/javascript\">$('#UserTbl tr:first').removeClass('TableHeader');$('#AboutMeTbl tr:first').removeClass('TableHeader');</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
                    }

                    if (!string.IsNullOrWhiteSpace(jProfiles.TextColor)) DisplayContent.Style.Add("color", "#" + jProfiles.TextColor);
                    if (!string.IsNullOrWhiteSpace(jProfiles.LinkColor))
                    {
                        var scriptKey = "linkColorScript";
                        var javaScript = "<script type=\"text/javascript\">$('#AboutMeTbl a').css('color', '#" + jProfiles.LinkColor + "');</script>";
                        Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
                    }

                    // Custom Fields
                    var jCustom1 = SepCommon.DAL.CustomFields.Answer_Get(117193851274426, jProfiles.UserID);
                    CharitiesSupported.InnerHtml = Strings.Replace(jCustom1.FieldValue, Environment.NewLine, "<br/>");
                    var jCustom2 = SepCommon.DAL.CustomFields.Answer_Get(179115432969858, jProfiles.UserID);
                    CausesSupported.InnerHtml = Strings.Replace(jCustom2.FieldValue, Environment.NewLine, "<br/>");

                    // Price
                    var jCustom3 = SepCommon.DAL.CustomFields.Answer_Get(847562837400918, UserID.Value);
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT UnitPrice FROM ShopProducts WHERE ProductID=@ProductID", conn))
                        {
                            if (string.IsNullOrWhiteSpace(jCustom3.FieldValue))
                                cmd.Parameters.AddWithValue("@ProductID", 0);
                            else
                                cmd.Parameters.AddWithValue("@ProductID", jCustom3.FieldValue);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    DonateNow.Text = "Donate " + SepFunctions.Format_Currency(SepFunctions.openNull(RS["UnitPrice"])) + " to schedule a call with this celebrity";
                                }
                                else
                                {
                                    DonateNow.Visible = false;
                                }

                            }
                        }
                    }

                    // Show Images
                    ProfilePics.ContentUniqueID = Strings.ToString(jProfiles.ProfileID);
                    ProfilePics.ModuleID = GlobalVars.ModuleID;
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Profile~~ does not exist.") + "</div>";
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

    /// <summary>
    /// Class Submit_PayPal.
    /// </summary>
    public class Submit_PayPal
    {
        /// <summary>
        /// Sends the pay pal.
        /// </summary>
        /// <param name="GetInvoiceID">The get invoice identifier.</param>
        /// <param name="UnitPrice">The unit price.</param>
        /// <param name="returnURL">The return URL.</param>
        /// <returns>System.String.</returns>
        public string SendPayPal(long GetInvoiceID, string UnitPrice, string returnURL)
        {
            var sXML = string.Empty;
            var sRelayURL = SepFunctions.GetMasterDomain(false) + "payments.aspx";
            var GetCustomerID = SepFunctions.GetIdentity();

            var str = new StringBuilder();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='GATEWAYS'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sXML = SepFunctions.openNull(RS["ScriptText"]);
                        }

                    }
                }

                var sPayPal = SepFunctions.ParseXML("PayPal", sXML);
                var GetPayPalEmail = SepFunctions.ParseXML("EmailAddress", sPayPal);

                str.Append("<script type=\"text/javascript\">");
                str.Append("var PaymentForm = document.createElement('form');");
                str.Append("PaymentForm.setAttribute('method', 'post');");
                str.Append("PaymentForm.setAttribute('name', 'frmPayment');");
                str.Append("PaymentForm.setAttribute('action', 'https://www.paypal.com/cgi-bin/webscr');");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'cmd');");
                str.Append("hiddenInput.setAttribute('value', '_xclick');");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'business');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(GetPayPalEmail) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'item_name');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Order Number ~~" + GetInvoiceID + "~~")) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'item_number');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(GetCustomerID)) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'invoice');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(GetInvoiceID)) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'amount');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(Strings.ToString(SepFunctions.toDecimal(UnitPrice))) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'no_note');");
                str.Append("hiddenInput.setAttribute('value', '1');");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'currency_code');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(SepFunctions.Setup(992, "CurrencyCode")) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'notify_url');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(sRelayURL) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("var hiddenInput = document.createElement('input');");
                str.Append("hiddenInput.setAttribute('type', 'hidden');");
                str.Append("hiddenInput.setAttribute('name', 'return');");
                str.Append("hiddenInput.setAttribute('value', unescape('" + SepFunctions.EscQuotes(returnURL) + "'));");
                str.Append("PaymentForm.appendChild(hiddenInput);");

                str.Append("document.body.appendChild(PaymentForm);");
                str.Append("PaymentForm.submit();");
                str.Append("document.body.removeChild(PaymentForm);");
                str.Append("</script>");
            }

            return Strings.ToString(str);
        }
    }
}