<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="matchmaker.aspx.cs" inherits="wwwroot.matchmaker1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <LayoutTemplate>
            <table border="0" cellpadding="1">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td width="50%" nowrap="nowrap">
                    <div class="ArticleList" style="float: left; overflow: hidden; white-space: nowrap; width: 100%;">
                        <div style="float: left; margin-right: 10px;">
                            <img src="<%#
                this.Eval("DefaultPicture") %>"
                                border="0" alt="" />
                            <br />
                            Last Login: <%#
                this.Format_Date(this.Eval("LastLogin").ToString()) %>
                            <br />
                            Views: <%#
                this.Eval("Views") %>
                        </div>
                        <div style="display: inline-block; margin-right: 10px;">
                            <a href="<%= this.GetInstallFolder() %>match/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/"><%#
                this.Eval("Username") %></a>
                            <br />
                            Age: <%#
                this.Eval("Age") %>
                            <br />
                            Gender: <%#
                this.Eval("Sex") %>
                            <br />
                            Location: <%#
                this.Eval("Location") %>
                            <br />
                            Distance: <%#
                this.Eval("Distance") %>
                            <br />
                        </div>
                    </div>
                </td>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <td width="50%" nowrap="nowrap">
                <div class="ArticleList">
                    <div style="float: left; margin-right: 10px;">
                        <img src="<%#
                this.Eval("DefaultPicture") %>"
                            border="0" alt="" />
                        <br />
                        Last Login: <%#
                this.Format_Date(this.Eval("LastLogin").ToString()) %>
                        <br />
                        Views: <%#
                this.Eval("Views") %>
                    </div>
                    <div style="float: left;">
                        <a href="<%= this.GetInstallFolder() %>match/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/"><%#
                this.Eval("Username") %></a>
                        <br />
                        Age: <%#
                this.Eval("Age") %>
                        <br />
                        Gender: <%#
                this.Eval("Sex") %>
                        <br />
                        Location: <%#
                this.Eval("Location") %>
                        <br />
                        Distance: <%#
                this.Eval("Distance") %>
                        <br />
                    </div>
                </div>
            </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
    <br />
    <div class="PagingPanel">
        <asp:DataPager ID="PagerTemplate" runat="server" PagedControlID="ListContent" PageSize="20" OnPreRender="PagerTemplate_PreRender">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                <asp:TemplatePagerField>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField ButtonType="Button" LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="True" ShowPreviousPageButton="False" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:content>