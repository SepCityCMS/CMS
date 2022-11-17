<%@ page title="Approval Chains" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="approval_chains_modify.aspx.cs" inherits="wwwroot.approval_chains_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 983;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Approval Chain</h4>
                <input type="hidden" runat="server" ID="ChainID" />
                <div class="mb-3">
                    <label ID="ChainNameLabel" clientidmode="Static" runat="server" for="ChainName">Chain Name:</label>
                    <input type="text" ID="ChainName" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="ChainNameRequired" runat="server" ControlToValidate="ChainName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Chain Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ModulesLabel" clientidmode="Static" runat="server" for="Modules">Modules to apply this approval chain to:</label>
                    <sep:ModuleSelection ID="Modules" runat="server" ClientIDMode="Static" ModuleType="Approval" />
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalsLabel" clientidmode="Static" runat="server" for="Portals">Portals to apply this approval chain to:</label>
                    <sep:PortalSelection ID="Portals" runat="server" Text="|0|" ClientIDMode="Static" />
                </div>
                <br />
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th>Approval Names</th>
                        <th>Approval Emails</th>
                        <th>Order</th>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName1" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail1" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight1" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName2" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail2" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight2" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName3" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail3" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight3" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName4" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail4" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight4" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="4" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName5" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail5" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight5" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName6" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail6" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight6" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="6" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName7" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail7" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight7" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="7" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName8" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail8" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight8" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="8" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName9" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail9" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight9" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="9" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="ApprovalName10" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="ApprovalEmail10" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" />
                        </td>
                        <td>
                            <input type="text" id="Weight10" runat="server" class="form-control inline-block" style="width:160px;" ClientIDMode="Static" value="10" />
                        </td>
                    </tr>
                </table>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>