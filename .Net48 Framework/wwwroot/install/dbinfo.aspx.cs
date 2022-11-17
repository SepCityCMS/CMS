// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="dbinfo.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class dbinfo.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class dbinfo : Page
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
        /// Handles the Click event of the BackButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void BackButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect("default.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            var connectionString = "DATABASE=" + SepFunctions.FixWord(DatabaseName.Value) + ";SERVER=" + SepFunctions.FixWord(DatabaseAddress.Value) + ";user id=" + SepFunctions.FixWord(DatabaseUser.Value) + ";PASSWORD=" + SepFunctions.FixWord(DatabasePass.Value) + ";";

            ErrorMessage.InnerHtml = string.Empty;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT UserID FROM Members", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                Session["DBAddress"] = DatabaseAddress.Value;
                                Session["DBName"] = DatabaseName.Value;
                                Session["DBUser"] = DatabaseUser.Value;
                                Session["DBPass"] = DatabasePass.Value;
                                Session["DBCategories"] = DatabaseCategories.Value;
                                Session["DBSampleData"] = "No";

                                SepFunctions.Redirect("run_upgrade.aspx");
                                return;
                            }

                        }
                    }
                }
            }
            catch
            {
            }

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand())
                    {
                        var _with1 = cmd;
                        _with1.CommandType = CommandType.Text;
                        _with1.Connection = conn;
                        _with1.CommandText = "CREATE TABLE TestTable (Field1 varchar(20), Field2 Int)";
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand())
                    {
                        var _with2 = cmd;
                        _with2.CommandType = CommandType.Text;
                        _with2.Connection = conn;
                        _with2.CommandText = "DROP TABLE TestTable";
                        cmd.ExecuteNonQuery();
                    }

                    Session["DBAddress"] = DatabaseAddress.Value;
                    Session["DBName"] = DatabaseName.Value;
                    Session["DBUser"] = DatabaseUser.Value;
                    Session["DBPass"] = DatabasePass.Value;
                    Session["DBCategories"] = DatabaseCategories.Value;
                    Session["DBSampleData"] = "No";
                    SepFunctions.Redirect("personal.aspx");
                }
            }
            catch
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Invalid database information.</div>";
            }
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
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml")) SepFunctions.Redirect("installed.aspx");
            Label menuLabel = (Label)Master.FindControl("DBInfoSpan");
            if (menuLabel != null)
                menuLabel.Font.Bold = true;
        }
    }
}