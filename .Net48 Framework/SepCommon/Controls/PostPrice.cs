// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PostPrice.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class PostPrice.
    /// </summary>
    public class PostPrice
    {
        /// <summary>
        /// The m price unique identifier
        /// </summary>
        private string m_PriceUniqueID;

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
            get => SepCommon.SepCore.Strings.ToString(m_PriceUniqueID);

            set => m_PriceUniqueID = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (ModuleID == 0)
            {
                output.AppendLine("ModuleID is Required");
                return output.ToString();
            }

            if (string.IsNullOrWhiteSpace(PriceUniqueID))
            {
                output.AppendLine("PriceUniqueID is Required");
                return output.ToString();
            }

            var inidata = string.Empty;

            var fChecked = string.Empty;
            var bChecked = string.Empty;
            var hChecked = string.Empty;

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Description FROM ShopProducts WHERE ModuleID=@ModuleID AND ProductName='Posting Price'", conn))
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
                                output.AppendLine("<div style=\"width:300px;text-align:left;\" align=\"center\">");
                                output.Append(SepFunctions.LangText("Pricing Options:") + "<br/>");
                                using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM PricingOptions WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", conn))
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
                                    output.Append(SepFunctions.LangText("New Listing: Free") + "<br/>");
                                }
                                else
                                {
                                    output.Append(SepFunctions.LangText("New Listing Cost:") + " " + SepFunctions.Format_Currency(SepFunctions.ParseXML("NEWLISTING", inidata)) + "<br/>");
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("FEATURELISTING", inidata)) != SepFunctions.Format_Currency("0"))
                                {
                                    output.AppendLine("<input type=\"Checkbox\" name=\"PFeature\" value=\"1\"" + fChecked + "/> " + SepFunctions.LangText("Featured Listing") + " - " + SepFunctions.Format_Currency(SepFunctions.ParseXML("FEATURELISTING", inidata)) + "<br/>");
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("BOLDTITLE", inidata)) != SepFunctions.Format_Currency("0"))
                                {
                                    output.AppendLine("<input type=\"Checkbox\" name=\"PBold\" value=\"1\"" + bChecked + "/> " + SepFunctions.LangText("Bold Title") + " - " + SepFunctions.Format_Currency(SepFunctions.ParseXML("BOLDTITLE", inidata)) + "<br/>");
                                }

                                if (SepFunctions.Format_Currency(SepFunctions.ParseXML("HIGHLIGHTLISTING", inidata)) != SepFunctions.Format_Currency("0"))
                                {
                                    output.AppendLine("<input type=\"Checkbox\" name=\"PHighlight\" value=\"1\"" + hChecked + "/> " + SepFunctions.LangText("Highlight Listing") + " - " + SepFunctions.Format_Currency(SepFunctions.ParseXML("HIGHLIGHTLISTING", inidata)) + "<br/>");
                                }

                                output.AppendLine("</div>");
                                output.AppendLine("<hr width=\"100%\" size=\"1\" />");
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}