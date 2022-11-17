<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="messenger_display.aspx.cs" inherits="wwwroot.messenger_display" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <table width="100%" align="center" id="MsgDisplay" runat="server">
        <tr>
            <td valign="top" width="200">
                <table>
                    <tr>
                        <td>
                            <b>From</b>
                        </td>
                        <td>
                            <span ID="msgFrom" runat="server"></span>
                        </td>
                    </tr>
                    <tr class="TableBody1">
                        <td>
                            <b>To</b>
                        </td>
                        <td>
                            <span ID="msgTo" runat="server"></span>
                        </td>
                    </tr>
                    <tr class="TableBody1">
                        <td>
                            <b>Date</b>
                        </td>
                        <td>
                            <span ID="msgDate" runat="server"></span>
                        </td>
                    </tr>
                    <tr class="TableBody1">
                        <td>
                            <b>Subject</b>
                        </td>
                        <td>
                            <span ID="msgSubject" runat="server"></span>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" align="right">
                <span ID="msgFromUserImage" runat="server"></span>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <table border="0" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="right">
                            <asp:Button ID="BlockSenderButton" CssClass="btn btn-danger" runat="server" Text="Block Sender" OnClick="BlockSenderButton_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="ForwardButton" CssClass="btn btn-success" runat="server" Text="Forward" OnClick="ForwardButton_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="ReplyButton" CssClass="btn btn-primary" runat="server" Text="Reply" OnClick="ReplyButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span id="msgBody" runat="server"></span>
            </td>
        </tr>
    </table>
</asp:content>