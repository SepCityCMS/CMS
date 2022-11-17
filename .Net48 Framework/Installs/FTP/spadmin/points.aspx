<%@ page title="Points" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="points.aspx.cs" inherits="wwwroot.points" %>
<%@ import namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/jquery/jquery.FileTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        skipRestyling = true;

        function showModule(modid) {

            document.getElementById('Module989').style.display = 'none';
            document.getElementById('Module35').style.display = 'none';
            document.getElementById('Module31').style.display = 'none';
            document.getElementById('Module61').style.display = 'none';
            document.getElementById('Module20').style.display = 'none';
            document.getElementById('Module44').style.display = 'none';
            document.getElementById('Module8').style.display = 'none';
            document.getElementById('Module5').style.display = 'none';
            document.getElementById('Module46').style.display = 'none';
            document.getElementById('Module10').style.display = 'none';
            document.getElementById('Module13').style.display = 'none';
            document.getElementById('Module12').style.display = 'none';
            document.getElementById('Module14').style.display = 'none';
            document.getElementById('Module40').style.display = 'none';
            document.getElementById('Module66').style.display = 'none';
            document.getElementById('Module48').style.display = 'none';
            document.getElementById('Module19').style.display = 'none';
            document.getElementById('Module18').style.display = 'none';
            document.getElementById('Module17').style.display = 'none';
            document.getElementById('Module28').style.display = 'none';
            document.getElementById('Module25').style.display = 'none';
            document.getElementById('Module32').style.display = 'none';
            document.getElementById('Module43').style.display = 'none';
            document.getElementById('Module997').style.display = 'none';
            document.getElementById('Module7').style.display = 'none';
            document.getElementById('Module63').style.display = 'none';
            document.getElementById('Module36').style.display = 'none';
            document.getElementById('Module64').style.display = 'none';
            document.getElementById('PricingOptions').style.display = 'none';
            document.getElementById(modid).style.display = '';
            window.scrollTo(0, 0);
            restyleFormElements('#' + modid);
        }

        $(document)
            .ready(function () {
                $('#SetupMenuTree')
                    .fileTree({ root: '', script: 'menu_points.aspx' },
                        function (sID) {
                            showModule(sID.replace("ModuleID", "Module"));
                        });

                $('#SetupMainContent').width(($(document).width() - 255) + 'px');
                $('#SetupMenuTree')
                    .height(($(document).height() -
                        (<%=SepFunctions.Admin_Menu_Height(SepCommon.SepCore.Request.Item("ModuleID"))%> +20)) +
                        'px');
                $('#SetupMainContent')
                    .height(($(document).height() -
                        (<%=SepFunctions.Admin_Menu_Height(SepCommon.SepCore.Request.Item("ModuleID"))%> +15)) +
                        'px');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0) { 
                var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
                cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
                Response.Write(cAdminModuleMenu.Render()); 
            }
        %>

		<div class="col-md-12" id="modPageContent" runat="server">
            <input type="hidden" runat="server" ID="ModuleID" ClientIDMode="Static" />

            <div id="SetupMainDiv">
                <div id="SetupMenuTree"></div>

                <div id="SetupMainContent">
                    <div id="Module989" style="display: none">
                        <h2 runat="server" id="Module989Title">Access Class</h2>
                        <div class="mb-3">
                            <label ID="JoinClass2Label" clientidmode="Static" runat="server" for="PostJoinClass2">Join the Administrator class:</label>
                            Cost: <input type="text" id="PostJoinClass2" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetJoinClass2" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="JoinClass3Label" clientidmode="Static" runat="server" for="PostJoinClass3">Join the Demo class:</label>
                            Cost: <input type="text" id="PostJoinClass3" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetJoinClass3" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="JoinClass1Label" clientidmode="Static" runat="server" for="PostJoinClass1">Join the Everyone class:</label>
                            Cost: <input type="text" id="PostJoinClass1" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetJoinClass1" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="JoinClass4Label" clientidmode="Static" runat="server" for="PostJoinClass4">Join the Member class:</label>
                            Cost: <input type="text" id="PostJoinClass4" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetJoinClass4" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module35" style="display: none">
                        <h2 runat="server" id="Module35Title">Articles</h2>
                        <div class="mb-3">
                            <label ID="PostArticleLabel" clientidmode="Static" runat="server" for="PostPostArticle">Posting an article:</label>
                            Cost: <input type="text" id="PostPostArticle" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostArticle" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewArticleLabel" clientidmode="Static" runat="server" for="PostViewArticle">Viewing an article:</label>
                            Cost: <input type="text" id="PostViewArticle" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewArticle" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module31" style="display: none">
                        <h2 runat="server" id="Module31Title">Auction</h2>
                        <div class="mb-3">
                            <label ID="AddActionLabel" clientidmode="Static" runat="server" for="PostAddAction">Adding an auction:</label>
                            Cost: <input type="text" id="PostAddAction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddAction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewAuctionLabel" clientidmode="Static" runat="server" for="PostViewAuction">Viewing an auction:</label>
                            Cost: <input type="text" id="PostViewAuction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewAuction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="BidItemLabel" clientidmode="Static" runat="server" for="PostBidItem">Bidding on an item:</label>
                            Cost: <input type="text" id="PostBidItem" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetBidItem" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module61" style="display: none">
                        <h2 runat="server" id="Module61Title">Blogger</h2>
                        <div class="mb-3">
                            <label ID="AddBlogLabel" clientidmode="Static" runat="server" for="PostAddBlog">Adding a blog:</label>
                            Cost: <input type="text" id="PostAddBlog" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddBlog" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewBlogLabel" clientidmode="Static" runat="server" for="PostViewBlog">Viewing a blog:</label>
                            Cost: <input type="text" id="PostViewBlog" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewBlog" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module20" style="display: none">
                        <h2 runat="server" id="Module20Title">Business Directory</h2>
                        <div class="mb-3">
                            <label ID="PostBusinessLabel" clientidmode="Static" runat="server" for="PostPostBusiness">Posting a business:</label>
                            Cost: <input type="text" id="PostPostBusiness" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostBusiness" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewBusinessLabel" clientidmode="Static" runat="server" for="PostViewBusiness">Viewing a business:</label>
                            Cost: <input type="text" id="PostViewBusiness" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewBusiness" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module44" style="display: none">
                        <h2 runat="server" id="Module44Title">Classified Ads</h2>
                        <div class="mb-3">
                            <label ID="PostAdLabel" clientidmode="Static" runat="server" for="PostPostAd">Posting an ad:</label>
                            Cost: <input type="text" id="PostPostAd" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostAd" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewAdLabel" clientidmode="Static" runat="server" for="PostViewAd">Viewing an ad:</label>
                            Cost: <input type="text" id="PostViewAd" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewAd" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PurchaseItemLabel" clientidmode="Static" runat="server" for="PostPurchaseItem">Purchasing an item:</label>
                            Cost: <input type="text" id="PostPurchaseItem" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPurchaseItem" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module8" style="display: none">
                        <h2 runat="server" id="Module8Title">Comments & Ratings</h2>
                        <div class="mb-3">
                            <label ID="RateListLabel" clientidmode="Static" runat="server" for="PostRateList">Rating a listing:</label>
                            Cost: <input type="text" id="PostRateList" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetRateList" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="LeaveCommentLabel" clientidmode="Static" runat="server" for="PostLeaveComment">Leaving a comment:</label>
                            Cost: <input type="text" id="PostLeaveComment" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetLeaveComment" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module64" style="display: none">
                        <h2 runat="server" id="Module64Title">Conference Center</h2>
                        <div class="mb-3">
                            <label ID="CallingLabel" clientidmode="Static" runat="server" for="PostAddCalling">Calling a User:</label>
                            Cost: <input type="text" id="PostAddCalling" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddCalling" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module62" style="display: none">
                        <h2 runat="server" id="Module62Title">Countdown Auction</h2>
                        <div class="mb-3">
                            <label ID="AddCAuctionLabel" clientidmode="Static" runat="server" for="PostAddCAuction">Adding an auction:</label>
                            Cost: <input type="text" id="PostAddCAuction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddCAuction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="BidCAuctionLabel" clientidmode="Static" runat="server" for="PostBidCAuction">Bidding on an Item:</label>
                            Cost: <input type="text" id="PostBidCAuction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetBidCAuction" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module5" style="display: none">
                        <h2 runat="server" id="Module5Title">Discount System</h2>
                        <div class="mb-3">
                            <label ID="AddCouponLabel" clientidmode="Static" runat="server" for="PostAddCoupon">Adding a discount coupon:</label>
                            Cost: <input type="text" id="PostAddCoupon" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddCoupon" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module46" style="display: none">
                        <h2 runat="server" id="Module46Title">Event Calendar</h2>
                        <div class="mb-3">
                            <label ID="PostEventLabel" clientidmode="Static" runat="server" for="PostPostEvent">Posting an event:</label>
                            Cost: <input type="text" id="PostPostEvent" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostEvent" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewEventLabel" clientidmode="Static" runat="server" for="PostViewEvent">Viewing an event:</label>
                            Cost: <input type="text" id="PostViewEvent" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewEvent" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module10" style="display: none">
                        <h2 runat="server" id="Module10Title">Downloads</h2>
                        <div class="mb-3">
                            <label ID="UploadFileLabel" clientidmode="Static" runat="server" for="PostUploadFile">Uploading a file:</label>
                            Cost: <input type="text" id="PostUploadFile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetUploadFile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="DownloadFileLabel" clientidmode="Static" runat="server" for="PostDownloadFile">Downloading / view a download:</label>
                            Cost: <input type="text" id="PostDownloadFile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetDownloadFile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module13" style="display: none">
                        <h2 runat="server" id="Module13Title">Form Creator</h2>
                        <div class="mb-3">
                            <label ID="SubmitFormLabel" clientidmode="Static" runat="server" for="PostSubmitForm">Submitting a form:</label>
                            Cost: <input type="text" id="PostSubmitForm" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetSubmitForm" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module12" style="display: none">
                        <h2 runat="server" id="Module12Title">Forums</h2>
                        <div class="mb-3">
                            <label ID="PostTopicLabel" clientidmode="Static" runat="server" for="PostPostTopic">Posting a new topic:</label>
                            Cost: <input type="text" id="PostPostTopic" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostTopic" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ReplyTopicLabel" clientidmode="Static" runat="server" for="PostReplyTopic">Replying to a topic:</label>
                            Cost: <input type="text" id="PostReplyTopic" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetReplyTopic" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module14" style="display: none">
                        <h2 runat="server" id="Module14Title">Guestbook</h2>
                        <div class="mb-3">
                            <label ID="SignGuestbookLabel" clientidmode="Static" runat="server" for="PostSignGuestbook">Signing the guestbook:</label>
                            Cost: <input type="text" id="PostSignGuestbook" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetSignGuestbook" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module40" style="display: none">
                        <h2 runat="server" id="Module40Title">Hot or Not</h2>
                        <div class="mb-3">
                            <label ID="RatePictureLabel" clientidmode="Static" runat="server" for="PostRatePicture">Rating a picture:</label>
                            Cost: <input type="text" id="PostRatePicture" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetRatePicture" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module66" style="display: none">
                        <h2 runat="server" id="Module66Title">Job Board</h2>
                        <div class="mb-3">
                            <label ID="AddResumesLabel" clientidmode="Static" runat="server" for="PostAddResumes">Adding a resume:</label>
                            Cost: <input type="text" id="PostAddResumes" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddResumes" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ApplyJobsLabel" clientidmode="Static" runat="server" for="PostApplyJobs">Applying for a job:</label>
                            Cost: <input type="text" id="PostApplyJobs" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetApplyJobs" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PostJobsLabel" clientidmode="Static" runat="server" for="PostPostJobs">Posting a job listing:</label>
                            Cost: <input type="text" id="PostPostJobs" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostJobs" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewCandidateLabel" clientidmode="Static" runat="server" for="PostViewCandidate">View a Candidate:</label>
                            Cost: <input type="text" id="PostViewCandidate" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewCandidate" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module48" style="display: none">
                        <h2 runat="server" id="Module48Title">Job Listings</h2>
                        <div class="mb-3">
                            <label ID="AddResumeLabel" clientidmode="Static" runat="server" for="PostAddResume">Adding a resume:</label>
                            Cost: <input type="text" id="PostAddResume" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddResume" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PostJobLabel" clientidmode="Static" runat="server" for="PostPostJob">Posting a job listing:</label>
                            Cost: <input type="text" id="PostPostJob" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostJob" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="AddCompanyLabel" clientidmode="Static" runat="server" for="PostAddCompany">Adding a company:</label>
                            Cost: <input type="text" id="PostAddCompany" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddCompany" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module19" style="display: none">
                        <h2 runat="server" id="Module19Title">Link Directory</h2>
                        <div class="mb-3">
                            <label ID="PostLinkLabel" clientidmode="Static" runat="server" for="PostPostLink">Posting a link:</label>
                            Cost: <input type="text" id="PostPostLink" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostLink" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewLinkLabel" clientidmode="Static" runat="server" for="PostViewLink">Viewing a link:</label>
                            Cost: <input type="text" id="PostViewLink" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewLink" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module18" style="display: none">
                        <h2 runat="server" id="Module18Title">Match Maker</h2>
                        <div class="mb-3">
                            <label ID="CreateProfileLabel" clientidmode="Static" runat="server" for="PostCreateProfile">Creating a profile:</label>
                            Cost: <input type="text" id="PostCreateProfile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetCreateProfile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module17" style="display: none">
                        <h2 runat="server" id="Module17Title">Messenger</h2>
                        <div class="mb-3">
                            <label ID="SendMessageLabel" clientidmode="Static" runat="server" for="PostSendMessage">Sending a message:</label>
                            Cost: <input type="text" id="PostSendMessage" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetSendMessage" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module28" style="display: none">
                        <h2 runat="server" id="Module28Title">Photo Albums</h2>
                        <div class="mb-3">
                            <label ID="CreateAlbumLabel" clientidmode="Static" runat="server" for="PostCreateAlbum">Creating an album:</label>
                            Cost: <input type="text" id="PostCreateAlbum" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetCreateAlbum" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="UploadPictureLabel" clientidmode="Static" runat="server" for="PostUploadPicture">Uploading a picture:</label>
                            Cost: <input type="text" id="PostUploadPicture" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetUploadPicture" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module25" style="display: none">
                        <h2 runat="server" id="Module25Title">Polls</h2>
                        <div class="mb-3">
                            <label ID="VotePollLabel" clientidmode="Static" runat="server" for="PostVotePoll">Voting for a poll:</label>
                            Cost: <input type="text" id="PostVotePoll" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetVotePoll" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module32" style="display: none">
                        <h2 runat="server" id="Module32Title">Real Estate</h2>
                        <div class="mb-3">
                            <label ID="PostPropertyLabel" clientidmode="Static" runat="server" for="PostPostProperty">Posting a property:</label>
                            Cost: <input type="text" id="PostPostProperty" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostProperty" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="ViewPropertyLabel" clientidmode="Static" runat="server" for="PostViewProperty">Viewing a property:</label>
                            Cost: <input type="text" id="PostViewProperty" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetViewProperty" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module43" style="display: none">
                        <h2 runat="server" id="Module43Title">Refer a Friend</h2>
                        <div class="mb-3">
                            <label ID="ReferUserLabel" clientidmode="Static" runat="server" for="PostReferUser">Referring a user to the site:</label>
                            Cost: <input type="text" id="PostReferUser" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetReferUser" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module997" style="display: none">
                        <h2 runat="server" id="Module997Title">Signup Setup</h2>
                        <div class="mb-3">
                            <label ID="CreateAccountLabel" clientidmode="Static" runat="server" for="PostCreateAccount">Creating a new account:</label>
                            Cost: <input type="text" id="PostCreateAccount" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetCreateAccount" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module7" style="display: none">
                        <h2 runat="server" id="Module7Title">User Pages</h2>
                        <div class="mb-3">
                            <label ID="CreateSiteLabel" clientidmode="Static" runat="server" for="PostCreateSite">Creating a web site:</label>
                            Cost: <input type="text" id="PostCreateSite" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetCreateSite" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module63" style="display: none">
                        <h2 runat="server" id="Module63Title">User Profiles</h2>
                        <div class="mb-3">
                            <label ID="AddProfileLabel" clientidmode="Static" runat="server" for="PostAddProfile">Adding a profile:</label>
                            Cost: <input type="text" id="PostAddProfile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetAddProfile" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="Module36" style="display: none">
                        <h2 runat="server" id="Module36Title">User Reviews</h2>
                        <div class="mb-3">
                            <label ID="PostReviewLabel" clientidmode="Static" runat="server" for="PostPostReview">Posting a review:</label>
                            Cost: <input type="text" id="PostPostReview" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Receive: <input type="text" id="GetPostReview" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div id="PricingOptions" style="display: none">
                        <h2 runat="server" id="PricingOptionsTitle">Pricing Options</h2>
                        <div class="mb-3">
                            <label ID="PricingOption1Label" clientidmode="Static" runat="server" for="PricingOption1Points">Pricing Option 1:</label>
                            Number of Credits: <input type="text" id="PricingOption1Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption1Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption2Label" clientidmode="Static" runat="server" for="PricingOption2Points">Pricing Option 2:</label>
                            Number of Credits: <input type="text" id="PricingOption2Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption2Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption3Label" clientidmode="Static" runat="server" for="PricingOption3Points">Pricing Option 3:</label>
                            Number of Credits: <input type="text" id="PricingOption3Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption3Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption4Label" clientidmode="Static" runat="server" for="PricingOption4Points">Pricing Option 4:</label>
                            Number of Credits: <input type="text" id="PricingOption4Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption4Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption5Label" clientidmode="Static" runat="server" for="PricingOption5Points">Pricing Option 5:</label>
                            Number of Credits: <input type="text" id="PricingOption5Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption5Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption6Label" clientidmode="Static" runat="server" for="PricingOption6Points">Pricing Option 6:</label>
                            Number of Credits: <input type="text" id="PricingOption6Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption6Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption7Label" clientidmode="Static" runat="server" for="PricingOption7Points">Pricing Option 7:</label>
                            Number of Credits: <input type="text" id="PricingOption7Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption7Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption8Label" clientidmode="Static" runat="server" for="PricingOption8Points">Pricing Option 8:</label>
                            Number of Credits: <input type="text" id="PricingOption8Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption8Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption9Label" clientidmode="Static" runat="server" for="PricingOption9Points">Pricing Option 9:</label>
                            Number of Credits: <input type="text" id="PricingOption9Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption9Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption10Label" clientidmode="Static" runat="server" for="PricingOption10Points">Pricing Option 10:</label>
                            Number of Credits: <input type="text" id="PricingOption10Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption10Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption11Label" clientidmode="Static" runat="server" for="PricingOption11Points">Pricing Option 11:</label>
                            Number of Credits: <input type="text" id="PricingOption11Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption11Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption12Label" clientidmode="Static" runat="server" for="PricingOption12Points">Pricing Option 12:</label>
                            Number of Credits: <input type="text" id="PricingOption12Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption12Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption13Label" clientidmode="Static" runat="server" for="PricingOption13Points">Pricing Option 13:</label>
                            Number of Credits: <input type="text" id="PricingOption13Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption13Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption14Label" clientidmode="Static" runat="server" for="PricingOption14Points">Pricing Option 14:</label>
                            Number of Credits: <input type="text" id="PricingOption14Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption14Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption15Label" clientidmode="Static" runat="server" for="PricingOption15Points">Pricing Option 15:</label>
                            Number of Credits: <input type="text" id="PricingOption15Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption15Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption16Label" clientidmode="Static" runat="server" for="PricingOption16Points">Pricing Option 16:</label>
                            Number of Credits: <input type="text" id="PricingOption16Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption16Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption17Label" clientidmode="Static" runat="server" for="PricingOption17Points">Pricing Option 17:</label>
                            Number of Credits: <input type="text" id="PricingOption17Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption17Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption18Label" clientidmode="Static" runat="server" for="PricingOption18Points">Pricing Option 18:</label>
                            Number of Credits: <input type="text" id="PricingOption18Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption18Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption19Label" clientidmode="Static" runat="server" for="PricingOption19Points">Pricing Option 19:</label>
                            Number of Credits: <input type="text" id="PricingOption19Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption19Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                        <div class="mb-3">
                            <label ID="PricingOption20Label" clientidmode="Static" runat="server" for="PricingOption20Points">Pricing Option 20:</label>
                            Number of Credits: <input type="text" id="PricingOption20Points" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                            Cost: <input type="text" id="PricingOption20Price" runat="server" class="form-control inline-block" style="width:70px;" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="button-to-bottom">
            <button class="btn btn-primary" ID="SetupSave" runat="server" OnServerClick="SetupSave_Click">Save Changes</button>
            <span><span ID="SaveMessage" runat="server"></span></span>
        </div>

            <%
                if(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0) {
                    Response.Write("<script>");
                    Response.Write("document.getElementById('SetupMenuTree').style.display='none';");
                    Response.Write("document.getElementById('Module" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "').style.display='';");
                    Response.Write("restyleFormElements('#Module" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "');");
                    Response.Write("</script>");
                }
            %>
    </asp:Panel>
</asp:content>