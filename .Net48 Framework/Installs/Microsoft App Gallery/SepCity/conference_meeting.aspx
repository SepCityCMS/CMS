<%@ Page Language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master" 
    CodeBehind="conference_meeting.aspx.cs" Inherits="wwwroot.conference_meeting" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        var jsonData;

        function changeUserName() {
            $.ajax({
                url: config.siteBase + 'api/videoconference/lookupuserid?UserName=' + $('#ctl00_SiteContent_UserName').val(),
                async: false,
                success: function (data) {
                    if (data != null) {
                        $('#ctl00_SiteContent_UserID').val(data.UserID);
                    } else {
                        $('#ctl00_SiteContent_UserID').val('');
                        $('#ctl00_SiteContent_UserName').val('');
                    }
                }
            });

            if ($('#ctl00_SiteContent_UserID').val() != '') {
                $.ajax({
                    url: config.siteBase + 'api/videoconference/scheduletimes?UserID=' + $('#ctl00_SiteContent_UserID').val(),
                    async: false,
                    success: function (data) {
                        jsonData = data;
                        setDays(data);
                        $('#ctl00_SiteContent_MeetingDate').removeAttr('disabled');
                        $('#ctl00_SiteContent_MeetingTime').removeAttr('disabled');
                    }
                });
            } else {
                alert('You have entered an invalid User Name.');
                $('#ctl00_SiteContent_UserName').val('');
                $('#ctl00_SiteContent_UserID').val('');
                $('#ctl00_SiteContent_MeetingDate').val('');
                $('#ctl00_SiteContent_MeetingTime').val('');
                $('#ctl00_SiteContent_MeetingDate').attr('disabled', 'disabled');
                $('#ctl00_SiteContent_MeetingTime').attr('disabled', 'disabled');
            }
        }

        function setDays(data) {
            var userDates = new Array();
            if (data.SundayAvailableStart == '' || data.SundayAvailableEnd == '') {
                userDates.push(0);
            }
            if (data.MondayAvailableStart == '' || data.MondayAvailableEnd == '') {
                userDates.push(1);
            }
            if (data.TuesdayAvailableStart == '' || data.TuesdayAvailableEnd == '') {
                userDates.push(2);
            }
            if (data.WednesdayAvailableStart == '' || data.WednesdayAvailableEnd == '') {
                userDates.push(3);
            }
            if (data.ThursdayAvailableStart == '' || data.ThursdayAvailableEnd == '') {
                userDates.push(4);
            }
            if (data.FridayAvailableStart == '' || data.FridayAvailableEnd == '') {
                userDates.push(5);
            }
            if (data.SaturdayAvailableStart == '' || data.SaturdayAvailableEnd == '') {
                userDates.push(6);
            }
            $('#ctl00_SiteContent_MeetingDate').datetimepicker('setOptions', { disabledWeekDays: userDates });
        }

        function setTimes(day) {
            var userMinTime;
            var userMaxTime;
            switch (day) {
                case 1:
                    userMinTime = jsonData.MondayAvailableStart;
                    userMaxTime = jsonData.MondayAvailableEnd;
                    break;
                case 2:
                    userMinTime = jsonData.TuesdayAvailableStart;
                    userMaxTime = jsonData.TuesdayAvailableEnd;
                    break;
                case 3:
                    userMinTime = jsonData.WednesdayAvailableStart;
                    userMaxTime = jsonData.WednesdayAvailableEnd;
                    break;
                case 4:
                    userMinTime = jsonData.ThursdayAvailableStart;
                    userMaxTime = jsonData.ThursdayAvailableEnd;
                    break;
                case 5:
                    userMinTime = jsonData.FridayAvailableStart;
                    userMaxTime = jsonData.FridayAvailableEnd;
                    break;
                case 6:
                    userMinTime = jsonData.SaturdayAvailableStart;
                    userMaxTime = jsonData.SaturdayAvailableEnd;
                    break;
                default:
                    userMinTime = jsonData.SundayAvailableStart;
                    userMaxTime = jsonData.SundayAvailableEnd;
            }
            $('#ctl00_SiteContent_MeetingTime').datetimepicker('setOptions', { minTime: userMinTime });
            $('#ctl00_SiteContent_MeetingTime').datetimepicker('setOptions', { maxTime: userMaxTime });
        }

        $(document).ready(function () {
            $('#ctl00_SiteContent_MeetingDate').datetimepicker({
                timepicker: false,
                format: 'm/d/Y',
                datepicker: true,
                minDate: 0,
                onSelectDate: function (ct, $i) {
                    $('#ctl00_SiteContent_MeetingTime').val('');
                    setTimes(ct.getDay());
                }
            });
            $('#ctl00_SiteContent_MeetingTime').datetimepicker({
                timepicker: true,
                format: 'H:i',
                datepicker: false
            });;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">Create Meeting</h4>
            <input type="hidden" runat="server" id="MeetingID" />
            <input type="hidden" runat="server" id="UserID" />
            
            <div class="mb-3">
                <label id="UserNameLabel" clientidmode="Static" runat="server" for="UserName">User Name:</label>
                <input type="text" id="UserName" class="form-control" runat="server" onblur="changeUserName();" />
                <asp:CustomValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="User Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="EventDateLabel" clientidmode="Static" runat="server" for="MeetingDate">Meeting Date:</label>
                <div class="form-group">
                    <div class="input-group date" id="datetimepicker1">
                        <input type="text" id="MeetingDate" class="form-control" runat="server" disabled="disabled" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="StartTimeLabel" clientidmode="Static" runat="server" for="MeetingTime">Meeting Time:</label>
                <div class="form-group">
                    <div class="input-group date" id="datetimepicker2">
                        <input type="text" id="MeetingTime" class="form-control" runat="server" disabled="disabled" />
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
                <sep:WYSIWYGEditor runat="server" ID="Message" Width="99%" Height="450" />
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>