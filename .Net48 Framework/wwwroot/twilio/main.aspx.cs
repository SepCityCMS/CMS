// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="main.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.twilio
{
    using global::Twilio;
    using global::Twilio.Rest.Api.V2010.Account;
    using SepCommon;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class main.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class main : Page
    {
        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable BindData()
        {
            TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));
            var recordings = RecordingResource.Read();

            var tbl = new DataTable();
            var col1 = new DataColumn("Sid", typeof(string));
            var col2 = new DataColumn("CallSid", typeof(string));
            var col3 = new DataColumn("DateCreated", typeof(string));
            var col4 = new DataColumn("Duration", typeof(string));
            var col5 = new DataColumn("Url", typeof(string));
            tbl.Columns.Add(col1);
            tbl.Columns.Add(col2);
            tbl.Columns.Add(col3);
            tbl.Columns.Add(col4);
            tbl.Columns.Add(col5);
            foreach (var record in recordings)
            {
                if (record.Status == RecordingResource.StatusEnum.Completed)
                {
                    var row = tbl.NewRow();
                    row["Sid"] = record.Sid;
                    row["CallSid"] = record.CallSid;
                    row["DateCreated"] = record.DateCreated;
                    row["Duration"] = record.Duration;
                    row["Url"] = "https://api.twilio.com/2010-04-01/Accounts/" + SepFunctions.Setup(989, "TwilioAccountSID") + "/Recordings/" + record.Sid + ".mp3";
                    tbl.Rows.Add(row);
                }
            }

            return tbl;
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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
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

            if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAccountSID")) && string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAuthToken")))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must setup the API information first.") + "</div>";
                ManageGridView.Visible = false;
                return;
            }

            ManageGridView.DataSource = BindData();
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the Click event of the RunAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RunAction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterDoAction.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                return;
            }

            var sIDs = UniqueIDs.Value;

            if (SepCommon.SepCore.Strings.Len(sIDs) > 0)
            {
                try
                {
                    var arrIDs = SepCommon.SepCore.Strings.Split(sIDs, ",");

                    if (arrIDs != null)
                    {
                        string sAccountSID = SepFunctions.Setup(989, "TwilioAccountSID");
                        TwilioClient.Init(sAccountSID, SepFunctions.Setup(989, "TwilioAuthToken"));
                        for (var i = 0; i <= SepCommon.SepCore.Information.UBound(arrIDs); i++)
                        {
                            RecordingResource.Delete(pathSid: arrIDs[i], pathAccountSid: sAccountSID);
                        }
                    }
                }
                catch
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                DeleteResult.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully deleted recording(s)") + "</div>";

                ManageGridView.DataSource = BindData();
                ManageGridView.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
        }
    }
}