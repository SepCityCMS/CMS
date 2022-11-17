<%@ page title="Approve Content" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="approve_content.aspx.cs" inherits="wwwroot.approve_content" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server"></h4>
                <input type="hidden" ID="ModuleUniqueID" runat="server" />
                <input type="hidden" ID="ModuleID" runat="server" />

                <asp:PlaceHolder ID="FormShow" runat="server"></asp:PlaceHolder>

            <hr class="mb-4" />
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="SaveButton" runat="server" Text="Approve Content" onclick="SaveButton_Click" /></div>
        </div>
    </asp:Panel>
</asp:content>