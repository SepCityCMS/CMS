// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_members.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class portals_members.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_members : Page
    {
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
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Memberses the add access key.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Add_Access_Key(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Add_Access_Key(SepFunctions.toLong(KeyID.Value), UserIDs);
        }

        /// <summary>
        /// Memberses the add group.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Add_Group(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Add_Group(SepFunctions.toLong(ListID.Value), UserIDs);
        }

        /// <summary>
        /// Memberses the delete.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Delete(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Delete(UserIDs);
        }

        /// <summary>
        /// Memberses the mark active.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Mark_Active(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Mark_Active(UserIDs);
        }

        /// <summary>
        /// Memberses the mark not active.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Mark_Not_Active(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Mark_Not_Active(UserIDs);
        }

        /// <summary>
        /// Memberses the move class.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Move_Class(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Move_Class(SepFunctions.toLong(ClassID.Value), UserIDs);
        }

        /// <summary>
        /// Memberses the remove access key.
        /// </summary>
        /// <param name="UserIDs">The user i ds.</param>
        /// <returns>System.String.</returns>
        public string Members_Remove_Access_Key(string UserIDs)
        {
            return SepCommon.DAL.Members.Member_Remove_Access_Key(SepFunctions.toLong(KeyID.Value), UserIDs);
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
                    FilterDoAction.Items[1].Text = SepFunctions.LangText("Delete members");
                    FilterDoAction.Items[2].Text = SepFunctions.LangText("Mark as active");
                    FilterDoAction.Items[3].Text = SepFunctions.LangText("Mark as not active");
                    FilterDoAction.Items[4].Text = SepFunctions.LangText("Move to a class");
                    FilterDoAction.Items[5].Text = SepFunctions.LangText("Add access key");
                    FilterDoAction.Items[6].Text = SepFunctions.LangText("Remove access key");
                    FilterDoAction.Items[7].Text = SepFunctions.LangText("Add to group list");
                    ManageGridView.Columns[2].HeaderText = SepFunctions.LangText("User Name");
                    ManageGridView.Columns[3].HeaderText = SepFunctions.LangText("Full Name");
                    ManageGridView.Columns[4].HeaderText = SepFunctions.LangText("Email Address");
                    ManageGridView.Columns[5].HeaderText = SepFunctions.LangText("Active");
                    ManageGridView.Columns[6].HeaderText = SepFunctions.LangText("Last Login");
                    ManageGridView.Columns[7].HeaderText = SepFunctions.LangText("Signup Date");
                    RunAction.InnerText = SepFunctions.LangText("GO");
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
                ManageGridView.Visible = false;
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), true) == false)
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

            if (!Page.IsPostBack)
            {
                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }

            if (ManageGridView.Rows.Count == 0)
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No members have been added.") + "</div>";
                PageManageGridView.Visible = false;
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
                if (FilterDoAction.Value == "DeleteMembers") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Delete(sIDs) + "</div>";
                if (FilterDoAction.Value == "MarkActive") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Mark_Active(sIDs) + "</div>";
                if (FilterDoAction.Value == "MarkNotActive") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Mark_Not_Active(sIDs) + "</div>";
                if (FilterDoAction.Value == "MoveClass") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Move_Class(sIDs) + "</div>";
                if (FilterDoAction.Value == "AddAccessKey") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Add_Access_Key(sIDs) + "</div>";
                if (FilterDoAction.Value == "RemoveAccessKey") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Remove_Access_Key(sIDs) + "</div>";
                if (FilterDoAction.Value == "AddGroup") DeleteResult.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + Members_Add_Group(sIDs) + "</div>";

                dv = BindData();
                ManageGridView.DataSource = dv;
                ManageGridView.DataBind();
            }
            else
            {
                DeleteResult.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select at lease one item to perform this action.") + "</div>";
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>DataView.</returns>
        private DataView BindData()
        {
            var dMembers = SepCommon.DAL.Members.GetMembers(GridViewSortExpression, GetSortDirection(), ModuleSearch.Value, SepFunctions.Get_Portal_ID());

            dv = new DataView(SepFunctions.ListToDataTable(dMembers));
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