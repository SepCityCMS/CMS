// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="sitemap.aspx.cs" company="SepCity, Inc.">
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
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class sitemap.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class sitemap : Page
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

            GlobalVars.ModuleID = 30;

            var str = new StringBuilder();
            var intRecordCount = 0;
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


            long tmpMenuID;
            string GetPageName;
            if (SepFunctions.Get_Portal_ID() == 0)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT UniqueID,ModuleID,MenuID,PageID,UserPageName,LinkText,TargetWindow FROM ModulesNPages WHERE MenuID <> '0' AND Status=1 AND Activated='1' AND UserPageName <> '' ORDER BY MenuID, Weight, LinkText", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            tmpMenuID = 10;
                            while (RS.Read())
                                if (SepFunctions.Setup(GlobalVars.ModuleID, "Menu" + SepFunctions.openNull(RS["MenuID"]) + "Sitemap") == "Yes")
                                {
                                    bool showPage = false;
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) > 0)
                                    {
                                        if (SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])))) showPage = true;
                                    }
                                    else
                                    {
                                        showPage = true;
                                    }

                                    if (showPage)
                                    {
                                        intRecordCount += 1;
                                        if (tmpMenuID != SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"])))
                                        {
                                            if (intRecordCount > 1)
                                            {
                                                str.AppendLine("</ul>");
                                                str.AppendLine("</div>");
                                                str.AppendLine("</div>");
                                                str.AppendLine("</section>");
                                            }
                                            str.AppendLine("<section id=\"sec" + SepFunctions.openNull(RS["MenuID"]) + "\">");
                                            str.AppendLine("<h2>" + SepFunctions.Setup(GlobalVars.ModuleID, "Menu" + SepFunctions.openNull(RS["MenuID"]) + "Text") + "</h2>");
                                            str.AppendLine("<div class=\"row\">");
                                            str.AppendLine("<div class=\"col-md-3\">");
                                            str.AppendLine("<ul>");
                                        }

                                        switch (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])))
                                        {
                                            case 201:
                                                if (Strings.Left(SepFunctions.openNull(RS["UserPageName"]), 2) == "~/" || Strings.Left(SepFunctions.openNull(RS["UserPageName"]), 1) == "/")
                                                    GetPageName = sInstallFolder + Strings.Replace(Strings.Replace(SepFunctions.openNull(RS["UserPageName"]), "~/", string.Empty), "/", string.Empty);
                                                else
                                                    GetPageName = SepFunctions.openNull(RS["UserPageName"]);
                                                break;

                                            case 200:
                                                GetPageName = sInstallFolder + "page/" + SepFunctions.openNull(RS["UniqueID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["LinkText"]));
                                                break;

                                            default:
                                                GetPageName = sInstallFolder + SepFunctions.openNull(RS["UserPageName"]);
                                                break;
                                        }

                                        str.AppendLine("<li><a href=\"" + GetPageName + "\" target=\"" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["TargetWindow"])) ? SepFunctions.openNull(RS["TargetWindow"]) : "_self") + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a></li>");
                                    }

                                    tmpMenuID = SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"]));
                                }

                            str.AppendLine("</ul>");
                            str.AppendLine("</div>");
                            str.AppendLine("</div>");
                            str.AppendLine("</section>");
                        }
                    }
                }
            }
            else
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ID,PageID,MenuID,UserPageName,LinkText,TargetWindow FROM PortalPages WHERE MenuID <> 0 AND Enable='1' AND Activated='1' AND UserPageName <> '' AND (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%') ORDER BY MenuID, Weight, LinkText", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            tmpMenuID = 10;
                            while (RS.Read())
                            {
                                intRecordCount += 1;
                                if (tmpMenuID != SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"])))
                                {
                                    if (intRecordCount > 1)
                                        str.Append("</ul>");
                                    str.Append("<br/><br/><b>" + SepFunctions.PortalSetup("SiteMenu" + SepFunctions.openNull(RS["MenuID"])) + "</b>");
                                    str.Append("<ul>");
                                }

                                switch (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])))
                                {
                                    case 201:
                                        GetPageName = SepFunctions.openNull(RS["UserPageName"]);
                                        break;

                                    case 200:
                                        GetPageName = sInstallFolder + "page/" + SepFunctions.openNull(RS["ID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["LinkText"]));
                                        break;

                                    default:
                                        GetPageName = sInstallFolder + SepFunctions.openNull(RS["UserPageName"]);
                                        break;
                                }

                                var sTargetWindow = !string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["TargetWindow"])) ? SepFunctions.openNull(RS["TargetWindow"]) : "_self";

                                str.Append("<li><a href=\"" + GetPageName + "\" target=\"" + sTargetWindow + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a></li>");
                                tmpMenuID = SepFunctions.toLong(SepFunctions.openNull(RS["MenuID"]));
                            }

                            str.Append("</ul>");
                        }
                    }
                }
            }

            ModuleContent.InnerHtml = Strings.ToString(str);
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