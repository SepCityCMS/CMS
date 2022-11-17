<%@ page title="Affiliate" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="affiliate_image_upload.aspx.cs" inherits="wwwroot.affiliate_image_upload" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 39;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Upload Image</h4>
                <input type="hidden" runat="server" ID="ImageID" />
                <div class="mb-3">
                    <label ID="ImageNameLabel" clientidmode="Static" runat="server" for="ImageName">Image Name:</label>
                    <asp:FileUpload ID="ImageName" runat="server" CssClass="form-control" />
                    <asp:Image ID="AffiliateImage" runat="server" Visible="false" CssClass="imageEntry" />
                    <asp:CustomValidator ID="ImageNameRequired" runat="server" ControlToValidate="ImageName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="You must select an image."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalIDLabel" clientidmode="Static" runat="server" for="PortalID">Target Ad to Portal:</label>
                    <sep:PortalDropdown ID="PortalID" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>