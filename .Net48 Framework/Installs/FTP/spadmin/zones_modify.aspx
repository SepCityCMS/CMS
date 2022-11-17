<%@ page title="Zones" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="zones_modify.aspx.cs" inherits="wwwroot.zones_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = SepCommon.SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Zone</h4>
                <input type="hidden" runat="server" ID="ZoneID" />
                <input type="hidden" runat="server" ID="ModuleID" />
                <div class="mb-3">
                    <label ID="ZoneNameLabel" clientidmode="Static" runat="server" for="ZoneName">Zone Name:</label>
                    <input type="text" ID="ZoneName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="ZoneNameRequired" runat="server" ControlToValidate="ZoneName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Zone Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>