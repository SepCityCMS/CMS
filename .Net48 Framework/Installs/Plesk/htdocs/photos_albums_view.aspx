<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="photos_albums_view.aspx.cs" inherits="wwwroot.photos_albums_view" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <input type="hidden" id="AlbumID" runat="server" />

    <div id="ShowPassword" visible="false" runat="server">
        <p>
            You must enter a password to view the photos in this album.
	        <div class="col-lg-12">
                <div class="input-group">
                    <input type="text" id="Password" runat="server" ckass="form-control" />
                    <span class="input-group-btn">
                        <asp:Button ID="PassButton" class="btn btn-default" runat="server" Text="Submit" OnClick="PassButton_Click" />
                    </span>
                </div>
            </div>
        </p>
    </div>

    <div id="DisplayContent" runat="server">
        <h6 id="AlbumName" runat="server"></h6>
        <br />
        <sep:ContentImages ID="AlbumImages" runat="server" />
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </div>
</asp:content>