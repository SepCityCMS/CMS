<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="device_modify.aspx.cs" inherits="wwwroot.twilio.device_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <nav class="navbar navbar-inverse" role="banner">
            <div class="collapse navbar-collapse navbar-left">
                <ul class="nav navbar-nav">
                    <li><a href="devices.aspx">Devices</a></li>
                    <li><a href="device_modify.aspx">Add Device</a></li>
                </ul>
            </div>
        </nav>

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

            <input type="hidden" runat="server" id="DeviceID" clientidmode="Static" />
            <h4 id="ModifyLegend" runat="server">Add Device</h4>
            <div class="mb-3">
                <label id="DeviceNameLabel" clientidmode="Static" runat="server" for="DeviceName">Device Name:</label>
                <input type="text" id="DeviceName" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="DeviceNameRequired" runat="server" ControlToValidate="DeviceName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Device Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
                <input type="text" id="PhoneNumber" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="PhoneNumberRequired" runat="server" ControlToValidate="PhoneNumber"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Phone Number is required."
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