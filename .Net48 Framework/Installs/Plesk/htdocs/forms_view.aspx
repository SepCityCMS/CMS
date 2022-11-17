<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="forms_view.aspx.cs" inherits="wwwroot.forms_view" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFieldset" runat="server">

        <h4 id="ModifyLegend" runat="server"></h4>
        <input type="hidden" runat="server" id="FormID" />

        <div class="mb-3">
            <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
            <input type="text" id="EmailAddress" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <asp:Panel ID="QuestionsPanel" runat="server" ClientIDMode="Static"></asp:Panel>

        <br />
        <div class="mb-3" id="CaptchaRow" runat="server">
            <sep:Captcha ID="Recaptcha1" runat="server" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Submit" OnClick="SaveButton_Click" />
        </div>
    </div>
</asp:content>