<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="auction_search.aspx.cs" inherits="wwwroot.auction_search" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Search the Auction Ads</h1>

    <div class="mb-3">
        <div class="col-md-12 mb-12">
            <label for="q">Keywords:</label>
            <input type="text" id="q" runat="server" clientidmode="Static" onkeypress="if(checkEnter(event) == false){document.getElementById('SearchButton').click();return checkEnter(event);}" class="form-control" />
        </div>
    </div>

    <hr width="100%" />

    <div id="RadiusSearching" runat="server">
        <div class="mb-3">
            <div class="col-md-12 mb-12">
                <label>Location Search:</label>
            </div>
        </div>
        <div class="mb-3">
            <div class="col-md-12 mb-12">
                Within
			    <select id="Distance" runat="server" cssclass="inline-block" width="100px">
                    <option value="5">5</option>
                    <option value="10">10</option>
                    <option value="25">25</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                    <option value="ANY">Any</option>
                </select>
                <span id="DistanceText">
                    <span ID="MilesText" runat="server" ClientIDMode="Static"></span></span> from
			     <input type="text" id="PostalCode" runat="server" style="width: 100px" class="inline-block" />
                (Zip/Postal Code)
            </div>
        </div>
        <br />
        <div class="mb-3">
            <div class="col-md-12 mb-12">
                <div>
                    <label>Or:</label>
                </div>
            </div>
        </div>
        <br />
        <div class="mb-3">
            <div class="col-md-6" id="SearchCountry1" runat="server">
                <label>Country:</label>
            </div>
            <div class="col-md-6">
                <label>State/Province:</label>
            </div>
            <span ID="NoCountryHtml" runat="server"></span>
            <div class="col-md-6" id="SearchCountry2" runat="server">
                <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />
            </div>
            <div class="col-md-6">
                <sep:StateDropdown ID="State" runat="server" CssClass="form-control" />
            </div>
        </div>
    </div>
    <br />
    <div class="mb-3">
        <div class="col-md-12 mb-12">
            <asp:Button ID="SearchButton" runat="server" CssClass="btn btn-light" Text="Search" OnClick="SearchButton_Click" />
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