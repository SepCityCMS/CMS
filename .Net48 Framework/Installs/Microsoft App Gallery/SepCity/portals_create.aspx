<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="portals_create.aspx.cs" inherits="wwwroot.portals_create" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv">

        <h4 id="ModifyLegend" runat="server">Create Portal</h4>
        <input type="hidden" runat="server" id="PortalID" />

        <span ID="PricingPlans" runat="server"></span>

        <div class="mb-3" id="CategoryRow">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="60" ClientIDMode="Static" />
        </div>
        <div class="mb-3">
            <label id="PortalNameLabel" clientidmode="Static" runat="server" for="PortalName">Portal Name:</label>
            <input type="text" id="PortalName" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="PortalNameRequired" runat="server" ControlToValidate="PortalName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Portal Name is required."
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
            <label id="FriendlyNameLabel" clientidmode="Static" runat="server" for="FriendlyName">User Friendly URL (ex. myportal):</label>
            <div class="form-group">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span ID="MainDomain" runat="server"></span>go/
                    </div>
                    <input type="text" id="FriendlyName" runat="server" ckass="form-control" />
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="LanguageLabel" clientidmode="Static" runat="server" for="Language">Portal Language:</label>
            <sep:LanguageDropdown ID="Language" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <label id="TemplateLabel" clientidmode="Static" runat="server" for="Template">Portal Template:</label>
            <sep:TemplateDropdown ID="Template" runat="server" ModuleID="60" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <label id="SiteLogoLabel" clientidmode="Static" runat="server" for="SiteLogo">Site Logo:</label>
            <asp:FileUpload ID="SiteLogo" runat="server" />
        </div>
        <div class="mb-3">
            <asp:CheckBox ID="HidePortal" runat="server" Text="Hide portal from showing on directory list."></asp:CheckBox>
        </div>

        <hr class="mb-4" />

        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>