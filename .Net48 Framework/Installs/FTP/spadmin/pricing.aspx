<%@ page title="Pricing" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="pricing.aspx.cs" inherits="wwwroot.pricing" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Pricing Setup</h4>
                <div class="mb-3">
                    <label ID="NewListingLabel" clientidmode="Static" runat="server" for="NewListing">Price for a new listing:</label>
                    <input type="text" ID="NewListing" runat="server"  class="form-control" MaxLength="20" />
                </div>
                <div class="mb-3">
                    <label ID="FeaturedListingLabel" clientidmode="Static" runat="server" for="FeaturedListing">Price for a featured listing:</label>
                    <input type="text" ID="FeaturedListing" runat="server"  class="form-control" MaxLength="20" />
                </div>
                <div class="mb-3">
                    <label ID="BoldTitleLabel" clientidmode="Static" runat="server" for="BoldTitle">Price for bold title:</label>
                    <input type="text" ID="BoldTitle" runat="server"  class="form-control" MaxLength="20" />
                </div>
                <div class="mb-3">
                    <label ID="HighlightListingLabel" clientidmode="Static" runat="server" for="HighlightListing">Price for highlighted listing:</label>
                    <input type="text" ID="HighlightListing" runat="server"  class="form-control" MaxLength="20" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>