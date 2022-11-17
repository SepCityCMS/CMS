// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="newsletters_send.aspx.cs" company="SepCity, Inc.">
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
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class newsletters_send.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class newsletters_send : Page
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
                    EmailTo.Items[0].Text = SepFunctions.LangText("All Newsletters");
                    EmailTo.Items[1].Text = SepFunctions.LangText("All Members");
                    EmailTo.Items[2].Text = SepFunctions.LangText("All Non-Customers");
                    EmailTo.Items[3].Text = SepFunctions.LangText("All Paid Customers");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Send Newsletter");
                    PortalLabel.InnerText = SepFunctions.LangText("Portals a user must signup in to receive this newsletter:");
                    AccessKeysLabel.InnerText = SepFunctions.LangText("Access Keys a user must have to receive this newsletter:");
                    EmailFromLavel.InnerText = SepFunctions.LangText("Send Email From:");
                    EmailToLabel.InnerText = SepFunctions.LangText("Send Email To:");
                    EmailSubjectLabel.InnerText = SepFunctions.LangText("Email Subject:");
                    EmailBodyLabel.InnerText = SepFunctions.LangText("Email Body:");
                    EmailSubjectRequired.ErrorMessage = SepFunctions.LangText("~~Email Subject~~ is required.");
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

            GlobalVars.ModuleID = 24;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("NewsletAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("NewsletAdmin"), false) == false)
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), false) == false) KeyRow.Visible = false;

            // Populate Send From Dropdown
            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(GlobalVars.ModuleID, "NewsletFromEmail")))
            {
                var arrEmails = Strings.Split(SepFunctions.Setup(GlobalVars.ModuleID, "NewsletFromEmail"), Environment.NewLine);
                if (arrEmails != null)
                {
                    for (var i = 0; i <= Information.UBound(arrEmails); i++)
                        if (!string.IsNullOrWhiteSpace(arrEmails[i]))
                            EmailFrom.Items.Add(new ListItem(Strings.Trim(arrEmails[i]), Strings.Trim(arrEmails[i])));
                }
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(991, "AdminEmailAddress"))) EmailFrom.Items.Add(new ListItem(SepFunctions.Setup(991, "AdminEmailAddress"), SepFunctions.Setup(991, "AdminEmailAddress")));

            // Populate Send To Dropdown
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT LetterID,NewsletName FROM Newsletters WHERE Status <> -1 ORDER BY NewsletName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                                EmailTo.Items.Add(new ListItem(SepFunctions.openNull(RS["NewsletName"]), "LTR:" + SepFunctions.openNull(RS["LetterID"])));
                    }
                }

                using (var cmd = new SqlCommand("SELECT ListID,ListName FROM GroupLists WHERE Status <> -1 ORDER BY ListName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                                EmailTo.Items.Add(new ListItem(SepFunctions.LangText("Group List:") + " " + SepFunctions.openNull(RS["ListName"]), "GRP:" + SepFunctions.openNull(RS["ListID"])));
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SendButton_Click(object sender, EventArgs e)
        {
            var intRecordCount = 0;
            string[] arrSplit = null;
            var tmpEmail = string.Empty;

            var SqlStr = string.Empty;

            var GetNewsRemoveText = SepFunctions.Setup(GlobalVars.ModuleID, "NewsletRemoveText");
            var GetAdminEmail = EmailFrom.Value;

            var sProcessId = Strings.ToString(SepFunctions.GetIdentity());

            HttpContext.Current.Server.ScriptTimeout = 3600;

            switch (EmailTo.Value)
            {
                case "All":
                    SqlStr = "SELECT DISTINCT EmailAddress,UserID FROM NewslettersUsers WHERE EmailAddress <> ''" + Strings.ToString(PortalID.Text != "-1" ? " AND PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty);
                    break;

                case "AllMembers":
                    SqlStr = "SELECT DISTINCT EmailAddress,UserID FROM Members WHERE EmailAddress <> '' AND Status=1" + Strings.ToString(PortalID.Text != "-1" ? " AND PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty);
                    break;

                case "AllPaidCustomers":
                    SqlStr = "SELECT DISTINCT M.EmailAddress,M.UserID FROM Members AS M,Invoices AS INV WHERE M.UserID=INV.UserID AND INV.inCart=0 AND INV.Status > 0 AND M.EmailAddress <> '' AND M.Status=1" + Strings.ToString(PortalID.Text != "-1" ? " AND M.PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty);
                    break;

                case "AllNonCustomers":
                    SqlStr = "SELECT DISTINCT M.EmailAddress,M.UserID FROM Members AS M WHERE M.UserID NOT IN (SELECT UserID FROM Invoices WHERE UserID=M.UserID) AND M.EmailAddress <> '' AND M.Status=1" + Strings.ToString(PortalID.Text != "-1" ? " AND M.PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty);
                    break;

                default:
                    switch (Strings.Left(EmailTo.Value, 4))
                    {
                        case "LTR:":
                            SqlStr = "SELECT DISTINCT EmailAddress,UserID FROM NewslettersUsers WHERE EmailAddress <> '' AND LetterID='" + SepFunctions.FixWord(Strings.Replace(EmailTo.Value, "LTR:", string.Empty)) + "'" + Strings.ToString(PortalID.Text != "-1" ? " AND PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty);
                            break;

                        case "GRP:":
                            SqlStr = "SELECT DISTINCT M.EmailAddress,M.UserID FROM GroupListsUsers AS GU,Members AS M WHERE GU.UserID=M.UserID AND GU.ListID='" + SepFunctions.FixWord(Strings.Replace(EmailTo.Value, "GRP:", string.Empty)) + "' AND M.EmailAddress <> '' AND M.Status=1" + Strings.ToString(PortalID.Text != "-1" ? " AND M.PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty);
                            break;
                    }

                    break;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("INSERT INTO BG_Processes (ProcessID, ProcessName, IntervalSeconds, Status, RecurringDays) VALUES(@ProcessID, @ProcessName, @IntervalSeconds, @Status, '0')", conn))
                {
                    cmd.Parameters.AddWithValue("@ProcessID", sProcessId);
                    cmd.Parameters.AddWithValue("@ProcessName", "Email");
                    cmd.Parameters.AddWithValue("@IntervalSeconds", 10);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                            {
                                if (tmpEmail != SepFunctions.openNull(RS["EmailAddress"]))
                                {
                                    bool SendLetter = false;
                                    if (!string.IsNullOrWhiteSpace(Request.Form["AccessKeys"]))
                                        using (var cmd2 = new SqlCommand("SELECT AccessKeys FROM Members WHERE UserID='" + SepFunctions.openNull(RS["UserID"], true) + "' AND Status <> -1" + Strings.ToString(PortalID.Text != "-1" ? " AND PortalID=" + SepFunctions.toLong(PortalID.Text) + string.Empty : string.Empty), conn))
                                        {
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (RS2.HasRows)
                                                {
                                                    RS2.Read();
                                                    arrSplit = Strings.Split(SepFunctions.openNull(RS2["AccessKeys"]), ",");
                                                    if (arrSplit != null)
                                                    {
                                                        for (var i = 0; i <= Information.UBound(arrSplit); i++)
                                                            if (SepFunctions.toLong(Request.Form["AccessKeys"]) == SepFunctions.toLong(arrSplit[i]))
                                                            {
                                                                SendLetter = true;
                                                                break;
                                                            }
                                                    }
                                                }
                                            }
                                        }
                                    else
                                        SendLetter = true;

                                    if (SendLetter)
                                    {
                                        var cReplace = new Replace();
                                        string GetEmailBody = cReplace.Replace_Widgets(EmailBody.Text, GlobalVars.ModuleID, false);
                                        GetEmailBody += "<br/><br/><p align=\"center\"><a href=\"" + SepFunctions.GetMasterDomain(true) + "default.aspx?DoAction=NewsletterRemove&EmailAddress=" + SepFunctions.UrlEncode(SepFunctions.openNull(RS["EmailAddress"])) + "\" target=\"_blank\">" + Strings.ToString(!string.IsNullOrWhiteSpace(GetNewsRemoveText) ? GetNewsRemoveText : SepFunctions.LangText("Click here to unsubscribe to our newsletters.")) + "</a></p>";
                                        intRecordCount += 1;
                                        using (var cmd2 = new SqlCommand("INSERT INTO BG_Emails (ProcessID, To_Email_Address, From_Email_Address, Email_Subject, Email_Body, Email_Attachment) VALUES(@ProcessID, @To_Email_Address, @From_Email_Address, @Email_Subject, @Email_Body, @Email_Attachment)", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@ProcessId", sProcessId);
                                            cmd2.Parameters.AddWithValue("@To_Email_Address", SepFunctions.openNull(RS["EmailAddress"]));
                                            cmd2.Parameters.AddWithValue("@From_Email_Address", GetAdminEmail);
                                            cmd2.Parameters.AddWithValue("@Email_Subject", EmailSubject.Value);
                                            cmd2.Parameters.AddWithValue("@Email_Body", SepFunctions.Replace_Fields(GetEmailBody, SepFunctions.openNull(RS["UserID"])));
                                            cmd2.Parameters.AddWithValue("@Email_Attachment", string.Empty);
                                            cmd2.ExecuteNonQuery();
                                        }

                                        cReplace.Dispose();
                                    }
                                }

                                tmpEmail = SepFunctions.openNull(RS["EmailAddress"]);
                            }

                    }
                }

                if (Strings.Left(EmailTo.Value, 4) == "LTR:")
                    using (var cmd = new SqlCommand("INSERT INTO NewslettersSent (SentID, LetterID, AccessKeys, EmailSubject, EmailBody, DateSent, PortalID, Status) VALUES(@SentID, @LetterID, @AccessKeys, @EmailSubject, @EmailBody, @DateSent, @PortalID, 1)", conn))
                    {
                        cmd.Parameters.AddWithValue("@SentID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@LetterID", Strings.Replace(EmailTo.Value, "LTR:", string.Empty));
                        cmd.Parameters.AddWithValue("@AccessKeys", Request.Form["AccessKeys"]);
                        cmd.Parameters.AddWithValue("@EmailSubject", EmailSubject.Value);
                        cmd.Parameters.AddWithValue("@EmailBody", EmailBody.Text);
                        cmd.Parameters.AddWithValue("@DateSent", DateTime.Now);
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.toLong(PortalID.Text));
                        cmd.ExecuteNonQuery();
                    }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Newsletter has been successfully sent.") + "</div>";

            SendLetterForm.Visible = false;
        }
    }
}