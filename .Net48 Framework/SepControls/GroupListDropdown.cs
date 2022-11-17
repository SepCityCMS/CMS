// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="GroupListDropdown.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    /// Class GroupListDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:GroupListDropdown runat=server></{0}:GroupListDropdown>")]
    public class GroupListDropdown : WebControl
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
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess")) == false)
            {
                return;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
                output.WriteLine("<option value=\"\">" + SepFunctions.LangText("--- Select a Group List ---") + "</option>");

                using (var cmd = new SqlCommand("SELECT ListID,ListName FROM GroupLists WHERE Status <> -1 ORDER BY ListName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.WriteLine("<option value=\"" + SepFunctions.openNull(RS["ListID"]) + "\"" + Strings.ToString(SepFunctions.openNull(RS["ListID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["ListName"]) + "</option>");
                        }
                    }
                }

                output.WriteLine("</select>");
            }
        }
    }
}