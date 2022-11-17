<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="downloads_view_photo.aspx.cs" inherits="wwwroot.downloads_view_photo" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <table width="100%">
        <tr>
            <td valign="top">
                <span ID="ProfilePic" runat="server"></span>
            </td>
            <td width="50%" valign="top">Caption:
                <span ID="Caption" runat="server"></span>
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

    <div id="DisplayContent" runat="server">
        <asp:Image ID="PictureUrl" runat="server" />
    </div>
    <%
        var cSocialShare = new SepCityControls.SocialShare();
        this.Response.Write(cSocialShare.Render());
    %>
</asp:content>