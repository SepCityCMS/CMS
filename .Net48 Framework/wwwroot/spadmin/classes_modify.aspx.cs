// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="classes_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class classes_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class classes_modify : Page
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
                    RecurringCycle1.Items[0].Text = SepFunctions.LangText("1 Month");
                    RecurringCycle1.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle1.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle1.Items[3].Text = SepFunctions.LangText("1 Year");
                    RecurringCycle2.Items[0].Text = SepFunctions.LangText("1 Month");
                    RecurringCycle2.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle2.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle2.Items[3].Text = SepFunctions.LangText("1 Year");
                    RecurringCycle3.Items[0].Text = SepFunctions.LangText("1 Month");
                    RecurringCycle3.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle3.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle3.Items[3].Text = SepFunctions.LangText("1 Year");
                    RecurringCycle4.Items[0].Text = SepFunctions.LangText("1 Month");
                    RecurringCycle4.Items[1].Text = SepFunctions.LangText("3 Months");
                    RecurringCycle4.Items[2].Text = SepFunctions.LangText("6 Months");
                    RecurringCycle4.Items[3].Text = SepFunctions.LangText("1 Year");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Access Class");
                    ClassNameLabel.InnerText = SepFunctions.LangText("Class Name:");
                    AccessKeysLabel.InnerText = SepFunctions.LangText("Access Keys to assign users in this class:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portals to Show Class In:");
                    ClassNameRequired.ErrorMessage = SepFunctions.LangText("~~Class Name~~ is required.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSecurity")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), false) == false || SepFunctions.ModuleActivated(68))
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

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ClassID")) == 1 || SepFunctions.toLong(SepCommon.SepCore.Request.Item("ClassID")) == 2)
            {
                ModifyClassDiv.Visible = false;
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You cannot modify the Everyone or Administrator class") + "</div>";
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ClassID")))
            {
                var jClasses = SepCommon.DAL.Security.Class_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ClassID")));

                if (jClasses.ClassID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Access Class~~ does not exist.") + "</div>";
                    ModifyClassDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Class");
                    ClassID.Value = SepCommon.SepCore.Request.Item("ClassID");
                    ClassName.Value = jClasses.ClassName;
                    AccessKeySelection.Text = jClasses.KeyIDs;
                    Description.Text = jClasses.Description;
                    UnitPrice1.Value = Strings.ToString(jClasses.UnitPrice1);
                    RecurringPrice1.Value = Strings.ToString(jClasses.RecurringPrice1);
                    RecurringCycle1.Value = jClasses.RecurringCycle1;
                    UnitPrice2.Value = Strings.ToString(jClasses.UnitPrice2);
                    RecurringPrice2.Value = Strings.ToString(jClasses.RecurringPrice2);
                    RecurringCycle2.Value = jClasses.RecurringCycle2;
                    UnitPrice3.Value = Strings.ToString(jClasses.UnitPrice3);
                    RecurringPrice3.Value = Strings.ToString(jClasses.RecurringPrice3);
                    RecurringCycle3.Value = jClasses.RecurringCycle3;
                    UnitPrice4.Value = Strings.ToString(jClasses.UnitPrice4);
                    RecurringPrice4.Value = Strings.ToString(jClasses.RecurringPrice4);
                    RecurringCycle4.Value = jClasses.RecurringCycle4;
                    if (jClasses.PrivateClass) PrivateClass.Checked = true;
                    LoggedDays.Value = jClasses.LoggedDays;
                    LoggedSwitchTo.Text = jClasses.LoggedSwitchTo;
                    InDays.Value = jClasses.InDays;
                    InSwitchTo.Text = jClasses.InSwitchTo;
                    PortalSelection.Text = jClasses.PortalIDs;
                }
            }
            else
            {
                if (Page.IsPostBack)
                {
                    LoggedSwitchTo.Text = Request.Form["LoggedSwitchTo"];
                    InSwitchTo.Text = Request.Form["InSwitchTo"];
                }

                if (string.IsNullOrWhiteSpace(ClassID.Value)) ClassID.Value = Strings.ToString(SepFunctions.GetIdentity());
                UnitPrice1.Value = SepFunctions.Format_Currency(UnitPrice1.Value);
                RecurringPrice1.Value = SepFunctions.Format_Currency(RecurringPrice1.Value);
                UnitPrice2.Value = SepFunctions.Format_Currency(UnitPrice2.Value);
                RecurringPrice2.Value = SepFunctions.Format_Currency(RecurringPrice2.Value);
                UnitPrice3.Value = SepFunctions.Format_Currency(UnitPrice3.Value);
                RecurringPrice3.Value = SepFunctions.Format_Currency(RecurringPrice3.Value);
                UnitPrice4.Value = SepFunctions.Format_Currency(UnitPrice4.Value);
                RecurringPrice4.Value = SepFunctions.Format_Currency(RecurringPrice4.Value);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sReturn = SepCommon.DAL.Security.Class_Save(SepFunctions.toLong(ClassID.Value), ClassName.Value, AccessKeySelection.Text, Description.Text, SepFunctions.toDecimal(UnitPrice1.Value), SepFunctions.toDecimal(RecurringPrice1.Value), RecurringCycle1.Value, SepFunctions.toDecimal(UnitPrice2.Value), SepFunctions.toDecimal(RecurringPrice2.Value), RecurringCycle2.Value, SepFunctions.toDecimal(UnitPrice3.Value), SepFunctions.toDecimal(RecurringPrice3.Value), RecurringCycle3.Value, SepFunctions.toDecimal(UnitPrice4.Value), SepFunctions.toDecimal(RecurringPrice4.Value), RecurringCycle4.Value, PrivateClass.Checked ? "true" : "false", LoggedDays.Value, SepCommon.SepCore.Request.Item("LoggedSwitchTo"), InDays.Value, SepCommon.SepCore.Request.Item("InSwitchTo"), PortalSelection.Text);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";
        }
    }
}