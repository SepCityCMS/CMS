<%@ page title="News" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="news_modify.aspx.cs" inherits="wwwroot.news_modify" %>
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
            cAdminModuleMenu.ModuleID = 23;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add News</h4>
                <input type="hidden" runat="server" ID="NewsID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="23" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="23" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="TopicNameLabel" clientidmode="Static" runat="server" for="TopicName">Topic Name:</label>
                    <input type="text" ID="TopicName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="TopicNameRequired" runat="server" ControlToValidate="TopicName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Topic Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="AdditionalOptionsLabel" clientidmode="Static" runat="server" for="AdditionalOptions">Additional Options:</label>
                    <select ID="AdditionalOptions" runat="server" class="form-control">
                        <option value="Parent Window (Default)">Parent</option>
                        <option value="New Window">NewWin</option>
                        <option value="No Link to Story">NoLink</option>
                        <option value="Forward to URL">Forward</option>
                    </select>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
                    <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="23" />
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
                    <label ID="NewsHeadlineLabel" clientidmode="Static" runat="server" for="NewsHeadline">News Headline:</label>
                    <input type="text" ID="NewsHeadline" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="NewsHeadlineRequired" runat="server" ControlToValidate="NewsHeadline"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="News Headline is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor Runat="server" ID="NewsStory" Width="99%" Height="450" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>