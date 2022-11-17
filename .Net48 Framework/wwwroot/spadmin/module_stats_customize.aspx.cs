// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="module_stats_customize.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class module_stats_customize.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class module_stats_customize : Page
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
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Customize the Module Statistics");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
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
            TranslatePage();

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), false) == false)
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

            if (!Page.IsPostBack)
            {
                var sXML = string.Empty;

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='ADMMAIN' AND UserID='0'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sXML = SepFunctions.openNull(RS["ScriptText"]);
                            }
                            else
                            {
                                using (var cDefaults = new module_stats())
                                {
                                    sXML = cDefaults.Get_Default_XML();
                                }
                            }

                        }
                    }
                }

                Field1.Value = SepFunctions.ParseXML("FIELD1", sXML);
                Field2.Value = SepFunctions.ParseXML("FIELD2", sXML);
                Field3.Value = SepFunctions.ParseXML("FIELD3", sXML);
                Field4.Value = SepFunctions.ParseXML("FIELD4", sXML);
                Field5.Value = SepFunctions.ParseXML("FIELD5", sXML);
                Field6.Value = SepFunctions.ParseXML("FIELD6", sXML);
                Field7.Value = SepFunctions.ParseXML("FIELD7", sXML);
                Field8.Value = SepFunctions.ParseXML("FIELD8", sXML);
                Field9.Value = SepFunctions.ParseXML("FIELD9", sXML);
                Field10.Value = SepFunctions.ParseXML("FIELD10", sXML);
                Field11.Value = SepFunctions.ParseXML("FIELD11", sXML);
                Field12.Value = SepFunctions.ParseXML("FIELD12", sXML);
                Field13.Value = SepFunctions.ParseXML("FIELD13", sXML);
                Field14.Value = SepFunctions.ParseXML("FIELD14", sXML);
                Field15.Value = SepFunctions.ParseXML("FIELD15", sXML);
                Field16.Value = SepFunctions.ParseXML("FIELD16", sXML);
                Field17.Value = SepFunctions.ParseXML("FIELD17", sXML);
                Field18.Value = SepFunctions.ParseXML("FIELD18", sXML);
                Field19.Value = SepFunctions.ParseXML("FIELD19", sXML);
                Field20.Value = SepFunctions.ParseXML("FIELD20", sXML);
                Field21.Value = SepFunctions.ParseXML("FIELD21", sXML);
                Field22.Value = SepFunctions.ParseXML("FIELD22", sXML);
                Field23.Value = SepFunctions.ParseXML("FIELD23", sXML);
                Field24.Value = SepFunctions.ParseXML("FIELD24", sXML);
                Field25.Value = SepFunctions.ParseXML("FIELD25", sXML);
                Field26.Value = SepFunctions.ParseXML("FIELD26", sXML);
                Field27.Value = SepFunctions.ParseXML("FIELD27", sXML);
                Field28.Value = SepFunctions.ParseXML("FIELD28", sXML);
                Field29.Value = SepFunctions.ParseXML("FIELD29", sXML);
                Field30.Value = SepFunctions.ParseXML("FIELD30", sXML);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var bUpdate = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='ADMMAIN' AND UserID='0'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows) bUpdate = true;
                    }
                }

                string sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                sXML += "<root>" + Environment.NewLine;
                sXML += "<FIELD1>" + SepFunctions.HTMLEncode(Field1.Value) + "</FIELD1>" + Environment.NewLine;
                sXML += "<FIELD2>" + SepFunctions.HTMLEncode(Field2.Value) + "</FIELD2>" + Environment.NewLine;
                sXML += "<FIELD3>" + SepFunctions.HTMLEncode(Field3.Value) + "</FIELD3>" + Environment.NewLine;
                sXML += "<FIELD4>" + SepFunctions.HTMLEncode(Field4.Value) + "</FIELD4>" + Environment.NewLine;
                sXML += "<FIELD5>" + SepFunctions.HTMLEncode(Field5.Value) + "</FIELD5>" + Environment.NewLine;
                sXML += "<FIELD6>" + SepFunctions.HTMLEncode(Field6.Value) + "</FIELD6>" + Environment.NewLine;
                sXML += "<FIELD7>" + SepFunctions.HTMLEncode(Field7.Value) + "</FIELD7>" + Environment.NewLine;
                sXML += "<FIELD8>" + SepFunctions.HTMLEncode(Field8.Value) + "</FIELD8>" + Environment.NewLine;
                sXML += "<FIELD9>" + SepFunctions.HTMLEncode(Field9.Value) + "</FIELD9>" + Environment.NewLine;
                sXML += "<FIELD10>" + SepFunctions.HTMLEncode(Field10.Value) + "</FIELD10>" + Environment.NewLine;
                sXML += "<FIELD11>" + SepFunctions.HTMLEncode(Field11.Value) + "</FIELD11>" + Environment.NewLine;
                sXML += "<FIELD12>" + SepFunctions.HTMLEncode(Field12.Value) + "</FIELD12>" + Environment.NewLine;
                sXML += "<FIELD13>" + SepFunctions.HTMLEncode(Field13.Value) + "</FIELD13>" + Environment.NewLine;
                sXML += "<FIELD14>" + SepFunctions.HTMLEncode(Field14.Value) + "</FIELD14>" + Environment.NewLine;
                sXML += "<FIELD15>" + SepFunctions.HTMLEncode(Field15.Value) + "</FIELD15>" + Environment.NewLine;
                sXML += "<FIELD16>" + SepFunctions.HTMLEncode(Field16.Value) + "</FIELD16>" + Environment.NewLine;
                sXML += "<FIELD17>" + SepFunctions.HTMLEncode(Field17.Value) + "</FIELD17>" + Environment.NewLine;
                sXML += "<FIELD18>" + SepFunctions.HTMLEncode(Field18.Value) + "</FIELD18>" + Environment.NewLine;
                sXML += "<FIELD19>" + SepFunctions.HTMLEncode(Field19.Value) + "</FIELD19>" + Environment.NewLine;
                sXML += "<FIELD20>" + SepFunctions.HTMLEncode(Field20.Value) + "</FIELD20>" + Environment.NewLine;
                sXML += "<FIELD21>" + SepFunctions.HTMLEncode(Field21.Value) + "</FIELD21>" + Environment.NewLine;
                sXML += "<FIELD22>" + SepFunctions.HTMLEncode(Field22.Value) + "</FIELD22>" + Environment.NewLine;
                sXML += "<FIELD23>" + SepFunctions.HTMLEncode(Field23.Value) + "</FIELD23>" + Environment.NewLine;
                sXML += "<FIELD24>" + SepFunctions.HTMLEncode(Field24.Value) + "</FIELD24>" + Environment.NewLine;
                sXML += "<FIELD25>" + SepFunctions.HTMLEncode(Field25.Value) + "</FIELD25>" + Environment.NewLine;
                sXML += "<FIELD26>" + SepFunctions.HTMLEncode(Field26.Value) + "</FIELD26>" + Environment.NewLine;
                sXML += "<FIELD27>" + SepFunctions.HTMLEncode(Field27.Value) + "</FIELD27>" + Environment.NewLine;
                sXML += "<FIELD28>" + SepFunctions.HTMLEncode(Field28.Value) + "</FIELD28>" + Environment.NewLine;
                sXML += "<FIELD29>" + SepFunctions.HTMLEncode(Field29.Value) + "</FIELD29>" + Environment.NewLine;
                sXML += "<FIELD30>" + SepFunctions.HTMLEncode(Field30.Value) + "</FIELD30>" + Environment.NewLine;
                sXML += "</root>" + Environment.NewLine;

                var SqlStr = string.Empty;
                if (bUpdate)
                    SqlStr = "UPDATE Scripts SET ScriptText=@ScriptText WHERE ScriptType='ADMMAIN' AND UserID='0'";
                else
                    SqlStr = "INSERT INTO Scripts (ScriptType, Description, ScriptText, ModuleIDs, CatIDs, PortalIDs, DatePosted, UserID) VALUES ('ADMMAIN', 'Module Statistics', @ScriptText, '|0|', '|0|', '|0|', @DatePosted, '0')";
                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ScriptText", sXML);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Module Statistics have been successfully saved.") + "</div>";
        }
    }
}