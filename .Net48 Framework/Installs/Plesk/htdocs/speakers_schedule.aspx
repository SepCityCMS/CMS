<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="speakers_schedule.aspx.cs" inherits="wwwroot.speakers_schedule" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.SpeakerDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.SpeakerTime.ClientID, "true", "false", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Request a Speaker Form</h4>
        <input type="hidden" runat="server" id="SpeakerID" />

        <div class="mb-3">
            <label id="SpeakersNameLabel" clientidmode="Static" runat="server" for="SpeakersName">Speaker's Name:</label>
            <input type="text" id="SpeakersName" runat="server" class="form-control" readonly="true" clientidmode="Static" />
            <asp:CustomValidator ID="SpeakersNameRequired" runat="server" ControlToValidate="SpeakersName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Speaker's Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="YourNameLabel" clientidmode="Static" runat="server" for="YourName">Your Name:</label>
            <input type="text" id="YourName" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="YourNameRequired" runat="server" ControlToValidate="YourName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Your Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SpeechIDLabel" clientidmode="Static" runat="server" for="SpeechID">Subject:</label>
            <select id="SpeechID" runat="server" class="form-control" clientidmode="Static"></select>
            <asp:CustomValidator ID="SpeechIDRequired" runat="server" ControlToValidate="SpeechID"
                ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Company Name:</label>
            <input type="text" id="CompanyName" runat="server" class="form-control" clientidmode="Static" />
        </div>
        <div class="mb-3">
            <label id="StreetAddressLabel" clientidmode="Static" runat="server" for="StreetAddress">Street Address:</label>
            <input type="text" id="StreetAddress" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="StreetAddressRequired" runat="server" ControlToValidate="StreetAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Street Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="CityLabel" clientidmode="Static" runat="server" for="City">City:</label>
            <input type="text" id="City" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="CityRequired" runat="server" ControlToValidate="City"
                ClientValidationFunction="customFormValidator" ErrorMessage="City is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="PhoneNumberLabel" clientidmode="Static" runat="server" for="PhoneNumber">Phone Number:</label>
            <input type="text" id="PhoneNumber" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="PhoneNumberRequired" runat="server" ControlToValidate="PhoneNumber"
                ClientValidationFunction="customFormValidator" ErrorMessage="Phone Number is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="EmailAddressLabel" clientidmode="Static" runat="server" for="EmailAddress">Email Address:</label>
            <input type="text" id="EmailAddress" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="EmailAddressRequired" runat="server" ControlToValidate="EmailAddress"
                ClientValidationFunction="customFormValidator" ErrorMessage="Email Address is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="SpeakerDateLabel" clientidmode="Static" runat="server" for="SpeakerDate">Date for Speaker:</label>
            <div class="form-group">
                <div class="input-group date" id="SpeakerDateDiv">
                    <input type="text" id="SpeakerDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="SpeakerTimeLabel" clientidmode="Static" runat="server" for="SpeakerTime">Time for Speaker:</label>
            <div class="form-group">
                <div class="input-group date" id="SpeakerTimeDiv">
                    <input type="text" id="SpeakerTime" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                </div>
            </div>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Request Speaker" onclick="SaveButton_Click" />
        </div>
    </div>
</asp:content>