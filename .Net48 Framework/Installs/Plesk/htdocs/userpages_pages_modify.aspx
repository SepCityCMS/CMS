<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userpages_pages_modify.aspx.cs" inherits="wwwroot.userpages_pages_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Web Page</h4>
        <input type="hidden" runat="server" id="PageID" />

        <div class="mb-3" id="MenuRow" runat="server">
            <label id="MenuIDLabel" clientidmode="Static" runat="server" for="MenuID">Select a selection where to list this page:</label>
            <sep:MenuDropdown ID="MenuID" runat="server" CssClass="form-control" ShowNotOnAMenu="false" OnUserPage="true" />
        </div>
        <div class="mb-3">
            <label id="PageTitleLabel" clientidmode="Static" runat="server" for="PageTitle">Page Title:</label>
            <input type="text" id="PageTitle" runat="server" class="form-control" />
            <asp:CustomValidator ID="PageTitleRequired" runat="server" ControlToValidate="PageTitle"
                ClientValidationFunction="customFormValidator" ErrorMessage="Page Title is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 7;
            cCustomFields.FieldUniqueID = this.PageID.Value;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="PageText" Width="99%" Height="450" moduleid="7" Mode="userpage" />
        </div>
        <div class="mb-3">
            <label id="PagePasswordLabel" clientidmode="Static" runat="server" for="PagePassword">Password for this Page:</label>
            <input type="text" id="PagePassword" runat="server" class="form-control" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>