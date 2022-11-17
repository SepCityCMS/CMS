<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="classifieds_modify.aspx.cs" inherits="wwwroot.classifieds_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Post a Classified Ad</h4>
        <input type="hidden" runat="server" id="AdID" />

        <sep:PostPrice ID="PostPricing" runat="server" ModuleID="44" />

        <div class="mb-3">
            <label id="CategoryLabel" clientidmode="Static" runat="server">Select a Category in the box below where to list your item:</label>
            <sep:CategoryDropdown ID="Category" runat="server" ModuleID="44" ClientIDMode="Static" />
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
            <label id="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
            <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="44" />
        </div>
        <div class="mb-3">
            <sep:WYSIWYGEditor runat="server" ID="FullDescription" Width="99%" Height="450" />
        </div>
        <div class="mb-3">
            <label id="QuantityLabel" clientidmode="Static" runat="server" for="Quantity">Quantity:</label>
            <input type="text" id="Quantity" runat="server" class="form-control" text="1" />
            <asp:CustomValidator ID="QuantityRequired" runat="server" ControlToValidate="Quantity"
                ClientValidationFunction="customFormValidator" ErrorMessage="Quantity is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
            <input type="text" id="Price" runat="server" class="form-control" />
            <asp:CustomValidator ID="PriceRequired" runat="server" ControlToValidate="Price"
                ClientValidationFunction="customFormValidator" ErrorMessage="Price is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 44;
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