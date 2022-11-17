<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="elearning_my_courses.aspx.cs" inherits="wwwroot.elearning_my_courses" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            restyleGridView("#ListContent");
        });
    </script>
    <script src="<%= this.GetInstallFolder(true) %>js/gridview.js" type="text/javascript"></script>
    <script src="<%= this.GetInstallFolder(true) %>js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>My Courses</h1>

    <span class="successNotification" id="successNotification">
        <span ID="DeleteResult" runat="server"></span>
    </span>

    <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="true"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderText="Course Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href="<%= this.GetInstallFolder() %>course/<%#
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
</asp:content>