<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="shopping_shipping_modify.aspx.cs" inherits="wwwroot.shopping_shipping_modify" %>

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
                <li role="presentation" class="active"><a href="shopping_shipping_modify.aspx">Add Shipping Method</a></li>
                <li role="presentation"><a href="shopping_shipping_config.aspx">Configure</a></li>
            </ul>

            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Shipping Method</h4>
                <input type="hidden" runat="server" id="MethodID" />
                <div class="mb-3">
                    <label id="MethodNameLabel" clientidmode="Static" runat="server" for="MethodName">Method Name:</label>
                    <input type="text" id="MethodName" runat="server" class="form-control" maxlength="100" />
                    <asp:CustomValidator ID="MethodNameRequired" runat="server" ControlToValidate="MethodName"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Method Name is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea id="Description" runat="server" class="form-control"></textarea>
                    <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label id="CarrierLabel" clientidmode="Static" runat="server" for="Carrier">Carrier:</label>
                    <asp:DropDownList ID="Carrier" runat="server" CssClass="form-control" AutoPostBack="True" clientidmode="Static" EnableViewState="True" OnSelectedIndexChanged="Carrier_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label id="ShippingServiceLabel" clientidmode="Static" runat="server" for="ShippingService">Shipping Service:</label>
                    <select id="ShippingService" runat="server" class="form-control">
                    </select>
                </div>
                <div class="mb-3">
                    <label id="DeliveryTimeLabel" clientidmode="Static" runat="server" for="DeliveryTime">Delivery Time:</label>
                    <input type="text" id="DeliveryTime" runat="server" class="form-control" maxlength="100" />
                </div>

                <hr class="mb-4" />
                <div class="mb-3">
                    <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
                </div>
            </div>
        </div>
    </div>
</asp:content>