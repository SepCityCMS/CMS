<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="messenger_compose.aspx.cs" inherits="wwwroot.messenger_compose" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ComposeForm" runat="server">

        <h4 id="ModifyLegend" runat="server">Compose a Message</h4>
        <div class="mb-3">
            <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">Your User Name:</label>
            <span ID="Username" runat="server"></span>
        </div>
        <div class="mb-3">
            <label id="ToUserNameLabel" clientidmode="Static" runat="server" for="ToUserName">To User Name:</label>
            <input type="text" id="ToUserName" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <br />
            <div class="mb-3">
                <asp:Button CssClass="btn btn-success" ID="Mass_Message" runat="server" OnClientClick="document.getElementById('ToUserName').value = '[MASS_MESSAGE]';return false;" Text="Mass Message" />
            </div>
            <asp:CustomValidator ID="ToUserNameRequired" runat="server" ControlToValidate="ToUserName"
                ClientValidationFunction="customFormValidator" ErrorMessage="To User Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
            <input type="text" id="Subject" runat="server" class="form-control" maxlength="100" clientidmode="Static" />
            <asp:CustomValidator ID="SubjectRequired" runat="server" ControlToValidate="Subject"
                ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="Message" Width="99%" Height="450" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SendButton" runat="server" Text="Send Message" OnClick="SendButton_Click" />
        </div>
    </div>
</asp:content>