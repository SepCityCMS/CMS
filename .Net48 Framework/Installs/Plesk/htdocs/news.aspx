<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="news.aspx.cs" inherits="wwwroot.news1" %>

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
        cCategories.ModuleID = 23;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />
    
    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="article-bx">
                <div class="row">
                    <div class="col-md-12">
                        <div class="article-content-area">
                            <div class="article-top-content">
                                <h3><a href="<%= this.GetInstallFolder() %>news/<%#
                this.Eval("NewsID") %>/<%#
                this.Format_ISAPI(this.Eval("Topic")) %>/"><%#
                this.Eval("Topic") %></a></h3>
                                <p><%# SepCommon.SepCore.Strings.Left(Convert.ToString(this.Eval("Headline")), 500) + Convert.ToString(SepCommon.SepCore.Strings.Len(Convert.ToString(this.Eval("Headline"))) > 500 ? "..." : "") %></p>
                            </div>
                            <div class="article-btn-group">
                                <p><span>Date :</span> <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %></p>
                                <a href="<%= this.GetInstallFolder() %>news/<%#
                this.Eval("NewsID") %>/<%#
                this.Format_ISAPI(this.Eval("Topic")) %>/" class="btn btn-primary">Read More</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </itemtemplate>
    </asp:ListView>

    <br />

    <div class="PagingPanel">
        <asp:DataPager ID="PagerTemplate" runat="server" PagedControlID="ListContent" PageSize="20" OnPreRender="PagerTemplate_PreRender">
            <fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                <asp:TemplatePagerField>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField ButtonType="Button" LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="True" ShowPreviousPageButton="False" />
            </fields>
        </asp:DataPager>
    </div>

</asp:content>