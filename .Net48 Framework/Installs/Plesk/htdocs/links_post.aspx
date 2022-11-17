<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="links_post.aspx.cs" inherits="wwwroot.links_post" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Website</h4>
        <input type="hidden" runat="server" id="LinkID" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="19" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SiteNameLabel" clientidmode="Static" runat="server" for="SiteName">Site Name:</label>
            <input type="text" id="SiteName" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="SiteNameRequired" runat="server" ControlToValidate="SiteName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Site Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SiteURLLabel" clientidmode="Static" runat="server" for="SiteURL">Site URL:</label>
            <input type="text" id="SiteURL" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="SiteURLRequired" runat="server" ControlToValidate="SiteURL"
                ClientValidationFunction="customFormValidator" ErrorMessage="Site URL is required."
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

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>