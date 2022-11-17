// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CustomFields.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class CustomFields.
    /// </summary>
    public class CustomFields
    {
        /// <summary>
        /// The m field unique identifier
        /// </summary>
        private string m_FieldUniqueID;

        /// <summary>
        /// The m is read only
        /// </summary>
        private bool m_isReadOnly;

        /// <summary>
        /// The m recurring cycle
        /// </summary>
        private string m_RecurringCycle;

        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

        /// <summary>
        /// Gets or sets the field unique identifier.
        /// </summary>
        /// <value>The field unique identifier.</value>
        public string FieldUniqueID
        {
            get => Strings.ToString(m_FieldUniqueID);

            set => m_FieldUniqueID = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool isReadOnly
        {
            get => Convert.ToBoolean(m_isReadOnly);

            set => m_isReadOnly = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the recurring cycle.
        /// </summary>
        /// <value>The recurring cycle.</value>
        public string RecurringCycle
        {
            get => Strings.ToString(m_RecurringCycle);

            set => m_RecurringCycle = value;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => Strings.ToString(m_UserID);

            set => m_UserID = value;
        }

        /// <summary>
        /// Customs the fields input.
        /// </summary>
        /// <param name="sAnswerType">Type of the s answer.</param>
        /// <param name="iOffset">The i offset.</param>
        /// <param name="intModuleID">The int module identifier.</param>
        /// <param name="intUniqueID">The int unique identifier.</param>
        /// <param name="GetCycle">The get cycle.</param>
        /// <param name="isReadOnly">if set to <c>true</c> [is read only].</param>
        /// <returns>System.String.</returns>
        public string Custom_Fields_Input(string sAnswerType, string iOffset, int intModuleID, string intUniqueID, string GetCycle, bool isReadOnly)
        {
            var str = new StringBuilder();

            var GetCustomAnswer = string.Empty;
            var GetUserFieldID = string.Empty;
            var ssel = string.Empty;
            string[] arrAnswer = null;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT UserFieldID,FieldValue FROM CustomFieldUsers WHERE FieldID=@FieldID AND UserID=@UserID AND ModuleID=@ModuleID AND UniqueID=@UniqueID AND PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@FieldID", iOffset);
                    if (string.IsNullOrWhiteSpace(UserID))
                    {
                        cmd.Parameters.AddWithValue("@UserID", string.Empty);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                    }

                    cmd.Parameters.AddWithValue("@ModuleID", intModuleID);
                    if (string.IsNullOrWhiteSpace(intUniqueID))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", string.Empty);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", intUniqueID);
                    }

                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            GetCustomAnswer = SepFunctions.openNull(RS["FieldValue"]);
                            GetUserFieldID = SepFunctions.openNull(RS["UserFieldID"]);
                        }
                    }
                }

                if (isReadOnly)
                {
                    switch (sAnswerType)
                    {
                        case "image":
                            str.Append("<img src=\"" + sImageFolder + "images/" + GetCustomAnswer + "\" alt=\"Image\" align=\"left\" />");
                            break;

                        default:
                            str.Append(GetCustomAnswer);
                            break;
                    }
                }
                else
                {
                    switch (sAnswerType)
                    {
                        case "ShortAnswer":
                            str.Append("<input type=\"text\" name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"textEntry\" value=\"" + SepFunctions.HTMLEncode(GetCustomAnswer) + "\" />");
                            break;

                        case "LongAnswer":
                            str.Append("<textarea name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"textareaEntry\">" + SepFunctions.HTMLEncode(GetCustomAnswer) + "</textarea>");
                            break;

                        case "DropdownM":
                            str.Append("<select name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"selectEntry\" style=\"height:70px;\" multiple=\"multiple\">");
                            using (SqlCommand cmd = new SqlCommand("SELECT * FROM CustomFieldOptions WHERE FieldID=@FieldID ORDER BY Weight,OptionName", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", iOffset);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    while (RS.Read())
                                    {
                                        if (Strings.InStr(GetCustomAnswer, ",") > 0)
                                        {
                                            arrAnswer = Strings.Split(GetCustomAnswer, ",");

                                            if (arrAnswer != null)
                                            {
                                                for (var i = 0; i <= Information.UBound(arrAnswer); i++)
                                                {
                                                    if (arrAnswer[i] == SepFunctions.openNull(RS["OptionValue"]))
                                                    {
                                                        ssel = " selected=\"selected\"";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        ssel = string.Empty;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (GetCustomAnswer == SepFunctions.openNull(RS["OptionValue"]))
                                            {
                                                ssel = " selected=\"selected\"";
                                            }
                                            else
                                            {
                                                ssel = string.Empty;
                                            }
                                        }

                                        str.Append("<option value=\"" + SepFunctions.openNull(RS["OptionValue"]) + "\"" + ssel + ">" + SepFunctions.openNull(RS["OptionName"]) + Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])) != 0 || SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])) != 0 ? " ( Additional " + SepFunctions.Format_Long_Price(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])), SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])), GetCycle) + ")" : string.Empty) + "</option>");
                                    }
                                }
                            }

                            str.Append("</select>");
                            break;

                        case "DropdownS":
                            str.Append("<select name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"selectEntry\">");
                            using (SqlCommand cmd = new SqlCommand("SELECT * FROM CustomFieldOptions WHERE FieldID=@FieldID ORDER BY Weight,OptionName", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", iOffset);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    str.Append("<option value=\"\">" + SepFunctions.LangText("--- Make Your Selection ---") + "</option>");
                                    while (RS.Read())
                                    {
                                        if (GetCustomAnswer == SepFunctions.openNull(RS["OptionValue"]))
                                        {
                                            ssel = " selected=\"selected\"";
                                        }
                                        else
                                        {
                                            ssel = string.Empty;
                                        }

                                        str.Append("<option value=\"" + SepFunctions.openNull(RS["OptionValue"]) + "\"" + ssel + ">" + SepFunctions.openNull(RS["OptionName"]) + Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])) != 0 || SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])) != 0 ? " (Additional " + SepFunctions.Format_Long_Price(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])), SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])), GetCycle) + ")" : string.Empty) + "</option>");
                                    }
                                }
                            }

                            str.Append("</select>");
                            break;

                        case "Radio":
                            if (intModuleID != 41)
                            {
                                str.Append("<div style=\"margin-left:20px\">");
                            }

                            using (SqlCommand cmd = new SqlCommand("SELECT * FROM CustomFieldOptions WHERE FieldID=@FieldID ORDER BY Weight,OptionName", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", iOffset);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    while (RS.Read())
                                    {
                                        if (GetCustomAnswer == SepFunctions.openNull(RS["OptionValue"]))
                                        {
                                            ssel = " checked=\"checked\"";
                                        }
                                        else
                                        {
                                            ssel = string.Empty;
                                        }

                                        str.Append("<input type=\"radio\" name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"radioEntry\" value=\"" + SepFunctions.openNull(RS["OptionValue"]) + "\"" + ssel + " /> " + SepFunctions.openNull(RS["OptionName"]) + Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])) != 0 || SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])) != 0 ? " (Additional " + SepFunctions.Format_Long_Price(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])), SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])), GetCycle) + ")" : string.Empty) + "<br/>");
                                    }
                                }
                            }

                            if (intModuleID != 41)
                            {
                                str.Append("</div>");
                            }

                            break;

                        case "Checkbox":
                            if (intModuleID != 41)
                            {
                                str.Append("<div style=\"margin-left:20px\">");
                            }

                            using (SqlCommand cmd = new SqlCommand("SELECT * FROM CustomFieldOptions WHERE FieldID=@FieldID ORDER BY Weight,OptionName", conn))
                            {
                                cmd.Parameters.AddWithValue("@FieldID", iOffset);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    while (RS.Read())
                                    {
                                        if (Strings.InStr(GetCustomAnswer, ",") > 0)
                                        {
                                            arrAnswer = Strings.Split(GetCustomAnswer, ",");

                                            if (arrAnswer != null)
                                            {
                                                for (var i = 0; i <= Information.UBound(arrAnswer); i++)
                                                {
                                                    if (arrAnswer[i] == SepFunctions.openNull(RS["OptionValue"]))
                                                    {
                                                        ssel = " checked=\"checked\"";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        ssel = string.Empty;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (GetCustomAnswer == SepFunctions.openNull(RS["OptionValue"]))
                                            {
                                                ssel = " checked=\"checked\"";
                                            }
                                            else
                                            {
                                                ssel = string.Empty;
                                            }
                                        }

                                        str.Append("<input type=\"checkbox\" name=\"Custom" + iOffset + "\" id=\"Custom" + SepFunctions.openNull(RS["OptionID"]) + "\" class=\"checkboxEntry\" value=\"" + SepFunctions.openNull(RS["OptionValue"]) + "\"" + ssel + " />");
                                        str.Append("<label for=\"Custom" + SepFunctions.openNull(RS["OptionID"]) + "\">" + SepFunctions.openNull(RS["OptionName"]) + Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])) != 0 || SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])) != 0 ? " (Additional " + SepFunctions.Format_Long_Price(SepFunctions.toLong(SepFunctions.openNull(RS["Price"])), SepFunctions.toLong(SepFunctions.openNull(RS["RecurringPrice"])), GetCycle) + ")" : string.Empty) + "</label><br/>");
                                    }
                                }
                            }

                            if (intModuleID != 41)
                            {
                                str.Append("</div>");
                            }

                            break;

                        case "Date":
                            str.Append("<input type=\"text\" name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"textEntry\" value=\"" + SepFunctions.HTMLEncode(GetCustomAnswer) + "\" />");
                            str.Append("<script type=\"text/javascript\">$('#Custom" + iOffset + "').datetimepicker({ timepicker: false, format: 'd/m/Y' });</script>");
                            break;

                        case "Image":
                            if (string.IsNullOrWhiteSpace(GetCustomAnswer))
                            {
                                str.Append("<input type=\"file\" name=\"Custom" + iOffset + "\" id=\"Custom" + iOffset + "\" class=\"fileEntry\" />");
                            }
                            else
                            {
                                str.Append("<img src=\"" + sImageFolder + "images/" + GetCustomAnswer + "\" alt=\"Image\" align=\"left\" />");
                                str.Append("<a href=\"" + sInstallFolder + "userpages.aspx?DoAction=RemoveImage&FieldID=" + SepFunctions.UrlEncode(GetUserFieldID) + "\">" + SepFunctions.LangText("Delete Image") + "</a>");
                            }

                            break;
                    }
                }
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var wc = string.Empty;
            var tmpSectionID = string.Empty;

            if (ModuleID == 0)
            {
                output.AppendLine("ModuleID is Required");
                return output.ToString();
            }

            if (ModuleID == 41 && !string.IsNullOrWhiteSpace(FieldUniqueID))
            {
                wc += " AND UniqueIDs LIKE '%|" + FieldUniqueID + "|%'";
            }

            if (ModuleID != 41)
            {
                wc += " AND (PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0)";
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM CustomFields WHERE ModuleIDs LIKE '%|" + ModuleID + "|%' AND Status <> -1" + wc + " ORDER BY SectionID,Weight,FieldName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            if (ModuleID == 41)
                            {
                                output.AppendLine("<tr>");
                                output.AppendLine("<td colspan=\"2\" class=\"TitleText\">" + SepFunctions.LangText("Please complete the following options.") + "<br/><br/></td>");
                                output.AppendLine("</tr>");
                            }

                            while (RS.Read())
                            {
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["SectionID"])) && tmpSectionID != SepFunctions.openNull(RS["SectionID"]))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM CustomSections WHERE SectionID=@SectionID AND Status <> -1", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@SectionID", SepFunctions.openNull(RS["SectionID"]));
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                output.AppendLine("<div class=\"SectionTitle\">" + SepFunctions.openNull(RS2["SectionName"]) + "</div>");
                                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS2["SectionText"])))
                                                {
                                                    output.AppendLine("<div class=\"SectionText\">" + SepFunctions.openNull(RS2["SectionText"]) + "</div>");
                                                }
                                            }
                                        }
                                    }
                                }

                                switch (ModuleID)
                                {
                                    case 7:
                                        output.AppendLine("<label>" + SepFunctions.openNull(RS["FieldName"]) + ":</label>");
                                        output.Append(Custom_Fields_Input(SepFunctions.openNull(RS["AnswerType"]), SepFunctions.openNull(RS["FieldID"]), ModuleID, FieldUniqueID, RecurringCycle, isReadOnly));
                                        break;

                                    case 41:
                                        output.AppendLine("<tr>");
                                        output.AppendLine("<td width=\"30%\" valign=\"top\" style=\"padding:5px 0 5px 0\">" + Strings.ToString(SepFunctions.openBoolean(RS["Required"]) ? "<span class=\"requiredIcon\">*</span>" : string.Empty) + SepFunctions.openNull(RS["FieldName"]) + "</td>");
                                        output.AppendLine("<td width=\"70%\" valign=\"top\" style=\"padding:5px 0 5px 0\">" + Custom_Fields_Input(SepFunctions.openNull(RS["AnswerType"]), SepFunctions.openNull(RS["FieldID"]), ModuleID, FieldUniqueID, RecurringCycle, isReadOnly) + "</td>");
                                        output.AppendLine("</tr>");
                                        break;

                                    default:
                                        output.AppendLine("<p>");
                                        output.AppendLine("<label for=\"Custom" + SepFunctions.openNull(RS["FieldID"]) + "\" id=\"Custom" + SepFunctions.openNull(RS["FieldID"]) + "Label\">" + SepFunctions.openNull(RS["FieldName"]) + ":</label>");
                                        output.Append(Custom_Fields_Input(SepFunctions.openNull(RS["AnswerType"]), SepFunctions.openNull(RS["FieldID"]), ModuleID, FieldUniqueID, RecurringCycle, isReadOnly));
                                        output.AppendLine("</p>");
                                        break;
                                }

                                tmpSectionID = SepFunctions.openNull(RS["SectionID"]);
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}