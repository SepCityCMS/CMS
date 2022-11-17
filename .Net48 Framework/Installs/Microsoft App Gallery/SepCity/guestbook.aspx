<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="guestbook.aspx.cs" inherits="wwwroot.guestbook1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <div class="GridViewStyle">
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="false" ClientIDMode="Static"
            CssClass="GridViewStyle">
            <Columns>
                <asp:TemplateField HeaderText="Web Site URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Convert.ToString(this.Eval("SiteURL").ToString() != "" ? "<a href=\"" + this.Eval("SiteURL") + "\" target=\"_blank\">" + this.Eval("SiteURL") + "</a>" : "") %>
                        <br />
                        <%#
                this.Eval("Message") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>