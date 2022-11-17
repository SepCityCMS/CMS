<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="auction_modify.aspx.cs" inherits="wwwroot.auction_modify1" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Add Auction</h4>
        <input type="hidden" runat="server" id="AdID" />

        <sep:PostPrice ID="PostPricing" runat="server" ModuleID="31" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server">Select a Category in the box below where to list your item:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="31" ClientIDMode="Static" />
            <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="AdTitleLabel" clientidmode="Static" runat="server" for="AdTitle">Title:</label>
            <input type="text" id="AdTitle" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="AdTitleRequired" runat="server" ControlToValidate="AdTitle"
                ClientValidationFunction="customFormValidator" ErrorMessage="Title is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="StartingBidLabel" clientidmode="Static" runat="server" for="StartingBid">Starting Bid:</label>
            <input type="text" id="StartingBid" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="StartingBidRequired" runat="server" ControlToValidate="StartingBid"
                ClientValidationFunction="customFormValidator" ErrorMessage="Starting Bid is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="IncreaseBidsLabel" clientidmode="Static" runat="server" for="IncreaseBids">Money To Increase Bids By:</label>
            <input type="text" id="IncreaseBids" runat="server" class="form-control" clientidmode="Static" />
            <asp:CustomValidator ID="IncreaseBidsRequired" runat="server" ControlToValidate="IncreaseBids"
                ClientValidationFunction="customFormValidator" ErrorMessage="Increase Bids is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="31" />
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="FullDescription" Width="99%" Height="450" />
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 31;
            cCustomFields.FieldUniqueID = this.AdID.Value;
            cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
            this.Response.Write(cCustomFields.Render());
        %>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>