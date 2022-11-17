<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="refer.aspx.cs" inherits="wwwroot.refer" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ReferForm" runat="server">

        <h4 id="ModifyLegend" runat="server">Refer a Friend</h4>
        <input type="hidden" id="ModuleID" runat="server" />
        <input type="hidden" id="ReferURL" runat="server" />
        <div class="mb-3">
            <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Your Email Address:</label>
            <input type="text" id="EmailAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Your Email Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="FriendsEmail1Label" clientidmode="Static" runat="server" for="FriendsEmail1">Friend's Email Address 1:</label>
            <input type="text" id="FriendsEmail1" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="FriendsEmail1Required" runat="server" ControlToValidate="FriendsEmail1"
                ClientValidationFunction="customFormValidator" ErrorMessage="Friend's Email Address 1 is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="FriendsEmail2Label" clientidmode="Static" runat="server" for="FriendsEmail2">Friend's Email Address 2:</label>
            <input type="text" id="FriendsEmail2" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="FriendsEmail3Label" clientidmode="Static" runat="server" for="FriendsEmail3">Friend's Email Address 3:</label>
            <input type="text" id="FriendsEmail3" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="FriendsEmail4Label" clientidmode="Static" runat="server" for="FriendsEmail4">Friend's Email Address 4:</label>
            <input type="text" id="FriendsEmail4" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="FriendsEmail5Label" clientidmode="Static" runat="server" for="FriendsEmail5">Friend's Email Address 5:</label>
            <input type="text" id="FriendsEmail5" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="EmailSubjectLabel" clientidmode="Static" runat="server" for="EmailSubject">Email Subject:</label>
            <input type="text" id="EmailSubject" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="EmailBodyLabel" clientidmode="Static" runat="server" for="EmailBody">Email Body:</label>
            <textarea id="EmailBody" runat="server" class="form-control"></textarea>
        </div>
        <div class="mb-3" id="CaptchaRow" runat="server">
            <sep:Captcha ID="Recaptcha1" runat="server" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SendButton" runat="server" Text="Send Email" OnClick="SendButton_Click" />
        </div>
    </div>
</asp:content>