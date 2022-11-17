<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="profiles_display.aspx.cs" inherits="wwwroot.profiles_display" %>

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
                    <button type="button" id="Add_to_Friends" class="btn btn-light" onclick="document.location
    .href = '<%= this.GetInstallFolder() %>friends.aspx?DoAction=SaveFriend&amp;UserID=<%= sUserID %>';" runat="server">
                        Add to Friends</button>
                    <button type="button" id="Send_Message" class="btn btn-light" onclick="document.location.href =
    '<%= this.GetInstallFolder() %>messenger_compose.aspx?UserID=<%= sUserID %>';" runat="server">
                        Send Message</button>
                    <button type="button" id="User_Information" class="btn btn-light" onclick="document.location.href = '<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%= sUserID %>';">User Information</button>
                </td>
            </tr>
            <tr>
                <td valign="top" style="padding: 10px 10px 0 0; width: 200px;">
                    <div ID="ProfilePics" runat="server"></div>
                </td>
                <td valign="top" style="padding-top: 10px;">
                    <table>
                        <tr id="AgeRow" runat="server">
                            <td style="padding-right: 10px;">
                                <b>Age</b>
                            </td>
                            <td>
                                <span ID="Age" runat="server"></span>
                            </td>
                        </tr>
                        <tr id="GenderRow" runat="server">
                            <td style="padding-right: 10px;">
                                <b>Gender</b>
                            </td>
                            <td>
                                <span ID="Gender" runat="server"></span>
                            </td>
                        </tr>
                        <tr id="LocationRow" runat="server">
                            <td style="padding-right: 10px;">
                                <b>Location</b>
                            </td>
                            <td>
                                <span ID="Location" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                <b>Member Since</b>
                            </td>
                            <td>
                                <span ID="MemberSince" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                <b>Last Login Date</b>
                            </td>
                            <td>
                                <span ID="LastLoginDate" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                <b>Last Login Time</b>
                            </td>
                            <td>
                                <span ID="LastLoginTime" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                <b>Views</b>
                            </td>
                            <td>
                                <span ID="Views" runat="server"></span>
                            </td>
                        </tr>
                    </table>
                    <table class="Table" width="100%" align="center" id="AboutMeTbl">
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
                </td>
            </tr>
        </table>
        <br />
        <sep:ModuleReviews ID="UserReviews" runat="server" ModuleID="63" />
        <br />
        <%
            var cCustomFields = new SepCityControls.CustomFields();
            cCustomFields.ModuleID = 63;
            cCustomFields.isReadOnly = true;
            cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("ProfileID");
            cCustomFields.UserID = sUserID;
            this.Response.Write(cCustomFields.Render());
        %>
        <br />
        <sep:AudioPlayer ID="AudioFiles" runat="server" ModuleID="63" Width="400" />
        <br />
        
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