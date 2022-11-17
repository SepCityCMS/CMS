// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="AccessKeySelection.cs" company="SepCity, Inc.">
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
    /// Class AccessKeySelection.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class AccessKeySelection : WebControl
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

            double iHalf = 0;
            long aCount = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT Count(KeyID) AS RCount FROM AccessKeys WHERE Status <> -1", conn))
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

                output.WriteLine("<a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', 'checked');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:void(0)\" onclick=\"$('#" + ID + "Checkboxes').find(':checkbox').prop('checked', null);\">" + SepFunctions.LangText("Deselect All") + "</a>");

                output.WriteLine("<div class=\"MultiCheckboxDiv\" id=\"" + ID + "Checkboxes\">");
                output.WriteLine("<div class=\"MultiCheckboxDivLeft\">");

                var sqlStr = string.Empty;

                if (ExcludeEveryone == "True")
                {
                    sqlStr = "SELECT KeyID,KeyName FROM AccessKeys WHERE Status <> -1 AND KeyID <> '1' ORDER BY KeyName";
                }
                else
                {
                    sqlStr = "SELECT KeyID,KeyName FROM AccessKeys WHERE Status <> -1 ORDER BY KeyName";
                }

                using (var cmd = new SqlCommand(sqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            aCount += 1;
                            output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + SepFunctions.openNull(RS["KeyID"]) + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + SepFunctions.openNull(RS["KeyID"]) + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/>");
                            output.WriteLine("<label for=\"" + ID + Strings.ToString(aCount) + "\" style=\"width:210px;overflow:hidden;white-space:nowrap;vertical-align:middle;\">" + SepFunctions.openNull(RS["KeyName"]) + "</label><br/>");
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