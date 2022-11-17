// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AccessKeySelection.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class AccessKeySelection.
    /// </summary>
    public class AccessKeySelection
    {
        /// <summary>
        /// The m exclude everyone
        /// </summary>
        private string m_ExcludeEveryone;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the exclude everyone.
        /// </summary>
        /// <value>The exclude everyone.</value>
        public string ExcludeEveryone
        {
            get => Strings.ToString(m_ExcludeEveryone);

            set => m_ExcludeEveryone = value;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

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

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), true) == false)
            {
                return output.ToString();
            }

            double iHalf = 0;
            long aCount = 0;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Count(KeyID) AS RCount FROM AccessKeys WHERE Status <> -1", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            iHalf = Math.Ceiling(SepFunctions.toDouble(SepFunctions.openNull(RS["RCount"])) / 2);
                        }
                    }
                }

                output.AppendLine("<a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', 'checked');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', null);\">" + SepFunctions.LangText("Deselect All") + "</a>");

                output.AppendLine("<div class=\"MultiCheckboxDiv\" id=\"" + ID + "Checkboxes\">");
                output.AppendLine("<div class=\"MultiCheckboxDivLeft\">");

                string sqlStr = string.Empty;

                if (ExcludeEveryone == "True")
                {
                    sqlStr = "SELECT KeyID,KeyName FROM AccessKeys WHERE Status <> -1 AND KeyID <> '1' ORDER BY KeyName";
                }
                else
                {
                    sqlStr = "SELECT KeyID,KeyName FROM AccessKeys WHERE Status <> -1 ORDER BY KeyName";
                }

                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                aCount += 1;
                                output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + SepFunctions.openNull(RS["KeyID"]) + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + SepFunctions.openNull(RS["KeyID"]) + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/>");
                                output.AppendLine("<label for=\"" + ID + Strings.ToString(aCount) + "\" style=\"width:210px;overflow:hidden;white-space:nowrap;vertical-align:middle;\">" + SepFunctions.openNull(RS["KeyName"]) + "</label><br/>");
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