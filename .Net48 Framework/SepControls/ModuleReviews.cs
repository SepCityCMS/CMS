// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="ModuleReviews.cs" company="SepCity, Inc.">
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
    /// Class ModuleReviews.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ModuleReviews runat=server></{0}:ModuleReviews>")]
    public class ModuleReviews : WebControl
    {
        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => Strings.ToString(m_UserID);

            set => m_UserID = value;
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
            var sInstallFolder = SepFunctions.GetInstallFolder();
            long CurrentUserCount = 0;
            var DisableSave = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ReviewID FROM Reviews WHERE ModuleIDs LIKE '%|" + ModuleID + "|%' ORDER BY Weight,Question", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.WriteLine("<script type=\"text/javascript\">");
                            output.WriteLine("function saveReview() {");
                            while (RS.Read())
                            {
                                output.WriteLine("\t$.ajax({");
                                output.WriteLine("\t\turl: '" + sInstallFolder + "review_save.aspx?ModuleID=" + ModuleID + "&UserID=" + UserID + "&UniqueID=" + SepFunctions.openNull(RS["ReviewID"]) + "&Rating=' + $('#Review" + SepFunctions.openNull(RS["ReviewID"]) + "').val(),");
                                output.WriteLine("\t\ttype: 'POST'");
                                output.WriteLine("\t});");
                            }

                            output.WriteLine("alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("You have successfully rated this user.")) + "'));");
                            output.WriteLine("document.location.reload();");
                            output.WriteLine("}");
                            output.WriteLine("</script>");
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT * FROM Reviews WHERE ModuleIDs LIKE '%|" + ModuleID + "|%' ORDER BY Weight,Question", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            output.WriteLine("<div class=\"alert alert-secondary\">" + SepFunctions.LangText("Review Me") + "</div>");

                            output.WriteLine("<table class=\"Table\" width=\"100%\" align=\"center\">");
                            while (RS.Read())
                            {
                                output.WriteLine("<tr class=\"TableBody1\">");
                                output.WriteLine("<td valign=\"top\" width=\"180\"><b>" + SepFunctions.openNull(RS["Question"]) + "</b></td>");
                                using (var cmd2 = new SqlCommand("SELECT * FROM ReviewsUsers WHERE ReviewID=@ReviewID AND UserID=@UserID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ReviewID", SepFunctions.openNull(RS["ReviewID"]));
                                    cmd2.Parameters.AddWithValue("@UserID", UserID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            RS2.Read();
                                            double TotalStars = SepFunctions.toDouble(SepFunctions.openNull(RS2["TotalStars"])) / SepFunctions.toDouble(SepFunctions.openNull(RS2["TotalUsers"]));
                                            output.WriteLine("<td valign=\"top\">" + SepFunctions.Rating_Stars_Display(TotalStars) + "</td>");
                                        }
                                        else
                                        {
                                            output.WriteLine("<td valign=\"top\">" + SepFunctions.LangText("Not Rated") + "</td>");
                                        }
                                    }
                                }

                                output.WriteLine("<td valign=\"top\">");
                                if (SepFunctions.Check_Rating(ModuleID, SepFunctions.openNull(RS["ReviewID"]), UserID) == false)
                                {
                                    DisableSave = false;
                                    output.WriteLine("<select name=\"Review" + SepFunctions.openNull(RS["ReviewID"]) + "\" id=\"Review" + SepFunctions.openNull(RS["ReviewID"]) + "\" class=\"inline-block\" style=\"width:100px;\">");
                                    output.WriteLine("<option value=\"5\">" + SepFunctions.LangText("5 Stars") + "</option>");
                                    output.WriteLine("<option value=\"4\">" + SepFunctions.LangText("4 Stars") + "</option>");
                                    output.WriteLine("<option value=\"3\">" + SepFunctions.LangText("3 Stars") + "</option>");
                                    output.WriteLine("<option value=\"2\">" + SepFunctions.LangText("2 Stars") + "</option>");
                                    output.WriteLine("<option value=\"1\">" + SepFunctions.LangText("1 Stars") + "</option>");
                                    output.WriteLine("</select>");
                                }
                                else
                                {
                                    DisableSave = true;
                                }

                                output.WriteLine("</td>");
                                output.WriteLine("</tr>");
                                using (var cmd2 = new SqlCommand("SELECT ActivityID FROM Activities WHERE ModuleID=@ModuleID AND UniqueID=@ReviewID AND UserID=@UserID AND ActType='RATING'", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ModuleID", ModuleID);
                                    cmd2.Parameters.AddWithValue("@ReviewID", SepFunctions.openNull(RS["ReviewID"]));
                                    cmd2.Parameters.AddWithValue("@UserID", UserID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            CurrentUserCount += 1;
                                        }
                                    }
                                }
                            }

                            if (DisableSave == false)
                            {
                                output.WriteLine("<tr class=\"TableBody1\">");
                                output.WriteLine("<td colspan=\"2\"></td>");
                                output.WriteLine("<td><input type=\"button\" name=\"SaveReview\" id=\"SaveReviewBUtton\" onclick=\"saveReview()\" class=\"btn btn-light\" value=\"" + SepFunctions.LangText("Save") + "\" /></td>");
                                output.WriteLine("</tr>");
                            }

                            output.WriteLine("</table>");
                        }
                    }
                }
            }
        }
    }
}