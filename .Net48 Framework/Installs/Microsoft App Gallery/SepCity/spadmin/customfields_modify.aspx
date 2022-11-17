<%@ page title="Custom Fields" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="customfields_modify.aspx.cs" inherits="wwwroot.customfields_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/customfields_modify.js" type="text/javascript"></script>
    <style type="text/css">
        #OptionPanel {
            margin-left: 30px;
            max-height: 360px;
            overflow: auto;
            width: 310px;
        }

        #OptionAdd {
            margin-left: 30px;
        }
    </style>
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

                <h4 id="ModifyLegend" runat="server">Add Custom Field</h4>
                <input type="hidden" runat="server" ID="FieldID" />
                <input type="hidden" runat="server" ID="Weight" Value="0" />

                <div class="mb-3" ID="SectionRow" runat="server">
                    <label ID="SectionLabel" clientidmode="Static" runat="server" for="Section">Section:</label>
                    <select ID="Section" runat="server" ClientIDMode="Static" class="form-control" AutoPostBack="false">
                        <option value="">Select a Section</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="FieldNameLabel" clientidmode="Static" runat="server" for="FieldName">Field Name:</label>
                    <input type="text" ID="FieldName" runat="server" ClientIDMode="Static"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="FieldNameRequired" runat="server" ControlToValidate="FieldName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Field Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="AnswerTypeLabel" clientidmode="Static" runat="server" for="AnswerType">Answer Type:</label>
                    <select ID="AnswerType" runat="server" ClientIDMode="Static" class="form-control" AutoPostBack="false">
                        <option value="ShortAnswer">Short Answer</option>
                        <option value="LongAnswer">Long Answer</option>
                        <option value="DropdownM">Dropdown (Multiple Selection)</option>
                        <option value="DropdownS">Dropdown (Single Selection)</option>
                        <option value="Radio">Radio Buttons</option>
                        <option value="Checkbox">Checkboxes</option>
                        <option value="Date">Date</option>
                    </select>
                    <asp:CustomValidator ID="AnswerTypeRequired" runat="server" ControlToValidate="AnswerType"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Answer Type is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <asp:Panel ID="OptionPanel" runat="server" ClientIDMode="Static">
                </asp:Panel>
                <div class="mb-3" id="OptionAdd" style="display: none">
                    <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="AddOption" runat="server" Text="Add Option" UseSubmitBehavior="false" OnClientClick="return addCustomOption();" /></div>
                    <input type="hidden" ID="NumOptions" runat="server" Value="0" ClientIDMode="Static" />
                </div>
                <div class="mb-3">
                    <label ID="RequiredLabel" clientidmode="Static" runat="server" for="Required">Required:</label>
                    <select ID="Required" runat="server" ClientIDMode="Static" class="form-control">
                        <option value="True">Yes</option>
                        <option value="False">No</option>
                    </select>
                    <asp:CustomValidator ID="RequiredRequired" runat="server" ControlToValidate="Required"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Required is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="ModulesLabel" runat="server" ClientIDMode="Static" AssociatedControlID="Modules">Modules to show custom field in:</asp:Label>
                    <sep:ModuleSelection ID="Modules" runat="server" ClientIDMode="Static" ModuleType="CustomFields" />
                    <asp:CustomValidator ID="ModulesRequired" runat="server" ControlToValidate="Modules"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="You must select at lease one module to show this custom field in."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <asp:Label ID="PortalsLabel" runat="server" ClientIDMode="Static" AssociatedControlID="Portals">Portals to show custom field in:</asp:Label>
                    <sep:PortalSelection ID="Portals" runat="server" text="|-1|" ClientIDMode="Static" />
                </div>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>