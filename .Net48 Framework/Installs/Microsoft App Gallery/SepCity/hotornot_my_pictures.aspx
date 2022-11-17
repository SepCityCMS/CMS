<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="hotornot_my_pictures.aspx.cs" inherits="wwwroot.hotornot_my_pictures" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">My Pictures</h4>
        <input type="hidden" id="UploadID" runat="server" />

        <div class="mb-3">
            <label id="YourPicturesLabel" clientidmode="Static" runat="server" for="YourPictures">Upload Your Pictures:</label>
            <sep:UploadFiles ID="YourPictures" runat="server" ModuleID="40" FileType="Images" Mode="MultipleFiles" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>