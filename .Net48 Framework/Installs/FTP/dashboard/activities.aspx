<%@ page language="C#" viewstatemode="Enabled" masterpagefile="Site.Master" codebehind="activities.aspx.cs" inherits="wwwroot.activities2" %>

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
                        <h2>Activities</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a href="javascript:void(0)" onclick="changeDashYear()">Change Year</a></li>
                                </ul>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <!-- content starts here -->
                        <div class="row tile_count">
                            <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
                                <span class="count_top"><i class="fa fa-user"></i>Total Activities</span>
                                <div class="count">
                                    <span ID="TotalActivities" runat="server"></span>
                                </div>
                                <span class="count_bottom">
                                    <span ID="UpPercentage" runat="server"></span>% From last Week</span>
                            </div>
                        </div>
                        <!-- Activity Trend -->
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Monthly Trend Report</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content2">
                                    <div id="activity_trend" style="height: 300px; width: 100%;"></div>
                                </div>
                            </div>
                        </div>
                        <!-- /Activity Trend -->
                        <!-- Top 10 Used Activities -->
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Top 10 Used Activities</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content2">
                                    <div id="top_10_activities" style="height: 300px; width: 100%;"></div>
                                </div>
                            </div>
                        </div>
                        <!-- /Top 10 Used Activities -->
                        <!-- content ends here -->
                    </div>
                </div>
            </div>
        </div>

        <span ID="TrendData" runat="server"></span>
    </asp:Panel>
</asp:content>