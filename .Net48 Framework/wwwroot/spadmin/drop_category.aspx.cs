// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="drop_category.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using SepControls;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;

    /// <summary>
    /// Class drop_category.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class drop_category : Page
    {
        /// <summary>
        /// Categories the check security.
        /// </summary>
        /// <param name="intCatID">The int cat identifier.</param>
        /// <param name="SelKeys">The sel keys.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Category_Check_Security(string intCatID, int SelKeys = 0)
        {
            // SelKeys = 0 - AccessKeys SelKeys = 1 - WriteKeys SelKeys = 2 - ManageKeys

            var ModuleKeys = string.Empty;
            var ModuleHide = false;
            var ExcPortal = false;
            var iPortalID = SepFunctions.Get_Portal_ID();

            using (var CatConn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                CatConn.Open();

                using (var Catcmd = new SqlCommand("SELECT AccessKeys,WriteKeys,ManageKeys,ExcPortalSecurity,AccessHide,WriteHide FROM Categories AS CAT WHERE CAT.CatID=@CatID AND CAT.CatID IN (SELECT PortalID FROM CategoriesPortals WHERE PortalID=@PortalID AND CatID=CAT.CatID)", CatConn))
                {
                    Catcmd.Parameters.AddWithValue("@CatID", intCatID);
                    Catcmd.Parameters.AddWithValue("@PortalID", iPortalID);
                    using (SqlDataReader CatRS = Catcmd.ExecuteReader())
                    {
                        if (CatRS.HasRows)
                        {
                            CatRS.Read();
                            switch (SelKeys)
                            {
                                case 1:
                                    ModuleKeys = SepFunctions.openNull(CatRS["WriteKeys"]);
                                    ModuleHide = SepFunctions.openBoolean(CatRS["WriteHide"]);
                                    break;

                                case 2:
                                    ModuleKeys = SepFunctions.openNull(CatRS["ManageKeys"]);
                                    break;

                                default:
                                    ModuleKeys = SepFunctions.openNull(CatRS["AccessKeys"]);
                                    ModuleHide = SepFunctions.openBoolean(CatRS["AccessHide"]);
                                    break;
                            }

                            if (SepFunctions.openBoolean(CatRS["ExcPortalSecurity"]) && SelKeys == 2)
                                ExcPortal = false;
                            else
                                ExcPortal = true;
                        }
                    }
                }
            }

            if (ModuleHide)
                return SepFunctions.CompareKeys(ModuleKeys, ExcPortal);
            return true;
        }

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
        /// Gets the upload control.
        /// </summary>
        public void GetUploadControl()
        {
            var jScript = string.Empty;

            using (var cUpload = new UploadFiles())
            {
                using (var sw = new StringWriter())
                {
                    var htw = new HtmlTextWriter(sw);
                    cUpload.ModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                    cUpload.RenderControl(htw);

                    jScript += "<script type=\"text/javascript\">";
                    jScript += "$('#PHFileUpload').html(unescape('" + SepFunctions.EscQuotes(Strings.ToString(sw)) + "'));";
                    jScript += "</script>";
                    htw.Dispose();
                }
            }

            var cstype = GetType();

            Page.ClientScript.RegisterClientScriptBlock(cstype, "ButtonClickScript", jScript);
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
            if (SepCommon.SepCore.Request.Item("DoAction") == "GetUploadControl")
            {
                GetUploadControl();
                return;
            }

            var ModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            var iPortalID = SepFunctions.Get_Portal_ID();
            var wclause2 = string.Empty;
            var forceLewestLevel = SepFunctions.Setup(992, "CatLowestLvl");
            var isReadOnly = false;

            if (SepCommon.SepCore.Request.Item("Disabled") == "true")
                isReadOnly = true;

            if (SepCommon.SepCore.Request.Item("DoAction") == "GetHTML")
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT PageText FROM CategoriesPages WHERE CatID=@CatID AND ModuleID=@ModuleID AND PortalID=@PortalID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", SepCommon.SepCore.Request.Item("CatID"));
                        cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                Response.Write(SepFunctions.openNull(RS["PageText"]));
                            }

                        }
                    }
                }

                return;
            }

            string wclause1;
            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) > 0)
                wclause1 = " CAT.ListUnder='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("CatID")) + "'";
            else
                wclause1 = " CAT.ListUnder='0'";

            if (ModuleID > 0) wclause2 += " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + ModuleID + "' AND CatID=CAT.CatID AND Status <> -1)";

            if (SepCommon.SepCore.Request.Item("ModifyPortal") == "True")
                wclause2 += " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesPortals WHERE (PortalID=0 OR PortalID = -1) AND CatID=CAT.CatID)";
            else
                wclause2 += " AND CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesPortals WHERE (PortalID='" + iPortalID + "' OR PortalID = -1) AND CatID=CAT.CatID)";

            Response.Write("<div class=\"list-group\">" + Environment.NewLine);

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID")) > 0)
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ListUnder,CatType,(SELECT CategoryName FROM Categories AS CAT2 WHERE CatID=CAT1.ListUnder) AS CategoryName,(SELECT TOP 1 SCAT.ListUnder FROM Categories AS SCAT WHERE SCAT.ListUnder=CAT1.CatID" + Strings.Replace(wclause2, "CAT.", "SCAT.") + ") AS HasSubs FROM Categories AS CAT1 WHERE CatID=@CatID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", SepCommon.SepCore.Request.Item("CatID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["HasSubs"])) && SepFunctions.openNull(RS["HasSubs"]) != "0") Response.Write("<a href=\"javascript:void(0)\"" + Strings.ToString(isReadOnly ? "alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Category is disabled from editing.")) + "'));" : " onclick=\"openCategory('" + SepFunctions.openNull(RS["ListUnder"]) + "', '" + SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) + "', unescape('" + SepFunctions.EscQuotes(SepFunctions.openNull(RS["CategoryName"])) + "'), unescape('" + SepFunctions.EscQuotes(SepFunctions.openNull(RS["CatType"])) + "'), true, " + Strings.ToString(forceLewestLevel == "Yes" ? "false" : "true") + ");\"") + " class=\"list-group-item list-group-item-action\"><i class=\"fa fa-backward\"></i> " + SepFunctions.LangText("Back") + "</a>" + Environment.NewLine);
                                else
                                    wclause1 = " CAT.ListUnder='0'";
                            }

                        }
                    }
                }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT CatID,CategoryName,CatType,(SELECT TOP 1 SCAT.ListUnder FROM Categories AS SCAT WHERE SCAT.ListUnder=CAT.CatID" + Strings.Replace(wclause2, "CAT.", "SCAT.") + ") AS HasSubs FROM Categories AS CAT WHERE" + wclause1 + wclause2 + " AND Status <> -1 ORDER BY CategoryName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                                if (Category_Check_Security(SepFunctions.openNull(RS["CatID"]), 1))
                                {
                                    bool hasSubCats = false;
                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["HasSubs"])) && SepFunctions.openNull(RS["HasSubs"]) != "0") hasSubCats = true;
                                    Response.Write("<a href=\"javascript:void(0)\" class=\"list-group-item list-group-item-action\" style=\"cursor:pointer\"" + Strings.ToString(isReadOnly ? "alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Category is disabled from editing.")) + "'));" : " onclick=\"openCategory('" + SepFunctions.openNull(RS["CatID"]) + "', '" + SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) + "', unescape('" + SepFunctions.EscQuotes(SepFunctions.openNull(RS["CategoryName"])) + "'), unescape('" + SepFunctions.EscQuotes(SepFunctions.openNull(RS["CatType"])) + "'), " + Strings.ToString(hasSubCats ? "true" : "false") + ", " + Strings.ToString(forceLewestLevel == "Yes" ? Strings.ToString(hasSubCats ? "false" : "true") : "true") + ");\"") + ">" + Strings.ToString(hasSubCats ? "<i class=\"fa fa-plus\"></i>" : "<i class=\"fa fa-minus\"></i>") + " " + SepFunctions.openNull(RS["CategoryName"]) + "</a>" + Environment.NewLine);
                                }

                    }
                }
            }

            Response.Write("</div>" + Environment.NewLine);
        }
    }
}