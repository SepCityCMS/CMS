<%@ page title="Security" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="security.aspx.cs" inherits="wwwroot.security" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/jquery/jquery.FileTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        skipRestyling = true;

        function showModule(modid) {
            $('#Module994').hide();
            $('#Module996').hide();
            $('#Module2').hide();
            $('#Module39').hide();
            $('#Module35').hide();
            $('#Module31').hide();
            $('#Module61').hide();
            $('#Module69').hide();
            $('#Module20').hide();
            $('#Module42').hide();
            $('#Module44').hide();
            $('#Module5').hide();
            $('#Module37').hide();
            $('#Module46').hide();
            $('#Module9').hide();
            $('#Module10').hide();
            $('#Module13').hide();
            $('#Module12').hide();
            $('#Module14').hide();
            $('#Module40').hide();
            $('#Module19').hide();
            $('#Module18').hide();
            $('#Module17').hide();
            $('#Module23').hide();
            $('#Module24').hide();
            $('#Module47').hide();
            $('#Module28').hide();
            $('#Module25').hide();
            $('#Module60').hide();
            $('#Module32').hide();
            $('#Module43').hide();
            $('#Module995').hide();
            $('#Module41').hide();
            $('#Module50').hide();
            $('#Module7').hide();
            $('#Module63').hide();
            $('#Module36').hide();
            $('#Module34').hide();
            $('#Module64').hide();
            $('#Module66').hide();
            $('#Module6').hide();
            $('#Module62').hide();
            $('#Module65').hide();
            $('#' + modid).show();
            window.scrollTo(0, 0);
            restyleFormElements('#' + modid);
        }

        $(document)
            .ready(function () {
                $('#SetupMenuTree')
                    .fileTree({ root: '', script: 'menu_security.aspx?PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>' },
                        function (sID) {
                            showModule(sID.replace("ModuleID", "Module"));
                        });

                if ($('#SetupMenuTree').is(":visible")) {
                    $('#SetupMainContent').width(($(document).width() - 255) + 'px');
                    $('#SetupMainContent').height(($(document).height() - (<%=SepFunctions.Admin_Menu_Height(SepCommon.SepCore.Request.Item("ModuleID"))%> - 5)) + 'px');
                } else {
                    $('#SetupMainContent').width('100%');
                    $('#SetupMainContent').height(($(document).height() - 125) + 'px');
                }

                $('#SetupMenuTree')
                    .height(($(document).height() -
                            <%=SepFunctions.Admin_Menu_Height(SepCommon.SepCore.Request.Item("ModuleID"))%>) +
                        'px');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

<asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(ModuleID.Value);
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">
<input type="hidden" runat="server" ID="ModuleID" ClientIDMode="Static" />

<div id="SetupMainDiv">
<div id="SetupMenuTree"></div>

<div id="SetupMainContent">
<div id="Module994" style="display: none">
    <h2 runat="server" id="Module994Title">Setup the Account Management</h2>
    <div class="mb-3">
        <label ID="AdminUserManLabel" ClientIDMode="Static" runat="server">Keys to manage the users:</label>
        <sep:AccessKeySelection ID="AdminUserMan" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="GroupListsLabel" ClientIDMode="Static" runat="server">Keys to manage the group lists:</label>
        <sep:AccessKeySelection ID="GroupLists" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module996" style="display: none">
    <h2 runat="server" id="Module996Title">Setup the Administration Console</h2>
    <div class="mb-3">
        <label ID="AdminAccessLabel" ClientIDMode="Static" runat="server">Keys to access the administration console:</label>
        <sep:AccessKeySelection ID="AdminAccess" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminSetupLabel" ClientIDMode="Static" runat="server">Keys to edit the general setup:</label>
        <sep:AccessKeySelection ID="AdminSetup" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminEditPageLabel" ClientIDMode="Static" runat="server">Keys to manage the web pages:</label>
        <sep:AccessKeySelection ID="AdminEditPage" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminSecurityLabel" ClientIDMode="Static" runat="server">Keys to manage the website security:</label>
        <sep:AccessKeySelection ID="AdminSecurity" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminStatsLabel" ClientIDMode="Static" runat="server">Keys to view the website statistics:</label>
        <sep:AccessKeySelection ID="AdminStats" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminAdvanceLabel" ClientIDMode="Static" runat="server">Keys to access the admin console utilities:</label>
        <sep:AccessKeySelection ID="AdminAdvance" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminModuleManLabel" ClientIDMode="Static" runat="server">Keys to enter the module management:</label>
        <sep:AccessKeySelection ID="AdminModuleMan" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminSiteLooksLabel" ClientIDMode="Static" runat="server">Keys to manage the site templates:</label>
        <sep:AccessKeySelection ID="AdminSiteLooks" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminErrorLogsLabel" ClientIDMode="Static" runat="server">Keys to view error logs:</label>
        <sep:AccessKeySelection ID="AdminErrorLogs" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminActivitiesLabel" ClientIDMode="Static" runat="server">Keys to view/edit user activities:</label>
        <sep:AccessKeySelection ID="AdminActivities" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminPointSysLabel" ClientIDMode="Static" runat="server">Keys to edit the pointing system:</label>
        <sep:AccessKeySelection ID="AdminPointSys" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdminRecycleBinLabel" ClientIDMode="Static" runat="server">Keys to view/restore/delete items from the recycle bin:</label>
        <sep:AccessKeySelection ID="AdminRecycleBin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="SepCityStoreLabel" ClientIDMode="Static" runat="server">Keys to access to SepCity Store:</label>
        <sep:AccessKeySelection ID="SepCityStore" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module64" style="display: none">
    <h2 runat="server" id="Module64Title">Setup the Conference Center</h2>
    <div class="mb-3">
        <label ID="ConferenceAccessLabel" ClientIDMode="Static" runat="server">Keys to access the conference center:</label>
        <sep:AccessKeySelection ID="ConferenceAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ConferenceAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the conference center:</label>
        <sep:AccessKeySelection ID="ConferenceAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module66" style="display: none">
    <h2 runat="server" id="Module66Title">Setup the Job Board</h2>
    <div class="mb-3">
        <label ID="PCRAccessLabel" ClientIDMode="Static" runat="server">Keys to access the job board:</label>
        <sep:AccessKeySelection ID="PCRAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="PCRApplyLabel" ClientIDMode="Static" runat="server">Keys to apply for a job:</label>
        <sep:AccessKeySelection ID="PCRApply" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PCREmployerLabel" ClientIDMode="Static" runat="server">Keys to be able to post new jobs:</label>
        <sep:AccessKeySelection ID="PCREmployer" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PCRBrowseLabel" ClientIDMode="Static" runat="server">Keys to be able to browse for candidates:</label>
        <sep:AccessKeySelection ID="PCRBrowse" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PCRAdminLabel" ClientIDMode="Static" runat="server">Keys to be able to manage the job board:</label>
        <sep:AccessKeySelection ID="PCRAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module2" style="display: none">
    <h2 runat="server" id="Module2Title">Setup the Advertising</h2>
    <div class="mb-3">
        <label ID="AdsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the advertising page:</label>
        <sep:AccessKeySelection ID="AdsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="AdsStatsLabel" ClientIDMode="Static" runat="server">Keys a user must have to view their ad statistics:</label>
        <sep:AccessKeySelection ID="AdsStats" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdsAllStatsLabel" ClientIDMode="Static" runat="server">Keys to be able to see all the statistics:</label>
        <sep:AccessKeySelection ID="AdsAllStats" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AdsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the advertisements:</label>
        <sep:AccessKeySelection ID="AdsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module39" style="display: none">
    <h2 runat="server" id="Module39Title">Setup the Affiliate Program</h2>
    <div class="mb-3">
        <label ID="AffiliateJoinLabel" ClientIDMode="Static" runat="server">Keys to join the affiliate program:</label>
        <sep:AccessKeySelection ID="AffiliateJoin" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AffiliateAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the affiliate program:</label>
        <sep:AccessKeySelection ID="AffiliateAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module35" style="display: none">
    <h2 runat="server" id="Module35Title">Setup the Articles</h2>
    <div class="mb-3">
        <label ID="ArticlesAccessLabel" ClientIDMode="Static" runat="server">Keys to access the articles.:</label>
        <sep:AccessKeySelection ID="ArticlesAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ArticlesPostLabel" ClientIDMode="Static" runat="server">Keys to post a new article.:</label>
        <sep:AccessKeySelection ID="ArticlesPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="ArticlesAdminLabel" ClientIDMode="Static" runat="server">Keys to manage your articles.:</label>
        <sep:AccessKeySelection ID="ArticlesAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module31" style="display: none">
    <h2 runat="server" id="Module31Title">Setup the Auction</h2>
    <div class="mb-3">
        <label ID="AuctionAccessLabel" ClientIDMode="Static" runat="server">Keys to access the auction.:</label>
        <sep:AccessKeySelection ID="AuctionAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="AuctionPostLabel" ClientIDMode="Static" runat="server">Keys to post a new ad.:</label>
        <sep:AccessKeySelection ID="AuctionPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="AuctionAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the auctions.:</label>
        <sep:AccessKeySelection ID="AuctionAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module61" style="display: none">
    <h2 runat="server" id="Module61Title">Setup the Blogs</h2>
    <div class="mb-3">
        <label ID="BlogsAccessLabel" ClientIDMode="Static" runat="server">Keys to be able to access the blogs:</label>
        <sep:AccessKeySelection ID="BlogsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="BlogsCreateLabel" ClientIDMode="Static" runat="server">Keys to be able to create blogs:</label>
        <sep:AccessKeySelection ID="BlogsCreate" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="BlogsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the blogger:</label>
        <sep:AccessKeySelection ID="BlogsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module69" style="display: none">
    <h2 runat="server" id="Module69Title">Setup the Video Conferencing</h2>
    <div class="mb-3">
        <label ID="VideoConferenceAccessKeysLabel" ClientIDMode="Static" runat="server">Keys to be able to access the video conferencing:</label>
        <sep:AccessKeySelection ID="VideoConferenceAccessKeys" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="VideoConferenceCreateKeysLabel" ClientIDMode="Static" runat="server">Keys to be able to create meetings:</label>
        <sep:AccessKeySelection ID="VideoConferenceCreateKeys" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="VideoConferenceAcceptKeysLabel" ClientIDMode="Static" runat="server">Keys to be able to accept meetings:</label>
        <sep:AccessKeySelection ID="VideoConferenceAcceptKeys" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module20" style="display: none">
    <h2 runat="server" id="Module20Title">Setup the Business Directory</h2>
    <div class="mb-3">
        <label ID="BusinessAccessLabel" ClientIDMode="Static" runat="server">Keys to access the business directory.:</label>
        <sep:AccessKeySelection ID="BusinessAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="BusinessPostLabel" ClientIDMode="Static" runat="server">Keys to be able to post a business.:</label>
        <sep:AccessKeySelection ID="BusinessPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="BusinessSiteURLLabel" ClientIDMode="Static" runat="server">Keys to fill in the Site URL field.:</label>
        <sep:AccessKeySelection ID="BusinessSiteURL" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="BusinessMyListingsLabel" ClientIDMode="Static" runat="server">Keys to view My Listings.:</label>
        <sep:AccessKeySelection ID="BusinessMyListings" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="BusinessRandomLabel" ClientIDMode="Static" runat="server">Keys for user businesses to show up in random businesses.:</label>
        <sep:AccessKeySelection ID="BusinessRandom" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="BusinessAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the business directory.:</label>
        <sep:AccessKeySelection ID="BusinessAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module42" style="display: none">
    <h2 runat="server" id="Module42Title">Setup the Chat Rooms</h2>
    <div class="mb-3">
        <label ID="ChatAccessLabel" ClientIDMode="Static" runat="server">Keys to access the chat rooms.:</label>
        <sep:AccessKeySelection ID="ChatAccess" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module6" style="display: none">
    <h2 runat="server" id="Module6Title">Setup the Instant Messenger</h2>
    <div class="mb-3">
        <label ID="IMessengerAccessLabel" ClientIDMode="Static" runat="server">Keys to access the instant messenger:</label>
        <sep:AccessKeySelection ID="IMessengerAccess" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module44" style="display: none">
    <h2 runat="server" id="Module44Title">Setup the Classified Ads</h2>
    <div class="mb-3">
        <label ID="ClassifiedAccessLabel" ClientIDMode="Static" runat="server">Keys to access the classified ads.:</label>
        <sep:AccessKeySelection ID="ClassifiedAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ClassifiedPostLabel" ClientIDMode="Static" runat="server">Keys to post a new ad.:</label>
        <sep:AccessKeySelection ID="ClassifiedPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="ClassifiedAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the classified ads.:</label>
        <sep:AccessKeySelection ID="ClassifiedAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module5" style="display: none">
    <h2 runat="server" id="Module5Title">Setup the Discounts</h2>
    <div class="mb-3">
        <label ID="DiscountsAccessLabel" ClientIDMode="Static" runat="server">Keys to be able to access the discount module:</label>
        <sep:AccessKeySelection ID="DiscountsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="DiscountsPostLabel" ClientIDMode="Static" runat="server">Keys to be able to post a discount:</label>
        <sep:AccessKeySelection ID="DiscountsPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="DiscountsAdminLabel" ClientIDMode="Static" runat="server">Keys to be able to manage the discount system:</label>
        <sep:AccessKeySelection ID="DiscountsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module37" style="display: none">
    <h2 runat="server" id="Module37Title">Setup the E-Learning</h2>
    <div class="mb-3">
        <label ID="ELearningAccessLabel" ClientIDMode="Static" runat="server">Keys to access the E-Learning?:</label>
        <sep:AccessKeySelection ID="ELearningAccess" runat="server" text="|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ELearningAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the E-Learning?:</label>
        <sep:AccessKeySelection ID="ELearningAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module46" style="display: none">
    <h2 runat="server" id="Module46Title">Setup the Event Calendar</h2>
    <div class="mb-3">
        <label ID="EventsAccessLabel" ClientIDMode="Static" runat="server">Keys to be able to use the event calendar?:</label>
        <sep:AccessKeySelection ID="EventsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="EventsPostLabel" ClientIDMode="Static" runat="server">Keys to post new events to the event calendar.:</label>
        <sep:AccessKeySelection ID="EventsPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="EventsSharedLabel" ClientIDMode="Static" runat="server">Keys to share their events.:</label>
        <sep:AccessKeySelection ID="EventsShared" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="EventsTicketsLabel" ClientIDMode="Static" runat="server">Access keys to sell tickets:</label>
        <sep:AccessKeySelection ID="EventsTickets" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="EventsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the event calendar.:</label>
        <sep:AccessKeySelection ID="EventsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module9" style="display: none">
    <h2 runat="server" id="Module9Title">Setup the FAQ</h2>
    <div class="mb-3">
        <label ID="FAQAccessLabel" ClientIDMode="Static" runat="server">Keys to access the frequency asked questions.:</label>
        <sep:AccessKeySelection ID="FAQAccess" runat="server" text="|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="FAQAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the frequency asked questions.:</label>
        <sep:AccessKeySelection ID="FAQAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module10" style="display: none">
    <h2 runat="server" id="Module10Title">Setup the Downloads</h2>
    <div class="mb-3">
        <label ID="LibraryAccessLabel" ClientIDMode="Static" runat="server">Keys to access the Downloads:</label>
        <sep:AccessKeySelection ID="LibraryAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="LibraryUploadLabel" ClientIDMode="Static" runat="server">Keys to upload files:</label>
        <sep:AccessKeySelection ID="LibraryUpload" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="LibraryAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the Downloads:</label>
        <sep:AccessKeySelection ID="LibraryAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module13" style="display: none">
    <h2 runat="server" id="Module13Title">Setup the Forms</h2>
    <div class="mb-3">
        <label ID="FormsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the forms.:</label>
        <sep:AccessKeySelection ID="FormsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="FormsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the forms.:</label>
        <sep:AccessKeySelection ID="FormsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module12" style="display: none">
    <h2 runat="server" id="Module12Title">Setup the Forums</h2>
    <div class="mb-3">
        <label ID="ForumsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the forums:</label>
        <sep:AccessKeySelection ID="ForumsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ForumsPostLabel" ClientIDMode="Static" runat="server">Keys to post a new topic:</label>
        <sep:AccessKeySelection ID="ForumsPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="ForumsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the forums:</label>
        <sep:AccessKeySelection ID="ForumsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module14" style="display: none">
    <h2 runat="server" id="Module14Title">Setup the Guestbook</h2>
    <div class="mb-3">
        <label ID="GuestbookAccessLabel" ClientIDMode="Static" runat="server">Keys to access the guestbook.:</label>
        <sep:AccessKeySelection ID="GuestbookAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="GuestbookSignLabel" ClientIDMode="Static" runat="server">Keys to sign the guestbook.:</label>
        <sep:AccessKeySelection ID="GuestbookSign" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="GuestbookAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the guestbook.:</label>
        <sep:AccessKeySelection ID="GuestbookAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module40" style="display: none">
    <h2 runat="server" id="Module40Title">Setup the Hot or Not</h2>
    <div class="mb-3">
        <label ID="HotNotAccessLabel" ClientIDMode="Static" runat="server">Keys to access the hot or not:</label>
        <sep:AccessKeySelection ID="HotNotAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="HotNotUploadLabel" ClientIDMode="Static" runat="server">Keys to upload images to the hot or not:</label>
        <sep:AccessKeySelection ID="HotNotUpload" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="HotNotAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the hot or not:</label>
        <sep:AccessKeySelection ID="HotNotAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module19" style="display: none">
    <h2 runat="server" id="Module19Title">Setup the Link Directory</h2>
    <div class="mb-3">
        <label ID="LinksAccessLabel" ClientIDMode="Static" runat="server">Keys to access the link directory.:</label>
        <sep:AccessKeySelection ID="LinksAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="LinksPostLabel" ClientIDMode="Static" runat="server">Keys to add a new website.:</label>
        <sep:AccessKeySelection ID="LinksPost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="LinksAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the link directory.:</label>
        <sep:AccessKeySelection ID="LinksAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module18" style="display: none">
    <h2 runat="server" id="Module18Title">Setup the Match Maker</h2>
    <div class="mb-3">
        <label ID="MatchAccessLabel" ClientIDMode="Static" runat="server">Keys to access the match maker:</label>
        <sep:AccessKeySelection ID="MatchAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="MatchViewLabel" ClientIDMode="Static" runat="server">Keys to view other profiles:</label>
        <sep:AccessKeySelection ID="MatchView" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="MatchModifyLabel" ClientIDMode="Static" runat="server">Keys to add/edit their profile:</label>
        <sep:AccessKeySelection ID="MatchModify" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="MatchAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the match maker:</label>
        <sep:AccessKeySelection ID="MatchAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module17" style="display: none">
    <h2 runat="server" id="Module17Title">Setup the Messenger</h2>
    <div class="mb-3">
        <label ID="MessengerAccessLabel" ClientIDMode="Static" runat="server">Keys to access the messenger:</label>
        <sep:AccessKeySelection ID="MessengerAccess" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="MessengerReadLabel" ClientIDMode="Static" runat="server">Keys to read a message:</label>
        <sep:AccessKeySelection ID="MessengerRead" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="MessengerFindLabel" ClientIDMode="Static" runat="server">Keys to find a user:</label>
        <sep:AccessKeySelection ID="MessengerFind" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="MessengerComposeLabel" ClientIDMode="Static" runat="server">Keys to compose a new message:</label>
        <sep:AccessKeySelection ID="MessengerCompose" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="MessengerReplyLabel" ClientIDMode="Static" runat="server">Keys to reply to a message:</label>
        <sep:AccessKeySelection ID="MessengerReply" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="MessengerMassLabel" ClientIDMode="Static" runat="server">Keys to send messages to everyone.:</label>
        <sep:AccessKeySelection ID="MessengerMass" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module23" style="display: none">
    <h2 runat="server" id="Module23Title">Setup the News</h2>
    <div class="mb-3">
        <label ID="NewsAccessLabel" ClientIDMode="Static" runat="server">Keys to read the news:</label>
        <sep:AccessKeySelection ID="NewsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="NewsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the news:</label>
        <sep:AccessKeySelection ID="NewsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module24" style="display: none">
    <h2 runat="server" id="Module24Title">Setup the Newsletters</h2>
    <div class="mb-3">
        <label ID="NewsletJoinLabel" ClientIDMode="Static" runat="server">Keys to join a newsletter:</label>
        <sep:AccessKeySelection ID="NewsletJoin" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="NewsletAdminLabel" ClientIDMode="Static" runat="server">Keys to manage/send a newsletter:</label>
        <sep:AccessKeySelection ID="NewsletAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module47" style="display: none">
    <h2 runat="server" id="Module47Title">Setup the Online Games</h2>
    <div class="mb-3">
        <label ID="GamesAccessLabel" ClientIDMode="Static" runat="server">Keys to access the online games.:</label>
        <sep:AccessKeySelection ID="GamesAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="GamesPlayLabel" ClientIDMode="Static" runat="server">Keys to play the online games.:</label>
        <sep:AccessKeySelection ID="GamesPlay" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="GamesAdminLabel" ClientIDMode="Static" runat="server">Keys to manage online games.:</label>
        <sep:AccessKeySelection ID="GamesAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module28" style="display: none">
    <h2 runat="server" id="Module28Title">Setup the Photo Albums</h2>
    <div class="mb-3">
        <label ID="PhotosAccessLabel" ClientIDMode="Static" runat="server">Keys to access the photo albums.:</label>
        <sep:AccessKeySelection ID="PhotosAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="PhotosSharedLabel" ClientIDMode="Static" runat="server">Keys to view shared photos.:</label>
        <sep:AccessKeySelection ID="PhotosShared" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PhotosCreateLabel" ClientIDMode="Static" runat="server">Keys to create photo albums.:</label>
        <sep:AccessKeySelection ID="PhotosCreate" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PhotosPasswordLabel" ClientIDMode="Static" runat="server">Keys to password protect a photo album.:</label>
        <sep:AccessKeySelection ID="PhotosPassword" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PhotosAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the photo albums.:</label>
        <sep:AccessKeySelection ID="PhotosAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module25" style="display: none">
    <h2 runat="server" id="Module25Title">Setup the Polls</h2>
    <div class="mb-3">
        <label ID="PollsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the polls:</label>
        <sep:AccessKeySelection ID="PollsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="PollsVoteLabel" ClientIDMode="Static" runat="server">Keys to vote for a poll:</label>
        <sep:AccessKeySelection ID="PollsVote" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="PollsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the polls:</label>
        <sep:AccessKeySelection ID="PollsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module60" style="display: none">
    <h2 runat="server" id="Module60Title">Setup the Portals</h2>
    <div class="mb-3">
        <label ID="PortalsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the portal directory.:</label>
        <sep:AccessKeySelection ID="PortalsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="PortalsCreateLabel" ClientIDMode="Static" runat="server">Keys to create new portals.:</label>
        <sep:AccessKeySelection ID="PortalsCreate" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="PortalsAdminLabel" ClientIDMode="Static" runat="server">Keys to be able to manage the portals:</label>
        <sep:AccessKeySelection ID="PortalsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module32" style="display: none">
    <h2 runat="server" id="Module32Title">Setup the Real Estate</h2>
    <div class="mb-3">
        <label ID="RStateAccessLabel" ClientIDMode="Static" runat="server">Keys to access the real estate.:</label>
        <sep:AccessKeySelection ID="RStateAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="RStatePostLabel" ClientIDMode="Static" runat="server">Keys to post a new property.:</label>
        <sep:AccessKeySelection ID="RStatePost" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="RStateTenantsLabel" ClientIDMode="Static" runat="server">Keys to manage and search tenants.:</label>
        <sep:AccessKeySelection ID="RStateTenants" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="RStateAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the real estate.:</label>
        <sep:AccessKeySelection ID="RStateAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module43" style="display: none">
    <h2 runat="server" id="Module43Title">Setup the Refer a Friend</h2>
    <div class="mb-3">
        <label ID="ReferAccessLabel" ClientIDMode="Static" runat="server">Keys to access the refer a friend module.:</label>
        <sep:AccessKeySelection ID="ReferAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
</div>

<div id="Module995" style="display: none">
    <h2 runat="server" id="Module995Title">Setup the Shopping Cart</h2>
    <div class="mb-3">
        <label ID="ShopCartAdminLabel" ClientIDMode="Static" runat="server">Keys to view and manage the invoices:</label>
        <sep:AccessKeySelection ID="ShopCartAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module41" style="display: none">
    <h2 runat="server" id="Module41Title">Setup the Shopping Mall</h2>
    <div class="mb-3">
        <label ID="ShopMallAccessLabel" ClientIDMode="Static" runat="server">Keys to access the shopping mall.:</label>
        <sep:AccessKeySelection ID="ShopMallAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ShopMallStoreLabel" ClientIDMode="Static" runat="server">Keys to allow users to create their own store.:</label>
        <sep:AccessKeySelection ID="ShopMallStore" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="ShopMallAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the shopping mall.:</label>
        <sep:AccessKeySelection ID="ShopMallAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module50" style="display: none">
    <h2 runat="server" id="Module50Title">Setup the Speakers Bureau</h2>
    <div class="mb-3">
        <label ID="SpeakerAccessLabel" ClientIDMode="Static" runat="server">Keys to access the speakers bureau.:</label>
        <sep:AccessKeySelection ID="SpeakerAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="SpeakerAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the speakers bureau.:</label>
        <sep:AccessKeySelection ID="SpeakerAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module7" style="display: none">
    <h2 runat="server" id="Module7Title">Setup the User Pages</h2>
    <div class="mb-3">
        <label ID="UPagesAccessLabel" ClientIDMode="Static" runat="server">Keys to access the user pages.:</label>
        <sep:AccessKeySelection ID="UPagesAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="UPagesCreateLabel" ClientIDMode="Static" runat="server">Keys to create a new web site.:</label>
        <sep:AccessKeySelection ID="UPagesCreate" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="UPagesPayPalLabel" ClientIDMode="Static" runat="server">Keys a user must have to be able to use PayPal in their web pages.:</label>
        <sep:AccessKeySelection ID="UPagesPayPal" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3" id="UPagesPortalSelectionRow" runat="server">
        <label ID="UPagesPortalSelectionLabel" ClientIDMode="Static" runat="server">Keys to associate portals content to the user page.:</label>
        <sep:AccessKeySelection ID="UPagesPortalSelection" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="UPagesManageLabel" ClientIDMode="Static" runat="server">Keys to manage the user pages.:</label>
        <sep:AccessKeySelection ID="UPagesManage" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module63" style="display: none">
    <h2 runat="server" id="Module63Title">Setup the User Profiles</h2>
    <div class="mb-3">
        <label ID="ProfilesAccessLabel" ClientIDMode="Static" runat="server">Keys to access the user profiles:</label>
        <sep:AccessKeySelection ID="ProfilesAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ProfilesViewLabel" ClientIDMode="Static" runat="server">Keys to view other profiles:</label>
        <sep:AccessKeySelection ID="ProfilesView" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ProfilesModifyLabel" ClientIDMode="Static" runat="server">Keys to add/edit their profile:</label>
        <sep:AccessKeySelection ID="ProfilesModify" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="ProfilesAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the user profiles:</label>
        <sep:AccessKeySelection ID="ProfilesAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module36" style="display: none">
    <h2 runat="server" id="Module36Title">Setup the User Reviews</h2>
    <div class="mb-3">
        <label ID="ReviewsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the user reviews.:</label>
        <sep:AccessKeySelection ID="ReviewsAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="ReviewsWriteLabel" ClientIDMode="Static" runat="server">Keys to write a review.:</label>
        <sep:AccessKeySelection ID="ReviewsWrite" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="ReviewsAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the user reviews.:</label>
        <sep:AccessKeySelection ID="ReviewsAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module34" style="display: none">
    <h2 runat="server" id="Module34Title">Setup the Who's Online</h2>
    <div class="mb-3">
        <label ID="WhosOnlineLabel" ClientIDMode="Static" runat="server">Keys to see who is online:</label>
        <sep:AccessKeySelection ID="WhosOnline" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
</div>

<div id="Module62" style="display: none">
    <h2 runat="server" id="H2">Setup the User Feeds</h2>
    <div class="mb-3">
        <label ID="FeedsAccessLabel" ClientIDMode="Static" runat="server">Keys to access the User Feeds module:</label>
        <sep:AccessKeySelection ID="FeedsAccess" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
</div>

<div id="Module65" style="display: none">
    <h2 runat="server" id="H1">Setup the Vouchers</h2>
    <div class="mb-3">
        <label ID="VoucherAccessLabel" ClientIDMode="Static" runat="server">Keys to access the Vouchers module:</label>
        <sep:AccessKeySelection ID="VoucherAccess" runat="server" text="|1|,|2|,|3|,|4|" />
    </div>
    <div class="mb-3">
        <label ID="VoucherModifyLabel" ClientIDMode="Static" runat="server">Keys to add/edit Vouchers:</label>
        <sep:AccessKeySelection ID="VoucherModify" runat="server" text="|2|,|3|,|4|" ExcludeEveryone="True" />
    </div>
    <div class="mb-3">
        <label ID="VoucherAdminLabel" ClientIDMode="Static" runat="server">Keys to manage the Vouchers module:</label>
        <sep:AccessKeySelection ID="VoucherAdmin" runat="server" text="|2|" ExcludeEveryone="True" />
    </div>
</div>
</div>
</div>
            </div>

<div class="button-to-bottom">
    <button class="btn btn-primary" ID="SecuritySave" runat="server" OnServerClick="SecuritySave_Click">Save Changes</button>
    <span><span ID="SaveMessage" runat="server"></span></span>
</div>

<%
    Response.Write("<script>");
    if(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0) {
        Response.Write("$('#SetupMenuTree').hide();");
        Response.Write("$('#Module" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "').show();");
    } else {
        Response.Write("$('.pagecontent').css('top', '0px');");
    }
    Response.Write("</script>");
%>
</asp:Panel>
</asp:content>