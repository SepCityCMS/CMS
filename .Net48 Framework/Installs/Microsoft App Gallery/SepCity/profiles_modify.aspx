<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="profiles_modify.aspx.cs" inherits="wwwroot.profiles_modify" %>

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

        <div class="mb-3" id="ProfileTypeRow" runat="server">
            <label id="ProfileTypeLabel" clientidmode="Static" runat="server" for="ProfileType">Profile Type:</label>
            <select id="ProfileType" runat="server" />
        </div>
        <div class="mb-3" id="HotNotRow" runat="server">
            <asp:CheckBox ID="HotNot" runat="server" />
            Show Pictures in Hot or Not?
        </div>
        <div class="mb-3">
            <asp:CheckBox ID="AllowComments" runat="server" />
            Allow users to leave you comments?
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="AboutMe" Width="99%" Height="450" />
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 63;
            cCustomFields.FieldUniqueID = this.ProfileID.Value;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>
        <div class="mb-3">
            <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="63" />
        </div>
        <div class="mb-3" id="AudioFilesRow" runat="server">
            <label id="AudioFilesLabel" clientidmode="Static" runat="server" for="AudioFiles">Audio Files:</label>
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
                            <label id="BGColorLabel" clientidmode="Static" runat="server" for="BGColor">Background Color:</label>
                            <sep:ColorPicker ID="BGColor" runat="server" />
                        </div>
                    </td>
                    <td width="33%">
                        <div class="mb-3">
                            <label id="TextColorLabel" clientidmode="Static" runat="server" for="TextColor">Text Color:</label>
                            <sep:ColorPicker ID="TextColor" runat="server" />
                        </div>
                    </td>
                    <td width="33%">
                        <div class="mb-3">
                            <label id="LinkColorLabel" clientidmode="Static" runat="server" for="LinkColor">Link Color:</label>
                            <sep:ColorPicker ID="LinkColor" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>