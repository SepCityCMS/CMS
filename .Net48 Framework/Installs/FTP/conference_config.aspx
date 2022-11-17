<%@ Page Language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master" 
    CodeBehind="conference_config.aspx.cs" Inherits="wwwroot.conference_config" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.SundayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.SundayAvailableEnd.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.MondayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.MondayAvailableEnd.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.TuesdayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.TuesdayAvailableEnd.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.WednesdayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.WednesdayAvailableEnd.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.ThursdayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.ThursdayAvailableEnd.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.FridayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.FridayAvailableEnd.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.SaturdayAvailableStart.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.SaturdayAvailableEnd.ClientID, "true", "false", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">Configuration</h4>
            
            <div class="mb-3">
                <label id="ContactOnlineLabel" clientidmode="Static" runat="server" for="ContactOnline">Allow users to contact me if I am online:</label>
                <select id="ContactOnline" runat="server" class="form-control">
                    <option value="true">Yes</option>
                    <option value="false">No</option>
                </select>
            </div>
            <div class="mb-3">
                <label id="SundayAvailableStartLabel" clientidmode="Static" runat="server" for="SundayAvailableStart">Sunday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="SundayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="SundayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="MondayAvailableStartLabel" clientidmode="Static" runat="server" for="MondayAvailableStart">Monday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="MondayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="MondayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="TuesdayAvailableStartLabel" clientidmode="Static" runat="server" for="TuesdayAvailableStart">Tuesday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="TuesdayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="TuesdayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="WednesdayAvailableStartLabel" clientidmode="Static" runat="server" for="WednesdayAvailableStart">Wednesday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="WednesdayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="WednesdayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="ThursdayAvailableStartLabel" clientidmode="Static" runat="server" for="ThursdayAvailableStart">Thursday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="ThursdayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="ThursdayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="FridayAvailableStartLabel" clientidmode="Static" runat="server" for="FridayAvailableStart">Friday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="FridayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="FridayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="SaturdayAvailableStartLabel" clientidmode="Static" runat="server" for="SaturdayAvailableStart">Saturday Availability Times:</label>
                <div class="form-row">
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="SaturdayAvailableStart" class="form-control" runat="server" placeholder="Start Time" />
                    </div>
                    <div class="col date" id="datetimepicker2">
                        <input type="text" id="SaturdayAvailableEnd" class="form-control" runat="server" placeholder="End Time" />
                    </div>
                </div>
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
</asp:content>