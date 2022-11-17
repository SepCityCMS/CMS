<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="downloads_view_video.aspx.cs" inherits="wwwroot.downloads_view_video" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="VideoContent" runat="server">
        <table width="100%">
            <tr>
                <td valign="top">
                    <span ID="ProfilePic" runat="server"></span>
                </td>
                <td width="50%" valign="top">Song Title:
                    <span ID="SongTitle" runat="server"></span>
                    <br />
                    Album Name:
                    <span ID="AlbumName" runat="server"></span>
                </td>
                <td width="50%" align="right" valign="top">Uploaded By:
                    <span ID="UploadedBy" runat="server"></span>
                    <br />
                    Viewed
                    <span ID="Visits" runat="server"></span>
                    <br />
                    <sep:RatingStars ID="RatingStars" runat="server" ModuleID="10" />
                </td>
            </tr>
        </table>
    </div>

    <div id="DisplayContent" runat="server">
        <sep:VideoPlayer ID="VideoPlayer" runat="server" Width="480" Height="360" />
        <p align="center" id="ShareDiv" runat="server">
            <b>Code to share this video:</b>
            <input type="text" id="ShareHTML" runat="server" class="form-control" width="480" />
        </p>
    </div>
    <%
        var cSocialShare = new SepCityControls.SocialShare();
        this.Response.Write(cSocialShare.Render());
    %>
</asp:content>