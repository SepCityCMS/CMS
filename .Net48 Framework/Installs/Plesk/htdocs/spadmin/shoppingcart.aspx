<%@ page title="Shopping Cart" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shoppingcart.aspx.cs" inherits="wwwroot.shoppingcart" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        skipRestyling = true;

        function openPayPal() {
            $('#tabPayPal a').addClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $("#AuthorizeNet").hide();
            $("#eSelect").hide();
            $("#Checks").hide();
            $("#SSLServer").hide();
            $("#MultiSafepay").hide();
            $("#Square").hide();
            $("#Stripe").hide();
            $("#PayPal").show();
            restyleFormElements('#PayPal');
        }

        function openAuthorizeNet() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').addClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $("#PayPal").hide();
            $("#eSelect").hide();
            $("#Checks").hide();
            $("#SSLServer").hide();
            $("#MultiSafepay").hide();
            $("#Square").hide();
            $("#Stripe").hide();
            $("#AuthorizeNet").show();
            restyleFormElements('#AuthorizeNet');
        }

        function openeSelect() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').addClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $("#PayPal").hide();
            $("#AuthorizeNet").hide();
            $("#Checks").hide();
            $("#SSLServer").hide();
            $("#MultiSafepay").hide();
            $("#Square").hide();
            $("#Stripe").hide();
            $("#eSelect").show();
            restyleFormElements('#eSelect');
        }

        function openChecks() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').addClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $("#PayPal").hide();
            $("#AuthorizeNet").hide();
            $("#eSelect").hide();
            $("#SSLServer").hide();
            $("#MultiSafepay").hide();
            $("#Square").hide();
            $("#Stripe").hide();
            $("#Checks").show();
            restyleFormElements('#Checks');
        }

        function openSSLServer() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').addClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $("#PayPal").hide();
            $("#AuthorizeNet").hide();
            $("#eSelect").hide();
            $("#Checks").hide();
            $("#MultiSafepay").hide();
            $("#Square").hide();
            $("#Stripe").hide();
            $("#SSLServer").show();
            restyleFormElements('#SSLServer');
        }

        function openMultiSafepay() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $('#tabMultiSafepay a').addClass('btn-info');
            $("#PayPal").hide();
            $("#AuthorizeNet").hide();
            $("#eSelect").hide();
            $("#Checks").hide();
            $("#SSLServer").hide();
            $("#Square").hide();
            $("#Stripe").hide();
            $("#MultiSafepay").show();
            restyleFormElements('#MultiSafepay');
        }

        function openSquare() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').addClass('btn-info');
            $('#tabStripe a').removeClass('btn-info');
            $("#PayPal").hide();
            $("#AuthorizeNet").hide();
            $("#eSelect").hide();
            $("#Checks").hide();
            $("#SSLServer").hide();
            $("#Stripe").hide();
            $("#MultiSafepay").hide();
            $("#Square").show();
            restyleFormElements('#Square');
        }

        function openStripe() {
            $('#tabPayPal a').removeClass('btn-info');
            $('#tabAuthorize a').removeClass('btn-info');
            $('#tabeSelect a').removeClass('btn-info');
            $('#tabChecks a').removeClass('btn-info');
            $('#tabSSL a').removeClass('btn-info');
            $('#tabMultiSafepay a').removeClass('btn-info');
            $('#tabSquare a').removeClass('btn-info');
            $('#tabStripe a').addClass('btn-info');
            $("#PayPal").hide();
            $("#AuthorizeNet").hide();
            $("#eSelect").hide();
            $("#Checks").hide();
            $("#SSLServer").hide();
            $("#Square").hide();
            $("#MultiSafepay").hide();
            $("#Stripe").show();
            restyleFormElements('#Stripe');
        }

        $(document)
            .ready(function () {
                restyleFormElements('#PayPal');
                $('#tabPayPal a').addClass('btn-info');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 995;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

            <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Payment Gateways</h4>

                <div class="panel panel-default" id="PageManageGridView" runat="server">
                    <div class="panel-body">
                        <ul class="nav nav-pills">
                            <li class="nav-item" role="presentation" id="tabPayPal">
                                <a class="nav-link" href="javascript:void(0)" onclick="openPayPal();">PayPal</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabAuthorize">
                                <a class="nav-link" href="javascript:void(0)" onclick="openAuthorizeNet();">Authorize.Net</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabSquare">
                                <a class="nav-link" href="javascript:void(0)" onclick="openSquare();">Square</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabStripe">
                                <a class="nav-link" href="javascript:void(0)" onclick="openStripe();">Stripe</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabeSelect">
                                <a class="nav-link" href="javascript:void(0)" onclick="openeSelect();">eSelect</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabChecks">
                                <a class="nav-link" href="javascript:void(0)" onclick="openChecks();">Checks/Money Orders</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabSSL">
                                <a class="nav-link" href="javascript:void(0)" onclick="openSSLServer();">Custom SSL Server</a>
                            </li>
                            <li class="nav-item" role="presentation" id="tabMultiSafepay">
                                <a class="nav-link" href="javascript:void(0)" onclick="openMultiSafepay();">MultiSafepay</a>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="panel-body">
                    <div id="PayPal">
                        <div class="mb-3">
                            <label ID="PayPalEmailLabel" clientidmode="Static" runat="server" for="PayPalEmail">PayPal Email Address:</label>
                            <input type="text" ID="PayPalEmail" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                    </div>
                    <div id="Square" style="display: none">
                        <div class="mb-3">
                            <label ID="SquareAccessTokenLabel" clientidmode="Static" runat="server" for="SquareAccessToken">Square Access Token:</label>
                            <input type="text" ID="SquareAccessToken" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="SquareLocationIdLabel" clientidmode="Static" runat="server" for="SquareLocationId">Square Location Id:</label>
                            <input type="text" ID="SquareLocationId" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                    </div>
                    <div id="Stripe" style="display: none">
                        <div class="mb-3">
                            <label ID="StripePublishableKeyLabel" clientidmode="Static" runat="server" for="StripePublishableKey">Stripe Publishable Key:</label>
                            <input type="text" ID="StripePublishableKey" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="SecretKeyLabel" clientidmode="Static" runat="server" for="StripeSecretKey">Stripe Secret Key:</label>
                            <input type="text" ID="StripeSecretKey" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                    </div>
                    <div id="AuthorizeNet" style="display: none">
                        <div class="mb-3">
                            <label ID="AuthorizeNetLoginLabel" clientidmode="Static" runat="server" for="AuthorizeNetLogin">Authorize.Net Login:</label>
                            <input type="text" ID="AuthorizeNetLogin" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="AuthorizeNetTransKeyLabel" clientidmode="Static" runat="server" for="AuthorizeNetTransKey">Authorize.Net Transaction Key:</label>
                            <input type="text" ID="AuthorizeNetTransKey" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                    </div>
                    <div id="eSelect" style="display: none">
                        <div class="mb-3">
                            <label ID="eSelectAPILabel" clientidmode="Static" runat="server" for="eSelectAPI">eSelect Plus API Token:</label>
                            <input type="text" ID="eSelectAPI" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="eSelectStoreIDLabel" clientidmode="Static" runat="server" for="eSelectStoreID">eSelect Plus Store ID:</label>
                            <input type="text" ID="eSelectStoreID" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                    </div>
                    <div id="Checks" style="display: none">
                        <div class="mb-3">
                            <label ID="ChecksEmailLabel" clientidmode="Static" runat="server" for="ChecksEmail">Check/Money Order Email Payments To:</label>
                            <input type="text" ID="ChecksEmail" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="ChecksAddressLabel" clientidmode="Static" runat="server" for="ChecksAddress">Check/Money Order Mail to Address:</label>
                            <textarea ID="ChecksAddress" runat="server"  class="form-control"></textarea>
                        </div>
                        <div class="mb-3">
                            <label ID="ChecksInstructionsLabel" clientidmode="Static" runat="server" for="ChecksInstructions">Check/Money Order Payment Instructions:</label>
                            <textarea ID="ChecksInstructions" runat="server"  class="form-control"></textarea>
                        </div>
                    </div>
                    <div id="SSLServer" style="display: none">
                        <div class="mb-3">
                            <label ID="SSLServerURLLabel" clientidmode="Static" runat="server" for="SSLServerURL">SSL Server URL (ex. https://www.example.com):</label>
                            <input type="text" ID="SSLServerURL" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="SSLServerHeaderLabel" clientidmode="Static" runat="server" for="SSLServerHeader">SSL Server Header:</label>
                            <textarea ID="SSLServerHeader" runat="server"  class="form-control"></textarea>
                        </div>
                        <div class="mb-3">
                            <label ID="SSLServerFooterLabel" clientidmode="Static" runat="server" for="SSLServerFooter">SSL Server Footer:</label>
                            <textarea ID="SSLServerFooter" runat="server"  class="form-control"></textarea>
                        </div>
                    </div>
                    <div id="MultiSafepay" style="display: none">
                        <div class="mb-3">
                            <label ID="MultiSafeAccountIDLabel" clientidmode="Static" runat="server" for="MultiSafeAccountID">MultiSafepay Account ID:</label>
                            <input type="text" ID="MultiSafeAccountID" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="MultiSafeSiteIDLabel" clientidmode="Static" runat="server" for="MultiSafeSiteID">MultiSafepay Site ID:</label>
                            <input type="text" ID="MultiSafeSiteID" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                        <div class="mb-3">
                            <label ID="MultiSafeSiteCodeLabel" clientidmode="Static" runat="server" for="MultiSafeSiteCode">MultiSafepay Site Code:</label>
                            <input type="text" ID="MultiSafeSiteCode" runat="server"  class="form-control" MaxLength="100" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>