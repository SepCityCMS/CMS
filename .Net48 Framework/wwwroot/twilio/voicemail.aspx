<%@ page title="Twilio Control Panel" language="C#" masterpagefile="Site.Master"
    codebehind="voicemail.aspx.cs" inherits="wwwroot.twilio.voicemail" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Setup Voicemail</h4>
            <div class="mb-3">
                <label id="UploadFileLabel" clientidmode="Static" runat="server" for="UploadFile">Upload Voicemail MP3 File to Play:</label>
                <asp:FileUpload ID="UploadFile" runat="server" CssClass="FileEntry"></asp:FileUpload>
                <div id="FileInfoRow" runat="server">
                    <span ID="FileName" runat="server"></span>
                    : <a href="voicemail.aspx?DoAction=DeleteFile">Delete File</a>
                </div>
            </div>
            <div class="mb-3">
                -- Or --
            </div>
            <div class="mb-3">
                <label id="ReadTextLabel" clientidmode="Static" runat="server" for="ReadText">Read text to play:</label>
                <input type="text" id="ReadText" runat="server" class="form-control" maxlength="100" />
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>