<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="matchmaker_profile_modify.aspx.cs" inherits="wwwroot.matchmaker_profile_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Create Your Profile</h4>
        <input type="hidden" runat="server" id="ProfileID" />

        <div class="mb-3">
            <label id="AboutMeLabel" clientidmode="Static" runat="server" for="AboutMe">About Me:</label>
            <sep:WYSIWYGEditor runat="server" ID="AboutMe" Width="99%" Height="260" />
        </div>
        <div class="mb-3">
            <label id="AboutMyMatchLabel" clientidmode="Static" runat="server" for="AboutMyMatch">About My Match:</label>
            <sep:WYSIWYGEditor runat="server" ID="AboutMyMatch" Width="99%" Height="260" />
        </div>
        <div class="mb-3">
            <asp:CheckBox ID="AllowComments" runat="server" />
            Allow users to leave you comments?
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 18;
            cCustomFields.FieldUniqueID = this.ProfileID.Value;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>
        <div class="mb-3">
            <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="18" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>