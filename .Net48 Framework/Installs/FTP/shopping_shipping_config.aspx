<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_shipping_config.aspx.cs" inherits="wwwroot.shopping_shipping_config" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="panel panel-default">
        <div class="panel-body">
            <ul class="nav nav-pills">
                <li class="nav-item" role="presentation" id="tabConfigure"><a class="nav-link" href="shopping_my_store.aspx">Configure Store</a></li>
                <li class="nav-item" role="presentation" id="tabProducts"><a class="nav-link" href="shopping_my_products.aspx">My Products</a></li>
                <li class="nav-item" role="presentation" id="tabOrders"><a class="nav-link" href="shopping_my_orders.aspx">My Orders</a></li>
                <li class="nav-item" role="presentation" id="tabShipping"><a class="nav-link btn-info" href="shopping_shipping.aspx">Shipping Methods</a></li>
                <li class="nav-item" role="presentation" id="tabAnalytics"><a class="nav-link" href="shopping_analytics.aspx">Analytics</a></li>
            </ul>
        </div>
    </div>

    <div class="panel-body">
        <div id="sectionConfigure">

            <ul class="nav nav-tabs">
                <li role="presentation"><a href="shopping_shipping.aspx">Manage</a></li>
                <li role="presentation"><a href="shopping_shipping_modify.aspx">Add Shipping Method</a></li>
                <li role="presentation" class="active"><a href="shopping_shipping_config.aspx">Configure</a></li>
            </ul>

            <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Configure Shipping</h4>
                <div>
                    <div style="font-weight: bold;">Setup FedEx</div>
                    <div>Get the API Key by going <a href="http://www.fedex.com/us/developer/" target="_blank">here</a>.</div>
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

                <div>
                    <div style="font-weight: bold; padding-top: 10px;">Setup UPS</div>
                    <div>Get the API Key by going <a href="https://www.ups.com/upsdeveloperkit" target="_blank">here</a>.</div>
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

                <div>
                    <div style="font-weight: bold; padding-top: 10px;">Setup USPS</div>
                    <div>Get the API Key by going <a href="https://www.usps.com/business/web-tools-apis/welcome.htm" target="_blank">here</a>.</div>
                    <div class="mb-3">
                        <label id="USPSUserIDLabel" clientidmode="Static" runat="server" for="USPSUserID">USPS User ID:</label>
                        <input type="text" id="USPSUserID" runat="server" class="form-control" />
                    </div>
                </div>

                <hr class="mb-4" />
                <div class="mb-3">
                    <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
                </div>
            </div>
        </div>
    </div>
</asp:content>