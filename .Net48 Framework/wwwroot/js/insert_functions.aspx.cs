// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="insert_functions.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class insert_functions.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class insert_functions : Page
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
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Account Menu"), "[[AccountMenu]]"));

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SepFunctions.Setup(2, "AdsEnable") == "Enable")
                    using (var cmd = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ModuleID='2' AND Status <> -1 ORDER BY ZoneName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read()) dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Advertisement (~~" + SepFunctions.openNull(RS["ZoneName"]) + "~~)"), "[[Ads|" + SepFunctions.openNull(RS["ZoneName"]) + "]]"));
                        }
                    }

                using (var cmd = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ModuleID='1' AND Status <> -1 ORDER BY ZoneName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read()) dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Content Rotator (~~" + SepFunctions.openNull(RS["ZoneName"]) + "~~)"), "[[CR|" + SepFunctions.openNull(RS["ZoneName"]) + "]]"));
                    }
                }
            }

            if (SepFunctions.Setup(46, "EventsEnable") == "Enable" && SepFunctions.ModuleActivated(46)) dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Event Calendar"), "[[EventCalendar]]"));
            if (SepFunctions.Setup(33, "FriendsEnable") == "Enable") dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Friends List"), "[[FriendList]]"));
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Latest Signups"), "[[NewestMembers]]"));
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Member Statistics"), "[[MemberStats]]"));
            if (SepFunctions.Setup(25, "PollsEnable") == "Enable" && SepFunctions.ModuleActivated(25)) dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Random Polls"), "[[Polls]]"));
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Search Engine"), "[[SearchEngine]]"));
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Site Logo"), "[[SiteLogo]]"));
            if (SepFunctions.Setup(993, "UsrChangeLayout") == "Enable" && SepFunctions.ModuleActivated(993)) dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Skins Dropdown"), "[[SiteSkins]]"));
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Stock Quotes"), "[[Stocks]]"));
            if (SepFunctions.Setup(17, "MessengerEnable") == "Enable") dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Unread Messages"), "[[UnreadMessages]]"));
            if (SepFunctions.Setup(63, "ProfilesEnable") == "Enable" && SepFunctions.ModuleActivated(63)) dinsNew.Items.Add(new ListItem(SepFunctions.LangText("User Profile Picture"), "[[ProfilePic]]"));

            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Website Name"), "[[SiteName]]"));
            dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Who's Online"), "[[WhosOn]]"));
            for (var i = 1; i <= 7; i++)
            {
                string GetMenuText = SepFunctions.Setup(993, "Menu" + i + "Text");
                if (string.IsNullOrWhiteSpace(GetMenuText))
                    GetMenuText = "Site Menu " + i;
                dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Menu (~~" + GetMenuText + "~~)"), "[[SiteMenu" + i + "]]"));
                dinsNew.Items.Add(new ListItem(SepFunctions.LangText("Menu (~~" + GetMenuText + "~~) (Vertical)"), "[[SiteMenu" + i + "V]]"));
            }
        }
    }
}