// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="AccessClassDropdown.cs" company="SepCity, Inc.">
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
    /// Class AccessClassDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:AccessClassDropdown runat=server></{0}:AccessClassDropdown>")]
    public class AccessClassDropdown : WebControl
    {
        /// <summary>
        /// The m select width.
        /// </summary>
        private string m_SelectWidth;

        /// <summary>
        /// The m show delete account.
        /// </summary>
        private string m_showDeleteAccount;

        /// <summary>
        /// The m text.
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
        /// Gets or sets the show delete account.
        /// </summary>
        /// <value>The show delete account.</value>
        public string showDeleteAccount
        {
            get => Strings.ToString(m_showDeleteAccount);

            set => m_showDeleteAccount = value;
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

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                var sWidth = !string.IsNullOrWhiteSpace(SelectWidth) ? " style=\"width:" + SelectWidth + "\"" : string.Empty;
                output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\"" + sWidth + ">");
                output.WriteLine("<option value=\"\">" + SepFunctions.LangText("--- Select a Access Class ---") + "</option>");

                if (Strings.LCase(showDeleteAccount) == "true")
                {
                    output.WriteLine("<option value=\"DeleteAccount\"" + Strings.ToString("DeleteAccount" == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Delete Account") + "</option>");
                }

                var excludeAdmin = SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity")) == false ? " AND ClassID <> '2'" : string.Empty;
                using (var cmd = new SqlCommand("SELECT ClassID,ClassName FROM AccessClasses WHERE Status <> -1" + excludeAdmin + " ORDER BY ClassName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            output.WriteLine("<option value=\"" + SepFunctions.openNull(RS["ClassID"]) + "\"" + Strings.ToString(SepFunctions.openNull(RS["ClassID"]) == Text ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(RS["ClassName"]) + "</option>");
                        }
                    }
                }

                output.WriteLine("</select>");
            }
        }
    }
}