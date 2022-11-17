<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="vouchers_display.aspx.cs" inherits="wwwroot.vouchers_display" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="DisplayContent" runat="server">
        <h1>
            <span ID="BuyTitle" runat="server"></span>
        </h1>

        <table width="100%">
            <tr>
                <td valign="top">
                    <span ID="Logo" runat="server"></span>
                </td>
                <td width="100%" valign="top">
                    <span ID="BusinessName" runat="server"></span>
                    <br />
                    <br />
                    <span ID="ShortDescription" runat="server"></span>
                </td>
                <td valign="top">
                    <div style="background-image: url(/images/admin/save_money.gif); background-repeat: no-repeat; height: 57px; padding-top: 26px; text-align: center; width: 57px;">
                        <span ID="SavePercent" runat="server"></span>
                    </div>
                </td>
            </tr>
        </table>

        <span ID="LogoImageUpload" runat="server"></span>

        <span ID="LongDescription" runat="server"></span>
        <br />
        <br />

        <span ID="GoogleMap" runat="server"></span>

        <table width="100%" class="VoucherTable" cellspacing="0" cellpadding="0">
            <tr class="VoucherTblHdr">
                <td rowspan="2" valign="middle">
                    <input type="button" class="btn btn-primary" onclick="document.location.href = '<%= this.GetInstallFolder() %>vouchers_purchase.aspx?VoucherID=<%= sVoucherID %>'" value="Buy Now" />
                </td>
                <td>Sale Price</td>
                <td>Value</td>
                <td>Discount</td>
                <td>You Save</td>
                <td>Deals Bought</td>
                <td>QTY Remaining</td>
                <td>Time Left to Buy</td>
            </tr>
            <tr class="VoucherTblCols">
                <td>
                    <b>
                        <span ID="SalePrice" runat="server"></span>
                    </b>
                </td>
                <td>
                    <b>
                        <span ID="RegularPrice" runat="server"></span>
                    </b>
                </td>
                <td>
                    <span ID="SavePercent2" runat="server"></span>
                </td>
                <td>
                    <span ID="SavePrice" runat="server"></span>
                </td>
                <td>
                    <span ID="DealsBought" runat="server"></span>
                </td>
                <td>
                    <span ID="QuantityRemaining" runat="server"></span>
                </td>
                <td>
                    <span ID="TimeRemaining" runat="server"></span>
                </td>
            </tr>
        </table>

        <span ID="Disclaimer" runat="server"></span>
        <br />
        <br />
        <span ID="FinePrint" runat="server"></span>
        <br />
        <br />

        <table width="100%">
            <tr>
                <td width="50%">Don't wait, grab a hold of it before its too late!<br />
                    Be sure to <a href="<%= this.GetInstallFolder() %>refer.aspx?URL=<%= SepFunctions.UrlEncode(this.GetInstallFolder() + "vouchers_display.aspx?VoucherID=" + sVoucherID) %>&ModuleID=65" target="_blank">tell your friends!</a><br />
                    Click 'Buy Now' to get it now!</td>
                <td width="50%">Get registered now: <a href="<%= this.GetInstallFolder() %>signup.aspx" target="_blank">Click Here</a></td>
            </tr>
        </table>
        <br />

        <table width="100%">
            <tr>
                <td>Make sure you sign up for our 'Discounts' email newsletter so you are the first to be notified and you never miss a great deal! Just check the box!</td>
            </tr>
        </table>
        <br />

        <table width="100%">
            <tr>
                <td width="50%">Tweet this deal to your friends</td>
                <td width="50%">
                    <span ID="TwitterHTML" runat="server"></span>
                </td>
            </tr>
        </table>
        <br />

        <table width="100%">
            <tr>
                <td>Check out our <a href="<%= this.GetInstallFolder() %>vouchers.aspx" target="_blank">other deals!</a></td>
            </tr>
        </table>
        <br />

        <b>Local Businesses!</b><br />
        If you are a local business looking to conduct a Power Buy Campaign online with us here, then get your engine started <a href="<%= this.GetInstallFolder() %>contactus.aspx" target="_blank">Contact Us</a> today!

        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </div>
</asp:content>