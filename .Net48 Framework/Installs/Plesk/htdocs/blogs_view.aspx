<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="blogs_view.aspx.cs" inherits="wwwroot.blogs_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <h1 class="ListingTitle">
            <span ID="BlogName" runat="server"></span>
        </h1>

        <hr class="HrLine" />
        <br />

        <span ID="BlogContent" runat="server"></span>

        <br />
        <br />

        This blog has been viewed
        <span ID="Views" runat="server"></span>
        times.
        <br />
        This blog was posted on:
        <br />
        <span ID="DatePosted" runat="server"></span>
        <br />
        This blog was posted by:
        <br />
        <span ID="PostedBy" runat="server"></span>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
        <br />
        <br />

        <div class="row">
            <div class="col-sm-6">
                <%
                    if (sShowComments && SepCommon.SepCore.Request.Item("DoAction") != "Print")
                    {
                        var cComments = new SepCityControls.Comments();
                        cComments.ModuleID = 61;
                        cComments.ID = "Comments";
                        cComments.ContentUniqueID = SepCommon.SepCore.Request.Item("BlogID");
                        cComments.ReplyUserID = sUserId;
                        cComments.UserID = SepCommon.SepFunctions.Session_User_ID();
                        this.Response.Write(cComments.Render());
                    }
                %>
            </div>
            <div class="col-sm-6">
                <div id="OtherBlogsRow" runat="server">
                    <b>Blogs from This User</b><br />
                    <asp:DropDownList ID="UserBlogs" runat="server" CssClass="form-control" AutoPostBack="True" EnableViewState="True" OnSelectedIndexChanged="OtherBlogs_SelectedIndex">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
</asp:content>