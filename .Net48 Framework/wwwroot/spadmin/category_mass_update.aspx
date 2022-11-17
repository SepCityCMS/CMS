<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="category_mass_update.aspx.cs" inherits="wwwroot.category_mass_update" %>
<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Mass Update</h4>
            <input type="hidden" runat="server" id="CatIDs" clientidmode="Static" />

            <p runat="server" id="AccessKeysRow">
                <label id="AccessKeysLabel" clientidmode="Static" runat="server">Access keys required to access this category:</label>
                <sep:AccessKeySelection ID="AccessKeysSelection" runat="server" Text="|1|,|2|,|3|,|4|" ClientIDMode="Static" />
                <br />
                <asp:CheckBox runat="server" ID="AccessKeysHide" />
                Hide if users does not have a selected access key from above.
            </p>
            <p runat="server" id="WriteKeysRow">
                <label id="WriteKeysLabel" clientidmode="Static" runat="server">Access keys required to upload/write content this category:</label>
                <sep:AccessKeySelection ID="WriteKeysSelection" runat="server" Text="|2|,|3|,|4|" ClientIDMode="Static" />
                <br />
                <asp:CheckBox runat="server" ID="WriteKeysHide" />
                Hide if users does not have a selected access key from above.
            </p>
            <p runat="server" id="ManageKeysRow">
                <label id="ManageKeysLabel" clientidmode="Static" runat="server">Access keys required to manage content this category:</label>
                <sep:AccessKeySelection ID="ManageKeysSelection" runat="server" Text="|2|" ClientIDMode="Static" />
            </p>
            <p runat="server" id="PortalsRow">
                <label id="PortalSelectionLabel" clientidmode="Static" runat="server">Select the portals to show this category in:</label>
                <sep:PortalSelection ID="PortalSelection" runat="server" Text="|0|" />
                <br />
                <asp:CheckBox runat="server" ID="ShareContent" />
                Share the content this this category with the selected portals from above.
                    <br />
                <asp:CheckBox runat="server" ID="ExcludePortalSecurity" />
                Overwrite portal manager security.
            </p>
            <p runat="server" id="ModulesRow">
                <label id="ModulesLabel" clientidmode="Static" runat="server" for="Modules">Modules to Show Category In:</label>
                <sep:ModuleSelection ID="Modules" runat="server" ModuleType="Categories" ClientIDMode="Static" />
            </p>

            <hr class="mb-4" />
            <div class="mb-3">
                <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
            </div>
        </div>
    </asp:Panel>
</asp:content>