<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="blogs.aspx.cs" inherits="wwwroot.blogs1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 61;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <hr />
    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <a href='<%= this.GetInstallFolder() %>blogs/<%#
                this.Eval("BlogID") %>/<%#
                this.Format_ISAPI(this.Eval("BlogName")) %>/'><%#
                this.Eval("BlogName") %></a>
            <br />
            Posted <%#
                this.Time_Long_Difference(this.Format_Date(this.Eval("DatePosted").ToString())) %> ago by <%#
                this.Eval("UserName") %>
            <br />
            <%#
                this.Eval("Description") %>
            <br />
            <hr />
        </ItemTemplate>
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