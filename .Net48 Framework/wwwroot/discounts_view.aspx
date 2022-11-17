<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="discounts_view.aspx.cs" inherits="wwwroot.discounts_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function Print() {
            var row = $(".DiscountTable").clone(true);
            var printWin = window.open('', '', 'left=0", ",top=0,width=1000,height=600,status=0');
            $(".PrintCoupon", row).remove();
            var sStyle =
                "<link rel=\"stylesheet\" href=\"<%= this.GetInstallFolder() %>skins/BusinessCasual/styles/colors.css\" type=\"text/css\" />";
            var printBut = "<p align=\"center\"><input type=\"button\" value=\"Print\" onclick=\"print()\" /></p>";
            var dv = $("<div />");
            dv.append(sStyle);
            dv.append(row);
            dv.append(printBut);
            printWin.document.write(dv.html());
            printWin.document.close();
            printWin.focus();
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:Panel ID="DisplayContent" runat="server">
        <table class="DiscountTable" width="500" height="231" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <br />
                    <table align="center" cellpadding="0" cellspacing="0" width="95%">
                        <tr class="TableHeader">
                            <td colspan="2" align="center" height="20">
                                <table cellpadding="0" cellspacing="0" width="98%">
                                    <tr>
                                        <td width="33%">
                                            <b>Code:</b>
                                            <span ID="DiscountCode" runat="server"></span>
                                        </td>
                                        <td width="33%" align="center">
                                            <b>Expires:</b>
                                            <span ID="ExpireDate" runat="server"></span>
                                        </td>
                                        <td width="33%" align="right">
                                            <b>Quantity:</b>
                                            <span ID="Quantity" runat="server"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <span ID="CompanyName" runat="server"></span>
                    <table align="center" cellpadding="0" cellspacing="0" width="95%">
                        <tr>
                            <span ID="ProductImage" runat="server"></span>
                            <td align="center" width="100%">
                                <span class="DiscountPrice">
                                    <span ID="MarkOffPrice" runat="server"></span></span>
                                <br />
                                <span class="DiscountLabel">
                                    <span ID="LabelText" runat="server"></span></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="20">
                                <table width="100%">
                                    <tr>
                                        <span ID="Disclaimer" runat="server"></span>
                                        <td align="right">
                                            <span ID="BarCodeImage" runat="server"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <br />

        <p align="center">
            <input type="button" value="Print" onclick="Print()">
        </p>
    </asp:Panel>
</asp:content>