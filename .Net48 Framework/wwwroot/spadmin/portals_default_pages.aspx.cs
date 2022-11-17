// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_default_pages.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class portals_default_pages.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_default_pages : Page
    {
        /// <summary>
        /// The menu identifier
        /// </summary>
        public static long MenuID = 3;

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv
        /// </summary>
        private DataView dv = new DataView();

        /// <summary>
        /// Gets or sets the grid view sort direction.
        /// </summary>
        /// <value>The grid view sort direction.</value>
        private string GridViewSortDirection
        {
            get => ViewState["SortDirection"] == null ? "ASC" : ViewState["SortDirection"].ToString();
            set => ViewState["SortDirection"] = value;
        }

        /// <summary>
        /// Gets or sets the grid view sort expression.
        /// </summary>
        /// <value>The grid view sort expression.</value>
        private string GridViewSortExpression
        {
            get => ViewState["SortExpression"] == null ? string.Empty : ViewState["SortExpression"].ToString();
            set => ViewState["SortExpression"] = value;
        }

        /// <summary>
        /// Deletes the web pages.
        /// </summary>
        /// <param name="IDs">The i ds.</param>
        /// <returns>System.String.</returns>
        public string Delete_Web_Pages(string IDs)
        {
            var arrIDs = Strings.Split(IDs, ",");
            var CanNotDelete = false;

            if (arrIDs != null)
            {
                for (var i = 0; i <= Information.UBound(arrIDs); i++)
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT UniqueID FROM PortalPages WHERE (PortalID=@PortalID OR PortalID = -1) AND UniqueID=@UniqueID AND (PageID='200' OR PageID='201')", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", arrIDs[i]);
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (!RS.HasRows) CanNotDelete = true;
                            }
                        }

                        if (CanNotDelete == false)
                        {
                            using (var cmd = new SqlCommand("UPDATE PortalPages SET Status='-1', DateDeleted=@DateDeleted WHERE (PortalID=@PortalID OR PortalID = -1) AND UniqueID=@UniqueID AND (PageID='200' OR PageID='201')", conn))
                            {
                                cmd.Parameters.AddWithValue("@UniqueID", arrIDs[i]);
                                cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                cmd.ExecuteNonQuery();
                            }

                            var cLucene = new LuceneDelete();
                            cLucene.DeleteText(995, SepFunctions.toLong(arrIDs[i]));
                        }
                    }
            }

            SepFunctions.Cache_Remove();

            return SepFunctions.LangText("Web page(s) have been successfully deleted.") + Strings.ToString(CanNotDelete ? " " + SepFunctions.LangText("(One or more web pages could not be deleted. This can be caused if you are trying to delete a page that is built into our system.)") : string.Empty);
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
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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
                    FilterDoAction.Items[0].Text = SepFunctions.LangText("Select an Action");
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Pages");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("Page Name");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Enabled");
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
        /// Handles the PageIndexChanging event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetSortDirection();
            ManageGridView.PageIndex = e.NewPageIndex;
            ManageGridView.DataSource = BindData();
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the RowCommand event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cmdArguments = Strings.Split(Strings.ToString(e.CommandArgument), "||");
            var updateCache = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (e.CommandName == "EnableLink")
                {
                    updateCache = true;
                    Array.Resize(ref cmdArguments, 4);

                    if (cmdArguments[2] == "Yes")
                    {
                        using (var cmd = new SqlCommand("UPDATE PortalPages SET Status=1 WHERE PortalID=@PortalID AND UniqueID=@UniqueID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", cmdArguments[3]);
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            cmd.ExecuteNonQuery();
                        }

                        SepFunctions.IndexRecord(995, SepFunctions.toLong(cmdArguments[3]));
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("UPDATE PortalPages SET Status='0' WHERE PortalID=@PortalID AND UniqueID=@UniqueID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", cmdArguments[3]);
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            cmd.ExecuteNonQuery();
                        }

                        var cLucene = new LuceneDelete();
                        cLucene.DeleteText(995, SepFunctions.toLong(cmdArguments[3]));
                    }
                }

                if (e.CommandName == "MoveUp" || e.CommandName == "MoveDown")
                {
                    updateCache = true;
                    Array.Resize(ref cmdArguments, 5);

                    var sID = cmdArguments[2];
                    var sNewWeight = SepFunctions.toLong(cmdArguments[3]);
                    var sMenuID = cmdArguments[4];

                    long UpdateWeight = 0;

                    string[] arrCommands = null;
                    var arrCount = 0;

                    if (e.CommandName == "MoveDown")
                        sNewWeight += 1;
                    else
                        sNewWeight -= 1;

                    using (var cmd = new SqlCommand("SELECT PageID,UniqueID,Weight,UserPageName,MenuID FROM PortalPages WHERE PortalID=@PortalID AND UniqueID <> @UniqueID AND UserPageName <> '' AND MenuID=@MenuID AND Status <> -1 ORDER BY Weight, LinkText", conn))
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", sID);
                        cmd.Parameters.AddWithValue("@MenuID", sMenuID);
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                                if (SepFunctions.ModuleActivated(SepFunctions.toInt(SepFunctions.openNull(RS["PageID"]))))
                                {
                                    UpdateWeight += 1;
                                    if (UpdateWeight == sNewWeight)
                                        UpdateWeight += 1;
                                    Array.Resize(ref arrCommands, arrCount + 1);
                                    arrCommands[arrCount] = "UPDATE PortalPages SET Weight='" + SepFunctions.FixWord(Strings.ToString(UpdateWeight)) + "' WHERE PortalID=" + SepFunctions.Get_Portal_ID() + " AND UniqueID='" + SepFunctions.openNull(RS["UniqueID"], true) + "' AND Status <> -1";
                                    arrCount += 1;
                                }

                        }
                    }

                    Array.Resize(ref arrCommands, arrCount + 1);
                    if (arrCommands != null)
                    {
                        arrCommands[arrCount] = "UPDATE PortalPages SET Weight='" + SepFunctions.FixWord(Strings.ToString(sNewWeight)) + "' WHERE PortalID=" + SepFunctions.Get_Portal_ID() + " AND UniqueID='" + SepFunctions.FixWord(sID) + "' AND Status <> -1";
                        arrCount += 1;

                        for (var i = 0; i <= Information.UBound(arrCommands); i++)
                            using (var cmd = new SqlCommand(arrCommands[i], conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                    }
                }
            }

            if (updateCache)
            {
                SepFunctions.Cache_Remove();

                if (!string.IsNullOrWhiteSpace(cmdArguments[0]))
                    MenuID = SepFunctions.toLong(cmdArguments[0]);

                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }
        }

        /// <summary>
        /// Handles the Sorting event of the ManageGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs" /> instance containing the event data.</param>
        protected void ManageGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var imgArrowUp = "<i class=\"glyphicon glyphicon-arrow-up\">";
            var imgArrowDown = "<i class=\"glyphicon glyphicon-arrow-down\">";
            for (var i = 0; i <= ManageGridView.Columns.Count - 1; i++)
            {
                var iconPosition = ManageGridView.Columns[i].HeaderText.IndexOf("<i class=\"", StringComparison.OrdinalIgnoreCase);
                if (iconPosition > 0) ManageGridView.Columns[i].HeaderText = ManageGridView.Columns[i].HeaderText.Substring(0, iconPosition);
                if (ManageGridView.Columns[i].SortExpression == e.SortExpression)
                {
                    GetSortDirection();

                    if (GetSortDirection() == "ASC") ManageGridView.Columns[i].HeaderText += imgArrowUp;
                    else ManageGridView.Columns[i].HeaderText += imgArrowDown;
                }
            }

            GridViewSortExpression = e.SortExpression;

            DeleteResult.InnerHtml = string.Empty;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Handles the Click event of the ModuleSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ModuleSearchButton_Click(object sender, EventArgs e)
        {
            DeleteResult.InnerHtml = string.Empty;

            dv = BindData();
            ManageGridView.DataSource = dv;
            ManageGridView.DataBind();

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("Your search has returned no results.") + "</div>";
                PageManageGridView.Visible = false;
            }
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
            TranslatePage();

            GlobalVars.ModuleID = 60;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("PortalsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), false) == false)
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

            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("MenuID")) > 0)
                MenuID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("MenuID"));

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0)
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No web pages have been added.") + "</div>";
                    PageManageGridView.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the RunAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RunAction_Click(object sender, EventArgs e)
        {
            GetSortDirection();
            if (string.IsNullOrWhiteSpace(FilterDoAction.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                return;
            }

            var sIDs = UniqueIDs.Value;

            if (Strings.Len(sIDs) > 0)
            {
                DeleteResult.InnerHtml = Delete_Web_Pages(sIDs);

                SepFunctions.Cache_Remove();

                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dWebPages = SepCommon.DAL.Portals.GetPortalsPages(MenuID, 0, GridViewSortExpression, GetSortDirection(), ModuleSearch.Value);

            dv = new DataView(SepFunctions.ListToDataTable(dWebPages));
            return dv;
        }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetSortDirection()
        {
            switch (Strings.ToString(GridViewSortDirection))
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;

                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }
    }
}