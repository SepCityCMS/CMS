<%@ Page title="Activities" language="C#" viewstatemode="Enabled" Async="true" masterpagefile="Site.Master" 
    CodeBehind="zoom_meeting_create.aspx.cs" Inherits="wwwroot.spadmin.zoom_meeting_create" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(MeetingDate.ClientID, "true", "true", "")%>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">
        
        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 998;
            if (SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")) > 0) cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID"))) cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">
            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Create Zoom Meeting</h4>
                <input type="hidden" runat="server" ID="UserID" ClientIDMode="Static" />
                <input type="hidden" runat="server" ID="MeetingID" ClientIDMode="Static" />
                
                <div class="mb-3">
                    <label ID="ZoomUserLabel" clientidmode="Static" runat="server" for="ZoomUser">Zoom User:</label>
                    <select ID="ZoomUser" runat="server" ClientIDMode="Static" class="form-control" AutoPostBack="false">
                    </select>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ZoomUser"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Zoom User is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="MeetingDateLabel" clientidmode="Static" runat="server" for="MeetingDate">Meeting Date/Time:</label>
                    <div class="form-group">
                        <div class="input-group date" id="datetimepicker1">
                            <input type="text" id="MeetingDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="DurationLabel" clientidmode="Static" runat="server" for="Duration">Duration:</label>
                    <select ID="Duration" runat="server" ClientIDMode="Static" class="form-control" AutoPostBack="false">
                        <option value="15">15 Minutes</option>
                        <option value="30">30 Minutes</option>
                        <option value="45">45 Minutes</option>
                        <option value="60">1 Hour</option>
                        <option value="90">1 Hour, 30 Minutes</option>
                        <option value="120">2 Hours</option>
                        <option value="180">3 Hours</option>
                        <option value="240">4 Hours</option>
                        <option value="300">5 Hours</option>
                        <option value="360">6 Hours</option>
                        <option value="420">7 Hours</option>
                        <option value="480">8 Hours</option>
                    </select>
                    <asp:CustomValidator ID="DurationRequired" runat="server" ControlToValidate="Duration"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Duration is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="TopicLabel" clientidmode="Static" runat="server" for="Topic">Topic:</label>
                    <input type="text" name="Topic" id="Topic" runat="server" class="form-control" />
                    <asp:CustomValidator ID="TopicRequired" runat="server" ControlToValidate="Topic"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Topic is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="AgendaLabel" clientidmode="Static" runat="server" for="Agenda">Agenda:</label>
                    <textarea ID="Agenda" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="AgendaRequired" runat="server" ControlToValidate="Agenda"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Agenda is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>