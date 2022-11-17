// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="RatingStars.cs" company="SepCity, Inc.">
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
    /// Class RatingStars.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RatingStars runat=server></{0}:RatingStars>")]
    public class RatingStars : WebControl
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
            get
            {
                try
                {
                    return Convert.ToBoolean(m_disableAJAX);
                }
                catch
                {
                    return false;
                }
            }

            set => m_disableAJAX = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool isReadOnly
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(m_isReadOnly);
                }
                catch
                {
                    return false;
                }
            }

            set => m_isReadOnly = value;
        }

        /// <summary>
        /// Gets or sets the lookup identifier.
        /// </summary>
        /// <value>The lookup identifier.</value>
        public string LookupID
        {
            get => Strings.ToString(m_LookupID);

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
            if (ModuleID == 0)
            {
                output.WriteLine("ModuleID is Required");
                return;
            }

            if (string.IsNullOrWhiteSpace(LookupID))
            {
                output.WriteLine("LookupID is Required");
                return;
            }

            var alreadyRated = SepFunctions.Rating_Check(ModuleID, LookupID);

            var sControlId = ID + LookupID;

            var sImageFolder = SepFunctions.GetInstallFolder(true);
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (SepFunctions.Setup(8, "CNRREnable") == "Yes" || disableAJAX)
            {
                output.WriteLine("<script type=\"text/javascript\" src=\"" + sImageFolder + "js/rateit/jquery.rateit.min.js\"></script>");
                output.WriteLine("<link href=\"" + sImageFolder + "js/rateit/rateit.css\" rel=\"stylesheet\" type=\"text/css\">");

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
                    output.WriteLine("<script type=\"text/javascript\">");
                    output.WriteLine("$(document).ready(function () {");
                    output.WriteLine("$('.rateit').bind('rated reset', function (e) {");
                    if (disableAJAX)
                    {
                        output.WriteLine("\tvar ri = $(this);");
                        output.WriteLine("\tvar value = ri.rateit('value');");
                        output.WriteLine("\tri.rateit('readonly', true);");
                        output.WriteLine("$('#" + ID + "').val(value);");
                    }
                    else
                    {
                        if (alreadyRated == false)
                        {
                            output.WriteLine("\tvar ri = $(this);");
                            output.WriteLine("\tvar value = ri.rateit('value');");
                            output.WriteLine("\tri.rateit('readonly', true);");
                            output.WriteLine("\t$.ajax({");
                            output.WriteLine("\t\turl: '" + sInstallFolder + "rating_save.aspx?ModuleID=" + ModuleID + "&UniqueID=" + LookupID + "&Rating=' + value,");
                            output.WriteLine("\t\ttype: 'POST',");
                            output.WriteLine("\t\tsuccess: function (data) {");
                            output.WriteLine("        alert(data);");
                            output.WriteLine("\t\t}");
                            output.WriteLine("\t});");
                        }
                        else
                        {
                            output.WriteLine("alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("You have already rated this content.")) + "'));");
                        }
                    }

                    output.WriteLine("});");
                    output.WriteLine("});");
                    output.WriteLine("</script>");

                    var select1 = TotalStars == 1 ? " selected=\"selected\"" : string.Empty;
                    var select2 = TotalStars == 2 ? " selected=\"selected\"" : string.Empty;
                    var select3 = TotalStars == 3 ? " selected=\"selected\"" : string.Empty;
                    var select4 = TotalStars == 4 ? " selected=\"selected\"" : string.Empty;
                    var select5 = TotalStars == 5 ? " selected=\"selected\"" : string.Empty;
                    var sReadOnly = isReadOnly == false && disableAJAX == false ? " <span>" + SepFunctions.LangText("(Click on stars to rate)") + "</span>" : string.Empty; // -V3063
                    var sRated = alreadyRated && disableAJAX == false ? " data-rateit-readonly=\"true\"" : string.Empty;

                    output.WriteLine("<div style=\"margin: 5px 0px 5px 0px;\"><span style=\"display:inline-block;margin-top:3px;\"><select name=\"" + sControlId + "\" id=\"" + sControlId + "\" class=\"ignore\">");
                    output.WriteLine("<option value=\"1\"" + select1 + ">" + SepFunctions.LangText("Below Average") + "</option>");
                    output.WriteLine("<option value=\"2\"" + select2 + ">" + SepFunctions.LangText("Average") + "</option>");
                    output.WriteLine("<option value=\"3\"" + select3 + ">" + SepFunctions.LangText("Above Average") + "</option>");
                    output.WriteLine("<option value=\"4\"" + select4 + ">" + SepFunctions.LangText("Awesome") + "</option>");
                    output.WriteLine("<option value=\"5\"" + select5 + ">" + SepFunctions.LangText("Epic") + "</option>");
                    output.WriteLine("</select></span>" + sReadOnly + "</div>");
                    output.WriteLine("<div class=\"rateit\" data-rateit-backingfld=\"#" + sControlId + "\" data-rateit-resetable=\"false\" data-rateit-min=\"0\"" + sRated + "></div>");
                    if (disableAJAX)
                    {
                        output.WriteLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"" + Text + "\" />");
                    }
                }
            }
        }
    }
}