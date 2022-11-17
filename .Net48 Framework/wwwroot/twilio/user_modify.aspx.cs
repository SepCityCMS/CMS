// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="user_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class user_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class user_modify : Page
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

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM TwilioUsers WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                UserID.Value = SepFunctions.openNull(RS["UserID"]);
                                UserName.Value = SepFunctions.GetUserInformation("UserName", SepFunctions.openNull(RS["UserID"]));
                                ModifyLegend.InnerText = SepFunctions.LangText("Edit User");
                                Draw_Groups(SepFunctions.openNull(RS["GroupIDs"]));
                            }
                            else
                            {
                                Draw_Groups(string.Empty);
                            }

                        }
                    }
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Draw_Groups(string.Empty);
                }
            }

            if (string.IsNullOrWhiteSpace(UserID.Value))
            {
                UserID.Value = SepCommon.SepCore.Strings.ToString(SepFunctions.GetIdentity());
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
                using (var cmd = new SqlCommand("SELECT * FROM TwilioUsers WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID.Value);
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
                    SqlStr = "UPDATE TwilioUsers SET GroupIDs=@GroupIDs WHERE UserID=@UserID";
                }
                else
                {
                    SqlStr = "INSERT INTO TwilioUsers (UserID, GroupIDs) VALUES(@UserID, @GroupIDs)";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                    cmd.Parameters.AddWithValue("@GroupIDs", !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("GroupIDs")) ? SepCommon.SepCore.Request.Item("GroupIDs") : string.Empty);
                    cmd.ExecuteNonQuery();
                }
            }

            GroupIDsDiv.InnerHtml = string.Empty;
            Draw_Groups(SepCommon.SepCore.Request.Item("GroupIDs"));

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("User has been successfully saved.") + "</div>";
        }

        /// <summary>
        /// Draws the groups.
        /// </summary>
        /// <param name="GroupIds">The group ids.</param>
        private void Draw_Groups(string GroupIds)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM TwilioGroups ORDER BY GroupName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            var sChecked = string.Empty;
                            if (SepCommon.SepCore.Strings.InStr(GroupIds, SepFunctions.openNull(RS["GroupID"])) > 0)
                            {
                                sChecked = "  checked=\"checked\"";
                            }

                            GroupIDsDiv.InnerHtml += "<div><input id=\"GroupIDs\" type=\"checkbox\" name=\"GroupIDs\" class=\"checkbox-inline\" style=\"height: 16px; width: 16px; margin: 0px;\" value=\"" + SepFunctions.openNull(RS["GroupID"]) + "\"" + sChecked + "> ";
                            GroupIDsDiv.InnerHtml += "<label for=\"GroupIDs\" style=\"display: inline-block; margin-left: 8px;\">" + SepFunctions.openNull(RS["GroupName"]) + "</label></div>";
                        }

                    }
                }
            }
        }
    }
}