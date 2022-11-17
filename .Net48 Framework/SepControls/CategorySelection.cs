// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="CategorySelection.cs" company="SepCity, Inc.">
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
    /// Class CategorySelection.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CategorySelection runat=server></{0}:CategorySelection>")]
    public class CategorySelection : WebControl
    {
        /// <summary>
        /// The m only include
        /// </summary>
        private string m_OnlyInclude;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the only include.
        /// </summary>
        /// <value>The only include.</value>
        public string OnlyInclude
        {
            get => Strings.ToString(m_OnlyInclude);

            set => m_OnlyInclude = value;
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
        /// Raises the <see cref="System.Web.UI.Control.PreRender" /> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs" /> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var cName = "catSelectionJS";
            var cUrl = sImageFolder + "js/CategorySelection.js";
            var csType = GetType();

            var cs = Page.ClientScript;

            if (!cs.IsClientScriptIncludeRegistered(csType, cName))
            {
                cs.RegisterClientScriptInclude(csType, cName, ResolveClientUrl(cUrl));
            }
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            if (SepFunctions.showCategories() == false)
            {
                return;
            }

            long aa = 0;

            var wclause = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    wclause += " AND CatID IN ('" + Strings.Replace(Strings.Replace(OnlyInclude, "|", string.Empty), ",", "','") + "')";
                }

                output.WriteLine("<input type=\"hidden\" name=\"" + ID + "\" id=\"" + ID + "\" value=\"" + Text + "\" />");

                output.WriteLine("<div class=\"input-group\">");
                output.WriteLine("<input type=\"text\" name=\"CatSearch\" id=\"CatSearch\" placeholder=\"" + SepFunctions.LangText("Search for...") + "\" onkeydown=\"if(submitCatKeyPress(event) == '13'){submitCatSearch('" + SepFunctions.Get_Portal_ID() + "', '" + ID + "');return false;};\" />");
                output.WriteLine("<span class=\"input-group-btn\">");
                output.WriteLine("<button class=\"btn btn-default\" type=\"button\" onclick=\"submitCatSearch('" + SepFunctions.Get_Portal_ID() + "', '" + ID + "');\">" + SepFunctions.LangText("Search") + "</button>");
                output.WriteLine("</span>");
                output.WriteLine("</div>");

                output.WriteLine("<br/>");

                output.WriteLine("<div style=\"width:245px;float:left;\">" + SepFunctions.LangText("Available Categories") + "</div>");
                output.WriteLine("<div style=\"width:10px;float:left;\">&nbsp;&nbsp;</div>");
                output.WriteLine("<div style=\"width:245px;float:left;\">" + SepFunctions.LangText("Selected Categories") + "</div><br/>");
                output.WriteLine("<div id=\"SearchCatList\" style=\"width:245px;height:100px;overflow:auto;border:1px solid;border-color:#000000;float:left;\"><span id=\"SearchCatListHelp\">" + SepFunctions.LangText("Enter the beginning part of a category name above to run a search to select one or more categories.") + "<br/><br/>" + SepFunctions.LangText("(Ex. \"Oh\" will show \"Ohio\")") + "</span>");
                output.WriteLine("<table width=\"100%\" class=\"Table\" id=\"availableItems\">");
                output.WriteLine("</table>");
                output.WriteLine("</div>");
                output.WriteLine("<div style=\"width:10px;float:left;\">&nbsp;&nbsp;</div>");
                output.WriteLine("<div id=\"SearchCatSelected\" style=\"width:245px;height:100px;overflow:auto;border:1px solid;border-color:#000000;float:left;\">");
                output.WriteLine("<table width=\"100%\" class=\"Table\" id=\"selectedItems\">");
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    string[] arrCatID = Strings.Split(Text, ",");

                    if (arrCatID != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrCatID); i++)
                        {
                            if (arrCatID[i] != "|0|")
                            {
                                using (var cmd = new SqlCommand("SELECT CatID,CategoryName,ListUnder FROM Categories WHERE CatID=@CatID" + wclause, conn))
                                {
                                    cmd.Parameters.AddWithValue("@CatID", Strings.Replace(arrCatID[i], "|", string.Empty));
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (aa % 2 == 0)
                                            {
                                                output.WriteLine("<tr class=\"TableBody1\" id=\"sel" + SepFunctions.openNull(RS["CatID"]) + "\">");
                                            }
                                            else
                                            {
                                                output.WriteLine("<tr class=\"TableBody2\" id=\"sel" + SepFunctions.openNull(RS["CatID"]) + "\">");
                                            }

                                            output.WriteLine("<td style=\"cursor:pointer;\" onclick=\"removeCategory('" + SepFunctions.openNull(RS["CatID"]) + "', '" + ID + "');\" id=\"seltd" + SepFunctions.openNull(RS["CatID"]) + "\"><b>");
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ListUnder"])) && SepFunctions.openNull(RS["ListUnder"]) != "0")
                                            {
                                                using (var cmd2 = new SqlCommand("SELECT CategoryName FROM Categories WHERE CatID=@CatID", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@CatID", SepFunctions.openNull(RS["ListUnder"]));
                                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                    {
                                                        if (RS2.HasRows)
                                                        {
                                                            RS2.Read();
                                                            output.Write(SepFunctions.openNull(RS2["CategoryName"]) + " / ");
                                                        }
                                                    }
                                                }
                                            }

                                            output.Write(SepFunctions.openNull(RS["CategoryName"]));
                                            output.WriteLine("</b></td>");
                                            output.WriteLine("</tr>");
                                            aa += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                output.WriteLine("</table>");
                output.WriteLine("</div>");

                output.WriteLine("<div style=\"clear:left;\"></div>");
            }
        }
    }
}