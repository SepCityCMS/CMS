<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="matchmaker_search.aspx.cs" inherits="wwwroot.matchmaker_search" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <h1>Search User Profiles</h1>

    <table width="100%">
        <tr>
            <td colspan="2">I am a
                <select id="IAm" runat="server" class="inline-block" width="150">
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                </select>
                looking for a
                <select id="SearchingFor" runat="server" class="inline-block" width="150">
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2">Between
                <select id="StartAge" runat="server" class="inline-block" width="90">
                </select>
                and
                <select id="EndAge" runat="server" class="inline-block" width="90">
                </select>
                years old.
            </td>
        </tr>
    </table>

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
			    <select id="Distance" runat="server" class="inline-block" width="100px">
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

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <br />

    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <LayoutTemplate>
            <table border="0" cellpadding="1">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td width="50%" nowrap="nowrap">
                    <div class="ArticleList" style="float: left; overflow: hidden; white-space: nowrap; width: 100%;">
                        <div style="float: left;">
                            <img src="<%#
                this.Eval("DefaultPicture") %>"
                                border="0" alt="" />
                            <br />
                            Last Login: <%#
                this.Format_Date(this.Eval("LastLogin").ToString()) %>
                            <br />
                            Views: <%#
                this.Eval("Views") %>
                        </div>
                        <div style="display: inline-block;">
                            <a href="<%= this.GetInstallFolder() %>match/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/"><%#
                this.Eval("Username") %></a>
                            <br />
                            Age: <%#
                this.Eval("Age") %>
                            <br />
                            Gender: <%#
                this.Eval("Sex") %>
                            <br />
                            Location: <%#
                this.Eval("Location") %>
                            <br />
                            Distance: <%#
                this.Eval("Distance") %>
                            <br />
                        </div>
                    </div>
                </td>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <td width="50%" nowrap="nowrap">
                <div class="ArticleList">
                    <div style="float: left;">
                        <img src="<%#
                this.Eval("DefaultPicture") %>"
                            border="0" alt="" />
                        <br />
                        Last Login: <%#
                this.Format_Date(this.Eval("LastLogin").ToString()) %>
                        <br />
                        Views: <%#
                this.Eval("Views") %>
                    </div>
                    <div style="float: left;">
                        <a href="<%= this.GetInstallFolder() %>match/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/"><%#
                this.Eval("Username") %></a>
                        <br />
                        Age: <%#
                this.Eval("Age") %>
                        <br />
                        Gender: <%#
                this.Eval("Sex") %>
                        <br />
                        Location: <%#
                this.Eval("Location") %>
                        <br />
                        Distance: <%#
                this.Eval("Distance") %>
                        <br />
                    </div>
                </div>
            </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
    <br />
    <div class="PagingPanel">
        <asp:DataPager ID="PagerTemplate" runat="server" PagedControlID="ListContent" PageSize="20" OnPreRender="PagerTemplate_PreRender">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                <asp:TemplatePagerField>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField ButtonType="Button" LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="True" ShowPreviousPageButton="False" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:content>