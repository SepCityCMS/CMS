// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="memberships.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class memberships.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class memberships : Page
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
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
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
            GlobalVars.ModuleID = 38;

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

            if (SepCommon.SepCore.Request.Item("DoAction") == "Restricted") PageText.InnerHtml += "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have accessed a restricted area that requires a membership upgrade.") + "</div>";

            var cMemberships = SepCommon.DAL.Security.GetMemberships();

            long acount = 0;

            for (var i = 0; i <= cMemberships.Count - 1; i++)
            {
                var cBuyButton = new Button
                {
                    Text = SepFunctions.LangText("Order Now"),
                    ID = "MemberBuyButton" + i,
                    ClientIDMode = ClientIDMode.Static,
                    OnClientClick = "orderMembership('MemberOrderSelect" + i + "');return false;"
                };

                var cClassName = new Literal();
                var cDiv = new HtmlGenericControl("div");

                // if (acount % 2 == 0)
                // {
                // cDiv.Attributes.Add("class", "MembershipLeft");
                // }
                // else
                // {
                // cDiv.Attributes.Add("class", "MembershipRight");
                // }
                cClassName.Text += "<p class=\"MembershipTitle\">" + cMemberships[i].ClassName + "</p>";
                cClassName.Text += cMemberships[i].Description;

                var cSelect = new DropDownList
                {
                    ID = "MemberOrderSelect" + i,
                    ClientIDMode = ClientIDMode.Static
                };
                if (SepFunctions.Format_Currency(cMemberships[i].RecurringPrice1) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(cMemberships[i].UnitPrice1) != SepFunctions.Format_Currency(0)) cSelect.Items.Add(new ListItem(SepFunctions.Pricing_Long_Price(cMemberships[i].UnitPrice1, cMemberships[i].RecurringPrice1, cMemberships[i].RecurringCycle1), Strings.ToString(cMemberships[i].ProductID1)));
                if (SepFunctions.Format_Currency(cMemberships[i].RecurringPrice2) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(cMemberships[i].UnitPrice2) != SepFunctions.Format_Currency(0)) cSelect.Items.Add(new ListItem(SepFunctions.Pricing_Long_Price(cMemberships[i].UnitPrice2, cMemberships[i].RecurringPrice2, cMemberships[i].RecurringCycle2), Strings.ToString(cMemberships[i].ProductID2)));
                if (SepFunctions.Format_Currency(cMemberships[i].RecurringPrice3) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(cMemberships[i].UnitPrice3) != SepFunctions.Format_Currency(0)) cSelect.Items.Add(new ListItem(SepFunctions.Pricing_Long_Price(cMemberships[i].UnitPrice3, cMemberships[i].RecurringPrice3, cMemberships[i].RecurringCycle3), Strings.ToString(cMemberships[i].ProductID3)));
                if (SepFunctions.Format_Currency(cMemberships[i].RecurringPrice4) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(cMemberships[i].UnitPrice4) != SepFunctions.Format_Currency(0)) cSelect.Items.Add(new ListItem(SepFunctions.Pricing_Long_Price(cMemberships[i].UnitPrice4, cMemberships[i].RecurringPrice4, cMemberships[i].RecurringCycle4), Strings.ToString(cMemberships[i].ProductID4)));

                cDiv.Controls.Add(cClassName);
                cDiv.Controls.Add(cSelect);
                cDiv.Controls.Add(cBuyButton);

                ListClasses.Controls.Add(cDiv);
                acount += 1;
            }

            if (acount == 0) PageText.InnerHtml = SepFunctions.SendGenericError(404);
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
    }
}