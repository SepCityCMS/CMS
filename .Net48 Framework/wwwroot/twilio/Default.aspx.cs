// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="Default.aspx.cs" company="SepCity, Inc.">
//     Copyright � SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class TwilioDefault.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class TwilioDefault : Page
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
            var sVersion = SepFunctions.GetVersion();

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) == false)
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

            CWVersion.InnerHtml = sVersion;

            // using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            // {
            // conn.Open();
            // using (var cmd = new SqlCommand("SELECT * FROM TwilioGroups ORDER BY GroupName",
            // conn))
            // {
            // using (SqlDataReader RS = cmd.ExecuteReader()) {
            // while (RS.Read())
            // {
            // ListGroups.Text += "<li role=\"presentation\" id=\"" + SepCommon.SepCore.Strings.Replace(SepFunctions.ReplaceSpecial(SepFunctions.openNull(RS["GroupName"])), " ", "") + "\"><a href=\"main.aspx?FolderID=" + SepFunctions.openNull(RS["GroupID"]) + "\" onclick=\"clickNavigator('" + SepCommon.SepCore.Strings.Replace(SepFunctions.ReplaceSpecial(SepFunctions.openNull(RS["GroupName"])), " ", "") + "');\" target=\"adminnav\">" + SepFunctions.openNull(RS["GroupName"]) + "</a></li>";
            // }
            // }
            // }

            // }
        }
    }
}