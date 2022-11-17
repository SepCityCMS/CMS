<%@ page title="Match Maker" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="matchmaker_modify.aspx.cs" inherits="wwwroot.matchmaker_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 18;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Profile</h4>
                <input type="hidden" runat="server" ID="ProfileID" />
                <input type="hidden" runat="server" ID="UserID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="18" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="AboutMeLabel" clientidmode="Static" runat="server" for="AboutMe">About Me:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="AboutMe" Width="99%" Height="260" />
                </div>
                <div class="mb-3">
                    <label ID="AboutMyMatchLabel" clientidmode="Static" runat="server" for="AboutMyMatch">About My Match:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="AboutMyMatch" Width="99%" Height="260" />
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="AllowComments" runat="server" /> Allow users to leave you comments?
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 18;
                    cCustomFields.FieldUniqueID = ProfileID.Value;
                    cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
                    Response.Write(cCustomFields.Render()); 
                %>
                <div class="mb-3">
                    <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
                    <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="18" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>