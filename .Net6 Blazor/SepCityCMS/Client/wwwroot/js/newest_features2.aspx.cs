// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="newest_features2.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class newest_features2.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class newest_features2 : Page
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
            if (SepFunctions.Setup(35, "ArticlesEnable") == "Enable" && SepFunctions.ModuleActivated(35)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Articles"), "[[DisplayNewestArticles||"));
            if (SepFunctions.Setup(31, "AuctionEnable") == "Enable" && SepFunctions.ModuleActivated(31)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Auctions"), "[[DisplayNewestAuctions||"));
            if (SepFunctions.Setup(61, "BlogsEnable") == "Enable" && SepFunctions.ModuleActivated(61)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newsletters"), "[[DisplayNewestBlogs||"));
            if (SepFunctions.Setup(20, "BusinessEnable") == "Enable" && SepFunctions.ModuleActivated(20)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Businesses"), "[[DisplayNewestBusiness||"));
            if (SepFunctions.Setup(44, "ClassifiedEnable") == "Enable" && SepFunctions.ModuleActivated(44)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Classifieds"), "[[DisplayNewestClassifieds||"));

            if (SepFunctions.Setup(5, "DiscountsEnable") == "Enable" && SepFunctions.ModuleActivated(5)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Discounts"), "[[DisplayNewestDiscounts||"));
            if (SepFunctions.Setup(46, "EventsEnable") == "Enable" && SepFunctions.ModuleActivated(46)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Events"), "[[DisplayNewestEvents||"));
            if (SepFunctions.Setup(10, "LibraryEnable") == "Enable" && SepFunctions.ModuleActivated(10)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Files"), "[[DisplayNewestDownloads||"));
            if (SepFunctions.Setup(12, "ForumsEnable") == "Enable" && SepFunctions.ModuleActivated(12)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Forums"), "[[DisplayNewestForums||"));
            if (SepFunctions.Setup(19, "LinksEnable") == "Enable" && SepFunctions.ModuleActivated(19)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Links"), "[[DisplayNewestLinks||"));
            if (SepFunctions.Setup(32, "RStateEnable") == "Enable" && SepFunctions.ModuleActivated(32)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Properties"), "[[DisplayNewestProperties||"));
            if (SepFunctions.Setup(65, "VoucherEnable") == "Enable" && SepFunctions.ModuleActivated(65)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("Newest Vouchers"), "[[DisplayNewestVouchers||"));

            // Simple Inserts
            if (SepFunctions.Setup(35, "ArticlesEnable") == "Enable" && SepFunctions.ModuleActivated(35)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Articles"), "[[DisplayNewestArticles||Simple]]"));
            if (SepFunctions.Setup(31, "AuctionEnable") == "Enable" && SepFunctions.ModuleActivated(31)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Auctions"), "[[DisplayNewestAuctions||Simple]]"));
            if (SepFunctions.Setup(61, "BlogsEnable") == "Enable" && SepFunctions.ModuleActivated(61)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Blogs"), "[[DisplayNewestBlogs||Simple]]"));
            if (SepFunctions.Setup(20, "BusinessEnable") == "Enable" && SepFunctions.ModuleActivated(20)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Businesses"), "[[DisplayNewestBusiness||Simple]]"));
            if (SepFunctions.Setup(44, "ClassifiedEnable") == "Enable" && SepFunctions.ModuleActivated(44)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Classifieds"), "[[DisplayNewestClassifieds||Simple]]"));
            if (SepFunctions.Setup(46, "EventsEnable") == "Enable" && SepFunctions.ModuleActivated(46)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Events"), "[[DisplayNewestEvents||Simple]]"));
            if (SepFunctions.Setup(10, "LibraryEnable") == "Enable" && SepFunctions.ModuleActivated(10)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Files"), "[[DisplayNewestDownloads||Simple]]"));
            if (SepFunctions.Setup(12, "ForumsEnable") == "Enable" && SepFunctions.ModuleActivated(12)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Forums"), "[[DisplayNewestForums||Simple]]"));
            if (SepFunctions.Setup(19, "LinksEnable") == "Enable" && SepFunctions.ModuleActivated(19)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Links"), "[[DisplayNewestLinks||Simple]]"));
            if (SepFunctions.Setup(32, "RStateEnable") == "Enable" && SepFunctions.ModuleActivated(32)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Properties"), "[[DisplayNewestProperties||Simple]]"));
            if (SepFunctions.Setup(65, "VoucherEnable") == "Enable" && SepFunctions.ModuleActivated(65)) dinsFeature.Items.Add(new ListItem(SepFunctions.LangText("(Simple) Newest Vouchers"), "[[DisplayNewestVouchers||Simple]]"));
        }
    }
}