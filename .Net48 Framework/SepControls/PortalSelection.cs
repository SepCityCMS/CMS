// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="PortalSelection.cs" company="SepCity, Inc.">
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
    /// Class PortalSelection.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class PortalSelection : WebControl
    {
        /// <summary>
        /// The m exclude master
        /// </summary>
        private bool m_ExcludeMaster;

        /// <summary>
        /// The m only include
        /// </summary>
        private string m_OnlyInclude;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets a value indicating whether [exclude master].
        /// </summary>
        /// <value><c>true</c> if [exclude master]; otherwise, <c>false</c>.</value>
        public bool ExcludeMaster
        {
            get
            {
                var s = Convert.ToBoolean(m_ExcludeMaster);
                return s;
            }

            set => m_ExcludeMaster = value;
        }

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

                if (string.IsNullOrWhiteSpace(s))
                {
                    return "|-1|";
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
            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable")
            {
                return;
            }

            double iHalf = 0;
            long aCount = 0;

            var wclause = string.Empty;

            if (!string.IsNullOrWhiteSpace(OnlyInclude))
            {
                wclause += " AND PortalID IN ('" + Strings.Replace(Strings.Replace(OnlyInclude, "|", string.Empty), ",", "','") + "')";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Count(PortalID) AS RCount FROM Portals WHERE Status <> -1" + wclause, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            iHalf = Math.Ceiling((SepFunctions.toDouble(SepFunctions.openNull(RS["RCount"])) - 2) / 2);
                        }
                    }
                }

                output.WriteLine("<a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', 'checked');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', null);\">" + SepFunctions.LangText("Deselect All") + "</a>");

                output.WriteLine("<div class=\"MultiCheckboxDiv\" id=\"" + ID + "Checkboxes\">");
                output.WriteLine("<div class=\"MultiCheckboxDivLeft\">");

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    if (Strings.InStr(OnlyInclude, "|-1|") > 0)
                    {
                        aCount += 1;
                        output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Portals") + "</label><br/>");
                    }

                    if (Strings.InStr(OnlyInclude, "|0|") > 0 && ExcludeMaster == false)
                    {
                        aCount += 1;
                        output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|0|\"" + Strings.ToString(Strings.InStr(Text, "|0|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("Main Portal") + "</label><br/>");
                    }
                }
                else
                {
                    aCount += 1;
                    output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Portals") + "</label><br/>");
                    if (ExcludeMaster == false)
                    {
                        aCount += 1;
                        output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|0|\"" + Strings.ToString(Strings.InStr(Text, "|0|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("Main Portal") + "</label><br/>");
                    }
                }

                using (var cmd = new SqlCommand("SELECT PortalID,PortalTitle FROM Portals WHERE Status <> -1" + wclause + " ORDER BY PortalTitle", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            aCount += 1;
                            output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + SepFunctions.openNull(RS["PortalID"]) + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + SepFunctions.openNull(RS["PortalID"]) + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.openNull(RS["PortalTitle"]) + "</label><br/>");
                            if (aCount == iHalf)
                            {
                                // -V3024
                                output.WriteLine("</div><div class=\"MultiCheckboxDivRight\">");
                            }
                        }
                    }
                }

                output.WriteLine("</div>");
                output.WriteLine("</div>");
            }
        }
    }
}