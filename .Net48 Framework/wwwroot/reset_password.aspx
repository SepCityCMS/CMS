<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="reset_password.aspx.cs" inherits="wwwroot.reset_password" %>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Reset Pasword</h1>

    <span id="failureNotification">
        <span ID="idLoginErrorMsg" runat="server"></span>
    </span>

    <span ID="BackLiteral" runat="server"></span>

    <div class="LoginDiv" id="LoginDiv" runat="server">
        <input type="hidden" id="ResetID" runat="server" clientidmode="Static" />
        <input type="hidden" id="UserID" runat="server" clientidmode="Static" />

        <div class="mb-3" id="SecretQuestionRow" runat="server">
            <label id="SwcretQuestionLabel" clientidmode="Static" runat="server" for="SecretQuestion">Secret Question:</label>
            <br />
            <strong>
                <span ID="SecretQuestion" runat="server"></span>
            </strong>
        </div>
        <div class="mb-3" id="SecretAnswerRow" runat="server">
            <label id="SecretAnswerLabel" clientidmode="Static" runat="server" for="SecretAnswer">Secret Answer:</label>
            <input type="text" id="SecretAnswer" runat="server" class="form-control" />
            <asp:CustomValidator ID="SecretAnswerRequired" runat="server" ControlToValidate="SecretAnswer"
                ClientValidationFunction="customFormValidator" ErrorMessage="Secret Answer is required."
                ValidationGroup="LoginUserValidationGroup">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="PasswordLabel" clientidmode="Static" runat="server" for="Password">Enter a Password:</label>
            <input type="password" runat="server" id="Password" class="form-control" maxlength="21" />
            <asp:RegularExpressionValidator ID="PasswordRegularExpression1" runat="server" Display="dynamic"
                ControlToValidate="Password"
                ErrorMessage="Password must contain one of @#$%^&*/!."
                ValidationExpression=".*[@#$%^&*/!].*" />
            <asp:RegularExpressionValidator ID="PasswordRegularExpression2" runat="server" Display="dynamic"
                ControlToValidate="Password"
                ErrorMessage="Password must be between 4-20 characters."
                ValidationExpression="[^\s]{4,20}" />
            <asp:CustomValidator ID="RePasswordRequired" runat="server" ControlToValidate="Password"
                ClientValidationFunction="customFormValidator" ErrorMessage="Password is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="RePasswordLabel" clientidmode="Static" runat="server" for="RePassword">Re-enter a Password:</label>
            <input type="password" runat="server" id="RePassword" class="form-control" maxlength="21" />
            <asp:CompareValidator ID="RePasswordCompare" runat="server"
                ControlToValidate="RePassword"
                ControlToCompare="Password"
                ErrorMessage="Passwords do not match." />
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