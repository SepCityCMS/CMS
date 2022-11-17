// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="PageSelection.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class PageSelection.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PageSelection runat=server></{0}:PageSelection>")]
    public class PageSelection : WebControl
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
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess")) == false)
            {
                return;
            }

            double iHalf = 0;
            long aCount = 0;
            var arrPageIDs = new ArrayList();
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    var arrOnlyInclude = Strings.Split(OnlyInclude, ",");
                    if (arrOnlyInclude != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrOnlyInclude); i++)
                        {
                            iHalf += 1;
                            arrPageIDs.Add(Strings.Replace(arrOnlyInclude[i], "|", string.Empty));
                        }
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("SELECT UniqueID,ModuleID,PageID FROM ModulesNPages WHERE (UserPageName <> '' OR ModuleID='39' OR ModuleID='62' OR ModuleID='3') AND PageID <> '201' AND Status <> -1 ORDER BY LinkText", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                while (RS.Read())
                                {
                                    bool showRow = false;
                                    if (SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"])) > 0)
                                    {
                                        if (SepFunctions.ModuleActivated(SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"]))))
                                        {
                                            showRow = true;
                                        }
                                    }
                                    else
                                    {
                                        showRow = true;
                                    }

                                    if (showRow)
                                    {
                                        iHalf += 1;
                                        if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) < 200)
                                        {
                                            arrPageIDs.Add(SepFunctions.openNull(RS["PageID"]));
                                        }
                                        else
                                        {
                                            arrPageIDs.Add(SepFunctions.openNull(RS["UniqueID"]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                output.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\">");
                output.WriteLine("function " + ID + "Selection(isSelect) {");
                output.WriteLine("$(\"#div" + ID + " input[type='checkbox']\").each(function () {");
                output.WriteLine("if(isSelect == 'select') {");
                output.WriteLine("$(this).prop('checked', true);");
                output.WriteLine("} else {");
                output.WriteLine("$(this).prop('checked', false);");
                output.WriteLine("}");
                output.WriteLine("});");
                output.WriteLine("}");
                output.WriteLine("</script>");

                output.WriteLine("<a href=\"javascript:" + ID + "Selection('select');\">" + SepFunctions.LangText("Select All") + "</a> | <a href=\"javascript:" + ID + "Selection('deselect');\">" + SepFunctions.LangText("De-Select All") + "</a>");
                output.WriteLine("<br/>");
                output.WriteLine("<div class=\"MultiCheckboxDiv\" id=\"div" + ID + "\">");
                output.WriteLine("<div class=\"MultiCheckboxDivLeft\">");
                if (!string.IsNullOrWhiteSpace(OnlyInclude))
                {
                    if (Strings.InStr(OnlyInclude, "|-1|") > 0)
                    {
                        aCount += 1;
                        iHalf += 1;
                        output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Pages") + "</label><br/>");
                    }
                }
                else
                {
                    aCount += 1;
                    iHalf += 1;
                    output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|-1|\"" + Strings.ToString(Strings.InStr(Text, "|-1|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.LangText("All Pages") + "</label><br/>");
                }

                if (iHalf > 2)
                {
                    iHalf = SepFunctions.toLong(Strings.ToString(Math.Ceiling(SepFunctions.toDouble(Strings.ToString(iHalf)) / 2)));
                }

                for (var i = 0; i <= arrPageIDs.Count - 1; i++)
                {
                    using (var cmd = new SqlCommand("SELECT LinkText,UniqueID,ModuleID,PageID FROM ModulesNPages WHERE (UniqueID=@UniqueID OR PageID=@UniqueID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", arrPageIDs[i].ToString());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                aCount += 1;
                                string sUniqueID;
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) < 200)
                                {
                                    sUniqueID = SepFunctions.openNull(RS["PageID"]);
                                }
                                else
                                {
                                    sUniqueID = SepFunctions.openNull(RS["UniqueID"]);
                                }

                                output.WriteLine("<input type=\"checkbox\" id=\"" + ID + Strings.ToString(aCount) + "\" name=\"" + ID + "\" class=\"checkboxField\" value=\"|" + sUniqueID + "|\"" + Strings.ToString(Strings.InStr(Text, "|" + sUniqueID + "|") > 0 ? " checked=\"checked\"" : string.Empty) + "/><label for=\"" + ID + Strings.ToString(aCount) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</label><br/>");
                                if (aCount == iHalf)
                                {
                                    // -V3024
                                    output.WriteLine("</div><div class=\"MultiCheckboxDivRight\">");
                                }
                            }
                        }
                    }
                }

                output.WriteLine("</div>");
                output.WriteLine("</div>");
            }
        }
    }
}