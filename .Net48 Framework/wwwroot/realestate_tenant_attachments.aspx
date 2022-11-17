<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_tenant_attachments.aspx.cs" inherits="wwwroot.realestate_tenant_attachments" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv">
        <input type="hidden" id="TenantID" runat="server" clientidmode="Static" />

        <h4 id="ModifyLegend" runat="server">Tenant Attachments</h4>
        <div class="mb-3">
            <label id="AttachmentsLabel" clientidmode="Static" runat="server" for="Attachments">Select Attachments:</label>
            <sep:UploadFiles ID="Attachments" runat="server" ModuleID="32" Mode="MultipleFiles" FileType="Any" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>