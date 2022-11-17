<%@ page title="Portals" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_modify.aspx.cs" inherits="wwwroot.portals_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/filters.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 60;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Portal</h4>
                <input type="hidden" runat="server" ID="PortalID" />
                <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />

                <div class="mb-3" ID="PricePlanRow" runat="server">
                    <label ID="PricePlanLabel" ClientIDMode="Static" runat="server" for="PricePlan">Select a pricing plan for this portal:</label>
                    <select ID="PricePlan" runat="server" ClientIDMode="Static" class="form-control" EnableViewState="True">
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name to Assign Portal to:</label>
                    <input type="text" name="UserName" id="UserName" runat="server" class="form-control" placeholder="Click to select a user name" onclick="openUserSearch($(this).attr('id'), 'UserID')" />
                    <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="CategoryRow">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="60" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="PortalNameLabel" clientidmode="Static" runat="server" for="PortalName">Portal Name:</label>
                    <input type="text" ID="PortalName" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="PortalNameRequired" runat="server" ControlToValidate="PortalName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Portal Name is required."
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
                    <label ID="DomainNameLabel" clientidmode="Static" runat="server" for="DomainName">Domain Name:</label>
                    <input type="text" ID="DomainName" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="FriendlyNameLabel" clientidmode="Static" runat="server" for="FriendlyName">User Friendly URL (ex. myportal):</label>
				    <div class="form-group">
					    <div class="input-group">
						    <div class="input-group-addon"><span id="MainDomain" runat="server"></span>go/</div>
						    <input type="text" id="FriendlyName" runat="server" ckass="form-control" />
					    </div>
				    </div>
                </div>
                <div class="mb-3">
                    <label ID="LanguageLabel" clientidmode="Static" runat="server" for="Language">Portal Language:</label>
                    <sep:LanguageDropdown ID="Language" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="TemplateLabel" clientidmode="Static" runat="server" for="Template">Portal Template:</label>
                    <sep:TemplateDropdown ID="Template" runat="server" ModuleID="60" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="SiteLogoLabel" clientidmode="Static" runat="server" for="SiteLogo">Site Logo:</label>
                    <asp:Image ID="SiteLogoImg" runat="server" Visible="false" />
                    <asp:CheckBox ID="RemoveSiteLogo" runat="server" Text="Remove Site Logo" />
                    <br />
                    <asp:FileUpload ID="SiteLogo" runat="server" />
                </div>
                <div class="mb-3">
                    <label ID="LoginKeysLabel" clientidmode="Static" runat="server" for="LoginKeys">Keys to login to this portal:</label>
                    <sep:AccessKeySelection ID="LoginKeys" runat="server" Text="|1|,|2|,|3|,|4|" />
                </div>
                <div class="mb-3">
                    <label ID="ManageKeysLabel" clientidmode="Static" runat="server" for="ManageKeys">Keys to manage this portal:</label>
                    <sep:AccessKeySelection ID="ManageKeys" runat="server" Text="|2|" EnableViewState="True" />
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="HidePortal" runat="server" Text="Hide portal from showing on directory list."></asp:CheckBox>
                </div>
                <div class="mb-3">
                    <label ID="StatusLabel" clientidmode="Static" runat="server" for="Status">Status:</label>
                    <select id="Status" runat="server" class="form-control">
                        <option value="1">Enabled</option>
                        <option value="0">Disabled</option>
                    </select>
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>