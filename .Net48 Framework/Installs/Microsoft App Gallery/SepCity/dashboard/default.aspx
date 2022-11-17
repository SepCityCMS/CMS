<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" codebehind="default.aspx.cs" inherits="wwwroot._default7" %>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">
        <div class="page-title">
            <div class="title_left">
                <h3></h3>
            </div>

            <div class="title_right">
                <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="mb-3">
            <div class="col-md-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>My Dashboard</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <!-- content starts here -->
                        <div class="row tile_count">
                            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
                                <span class="count_top"><i class="fa fa-user"></i>Total Users</span>
                                <div class="count">
                                    <span ID="TotalUsers" runat="server"></span>
                                </div>
                                <span class="count_bottom">
                                    <span ID="PercentUsers" runat="server"></span>
                                    From last Week</span>
                            </div>
                            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
                                <span class="count_top"><i class="fa fa-user"></i>Total Males</span>
                                <div class="count">
                                    <span ID="TotalMales" runat="server"></span>
                                </div>
                                <span class="count_bottom">
                                    <span ID="PercentMales" runat="server"></span>
                                    From last Week</span>
                            </div>
                            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
                                <span class="count_top"><i class="fa fa-user"></i>Total Females</span>
                                <div class="count">
                                    <span ID="TotalFemales" runat="server"></span>
                                </div>
                                <span class="count_bottom">
                                    <span ID="PercentFemales" runat="server"></span>
                                    From last Week</span>
                            </div>
                            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
                                <span class="count_top"><i class="fa fa-user"></i>Total Categories</span>
                                <div class="count">
                                    <span ID="TotalCategories" runat="server"></span>
                                </div>
                            </div>
                        </div>

                        <div class="mb-3" id="UserLocations" runat="server">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h2>User location <small>geo-presentation</small></h2>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="dashboard-widget-content">
                                            <div class="col-md-4 hidden-small">
                                                <h2 class="line_30">
                                                    <span ID="TotalUsers2" runat="server"></span>
                                                    users from
                                                    <span ID="numCountries" runat="server"></span>
                                                    countries</h2>

                                                <table class="countries_list">
                                                    <tbody>
                                                        <span ID="CountryList" runat="server"></span>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div id="world-map-gdp" class="col-md-8 col-sm-12 col-xs-12" style="height: 230px;"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- content ends here -->
                    </div>
                </div>
            </div>
        </div>

        <span ID="TrendData" runat="server"></span>
    </asp:Panel>
</asp:content>