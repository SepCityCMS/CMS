<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="downloads.aspx.cs" inherits="wwwroot.downloads1" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#LatestContent");
                restyleGridView("#PopularContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 10;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <asp:GridView ID="AudioContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridViewAudio_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table width="100%" align="center" class="Table">
                        <tr class="TableHeader">
                            <td>
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableHeader">
                                        <td width="33%" valign="top">
                                            <b><%#
                this.Eval("Field2") %> - <%#
                this.Eval("Field1") %></b>
                                        </td>

                                        <td width="50%" valign="top" align="center" nowrap="nowrap">
                                            <sep:RatingStars ID="RateStars" runat="server" ModuleID="10" LookupID='<%#
                this.Eval("FileID") %>' />
                                        </td>
                                        <td width="33%" valign="top" align="right">
                                            <b><%= Convert.ToString(sLibraryDownload == "Yes" ? "Downloaded" : "Played") %> <%#
                this.Eval("TotalDownloads") %> times</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="TableBody1">
                            <td>
                                <%#
                this.Eval("Field3") %>
                            </a>
                            <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                <tr class="TableBody1">
                                    <td valign="top">
                                        <b>
                                            <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'><%= Convert.ToString(sLibraryDownload == "Yes" ? "Downloaded" : "Play Audio") %></a>
                                        </b>
                                    </td>
                                    <td align="right" valign="top">
                                        <b>Posted By:</b> <a href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>'><%#
                this.Eval("UserName") %></a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="DocumentContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" OnPageIndexChanging="ManageGridViewDocument_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table width="100%" align="center" class="Table">
                        <tr class="TableTitle">
                            <td>&nbsp;&nbsp;&nbsp;<b><%#
                this.Eval("Field1") %></b></td>
                        </tr>
                        <tr class="TableHeader">
                            <td>
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableHeader">
                                        <td width="33%" valign="top">
                                            <b>Downloads:</b> <%#
                this.Eval("TotalDownloads") %>
                                        </td>
                                        <td width="33%" valign="top" align="center">
                                            <b>Date Posted</b> <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                                        </td>

                                        <td width="50%" valign="top" align="center" nowrap="nowrap">
                                            <sep:RatingStars ID="RateStars" runat="server" ModuleID="10" LookupID='<%#
                this.Eval("FileID") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="TableBody1">
                            <td>
                                <%#
                this.Eval("Field3") %><br />
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableBody1">
                                        <td valign="top">
                                            <b>
                                                <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'>Download Now</a>
                                            </b>
                                        </td>
                                        <td align="right">
                                            <b>Posted By:</b> <a href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>'><%#
                this.Eval("UserName") %></a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="ImageContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" OnPageIndexChanging="ManageGridViewImage_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast" Width="100%">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" valign="top" style="padding: 5px;">
                                <img src="<%#
                this.Eval("FileName") %>" alt="<%#
                this.Eval("Field1") %>" border="0" />
                            </td>
                            <td width="100%" valign="top" style="padding-left: 10px;">
                                <b><%#
                this.Eval("Field1") %></b>
                                <br />
                                <b>Date Posted:</b> <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                                <br />
                                <b>User Name: </b><a href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>'><%#
                this.Eval("UserName") %></a>
                                <br />
                                <b>Downloads: <%#
                this.Eval("TotalDownloads") %></b>
                                <br />
                                <sep:RatingStars ID="RatingStars1" runat="server" ModuleID="10" LookupID='<%#
                this.Eval("FileID") %>' />
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="SoftwareContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" OnPageIndexChanging="ManageGridViewSoft_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table width="100%" align="center" class="Table">
                        <tr class="TableTitle">
                            <td>
                                <table width="96%" border="0" cellpadding="0" cellspacing="0" align="center">
                                    <tr>
                                        <td width="33%" valign="top">
                                            <b><%#
                this.Eval("Field1") %></b>
                                        </td>
                                        <td width="33%" valign="top" align="center">
                                            <b>Version:</b> <%#
                this.Eval("Field2") %>
                                        </td>
                                        <td width="33%" valign="top" align="right">
                                            <b>Price:</b> <%#
                Convert.ToString(this.Format_Currency(this.Eval("Field3").ToString()) == this.Format_Currency("0") ? "FREE" : this.Format_Currency(this.Eval("Field3").ToString())) %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="TableHeader">
                            <td>
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableHeader">
                                        <td width="33%" valign="top">
                                            <b>Downloads:</b> <%#
                this.Eval("TotalDownloads") %>
                                        </td>
                                        <td width="33%" valign="top" align="center">
                                            <b>Date Posted:</b> <%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %>
                                        </td>

                                        <td width="50%" valign="top" align="center" nowrap="nowrap">
                                            <sep:RatingStars ID="RateStars" runat="server" ModuleID="10" LookupID='<%#
                this.Eval("FileID") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="TableBody1">
                            <td>
                                <%#
                this.Eval("Field4") %><br />
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableBody1">
                                        <td valign="top">
                                            <b>
                                                <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'>Download Now</a>
                                            </b>
                                        </td>
                                        <td align="right">
                                            <b>User Name:</b> <a href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>'><%#
                this.Eval("UserName") %></a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="VideoContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" OnPageIndexChanging="ManageGridViewVideo_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table width="100%" align="center" class="Table">
                        <tr class="TableHeader">
                            <td>
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableHeader">
                                        <td width="33%" valign="top">
                                            <b><%#
                this.Eval("Field1") %></b>
                                        </td>
                                        <td width="50%" valign="top" align="center" nowrap="nowrap">
                                            <sep:RatingStars ID="RateStars" runat="server" ModuleID="10" LookupID='<%#
                this.Eval("FileID") %>' />
                                        </td>
                                        <td width="33%" valign="top" align="right">
                                            <b><%= Convert.ToString(sLibraryDownload == "Yes" ? "Downloadeded" : "Played") %> <%#
                this.Eval("TotalDownloads") %> times</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="TableBody1">
                            <td>
                                <%#
                this.Eval("Field3") %><br />
                                <table width="96%" border="0" cellpadding="1" cellspacing="0">
                                    <tr class="TableBody1">
                                        <td valign="top">
                                            <b>
                                                <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'><%= Convert.ToString(sLibraryDownload == "Yes" ? "Download Video" : "Play Video") %></a>
                                            </b>
                                        </td>
                                        <td align="right" valign="top">
                                            <b>User Name:</b> <a href='<%= this.GetInstallFolder() %>userinfo.aspx?UserID=<%#
                this.Eval("UserID") %>'><%#
                this.Eval("UserName") %></a>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <div class="row">
        <div class="col-md-6" id="LatestContentColumn" runat="server" visible="false">
            <asp:GridView ID="LatestContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
                CssClass="GridViewStyle" Caption="Latest Downloads">
                <Columns>
                    <asp:TemplateField HeaderText="Artist Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'><%#
                this.Eval("Field3") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name / Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'><%#
                this.Eval("Field1") %></a>
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
        </div>
        <div class="col-md-6" id="PopularContentColumn" runat="server" visible="false">
            <asp:GridView ID="PopularContent" Visible="false" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
                CssClass="GridViewStyle" Caption="Popular Downloads">
                <Columns>
                    <asp:TemplateField HeaderText="Artist Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'><%#
                this.Eval("Field3") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name / Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <a href='<%= this.GetInstallFolder() %>downloads_view.aspx?FileID=<%#
                this.Eval("FileID") %>'><%#
                this.Eval("Field1") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Downloads" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                this.Eval("TotalDownloads") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:content>