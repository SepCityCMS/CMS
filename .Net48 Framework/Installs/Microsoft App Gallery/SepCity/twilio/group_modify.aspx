<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="group_modify.aspx.cs" inherits="wwwroot.twilio.group_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
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

            <input type="hidden" runat="server" id="GroupID" />
            <h4 id="ModifyLegend" runat="server">Add Group</h4>
            <div class="mb-3">
                <label id="GroupNameLabel" clientidmode="Static" runat="server" for="GroupName">Group Name:</label>
                <input type="text" id="GroupName" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="GroupNameRequired" runat="server" ControlToValidate="GroupName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Group Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>