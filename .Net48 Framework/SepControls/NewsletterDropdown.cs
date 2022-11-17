// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="NewsletterDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class NewsletterDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:NewsletterDropdown runat=server></{0}:NewsletterDropdown>")]
    public class NewsletterDropdown : WebControl
    {
        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

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
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (SepFunctions.Setup(24, "NewsLetEnable") != "Enable")
            {
                return;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
                output.WriteLine("<option value=\"\">" + SepFunctions.LangText("Select a Newsletter") + "</option>");
                using (var cmd = new SqlCommand("SELECT LetterID,NewsletName FROM Newsletters WHERE PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0 ORDER BY NewsletName", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.WriteLine("<option value=\"" + SepFunctions.openNull(RS["LetterID"]) + "\"" + Strings.ToString(Text == SepFunctions.openNull(RS["LetterID"]) ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["NewsletName"]) + "</option>");
                        }
                    }
                }

                output.WriteLine("</select>");
            }
        }
    }
}