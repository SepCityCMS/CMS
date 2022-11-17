<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="classifieds_feedback.aspx.cs" inherits="wwwroot.classifieds_feedback" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv">

        <h4 id="ModifyLegend" runat="server">Leave Feedback</h4>
        <input type="hidden" id="AdID" runat="server" />
        <input type="hidden" id="ToUserID" runat="server" />
        <input type="hidden" id="FromUserID" runat="server" />
        <input type="hidden" id="BORS" runat="server" />
        <div class="mb-3">
            <label id="RatingLabel" clientidmode="Static" runat="server" for="Rating">Rating:</label>
            <select id="Rating" runat="server" class="form-control">
                <option value="1 - Horrible">1</option>
                <option value="2 - Poor">2</option>
                <option value="3 - Fair">3</option>
                <option value="4 - Good">4</option>
                <option value="5 - Excellent">5" Selected="True</option>
            </select>
            <asp:CustomValidator ID="RatingRequired" runat="server" ControlToValidate="Rating"
                ClientValidationFunction="customFormValidator" ErrorMessage="Rating is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label id="CommentsLabel" clientidmode="Static" runat="server" for="Comments">Comments:</label>
            <input type="text" id="Comments" runat="server" class="form-control" maxlength="255" />
            <asp:CustomValidator ID="CommentsRequired" runat="server" ControlToValidate="Comments"
                ClientValidationFunction="customFormValidator" ErrorMessage="Comments is required."
                ValidateEmptyText="true" Display="Dynamic">
            </asp:CustomValidator>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <button class="btn btn-primary" id="SaveButton" runat="server" onserverclick="SaveButton_Click">Save</button>
        </div>
    </div>
</asp:content>