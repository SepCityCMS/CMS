<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userpages_site_view.aspx.cs" inherits="wwwroot.userpages_site_view" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <div class="LoginDiv" id="PasswordDiv" runat="server" visible="false">
        <span id="failureNotification">
            <span ID="idPasswordErrorMsg" runat="server"></span>
        </span>

        <h4 id="PasswordLegend" runat="server">Password Required</h4>
        <div class="mb-3">
            <label id="PagePasswordLabel" clientidmode="Static" runat="server" for="PagePassword">Enter Password Below to View this Page:</label>
            <input type="text" id="PagePassword" runat="server" class="form-control" />
        </div>

        <div class="row submitButton">
            <div class="mb-3">
                <asp:Button CssClass="btn btn-primary" ID="PasswordButton" runat="server" Text="View Page" OnClick="PasswordButton_Click" />
            </div>
        </div>
    </div>

    <div class="LoginDiv" id="RequestAccessDiv" runat="server" visible="false">
        <span id="failureNotification">
            <span ID="idAccessErrorMsg" runat="server"></span>
        </span>

        <h4 id="AccessLegend" runat="server">Request Access</h4>
        <div class="mb-3">
            <span>This web site requires that you request access to view this page.</span>
        </div>

        <div class="row submitButton">
            <div class="mb-3">
                <asp:Button CssClass="btn btn-primary" ID="RequestButton" runat="server" Text="Send Request" onclick="RequestButton_Click" />
            </div>
        </div>
    </div>

    <div id="Guestbook" runat="server" visible="false">
        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <asp:GridView ID="GuestbookGridView" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false" Width="100%"
            AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table width="100%" align="center" class="Table">
                            <tr>
                                <td>
                                    <b>Name</b> <%#
                this.Eval("EmailAddress") %>
                                </td>
                                <td>
                                    <b>Site URL</b> <a href="<%#
                this.Eval("SiteURL") %>"
                                        target="_blank"><%#
                this.Eval("SiteURL") %></a>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><%#
                SepCommon.SepCore.Strings.Replace(this.Eval("Message").ToString(), Environment.NewLine, "<br />") %></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <b>Date Posted</b> <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Sign the Guestbook</h4>
            <input type="hidden" id="EntryID" runat="server" />
            <div class="mb-3">
                <label id="YourNameLabel" clientidmode="Static" runat="server" for="YourName">Your Name:</label>
                <input type="text" id="YourName" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="WebSiteURLLabel" clientidmode="Static" runat="server" for="WebSiteURL">Web Site URL:</label>
                <input type="text" id="WebSiteURL" runat="server" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="MessageLabel" clientidmode="Static" runat="server" for="Message">Message:</label>
                <textarea id="Message" runat="server" class="form-control"></textarea>
            </div>
            <div class="mb-3" id="CaptchaRow" runat="server">
                <sep:Captcha ID="Recaptcha1" runat="server" />
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </div>
</asp:content>