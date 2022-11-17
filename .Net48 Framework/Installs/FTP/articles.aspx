<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="articles.aspx.cs" inherits="wwwroot.articles1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#NewestContent");
                $('#NewestContent tbody th').hide();
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 35;
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
                    <div class="col-md-4 no-padding">
                        <div class="article-img">
                            <img src="<%#
                this.Eval("DefaultPicture") %>" alt="" class="img-fluid">
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="article-content-area">
                            <div class="article-top-content">
                                <h3><a href="<%= this.GetInstallFolder() %>article/<%#
                this.Eval("ArticleID") %>/<%#
                this.Format_ISAPI(this.Eval("Headline")) %>/"><%#
                this.Eval("Headline") %></a></h3>
                                <p><%# SepCommon.SepCore.Strings.Left(Convert.ToString(this.Eval("Summary")), 500) + Convert.ToString(SepCommon.SepCore.Strings.Len(Convert.ToString(this.Eval("Summary"))) > 500 ? "..." : "") %></p>
                            </div>
                            <div class="article-btn-group">
                                <p><i class="fa fa-user-o"></i> Posted By: <strong><a href="<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>"><%#
                this.Eval("Author") %></a></strong></p>
                                <p><span>Date :</span> <%#
                this.Format_Date(this.Eval("Headline_Date").ToString()) %></p>
                                <a href="<%= this.GetInstallFolder() %>article/<%#
                this.Eval("ArticleID") %>/<%#
                this.Format_ISAPI(this.Eval("Headline")) %>/" class="btn btn-primary">Read More</a>
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

    <div runat="server" ID="NewestListings"><h5>Latest Article Postings</h5></div>

    <asp:GridView ID="NewestContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <div class="article-bx">
                        <div class="row">
                            <div class="col-md-4 no-padding">
                                <div class="article-img">
                                    <img src="<%#
                this.Eval("DefaultPicture") %>" alt="" class="img-fluid">
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="article-content-area">
                                    <div class="article-top-content">
                                        <h3><a href="<%= this.GetInstallFolder() %>article/<%#
                this.Eval("ArticleID") %>/<%#
                this.Format_ISAPI(this.Eval("Headline")) %>/"><%#
                this.Eval("Headline") %></a></h3>
                                        <p><%# SepCommon.SepCore.Strings.Left(Convert.ToString(this.Eval("Summary")), 500) + Convert.ToString(SepCommon.SepCore.Strings.Len(Convert.ToString(this.Eval("Summary"))) > 500 ? "..." : "") %></p>
                                    </div>
                                    <div class="article-btn-group">
                                        <p><i class="fa fa-user-o"></i> Posted By: <strong><a href="<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>"><%#
                this.Eval("Author") %></a></strong></p>
                                        <p><span>Date :</span> <%#
                this.Format_Date(this.Eval("Headline_Date").ToString()) %></p>
                                        <a href="<%= this.GetInstallFolder() %>article/<%#
                this.Eval("ArticleID") %>/<%#
                this.Format_ISAPI(this.Eval("Headline")) %>/" class="btn btn-primary">Read More</a>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>