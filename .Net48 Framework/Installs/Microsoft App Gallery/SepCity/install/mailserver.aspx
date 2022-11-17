<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="mailserver.aspx.cs" inherits="wwwroot.mailserver" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentsmtp" runat="server">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">SMTP Server Information</h4>

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <div class="mb-3">
                Please provide your SMTP Server information down below. This information is used to send out emails from your website. If you do not know your SMTP Server information than you can request this from your hosting provider / ISP.
            </div>
            <div class="mb-3">
                <label id="SMTPServerLabel" clientidmode="Static" runat="server" for="SMTPServer">SMTP Server:</label>
                <input type="text" id="SMTPServer" runat="server" class="form-control" maxlength="100" text="127.0.0.1" />
                <asp:CustomValidator ID="SMTPServerRequired" runat="server" ControlToValidate="SMTPServer"
                    ClientValidationFunction="customFormValidator" ErrorMessage="SMTP Server is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="SMTPUserLabel" clientidmode="Static" runat="server" for="SMTPUser">SMTP User Name (Optional):</label>
                <input type="text" id="SMTPUser" runat="server" class="form-control" maxlength="100" />
            </div>
            <div class="mb-3">
                <label id="SMTPPassLabel" clientidmode="Static" runat="server" for="SMTPPass">SMTP Password (Optional):</label>
                <input type="password" id="SMTPPass" runat="server" class="form-control" maxlength="100" />
            </div>

            <div class="mb-3" align="center">
                <asp:Button CssClass="btn btn-secondary" ID="BackButton" runat="server" Text="Back" OnClick="BackButton_Click" />
                <asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue" OnClick="ContinueButton_Click" />
            </div>
        </div>
    </div>
</asp:content>