// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="security.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class security.
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class security : Page
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
        /// Populates the security.
        /// </summary>
        public void Populate_Security()
        {
            XmlDocument doc = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "security.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    doc.Load(reader);

                    var root = doc.DocumentElement;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AdminUserMan") != null) AdminUserMan.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AdminUserMan").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/GroupLists") != null) GroupLists.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE994/GroupLists").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminAccess") != null) AdminAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminSetup") != null) AdminSetup.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminSetup").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminEditPage") != null) AdminEditPage.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminEditPage").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminSecurity") != null) AdminSecurity.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminSecurity").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminStats") != null) AdminStats.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminStats").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminAdvance") != null) AdminAdvance.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminAdvance").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminModuleMan") != null) AdminModuleMan.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminModuleMan").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminSiteLooks") != null) AdminSiteLooks.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminSiteLooks").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminErrorLogs") != null) AdminErrorLogs.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminErrorLogs").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminActivities") != null) AdminActivities.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminActivities").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminPointSys") != null) AdminPointSys.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminPointSys").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminRecycleBin") != null) AdminRecycleBin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/AdminRecycleBin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE996/SepCityStore") != null) SepCityStore.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE996/SepCityStore").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/ConferenceAccess") != null) ConferenceAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE64/ConferenceAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/ConferenceAdmin") != null) ConferenceAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE64/ConferenceAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAccess") != null) PCRAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRApply") != null) PCRApply.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRApply").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCREmployer") != null) PCREmployer.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCREmployer").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRBrowse") != null) PCRBrowse.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRBrowse").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAdmin") != null) PCRAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsAccess") != null) AdsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsStats") != null) AdsStats.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsStats").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsAllStats") != null) AdsAllStats.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsAllStats").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsAdmin") != null) AdsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateJoin") != null) AffiliateJoin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateJoin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateAdmin") != null) AffiliateAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesAccess") != null) ArticlesAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesPost") != null) ArticlesPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesAdmin") != null) ArticlesAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionAccess") != null) AuctionAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionPost") != null) AuctionPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionAdmin") != null) AuctionAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsAccess") != null) BlogsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsCreate") != null) BlogsCreate.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsCreate").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsAdmin") != null) BlogsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceAccessKeys") != null) VideoConferenceAccessKeys.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceAccessKeys").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceCreateKeys") != null) VideoConferenceCreateKeys.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceCreateKeys").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceAcceptKeys") != null) VideoConferenceAcceptKeys.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceAcceptKeys").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessAccess") != null) BusinessAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessPost") != null) BusinessPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessSiteURL") != null) BusinessSiteURL.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessSiteURL").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessMyListings") != null) BusinessMyListings.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessMyListings").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessRandom") != null) BusinessRandom.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessRandom").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessAdmin") != null) BusinessAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE42/ChatAccess") != null) ChatAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE42/ChatAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE6/IMessengerAccess") != null) IMessengerAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE6/IMessengerAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedAccess") != null) ClassifiedAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedPost") != null) ClassifiedPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedAdmin") != null) ClassifiedAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsAccess") != null) DiscountsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsPost") != null) DiscountsPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsAdmin") != null) DiscountsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningAccess") != null) ELearningAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningAdmin") != null) ELearningAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsAccess") != null) EventsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsPost") != null) EventsPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsShared") != null) EventsShared.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsShared").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsTickets") != null) EventsTickets.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsTickets").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsAdmin") != null) EventsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQAccess") != null) FAQAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQAdmin") != null) FAQAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryAccess") != null) LibraryAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryUpload") != null) LibraryUpload.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryUpload").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryAdmin") != null) LibraryAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsAccess") != null) FormsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsAdmin") != null) FormsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsAccess") != null) ForumsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsPost") != null) ForumsPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsAdmin") != null) ForumsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookAccess") != null) GuestbookAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookSign") != null) GuestbookSign.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookSign").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookAdmin") != null) GuestbookAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotAccess") != null) HotNotAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotUpload") != null) HotNotUpload.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotUpload").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotAdmin") != null) HotNotAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksAccess") != null) LinksAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksPost") != null) LinksPost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksPost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksAdmin") != null) LinksAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchAccess") != null) MatchAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchView") != null) MatchView.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchView").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchModify") != null) MatchModify.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchModify").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchAdmin") != null) MatchAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerAccess") != null) MessengerAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerRead") != null) MessengerRead.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerRead").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerFind") != null) MessengerFind.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerFind").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerCompose") != null) MessengerCompose.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerCompose").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerReply") != null) MessengerReply.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerReply").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerMass") != null) MessengerMass.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerMass").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsAccess") != null) NewsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsAdmin") != null) NewsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletJoin") != null) NewsletJoin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletJoin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletAdmin") != null) NewsletAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesAccess") != null) GamesAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesPlay") != null) GamesPlay.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesPlay").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesAdmin") != null) GamesAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosAccess") != null) PhotosAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosShared") != null) PhotosShared.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosShared").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosCreate") != null) PhotosCreate.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosCreate").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosPassword") != null) PhotosPassword.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosPassword").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosAdmin") != null) PhotosAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsAccess") != null) PollsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsVote") != null) PollsVote.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsVote").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsAdmin") != null) PollsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsAccess") != null) PortalsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsCreate") != null) PortalsCreate.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsCreate").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsAdmin") != null) PortalsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateAccess") != null) RStateAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStatePost") != null) RStatePost.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStatePost").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateTenants") != null) RStateTenants.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateTenants").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateAdmin") != null) RStateAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferAccess") != null) ReferAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE995/ShopCartAdmin") != null) ShopCartAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE995/ShopCartAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallAccess") != null) ShopMallAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallStore") != null) ShopMallStore.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallStore").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallAdmin") != null) ShopMallAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/SpeakerAccess") != null) SpeakerAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE50/SpeakerAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/SpeakerAdmin") != null) SpeakerAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE50/SpeakerAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesAccess") != null) UPagesAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesCreate") != null) UPagesCreate.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesCreate").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesPayPal") != null) UPagesPayPal.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesPayPal").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesPortalSelection") != null) UPagesPortalSelection.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesPortalSelection").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesManage") != null) UPagesManage.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesManage").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAccess") != null) ProfilesAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesView") != null) ProfilesView.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesView").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesModify") != null) ProfilesModify.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesModify").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAdmin") != null) ProfilesAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE36/ReviewsAccess") != null) ReviewsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE36/ReviewsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE36/ReviewsWrite") != null) ReviewsWrite.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE36/ReviewsWrite").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE36/ReviewsAdmin") != null) ReviewsAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE36/ReviewsAdmin").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE34/WhosOnline") != null) WhosOnline.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE34/WhosOnline").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE62/FeedsAccess") != null) FeedsAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE62/FeedsAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAccess") != null) VoucherAccess.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAccess").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherModify") != null) VoucherModify.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherModify").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAdmin") != null) VoucherAdmin.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAdmin").InnerText;
                }
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
                    Module994Title.InnerHtml = SepFunctions.LangText("Setup the Account Management");
                    Module996Title.InnerHtml = SepFunctions.LangText("Setup the Administration Console");
                    Module64Title.InnerHtml = SepFunctions.LangText("Setup the Conference Center");
                    Module66Title.InnerHtml = SepFunctions.LangText("Setup the Job Board");
                    Module2Title.InnerHtml = SepFunctions.LangText("Setup the Advertising");
                    Module39Title.InnerHtml = SepFunctions.LangText("Setup the Affiliate Program");
                    Module35Title.InnerHtml = SepFunctions.LangText("Setup the Articles");
                    Module31Title.InnerHtml = SepFunctions.LangText("Setup the Auction");
                    Module61Title.InnerHtml = SepFunctions.LangText("Setup the Blogs");
                    Module20Title.InnerHtml = SepFunctions.LangText("Setup the Business Directory");
                    Module42Title.InnerHtml = SepFunctions.LangText("Setup the Chat Rooms");
                    Module6Title.InnerHtml = SepFunctions.LangText("Setup the Instant Messenger");
                    Module44Title.InnerHtml = SepFunctions.LangText("Setup the Classified Ads");
                    Module5Title.InnerHtml = SepFunctions.LangText("Setup the Discounts");
                    Module37Title.InnerHtml = SepFunctions.LangText("Setup the E-Learning");
                    Module46Title.InnerHtml = SepFunctions.LangText("Setup the Event Calendar");
                    Module9Title.InnerHtml = SepFunctions.LangText("Setup the FAQ");
                    Module10Title.InnerHtml = SepFunctions.LangText("Setup the Downloads");
                    Module13Title.InnerHtml = SepFunctions.LangText("Setup the Forms");
                    Module12Title.InnerHtml = SepFunctions.LangText("Setup the Forums");
                    Module14Title.InnerHtml = SepFunctions.LangText("Setup the Guestbook");
                    Module40Title.InnerHtml = SepFunctions.LangText("Setup the Hot or Not");
                    Module19Title.InnerHtml = SepFunctions.LangText("Setup the Link Directory");
                    Module18Title.InnerHtml = SepFunctions.LangText("Setup the Match Maker");
                    Module17Title.InnerHtml = SepFunctions.LangText("Setup the Messenger");
                    Module23Title.InnerHtml = SepFunctions.LangText("Setup the News");
                    Module24Title.InnerHtml = SepFunctions.LangText("Setup the Newsletters");
                    Module47Title.InnerHtml = SepFunctions.LangText("Setup the Online Games");
                    Module28Title.InnerHtml = SepFunctions.LangText("Setup the Photo Albums");
                    Module25Title.InnerHtml = SepFunctions.LangText("Setup the Polls");
                    Module60Title.InnerHtml = SepFunctions.LangText("Setup the Portals");
                    Module32Title.InnerHtml = SepFunctions.LangText("Setup the Real Estate");
                    Module43Title.InnerHtml = SepFunctions.LangText("Setup the Refer a Friend");
                    Module995Title.InnerHtml = SepFunctions.LangText("Setup the Shopping Cart");
                    Module41Title.InnerHtml = SepFunctions.LangText("Setup the Shopping Mall");
                    Module50Title.InnerHtml = SepFunctions.LangText("Setup the Speakers Bureau");
                    Module7Title.InnerHtml = SepFunctions.LangText("Setup the User Pages");
                    Module63Title.InnerHtml = SepFunctions.LangText("Setup the User Profiles");
                    Module36Title.InnerHtml = SepFunctions.LangText("Setup the User Reviews");
                    Module34Title.InnerHtml = SepFunctions.LangText("Setup the Who's Online");
                    H2.InnerHtml = SepFunctions.LangText("Setup the User Feeds");
                    H1.InnerHtml = SepFunctions.LangText("Setup the Vouchers");
                    AdminUserManLabel.InnerText = SepFunctions.LangText("Keys to manage the users:");
                    GroupListsLabel.InnerText = SepFunctions.LangText("Keys to manage the group lists:");
                    AdminAccessLabel.InnerText = SepFunctions.LangText("Keys to access the administration console:");
                    AdminSetupLabel.InnerText = SepFunctions.LangText("Keys to edit the general setup:");
                    AdminEditPageLabel.InnerText = SepFunctions.LangText("Keys to manage the web pages:");
                    AdminSecurityLabel.InnerText = SepFunctions.LangText("Keys to manage the website security:");
                    AdminStatsLabel.InnerText = SepFunctions.LangText("Keys to view the website statistics:");
                    AdminAdvanceLabel.InnerText = SepFunctions.LangText("Keys to access the admin console utilities:");
                    AdminModuleManLabel.InnerText = SepFunctions.LangText("Keys to enter the module management:");
                    AdminSiteLooksLabel.InnerText = SepFunctions.LangText("Keys to manage the site looks:");
                    AdminErrorLogsLabel.InnerText = SepFunctions.LangText("Keys to view error logs:");
                    AdminActivitiesLabel.InnerText = SepFunctions.LangText("Keys to view/edit user activities:");
                    AdminPointSysLabel.InnerText = SepFunctions.LangText("Keys to edit the pointing system:");
                    AdminRecycleBinLabel.InnerText = SepFunctions.LangText("Keys to view/restore/delete items from the recycle bin:");
                    SepCityStoreLabel.InnerText = SepFunctions.LangText("Keys to access to SepCity Store:");
                    ConferenceAccessLabel.InnerText = SepFunctions.LangText("Keys to access the conference center:");
                    ConferenceAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the conference center:");
                    PCRAccessLabel.InnerText = SepFunctions.LangText("Keys to access the job board:");
                    PCRApplyLabel.InnerText = SepFunctions.LangText("Keys to apply for a job:");
                    PCREmployerLabel.InnerText = SepFunctions.LangText("Keys to be able to post new jobs:");
                    PCRBrowseLabel.InnerText = SepFunctions.LangText("Keys to be able to browse for candidates:");
                    PCRAdminLabel.InnerText = SepFunctions.LangText("Keys to be able to manage the job board:");
                    AdsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the advertising page:");
                    AdsStatsLabel.InnerText = SepFunctions.LangText("Keys a user must have to view their ad statistics:");
                    AdsAllStatsLabel.InnerText = SepFunctions.LangText("Keys to be able to see all the statistics:");
                    AdsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the advertisements:");
                    AffiliateJoinLabel.InnerText = SepFunctions.LangText("Keys to join the affiliate program:");
                    AffiliateAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the affiliate program:");
                    ArticlesAccessLabel.InnerText = SepFunctions.LangText("Keys to access the articles.:");
                    ArticlesPostLabel.InnerText = SepFunctions.LangText("Keys to post a new article.:");
                    ArticlesAdminLabel.InnerText = SepFunctions.LangText("Keys to manage your articles.:");
                    AuctionAccessLabel.InnerText = SepFunctions.LangText("Keys to access the auction.:");
                    AuctionPostLabel.InnerText = SepFunctions.LangText("Keys to post a new ad.:");
                    AuctionAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the auctions.:");
                    BlogsAccessLabel.InnerText = SepFunctions.LangText("Keys to be able to access the blogs:");
                    BlogsCreateLabel.InnerText = SepFunctions.LangText("Keys to be able to create blogs:");
                    BlogsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the blogger:");
                    BusinessAccessLabel.InnerText = SepFunctions.LangText("Keys to access the business directory.:");
                    BusinessPostLabel.InnerText = SepFunctions.LangText("Keys to be able to post a business.:");
                    BusinessSiteURLLabel.InnerText = SepFunctions.LangText("Keys to fill in the Site URL field.:");
                    BusinessMyListingsLabel.InnerText = SepFunctions.LangText("Keys to view My Listings.:");
                    BusinessRandomLabel.InnerText = SepFunctions.LangText("Keys for user businesses to show up in random businesses.:");
                    BusinessAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the business directory.:");
                    ChatAccessLabel.InnerText = SepFunctions.LangText("Keys to access the chat rooms.:");
                    IMessengerAccessLabel.InnerText = SepFunctions.LangText("Keys to access the instant messenger:");
                    ClassifiedAccessLabel.InnerText = SepFunctions.LangText("Keys to access the classified ads.:");
                    ClassifiedPostLabel.InnerText = SepFunctions.LangText("Keys to post a new ad.:");
                    ClassifiedAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the classified ads.:");
                    DiscountsAccessLabel.InnerText = SepFunctions.LangText("Keys to be able to access the discount module:");
                    DiscountsPostLabel.InnerText = SepFunctions.LangText("Keys to be able to post a discount:");
                    DiscountsAdminLabel.InnerText = SepFunctions.LangText("Keys to be able to manage the discount system:");
                    ELearningAccessLabel.InnerText = SepFunctions.LangText("Keys to access the E-Learning?:");
                    ELearningAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the E-Learning?:");
                    EventsAccessLabel.InnerText = SepFunctions.LangText("Keys to be able to use the event calendar?:");
                    EventsPostLabel.InnerText = SepFunctions.LangText("Keys to post new events to the event calendar.:");
                    EventsSharedLabel.InnerText = SepFunctions.LangText("Keys to share their events.:");
                    EventsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the event calendar.:");
                    FAQAccessLabel.InnerText = SepFunctions.LangText("Keys to access the frequency asked questions.:");
                    FAQAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the frequency asked questions.:");
                    LibraryAccessLabel.InnerText = SepFunctions.LangText("Keys to access the Downloads:");
                    LibraryUploadLabel.InnerText = SepFunctions.LangText("Keys to upload files:");
                    LibraryAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the Downloads:");
                    FormsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the forms.:");
                    FormsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the forms.:");
                    ForumsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the forums:");
                    ForumsPostLabel.InnerText = SepFunctions.LangText("Keys to post a new topic:");
                    ForumsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the forums:");
                    GuestbookAccessLabel.InnerText = SepFunctions.LangText("Keys to access the guestbook.:");
                    GuestbookSignLabel.InnerText = SepFunctions.LangText("Keys to sign the guestbook.:");
                    GuestbookAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the guestbook.:");
                    HotNotAccessLabel.InnerText = SepFunctions.LangText("Keys to access the hot or not:");
                    HotNotUploadLabel.InnerText = SepFunctions.LangText("Keys to upload images to the hot or not:");
                    HotNotAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the hot or not:");
                    LinksAccessLabel.InnerText = SepFunctions.LangText("Keys to access the link directory.:");
                    LinksPostLabel.InnerText = SepFunctions.LangText("Keys to add a new website.:");
                    LinksAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the link directory.:");
                    MatchAccessLabel.InnerText = SepFunctions.LangText("Keys to access the match maker:");
                    MatchViewLabel.InnerText = SepFunctions.LangText("Keys to view other profiles:");
                    MatchModifyLabel.InnerText = SepFunctions.LangText("Keys to add/edit their profile:");
                    MatchAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the match maker:");
                    MessengerAccessLabel.InnerText = SepFunctions.LangText("Keys to access the messenger:");
                    MessengerReadLabel.InnerText = SepFunctions.LangText("Keys to read a message:");
                    MessengerFindLabel.InnerText = SepFunctions.LangText("Keys to find a user:");
                    MessengerComposeLabel.InnerText = SepFunctions.LangText("Keys to compose a new message:");
                    MessengerReplyLabel.InnerText = SepFunctions.LangText("Keys to reply to a message:");
                    MessengerMassLabel.InnerText = SepFunctions.LangText("Keys to send messages to everyone.:");
                    NewsAccessLabel.InnerText = SepFunctions.LangText("Keys to read the news:");
                    NewsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the news:");
                    NewsletJoinLabel.InnerText = SepFunctions.LangText("Keys to join a newsletter:");
                    NewsletAdminLabel.InnerText = SepFunctions.LangText("Keys to manage/send a newsletter:");
                    GamesAccessLabel.InnerText = SepFunctions.LangText("Keys to access the online games.:");
                    GamesPlayLabel.InnerText = SepFunctions.LangText("Keys to play the online games.:");
                    GamesAdminLabel.InnerText = SepFunctions.LangText("Keys to manage online games.:");
                    PhotosAccessLabel.InnerText = SepFunctions.LangText("Keys to access the photo albums.:");
                    PhotosSharedLabel.InnerText = SepFunctions.LangText("Keys to view shared photos.:");
                    PhotosCreateLabel.InnerText = SepFunctions.LangText("Keys to create photo albums.:");
                    PhotosPasswordLabel.InnerText = SepFunctions.LangText("Keys to password protect a photo album.:");
                    PhotosAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the photo albums.:");
                    PollsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the polls:");
                    PollsVoteLabel.InnerText = SepFunctions.LangText("Keys to vote for a poll:");
                    PollsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the polls:");
                    PortalsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the portal directory.:");
                    PortalsCreateLabel.InnerText = SepFunctions.LangText("Keys to create new portals.:");
                    PortalsAdminLabel.InnerText = SepFunctions.LangText("Keys to be able to manage the portals:");
                    RStateAccessLabel.InnerText = SepFunctions.LangText("Keys to access the real estate.:");
                    RStatePostLabel.InnerText = SepFunctions.LangText("Keys to post a new property.:");
                    RStateAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the real estate.:");
                    ReferAccessLabel.InnerText = SepFunctions.LangText("Keys to access the refer a friend module.:");
                    ShopCartAdminLabel.InnerText = SepFunctions.LangText("Keys to view and manage the invoices:");
                    ShopMallAccessLabel.InnerText = SepFunctions.LangText("Keys to access the shopping mall.:");
                    ShopMallStoreLabel.InnerText = SepFunctions.LangText("Keys to allow users to create their own store.:");
                    ShopMallAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the shopping mall.:");
                    SpeakerAccessLabel.InnerText = SepFunctions.LangText("Keys to access the speakers bureau.:");
                    SpeakerAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the speakers bureau.:");
                    UPagesAccessLabel.InnerText = SepFunctions.LangText("Keys to access the user pages.:");
                    UPagesCreateLabel.InnerText = SepFunctions.LangText("Keys to create a new web site.:");
                    UPagesPayPalLabel.InnerText = SepFunctions.LangText("Keys a user must have to be able to use PayPal in their web pages.:");
                    UPagesPortalSelectionLabel.InnerText = SepFunctions.LangText("Keys to associate portals content to the user page.:");
                    UPagesManageLabel.InnerText = SepFunctions.LangText("Keys to manage the user pages.:");
                    ProfilesAccessLabel.InnerText = SepFunctions.LangText("Keys to access the user profiles:");
                    ProfilesViewLabel.InnerText = SepFunctions.LangText("Keys to view other profiles:");
                    ProfilesModifyLabel.InnerText = SepFunctions.LangText("Keys to add/edit their profile:");
                    ProfilesAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the user profiles:");
                    ReviewsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the user reviews.:");
                    ReviewsWriteLabel.InnerText = SepFunctions.LangText("Keys to write a review.:");
                    ReviewsAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the user reviews.:");
                    WhosOnlineLabel.InnerText = SepFunctions.LangText("Keys to see who is online:");
                    FeedsAccessLabel.InnerText = SepFunctions.LangText("Keys to access the User Feeds module:");
                    VoucherAccessLabel.InnerText = SepFunctions.LangText("Keys to access the Vouchers module:");
                    VoucherModifyLabel.InnerText = SepFunctions.LangText("Keys to add/edit Vouchers:");
                    VoucherAdminLabel.InnerText = SepFunctions.LangText("Keys to manage the Vouchers module:");
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
            TranslatePage();

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSecurity")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSecurity"), false) == false)
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

            if (!Page.IsPostBack)
            {
                ModuleID.Value = SepCommon.SepCore.Request.Item("ModuleID");
                if (SepFunctions.ModuleActivated(60) == false) UPagesPortalSelectionRow.Visible = false;

                try
                {
                    Populate_Security();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SecuritySave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SecuritySave_Click(object sender, EventArgs e)
        {
            var strXml = string.Empty;

            strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            strXml += "<ROOTLEVEL>" + Environment.NewLine;

            strXml += "<MODULE994>" + Environment.NewLine;
            strXml += "<AdminUserMan>" + SepFunctions.HTMLEncode(AdminUserMan.Text) + "</AdminUserMan>" + Environment.NewLine;
            strXml += "<GroupLists>" + SepFunctions.HTMLEncode(GroupLists.Text) + "</GroupLists>" + Environment.NewLine;
            strXml += "</MODULE994>" + Environment.NewLine;

            strXml += "<MODULE996>" + Environment.NewLine;
            strXml += "<AdminAccess>" + SepFunctions.HTMLEncode(AdminAccess.Text) + "</AdminAccess>" + Environment.NewLine;
            strXml += "<AdminSetup>" + SepFunctions.HTMLEncode(AdminSetup.Text) + "</AdminSetup>" + Environment.NewLine;
            strXml += "<AdminEditPage>" + SepFunctions.HTMLEncode(AdminEditPage.Text) + "</AdminEditPage>" + Environment.NewLine;
            strXml += "<AdminSecurity>" + SepFunctions.HTMLEncode(AdminSecurity.Text) + "</AdminSecurity>" + Environment.NewLine;
            strXml += "<AdminStats>" + SepFunctions.HTMLEncode(AdminStats.Text) + "</AdminStats>" + Environment.NewLine;
            strXml += "<AdminAdvance>" + SepFunctions.HTMLEncode(AdminAdvance.Text) + "</AdminAdvance>" + Environment.NewLine;
            strXml += "<AdminModuleMan>" + SepFunctions.HTMLEncode(AdminModuleMan.Text) + "</AdminModuleMan>" + Environment.NewLine;
            strXml += "<AdminSiteLooks>" + SepFunctions.HTMLEncode(AdminSiteLooks.Text) + "</AdminSiteLooks>" + Environment.NewLine;
            strXml += "<AdminErrorLogs>" + SepFunctions.HTMLEncode(AdminErrorLogs.Text) + "</AdminErrorLogs>" + Environment.NewLine;
            strXml += "<AdminActivities>" + SepFunctions.HTMLEncode(AdminActivities.Text) + "</AdminActivities>" + Environment.NewLine;
            strXml += "<AdminPointSys>" + SepFunctions.HTMLEncode(AdminPointSys.Text) + "</AdminPointSys>" + Environment.NewLine;
            strXml += "<AdminRecycleBin>" + SepFunctions.HTMLEncode(AdminRecycleBin.Text) + "</AdminRecycleBin>" + Environment.NewLine;
            strXml += "<SepCityStore>" + SepFunctions.HTMLEncode(SepCityStore.Text) + "</SepCityStore>" + Environment.NewLine;
            strXml += "</MODULE996>" + Environment.NewLine;

            strXml += "<MODULE64>" + Environment.NewLine;
            strXml += "<ConferenceAccess>" + SepFunctions.HTMLEncode(ConferenceAccess.Text) + "</ConferenceAccess>" + Environment.NewLine;
            strXml += "<ConferenceAdmin>" + SepFunctions.HTMLEncode(ConferenceAdmin.Text) + "</ConferenceAdmin>" + Environment.NewLine;
            strXml += "</MODULE64>" + Environment.NewLine;

            strXml += "<MODULE66>" + Environment.NewLine;
            strXml += "<PCRAccess>" + SepFunctions.HTMLEncode(ConferenceAccess.Text) + "</PCRAccess>" + Environment.NewLine;
            strXml += "<PCRApply>" + SepFunctions.HTMLEncode(PCRApply.Text) + "</PCRApply>" + Environment.NewLine;
            strXml += "<PCREmployer>" + SepFunctions.HTMLEncode(PCREmployer.Text) + "</PCREmployer>" + Environment.NewLine;
            strXml += "<PCRBrowse>" + SepFunctions.HTMLEncode(PCRBrowse.Text) + "</PCRBrowse>" + Environment.NewLine;
            strXml += "<PCRAdmin>" + SepFunctions.HTMLEncode(PCRAdmin.Text) + "</PCRAdmin>" + Environment.NewLine;
            strXml += "</MODULE66>" + Environment.NewLine;

            strXml += "<MODULE2>" + Environment.NewLine;
            strXml += "<AdsAccess>" + SepFunctions.HTMLEncode(AdsAccess.Text) + "</AdsAccess>" + Environment.NewLine;
            strXml += "<AdsStats>" + SepFunctions.HTMLEncode(AdsStats.Text) + "</AdsStats>" + Environment.NewLine;
            strXml += "<AdsAllStats>" + SepFunctions.HTMLEncode(AdsAllStats.Text) + "</AdsAllStats>" + Environment.NewLine;
            strXml += "<AdsAdmin>" + SepFunctions.HTMLEncode(AdsAdmin.Text) + "</AdsAdmin>" + Environment.NewLine;
            strXml += "</MODULE2>" + Environment.NewLine;

            strXml += "<MODULE39>" + Environment.NewLine;
            strXml += "<AffiliateJoin>" + SepFunctions.HTMLEncode(AffiliateJoin.Text) + "</AffiliateJoin>" + Environment.NewLine;
            strXml += "<AffiliateAdmin>" + SepFunctions.HTMLEncode(AffiliateAdmin.Text) + "</AffiliateAdmin>" + Environment.NewLine;
            strXml += "</MODULE39>" + Environment.NewLine;

            strXml += "<MODULE35>" + Environment.NewLine;
            strXml += "<ArticlesAccess>" + SepFunctions.HTMLEncode(ArticlesAccess.Text) + "</ArticlesAccess>" + Environment.NewLine;
            strXml += "<ArticlesPost>" + SepFunctions.HTMLEncode(ArticlesPost.Text) + "</ArticlesPost>" + Environment.NewLine;
            strXml += "<ArticlesAdmin>" + SepFunctions.HTMLEncode(ArticlesAdmin.Text) + "</ArticlesAdmin>" + Environment.NewLine;
            strXml += "</MODULE35>" + Environment.NewLine;

            strXml += "<MODULE31>" + Environment.NewLine;
            strXml += "<AuctionAccess>" + SepFunctions.HTMLEncode(AuctionAccess.Text) + "</AuctionAccess>" + Environment.NewLine;
            strXml += "<AuctionPost>" + SepFunctions.HTMLEncode(AuctionPost.Text) + "</AuctionPost>" + Environment.NewLine;
            strXml += "<AuctionAdmin>" + SepFunctions.HTMLEncode(AuctionAdmin.Text) + "</AuctionAdmin>" + Environment.NewLine;
            strXml += "</MODULE31>" + Environment.NewLine;

            strXml += "<MODULE61>" + Environment.NewLine;
            strXml += "<BlogsAccess>" + SepFunctions.HTMLEncode(BlogsAccess.Text) + "</BlogsAccess>" + Environment.NewLine;
            strXml += "<BlogsCreate>" + SepFunctions.HTMLEncode(BlogsCreate.Text) + "</BlogsCreate>" + Environment.NewLine;
            strXml += "<BlogsAdmin>" + SepFunctions.HTMLEncode(BlogsAdmin.Text) + "</BlogsAdmin>" + Environment.NewLine;
            strXml += "</MODULE61>" + Environment.NewLine;

            strXml += "<MODULE69>" + Environment.NewLine;
            strXml += "<VideoConferenceAccessKeys>" + SepFunctions.HTMLEncode(VideoConferenceAccessKeys.Text) + "</VideoConferenceAccessKeys>" + Environment.NewLine;
            strXml += "<VideoConferenceCreateKeys>" + SepFunctions.HTMLEncode(VideoConferenceCreateKeys.Text) + "</VideoConferenceCreateKeys>" + Environment.NewLine;
            strXml += "<VideoConferenceAcceptKeys>" + SepFunctions.HTMLEncode(VideoConferenceAcceptKeys.Text) + "</VideoConferenceAcceptKeys>" + Environment.NewLine;
            strXml += "</MODULE69>" + Environment.NewLine;

            strXml += "<MODULE20>" + Environment.NewLine;
            strXml += "<BusinessAccess>" + SepFunctions.HTMLEncode(BusinessAccess.Text) + "</BusinessAccess>" + Environment.NewLine;
            strXml += "<BusinessPost>" + SepFunctions.HTMLEncode(BusinessPost.Text) + "</BusinessPost>" + Environment.NewLine;
            strXml += "<BusinessSiteURL>" + SepFunctions.HTMLEncode(BusinessSiteURL.Text) + "</BusinessSiteURL>" + Environment.NewLine;
            strXml += "<BusinessMyListings>" + SepFunctions.HTMLEncode(BusinessMyListings.Text) + "</BusinessMyListings>" + Environment.NewLine;
            strXml += "<BusinessRandom>" + SepFunctions.HTMLEncode(BusinessRandom.Text) + "</BusinessRandom>" + Environment.NewLine;
            strXml += "<BusinessAdmin>" + SepFunctions.HTMLEncode(BusinessAdmin.Text) + "</BusinessAdmin>" + Environment.NewLine;
            strXml += "</MODULE20>" + Environment.NewLine;

            strXml += "<MODULE42>" + Environment.NewLine;
            strXml += "<ChatAccess>" + SepFunctions.HTMLEncode(ChatAccess.Text) + "</ChatAccess>" + Environment.NewLine;
            strXml += "</MODULE42>" + Environment.NewLine;

            strXml += "<MODULE6>" + Environment.NewLine;
            strXml += "<IMessengerAccess>" + SepFunctions.HTMLEncode(IMessengerAccess.Text) + "</IMessengerAccess>" + Environment.NewLine;
            strXml += "</MODULE6>" + Environment.NewLine;

            strXml += "<MODULE44>" + Environment.NewLine;
            strXml += "<ClassifiedAccess>" + SepFunctions.HTMLEncode(ClassifiedAccess.Text) + "</ClassifiedAccess>" + Environment.NewLine;
            strXml += "<ClassifiedPost>" + SepFunctions.HTMLEncode(ClassifiedPost.Text) + "</ClassifiedPost>" + Environment.NewLine;
            strXml += "<ClassifiedAdmin>" + SepFunctions.HTMLEncode(ClassifiedAdmin.Text) + "</ClassifiedAdmin>" + Environment.NewLine;
            strXml += "</MODULE44>" + Environment.NewLine;

            strXml += "<MODULE5>" + Environment.NewLine;
            strXml += "<DiscountsAccess>" + SepFunctions.HTMLEncode(DiscountsAccess.Text) + "</DiscountsAccess>" + Environment.NewLine;
            strXml += "<DiscountsPost>" + SepFunctions.HTMLEncode(DiscountsPost.Text) + "</DiscountsPost>" + Environment.NewLine;
            strXml += "<DiscountsAdmin>" + SepFunctions.HTMLEncode(DiscountsAdmin.Text) + "</DiscountsAdmin>" + Environment.NewLine;
            strXml += "</MODULE5>" + Environment.NewLine;

            strXml += "<MODULE37>" + Environment.NewLine;
            strXml += "<ELearningAccess>" + SepFunctions.HTMLEncode(ELearningAccess.Text) + "</ELearningAccess>" + Environment.NewLine;
            strXml += "<ELearningAdmin>" + SepFunctions.HTMLEncode(ELearningAdmin.Text) + "</ELearningAdmin>" + Environment.NewLine;
            strXml += "</MODULE37>" + Environment.NewLine;

            strXml += "<MODULE46>" + Environment.NewLine;
            strXml += "<EventsAccess>" + SepFunctions.HTMLEncode(EventsAccess.Text) + "</EventsAccess>" + Environment.NewLine;
            strXml += "<EventsPost>" + SepFunctions.HTMLEncode(EventsPost.Text) + "</EventsPost>" + Environment.NewLine;
            strXml += "<EventsShared>" + SepFunctions.HTMLEncode(EventsShared.Text) + "</EventsShared>" + Environment.NewLine;
            strXml += "<EventsTickets>" + SepFunctions.HTMLEncode(EventsTickets.Text) + "</EventsTickets>" + Environment.NewLine;
            strXml += "<EventsAdmin>" + SepFunctions.HTMLEncode(EventsAdmin.Text) + "</EventsAdmin>" + Environment.NewLine;
            strXml += "</MODULE46>" + Environment.NewLine;

            strXml += "<MODULE9>" + Environment.NewLine;
            strXml += "<FAQAccess>" + SepFunctions.HTMLEncode(FAQAccess.Text) + "</FAQAccess>" + Environment.NewLine;
            strXml += "<FAQAdmin>" + SepFunctions.HTMLEncode(FAQAdmin.Text) + "</FAQAdmin>" + Environment.NewLine;
            strXml += "</MODULE9>" + Environment.NewLine;

            strXml += "<MODULE10>" + Environment.NewLine;
            strXml += "<LibraryAccess>" + SepFunctions.HTMLEncode(LibraryAccess.Text) + "</LibraryAccess>" + Environment.NewLine;
            strXml += "<LibraryUpload>" + SepFunctions.HTMLEncode(LibraryUpload.Text) + "</LibraryUpload>" + Environment.NewLine;
            strXml += "<LibraryAdmin>" + SepFunctions.HTMLEncode(LibraryAdmin.Text) + "</LibraryAdmin>" + Environment.NewLine;
            strXml += "</MODULE10>" + Environment.NewLine;

            strXml += "<MODULE13>" + Environment.NewLine;
            strXml += "<FormsAccess>" + SepFunctions.HTMLEncode(FormsAccess.Text) + "</FormsAccess>" + Environment.NewLine;
            strXml += "<FormsAdmin>" + SepFunctions.HTMLEncode(FormsAdmin.Text) + "</FormsAdmin>" + Environment.NewLine;
            strXml += "</MODULE13>" + Environment.NewLine;

            strXml += "<MODULE12>" + Environment.NewLine;
            strXml += "<ForumsAccess>" + SepFunctions.HTMLEncode(ForumsAccess.Text) + "</ForumsAccess>" + Environment.NewLine;
            strXml += "<ForumsPost>" + SepFunctions.HTMLEncode(ForumsPost.Text) + "</ForumsPost>" + Environment.NewLine;
            strXml += "<ForumsAdmin>" + SepFunctions.HTMLEncode(ForumsAdmin.Text) + "</ForumsAdmin>" + Environment.NewLine;
            strXml += "</MODULE12>" + Environment.NewLine;

            strXml += "<MODULE14>" + Environment.NewLine;
            strXml += "<GuestbookAccess>" + SepFunctions.HTMLEncode(GuestbookAccess.Text) + "</GuestbookAccess>" + Environment.NewLine;
            strXml += "<GuestbookSign>" + SepFunctions.HTMLEncode(GuestbookSign.Text) + "</GuestbookSign>" + Environment.NewLine;
            strXml += "<GuestbookAdmin>" + SepFunctions.HTMLEncode(GuestbookAdmin.Text) + "</GuestbookAdmin>" + Environment.NewLine;
            strXml += "</MODULE14>" + Environment.NewLine;

            strXml += "<MODULE40>" + Environment.NewLine;
            strXml += "<HotNotAccess>" + SepFunctions.HTMLEncode(HotNotAccess.Text) + "</HotNotAccess>" + Environment.NewLine;
            strXml += "<HotNotUpload>" + SepFunctions.HTMLEncode(HotNotUpload.Text) + "</HotNotUpload>" + Environment.NewLine;
            strXml += "<HotNotAdmin>" + SepFunctions.HTMLEncode(HotNotAdmin.Text) + "</HotNotAdmin>" + Environment.NewLine;
            strXml += "</MODULE40>" + Environment.NewLine;

            strXml += "<MODULE19>" + Environment.NewLine;
            strXml += "<LinksAccess>" + SepFunctions.HTMLEncode(LinksAccess.Text) + "</LinksAccess>" + Environment.NewLine;
            strXml += "<LinksPost>" + SepFunctions.HTMLEncode(LinksPost.Text) + "</LinksPost>" + Environment.NewLine;
            strXml += "<LinksAdmin>" + SepFunctions.HTMLEncode(LinksAdmin.Text) + "</LinksAdmin>" + Environment.NewLine;
            strXml += "</MODULE19>" + Environment.NewLine;

            strXml += "<MODULE18>" + Environment.NewLine;
            strXml += "<MatchAccess>" + SepFunctions.HTMLEncode(MatchAccess.Text) + "</MatchAccess>" + Environment.NewLine;
            strXml += "<MatchView>" + SepFunctions.HTMLEncode(MatchView.Text) + "</MatchView>" + Environment.NewLine;
            strXml += "<MatchModify>" + SepFunctions.HTMLEncode(MatchModify.Text) + "</MatchModify>" + Environment.NewLine;
            strXml += "<MatchAdmin>" + SepFunctions.HTMLEncode(MatchAdmin.Text) + "</MatchAdmin>" + Environment.NewLine;
            strXml += "</MODULE18>" + Environment.NewLine;

            strXml += "<MODULE17>" + Environment.NewLine;
            strXml += "<MessengerAccess>" + SepFunctions.HTMLEncode(MessengerAccess.Text) + "</MessengerAccess>" + Environment.NewLine;
            strXml += "<MessengerRead>" + SepFunctions.HTMLEncode(MessengerRead.Text) + "</MessengerRead>" + Environment.NewLine;
            strXml += "<MessengerFind>" + SepFunctions.HTMLEncode(MessengerFind.Text) + "</MessengerFind>" + Environment.NewLine;
            strXml += "<MessengerCompose>" + SepFunctions.HTMLEncode(MessengerCompose.Text) + "</MessengerCompose>" + Environment.NewLine;
            strXml += "<MessengerReply>" + SepFunctions.HTMLEncode(MessengerReply.Text) + "</MessengerReply>" + Environment.NewLine;
            strXml += "<MessengerMass>" + SepFunctions.HTMLEncode(MessengerMass.Text) + "</MessengerMass>" + Environment.NewLine;
            strXml += "</MODULE17>" + Environment.NewLine;

            strXml += "<MODULE23>" + Environment.NewLine;
            strXml += "<NewsAccess>" + SepFunctions.HTMLEncode(NewsAccess.Text) + "</NewsAccess>" + Environment.NewLine;
            strXml += "<NewsAdmin>" + SepFunctions.HTMLEncode(NewsAdmin.Text) + "</NewsAdmin>" + Environment.NewLine;
            strXml += "</MODULE23>" + Environment.NewLine;

            strXml += "<MODULE24>" + Environment.NewLine;
            strXml += "<NewsletJoin>" + SepFunctions.HTMLEncode(NewsletJoin.Text) + "</NewsletJoin>" + Environment.NewLine;
            strXml += "<NewsletAdmin>" + SepFunctions.HTMLEncode(NewsletAdmin.Text) + "</NewsletAdmin>" + Environment.NewLine;
            strXml += "</MODULE24>" + Environment.NewLine;

            strXml += "<MODULE47>" + Environment.NewLine;
            strXml += "<GamesAccess>" + SepFunctions.HTMLEncode(GamesAccess.Text) + "</GamesAccess>" + Environment.NewLine;
            strXml += "<GamesPlay>" + SepFunctions.HTMLEncode(GamesPlay.Text) + "</GamesPlay>" + Environment.NewLine;
            strXml += "<GamesAdmin>" + SepFunctions.HTMLEncode(GamesAdmin.Text) + "</GamesAdmin>" + Environment.NewLine;
            strXml += "</MODULE47>" + Environment.NewLine;

            strXml += "<MODULE28>" + Environment.NewLine;
            strXml += "<PhotosAccess>" + SepFunctions.HTMLEncode(PhotosAccess.Text) + "</PhotosAccess>" + Environment.NewLine;
            strXml += "<PhotosShared>" + SepFunctions.HTMLEncode(PhotosShared.Text) + "</PhotosShared>" + Environment.NewLine;
            strXml += "<PhotosCreate>" + SepFunctions.HTMLEncode(PhotosCreate.Text) + "</PhotosCreate>" + Environment.NewLine;
            strXml += "<PhotosPassword>" + SepFunctions.HTMLEncode(PhotosPassword.Text) + "</PhotosPassword>" + Environment.NewLine;
            strXml += "<PhotosAdmin>" + SepFunctions.HTMLEncode(PhotosAdmin.Text) + "</PhotosAdmin>" + Environment.NewLine;
            strXml += "</MODULE28>" + Environment.NewLine;

            strXml += "<MODULE25>" + Environment.NewLine;
            strXml += "<PollsAccess>" + SepFunctions.HTMLEncode(PollsAccess.Text) + "</PollsAccess>" + Environment.NewLine;
            strXml += "<PollsVote>" + SepFunctions.HTMLEncode(PollsVote.Text) + "</PollsVote>" + Environment.NewLine;
            strXml += "<PollsAdmin>" + SepFunctions.HTMLEncode(PollsAdmin.Text) + "</PollsAdmin>" + Environment.NewLine;
            strXml += "</MODULE25>" + Environment.NewLine;

            strXml += "<MODULE60>" + Environment.NewLine;
            strXml += "<PortalsAccess>" + SepFunctions.HTMLEncode(PortalsAccess.Text) + "</PortalsAccess>" + Environment.NewLine;
            strXml += "<PortalsCreate>" + SepFunctions.HTMLEncode(PortalsCreate.Text) + "</PortalsCreate>" + Environment.NewLine;
            strXml += "<PortalsAdmin>" + SepFunctions.HTMLEncode(PortalsAdmin.Text) + "</PortalsAdmin>" + Environment.NewLine;
            strXml += "</MODULE60>" + Environment.NewLine;

            strXml += "<MODULE32>" + Environment.NewLine;
            strXml += "<RStateAccess>" + SepFunctions.HTMLEncode(RStateAccess.Text) + "</RStateAccess>" + Environment.NewLine;
            strXml += "<RStatePost>" + SepFunctions.HTMLEncode(RStatePost.Text) + "</RStatePost>" + Environment.NewLine;
            strXml += "<RStateTenants>" + SepFunctions.HTMLEncode(RStateTenants.Text) + "</RStateTenants>" + Environment.NewLine;
            strXml += "<RStateAdmin>" + SepFunctions.HTMLEncode(RStateAdmin.Text) + "</RStateAdmin>" + Environment.NewLine;
            strXml += "</MODULE32>" + Environment.NewLine;

            strXml += "<MODULE43>" + Environment.NewLine;
            strXml += "<ReferAccess>" + SepFunctions.HTMLEncode(ReferAccess.Text) + "</ReferAccess>" + Environment.NewLine;
            strXml += "</MODULE43>" + Environment.NewLine;

            strXml += "<MODULE995>" + Environment.NewLine;
            strXml += "<ShopCartAdmin>" + SepFunctions.HTMLEncode(ShopCartAdmin.Text) + "</ShopCartAdmin>" + Environment.NewLine;
            strXml += "</MODULE995>" + Environment.NewLine;

            strXml += "<MODULE41>" + Environment.NewLine;
            strXml += "<ShopMallAccess>" + SepFunctions.HTMLEncode(ShopMallAccess.Text) + "</ShopMallAccess>" + Environment.NewLine;
            strXml += "<ShopMallStore>" + SepFunctions.HTMLEncode(ShopMallStore.Text) + "</ShopMallStore>" + Environment.NewLine;
            strXml += "<ShopMallAdmin>" + SepFunctions.HTMLEncode(ShopMallAdmin.Text) + "</ShopMallAdmin>" + Environment.NewLine;
            strXml += "</MODULE41>" + Environment.NewLine;

            strXml += "<MODULE50>" + Environment.NewLine;
            strXml += "<SpeakerAccess>" + SepFunctions.HTMLEncode(SpeakerAccess.Text) + "</SpeakerAccess>" + Environment.NewLine;
            strXml += "<SpeakerAdmin>" + SepFunctions.HTMLEncode(SpeakerAdmin.Text) + "</SpeakerAdmin>" + Environment.NewLine;
            strXml += "</MODULE50>" + Environment.NewLine;

            strXml += "<MODULE7>" + Environment.NewLine;
            strXml += "<UPagesAccess>" + SepFunctions.HTMLEncode(UPagesAccess.Text) + "</UPagesAccess>" + Environment.NewLine;
            strXml += "<UPagesCreate>" + SepFunctions.HTMLEncode(UPagesCreate.Text) + "</UPagesCreate>" + Environment.NewLine;
            strXml += "<UPagesPayPal>" + SepFunctions.HTMLEncode(UPagesPayPal.Text) + "</UPagesPayPal>" + Environment.NewLine;
            strXml += "<UPagesPortalSelection>" + SepFunctions.HTMLEncode(UPagesPortalSelection.Text) + "</UPagesPortalSelection>" + Environment.NewLine;
            strXml += "<UPagesManage>" + SepFunctions.HTMLEncode(UPagesManage.Text) + "</UPagesManage>" + Environment.NewLine;
            strXml += "</MODULE7>" + Environment.NewLine;

            strXml += "<MODULE63>" + Environment.NewLine;
            strXml += "<ProfilesAccess>" + SepFunctions.HTMLEncode(ProfilesAccess.Text) + "</ProfilesAccess>" + Environment.NewLine;
            strXml += "<ProfilesView>" + SepFunctions.HTMLEncode(ProfilesView.Text) + "</ProfilesView>" + Environment.NewLine;
            strXml += "<ProfilesModify>" + SepFunctions.HTMLEncode(ProfilesModify.Text) + "</ProfilesModify>" + Environment.NewLine;
            strXml += "<ProfilesAdmin>" + SepFunctions.HTMLEncode(ProfilesAdmin.Text) + "</ProfilesAdmin>" + Environment.NewLine;
            strXml += "</MODULE63>" + Environment.NewLine;

            strXml += "<MODULE36>" + Environment.NewLine;
            strXml += "<ReviewsAccess>" + SepFunctions.HTMLEncode(ReviewsAccess.Text) + "</ReviewsAccess>" + Environment.NewLine;
            strXml += "<ReviewsWrite>" + SepFunctions.HTMLEncode(ReviewsWrite.Text) + "</ReviewsWrite>" + Environment.NewLine;
            strXml += "<ReviewsAdmin>" + SepFunctions.HTMLEncode(ReviewsAdmin.Text) + "</ReviewsAdmin>" + Environment.NewLine;
            strXml += "</MODULE36>" + Environment.NewLine;

            strXml += "<MODULE34>" + Environment.NewLine;
            strXml += "<WhosOnline>" + SepFunctions.HTMLEncode(WhosOnline.Text) + "</WhosOnline>" + Environment.NewLine;
            strXml += "</MODULE34>" + Environment.NewLine;

            strXml += "<MODULE62>" + Environment.NewLine;
            strXml += "<FeedsAccess>" + SepFunctions.HTMLEncode(FeedsAccess.Text) + "</FeedsAccess>" + Environment.NewLine;
            strXml += "</MODULE62>" + Environment.NewLine;

            strXml += "<MODULE65>" + Environment.NewLine;
            strXml += "<VoucherAccess>" + SepFunctions.HTMLEncode(VoucherAccess.Text) + "</VoucherAccess>" + Environment.NewLine;
            strXml += "<VoucherModify>" + SepFunctions.HTMLEncode(VoucherModify.Text) + "</VoucherModify>" + Environment.NewLine;
            strXml += "<VoucherAdmin>" + SepFunctions.HTMLEncode(VoucherAdmin.Text) + "</VoucherAdmin>" + Environment.NewLine;
            strXml += "</MODULE65>" + Environment.NewLine;

            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "security.xml"))
            {
                outfile.Write(strXml);
            }

            SaveMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Security successfully saved.") + "</div>";
        }
    }
}