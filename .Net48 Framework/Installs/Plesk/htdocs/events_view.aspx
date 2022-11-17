<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="events_view.aspx.cs" inherits="wwwroot.events_view" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <div class="mb-3">
            <b>
                <span ID="Subject" runat="server"></span></b>
        </div>
        <div class="mb-3">
            <label id="EventTypeLabel" clientidmode="Static" runat="server" for="EventType">Event Type:</label>
            <span>
                <span ID="EventType" runat="server"></span></span>
        </div>
        <div class="mb-3">
            <label id="BeginTimeLabel" clientidmode="Static" runat="server" for="BeginTime">Begin Time:</label>
            <span>
                <span ID="BeginTime" runat="server"></span></span>
        </div>
        <div class="mb-3">
            <label id="EndTimeLabel" clientidmode="Static" runat="server" for="EndTime">End Time:</label>
            <span>
                <span ID="EndTime" runat="server"></span></span>
        </div>
        <div class="mb-3">
            <label id="EventDateLabel" clientidmode="Static" runat="server" for="EventDate">Event Date:</label>
            <span>
                <span ID="EventDate" runat="server"></span></span>
        </div>
        <div class="mb-3">
            This event has been viewed
            <span ID="Viewed" runat="server"></span>
            times.
        </div>
        <hr />
        <br />
        <span ID="EventContent" runat="server"></span>
        <br />
        <span id="QuantitySpan" runat="server">Quantity:
            <input type="text" id="Quantity" runat="server" class="form-control" width="80px" text="1" /></span>
        <asp:Button ID="BuyButton" runat="server" Text="Buy Now" OnClick="BuyButton_Click" />
        <span ID="DoorPrice" runat="server"></span>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
        <br />
        <br />
        <sep:ContentImages ID="EventPictures" runat="server" />
    </div>
</asp:content>