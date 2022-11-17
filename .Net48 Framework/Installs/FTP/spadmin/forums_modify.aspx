<%@ page title="Forums" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="forums_modify.aspx.cs" inherits="wwwroot.forums_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 12;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Topic</h4>
                <input type="hidden" runat="server" ID="TopicID" />
                <input type="hidden" runat="server" ID="ReplyID" />

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server" for="Category">Select a Category in the box below:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="12" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
                    <input type="text" ID="Subject" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="SubjectRequired" runat="server" ControlToValidate="Subject"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="EmailReply" runat="server" Text="Receive an email when someone replies to this topic." />
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor Runat="server" ID="Message" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="AttachmentLabel" clientidmode="Static" runat="server" for="Attachment">Add an Attachment:</label>
                    <asp:FileUpload ID="Attachment" runat="server" />
                    <span ID="FileAttachment" runat="server" Visible="false"></span>
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>