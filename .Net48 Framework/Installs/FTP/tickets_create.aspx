<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="tickets_create.aspx.cs" inherits="wwwroot.tickets_create" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span class="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ContactDiv" runat="server">

        <div class="mb-3">
            <div class="col-sm-10 col-sm-offset-1">
                <div class="form-group" id="DepartmentRow" runat="server">
                    <label id="DepartmentLabel" clientidmode="Static" runat="server" for="Department">Department:</label>
                    <select id="Department" runat="server" class="form-control">
                        <option value="">Select a Department</option>
                    </select>
                    <asp:CustomValidator ID="DepartmentRequired" runat="server" ControlToValidate="Department"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Department is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="form-group">
                    <label id="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
                    <input type="text" id="Subject" runat="server" class="form-control" />
                    <asp:CustomValidator ID="SubjectRequired" runat="server" ControlToValidate="Subject"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="form-group">
                    <label id="MessageBodyLabel" clientidmode="Static" runat="server" for="MessageBody">Message Body:</label>
                    <textarea id="MessageBody" runat="server" class="form-control"></textarea>
                    <asp:CustomValidator ID="MessageBodyRequired" runat="server" ControlToValidate="MessageBody"
                        ClientValidationFunction="customFormValidator" ErrorMessage="Message Body is required."
                        ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div class="form-group" style="text-align: center;">
                <asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Submit Ticket" OnClick="SaveButton_Click" />
            </div>
        </div>
    </div>
</asp:content>