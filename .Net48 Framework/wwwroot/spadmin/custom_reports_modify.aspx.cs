// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="custom_reports_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class custom_reports_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class custom_reports_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Custom Report");
                    ReportTitleLabel.InnerText = SepFunctions.LangText("Report Title:");
                    SQLStatementLabel.InnerText = SepFunctions.LangText("SQL Statement:");
                    FieldNameRequired.ErrorMessage = SepFunctions.LangText("~~Report Title~~ is required.");
                    SQLStatementRequired.ErrorMessage = SepFunctions.LangText("~~SQL Statement~~ is required.");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false) == false)
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReportID")))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT Description,ScriptText FROM Scripts WHERE ID=@ReportID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ReportID", SepCommon.SepCore.Request.Item("ReportID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                string strXml = SepFunctions.openNull(RS["ScriptText"]);
                                ReportID.Value = SepCommon.SepCore.Request.Item("ReportID");
                                ReportTitle.Value = SepFunctions.HTMLDecode(SepFunctions.openNull(RS["Description"]));
                                SQLStatement.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("SQLSTATEMENT", strXml));
                                Head1.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD1", strXml));
                                Head2.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD2", strXml));
                                Head3.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD3", strXml));
                                Head4.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD4", strXml));
                                Head5.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD5", strXml));
                                Head6.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD6", strXml));
                                Head7.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD7", strXml));
                                Head8.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD8", strXml));
                                Head9.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD9", strXml));
                                Head10.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD10", strXml));
                                Head11.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD11", strXml));
                                Head12.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD12", strXml));
                                Head13.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD13", strXml));
                                Head14.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD14", strXml));
                                Head15.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD15", strXml));
                                Head16.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD16", strXml));
                                Head17.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD17", strXml));
                                Head18.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("HEAD18", strXml));
                                Body1.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY1", strXml));
                                Body2.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY2", strXml));
                                Body3.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY3", strXml));
                                Body4.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY4", strXml));
                                Body5.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY5", strXml));
                                Body6.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY6", strXml));
                                Body7.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY7", strXml));
                                Body8.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY8", strXml));
                                Body9.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY9", strXml));
                                Body10.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY10", strXml));
                                Body11.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY11", strXml));
                                Body12.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY12", strXml));
                                Body13.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY13", strXml));
                                Body14.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY14", strXml));
                                Body15.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY15", strXml));
                                Body16.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY16", strXml));
                                Body17.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY17", strXml));
                                Body18.Value = SepFunctions.HTMLDecode(SepFunctions.ParseXML("BODY18", strXml));
                            }

                        }
                    }
                }
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
            var strXml = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ID FROM Scripts WHERE ID=@ReportID", conn))
                {
                    cmd.Parameters.AddWithValue("@ReportID", ReportID.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            bUpdate = true;
                        }

                    }
                }

                strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                strXml += "<ROOT>" + Environment.NewLine;
                strXml += "<SQLSTATEMENT>" + SepFunctions.HTMLEncode(SQLStatement.Value) + "</SQLSTATEMENT>" + Environment.NewLine;
                strXml += "<HEAD1>" + SepFunctions.HTMLEncode(Head1.Value) + "</HEAD1>" + Environment.NewLine;
                strXml += "<HEAD2>" + SepFunctions.HTMLEncode(Head2.Value) + "</HEAD2>" + Environment.NewLine;
                strXml += "<HEAD3>" + SepFunctions.HTMLEncode(Head3.Value) + "</HEAD3>" + Environment.NewLine;
                strXml += "<HEAD4>" + SepFunctions.HTMLEncode(Head4.Value) + "</HEAD4>" + Environment.NewLine;
                strXml += "<HEAD5>" + SepFunctions.HTMLEncode(Head5.Value) + "</HEAD5>" + Environment.NewLine;
                strXml += "<HEAD6>" + SepFunctions.HTMLEncode(Head6.Value) + "</HEAD6>" + Environment.NewLine;
                strXml += "<HEAD7>" + SepFunctions.HTMLEncode(Head7.Value) + "</HEAD7>" + Environment.NewLine;
                strXml += "<HEAD8>" + SepFunctions.HTMLEncode(Head8.Value) + "</HEAD8>" + Environment.NewLine;
                strXml += "<HEAD9>" + SepFunctions.HTMLEncode(Head9.Value) + "</HEAD9>" + Environment.NewLine;
                strXml += "<HEAD10>" + SepFunctions.HTMLEncode(Head10.Value) + "</HEAD10>" + Environment.NewLine;
                strXml += "<HEAD11>" + SepFunctions.HTMLEncode(Head11.Value) + "</HEAD11>" + Environment.NewLine;
                strXml += "<HEAD12>" + SepFunctions.HTMLEncode(Head12.Value) + "</HEAD12>" + Environment.NewLine;
                strXml += "<HEAD13>" + SepFunctions.HTMLEncode(Head13.Value) + "</HEAD13>" + Environment.NewLine;
                strXml += "<HEAD14>" + SepFunctions.HTMLEncode(Head14.Value) + "</HEAD14>" + Environment.NewLine;
                strXml += "<HEAD15>" + SepFunctions.HTMLEncode(Head15.Value) + "</HEAD15>" + Environment.NewLine;
                strXml += "<HEAD16>" + SepFunctions.HTMLEncode(Head16.Value) + "</HEAD16>" + Environment.NewLine;
                strXml += "<HEAD17>" + SepFunctions.HTMLEncode(Head17.Value) + "</HEAD17>" + Environment.NewLine;
                strXml += "<HEAD18>" + SepFunctions.HTMLEncode(Head18.Value) + "</HEAD18>" + Environment.NewLine;
                strXml += "<BODY1>" + SepFunctions.HTMLEncode(Body1.Value) + "</BODY1>" + Environment.NewLine;
                strXml += "<BODY2>" + SepFunctions.HTMLEncode(Body2.Value) + "</BODY2>" + Environment.NewLine;
                strXml += "<BODY3>" + SepFunctions.HTMLEncode(Body3.Value) + "</BODY3>" + Environment.NewLine;
                strXml += "<BODY4>" + SepFunctions.HTMLEncode(Body4.Value) + "</BODY4>" + Environment.NewLine;
                strXml += "<BODY5>" + SepFunctions.HTMLEncode(Body5.Value) + "</BODY5>" + Environment.NewLine;
                strXml += "<BODY6>" + SepFunctions.HTMLEncode(Body6.Value) + "</BODY6>" + Environment.NewLine;
                strXml += "<BODY7>" + SepFunctions.HTMLEncode(Body7.Value) + "</BODY7>" + Environment.NewLine;
                strXml += "<BODY8>" + SepFunctions.HTMLEncode(Body8.Value) + "</BODY8>" + Environment.NewLine;
                strXml += "<BODY9>" + SepFunctions.HTMLEncode(Body9.Value) + "</BODY9>" + Environment.NewLine;
                strXml += "<BODY10>" + SepFunctions.HTMLEncode(Body10.Value) + "</BODY10>" + Environment.NewLine;
                strXml += "<BODY11>" + SepFunctions.HTMLEncode(Body11.Value) + "</BODY11>" + Environment.NewLine;
                strXml += "<BODY12>" + SepFunctions.HTMLEncode(Body12.Value) + "</BODY12>" + Environment.NewLine;
                strXml += "<BODY13>" + SepFunctions.HTMLEncode(Body13.Value) + "</BODY13>" + Environment.NewLine;
                strXml += "<BODY14>" + SepFunctions.HTMLEncode(Body14.Value) + "</BODY14>" + Environment.NewLine;
                strXml += "<BODY15>" + SepFunctions.HTMLEncode(Body15.Value) + "</BODY15>" + Environment.NewLine;
                strXml += "<BODY16>" + SepFunctions.HTMLEncode(Body16.Value) + "</BODY16>" + Environment.NewLine;
                strXml += "<BODY17>" + SepFunctions.HTMLEncode(Body17.Value) + "</BODY17>" + Environment.NewLine;
                strXml += "<BODY18>" + SepFunctions.HTMLEncode(Body18.Value) + "</BODY18>" + Environment.NewLine;
                strXml += "</ROOT>" + Environment.NewLine;

                var SqlStr = string.Empty;
                if (bUpdate)
                    SqlStr = "UPDATE Scripts SET ScriptText=@ScriptText, Description=@ReportTitle WHERE ID=@ReportID";
                else
                    SqlStr = "INSERT INTO Scripts (ScriptType, Description, ScriptText, PortalIDs, DatePosted) VALUES ('REPORT', @ReportTitle, @ScriptText, '|0|', @DatePosted)";
                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ReportID", ReportID.Value);
                    cmd.Parameters.AddWithValue("@ReportTitle", ReportTitle.Value);
                    cmd.Parameters.AddWithValue("@ScriptText", strXml);
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                if (bUpdate == false)
                {
                    using (var cmd = new SqlCommand("SELECT ID FROM Scripts ORDER BY ID DESC", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                ReportID.Value = SepFunctions.openNull(RS["ID"]);
                            }

                        }
                    }
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Report has been successfully saved.") + "</div>";
        }
    }
}