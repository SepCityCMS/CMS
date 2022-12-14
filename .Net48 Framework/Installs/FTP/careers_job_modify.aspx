<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers_job_modify.aspx.cs" inherits="wwwroot.careers_job_modify" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">
        <input type="hidden" id="JobId" runat="server" />

        <sep:PostPrice ID="PostPricing" runat="server" ModuleID="66" />

        <h4 id="ModifyLegend" runat="server">Add Position</h4>

        <asp:Panel ID="ScreenHTML" runat="server" EnableViewState="true"></asp:Panel>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>