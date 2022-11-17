// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="customfields_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class customfields_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class customfields_modify : Page
    {
        /// <summary>
        /// Adds the options row.
        /// </summary>
        /// <param name="iNum">The i number.</param>
        /// <param name="OptionID">The option identifier.</param>
        /// <param name="txtValue">The text value.</param>
        /// <param name="addMode">if set to <c>true</c> [add mode].</param>
        public void Add_Options_Row(int iNum, string OptionID, string txtValue, bool addMode)
        {
            var pRow = new HtmlGenericControl("p")
            {
                ID = "Option" + iNum + "Row",
                ClientIDMode = ClientIDMode.Static
            };
            if (addMode)
                pRow.Style.Add("display", "none");

            using (var lblDynamic = new Label())
            {
                lblDynamic.Text = "Option " + iNum;
                lblDynamic.ID = "Option" + iNum + "Label";
                lblDynamic.AssociatedControlID = "Option" + iNum;
                pRow.Controls.Add(lblDynamic);
            }

            using (var txtDynamic = new TextBox())
            {
                txtDynamic.ID = "Option" + iNum;
                txtDynamic.CssClass = "form-control";
                txtDynamic.MaxLength = 100;
                txtDynamic.Text = txtValue;
                pRow.Controls.Add(txtDynamic);
            }

            using (var hdnDynamic = new HiddenField())
            {
                hdnDynamic.ID = "OptionID" + iNum;
                hdnDynamic.Value = OptionID;
                pRow.Controls.Add(hdnDynamic);
            }

            OptionPanel.Controls.Add(pRow);
        }

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
        /// Loads the custom options.
        /// </summary>
        public void Load_Custom_Options()
        {
            var cOptions = SepCommon.DAL.CustomFields.GetCustomFieldOptions(SepFunctions.toLong(FieldID.Value));

            for (var i = 0; i <= cOptions.Count - 1; i++)
            {
                NumOptions.Value = Strings.ToString(SepFunctions.toInt(NumOptions.Value) + 1);
                Add_Options_Row(i + 1, Strings.ToString(cOptions[i].OptionID), cOptions[i].OptionName, false);
            }
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
                    Section.Items[0].Text = SepFunctions.LangText("Select a Section");
                    AnswerType.Items[0].Text = SepFunctions.LangText("Short Answer");
                    AnswerType.Items[1].Text = SepFunctions.LangText("Long Answer");
                    AnswerType.Items[2].Text = SepFunctions.LangText("Dropdown (Multiple Selection)");
                    AnswerType.Items[3].Text = SepFunctions.LangText("Dropdown (Single Selection)");
                    AnswerType.Items[4].Text = SepFunctions.LangText("Radio Buttons");
                    AnswerType.Items[5].Text = SepFunctions.LangText("Checkboxes");
                    AnswerType.Items[6].Text = SepFunctions.LangText("Date");
                    Required.Items[0].Text = SepFunctions.LangText("Yes");
                    Required.Items[1].Text = SepFunctions.LangText("No");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Custom Field");
                    SectionLabel.InnerText = SepFunctions.LangText("Section:");
                    FieldNameLabel.InnerText = SepFunctions.LangText("Field Name:");
                    AnswerTypeLabel.InnerText = SepFunctions.LangText("Answer Type:");
                    RequiredLabel.InnerText = SepFunctions.LangText("Required:");
                    ModulesLabel.Text = SepFunctions.LangText("Modules to show custom field in:");
                    PortalsLabel.Text = SepFunctions.LangText("Portals to show custom field in:");
                    FieldNameRequired.ErrorMessage = SepFunctions.LangText("~~Field Name~~ is required.");
                    AnswerTypeRequired.ErrorMessage = SepFunctions.LangText("~~Answer Type~~ is required.");
                    RequiredRequired.ErrorMessage = SepFunctions.LangText("~~Required~~ is required.");
                    ModulesRequired.ErrorMessage = SepFunctions.LangText("You must select at lease one module to show this custom field in.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) == false)
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

            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable" || SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), false) == false)
            {
                PortalsRow.Visible = false;
                Portals.Text = "|" + SepFunctions.Get_Portal_ID() + "|";
            }

            if (!Page.IsPostBack)
            {
                var dCustomFieldsSections = SepCommon.DAL.CustomFields.GetCustomFieldSections();

                if (dCustomFieldsSections.Count > 0)
                    for (var i = 0; i <= dCustomFieldsSections.Count - 1; i++)
                        Section.Items.Add(new ListItem(dCustomFieldsSections[i].SectionName, Strings.ToString(dCustomFieldsSections[i].SectionID)));
                else SectionRow.Visible = false;
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FieldID")))
            {
                var jFields = SepCommon.DAL.CustomFields.Field_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("FieldID")));

                if (jFields.FieldID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Field~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Custom Field");
                    FieldID.Value = SepCommon.SepCore.Request.Item("FieldID");
                    Section.Value = Strings.ToString(jFields.SectionID);
                    FieldName.Value = jFields.FieldName;
                    AnswerType.Value = jFields.AnswerType;
                    Required.Value = Strings.ToString(jFields.Required);

                    // if (jFields.Searchable == false) Searchable.Value = "False";
                    Weight.Value = Strings.ToString(jFields.Weight);
                    Modules.Text = jFields.ModuleIDs;
                    Portals.Text = jFields.PortalIDs;
                }

                Load_Custom_Options();
            }
            else
            {
                if (Page.IsPostBack)
                {
                    Modules.Text = Request.Form["Modules"];
                    Portals.Text = Request.Form["Portals"];
                }
                else
                {
                    Required.Value = "No";

                    // Searchable.Value = "No";
                    Modules.Text = "|" + SepCommon.SepCore.Request.Item("ModuleID") + "|";
                    Portals.Text = "|" + SepFunctions.Get_Portal_ID() + "|";
                    for (var i = 1; i <= 5; i++) Add_Options_Row(i, Strings.ToString(SepFunctions.GetIdentity()), string.Empty, true);
                    NumOptions.Value = "5";

                    if (string.IsNullOrWhiteSpace(FieldID.Value)) FieldID.Value = Strings.ToString(SepFunctions.GetIdentity());
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
            if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Modules")))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one module to show this custom field in.") + "</div>";
                return;
            }

            var sReturn = SepCommon.DAL.CustomFields.Field_Save(SepFunctions.toLong(FieldID.Value), SepFunctions.toLong(Section.Value), FieldName.Value, AnswerType.Value, SepFunctions.toBoolean(Required.Value), true, SepFunctions.toLong(Weight.Value), Request.Form["Modules"], Request.Form["Portals"], string.Empty);

            if (Strings.InStr(sReturn, SepFunctions.LangText("Successfully")) > 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + sReturn + "</div>";
            else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + sReturn + "</div>";

            for (var i = 1; i <= SepFunctions.toInt(SepCommon.SepCore.Request.Item("ctl00$MainContent$NumOptions")); i++)
            {
                var sValue = SepCommon.SepCore.Request.Item("ctl00$MainContent$Option" + i);
                if (!string.IsNullOrWhiteSpace(sValue)) _ = SepCommon.DAL.CustomFields.Field_Option_Save(SepFunctions.toLong(Strings.Left(SepCommon.SepCore.Request.Item("ctl00$MainContent$OptionID" + i), 15)), SepFunctions.toLong(FieldID.Value), sValue, sValue, 0, 0, 0);
            }

            Load_Custom_Options();
        }
    }
}