<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="celebrities_schedule_view.aspx.cs" inherits="wwwroot.conference_schedule_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="PageContent" runat="server">
        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div id="DisplayContent" runat="server">
            <div class="TableTitle" style="width: 100%">
                <b>Viewing Item (<span ID="Subject" runat="server"></span>)</b>
            </div>
            <div class="mb-3">
                <label id="EventDateLabel" clientidmode="Static" runat="server" for="EventDate">Event Date:</label>
                <span>
                    <span ID="EventDate" runat="server"></span></span>
            </div>
            <div class="mb-3">
                <label id="BeginTimeLabel" clientidmode="Static" runat="server" for="BeginTime">Request Call Between Time:</label>
                <span>
                    <span ID="BeginTime" runat="server"></span></span> and <span>
                        <span ID="EndTime" runat="server"></span></span>
            </div>
            <div class="mb-3">
                This item has been viewed
                <span ID="Viewed" runat="server"></span>
                times.
            </div>
            <hr />
            <br />
            <span ID="EventContent" runat="server"></span>
        </div>
    </div>
</asp:content>