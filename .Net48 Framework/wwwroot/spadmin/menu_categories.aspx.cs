// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="menu_categories.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;

    /// <summary>
    /// Class menu_categories.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class menu_categories : Page
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
            var ds = new DataSet();

            var wclause = " AND ListUnder='0'";

            var iPortalID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"));
            var sFolder = Request.Form["folder"];

            if (Strings.Left(sFolder, 3) == "Cat") wclause = " AND (ListUnder='" + SepFunctions.FixWord(Strings.Replace(sFolder, "Cat", string.Empty)) + "')";

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0) wclause += " AND (CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesModules WHERE ModuleID='" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "' AND CatID=CAT.CatID AND Status <> -1))";

            Context.Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;width:180px;\">" + Environment.NewLine);
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                using (var cmd = new SqlCommand("SELECT CatID,CategoryName,(SELECT TOP 1 CatID FROM Categories WHERE ListUnder=CAT.CatID) AS HasSubs FROM Categories AS CAT WHERE CAT.CatID IN (SELECT TOP 1 CatID FROM CategoriesPortals WHERE (PortalID=" + iPortalID + " OR PortalID = -1) AND CatID=CAT.CatID) AND Status <> -1" + wclause + " ORDER BY CategoryName", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
            }

            for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(ds.Tables[0].Rows[i]["HasSubs"]))) Context.Response.Write("<li class=\"directoryplus collapsed\"><input type=\"checkbox\" name=\"CatID\" value=\"" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "\" /><a href=\"category_modify.aspx?CatID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "&ModuleID=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "&TopMenu=False&PortalID=" + iPortalID + "\" rel=\"Cat" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "\" id=\"Cat" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "\">" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CategoryName"]) + "</a></li>" + Environment.NewLine);
                else Context.Response.Write("<li class=\"directory\"><input type=\"checkbox\" name=\"CatID\" value=\"" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "\" /><a href=\"category_modify.aspx?CatID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "&ModuleID=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "&TopMenu=False&PortalID=" + iPortalID + "\" rel=\"Cat" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "\" id=\"Cat" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CatID"]) + "\">" + SepFunctions.openNull(ds.Tables[0].Rows[i]["CategoryName"]) + "</a></li>" + Environment.NewLine);
            ds.Dispose();
            Context.Response.Write("</ul>" + Environment.NewLine);
        }
    }
}