// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="setup.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class setup.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class setup : Page
    {
        /// <summary>
        /// Disables the module.
        /// </summary>
        /// <param name="iModuleID">The i module identifier.</param>
        /// <param name="sValue">The s value.</param>
        public void Disable_Module(int iModuleID, string sValue)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (sValue == "Enable")
                    using (var cmd = new SqlCommand("UPDATE ModulesNPages SET Status=1 WHERE ModuleID=@ModuleID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                        cmd.ExecuteNonQuery();
                    }
                else
                    using (var cmd = new SqlCommand("UPDATE ModulesNPages SET Status='0' WHERE ModuleID=@ModuleID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                        cmd.ExecuteNonQuery();
                    }
            }
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
        /// Populates the settings.
        /// </summary>
        public void Populate_Settings()
        {
            var doc = new XmlDocument();
            doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");

            // Select the book node with the matching attribute value.
            var root = doc.DocumentElement;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/LoginPage") != null) LoginPage.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/LoginPage").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowMemberships") != null) PPShowMemberships.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowMemberships").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowOrders") != null) PPShowOrders.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowOrders").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowAdStats") != null) PPShowAdStats.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowAdStats").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowFriends") != null) PPShowFriends.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowFriends").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowFavorites") != null) PPShowFavorites.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowFavorites").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowAffiliate") != null) PPShowAffiliate.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowAffiliate").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowMessenger") != null) PPShowMessenger.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowMessenger").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowProfile") != null) PPShowProfile.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowProfile").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowBlogs") != null) PPShowBlogs.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowBlogs").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowForums") != null) PPShowForums.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowForums").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowMain") != null) PPShowMain.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPShowMain").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPDefaultPage") != null) PPDefaultPage.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/PPDefaultPage").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/FriendsEnable") != null) FriendsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/FriendsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/ShowPoints") != null) ShowCredits.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/ShowPoints").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE33/ShowCreditsSignup") != null) ShowCreditsSignup.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE33/ShowCreditsSignup").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskGender") != null) AskGender.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskGender").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskPhoneNumber") != null) AskPhoneNumber.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskPhoneNumber").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/ReqPhoneNumber") != null) ReqPhoneNumber.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/ReqPhoneNumber").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskStreetAddress") != null) AskStreetAddress.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskStreetAddress").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskCity") != null) AskCity.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskCity").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskState") != null) AskState.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskState").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskZipCode") != null) AskZipCode.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskZipCode").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskCountry") != null) AskCountry.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskCountry").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/ReqAddress") != null) ReqAddress.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/ReqAddress").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskBirthDate") != null) AskBirthDate.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskBirthDate").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskPayPal") != null) AskPayPal.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskPayPal").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/ReqPayPal") != null) ReqPayPal.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/ReqPayPal").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/PostHide") != null) PostHide.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/PostHide").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskFriends") != null) AskFriends.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE994/AskFriends").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/AdminFullName") != null) AdminFullName.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/AdminFullName").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/AdminEmailAddress") != null) AdminEmailAddress.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/AdminEmailAddress").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyName") != null) CompanyName.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyName").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanySlogan") != null) CompanySlogan.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanySlogan").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyPhone") != null) CompanyPhone.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyPhone").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyAddressLine1") != null) CompanyAddressLine1.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyAddressLine1").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyCity") != null) CompanyCity.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyCity").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyState") != null) CompanyState.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyState").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyZipCode") != null) CompanyZipCode.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyZipCode").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyCountry") != null) CompanyCountry.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/CompanyCountry").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE991/TwitterUsername") != null) TwitterUsername.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE991/TwitterUsername").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/ConferenceEnable") != null) ConferenceEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE64/ConferenceEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/ModeratorClass") != null) ModeratorClass.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE64/ModeratorClass").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/TwilioSMSReminderMsg") != null) TwilioSMSReminderMsg.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE64/TwilioSMSReminderMsg").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/TwilioSMSReminderOffset") != null) TwilioSMSReminderOffset.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE64/TwilioSMSReminderOffset").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/PercentCelebrities") != null) PercentCelebrities.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE64/PercentCelebrities").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsEnable") != null) AdsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsStatsEnable") != null) AdsStatsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE2/AdsStatsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE2/SponsorsTarget") != null) SponsorsTarget.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE2/SponsorsTarget").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateEnable") != null) AffiliateEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateLVL1") != null) AffiliateLVL1.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateLVL1").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateLVL2") != null) AffiliateLVL2.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateLVL2").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateSignup") != null) AffiliateSignup.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateSignup").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateIDReq") != null) AffiliateIDReq.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateIDReq").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateImageText") != null) AffiliateImageText.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateImageText").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateReturnPage") != null) AffiliateReturnPage.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE39/AffiliateReturnPage").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesEnable") != null) ArticlesEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticlesEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/Articles10Newest") != null) Articles10Newest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/Articles10Newest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticleShowPic") != null) ArticleShowPic.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticleShowPic").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticleShowSource") != null) ArticleShowSource.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticleShowSource").InnerText;
            else ArticleShowSource.Value = "Yes";

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticleShowMeta") != null) ArticleShowMeta.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/ArticleShowMeta").InnerText;
            else ArticleShowMeta.Value = "Yes";

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionEnable") != null) AuctionEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionDisplayNewest") != null) AuctionDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionDeleteDays") != null) AuctionDeleteDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionDeleteDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionEmailSubject") != null) AuctionEmailSubject.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionEmailSubject").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionEmailBody") != null) AuctionEmailBody.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/AuctionEmailBody").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsEnable") != null) BlogsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE61/BlogsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessEnable") != null) BusinessEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessDisplayNewest") != null) BusinessDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessClaim") != null) BusinessClaim.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessClaim").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessUserAddress") != null) BusinessUserAddress.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE20/BusinessUserAddress").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE42/ChatEnable") != null) ChatEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE42/ChatEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE6/IMessengerEnable") != null) IMessengerEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE6/IMessengerEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedEnable") != null) ClassifiedEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedDisplayNewest") != null) ClassifiedDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedDeleteDays") != null) ClassifiedDeleteDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedDeleteDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedEmailSubject") != null) ClassifiedEmailSubject.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedEmailSubject").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedEmailBody") != null) ClassifiedEmailBody.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedEmailBody").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedBuy") != null) ClassifiedBuy.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/ClassifiedBuy").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/CNRCEnable") != null) CNRCEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE8/CNRCEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/CNRREnable") != null) CNRREnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE8/CNRREnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/RGraphEnable") != null) RGraphEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE8/RGraphEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEnable") != null) ContactEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEmail") != null) ContactEmail.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEmail").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEmailSubject") != null) ContactEmailSubject.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEmailSubject").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEmailBody") != null) ContactEmailBody.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactEmailBody").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactStreetAddress") != null) ContactStreetAddress.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactStreetAddress").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactAddress") != null) ContactAddress.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactAddress").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactPhoneNumber") != null) ContactPhoneNumber.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactPhoneNumber").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactFaxNumber") != null) ContactFaxNumber.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactFaxNumber").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactFileTypes") != null) ContactFileTypes.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactFileTypes").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactMaxFiles") != null) ContactMaxFiles.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE4/ContactMaxFiles").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE1/CREnable") != null) CREnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE1/CREnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsEnable") != null) DiscountsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsDisplayNewest") != null) DiscountsDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE5/DiscountsDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningEnable") != null) ELearningEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningNewest") != null) ELearningNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE37/ELearningNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsEnable") != null) EventsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsSMSReminders") != null) EventsSMSReminders.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE46/EventsSMSReminders").InnerText;
            else EventsSMSReminders.Value = "No";

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQEnable") != null) FAQEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQNewest") != null) FAQNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE9/FAQNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryEnable") != null) LibraryEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryDisplayNewest") != null) LibraryDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryDisplayPopular") != null) LibraryDisplayPopular.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryDisplayPopular").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryMaxUpload") != null) LibraryMaxUpload.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryMaxUpload").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryDownload") != null) LibraryDownload.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/LibraryDownload").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsEnable") != null) FormsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsSignup") != null) FormsSignup.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE13/FormsSignup").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsEnable") != null) ForumsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsDeleteDays") != null) ForumsDeleteDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsDeleteDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsDisplayNewest") != null) ForumsDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsUsersEdit") != null) ForumsUsersEdit.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsUsersEdit").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsAttachment") != null) ForumsAttachment.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE12/ForumsAttachment").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookEnable") != null) GuestbookEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookDeleteDays") != null) GuestbookDeleteDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE14/GuestbookDeleteDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE57/HoroscopesEnable") != null) HoroscopesEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE57/HoroscopesEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotEnable") != null) HotNotEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotProfiles") != null) HotNotProfiles.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE40/HotNotProfiles").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE56/INewsEnable") != null) INewsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE56/INewsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksEnable") != null) LinksEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksDisplayNewest") != null) LinksDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE19/LinksDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchEnable") != null) MatchEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchPicNo") != null) MatchPicNo.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE18/MatchPicNo").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerEnable") != null) MessengerEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerDeleteDays") != null) MessengerDeleteDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerDeleteDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerSMS") != null) MessengerSMS.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE17/MessengerSMS").InnerText;
            else MessengerSMS.Value = "No";

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsEnable") != null) NewsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsDeleteDays") != null) NewsDeleteDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE23/NewsDeleteDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsLetEnable") != null) NewsLetEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsLetEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletRemoveText") != null) NewsletRemoveText.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletRemoveText").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletFromEmail") != null) NewsletFromEmail.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletFromEmail").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlyEnable") != null) NewsletNightlyEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlyEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlyNews") != null) NewsletNightlyNews.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlyNews").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlySubject") != null) NewsletNightlySubject.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlySubject").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlyBody") != null) NewsletNightlyBody.Text = root.SelectSingleNode("/ROOTLEVEL/MODULE24/NewsletNightlyBody").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesEnable") != null) GamesEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE47/GamesEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosEnable") != null) PhotosEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotosEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotoNumber") != null) PhotoNumber.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE28/PhotoNumber").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsEnable") != null) PollsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE25/PollsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsEnable") != null) PortalsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalMasterDomain") != null) PortalMasterDomain.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalMasterDomain").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalProfiles") != null) PortalProfiles.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalProfiles").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalSiteLooks") != null) PortalSiteLooks.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE60/PortalSiteLooks").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateEnable") != null) RStateEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateDisplayNewest") != null) RStateDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateNewestRent") != null) RStateNewestRent.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateNewestRent").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateNewestSale") != null) RStateNewestSale.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateNewestSale").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateStateDrop") != null) RStateStateDrop.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateStateDrop").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateCountryDrop") != null) RStateCountryDrop.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateCountryDrop").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateMaxPhotos") != null) RStateMaxPhotos.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/RStateMaxPhotos").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferEnable") != null) ReferEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferDisplayStats") != null) ReferDisplayStats.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferDisplayStats").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferTop") != null) ReferTop.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferTop").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferEmailSubject") != null) ReferEmailSubject.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferEmailSubject").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferEmailBody") != null) ReferEmailBody.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE43/ReferEmailBody").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE3/SearchModules") != null) SearchModules.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE3/SearchModules").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE3/SearchRadius") != null) SearchRadius.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE3/SearchRadius").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE3/SearchCountry") != null) SearchCountry.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE3/SearchCountry").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE995/TaxCalcEnable") != null) TaxCalcEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE995/TaxCalcEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE995/OrderFinalText") != null) OrderFinalText.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE995/OrderFinalText").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE995/EmailTempAdmin") != null) EmailTempAdmin.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE995/EmailTempAdmin").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE995/EmailTempCust") != null) EmailTempCust.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE995/EmailTempCust").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallEnable") != null) ShopMallEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallWishList") != null) ShopMallWishList.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallWishList").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallSalesPage") != null) ShopMallSalesPage.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallSalesPage").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallElectronic") != null) ShopMallElectronic.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallElectronic").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallDisplayNewest") != null) ShopMallDisplayNewest.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/ShopMallDisplayNewest").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/StartupClass") != null) StartupClass.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/StartupClass").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/EmailAdminNew") != null) EmailAdminNew.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/EmailAdminNew").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupVerify") != null) SignupVerify.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupVerify").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAgreement") != null) SignupAgreement.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAgreement").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAdminApp") != null) SignupAdminApp.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAdminApp").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAge") != null) SignupAge.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAge").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/LoginEmail") != null) LoginEmail.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/LoginEmail").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAREmail") != null) SignupAREmail.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupAREmail").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupARSubject") != null) SignupARSubject.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupARSubject").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupARBody") != null) SignupARBody.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupARBody").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupPage") != null) SignupPage.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupPage").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/AutoUser") != null) AutoUser.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/AutoUser").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupSecTitle") != null) SignupSecTitle.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupSecTitle").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupSecDesc") != null) SignupSecDesc.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupSecDesc").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupSelMem") != null) SignupSelMem.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE997/SignupSelMem").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/FacebookAPIKey") != null) FacebookAPIKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/FacebookAPIKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalClientID") != null) PayPalClientID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalClientID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret").InnerText))
                    PayPalSecret.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GooglereCAPTCHAPublicKey") != null) GooglereCAPTCHAPublicKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/GooglereCAPTCHAPublicKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GooglereCAPTCHAPrivateKey") != null) GooglereCAPTCHAPrivateKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/GooglereCAPTCHAPrivateKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GoogleAnalyticsID") != null) GoogleAnalyticsID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/GoogleAnalyticsID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GoogleAnalyticsClientID") != null) GoogleAnalyticsClientID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/GoogleAnalyticsClientID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GoogleMapsAPI") != null) GoogleMapsAPI.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/GoogleMapsAPI").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID") != null) TwilioAccountSID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken") != null) TwilioAuthToken.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioPhoneNumber") != null) TwilioPhoneNumber.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioPhoneNumber").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioVideoSID") != null) TwilioVideoSID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioVideoSID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioVideoSecret") != null) TwilioVideoSecret.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioVideoSecret").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/CloudFlareAPI") != null) CloudFlareAPI.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/CloudFlareAPI").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/CloudFlareEmail") != null) CloudFlareEmail.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/CloudFlareEmail").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/CloudFlareDomain") != null) CloudFlareDomain.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/CloudFlareDomain").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/LinkedInAPI") != null) LinkedInAPI.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/LinkedInAPI").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/LinkedInSecret") != null) LinkedInSecret.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/LinkedInSecret").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExAccountNum") != null) FedExAccountNum.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExAccountNum").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExMeterNum") != null) FedExMeterNum.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExMeterNum").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServiceKey") != null) FedExServiceKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServiceKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServicePass") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServicePass").InnerText))
                    FedExServicePass.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSAccountNum") != null) UPSAccountNum.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSAccountNum").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSUserName") != null) UPSUserName.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSUserName").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSPassword") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSPassword").InnerText))
                    UPSPassword.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSShipperNum") != null) UPSShipperNum.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSShipperNum").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/USPSUserID") != null) USPSUserID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/USPSUserID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/ZoomAPIKey") != null) ZoomAPIKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/ZoomAPIKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/ZoomAPISecret") != null) ZoomAPISecret.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/ZoomAPISecret").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalClientID") != null) PayPalClientID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalClientID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret").InnerText))
                    PayPalSecret.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE68/LDAPPath") != null) LDAPPath.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE68/LDAPPath").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE68/LDAPDomain") != null) LDAPDomain.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE68/LDAPDomain").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceEnable") != null) VideoConferenceEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceSendSMS") != null) VideoConferenceSendSMS.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceSendSMS").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceSMSOffset") != null) VideoConferenceSMSOffset.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE69/VideoConferenceSMSOffset").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityAPIKey") != null) SepCityAPIKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityAPIKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityUser") != null) SepCityUser.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityUser").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityPassword") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityPassword").InnerText))
                    SepCityPassword.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/CRMVersion") != null)
            {
                CRMVersion.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/CRMVersion").InnerText;
            }

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/STURL") != null) STURL.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/STURL").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/STUser") != null) STUser.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/STUser").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/STPass") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/STPass").InnerText))
                    STPass.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/STKBEnable") != null) STKBEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/STKBEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/STKBNewsEnable") != null) STKBNewsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/STKBNewsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/STDatabase") != null) STDatabase.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/STDatabase").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMURL") != null) SugarCRMURL.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMURL").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMUser") != null) SugarCRMUser.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMUser").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/SugarCRMPass") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/SugarCRMPass").InnerText))
                    SugarCRMPass.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMDatabase") != null) SugarCRMDatabase.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMDatabase").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMURL") != null) SuiteCRMURL.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMURL").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMUser") != null) SuiteCRMUser.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMUser").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/SuiteCRMPass") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/SuiteCRMPass").InnerText))
                    SuiteCRMPass.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMDatabase") != null) SuiteCRMDatabase.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMDatabase").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSURL") != null) WHMCSURL.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSURL").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSUser") != null) WHMCSUser.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSUser").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/WHMCSPass") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE989/WHMCSPass").InnerText))
                    WHMCSPass.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSKB") != null) WHMCSKB.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSKB").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSKB") != null) WHMCSKB.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSKB").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/SpeakerEnable") != null) SpeakerEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE50/SpeakerEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE15/StocksSymbols") != null) StocksSymbols.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE15/StocksSymbols").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesEnable") != null) UPagesEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesSignupProcess") != null) UPagesSignupProcess.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesSignupProcess").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesTop10") != null) UPagesTop10.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesTop10").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu1") != null) UPagesMenu1.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu1").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu2") != null) UPagesMenu2.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu2").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu3") != null) UPagesMenu3.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu3").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu4") != null) UPagesMenu4.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu4").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu5") != null) UPagesMenu5.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu5").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu6") != null) UPagesMenu6.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu6").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu7") != null) UPagesMenu7.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMenu7").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu1") != null) UPagesMainMenu1.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu1").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu2") != null) UPagesMainMenu2.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu2").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu3") != null) UPagesMainMenu3.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu3").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu4") != null) UPagesMainMenu4.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu4").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu5") != null) UPagesMainMenu5.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu5").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu6") != null) UPagesMainMenu6.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu6").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu7") != null) UPagesMainMenu7.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE7/UPagesMainMenu7").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesEnable") != null) ProfilesEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAskSignup") != null) ProfilesAskSignup.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAskSignup").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesPicNo") != null) ProfilesPicNo.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesPicNo").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfileRequired") != null) ProfileRequired.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfileRequired").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesType1") != null) ProfilesType1.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesType1").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesType2") != null) ProfilesType2.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesType2").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesType3") != null) ProfilesType3.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesType3").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesColor") != null) ProfilesColor.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesColor").InnerText;
            else ProfilesColor.Value = "No";

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAudio") != null) ProfilesAudio.Value = Strings.ToString(SepFunctions.toLong(root.SelectSingleNode("/ROOTLEVEL/MODULE63/ProfilesAudio").InnerText));
            else ProfilesAudio.Value = "0";

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherEnable") != null) VoucherEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherDaysAdd") != null) VoucherDaysAdd.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherDaysAdd").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherExpireDays") != null) VoucherExpireDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherExpireDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAgreement") != null) VoucherAgreement.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAgreement").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAgreementBuy") != null) VoucherAgreementBuy.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherAgreementBuy").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherTop10") != null) VoucherTop10.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE65/VoucherTop10").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCREnable") != null) PCREnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCREnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAPIKey") != null) PCRAPIKey.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAPIKey").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAppID") != null) PCRAppID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAppID").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAPIURL") != null) PCRAPIURL.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRAPIURL").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRDatabaseId") != null) PCRDatabaseId.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRDatabaseId").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRUserName") != null) PCRUserName.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRUserName").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRPassword") != null)
                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRPassword").InnerText))
                    PCRPassword.Value = SepFunctions.LangText("(Encrypted Data)");

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRExpireDays") != null) PCRExpireDays.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRExpireDays").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE55/WeatherEnable") != null) WeatherEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE55/WeatherEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE62/FeedsEnable") != null) FeedsEnable.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE62/FeedsEnable").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu1Text") != null) Menu1Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu1Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu2Text") != null) Menu2Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu2Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu3Text") != null) Menu3Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu3Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu4Text") != null) Menu4Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu4Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu5Text") != null) Menu5Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu5Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu6Text") != null) Menu6Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu6Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu7Text") != null) Menu7Text.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu7Text").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu1Sitemap") != null) Menu1Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu1Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu2Sitemap") != null) Menu2Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu2Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu3Sitemap") != null) Menu3Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu3Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu4Sitemap") != null) Menu4Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu4Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu5Sitemap") != null) Menu5Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu5Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu6Sitemap") != null) Menu6Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu6Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu7Sitemap") != null) Menu7Sitemap.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/Menu7Sitemap").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo") != null)
            {
                if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo").InnerText) > 0)
                {
                    SiteLogoImg.ImageUrl = "data:image/png;base64," + root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo").InnerText;
                    SiteLogoImg.Style.Add("display", "block");
                    SiteLogoLabel.InnerHtml = SepFunctions.LangText("Replace Website Logo");
                    SiteLogoImg.Visible = true;
                    RemoveSiteLogo.Visible = true;
                }
                else
                {
                    SiteLogoImg.Visible = false;
                    RemoveSiteLogo.Visible = false;
                }
            }

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/RSSTop") != null) RSSTop.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/RSSTop").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/FavoritesTop") != null) FavoritesTop.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/FavoritesTop").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/MainPageTop") != null) MainPageTop.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/MainPageTop").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/SocialSharing") != null) SocialSharing.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE993/SocialSharing").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/WebSiteName") != null) WebSiteName.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/WebSiteName").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/CurrencyCode") != null) CurrencyCode.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/CurrencyCode").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/BadWordFilter") != null) BadWordFilter.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/BadWordFilter").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/RecPerAPage") != null) RecPerAPage.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/RecPerAPage").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/MaxImageSize") != null) MaxImageSize.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/MaxImageSize").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/SiteLang") != null) SiteLang.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/SiteLang").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/CatCount") != null) CatCount.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/CatCount").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/TimeOffset") != null) TimeOffset.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/TimeOffset").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE992/CatLowestLvl") != null) CatLowestLvl.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE992/CatLowestLvl").InnerText;

            if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/RequireSSL") != null)
            {
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/RequireSSL").InnerText == "Yes")
                {
                    RequireSSL.Value = "Yes";
                }
                else
                {
                    RequireSSL.Value = "No";
                }
            }
            else
            {
                RequireSSL.Value = "No";
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
                    PPShowMemberships.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowMemberships.Items[1].Value = SepFunctions.LangText("No");
                    PPShowOrders.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowOrders.Items[1].Value = SepFunctions.LangText("No");
                    PPShowAdStats.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowAdStats.Items[1].Value = SepFunctions.LangText("No");
                    PPShowFriends.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowFriends.Items[1].Value = SepFunctions.LangText("No");
                    PPShowFavorites.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowFavorites.Items[1].Value = SepFunctions.LangText("No");
                    PPShowAffiliate.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowAffiliate.Items[1].Value = SepFunctions.LangText("No");
                    PPShowMessenger.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowMessenger.Items[1].Value = SepFunctions.LangText("No");
                    PPShowProfile.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowProfile.Items[1].Value = SepFunctions.LangText("No");
                    PPShowBlogs.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowBlogs.Items[1].Value = SepFunctions.LangText("No");
                    PPShowForums.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowForums.Items[1].Value = SepFunctions.LangText("No");
                    PPShowMain.Items[0].Value = SepFunctions.LangText("Yes");
                    PPShowMain.Items[1].Value = SepFunctions.LangText("No");
                    PPDefaultPage.Items[0].Value = SepFunctions.LangText("Account");
                    PPDefaultPage.Items[1].Value = SepFunctions.LangText("PageText");
                    PPDefaultPage.Items[2].Value = SepFunctions.LangText("Memberships");
                    PPDefaultPage.Items[3].Value = SepFunctions.LangText("Orders");
                    PPDefaultPage.Items[4].Value = SepFunctions.LangText("AdStats");
                    PPDefaultPage.Items[5].Value = SepFunctions.LangText("Friends");
                    PPDefaultPage.Items[6].Value = SepFunctions.LangText("Favorites");
                    PPDefaultPage.Items[7].Value = SepFunctions.LangText("Affiliate");
                    PPDefaultPage.Items[8].Value = SepFunctions.LangText("Messenger");
                    FriendsEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    FriendsEnable.Items[1].Value = SepFunctions.LangText("No");
                    ShowCredits.Items[0].Value = SepFunctions.LangText("Yes");
                    ShowCredits.Items[1].Value = SepFunctions.LangText("No");
                    AskGender.Items[0].Value = SepFunctions.LangText("Yes");
                    AskGender.Items[1].Value = SepFunctions.LangText("No");
                    AskPhoneNumber.Items[0].Value = SepFunctions.LangText("Yes");
                    AskPhoneNumber.Items[1].Value = SepFunctions.LangText("No");
                    ReqPhoneNumber.Items[0].Value = SepFunctions.LangText("Yes");
                    ReqPhoneNumber.Items[1].Value = SepFunctions.LangText("No");
                    AskStreetAddress.Items[0].Value = SepFunctions.LangText("Yes");
                    AskStreetAddress.Items[1].Value = SepFunctions.LangText("No");
                    AskCity.Items[0].Value = SepFunctions.LangText("Yes");
                    AskCity.Items[1].Value = SepFunctions.LangText("No");
                    AskState.Items[0].Value = SepFunctions.LangText("Yes");
                    AskState.Items[1].Value = SepFunctions.LangText("No");
                    AskZipCode.Items[0].Value = SepFunctions.LangText("Yes");
                    AskZipCode.Items[1].Value = SepFunctions.LangText("No");
                    AskCountry.Items[0].Value = SepFunctions.LangText("Yes");
                    AskCountry.Items[1].Value = SepFunctions.LangText("No");
                    ReqAddress.Items[0].Value = SepFunctions.LangText("Yes");
                    ReqAddress.Items[1].Value = SepFunctions.LangText("No");
                    AskBirthDate.Items[0].Value = SepFunctions.LangText("Yes");
                    AskBirthDate.Items[1].Value = SepFunctions.LangText("No");
                    AskPayPal.Items[0].Value = SepFunctions.LangText("Yes");
                    AskPayPal.Items[1].Value = SepFunctions.LangText("No");
                    ReqPayPal.Items[0].Value = SepFunctions.LangText("Yes");
                    ReqPayPal.Items[1].Value = SepFunctions.LangText("No");
                    PostHide.Items[0].Value = SepFunctions.LangText("Yes");
                    PostHide.Items[1].Value = SepFunctions.LangText("No");
                    AskFriends.Items[0].Value = SepFunctions.LangText("Yes");
                    AskFriends.Items[1].Value = SepFunctions.LangText("No");
                    ConferenceEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ConferenceEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    AdsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    AdsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    AdsStatsEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    AdsStatsEnable.Items[1].Value = SepFunctions.LangText("No");
                    AffiliateEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    AffiliateEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    AffiliateSignup.Items[0].Value = SepFunctions.LangText("Yes");
                    AffiliateSignup.Items[1].Value = SepFunctions.LangText("No");
                    AffiliateIDReq.Items[0].Value = SepFunctions.LangText("Yes");
                    AffiliateIDReq.Items[1].Value = SepFunctions.LangText("No");
                    ArticlesEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ArticlesEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    Articles10Newest.Items[0].Value = SepFunctions.LangText("Yes");
                    Articles10Newest.Items[1].Value = SepFunctions.LangText("No");
                    ArticleShowPic.Items[0].Value = SepFunctions.LangText("Yes");
                    ArticleShowPic.Items[1].Value = SepFunctions.LangText("No");
                    ArticleShowSource.Items[0].Value = SepFunctions.LangText("Yes");
                    ArticleShowSource.Items[1].Value = SepFunctions.LangText("No");
                    ArticleShowMeta.Items[0].Value = SepFunctions.LangText("Yes");
                    ArticleShowMeta.Items[1].Value = SepFunctions.LangText("No");
                    AuctionEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    AuctionEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    AuctionDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    AuctionDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    BlogsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    BlogsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    BusinessEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    BusinessEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    BusinessDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    BusinessDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    BusinessClaim.Items[0].Value = SepFunctions.LangText("Yes");
                    BusinessClaim.Items[1].Value = SepFunctions.LangText("No");
                    BusinessUserAddress.Items[0].Value = SepFunctions.LangText("Yes");
                    BusinessUserAddress.Items[1].Value = SepFunctions.LangText("No");
                    ChatEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ChatEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    IMessengerEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    IMessengerEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ClassifiedEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ClassifiedEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ClassifiedDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    ClassifiedDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    ClassifiedBuy.Items[0].Value = SepFunctions.LangText("Yes");
                    ClassifiedBuy.Items[1].Value = SepFunctions.LangText("No");
                    CNRCEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    CNRCEnable.Items[1].Value = SepFunctions.LangText("No");
                    CNRREnable.Items[0].Value = SepFunctions.LangText("Yes");
                    CNRREnable.Items[1].Value = SepFunctions.LangText("No");
                    RGraphEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    RGraphEnable.Items[1].Value = SepFunctions.LangText("No");
                    ContactEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ContactEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ContactStreetAddress.Items[0].Value = SepFunctions.LangText("Yes");
                    ContactStreetAddress.Items[1].Value = SepFunctions.LangText("No");
                    ContactAddress.Items[0].Value = SepFunctions.LangText("Yes");
                    ContactAddress.Items[1].Value = SepFunctions.LangText("No");
                    ContactPhoneNumber.Items[0].Value = SepFunctions.LangText("Yes");
                    ContactPhoneNumber.Items[1].Value = SepFunctions.LangText("No");
                    ContactFaxNumber.Items[0].Value = SepFunctions.LangText("Yes");
                    ContactFaxNumber.Items[1].Value = SepFunctions.LangText("No");
                    ContactFileTypes.Items[0].Value = SepFunctions.LangText("Disabled");
                    ContactFileTypes.Items[1].Value = SepFunctions.LangText("Any");
                    ContactFileTypes.Items[2].Value = SepFunctions.LangText("Audio");
                    ContactFileTypes.Items[3].Value = SepFunctions.LangText("Document");
                    ContactFileTypes.Items[4].Value = SepFunctions.LangText("Images");
                    ContactFileTypes.Items[5].Value = SepFunctions.LangText("Software");
                    ContactFileTypes.Items[6].Value = SepFunctions.LangText("Video");
                    CREnable.Items[0].Value = SepFunctions.LangText("Yes");
                    CREnable.Items[1].Value = SepFunctions.LangText("No");
                    DiscountsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    DiscountsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    DiscountsDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    DiscountsDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    ELearningEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ELearningEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    EventsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    EventsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    EventsSMSReminders.Items[0].Value = SepFunctions.LangText("Yes");
                    EventsSMSReminders.Items[1].Value = SepFunctions.LangText("No");
                    FAQEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    FAQEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    FAQNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    FAQNewest.Items[1].Value = SepFunctions.LangText("No");
                    LibraryEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    LibraryEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    LibraryDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    LibraryDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    LibraryDisplayNewest.Items[2].Value = SepFunctions.LangText("Audio Only");
                    LibraryDisplayPopular.Items[0].Value = SepFunctions.LangText("Yes");
                    LibraryDisplayPopular.Items[1].Value = SepFunctions.LangText("No");
                    LibraryDisplayPopular.Items[2].Value = SepFunctions.LangText("Audio Only");
                    LibraryDownload.Items[0].Value = SepFunctions.LangText("Yes");
                    LibraryDownload.Items[1].Value = SepFunctions.LangText("No");
                    FormsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    FormsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    FormsSignup.Items[0].Value = SepFunctions.LangText("Disabled");
                    ForumsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ForumsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ForumsDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    ForumsDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    ForumsUsersEdit.Items[0].Value = SepFunctions.LangText("Yes");
                    ForumsUsersEdit.Items[1].Value = SepFunctions.LangText("No");
                    ForumsAttachment.Items[0].Value = SepFunctions.LangText("Yes");
                    ForumsAttachment.Items[1].Value = SepFunctions.LangText("No");
                    GuestbookEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    GuestbookEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    HoroscopesEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    HoroscopesEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    HotNotEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    HotNotEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    HotNotProfiles.Items[0].Value = SepFunctions.LangText("Yes");
                    HotNotProfiles.Items[1].Value = SepFunctions.LangText("No");
                    STKBEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    STKBEnable.Items[1].Value = SepFunctions.LangText("No");
                    STKBNewsEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    STKBNewsEnable.Items[1].Value = SepFunctions.LangText("No");
                    STDatabase.Items[0].Value = SepFunctions.LangText("When submit a support ticket");
                    STDatabase.Items[1].Value = SepFunctions.LangText("When new users signup");
                    STDatabase.Items[2].Value = SepFunctions.LangText("When users place an order");
                    SugarCRMDatabase.Items[0].Value = SepFunctions.LangText("When submit a support ticket");
                    SugarCRMDatabase.Items[1].Value = SepFunctions.LangText("When new users signup");
                    SugarCRMDatabase.Items[2].Value = SepFunctions.LangText("When users place an order");
                    SuiteCRMDatabase.Items[0].Value = SepFunctions.LangText("When submit a support ticket");
                    SuiteCRMDatabase.Items[1].Value = SepFunctions.LangText("When new users signup");
                    SuiteCRMDatabase.Items[2].Value = SepFunctions.LangText("When users place an order");
                    WHMCSKB.Items[0].Value = SepFunctions.LangText("Yes");
                    WHMCSKB.Items[1].Value = SepFunctions.LangText("No");
                    WHMCSDatabase.Items[0].Value = SepFunctions.LangText("When submit a support ticket");
                    WHMCSDatabase.Items[1].Value = SepFunctions.LangText("When new users signup");
                    WHMCSDatabase.Items[2].Value = SepFunctions.LangText("When users place an order");
                    INewsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    INewsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    LinksEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    LinksEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    LinksDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    LinksDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    MatchEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    MatchEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    MessengerEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    MessengerEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    MessengerSMS.Items[0].Value = SepFunctions.LangText("Yes");
                    MessengerSMS.Items[1].Value = SepFunctions.LangText("No");
                    NewsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    NewsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    NewsLetEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    NewsLetEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    NewsletNightlyEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    NewsletNightlyEnable.Items[1].Value = SepFunctions.LangText("No");
                    NewsletNightlyNews.Items[0].Value = SepFunctions.LangText("Select a Newsletter");
                    GamesEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    GamesEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    PhotosEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    PhotosEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    PollsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    PollsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    PortalsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    PortalsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    PortalProfiles.Items[0].Value = SepFunctions.LangText("Yes");
                    PortalProfiles.Items[1].Value = SepFunctions.LangText("No");
                    PortalSiteLooks.Items[0].Value = SepFunctions.LangText("Yes");
                    PortalSiteLooks.Items[1].Value = SepFunctions.LangText("No");
                    RStateEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    RStateEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    RStateDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    RStateDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    RStateNewestRent.Items[0].Value = SepFunctions.LangText("Yes");
                    RStateNewestRent.Items[1].Value = SepFunctions.LangText("No");
                    RStateNewestSale.Items[0].Value = SepFunctions.LangText("Yes");
                    RStateNewestSale.Items[1].Value = SepFunctions.LangText("No");
                    RStateStateDrop.Items[0].Value = SepFunctions.LangText("Yes");
                    RStateStateDrop.Items[1].Value = SepFunctions.LangText("No");
                    RStateCountryDrop.Items[0].Value = SepFunctions.LangText("Yes");
                    RStateCountryDrop.Items[1].Value = SepFunctions.LangText("No");
                    ReferEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ReferEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ReferDisplayStats.Items[0].Value = SepFunctions.LangText("Yes");
                    ReferDisplayStats.Items[1].Value = SepFunctions.LangText("No");
                    ReferTop.Items[0].Value = SepFunctions.LangText("Yes");
                    ReferTop.Items[1].Value = SepFunctions.LangText("No");
                    SearchModules.Items[0].Value = SepFunctions.LangText("Yes");
                    SearchModules.Items[1].Value = SepFunctions.LangText("No");
                    SearchRadius.Items[0].Value = SepFunctions.LangText("Yes");
                    SearchRadius.Items[1].Value = SepFunctions.LangText("No");
                    TaxCalcEnable.Items[0].Value = SepFunctions.LangText("Yes");
                    TaxCalcEnable.Items[1].Value = SepFunctions.LangText("No");
                    ShopMallEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ShopMallEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ShopMallWishList.Items[0].Value = SepFunctions.LangText("Yes");
                    ShopMallWishList.Items[1].Value = SepFunctions.LangText("No");
                    ShopMallSalesPage.Items[0].Value = SepFunctions.LangText("Yes");
                    ShopMallSalesPage.Items[1].Value = SepFunctions.LangText("No");
                    ShopMallDisplayNewest.Items[0].Value = SepFunctions.LangText("Yes");
                    ShopMallDisplayNewest.Items[1].Value = SepFunctions.LangText("No");
                    SignupVerify.Items[0].Value = SepFunctions.LangText("Yes");
                    SignupVerify.Items[1].Value = SepFunctions.LangText("No");
                    SignupAdminApp.Items[0].Value = SepFunctions.LangText("Yes");
                    SignupAdminApp.Items[1].Value = SepFunctions.LangText("No");
                    LoginEmail.Items[0].Value = SepFunctions.LangText("Yes");
                    LoginEmail.Items[1].Value = SepFunctions.LangText("No");
                    SignupSelMem.Items[0].Value = SepFunctions.LangText("Yes");
                    SignupSelMem.Items[1].Value = SepFunctions.LangText("No");
                    SpeakerEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    SpeakerEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    UPagesEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    UPagesEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    UPagesSignupProcess.Items[0].Value = SepFunctions.LangText("Disabled");
                    UPagesSignupProcess.Items[1].Value = SepFunctions.LangText("Allow users to select a user page on the signup form");
                    UPagesSignupProcess.Items[2].Value = SepFunctions.LangText("Require users to select a user page on the signup form");
                    UPagesTop10.Items[0].Value = SepFunctions.LangText("No");
                    UPagesTop10.Items[1].Value = SepFunctions.LangText("Yes");
                    ProfilesEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    ProfilesEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    ProfilesAskSignup.Items[0].Value = SepFunctions.LangText("Yes");
                    ProfilesAskSignup.Items[1].Value = SepFunctions.LangText("No");
                    ProfileRequired.Items[0].Value = SepFunctions.LangText("Yes");
                    ProfileRequired.Items[1].Value = SepFunctions.LangText("No");
                    ProfilesColor.Items[0].Value = SepFunctions.LangText("Yes");
                    ProfilesColor.Items[1].Value = SepFunctions.LangText("No");
                    WeatherEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    WeatherEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    FeedsEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    FeedsEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    VoucherEnable.Items[0].Value = SepFunctions.LangText("Enable");
                    VoucherEnable.Items[1].Value = SepFunctions.LangText("Disable");
                    VoucherTop10.Items[0].Value = SepFunctions.LangText("Yes");
                    VoucherTop10.Items[1].Value = SepFunctions.LangText("No");
                    PCREnable.Items[0].Value = SepFunctions.LangText("Enable");
                    PCREnable.Items[1].Value = SepFunctions.LangText("Disable");
                    Menu1Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu1Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    Menu2Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu2Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    Menu3Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu3Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    Menu4Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu4Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    Menu5Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu5Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    Menu6Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu6Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    Menu7Sitemap.Items[0].Value = SepFunctions.LangText("Yes");
                    Menu7Sitemap.Items[1].Value = SepFunctions.LangText("No");
                    RSSTop.Items[0].Value = SepFunctions.LangText("Yes");
                    RSSTop.Items[1].Value = SepFunctions.LangText("No");
                    FavoritesTop.Items[0].Value = SepFunctions.LangText("Yes");
                    FavoritesTop.Items[1].Value = SepFunctions.LangText("No");
                    MainPageTop.Items[0].Value = SepFunctions.LangText("Yes");
                    MainPageTop.Items[1].Value = SepFunctions.LangText("No");
                    SocialSharing.Items[0].Value = SepFunctions.LangText("Yes");
                    SocialSharing.Items[1].Value = SepFunctions.LangText("No");
                    CurrencyCode.Items[0].Value = SepFunctions.LangText("CAD");
                    CurrencyCode.Items[1].Value = SepFunctions.LangText("EUR");
                    CurrencyCode.Items[2].Value = SepFunctions.LangText("GBP");
                    CurrencyCode.Items[3].Value = SepFunctions.LangText("RM");
                    CurrencyCode.Items[4].Value = SepFunctions.LangText("USD");
                    SiteLang.Items[0].Value = SepFunctions.LangText("English (United States)");
                    SiteLang.Items[1].Value = SepFunctions.LangText("Dutch (The Netherlands)");
                    SiteLang.Items[2].Value = SepFunctions.LangText("French (Canada)");
                    SiteLang.Items[3].Value = SepFunctions.LangText("French (France)");
                    SiteLang.Items[4].Value = SepFunctions.LangText("Portuguese (Brazil)");
                    SiteLang.Items[5].Value = SepFunctions.LangText("Spanish (Mexico)");
                    SiteLang.Items[6].Value = SepFunctions.LangText("Spanish (Spain)");
                    CatCount.Items[0].Value = SepFunctions.LangText("Yes");
                    CatCount.Items[1].Value = SepFunctions.LangText("No");
                    CatLowestLvl.Items[0].Value = SepFunctions.LangText("Yes");
                    CatLowestLvl.Items[1].Value = SepFunctions.LangText("No");
                    Module33Title.InnerHtml = SepFunctions.LangText("Setup the Account Info");
                    Module994Title.InnerHtml = SepFunctions.LangText("Setup the Account Management");
                    Module991Title.InnerHtml = SepFunctions.LangText("Administrator Information");
                    Module64Title.InnerHtml = SepFunctions.LangText("Setup the Conference Center");
                    Module2Title.InnerHtml = SepFunctions.LangText("Setup the Advertisements");
                    Module39Title.InnerHtml = SepFunctions.LangText("Setup the Affiliate Program");
                    Module35Title.InnerHtml = SepFunctions.LangText("Setup the Articles");
                    Module31Title.InnerHtml = SepFunctions.LangText("Setup the Auctions");
                    Module61Title.InnerHtml = SepFunctions.LangText("Setup the Blogger");
                    Module20Title.InnerHtml = SepFunctions.LangText("Setup the Business Directory");
                    Module42Title.InnerHtml = SepFunctions.LangText("Setup the Chat Rooms");
                    Module6Title.InnerHtml = SepFunctions.LangText("Setup the Instant Messenger");
                    Module44Title.InnerHtml = SepFunctions.LangText("Setup the Classified Ads");
                    Module8Title.InnerHtml = SepFunctions.LangText("Setup the Comments & Ratings");
                    Module4Title.InnerHtml = SepFunctions.LangText("Setup the Contact Us");
                    Module1Title.InnerHtml = SepFunctions.LangText("Setup the Content Rotator");
                    Module5Title.InnerHtml = SepFunctions.LangText("Setup the Discount System");
                    Module37Title.InnerHtml = SepFunctions.LangText("Setup the E-Learning");
                    Module46Title.InnerHtml = SepFunctions.LangText("Setup the Event Calendar");
                    Module9Title.InnerHtml = SepFunctions.LangText("Setup the Frequency Asked Questions");
                    Module10Title.InnerHtml = SepFunctions.LangText("Setup the Downloads");
                    Module13Title.InnerHtml = SepFunctions.LangText("Setup the Forms");
                    Module12Title.InnerHtml = SepFunctions.LangText("Setup the Forums");
                    Module14Title.InnerHtml = SepFunctions.LangText("Setup the Guestbook");
                    Module57Title.InnerHtml = SepFunctions.LangText("Setup the Horoscopes");
                    Module40Title.InnerHtml = SepFunctions.LangText("Setup the Hot or Not");
                    Module989ATitle.InnerHtml = SepFunctions.LangText("Setup Facebook");
                    Module989BTitle.InnerHtml = SepFunctions.LangText("Setup Google reCAPTCHA");
                    H2.InnerHtml = SepFunctions.LangText("Setup Google Analytics");
                    Module989CTitle.InnerHtml = SepFunctions.LangText("Setup Twilio");
                    Module989DTitle.InnerHtml = SepFunctions.LangText("Setup CloudFlare");
                    Module989ETitle.InnerHtml = SepFunctions.LangText("Setup LinkedIn");
                    Module67Title.InnerHtml = SepFunctions.LangText("Setup CRM/Support");
                    Module56Title.InnerHtml = SepFunctions.LangText("Setup the International News");
                    Module19Title.InnerHtml = SepFunctions.LangText("Setup the Links");
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
                    Module3Title.InnerHtml = SepFunctions.LangText("Setup the Search Engine");
                    Module995Title.InnerHtml = SepFunctions.LangText("Setup the Shopping Cart");
                    Module41Title.InnerHtml = SepFunctions.LangText("Setup the Shopping Mall");
                    Module997Title.InnerHtml = SepFunctions.LangText("Setup the Signup Setup");
                    Module50Title.InnerHtml = SepFunctions.LangText("Setup the Speaker's Bureau");
                    Module15Title.InnerHtml = SepFunctions.LangText("Setup the Stocks");
                    Module7Title.InnerHtml = SepFunctions.LangText("Setup the User Pages");
                    Module63Title.InnerHtml = SepFunctions.LangText("Setup the User Profiles");
                    Module55Title.InnerHtml = SepFunctions.LangText("Setup the Weather Forecast");
                    Module62Title.InnerHtml = SepFunctions.LangText("User Feeds");
                    Module65Title.InnerHtml = SepFunctions.LangText("Setup the Vouchers");
                    H1.InnerHtml = SepFunctions.LangText("Setup PCRecruiter");
                    Module993Title.InnerHtml = SepFunctions.LangText("Setup the Website Layout");
                    Module992Title.InnerHtml = SepFunctions.LangText("Setup the Website");
                    LoginPageLabel.InnerHtml = SepFunctions.LangText("Page to go to when someone logs into your web site:");
                    PPShowMembershipsLabel.InnerHtml = SepFunctions.LangText("Show the memberships tab by default:");
                    PPShowOrdersLabel.InnerHtml = SepFunctions.LangText("Show the order status tab by default:");
                    PPShowAdStatsLabel.InnerHtml = SepFunctions.LangText("Show the ad stats tab by default:");
                    PPShowFriendsLabel.InnerHtml = SepFunctions.LangText("Show the friends tab by default:");
                    PPShowFavoritesLabel.InnerHtml = SepFunctions.LangText("Show the favorites tab by default:");
                    PPShowAffiliateLabel.InnerHtml = SepFunctions.LangText("Show the affiliate tabs by default:");
                    PPShowMessengerLabel.InnerHtml = SepFunctions.LangText("Show the messenger tab by default:");
                    PPShowProfileLabel.InnerHtml = SepFunctions.LangText("Show the profile tab by default:");
                    PPShowBlogsLabel.InnerHtml = SepFunctions.LangText("Show the blogs tab by default:");
                    PPShowForumsLabel.InnerHtml = SepFunctions.LangText("Show the forums tab by default:");
                    PPShowMainLabel.InnerHtml = SepFunctions.LangText("Show the main page tab by default:");
                    PPDefaultPageLabel.InnerHtml = SepFunctions.LangText("Default page a user will go to when entering their account:");
                    FriendsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the friends list:");
                    ShowCreditsLabel.InnerHtml = SepFunctions.LangText("Show credits that a user has in the account info page:");
                    AskGenderLabel.InnerHtml = SepFunctions.LangText("Ask users their gender when they signup:");
                    AskPhoneNumberLabel.InnerHtml = SepFunctions.LangText("Ask users their phone number:");
                    ReqPhoneNumberLabel.InnerHtml = SepFunctions.LangText("Require phone number when users signup:");
                    AskStreetAddressLabel.InnerHtml = SepFunctions.LangText("Ask users their street address:");
                    AskCityLabel.InnerHtml = SepFunctions.LangText("Ask users their city:");
                    AskStateLabel.InnerHtml = SepFunctions.LangText("Ask users the state they live in:");
                    AskZipCodeLabel.InnerHtml = SepFunctions.LangText("Ask users their zip code:");
                    AskCountryLabel.InnerHtml = SepFunctions.LangText("Ask users their country:");
                    ReqAddressLabel.InnerHtml = SepFunctions.LangText("Require address when users signup:");
                    AskBirthDateLabel.InnerHtml = SepFunctions.LangText("Ask users their date of birth:");
                    AskPayPalLabel.InnerHtml = SepFunctions.LangText("Ask users their PayPal account:");
                    ReqPayPalLabel.InnerHtml = SepFunctions.LangText("Require paypal account when users signup:");
                    PostHideLabel.InnerHtml = SepFunctions.LangText("Hide post links if user has no access:");
                    AskFriendsLabel.InnerHtml = SepFunctions.LangText("Ask users to approve friends:");
                    AdminFullNameLabel.InnerHtml = SepFunctions.LangText("Full name of the administrator:");
                    AdminEmailAddressLabel.InnerHtml = SepFunctions.LangText("Administrator email address:");
                    CompanyNameLabel.InnerHtml = SepFunctions.LangText("Your company name:");
                    CompanySloganLabel.InnerHtml = SepFunctions.LangText("Your company slogan:");
                    CompanyPhoneLabel.InnerHtml = SepFunctions.LangText("Your company phone number:");
                    CompanyAddressLine1Label.InnerHtml = SepFunctions.LangText("Your company street address:");
                    CompanyCityLabel.InnerHtml = SepFunctions.LangText("City your company is located in:");
                    CompanyStateLabel.InnerHtml = SepFunctions.LangText("State your company is located in:");
                    CompanyZipCodeLabel.InnerHtml = SepFunctions.LangText("Your company zip/postal code:");
                    CompanyCountryLabel.InnerHtml = SepFunctions.LangText("Your company country:");
                    TwitterUsernameLabel.InnerHtml = SepFunctions.LangText("Twitter User Name:");
                    ConferenceEnableLabel.InnerHtml = SepFunctions.LangText("Enable the conference center:");
                    ModeratorClassLabel.InnerHtml = SepFunctions.LangText("Select an access class for the moderator's:");
                    TwilioSMSReminderMsgLabel.InnerHtml = SepFunctions.LangText("SMS message reminder text (Max 150 Characters):");
                    TwilioSMSReminderOffsetLabel.InnerHtml = SepFunctions.LangText("Hours before call is scheduled to send an SMS message to the user:");
                    AdsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the banner advertisements:");
                    AdsStatsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the statistics so advertisers can lookup their statistics:");
                    SponsorsTargetLabel.InnerHtml = SepFunctions.LangText("Target window to open the sponsors website in:");
                    AffiliateEnableLabel.InnerHtml = SepFunctions.LangText("Enable the affiliate module:");
                    AffiliateLVL1Label.InnerHtml = SepFunctions.LangText("Level 1 Percentage:");
                    AffiliateLVL2Label.InnerHtml = SepFunctions.LangText("Level 2 Percentage:");
                    AffiliateSignupLabel.InnerHtml = SepFunctions.LangText("Allow users to enter affiliate in signup form:");
                    AffiliateIDReqLabel.InnerHtml = SepFunctions.LangText("Require affiliate to be entered on signup form:");
                    AffiliateImageTextLabel.InnerHtml = SepFunctions.LangText("Text to show on the affiliate image page. (HTML is supported):");
                    AffiliateReturnPageLabel.InnerHtml = SepFunctions.LangText("Page to return users to when they click the affiliate image:");
                    ArticlesEnableLabel.InnerHtml = SepFunctions.LangText("Enable the articles module:");
                    Articles10NewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest articles:");
                    ArticleShowPicLabel.InnerHtml = SepFunctions.LangText("Show profile picture when displaying an article:");
                    ArticleShowSourceLabel.InnerHtml = SepFunctions.LangText("Allow users to enter the article source information when submitting an article:");
                    ArticleShowMetaLabel.InnerHtml = SepFunctions.LangText("Allow users to fill out meta tags when submitting an article:");
                    AuctionEnableLabel.InnerHtml = SepFunctions.LangText("Enable the auction:");
                    AuctionDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest ad postings:");
                    AuctionDeleteDaysLabel.InnerHtml = SepFunctions.LangText("Enter the days till an ad expires:");
                    AuctionEmailSubjectLabel.InnerHtml = SepFunctions.LangText("Subject of the email when someone posts a new ad:");
                    AuctionEmailBodyLabel.InnerHtml = SepFunctions.LangText("Body of the email message when someone posts a new ad:");
                    BlogsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the blogger:");
                    BusinessEnableLabel.InnerHtml = SepFunctions.LangText("Enable the business directory:");
                    BusinessDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest business on the main page:");
                    BusinessClaimLabel.InnerHtml = SepFunctions.LangText("Allow users to claim listings. (Members must verify the contact email assigned to the business):");
                    BusinessUserAddressLabel.InnerHtml = SepFunctions.LangText("Get address information from the user instead of allowing a user to enter a business address:");
                    ChatEnableLabel.InnerHtml = SepFunctions.LangText("Enable the chat rooms:");
                    IMessengerEnableLabel.InnerHtml = SepFunctions.LangText("Enable the instant messenger:");
                    ClassifiedEnableLabel.InnerHtml = SepFunctions.LangText("Enable the classified ads:");
                    ClassifiedDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest ad postings:");
                    ClassifiedDeleteDaysLabel.InnerHtml = SepFunctions.LangText("Enter the days till an ad expires:");
                    ClassifiedEmailSubjectLabel.InnerHtml = SepFunctions.LangText("Subject of the email when someone posts a new ad:");
                    ClassifiedEmailBodyLabel.InnerHtml = SepFunctions.LangText("Body of the email message when someone posts a new ad:");
                    ClassifiedBuyLabel.InnerHtml = SepFunctions.LangText("Disable the buy now button:");
                    CNRCEnableLabel.InnerHtml = SepFunctions.LangText("Enable the user comments:");
                    CNRREnableLabel.InnerHtml = SepFunctions.LangText("Enable the user rating system:");
                    RGraphEnableLabel.InnerHtml = SepFunctions.LangText("Show the rating graph when viewing a listing:");
                    ContactEnableLabel.InnerHtml = SepFunctions.LangText("Enable the contact page:");
                    ContactEmailLabel.InnerHtml = SepFunctions.LangText("Email address to send the contact form. (Leave blank to use the admin email address):");
                    ContactEmailSubjectLabel.InnerHtml = SepFunctions.LangText("Subject of the message that is sent to the user when they submit your contact form:");
                    ContactEmailBodyLabel.InnerHtml = SepFunctions.LangText("Body of the message that is sent to the user when they submit your form:");
                    ContactStreetAddressLabel.InnerHtml = SepFunctions.LangText("Ask users for their street address:");
                    ContactAddressLabel.InnerHtml = SepFunctions.LangText("Ask users for their city, state, and zipcode:");
                    ContactPhoneNumberLabel.InnerHtml = SepFunctions.LangText("Ask users for their phone number:");
                    ContactFaxNumberLabel.InnerHtml = SepFunctions.LangText("Ask users for their fax number:");
                    ContactFileTypesLabel.InnerHtml = SepFunctions.LangText("File type users can upload on the contact form:");
                    ContactMaxFilesLabel.InnerHtml = SepFunctions.LangText("The maximum number of files a user can upload to the contact form:");
                    CREnableLabel.InnerHtml = SepFunctions.LangText("Enable the content rotator:");
                    DiscountsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the discount system:");
                    DiscountsDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the top 10 newest coupons:");
                    ELearningEnableLabel.InnerHtml = SepFunctions.LangText("Enable the E-Learning:");
                    EventsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the event calendar:");
                    EventsSMSRemindersLabel.InnerHtml = SepFunctions.LangText("Allow users to receive SMS reminders for events:");
                    FAQEnableLabel.InnerHtml = SepFunctions.LangText("Enable the frequency asked questions:");
                    FAQNewestLabel.InnerHtml = SepFunctions.LangText("Show 10 newest FAQ's:");
                    LibraryEnableLabel.InnerHtml = SepFunctions.LangText("Enable the Downloads:");
                    LibraryDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest file uploads on your main file library page:");
                    LibraryDisplayPopularLabel.InnerHtml = SepFunctions.LangText("Display the most popular file downloads on your main file library page:");
                    LibraryMaxUploadLabel.InnerHtml = SepFunctions.LangText("Maximum upload size in the Downloads. (MB):");
                    LibraryDownloadLabel.InnerHtml = SepFunctions.LangText("Force users to download audio/video files?:");
                    FormsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the forms module:");
                    FormsSignupLabel.InnerHtml = SepFunctions.LangText("Form for users to fill out when signing up:");
                    ForumsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the forums:");
                    ForumsDeleteDaysLabel.InnerHtml = SepFunctions.LangText("Days till old messages get deleted:");
                    ForumsDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest forum postings on the main forums page:");
                    ForumsUsersEditLabel.InnerHtml = SepFunctions.LangText("Enable your users to edit their posts:");
                    ForumsAttachmentLabel.InnerHtml = SepFunctions.LangText("Allow users to upload an attachment in their topic:");
                    GuestbookEnableLabel.InnerHtml = SepFunctions.LangText("Enable the guestbook:");
                    GuestbookDeleteDaysLabel.InnerHtml = SepFunctions.LangText("Days till old messages get deleted:");
                    HoroscopesEnableLabel.InnerHtml = SepFunctions.LangText("Enable the horoscopes:");
                    HotNotEnableLabel.InnerHtml = SepFunctions.LangText("Enable the hot or not:");
                    HotNotProfilesLabel.InnerHtml = SepFunctions.LangText("Allow pictures from the user profiles:");
                    FacebookAPIKeyLabel.InnerHtml = SepFunctions.LangText("Your Facebook App ID (Leave blank to disable):<br />Get the API Key by signing up <a href=\"https://developers.facebook.com/apps\" target=\"_blank\">here</a>.");
                    GooglereCAPTCHAPublicKeyLabel.InnerHtml = SepFunctions.LangText("Google reCAPTCHA Public Key (Leave blank to disable):<br />Get the API Key by signing up <a href=\"http://www.google.com/recaptcha\" target=\"_blank\">here</a>.");
                    GooglereCAPTCHAPrivateKeyLabel.InnerHtml = SepFunctions.LangText("Google reCAPTCHA Private Key (Leave blank to disable):<br />Get the API Key by signing up <a href=\"http://www.google.com/recaptcha\" target=\"_blank\">here</a>.");
                    AnalyticsIDLabel.InnerHtml = SepFunctions.LangText("Analytics ID (Leave blank to disable):<br />Get the ID by going <a href=\"https://analytics.google.com/analytics/web/#home/\" target=\"_blank\">here</a>. The Analytics ID will be the letters and numbers next to your web site name after you create your site. (ex. XX-55555555-5)");
                    GoogleAnalyticsClientIDLabel.InnerHtml = SepFunctions.LangText("Analytics Client ID (Leave blank to disable):<br />Get the Client ID by going <a href=\"https://console.developers.google.com\" target=\"_blank\">here</a> and clicking \"Enable API\" and select Google Analytics to generate a Client ID.");
                    TwilioAccountSIDLabel.InnerHtml = SepFunctions.LangText("Twilio API Account sID:");
                    TwilioAuthTokenLabel.InnerHtml = SepFunctions.LangText("Twilio API Auth Token:");
                    TwilioPhoneNumberLabel.InnerHtml = SepFunctions.LangText("Twilio phone number to send calls from:");
                    CloudFlareAPILabel.InnerHtml = SepFunctions.LangText("CloudFlare API Key:");
                    CloudFlareEmailLabel.InnerHtml = SepFunctions.LangText("CloudFlare Email Address:");
                    CloudFlareDomainLabel.InnerHtml = SepFunctions.LangText("CloudFlare Domain Name:");
                    LinkedInLabel.InnerHtml = SepFunctions.LangText("LinkedIn API Key:");
                    LinkedInSecretLabel.InnerHtml = SepFunctions.LangText("LinkedIn Secret:");
                    STURLLabel.InnerHtml = SepFunctions.LangText("SmarterTrack URL (ex. http://www.domain.com/support/):");
                    STUserLabel.InnerHtml = SepFunctions.LangText("SmarterTrack Admin User Name:");
                    STPassLabel.InnerHtml = SepFunctions.LangText("SmarterTrack Admin Password:");
                    STKBEnableLabel.InnerHtml = SepFunctions.LangText("Embed the KnowledgeBase on your site?:");
                    STKBNewsEnableLabel.InnerHtml = SepFunctions.LangText("Embed the News feeds on your site?:");
                    STDatabaseLabel.InnerHtml = SepFunctions.LangText("When do you want to add users to the SmarterTrack database?:");
                    SugarCRMURLLabel.InnerHtml = SepFunctions.LangText("SugarCRM URL (ex. http://www.domain.com/support/):");
                    SugarCRMUserLabel.InnerHtml = SepFunctions.LangText("SugarCRM Admin User Name:");
                    SugarCRMPassLabel.InnerHtml = SepFunctions.LangText("SugarCRM Admin Password:");
                    SugarCRMDatabaseLabel.InnerHtml = SepFunctions.LangText("When do you want to add users to the SugarCRM database?:");
                    SuiteCRMURLLabel.InnerHtml = SepFunctions.LangText("SuiteCRM URL (ex. http://www.domain.com/support/):");
                    SuiteCRMUserLabel.InnerHtml = SepFunctions.LangText("SuiteCRM Admin User Name:");
                    SuiteCRMPassLabel.InnerHtml = SepFunctions.LangText("SuiteCRM Admin Password:");
                    SuiteCRMDatabaseLabel.InnerHtml = SepFunctions.LangText("When do you want to add users to the SuiteCRM database?:");
                    WHMCSURLLabel.InnerHtml = SepFunctions.LangText("WHMCS URL (ex. http://www.domain.com/support/):");
                    WHMCSUserLabel.InnerHtml = SepFunctions.LangText("WHMCS Admin User Name:");
                    WHMCSPassLabel.InnerHtml = SepFunctions.LangText("WHMCS Admin Password:");
                    WHMCSKBLabel.InnerHtml = SepFunctions.LangText("Embed the KnowledgeBase on your site?:");
                    WHMCSDatabaseLabel.InnerHtml = SepFunctions.LangText("When do you want to add users to the WHMCS database?:");
                    INewsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the international news:");
                    LinksEnableLabel.InnerHtml = SepFunctions.LangText("Enable the links page:");
                    LinksDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest links on your links page:");
                    MatchEnableLabel.InnerHtml = SepFunctions.LangText("Enable the match maker:");
                    MatchPicNoLabel.InnerHtml = SepFunctions.LangText("Number of pictures a user can upload:");
                    MessengerEnableLabel.InnerHtml = SepFunctions.LangText("Enable the messenger:");
                    MessengerDeleteDaysLabel.InnerHtml = SepFunctions.LangText("Days till old messages gets deleted:");
                    MessengerSMSLabel.InnerHtml = SepFunctions.LangText("Allow users to receive an SMS message when they receive a new message:");
                    NewsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the news:");
                    NewsDeleteDaysLabel.InnerHtml = SepFunctions.LangText("Days till old news gets deleted:");
                    NewsLetEnableLabel.InnerHtml = SepFunctions.LangText("Enable the newsletters:");
                    NewsletRemoveTextLabel.InnerHtml = SepFunctions.LangText("Removal text to display on the bottom of your newsletters:");
                    NewsletFromEmailLabel.InnerHtml = SepFunctions.LangText("Email to send newsletters from. (One email per a line):");
                    NewsletNightlyEnableLabel.InnerHtml = SepFunctions.LangText("Enable having a nightly newsletter:");
                    NewsletNightlyNewsLabel.InnerHtml = SepFunctions.LangText("Select a newsletter to send to:");
                    NewsletNightlySubjectLabel.InnerHtml = SepFunctions.LangText("Subject of the email:");
                    NewsletNightlyBodyLabel.InnerHtml = SepFunctions.LangText("Body of the email:");
                    GamesEnableLabel.InnerHtml = SepFunctions.LangText("Enable the online games:");
                    PhotosEnableLabel.InnerHtml = SepFunctions.LangText("Enable the photo albums:");
                    PhotoNumberLabel.InnerHtml = SepFunctions.LangText("Number of photos a user to upload per an album:");
                    PollsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the polls:");
                    PortalsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the portals:");
                    PortalMasterDomainLabel.InnerHtml = SepFunctions.LangText("Master site domain name. (EX: domain.com):");
                    PortalProfilesLabel.InnerHtml = SepFunctions.LangText("Share user profiles among all portals:");
                    PortalSiteLooksLabel.InnerHtml = SepFunctions.LangText("Enable the Site Looks to portal administrators:");
                    RStateEnableLabel.InnerHtml = SepFunctions.LangText("Enable the real estate module:");
                    RStateDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest properties:");
                    RStateNewestRentLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest properties for rent:");
                    RStateNewestSaleLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest properties for sale:");
                    RStateStateDropLabel.InnerHtml = SepFunctions.LangText("Show the State/Province dropdown:");
                    RStateCountryDropLabel.InnerHtml = SepFunctions.LangText("Show the Country dropdown:");
                    ReferEnableLabel.InnerHtml = SepFunctions.LangText("Enable the refer a friend module:");
                    ReferDisplayStatsLabel.InnerHtml = SepFunctions.LangText("Enable the referral statistics:");
                    ReferTopLabel.InnerHtml = SepFunctions.LangText("Show the refer a friend on the top menu for each module:");
                    ReferEmailSubjectLabel.InnerHtml = SepFunctions.LangText("Subject of the email a user will see when they have been referred to your site:");
                    ReferEmailBodyLabel.InnerHtml = SepFunctions.LangText("Body of the email message that a user will see when they have been referred to your site:");
                    SearchModulesLabel.InnerHtml = SepFunctions.LangText("Allow searching through individual modules:");
                    SearchRadiusLabel.InnerHtml = SepFunctions.LangText("Enable the radius searching:");
                    TaxCalcEnableLabel.InnerHtml = SepFunctions.LangText("Enable the tax calculator:");
                    OrderFinalTextLabel.InnerHtml = SepFunctions.LangText("Text in the shopping cart when an order is being processed. (HTML Supported):");
                    EmailTempAdminLabel.InnerHtml = SepFunctions.LangText("Email Template to send the administrator when an order has been successfully processed:");
                    EmailTempCustLabel.InnerHtml = SepFunctions.LangText("Email Template to send the customer when an order has been successfully processed:");
                    ShopMallEnableLabel.InnerHtml = SepFunctions.LangText("Enable the shopping mall:");
                    ShopMallWishListLabel.InnerHtml = SepFunctions.LangText("Enable the wish list:");
                    ShopMallSalesPageLabel.InnerHtml = SepFunctions.LangText("Enable current sales page:");
                    ShopMallElectronicLabel.InnerHtml = SepFunctions.LangText("Electronic email header:");
                    ShopMallDisplayNewestLabel.InnerHtml = SepFunctions.LangText("Display the 10 newest products on your main shopping mall page:");
                    StartupClassLabel.InnerHtml = SepFunctions.LangText("Class to start off new users in:");
                    EmailAdminNewLabel.InnerHtml = SepFunctions.LangText("Email address to send new signups to. (Leave blank to disable):");
                    SignupVerifyLabel.InnerHtml = SepFunctions.LangText("Send users an email to verify their account:");
                    SignupAgreementLabel.InnerHtml = SepFunctions.LangText("Agreement before signup. (Leave blank to disable):");
                    SignupAdminAppLabel.InnerHtml = SepFunctions.LangText("The administrator has to approve accounts:");
                    SignupAgeLabel.InnerHtml = SepFunctions.LangText("Age a new member must be to signup:");
                    LoginEmailLabel.InnerHtml = SepFunctions.LangText("Have users login with their email address instead of their username:");
                    SignupAREmailLabel.InnerHtml = SepFunctions.LangText("Email address that the auto respond email comes from:");
                    SignupARSubjectLabel.InnerHtml = SepFunctions.LangText("Auto respond email subject when someone signs up:");
                    SignupARBodyLabel.InnerHtml = SepFunctions.LangText("Auto respond email body when someone signs up:");
                    SignupPageLabel.InnerHtml = SepFunctions.LangText("Page to return user to after a user signs up successfully:");
                    AutoUserLabel.InnerHtml = SepFunctions.LangText("Auto-Generated Suffix. (Leave blank to allow users to create their own username):");
                    SignupSecTitleLabel.InnerHtml = SepFunctions.LangText("Signup section title (Text shows within the signup form above all the fields):");
                    SignupSecDescLabel.InnerHtml = SepFunctions.LangText("Signup section description (Text shows within the signup form above all the fields):");
                    SignupSelMemLabel.InnerHtml = SepFunctions.LangText("Only show free memberships on the signup form:");
                    SpeakerEnableLabel.InnerHtml = SepFunctions.LangText("Enable the speakers bureau:");
                    StocksSymbolsLabel.InnerHtml = SepFunctions.LangText("Symbols to display. (Separate the symbols by commas):");
                    UPagesEnableLabel.InnerHtml = SepFunctions.LangText("Enable the user pages:");
                    UPagesSignupProcessLabel.InnerHtml = SepFunctions.LangText("Website Signup Process:");
                    UPagesTop10Label.InnerHtml = SepFunctions.LangText("Have the module top 10 lists show only the items the user has posted that created the user page:");
                    ProfilesEnableLabel.InnerHtml = SepFunctions.LangText("Enable the user profiles:");
                    ProfilesAskSignupLabel.InnerHtml = SepFunctions.LangText("Ask users when they signup to fill out their profile:");
                    ProfilesPicNoLabel.InnerHtml = SepFunctions.LangText("Number of pictures a user can upload:");
                    ProfileRequiredLabel.InnerHtml = SepFunctions.LangText("Require profile after signup:");
                    ProfilesType1Label.InnerHtml = SepFunctions.LangText("Profile Type 1. (Leave blank to disable):");
                    ProfilesType2Label.InnerHtml = SepFunctions.LangText("Profile Type 2. (Leave blank to disable):");
                    ProfilesType3Label.InnerHtml = SepFunctions.LangText("Profile Type 3. (Leave blank to disable):");
                    ProfilesColorLabel.InnerHtml = SepFunctions.LangText("Allow users to customize their profile colors:");
                    ProfilesAudioLabel.InnerHtml = SepFunctions.LangText("Maximum number of audio files a user can upload to their profile:");
                    WeatherEnableLabel.InnerHtml = SepFunctions.LangText("Enable the weather forecast:");
                    FeedsEnableLabel.InnerHtml = SepFunctions.LangText("Enable the User Feeds.:");
                    VoucherEnableLabel.InnerHtml = SepFunctions.LangText("Enable the vouchers.:");
                    VoucherDaysAddLabel.InnerHtml = SepFunctions.LangText("Numbers of days to add to the valid from date when a user purchases a voucher.:");
                    VoucherExpireDaysLabel.InnerHtml = SepFunctions.LangText("Number of days until a voucher expires:");
                    VoucherAgreementLabel.InnerHtml = SepFunctions.LangText("Voucher post agreement:");
                    VoucherAgreementBuyLabel.InnerHtml = SepFunctions.LangText("Voucher buy agreement:");
                    VoucherTop10Label.InnerHtml = SepFunctions.LangText("Display the top 10 newest vouchers:");
                    PCREnableLabel.InnerHtml = SepFunctions.LangText("Enable PCRecruiter:");
                    PCRAPIKeyLabel.InnerHtml = SepFunctions.LangText("PCRecruiter API Key:");
                    PCRAppIDLabel.InnerHtml = SepFunctions.LangText("PCRecruiter App ID:");
                    PCRAPIURLLabel.InnerHtml = SepFunctions.LangText("PCRecruiter API URL (Ex. https://www.pcrecruiter.net/rest/api/):");
                    PCRDatabaseIdLabel.InnerHtml = SepFunctions.LangText("PCRecruiter Database ID:");
                    PCRUserNameLabel.InnerHtml = SepFunctions.LangText("PCRecruiter Database User Name:");
                    PCRPasswordLabel.InnerHtml = SepFunctions.LangText("PCRecruiter Database Password:");
                    PCRExpireDaysLabel.InnerHtml = SepFunctions.LangText("Days until a job expires:");
                    Menu1TextLabel.InnerHtml = SepFunctions.LangText("Menu 1 Text:");
                    Menu2TextLabel.InnerHtml = SepFunctions.LangText("Menu 2 Text:");
                    Menu3TextLabel.InnerHtml = SepFunctions.LangText("Menu 3 Text:");
                    Menu4TextLabel.InnerHtml = SepFunctions.LangText("Menu 4 Text:");
                    Menu5TextLabel.InnerHtml = SepFunctions.LangText("Menu 5 Text:");
                    Menu6TextLabel.InnerHtml = SepFunctions.LangText("Menu 6 Text:");
                    Menu7TextLabel.InnerHtml = SepFunctions.LangText("Menu 7 Text:");
                    Menu1SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 1 on the sitemap:");
                    Menu2SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 2 on the sitemap:");
                    Menu3SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 3 on the sitemap:");
                    Menu4SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 4 on the sitemap:");
                    Menu5SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 5 on the sitemap:");
                    Menu6SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 6 on the sitemap:");
                    Menu7SitemapLabel.InnerHtml = SepFunctions.LangText("Show Menu 7 on the sitemap:");
                    SiteLogoLabel.InnerHtml = SepFunctions.LangText("Website Logo:");
                    RSSTopLabel.InnerHtml = SepFunctions.LangText("Enable the RSS Feeds button:");
                    FavoritesTopLabel.InnerHtml = SepFunctions.LangText("Enable the Favorites button:");
                    MainPageTopLabel.InnerHtml = SepFunctions.LangText("Enable the Main Page button:");
                    SocialSharingLabel.InnerHtml = SepFunctions.LangText("Enable the social network sharing:");
                    WebSiteNameLabel.InnerHtml = SepFunctions.LangText("Name of your website:");
                    CurrencyCodeLabel.InnerHtml = SepFunctions.LangText("Currency Code:");
                    BadWordFilterLabel.InnerHtml = SepFunctions.LangText("Bad words to filter out of your website. (Seperated by Comma's):");
                    RecPerAPageLabel.InnerHtml = SepFunctions.LangText("Records to show per a page:");
                    MaxImageSizeLabel.InnerHtml = SepFunctions.LangText("Maximum image size that can be uploaded. (KB):");
                    SiteLangLabel.InnerHtml = SepFunctions.LangText("Select your primary language:");
                    CatCountLabel.InnerHtml = SepFunctions.LangText("Show content count when displaying categories:");
                    TimeOffsetLabel.InnerHtml = SepFunctions.LangText("Time offset in hours:");
                    CatLowestLvlLabel.InnerHtml = SepFunctions.LangText("Require the lowest level of category to be selected:");
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
        /// Handles the Click event of the LDAP_Sync control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void LDAP_Sync_Click(object sender, EventArgs e)
        {
            using (var LD = new LDAP())
            {
                LD.First_Time_Install();
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSetup")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSetup"), false) == false)
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

            if (SepFunctions.ModuleActivated(68))
            {
                AffiliateSignupRow.Visible = false;
                AffiliateIDReqRow.Visible = false;
                FormsSignupRow.Visible = false;
                STDatabase.Items[1].Enabled = false;
                SugarCRMDatabase.Items[1].Enabled = false;
                SuiteCRMDatabase.Items[1].Enabled = false;
                WHMCSDatabase.Items[1].Enabled = false;
                Module997.Visible = false;
                UPagesSignupProcessRow.Visible = false;
                ProfilesAskSignupRow.Visible = false;
                ProfileRequiredRow.Visible = false;
                ShowCreditsSignupRow.Visible = false;
                Module994.Visible = false;
                LoginEmailRow.Visible = false;
            }

            if (SepFunctions.isProfessionalEdition() == false)
            {
                CloudFlareAPIRow.Visible = false;
                CloudFlareDomainRow.Visible = false;
                CloudFlareEmailRow.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                ModuleID.Value = SepCommon.SepCore.Request.Item("ModuleID");

                if (SepFunctions.ModuleActivated(63) == false)
                {
                    ProfileTabRow.Visible = false;
                    ArticleShowPicRow.Visible = false;
                    HotNotProfilesRow.Visible = false;
                    PortalProfilesRow.Visible = false;
                }
                else
                {
                    PPDefaultPage.Items.Add(new ListItem("Profile", "Profile"));
                }

                if (SepFunctions.ModuleActivated(61) == false) BlogTabRow.Visible = false;
                else PPDefaultPage.Items.Add(new ListItem("Blogs", "Blogs"));

                if (SepFunctions.ModuleActivated(12) == false) ForumsTabRow.Visible = false;
                else PPDefaultPage.Items.Add(new ListItem("Forums", "Forums"));

                if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAccountSID")) && string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "TwilioAuthToken")))
                {
                    EventsSMSRow.Visible = false;
                    MessengerSMSRow.Visible = false;
                }

                try
                {
                    SepFunctions.Populate_Countries(CompanyCountry);
                    SepFunctions.Populate_Access_Classes(StartupClass);
                    SepFunctions.Populate_Forms(FormsSignup);
                    SepFunctions.Populate_Web_Pages(SignupPage);

                    // Populate Login Page
                    SepFunctions.Populate_Web_Pages(LoginPage);
                    SepFunctions.Populate_Forms(LoginPage);

                    // End Populate Login Page
                    SepFunctions.Populate_Web_Pages2(AffiliateReturnPage);
                    SepFunctions.Populate_Email_Templates(EmailTempAdmin);
                    SepFunctions.Populate_Email_Templates(EmailTempCust);

                    // Populate nightly newsletters
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT LetterID,NewsletName FROM Newsletters WHERE Status <> -1 ORDER BY NewsletName", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                    while (RS.Read())
                                        NewsletNightlyNews.Items.Add(new ListItem(SepFunctions.openNull(RS["NewsletName"]), SepFunctions.openNull(RS["LetterID"])));
                            }
                        }
                    }

                    // End Populate nightly newsletters
                    Populate_Settings();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SetupSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SetupSave_Click(object sender, EventArgs e)
        {
            var strXml = string.Empty;
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            if (SepFunctions.Setup(997, "LoginEmail") != LoginEmail.Value)
            {
                if (LoginEmail.Value == "Yes")
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", SepFunctions.GetUserInformation("EmailAddress"));
                else
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", SepFunctions.GetUserInformation("UserName"));

                Response.Cookies["UserInfo"].Value = string.Empty;
            }

            if (sSiteName != WebSiteName.Value)
            {
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(WebSiteName.Value, " ", string.Empty), 5) + "Username", SepFunctions.Session_User_Name());
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(WebSiteName.Value, " ", string.Empty), 5) + "UserID", SepFunctions.Session_User_ID());
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(WebSiteName.Value, " ", string.Empty), 5) + "Password", SepFunctions.Session_Password());
            }

            strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            strXml += "<ROOTLEVEL>" + Environment.NewLine;

            strXml += "<MODULE33>" + Environment.NewLine;
            strXml += "<LoginPage>" + SepFunctions.HTMLEncode(LoginPage.Value) + "</LoginPage>" + Environment.NewLine;
            strXml += "<PPShowMemberships>" + SepFunctions.HTMLEncode(PPShowMemberships.Value) + "</PPShowMemberships>" + Environment.NewLine;
            strXml += "<PPShowOrders>" + SepFunctions.HTMLEncode(PPShowOrders.Value) + "</PPShowOrders>" + Environment.NewLine;
            strXml += "<PPShowAdStats>" + SepFunctions.HTMLEncode(PPShowAdStats.Value) + "</PPShowAdStats>" + Environment.NewLine;
            strXml += "<PPShowFriends>" + SepFunctions.HTMLEncode(PPShowFriends.Value) + "</PPShowFriends>" + Environment.NewLine;
            strXml += "<PPShowFavorites>" + SepFunctions.HTMLEncode(PPShowFavorites.Value) + "</PPShowFavorites>" + Environment.NewLine;
            strXml += "<PPShowAffiliate>" + SepFunctions.HTMLEncode(PPShowAffiliate.Value) + "</PPShowAffiliate>" + Environment.NewLine;
            strXml += "<PPShowMessenger>" + SepFunctions.HTMLEncode(PPShowMessenger.Value) + "</PPShowMessenger>" + Environment.NewLine;
            strXml += "<PPShowProfile>" + SepFunctions.HTMLEncode(PPShowProfile.Value) + "</PPShowProfile>" + Environment.NewLine;
            strXml += "<PPShowBlogs>" + SepFunctions.HTMLEncode(PPShowBlogs.Value) + "</PPShowBlogs>" + Environment.NewLine;
            strXml += "<PPShowForums>" + SepFunctions.HTMLEncode(PPShowForums.Value) + "</PPShowForums>" + Environment.NewLine;
            strXml += "<PPShowMain>" + SepFunctions.HTMLEncode(PPShowMain.Value) + "</PPShowMain>" + Environment.NewLine;
            strXml += "<PPDefaultPage>" + SepFunctions.HTMLEncode(PPDefaultPage.Value) + "</PPDefaultPage>" + Environment.NewLine;
            strXml += "<FriendsEnable>" + SepFunctions.HTMLEncode(FriendsEnable.Value) + "</FriendsEnable>" + Environment.NewLine;
            strXml += "<ShowPoints>" + SepFunctions.HTMLEncode(ShowCredits.Value) + "</ShowPoints>" + Environment.NewLine;
            strXml += "<ShowCreditsSignup>" + SepFunctions.HTMLEncode(ShowCreditsSignup.Value) + "</ShowCreditsSignup>" + Environment.NewLine;
            strXml += "</MODULE33>" + Environment.NewLine;

            strXml += "<MODULE994>" + Environment.NewLine;
            strXml += "<AskGender>" + SepFunctions.HTMLEncode(AskGender.Value) + "</AskGender>" + Environment.NewLine;
            strXml += "<AskPhoneNumber>" + SepFunctions.HTMLEncode(AskPhoneNumber.Value) + "</AskPhoneNumber>" + Environment.NewLine;
            strXml += "<ReqPhoneNumber>" + SepFunctions.HTMLEncode(ReqPhoneNumber.Value) + "</ReqPhoneNumber>" + Environment.NewLine;
            strXml += "<AskStreetAddress>" + SepFunctions.HTMLEncode(AskStreetAddress.Value) + "</AskStreetAddress>" + Environment.NewLine;
            strXml += "<AskCity>" + SepFunctions.HTMLEncode(AskCity.Value) + "</AskCity>" + Environment.NewLine;
            strXml += "<AskState>" + SepFunctions.HTMLEncode(AskState.Value) + "</AskState>" + Environment.NewLine;
            strXml += "<AskZipCode>" + SepFunctions.HTMLEncode(AskZipCode.Value) + "</AskZipCode>" + Environment.NewLine;
            strXml += "<AskCountry>" + SepFunctions.HTMLEncode(AskCountry.Value) + "</AskCountry>" + Environment.NewLine;
            strXml += "<ReqAddress>" + SepFunctions.HTMLEncode(ReqAddress.Value) + "</ReqAddress>" + Environment.NewLine;
            strXml += "<AskBirthDate>" + SepFunctions.HTMLEncode(AskBirthDate.Value) + "</AskBirthDate>" + Environment.NewLine;
            strXml += "<AskPayPal>" + SepFunctions.HTMLEncode(AskPayPal.Value) + "</AskPayPal>" + Environment.NewLine;
            strXml += "<ReqPayPal>" + SepFunctions.HTMLEncode(ReqPayPal.Value) + "</ReqPayPal>" + Environment.NewLine;
            strXml += "<PostHide>" + SepFunctions.HTMLEncode(PostHide.Value) + "</PostHide>" + Environment.NewLine;
            strXml += "<AskFriends>" + SepFunctions.HTMLEncode(AskFriends.Value) + "</AskFriends>" + Environment.NewLine;
            strXml += "</MODULE994>" + Environment.NewLine;

            strXml += "<MODULE991>" + Environment.NewLine;
            strXml += "<AdminFullName>" + SepFunctions.HTMLEncode(AdminFullName.Value) + "</AdminFullName>" + Environment.NewLine;
            strXml += "<AdminEmailAddress>" + SepFunctions.HTMLEncode(AdminEmailAddress.Value) + "</AdminEmailAddress>" + Environment.NewLine;
            strXml += "<CompanyName>" + SepFunctions.HTMLEncode(CompanyName.Value) + "</CompanyName>" + Environment.NewLine;
            strXml += "<CompanySlogan>" + SepFunctions.HTMLEncode(CompanySlogan.Value) + "</CompanySlogan>" + Environment.NewLine;
            strXml += "<CompanyPhone>" + SepFunctions.HTMLEncode(CompanyPhone.Value) + "</CompanyPhone>" + Environment.NewLine;
            strXml += "<CompanyAddressLine1>" + SepFunctions.HTMLEncode(CompanyAddressLine1.Value) + "</CompanyAddressLine1>" + Environment.NewLine;
            strXml += "<CompanyCity>" + SepFunctions.HTMLEncode(CompanyCity.Value) + "</CompanyCity>" + Environment.NewLine;
            strXml += "<CompanyState>" + SepFunctions.HTMLEncode(CompanyState.Value) + "</CompanyState>" + Environment.NewLine;
            strXml += "<CompanyZipCode>" + SepFunctions.HTMLEncode(CompanyZipCode.Value) + "</CompanyZipCode>" + Environment.NewLine;
            strXml += "<CompanyCountry>" + SepFunctions.HTMLEncode(CompanyCountry.Value) + "</CompanyCountry>" + Environment.NewLine;
            strXml += "<TwitterUsername>" + SepFunctions.HTMLEncode(TwitterUsername.Value) + "</TwitterUsername>" + Environment.NewLine;
            strXml += "</MODULE991>" + Environment.NewLine;

            strXml += "<MODULE64>" + Environment.NewLine;
            strXml += "<ConferenceEnable>" + SepFunctions.HTMLEncode(ConferenceEnable.Value) + "</ConferenceEnable>" + Environment.NewLine;
            strXml += "<ModeratorClass>" + SepFunctions.HTMLEncode(ModeratorClass.Text) + "</ModeratorClass>" + Environment.NewLine;
            strXml += "<TwilioSMSReminderMsg>" + SepFunctions.HTMLEncode(TwilioSMSReminderMsg.Value) + "</TwilioSMSReminderMsg>" + Environment.NewLine;
            strXml += "<TwilioSMSReminderOffset>" + SepFunctions.HTMLEncode(TwilioSMSReminderOffset.Value) + "</TwilioSMSReminderOffset>" + Environment.NewLine;
            strXml += "<PercentCelebrities>" + SepFunctions.HTMLEncode(PercentCelebrities.Value) + "</PercentCelebrities>" + Environment.NewLine;
            strXml += "</MODULE64>" + Environment.NewLine;
            Disable_Module(64, ConferenceEnable.Value);

            strXml += "<MODULE2>" + Environment.NewLine;
            strXml += "<AdsEnable>" + SepFunctions.HTMLEncode(AdsEnable.Value) + "</AdsEnable>" + Environment.NewLine;
            strXml += "<AdsStatsEnable>" + SepFunctions.HTMLEncode(AdsStatsEnable.Value) + "</AdsStatsEnable>" + Environment.NewLine;
            strXml += "<SponsorsTarget>" + SepFunctions.HTMLEncode(SponsorsTarget.Value) + "</SponsorsTarget>" + Environment.NewLine;
            strXml += "</MODULE2>" + Environment.NewLine;
            Disable_Module(2, AdsEnable.Value);

            strXml += "<MODULE39>" + Environment.NewLine;
            strXml += "<AffiliateEnable>" + SepFunctions.HTMLEncode(AffiliateEnable.Value) + "</AffiliateEnable>" + Environment.NewLine;
            strXml += "<AffiliateLVL1>" + SepFunctions.HTMLEncode(AffiliateLVL1.Value) + "</AffiliateLVL1>" + Environment.NewLine;
            strXml += "<AffiliateLVL2>" + SepFunctions.HTMLEncode(AffiliateLVL2.Value) + "</AffiliateLVL2>" + Environment.NewLine;
            strXml += "<AffiliateSignup>" + SepFunctions.HTMLEncode(AffiliateSignup.Value) + "</AffiliateSignup>" + Environment.NewLine;
            strXml += "<AffiliateIDReq>" + SepFunctions.HTMLEncode(AffiliateIDReq.Value) + "</AffiliateIDReq>" + Environment.NewLine;
            strXml += "<AffiliateImageText>" + SepFunctions.HTMLEncode(AffiliateImageText.Value) + "</AffiliateImageText>" + Environment.NewLine;
            strXml += "<AffiliateReturnPage>" + SepFunctions.HTMLEncode(AffiliateReturnPage.Value) + "</AffiliateReturnPage>" + Environment.NewLine;
            strXml += "</MODULE39>" + Environment.NewLine;
            Disable_Module(39, AffiliateEnable.Value);

            strXml += "<MODULE35>" + Environment.NewLine;
            strXml += "<ArticlesEnable>" + SepFunctions.HTMLEncode(ArticlesEnable.Value) + "</ArticlesEnable>" + Environment.NewLine;
            strXml += "<Articles10Newest>" + SepFunctions.HTMLEncode(Articles10Newest.Value) + "</Articles10Newest>" + Environment.NewLine;
            strXml += "<ArticleShowPic>" + SepFunctions.HTMLEncode(ArticleShowPic.Value) + "</ArticleShowPic>" + Environment.NewLine;
            strXml += "<ArticleShowSource>" + SepFunctions.HTMLEncode(ArticleShowSource.Value) + "</ArticleShowSource>" + Environment.NewLine;
            strXml += "<ArticleShowMeta>" + SepFunctions.HTMLEncode(ArticleShowMeta.Value) + "</ArticleShowMeta>" + Environment.NewLine;
            strXml += "</MODULE35>" + Environment.NewLine;
            Disable_Module(35, ArticlesEnable.Value);

            strXml += "<MODULE31>" + Environment.NewLine;
            strXml += "<AuctionEnable>" + SepFunctions.HTMLEncode(AuctionEnable.Value) + "</AuctionEnable>" + Environment.NewLine;
            strXml += "<AuctionDisplayNewest>" + SepFunctions.HTMLEncode(AuctionDisplayNewest.Value) + "</AuctionDisplayNewest>" + Environment.NewLine;
            strXml += "<AuctionDeleteDays>" + SepFunctions.HTMLEncode(AuctionDeleteDays.Value) + "</AuctionDeleteDays>" + Environment.NewLine;
            strXml += "<AuctionEmailSubject>" + SepFunctions.HTMLEncode(AuctionEmailSubject.Value) + "</AuctionEmailSubject>" + Environment.NewLine;
            strXml += "<AuctionEmailBody>" + SepFunctions.HTMLEncode(AuctionEmailBody.Value) + "</AuctionEmailBody>" + Environment.NewLine;
            strXml += "</MODULE31>" + Environment.NewLine;
            Disable_Module(31, AuctionEnable.Value);

            strXml += "<MODULE61>" + Environment.NewLine;
            strXml += "<BlogsEnable>" + SepFunctions.HTMLEncode(BlogsEnable.Value) + "</BlogsEnable>" + Environment.NewLine;
            strXml += "</MODULE61>" + Environment.NewLine;
            Disable_Module(61, BlogsEnable.Value);

            strXml += "<MODULE20>" + Environment.NewLine;
            strXml += "<BusinessEnable>" + SepFunctions.HTMLEncode(BusinessEnable.Value) + "</BusinessEnable>" + Environment.NewLine;
            strXml += "<BusinessDisplayNewest>" + SepFunctions.HTMLEncode(BusinessDisplayNewest.Value) + "</BusinessDisplayNewest>" + Environment.NewLine;
            strXml += "<BusinessClaim>" + SepFunctions.HTMLEncode(BusinessClaim.Value) + "</BusinessClaim>" + Environment.NewLine;
            strXml += "<BusinessUserAddress>" + SepFunctions.HTMLEncode(BusinessUserAddress.Value) + "</BusinessUserAddress>" + Environment.NewLine;
            strXml += "</MODULE20>" + Environment.NewLine;
            Disable_Module(20, BusinessEnable.Value);

            strXml += "<MODULE42>" + Environment.NewLine;
            strXml += "<ChatEnable>" + SepFunctions.HTMLEncode(ChatEnable.Value) + "</ChatEnable>" + Environment.NewLine;
            strXml += "</MODULE42>" + Environment.NewLine;
            Disable_Module(42, ChatEnable.Value);

            strXml += "<MODULE6>" + Environment.NewLine;
            strXml += "<IMessengerEnable>" + SepFunctions.HTMLEncode(IMessengerEnable.Value) + "</IMessengerEnable>" + Environment.NewLine;
            strXml += "</MODULE6>" + Environment.NewLine;
            Disable_Module(6, IMessengerEnable.Value);

            strXml += "<MODULE44>" + Environment.NewLine;
            strXml += "<ClassifiedEnable>" + SepFunctions.HTMLEncode(ClassifiedEnable.Value) + "</ClassifiedEnable>" + Environment.NewLine;
            strXml += "<ClassifiedDisplayNewest>" + SepFunctions.HTMLEncode(ClassifiedDisplayNewest.Value) + "</ClassifiedDisplayNewest>" + Environment.NewLine;
            strXml += "<ClassifiedDeleteDays>" + SepFunctions.HTMLEncode(ClassifiedDeleteDays.Value) + "</ClassifiedDeleteDays>" + Environment.NewLine;
            strXml += "<ClassifiedEmailSubject>" + SepFunctions.HTMLEncode(ClassifiedEmailSubject.Value) + "</ClassifiedEmailSubject>" + Environment.NewLine;
            strXml += "<ClassifiedEmailBody>" + SepFunctions.HTMLEncode(ClassifiedEmailBody.Value) + "</ClassifiedEmailBody>" + Environment.NewLine;
            strXml += "<ClassifiedBuy>" + SepFunctions.HTMLEncode(ClassifiedBuy.Value) + "</ClassifiedBuy>" + Environment.NewLine;
            strXml += "</MODULE44>" + Environment.NewLine;
            Disable_Module(44, ClassifiedEnable.Value);

            strXml += "<MODULE8>" + Environment.NewLine;
            strXml += "<CNRCEnable>" + SepFunctions.HTMLEncode(CNRCEnable.Value) + "</CNRCEnable>" + Environment.NewLine;
            strXml += "<CNRREnable>" + SepFunctions.HTMLEncode(CNRREnable.Value) + "</CNRREnable>" + Environment.NewLine;
            strXml += "<RGraphEnable>" + SepFunctions.HTMLEncode(RGraphEnable.Value) + "</RGraphEnable>" + Environment.NewLine;
            strXml += "</MODULE8>" + Environment.NewLine;

            strXml += "<MODULE4>" + Environment.NewLine;
            strXml += "<ContactEnable>" + SepFunctions.HTMLEncode(ContactEnable.Value) + "</ContactEnable>" + Environment.NewLine;
            strXml += "<ContactEmail>" + SepFunctions.HTMLEncode(ContactEmail.Value) + "</ContactEmail>" + Environment.NewLine;
            strXml += "<ContactEmailSubject>" + SepFunctions.HTMLEncode(ContactEmailSubject.Value) + "</ContactEmailSubject>" + Environment.NewLine;
            strXml += "<ContactEmailBody>" + SepFunctions.HTMLEncode(ContactEmailBody.Value) + "</ContactEmailBody>" + Environment.NewLine;
            strXml += "<ContactStreetAddress>" + SepFunctions.HTMLEncode(ContactStreetAddress.Value) + "</ContactStreetAddress>" + Environment.NewLine;
            strXml += "<ContactAddress>" + SepFunctions.HTMLEncode(ContactAddress.Value) + "</ContactAddress>" + Environment.NewLine;
            strXml += "<ContactPhoneNumber>" + SepFunctions.HTMLEncode(ContactPhoneNumber.Value) + "</ContactPhoneNumber>" + Environment.NewLine;
            strXml += "<ContactFaxNumber>" + SepFunctions.HTMLEncode(ContactFaxNumber.Value) + "</ContactFaxNumber>" + Environment.NewLine;
            strXml += "<ContactFileTypes>" + SepFunctions.HTMLEncode(ContactFileTypes.Value) + "</ContactFileTypes>" + Environment.NewLine;
            strXml += "<ContactMaxFiles>" + SepFunctions.HTMLEncode(ContactMaxFiles.Value) + "</ContactMaxFiles>" + Environment.NewLine;
            strXml += "</MODULE4>" + Environment.NewLine;
            Disable_Module(4, ContactEnable.Value);

            strXml += "<MODULE1>" + Environment.NewLine;
            strXml += "<CREnable>" + SepFunctions.HTMLEncode(CREnable.Value) + "</CREnable>" + Environment.NewLine;
            strXml += "</MODULE1>" + Environment.NewLine;

            strXml += "<MODULE5>" + Environment.NewLine;
            strXml += "<DiscountsEnable>" + SepFunctions.HTMLEncode(DiscountsEnable.Value) + "</DiscountsEnable>" + Environment.NewLine;
            strXml += "<DiscountsDisplayNewest>" + SepFunctions.HTMLEncode(DiscountsDisplayNewest.Value) + "</DiscountsDisplayNewest>" + Environment.NewLine;
            strXml += "</MODULE5>" + Environment.NewLine;
            Disable_Module(5, DiscountsEnable.Value);

            strXml += "<MODULE37>" + Environment.NewLine;
            strXml += "<ELearningEnable>" + SepFunctions.HTMLEncode(ELearningEnable.Value) + "</ELearningEnable>" + Environment.NewLine;
            strXml += "<ELearningNewest>" + SepFunctions.HTMLEncode(ELearningNewest.Value) + "</ELearningNewest>" + Environment.NewLine;
            strXml += "</MODULE37>" + Environment.NewLine;
            Disable_Module(37, ELearningEnable.Value);

            strXml += "<MODULE46>" + Environment.NewLine;
            strXml += "<EventsEnable>" + SepFunctions.HTMLEncode(EventsEnable.Value) + "</EventsEnable>" + Environment.NewLine;
            strXml += "<EventsSMSReminders>" + SepFunctions.HTMLEncode(EventsSMSReminders.Value) + "</EventsSMSReminders>" + Environment.NewLine;
            strXml += "</MODULE46>" + Environment.NewLine;
            Disable_Module(46, EventsEnable.Value);

            strXml += "<MODULE9>" + Environment.NewLine;
            strXml += "<FAQEnable>" + SepFunctions.HTMLEncode(FAQEnable.Value) + "</FAQEnable>" + Environment.NewLine;
            strXml += "<FAQNewest>" + SepFunctions.HTMLEncode(FAQNewest.Value) + "</FAQNewest>" + Environment.NewLine;
            strXml += "</MODULE9>" + Environment.NewLine;
            Disable_Module(9, FAQEnable.Value);

            strXml += "<MODULE10>" + Environment.NewLine;
            strXml += "<LibraryEnable>" + SepFunctions.HTMLEncode(LibraryEnable.Value) + "</LibraryEnable>" + Environment.NewLine;
            strXml += "<LibraryDisplayNewest>" + SepFunctions.HTMLEncode(LibraryDisplayNewest.Value) + "</LibraryDisplayNewest>" + Environment.NewLine;
            strXml += "<LibraryDisplayPopular>" + SepFunctions.HTMLEncode(LibraryDisplayPopular.Value) + "</LibraryDisplayPopular>" + Environment.NewLine;
            strXml += "<LibraryMaxUpload>" + SepFunctions.HTMLEncode(SepFunctions.toLong(LibraryMaxUpload.Value) > 200 ? "200" : LibraryMaxUpload.Value) + "</LibraryMaxUpload>" + Environment.NewLine;
            strXml += "<LibraryDownload>" + SepFunctions.HTMLEncode(LibraryDownload.Value) + "</LibraryDownload>" + Environment.NewLine;
            strXml += "</MODULE10>" + Environment.NewLine;
            Disable_Module(10, LibraryEnable.Value);

            strXml += "<MODULE13>" + Environment.NewLine;
            strXml += "<FormsEnable>" + SepFunctions.HTMLEncode(FormsEnable.Value) + "</FormsEnable>" + Environment.NewLine;
            strXml += "<FormsSignup>" + SepFunctions.HTMLEncode(FormsSignup.Value) + "</FormsSignup>" + Environment.NewLine;
            strXml += "</MODULE13>" + Environment.NewLine;
            Disable_Module(13, FormsEnable.Value);

            strXml += "<MODULE12>" + Environment.NewLine;
            strXml += "<ForumsEnable>" + SepFunctions.HTMLEncode(ForumsEnable.Value) + "</ForumsEnable>" + Environment.NewLine;
            strXml += "<ForumsDeleteDays>" + SepFunctions.HTMLEncode(ForumsDeleteDays.Value) + "</ForumsDeleteDays>" + Environment.NewLine;
            strXml += "<ForumsDisplayNewest>" + SepFunctions.HTMLEncode(ForumsDisplayNewest.Value) + "</ForumsDisplayNewest>" + Environment.NewLine;
            strXml += "<ForumsUsersEdit>" + SepFunctions.HTMLEncode(ForumsUsersEdit.Value) + "</ForumsUsersEdit>" + Environment.NewLine;
            strXml += "<ForumsAttachment>" + SepFunctions.HTMLEncode(ForumsAttachment.Value) + "</ForumsAttachment>" + Environment.NewLine;
            strXml += "</MODULE12>" + Environment.NewLine;
            Disable_Module(12, ForumsEnable.Value);

            strXml += "<MODULE14>" + Environment.NewLine;
            strXml += "<GuestbookEnable>" + SepFunctions.HTMLEncode(GuestbookEnable.Value) + "</GuestbookEnable>" + Environment.NewLine;
            strXml += "<GuestbookDeleteDays>" + SepFunctions.HTMLEncode(GuestbookDeleteDays.Value) + "</GuestbookDeleteDays>" + Environment.NewLine;
            strXml += "</MODULE14>" + Environment.NewLine;
            Disable_Module(14, GuestbookEnable.Value);

            strXml += "<MODULE57>" + Environment.NewLine;
            strXml += "<HoroscopesEnable>" + SepFunctions.HTMLEncode(HoroscopesEnable.Value) + "</HoroscopesEnable>" + Environment.NewLine;
            strXml += "</MODULE57>" + Environment.NewLine;
            Disable_Module(57, HoroscopesEnable.Value);

            strXml += "<MODULE40>" + Environment.NewLine;
            strXml += "<HotNotEnable>" + SepFunctions.HTMLEncode(HotNotEnable.Value) + "</HotNotEnable>" + Environment.NewLine;
            strXml += "<HotNotProfiles>" + SepFunctions.HTMLEncode(HotNotProfiles.Value) + "</HotNotProfiles>" + Environment.NewLine;
            strXml += "</MODULE40>" + Environment.NewLine;
            Disable_Module(40, HotNotEnable.Value);

            strXml += "<MODULE56>" + Environment.NewLine;
            strXml += "<INewsEnable>" + SepFunctions.HTMLEncode(INewsEnable.Value) + "</INewsEnable>" + Environment.NewLine;
            strXml += "</MODULE56>" + Environment.NewLine;
            Disable_Module(56, INewsEnable.Value);

            strXml += "<MODULE19>" + Environment.NewLine;
            strXml += "<LinksEnable>" + SepFunctions.HTMLEncode(LinksEnable.Value) + "</LinksEnable>" + Environment.NewLine;
            strXml += "<LinksDisplayNewest>" + SepFunctions.HTMLEncode(LinksDisplayNewest.Value) + "</LinksDisplayNewest>" + Environment.NewLine;
            strXml += "</MODULE19>" + Environment.NewLine;
            Disable_Module(19, LinksEnable.Value);

            strXml += "<MODULE18>" + Environment.NewLine;
            strXml += "<MatchEnable>" + SepFunctions.HTMLEncode(MatchEnable.Value) + "</MatchEnable>" + Environment.NewLine;
            strXml += "<MatchPicNo>" + SepFunctions.HTMLEncode(MatchPicNo.Value) + "</MatchPicNo>" + Environment.NewLine;
            strXml += "</MODULE18>" + Environment.NewLine;
            Disable_Module(18, MatchEnable.Value);

            strXml += "<MODULE17>" + Environment.NewLine;
            strXml += "<MessengerEnable>" + SepFunctions.HTMLEncode(MessengerEnable.Value) + "</MessengerEnable>" + Environment.NewLine;
            strXml += "<MessengerDeleteDays>" + SepFunctions.HTMLEncode(MessengerDeleteDays.Value) + "</MessengerDeleteDays>" + Environment.NewLine;
            strXml += "<MessengerSMS>" + SepFunctions.HTMLEncode(MessengerSMS.Value) + "</MessengerSMS>" + Environment.NewLine;
            strXml += "</MODULE17>" + Environment.NewLine;
            Disable_Module(17, MessengerEnable.Value);

            strXml += "<MODULE23>" + Environment.NewLine;
            strXml += "<NewsEnable>" + SepFunctions.HTMLEncode(NewsEnable.Value) + "</NewsEnable>" + Environment.NewLine;
            strXml += "<NewsDeleteDays>" + SepFunctions.HTMLEncode(NewsDeleteDays.Value) + "</NewsDeleteDays>" + Environment.NewLine;
            strXml += "</MODULE23>" + Environment.NewLine;
            Disable_Module(23, NewsEnable.Value);

            strXml += "<MODULE24>" + Environment.NewLine;
            strXml += "<NewsLetEnable>" + SepFunctions.HTMLEncode(NewsLetEnable.Value) + "</NewsLetEnable>" + Environment.NewLine;
            strXml += "<NewsletRemoveText>" + SepFunctions.HTMLEncode(NewsletRemoveText.Value) + "</NewsletRemoveText>" + Environment.NewLine;
            strXml += "<NewsletFromEmail>" + SepFunctions.HTMLEncode(NewsletFromEmail.Value) + "</NewsletFromEmail>" + Environment.NewLine;
            strXml += "<NewsletNightlyEnable>" + SepFunctions.HTMLEncode(NewsletNightlyEnable.Value) + "</NewsletNightlyEnable>" + Environment.NewLine;
            strXml += "<NewsletNightlyNews>" + SepFunctions.HTMLEncode(NewsletNightlyNews.Value) + "</NewsletNightlyNews>" + Environment.NewLine;
            strXml += "<NewsletNightlySubject>" + SepFunctions.HTMLEncode(NewsletNightlySubject.Value) + "</NewsletNightlySubject>" + Environment.NewLine;
            strXml += "<NewsletNightlyBody>" + SepFunctions.HTMLEncode(NewsletNightlyBody.Text) + "</NewsletNightlyBody>" + Environment.NewLine;
            strXml += "</MODULE24>" + Environment.NewLine;

            strXml += "<MODULE47>" + Environment.NewLine;
            strXml += "<GamesEnable>" + SepFunctions.HTMLEncode(GamesEnable.Value) + "</GamesEnable>" + Environment.NewLine;
            strXml += "</MODULE47>" + Environment.NewLine;
            Disable_Module(47, GamesEnable.Value);

            strXml += "<MODULE28>" + Environment.NewLine;
            strXml += "<PhotosEnable>" + SepFunctions.HTMLEncode(PhotosEnable.Value) + "</PhotosEnable>" + Environment.NewLine;
            strXml += "<PhotoNumber>" + SepFunctions.HTMLEncode(PhotoNumber.Value) + "</PhotoNumber>" + Environment.NewLine;
            strXml += "</MODULE28>" + Environment.NewLine;
            Disable_Module(28, PhotosEnable.Value);

            strXml += "<MODULE25>" + Environment.NewLine;
            strXml += "<PollsEnable>" + SepFunctions.HTMLEncode(PollsEnable.Value) + "</PollsEnable>" + Environment.NewLine;
            strXml += "</MODULE25>" + Environment.NewLine;
            Disable_Module(25, PollsEnable.Value);

            strXml += "<MODULE68>" + Environment.NewLine;
            strXml += "<LDAPPath>" + SepFunctions.HTMLEncode(LDAPPath.Value) + "</LDAPPath>" + Environment.NewLine;
            strXml += "<LDAPDomain>" + SepFunctions.HTMLEncode(LDAPDomain.Value) + "</LDAPDomain>" + Environment.NewLine;
            strXml += "</MODULE68>" + Environment.NewLine;

            strXml += "<MODULE69>" + Environment.NewLine;
            strXml += "<VideoConferenceEnable>" + SepFunctions.HTMLEncode(VideoConferenceEnable.Value) + "</VideoConferenceEnable>" + Environment.NewLine;
            strXml += "<VideoConferenceSendSMS>" + SepFunctions.HTMLEncode(VideoConferenceSendSMS.Value) + "</VideoConferenceSendSMS>" + Environment.NewLine;
            strXml += "<VideoConferenceSMSOffset>" + SepFunctions.HTMLEncode(VideoConferenceSMSOffset.Value) + "</VideoConferenceSMSOffset>" + Environment.NewLine;
            strXml += "</MODULE69>" + Environment.NewLine;
            Disable_Module(69, VideoConferenceEnable.Value);

            strXml += "<MODULE70>" + Environment.NewLine;
            strXml += "<SepCityAPIKey>" + SepFunctions.HTMLEncode(SepCityAPIKey.Value) + "</SepCityAPIKey>" + Environment.NewLine;
            strXml += "<SepCityUser>" + SepFunctions.HTMLEncode(SepCityUser.Value) + "</SepCityUser>" + Environment.NewLine;
            if (SepCityPassword.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<SepCityPassword>" + SepFunctions.HTMLEncode(SepFunctions.AES_Encrypt(SepCityPassword.Value, "8695vGPjDlbG5vGP")) + " </SepCityPassword>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityPassword") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityPassword").InnerText) > 0)
                        strXml += "<SepCityPassword>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE70/SepCityPassword").InnerText) + "</SepCityPassword>" + Environment.NewLine;
            }

            strXml += "</MODULE70>" + Environment.NewLine;

            strXml += "<MODULE60>" + Environment.NewLine;
            strXml += "<PortalsEnable>" + SepFunctions.HTMLEncode(PortalsEnable.Value) + "</PortalsEnable>" + Environment.NewLine;
            strXml += "<PortalMasterDomain>" + SepFunctions.HTMLEncode(PortalMasterDomain.Value) + "</PortalMasterDomain>" + Environment.NewLine;
            strXml += "<PortalProfiles>" + SepFunctions.HTMLEncode(PortalProfiles.Value) + "</PortalProfiles>" + Environment.NewLine;
            strXml += "<PortalSiteLooks>" + SepFunctions.HTMLEncode(PortalSiteLooks.Value) + "</PortalSiteLooks>" + Environment.NewLine;
            strXml += "</MODULE60>" + Environment.NewLine;
            Disable_Module(60, PortalsEnable.Value);

            strXml += "<MODULE32>" + Environment.NewLine;
            strXml += "<RStateEnable>" + SepFunctions.HTMLEncode(RStateEnable.Value) + "</RStateEnable>" + Environment.NewLine;
            strXml += "<RStateDisplayNewest>" + SepFunctions.HTMLEncode(RStateDisplayNewest.Value) + "</RStateDisplayNewest>" + Environment.NewLine;
            strXml += "<RStateNewestRent>" + SepFunctions.HTMLEncode(RStateNewestRent.Value) + "</RStateNewestRent>" + Environment.NewLine;
            strXml += "<RStateNewestSale>" + SepFunctions.HTMLEncode(RStateNewestSale.Value) + "</RStateNewestSale>" + Environment.NewLine;
            strXml += "<RStateStateDrop>" + SepFunctions.HTMLEncode(RStateStateDrop.Value) + "</RStateStateDrop>" + Environment.NewLine;
            strXml += "<RStateCountryDrop>" + SepFunctions.HTMLEncode(RStateCountryDrop.Value) + "</RStateCountryDrop>" + Environment.NewLine;
            strXml += "<RStateMaxPhotos>" + SepFunctions.HTMLEncode(RStateMaxPhotos.Value) + "</RStateMaxPhotos>" + Environment.NewLine;
            strXml += "</MODULE32>" + Environment.NewLine;
            Disable_Module(32, RStateEnable.Value);

            strXml += "<MODULE43>" + Environment.NewLine;
            strXml += "<ReferEnable>" + SepFunctions.HTMLEncode(ReferEnable.Value) + "</ReferEnable>" + Environment.NewLine;
            strXml += "<ReferDisplayStats>" + SepFunctions.HTMLEncode(ReferDisplayStats.Value) + "</ReferDisplayStats>" + Environment.NewLine;
            strXml += "<ReferTop>" + SepFunctions.HTMLEncode(ReferTop.Value) + "</ReferTop>" + Environment.NewLine;
            strXml += "<ReferEmailSubject>" + SepFunctions.HTMLEncode(ReferEmailSubject.Value) + "</ReferEmailSubject>" + Environment.NewLine;
            strXml += "<ReferEmailBody>" + SepFunctions.HTMLEncode(ReferEmailBody.Value) + "</ReferEmailBody>" + Environment.NewLine;
            strXml += "</MODULE43>" + Environment.NewLine;
            Disable_Module(43, ReferEnable.Value);

            strXml += "<MODULE3>" + Environment.NewLine;
            strXml += "<SearchModules>" + SepFunctions.HTMLEncode(SearchModules.Value) + "</SearchModules>" + Environment.NewLine;
            strXml += "<SearchRadius>" + SepFunctions.HTMLEncode(SearchRadius.Value) + "</SearchRadius>" + Environment.NewLine;
            strXml += "<SearchCountry>" + SepFunctions.HTMLEncode(SearchCountry.Value) + "</SearchCountry>" + Environment.NewLine;
            strXml += "</MODULE3>" + Environment.NewLine;

            strXml += "<MODULE995>" + Environment.NewLine;
            strXml += "<TaxCalcEnable>" + SepFunctions.HTMLEncode(TaxCalcEnable.Value) + "</TaxCalcEnable>" + Environment.NewLine;
            strXml += "<OrderFinalText>" + SepFunctions.HTMLEncode(OrderFinalText.Value) + "</OrderFinalText>" + Environment.NewLine;
            strXml += "<EmailTempAdmin>" + SepFunctions.HTMLEncode(EmailTempAdmin.Value) + "</EmailTempAdmin>" + Environment.NewLine;
            strXml += "<EmailTempCust>" + SepFunctions.HTMLEncode(EmailTempCust.Value) + "</EmailTempCust>" + Environment.NewLine;
            strXml += "</MODULE995>" + Environment.NewLine;

            strXml += "<MODULE41>" + Environment.NewLine;
            strXml += "<ShopMallEnable>" + SepFunctions.HTMLEncode(ShopMallEnable.Value) + "</ShopMallEnable>" + Environment.NewLine;
            strXml += "<ShopMallWishList>" + SepFunctions.HTMLEncode(ShopMallWishList.Value) + "</ShopMallWishList>" + Environment.NewLine;
            strXml += "<ShopMallSalesPage>" + SepFunctions.HTMLEncode(ShopMallSalesPage.Value) + "</ShopMallSalesPage>" + Environment.NewLine;
            strXml += "<ShopMallElectronic>" + SepFunctions.HTMLEncode(ShopMallElectronic.Value) + "</ShopMallElectronic>" + Environment.NewLine;
            strXml += "<ShopMallDisplayNewest>" + SepFunctions.HTMLEncode(ShopMallDisplayNewest.Value) + "</ShopMallDisplayNewest>" + Environment.NewLine;
            strXml += "</MODULE41>" + Environment.NewLine;
            Disable_Module(41, ShopMallEnable.Value);

            strXml += "<MODULE997>" + Environment.NewLine;
            strXml += "<StartupClass>" + SepFunctions.HTMLEncode(StartupClass.Value) + "</StartupClass>" + Environment.NewLine;
            strXml += "<EmailAdminNew>" + SepFunctions.HTMLEncode(EmailAdminNew.Value) + "</EmailAdminNew>" + Environment.NewLine;
            strXml += "<SignupVerify>" + SepFunctions.HTMLEncode(SignupVerify.Value) + "</SignupVerify>" + Environment.NewLine;
            strXml += "<SignupAgreement>" + SepFunctions.HTMLEncode(SignupAgreement.Value) + "</SignupAgreement>" + Environment.NewLine;
            strXml += "<SignupAdminApp>" + SepFunctions.HTMLEncode(SignupAdminApp.Value) + "</SignupAdminApp>" + Environment.NewLine;
            strXml += "<SignupAge>" + SepFunctions.HTMLEncode(SignupAge.Value) + "</SignupAge>" + Environment.NewLine;
            strXml += "<LoginEmail>" + SepFunctions.HTMLEncode(LoginEmail.Value) + "</LoginEmail>" + Environment.NewLine;
            strXml += "<SignupAREmail>" + SepFunctions.HTMLEncode(SignupAREmail.Value) + "</SignupAREmail>" + Environment.NewLine;
            strXml += "<SignupARSubject>" + SepFunctions.HTMLEncode(SignupARSubject.Value) + "</SignupARSubject>" + Environment.NewLine;
            strXml += "<SignupARBody>" + SepFunctions.HTMLEncode(SignupARBody.Value) + "</SignupARBody>" + Environment.NewLine;
            strXml += "<SignupPage>" + SepFunctions.HTMLEncode(SignupPage.Value) + "</SignupPage>" + Environment.NewLine;
            strXml += "<AutoUser>" + SepFunctions.HTMLEncode(AutoUser.Value) + "</AutoUser>" + Environment.NewLine;
            strXml += "<SignupSecTitle>" + SepFunctions.HTMLEncode(SignupSecTitle.Value) + "</SignupSecTitle>" + Environment.NewLine;
            strXml += "<SignupSecDesc>" + SepFunctions.HTMLEncode(SignupSecDesc.Value) + "</SignupSecDesc>" + Environment.NewLine;
            strXml += "<SignupSelMem>" + SepFunctions.HTMLEncode(SignupSelMem.Value) + "</SignupSelMem>" + Environment.NewLine;
            strXml += "</MODULE997>" + Environment.NewLine;

            strXml += "<MODULE989>" + Environment.NewLine;
            strXml += "<FacebookAPIKey>" + SepFunctions.HTMLEncode(FacebookAPIKey.Value) + "</FacebookAPIKey>" + Environment.NewLine;
            strXml += "<GooglereCAPTCHAPublicKey>" + SepFunctions.HTMLEncode(GooglereCAPTCHAPublicKey.Value) + "</GooglereCAPTCHAPublicKey>" + Environment.NewLine;
            strXml += "<GooglereCAPTCHAPrivateKey>" + SepFunctions.HTMLEncode(GooglereCAPTCHAPrivateKey.Value) + "</GooglereCAPTCHAPrivateKey>" + Environment.NewLine;
            strXml += "<GoogleAnalyticsID>" + SepFunctions.HTMLEncode(GoogleAnalyticsID.Value) + "</GoogleAnalyticsID>" + Environment.NewLine;
            strXml += "<GoogleAnalyticsClientID>" + SepFunctions.HTMLEncode(GoogleAnalyticsClientID.Value) + "</GoogleAnalyticsClientID>" + Environment.NewLine;
            strXml += "<GoogleMapsAPI>" + SepFunctions.HTMLEncode(GoogleMapsAPI.Value) + "</GoogleMapsAPI>" + Environment.NewLine;
            strXml += "<TwilioAccountSID>" + SepFunctions.HTMLEncode(TwilioAccountSID.Value) + "</TwilioAccountSID>" + Environment.NewLine;
            strXml += "<TwilioAuthToken>" + SepFunctions.HTMLEncode(TwilioAuthToken.Value) + "</TwilioAuthToken>" + Environment.NewLine;
            strXml += "<TwilioPhoneNumber>" + SepFunctions.HTMLEncode(TwilioPhoneNumber.Value) + "</TwilioPhoneNumber>" + Environment.NewLine;
            strXml += "<TwilioVideoSID>" + SepFunctions.HTMLEncode(TwilioVideoSID.Value) + "</TwilioVideoSID>" + Environment.NewLine;
            strXml += "<TwilioVideoSecret>" + SepFunctions.HTMLEncode(TwilioVideoSecret.Value) + "</TwilioVideoSecret>" + Environment.NewLine;
            strXml += "<PayPalClientID>" + SepFunctions.HTMLEncode(PayPalClientID.Value) + "</PayPalClientID>" + Environment.NewLine;
            if (PayPalSecret.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<PayPalSecret>" + SepFunctions.HTMLEncode(SepFunctions.AES_Encrypt(PayPalSecret.Value, "PayPalSecret")) + " </SepCityPassword>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret").InnerText) > 0)
                        strXml += "<PayPalSecret>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret").InnerText) + "</PayPalSecret>" + Environment.NewLine;
            }
            if (!string.IsNullOrWhiteSpace(CloudFlareAPI.Value) && !string.IsNullOrWhiteSpace(CloudFlareEmail.Value) && !string.IsNullOrWhiteSpace(CloudFlareDomain.Value))
            {
                var sUrl = "https://www.cloudflare.com/api_json.html";
                sUrl += "?a=minify";
                sUrl += "&tkn=" + SepFunctions.UrlEncode(CloudFlareAPI.Value);
                sUrl += "&email=" + SepFunctions.UrlEncode(CloudFlareEmail.Value);
                sUrl += "&z=" + SepFunctions.UrlEncode(CloudFlareDomain.Value);
                sUrl += "&v=7";
                SepFunctions.Send_Get(sUrl);
            }

            strXml += "<CloudFlareAPI>" + SepFunctions.HTMLEncode(CloudFlareAPI.Value) + "</CloudFlareAPI>" + Environment.NewLine;
            strXml += "<CloudFlareEmail>" + SepFunctions.HTMLEncode(CloudFlareEmail.Value) + "</CloudFlareEmail>" + Environment.NewLine;
            strXml += "<CloudFlareDomain>" + SepFunctions.HTMLEncode(CloudFlareDomain.Value) + "</CloudFlareDomain>" + Environment.NewLine;
            strXml += "<LinkedInAPI>" + SepFunctions.HTMLEncode(LinkedInAPI.Value) + "</LinkedInAPI>" + Environment.NewLine;
            strXml += "<LinkedInSecret>" + SepFunctions.HTMLEncode(LinkedInSecret.Value) + "</LinkedInSecret>" + Environment.NewLine;
            strXml += "<FedExAccountNum>" + SepFunctions.HTMLEncode(FedExAccountNum.Value) + "</FedExAccountNum>" + Environment.NewLine;
            strXml += "<FedExMeterNum>" + SepFunctions.HTMLEncode(FedExMeterNum.Value) + "</FedExMeterNum>" + Environment.NewLine;
            strXml += "<FedExServiceKey>" + SepFunctions.HTMLEncode(FedExServiceKey.Value) + "</FedExServiceKey>" + Environment.NewLine;
            if (FedExServicePass.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<FedExServicePass>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(FedExServicePass.Value)) + "</FedExServicePass>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServicePass") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServicePass").InnerText) > 0)
                        strXml += "<FedExServicePass>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE989/FedExServicePass").InnerText) + "</FedExServicePass>" + Environment.NewLine;
            }

            strXml += "<UPSAccountNum>" + SepFunctions.HTMLEncode(UPSAccountNum.Value) + "</UPSAccountNum>" + Environment.NewLine;
            strXml += "<UPSUserName>" + SepFunctions.HTMLEncode(UPSUserName.Value) + "</UPSUserName>" + Environment.NewLine;
            if (UPSPassword.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<UPSPassword>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(UPSPassword.Value)) + "</UPSPassword>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSPassword") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSPassword").InnerText) > 0)
                        strXml += "<UPSPassword>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE989/UPSPassword").InnerText) + "</UPSPassword>" + Environment.NewLine;
            }

            strXml += "<UPSShipperNum>" + SepFunctions.HTMLEncode(UPSShipperNum.Value) + "</UPSShipperNum>" + Environment.NewLine;
            strXml += "<USPSUserID>" + SepFunctions.HTMLEncode(USPSUserID.Value) + "</USPSUserID>" + Environment.NewLine;
            strXml += "<ZoomAPIKey>" + SepFunctions.HTMLEncode(ZoomAPIKey.Value) + "</ZoomAPIKey>" + Environment.NewLine;
            strXml += "<ZoomAPISecret>" + SepFunctions.HTMLEncode(ZoomAPISecret.Value) + "</ZoomAPISecret>" + Environment.NewLine;
            strXml += "<PayPalClientID>" + SepFunctions.HTMLEncode(PayPalClientID.Value) + "</PayPalClientID>" + Environment.NewLine;
            if (PayPalSecret.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<PayPalSecret>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(PayPalSecret.Value)) + "</PayPalSecret>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret").InnerText) > 0)
                        strXml += "<PayPalSecret>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PayPalSecret").InnerText) + "</PayPalSecret>" + Environment.NewLine;
            }
            strXml += "</MODULE989>" + Environment.NewLine;

            strXml += "<MODULE67>" + Environment.NewLine;
            strXml += "<CRMVersion>" + SepFunctions.HTMLEncode(CRMVersion.Value) + "</CRMVersion>" + Environment.NewLine;
            strXml += "<STURL>" + SepFunctions.HTMLEncode(STURL.Value) + "</STURL>" + Environment.NewLine;
            strXml += "<STUser>" + SepFunctions.HTMLEncode(STUser.Value) + "</STUser>" + Environment.NewLine;
            if (STPass.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<STPass>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(STPass.Value)) + "</STPass>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/STPass") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE67/STPass").InnerText) > 0)
                        strXml += "<STPass>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE67/STPass").InnerText) + "</STPass>" + Environment.NewLine;
            }

            strXml += "<STKBEnable>" + SepFunctions.HTMLEncode(STKBEnable.Value) + "</STKBEnable>" + Environment.NewLine;
            strXml += "<STKBNewsEnable>" + SepFunctions.HTMLEncode(STKBNewsEnable.Value) + "</STKBNewsEnable>" + Environment.NewLine;
            strXml += "<STDatabase>" + SepFunctions.HTMLEncode(STDatabase.Value) + "</STDatabase>" + Environment.NewLine;
            strXml += "<SugarCRMURL>" + SepFunctions.HTMLEncode(SugarCRMURL.Value) + "</SugarCRMURL>" + Environment.NewLine;
            strXml += "<SugarCRMUser>" + SepFunctions.HTMLEncode(SugarCRMUser.Value) + "</SugarCRMUser>" + Environment.NewLine;
            if (SugarCRMPass.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<SugarCRMPass>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(SugarCRMPass.Value)) + "</SugarCRMPass>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMPass") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMPass").InnerText) > 0)
                        strXml += "<SugarCRMPass>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE67/SugarCRMPass").InnerText) + "</SugarCRMPass>" + Environment.NewLine;
            }

            strXml += "<SugarCRMDatabase>" + SepFunctions.HTMLEncode(SugarCRMDatabase.Value) + "</SugarCRMDatabase>" + Environment.NewLine;
            strXml += "<SuiteCRMURL>" + SepFunctions.HTMLEncode(SuiteCRMURL.Value) + "</SuiteCRMURL>" + Environment.NewLine;
            strXml += "<SuiteCRMUser>" + SepFunctions.HTMLEncode(SuiteCRMUser.Value) + "</SuiteCRMUser>" + Environment.NewLine;
            if (SuiteCRMPass.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<SuiteCRMPass>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(SuiteCRMPass.Value)) + "</SuiteCRMPass>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMPass") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMPass").InnerText) > 0)
                        strXml += "<SuiteCRMPass>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE67/SuiteCRMPass").InnerText) + "</SuiteCRMPass>" + Environment.NewLine;
            }

            strXml += "<SuiteCRMDatabase>" + SepFunctions.HTMLEncode(SuiteCRMDatabase.Value) + "</SuiteCRMDatabase>" + Environment.NewLine;
            strXml += "<WHMCSURL>" + SepFunctions.HTMLEncode(WHMCSURL.Value) + "</WHMCSURL>" + Environment.NewLine;
            strXml += "<WHMCSUser>" + SepFunctions.HTMLEncode(WHMCSUser.Value) + "</WHMCSUser>" + Environment.NewLine;
            if (WHMCSPass.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<WHMCSPass>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(WHMCSPass.Value)) + "</WHMCSPass>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSPass") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSPass").InnerText) > 0)
                        strXml += "<WHMCSPass>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE67/WHMCSPass").InnerText) + "</WHMCSPass>" + Environment.NewLine;
            }

            strXml += "<WHMCSKB>" + SepFunctions.HTMLEncode(WHMCSKB.Value) + "</WHMCSKB>" + Environment.NewLine;
            strXml += "<WHMCSDatabase>" + SepFunctions.HTMLEncode(WHMCSDatabase.Value) + "</WHMCSDatabase>" + Environment.NewLine;
            strXml += "</MODULE67>" + Environment.NewLine;

            strXml += "<MODULE50>" + Environment.NewLine;
            strXml += "<SpeakerEnable>" + SepFunctions.HTMLEncode(SpeakerEnable.Value) + "</SpeakerEnable>" + Environment.NewLine;
            strXml += "</MODULE50>" + Environment.NewLine;
            Disable_Module(50, SpeakerEnable.Value);

            strXml += "<MODULE15>" + Environment.NewLine;
            strXml += "<StocksSymbols>" + SepFunctions.HTMLEncode(StocksSymbols.Value) + "</StocksSymbols>" + Environment.NewLine;
            strXml += "</MODULE15>" + Environment.NewLine;

            strXml += "<MODULE7>" + Environment.NewLine;
            strXml += "<UPagesEnable>" + SepFunctions.HTMLEncode(UPagesEnable.Value) + "</UPagesEnable>" + Environment.NewLine;
            strXml += "<UPagesSignupProcess>" + SepFunctions.HTMLEncode(UPagesSignupProcess.Value) + "</UPagesSignupProcess>" + Environment.NewLine;
            strXml += "<UPagesTop10>" + SepFunctions.HTMLEncode(UPagesTop10.Value) + "</UPagesTop10>" + Environment.NewLine;
            strXml += "<UPagesMenu1>" + SepFunctions.HTMLEncode(UPagesMenu1.Value) + "</UPagesMenu1>" + Environment.NewLine;
            strXml += "<UPagesMenu2>" + SepFunctions.HTMLEncode(UPagesMenu2.Value) + "</UPagesMenu2>" + Environment.NewLine;
            strXml += "<UPagesMenu3>" + SepFunctions.HTMLEncode(UPagesMenu3.Value) + "</UPagesMenu3>" + Environment.NewLine;
            strXml += "<UPagesMenu4>" + SepFunctions.HTMLEncode(UPagesMenu4.Value) + "</UPagesMenu4>" + Environment.NewLine;
            strXml += "<UPagesMenu5>" + SepFunctions.HTMLEncode(UPagesMenu5.Value) + "</UPagesMenu5>" + Environment.NewLine;
            strXml += "<UPagesMenu6>" + SepFunctions.HTMLEncode(UPagesMenu6.Value) + "</UPagesMenu6>" + Environment.NewLine;
            strXml += "<UPagesMenu7>" + SepFunctions.HTMLEncode(UPagesMenu7.Value) + "</UPagesMenu7>" + Environment.NewLine;
            strXml += "<UPagesMainMenu1>" + SepFunctions.HTMLEncode(UPagesMainMenu1.Value) + "</UPagesMainMenu1>" + Environment.NewLine;
            strXml += "<UPagesMainMenu2>" + SepFunctions.HTMLEncode(UPagesMainMenu2.Value) + "</UPagesMainMenu2>" + Environment.NewLine;
            strXml += "<UPagesMainMenu3>" + SepFunctions.HTMLEncode(UPagesMainMenu3.Value) + "</UPagesMainMenu3>" + Environment.NewLine;
            strXml += "<UPagesMainMenu4>" + SepFunctions.HTMLEncode(UPagesMainMenu4.Value) + "</UPagesMainMenu4>" + Environment.NewLine;
            strXml += "<UPagesMainMenu5>" + SepFunctions.HTMLEncode(UPagesMainMenu5.Value) + "</UPagesMainMenu5>" + Environment.NewLine;
            strXml += "<UPagesMainMenu6>" + SepFunctions.HTMLEncode(UPagesMainMenu6.Value) + "</UPagesMainMenu6>" + Environment.NewLine;
            strXml += "<UPagesMainMenu7>" + SepFunctions.HTMLEncode(UPagesMainMenu7.Value) + "</UPagesMainMenu7>" + Environment.NewLine;
            strXml += "</MODULE7>" + Environment.NewLine;
            Disable_Module(7, UPagesEnable.Value);

            strXml += "<MODULE63>" + Environment.NewLine;
            strXml += "<ProfilesEnable>" + SepFunctions.HTMLEncode(ProfilesEnable.Value) + "</ProfilesEnable>" + Environment.NewLine;
            strXml += "<ProfilesAskSignup>" + SepFunctions.HTMLEncode(ProfilesAskSignup.Value) + "</ProfilesAskSignup>" + Environment.NewLine;
            strXml += "<ProfilesPicNo>" + SepFunctions.HTMLEncode(ProfilesPicNo.Value) + "</ProfilesPicNo>" + Environment.NewLine;
            strXml += "<ProfileRequired>" + SepFunctions.HTMLEncode(ProfileRequired.Value) + "</ProfileRequired>" + Environment.NewLine;
            strXml += "<ProfilesType1>" + SepFunctions.HTMLEncode(ProfilesType1.Value) + "</ProfilesType1>" + Environment.NewLine;
            strXml += "<ProfilesType2>" + SepFunctions.HTMLEncode(ProfilesType2.Value) + "</ProfilesType2>" + Environment.NewLine;
            strXml += "<ProfilesType3>" + SepFunctions.HTMLEncode(ProfilesType3.Value) + "</ProfilesType3>" + Environment.NewLine;
            strXml += "<ProfilesColor>" + SepFunctions.HTMLEncode(ProfilesColor.Value) + "</ProfilesColor>" + Environment.NewLine;
            strXml += "<ProfilesAudio>" + SepFunctions.HTMLEncode(ProfilesAudio.Value) + "</ProfilesAudio>" + Environment.NewLine;
            strXml += "</MODULE63>" + Environment.NewLine;
            Disable_Module(63, ProfilesEnable.Value);

            strXml += "<MODULE65>" + Environment.NewLine;
            strXml += "<VoucherEnable>" + SepFunctions.HTMLEncode(VoucherEnable.Value) + "</VoucherEnable>" + Environment.NewLine;
            strXml += "<VoucherDaysAdd>" + SepFunctions.HTMLEncode(VoucherDaysAdd.Value) + "</VoucherDaysAdd>" + Environment.NewLine;
            strXml += "<VoucherExpireDays>" + SepFunctions.HTMLEncode(VoucherExpireDays.Value) + "</VoucherExpireDays>" + Environment.NewLine;
            strXml += "<VoucherAgreement>" + SepFunctions.HTMLEncode(VoucherAgreement.Value) + "</VoucherAgreement>" + Environment.NewLine;
            strXml += "<VoucherAgreementBuy>" + SepFunctions.HTMLEncode(VoucherAgreementBuy.Value) + "</VoucherAgreementBuy>" + Environment.NewLine;
            strXml += "<VoucherTop10>" + SepFunctions.HTMLEncode(VoucherTop10.Value) + "</VoucherTop10>" + Environment.NewLine;
            strXml += "</MODULE65>" + Environment.NewLine;
            Disable_Module(65, VoucherEnable.Value);

            strXml += "<MODULE66>" + Environment.NewLine;
            strXml += "<PCREnable>" + SepFunctions.HTMLEncode(PCREnable.Value) + "</PCREnable>" + Environment.NewLine;
            strXml += "<PCRAPIKey>" + SepFunctions.HTMLEncode(PCRAPIKey.Value) + "</PCRAPIKey>" + Environment.NewLine;
            strXml += "<PCRAppID>" + SepFunctions.HTMLEncode(PCRAppID.Value) + "</PCRAppID>" + Environment.NewLine;
            strXml += "<PCRAPIURL>" + SepFunctions.HTMLEncode(PCRAPIURL.Value) + "</PCRAPIURL>" + Environment.NewLine;
            strXml += "<PCRDatabaseId>" + SepFunctions.HTMLEncode(PCRDatabaseId.Value) + "</PCRDatabaseId>" + Environment.NewLine;
            strXml += "<PCRUserName>" + SepFunctions.HTMLEncode(PCRUserName.Value) + "</PCRUserName>" + Environment.NewLine;
            if (PCRPassword.Value != SepFunctions.LangText("(Encrypted Data)"))
            {
                strXml += "<PCRPassword>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(PCRPassword.Value)) + "</PCRPassword>" + Environment.NewLine;
            }
            else
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");
                var root = doc.DocumentElement;
                if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRPassword") != null)
                    if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRPassword").InnerText) > 0)
                        strXml += "<PCRPassword>" + SepFunctions.HTMLEncode(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PCRPassword").InnerText) + "</PCRPassword>" + Environment.NewLine;
            }

            strXml += "<PCRExpireDays>" + SepFunctions.HTMLEncode(PCRExpireDays.Value) + "</PCRExpireDays>" + Environment.NewLine;
            strXml += "</MODULE66>" + Environment.NewLine;
            Disable_Module(66, PCREnable.Value);

            strXml += "<MODULE55>" + Environment.NewLine;
            strXml += "<WeatherEnable>" + SepFunctions.HTMLEncode(WeatherEnable.Value) + "</WeatherEnable>" + Environment.NewLine;
            strXml += "</MODULE55>" + Environment.NewLine;
            Disable_Module(55, WeatherEnable.Value);

            strXml += "<MODULE62>" + Environment.NewLine;
            strXml += "<FeedsEnable>" + SepFunctions.HTMLEncode(FeedsEnable.Value) + "</FeedsEnable>" + Environment.NewLine;
            strXml += "</MODULE62>" + Environment.NewLine;
            Disable_Module(62, FeedsEnable.Value);

            strXml += "<MODULE993>" + Environment.NewLine;
            strXml += "<Menu1Text>" + SepFunctions.HTMLEncode(Menu1Text.Value) + "</Menu1Text>" + Environment.NewLine;
            strXml += "<Menu2Text>" + SepFunctions.HTMLEncode(Menu2Text.Value) + "</Menu2Text>" + Environment.NewLine;
            strXml += "<Menu3Text>" + SepFunctions.HTMLEncode(Menu3Text.Value) + "</Menu3Text>" + Environment.NewLine;
            strXml += "<Menu4Text>" + SepFunctions.HTMLEncode(Menu4Text.Value) + "</Menu4Text>" + Environment.NewLine;
            strXml += "<Menu5Text>" + SepFunctions.HTMLEncode(Menu5Text.Value) + "</Menu5Text>" + Environment.NewLine;
            strXml += "<Menu6Text>" + SepFunctions.HTMLEncode(Menu6Text.Value) + "</Menu6Text>" + Environment.NewLine;
            strXml += "<Menu7Text>" + SepFunctions.HTMLEncode(Menu7Text.Value) + "</Menu7Text>" + Environment.NewLine;
            strXml += "<Menu1Sitemap>" + SepFunctions.HTMLEncode(Menu1Sitemap.Value) + "</Menu1Sitemap>" + Environment.NewLine;
            strXml += "<Menu2Sitemap>" + SepFunctions.HTMLEncode(Menu2Sitemap.Value) + "</Menu2Sitemap>" + Environment.NewLine;
            strXml += "<Menu3Sitemap>" + SepFunctions.HTMLEncode(Menu3Sitemap.Value) + "</Menu3Sitemap>" + Environment.NewLine;
            strXml += "<Menu4Sitemap>" + SepFunctions.HTMLEncode(Menu4Sitemap.Value) + "</Menu4Sitemap>" + Environment.NewLine;
            strXml += "<Menu5Sitemap>" + SepFunctions.HTMLEncode(Menu5Sitemap.Value) + "</Menu5Sitemap>" + Environment.NewLine;
            strXml += "<Menu6Sitemap>" + SepFunctions.HTMLEncode(Menu6Sitemap.Value) + "</Menu6Sitemap>" + Environment.NewLine;
            strXml += "<Menu7Sitemap>" + SepFunctions.HTMLEncode(Menu7Sitemap.Value) + "</Menu7Sitemap>" + Environment.NewLine;

            var imageData = string.Empty;

            if (RemoveSiteLogo.Checked)
            {
                imageData = string.Empty;
                SiteLogoImg.Visible = false;
                RemoveSiteLogo.Visible = false;
                RemoveSiteLogo.Checked = false;
            }
            else
            {
                if (SiteLogo.PostedFile == null || string.IsNullOrWhiteSpace(SiteLogo.PostedFile.FileName))
                {
                    var doc = new XmlDocument();
                    doc.Load(SepFunctions.GetDirValue("app_data") + "settings.xml");

                    // Select the book node with the matching attribute value.
                    var root = doc.DocumentElement;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo") != null)
                    {
                        if (Strings.Len(root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo").InnerText) > 0)
                        {
                            SiteLogoImg.ImageUrl = "data:image/png;base64," + root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo").InnerText;
                            SiteLogoImg.Style.Add("display", "block");
                            SiteLogoLabel.InnerHtml = SepFunctions.LangText("Replace Website Logo");
                            SiteLogoImg.Visible = true;
                            RemoveSiteLogo.Visible = true;
                            imageData = root.SelectSingleNode("/ROOTLEVEL/MODULE993/SiteLogo").InnerText;
                        }
                        else
                        {
                            SiteLogoImg.Visible = false;
                            RemoveSiteLogo.Visible = false;
                        }
                    }
                    else
                    {
                        SiteLogoImg.Visible = false;
                        RemoveSiteLogo.Visible = false;
                    }
                }
                else
                {
                    var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(SiteLogo.PostedFile.InputStream.Length)) + 1];
                    SiteLogo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                    imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));

                    if (Strings.Len(imageData) > 0)
                    {
                        SiteLogoImg.ImageUrl = "data:image/png;base64," + imageData;
                        SiteLogoImg.Style.Add("display", "block");
                        SiteLogoLabel.InnerHtml = SepFunctions.LangText("Replace Website Logo");
                        SiteLogoImg.Visible = true;
                        RemoveSiteLogo.Visible = true;
                    }
                    else
                    {
                        SiteLogoImg.Visible = false;
                        RemoveSiteLogo.Visible = false;
                    }
                }
            }

            strXml += "<SiteLogo>" + SepFunctions.HTMLEncode(imageData) + "</SiteLogo>" + Environment.NewLine;
            strXml += "<RSSTop>" + SepFunctions.HTMLEncode(RSSTop.Value) + "</RSSTop>" + Environment.NewLine;
            strXml += "<FavoritesTop>" + SepFunctions.HTMLEncode(FavoritesTop.Value) + "</FavoritesTop>" + Environment.NewLine;
            strXml += "<MainPageTop>" + SepFunctions.HTMLEncode(MainPageTop.Value) + "</MainPageTop>" + Environment.NewLine;
            strXml += "<SocialSharing>" + SepFunctions.HTMLEncode(SocialSharing.Value) + "</SocialSharing>" + Environment.NewLine;
            strXml += "<RequireSSL>" + SepFunctions.HTMLEncode(RequireSSL.Value) + "</RequireSSL>" + Environment.NewLine;
            strXml += "</MODULE993>" + Environment.NewLine;

            strXml += "<MODULE992>" + Environment.NewLine;
            strXml += "<WebSiteName>" + SepFunctions.HTMLEncode(WebSiteName.Value) + "</WebSiteName>" + Environment.NewLine;
            strXml += "<CurrencyCode>" + SepFunctions.HTMLEncode(CurrencyCode.Value) + "</CurrencyCode>" + Environment.NewLine;
            strXml += "<BadWordFilter>" + SepFunctions.HTMLEncode(BadWordFilter.Value) + "</BadWordFilter>" + Environment.NewLine;
            strXml += "<RecPerAPage>" + SepFunctions.HTMLEncode(RecPerAPage.Value) + "</RecPerAPage>" + Environment.NewLine;
            strXml += "<MaxImageSize>" + SepFunctions.HTMLEncode(SepFunctions.toLong(MaxImageSize.Value) > 10240 ? "10240" : MaxImageSize.Value) + "</MaxImageSize>" + Environment.NewLine;
            strXml += "<SiteLang>" + SepFunctions.HTMLEncode(SiteLang.Value) + "</SiteLang>" + Environment.NewLine;
            strXml += "<CatCount>" + SepFunctions.HTMLEncode(CatCount.Value) + "</CatCount>" + Environment.NewLine;
            strXml += "<TimeOffset>" + SepFunctions.HTMLEncode(TimeOffset.Value) + "</TimeOffset>" + Environment.NewLine;
            strXml += "<CatLowestLvl>" + SepFunctions.HTMLEncode(CatLowestLvl.Value) + "</CatLowestLvl>" + Environment.NewLine;
            strXml += "</MODULE992>" + Environment.NewLine;

            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings.xml"))
            {
                outfile.Write(strXml);
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                if (NewsletNightlyEnable.Value == "Yes" && !string.IsNullOrWhiteSpace(NewsletNightlyNews.Value) && !string.IsNullOrWhiteSpace(NewsletNightlySubject.Value) && !string.IsNullOrWhiteSpace(NewsletNightlyBody.Text))
                    using (var cmd = new SqlCommand("SELECT ProcessID FROM BG_Processes WHERE ProcessName='NightlyNews' AND RecurringDays='1'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                                using (var cmd2 = new SqlCommand("UPDATE BG_Processes SET ProcessID=@ProcessID WHERE ProcessName='NightlyNews' AND RecurringDays='1'", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ProcessID", NewsletNightlyNews.Value);
                                    cmd2.ExecuteNonQuery();
                                }
                            else
                                using (var cmd2 = new SqlCommand("INSERT INTO BG_Processes (ProcessID, ProcessName, IntervalSeconds, Status, RecurringDays) VALUES(@ProcessID, 'NightlyNews', '10', '1', '1')", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ProcessID", NewsletNightlyNews.Value);
                                    cmd2.ExecuteNonQuery();
                                }

                        }
                    }
                else
                    using (var cmd = new SqlCommand("DELETE FROM BG_Processes WHERE ProcessName='NightlyNews' AND RecurringDays='1'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
            }

            SepFunctions.Cache_Remove();

            SaveMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Settings successfully saved.") + "</div>";
        }
    }
}