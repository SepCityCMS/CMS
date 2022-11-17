<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="advertising_order.aspx.cs" inherits="wwwroot.advertising_order" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="AdOrderForm" runat="server">

        <h4 id="ModifyLegend" runat="server">Order Advertisement Space</h4>
        <input type="hidden" runat="server" id="PlanID" />
        <input type="hidden" runat="server" id="MaxClicks" />
        <input type="hidden" runat="server" id="MaxExposures" />
        <div class="mb-3">
            <label id="WebSiteURLLabel" clientidmode="Static" runat="server" for="WebSiteURL">Website URL to send users to when clicking on your image:</label>
            <input type="text" id="WebSiteURL" runat="server" class="form-control" maxlength="100" clientidmode="Static" text="http://" />
            <asp:CustomValidator ID="WebSiteURLRequired" runat="server" ControlToValidate="WebSiteURL"
                ClientValidationFunction="customFormValidator" ErrorMessage="WebSite URL is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="AdImageLabel" clientidmode="Static" runat="server" for="AdImage">Image of your ad (Only accept png, gif, and jpg images):</label>
            <asp:FileUpload ID="AdImage" runat="server" CssClass="form-control" />
            <asp:CustomValidator ID="AdImageRequired" runat="server" ControlToValidate="AdImage"
                ClientValidationFunction="customFormValidator" ErrorMessage="Image of your ad is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" runat="server" id="ZonesRow">
            <label id="ZoneLabel" clientidmode="Static" runat="server" for="Zone">Select a zone to target the advertisement to:</label>
            <sep:ZoneDropdown ID="Zone" runat="server" ModuleID="2" CssClass="form-control" />
        </div>
        <div class="mb-3" runat="server" id="CategoriesRow">
            <label id="CategoriesLabel" clientidmode="Static" runat="server" for="Categories">Target Ad to Categories:</label>
            <sep:CategorySelection ID="Categories" runat="server" Text="|0|" ClientIDMode="Static" />
        </div>
        <div class="mb-3" runat="server" id="WebPagesRow">
            <label id="PagesLabel" clientidmode="Static" runat="server" for="Pages">Target Ad to Pages:</label>
            <sep:PageSelection ID="Pages" runat="server" Text="|0|" ClientIDMode="Static" />
        </div>
        <div class="mb-3" runat="server" id="PortalsRow">
            <label id="PortalsLabel" clientidmode="Static" runat="server" for="Portals">Target Ad to Portals:</label>
            <sep:PortalSelection ID="Portals" runat="server" Text="|0|" ClientIDMode="Static" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="CartButton" runat="server" Text="Add to Cart" OnClick="CartButton_Click" />
        </div>
    </div>
</asp:content>