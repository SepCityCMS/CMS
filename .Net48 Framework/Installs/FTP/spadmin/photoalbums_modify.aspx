<%@ page title="Photo Albums" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="photoalbums_modify.aspx.cs" inherits="wwwroot.photoalbums_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 28;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Album</h4>
                <input type="hidden" runat="server" ID="AlbumID" />

                <div class="mb-3">
                    <label ID="AlbumNameLabel" clientidmode="Static" runat="server" for="AlbumName">Album Name:</label>
                    <input type="text" ID="AlbumName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="LetterNameRequired" runat="server" ControlToValidate="AlbumName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Album Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="DescriptionLabel" clientidmode="Static" runat="server" for="Description">Description:</label>
                    <textarea ID="Description" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="DescriptionRequired" runat="server" ControlToValidate="Description"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Description is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="ShareAlbum" runat="server" Text="Share this album with other users?" />
                </div>
                <div class="mb-3">
                    <label ID="AlbumPasswordLabel" clientidmode="Static" runat="server" for="AlbumPassword">Password (Users will be required to enter this password to view photos in this album):</label>
                    <input type="text" ID="AlbumPassword" runat="server"  class="form-control" MaxLength="25" />
                </div>
                <div class="mb-3">
                    <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Select Photos to Upload:</label>
                    <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="28" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>