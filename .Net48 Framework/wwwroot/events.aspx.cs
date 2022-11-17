// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="events.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class events.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class events : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.String.</returns>
        public string Format_Date(string sDate)
        {
            return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
        }

        /// <summary>
        /// Formats the ISAPI.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public string Format_ISAPI(object sText)
        {
            return SepFunctions.Format_ISAPI(Strings.ToString(sText));
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
        }

        /// <summary>
        /// Sessions the user identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Session_UserID()
        {
            return SepFunctions.Session_User_ID();
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
                    EventType.Items[0].Text = SepFunctions.LangText("All Event Types");
                    ListContent.Columns[0].HeaderText = SepFunctions.LangText("Subject");
                    ListContent.Columns[1].HeaderText = SepFunctions.LangText("Event Type");
                    ListContent.Columns[2].HeaderText = SepFunctions.LangText("Event Date / Time");
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
                if (dv != null)
                {
                    dv.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ListContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ListContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListContent.PageIndex = e.NewPageIndex;
            ListContent.DataSource = BindData();
            ListContent.DataBind();
        }

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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 46;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("EventID")))
            {
                var jEvents = SepCommon.DAL.Events.Event_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("EventID")));

                if (jEvents.EventID > 0) SepFunctions.Redirect(sInstallFolder + "event/" + SepCommon.SepCore.Request.Item("EventID") + "/" + Format_ISAPI(jEvents.Subject));
            }

            if (SepFunctions.Setup(GlobalVars.ModuleID, "EventsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("EventsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            drawCalendar.InnerHtml = SepFunctions.Draw_Calendar(500, SepFunctions.toDate(SepCommon.SepCore.Request.Item("EventDate")), SepCommon.SepCore.Request.Item("EventType"));

            ListContent.Caption = SepFunctions.LangText("Events for ~~" + Strings.FormatDateTime(SepFunctions.toDate(SepCommon.SepCore.Request.Item("EventDate")), Strings.DateNamedFormat.LongDate) + "~~");

            if (!IsPostBack)
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT TypeID,TypeName FROM EventTypes WHERE Status <> -1 AND PortalID=@PortalID ORDER BY TypeName", conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                                while (RS.Read())
                                    EventType.Items.Add(new ListItem(SepFunctions.openNull(RS["TypeName"]), SepFunctions.openNull(RS["TypeID"])));
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("EventType"))) EventType.Value = SepCommon.SepCore.Request.Item("EventType");

                dv = BindData();
                ListContent.DataSource = dv;
                ListContent.DataBind();
            }

            if (ListContent.Rows.Count == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("There are currently no events for ~~" + Strings.FormatDateTime(SepFunctions.toDate(SepCommon.SepCore.Request.Item("EventDate")), Strings.DateNamedFormat.LongDate) + "~~") + "</div>";
                ListContent.Visible = false;
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var cEvents = SepCommon.DAL.Events.GetEvents(EventDate: Strings.ToString(SepFunctions.toDate(SepCommon.SepCore.Request.Item("EventDate"))), EventTypeID: SepFunctions.toLong(SepCommon.SepCore.Request.Item("EventType")));

            dv = new DataView(SepFunctions.ListToDataTable(cEvents));
            return dv;
        }
    }
}