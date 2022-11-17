<%@ page title="Downloads" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="downloads_modify.aspx.cs" inherits="wwwroot.downloads_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/plupload/2.1.8/plupload.full.min.js"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 10;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="FormContent" runat="server">

                <h4 id="ModifyLegend" runat="server">Upload File</h4>
                <input type="hidden" runat="server" ID="FileID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="10" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="10" ClientIDMode="Static" UploadControl="FileUpload" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div id="AudioRows" runat="server" clientidmode="Static" style="display: none">
                    <div class="mb-3">
                        <label ID="SongTitleLabel" clientidmode="Static" runat="server" for="SongTitle">Song Title:</label>
                        <input type="text" ID="SongTitle" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="AlbumLabel" runat="server" clientidmode="Static" AssociatedControlID="Album">Album Name:</asp:Label>
                        <input type="text" ID="Album" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                </div>
                <div id="DocumentRows" runat="server" clientidmode="Static" style="display: none">
                    <div class="mb-3">
                        <label ID="DocumentTitleLabel" clientidmode="Static" runat="server" for="DocumentTitle">Document Title:</label>
                        <input type="text" ID="DocumentTitle" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="DescriptionLabel" runat="server" clientidmode="Static" AssociatedControlID="Description">Description:</asp:Label>
                        <textarea ID="Description" runat="server"  class="form-control"></textarea>
                    </div>
                </div>
                <div id="VideoRows" runat="server" clientidmode="Static" style="display: none">
                    <div class="mb-3">
                        <label ID="VideoTitleLabel" clientidmode="Static" runat="server" for="VideoTitle">Video Title:</label>
                        <input type="text" ID="VideoTitle" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <label ID="VideoDescLabel" clientidmode="Static" runat="server" for="VideoDesc">Description:</label>
                        <textarea ID="VideoDesc" runat="server"  class="form-control"></textarea>
                    </div>
                </div>
                <div id="ImageRows" runat="server" clientidmode="Static" style="display: none">
                    <div class="mb-3">
                        <label ID="CaptionLabel" clientidmode="Static" runat="server" for="Caption">Caption:</label>
                        <input type="text" ID="Caption" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                </div>
                <div id="SoftwareRows" runat="server" clientidmode="Static" style="display: none">
                    <div class="mb-3">
                        <label ID="ApplicationNameLabel" clientidmode="Static" runat="server" for="ApplicationName">Application Name:</label>
                        <input type="text" ID="ApplicationName" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <label ID="VersionLabel" clientidmode="Static" runat="server" for="Version">Version:</label>
                        <input type="text" ID="Version" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <label ID="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
                        <input type="text" ID="Price" runat="server"  class="form-control" ClientIDMode="Static" />
                    </div>
                    <div class="mb-3">
                        <label ID="AppDescLabel" clientidmode="Static" runat="server" for="AppDesc">Description:</label>
                        <textarea ID="AppDesc" runat="server"  class="form-control"></textarea>
                    </div>
                </div>
                <div class="mb-3" id="UploadFileRow" runat="server">
                    <div id="PHFileUpload"></div>
                </div>
        </div>
            </div>
    </asp:Panel>
</asp:content>