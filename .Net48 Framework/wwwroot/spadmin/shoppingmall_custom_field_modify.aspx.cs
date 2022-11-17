// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shoppingmall_custom_field_modify.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class shoppingmall_custom_field_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shoppingmall_custom_field_modify : Page
    {
        /// <summary>
        /// Handles the SelectedIndexChanged event of the AnswerType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void AnswerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (AnswerType.SelectedValue)
            {
                case "DropdownM":
                case "DropdownS":
                case "Radio":
                case "Checkbox":
                    showOptions.Visible = true;
                    optionList.Visible = true;
                    break;

                default:
                    showOptions.Visible = false;
                    optionList.Visible = false;
                    CustomFieldOptions.Value = string.Empty;
                    break;
            }
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    AnswerType.Items[0].Text = SepFunctions.LangText("Short Answer");
                    AnswerType.Items[1].Text = SepFunctions.LangText("Long Answer");
                    AnswerType.Items[2].Text = SepFunctions.LangText("Dropdown (Multiple Selection)");
                    AnswerType.Items[3].Text = SepFunctions.LangText("Dropdown (Single Selection)");
                    AnswerType.Items[4].Text = SepFunctions.LangText("Radio Buttons");
                    AnswerType.Items[5].Text = SepFunctions.LangText("Checkboxes");
                    AnswerType.Items[6].Text = SepFunctions.LangText("Date");
                    Require.Items[0].Text = SepFunctions.LangText("No");
                    Require.Items[1].Text = SepFunctions.LangText("Yes");
                    FieldNameLabel.InnerText = SepFunctions.LangText("Field Name:");
                    AnswerTypeLabel.InnerText = SepFunctions.LangText("Answer Type:");
                    OptionNameLabel.InnerText = SepFunctions.LangText("Option Name:");
                    OptionOrderLabel.InnerText = SepFunctions.LangText("Order:");
                    PriceLabel.InnerText = SepFunctions.LangText("Price:");
                    RecurringPriceLabel.InnerText = SepFunctions.LangText("Recurring Price:");
                    OrderLabel.InnerText = SepFunctions.LangText("Order:");
                    RequiredLabel.InnerText = SepFunctions.LangText("Required:");
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

            GlobalVars.ModuleID = 41;

            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopCartAdmin"), true) == false)
            {
                Response.Write("<div align=\"center\" style=\"margin-top:50px\">");
                Response.Write("<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>");
                Response.Write(SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>");
                Response.Write("</div>");
                form1.Visible = false;
                return;
            }

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("FieldID")))
                {
                    FieldID.Value = SepCommon.SepCore.Request.Item("FieldID");
                    FieldMode.Value = "edit";
                    var scriptKey = "editCustomField";
                    var javaScript = "<script type=\"text/javascript\">editField('" + SepCommon.SepCore.Request.Item("FieldID") + "');</script>";
                    Page.ClientScript.RegisterStartupScript(GetType(), scriptKey, javaScript);
                }
                else
                {
                    FieldID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    OptionID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    FieldMode.Value = "add";
                    Order.Value = "1";
                    showOptions.Visible = false;
                    optionList.Visible = false;
                }
            }
        }
    }
}