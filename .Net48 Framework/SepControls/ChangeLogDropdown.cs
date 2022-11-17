// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="ChangeLogDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class ChangeLogDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ChangeLogDropdown runat=server></{0}:ChangeLogDropdown>")]
    public class ChangeLogDropdown : WebControl
    {
        /// <summary>
        /// The m change unique identifier
        /// </summary>
        private string m_ChangeUniqueID;

        /// <summary>
        /// The m module identifier
        /// </summary>
        private int m_ModuleID;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the change unique identifier.
        /// </summary>
        /// <value>The change unique identifier.</value>
        public string ChangeUniqueID
        {
            get => Strings.ToString(m_ChangeUniqueID);

            set => m_ChangeUniqueID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID
        {
            get => Convert.ToInt32(m_ModuleID);

            set => m_ModuleID = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                var s = Context.Request.Form[ID];
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // -V3013
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity")) == false)
            {
                return;
            }

            var href = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ChangeID,DateChanged FROM ChangeLog WHERE ModuleID='" + ModuleID + "' AND UniqueID='" + UniqueID + "' ORDER BY DateChanged DESC", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.WriteLine("<label id=\"" + ID + "Label\" for=\"" + ID + "\">" + SepFunctions.LangText("Select a Change Log Date/Time") + ":</label>");
                            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\" onchange=\"document.location.href = this.value\">");
                            switch (ModuleID)
                            {
                                case 5:
                                    output.WriteLine("<option value=\"discounts_modify.aspx?DiscountID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 9:
                                    output.WriteLine("<option value=\"faq_modify.aspx?FAQID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 10:
                                    output.WriteLine("<option value=\"downloads_modify.aspx?FileID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 14:
                                    output.WriteLine("<option value=\"guestbook_modify.aspx?EntryID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 18:
                                    output.WriteLine("<option value=\"matchmaker_modify.aspx?ProfileID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 19:
                                    output.WriteLine("<option value=\"linkdirectory_modify.aspx?LinkID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 20:
                                    output.WriteLine("<option value=\"business_modify.aspx?BusinessID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 23:
                                    output.WriteLine("<option value=\"news_modify.aspx?NewsID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 31:
                                    output.WriteLine("<option value=\"auction_modify.aspx?AdID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 32:
                                    output.WriteLine("<option value=\"realestate_modify.aspx?PropertyID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 35:
                                    output.WriteLine("<option value=\"articles_modify.aspx?ArticleID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 37:
                                    output.WriteLine("<option value=\"elearning_modify.aspx?CourseID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 41:
                                    output.WriteLine("<option value=\"shoppingmall_modify.aspx?ProductID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 44:
                                    output.WriteLine("<option value=\"classifiedads_modify.aspx?AdID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 46:
                                    output.WriteLine("<option value=\"eventcalendar_modify.aspx?EventID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 61:
                                    output.WriteLine("<option value=\"blogs_modify.aspx?BlogID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 63:
                                    output.WriteLine("<option value=\"userprofiles_modify.aspx?ProfileID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 65:
                                    output.WriteLine("<option value=\"vouchers_modify.aspx?VoucherID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;
                            }

                            while (RS.Read())
                            {
                                switch (ModuleID)
                                {
                                    case 5:
                                        href = "discounts_modify.aspx?DiscountID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 9:
                                        href = "faq_modify.aspx?FAQID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 10:
                                        href = "downloads_modify.aspx?FileID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 14:
                                        href = "guestbook_modify.aspx?EntryID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 18:
                                        href = "matchmaker_modify.aspx?ProfileID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 19:
                                        href = "linkdirectory_modify.aspx?LinkID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 20:
                                        href = "business_modify.aspx?BusinessID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 23:
                                        href = "news_modify.aspx?NewsID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 31:
                                        href = "auction_modify.aspx?AdID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 32:
                                        href = "realestate_modify.aspx?PropertyID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 35:
                                        href = "articles_modify.aspx?ArticleID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 37:
                                        href = "elearning_modify.aspx?CourseID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 41:
                                        href = "shoppingmall_modify.aspx?ProductID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 44:
                                        href = "classifiedads_modify.aspx?AdID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 46:
                                        href = "eventcalendar_modify.aspx?EventID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 61:
                                        href = "blogs_modify.aspx?BlogID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 63:
                                        href = "userprofiles_modify.aspx?ProfileID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;

                                    case 65:
                                        href = "vouchers_modify.aspx?VoucherID=" + UniqueID + "&ChangeID=" + SepFunctions.openNull(RS["ChangeID"]) + "&ModuleID=" + ModuleID;
                                        break;
                                }

                                output.WriteLine("<option value=\"" + href + "\"" + Strings.ToString(SepFunctions.openNull(RS["ChangeID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["DateChanged"]) + "</option>");
                            }

                            output.WriteLine("</select>");
                        }
                    }
                }
            }
        }
    }
}