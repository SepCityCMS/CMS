// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="favorites_add.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI;

    /// <summary>
    /// Class favorites_add.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class favorites_add : Page
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
            GlobalVars.ModuleID = 33;

            var GetPageTitle = string.Empty;
            string GetPageURL = SepFunctions.FixWord(Strings.Replace(Strings.Replace(SepCommon.SepCore.Request.Item("PageURL"), "&ModuleID=" + SepCommon.SepCore.Request.Item("ModuleID"), string.Empty), "?ModuleID=" + SepCommon.SepCore.Request.Item("ModuleID"), string.Empty));


            string sTitle;
            if (Strings.Left(SepCommon.SepCore.Request.Item("PageURL"), 9) == "WISHLIST:")
                sTitle = "wish list";
            else
                sTitle = "favorites";

            if (Strings.Len(GetPageURL) > 255 || Strings.Len(GetPageURL) == 0)
            {
                Response.Write("<p>" + SepFunctions.LangText("This page can not be added to your " + sTitle + ".") + "</p>");
                return;
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && !string.IsNullOrWhiteSpace(GetPageURL))
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ModuleID")))
                        using (var cmd = new SqlCommand("SELECT LinkText FROM ModulesNPages WHERE ModuleID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ModuleID")) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetPageTitle = SepFunctions.openNull(RS["LinkText"]);
                                }

                            }
                        }

                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("CatID")))
                        using (var cmd = new SqlCommand("SELECT CategoryName FROM Categories WHERE CatID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("CatID")) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetPageTitle += " - " + SepFunctions.openNull(RS["CategoryName"]);
                                }

                            }
                        }

                    using (var cmd = new SqlCommand("SELECT ID FROM Favorites WHERE PageURL='" + SepFunctions.FixWord(GetPageURL) + "' AND UserID='" + SepFunctions.Session_User_ID() + "' AND ModuleID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ModuleID")) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                using (var cmd2 = new SqlCommand("INSERT INTO Favorites (UserID,PageTitle,PageURL,ModuleID,DatePosted,LastAccessed) VALUES('" + SepFunctions.Session_User_ID() + "','" + SepFunctions.FixWord(Strings.Left(SepFunctions.FixWord(GetPageTitle), 100)) + "','" + SepFunctions.FixWord(GetPageURL) + "','" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ModuleID")) + "',GETDATE(),GETDATE())", conn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }

                                Response.Write("<p>" + SepFunctions.LangText("Page Successfully Added to Your " + sTitle + ".") + "</p>");
                            }
                            else
                            {
                                Response.Write("<p>" + SepFunctions.LangText("Page is already in your " + sTitle + ".") + "</p>");
                            }

                        }
                    }
                }
            else Response.Write("<p>" + SepFunctions.LangText("You must be login to add this page to your " + sTitle + ".") + "</p>");
        }
    }
}