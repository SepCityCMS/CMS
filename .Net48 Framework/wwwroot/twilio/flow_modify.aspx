<%@ page title="Twilio Control Panel" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="flow_modify.aspx.cs" inherits="wwwroot.twilio.flow_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <nav class="navbar navbar-inverse" role="banner">
            <div class="collapse navbar-collapse navbar-left">
                <ul class="nav navbar-nav">
                    <li><a href="flows.aspx">Flows</a></li>
                    <li><a href="flow_modify.aspx">Add Flow</a></li>
                </ul>
            </div>
        </nav>

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <input type="hidden" runat="server" id="FlowID" clientidmode="Static" />
            <h4 id="ModifyLegend" runat="server">Add Flow</h4>
            <div class="mb-3">
                <label id="FlowNameLabel" clientidmode="Static" runat="server" for="FlowName">Flow Name:</label>
                <input type="text" id="FlowName" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="FlowNameRequired" runat="server" ControlToValidate="FlowName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Flow Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="NumberIDsLabel" clientidmode="Static" runat="server">Associated Numbers:</label>
                <div runat="server" id="NumberIDsDiv"></div>
            </div>
            <div class="mb-3">
                <label id="IncomingCallLabel" clientidmode="Static" runat="server" for="IncomingCall">Incoming Call Action:</label>
                <asp:DropDownList ID="IncomingCall" runat="server" CssClass="form-control" AutoPostBack="True" clientidmode="Static" EnableViewState="True" OnSelectedIndexChanged="IncomingCall_SelectedIndex">
                    <asp:ListItem Text="Main Menu" Value="Menu" />
                    <asp:ListItem Text="Call a Group" Value="Group" />
                    <asp:ListItem Text="Call a User" Value="User" />
                    <asp:ListItem Text="Call a Device" Value="Device" />
                </asp:DropDownList>
            </div>
            <asp:Panel ID="CallActions" runat="server">
            </asp:Panel>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>