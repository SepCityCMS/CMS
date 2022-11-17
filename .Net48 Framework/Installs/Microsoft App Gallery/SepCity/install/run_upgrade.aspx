<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="run_upgrade.aspx.cs" inherits="wwwroot.run_upgrade" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentupgrade" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

            <h4 id="Legend2" runat="server">Activation Information</h4>
            <div class="mb-3">
                Please enter your serial number information below. If you do not have a serial number than <a href="https://www.sepcity.com/activation.aspx" target="_blank">click here to get a serial number for free</a>.
            </div>
            <div class="mb-3">
                <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                <input type="text" id="UserName" runat="server" class="form-control" maxlength="25" clientidmode="Static" />
                <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="PasswordLabel" clientidmode="Static" runat="server" for="Password">Enter a Password:</label>
                <input type="password" runat="server" id="Password" class="form-control" maxlength="12" />
                <asp:CustomValidator ID="RePasswordRequired" runat="server" ControlToValidate="Password"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Password is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="LicenseKeyLabel" clientidmode="Static" runat="server" for="LicenseKey">License Key:</label>
                <input type="text" id="LicenseKey" runat="server" class="form-control" maxlength="40" />
                <asp:CustomValidator ID="SerialNumRequired" runat="server" ControlToValidate="LicenseKey"
                    ClientValidationFunction="customFormValidator" ErrorMessage="License Key is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
        </div>

        <div class="ModFormDiv">

            <h4 id="Legend1" runat="server">SMTP Server Information</h4>
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
                <input type="text" id="SMTPPass" runat="server" class="form-control" textmode="Password" maxlength="100" />
            </div>
        </div>

        <div class="mb-3" align="center">
            <asp:Button CssClass="btn btn-secondary" ID="BackButton" runat="server" Text="Back" OnClick="BackButton_Click" />
            <asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Run Update" OnClick="ContinueButton_Click" />
        </div>
    </div>
</asp:content>