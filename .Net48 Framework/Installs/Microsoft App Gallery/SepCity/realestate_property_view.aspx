<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate_property_view.aspx.cs" inherits="wwwroot.realestate_property_view" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <h1 id="PropertyTitle" runat="server"></h1>
        <h2 id="PricingTitle" runat="server"></h2>

        <p align="center">
            <span ID="Location" runat="server"></span>
            <br />
            <span ID="MLSNumber" runat="server"></span>
        </p>

        <table width="100%">
            <tr>
                <td width="50%">
                    <span ID="PostedBy" runat="server"></span>
                    <span ID="NumRooms" runat="server"></span>
                    <span ID="NumBathrooms" runat="server"></span>
                    <span ID="SizeLot" runat="server"></span>
                    <span ID="NumBedrooms" runat="server"></span>
                    <span ID="DiningRoom" runat="server"></span>
                    <span ID="YearBuilt" runat="server"></span>
                </td>
                <td width="50%" id="MapCol" runat="server">
                    <div id="idGoogleMapDiv"></div>
                    <span ID="GoogleMap" runat="server"></span>
                    <div id="idGoogleMapAddress" style="display: none;"><%= sAddress %></div>
                </td>
            </tr>
        </table>

        <span ID="Description" runat="server"></span>

        <br />

        <sep:ContentImages ID="Pictures" runat="server" ModuleID="32" />

        <span ID="FeatureInterior" runat="server"></span>
        <span ID="FeatureExterior" runat="server"></span>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </div>
</asp:content>