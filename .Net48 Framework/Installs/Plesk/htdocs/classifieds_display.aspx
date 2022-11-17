<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="classifieds_display.aspx.cs" inherits="wwwroot.classifieds_display" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <table width="95%" class="Table">
            <tr>
                <td class="TableTitle" colspan="1">
                    <b>
                        <span ID="AdTitle" runat="server"></span>
                    </b>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center">
                    <b>Item #
                        <span ID="AdID" runat="server"></span></b>
                </td>
            </tr>
        </table>
        <br />

        <table width="95%">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td width="120" valign="top">
                                <b>Unit Price</b>
                            </td>
                            <td>
                                <span ID="UnitPrice" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td width="120" valign="top">
                                <b>Quantity</b>
                            </td>
                            <td>
                                <span ID="Quantity" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td width="120" valign="top">
                                <b>Contact Member</b>
                            </td>
                            <td>
                                <a href="<%= this.GetInstallFolder() %>messenger_compose.aspx?UserID=<%= sUserID %>">
                                    <span ID="UserName" runat="server"></span>
                                </a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td width="100" valign="top">
                                <b>Location</b>
                            </td>
                            <td>
                                <span ID="Location" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td width="100" valign="top">
                                <b>Date Posted</b>
                            </td>
                            <td>
                                <span ID="DatePosted" runat="server"></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />

        <table width="100%">
            <tr>
                <td valign="top" nowrap="nowrap" width="50%">
                    <sep:ContentImages ID="ClassifiedImages" runat="server" />
                </td>
                <td valign="top" nowrap="nowrap" width="50%">
                    <%
                        var cCustomFields = new SepCityControls.CustomFields();
                        cCustomFields.ModuleID = 44;
                        cCustomFields.isReadOnly = true;
                        cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("AdID");
                        cCustomFields.UserID = sUserID;
                        this.Response.Write(cCustomFields.Render());
                    %>
                    This ad has been viewed
                    <span ID="Visits" runat="server"></span>
                    times
                    <br />
                    <span ID="RatingText" runat="server"></span>
                </td>
            </tr>
        </table>
        <br />

        <table class="Table" width="95%">
            <tr class="TableHeader">
                <td align="center">
                    <b>Description</b>
                </td>
            </tr>
        </table>
        <br />

        <table class="Table" width="95%">
            <tr>
                <td valign="top">
                    <span ID="Description" runat="server"></span>
                </td>
            </tr>
        </table>
        <br />
        <br />

        <table class="Table" width="95%">
            <tr>
                <td valign="top" align="right">
                    <b>
                        <span ID="UnitPrice2" runat="server"></span>
                    </b>
                    <br />
                    <br />
                    <asp:Button ID="BuyButton" cssclass="btn btn-primary" runat="server" Text="Buy Now" OnClick="BuyButton_Click" />
                </td>
            </tr>
        </table>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </div>
</asp:content>