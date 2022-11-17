<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="settings.aspx.cs" inherits="wwwroot.twilio.settings" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

            <input type="hidden" runat="server" id="OldTwilioSID" />
            <input type="hidden" runat="server" id="OldTwilioToken" />
            <h4 id="ModifyLegend" runat="server">Settings</h4>
            <div class="mb-3">
                <label id="TwilioSIDLabel" clientidmode="Static" runat="server" for="TwilioSID">Twilio SID:</label>
                <input type="text" id="TwilioSID" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="TwilioSIDRequired" runat="server" ControlToValidate="TwilioSID"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Twilio SID is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="TwilioTokenLabel" clientidmode="Static" runat="server" for="TwilioToken">Twilio Token:</label>
                <input type="text" id="TwilioToken" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="TwilioTokenRequired" runat="server" ControlToValidate="TwilioToken"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Twilio Token is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>