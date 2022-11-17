// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="friends.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class friends.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class friends : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The dv friends
        /// </summary>
        private DataView dvFriends = new DataView();

        /// <summary>
        /// The dv waiting friends
        /// </summary>
        private DataView dvWaitingFriends = new DataView();

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
        /// Gets the install folder.
        /// </summary>
        /// <param name="excludePortals">if set to <c>true</c> [exclude portals].</param>
        /// <returns>System.String.</returns>
        public string GetInstallFolder(bool excludePortals = false)
        {
            return SepFunctions.GetInstallFolder(excludePortals);
        }

        /// <summary>
        /// Saves the friend.
        /// </summary>
        public void Save_Friend()
        {
            string sUser;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
                sUser = SepFunctions.GetUserInformation("UserName", SepCommon.SepCore.Request.Item("UserID"));
            else
                sUser = FriendUserName.Value;

            if (!string.IsNullOrWhiteSpace(sUser))
            {
                SaveResult.InnerHtml = SepCommon.DAL.Friends.Friends_Save(sUser, SepFunctions.Session_User_Name());
                DeleteResult.InnerHtml = string.Empty;
            }
            else
            {
                SaveResult.InnerHtml = SepFunctions.LangText("Invalid user, could not add as friend.");
            }
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
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete Friend(s)");
                    ApproveDoAction.Items[0].Text = SepFunctions.LangText("Select an Action");
                    ApproveDoAction.Items[1].Text = SepFunctions.LangText("Approve Friend(s)");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("User Name");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("BirthDate");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Gender");
                    ManageGridView.Columns[4].HeaderText = SepFunctions.LangText("Approved");
                    ManageGridView.Columns[5].HeaderText = SepFunctions.LangText("Online");
                    ApproveFriendsGrid.Columns[1].HeaderText = SepFunctions.LangText("User Name");
                    ApproveFriendsGrid.Columns[2].HeaderText = SepFunctions.LangText("BirthDate");
                    ApproveFriendsGrid.Columns[3].HeaderText = SepFunctions.LangText("Gender");
                    ApproveFriendsGrid.Columns[4].HeaderText = SepFunctions.LangText("Online");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ApproveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ApproveDoAction.Value))
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select an action.") + "</div>";
                return;
            }

            var sIDs = UniqueIDs.Value;

            if (Strings.Len(sIDs) > 0)
            {
                try
                {
                    var sDeleteResult = SepCommon.DAL.Friends.Friends_Approve(sIDs, SepFunctions.Session_User_ID());
                    DeleteResult.InnerHtml = Strings.InStr(sDeleteResult, SepFunctions.LangText("Successfully")) > 0 ? "<div class=\"alert alert-success\" role=\"alert\">" + sDeleteResult + "</div>" : "<div class=\"alert alert-danger\" role=\"alert\">" + sDeleteResult + "</div>";
                }
                catch
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                dvWaitingFriends = BindDataApprove();
                ApproveFriendsGrid.DataSource = dvWaitingFriends;
                ApproveFriendsGrid.DataBind();

                dvFriends = BindData();
                ManageGridView.DataSource = dvFriends;
                ManageGridView.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to approve a friend.") + "</div>";
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
                if (dvFriends != null)
                {
                    dvFriends.Dispose();
                }

                if (dvWaitingFriends != null)
                {
                    dvWaitingFriends.Dispose();
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

            dvFriends = BindData();
            ManageGridView.DataSource = dvFriends;
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

            dvFriends = BindData();
            ManageGridView.DataSource = dvFriends;
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 33;

            if (SepCommon.SepCore.Request.Item("DoAction") == "SaveFriend")
            {
                if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                {
                    SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "friends.aspx?DoAction=SaveFriend&UserID=" + SepCommon.SepCore.Request.Item("UserID"));
                    SepFunctions.Redirect(sInstallFolder + "login.aspx");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
                {
                    SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "friends.aspx");
                    SepFunctions.Redirect(sInstallFolder + "login.aspx");
                }
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepCommon.SepCore.Request.Item("DoAction") == "SaveFriend") Save_Friend();

            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));

            if (!Page.IsPostBack)
            {
                dvFriends = BindData();
                ManageGridView.DataSource = dvFriends;
                ManageGridView.DataBind();

                dvWaitingFriends = BindDataApprove();
                ApproveFriendsGrid.DataSource = dvWaitingFriends;
                ApproveFriendsGrid.DataBind();
            }

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("You currently do not have any friends on our web site.") + "</div>";
                PageManageGridView.Visible = false;
            }

            if (ApproveFriendsGrid.Rows.Count == 0) ApproveGrid.Visible = false;
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
                try
                {
                    var sDeleteResult = SepCommon.DAL.Friends.Friends_Delete(sIDs);
                    DeleteResult.InnerHtml = Strings.InStr(sDeleteResult, SepFunctions.LangText("Successfully")) > 0 ? "<div class=\"alert alert-success\" role=\"alert\">" + sDeleteResult + "</div>" : "<div class=\"alert alert-danger\" role=\"alert\">" + sDeleteResult + "</div>";
                }
                catch
                {
                    DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error deleting.") + "</div>";
                }

                dvFriends = BindData();
                ManageGridView.DataSource = dvFriends;
                ManageGridView.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to run an action.") + "</div>";
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveFriend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveFriend_Click(object sender, EventArgs e)
        {
            Save_Friend();

            dvFriends = BindData();
            ManageGridView.DataSource = dvFriends;
            ManageGridView.DataBind();
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dFriends = SepCommon.DAL.Friends.GetFriends(GridViewSortExpression, GetSortDirection(), ModuleSearch.Value);

            dvFriends = new DataView(SepFunctions.ListToDataTable(dFriends));
            return dvFriends;
        }

        /// <summary>
        /// Binds the data approve.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindDataApprove()
        {
            var dFriends = SepCommon.DAL.Friends.GetFriends(GridViewSortExpression, GetSortDirection(), ApprovedFriends: false);

            dvWaitingFriends = new DataView(SepFunctions.ListToDataTable(dFriends));
            return dvWaitingFriends;
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