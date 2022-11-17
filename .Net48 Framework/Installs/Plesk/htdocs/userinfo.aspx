<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userinfo.aspx.cs" inherits="wwwroot.userinfo" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function loadOverview() {
            $('#OverviewDiv').show();
            $('#ActivityDiv').hide();
            return false;
        }

        function loadActivity() {
            $('#OverviewDiv').hide();
            $('#ActivityDiv').show();
            return false;
        }

        $(document).ready(function () {
            loadOverview();
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <p align="center">
        <b>Search</b>
        <input type="text" id="Keywords" runat="server" class="inline-block" style="width: 200px" />
        <asp:Button ID="SearchButton" runat="server" CssClass="btn btn-light" Text="Go" OnClick="SearchButton_Click" />
    </p>

    <div class="GridViewStyle">
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
            CssClass="GridViewStyle" Visible="false">
            <Columns>
                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <a href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>'><%#
                this.Eval("Username") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("FirstName") %> <%#
                this.Eval("LastName") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="UserInformation" runat="server">
        <div class="row profile">
            <div class="col-md-3">
                <div class="profile-sidebar">
                    <!-- SIDEBAR USERPIC -->
                    <div class="profile-userpic" style="text-align:center;">
                        <span ID="ProfileImage" runat="server"></span>
                    </div>
                    <!-- END SIDEBAR USERPIC -->
                    <!-- SIDEBAR USER TITLE -->
                    <div class="profile-usertitle">
                        <div class="profile-usertitle-name">
                            <span ID="UserName" runat="server"></span>
                        </div>
                        <div class="profile-usertitle-job">
                            <span ID="Location" runat="server"></span>
                        </div>
                    </div>
                    <!-- END SIDEBAR USER TITLE -->
                    <!-- SIDEBAR MENU -->
                    <div class="profile-usermenu">
                        <ul class="nav">
                            <li class="active">
                                <a href="javascript:void(0)" onclick="loadOverview();">
                                    <i class="glyphicon glyphicon-home"></i>
                                    Overview </a>
                            </li>
                            <li>
                                <a href="javascript:void(0)" onclick="loadActivity()">
                                    <i class="glyphicon glyphicon-user"></i>
                                    Activity </a>
                            </li>
                        </ul>
                    </div>
                    <!-- END MENU -->
                </div>
            </div>
            <div class="col-md-9">
                <div class="profile-content">
                    <div id="OverviewDiv">
                        <span ID="OnlineText" runat="server"></span>
                        <br />
                        <strong>Join Date:</strong>
                        <span ID="JoinDate" runat="server"></span>
                        <br />
                        <strong>Last Login:</strong>
                        <span ID="LastLogin" runat="server"></span>
                        <br />
                        <strong>Membership:</strong>
                        <span ID="Membership" runat="server"></span>
                        <!-- SIDEBAR BUTTONS -->
                        <div class="profile-userbuttons">
                            <a id="AddFriend" runat="server" class="btn btn-success" href="<%= this.GetInstallFolder() %>friends.aspx?DoAction=SaveFriend&amp;UserID=<%= sUserID %>">Add as Friend</a>
                            <a id="MessageButton" class="btn btn-danger" href="<%= this.GetInstallFolder() %>messenger_compose.aspx?UserID=<%= sUserID %>">Message</a>
                            <a id="VideoCall" runat="server" clientidmode="static" class="btn btn-secondary" href="javascript:void(0)">Video Call</a>
                        </div>
                        <!-- END SIDEBAR BUTTONS -->
                    </div>
                    <div id="ActivityDiv">
                        <span ID="Activity" runat="server"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:content>