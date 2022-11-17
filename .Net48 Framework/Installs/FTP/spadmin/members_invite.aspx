<%@ page title="Invite Member" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="members_invite.aspx.cs" inherits="wwwroot.members_invite" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>
        
		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Invite Member</h4>
                <input type="hidden" runat="server" ID="InviteID" />
                <div class="mb-3">
                    <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                    <input type="text" ID="EmailAddress" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="FirstNameLabel" clientidmode="Static" runat="server" for="FirstName">First Name:</label>
                    <input type="text" ID="FirstName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="First Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="LastNameLabel" clientidmode="Static" runat="server" for="LastName">Last Name:</label>
                    <input type="text" ID="LastName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Last Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="JoinClassLabel" clientidmode="Static" runat="server" for="JoinClass">Access Class this member will join when they signup:</label>
                    <sep:AccessClassDropdown ID="JoinClass" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Send</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>