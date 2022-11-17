// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="advertising.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class user_advertising.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class user_advertising : Page
    {
        /// <summary>
        /// The s price identifier
        /// </summary>
        public static string sPriceID = string.Empty;

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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 2;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "AdsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("AdsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            var str = new StringBuilder();

            var intRecordCount = 0;
            var lAdvertisementPrices = new List<AdvertisementPrices>();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM ShopProducts WHERE ModuleID=@ModuleID AND Status=1 ORDER BY ProductName", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", GlobalVars.ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            intRecordCount += 1;
                            sPriceID = SepFunctions.openNull(RS["ProductID"]);
                            var dAdvertisementPrices = new SepCommon.Models.AdvertisementPrices { PlanName = SepFunctions.openNull(RS["ProductName"]) };
                            dAdvertisementPrices.LongPrice = SepFunctions.Pricing_Long_Price(SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"])), SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"])), SepFunctions.openNull(RS["RecurringCycle"]));
                            dAdvertisementPrices.Description = SepFunctions.openNull(RS["Description"]);

                            string sShortDesc = SepFunctions.openNull(RS["ShortDesc"]);
                            if (!string.IsNullOrWhiteSpace(sShortDesc))
                            {
                                string iMaxClicks = SepFunctions.ParseXML("MAXCLICKS", sShortDesc);
                                string iMaxExposures = SepFunctions.ParseXML("MAXEXPOSURES", sShortDesc);
                                string sZone = SepFunctions.ParseXML("ZONE", sShortDesc);
                                string sTargetPage = SepFunctions.ParseXML("TARGETPAGE", sShortDesc);
                                string sTargetCat = SepFunctions.ParseXML("TARGETCAT", sShortDesc);
                                string sTargetPortal = SepFunctions.ParseXML("TARGETPORTAL", sShortDesc);
                                if (SepFunctions.toLong(iMaxClicks) > 0)
                                    str.Append("Maximum Clicks: " + iMaxClicks + "<br/>");
                                if (SepFunctions.toLong(iMaxExposures) > 0)
                                    str.Append("Maximum Exposures: " + iMaxExposures + "<br/>");
                                if (!string.IsNullOrWhiteSpace(sZone))
                                    if (Strings.InStr(sZone, "|-1|") == 0)
                                    {
                                        str.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                                        str.Append("<tr>");
                                        str.Append("<td align=\"right\" valign=\"top\">");
                                        str.Append(SepFunctions.LangText("Target Zones:"));
                                        str.Append("</td><td valign=\"top\" style=\"padding-left:5px\">");
                                        string[] arrZones = Strings.Split(Strings.Replace(sZone, "|", string.Empty), ",");
                                        if (arrZones != null)
                                        {
                                            for (var i = 0; i <= Information.UBound(arrZones); i++)
                                                using (var cmd2 = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ZoneID=@ZoneID", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@ZoneID", arrZones[i]);
                                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                    {
                                                        if (RS2.HasRows)
                                                        {
                                                            RS2.Read();
                                                            str.Append(SepFunctions.openNull(RS2["ZoneName"]) + "<br/>");
                                                        }
                                                    }
                                                }
                                        }

                                        str.Append("</td></tr></table>");
                                    }

                                if (!string.IsNullOrWhiteSpace(sTargetPage))
                                    if (Strings.InStr(sTargetPage, "|-1|") == 0 && Strings.InStr(sTargetPage, "|-2|") == 0)
                                    {
                                        str.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                                        str.Append("<tr>");
                                        str.Append("<td align=\"right\" valign=\"top\">");
                                        str.Append(SepFunctions.LangText("Target Pages:"));
                                        str.Append("</td><td valign=\"top\" style=\"padding-left:5px\">");
                                        string[] arrPages = Strings.Split(Strings.Replace(sTargetPage, "|", string.Empty), ",");
                                        if (arrPages != null)
                                        {
                                            for (var i = 0; i <= Information.UBound(arrPages); i++)
                                                using (var cmd2 = new SqlCommand("SELECT LinkText FROM ModulesNPages WHERE ModuleID > 0 AND ModuleID=@ModuleID", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@ModuleID", arrPages[i]);
                                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                    {
                                                        if (!RS2.HasRows)
                                                        {
                                                            using (var cmd3 = new SqlCommand("SELECT LinkText FROM ModulesNPages WHERE UserPageName <> '' AND PageID=@PageID", conn))
                                                            {
                                                                cmd3.Parameters.AddWithValue("@PageID", arrPages[i]);
                                                                using (SqlDataReader RS3 = cmd3.ExecuteReader())
                                                                {
                                                                    if (!RS3.HasRows)
                                                                    {
                                                                        using (var cmd4 = new SqlCommand("SELECT * FROM ShopProducts WHERE ModuleID=@ModuleID AND Status=1 ORDER BY ProductName", conn))
                                                                        {
                                                                            cmd4.Parameters.AddWithValue("@ModuleID", GlobalVars.ModuleID);
                                                                            using (SqlDataReader RS4 = cmd4.ExecuteReader())
                                                                            {
                                                                                if (RS4.HasRows)
                                                                                {
                                                                                    RS4.Read();
                                                                                    str.Append(SepFunctions.openNull(RS4["LinkText"]) + "<br/>");
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        RS3.Read();
                                                                        str.Append(SepFunctions.openNull(RS3["LinkText"]) + "<br/>");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            RS2.Read();
                                                            str.Append(SepFunctions.openNull(RS2["LinkText"]) + "<br/>");
                                                        }
                                                    }
                                                }
                                        }

                                        str.Append("</td></tr></table>");
                                    }

                                if (!string.IsNullOrWhiteSpace(sTargetCat))
                                    if (Strings.InStr(sTargetCat, "|-1|") == 0 && Strings.InStr(sTargetCat, "|-2|") == 0)
                                    {
                                        str.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                                        str.Append("<tr>");
                                        str.Append("<td align=\"right\" valign=\"top\">");
                                        str.Append(SepFunctions.LangText("Target Categories:"));
                                        str.Append("</td><td valign=\"top\" style=\"padding-left:5px\">");
                                        string[] arrCategories = Strings.Split(Strings.Replace(sTargetCat, "|", string.Empty), ",");
                                        if (arrCategories != null)
                                        {
                                            for (var i = 0; i <= Information.UBound(arrCategories); i++)
                                                using (var cmd2 = new SqlCommand("SELECT CategoryName FROM Categories WHERE CatID=@CatID", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@CatID", arrCategories[i]);
                                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                    {
                                                        if (RS2.HasRows)
                                                        {
                                                            RS2.Read();
                                                            str.Append(SepFunctions.openNull(RS2["CategoryName"]) + "<br/>");
                                                        }
                                                    }
                                                }
                                        }

                                        str.Append("</td></tr></table>");
                                    }

                                if (!string.IsNullOrWhiteSpace(sTargetPortal) && SepFunctions.Setup(60, "PortalsEnable") == "Enable")
                                    if (Strings.InStr(sTargetPortal, "|-1|") == 0)
                                    {
                                        str.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                                        str.Append("<tr>");
                                        str.Append("<td align=\"right\" valign=\"top\">");
                                        str.Append(SepFunctions.LangText("Target Portals:"));
                                        str.Append("</td><td valign=\"top\" style=\"padding-left:5px\">");
                                        string[] arrPortals = Strings.Split(Strings.Replace(sTargetPortal, "|", string.Empty), ",");
                                        if (arrPortals != null)
                                        {
                                            for (var i = 0; i <= Information.UBound(arrPortals); i++)
                                                if (SepFunctions.toLong(arrPortals[i]) == 0)
                                                    str.Append(SepFunctions.LangText("Master Portal") + "<br/>");
                                                else
                                                    using (var cmd2 = new SqlCommand("SELECT PortalTitle FROM Portals WHERE PortalID=@PortalID", conn))
                                                    {
                                                        cmd2.Parameters.AddWithValue("@PortalID", arrPortals[i]);
                                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                        {
                                                            if (RS2.HasRows)
                                                            {
                                                                RS2.Read();
                                                                str.Append(SepFunctions.openNull(RS2["PortalTitle"]) + "<br/>");
                                                            }
                                                        }
                                                    }
                                        }

                                        str.Append("</td></tr></table>");
                                    }
                            }

                            dAdvertisementPrices.Zones = Strings.ToString(str);
                            lAdvertisementPrices.Add(dAdvertisementPrices);
                            str.Clear();
                        }
                    }
                }
            }

            AdContent.DataSource = lAdvertisementPrices;
            AdContent.DataBind();
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }
    }
}