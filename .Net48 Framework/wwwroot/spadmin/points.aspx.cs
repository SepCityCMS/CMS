// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="points.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class points.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class points : Page
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
        /// Populates the points.
        /// </summary>
        public void Populate_Points()
        {
            XmlDocument doc = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "points.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    doc.Load(reader);

                    // Select the book node with the matching attribute value.
                    var root = doc.DocumentElement;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass2") != null) PostJoinClass2.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass2").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass2") != null) GetJoinClass2.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass2").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass3") != null) PostJoinClass3.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass3").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass3") != null) GetJoinClass3.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass3").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass1") != null) PostJoinClass1.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass1").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass1") != null) GetJoinClass1.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass1").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass4") != null) PostJoinClass4.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/PostJoinClass4").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass4") != null) GetJoinClass4.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE989/GetJoinClass4").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/PostPostArticle") != null) PostPostArticle.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE35/PostPostArticle").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/GetPostArticle") != null) GetPostArticle.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE35/GetPostArticle").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/PostViewArticle") != null) PostViewArticle.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE35/PostViewArticle").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/GetViewArticle") != null) GetViewArticle.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE35/GetViewArticle").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/PostAddAction") != null) PostAddAction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE31/PostAddAction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/GetAddAction") != null) GetAddAction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE31/GetAddAction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/PostViewAuction") != null) PostViewAuction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE31/PostViewAuction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/GetViewAuction") != null) GetViewAuction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE31/GetViewAuction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/PostBidItem") != null) PostBidItem.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE31/PostBidItem").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/GetBidItem") != null) GetBidItem.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE31/GetBidItem").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/PostAddBlog") != null) PostAddBlog.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE61/PostAddBlog").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/GetAddBlog") != null) GetAddBlog.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE61/GetAddBlog").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/PostViewBlog") != null) PostViewBlog.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE61/PostViewBlog").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE61/GetViewBlog") != null) GetViewBlog.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE61/GetViewBlog").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/PostPostBusiness") != null) PostPostBusiness.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE20/PostPostBusiness").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/GetPostBusiness") != null) GetPostBusiness.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE20/GetPostBusiness").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/PostViewBusiness") != null) PostViewBusiness.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE20/PostViewBusiness").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/GetViewBusiness") != null) GetViewBusiness.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE20/GetViewBusiness").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/PostPostAd") != null) PostPostAd.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE44/PostPostAd").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/GetPostAd") != null) GetPostAd.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE44/GetPostAd").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/PostViewAd") != null) PostViewAd.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE44/PostViewAd").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/GetViewAd") != null) GetViewAd.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE44/GetViewAd").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/PostPurchaseItem") != null) PostPurchaseItem.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE44/PostPurchaseItem").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/GetPurchaseItem") != null) GetPurchaseItem.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE44/GetPurchaseItem").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/PostRateList") != null) PostRateList.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE8/PostRateList").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/GetRateList") != null) GetRateList.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE8/GetRateList").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/PostLeaveComment") != null) PostLeaveComment.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE8/PostLeaveComment").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE8/GetLeaveComment") != null) GetLeaveComment.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE8/GetLeaveComment").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/PostAddCalling") != null) PostAddCalling.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE64/PostAddCalling").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE64/GetAddCalling") != null) GetAddCalling.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE64/GetAddCalling").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE62/PostAddCAuction") != null) PostAddCAuction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE62/PostAddCAuction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE62/GetAddCAuction") != null) GetAddCAuction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE62/GetAddCAuction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE62/PostBidCAuction") != null) PostBidCAuction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE62/PostBidCAuction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE62/GetBidCAuction") != null) GetBidCAuction.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE62/GetBidCAuction").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/PostAddCoupon") != null) PostAddCoupon.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE5/PostAddCoupon").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE5/GetAddCoupon") != null) GetAddCoupon.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE5/GetAddCoupon").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/PostPostEvent") != null) PostPostEvent.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE46/PostPostEvent").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/GetPostEvent") != null) GetPostEvent.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE46/GetPostEvent").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/PostViewEvent") != null) PostViewEvent.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE46/PostViewEvent").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/GetViewEvent") != null) GetViewEvent.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE46/GetViewEvent").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/PostUploadFile") != null) PostUploadFile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE10/PostUploadFile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/GetUploadFile") != null) GetUploadFile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE10/GetUploadFile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/PostDownloadFile") != null) PostDownloadFile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE10/PostDownloadFile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/GetDownloadFile") != null) GetDownloadFile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE10/GetDownloadFile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE13/PostSubmitForm") != null) PostSubmitForm.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE13/PostSubmitForm").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE13/GetSubmitForm") != null) GetSubmitForm.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE13/GetSubmitForm").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/PostPostTopic") != null) PostPostTopic.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE12/PostPostTopic").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/GetPostTopic") != null) GetPostTopic.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE12/GetPostTopic").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/PostReplyTopic") != null) PostReplyTopic.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE12/PostReplyTopic").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE12/GetReplyTopic") != null) GetReplyTopic.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE12/GetReplyTopic").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/PostSignGuestbook") != null) PostSignGuestbook.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE14/PostSignGuestbook").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE14/GetSignGuestbook") != null) GetSignGuestbook.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE14/GetSignGuestbook").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/PostRatePicture") != null) PostRatePicture.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE40/PostRatePicture").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE40/GetRatePicture") != null) GetRatePicture.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE40/GetRatePicture").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostAddResumes") != null) PostAddResumes.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostAddResumes").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetAddResumes") != null) GetAddResumes.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetAddResumes").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostApplyJobs") != null) PostApplyJobs.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostApplyJobs").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetApplyJobs") != null) GetApplyJobs.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetApplyJobs").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostPostJobs") != null) PostPostJobs.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostPostJobs").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetPostJobs") != null) GetPostJobs.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetPostJobs").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostViewCandidate") != null) PostViewCandidate.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/PostViewCandidate").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetViewCandidate") != null) GetViewCandidate.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE66/GetViewCandidate").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE48/PostAddResume") != null) PostAddResume.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE48/PostAddResume").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE48/GetAddResume") != null) GetAddResume.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE48/GetAddResume").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE48/PostPostJob") != null) PostPostJob.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE48/PostPostJob").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE48/GetPostJob") != null) GetPostJob.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE48/GetPostJob").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE48/PostAddCompany") != null) PostAddCompany.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE48/PostAddCompany").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE48/GetAddCompany") != null) GetAddCompany.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE48/GetAddCompany").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/PostPostLink") != null) PostPostLink.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE19/PostPostLink").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/GetPostLink") != null) GetPostLink.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE19/GetPostLink").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/PostViewLink") != null) PostViewLink.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE19/PostViewLink").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE19/GetViewLink") != null) GetViewLink.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE19/GetViewLink").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/PostCreateProfile") != null) PostCreateProfile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE18/PostCreateProfile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/GetCreateProfile") != null) GetCreateProfile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE18/GetCreateProfile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/PostSendMessage") != null) PostSendMessage.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE17/PostSendMessage").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE17/GetSendMessage") != null) GetSendMessage.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE17/GetSendMessage").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PostCreateAlbum") != null) PostCreateAlbum.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE28/PostCreateAlbum").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/GetCreateAlbum") != null) GetCreateAlbum.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE28/GetCreateAlbum").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/PostUploadPicture") != null) PostUploadPicture.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE28/PostUploadPicture").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/GetUploadPicture") != null) GetUploadPicture.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE28/GetUploadPicture").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE25/PostVotePoll") != null) PostVotePoll.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE25/PostVotePoll").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE25/GetVotePoll") != null) GetVotePoll.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE25/GetVotePoll").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/PostPostProperty") != null) PostPostProperty.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE32/PostPostProperty").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/GetPostProperty") != null) GetPostProperty.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE32/GetPostProperty").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/PostViewProperty") != null) PostViewProperty.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE32/PostViewProperty").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/GetViewProperty") != null) GetViewProperty.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE32/GetViewProperty").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/PostReferUser") != null) PostReferUser.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE43/PostReferUser").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE43/GetReferUser") != null) GetReferUser.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE43/GetReferUser").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/PostCreateAccount") != null) PostCreateAccount.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE997/PostCreateAccount").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE997/GetCreateAccount") != null) GetCreateAccount.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE997/GetCreateAccount").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/PostCreateSite") != null) PostCreateSite.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE7/PostCreateSite").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE7/GetCreateSite") != null) GetCreateSite.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE7/GetCreateSite").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/PostAddProfile") != null) PostAddProfile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE63/PostAddProfile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/GetAddProfile") != null) GetAddProfile.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE63/GetAddProfile").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE36/PostPostReview") != null) PostPostReview.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE36/PostPostReview").InnerText));

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE36/GetPostReview") != null) GetPostReview.Value = Strings.ToString(SepFunctions.toDouble(root.SelectSingleNode("/ROOTLEVEL/MODULE36/GetPostReview").InnerText));
                }
            }
        }

        /// <summary>
        /// Populates the pricing options.
        /// </summary>
        /// <param name="Num">The number.</param>
        /// <param name="PointField">The point field.</param>
        /// <param name="PriceField">The price field.</param>
        public void Populate_Pricing_Options(int Num, HtmlInputText PointField, HtmlInputText PriceField)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Inventory,UnitPrice FROM ShopProducts WHERE ModelNumber=@ModelNumber", conn))
                {
                    cmd.Parameters.AddWithValue("@ModelNumber", "PricingOption" + Num);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            PointField.Value = SepFunctions.toLong(SepFunctions.openNull(RS["Inventory"])).ToString();
                            PriceField.Value = SepFunctions.Format_Currency(SepFunctions.openNull(RS["UnitPrice"]));
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Saves the pricing option.
        /// </summary>
        /// <param name="Num">The number.</param>
        /// <param name="Points">The points.</param>
        /// <param name="Price">The price.</param>
        public void Save_Pricing_Option(int Num, long Points, string Price)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                bool bUpdate = false;
                if (Points > 0 && SepFunctions.Format_Currency(SepFunctions.toDecimal(Price)) != SepFunctions.Format_Currency("0"))
                {
                    using (var cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ModelNumber=@ModelNumber", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModelNumber", "PricingOption" + Num);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows) bUpdate = true;
                        }
                    }


                    string SqlStr;
                    if (bUpdate)
                        SqlStr = "UPDATE ShopProducts SET UnitPrice=@UnitPrice, ProductName=@ProductName, Inventory=@Inventory WHERE ModelNumber=@ModelNumber";
                    else
                        SqlStr = "INSERT INTO ShopProducts (ProductID, CatID, Inventory, ProductName, ModelNumber, UnitPrice, ModuleID, PortalID, Status) VALUES (@ProductID, '0', @Inventory, @ProductName, @ModelNumber, @UnitPrice, '973', @PortalID, '1')";
                    using (var cmd = new SqlCommand(SqlStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", SepFunctions.GetIdentity());
                        cmd.Parameters.AddWithValue("@ModelNumber", "PricingOption" + Num);
                        cmd.Parameters.AddWithValue("@ProductName", Points + " Credits");
                        cmd.Parameters.AddWithValue("@Inventory", Points);
                        cmd.Parameters.AddWithValue("@UnitPrice", SepFunctions.toDecimal(Price));
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("DELETE FROM ShopProducts WHERE ModelNumber=@ModelNumber", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModelNumber", "PricingOption" + Num);
                        cmd.ExecuteNonQuery();
                    }
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
                    Module989Title.InnerHtml = SepFunctions.LangText("Access Class");
                    Module35Title.InnerHtml = SepFunctions.LangText("Articles");
                    Module31Title.InnerHtml = SepFunctions.LangText("Auction");
                    Module61Title.InnerHtml = SepFunctions.LangText("Blogger");
                    Module20Title.InnerHtml = SepFunctions.LangText("Business Directory");
                    Module44Title.InnerHtml = SepFunctions.LangText("Classified Ads");
                    Module8Title.InnerHtml = SepFunctions.LangText("Comments & Ratings");
                    Module64Title.InnerHtml = SepFunctions.LangText("Conference Center");
                    Module62Title.InnerHtml = SepFunctions.LangText("Countdown Auction");
                    Module5Title.InnerHtml = SepFunctions.LangText("Discount System");
                    Module46Title.InnerHtml = SepFunctions.LangText("Event Calendar");
                    Module10Title.InnerHtml = SepFunctions.LangText("Downloads");
                    Module13Title.InnerHtml = SepFunctions.LangText("Form Creator");
                    Module12Title.InnerHtml = SepFunctions.LangText("Forums");
                    Module14Title.InnerHtml = SepFunctions.LangText("Guestbook");
                    Module40Title.InnerHtml = SepFunctions.LangText("Hot or Not");
                    Module66Title.InnerHtml = SepFunctions.LangText("Job Board");
                    Module48Title.InnerHtml = SepFunctions.LangText("Job Listings");
                    Module19Title.InnerHtml = SepFunctions.LangText("Link Directory");
                    Module18Title.InnerHtml = SepFunctions.LangText("Match Maker");
                    Module17Title.InnerHtml = SepFunctions.LangText("Messenger");
                    Module28Title.InnerHtml = SepFunctions.LangText("Photo Albums");
                    Module25Title.InnerHtml = SepFunctions.LangText("Polls");
                    Module32Title.InnerHtml = SepFunctions.LangText("Real Estate");
                    Module43Title.InnerHtml = SepFunctions.LangText("Refer a Friend");
                    Module997Title.InnerHtml = SepFunctions.LangText("Signup Setup");
                    Module7Title.InnerHtml = SepFunctions.LangText("User Pages");
                    Module63Title.InnerHtml = SepFunctions.LangText("User Profiles");
                    Module36Title.InnerHtml = SepFunctions.LangText("User Reviews");
                    PricingOptionsTitle.InnerHtml = SepFunctions.LangText("Pricing Options");
                    JoinClass2Label.InnerText = SepFunctions.LangText("Join the Administrator class:");
                    JoinClass3Label.InnerText = SepFunctions.LangText("Join the Demo class:");
                    JoinClass1Label.InnerText = SepFunctions.LangText("Join the Everyone class:");
                    JoinClass4Label.InnerText = SepFunctions.LangText("Join the Member class:");
                    PostArticleLabel.InnerText = SepFunctions.LangText("Posting an article:");
                    ViewArticleLabel.InnerText = SepFunctions.LangText("Viewing an article:");
                    AddActionLabel.InnerText = SepFunctions.LangText("Adding an auction:");
                    ViewAuctionLabel.InnerText = SepFunctions.LangText("Viewing an auction:");
                    BidItemLabel.InnerText = SepFunctions.LangText("Bidding on an item:");
                    AddBlogLabel.InnerText = SepFunctions.LangText("Adding a blog:");
                    ViewBlogLabel.InnerText = SepFunctions.LangText("Viewing a blog:");
                    PostBusinessLabel.InnerText = SepFunctions.LangText("Posting a business:");
                    ViewBusinessLabel.InnerText = SepFunctions.LangText("Viewing a business:");
                    PostAdLabel.InnerText = SepFunctions.LangText("Posting an ad:");
                    ViewAdLabel.InnerText = SepFunctions.LangText("Viewing an ad:");
                    PurchaseItemLabel.InnerText = SepFunctions.LangText("Purchasing an item:");
                    RateListLabel.InnerText = SepFunctions.LangText("Rating a listing:");
                    LeaveCommentLabel.InnerText = SepFunctions.LangText("Leaving a comment:");
                    CallingLabel.InnerText = SepFunctions.LangText("Calling a User:");
                    AddCAuctionLabel.InnerText = SepFunctions.LangText("Adding an auction:");
                    BidCAuctionLabel.InnerText = SepFunctions.LangText("Bidding on an Item:");
                    AddCouponLabel.InnerText = SepFunctions.LangText("Adding a discount coupon:");
                    PostEventLabel.InnerText = SepFunctions.LangText("Posting an event:");
                    ViewEventLabel.InnerText = SepFunctions.LangText("Viewing an event:");
                    UploadFileLabel.InnerText = SepFunctions.LangText("Uploading a file:");
                    DownloadFileLabel.InnerText = SepFunctions.LangText("Downloading / view a download:");
                    SubmitFormLabel.InnerText = SepFunctions.LangText("Submitting a form:");
                    PostTopicLabel.InnerText = SepFunctions.LangText("Posting a new topic:");
                    ReplyTopicLabel.InnerText = SepFunctions.LangText("Replying to a topic:");
                    SignGuestbookLabel.InnerText = SepFunctions.LangText("Signing the guestbook:");
                    RatePictureLabel.InnerText = SepFunctions.LangText("Rating a picture:");
                    AddResumesLabel.InnerText = SepFunctions.LangText("Adding a resume:");
                    ApplyJobsLabel.InnerText = SepFunctions.LangText("Applying for a job:");
                    PostJobsLabel.InnerText = SepFunctions.LangText("Posting a job listing:");
                    ViewCandidateLabel.InnerText = SepFunctions.LangText("View a Candidate:");
                    AddResumeLabel.InnerText = SepFunctions.LangText("Adding a resume:");
                    PostJobLabel.InnerText = SepFunctions.LangText("Posting a job listing:");
                    AddCompanyLabel.InnerText = SepFunctions.LangText("Adding a company:");
                    PostLinkLabel.InnerText = SepFunctions.LangText("Posting a link:");
                    ViewLinkLabel.InnerText = SepFunctions.LangText("Viewing a link:");
                    CreateProfileLabel.InnerText = SepFunctions.LangText("Creating a profile:");
                    SendMessageLabel.InnerText = SepFunctions.LangText("Sending a message:");
                    CreateAlbumLabel.InnerText = SepFunctions.LangText("Creating an album:");
                    UploadPictureLabel.InnerText = SepFunctions.LangText("Uploading a picture:");
                    VotePollLabel.InnerText = SepFunctions.LangText("Voting for a poll:");
                    PostPropertyLabel.InnerText = SepFunctions.LangText("Posting a property:");
                    ViewPropertyLabel.InnerText = SepFunctions.LangText("Viewing a property:");
                    ReferUserLabel.InnerText = SepFunctions.LangText("Referring a user to the site:");
                    CreateAccountLabel.InnerText = SepFunctions.LangText("Creating a new account:");
                    CreateSiteLabel.InnerText = SepFunctions.LangText("Creating a web site:");
                    AddProfileLabel.InnerText = SepFunctions.LangText("Adding a profile:");
                    PostReviewLabel.InnerText = SepFunctions.LangText("Posting a review:");
                    PricingOption1Label.InnerText = SepFunctions.LangText("Pricing Option 1:");
                    PricingOption2Label.InnerText = SepFunctions.LangText("Pricing Option 2:");
                    PricingOption3Label.InnerText = SepFunctions.LangText("Pricing Option 3:");
                    PricingOption4Label.InnerText = SepFunctions.LangText("Pricing Option 4:");
                    PricingOption5Label.InnerText = SepFunctions.LangText("Pricing Option 5:");
                    PricingOption6Label.InnerText = SepFunctions.LangText("Pricing Option 6:");
                    PricingOption7Label.InnerText = SepFunctions.LangText("Pricing Option 7:");
                    PricingOption8Label.InnerText = SepFunctions.LangText("Pricing Option 8:");
                    PricingOption9Label.InnerText = SepFunctions.LangText("Pricing Option 9:");
                    PricingOption10Label.InnerText = SepFunctions.LangText("Pricing Option 10:");
                    PricingOption11Label.InnerText = SepFunctions.LangText("Pricing Option 11:");
                    PricingOption12Label.InnerText = SepFunctions.LangText("Pricing Option 12:");
                    PricingOption13Label.InnerText = SepFunctions.LangText("Pricing Option 13:");
                    PricingOption14Label.InnerText = SepFunctions.LangText("Pricing Option 14:");
                    PricingOption15Label.InnerText = SepFunctions.LangText("Pricing Option 15:");
                    PricingOption16Label.InnerText = SepFunctions.LangText("Pricing Option 16:");
                    PricingOption17Label.InnerText = SepFunctions.LangText("Pricing Option 17:");
                    PricingOption18Label.InnerText = SepFunctions.LangText("Pricing Option 18:");
                    PricingOption19Label.InnerText = SepFunctions.LangText("Pricing Option 19:");
                    PricingOption20Label.InnerText = SepFunctions.LangText("Pricing Option 20:");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminPointSys")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminPointSys"), false) == false)
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

            if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0)
            {
                modPageContent.Attributes.Remove("class");
                modPageContent.Attributes.Add("class", "col-md-12 pagecontent");
            }

            if (!Page.IsPostBack)
            {
                ModuleID.Value = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")).ToString();

                try
                {
                    Populate_Points();

                    Populate_Pricing_Options(1, PricingOption1Points, PricingOption1Price);
                    Populate_Pricing_Options(2, PricingOption2Points, PricingOption2Price);
                    Populate_Pricing_Options(3, PricingOption3Points, PricingOption3Price);
                    Populate_Pricing_Options(4, PricingOption4Points, PricingOption4Price);
                    Populate_Pricing_Options(5, PricingOption5Points, PricingOption5Price);
                    Populate_Pricing_Options(6, PricingOption6Points, PricingOption6Price);
                    Populate_Pricing_Options(7, PricingOption7Points, PricingOption7Price);
                    Populate_Pricing_Options(8, PricingOption8Points, PricingOption8Price);
                    Populate_Pricing_Options(9, PricingOption9Points, PricingOption9Price);
                    Populate_Pricing_Options(10, PricingOption10Points, PricingOption10Price);
                    Populate_Pricing_Options(11, PricingOption11Points, PricingOption11Price);
                    Populate_Pricing_Options(12, PricingOption12Points, PricingOption12Price);
                    Populate_Pricing_Options(13, PricingOption13Points, PricingOption13Price);
                    Populate_Pricing_Options(14, PricingOption14Points, PricingOption14Price);
                    Populate_Pricing_Options(15, PricingOption15Points, PricingOption15Price);
                    Populate_Pricing_Options(16, PricingOption16Points, PricingOption16Price);
                    Populate_Pricing_Options(17, PricingOption17Points, PricingOption17Price);
                    Populate_Pricing_Options(18, PricingOption18Points, PricingOption18Price);
                    Populate_Pricing_Options(19, PricingOption19Points, PricingOption19Price);
                    Populate_Pricing_Options(20, PricingOption20Points, PricingOption20Price);
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

            strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;

            strXml += "<ROOTLEVEL>" + Environment.NewLine;

            strXml += "<MODULE989>" + Environment.NewLine;
            strXml += "<PostJoinClass2>" + SepFunctions.toLong(PostJoinClass2.Value) + "</PostJoinClass2>" + Environment.NewLine;
            strXml += "<GetJoinClass2>" + SepFunctions.toLong(GetJoinClass2.Value) + "</GetJoinClass2>" + Environment.NewLine;
            strXml += "<PostJoinClass3>" + SepFunctions.toLong(PostJoinClass3.Value) + "</PostJoinClass3>" + Environment.NewLine;
            strXml += "<GetJoinClass3>" + SepFunctions.toLong(GetJoinClass3.Value) + "</GetJoinClass3>" + Environment.NewLine;
            strXml += "<PostJoinClass1>" + SepFunctions.toLong(PostJoinClass1.Value) + "</PostJoinClass1>" + Environment.NewLine;
            strXml += "<GetJoinClass1>" + SepFunctions.toLong(GetJoinClass1.Value) + "</GetJoinClass1>" + Environment.NewLine;
            strXml += "<PostJoinClass4>" + SepFunctions.toLong(PostJoinClass4.Value) + "</PostJoinClass4>" + Environment.NewLine;
            strXml += "<GetJoinClass4>" + SepFunctions.toLong(GetJoinClass4.Value) + "</GetJoinClass4>" + Environment.NewLine;
            strXml += "</MODULE989>" + Environment.NewLine;

            strXml += "<MODULE39>" + Environment.NewLine;
            strXml += "<PostReferUser>" + SepFunctions.toLong(PostReferUser.Value) + "</PostReferUser>" + Environment.NewLine;
            strXml += "<GetReferUser>" + SepFunctions.toLong(GetReferUser.Value) + "</GetReferUser>" + Environment.NewLine;
            strXml += "</MODULE39>" + Environment.NewLine;

            strXml += "<MODULE35>" + Environment.NewLine;
            strXml += "<PostPostArticle>" + SepFunctions.toLong(PostPostArticle.Value) + "</PostPostArticle>" + Environment.NewLine;
            strXml += "<GetPostArticle>" + SepFunctions.toLong(GetPostArticle.Value) + "</GetPostArticle>" + Environment.NewLine;
            strXml += "<PostViewArticle>" + SepFunctions.toLong(PostViewArticle.Value) + "</PostViewArticle>" + Environment.NewLine;
            strXml += "<GetViewArticle>" + SepFunctions.toLong(GetViewArticle.Value) + "</GetViewArticle>" + Environment.NewLine;
            strXml += "</MODULE35>" + Environment.NewLine;

            strXml += "<MODULE31>" + Environment.NewLine;
            strXml += "<PostAddAction>" + SepFunctions.toLong(PostAddAction.Value) + "</PostAddAction>" + Environment.NewLine;
            strXml += "<GetAddAction>" + SepFunctions.toLong(GetAddAction.Value) + "</GetAddAction>" + Environment.NewLine;
            strXml += "<PostViewAuction>" + SepFunctions.toLong(PostViewAuction.Value) + "</PostViewAuction>" + Environment.NewLine;
            strXml += "<GetViewAuction>" + SepFunctions.toLong(GetViewAuction.Value) + "</GetViewAuction>" + Environment.NewLine;
            strXml += "<PostBidItem>" + SepFunctions.toLong(PostBidItem.Value) + "</PostBidItem>" + Environment.NewLine;
            strXml += "<GetBidItem>" + SepFunctions.toLong(GetBidItem.Value) + "</GetBidItem>" + Environment.NewLine;
            strXml += "</MODULE31>" + Environment.NewLine;

            strXml += "<MODULE61>" + Environment.NewLine;
            strXml += "<PostAddBlog>" + SepFunctions.toLong(PostAddBlog.Value) + "</PostAddBlog>" + Environment.NewLine;
            strXml += "<GetAddBlog>" + SepFunctions.toLong(GetAddBlog.Value) + "</GetAddBlog>" + Environment.NewLine;
            strXml += "<PostViewBlog>" + SepFunctions.toLong(PostViewBlog.Value) + "</PostViewBlog>" + Environment.NewLine;
            strXml += "<GetViewBlog>" + SepFunctions.toLong(GetViewBlog.Value) + "</GetViewBlog>" + Environment.NewLine;
            strXml += "</MODULE61>" + Environment.NewLine;

            strXml += "<MODULE20>" + Environment.NewLine;
            strXml += "<PostPostBusiness>" + SepFunctions.toLong(PostPostBusiness.Value) + "</PostPostBusiness>" + Environment.NewLine;
            strXml += "<GetPostBusiness>" + SepFunctions.toLong(GetPostBusiness.Value) + "</GetPostBusiness>" + Environment.NewLine;
            strXml += "<PostViewBusiness>" + SepFunctions.toLong(PostViewBusiness.Value) + "</PostViewBusiness>" + Environment.NewLine;
            strXml += "<GetViewBusiness>" + SepFunctions.toLong(GetViewBusiness.Value) + "</GetViewBusiness>" + Environment.NewLine;
            strXml += "</MODULE20>" + Environment.NewLine;

            strXml += "<MODULE44>" + Environment.NewLine;
            strXml += "<PostPostAd>" + SepFunctions.toLong(PostPostAd.Value) + "</PostPostAd>" + Environment.NewLine;
            strXml += "<GetPostAd>" + SepFunctions.toLong(GetPostAd.Value) + "</GetPostAd>" + Environment.NewLine;
            strXml += "<PostViewAd>" + SepFunctions.toLong(PostViewAd.Value) + "</PostViewAd>" + Environment.NewLine;
            strXml += "<GetViewAd>" + SepFunctions.toLong(GetViewAd.Value) + "</GetViewAd>" + Environment.NewLine;
            strXml += "<PostPurchaseItem>" + SepFunctions.toLong(PostPurchaseItem.Value) + "</PostPurchaseItem>" + Environment.NewLine;
            strXml += "<GetPurchaseItem>" + SepFunctions.toLong(GetPurchaseItem.Value) + "</GetPurchaseItem>" + Environment.NewLine;
            strXml += "</MODULE44>" + Environment.NewLine;

            strXml += "<MODULE8>" + Environment.NewLine;
            strXml += "<PostRateList>" + SepFunctions.toLong(PostRateList.Value) + "</PostRateList>" + Environment.NewLine;
            strXml += "<GetRateList>" + SepFunctions.toLong(GetRateList.Value) + "</GetRateList>" + Environment.NewLine;
            strXml += "<PostLeaveComment>" + SepFunctions.toLong(PostLeaveComment.Value) + "</PostLeaveComment>" + Environment.NewLine;
            strXml += "<GetLeaveComment>" + SepFunctions.toLong(GetLeaveComment.Value) + "</GetLeaveComment>" + Environment.NewLine;
            strXml += "</MODULE8>" + Environment.NewLine;

            strXml += "<MODULE64>" + Environment.NewLine;
            strXml += "<PostAddCalling>" + SepFunctions.toLong(PostAddCalling.Value) + "</PostAddCalling>" + Environment.NewLine;
            strXml += "<GetAddCalling>" + SepFunctions.toLong(GetAddCalling.Value) + "</GetAddCalling>" + Environment.NewLine;
            strXml += "</MODULE64>" + Environment.NewLine;

            strXml += "<MODULE62>" + Environment.NewLine;
            strXml += "<PostAddCAuction>" + SepFunctions.toLong(PostAddCAuction.Value) + "</PostAddCAuction>" + Environment.NewLine;
            strXml += "<GetAddCAuction>" + SepFunctions.toLong(GetAddCAuction.Value) + "</GetAddCAuction>" + Environment.NewLine;
            strXml += "<PostBidCAuction>" + SepFunctions.toLong(PostBidCAuction.Value) + "</PostBidCAuction>" + Environment.NewLine;
            strXml += "<GetBidCAuction>" + SepFunctions.toLong(GetBidCAuction.Value) + "</GetBidCAuction>" + Environment.NewLine;
            strXml += "</MODULE62>" + Environment.NewLine;

            strXml += "<MODULE5>" + Environment.NewLine;
            strXml += "<PostAddCoupon>" + SepFunctions.toLong(PostAddCoupon.Value) + "</PostAddCoupon>" + Environment.NewLine;
            strXml += "<GetAddCoupon>" + SepFunctions.toLong(GetAddCoupon.Value) + "</GetAddCoupon>" + Environment.NewLine;
            strXml += "</MODULE5>" + Environment.NewLine;

            strXml += "<MODULE46>" + Environment.NewLine;
            strXml += "<PostPostEvent>" + SepFunctions.toLong(PostPostEvent.Value) + "</PostPostEvent>" + Environment.NewLine;
            strXml += "<GetPostEvent>" + SepFunctions.toLong(GetPostEvent.Value) + "</GetPostEvent>" + Environment.NewLine;
            strXml += "<PostViewEvent>" + SepFunctions.toLong(PostViewEvent.Value) + "</PostViewEvent>" + Environment.NewLine;
            strXml += "<GetViewEvent>" + SepFunctions.toLong(GetViewEvent.Value) + "</GetViewEvent>" + Environment.NewLine;
            strXml += "</MODULE46>" + Environment.NewLine;

            strXml += "<MODULE10>" + Environment.NewLine;
            strXml += "<PostUploadFile>" + SepFunctions.toLong(PostUploadFile.Value) + "</PostUploadFile>" + Environment.NewLine;
            strXml += "<GetUploadFile>" + SepFunctions.toLong(GetUploadFile.Value) + "</GetUploadFile>" + Environment.NewLine;
            strXml += "<PostDownloadFile>" + SepFunctions.toLong(PostDownloadFile.Value) + "</PostDownloadFile>" + Environment.NewLine;
            strXml += "<GetDownloadFile>" + SepFunctions.toLong(GetDownloadFile.Value) + "</GetDownloadFile>" + Environment.NewLine;
            strXml += "</MODULE10>" + Environment.NewLine;

            strXml += "<MODULE13>" + Environment.NewLine;
            strXml += "<PostSubmitForm>" + SepFunctions.toLong(PostSubmitForm.Value) + "</PostSubmitForm>" + Environment.NewLine;
            strXml += "<GetSubmitForm>" + SepFunctions.toLong(GetSubmitForm.Value) + "</GetSubmitForm>" + Environment.NewLine;
            strXml += "</MODULE13>" + Environment.NewLine;

            strXml += "<MODULE12>" + Environment.NewLine;
            strXml += "<PostPostTopic>" + SepFunctions.toLong(PostPostTopic.Value) + "</PostPostTopic>" + Environment.NewLine;
            strXml += "<GetPostTopic>" + SepFunctions.toLong(GetPostTopic.Value) + "</GetPostTopic>" + Environment.NewLine;
            strXml += "<PostReplyTopic>" + SepFunctions.toLong(PostReplyTopic.Value) + "</PostReplyTopic>" + Environment.NewLine;
            strXml += "<GetReplyTopic>" + SepFunctions.toLong(GetReplyTopic.Value) + "</GetReplyTopic>" + Environment.NewLine;
            strXml += "</MODULE12>" + Environment.NewLine;

            strXml += "<MODULE14>" + Environment.NewLine;
            strXml += "<PostSignGuestbook>" + SepFunctions.toLong(PostSignGuestbook.Value) + "</PostSignGuestbook>" + Environment.NewLine;
            strXml += "<GetSignGuestbook>" + SepFunctions.toLong(GetSignGuestbook.Value) + "</GetSignGuestbook>" + Environment.NewLine;
            strXml += "</MODULE14>" + Environment.NewLine;

            strXml += "<MODULE40>" + Environment.NewLine;
            strXml += "<PostRatePicture>" + SepFunctions.toLong(PostRatePicture.Value) + "</PostRatePicture>" + Environment.NewLine;
            strXml += "<GetRatePicture>" + SepFunctions.toLong(GetRatePicture.Value) + "</GetRatePicture>" + Environment.NewLine;
            strXml += "</MODULE40>" + Environment.NewLine;

            strXml += "<MODULE66>" + Environment.NewLine;
            strXml += "<PostAddResumes>" + SepFunctions.toLong(PostAddResumes.Value) + "</PostAddResumes>" + Environment.NewLine;
            strXml += "<GetAddResumes>" + SepFunctions.toLong(GetAddResumes.Value) + "</GetAddResumes>" + Environment.NewLine;
            strXml += "<PostPostJobs>" + SepFunctions.toLong(PostPostJobs.Value) + "</PostPostJobs>" + Environment.NewLine;
            strXml += "<GetPostJobs>" + SepFunctions.toLong(GetPostJobs.Value) + "</GetPostJobs>" + Environment.NewLine;
            strXml += "<PostViewCandidate>" + SepFunctions.toLong(PostViewCandidate.Value) + "</PostViewCandidate>" + Environment.NewLine;
            strXml += "<GetViewCandidate>" + SepFunctions.toLong(GetViewCandidate.Value) + "</GetViewCandidate>" + Environment.NewLine;
            strXml += "</MODULE66>" + Environment.NewLine;

            strXml += "<MODULE48>" + Environment.NewLine;
            strXml += "<PostAddResume>" + SepFunctions.toLong(PostAddResume.Value) + "</PostAddResume>" + Environment.NewLine;
            strXml += "<GetAddResume>" + SepFunctions.toLong(GetAddResume.Value) + "</GetAddResume>" + Environment.NewLine;
            strXml += "<PostPostJob>" + SepFunctions.toLong(PostPostJob.Value) + "</PostPostJob>" + Environment.NewLine;
            strXml += "<GetPostJob>" + SepFunctions.toLong(GetPostJob.Value) + "</GetPostJob>" + Environment.NewLine;
            strXml += "<PostAddCompany>" + SepFunctions.toLong(PostAddCompany.Value) + "</PostAddCompany>" + Environment.NewLine;
            strXml += "<GetAddCompany>" + SepFunctions.toLong(GetAddCompany.Value) + "</GetAddCompany>" + Environment.NewLine;
            strXml += "</MODULE48>" + Environment.NewLine;

            strXml += "<MODULE19>" + Environment.NewLine;
            strXml += "<PostPostLink>" + SepFunctions.toLong(PostPostLink.Value) + "</PostPostLink>" + Environment.NewLine;
            strXml += "<GetPostLink>" + SepFunctions.toLong(GetPostLink.Value) + "</GetPostLink>" + Environment.NewLine;
            strXml += "<PostViewLink>" + SepFunctions.toLong(PostViewLink.Value) + "</PostViewLink>" + Environment.NewLine;
            strXml += "<GetViewLink>" + SepFunctions.toLong(GetViewLink.Value) + "</GetViewLink>" + Environment.NewLine;
            strXml += "</MODULE19>" + Environment.NewLine;

            strXml += "<MODULE18>" + Environment.NewLine;
            strXml += "<PostCreateProfile>" + SepFunctions.toLong(PostCreateProfile.Value) + "</PostCreateProfile>" + Environment.NewLine;
            strXml += "<GetCreateProfile>" + SepFunctions.toLong(GetCreateProfile.Value) + "</GetCreateProfile>" + Environment.NewLine;
            strXml += "</MODULE18>" + Environment.NewLine;

            strXml += "<MODULE17>" + Environment.NewLine;
            strXml += "<PostSendMessage>" + SepFunctions.toLong(PostSendMessage.Value) + "</PostSendMessage>" + Environment.NewLine;
            strXml += "<GetSendMessage>" + SepFunctions.toLong(GetSendMessage.Value) + "</GetSendMessage>" + Environment.NewLine;
            strXml += "</MODULE17>" + Environment.NewLine;

            strXml += "<MODULE28>" + Environment.NewLine;
            strXml += "<PostCreateAlbum>" + SepFunctions.toLong(PostCreateAlbum.Value) + "</PostCreateAlbum>" + Environment.NewLine;
            strXml += "<GetCreateAlbum>" + SepFunctions.toLong(GetCreateAlbum.Value) + "</GetCreateAlbum>" + Environment.NewLine;
            strXml += "<PostUploadPicture>" + SepFunctions.toLong(PostUploadPicture.Value) + "</PostUploadPicture>" + Environment.NewLine;
            strXml += "<GetUploadPicture>" + SepFunctions.toLong(GetUploadPicture.Value) + "</GetUploadPicture>" + Environment.NewLine;
            strXml += "</MODULE28>" + Environment.NewLine;

            strXml += "<MODULE25>" + Environment.NewLine;
            strXml += "<PostVotePoll>" + SepFunctions.toLong(PostVotePoll.Value) + "</PostVotePoll>" + Environment.NewLine;
            strXml += "<GetVotePoll>" + SepFunctions.toLong(GetVotePoll.Value) + "</GetVotePoll>" + Environment.NewLine;
            strXml += "</MODULE25>" + Environment.NewLine;

            strXml += "<MODULE32>" + Environment.NewLine;
            strXml += "<PostPostProperty>" + SepFunctions.toLong(PostPostProperty.Value) + "</PostPostProperty>" + Environment.NewLine;
            strXml += "<GetPostProperty>" + SepFunctions.toLong(GetPostProperty.Value) + "</GetPostProperty>" + Environment.NewLine;
            strXml += "<PostViewProperty>" + SepFunctions.toLong(PostViewProperty.Value) + "</PostViewProperty>" + Environment.NewLine;
            strXml += "<GetViewProperty>" + SepFunctions.toLong(GetViewProperty.Value) + "</GetViewProperty>" + Environment.NewLine;
            strXml += "</MODULE32>" + Environment.NewLine;

            strXml += "<MODULE43>" + Environment.NewLine;
            strXml += "<PostReferUser>" + SepFunctions.toLong(PostReferUser.Value) + "</PostReferUser>" + Environment.NewLine;
            strXml += "<GetReferUser>" + SepFunctions.toLong(GetReferUser.Value) + "</GetReferUser>" + Environment.NewLine;
            strXml += "</MODULE43>" + Environment.NewLine;

            strXml += "<MODULE997>" + Environment.NewLine;
            strXml += "<PostCreateAccount>" + SepFunctions.toLong(PostCreateAccount.Value) + "</PostCreateAccount>" + Environment.NewLine;
            strXml += "<GetCreateAccount>" + SepFunctions.toLong(GetCreateAccount.Value) + "</GetCreateAccount>" + Environment.NewLine;
            strXml += "</MODULE997>" + Environment.NewLine;

            strXml += "<MODULE7>" + Environment.NewLine;
            strXml += "<PostCreateSite>" + SepFunctions.toLong(PostCreateSite.Value) + "</PostCreateSite>" + Environment.NewLine;
            strXml += "<GetCreateSite>" + SepFunctions.toLong(GetCreateSite.Value) + "</GetCreateSite>" + Environment.NewLine;
            strXml += "</MODULE7>" + Environment.NewLine;

            strXml += "<MODULE63>" + Environment.NewLine;
            strXml += "<PostAddProfile>" + SepFunctions.toLong(PostAddProfile.Value) + "</PostAddProfile>" + Environment.NewLine;
            strXml += "<GetAddProfile>" + SepFunctions.toLong(GetAddProfile.Value) + "</GetAddProfile>" + Environment.NewLine;
            strXml += "</MODULE63>" + Environment.NewLine;

            strXml += "<MODULE36>" + Environment.NewLine;
            strXml += "<PostPostReview>" + SepFunctions.toLong(PostPostReview.Value) + "</PostPostReview>" + Environment.NewLine;
            strXml += "<GetPostReview>" + SepFunctions.toLong(GetPostReview.Value) + "</GetPostReview>" + Environment.NewLine;
            strXml += "</MODULE36>" + Environment.NewLine;

            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "points.xml"))
            {
                outfile.Write(strXml);
            }

            Save_Pricing_Option(1, SepFunctions.toLong(PricingOption1Points.Value), PricingOption1Price.Value);
            Save_Pricing_Option(2, SepFunctions.toLong(PricingOption2Points.Value), PricingOption2Price.Value);
            Save_Pricing_Option(3, SepFunctions.toLong(PricingOption3Points.Value), PricingOption3Price.Value);
            Save_Pricing_Option(4, SepFunctions.toLong(PricingOption4Points.Value), PricingOption4Price.Value);
            Save_Pricing_Option(5, SepFunctions.toLong(PricingOption5Points.Value), PricingOption5Price.Value);
            Save_Pricing_Option(6, SepFunctions.toLong(PricingOption6Points.Value), PricingOption6Price.Value);
            Save_Pricing_Option(7, SepFunctions.toLong(PricingOption7Points.Value), PricingOption7Price.Value);
            Save_Pricing_Option(8, SepFunctions.toLong(PricingOption8Points.Value), PricingOption8Price.Value);
            Save_Pricing_Option(9, SepFunctions.toLong(PricingOption9Points.Value), PricingOption9Price.Value);
            Save_Pricing_Option(10, SepFunctions.toLong(PricingOption10Points.Value), PricingOption10Price.Value);
            Save_Pricing_Option(11, SepFunctions.toLong(PricingOption11Points.Value), PricingOption11Price.Value);
            Save_Pricing_Option(12, SepFunctions.toLong(PricingOption12Points.Value), PricingOption12Price.Value);
            Save_Pricing_Option(13, SepFunctions.toLong(PricingOption13Points.Value), PricingOption13Price.Value);
            Save_Pricing_Option(14, SepFunctions.toLong(PricingOption14Points.Value), PricingOption14Price.Value);
            Save_Pricing_Option(15, SepFunctions.toLong(PricingOption15Points.Value), PricingOption15Price.Value);
            Save_Pricing_Option(16, SepFunctions.toLong(PricingOption16Points.Value), PricingOption16Price.Value);
            Save_Pricing_Option(17, SepFunctions.toLong(PricingOption17Points.Value), PricingOption17Price.Value);
            Save_Pricing_Option(18, SepFunctions.toLong(PricingOption18Points.Value), PricingOption18Price.Value);
            Save_Pricing_Option(19, SepFunctions.toLong(PricingOption19Points.Value), PricingOption19Price.Value);
            Save_Pricing_Option(20, SepFunctions.toLong(PricingOption20Points.Value), PricingOption20Price.Value);

            SaveMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Settings successfully saved.") + "</div>";
        }
    }
}