// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ModuleSelection.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ModuleSelection.
    /// </summary>
    public class ModuleSelection
    {
        /// <summary>
        /// The m module type
        /// </summary>
        private string m_ModuleType;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the type of the module.
        /// </summary>
        /// <value>The type of the module.</value>
        public string ModuleType
        {
            get => Strings.ToString(m_ModuleType);

            set => m_ModuleType = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                var s = Request.Item(ID);
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) == false)
            {
                return output.ToString();
            }

            double iHalf = 0;
            long aCount = 0;

            var arrPageIDs = new ArrayList();


            string wclause;
            switch (ModuleType)
            {
                case "Categories":
                    wclause = "AND ModuleID IN (35, 31, 20, 44, 5, 37, 9, 10, 12, 19, 52, 60, 41, 36, 65, 61, 23, 7)";
                    break;

                case "CustomFields":
                    wclause = "AND ModuleID IN (35, 31, 20, 44, 19, 18, 29, 7, 107, 32, 63)";
                    break;

                case "Reviews":
                    wclause = "AND ModuleID IN (18, 63)";
                    break;

                case "Approval":
                    wclause = "AND ModuleID IN (35, 20, 10, 19, 18, 52, 63, 999, 65)";
                    break;

                case "Portals":
                    wclause = "AND ModuleID IN (35, 31, 20, 44, 46, 13, 14, 40, 63, 56, 57, 23, 47, 18, 25, 28, 32, 50, 5, 37, 9, 10, 12, 19, 52, 41, 36, 65, 61, 23, 7)";
                    break;

                default:
                    wclause = "AND (UserPageName <> '' OR ModuleID='39' OR ModuleID='62' OR ModuleID='3') AND PageID < '200'";
                    break;
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT UniqueID,ModuleID FROM ModulesNPages WHERE Status <> -1 " + wclause + " ORDER BY LinkText", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                if (SepFunctions.ModuleActivated(Convert.ToInt64(SepFunctions.openNull(RS["ModuleID"]))))
                                {
                                    iHalf += 1;
                                    arrPageIDs.Add(SepFunctions.openNull(RS["UniqueID"]));
                                }
                            }

                            iHalf = Math.Ceiling((SepFunctions.toDouble(Strings.ToString(iHalf)) - 2) / 2);
                        }
                    }
                }

                output.AppendLine("<a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', 'checked');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', null);\">" + SepFunctions.LangText("Deselect All") + "</a>");

                output.AppendLine("<div class=\"MultiCheckboxDiv\" id=\"" + ID + "Checkboxes\">");
                output.AppendLine("<div class=\"MultiCheckboxDivLeft\">");

                for (var i = 0; i <= arrPageIDs.Count - 1; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT LinkText,ModuleID FROM ModulesNPages WHERE UniqueID=@UniqueID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", arrPageIDs[i].ToString());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                aCount += 1;
                                output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + SepFunctions.openNull(RS["ModuleID"]) + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + SepFunctions.openNull(RS["ModuleID"]) + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</label><br/>");
                                if (aCount == iHalf)
                                {
                                    // -V3024
                                    output.AppendLine("</div><div class=\"MultiCheckboxDivRight\">");
                                }
                            }
                        }
                    }
                }

                output.AppendLine("</div>");
                output.AppendLine("</div>");
            }

            return output.ToString();
        }
    }
}