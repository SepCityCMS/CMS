// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="topcmm_friends.aspx.cs" company="SepCity, Inc.">
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

    /// <summary>
    /// Class topcmm_friends.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class topcmm_friends : Page
    {
        /// <summary>
        /// Adds the friend.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        public void Add_Friend(string UserName)
        {
            var result = "<FL r=\"1\">";

            var sReturn = SepCommon.DAL.Friends.Friends_Save(UserName, SepFunctions.Session_User_Name());

            if (sReturn != SepFunctions.LangText("User does not exist on our web site.") && !string.IsNullOrWhiteSpace(sReturn)) result = "<FL r=\"0\">";

            Response.Clear();

            Response.Write(result);

            Response.End();
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
        /// Lists the friends.
        /// </summary>
        public void List_Friends()
        {
            var sXML = string.Empty;

            Response.Clear();

            if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("username")))
            {
                Response.End();
                return;
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                sXML += "<UD>" + Environment.NewLine;
                sXML += "<FL>" + Environment.NewLine;
                sXML += "<g n=\"general\">" + Environment.NewLine;
                using (var cmd = new SqlCommand("SELECT M.Username FROM FriendsList AS FL,Members AS M WHERE M.UserID=FL.AddedUserID AND FL.UserID IN (SELECT UserID FROM Members WHERE UserName='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("username")) + "') AND M.Status=1 ORDER BY M.Username", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read()) sXML += "<u n=\"" + SepFunctions.openNull(RS["Username"]) + "\"></u>" + Environment.NewLine;
                    }
                }

                sXML += "</g>" + Environment.NewLine;
                sXML += "<g n=\"stranger\">" + Environment.NewLine;
                sXML += "</g>" + Environment.NewLine;
                sXML += "</FL>" + Environment.NewLine;
                sXML += "</UD>";
            }

            Response.Write(sXML);

            Response.End();
        }

        /// <summary>
        /// Removes the friend.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        public void Remove_Friend(string UserName)
        {
            var result = "<FL r=\"1\">";

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserName=@UserName) AND Status=1", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();

                            var sReturn = SepCommon.DAL.Friends.Friends_Delete(SepFunctions.openNull(RS["UserID"]));

                            if (!string.IsNullOrWhiteSpace(sReturn)) result = "<FL r=\"0\">";
                        }

                    }
                }
            }

            Response.Clear();

            Response.Write(result);

            Response.End();
        }

        /// <summary>
        /// Updates the friend.
        /// </summary>
        public void Update_Friend()
        {
            if (SepCommon.SepCore.Request.Item("friendgroup") == "stranger") Remove_Friend(SepCommon.SepCore.Request.Item("destusername"));
            else Add_Friend(SepCommon.SepCore.Request.Item("destusername"));
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
            switch (SepFunctions.toInt(SepCommon.SepCore.Request.Item("action")))
            {
                case 1:
                    Add_Friend(SepCommon.SepCore.Request.Item("destusername"));
                    break;

                case 2:
                    Remove_Friend(SepCommon.SepCore.Request.Item("destusername"));
                    break;

                case 3:
                    Update_Friend();
                    break;

                default:
                    List_Friends();
                    break;
            }
        }
    }
}