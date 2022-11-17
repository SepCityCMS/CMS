// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="reset_password.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class reset_password.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class reset_password : Page
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
                    SwcretQuestionLabel.InnerText = SepFunctions.LangText("Secret Question:");
                    SecretAnswerLabel.InnerText = SepFunctions.LangText("Secret Answer:");
                    PasswordLabel.InnerText = SepFunctions.LangText("Enter a Password:");
                    RePasswordLabel.InnerText = SepFunctions.LangText("Re-enter a Password:");
                    SecretAnswerRequired.ErrorMessage = SepFunctions.LangText("~~Secret Answer~~ is required.");
                    RePasswordRequired.ErrorMessage = SepFunctions.LangText("~~Password~~ is required.");
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

            GlobalVars.ModuleID = 21;

            Page.Title = SepFunctions.LangText("Reset Password") + " : " + SepFunctions.Setup(992, "WebSiteName");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT Secret_Question,PasswordResetDate FROM Members WHERE Status=1 AND PasswordResetID=@ResetID AND UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")) ? SepCommon.SepCore.Request.Item("UserID") : string.Empty);
                    cmd.Parameters.AddWithValue("@ResetID", !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ResetID")) ? SepCommon.SepCore.Request.Item("ResetID") : string.Empty);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            LoginDiv.Visible = false;
                            idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This link has expired and cannot be viewed.") + "</div>";
                        }
                        else
                        {
                            RS.Read();
                            if (DateAndTime.DateDiff(DateAndTime.DateInterval.Day, SepFunctions.toDate(SepFunctions.openNull(RS["PasswordResetDate"])), DateTime.Now) < 2)
                            {
                                UserID.Value = SepCommon.SepCore.Request.Item("UserID");
                                ResetID.Value = SepCommon.SepCore.Request.Item("ResetID");

                                if (string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["Secret_Question"])) || string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["Secret_Answer"])))
                                {
                                    SecretQuestionRow.Visible = false;
                                    SecretAnswerRow.Visible = false;
                                }
                                else
                                {
                                    SecretQuestion.InnerHtml = SepFunctions.openNull(RS["Secret_Question"]);
                                }
                            }
                            else
                            {
                                LoginDiv.Visible = false;
                                idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This link has expired and cannot be viewed.") + "</div>";
                            }
                        }

                    }
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
        /// Handles the Click event of the PasswordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PasswordButton_Click(object sender, EventArgs e)
        {
            if (Password.Value != RePassword.Value)
            {
                idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Passwords do not match.") + "</div>";
                return;
            }

            if (Regex.IsMatch(Password.Value, ".*[@#$%^&*/!].*") == false)
            {
                idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password must contain one of @#$%^&*/!.") + "</div>";
                return;
            }

            if (Regex.IsMatch(Password.Value, "[^\\s]{4,20}") == false)
            {
                idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password must be between 4-20 characters.") + "</div>";
                return;
            }

            if ((!string.IsNullOrWhiteSpace(SecretAnswer.Value) || !SecretAnswerRow.Visible) && !string.IsNullOrWhiteSpace(Password.Value))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT Secret_Answer FROM Members WHERE Status=1 AND PasswordResetID=@ResetID AND UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                        cmd.Parameters.AddWithValue("@ResetID", ResetID.Value);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (Strings.Trim(Strings.UCase(SepFunctions.openNull(RS["Secret_Answer"]))) == Strings.Trim(Strings.UCase(SecretAnswer.Value)))
                                {
                                    using (var cmd2 = new SqlCommand("UPDATE Members SET Password=@Password, PasswordResetID='' WHERE Status=1 AND PasswordResetID=@PasswordResetID AND UserID=@UserID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@Password", SepFunctions.MD5Hash_Encrypt(Password.Value));
                                        cmd2.Parameters.AddWithValue("@PasswordResetID", ResetID.Value);
                                        cmd2.Parameters.AddWithValue("@UserID", UserID.Value);
                                        cmd2.ExecuteNonQuery();
                                    }

                                    idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully changed your password.") + "</div>";
                                    LoginDiv.Visible = false;
                                }
                                else
                                {
                                    idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid answer, please try again.") + "</div>";
                                    LoginDiv.Visible = true;
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid answer, please try again.") + "</div>";
                LoginDiv.Visible = true;
            }
        }
    }
}