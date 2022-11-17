<%@ Page Language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master" 
    CodeBehind="conference_schedule.aspx.cs" Inherits="wwwroot.conference_schedule1" %>
<%@ Import Namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            restyleGridView("#MeetingToGrid");
            restyleGridView("#MeetingFromGrid");
        });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h2>My Schedule</h2>
    
    <asp:GridView ID="MeetingToGrid" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" EnableViewState="True">
        <Columns>
            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href='<%= this.GetInstallFolder() %>video/default.aspx?MeetingID=<%#
                        this.Eval("MeetingID") %>'><%#
                        this.Eval("Subject") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%# this.Eval("ToUserName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
            this.Eval("StartDate") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />

    <asp:GridView ID="MeetingFromGrid" runat="server" AutoGenerateColumns="False" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" EnableViewState="True">
        <Columns>
            <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <a href='<%= this.GetInstallFolder() %>video/default.aspx?MeetingID=<%#
                        this.Eval("MeetingID") %>'><%#
                        this.Eval("Subject") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%# this.Eval("FromUserName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%#
            this.Eval("StartDate") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:content>
