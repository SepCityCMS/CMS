// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="classifieds_feedback.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class classifieds_feedback.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class classifieds_feedback : Page
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
                    Rating.Items[0].Text = SepFunctions.LangText("1 - Horrible");
                    Rating.Items[1].Text = SepFunctions.LangText("2 - Poor");
                    Rating.Items[2].Text = SepFunctions.LangText("3 - Fair");
                    Rating.Items[3].Text = SepFunctions.LangText("4 - Good");
                    Rating.Items[4].Text = SepFunctions.LangText("5 - Excellent");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Leave Feedback");
                    RatingLabel.InnerText = SepFunctions.LangText("Rating:");
                    CommentsLabel.InnerText = SepFunctions.LangText("Comments:");
                    RatingRequired.ErrorMessage = SepFunctions.LangText("~~Rating~~ is required.");
                    CommentsRequired.ErrorMessage = SepFunctions.LangText("~~Comments~~ is required.");
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

            GlobalVars.ModuleID = 44;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ClassifiedAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")) && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FeedbackID")))
            {
                AdID.Value = SepCommon.SepCore.Request.Item("AdID");
                ModifyLegend.InnerHtml = SepFunctions.LangText("View Feedback");
                SaveButton.Visible = false;
                Rating.Visible = false;
                Comments.Visible = false;
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT Rating,Message FROM ClassifiedsFeedback WHERE FeedbackID=@FeedbackID AND AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedbackID", SepCommon.SepCore.Request.Item("FeedbackID"));
                        cmd.Parameters.AddWithValue("@AdID", SepCommon.SepCore.Request.Item("AdID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                Rating.Value = SepFunctions.openNull(RS["Rating"]);
                                Comments.Value = SepFunctions.openNull(RS["Message"]);
                            }

                        }
                    }
                }
            }
            else
            {
                AdID.Value = SepCommon.SepCore.Request.Item("AdID");
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SoldUserID")))
                {
                    ToUserID.Value = SepCommon.SepCore.Request.Item("SoldUserID");
                    FromUserID.Value = SepFunctions.Session_User_ID();
                    BORS.Value = "S";
                }

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("SellerUserID")))
                {
                    ToUserID.Value = SepCommon.SepCore.Request.Item("SellerUserID");
                    FromUserID.Value = SepFunctions.Session_User_ID();
                    BORS.Value = "B";
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("INSERT INTO ClassifiedsFeedback (FeedbackID, Message, AdID, ToUserID, FromUserID, BORS, Rating, DatePosted, Status) VALUES(@FeedbackID, @Message, @AdID, @ToUserID, @FromUserID, @BORS, @Rating, @DatePosted, '1')", conn))
                {
                    cmd.Parameters.AddWithValue("@FeedbackID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@Message", Comments.Value);
                    cmd.Parameters.AddWithValue("@AdID", AdID.Value);
                    cmd.Parameters.AddWithValue("@ToUserID", ToUserID.Value);
                    cmd.Parameters.AddWithValue("@FromUserID", FromUserID.Value);
                    cmd.Parameters.AddWithValue("@BORS", BORS.Value);
                    cmd.Parameters.AddWithValue("@Rating", Rating.Value);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            SepFunctions.Redirect(sInstallFolder + "classifieds_manage.aspx");
        }
    }
}