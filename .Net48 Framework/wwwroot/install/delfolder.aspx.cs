// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="delfolder.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Web.UI;

    /// <summary>
    /// Class delfolder.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class delfolder : Page
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
        /// Runs the SQL scripts.
        /// </summary>
        /// <param name="sqlScript">The SQL script.</param>
        public void Run_SQL_Scripts(string sqlScript)
        {
            var SqlStr = string.Empty;
            string[] arrUpdate = Strings.Split(sqlScript, Environment.NewLine);

            // Run SQL Script
            try
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    if (arrUpdate != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrUpdate); i++)
                            if (Strings.Trim(arrUpdate[i]) == "GO")
                            {
                                using (var cmd = new SqlCommand(SqlStr + ";", conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                SqlStr = string.Empty;
                            }
                            else
                            {
                                SqlStr += arrUpdate[i] + " ";
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error creating the default tables. (" + ex.Message + ")");
            }
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            SepFunctions.Redirect(SepFunctions.GetInstallFolder(false) + "default.aspx");
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
            var runUpdate = false;

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "version.txt"))
            {
                // Run Future SQL scripts after version 1.6
                using (var objTables = new StreamReader(SepFunctions.GetDirValue("app_data") + "version.txt"))
                {
                    switch (Strings.Trim(objTables.ReadToEnd()))
                    {
                        case "1.6":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "1.7":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "1.8":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "1.9":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.0":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.1":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.2":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.3":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.4":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.5":

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.6":

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.7":

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.8":
                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "2.9":

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;

                        case "3.0":

                            using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                            {
                                Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                            }

                            runUpdate = true;
                            break;
                    }
                }
            }
            else
            {
                using (var objTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.6.sql"))
                {
                    Run_SQL_Scripts(objTables.ReadToEnd());
                }

                using (var objTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.7.sql"))
                {
                    Run_SQL_Scripts(objTables.ReadToEnd());
                }

                using (var objTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.8.sql"))
                {
                    Run_SQL_Scripts(objTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-1.9.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.0.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.1.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.2.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.3.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.4.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.5.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.6.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.7.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.8.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-2.9.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.0.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                using (var objUpdateTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\update-3.1.sql"))
                {
                    Run_SQL_Scripts(objUpdateTables.ReadToEnd());
                }

                runUpdate = true;
            }

            if (runUpdate)
            {
                using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "version.txt"))
                {
                    outfile.Write(SepFunctions.GetVersion());
                }

                ModifyLegend.InnerText = "Update has been successful.";
                InstallText.InnerHtml = "You have successfully updated SepCity! Please remove the install folder from your web server and click below to continue to your site.";
            }
        }
    }
}