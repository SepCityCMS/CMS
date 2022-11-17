<%@ page title="Affiliate" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="affiliate_downline.aspx.cs" inherits="wwwroot.affiliate_downline" enableeventvalidation="false" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 39;
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID"))) cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Affiliate User Downline"></span>
        </h2>

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
                            <a href='email_user.aspx?UserID=<%#
                Eval("UserID")%>'><%#
                Eval("FirstName")%> <%#
                Eval("LastName")%> &lt;<%#
                Eval("EmailAddress")%>&gt;</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Joined" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("DateJoined")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Go" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
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
</div>
    </asp:Panel>
</asp:content>