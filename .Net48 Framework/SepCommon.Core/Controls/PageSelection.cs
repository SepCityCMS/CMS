// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PageSelection.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class PageSelection.
    /// </summary>
    public class PageSelection
    {
        /// <summary>
        /// The m only include
        /// </summary>
        private string m_OnlyInclude;

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
        /// Gets or sets the only include.
        /// </summary>
        /// <value>The only include.</value>
        public string OnlyInclude
        {
            get => Strings.ToString(m_OnlyInclude);

            set => m_OnlyInclude = value;
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

            var showRow = false;
            var arrPageIDs = new ArrayList();
            var sUniqueID = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    var arrOnlyInclude = Strings.Split(OnlyInclude, ",");
                    if (arrOnlyInclude != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrOnlyInclude); i++)
                        {
                            iHalf = iHalf + 1;
                            arrPageIDs.Add(Strings.Replace(arrOnlyInclude[i], "|", string.Empty));
                        }
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT UniqueID,ModuleID,PageID FROM ModulesNPages WHERE (UserPageName <> '' OR ModuleID='39' OR ModuleID='62' OR ModuleID='3') AND PageID <> '201' AND Status <> -1 ORDER BY LinkText", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                showRow = false;
                                if (SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"])) > 0)
                                {
                                    if (SepFunctions.ModuleActivated(SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]))))
                                    {
                                        showRow = true;
                                    }
                                }
                                else
                                {
                                    showRow = true;
                                }

                                if (showRow)
                                {
                                    iHalf = iHalf + 1;
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) < 200)
                                    {
                                        arrPageIDs.Add(SepFunctions.openNull(RS["PageID"]));
                                    }
                                    else
                                    {
                                        arrPageIDs.Add(SepFunctions.openNull(RS["UniqueID"]));
                                    }
                                }
                            }
                        }
                    }
                }

                output.AppendLine("<script type=\"text/javascript\" language=\"JavaScript\">");
                output.AppendLine("function " + ID + "Selection(isSelect) {");
                output.AppendLine("$(\"#div" + ID + " input[type='checkbox']\").each(function () {");
                output.AppendLine("if(isSelect == 'select') {");
                output.AppendLine("$(this).prop('checked', true);");
                output.AppendLine("} else {");
                output.AppendLine("$(this).prop('checked', false);");
                output.AppendLine("}");
                output.AppendLine("});");
                output.AppendLine("}");
                output.AppendLine("</script>");

                output.AppendLine("<a href=\"javascript:" + ID + "Selection('select');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:" + ID + "Selection('deselect');\">" + SepFunctions.LangText("De-Select All") + "</a>");
                output.AppendLine("<br/>");
                output.AppendLine("<div class=\"MultiCheckboxDiv\" id=\"div" + ID + "\">");
                output.AppendLine("<div class=\"MultiCheckboxDivLeft\">");
                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    if (Strings.InStr(OnlyInclude, "|-1|") > 0)
                    {
                        aCount = aCount + 1;
                        iHalf = iHalf + 1;
                        output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Pages") + "</label><br/>");
                    }
                }
                else
                {
                    aCount = aCount + 1;
                    iHalf = iHalf + 1;
                    output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Pages") + "</label><br/>");
                }

                if (iHalf > 2)
                {
                    iHalf = SepFunctions.toLong(Strings.ToString(Math.Ceiling(SepFunctions.toDouble(Strings.ToString(iHalf)) / 2)));
                }

                for (var i = 0; i <= arrPageIDs.Count - 1; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT LinkText,UniqueID,ModuleID,PageID FROM ModulesNPages WHERE (UniqueID=@UniqueID OR PageID=@UniqueID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", arrPageIDs[i].ToString());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                aCount = aCount + 1;
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) < 200)
                                {
                                    sUniqueID = SepFunctions.openNull(RS["PageID"]);
                                }
                                else
                                {
                                    sUniqueID = SepFunctions.openNull(RS["UniqueID"]);
                                }

                                output.AppendLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + sUniqueID + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + sUniqueID + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</label><br/>");
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