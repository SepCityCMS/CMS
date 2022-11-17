<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="celebrities_schedule_call.aspx.cs" inherits="wwwroot.conference_schedule_call" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.EventDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.BegTime.ClientID, "true", "false", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="PageContent" runat="server">

        <asp:Panel ID="UpdatePanel" runat="server">

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Schedule Call</h4>
                <input type="hidden" runat="server" id="EventID" />
                <input type="hidden" runat="server" id="ProfileID" />
                <div class="mb-3">
                    <label id="EventDateLabel" clientidmode="Static" runat="server" for="EventDate">Request Call Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="datetimepicker1">
                            <input type="text" id="EventDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label id="StartTimeLabel" clientidmode="Static" runat="server" for="BegTime">Start Time:</label>
                    <div class="form-group">
                        <div class="input-group date" id="datetimepicker2">
                            <input type="text" id="BegTime" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label id="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
                    <input type="text" id="Subject" runat="server" class="form-control" clientidmode="Static" />
                    <asp:CustomValidator ID="SubjectRequired" runat="server" ControlToValidate="Subject"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor runat="server" ID="EventContent" Width="99%" Height="450" />
                </div>

                <hr class="mb-4" />
                <div class="mb-3">
                    <asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Schedule Now" OnClick="SaveButton_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:content>