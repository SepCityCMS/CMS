// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="modules.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class modules.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class modules : Page
    {
        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            var str = new StringBuilder();
            TranslatePage();

            if (SepFunctions.Admin_Login_Required("2"))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys("|2|", true) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                long acount = 0;
                using (var cmd = new SqlCommand("SELECT TOP 10 ModuleID,COUNT(ModuleID) AS Count FROM Activities WHERE ModuleID < 100 GROUP BY ModuleID ORDER BY Count DESC", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        str.AppendLine("<script type=\"text/javascript\">");
                        str.AppendLine("Morris.Bar({");
                        str.AppendLine("element: 'top_10_modules',");
                        str.AppendLine("xkey: 'acttype',");
                        str.AppendLine("ykeys: ['value'],");
                        str.AppendLine("labels: ['Count'],");
                        str.AppendLine("xLabelAngle: 25,");
                        str.AppendLine("hideHover: 'auto',");
                        str.AppendLine("lineColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],");
                        str.AppendLine("resize: false,");
                        str.AppendLine("data: [");
                        while (RS.Read())
                        {
                            acount += 1;
                            if (acount > 1)
                                str.AppendLine(",");
                            str.AppendLine("{ acttype: unescape('" + SepFunctions.EscQuotes(SepFunctions.GetModuleName(SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"])))) + "'), value: " + SepFunctions.toInt(SepFunctions.openNull(RS["Count"])) + " }");
                        }

                        str.AppendLine("]");
                        str.AppendLine("});");
                        str.AppendLine("</script>");
                    }
                }

                acount = 0;
                using (var cmd = new SqlCommand("SELECT TOP 10 ModuleID,COUNT(ModuleID) AS Count FROM Activities WHERE ModuleID < 100 GROUP BY ModuleID ORDER BY Count ASC", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        str.AppendLine("<script type=\"text/javascript\">");
                        str.AppendLine("Morris.Bar({");
                        str.AppendLine("element: 'top_10_less_modules',");
                        str.AppendLine("xkey: 'acttype',");
                        str.AppendLine("ykeys: ['value'],");
                        str.AppendLine("labels: ['Count'],");
                        str.AppendLine("xLabelAngle: 25,");
                        str.AppendLine("hideHover: 'auto',");
                        str.AppendLine("lineColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],");
                        str.AppendLine("resize: false,");
                        str.AppendLine("data: [");
                        while (RS.Read())
                        {
                            acount += 1;
                            if (acount > 1)
                                str.AppendLine(",");
                            str.AppendLine("{ acttype: unescape('" + SepFunctions.EscQuotes(SepFunctions.GetModuleName(SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"])))) + "'), value: " + SepFunctions.toInt(SepFunctions.openNull(RS["Count"])) + " }");
                        }

                        str.AppendLine("]");
                        str.AppendLine("});");
                        str.AppendLine("</script>");
                    }
                }
            }

            TrendData.InnerHtml = SepCommon.SepCore.Strings.ToString(str);
        }
    }
}