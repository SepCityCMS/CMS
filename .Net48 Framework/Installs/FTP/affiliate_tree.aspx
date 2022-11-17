<%@ page title="Affiliate Tree" language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="affiliate_tree.aspx.cs" inherits="wwwroot.affiliate_tree1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span class="successNotification" id="successNotification">
        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="GridViewStyle">
        <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                      CssClass="GridViewStyle" OnRowDataBound="ManageGridView_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Level" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("Level")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Full Name / Email Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("FirstName")%> <%#
                Eval("LastName")%> &lt;<%#
                Eval("EmailAddress")%>&gt;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Joined" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#
                Eval("DateJoined")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <asp:Button ID="PrevResults" runat="server" Text="Prev" OnClick="PrevResults_Click" CommandName="ReferralID" CommandArgument='<%#
                Eval("PrevAffiliateID") + "||" + Eval("Level")%>' />
                        <asp:Button ID="MoreResults" runat="server" Text="Next" OnClick="MoreResults_Click" CommandName="AffiliateID" CommandArgument='<%#
                Eval("AffiliateID") + "||" + Eval("Level")%>' />
                        <asp:HiddenField ID="AffiliateLevel" runat="server" Value='<%#
                Eval("Level")%>' />
                        <asp:HiddenField ID="AffiliateHasLevels" runat="server" Value='<%#
                Eval("HasLevels")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:content>