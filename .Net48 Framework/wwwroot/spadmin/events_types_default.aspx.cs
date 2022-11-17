// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="events_types_default.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class events_types_default.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class events_types_default : Page
    {
        /// <summary>
        /// Deletes the default categories.
        /// </summary>
        public static void Del_Default_Categories()
        {
        }

        /// <summary>
        /// Adds the default categories.
        /// </summary>
        public void Add_Default_Categories()
        {
            PageHeader.InnerText = SepFunctions.LangText("Add Default Categories");
            if (File.Exists(SepFunctions.GetDirValue("App_Data") + "DefaultData\\cat-add-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("ModuleID")) + ".sql"))
            {
                Run_SQL_Script("cat-add-" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + ".sql");

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("INSERT INTO Scripts (ModuleIDs,CatIDs,UserID,PortalIDs,ScriptType,Description,ScriptText,DatePosted) VALUES('|" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ModuleID")) + "|','','" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "','','" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("SType")) + "','Default Data Insert','','" + DateTime.Today + "')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Default data has been added successfully.") + "</div>";
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There is a missing SQL Script file in your install. Please contact support to fix this function.") + "</div>";
            }
        }

        /// <summary>
        /// Adds the default data.
        /// </summary>
        public void Add_Default_Data()
        {
            PageHeader.InnerText = SepFunctions.LangText("Add Default Data");
            if (File.Exists(SepFunctions.GetDirValue("App_Data") + "DefaultData\\def-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("SQL")) + "-add.sql"))
            {
                Run_SQL_Script("def-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("SQL")) + "-add.sql");

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("INSERT INTO Scripts (ModuleIDs,CatIDs,UserID,PortalIDs,ScriptType,Description,ScriptText,DatePosted) VALUES('|" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ModuleID")) + "|','','" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "','','" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("SType")) + "','Default Data Insert','','" + DateTime.Today + "')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Default data has been added successfully.") + "</div>";
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There is a missing SQL Script file in your install. Please contact support to fix this function.") + "</div>";
            }
        }

        /// <summary>
        /// Defaults the data.
        /// </summary>
        public void Default_Data()
        {
            PageHeader.InnerText = SepFunctions.LangText("Invalid");
            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid Request") + "</div>";
        }

        /// <summary>
        /// Deletes the default data.
        /// </summary>
        public void Del_Default_Data()
        {
            PageHeader.InnerText = SepFunctions.LangText("Delete Default Data");
            if (File.Exists(SepFunctions.GetDirValue("App_Data") + "DefaultData\\def-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("SQL")) + "-del.sql"))
            {
                Run_SQL_Script("def-" + SepFunctions.CleanFileName(SepCommon.SepCore.Request.Item("SQL")) + "-del.sql");

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM Scripts WHERE ScriptType='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("SType")) + "' AND ModuleIDs LIKE '%|" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ModuleID")) + "|%'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Default data has been removed successfully.") + "</div>";
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There is a missing SQL Script file in your install. Please contact support to fix this function.") + "</div>";
            }
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
        /// Runs the SQL script.
        /// </summary>
        /// <param name="sFile">The s file.</param>
        public void Run_SQL_Script(string sFile)
        {
            var SqlData = string.Empty;
            if (File.Exists(SepFunctions.GetDirValue("App_Data") + "DefaultData\\" + sFile))
            {
                using (var oFile = new StreamReader(SepFunctions.GetDirValue("App_Data") + "DefaultData\\" + sFile))
                {
                    SqlData = Strings.ToString(oFile.Read());
                }

                string[] arrSqlData = Strings.Split(SqlData, "[[GO]]");
                if (arrSqlData != null)
                {
                    for (var i = 0; i <= Information.UBound(arrSqlData); i++)
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(arrSqlData[i]) && arrSqlData[i] != "[[GO]]")
                                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                                {
                                    conn.Open();
                                    using (var cmd = new SqlCommand(arrSqlData[i], conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                        }
                        catch
                        {
                        }
                }
            }
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
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
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("EventsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("EventsAdmin"), true) == false)
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

            switch (SepCommon.SepCore.Request.Item("DoAction"))
            {
                case "AddDefaultData":
                    Add_Default_Data();
                    break;

                case "DelDefaultData":
                    Del_Default_Data();
                    break;

                case "AddDefaultCat":
                    Add_Default_Categories();
                    break;

                case "DelDefaultCat":
                    Del_Default_Categories();
                    break;

                default:
                    Default_Data();
                    break;
            }
        }
    }
}