<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="guestbook_sign.aspx.cs" inherits="wwwroot.guestbook_sign" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Sign the Guestbook</h4>
        <input type="hidden" runat="server" id="EntryID" />

        <div class="mb-3">
            <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
            <input type="text" id="EmailAddress" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="WebSiteURLLabel" clientidmode="Static" runat="server" for="WebSiteURL">Web Site URL:</label>
            <input type="text" id="WebSiteURL" runat="server" class="form-control" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="MessageLabel" clientidmode="Static" runat="server" for="Message">Message:</label>
            <textarea id="Message" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="MessageRequired" runat="server" ControlToValidate="Message"
                ClientValidationFunction="customFormValidator" ErrorMessage="Message is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>