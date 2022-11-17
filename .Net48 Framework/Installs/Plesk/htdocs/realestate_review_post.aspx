<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_review_post.aspx.cs" inherits="wwwroot.realestate_review_post" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Write Review</h4>
        <input type="hidden" runat="server" id="TenantID" />

        <div class="mb-3">
            <label id="PropertyIDLabel" clientidmode="Static" runat="server" for="PropertyID">Select Property:</label>
            <select runat="server" id="PropertyID" clientidmode="Static" class="form-control">
            </select>
            <asp:CustomValidator ID="PropertyIDRequired" runat="server" ControlToValidate="PropertyID"
                ClientValidationFunction="customFormValidator" ErrorMessage="Property is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <div class="mb-3">
            <label id="RatingLabel" clientidmode="Static" runat="server" for="Rating">Rating:</label>
            <sep:RatingStars ID="Rating" runat="server" ModuleID="32" />
        </div>
        <div class="mb-3">
            <label id="ComplaintsLabel" clientidmode="Static" runat="server" for="Complaints">Complaints:</label>
            <textarea id="Complaints" runat="server" class="form-control"></textarea>
        </div>

        <div class="mb-3">
            <label id="ComplimentsLabel" clientidmode="Static" runat="server" for="Compliments">Compliments:</label>
            <textarea id="Compliments" runat="server" class="form-control"></textarea>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>