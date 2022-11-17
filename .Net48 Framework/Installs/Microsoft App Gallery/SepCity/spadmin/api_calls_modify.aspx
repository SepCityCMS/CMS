<%@ Page title="Modify API Call" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" 
    CodeBehind="api_calls_modify.aspx.cs" Inherits="wwwroot.api_calls_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 988;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add API Call</h4>
                <input type="hidden" runat="server" ID="APIID" />
            
                <div class="mb-3">
                    <label ID="ApiMethodLabel" clientidmode="Static" runat="server" for="ApiMethod">Method:</label>
                    <asp:DropDownList ID="ApiMethod" runat="server" CssClass="form-control" AutoPostBack="True" EnableViewState="True" OnSelectedIndexChanged="ApiMethod_SelectedIndexChanged">
                        <asp:ListItem Text="GET" Value="GET" />
                        <asp:ListItem Text="POST" Value="POST" />
                        <asp:ListItem Text="PUT" Value="PUT" />
                        <asp:ListItem Text="DELETE" Value="DELETE" />
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label ID="ApiURLLabel" clientidmode="Static" runat="server" for="ApiURL">API URL:</label>
                    <input type="text" ID="ApiURL" runat="server"  class="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="ApiHeadersLabel" clientidmode="Static" runat="server" for="ApiHeaders">API Send Headers:</label>
                    <textarea ID="ApiHeaders" runat="server"  class="form-control"></textarea>
                </div>
                <div class="mb-3" id="ApiBodyRow" runat="server">
                    <label ID="ApiBodyLabel" clientidmode="Static" runat="server" for="ApiBody">API Send Body:</label>
                    <textarea ID="ApiBody" runat="server"  class="form-control"></textarea>
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>