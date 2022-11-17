<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="forums_display.aspx.cs" inherits="wwwroot.forums_display" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#ReplyContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <table width="95%" align="center" class="Table">
        <tr class="TableHeader">
            <td width="165">
                <b>Author</b>
            </td>
            <td>
                <button type="button" id="replyButton" class="btn btn-success" style="float: right; margin-right: 5px;" onclick="document.location.href='<%= this.GetInstallFolder() %>forums_post.aspx?TopicID=<%= sTopicID %>&amp;CatID=<%= sCatID %>';"><i class="fa fa-mail-reply"></i> Post Reply</button>
                <button type="button" id="newTopicButton" class="btn btn-primary" style="float: right; margin-right: 5px;" onclick="document.location.href='<%= this.GetInstallFolder() %>forums_post.aspx?CatID=<%= sCatID %>';"><i class="fa fa-plus"></i> New Topic</button>
                <button type="button" id="backButton" class="btn btn-secondary" style="float: right; margin-right: 5px;" onclick="window.history.back();"><i class="fa fa-arrow-left"></i> Back</button>
                <b>Thread</b>
            </td>
        </tr>
        <tr class="TableBody1">
            <td>
                <img src="<%= sProfileImage %>" alt="Thumbnail Image" title="Thumbnail Image" border="0" />
                <br />
                <b>
                    <span ID="Username" runat="server"></span>
                </b>
                <br />
                <br />
                Registered:
                <span ID="DateRegistered" runat="server"></span>
                <br />
                Posts:
                <span ID="TotalPosts" runat="server"></span>
                <br />
                Status:
                <span ID="OnlineStatus" runat="server"></span>
            </td>
            <td valign="top">
                <span ID="Message" runat="server"></span>
                <div id="AttachmentRow" runat="server">
                    <br />
                    <br />
                    <span ID="Attachment" runat="server"></span>
                </div>
            </td>
        </tr>
        <tr class="TableBody1">
            <td>
                <span ID="DatePosted" runat="server"></span>
            </td>
            <td align="right">
                <button type="button" id="userInfoButton2" class="btn btn-light" style="float: right; margin-right: 5px;" onclick="document.location.href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%= sUserID %>';"><i class="fa fa-info"></i> User Info</button>
                <button type="button" id="profileButton" class="btn btn-light" style="float: right; margin-right: 5px;" onclick="document.location.href='<%= this.GetInstallFolder() %>profile/<%= sProfileID %>/<%= sUserName %>/';"><i class="fa fa-file-text-o"></i> View Profile</button>
                <button type="button" id="newMessageButton" class="btn btn-light" style="float: right; margin-right: 5px;" onclick="document.location.href='<%= this.GetInstallFolder() %>messenger_compose.aspx?UserID=<%= sUserID %>';"><i class="fa fa-plus"></i> Compose Message</button>
            </td>
        </tr>
    </table>
    <br />

    <div class="GridViewStyle">
        <asp:GridView ID="ReplyContent" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
            CssClass="GridViewStyle">
            <Columns>
                <asp:TemplateField HeaderText="Reply Info" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <img src='<%#
                this.Eval("ProfileImage") %>'
                            alt="Thumbnail Image" title="Thumbnail Image" border="0" />
                        <br />
                        <b><%#
                this.Eval("UserName") %></b>
                        <br />
                        <br />
                        Registered: <%#
                this.Format_Date(this.Eval("DateRegistered").ToString()) %>
                        <br />
                        Posts: <%#
                this.Eval("TotalPosts") %>
                        <br />
                        Status: <%#
                this.Eval("OnlineStatus") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Message" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <div style="min-height: 200px; position: relative; width: 100%;">
                            <%#
                this.Eval("Message") %>
                            <div style="clear: both; height: 30px;"></div>
                            <div style="bottom: 0; position: absolute; right: 0;">
                                <button type="button" id="userInfoButton" class="btn btn-light" style="float: right; margin-right: 5px;" onclick='
<%#"document.location.href=\"" + this.GetInstallFolder() + "userinfo.aspx?UserID=" + this.Eval("UserID") + "\"" %>'><i class="fa fa-info"></i> User Info</button>
                                <button type="button" id="profileButton" class="btn btn-light" style="float: right; margin-right: 5px;" onclick='
<%#"document.location.href=\"" + this.GetInstallFolder() + "profile/" + this.Eval("ProfileID") + "/" + this.Eval("UserName") + "/\"" %>'><i class="fa fa-file-text-o"></i> View Profile</button>
                                <button type="button" id="newMessageButton" class="btn btn-light" style="float: right; margin-right: 5px;" onclick='
<%#"document.location.href=\"" + this.GetInstallFolder() + "messenger_compose.aspx?UserID=" + this.Eval("UserID") + "\"" %>'><i class="fa fa-plus"></i> Compose Message</button>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <button type="button" id="replyButton2" class="btn btn-success" style="float: right; margin-right: 5px;" onclick="document.location.href =
    '<%= this.GetInstallFolder() %>forums_post.aspx?TopicID=<%= sTopicID %>&amp;CatID=<%= sCatID %>';"><i class="fa fa-mail-reply"></i> Post Reply</button>
    <button type="button" id="newTopicButton2" class="btn btn-primary" style="float: right; margin-right: 5px;" onclick="document.location.href =
    '<%= this.GetInstallFolder() %>forums_post.aspx?CatID=<%= sCatID %>';"><i class="fa fa-plus"></i> New Topic</button>
    <button type="button" id="backButton2" class="btn btn-secondary" style="float: right; margin-right: 5px;" onclick="window.history.back();"><i class="fa fa-arrow-left"></i>  Back</button>

    <%
        var cSocialShare = new SepCityControls.SocialShare();
        this.Response.Write(cSocialShare.Render());
    %>
</asp:content>