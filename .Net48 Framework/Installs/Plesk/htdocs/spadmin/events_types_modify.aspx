<%@ page title="Event Types" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="events_types_modify.aspx.cs" inherits="wwwroot.events_types_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 46;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Event Type</h4>
                <input type="hidden" runat="server" ID="TypeID" />

                <div class="mb-3">
                    <label ID="TypeNameLabel" clientidmode="Static" runat="server" for="TypeName">Type Name:</label>
                    <input type="text" ID="TypeName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="TypeNameRequired" runat="server" ControlToValidate="TypeName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Type Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" id="ViewKeysRow" runat="server">
                    <label ID="ViewKeysLabel" ClientIDMode="Static" runat="server">Keys to view events in this event type:</label>
                    <sep:AccessKeySelection ID="ViewKeysSelection" runat="server" text="|1|,|2|,|3|,|4|" ClientIDMode="Static" />
                </div>
                <div class="mb-3" id="PostKeysRow" runat="server">
                    <label ID="PostKeysLabel" ClientIDMode="Static" runat="server">Keys to post to this event type:</label>
                    <sep:AccessKeySelection ID="PostKeysSelection" runat="server" text="|2|,|3|,|4|" ClientIDMode="Static" ExcludeEveryone="true" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>