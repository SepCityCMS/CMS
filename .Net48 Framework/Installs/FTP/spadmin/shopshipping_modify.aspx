<%@ page title="Shopping Mall Shipping" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="shopshipping_modify.aspx.cs" inherits="wwwroot.shopshipping_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 995;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Shipping Method</h4>
                <input type="hidden" runat="server" ID="MethodID" />
                <div class="mb-3">
                    <label ID="MethodNameLabel" clientidmode="Static" runat="server" for="MethodName">Method Name:</label>
                    <input type="text" ID="MethodName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="MethodNameRequired" runat="server" ControlToValidate="MethodName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Method Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea ID="Description" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="CarrierLabel" clientidmode="Static" runat="server" for="Carrier">Carrier:</label>
                    <asp:DropDownList ID="Carrier" runat="server" CssClass="form-control" AutoPostBack="True" clientidmode="Static" EnableViewState="True" OnSelectedIndexChanged="Carrier_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label ID="ShippingServiceLabel" clientidmode="Static" runat="server" for="ShippingService">Shipping Service:</label>
                    <select ID="ShippingService" runat="server" class="form-control">
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="DeliveryTimeLabel" clientidmode="Static" runat="server" for="DeliveryTime">Delivery Time:</label>
                    <input type="text" ID="DeliveryTime" runat="server"  class="form-control" MaxLength="100" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>