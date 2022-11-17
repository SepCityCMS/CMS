<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="forums_post.aspx.cs" inherits="wwwroot.forums_post" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Post a Topic</h4>
        <input type="hidden" runat="server" id="TopicID" />
        <input type="hidden" runat="server" id="CatID" />
        <input type="hidden" runat="server" id="ReplyID" />

        <div class="mb-3">
            <label id="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
            <input type="text" id="Subject" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="SubjectRequired" runat="server" ControlToValidate="Subject"
                ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <asp:CheckBox ID="EmailReply" runat="server" Checked="true" />
            Receive an email when someone replies to this topic.
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="Message" Width="99%" Height="450" />
        </div>
        <div class="mb-3">
            <label id="AttachmentLabel" clientidmode="Static" runat="server" for="Attachment">Add an Attachment:</label>
            <asp:FileUpload ID="Attachment" runat="server" />
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button type="button" class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>