// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="change_domain.aspx.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
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
    /// Class change_domain.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class change_domain : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Change Domain");
                    OldDomainLabel.InnerText = SepFunctions.LangText("Old Domain Name:");
                    NewDomainLabel.InnerText = SepFunctions.LangText("New Domain Name:");
                    OldDomainRequired.ErrorMessage = SepFunctions.LangText("~~Old Domain Name~~ is required.");
                    NewDomainRequired.ErrorMessage = SepFunctions.LangText("~~New Domain Name~~ is required.");
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
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var SqlStr = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UniqueID,PageText FROM ModulesNPages WHERE PageText LIKE '%" + SepFunctions.FixWord(OldDomain.Value) + "%'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                                SqlStr += "UPDATE ModulesNPages SET PageText='" + SepFunctions.FixWord(Strings.Replace(SepFunctions.openNull(RS["PageText"]), OldDomain.Value, NewDomain.Value)) + "' WHERE UniqueID='" + SepFunctions.openNull(RS["UniqueID"], true) + "';";
                    }
                }

                if (!string.IsNullOrWhiteSpace(SqlStr))
                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                SqlStr = string.Empty;

                using (var cmd = new SqlCommand("SELECT UniqueID,PageText FROM PortalPages WHERE PageText LIKE '%" + SepFunctions.FixWord(OldDomain.Value) + "%'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                                SqlStr += "UPDATE PortalPages SET PageText='" + SepFunctions.FixWord(Strings.Replace(SepFunctions.openNull(RS["PageText"]), OldDomain.Value, NewDomain.Value)) + "' WHERE UniqueID='" + SepFunctions.openNull(RS["UniqueID"], true) + "';";
                    }
                }

                if (!string.IsNullOrWhiteSpace(SqlStr))
                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Domain Successfully Changed") + "</div>";
        }
    }
}