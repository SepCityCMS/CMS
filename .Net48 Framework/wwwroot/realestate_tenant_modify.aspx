<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_tenant_modify.aspx.cs" inherits="wwwroot.realestate_tenant_modify" %>
<%@ Import Namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%= SepFunctions.Date_Picker(this.BirthDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.MoveInDate.ClientID, "false", "true", "") %>;
            <%= SepFunctions.Date_Picker(this.MoveOutDate.ClientID, "false", "true", "") %>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Tenant</h4>
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
            <label id="FullNameLabel" clientidmode="Static" runat="server" for="FullName">Full Name:</label>
            <input type="text" id="FullName" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="FullNameRequired" runat="server" ControlToValidate="FullName"
                ClientValidationFunction="customFormValidator" ErrorMessage="Full Name is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <div class="mb-3">
            <label id="TenantNumberLabel" clientidmode="Static" runat="server" for="TenantNumber">Last 4 digits of ID / Social Security Number:</label>
            <input type="text" id="TenantNumber" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="TenantNumberRequired" runat="server" ControlToValidate="TenantNumber"
                ClientValidationFunction="customFormValidator" ErrorMessage="Last 4 of Social Security Number is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <div class="mb-3">
            <label id="BirthDateLabel" clientidmode="Static" runat="server" for="BirthDate">Date Of Birth:</label>
            <input type="text" id="BirthDate" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="BirthDateRequired" runat="server" ControlToValidate="BirthDate"
                ClientValidationFunction="customFormValidator" ErrorMessage="Date Of Birth is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <div class="mb-3">
            <label id="MoveInDateLabel" clientidmode="Static" runat="server" for="MoveInDate">Date Moved In:</label>
            <input type="text" id="MoveInDate" runat="server" class="form-control" maxlength="100" />
            <asp:CustomValidator ID="MoveInDateRequired" runat="server" ControlToValidate="MoveInDate"
                ClientValidationFunction="customFormValidator" ErrorMessage="Date Moved In is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <div class="mb-3">
            <label id="MoveOutDateLabel" clientidmode="Static" runat="server" for="MoveOutDate">Date Moved Out:</label>
            <input type="text" id="MoveOutDate" runat="server" class="form-control" maxlength="100" />
        </div>

        <div class="mb-3">
            <label id="PhotoLabel" clientidmode="Static" runat="server" for="Photo">Photo:</label>
            <sep:UploadFiles ID="Photo" runat="server" Mode="SingleFile" FileType="Images" ModuleID="32" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>