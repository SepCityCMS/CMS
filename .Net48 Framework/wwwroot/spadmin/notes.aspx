<%@ page title="Notes" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="notes.aspx.cs" inherits="wwwroot.notes" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

            <div class="ModFormDiv" id="ModFormDiv" runat="server">
                <h4 id="ModifyLegend" runat="server">Add Note</h4>
                <input type="hidden" runat="server" ID="UserID" />
                <div class="mb-3">
                    <label ID="NotesLabel" clientidmode="Static" runat="server" for="Notes">Notes:</label>
                    <textarea ID="Notes" runat="server"  class="form-control"></textarea>
                    <asp:CustomValidator ID="NotesRequired" runat="server" ControlToValidate="Notes"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Notes is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
            <textarea ID="AllNotes" runat="server" class="form-control" readonly="readonly" style="width:98%;height:270px"></textarea>
            <div class="button-to-bottom">
	            <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
	            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
            </div>
        </div>
    </asp:Panel>
</asp:content>