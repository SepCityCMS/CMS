// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="blogs_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class blogs_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class blogs_view : Page
    {
        public static bool sShowComments = false;

        /// <summary>
        /// The c common
        /// </summary>
        public static string sUserId = string.Empty;

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
        /// Handles the SelectedIndex event of the OtherBlogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void OtherBlogs_SelectedIndex(object sender, EventArgs e)
        {
            SepFunctions.Redirect(UserBlogs.SelectedValue);
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 61;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "BlogsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("BlogsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0] + " - " + SepFunctions.LangText("Display Blog");

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("BlogID")))
                {
                    var sBlogId = SepFunctions.toLong(SepCommon.SepCore.Request.Item("BlogID"));

                    var jBlogs = SepCommon.DAL.Blogs.Blog_Get(sBlogId);
                    if (jBlogs.BlogID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Blog~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                    else
                    {
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewBlog", "GetViewBlog", Strings.ToString(sBlogId), false) == false)
                        {
                            SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                            return;
                        }

                        Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jBlogs.BlogName);
                        BlogName.InnerHtml = jBlogs.BlogName;
                        DatePosted.InnerHtml = Strings.ToString(jBlogs.DatePosted);
                        BlogContent.InnerHtml = jBlogs.Message;
                        PostedBy.InnerHtml = jBlogs.Username;
                        Views.InnerHtml = Strings.ToString(jBlogs.Hits);
                        sShowComments = jBlogs.Comments;
                        sUserId = jBlogs.UserID;
                        if (SepCommon.SepCore.Request.Item("DoAction") != "Print")
                        {
                            List<Blogs> jBlogs2 = SepCommon.DAL.Blogs.GetBlogs(UserID: jBlogs.UserID);
                            if (jBlogs2.Count > 0)
                            {
                                if (jBlogs2.Count > 1)
                                {
                                    UserBlogs.Items.Add(new ListItem(SepFunctions.LangText("Select from ~~" + jBlogs2.Count + "~~ other blogs"), string.Empty));
                                }
                                else
                                {
                                    UserBlogs.Items.Add(new ListItem(SepFunctions.LangText("Select from ~~" + jBlogs2.Count + "~~ other blog"), string.Empty));
                                }

                                for (var i = 0; i <= jBlogs2.Count - 1; i++)
                                {
                                    UserBlogs.Items.Add(new ListItem(jBlogs2[i].BlogName, "/blogs/" + jBlogs2[i].BlogID + "/" + SepFunctions.Format_ISAPI(jBlogs2[i].BlogName) + "/"));
                                }
                            }
                            else
                            {
                                OtherBlogsRow.Visible = false;
                            }
                        }
                        else
                        {
                            OtherBlogsRow.Visible = false;
                        }
                    }
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Blog~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
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
    }
}