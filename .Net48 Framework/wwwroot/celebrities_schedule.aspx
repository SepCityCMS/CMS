<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="celebrities_schedule.aspx.cs" inherits="wwwroot.conference_schedule" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#ListContent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <div id="PageContent" runat="server">
        <span id="failureNotification">
            <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <asp:GridView ID="ListContent" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static" ShowHeader="true"
            CssClass="GridViewStyle">
            <Columns>
                <asp:TemplateField HeaderText="Full Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>celebrities_schedule_view.aspx?EventID=<%#
                this.Eval("EventID") %>"><%#
                this.GetFullName(this.Eval("UserID").ToString()) %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Eval("Subject") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date/Time" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                this.Format_Date(this.Eval("EventDate").ToString()) %> <%#
                this.Eval("BegTime") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Call Now" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <a href="<%= this.GetInstallFolder() %>video/default.aspx?DoAction=MakeCall&UserID=<%#
                this.Eval("UserID") %>">Call Now</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>