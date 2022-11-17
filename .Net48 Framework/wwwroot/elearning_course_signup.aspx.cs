// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_course_signup.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class elearning_course_signup.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_course_signup : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 37;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ELearningEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ELearningAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "elearning_course_signup.aspx?CourseID=" + SepCommon.SepCore.Request.Item("CourseID"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var sReturn = 0;
            var CourseName = string.Empty;
            var bActive = true;

            var jCourse = SepCommon.DAL.ELearning.Course_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("CourseID")));

            if (jCourse.CourseID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Course~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
            }
            else
            {
                CourseName = jCourse.CourseName;
                if (SepFunctions.Format_Currency(jCourse.Price) != SepFunctions.Format_Currency("0") || SepFunctions.Format_Currency(jCourse.RecurringPrice) != SepFunctions.Format_Currency("0"))
                {
                    bActive = false;
                    SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, 37, Strings.ToString(jCourse.CourseID), "1", string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, SepFunctions.Get_Portal_ID());
                }

                sReturn = SepCommon.DAL.ELearning.Student_Save(SepFunctions.GetIdentity(), SepFunctions.toLong(SepCommon.SepCore.Request.Item("CourseID")), SepFunctions.Session_User_ID(), bActive, DateTime.Now, string.Empty);
            }

            if (bActive)
            {
                if (sReturn == 2) ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have already registered for ~~" + CourseName + "~~.") + "</div>";
                else ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully registered for ~~" + CourseName + "~~.") + "</div>";
            }
            else
            {
                SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
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