<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="activation.aspx.cs" inherits="wwwroot.activation1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentactinfo" runat="server">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Activation Information</h4>

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

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

            <div class="mb-3" align="center">
                <asp:Button CssClass="btn btn-secondary" ID="BackButton" runat="server" Text="Back" OnClick="BackButton_Click" />
                <asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue" OnClick="ContinueButton_Click" />
            </div>
        </div>
    </div>
</asp:content>