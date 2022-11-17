// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ChangeLogDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ChangeLogDropdown.
    /// </summary>
    public class ChangeLogDropdown
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
            get => SepCommon.SepCore.Strings.ToString(m_ChangeUniqueID);

            set => m_ChangeUniqueID = value;
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

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
        public string Text
        {
            get
            {
                var s = SepCommon.SepCore.Request.Item(ID);
                if (s == null)
                {
                    s = SepCommon.SepCore.Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string UniqueID { get; set; }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), true) == false)
            {
                return output.ToString();
            }

            var href = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT ChangeID,DateChanged FROM ChangeLog WHERE ModuleID='" + ModuleID + "' AND UniqueID='" + UniqueID + "' ORDER BY DateChanged DESC", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.AppendLine("<label id=\"" + ID + "Label\" for=\"" + ID + "\">" + SepFunctions.LangText("Select a Change Log Date/Time") + ":</label>");
                            output.AppendLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\" onchange=\"document.location.href = this.value\">");
                            switch (ModuleID)
                            {
                                case 5:
                                    output.AppendLine("<option value=\"discounts_modify.aspx?DiscountID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 9:
                                    output.AppendLine("<option value=\"faq_modify.aspx?FAQID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 10:
                                    output.AppendLine("<option value=\"downloads_modify.aspx?FileID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 14:
                                    output.AppendLine("<option value=\"guestbook_modify.aspx?EntryID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 18:
                                    output.AppendLine("<option value=\"matchmaker_modify.aspx?ProfileID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 19:
                                    output.AppendLine("<option value=\"linkdirectory_modify.aspx?LinkID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 20:
                                    output.AppendLine("<option value=\"business_modify.aspx?BusinessID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 23:
                                    output.AppendLine("<option value=\"news_modify.aspx?NewsID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 31:
                                    output.AppendLine("<option value=\"auction_modify.aspx?AdID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 32:
                                    output.AppendLine("<option value=\"realestate_modify.aspx?PropertyID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 35:
                                    output.AppendLine("<option value=\"articles_modify.aspx?ArticleID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 37:
                                    output.AppendLine("<option value=\"elearning_modify.aspx?CourseID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 41:
                                    output.AppendLine("<option value=\"shoppingmall_modify.aspx?ProductID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 44:
                                    output.AppendLine("<option value=\"classifiedads_modify.aspx?AdID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 46:
                                    output.AppendLine("<option value=\"eventcalendar_modify.aspx?EventID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 61:
                                    output.AppendLine("<option value=\"blogs_modify.aspx?BlogID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 63:
                                    output.AppendLine("<option value=\"userprofiles_modify.aspx?ProfileID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
                                    break;

                                case 65:
                                    output.AppendLine("<option value=\"vouchers_modify.aspx?VoucherID=" + UniqueID + "&ModuleID=" + ModuleID + "\">" + SepFunctions.LangText("Current Saved Version") + "</option>");
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

                                output.AppendLine("<option value=\"" + href + "\"" + SepCommon.SepCore.Strings.ToString(SepFunctions.openNull(RS["ChangeID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["DateChanged"]) + "</option>");
                            }

                            output.AppendLine("</select>");
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}