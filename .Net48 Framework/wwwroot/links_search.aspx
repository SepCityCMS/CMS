<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="links_search.aspx.cs" inherits="wwwroot.links_search" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Search the Links</h1>

    <div class="col-lg-12">
        <div class="input-group">
            <input type="text" id="q" runat="server" clientidmode="Static" class="form-control" onkeypress="if(checkEnter(event) == false){document.getElementById('SearchButton').click();return checkEnter(event);}" />
            <span class="input-group-btn">
                <asp:Button ID="SearchButton" CssClass="btn btn-light" runat="server" Text="Search" OnClick="SearchButton_Click" />
            </span>
        </div>
    </div>

    <br />

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div id="SearchResults" runat="server">
        <asp:Label ID="LabelSummary" runat="server" Text="<%#
                this.Summary %>" CssClass="SearchSummary"></asp:Label>
        <br />
        <br />

        Result page:
        <asp:Repeater ID="Repeater3" runat="server" DataSource="<%#
                this.Paging %>">
            <ItemTemplate>
                <%#
                DataBinder.Eval(Container.DataItem, "html") %>
            </ItemTemplate>
        </asp:Repeater>
        <br />

        <asp:Repeater ID="Repeater1" runat="server" DataSource="<%#
                this.Results %>">
            <ItemTemplate>
                <p class="SearchResults">
                    <div>
                        <a href='http://<%#
                DataBinder.Eval(Container.DataItem, "url") %>'><%#
                DataBinder.Eval(Container.DataItem, "title") %></a>
                    </div>
                    <div class="SearchURL">
                        <a href='http://<%#
                DataBinder.Eval(Container.DataItem, "url") %>'><%#
                DataBinder.Eval(Container.DataItem, "url") %></a>
                    </div>
                    <div class="SearchDescription">
                        <%#
                DataBinder.Eval(Container.DataItem, "description") %>
                    </div>
                </p>
            </ItemTemplate>
        </asp:Repeater>
        <br />

        Result page:
        <asp:Repeater ID="Repeater2" runat="server" DataSource="<%#
                this.Paging %>">
            <ItemTemplate>
                <%#
                DataBinder.Eval(Container.DataItem, "html") %>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:content>