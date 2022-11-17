<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userpages_config.aspx.cs" inherits="wwwroot.userpages_config" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Configuration</h4>
        <input type="hidden" runat="server" id="SiteID" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="7" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SiteNameLabel" clientidmode="Static" runat="server" for="SiteName">Site Name:</label>
            <input type="text" id="SiteName" runat="server" class="form-control" />
            <asp:CustomValidator ID="SiteNameRequired" runat="server" ControlToValidate="SiteName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Site Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SiteLogoLabel" clientidmode="Static" runat="server" for="SiteLogo">Site Logo:</label>
            <sep:UploadFiles ID="SiteLogo" runat="server" ModuleID="7" FileType="Images" Mode="SingleFile" />
        </div>
        <div class="mb-3">
            <label id="SiteSloganLabel" clientidmode="Static" runat="server" for="SiteSlogan">Site Slogan:</label>
            <input type="text" id="SiteSlogan" runat="server" class="form-control" />
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
            <label id="TemplateIDLabel" clientidmode="Static" runat="server" for="TemplateID">Select Template:</label>
            <sep:TemplateDropdown ID="TemplateID" runat="server" ModuleID="7" />
        </div>
        <div class="mb-3">
            <label id="ShowListLabel" clientidmode="Static" runat="server" for="ShowList">Show your site on the site listings:</label>
            <select id="ShowList" runat="server" class="form-control">
                <option value="true">Yes</option>
                <option value="false">No</option>
            </select>
        </div>
        <div class="mb-3" id="PortalSelectionRow" runat="server">
            <label id="PortalSelectionLabel" clientidmode="Static" runat="server" for="PortalSelection">Portal to associate your user site with:</label>
            <sep:PortalDropdown ID="PortalSelection" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <label id="InviteOnlyLabel" clientidmode="Static" runat="server" for="InviteOnly">Make your web site invite only:</label>
            <select id="InviteOnly" runat="server" class="form-control">
                <option value="true">Yes</option>
                <option value="false">No</option>
            </select>
        </div>
        <div class="mb-3">
            <asp:CheckBox ID="EnableGuestbook" runat="server" Text="Enable Guestbook" />
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 7;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>