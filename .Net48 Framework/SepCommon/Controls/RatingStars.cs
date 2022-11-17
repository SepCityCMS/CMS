// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="RatingStars.cs" company="SepCity, Inc.">
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
    /// Class RatingStars.
    /// </summary>
    public class RatingStars
    {
        /// <summary>
        /// The m disable ajax
        /// </summary>
        private bool m_disableAJAX;

        /// <summary>
        /// The m is read only
        /// </summary>
        private bool m_isReadOnly;

        /// <summary>
        /// The m lookup identifier
        /// </summary>
        private string m_LookupID;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets a value indicating whether [disable ajax].
        /// </summary>
        /// <value><c>true</c> if [disable ajax]; otherwise, <c>false</c>.</value>
        public bool disableAJAX
        {
            get => m_disableAJAX;

            set => m_disableAJAX = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool isReadOnly
        {
            get => m_isReadOnly;

            set => m_isReadOnly = value;
        }

        /// <summary>
        /// Gets or sets the lookup identifier.
        /// </summary>
        /// <value>The lookup identifier.</value>
        public string LookupID
        {
            get => SepCommon.SepCore.Strings.ToString(m_LookupID);

            set => m_LookupID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                var s = SepCommon.SepCore.Request.Item(ID);
                if (s == null)
                {
                    s = SepCommon.SepCore.Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
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

            if (string.IsNullOrWhiteSpace(LookupID))
            {
                output.AppendLine("LookupID is Required");
                return output.ToString();
            }

            var alreadyRated = SepFunctions.Rating_Check(ModuleID, LookupID);

            var sControlId = ID + LookupID;

            var sImageFolder = SepFunctions.GetInstallFolder(true);
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (SepFunctions.Setup(8, "CNRREnable") == "Yes" || disableAJAX)
            {
                output.AppendLine("<script type=\"text/javascript\" src=\"" + sImageFolder + "js/rateit/jquery.rateit.min.js\"></script>");
                output.AppendLine("<link href=\"" + sImageFolder + "js/rateit/rateit.css\" rel=\"stylesheet\" type=\"text/css\">");

                long intTotalStars = 0;
                long intPossible = 0;

                using (SqlConnection RateConn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    RateConn.Open();
                    using (SqlCommand Ratecmd = new SqlCommand("SELECT Stars FROM Ratings WHERE ModuleID=@ModuleID AND UniqueID=@UniqueID", RateConn))
                    {
                        Ratecmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        Ratecmd.Parameters.AddWithValue("@UniqueID", LookupID);
                        using (SqlDataReader RateRS = Ratecmd.ExecuteReader())
                        {
                            while (RateRS.Read())
                            {
                                intTotalStars += SepFunctions.toInt(SepFunctions.openNull(RateRS["Stars"]));
                                intPossible += 1;
                            }
                        }
                    }
                }

                long TotalStars = 0;

                if (intPossible > 0)
                {
                    TotalStars = intTotalStars / intPossible;
                }

                if (isReadOnly == false)
                {
                    output.AppendLine("<script type=\"text/javascript\">");
                    output.AppendLine("$(document).ready(function () {");
                    output.AppendLine("$('.rateit').bind('rated reset', function (e) {");
                    if (disableAJAX)
                    {
                        output.AppendLine("\tvar ri = $(this);");
                        output.AppendLine("\tvar value = ri.rateit('value');");
                        output.AppendLine("\tri.rateit('readonly', true);");
                        output.AppendLine("$('#" + ID + "').val(value);");
                    }
                    else
                    {
                        if (alreadyRated == false)
                        {
                            output.AppendLine("\tvar ri = $(this);");
                            output.AppendLine("\tvar value = ri.rateit('value');");
                            output.AppendLine("\tri.rateit('readonly', true);");
                            output.AppendLine("\t$.ajax({");
                            output.AppendLine("\t\turl: '" + sInstallFolder + "rating_save.aspx?ModuleID=" + ModuleID + "&UniqueID=" + LookupID + "&Rating=' + value,");
                            output.AppendLine("\t\ttype: 'POST',");
                            output.AppendLine("\t\tsuccess: function (data) {");
                            output.AppendLine("        alert(data);");
                            output.AppendLine("\t\t}");
                            output.AppendLine("\t});");
                        }
                        else
                        {
                            output.AppendLine("alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("You have already rated this content.")) + "'));");
                        }
                    }

                    output.AppendLine("});");
                    output.AppendLine("});");
                    output.AppendLine("</script>");

                    var select1 = TotalStars == 1 ? " selected=\"selected\"" : string.Empty;
                    var select2 = TotalStars == 2 ? " selected=\"selected\"" : string.Empty;
                    var select3 = TotalStars == 3 ? " selected=\"selected\"" : string.Empty;
                    var select4 = TotalStars == 4 ? " selected=\"selected\"" : string.Empty;
                    var select5 = TotalStars == 5 ? " selected=\"selected\"" : string.Empty;
                    var sReadOnly = isReadOnly == false && disableAJAX == false ? " <span>" + SepFunctions.LangText("(Click on stars to rate)") + "</span>" : string.Empty; // -V3063
                    var sRated = alreadyRated && disableAJAX == false ? " data-rateit-readonly=\"true\"" : string.Empty;

                    output.AppendLine("<div style=\"margin: 5px 0px 5px 0px;\"><span style=\"display:inline-block;margin-top:3px;\"><select name=\"" + sControlId + "\" id=\"" + sControlId + "\" class=\"ignore\">");
                    output.AppendLine("<option value=\"1\"" + select1 + ">" + SepFunctions.LangText("Below Average") + "</option>");
                    output.AppendLine("<option value=\"2\"" + select2 + ">" + SepFunctions.LangText("Average") + "</option>");
                    output.AppendLine("<option value=\"3\"" + select3 + ">" + SepFunctions.LangText("Above Average") + "</option>");
                    output.AppendLine("<option value=\"4\"" + select4 + ">" + SepFunctions.LangText("Awesome") + "</option>");
                    output.AppendLine("<option value=\"5\"" + select5 + ">" + SepFunctions.LangText("Epic") + "</option>");
                    output.AppendLine("</select></span>" + sReadOnly + "</div>");
                    output.AppendLine("<div class=\"rateit\" data-rateit-backingfld=\"#" + sControlId + "\" data-rateit-resetable=\"false\" data-rateit-min=\"0\"" + sRated + "></div>");
                    if (disableAJAX)
                    {
                        output.AppendLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"" + Text + "\" />");
                    }
                }
            }

            return output.ToString();
        }
    }
}