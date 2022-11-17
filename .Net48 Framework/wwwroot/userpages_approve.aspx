<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="userpages_approve.aspx.cs" inherits="wwwroot.userpages_approve" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Approve User</h4>
        <input type="hidden" runat="server" id="UserID" />
        <input type="hidden" runat="server" id="SiteUserName" />

        <div class="mb-3">
            <label id="ApproveLabel" clientidmode="Static" runat="server" for="Approve">Are you sure you want to approve this user?:</label>
            <select id="Approve" runat="server" class="form-control">
                <option value="Yes">Yes</option>
                <option value="No">No</option>
            </select>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Approve" />
        </div>
    </div>
</asp:content>