<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="forums.aspx.cs" inherits="wwwroot.forums1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function postTopic() {
            document.location.href = '<%= this.GetInstallFolder() %>forums_post.aspx?CatID=<%= sCatID %>';
        }

        $(document).ready(function () {
            restyleGridView("#NewestContent");
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    
    <span ID="CategoryContent" runat="server"></span>
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 12;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>
    </span>

    <div class="GridViewStyle">
        <asp:Button ID="PostButton" cssclass="btn btn-success" runat="server" Text="New Topic" UseSubmitBehavior="false" OnClientClick="postTopic();return false;" />
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
            CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
            OnSorting="ManageGridView_Sorting" EnableViewState="True">
            <Columns>
                <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Subject">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>forum/<%#
                this.Eval("TopicID") %>/<%#
                this.Format_ISAPI(this.Eval("Subject")) %>/"><%#
                this.Eval("Subject") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Posted By" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Username">
                    <ItemTemplate>
                        <%#
                this.Eval("Username") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Posted" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="DatePosted">
                    <ItemTemplate>
                        <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>

    <asp:GridView ID="NewestContent" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="Latest Topic Postings">
        <Columns>
            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Subject">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>forum/<%#
                this.Eval("TopicID") %>/<%#
                this.Format_ISAPI(this.Eval("Subject")) %>/"><%#
                this.Eval("Subject") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Posted By" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Username") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Posted" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>