<%@ page title="Polls" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="polls_modify.aspx.cs" inherits="wwwroot.polls_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(StartDate.ClientID, "false", "true", "")%>;
            <%=SepFunctions.Date_Picker(EndDate.ClientID, "false", "true", "$('#StartDate.ClientID').val()")%>;
            $('#<%=StartDate.ClientID%>')
                .on("dp.change",
                    function (e) {
                        $('#<%=EndDate.ClientID%>').data("DateTimePicker").minDate(e.date);
                });
            $('#<%=EndDate.ClientID%>')
                .on("dp.change",
                    function (e) {
                        $('#<%=StartDate.ClientID%>').data("DateTimePicker").maxDate(e.date);
                });
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 25;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFieldset" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Poll</h4>
                <input type="hidden" runat="server" ID="PollID" />

                <div class="mb-3">
                    <label ID="PollQuestionLabel" clientidmode="Static" runat="server" for="PollQuestion">Poll Question:</label>
                    <input type="text" ID="PollQuestion" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="PollQuestionRequired" runat="server" ControlToValidate="PollQuestion"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Poll Question is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PhotoLabel" clientidmode="Static" runat="server" for="Photo">Attach a Photo:</label>
                    <sep:UploadFiles ID="Photo" runat="server" Mode="SingleFile" FileType="Images" ModuleID="25" />
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalSelection ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" Text="|-1|" />
                </div>
                <div class="mb-3">
                    <label ID="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Start Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="StartDateDiv">
                            <input type="text" id="StartDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="EndDateLabel" clientidmode="Static" runat="server" for="EndDate">End Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="EndDateDiv">
                            <input type="text" id="EndDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <b>Choices</b>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>