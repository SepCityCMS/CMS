<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="photos_album_modify.aspx.cs" inherits="wwwroot.photos_album_modify" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Album</h4>
        <input type="hidden" runat="server" id="AlbumID" />

        <div class="mb-3">
            <label id="AlbumNameLabel" clientidmode="Static" runat="server" for="AlbumName">Album Name:</label>
            <input type="text" id="AlbumName" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="LetterNameRequired" runat="server" ControlToValidate="AlbumName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Album Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
            <textarea id="Description" runat="server" class="form-control"></textarea>
            <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3" id="AlbumPasswordRow" runat="server">
            <label id="AlbumPasswordLabel" clientidmode="Static" runat="server" for="AlbumPassword">Password (Users will be required to enter this password to view photos in this album):</label>
            <input type="text" id="AlbumPassword" runat="server" class="form-control" maxlength="25" />
        </div>
        <div class="mb-3" id="ShareRow" runat="server">
            <asp:CheckBox ID="ShareAlbum" runat="server" Text="Share this album with other users?" />
        </div>
        <div class="mb-3">
            <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Select Photos to Upload:</label>
            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="28" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>