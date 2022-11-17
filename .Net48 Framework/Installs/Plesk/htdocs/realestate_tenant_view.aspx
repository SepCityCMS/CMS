<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_tenant_view.aspx.cs" inherits="wwwroot.realestate_tenant_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <h1 id="TenantName" runat="server"></h1>

        <span ID="Photo" runat="server"></span>

        <span ID="RatingStars" runat="server"></span>

        <div>
            <label>Date Moved In:</label>
            <span ID="MovedIn" runat="server"></span>
        </div>

        <div>
            <label>Date Moved Out:</label>
            <span ID="MovedOut" runat="server"></span>
        </div>

        <div>
            <label>Last 4 digits of ID / Social Security Number:</label>
            <span ID="TenantNumber" runat="server"></span>
        </div>

        <div>
            <label>Date of Birth:</label>
            <span ID="BirthDate" runat="server"></span>
        </div>

        <h2>Reviews</h2>

        <asp:Repeater ID="ReviewList" runat="server" OnItemDataBound="ReviewList_ItemDataBound">
            <ItemTemplate>
                <hr />
                <span ID="UserRating" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rating") + "|" + DataBinder.Eval(Container.DataItem, "DatePosted") %>'></span>
                <br />
                <h4>Complaints</h4>
                <%#
                this.Eval("Complaints").ToString() %>
                <br />
                <br />
                <h4>Compliments</h4>
                <%#
                this.Eval("Compliments").ToString() %>
            </ItemTemplate>
        </asp:Repeater>

        <h2>Attachments</h2>

        <asp:Repeater ID="AttachmentList" runat="server">
            <ItemTemplate>
                <b>FileName:</b>
                <a href="<%= this.GetInstallFolder(false) %>realestate_attachment_download.aspx?UploadID=<%#
                this.Eval("UploadID").ToString() %>" target="_blank"><%#
                this.Eval("FileName").ToString() %></a>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:content>