<%@ page title="User Pages" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="userpages_modify.aspx.cs" inherits="wwwroot.userpages_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 7;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Edit Site</h4>
                <input type="hidden" runat="server" ID="SiteID" />
                <input type="hidden" runat="server" ID="UserID" />

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="7" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="SiteNameLabel" clientidmode="Static" runat="server" for="SiteName">Site Name:</label>
                    <input type="text" ID="SiteName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="SiteNameRequired" runat="server" ControlToValidate="SiteName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Site Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="SiteLogoLabel" clientidmode="Static" runat="server" for="SiteLogo">Site Logo:</label>
                    <sep:UploadFiles ID="SiteLogo" runat="server" ModuleID="7" FileType="Images" Mode="SingleFile" />
                </div>
                <div class="mb-3">
                    <label ID="SiteSloganLabel" clientidmode="Static" runat="server" for="SiteSlogan">Site Slogan:</label>
                    <input type="text" ID="SiteSlogan" runat="server"  class="form-control" />
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
                    <label ID="SelectTemplateLabel" clientidmode="Static" runat="server" for="TemplateID">Select Template:</label>
                    <sep:TemplateDropdown ID="TemplateID" runat="server" ModuleID="7" />
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="EnableGuestbook" runat="server" Text="Enable Guestbook" />
                </div>
                <div class="mb-3">
                    <label ID="ShowListLabel" clientidmode="Static" runat="server" for="ShowList">Show your site on the site listings:</label>
                    <select ID="ShowList" runat="server" class="form-control">
                        <option value="true">Yes</option>
                        <option value="false">No</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="PortalSelectionLabel" clientidmode="Static" runat="server" for="PortalSelection">Portal to associate your user site with:</label>
                    <sep:PortalDropdown id="PortalSelection" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="InviteOnlyLabel" clientidmode="Static" runat="server" for="InviteOnly">Make your web site invite only:</label>
                    <select ID="InviteOnly" runat="server" class="form-control">
                        <option value="true">Yes</option>
                        <option value="false">No</option>
                    </select>
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 7;
                    cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
                    Response.Write(cCustomFields.Render()); 
                %>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>