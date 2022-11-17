<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="login.aspx.cs" inherits="wwwroot.login" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <div class="login-frm">
        <span id="failureNotification">
            <span ID="idLoginErrorMsg" runat="server"></span>
        </span>
        <input type="hidden" id="LinkedInId" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_Token" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_Id" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_User" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_Email" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_FName" runat="server" clientidmode="Static" />
        <input type="hidden" id="Facebook_LName" runat="server" clientidmode="Static" />

        <div class="row FacebookImgRow" id="FacebookRow" runat="server">
            <a onclick="fbLogin();" href="javascript:void(0)" class="fb_button fb_button_medium">
                <span class="fb_button_text">Sign in with Facebook</span></a>
        </div>
        <div class="row LinkedInImgRow" id="LinkedInRow" runat="server">
            <script type="in/Login"></script>
        </div>
        <div class="form-group">
            <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
            <input type="text" id="UserName" runat="server" class="form-control" clientidmode="Static" />
        </div>
        <div class="form-group">
            <label id="UserPasswordLabel" clientidmode="Static" runat="server" for="Password">Password:</label>
            <input type="password" id="Password" runat="server" class="form-control" textmode="Password" clientidmode="Static" autocomplete="off" />
        </div>
        <div class="form-group">
            <ul>
                <li>
                    <asp:CheckBox ID="RememberMe" runat="server" /> Remember Me
                </li>
                <li>
                    <a href="forgot_password.aspx">Forgot Password?</a>
                </li>
            </ul>
        </div>

        <div class="form-group">
            <asp:Button CssClass="btn btn-primary" ID="LoginButton" runat="server" Text="Log In" OnClick="LoginButton_Click" />
        </div>

        <div id="SignupPassDiv" runat="server" class="form-group">
            <p class="para3">Don’t having Account?<a href="signup.aspx"> Register</a></p>
        </div>
    </div>
</asp:content>