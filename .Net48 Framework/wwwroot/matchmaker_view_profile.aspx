<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="matchmaker_view_profile.aspx.cs" inherits="wwwroot.matchmaker_view_profile" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <table class="Table" width="100%" align="center" id="UserTbl">
            <tr class="TableHeader">
                <td colspan="2">
                    <b>
                        <span ID="UserName" runat="server"></span>
                    </b>
                    <br />
                    <a href="/friends.aspx?DoAction=SaveFriend&amp;UserID=<%= sUserID %>" class="btn btn-light">Add to Friends</a>
                    <a href="/messenger_compose.aspx?UserID=<%= sUserID %>" class="btn btn-light">Send Message</a>
                    <a href="/userinfo.aspx?UserID=<%= sUserID %>" class="btn btn-light">User Information</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px 10px 0 0; width: 200px;">
                    <sep:ContentImages ID="ProfilePics" runat="server" />
                </td>
                <td valign="top" style="padding-top: 10px;">
                    <table cstyle="border-collapse: separate; border-spacing: 4px;">
                        <tr>
                            <td>
                                <b>Age</b>
                            </td>
                            <td>
                                <span ID="Age" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Gender</b>
                            </td>
                            <td>
                                <span ID="Gender" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Location</b>
                            </td>
                            <td>
                                <span ID="Location" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Member Since</b>
                            </td>
                            <td>
                                <span ID="MemberSince" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Last Login Date</b>
                            </td>
                            <td>
                                <span ID="LastLoginDate" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Last Login Time</b>
                            </td>
                            <td>
                                <span ID="LastLoginTime" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Views</b>
                            </td>
                            <td>
                                <span ID="Views" runat="server"></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <sep:ModuleReviews ID="UserReviews" runat="server" ModuleID="18" />
        <br />
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 18;
            cCustomFields.isReadOnly = true;
            cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("ProfileID");
            cCustomFields.UserID = sUserID;
            this.Response.Write(cCustomFields.Render());
        %>
        <br />

        <table class="Table" width="100%" align="center">
            <tr class="TableHeader">
                <td>
                    <b>About Me</b>
                </td>
            </tr>
            <tr>
                <td>
                    <span ID="AboutMe" runat="server"></span>
                </td>
            </tr>
        </table>

        <br />

        <table class="Table" width="100%" align="center">
            <tr class="TableHeader">
                <td>
                    <b>About My Match</b>
                </td>
            </tr>
            <tr>
                <td>
                    <span ID="AboutMyMatch" runat="server"></span>
                </td>
            </tr>
        </table>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>

        <br />
        
        <%
            if (sShowComments)
            {
                var cComments = new SepCityControls.Comments();
                cComments.ModuleID = 18;
                cComments.ID = "Comments";
                cComments.ContentUniqueID = SepCommon.SepCore.Request.Item("ProfileID");
                cComments.ReplyUserID = sUserID;
                cComments.UserID = SepCommon.SepFunctions.Session_User_ID();
                this.Response.Write(cComments.Render());
            }
        %>
    </div>
</asp:content>