<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="auction_display.aspx.cs" inherits="wwwroot.auction_display" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="BidContent" runat="server" visible="false">
        <label id="ConfirmTitleLabel">Title: </label>
        <br />
        <span ID="ConfirmTitle" runat="server"></span>
        <br />
        <label id="ConfirmMaxBidLabel">Max Bid: </label>
        <br />
        <span ID="ConfirmMaxBid" runat="server"></span>
        <br />
        <label id="ConfirmCurrentBidLabel">Current Bid: </label>
        <br />
        <span ID="ConfirmCurrentBid" runat="server"></span>
        <br />
        <label id="ConfirmNoteLabel">Note: </label>
        <br />
        <span ID="ConfirmNote" runat="server"></span>
        <br />
        <br />

        <asp:Button ID="ConfirmButton" runat="server" Text="Confirm Bid" OnClick="ConfirmButton_Click" />
    </div>

    <div id="DisplayContent" runat="server">
        <h1>
            <span ID="AdTitle" runat="server"></span></h1>
        <b>Item #
            <span ID="AdID" runat="server"></span></b>

        <br />

        <div class="mb-3">
            <div class="col-sm-6">
                <b>Current Bid:</b>
                <span ID="UnitPrice" runat="server"></span>
                <br />
                <b>Contact Member:</b> <a href="<%= this.GetInstallFolder() %>messenger_compose.aspx?UserID=<%= sUserID %>">
                    <span ID="UserName" runat="server"></span></a>
            </div>
            <div class="col-sm-6">
                <b>Location:</b>
                <span ID="Location" runat="server"></span>
                <br />
                <b>Date Posted:</b>
                <span ID="DatePosted" runat="server"></span>
            </div>
        </div>

        <br />

        <div class="mb-3">
            <div class="col-sm-6">
                <sep:ContentImages ID="AuctionImages" runat="server" />
            </div>
            <div class="col-sm-6">
                <%
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 31;
                    cCustomFields.isReadOnly = true;
                    cCustomFields.UserID = sUserID;
                    cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("AdID");
                    this.Response.Write(cCustomFields.Render());
                %>
                This ad has been viewed
				<span ID="Visits" runat="server"></span>
                times
				<br />
                <span ID="RatingText" runat="server"></span>
            </div>
        </div>

        <br />

        <div class="mb-3">
            <div class="col-sm-12">
                <span ID="Description" runat="server"></span>
            </div>
        </div>

        <br />
        <br />

        <div class="mb-3">
            <div class="col-sm-12">
                <b>Maximum Bid:
                    <input type="text" id="UnitPrice2" runat="server" class="inline-block" style="width: 90" /></b>
                <br />
                <br />
                <asp:Button ID="BidButton" runat="server" Text="Place Bid" OnClick="BidButton_Click" />
            </div>
        </div>
        
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </div>
</asp:content>