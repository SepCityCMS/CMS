<%@ page title="Speaker Bureau" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="speakerbureau_modify.aspx.cs" inherits="wwwroot.speakerbureau_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 50;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Speaker</h4>
                <input type="hidden" runat="server" ID="SpeakerID" />
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
                    <label ID="NameTitleLabel" clientidmode="Static" runat="server" for="NameTitle">Title:</label>
                    <select ID="NameTitle" runat="server" class="form-control">
                        <option value="">N/A</option>
                        <option value="Dr.">Dr.</option>
                        <option value="Miss">Miss</option>
                        <option value="Mr.">Mr.</option>
                        <option value="Mrs.">Mrs.</option>
                        <option value="Ms.">Ms.</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
                    <input type="text" ID="EmailAddress" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="CredentialsLabel" clientidmode="Static" runat="server" for="Credentials">Credentials:</label>
                    <textarea ID="Credentials" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="CredentialsRequired" runat="server" ControlToValidate="Credentials"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Credentials is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="BioLabel" clientidmode="Static" runat="server" for="Bio">Bio:</label>
                    <sep:WYSIWYGEditor ID="Bio" runat="server" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="PictureLabel" clientidmode="Static" runat="server" for="Picture">Picture:</label>
                    <sep:UploadFiles ID="Picture" runat="server" Mode="SingleFile" FileType="Images" ModuleID="50" />
                </div>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>