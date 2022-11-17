<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="businesses_display.aspx.cs" inherits="wwwroot.businesses_display" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <h1><span ID="BusinessName" runat="server"></span></h1>

        <sep:RatingStars ID="RatingStars" runat="server" ModuleID="20" />
        <br />
        <fb:like href="<%= sSiteUrl %><%= this.GetInstallFolder() %>facebook.aspx?BusinessID=<%= sBusinessID %>&ModuleID=20" send="true" layout="button_count" show_faces="true" width="450" action="like" font="" colorscheme="light"></fb:like>
        <br />

        <table width="95%">
            <tr class="TableBody1">
                <td>
                    <table width="99%">
                        <tr>
                            <td width="50%" valign="top">
                                <span id="ContactMember" runat="server"><b>Contact Member</b> <a href="<%= this.GetInstallFolder() %>messenger_compose.aspx?UserID=<%= sUserID %>">
                                    <span ID="UserName" runat="server" /></a></span><br></span>
                                <span id="ClaimBusiness" runat="server" visible="false"></span>
                                <span id="ShowAddressRow" runat="server">
                                    <b>Location</b>
                                    <span ID="StreetAddress" runat="server" /><br></span>
                                    <span ID="City" runat="server"></span>,
                                    <span ID="State" runat="server"></span>
                                    <span ID="PostalCode" runat="server" /><br></span>
                                </span>
                                <b>Phone Number</b>
                                <span ID="PhoneNumber" runat="server" /><br></span>
                                <b>Date Posted</b>
                                <span ID="DatePosted" runat="server" /><br></span>
                                <span class="businessHits"><b>Visits</b>
                                    <span ID="Visits" runat="server" /><br></span>
                                </span>
                                <b>Website</b>
                                <asp:HyperLink ID="WebSiteURL" runat="server" Target="_blank" />
                                <br />
                                <%
                                    var cCustomFields = new SepCityControls.CustomFields();
                                    cCustomFields.ModuleID = 20;
                                    cCustomFields.isReadOnly = true;
                                    cCustomFields.FieldUniqueID = sBusinessID;
                                    cCustomFields.UserID = sUserID;
                                    this.Response.Write(cCustomFields.Render());
                                %>
                            </td>
                            <td width="50%" align="right" valign="top" id="MapCol" runat="server">
                                <div id="idGoogleMapDiv"></div>
                                <span ID="GoogleMap" runat="server"></span>
                                <div id="idGoogleMapAddress" style="display: none;"><%= sAddress %></div>
                            </td>
                        </tr>
                    </table>

                    <span ID="FullDescription" runat="server"></span>
                    <%
                        var cSocialShare = new SepCityControls.SocialShare();
                        this.Response.Write(cSocialShare.Render());
                    %>

                    <br />
                    
                    <%
                        var cComments = new SepCityControls.Comments();
                        cComments.ModuleID = 20;
                        cComments.ID = "Comments";
                        cComments.ContentUniqueID = sBusinessID;
                        cComments.ReplyUserID = sUserID;
                        cComments.UserID = SepCommon.SepFunctions.Session_User_ID();
                        this.Response.Write(cComments.Render());
                    %>
                </td>
            </tr>
        </table>
    </div>
</asp:content>