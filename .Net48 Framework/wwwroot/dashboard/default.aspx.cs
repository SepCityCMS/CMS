// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="default.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class _default7.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class _default7 : Page
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
            long iTotalUsers = 0;

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

            var PortalID = SepFunctions.Get_Portal_ID();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Status=1 AND PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            iTotalUsers = SepFunctions.toLong(SepFunctions.openNull(RS["Counter"]));
                            TotalUsers.InnerHtml = Strings.ToString(iTotalUsers);
                            TotalUsers2.InnerHtml = Strings.ToString(iTotalUsers);
                        }

                    }
                }

                using (var cmd = new SqlCommand("SELECT (SELECT Count(UserId) FROM Members WHERE CreateDate BETWEEN DATEADD(day, -7, GETDATE()) AND GETDATE() AND Status=1 AND PortalID=@PortalID) AS ThisWeek, (SELECT Count(UserId) FROM Members WHERE CreateDate BETWEEN DATEADD(day, -14, GETDATE()) AND DATEADD(day, -7, GETDATE()) AND Status=1 AND PortalID=@PortalID) AS LastWeek", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var UserPercent = string.Empty;
                            if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) == SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])))
                            {
                                PercentFemales.InnerHtml = "<i><i class=\"fa\"></i> 0% </i>";
                            }
                            else
                            {
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) == 0)
                                {
                                    UserPercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) * 100, 0);
                                    PercentUsers.InnerHtml = "<i class=\"red\"><i class=\"fa fa-sort-desc\"></i> -" + UserPercent + "% </i>";
                                }
                                else
                                {
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) == 0 && SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) > 0)
                                    {
                                        UserPercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) * 100, 0);
                                        PercentUsers.InnerHtml = "<i class=\"green\"><i class=\"fa fa-sort-asc\"></i> " + UserPercent + "% </i>";
                                    }
                                    else
                                    {
                                        UserPercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) / SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) * 100, 0);
                                        if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) >= SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"]))) PercentUsers.InnerHtml = "<i class=\"green\"><i class=\"fa fa-sort-asc\"></i> " + UserPercent + "% </i>";
                                        else PercentUsers.InnerHtml = "<i class=\"red\"><i class=\"fa fa-sort-desc\"></i> -" + UserPercent + "% </i>";
                                    }
                                }
                                if(PercentUsers.InnerHtml.Length > 0)
                                {
                                    PercentUsers.InnerHtml = "0%";
                                }
                            }
                        }

                    }
                }

                using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Male='1' AND Status=1 AND PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            TotalMales.InnerHtml = SepFunctions.openNull(RS["Counter"]);
                        }

                    }
                }

                using (var cmd = new SqlCommand("SELECT (SELECT Count(UserId) FROM Members WHERE Male='1' AND Status=1 AND CreateDate BETWEEN DATEADD(day, -7, GETDATE()) AND GETDATE() AND PortalID=@PortalID) AS ThisWeek, (SELECT Count(UserId) FROM Members WHERE Male='1' AND CreateDate BETWEEN DATEADD(day, -14, GETDATE()) AND DATEADD(day, -7, GETDATE()) AND PortalID=@PortalID) AS LastWeek", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var MalePercent = string.Empty;
                            if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) == SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])))
                            {
                                PercentMales.InnerHtml = "<i><i class=\"fa\"></i> 0% </i>";
                            }
                            else
                            {
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) == 0)
                                {
                                    MalePercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) * 100, 0);
                                    PercentMales.InnerHtml = "<i class=\"red\"><i class=\"fa fa-sort-desc\"></i> -" + MalePercent + "% </i>";
                                }
                                else
                                {
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) == 0 && SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) > 0)
                                    {
                                        MalePercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) * 100, 0);
                                        PercentMales.InnerHtml = "<i class=\"green\"><i class=\"fa fa-sort-asc\"></i> " + MalePercent + "% </i>";
                                    }
                                    else
                                    {
                                        MalePercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) / SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) * 100, 0);
                                        if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) >= SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"]))) PercentMales.InnerHtml = "<i class=\"green\"><i class=\"fa fa-sort-asc\"></i> " + MalePercent + "% </i>";
                                        else PercentMales.InnerHtml = "<i class=\"red\"><i class=\"fa fa-sort-desc\"></i> -" + MalePercent + "% </i>";
                                    }
                                }
                            }
                        }

                    }
                }

                using (var cmd = new SqlCommand("SELECT Count(UserId) AS Counter FROM Members WHERE Male='0' AND Status=1 AND PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            TotalFemales.InnerHtml = SepFunctions.openNull(RS["Counter"]);
                        }

                    }
                }

                using (var cmd = new SqlCommand("SELECT (SELECT Count(UserId) FROM Members WHERE Male='0' AND Status=1 AND CreateDate BETWEEN DATEADD(day, -7, GETDATE()) AND GETDATE() AND PortalID=@PortalID) AS ThisWeek, (SELECT Count(UserId) FROM Members WHERE Male='0' AND CreateDate BETWEEN DATEADD(day, -14, GETDATE()) AND DATEADD(day, -7, GETDATE()) AND PortalID=@PortalID) AS LastWeek", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            var FemalePercent = string.Empty;
                            if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) == SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])))
                            {
                                PercentFemales.InnerHtml = "<i><i class=\"fa\"></i> 0% </i>";
                            }
                            else
                            {
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) == 0)
                                {
                                    FemalePercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) * 100, 0);
                                    PercentFemales.InnerHtml = "<i class=\"red\"><i class=\"fa fa-sort-desc\"></i> -" + FemalePercent + "% </i>";
                                }
                                else
                                {
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) == 0 && SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) > 0)
                                    {
                                        FemalePercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) * 100, 0);
                                        PercentFemales.InnerHtml = "<i class=\"green\"><i class=\"fa fa-sort-asc\"></i> " + FemalePercent + "% </i>";
                                    }
                                    else
                                    {
                                        FemalePercent = Strings.FormatNumber(SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) / SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"])) * 100, 0);
                                        if (SepFunctions.toLong(SepFunctions.openNull(RS["ThisWeek"])) >= SepFunctions.toLong(SepFunctions.openNull(RS["LastWeek"]))) PercentFemales.InnerHtml = "<i class=\"green\"><i class=\"fa fa-sort-asc\"></i> " + FemalePercent + "% </i>";
                                        else PercentFemales.InnerHtml = "<i class=\"red\"><i class=\"fa fa-sort-desc\"></i> -" + FemalePercent + "% </i>";
                                    }
                                }
                            }
                        }

                    }
                }

                using (var cmd = new SqlCommand("SELECT Count(CatID) AS Counter FROM Categories WHERE Status <> -1 AND Categories.CatID IN (SELECT CatID FROM CategoriesPortals WHERE PortalID=@PortalID)", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            TotalCategories.InnerHtml = SepFunctions.openNull(RS["Counter"]);
                        }

                    }
                }

                if (SepFunctions.Setup(994, "AskCountry") == "Yes")
                {
                    long acount = 0;
                    long countCountry = 0;
                    str.AppendLine("<script type=\"text/javascript\">");
                    using (var cmd = new SqlCommand("SELECT Country, COUNT(*) COUNT FROM Members WHERE Status=1 AND PortalID=@PortalID AND Country <> '' AND ISNUMERIC(Country)=0 GROUP BY Country order by Country", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            str.Append("var map_data = {");
                            while (RS.Read())
                            {
                                acount += 1;
                                if (acount > 1)
                                    str.Append(",");
                                str.Append("\"" + SepFunctions.openNull(RS["Country"]) + "\":\"" + SepFunctions.openNull(RS["Count"]) + "\"");
                                countCountry += 1;
                            }

                            str.Append("};" + Environment.NewLine);
                        }
                    }

                    str.AppendLine("$(document).ready(function(){");
                    str.AppendLine("  $('#world-map-gdp').vectorMap({");
                    str.AppendLine("    map: 'world_en',");
                    str.AppendLine("    backgroundColor: null,");
                    str.AppendLine("    color: '#ffffff',");
                    str.AppendLine("    hoverOpacity: 0.7,");
                    str.AppendLine("    selectedColor: '#666666',");
                    str.AppendLine("    enableZoom: false,");
                    str.AppendLine("    showTooltip: true,");
                    str.AppendLine("    values: map_data,");
                    str.AppendLine("    scaleColors: ['#E6F2F0', '#149B7E'],");
                    str.AppendLine("    normalizeFunction: 'polynomial'");
                    str.AppendLine("  });");
                    str.AppendLine("});");
                    str.AppendLine("</script>");

                    TrendData.InnerHtml = Strings.ToString(str);
                    numCountries.InnerHtml = Strings.ToString(countCountry);
                }

                if (SepFunctions.Setup(994, "AskCountry") != "Yes")
                {
                    UserLocations.Visible = false;
                }
                else
                {
                    str.Clear();
                    using (var cmd = new SqlCommand("SELECT TOP 4 Country, COUNT(*) COUNT FROM Members WHERE Status=1 AND PortalID=@PortalID AND Country <> '' AND ISNUMERIC(Country)=0 GROUP BY Country order by COUNT DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", PortalID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                                try
                                {
                                    var ri = new RegionInfo(Strings.UCase(SepFunctions.openNull(RS["Country"])));
                                    var userPercent = Strings.FormatNumber(SepFunctions.toDecimal(SepFunctions.openNull(RS["COUNT"])) / iTotalUsers * 100, 0);
                                    str.AppendLine("<tr>");
                                    str.AppendLine("<td>" + ri.DisplayName + "</td>");
                                    str.AppendLine("<td class=\"fs15 fw700 text-right\">" + userPercent + "%</td>");
                                    str.AppendLine("</tr>");
                                }
                                catch
                                {
                                }

                        }
                        CountryList.InnerHtml = Strings.ToString(str);
                    }
                }
            }
        }
    }
}