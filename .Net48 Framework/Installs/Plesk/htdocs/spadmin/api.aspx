<%@ page title="OAuth / API" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="api.aspx.cs" inherits="wwwroot.api" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">OAuth / API Configuration</h4>
                <div class="mb-3">
                    <label ID="OAuthClientIDLabel" clientidmode="Static" runat="server" for="OAuthClientID">OAuth Client ID:</label>
                    <input type="text" ID="OAuthClientID" runat="server"  class="form-control" ClientIDMode="Static" ReadOnly="true" />
                    <div class="mb-3"><asp:Button CssClass="btn btn-secondary" ID="GenClientID" runat="server" Text="Generate Key" onclick="GenClientID_Click" /></div>
                </div>
                <div class="mb-3">
                    <label ID="ClientSecretLabel" clientidmode="Static" runat="server" for="ClientSecret">OAuth Client Secret:</label>
                    <input type="text" ID="ClientSecret" runat="server"  class="form-control" ClientIDMode="Static" ReadOnly="true" />
                </div>
                <div class="mb-3">
                    <label ID="CreationDateLabel" clientidmode="Static" runat="server" for="CreationDate">OAuth Creation Date:</label>
                    <input type="text" ID="CreationDate" runat="server"  class="form-control" ClientIDMode="Static" ReadOnly="true" />
                </div>
                <div class="mb-3">
                    <label ID="AuthorizedDomainsLabel" clientidmode="Static" runat="server" for="AuthorizedDomains">Domains Authorized to Access the OAuth API (One per a line):</label>
                    <textarea ID="AuthorizedDomains" runat="server"  class="form-control"></textarea>
                </div>

            <span id="oAuthURL" runat="server"></span>
            <div class="button-to-bottom">
                <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>