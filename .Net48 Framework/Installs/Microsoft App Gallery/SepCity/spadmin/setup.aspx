<%@ page title="Setup" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="setup.aspx.cs" inherits="wwwroot.setup" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/jquery/jquery.FileTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        skipRestyling = true;

        function changeSupport() {
            if ($('#CRMVersion').length > 0) {
                switch ($('#CRMVersion').val()) {
                    case "SmarterTrack":
                        $('#Module67A').show();
                        $('#Module67A').show();
                        $('#Module67B').hide();
                        $('#Module67C').hide();
                        $('#Module67D').hide();
                        break;

                    case "SugarCRM":
                        $('#Module67B').show();
                        $('#Module67A').hide();
                        $('#Module67B').show();
                        $('#Module67C').hide();
                        $('#Module67D').hide();
                        break;

                    case "SuiteCRM":
                        $('#Module67C').show();
                        $('#Module67A').hide();
                        $('#Module67B').hide();
                        $('#Module67C').show();
                        $('#Module67D').hide();
                        break;

                    case "WHMCS":
                        $('#Module67D').show();
                        $('#Module67A').hide();
                        $('#Module67B').hide();
                        $('#Module67C').hide();
                        $('#Module67D').show();
                        break;

                    default:
                        $('#Module67A').hide();
                        $('#Module67B').hide();
                        $('#Module67C').hide();
                        $('#Module67D').hide();
                        break;
                }
            }
        }

        function showModule(modid) {
            try { $('#Module33').hide(); } catch (e) { }
            try { $('#Module994').hide(); } catch (e) { }
            try { $('#Module991').hide(); } catch (e) { }
            try { $('#Module2').hide(); } catch (e) { }
            try { $('#Module39').hide(); } catch (e) { }
            try { $('#Module35').hide(); } catch (e) { }
            try { $('#Module31').hide(); } catch (e) { }
            try { $('#Module61').hide(); } catch (e) { }
            try { $('#Module20').hide(); } catch (e) { }
            try { $('#Module42').hide(); } catch (e) { }
            try { $('#Module44').hide(); } catch (e) { }
            try { $('#Module8').hide(); } catch (e) { }
            try { $('#Module4').hide(); } catch (e) { }
            try { $('#Module1').hide(); } catch (e) { }
            try { $('#Module5').hide(); } catch (e) { }
            try { $('#Module37').hide(); } catch (e) { }
            try { $('#Module46').hide(); } catch (e) { }
            try { $('#Module9').hide(); } catch (e) { }
            try { $('#Module10').hide(); } catch (e) { }
            try { $('#Module13').hide(); } catch (e) { }
            try { $('#Module12').hide(); } catch (e) { }
            try { $('#Module14').hide(); } catch (e) { }
            try { $('#Module57').hide(); } catch (e) { }
            try { $('#Module40').hide(); } catch (e) { }
            try { $('#Module56').hide(); } catch (e) { }
            try { $('#Module19').hide(); } catch (e) { }
            try { $('#Module18').hide(); } catch (e) { }
            try { $('#Module17').hide(); } catch (e) { }
            try { $('#Module23').hide(); } catch (e) { }
            try { $('#Module24').hide(); } catch (e) { }
            try { $('#Module47').hide(); } catch (e) { }
            try { $('#Module28').hide(); } catch (e) { }
            try { $('#Module25').hide(); } catch (e) { }
            try { $('#Module60').hide(); } catch (e) { }
            try { $('#Module32').hide(); } catch (e) { }
            try { $('#Module43').hide(); } catch (e) { }
            try { $('#Module3').hide(); } catch (e) { }
            try { $('#Module995').hide(); } catch (e) { }
            try { $('#Module41').hide(); } catch (e) { }
            try { $('#Module997').hide(); } catch (e) { }
            try { $('#Module50').hide(); } catch (e) { }
            try { $('#Module15').hide(); } catch (e) { }
            try { $('#Module6').hide(); } catch (e) { }
            try { $('#Module7').hide(); } catch (e) { }
            try { $('#Module63').hide(); } catch (e) { }
            try { $('#Module55').hide(); } catch (e) { }
            try { $('#Module993').hide(); } catch (e) { }
            try { $('#Module992').hide(); } catch (e) { }
            try { $('#Module62').hide(); } catch (e) { }
            try { $('#Module65').hide(); } catch (e) { }
            try { $('#Module66').hide(); } catch (e) { }
            try { $('#Module64').hide(); } catch (e) { }
            try { $('#Module70').hide(); } catch (e) { }
            try { $('#Module989A').hide(); } catch (e) { }
            try { $('#Module989B').hide(); } catch (e) { }
            try { $('#Module989C').hide(); } catch (e) { }
            try { $('#Module989D').hide(); } catch (e) { }
            try { $('#Module989E').hide(); } catch (e) { }
            try { $('#Module989F').hide(); } catch (e) { }
            try { $('#Module989G').hide(); } catch (e) { }
            try { $('#Module989H').hide(); } catch (e) { }
            try { $('#Module989I').hide(); } catch (e) { }
            try { $('#Module989J').hide(); } catch (e) { }
            try { $('#Module989K').hide(); } catch (e) { }
            try { $('#Module989L').hide(); } catch (e) { }
            try { $('#Module67').hide(); } catch (e) { }
            try { $('#Module68').hide(); } catch (e) { }
            try { $('#Module69').hide(); } catch (e) { }
            $('#' + modid).show();
            window.scrollTo(0, 0);
            restyleFormElements('#' + modid);
        }

        $(document)
            .ready(function () {
                $('#SetupMenuTree')
                    .fileTree({ root: '', script: 'menu_setup.aspx?PortalID=<%=SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID"))%>' },
                        function (sID) {
                            showModule(sID.replace("ModuleID", "Module"));
                        });

                if ($('#SetupMenuTree').is(":visible")) {
                    $('#SetupMainContent').width(($(document).width() - 255) + 'px');
                    $('#SetupMainContent').height(($(document).height() - (<%=SepFunctions.Admin_Menu_Height(SepCommon.SepCore.Request.Item("ModuleID"))%> + 15)) + 'px');
                } else {
                    $('#SetupMainContent').width('100%');
                    $('#SetupMainContent').height(($(document).height() - 125) + 'px');
                }

                $('#SetupMenuTree')
                    .height(($(document).height() -
                        (<%=SepFunctions.Admin_Menu_Height(SepCommon.SepCore.Request.Item("ModuleID"))%> +20)) +
                        'px');

                changeSupport();
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
        <input type="hidden" runat="server" id="ModuleID" clientidmode="Static" />

        <div id="SetupMainDiv">
            <div id="SetupMenuTree"></div>

            <div id="SetupMainContent">
                <div id="Module33" style="display: none">
                    <h2 runat="server" id="Module33Title">Setup the Account Info</h2>
                    <div class="mb-3">
                        <label id="LoginPageLabel" clientidmode="Static" runat="server" for="LoginPage">Page to go to when someone logs into your web site:</label>
                        <select id="LoginPage" runat="server" class="form-control">
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowMembershipsLabel" clientidmode="Static" runat="server" for="PPShowMemberships">Show the memberships tab by default:</label>
                        <select id="PPShowMemberships" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowOrdersLabel" clientidmode="Static" runat="server" for="PPShowOrders">Show the order status tab by default:</label>
                        <select id="PPShowOrders" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowAdStatsLabel" clientidmode="Static" runat="server" for="PPShowAdStats">Show the ad stats tab by default:</label>
                        <select id="PPShowAdStats" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowFriendsLabel" clientidmode="Static" runat="server" for="PPShowFriends">Show the friends tab by default:</label>
                        <select id="PPShowFriends" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowFavoritesLabel" clientidmode="Static" runat="server" for="PPShowFavorites">Show the favorites tab by default:</label>
                        <select id="PPShowFavorites" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowAffiliateLabel" clientidmode="Static" runat="server" for="PPShowAffiliate">Show the affiliate tabs by default:</label>
                        <select id="PPShowAffiliate" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowMessengerLabel" clientidmode="Static" runat="server" for="PPShowMessenger">Show the messenger tab by default:</label>
                        <select id="PPShowMessenger" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3" id="ProfileTabRow" runat="server">
                        <label id="PPShowProfileLabel" clientidmode="Static" runat="server" for="PPShowProfile">Show the profile tab by default:</label>
                        <select id="PPShowProfile" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3" id="BlogTabRow" runat="server">
                        <label id="PPShowBlogsLabel" clientidmode="Static" runat="server" for="PPShowBlogs">Show the blogs tab by default:</label>
                        <select id="PPShowBlogs" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3" id="ForumsTabRow" runat="server">
                        <label id="PPShowForumsLabel" clientidmode="Static" runat="server" for="PPShowForums">Show the forums tab by default:</label>
                        <select id="PPShowForums" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPShowMainLabel" clientidmode="Static" runat="server" for="PPShowMain">Show the main page tab by default:</label>
                        <select id="PPShowMain" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PPDefaultPageLabel" clientidmode="Static" runat="server" for="PPDefaultPage">Default page a user will go to when entering their account:</label>
                        <select id="PPDefaultPage" runat="server" class="form-control">
                            <option value="Account">Account</option>
                            <option value="PageText">PageText</option>
                            <option value="Memberships">Memberships</option>
                            <option value="Orders">Orders</option>
                            <option value="AdStats">AdStats</option>
                            <option value="Friends">Friends</option>
                            <option value="Favorites">Favorites</option>
                            <option value="Affiliate">Affiliate</option>
                            <option value="Messenger">Messenger</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="FriendsEnableLabel" clientidmode="Static" runat="server" for="FriendsEnable">Enable the friends list:</label>
                        <select id="FriendsEnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ShowCreditsLabel" clientidmode="Static" runat="server" for="ShowCredits">Show credits that a user has in the account info page:</label>
                        <select id="ShowCredits" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3" id="ShowCreditsSignupRow" runat="server">
                        <label id="ShowCreditsSignupLabel" clientidmode="Static" runat="server" for="ShowCreditsSignup">Show credits on the signup page:</label>
                        <select id="ShowCreditsSignup" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module994" style="display: none" runat="server" ClientIDMode="Static">
                    <h2 runat="server" id="Module994Title">Setup the Account Management</h2>
                    <div class="mb-3">
                        <label id="AskGenderLabel" clientidmode="Static" runat="server" for="AskGender">Ask users their gender when they signup:</label>
                        <select id="AskGender" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskPhoneNumberLabel" clientidmode="Static" runat="server" for="AskPhoneNumber">Ask users their phone number:</label>
                        <select id="AskPhoneNumber" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ReqPhoneNumberLabel" clientidmode="Static" runat="server" for="ReqPhoneNumber">Require phone number when users signup:</label>
                        <select id="ReqPhoneNumber" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskStreetAddressLabel" clientidmode="Static" runat="server" for="AskStreetAddress">Ask users their street address:</label>
                        <select id="AskStreetAddress" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskCityLabel" clientidmode="Static" runat="server" for="AskCity">Ask users their city:</label>
                        <select id="AskCity" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskStateLabel" clientidmode="Static" runat="server" for="AskState">Ask users the state they live in:</label>
                        <select id="AskState" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskZipCodeLabel" clientidmode="Static" runat="server" for="AskZipCode">Ask users their zip code:</label>
                        <select id="AskZipCode" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskCountryLabel" clientidmode="Static" runat="server" for="AskCountry">Ask users their country:</label>
                        <select id="AskCountry" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ReqAddressLabel" clientidmode="Static" runat="server" for="ReqAddress">Require address when users signup:</label>
                        <select id="ReqAddress" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskBirthDateLabel" clientidmode="Static" runat="server" for="AskBirthDate">Ask users their date of birth:</label>
                        <select id="AskBirthDate" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskPayPalLabel" clientidmode="Static" runat="server" for="AskPayPal">Ask users their PayPal account:</label>
                        <select id="AskPayPal" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ReqPayPalLabel" clientidmode="Static" runat="server" for="ReqPayPal">Require paypal account when users signup:</label>
                        <select id="ReqPayPal" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PostHideLabel" clientidmode="Static" runat="server" for="PostHide">Hide post links if user has no access:</label>
                        <select id="PostHide" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AskFriendsLabel" clientidmode="Static" runat="server" for="AskFriends">Ask users to approve friends:</label>
                        <select id="AskFriends" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module991" style="display: none">
                    <h2 runat="server" id="Module991Title">Administrator Information</h2>
                    <div class="mb-3">
                        <label id="AdminFullNameLabel" clientidmode="Static" runat="server" for="AdminFullName">Full name of the administrator:</label>
                        <input type="text" id="AdminFullName" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="AdminEmailAddressLabel" clientidmode="Static" runat="server" for="AdminEmailAddress">Administrator email address:</label>
                        <input type="text" id="AdminEmailAddress" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Your company name:</label>
                        <input type="text" id="CompanyName" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanySloganLabel" clientidmode="Static" runat="server" for="CompanySlogan">Your company slogan:</label>
                        <input type="text" id="CompanySlogan" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyPhoneLabel" clientidmode="Static" runat="server" for="CompanyPhone">Your company phone number:</label>
                        <input type="text" id="CompanyPhone" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyAddressLine1Label" clientidmode="Static" runat="server" for="CompanyAddressLine1">Your company street address:</label>
                        <input type="text" id="CompanyAddressLine1" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyCityLabel" clientidmode="Static" runat="server" for="CompanyCity">City your company is located in:</label>
                        <input type="text" id="CompanyCity" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyStateLabel" clientidmode="Static" runat="server" for="CompanyState">State your company is located in:</label>
                        <input type="text" id="CompanyState" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyZipCodeLabel" clientidmode="Static" runat="server" for="CompanyZipCode">Your company zip/postal code:</label>
                        <input type="text" id="CompanyZipCode" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CompanyCountryLabel" clientidmode="Static" runat="server" for="CompanyCountry">Your company country:</label>
                        <select id="CompanyCountry" runat="server" class="form-control">
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="TwitterUsernameLabel" clientidmode="Static" runat="server" for="TwitterUsername">Twitter User Name:</label>
                        <input type="text" id="TwitterUsername" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module64" style="display: none">
                    <h2 runat="server" id="Module64Title">Setup the Conference Center</h2>
                    <div class="mb-3">
                        <label id="ConferenceEnableLabel" clientidmode="Static" runat="server" for="ConferenceEnable">Enable the conference center:</label>
                        <select id="ConferenceEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ModeratorClassLabel" clientidmode="Static" runat="server" for="ModeratorClass">Select an access class for the moderator's:</label>
                        <sep:AccessClassDropdown ID="ModeratorClass" runat="server" Cssclass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="TwilioSMSReminderMsgLabel" clientidmode="Static" runat="server" for="TwilioSMSReminderMsg">SMS message reminder text (Max 150 Characters):</label>
                        <input type="text" id="TwilioSMSReminderMsg" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="TwilioSMSReminderOffsetLabel" clientidmode="Static" runat="server" for="TwilioSMSReminderOffset">Hours before call is scheduled to send an SMS message to the user:</label>
                        <input type="text" id="TwilioSMSReminderOffset" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PercentCelebritiesLabel" clientidmode="Static" runat="server" for="PercentCelebrities">Percentage to pay celebrities:</label>
                        <input type="text" id="PercentCelebrities" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module2" style="display: none">
                    <h2 runat="server" id="Module2Title">Setup the Advertisements</h2>
                    <div class="mb-3">
                        <label id="AdsEnableLabel" clientidmode="Static" runat="server" for="AdsEnable">Enable the banner advertisements:</label>
                        <select id="AdsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AdsStatsEnableLabel" clientidmode="Static" runat="server" for="AdsStatsEnable">Enable the statistics so advertisers can lookup their statistics:</label>
                        <select id="AdsStatsEnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SponsorsTargetLabel" clientidmode="Static" runat="server" for="SponsorsTarget">Target window to open the sponsors website in:</label>
                        <input type="text" id="SponsorsTarget" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module39" style="display: none">
                    <h2 runat="server" id="Module39Title">Setup the Affiliate Program</h2>
                    <div class="mb-3">
                        <label id="AffiliateEnableLabel" clientidmode="Static" runat="server" for="AffiliateEnable">Enable the affiliate module:</label>
                        <select id="AffiliateEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AffiliateLVL1Label" clientidmode="Static" runat="server" for="AffiliateLVL1">Level 1 Percentage:</label>
                        <input type="text" id="AffiliateLVL1" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="AffiliateLVL2Label" clientidmode="Static" runat="server" for="AffiliateLVL2">Level 2 Percentage:</label>
                        <input type="text" id="AffiliateLVL2" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" id="AffiliateSignupRow" runat="server">
                        <label id="AffiliateSignupLabel" clientidmode="Static" runat="server" for="AffiliateSignup">Allow users to enter affiliate in signup form:</label>
                        <select id="AffiliateSignup" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3" id="AffiliateIDReqRow" runat="server">
                        <label id="AffiliateIDReqLabel" clientidmode="Static" runat="server" for="AffiliateIDReq">Require affiliate to be entered on signup form:</label>
                        <select id="AffiliateIDReq" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AffiliateImageTextLabel" clientidmode="Static" runat="server" for="AffiliateImageText">Text to show on the affiliate image page. (HTML is supported):</label>
                        <textarea ID="AffiliateImageText" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="AffiliateReturnPageLabel" clientidmode="Static" runat="server" for="AffiliateReturnPage">Page to return users to when they click the affiliate image:</label>
                        <select id="AffiliateReturnPage" runat="server" class="form-control">
                        </select>
                    </div>
                </div>

                <div id="Module35" style="display: none">
                    <h2 runat="server" id="Module35Title">Setup the Articles</h2>
                    <div class="mb-3">
                        <label id="ArticlesEnableLabel" clientidmode="Static" runat="server" for="ArticlesEnable">Enable the articles module:</label>
                        <select id="ArticlesEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Articles10NewestLabel" clientidmode="Static" runat="server" for="Articles10Newest">Display the 10 newest articles:</label>
                        <select id="Articles10Newest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3" id="ArticleShowPicRow" runat="server">
                        <label id="ArticleShowPicLabel" clientidmode="Static" runat="server" for="ArticleShowPic">Show profile picture when displaying an article:</label>
                        <select id="ArticleShowPic" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ArticleShowSourceLabel" clientidmode="Static" runat="server" for="ArticleShowSource">Allow users to enter the article source information when submitting an article:</label>
                        <select id="ArticleShowSource" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ArticleShowMetaLabel" clientidmode="Static" runat="server" for="ArticleShowMeta">Allow users to fill out meta tags when submitting an article:</label>
                        <select id="ArticleShowMeta" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module31" style="display: none">
                    <h2 runat="server" id="Module31Title">Setup the Auctions</h2>
                    <div class="mb-3">
                        <label id="AuctionEnableLabel" clientidmode="Static" runat="server" for="AuctionEnable">Enable the auction:</label>
                        <select id="AuctionEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AuctionDisplayNewestLabel" clientidmode="Static" runat="server" for="AuctionDisplayNewest">Display the 10 newest ad postings:</label>
                        <select id="AuctionDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AuctionDeleteDaysLabel" clientidmode="Static" runat="server" for="AuctionDeleteDays">Enter the days till an ad expires:</label>
                        <input type="text" id="AuctionDeleteDays" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="AuctionEmailSubjectLabel" clientidmode="Static" runat="server" for="AuctionEmailSubject">Subject of the email when someone posts a new ad:</label>
                        <input type="text" id="AuctionEmailSubject" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="AuctionEmailBodyLabel" clientidmode="Static" runat="server" for="AuctionEmailBody">Body of the email message when someone posts a new ad:</label>
                        <textarea ID="AuctionEmailBody" runat="server"   class="form-control"></textarea>
                    </div>
                </div>

                <div id="Module61" style="display: none">
                    <h2 runat="server" id="Module61Title">Setup the Blogger</h2>
                    <div class="mb-3">
                        <label id="BlogsEnableLabel" clientidmode="Static" runat="server" for="BlogsEnable">Enable the blogger:</label>
                        <select id="BlogsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module20" style="display: none">
                    <h2 runat="server" id="Module20Title">Setup the Business Directory</h2>
                    <div class="mb-3">
                        <label id="BusinessEnableLabel" clientidmode="Static" runat="server" for="BusinessEnable">Enable the business directory:</label>
                        <select id="BusinessEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="BusinessDisplayNewestLabel" clientidmode="Static" runat="server" for="BusinessDisplayNewest">Display the 10 newest business on the main page:</label>
                        <select id="BusinessDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="BusinessClaimLabel" clientidmode="Static" runat="server" for="BusinessClaim">Allow users to claim listings. (Members must verify the contact email assigned to the business):</label>
                        <select id="BusinessClaim" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="BusinessUserAddressLabel" clientidmode="Static" runat="server" for="BusinessUserAddress">Get address information from the user instead of allowing a user to enter a business address:</label>
                        <select id="BusinessUserAddress" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module42" style="display: none">
                    <h2 runat="server" id="Module42Title">Setup the Chat Rooms</h2>
                    <div class="mb-3">
                        <label id="ChatEnableLabel" clientidmode="Static" runat="server" for="ChatEnable">Enable the chat rooms:</label>
                        <select id="ChatEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module6" style="display: none">
                    <h2 runat="server" id="Module6Title">Setup the Instant Messenger</h2>
                    <div class="mb-3">
                        <label id="IMessengerEnableLabel" clientidmode="Static" runat="server" for="IMessengerEnable">Enable the instant messenger:</label>
                        <select id="IMessengerEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module44" style="display: none">
                    <h2 runat="server" id="Module44Title">Setup the Classified Ads</h2>
                    <div class="mb-3">
                        <label id="ClassifiedEnableLabel" clientidmode="Static" runat="server" for="ClassifiedEnable">Enable the classified ads:</label>
                        <select id="ClassifiedEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ClassifiedDisplayNewestLabel" clientidmode="Static" runat="server" for="ClassifiedDisplayNewest">Display the 10 newest ad postings:</label>
                        <select id="ClassifiedDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ClassifiedDeleteDaysLabel" clientidmode="Static" runat="server" for="ClassifiedDeleteDays">Enter the days till an ad expires:</label>
                        <input type="text" id="ClassifiedDeleteDays" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ClassifiedEmailSubjectLabel" clientidmode="Static" runat="server" for="ClassifiedEmailSubject">Subject of the email when someone posts a new ad:</label>
                        <input type="text" id="ClassifiedEmailSubject" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ClassifiedEmailBodyLabel" clientidmode="Static" runat="server" for="ClassifiedEmailBody">Body of the email message when someone posts a new ad:</label>
                        <textarea ID="ClassifiedEmailBody" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="ClassifiedBuyLabel" clientidmode="Static" runat="server" for="ClassifiedBuy">Disable the buy now button:</label>
                        <select id="ClassifiedBuy" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module8" style="display: none">
                    <h2 runat="server" id="Module8Title">Setup the Comments & Ratings</h2>
                    <div class="mb-3">
                        <label id="CNRCEnableLabel" clientidmode="Static" runat="server" for="CNRCEnable">Enable the user comments:</label>
                        <select id="CNRCEnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="CNRREnableLabel" clientidmode="Static" runat="server" for="CNRREnable">Enable the user rating system:</label>
                        <select id="CNRREnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RGraphEnableLabel" clientidmode="Static" runat="server" for="RGraphEnable">Show the rating graph when viewing a listing:</label>
                        <select id="RGraphEnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module4" style="display: none">
                    <h2 runat="server" id="Module4Title">Setup the Contact Us</h2>
                    <div class="mb-3">
                        <label id="ContactEnableLabel" clientidmode="Static" runat="server" for="ContactEnable">Enable the contact page:</label>
                        <select id="ContactEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ContactEmailLabel" clientidmode="Static" runat="server" for="ContactEmail">Email address to send the contact form. (Leave blank to use the admin email address):</label>
                        <input type="text" id="ContactEmail" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ContactEmailSubjectLabel" clientidmode="Static" runat="server" for="ContactEmailSubject">Subject of the message that is sent to the user when they submit your contact form:</label>
                        <input type="text" id="ContactEmailSubject" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ContactEmailBodyLabel" clientidmode="Static" runat="server" for="ContactEmailBody">Body of the message that is sent to the user when they submit your form:</label>
                        <textarea ID="ContactEmailBody" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="ContactStreetAddressLabel" clientidmode="Static" runat="server" for="ContactStreetAddress">Ask users for their street address:</label>
                        <select id="ContactStreetAddress" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ContactAddressLabel" clientidmode="Static" runat="server" for="ContactAddress">Ask users for their city, state, and zipcode:</label>
                        <select id="ContactAddress" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ContactPhoneNumberLabel" clientidmode="Static" runat="server" for="ContactPhoneNumber">Ask users for their phone number:</label>
                        <select id="ContactPhoneNumber" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ContactFaxNumberLabel" clientidmode="Static" runat="server" for="ContactFaxNumber">Ask users for their fax number:</label>
                        <select id="ContactFaxNumber" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ContactFileTypesLabel" clientidmode="Static" runat="server" for="ContactFileTypes">File type users can upload on the contact form:</label>
                        <select id="ContactFileTypes" runat="server" class="form-control">
                            <option value="">Disabled</option>
                            <option value="Any">Any</option>
                            <option value="Audio">Audio</option>
                            <option value="Document">Document</option>
                            <option value="Images">Images</option>
                            <option value="Software">Software</option>
                            <option value="Video">Video</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ContactMaxFilesLabel" clientidmode="Static" runat="server" for="ContactMaxFiles">The maximum number of files a user can upload to the contact form:</label>
                        <input type="text" id="ContactMaxFiles" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module1" style="display: none">
                    <h2 runat="server" id="Module1Title">Setup the Content Rotator</h2>
                    <div class="mb-3">
                        <label id="CREnableLabel" clientidmode="Static" runat="server" for="CREnable">Enable the content rotator:</label>
                        <select id="CREnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module5" style="display: none">
                    <h2 runat="server" id="Module5Title">Setup the Discount System</h2>
                    <div class="mb-3">
                        <label id="DiscountsEnableLabel" clientidmode="Static" runat="server" for="DiscountsEnable">Enable the discount system:</label>
                        <select id="DiscountsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="DiscountsDisplayNewestLabel" clientidmode="Static" runat="server" for="DiscountsDisplayNewest">Display the top 10 newest coupons:</label>
                        <select id="DiscountsDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module37" style="display: none">
                    <h2 runat="server" id="Module37Title">Setup the E-Learning</h2>
                    <div class="mb-3">
                        <label id="ELearningEnableLabel" clientidmode="Static" runat="server" for="ELearningEnable">Enable the E-Learning:</label>
                        <select id="ELearningEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ELearningNewestLabel" clientidmode="Static" runat="server" for="ELearningNewest">Show 10 newest courses:</label>
                        <select id="ELearningNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module46" style="display: none">
                    <h2 runat="server" id="Module46Title">Setup the Event Calendar</h2>
                    <div class="mb-3">
                        <label id="EventsEnableLabel" clientidmode="Static" runat="server" for="EventsEnable">Enable the event calendar:</label>
                        <select id="EventsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3" runat="server" id="EventsSMSRow">
                        <label id="EventsSMSRemindersLabel" clientidmode="Static" runat="server" for="EventsSMSReminders">Allow users to receive SMS reminders for events:</label>
                        <select id="EventsSMSReminders" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module9" style="display: none">
                    <h2 runat="server" id="Module9Title">Setup the Frequency Asked Questions</h2>
                    <div class="mb-3">
                        <label id="FAQEnableLabel" clientidmode="Static" runat="server" for="FAQEnable">Enable the frequency asked questions:</label>
                        <select id="FAQEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="FAQNewestLabel" clientidmode="Static" runat="server" for="FAQNewest">Show 10 newest FAQ's:</label>
                        <select id="FAQNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module10" style="display: none">
                    <h2 runat="server" id="Module10Title">Setup the Downloads</h2>
                    <div class="mb-3">
                        <label id="LibraryEnableLabel" clientidmode="Static" runat="server" for="LibraryEnable">Enable the Downloads:</label>
                        <select id="LibraryEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="LibraryDisplayNewestLabel" clientidmode="Static" runat="server" for="LibraryDisplayNewest">Display the 10 newest file uploads on your main file library page:</label>
                        <select id="LibraryDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                            <option value="Audio">Audio Only</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="LibraryDisplayPopularLabel" clientidmode="Static" runat="server" for="LibraryDisplayPopular">Display the most popular file downloads on your main file library page:</label>
                        <select id="LibraryDisplayPopular" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                            <option value="Audio">Audio Only</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="LibraryMaxUploadLabel" clientidmode="Static" runat="server" for="LibraryMaxUpload">Maximum upload size in the Downloads. (MB):</label>
                        <input type="text" id="LibraryMaxUpload" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="LibraryDownloadLabel" clientidmode="Static" runat="server" for="LibraryDownload">Force users to download audio/video files?:</label>
                        <select id="LibraryDownload" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module13" style="display: none">
                    <h2 runat="server" id="Module13Title">Setup the Forms</h2>
                    <div class="mb-3">
                        <label id="FormsEnableLabel" clientidmode="Static" runat="server" for="FormsEnable">Enable the forms module:</label>
                        <select id="FormsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3" id="FormsSignupRow" runat="server">
                        <label id="FormsSignupLabel" clientidmode="Static" runat="server" for="FormsSignup">Form for users to fill out when signing up:</label>
                        <select id="FormsSignup" runat="server" class="form-control">
                            <option value="">Disabled</option>
                        </select>
                    </div>
                </div>

                <div id="Module12" style="display: none">
                    <h2 runat="server" id="Module12Title">Setup the Forums</h2>
                    <div class="mb-3">
                        <label id="ForumsEnableLabel" clientidmode="Static" runat="server" for="ForumsEnable">Enable the forums:</label>
                        <select id="ForumsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ForumsDeleteDaysLabel" clientidmode="Static" runat="server" for="ForumsDeleteDays">Days till old messages get deleted:</label>
                        <input type="text" id="ForumsDeleteDays" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ForumsDisplayNewestLabel" clientidmode="Static" runat="server" for="ForumsDisplayNewest">Display the 10 newest forum postings on the main forums page:</label>
                        <select id="ForumsDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ForumsUsersEditLabel" clientidmode="Static" runat="server" for="ForumsUsersEdit">Enable your users to edit their posts:</label>
                        <select id="ForumsUsersEdit" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ForumsAttachmentLabel" clientidmode="Static" runat="server" for="ForumsAttachment">Allow users to upload an attachment in their topic:</label>
                        <select id="ForumsAttachment" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module14" style="display: none">
                    <h2 runat="server" id="Module14Title">Setup the Guestbook</h2>
                    <div class="mb-3">
                        <label id="GuestbookEnableLabel" clientidmode="Static" runat="server" for="GuestbookEnable">Enable the guestbook:</label>
                        <select id="GuestbookEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="GuestbookDeleteDaysLabel" clientidmode="Static" runat="server" for="GuestbookDeleteDays">Days till old messages get deleted:</label>
                        <input type="text" id="GuestbookDeleteDays" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module57" style="display: none">
                    <h2 runat="server" id="Module57Title">Setup the Horoscopes</h2>
                    <div class="mb-3">
                        <label id="HoroscopesEnableLabel" clientidmode="Static" runat="server" for="HoroscopesEnable">Enable the horoscopes:</label>
                        <select id="HoroscopesEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module40" style="display: none">
                    <h2 runat="server" id="Module40Title">Setup the Hot or Not</h2>
                    <div class="mb-3">
                        <label id="HotNotEnableLabel" clientidmode="Static" runat="server" for="HotNotEnable">Enable the hot or not:</label>
                        <select id="HotNotEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3" id="HotNotProfilesRow" runat="server">
                        <label id="HotNotProfilesLabel" clientidmode="Static" runat="server" for="HotNotProfiles">Allow pictures from the user profiles:</label>
                        <select id="HotNotProfiles" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module989A" style="display: none">
                    <h2 runat="server" id="Module989ATitle">Setup Facebook</h2>
                    <div class="mb-3">
                        <label id="FacebookAPIKeyLabel" clientidmode="Static" runat="server" for="FacebookAPIKey">Your Facebook App ID (Leave blank to disable):<br />Get the API Key by signing up <a href="https://developers.facebook.com/apps" target="_blank">here</a>.</label>
                        <input type="text" id="FacebookAPIKey" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989B" style="display: none">
                    <h2 runat="server" id="Module989BTitle">Setup Google reCAPTCHA</h2>
                    <div class="mb-3">
                        <label id="GooglereCAPTCHAPublicKeyLabel" clientidmode="Static" runat="server" for="GooglereCAPTCHAPublicKey">Google reCAPTCHA Public Key (Leave blank to disable):<br />Get the API Key by signing up <a href="http://www.google.com/recaptcha" target="_blank">here</a>.</label>
                        <input type="text" id="GooglereCAPTCHAPublicKey" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="GooglereCAPTCHAPrivateKeyLabel" clientidmode="Static" runat="server" for="GooglereCAPTCHAPrivateKey">Google reCAPTCHA Private Key (Leave blank to disable):<br />Get the API Key by signing up <a href="http://www.google.com/recaptcha" target="_blank">here</a>.</label>
                        <input type="text" id="GooglereCAPTCHAPrivateKey" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989C" style="display: none">
                    <h2 runat="server" id="H2">Setup Google Analytics</h2>
                    <div class="mb-3">
                        <label id="AnalyticsIDLabel" clientidmode="Static" runat="server" for="GoogleAnalyticsID">Analytics ID (Leave blank to disable):<br />Get the ID by going <a href="https://analytics.google.com/analytics/web/#home/" target="_blank">here</a>. The Analytics ID will be the letters and numbers next to your web site name after you create your site. (ex. XX-55555555-5)</label>
                        <input type="text" id="GoogleAnalyticsID" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="GoogleAnalyticsClientIDLabel" clientidmode="Static" runat="server" for="GoogleAnalyticsClientID">Analytics Client ID (Leave blank to disable):<br />Get the Client ID by going <a href="https://console.developers.google.com" target="_blank">here</a> and clicking "Enable API" and select Google Analytics to generate a Client ID.</label>
                        <input type="text" id="GoogleAnalyticsClientID" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989J" style="display: none">
                    <h2 runat="server" id="Module989JTitle">Setup Google Maps</h2>
                    <div class="mb-3">
                        <label id="GoogleMapsAPILabel" clientidmode="Static" runat="server" for="GoogleMapsAPI">Maps API Key (Leave blank to disable):<br />Get the API Key by going <a href="https://developers.google.com/maps/documentation/javascript/get-api-key" target="_blank">here</a>.</label>
                        <input type="text" id="GoogleMapsAPI" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989L" style="display: none">
                    <h2 runat="server" id="Module989LTitle">Setup PayPal Business</h2>
                    <div class="mb-3">
                        <label id="PayPalClientIDLabel" clientidmode="Static" runat="server" for="PayPalClientID">Your PayPal Client ID (Leave blank to disable):<br />Get the Client ID by signing up <a href="https://developer.paypal.com/home" target="_blank">here</a>.</label>
                        <input type="text" id="PayPalClientID" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PayPalSecretLabel" clientidmode="Static" runat="server" for="PayPalSecret">Your PayPal Secret (Leave blank to disable):<br />Get the Secret by signing up <a href="https://developer.paypal.com/home" target="_blank">here</a>.</label>
                        <input type="text" id="PayPalSecret" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989D" style="display: none">
                    <h2 runat="server" id="Module989CTitle">Setup Twilio</h2>
                    <div class="mb-3">
                        <label id="TwilioAccountSIDLabel" clientidmode="Static" runat="server" for="TwilioAccountSID">Twilio API Account SID:<br />Get the Account SID by signing up <a href="http://www.twillio.com/" target="_blank">here</a>.</label>
                        <input type="text" id="TwilioAccountSID" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="TwilioAuthTokenLabel" clientidmode="Static" runat="server" for="TwilioAuthToken">Twilio API Auth Token:</label>
                        <input type="text" id="TwilioAuthToken" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="TwilioPhoneNumberLabel" clientidmode="Static" runat="server" for="TwilioPhoneNumber">Twilio phone number to send calls from (ex. +14445556666):</label>
                        <input type="text" id="TwilioPhoneNumber" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="TwilioVideoSIDLabel" clientidmode="Static" runat="server" for="TwilioVideoSID">Twilio Video API Key SID:</label>
                        <input type="text" id="TwilioVideoSID" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="TwilioVideoSecretLabel" clientidmode="Static" runat="server" for="TwilioVideoSecret">Twilio Video API Key Secret:</label>
                        <input type="text" id="TwilioVideoSecret" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989E" style="display: none">
                    <h2 runat="server" id="Module989DTitle">Setup CloudFlare</h2>
                    <div class="mb-3" id="CloudFlareAPIRow" runat="server">
                        <label id="CloudFlareAPILabel" clientidmode="Static" runat="server" for="CloudFlareAPI">CloudFlare API Key:<br />Get the API Key by signing up <a href="https://www.cloudflare.com/" target="_blank">here</a>.</label>
                        <input type="text" id="CloudFlareAPI" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" id="CloudFlareEmailRow" runat="server">
                        <label id="CloudFlareEmailLabel" clientidmode="Static" runat="server" for="CloudFlareEmail">CloudFlare Email Address:</label>
                        <input type="text" id="CloudFlareEmail" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" id="CloudFlareDomainRow" runat="server">
                        <label id="CloudFlareDomainLabel" clientidmode="Static" runat="server" for="CloudFlareDomain">CloudFlare Domain Name:</label>
                        <input type="text" id="CloudFlareDomain" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989F" style="display: none">
                    <h2 runat="server" id="Module989ETitle">Setup LinkedIn</h2>
                    <div class="mb-3">
                        <label id="LinkedInLabel" clientidmode="Static" runat="server" for="LinkedInAPI">LinkedIn API Key:<br />Get the API Key by going <a href="https://www.linkedin.com/developer/apps" target="_blank">here</a>.</label>
                        <input type="text" id="LinkedInAPI" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="LinkedInSecretLabel" clientidmode="Static" runat="server" for="LinkedInSecret">LinkedIn Secret:</label>
                        <input type="text" id="LinkedInSecret" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989G" style="display: none">
                    <h2 runat="server" id="Module989GTitle">Setup FedEx</h2>
                    Get the API Key by going <a href="http://www.fedex.com/us/developer/" target="_blank">here</a>.
                    <div class="mb-3">
                        <label id="FedExAccountNumLabel" clientidmode="Static" runat="server" for="FedExAccountNum">FedEx Account Number:</label>
                        <input type="text" id="FedExAccountNum" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="FedExMeterNumLabel" clientidmode="Static" runat="server" for="FedExMeterNum">FedEx Meter Number:</label>
                        <input type="text" id="FedExMeterNum" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="FedExServiceKeyLabel" clientidmode="Static" runat="server" for="FedExServiceKey">FedEx Service Key:</label>
                        <input type="text" id="FedExServiceKey" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="FedExServicePassLabel" clientidmode="Static" runat="server" for="FedExServicePass">FedEx Service Password:</label>
                        <input type="text" id="FedExServicePass" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989H" style="display: none">
                    <h2 runat="server" id="Module989HTitle">Setup UPS</h2>
                    Get the API Key by going <a href="https://www.ups.com/upsdeveloperkit" target="_blank">here</a>.
                    <div class="mb-3">
                        <label id="UPSAccountNumLabel" clientidmode="Static" runat="server" for="UPSAccountNum">UPS Access Key:</label>
                        <input type="text" id="UPSAccountNum" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPSUserNameLabel" clientidmode="Static" runat="server" for="UPSUserName">UPS User Name:</label>
                        <input type="text" id="UPSUserName" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPSPasswordLabel" clientidmode="Static" runat="server" for="UPSPassword">UPS Password:</label>
                        <input type="text" id="UPSPassword" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPSShipperNumLabel" clientidmode="Static" runat="server" for="UPSShipperNum">UPS Account:</label>
                        <input type="text" id="UPSShipperNum" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989I" style="display: none">
                    <h2 runat="server" id="Module989ITitle">Setup USPS</h2>
                    Get the API Key by going <a href="https://www.usps.com/business/web-tools-apis/welcome.htm" target="_blank">here</a>.
                    <div class="mb-3">
                        <label id="USPSUserIDLabel" clientidmode="Static" runat="server" for="USPSUserID">USPS User ID:</label>
                        <input type="text" id="USPSUserID" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module989K" style="display: none">
                    <h2 runat="server" id="Module989KTitle">Setup Zoom</h2>
                    Get the API Key by going <a href="https://marketplace.zoom.us/" target="_blank">here</a>.
                    <div class="mb-3">
                        <label id="ZoomAPIKeyLabel" clientidmode="Static" runat="server" for="ZoomAPIKey">Zoom API Key:</label>
                        <input type="text" id="ZoomAPIKey" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ZoomAPISecretLabel" clientidmode="Static" runat="server" for="ZoomAPISecret">Zoom API Secret:</label>
                        <input type="text" id="ZoomAPISecret" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module68" style="display: none">
                    <h2 runat="server" id="Module68Title">LDAP Integration</h2>
                    <div class="mb-3">
                        <label id="LDAPPathLabel" clientidmode="Static" runat="server" for="LDAPPath">LDAP Path (ex. LDAP://mypath.com/DC=mypath,DC=com):</label>
                        <input type="text" id="LDAPPath" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="LDAPDomainLabel" clientidmode="Static" runat="server" for="LDAPDomain">LDAP Domain (ex. mypath):</label>
                        <input type="text" id="LDAPDomain" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        After you have entered valid information above and pressed the save button then click the Sync button below to sync your LDAP users and groups.
                    </div>
                    <div class="mb-3">
                        <asp:Button id="SyncLDAP" runat="server" text="Sync" onclick="LDAP_Sync_Click" />
                    </div>
                </div>

                <div id="Module69" style="display: none">
                    <h2 runat="server" id="Module69Title">Video Conferencing</h2>
                    <div class="mb-3">
                        <label id="VideoConferenceEnableLabel" clientidmode="Static" runat="server" for="VideoConferenceEnable">Enable the Video Conferencing</label>
                        <select id="VideoConferenceEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="VideoConferenceSendSMSLabel" clientidmode="Static" runat="server" for="VideoConferenceSendSMS">Send SMS Message when a new meeting is created. (Requires Twilio to be configured)</label>
                        <select id="VideoConferenceSendSMS" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="VideoConferenceSMSOffsetLabel" clientidmode="Static" runat="server" for="VideoConferenceSMSOffset">Minutes before a meeting to send SMS reminder ("0" to disable)</label>
                        <input type="text" id="VideoConferenceSMSOffset" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module70" style="display: none">
                    <h2 runat="server" id="Module70Title">Radius/IP2Location Integration</h2>
                    <div class="mb-3">
                        <label id="SepCityAPIKeyLabel" clientidmode="Static" runat="server" for="SepCityAPIKey">SepCity API Key, get it by logging into <a href="http://new.sepcity.com/account/api_key.aspx" target="_blank">SepCity</a>:</label>
                        <input type="text" id="SepCityAPIKey" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SepCityUserLabel" clientidmode="Static" runat="server" for="SepCityUser">SepCity User Name:</label>
                        <input type="text" id="SepCityUser" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SepCityPasswordLabel" clientidmode="Static" runat="server" for="SepCityPassword">SepCity Password:</label>
                        <input type="text" id="SepCityPassword" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module67" style="display: none">
                    <h2 runat="server" id="Module67Title">Setup CRM/Support</h2>
                    <div class="mb-3">
                        <label id="CRMVersionLabel" clientidmode="Static" runat="server" for="CRMVersion">Select which CRM/Support system you wish to use:</label>
                        <select id="CRMVersion" runat="server" class="form-control" ClientIDMode="Static" onchange="changeSupport();">
                            <option value="Disabled">Disabled</option>
                            <option value="SmarterTrack">SmarterTrack</option>
                            <option value="SugarCRM">SugarCRM</option>
                            <option value="SuiteCRM">SuiteCRM</option>
                            <option value="WHMCS">WHMCS</option>
                        </select>
                    </div>
                    <div id="Module67A" runat="server" ClientIDMode="Static">
                        <div class="mb-3">
                            <label id="STURLLabel" clientidmode="Static" runat="server" for="STURL">SmarterTrack URL (ex. http://www.domain.com/support/):</label>
                            <input type="text" id="STURL" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="STUserLabel" clientidmode="Static" runat="server" for="STUser">SmarterTrack Admin User Name:</label>
                            <input type="text" id="STUser" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="STPassLabel" clientidmode="Static" runat="server" for="STPass">SmarterTrack Admin Password:</label>
                            <input type="text" id="STPass" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="STKBEnableLabel" clientidmode="Static" runat="server" for="STKBEnable">Embed the KnowledgeBase on your site?:</label>
                            <select id="STKBEnable" runat="server" class="form-control">
                                <option value="Yes">Yes</option>
                                <option value="No">No</option>
                            </select>
                        </div>
                        <div class="mb-3">
                            <label id="STKBNewsEnableLabel" clientidmode="Static" runat="server" for="STKBNewsEnable">Embed the News feeds on your site?:</label>
                            <select id="STKBNewsEnable" runat="server" class="form-control">
                                <option value="Yes">Yes</option>
                                <option value="No">No</option>
                            </select>
                        </div>
                        <div class="mb-3">
                            <label id="STDatabaseLabel" clientidmode="Static" runat="server" for="STDatabase">When do you want to add users to the SmarterTrack database?:</label>
                            <select id="STDatabase" runat="server" class="form-control">
                                <option value="NewTicket">When submit a support ticket</option>
                                <option value="NewSignup">When new users signup</option>
                                <option value="NewOrder">When users place an order</option>
                            </select>
                        </div>
                    </div>
                    <div id="Module67B" runat="server" ClientIDMode="Static">
                        <div class="mb-3">
                            <label id="SugarCRMURLLabel" clientidmode="Static" runat="server" for="SugarCRMURL">SugarCRM URL (ex. http://www.domain.com/support/):</label>
                            <input type="text" id="SugarCRMURL" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="SugarCRMUserLabel" clientidmode="Static" runat="server" for="SugarCRMUser">SugarCRM Admin User Name:</label>
                            <input type="text" id="SugarCRMUser" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="SugarCRMPassLabel" clientidmode="Static" runat="server" for="SugarCRMPass">SugarCRM Admin Password:</label>
                            <input type="text" id="SugarCRMPass" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="SugarCRMDatabaseLabel" clientidmode="Static" runat="server" for="SugarCRMDatabase">When do you want to add users to the SugarCRM database?:</label>
                            <select id="SugarCRMDatabase" runat="server" class="form-control">
                                <option value="NewTicket">When submit a support ticket</option>
                                <option value="NewSignup">When new users signup</option>
                                <option value="NewOrder">When users place an order</option>
                            </select>
                        </div>
                    </div>
                    <div id="Module67C" runat="server" ClientIDMode="Static">
                        <div class="mb-3">
                            <label id="SuiteCRMURLLabel" clientidmode="Static" runat="server" for="SuiteCRMURL">SuiteCRM URL (ex. http://www.domain.com/support/):</label>
                            <input type="text" id="SuiteCRMURL" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="SuiteCRMUserLabel" clientidmode="Static" runat="server" for="SuiteCRMUser">SuiteCRM Admin User Name:</label>
                            <input type="text" id="SuiteCRMUser" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="SuiteCRMPassLabel" clientidmode="Static" runat="server" for="SuiteCRMPass">SuiteCRM Admin Password:</label>
                            <input type="text" id="SuiteCRMPass" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="SuiteCRMDatabaseLabel" clientidmode="Static" runat="server" for="SuiteCRMDatabase">When do you want to add users to the SuiteCRM database?:</label>
                            <select id="SuiteCRMDatabase" runat="server" class="form-control">
                                <option value="NewTicket">When submit a support ticket</option>
                                <option value="NewSignup">When new users signup</option>
                                <option value="NewOrder">When users place an order</option>
                            </select>
                        </div>
                    </div>
                    <div id="Module67D" runat="server" ClientIDMode="Static">
                        <div class="mb-3">
                            <label id="WHMCSURLLabel" clientidmode="Static" runat="server" for="WHMCSURL">WHMCS URL (ex. http://www.domain.com/support/):</label>
                            <input type="text" id="WHMCSURL" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="WHMCSUserLabel" clientidmode="Static" runat="server" for="WHMCSUser">WHMCS Admin User Name:</label>
                            <input type="text" id="WHMCSUser" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="WHMCSPassLabel" clientidmode="Static" runat="server" for="WHMCSPass">WHMCS Admin Password:</label>
                            <input type="text" id="WHMCSPass" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label id="WHMCSKBLabel" clientidmode="Static" runat="server" for="WHMCSKB">Embed the KnowledgeBase on your site?:</label>
                            <select id="WHMCSKB" runat="server" class="form-control">
                                <option value="Yes">Yes</option>
                                <option value="No">No</option>
                            </select>
                        </div>
                        <div class="mb-3">
                            <label id="WHMCSDatabaseLabel" clientidmode="Static" runat="server" for="WHMCSDatabase">When do you want to add users to the WHMCS database?:</label>
                            <select id="WHMCSDatabase" runat="server" class="form-control">
                                <option value="NewTicket">When submit a support ticket</option>
                                <option value="NewSignup">When new users signup</option>
                                <option value="NewOrder">When users place an order</option>
                            </select>
                        </div>
                    </div>
                </div>

                <div id="Module56" style="display: none">
                    <h2 runat="server" id="Module56Title">Setup the International News</h2>
                    <div class="mb-3">
                        <label id="INewsEnableLabel" clientidmode="Static" runat="server" for="INewsEnable">Enable the international news:</label>
                        <select id="INewsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module19" style="display: none">
                    <h2 runat="server" id="Module19Title">Setup the Links</h2>
                    <div class="mb-3">
                        <label id="LinksEnableLabel" clientidmode="Static" runat="server" for="LinksEnable">Enable the links page:</label>
                        <select id="LinksEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="LinksDisplayNewestLabel" clientidmode="Static" runat="server" for="LinksDisplayNewest">Display the 10 newest links on your links page:</label>
                        <select id="LinksDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module18" style="display: none">
                    <h2 runat="server" id="Module18Title">Setup the Match Maker</h2>
                    <div class="mb-3">
                        <label id="MatchEnableLabel" clientidmode="Static" runat="server" for="MatchEnable">Enable the match maker:</label>
                        <select id="MatchEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="MatchPicNoLabel" clientidmode="Static" runat="server" for="MatchPicNo">Number of pictures a user can upload:</label>
                        <input type="text" id="MatchPicNo" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module17" style="display: none">
                    <h2 runat="server" id="Module17Title">Setup the Messenger</h2>
                    <div class="mb-3">
                        <label id="MessengerEnableLabel" clientidmode="Static" runat="server" for="MessengerEnable">Enable the messenger:</label>
                        <select id="MessengerEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="MessengerDeleteDaysLabel" clientidmode="Static" runat="server" for="MessengerDeleteDays">Days till old messages gets deleted:</label>
                        <input type="text" id="MessengerDeleteDays" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" runat="server" id="MessengerSMSRow">
                        <label id="MessengerSMSLabel" clientidmode="Static" runat="server" for="MessengerSMS">Allow users to receive an SMS message when they receive a new message:</label>
                        <select id="MessengerSMS" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module23" style="display: none">
                    <h2 runat="server" id="Module23Title">Setup the News</h2>
                    <div class="mb-3">
                        <label id="NewsEnableLabel" clientidmode="Static" runat="server" for="NewsEnable">Enable the news:</label>
                        <select id="NewsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="NewsDeleteDaysLabel" clientidmode="Static" runat="server" for="NewsDeleteDays">Days till old news gets deleted:</label>
                        <input type="text" id="NewsDeleteDays" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module24" style="display: none">
                    <h2 runat="server" id="Module24Title">Setup the Newsletters</h2>
                    <div class="mb-3">
                        <label id="NewsLetEnableLabel" clientidmode="Static" runat="server" for="NewsLetEnable">Enable the newsletters:</label>
                        <select id="NewsLetEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="NewsletRemoveTextLabel" clientidmode="Static" runat="server" for="NewsletRemoveText">Removal text to display on the bottom of your newsletters:</label>
                        <input type="text" id="NewsletRemoveText" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="NewsletFromEmailLabel" clientidmode="Static" runat="server" for="NewsletFromEmail">Email to send newsletters from. (One email per a line):</label>
                        <textarea ID="NewsletFromEmail" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="NewsletNightlyEnableLabel" clientidmode="Static" runat="server" for="NewsletNightlyEnable">Enable having a nightly newsletter:</label>
                        <select id="NewsletNightlyEnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="NewsletNightlyNewsLabel" clientidmode="Static" runat="server" for="NewsletNightlyNews">Select a newsletter to send to:</label>
                        <select id="NewsletNightlyNews" runat="server" class="form-control">
                            <option value="">Select a Newsletter</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="NewsletNightlySubjectLabel" clientidmode="Static" runat="server" for="NewsletNightlySubject">Subject of the email:</label>
                        <input type="text" id="NewsletNightlySubject" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="NewsletNightlyBodyLabel" clientidmode="Static" runat="server" for="NewsletNightlyBody">Body of the email:</label>
                        <sep:WYSIWYGEditor runat="server" id="NewsletNightlyBody" Mode="advanced" Width="99%" Height="450" RelativeURLs="true" />
                    </div>
                </div>

                <div id="Module47" style="display: none">
                    <h2 runat="server" id="Module47Title">Setup the Online Games</h2>
                    <div class="mb-3">
                        <label id="GamesEnableLabel" clientidmode="Static" runat="server" for="GamesEnable">Enable the online games:</label>
                        <select id="GamesEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module28" style="display: none">
                    <h2 runat="server" id="Module28Title">Setup the Photo Albums</h2>
                    <div class="mb-3">
                        <label id="PhotosEnableLabel" clientidmode="Static" runat="server" for="PhotosEnable">Enable the photo albums:</label>
                        <select id="PhotosEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PhotoNumberLabel" clientidmode="Static" runat="server" for="PhotoNumber">Number of photos a user to upload per an album:</label>
                        <input type="text" id="PhotoNumber" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module25" style="display: none">
                    <h2 runat="server" id="Module25Title">Setup the Polls</h2>
                    <div class="mb-3">
                        <label id="PollsEnableLabel" clientidmode="Static" runat="server" for="PollsEnable">Enable the polls:</label>
                        <select id="PollsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module60" style="display: none">
                    <h2 runat="server" id="Module60Title">Setup the Portals</h2>
                    <div class="mb-3">
                        <label id="PortalsEnableLabel" clientidmode="Static" runat="server" for="PortalsEnable">Enable the portals:</label>
                        <select id="PortalsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PortalMasterDomainLabel" clientidmode="Static" runat="server" for="PortalMasterDomain">Master site domain name. (EX: domain.com):</label>
                        <input type="text" id="PortalMasterDomain" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" id="PortalProfilesRow" runat="server">
                        <label id="PortalProfilesLabel" clientidmode="Static" runat="server" for="PortalProfiles">Share user profiles among all portals:</label>
                        <select id="PortalProfiles" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PortalSiteLooksLabel" clientidmode="Static" runat="server" for="PortalSiteLooks">Enable the Site Templates to portal administrators:</label>
                        <select id="PortalSiteLooks" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module32" style="display: none">
                    <h2 runat="server" id="Module32Title">Setup the Real Estate</h2>
                    <div class="mb-3">
                        <label id="RStateEnableLabel" clientidmode="Static" runat="server" for="RStateEnable">Enable the real estate module:</label>
                        <select id="RStateEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RStateDisplayNewestLabel" clientidmode="Static" runat="server" for="RStateDisplayNewest">Display the 10 newest properties:</label>
                        <select id="RStateDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RStateNewestRentLabel" clientidmode="Static" runat="server" for="RStateNewestRent">Display the 10 newest properties for rent:</label>
                        <select id="RStateNewestRent" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RStateNewestSaleLabel" clientidmode="Static" runat="server" for="RStateNewestSale">Display the 10 newest properties for sale:</label>
                        <select id="RStateNewestSale" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RStateStateDropLabel" clientidmode="Static" runat="server" for="RStateStateDrop">Show the State/Province dropdown:</label>
                        <select id="RStateStateDrop" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RStateCountryDropLabel" clientidmode="Static" runat="server" for="RStateCountryDrop">Show the Country dropdown:</label>
                        <select id="RStateCountryDrop" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RStateMaxPhotosLabel" clientidmode="Static" runat="server" for="RStateMaxPhotos">Max photos a user can upload per a property:</label>
                        <input type="text" id="RStateMaxPhotos" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module43" style="display: none">
                    <h2 runat="server" id="Module43Title">Setup the Refer a Friend</h2>
                    <div class="mb-3">
                        <label id="ReferEnableLabel" clientidmode="Static" runat="server" for="ReferEnable">Enable the refer a friend module:</label>
                        <select id="ReferEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ReferDisplayStatsLabel" clientidmode="Static" runat="server" for="ReferDisplayStats">Enable the referral statistics:</label>
                        <select id="ReferDisplayStats" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ReferTopLabel" clientidmode="Static" runat="server" for="ReferTop">Show the refer a friend on the top menu for each module:</label>
                        <select id="ReferTop" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ReferEmailSubjectLabel" clientidmode="Static" runat="server" for="ReferEmailSubject">Subject of the email a user will see when they have been referred to your site:</label>
                        <input type="text" id="ReferEmailSubject" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ReferEmailBodyLabel" clientidmode="Static" runat="server" for="ReferEmailBody">Body of the email message that a user will see when they have been referred to your site:</label>
                        <textarea ID="ReferEmailBody" runat="server"   class="form-control"></textarea>
                    </div>
                </div>

                <div id="Module3" style="display: none">
                    <h2 runat="server" id="Module3Title">Setup the Search Engine</h2>
                    <div class="mb-3">
                        <label id="SearchModulesLabel" clientidmode="Static" runat="server" for="SearchModules">Allow searching through individual modules:</label>
                        <select id="SearchModules" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SearchRadiusLabel" clientidmode="Static" runat="server" for="SearchRadius">Enable the radius searching:</label>
                        <select id="SearchRadius" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SearchCountryLabel" clientidmode="Static" runat="server" for="SearchCountry">Allow searching by country:</label>
                        <select id="SearchCountry" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module995" style="display: none">
                    <h2 runat="server" id="Module995Title">Setup the Shopping Cart</h2>
                    <div class="mb-3">
                        <label id="TaxCalcEnableLabel" clientidmode="Static" runat="server" for="TaxCalcEnable">Enable the tax calculator:</label>
                        <select id="TaxCalcEnable" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="OrderFinalTextLabel" clientidmode="Static" runat="server" for="OrderFinalText">Text in the shopping cart when an order is being processed. (HTML Supported):</label>
                        <textarea ID="OrderFinalText" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="EmailTempAdminLabel" clientidmode="Static" runat="server" for="EmailTempAdmin">Email Template to send the administrator when an order has been successfully processed:</label>
                        <select id="EmailTempAdmin" runat="server" class="form-control">
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="EmailTempCustLabel" clientidmode="Static" runat="server" for="EmailTempCust">Email Template to send the customer when an order has been successfully processed:</label>
                        <select id="EmailTempCust" runat="server" class="form-control">
                        </select>
                    </div>
                </div>

                <div id="Module41" style="display: none">
                    <h2 runat="server" id="Module41Title">Setup the Shopping Mall</h2>
                    <div class="mb-3">
                        <label id="ShopMallEnableLabel" clientidmode="Static" runat="server" for="ShopMallEnable">Enable the shopping mall:</label>
                        <select id="ShopMallEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ShopMallWishListLabel" clientidmode="Static" runat="server" for="ShopMallWishList">Enable the wish list:</label>
                        <select id="ShopMallWishList" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ShopMallSalesPageLabel" clientidmode="Static" runat="server" for="ShopMallSalesPage">Enable current sales page:</label>
                        <select id="ShopMallSalesPage" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ShopMallElectronicLabel" clientidmode="Static" runat="server" for="ShopMallElectronic">Electronic email header:</label>
                        <input type="text" id="ShopMallElectronic" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ShopMallDisplayNewestLabel" clientidmode="Static" runat="server" for="ShopMallDisplayNewest">Display the 10 newest products on your main shopping mall page:</label>
                        <select id="ShopMallDisplayNewest" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module997" style="display: none" runat="server" ClientIDMode="Static">
                    <h2 runat="server" id="Module997Title">Setup the Signup Setup</h2>
                    <div class="mb-3">
                        <label id="StartupClassLabel" clientidmode="Static" runat="server" for="StartupClass">Class to start off new users in:</label>
                        <select id="StartupClass" runat="server" class="form-control">
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="EmailAdminNewLabel" clientidmode="Static" runat="server" for="EmailAdminNew">Email address to send new signups to. (Leave blank to disable):</label>
                        <input type="text" id="EmailAdminNew" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SignupVerifyLabel" clientidmode="Static" runat="server" for="SignupVerify">Send users an email to verify their account:</label>
                        <select id="SignupVerify" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SignupAgreementLabel" clientidmode="Static" runat="server" for="SignupAgreement">Agreement before signup. (Leave blank to disable):</label>
                        <textarea ID="SignupAgreement" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="SignupAdminAppLabel" clientidmode="Static" runat="server" for="SignupAdminApp">The administrator has to approve accounts:</label>
                        <select id="SignupAdminApp" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SignupAgeLabel" clientidmode="Static" runat="server" for="SignupAge">Age a new member must be to signup:</label>
                        <input type="text" id="SignupAge" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" id="LoginEmailRow" runat="server">
                        <label id="LoginEmailLabel" clientidmode="Static" runat="server" for="LoginEmail">Have users login with their email address instead of their username:</label>
                        <select id="LoginEmail" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SignupAREmailLabel" clientidmode="Static" runat="server" for="SignupAREmail">Email address that the auto respond email comes from:</label>
                        <input type="text" id="SignupAREmail" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SignupARSubjectLabel" clientidmode="Static" runat="server" for="SignupARSubject">Auto respond email subject when someone signs up:</label>
                        <input type="text" id="SignupARSubject" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SignupARBodyLabel" clientidmode="Static" runat="server" for="SignupARBody">Auto respond email body when someone signs up:</label>
                        <textarea ID="SignupARBody" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="SignupPageLabel" clientidmode="Static" runat="server" for="SignupPage">Page to return user to after a user signs up successfully:</label>
                        <select id="SignupPage" runat="server" class="form-control">
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="AutoUserLabel" clientidmode="Static" runat="server" for="AutoUser">Auto-Generated Suffix. (Leave blank to allow users to create their own username):</label>
                        <input type="text" id="AutoUser" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SignupSecTitleLabel" clientidmode="Static" runat="server" for="SignupSecTitle">Signup section title (Text shows within the signup form above all the fields):</label>
                        <input type="text" id="SignupSecTitle" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SignupSecDescLabel" clientidmode="Static" runat="server" for="SignupSecDesc">Signup section description (Text shows within the signup form above all the fields):</label>
                        <textarea ID="SignupSecDesc" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="SignupSelMemLabel" clientidmode="Static" runat="server" for="SignupSelMem">Only show free memberships on the signup form:</label>
                        <select id="SignupSelMem" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module50" style="display: none">
                    <h2 runat="server" id="Module50Title">Setup the Speaker's Bureau</h2>
                    <div class="mb-3">
                        <label id="SpeakerEnableLabel" clientidmode="Static" runat="server" for="SpeakerEnable">Enable the speakers bureau:</label>
                        <select id="SpeakerEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module15" style="display: none">
                    <h2 runat="server" id="Module15Title">Setup the Stocks</h2>
                    <div class="mb-3">
                        <label id="StocksSymbolsLabel" clientidmode="Static" runat="server" for="StocksSymbols">Symbols to display. (Separate the symbols by commas):</label>
                        <input type="text" id="StocksSymbols" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module7" style="display: none">
                    <h2 runat="server" id="Module7Title">Setup the User Pages</h2>
                    <div class="mb-3">
                        <label id="UPagesEnableLabel" clientidmode="Static" runat="server" for="UPagesEnable">Enable the user pages:</label>
                        <select id="UPagesEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3" id="UPagesSignupProcessRow" runat="server">
                        <label id="UPagesSignupProcessLabel" clientidmode="Static" runat="server" for="UPagesSignupProcess">Website Signup Process:</label>
                        <select id="UPagesSignupProcess" runat="server" class="form-control">
                            <option value="Disabled">Disabled</option>
                            <option value="Allow">Allow users to select a user page on the signup form</option>
                            <option value="Require">Require users to select a user page on the signup form</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesTop10Label" clientidmode="Static" runat="server" for="UPagesTop10">Have the module top 10 lists show only the items the user has posted that created the user page:</label>
                        <select id="UPagesTop10" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu1Label" clientidmode="Static" runat="server" for="UPagesMenu1">Site Menu 1 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu1" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu2Label" clientidmode="Static" runat="server" for="UPagesMenu2">Site Menu 2 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu2" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu3Label" clientidmode="Static" runat="server" for="UPagesMenu3">Site Menu 3 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu3" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu4Label" clientidmode="Static" runat="server" for="UPagesMenu4">Site Menu 4 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu4" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu5Label" clientidmode="Static" runat="server" for="UPagesMenu5">Site Menu 5 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu5" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu6Label" clientidmode="Static" runat="server" for="UPagesMenu6">Site Menu 6 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu6" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMenu7Label" clientidmode="Static" runat="server" for="UPagesMenu7">Site Menu 7 Name (Leave blank to disable):</label>
                        <input type="text" id="UPagesMenu7" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu1Label" clientidmode="Static" runat="server" for="UPagesMainMenu1">Allow users to place items on the main site menu 1:</label>
                        <select id="UPagesMainMenu1" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu2Label" clientidmode="Static" runat="server" for="UPagesMainMenu2">Allow users to place items on the main site menu 2:</label>
                        <select id="UPagesMainMenu2" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu3Label" clientidmode="Static" runat="server" for="UPagesMainMenu3">Allow users to place items on the main site menu 3:</label>
                        <select id="UPagesMainMenu3" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu4Label" clientidmode="Static" runat="server" for="UPagesMainMenu4">Allow users to place items on the main site menu 4:</label>
                        <select id="UPagesMainMenu4" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu5Label" clientidmode="Static" runat="server" for="UPagesMainMenu5">Allow users to place items on the main site menu 5:</label>
                        <select id="UPagesMainMenu5" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu6Label" clientidmode="Static" runat="server" for="UPagesMainMenu6">Allow users to place items on the main site menu 6:</label>
                        <select id="UPagesMainMenu6" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="UPagesMainMenu7Label" clientidmode="Static" runat="server" for="UPagesMainMenu7">Allow users to place items on the main site menu 7:</label>
                        <select id="UPagesMainMenu7" runat="server" class="form-control">
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                </div>

                <div id="Module63" style="display: none">
                    <h2 runat="server" id="Module63Title">Setup the User Profiles</h2>
                    <div class="mb-3">
                        <label id="ProfilesEnableLabel" clientidmode="Static" runat="server" for="ProfilesEnable">Enable the user profiles:</label>
                        <select id="ProfilesEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3" id="ProfilesAskSignupRow" runat="server">
                        <label id="ProfilesAskSignupLabel" clientidmode="Static" runat="server" for="ProfilesAskSignup">Ask users when they signup to fill out their profile:</label>
                        <select id="ProfilesAskSignup" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ProfilesPicNoLabel" clientidmode="Static" runat="server" for="ProfilesPicNo">Number of pictures a user can upload:</label>
                        <input type="text" id="ProfilesPicNo" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3" id="ProfileRequiredRow" runat="server">
                        <label id="ProfileRequiredLabel" clientidmode="Static" runat="server" for="ProfileRequired">Require profile after signup:</label>
                        <select id="ProfileRequired" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ProfilesType1Label" clientidmode="Static" runat="server" for="ProfilesType1">Profile Type 1. (Leave blank to disable):</label>
                        <input type="text" id="ProfilesType1" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ProfilesType2Label" clientidmode="Static" runat="server" for="ProfilesType2">Profile Type 2. (Leave blank to disable):</label>
                        <input type="text" id="ProfilesType2" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ProfilesType3Label" clientidmode="Static" runat="server" for="ProfilesType3">Profile Type 3. (Leave blank to disable):</label>
                        <input type="text" id="ProfilesType3" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="ProfilesColorLabel" clientidmode="Static" runat="server" for="ProfilesColor">Allow users to customize their profile colors:</label>
                        <select id="ProfilesColor" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="ProfilesAudioLabel" clientidmode="Static" runat="server" for="ProfilesAudio">Maximum number of audio files a user can upload to their profile:</label>
                        <input type="text" id="ProfilesAudio" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module55" style="display: none">
                    <h2 runat="server" id="Module55Title">Setup the Weather Forecast</h2>
                    <div class="mb-3">
                        <label id="WeatherEnableLabel" clientidmode="Static" runat="server" for="WeatherEnable">Enable the weather forecast:</label>
                        <select id="WeatherEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module62" style="display: none">
                    <h2 runat="server" id="Module62Title">User Feeds</h2>
                    <div class="mb-3">
                        <label id="FeedsEnableLabel" clientidmode="Static" runat="server" for="FeedsEnable">Enable the User Feeds.:</label>
                        <select id="FeedsEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                </div>

                <div id="Module65" style="display: none">
                    <h2 runat="server" id="Module65Title">Setup the Vouchers</h2>
                    <div class="mb-3">
                        <label id="VoucherEnableLabel" clientidmode="Static" runat="server" for="VoucherEnable">Enable the vouchers.:</label>
                        <select id="VoucherEnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="VoucherDaysAddLabel" clientidmode="Static" runat="server" for="VoucherDaysAdd">Numbers of days to add to the valid from date when a user purchases a voucher.:</label>
                        <input type="text" id="VoucherDaysAdd" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="VoucherExpireDaysLabel" clientidmode="Static" runat="server" for="VoucherExpireDays">Number of days until a voucher expires:</label>
                        <input type="text" id="VoucherExpireDays" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="VoucherAgreementLabel" clientidmode="Static" runat="server" for="VoucherAgreement">Voucher post agreement:</label>
                        <textarea ID="VoucherAgreement" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="VoucherAgreementBuyLabel" clientidmode="Static" runat="server" for="VoucherAgreementBuy">Voucher buy agreement:</label>
                        <textarea ID="VoucherAgreementBuy" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="VoucherTop10Label" clientidmode="Static" runat="server" for="VoucherTop10">Display the top 10 newest vouchers:</label>
                        <select id="VoucherTop10" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module66" style="display: none">
                    <h2 runat="server" id="H1">Setup PCRecruiter</h2>
                    <div class="mb-3">
                        <label id="PCREnableLabel" clientidmode="Static" runat="server" for="PCREnable">Enable PCRecruiter:</label>
                        <select id="PCREnable" runat="server" class="form-control">
                            <option value="Enable">Enable</option>
                            <option value="Disable">Disable</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="PCRAPIKeyLabel" clientidmode="Static" runat="server" for="PCRAPIKey">PCRecruiter API Key:</label>
                        <input type="text" id="PCRAPIKey" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PCRAppIDLabel" clientidmode="Static" runat="server" for="PCRAppID">PCRecruiter App ID:</label>
                        <input type="text" id="PCRAppID" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PCRAPIURLLabel" clientidmode="Static" runat="server" for="PCRAPIURL">PCRecruiter API URL (Ex. https://www.pcrecruiter.net/rest/api/):</label>
                        <input type="text" id="PCRAPIURL" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PCRDatabaseIdLabel" clientidmode="Static" runat="server" for="PCRDatabaseId">PCRecruiter Database ID:</label>
                        <input type="text" id="PCRDatabaseId" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PCRUserNameLabel" clientidmode="Static" runat="server" for="PCRUserName">PCRecruiter Database User Name:</label>
                        <input type="text" id="PCRUserName" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PCRPasswordLabel" clientidmode="Static" runat="server" for="PCRPassword">PCRecruiter Database Password:</label>
                        <input type="text" id="PCRPassword" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="PCRExpireDaysLabel" clientidmode="Static" runat="server" for="PCRExpireDays">Days until a job expires:</label>
                        <input type="text" id="PCRExpireDays" runat="server" class="form-control" />
                    </div>
                </div>

                <div id="Module993" style="display: none">
                    <h2 runat="server" id="Module993Title">Setup the Website Layout</h2>
                    <div class="mb-3">
                        <label id="Menu1TextLabel" clientidmode="Static" runat="server" for="Menu1Text">Menu 1 Text:</label>
                        <input type="text" id="Menu1Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu2TextLabel" clientidmode="Static" runat="server" for="Menu2Text">Menu 2 Text:</label>
                        <input type="text" id="Menu2Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu3TextLabel" clientidmode="Static" runat="server" for="Menu3Text">Menu 3 Text:</label>
                        <input type="text" id="Menu3Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu4TextLabel" clientidmode="Static" runat="server" for="Menu4Text">Menu 4 Text:</label>
                        <input type="text" id="Menu4Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu5TextLabel" clientidmode="Static" runat="server" for="Menu5Text">Menu 5 Text:</label>
                        <input type="text" id="Menu5Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu6TextLabel" clientidmode="Static" runat="server" for="Menu6Text">Menu 6 Text:</label>
                        <input type="text" id="Menu6Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu7TextLabel" clientidmode="Static" runat="server" for="Menu7Text">Menu 7 Text:</label>
                        <input type="text" id="Menu7Text" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="Menu1SitemapLabel" clientidmode="Static" runat="server" for="Menu1Sitemap">Show Menu 1 on the sitemap:</label>
                        <select id="Menu1Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Menu2SitemapLabel" clientidmode="Static" runat="server" for="Menu2Sitemap">Show Menu 2 on the sitemap:</label>
                        <select id="Menu2Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Menu3SitemapLabel" clientidmode="Static" runat="server" for="Menu3Sitemap">Show Menu 3 on the sitemap:</label>
                        <select id="Menu3Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Menu4SitemapLabel" clientidmode="Static" runat="server" for="Menu4Sitemap">Show Menu 4 on the sitemap:</label>
                        <select id="Menu4Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Menu5SitemapLabel" clientidmode="Static" runat="server" for="Menu5Sitemap">Show Menu 5 on the sitemap:</label>
                        <select id="Menu5Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Menu6SitemapLabel" clientidmode="Static" runat="server" for="Menu6Sitemap">Show Menu 6 on the sitemap:</label>
                        <select id="Menu6Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="Menu7SitemapLabel" clientidmode="Static" runat="server" for="Menu7Sitemap">Show Menu 7 on the sitemap:</label>
                        <select id="Menu7Sitemap" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SiteLogoLabel" clientidmode="Static" runat="server" for="SiteLogo">Website Logo:</label>
                        <asp:Image ID="SiteLogoImg" runat="server" Visible="false" />
                        <asp:CheckBox ID="RemoveSiteLogo" runat="server" Text="Remove Site Logo" />
                        <br />
                        <asp:FileUpload ID="SiteLogo" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label id="RSSTopLabel" clientidmode="Static" runat="server" for="RSSTop">Enable the RSS Feeds button:</label>
                        <select id="RSSTop" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="FavoritesTopLabel" clientidmode="Static" runat="server" for="FavoritesTop">Enable the Favorites button:</label>
                        <select id="FavoritesTop" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="MainPageTopLabel" clientidmode="Static" runat="server" for="MainPageTop">Enable the Main Page button:</label>
                        <select id="MainPageTop" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="SocialSharingLabel" clientidmode="Static" runat="server" for="SocialSharing">Enable the social network sharing:</label>
                        <select id="SocialSharing" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>

                <div id="Module992" style="display: none">
                    <h2 runat="server" id="Module992Title">Setup the Website</h2>
                    <div class="mb-3">
                        <label id="WebSiteNameLabel" clientidmode="Static" runat="server" for="WebSiteName">Name of your website:</label>
                        <input type="text" id="WebSiteName" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CurrencyCodeLabel" clientidmode="Static" runat="server" for="CurrencyCode">Currency Code:</label>
                        <select id="CurrencyCode" runat="server" class="form-control">
                            <option value="CAD">CAD</option>
                            <option value="EUR">EUR</option>
                            <option value="GBP">GBP</option>
                            <option value="RM">RM</option>
                            <option value="USD">USD</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="BadWordFilterLabel" clientidmode="Static" runat="server" for="BadWordFilter">Bad words to filter out of your website. (Seperated by Comma's):</label>
                        <textarea ID="BadWordFilter" runat="server"   class="form-control"></textarea>
                    </div>
                    <div class="mb-3">
                        <label id="RecPerAPageLabel" clientidmode="Static" runat="server" for="RecPerAPage">Records to show per a page:</label>
                        <input type="text" id="RecPerAPage" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="MaxImageSizeLabel" clientidmode="Static" runat="server" for="MaxImageSize">Maximum image size that can be uploaded. (KB):</label>
                        <input type="text" id="MaxImageSize" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="SiteLangLabel" clientidmode="Static" runat="server" for="SiteLang">Select your primary language:</label>
                        <select id="SiteLang" runat="server" class="form-control">
                            <option value="en-US">English (United States)</option>
                            <option value="nl-NL">Dutch (The Netherlands)</option>
                            <option value="fr-CA">French (Canada)</option>
                            <option value="fr-FR">French (France)</option>
                            <option value="pt-BR">Portuguese (Brazil)</option>
                            <option value="es-MX">Spanish (Mexico)</option>
                            <option value="es-ES">Spanish (Spain)</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="CatCountLabel" clientidmode="Static" runat="server" for="CatCount">Show content count when displaying categories:</label>
                        <select id="CatCount" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="TimeOffsetLabel" clientidmode="Static" runat="server" for="TimeOffset">Time offset in hours:</label>
                        <input type="text" id="TimeOffset" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label id="CatLowestLvlLabel" clientidmode="Static" runat="server" for="CatLowestLvl">Require the lowest level of category to be selected:</label>
                        <select id="CatLowestLvl" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label id="RequireSSLLabel" clientidmode="Static" runat="server" for="RequireSSL">Require SSL Connection (Requires SSL Certificate installed on the web site):</label>
                        <select id="RequireSSL" runat="server" class="form-control">
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
            </div>

<div class="button-to-bottom">
    <button class="btn btn-primary" id="SetupSave" runat="server" OnServerClick="SetupSave_Click">Save Changes</button>
    <span><span id="SaveMessage" runat="server"></span></span>
</div>

        <%
            Response.Write("<script>");
            if(SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) > 0) {
                Response.Write("$('#SetupMenuTree').hide();");
                Response.Write("$('#Module" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "').show();");
                Response.Write("restyleFormElements('#Module" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("ModuleID")) + "');");
            } else {
                Response.Write("$('.pagecontent').css('top', '0px');");
            }
            Response.Write("</script>");
        %>
    </asp:Panel>
</asp:content>