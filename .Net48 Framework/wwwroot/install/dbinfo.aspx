<%@ page language="C#" viewstatemode="Enabled" masterpagefile="install.master"
    codebehind="dbinfo.aspx.cs" inherits="wwwroot.dbinfo" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="contentdatabase">

        <div class="ModFormDiv">

            <h4 id="ModifyLegend" runat="server">Database Information</h4>

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <div class="mb-3">
                Please enter your Microsoft SQL Server database information down below. The database must be created already and SepCity will create the tables / default data thats necessarily for SepCity to operate.
            </div>
            <div class="mb-3">
                <label id="DatabaseAddressLabel" clientidmode="Static" runat="server" for="DatabaseAddress">Database IP Address / Domain:</label>
                <input type="text" id="DatabaseAddress" runat="server" class="form-control" maxlength="100" text="127.0.0.1" />
                <asp:CustomValidator ID="DatabaseAddressRequired" runat="server" ControlToValidate="DatabaseAddress"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Database IP Address / Domain is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="DatabaseNameLabel" clientidmode="Static" runat="server" for="DatabaseName">Database Name:</label>
                <input type="text" id="DatabaseName" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="DatabaseNameRequired" runat="server" ControlToValidate="DatabaseName"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Database Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="DatabaseUserLabel" clientidmode="Static" runat="server" for="DatabaseUser">Database User Name:</label>
                <input type="text" id="DatabaseUser" runat="server" class="form-control" maxlength="100" />
                <asp:CustomValidator ID="DatabaseUserRequired" runat="server" ControlToValidate="DatabaseUser"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Database User Name is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="DatabasePassLabel" clientidmode="Static" runat="server" for="DatabasePass">Database Password:</label>
                <input type="password" id="DatabasePass" runat="server" class="form-control" maxlength="20" />
                <asp:CustomValidator ID="DatabasePassRequired" runat="server" ControlToValidate="DatabasePass"
                    ClientValidationFunction="customFormValidator" ErrorMessage="Database Password is required."
                    ValidateEmptyText="true" Display="Dynamic">
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <label id="DatabaseLanguageLabel" clientidmode="Static" runat="server" for="DatabaseLanguage">Your Web Site Language:</label>
                <select ID="DatabaseLanguage" runat="server" class="form-control">
                    <option value="EN-US">English (United States)</option>
                    <option value="NL-NL">Dutch (The Netherlands)</option>
                    <option value="FR-CA">French (Canada)</option>
                    <option value="FR-FR">French (France)</option>
                    <option value="MS-MY">Malaya (Malaysia)</option>
                    <option value="PT-BR">Portuguese (Brazil)</option>
                    <option value="RU-RU">Russian (Russia)</option>
                    <option value="ES-MX">Spanish (Mexico)</option>
                    <option value="ES-ES">Spanish (Spain)</option>
                </select>
            </div>
            <div class="mb-3">
                <label id="DatabaseCategoriesLabel" clientidmode="Static" runat="server" for="DatabaseCategories">Add Commonly Used Categories:</label>
                <select ID="DatabaseCategories" runat="server" class="form-control">
                    <option value="Yes">Yes</option>
                    <option value="No">No</option>
                </select>
            </div>

            <div class="mb-3" align="center">
                <asp:Button CssClass="btn btn-secondary" ID="BackButton" runat="server" Text="Back" OnClick="BackButton_Click" />
                <asp:Button CssClass="btn btn-primary" ID="ContinueButton" runat="server" Text="Continue" OnClick="ContinueButton_Click" />
            </div>
        </div>
    </div>
</asp:content>