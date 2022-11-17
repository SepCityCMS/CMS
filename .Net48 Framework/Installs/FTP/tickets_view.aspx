<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="tickets_view.aspx.cs" inherits="wwwroot.tickets_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span class="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ContactDiv" runat="server">

        <div class="mb-3">
            <div class="col-sm-10 col-sm-offset-1">
                <div class="form-group">
                    <label id="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
                    <span ID="Subject" runat="server"></span>
                </div>
                <div class="form-group">
                    <label id="DateReceivedLabel" clientidmode="Static" runat="server" for="DateReceived">Date Received:</label>
                    <span ID="DateReceived" runat="server"></span>
                </div>
                <div class="form-group">
                    <label id="StatusLabel" clientidmode="Static" runat="server" for="Status">Status:</label>
                    <span ID="Status" runat="server"></span>
                </div>
                <div class="form-group">
                    <label id="TicketNumberLabel" clientidmode="Static" runat="server" for="TicketNumber">Ticket Number:</label>
                    <span ID="TicketNumber" runat="server"></span>
                </div>
                <div class="form-group">
                    <label id="MessageBodyLabel" clientidmode="Static" runat="server" for="MessageBody">Message Body:</label>
                    <span ID="MessageBody" runat="server"></span>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
    </div>
</asp:content>