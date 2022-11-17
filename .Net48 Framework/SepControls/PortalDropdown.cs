// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="PortalDropdown.cs" company="SepCity, Inc.">
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
    /// Class PortalDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PortalDropdown runat=server></{0}:PortalDropdown>")]
    public class PortalDropdown : WebControl
    {
        /// <summary>
        /// The m select width
        /// </summary>
        private string m_SelectWidth;

        /// <summary>
        /// The m show all portals
        /// </summary>
        private bool m_ShowAllPortals;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the width of the select.
        /// </summary>
        /// <value>The width of the select.</value>
        public string SelectWidth
        {
            get => Strings.ToString(m_SelectWidth);

            set => m_SelectWidth = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show all portals].
        /// </summary>
        /// <value><c>true</c> if [show all portals]; otherwise, <c>false</c>.</value>
        public bool ShowAllPortals
        {
            get
            {
                var s = Convert.ToBoolean(m_ShowAllPortals);
                return s;
            }

            set => m_ShowAllPortals = value;
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
            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable")
            {
                return;
            }

            var sInstallFolder = SepFunctions.GetInstallFolder(true);

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                output.WriteLine("<script src=\"https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.5/js/select2.min.js\" type=\"text/javascript\"></script>");
                output.WriteLine("<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.5/css/select2.min.css\">");
                output.WriteLine("<link rel=\"stylesheet\" href=\"" + sInstallFolder + "skins/public/styles/select2-bootstrap.css\">");

                output.WriteLine("<script type=\"text/javascript\">");
                output.WriteLine("$(document).ready(function() {");
                output.WriteLine("  $('#" + ID + "').select2({");
                output.WriteLine("    theme: 'bootstrap4',");
                output.WriteLine("    width: '100%'");
                output.WriteLine("  });");
                output.WriteLine("});");
                output.WriteLine("</script>");

                var sWidth = !string.IsNullOrWhiteSpace(SelectWidth) ? " style=\"width: " + SelectWidth + "\"" : string.Empty;

                output.WriteLine("<Select name=\"" + ID + "\" id=\"" + ID + "\"" + sWidth + " Class=\"" + CssClass + "\">");
                output.WriteLine("<option value=\"-1\">" + SepFunctions.LangText("All Portals") + "</option>");
                output.WriteLine("<option value=\"0\"" + Strings.ToString("0" == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Main Portal") + "</option>");
                using (var cmd = new SqlCommand("SELECT PortalID,PortalTitle FROM Portals WHERE Status <> -1 ORDER BY PortalTitle", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.WriteLine("<option value=\"" + SepFunctions.openNull(RS["PortalID"]) + "\"" + Strings.ToString(SepFunctions.openNull(RS["PortalID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["PortalTitle"]) + "</option>");
                        }
                    }
                }

                output.WriteLine("</select>");
            }
        }
    }
}