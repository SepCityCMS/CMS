<%@ page title="User Profiles" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="userprofiles_modify.aspx.cs" inherits="wwwroot.userprofiles_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 63;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Profile</h4>
                <input type="hidden" runat="server" ID="ProfileID" />
                <input type="hidden" runat="server" ID="UserID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="63" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3" id="ProfileTypeRow" runat="server">
                    <label ID="ProfileTypeLabel" clientidmode="Static" runat="server" for="ProfileType">Profile Type:</label>
                    <select ID="ProfileType" runat="server" />
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="HotNot" runat="server" /> Show Pictures in Hot or Not?
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="AllowComments" runat="server" /> Allow users to leave you comments?
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor Runat="server" ID="AboutMe" Width="99%" Height="450" />
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 63;
                    cCustomFields.FieldUniqueID = ProfileID.Value;
                    if(sUserID != "") {
                        cCustomFields.UserID = sUserID;
                    } else {
                        cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
                    }
                    Response.Write(cCustomFields.Render()); 
                %>
                <div class="mb-3">
                    <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
                    <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="63" />
                </div>
                <div class="mb-3" id="AudioFilesRow" runat="server">
                    <label ID="AudioFilesLabel" clientidmode="Static" runat="server" for="AudioFiles">Audio Files:</label>
                    <sep:UploadFiles ID="AudioFiles" runat="server" Mode="MultipleFiles" FileType="Audio" ModuleID="63" />
                </div>
                <div id="ProfileColorOptions" runat="server">
                    <div class="mb-3">
                        Customize Profile Colors
                    </div>
                    <table width="100%">
                        <tr>
                            <td width="33%">
                                <div class="mb-3">
                                    <label ID="BGColorLabel" clientidmode="Static" runat="server" for="BGColor">Background Color:</label>
                                    <sep:ColorPicker ID="BGColor" runat="server" />
                                </div>
                            </td>
                            <td width="33%">
                                <div class="mb-3">
                                    <label ID="TextColorLabel" clientidmode="Static" runat="server" for="TextColor">Text Color:</label>
                                    <sep:ColorPicker ID="TextColor" runat="server" />
                                </div>
                            </td>
                            <td width="33%">
                                <div class="mb-3">
                                    <label ID="LinkColorLabel" clientidmode="Static" runat="server" for="LinkColor">Link Color:</label>
                                    <sep:ColorPicker ID="LinkColor" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>