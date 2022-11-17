<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="elearning.aspx.cs" inherits="wwwroot.elearning1" %>

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
        cCategories.ModuleID = 37;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="true"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <img class="img-fluid img-thumbnail" src="<%= this.GetInstallFolder(true) %>spadmin/show_image.aspx?ModuleID=37&UniqueID=<%#
                this.Eval("CourseID") %>" style="width:200px;border-width:0px;">
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder(false) %>course/<%#
                this.Eval("CourseID") %>/<%#
                this.Format_ISAPI(this.Eval("CourseName")) %>/"><%#
                this.Eval("CourseName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Instructor" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Instructor") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Credits" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Credits") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start/End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Format_Date(this.Eval("StartDate").ToString()) %> - <%#
                this.Format_Date(this.Eval("EndDate").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="NewestContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        Caption="Newest Courses">
        <Columns>
            <asp:TemplateField HeaderText="Course Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder(false) %>course/<%#
                this.Eval("CourseID") %>/<%#
                this.Format_ISAPI(this.Eval("CourseName")) %>/"><%#
                this.Eval("CourseName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Instructor" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Instructor") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Credits" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Eval("Credits") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start/End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
                this.Format_Date(this.Eval("StartDate").ToString()) %> - <%#
                this.Format_Date(this.Eval("EndDate").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>