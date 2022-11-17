<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="classifieds.aspx.cs" inherits="wwwroot.classifieds" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#ListContent");
                restyleGridView("#NewestContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 44;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="true"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderText="Thumbnail" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="90px">
                <ItemTemplate>
                    <div style="width: 100%;" <%# Convert.ToString(Convert.ToBoolean(this.Eval("Highlight")) ? " class=\"PHighlight\"" : "") %>>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "<strong>" : "") %>
                        <a href="<%= this.GetInstallFolder() %>classified/<%#
                this.Eval("AdID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/">
                            <img src='<%#
                this.Eval("DefaultPicture") %>'
                                border="0" alt='<%#
                this.Eval("Title") %>' />
                        </a>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "</strong>" : "") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <div style="width: 100%;" <%# Convert.ToString(Convert.ToBoolean(this.Eval("Highlight")) ? " class=\"PHighlight\"" : "") %>>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "<strong>" : "") %>
                        <a href="<%= this.GetInstallFolder() %>classified/<%#
                this.Eval("AdID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/"><%#
                this.Eval("Title") %></a>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "</strong>" : "") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <div style="width: 100%;" <%# Convert.ToString(Convert.ToBoolean(this.Eval("Highlight")) ? " class=\"PHighlight\"" : "") %>>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "<strong>" : "") %>
                        <%#
                this.Eval("Price") %>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "</strong>" : "") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="NewestContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="Latest Classified Ad Postings">
        <Columns>
            <asp:TemplateField HeaderText="Thumbnail" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="90px">
                <ItemTemplate>
                    <div style="width: 100%;" <%# Convert.ToString(Convert.ToBoolean(this.Eval("Highlight")) ? " class=\"PHighlight\"" : "") %>>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "<strong>" : "") %>
                        <a href="<%= this.GetInstallFolder() %>classified/<%#
                this.Eval("AdID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/">
                            <img src='<%#
                this.Eval("DefaultPicture") %>'
                                border="0" alt='<%#
                this.Eval("Title") %>' />
                            <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "</strong>" : "") %>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <div style="width: 100%;" <%# Convert.ToString(Convert.ToBoolean(this.Eval("Highlight")) ? " class=\"PHighlight\"" : "") %>>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "<strong>" : "") %>
                        <a href="<%= this.GetInstallFolder() %>classified/<%#
                this.Eval("AdID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/"><%#
                this.Eval("Title") %></a>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "</strong>" : "") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <div style="width: 100%;" <%# Convert.ToString(Convert.ToBoolean(this.Eval("Highlight")) ? " class=\"PHighlight\"" : "") %>>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "<strong>" : "") %>
                        <%#
                SepCommon.SepCore.Strings.FormatCurrency(this.Eval("Price")) %>
                        <%# Convert.ToString(Convert.ToBoolean(this.Eval("BoldTitle")) ? "</strong>" : "") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>