<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="events_modify.aspx.cs" inherits="wwwroot.events_modify" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.EventDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.BegTime.ClientID, "true", "false", "") %>;
            <%= SepFunctions.Date_Picker(this.EndTime.ClientID, "true", "false", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">Add Event</h4>
            <input type="hidden" runat="server" id="EventID" />

            <div class="mb-3">
                <label id="EventTypeLabel" clientidmode="Static" runat="server" for="EventType">Event Type:</label>
                <select id="EventType" runat="server" class="form-control">
                </select>
                <asp:CustomValidator ID="EventTypeRequired" runat="server" ControlToValidate="EventType"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Event Type is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="EventDateLabel" clientidmode="Static" runat="server" for="EventDate">Event Date:</label>
                <div class="form-group">
                    <div class="input-group date" id="datetimepicker1">
                        <input type="text" id="EventDate" class="form-control" runat="server" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
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
                <label id="EndTimeLabel" clientidmode="Static" runat="server" for="EndTime">End Time:</label>
                <div class="form-group">
                    <div class="input-group date" id="datetimepicker3">
                        <input type="text" id="EndTime" class="form-control" runat="server" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <label id="RecurringLabel" clientidmode="Static" runat="server" for="Recurring">Recurring Options:</label>
                <input type="text" id="Recurring" runat="server" ckass="form-control inlineBlock" clientidmode="Static" width="45" />
                <select id="RecurringCycle" runat="server" class="form-control inlineBlock" width="100">
                    <option value="D">Days</option>
                    <option value="W">Weeks</option>
                    <option value="M">Months</option>
                    <option value="Y">Years</option>
                </select>
                for
                    <input type="text" id="Duration" runat="server" ckass="form-control inlineBlock" clientidmode="Static" width="45" text="0" />
                times.
            </div>
            <div class="mb-3" id="EventPriceRow" runat="server">
                <asp:CheckBox ID="EnablePricing" runat="server" Text="Enable Ticket Pricing" value="1" onclick="if(this.checked == true){$('#Price1Row').show();$('#Price2Row').show();}else{$('#Price1Row').hide();$('#Price2Row').hide();}" />
                <div class="mb-3" id="Price1Row" style="display: none">
                    <label id="OnlinePriceLabel" clientidmode="Static" runat="server" for="OnlinePrice">Price to purchase ticket online:</label>
                    <input type="text" id="OnlinePrice" runat="server" class="form-control" clientidmode="Static" />
                </div>
                <div class="mb-3" id="Price2Row" style="display: none">
                    <label id="DoorPriceLabel" clientidmode="Static" runat="server" for="DoorPrice">Price to purchase ticket at the door:</label>
                    <input type="text" id="DoorPrice" runat="server" class="form-control" clientidmode="Static" />
                </div>
            </div>
            <div class="mb-3">
                <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Select Pictures:</label>
                <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="46" />
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
            <div class="mb-3">
                <asp:CheckBox ID="ShareEvent" runat="server" Text="Share this event with all of the members" />
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>