<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="user_modify.aspx.cs" inherits="wwwroot.twilio.user_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/filters.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <nav class="navbar navbar-inverse" role="banner">
            <div class="collapse navbar-collapse navbar-left">
                <ul class="nav navbar-nav">
                    <li><a href="users.aspx">Users</a></li>
                    <li><a href="user_modify.aspx">Add User</a></li>
                    <li><a href="groups.aspx">Groups</a></li>
                    <li><a href="group_modify.aspx">Add Group</a></li>
                </ul>
            </div>
        </nav>

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

            <input type="hidden" runat="server" id="UserID" clientidmode="Static" />
            <h4 id="ModifyLegend" runat="server">Add User</h4>
            <div class="mb-3">
                <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                <input type="text" name="UserName" id="UserName" runat="server" class="form-control" placeholder="Click to select a user name" onclick="openUserSearch($(this).attr('id'), 'UserID')" />
                <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="GroupIDsLabel" clientidmode="Static" runat="server">Associated Groups:</label>
                <div runat="server" id="GroupIDsDiv"></div>
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>