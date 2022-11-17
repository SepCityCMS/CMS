<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="profiles.aspx.cs" inherits="wwwroot.profiles" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
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
                        <div style="float: left;">
                            <img src="<%#
                this.Eval("DefaultPicture") %>" border="0" alt="" />
                            <br />
                            Last Login: <%#
                this.Format_Date(this.Eval("LastLogin").ToString()) %>
                            <br />
                            Views: <%#
                this.Eval("Views") %>
                        </div>
                        <div style="display: inline-block;">
                            <a href="<%= this.GetInstallFolder() %>@<%#
                this.Format_ISAPI(this.Eval("Username")) %>/"><%#
                this.Eval("Username") %></a>
                            <br />
                            <%# Convert.ToString(Convert.ToInt32(this.Eval("Age")) > 17 && this.ShowAge() ? "Age: " + this.Eval("Age") + "<br />" : "") %>
                            <%# Convert.ToString(this.ShowGender() ? "Gender: " + this.Eval("Sex") + "<br />" : "") %>
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
                    <div style="float: left;">
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
                        <a href="<%= this.GetInstallFolder() %>@<%#
                this.Format_ISAPI(this.Eval("Username")) %>/"><%#
                this.Eval("Username") %></a>
                        <br />
                        <%#
                Convert.ToString(Convert.ToInt32(this.Eval("Age")) > 17 && this.ShowAge() ? "Age: " + this.Eval("Age") + "<br />" : "") %>
                        <%#
                Convert.ToString(this.ShowGender() ? "Gender: " + this.Eval("Sex") + "<br />" : "") %>
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