<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="celebrities_profile_modify.aspx.cs" inherits="wwwroot.conference_profile_modify" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        $(document)
            .ready(function () {
                $('#Custom906800028608884').attr('name', 'Custom906800028608884');
                $('#Custom117193851274426').attr('name', 'Custom117193851274426');
                $('#Custom179115432969858').attr('name', 'Custom179115432969858');
                $('#Custom847562837400918').attr('name', 'Custom847562837400918');
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="PageContent" runat="server">
        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

            <h4 id="ModifyLegend" runat="server">Create Your Profile</h4>
            <input type="hidden" runat="server" id="ProfileID" />
            <div class="mb-3">
                <label id="Custom906800028608884Label" clientidmode="Static" runat="server" for="Custom906800028608884">Your Occupation: (Ex. Singer / Songwriter)</label>
                <input type="text" id="Custom906800028608884" runat="server" clientidmode="Static" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="AboutMeLabel" clientidmode="Static" runat="server" for="AboutMe">Enter your bio below:</label>
                <sep:WYSIWYGEditor runat="server" ID="AboutMe" Width="99%" Height="450" />
            </div>
            <div class="mb-3">
                <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Select an Image to Upload:</label>
                <sep:UploadFiles ID="Pictures" runat="server" Mode="SingleFile" FileType="Images" ModuleID="63" />
            </div>
            <div class="mb-3">
                <label id="Custom117193851274426Label" clientidmode="Static" runat="server" for="Custom117193851274426">Charities & Foundations Supported: (One per a Line)</label>
                <textarea id="Custom117193851274426" runat="server" class="form-control" clientidmode="Static"></textarea>
            </div>
            <div class="mb-3">
                <label id="Custom179115432969858Label" clientidmode="Static" runat="server" for="Custom179115432969858">Causes Supported: (One per a Line)</label>
                <textarea id="Custom179115432969858" runat="server" class="form-control" clientidmode="Static"></textarea>
            </div>
            <div class="mb-3">
                <label id="Custom847562837400918Label" clientidmode="Static" runat="server" for="Custom847562837400918">Cost per a Call</label>
                <select id="Custom847562837400918" runat="server" class="form-control inlineBlock" clientidmode="Static" width="150">
                </select>
            </div>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </div>
</asp:content>