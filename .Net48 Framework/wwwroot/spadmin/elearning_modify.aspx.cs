// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="elearning_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class elearning_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class elearning_modify : Page
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
                    RecurringCycle.Items[0].Text = SepFunctions.LangText("Monthly");
                    RecurringCycle.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle.Items[3].Text = SepFunctions.LangText("Yearly");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Course");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    InstructorLabel.InnerText = SepFunctions.LangText("Instructor:");
                    CourseNameLabel.InnerText = SepFunctions.LangText("Course Name:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Start Date:");
                    EndDateLabel.InnerText = SepFunctions.LangText("End Date:");
                    CreditsLabel.InnerText = SepFunctions.LangText("Credits:");
                    CoursePriceLabel.InnerText = SepFunctions.LangText("Course Price:");
                    RecurringPriceLabel.InnerText = SepFunctions.LangText("Recurring Price:");
                    RecurringCycleLabel.InnerText = SepFunctions.LangText("Recurring Cycle:");
                    CategoryRequired.ErrorMessage = SepFunctions.LangText("~~Category~~ is required.");
                    InstructorRequired.ErrorMessage = SepFunctions.LangText("~~Instructor~~ is required.");
                    CourseNameRequired.ErrorMessage = SepFunctions.LangText("~~Course Name~~ is required.");
                    CreditsRequired.ErrorMessage = SepFunctions.LangText("~~Credits~~ is required.");
                    CoursePriceRequired.ErrorMessage = SepFunctions.LangText("~~Course Price~~ is required.");
                    RecurringPriceRequired.ErrorMessage = SepFunctions.LangText("~~Recurring Price~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
            }
        }

        /// <summary>
        /// Handles the Clicked event of the DisableEndDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void DisableEndDate_Clicked(object sender, EventArgs e)
        {
            if (DisableEndDate.Checked) EndDate.Disabled = true;
            else EndDate.Disabled = false;
        }

        /// <summary>
        /// Handles the Clicked event of the DisableStartDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void DisableStartDate_Clicked(object sender, EventArgs e)
        {
            if (DisableStartDate.Checked) StartDate.Disabled = true;
            else StartDate.Disabled = false;
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

            GlobalVars.ModuleID = 37;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("ELearningAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ELearningAdmin"), true) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("CourseID")))
            {
                if (SepCommon.SepCore.Request.Item("DoAction") == "DeleteImage")
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID='37'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", SepCommon.SepCore.Request.Item("CourseID"));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Image has been successfully deleted.") + "</div>";

                    ThumbnailImagePreview.Visible = false;
                    CatImageDelete.Visible = false;
                }

                var jCourses = SepCommon.DAL.ELearning.Course_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("CourseID")), SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")));

                if (jCourses.CourseID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Course~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Course");
                    CourseID.Value = SepCommon.SepCore.Request.Item("CourseID");
                    Category.CatID = Strings.ToString(jCourses.CatID);
                    Instructor.Value = jCourses.Instructor;
                    CourseName.Value = jCourses.CourseName;
                    if (Strings.FormatDateTime(jCourses.StartDate, Strings.DateNamedFormat.ShortDate) == "1/1/2099")
                    {
                        DisableStartDate.Checked = true;
                        StartDate.Disabled = true;
                    }
                    else
                    {
                        StartDate.Value = Strings.FormatDateTime(jCourses.StartDate, Strings.DateNamedFormat.ShortDate);
                    }

                    if (Strings.FormatDateTime(jCourses.EndDate, Strings.DateNamedFormat.ShortDate) == "1/1/2099")
                    {
                        DisableEndDate.Checked = true;
                        EndDate.Disabled = true;
                    }
                    else
                    {
                        EndDate.Value = Strings.FormatDateTime(jCourses.EndDate, Strings.DateNamedFormat.ShortDate);
                    }

                    Credits.Value = Strings.ToString(jCourses.Credits);
                    if (!string.IsNullOrWhiteSpace(jCourses.ThumbnailImage))
                    {
                        ThumbnailImagePreview.Visible = true;
                        CatImageDelete.Visible = true;
                        ThumbnailImagePreview.ImageUrl = "show_image.aspx?ModuleID=37&UniqueID=" + SepCommon.SepCore.Request.Item("CourseID");
                        CatImageDelete.NavigateUrl = "elearning_modify.aspx?DoAction=DeleteImage&CourseID=" + SepCommon.SepCore.Request.Item("CourseID");
                        ImageNameLabel.InnerText = SepFunctions.LangText("Replace the Image Below:");
                    }

                    CourseDescription.Text = jCourses.Description;
                    CoursePrice.Value = SepFunctions.Format_Currency(jCourses.Price);
                    RecurringPrice.Value = SepFunctions.Format_Currency(jCourses.RecurringPrice);
                    RecurringCycle.Value = jCourses.RecurringCycle;

                    ChangeLog.ChangeUniqueID = SepCommon.SepCore.Request.Item("CourseID");
                    ChangeLog.Text = SepCommon.SepCore.Request.Item("ChangeID");

                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ChangeID")) > 0) SaveButton.InnerText = SepFunctions.LangText("Restore this Version");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(CourseID.Value)) CourseID.Value = Strings.ToString(SepFunctions.GetIdentity());

                if (!Page.IsPostBack)
                {
                    CoursePrice.Value = SepFunctions.Format_Currency("0");
                    RecurringPrice.Value = SepFunctions.Format_Currency("0");
                    StartDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, DateTime.Now), Strings.DateNamedFormat.GeneralDate);
                    EndDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 4, DateTime.Now), Strings.DateNamedFormat.GeneralDate);
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
            var imageType = string.Empty;
            var imageData = string.Empty;
            var sFileName = string.Empty;

            if (ThumbnailImage.PostedFile == null || string.IsNullOrWhiteSpace(ThumbnailImage.PostedFile.FileName))
            {
            }
            else
            {
                var sFileExt = Strings.LCase(Path.GetExtension(ThumbnailImage.PostedFile.FileName)); // -V3095
                if (sFileExt != ".jpg" && sFileExt != ".jpeg" && sFileExt != ".gif" && sFileExt != ".png")
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid image file format. (Only jpg, gif and png files are supported)") + "</div>";
                    return;
                }

                var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(ThumbnailImage.PostedFile.InputStream.Length)) + 1];
                ThumbnailImage.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                imageType = ThumbnailImage.PostedFile.ContentType;
                imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));

                sFileName = Path.GetFileName(ThumbnailImage.PostedFile.FileName); // -V3125
            }

            var sDate = SepFunctions.toDate(StartDate.Value);
            var eDate = SepFunctions.toDate(EndDate.Value);

            if (DisableStartDate.Checked) sDate = SepFunctions.toDate("1/1/2099");
            if (DisableEndDate.Checked) eDate = SepFunctions.toDate("1/1/2099");

            var intReturn = SepCommon.DAL.ELearning.Course_Save(SepFunctions.toLong(CourseID.Value), SepFunctions.toLong(Category.CatID), SepFunctions.Session_User_ID(), CourseName.Value, Instructor.Value, sDate, eDate, SepFunctions.toInt(Credits.Value), CoursePrice.Value, RecurringPrice.Value, RecurringCycle.Value, CourseDescription.Text, SepFunctions.Get_Portal_ID(), sFileName, imageType, imageData);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}