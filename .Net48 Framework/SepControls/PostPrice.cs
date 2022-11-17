// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="PostPrice.cs" company="SepCity, Inc.">
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
    /// Class PostPrice.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PostPrice runat=server></{0}:PostPrice>")]
    public class PostPrice : WebControl
    {
        /// <summary>
        /// The m price unique identifier
        /// </summary>
        private string m_PriceUniqueID;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the price unique identifier.
        /// </summary>
        /// <value>The price unique identifier.</value>
        public string PriceUniqueID
        {
            get => Strings.ToString(m_PriceUniqueID);

            set => m_PriceUniqueID = value;
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
            get => Strings.ToString(m_Text);

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
            if (ModuleID == 0)
            {
                output.WriteLine("ModuleID is Required");
                return;
            }

            if (string.IsNullOrWhiteSpace(PriceUniqueID))
            {
                output.WriteLine("PriceUniqueID is Required");
                return;
            }

            var inidata = string.Empty;

            var fChecked = string.Empty;
            var bChecked = string.Empty;
            var hChecked = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Description FROM ShopProducts WHERE ModuleID=@ModuleID AND ProductName='Posting Price'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            inidata = SepFunctions.openNull(RS["Description"]);
                            if (SepFunctions.Format_Currency(SepFunctions.ParseXML("NEWLISTING", inidata)) != SepFunctions.Format_Currency("0") || SepFunctions.Format_Currency(SepFunctions.ParseXML("FEATURELISTING", inidata)) != SepFunctions.Format_Currency("0") || SepFunctions.Format_Currency(SepFunctions.ParseXML("BOLDTITLE", inidata)) != SepFunctions.Format_Currency("0") || SepFunctions.Format_Currency(SepFunctions.ParseXML("HIGHLIGHTLISTING", inidata)) != SepFunctions.Format_Currency("0"))
                            {
                                output.WriteLine("<div style=\"width:300px;text-align:left;\" align=\"center\">");
                                output.Write(SepFunctions.LangText("Pricing Options:") + "<br/>");
                                using (var cmd2 = new SqlCommand("SELECT * FROM PricingOptions WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ModuleID", ModuleID);
                                    cmd2.Parameters.AddWithValue("@UniqueID", PriceUniqueID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            RS2.Read();
                                            if (SepFunctions.openBoolean(RS2["Featured"]))
                                            {
                                                fChecked = " checked=\"checked\"";
                                            }

                                            if (SepFunctions.openBoolean(RS2["BoldTitle"]))
                                            {
                                                bChecked = " checked=\"checked\"";
                                            }

                                            if (SepFunctions.openBoolean(RS2["Highlight"]))
                                            {
                                                hChecked = " checked=\"checked\"";
                                            }
                                        }
                                    }
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("NEWLISTING", inidata)) == SepFunctions.Format_Currency("0"))
                                {
                                    output.Write(SepFunctions.LangText("New Listing: Free") + "<br/>");
                                }
                                else
                                {
                                    output.Write(SepFunctions.LangText("New Listing Cost:") + " " + SepFunctions.Format_Currency(SepFunctions.ParseXML("NEWLISTING", inidata)) + "<br/>");
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("FEATURELISTING", inidata)) != SepFunctions.Format_Currency("0"))
                                {
                                    output.WriteLine("<input type=\"Checkbox\" name=\"PFeature\" value=\"1\"" + fChecked + "/> " + SepFunctions.LangText("Featured Listing") + " - " + SepFunctions.Format_Currency(SepFunctions.ParseXML("FEATURELISTING", inidata)) + "<br/>");
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("BOLDTITLE", inidata)) != SepFunctions.Format_Currency("0"))
                                {
                                    output.WriteLine("<input type=\"Checkbox\" name=\"PBold\" value=\"1\"" + bChecked + "/> " + SepFunctions.LangText("Bold Title") + " - " + SepFunctions.Format_Currency(SepFunctions.ParseXML("BOLDTITLE", inidata)) + "<br/>");
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("HIGHLIGHTLISTING", inidata)) != SepFunctions.Format_Currency("0"))
                                {
                                    output.WriteLine("<input type=\"Checkbox\" name=\"PHighlight\" value=\"1\"" + hChecked + "/> " + SepFunctions.LangText("Highlight Listing") + " - " + SepFunctions.Format_Currency(SepFunctions.ParseXML("HIGHLIGHTLISTING", inidata)) + "<br/>");
                                }

                                output.WriteLine("</div>");
                                output.WriteLine("<hr width=\"100%\" size=\"1\" />");
                            }
                        }
                    }
                }
            }
        }
    }
}