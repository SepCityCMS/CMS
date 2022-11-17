<%@ page title="Guestbook" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="guestbook_modify.aspx.cs" inherits="wwwroot.guestbook_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 14;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Entry</h4>
                <input type="hidden" runat="server" ID="EntryID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="14" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                    <input type="text" ID="EmailAddress" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="WebSiteURLLabel" clientidmode="Static" runat="server" for="WebSiteURL">Web Site URL:</label>
                    <input type="text" ID="WebSiteURL" runat="server"  class="form-control" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="MessageLabel" clientidmode="Static" runat="server" for="Message">Message:</label>
                    <textarea ID="Message" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="MessageRequired" runat="server" ControlToValidate="Message"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Message is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>