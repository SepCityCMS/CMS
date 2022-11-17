<%@ page title="General Setup" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="portals_setup.aspx.cs" inherits="wwwroot.portals_setup" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#SetupMainContent').width('100%');
            $('#SetupMainContent').height(($(document).height() - 125) + 'px');
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            if (SepCommon.SepFunctions.CompareKeys(SepCommon.SepFunctions.Security("PortalsAdmin"), false)) {
                var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
                cAdminModuleMenu.ModuleID = 60;
                Response.Write(cAdminModuleMenu.Render()); 
            }
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="General Setup"></span>
        </h2>

        <div id="SetupMainContent">
            <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />
            <h2>Website Setup</h2>
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
                <label ID="FriendlyNameLabel" clientidmode="Static" runat="server" for="FriendlyName">User Friendly URL (ex. myportal):</label>
				<div class="form-group">
					<div class="input-group">
						<div class="input-group-addon"><span id="MainDomain" runat="server"></span>go/</div>
						<input type="text" id="FriendlyName" runat="server" ckass="form-control" />
					</div>
				</div>
            </div>
            <div class="mb-3">
                <label ID="SiteLogoLabel" clientidmode="Static" runat="server" for="SiteLogo">Site Logo:</label>
                <asp:Image ID="SiteLogoImg" runat="server" Visible="false" />
                <asp:CheckBox ID="RemoveSiteLogo" runat="server" Text="Remove Site Logo" />
                <br />
                <asp:FileUpload ID="SiteLogo" runat="server" />
            </div>
            <div class="mb-3">
                <asp:CheckBox ID="HidePortal" runat="server" Text="Hide portal from showing on directory list."></asp:CheckBox>
            </div>
            <div class="mb-3">
                <label ID="SiteLangLabel" clientidmode="Static" runat="server" for="SiteLang">Select your primary language:</label>
                <select id="SiteLang" runat="server" class="form-control">
                    <option value="en-US">English (United States)</option>
                    <option value="nl-NL">Dutch (The Netherlands)</option>
                    <option value="fr-CA">French (Canada)</option>
                    <option value="fr-FR">French (France)</option>
                    <option value="ms-MY">Malaya (Malaysia)</option>
                    <option value="pt-BR">Portuguese (Brazil)</option>
                    <option value="ru-RU">Russian (Russia)</option>
                    <option value="es-MX">Spanish (Mexico)</option>
                    <option value="es-ES">Spanish (Spain)</option>
                </select>
            </div>
            <h2>Administrator Information</h2>
            <div class="mb-3">
                <label ID="FullNameLabel" clientidmode="Static" runat="server" for="FullName">Full Name:</label>
                <input type="text" ID="FullName" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                <input type="text" ID="EmailAddress" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Company Name:</label>
                <input type="text" ID="CompanyName" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="CompanySloganLabel" clientidmode="Static" runat="server" for="CompanySlogan">Company Slogan:</label>
                <input type="text" ID="CompanySlogan" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
                <input type="text" ID="StreetAddress" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
                <input type="text" ID="City" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="CompanyStateLabel" clientidmode="Static" runat="server" for="CompanyState">State your company is located in:</label>
                <input type="text" ID="CompanyState" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="CompanyZipCodeLabel" clientidmode="Static" runat="server" for="CompanyZipCode">Your company zip/postal code:</label>
                <input type="text" ID="CompanyZipCode" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="CompanyCountryLabel" clientidmode="Static" runat="server" for="CompanyCountry">Your company country:</label>
                <sep:CountryDropdown ID="CompanyCountry" runat="server" CssClass="form-control" />
            </div>

            <h2>Website Layout</h2>
            <div class="mb-3">
                <label ID="Menu1TextLabel" clientidmode="Static" runat="server" for="Menu1Text">Menu 1 Text:</label>
                <input type="text" ID="Menu1Text" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="Menu2TextLabel" clientidmode="Static" runat="server" for="Menu2Text">Menu 2 Text:</label>
                <input type="text" ID="Menu2Text" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="Menu3TextLabel" clientidmode="Static" runat="server" for="Menu3Text">Menu 3 Text:</label>
                <input type="text" ID="Menu3Text" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="Menu4TextLabel" clientidmode="Static" runat="server" for="Menu4Text">Menu 4 Text:</label>
                <input type="text" ID="Menu4Text" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="Menu5TextLabel" clientidmode="Static" runat="server" for="Menu5Text">Menu 5 Text:</label>
                <input type="text" ID="Menu5Text" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="Menu6TextLabel" clientidmode="Static" runat="server" for="Menu6Text">Menu 6 Text:</label>
                <input type="text" ID="Menu6Text" runat="server"  class="form-control" />
            </div>
            <div class="mb-3">
                <label ID="Menu7TextLabel" clientidmode="Static" runat="server" for="Menu7Text">Menu 7 Text:</label>
                <input type="text" ID="Menu7Text" runat="server"  class="form-control" />
            </div>
        </div>
    </div>

    <div class="button-to-bottom">
        <button class="btn btn-primary" ID="Button1" runat="server" OnServerClick="SetupSave_Click">Save Changes</button>
        <span><span ID="SaveMessage" runat="server"></span></span>
    </div>
    </asp:Panel>
</asp:content>