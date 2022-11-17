<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="downloads_upload.aspx.cs" inherits="wwwroot.downloads_upload" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/plupload/2.1.8/plupload.full.min.js"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="FormContent" runat="server">

        <h4 id="ModifyLegend" runat="server">Upload File</h4>
        <input type="hidden" runat="server" id="FileID" clientidmode="Static" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server" for="Category">Select a Category in the box below:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="10" ClientIDMode="Static" uploadcontrol="FileUpload" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div id="AudioRows" runat="server" clientidmode="Static" style="display: none">
            <div class="mb-3">
                <label id="ArtistLabel" clientidmode="Static" runat="server" for="Artist">Artist Name:</label>
                <input type="text" id="Artist" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <asp:Label ID="AlbumLabel" runat="server" ClientIDMode="Static" AssociatedControlID="Album">Album Name:</asp:Label>
                <input type="text" id="Album" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <label id="SongTitleLabel" clientidmode="Static" runat="server" for="SongTitle">Song Title:</label>
                <input type="text" id="SongTitle" runat="server" class="form-control" clientidmode="Static" />
            </div>
        </div>
        <div id="DocumentRows" runat="server" clientidmode="Static" style="display: none">
            <div class="mb-3">
                <label id="DocumentTitleLabel" clientidmode="Static" runat="server" for="DocumentTitle">Document Title:</label>
                <input type="text" id="DocumentTitle" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <asp:Label ID="DescriptionLabel" runat="server" ClientIDMode="Static" AssociatedControlID="Description">Description:</asp:Label>
                <textarea id="Description" runat="server" class="form-control"></textarea>
            </div>
        </div>
        <div id="VideoRows" runat="server" clientidmode="Static" style="display: none">
            <div class="mb-3">
                <label id="VideoTitleLabel" clientidmode="Static" runat="server" for="VideoTitle">Video Title:</label>
                <input type="text" id="VideoTitle" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <label id="VideoDescLabel" clientidmode="Static" runat="server" for="VideoDesc">Description:</label>
                <textarea id="VideoDesc" runat="server" class="form-control"></textarea>
            </div>
        </div>
        <div id="ImageRows" runat="server" clientidmode="Static" style="display: none">
            <div class="mb-3">
                <label id="CaptionLabel" clientidmode="Static" runat="server" for="Caption">Caption:</label>
                <input type="text" id="Caption" runat="server" class="form-control" clientidmode="Static" />
            </div>
        </div>
        <div id="SoftwareRows" runat="server" clientidmode="Static" style="display: none">
            <div class="mb-3">
                <label id="ApplicationNameLabel" clientidmode="Static" runat="server" for="ApplicationName">Application Name:</label>
                <input type="text" id="ApplicationName" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <label id="VersionLabel" clientidmode="Static" runat="server" for="Version">Version:</label>
                <input type="text" id="Version" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <label id="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
                <input type="text" id="Price" runat="server" class="form-control" clientidmode="Static" />
            </div>
            <div class="mb-3">
                <label id="AppDescLabel" clientidmode="Static" runat="server" for="AppDesc">Description:</label>
                <textarea id="AppDesc" runat="server" class="form-control"></textarea>
            </div>
        </div>
        <div class="mb-3">
            <div id="PHFileUpload"></div>
        </div>
    </div>
</asp:content>