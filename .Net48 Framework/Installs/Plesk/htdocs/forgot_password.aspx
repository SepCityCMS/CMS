<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="forgot_password.aspx.cs" inherits="wwwroot.forgot_password" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">
    
    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <span ID="idLoginErrorMsg" runat="server"></span>
    </span>

    <span ID="BackLiteral" runat="server"></span>

    <div class="LoginDiv" id="LoginDiv" runat="server">
        <h4>Reset Pasword</h4>
        <div class="mb-3">
            <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
            <input type="text" id="EmailAddress" runat="server" class="form-control" />
            <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                ValidationGroup="LoginUserValidationGroup">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <a href="signup.aspx">Signup Now</a> | <a href="login.aspx">Login</a>
        </div>
        
        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="PasswordButton" runat="server" Text="Submit" ValidationGroup="LoginUserValidationGroup" OnClick="PasswordButton_Click" />
        </div>
    </div>
</asp:content>