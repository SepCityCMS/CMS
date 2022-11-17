<%@ page title="Change Domain" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="change_domain.aspx.cs" inherits="wwwroot.change_domain" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Change Domain</h4>
                <div class="mb-3">
                    <label ID="OldDomainLabel" clientidmode="Static" runat="server" for="OldDomain">Old Domain Name:</label>
                    <input type="text" ID="OldDomain" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="OldDomainRequired" runat="server" ControlToValidate="OldDomain"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Old Domain Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="NewDomainLabel" clientidmode="Static" runat="server" for="NewDomain">New Domain Name:</label>
                    <input type="text" ID="NewDomain" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="NewDomainRequired" runat="server" ControlToValidate="NewDomain"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="New Domain Name is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>

            <hr class="mb-4" />
                <div class="mb-3"><button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button></div>
        </div>
    </asp:Panel>
</asp:content>