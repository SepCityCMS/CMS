<%@ page title="Import Utility" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="import_utility.aspx.cs" inherits="wwwroot.import_utility" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="Step1" runat="server">

                <input type="hidden" ID="TempFileName" runat="server" />
                <h4 id="ModifyLegend" runat="server">Import Utility</h4>
                <div class="mb-3">
                    <label ID="ModuleIDLabel" clientidmode="Static" runat="server" for="ModuleID">Select a module to import data into:</label>
                    <select ID="ModuleID" runat="server" class="form-control">
                    </select>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="PortalID">Portal to map data to:</label>
                    <sep:PortalDropdown ID="PortalID" runat="server" text="0" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="SeperatorLabel" clientidmode="Static" runat="server" for="Seperator">Seperator:</label>
                    <select ID="Seperator" runat="server" class="form-control">
                        <option value="Comma">Comma</option>
                        <option value="Tab">Tab</option>
                        <option value="Semicolon">Semicolon</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="FileNameLabel" clientidmode="Static" runat="server" for="FileName">Select a file to import:</label>
                    <asp:FileUpload ID="FileName" runat="server" />
                    <asp:CustomValidator ID="FileNameRequired" runat="server" ControlToValidate="FileName"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="File to import is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:CheckBox ID="Headings" runat="server" Text="Import file includes column headings" />
                </div>

            <hr class="mb-4" />
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="NextButton" runat="server" Text="Next Step" onclick="NextButton_Click" /></div>
        </div>

        <div id="Step2" runat="server" visible="false">

                <h4 ID="MapFieldsLegend" runat="server">Map Fields</h4>
                <div id="MapFieldsDiv" runat="server"></div>

            <hr class="mb-4" />
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="ImpButton" runat="server" Text="Import Data" /></div>
        </div>

        <div id="Step3" runat="server" visible="false">
            <span ID="ImportFinished" runat="server"></span>
        </div>
    </asp:Panel>
</asp:content>