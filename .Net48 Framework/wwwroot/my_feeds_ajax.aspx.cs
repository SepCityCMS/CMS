// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="my_feeds_ajax.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Web.UI;

    /// <summary>
    /// Class my_feeds_ajax.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class my_feeds_ajax : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();
            string sReturn = String.Empty;

            GlobalVars.ModuleID = 62;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "FeedsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("FeedsAccess"));

            switch (SepCommon.SepCore.Request.Item("DoAction"))
            {
                case "SaveComment":
                    sReturn = SepCommon.DAL.Comments.Comment_Save(SepFunctions.GetIdentity(), SepFunctions.Session_User_ID(), 62, SepFunctions.toLong(SepCommon.SepCore.Request.Item("FeedID")), 0, SepCommon.SepCore.Request.Item("CommentText"));
                    break;

                case "ShowComments":
                    var cComments = SepCommon.DAL.Comments.GetComments(62, SepFunctions.toLong(SepCommon.SepCore.Request.Item("FeedID")), 0);
                    for (var i = 0; i <= cComments.Count - 1; i++)
                    {
                        sReturn += SepFunctions.LangText("Posted by") + " " + SepFunctions.GetUserInformation("UserName", cComments[i].UserID) + " ";
                        sReturn += SepFunctions.TimeAgo(cComments[i].DatePosted) + "<br/>";
                        sReturn += cComments[i].Message + "<br/><br/>";
                    }

                    break;

                case "AddFavorite":
                    sReturn = SepCommon.DAL.MyFeeds.Feed_Add_Favorite(SepFunctions.Session_User_ID(), SepCommon.SepCore.Request.Item("FeedID"));
                    break;

                case "AddLike":
                    sReturn = SepCommon.DAL.MyFeeds.Feed_Like_Save(SepFunctions.Session_User_ID(), SepCommon.SepCore.Request.Item("FeedID"));
                    break;

                case "AddDislike":
                    sReturn = SepCommon.DAL.MyFeeds.Feed_Dislike_Save(SepFunctions.Session_User_ID(), SepCommon.SepCore.Request.Item("FeedID"));
                    break;

                case "Delete":
                    sReturn = SepCommon.DAL.MyFeeds.Feed_Delete(SepFunctions.Session_User_ID(), SepCommon.SepCore.Request.Item("FeedID"));
                    break;
            }

            Response.Write(sReturn);
        }
    }
}