// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="group_modify.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.twilio
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class group_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class group_modify : Page
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("GroupID")))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM TwilioGroups WHERE GroupID=@GroupID", conn))
                    {
                        cmd.Parameters.AddWithValue("@GroupID", SepCommon.SepCore.Request.Item("GroupID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                GroupID.Value = SepFunctions.openNull(RS["GroupID"]);
                                GroupName.Value = SepFunctions.openNull(RS["GroupName"]);
                                ModifyLegend.InnerText = SepFunctions.LangText("Edit Group");
                            }

                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(GroupID.Value))
            {
                GroupID.Value = SepCommon.SepCore.Strings.ToString(SepFunctions.GetIdentity());
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            bool bUpdate = false;
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioGroups WHERE GroupID=@GroupID", conn))
                {
                    cmd.Parameters.AddWithValue("@GroupID", GroupID.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            bUpdate = true;
                        }

                    }
                }

                string SqlStr;
                if (bUpdate)
                {
                    SqlStr = "UPDATE TwilioGroups SET GroupName=@GroupName WHERE GroupID=@GroupID";
                }
                else
                {
                    SqlStr = "INSERT INTO TwilioGroups (GroupID, GroupName) VALUES(@GroupID, @GroupName)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@GroupID", GroupID.Value);
                    cmd.Parameters.AddWithValue("@GroupName", GroupName.Value);
                    cmd.ExecuteNonQuery();
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Group has been successfully saved.") + "</div>";
        }
    }
}