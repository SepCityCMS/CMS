// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="default.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class _userdefault.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class _userdefault : Page
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
            GlobalVars.ModuleID = 16;

            if (!Response.IsClientConnected)
                return;
            SepCommon.SepCore.Session.removeCookie("returnUrl");

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepCommon.SepCore.Request.Item("DoAction") == "JoinNewsletter")
            {
                var NewsLetText = string.Empty;
                long aa = 0;

                if (Strings.Len(SepCommon.SepCore.Request.Item("NLEmailAddress")) < 6 || Strings.InStr(SepCommon.SepCore.Request.Item("NLEmailAddress"), "@") == 0 || Strings.InStr(SepCommon.SepCore.Request.Item("NLEmailAddress"), ".") == 0)
                    NewsLetText = SepFunctions.LangText("Either you didn't specify a correct email or someone has already joined with the email address you specified.");
                else
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT LetterID,NewsletName FROM Newsletters", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Newsletter" + SepFunctions.openNull(RS["LetterID"]))))
                                        using (var cmd2 = new SqlCommand("SELECT NUserID FROM NewslettersUsers WHERE LetterID=@LetterID AND EmailAddress=@EmailAddress", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LetterID", SepFunctions.openNull(RS["LetterID"]));
                                            cmd2.Parameters.AddWithValue("@EmailAddress", SepCommon.SepCore.Request.Item("NLEmailAddress"));
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (!RS2.HasRows)
                                                {
                                                    NewsLetText = SepFunctions.LangText("Thank you for joining our newsletter!");
                                                    using (var cmd3 = new SqlCommand("INSERT INTO NewslettersUsers (NUserID, LetterID, UserID, EmailAddress, Status, PortalID) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.openNull(RS["LetterID"], true) + "','" + SepFunctions.Session_User_ID() + "','" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("NLEmailAddress")) + "', '1', '" + SepFunctions.Get_Portal_ID() + "')", conn))
                                                    {
                                                        cmd3.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    NewsLetText = SepFunctions.LangText("You have already joined the ~~" + SepFunctions.openNull(RS["NewsletName"]) + "~~ newsletter.");
                                                }
                                            }
                                        }

                                    aa += 1;
                                }
                            }
                        }
                    }

                if (!string.IsNullOrWhiteSpace(NewsLetText)) PageText.InnerHtml += "<script type=\"text/javascript\" language=\"JavaScript\">alert(unescape('" + SepFunctions.EscQuotes(NewsLetText) + "'))</script>";
            }

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();
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
}