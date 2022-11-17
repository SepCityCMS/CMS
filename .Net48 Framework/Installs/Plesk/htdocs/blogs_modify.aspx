<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="blogs_modify.aspx.cs" inherits="wwwroot.blogs_modify1" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.StartDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.EndDate.ClientID, "false", "true", "$('#StartDate.ClientID').val()") %>;
            $('#<%= this.StartDate.ClientID %>')
                .on("dp.change",
                    function (e) {
                        $('#<%= this.EndDate.ClientID %>').data("DateTimePicker").minDate(e.date);
                });
            $('#<%= this.EndDate.ClientID %>')
                .on("dp.change",
                    function (e) {
                        $('#<%= this.StartDate.ClientID %>').data("DateTimePicker").maxDate(e.date);
                });
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Post a Blog</h4>
        <input type="hidden" runat="server" id="BlogID" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="61" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="BlogNameLabel" clientidmode="Static" runat="server" for="BlogName">Blog Name:</label>
            <input type="text" id="BlogName" runat="server" class="form-control" />
            <asp:CustomValidator ID="BlogNameRequired" runat="server" ControlToValidate="BlogName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Blog Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="StartDateLabel" clientidmode="Static" runat="server" for="StartDate">Start Date:</label>
            <div class="form-group">
                <div class="input-group date" id="StartDateDiv">
                    <input type="text" id="StartDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <label id="EndDateLabel" clientidmode="Static" runat="server" for="EndDate">End Date:</label>
            <div class="form-group">
                <div class="input-group date" id="EndDateDiv">
                    <input type="text" id="EndDate" class="form-control" runat="server" />
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="BlogText" Width="99%" Height="450" />
        </div>
        <div class="mb-3">
            <label id="AllowCommentsLabel" clientidmode="Static" runat="server" for="AllowComments">Allow other users to leave you comments on this blog:</label>
            <select id="AllowComments" runat="server" class="form-control">
                <option value="true">Yes</option>
                <option value="false">No</option>
            </select>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>