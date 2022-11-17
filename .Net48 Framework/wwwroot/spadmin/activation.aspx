<%@ page title="Activation" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="activation.aspx.cs" inherits="wwwroot.activation" uiculture="de-DE" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ActivationFieldset" runat="server">
            <div class="col-lg-12">
                <h4 id="ModifyLegend" runat="server">Activation</h4>
                <div class="mb-3">
                    <label ID="LicenseKeyLabel" ClientIDMode="Static" runat="server">License Key:</label>
                    <div class="col-lg-12">
                        <span ID="LicenseKey" runat="server"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="SoftwareEditionLabel" ClientIDMode="Static" runat="server">Software Edition:</label>
                    <div class="col-lg-12">
                        <span ID="SoftwareEdition" runat="server"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="StatusLabel" ClientIDMode="Static" runat="server">Status:</label>
                    <div class="col-lg-12">
                        <span ID="Status" runat="server"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="PurchaseDateLabel" ClientIDMode="Static" runat="server">Date Purchased:</label>
                    <div class="col-lg-12">
                        <span ID="PurchaseDate" runat="server"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="ExpireDateLabel" ClientIDMode="Static" runat="server">Expiration:</label>
                    <div class="col-lg-12">
                        <span ID="ExpireDate" runat="server"></span>
                    </div>
                </div>
                <div class="mb-3">
                    <label ID="ModuleListLabel" ClientIDMode="Static" runat="server">Modules:</label>
                    <div class="col-lg-12">
                        <span ID="ModuleList" runat="server"></span>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:content>