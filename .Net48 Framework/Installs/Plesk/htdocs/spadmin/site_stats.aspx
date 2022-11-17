<%@ page title="Site Statistics" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_stats.aspx.cs" inherits="wwwroot.site_stats" %>
<%@ import namespace="SepCommon" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        (function (w, d, s, g, js, fs) {
            g = w.gapi || (w.gapi = {}); g.analytics = { q: [], ready: function (f) { this.q.push(f); } };
            js = d.createElement(s); fs = d.getElementsByTagName(s)[0];
            js.src = 'https://apis.google.com/js/platform.js';
            fs.parentNode.insertBefore(js, fs); js.onload = function () { g.load('analytics'); };
        }(window, document, 'script'));

        gapi.analytics.ready(function () {

            /**
             * Authorize the user immediately if the user has already granted access.
             * If no access has been created, render an authorize button inside the
             * element with the ID "embed-api-auth-container".
             */
            gapi.analytics.auth.authorize({
                container: 'embed-api-auth-container',
                clientid: '<%=SepFunctions.Setup(989, "GoogleAnalyticsClientID")%>'
            });

            /**
             * Create a new ViewSelector instance to be rendered inside of an
             * element with the id "view-selector-container".
             */
            var viewSelector = new gapi.analytics.ViewSelector({
                container: 'view-selector-container'
            });

            // Render the view selector to the page.
            viewSelector.execute();

            /**
             * Create a new DataChart instance with the given query parameters
             * and Google chart options. It will be rendered inside an element
             * with the id "chart-container".
             */
            var dataChart = new gapi.analytics.googleCharts.DataChart({
                query: {
                    metrics: 'ga:sessions',
                    dimensions: 'ga:date',
                    'start-date': '30daysAgo',
                    'end-date': 'yesterday'
                },
                chart: {
                    container: 'chart-container',
                    type: 'LINE',
                    options: {
                        width: '100%'
                    }
                }
            });

            /**
             * Render the dataChart on the page whenever a new view is selected.
             */
            viewSelector.on('change', function (ids) {
                dataChart.set({ query: { ids: ids } }).execute();
            });

            /**
               * Create a ViewSelector for the first view to be rendered inside of an
               * element with the id "view-selector-1-container".
               */
            var viewSelector1 = new gapi.analytics.ViewSelector({
                container: 'view-selector-1-container'
            });

            /**
             * Create a ViewSelector for the second view to be rendered inside of an
             * element with the id "view-selector-2-container".
             */
            var viewSelector2 = new gapi.analytics.ViewSelector({
                container: 'view-selector-2-container'
            });

            // Render both view selectors to the page.
            viewSelector1.execute();
            viewSelector2.execute();

            /**
             * Create the first DataChart for top countries over the past 30 days.
             * It will be rendered inside an element with the id "chart-1-container".
             */
            var dataChart1 = new gapi.analytics.googleCharts.DataChart({
                query: {
                    metrics: 'ga:sessions',
                    dimensions: 'ga:country',
                    'start-date': '30daysAgo',
                    'end-date': 'yesterday',
                    'max-results': 6,
                    sort: '-ga:sessions'
                },
                chart: {
                    container: 'chart-1-container',
                    type: 'PIE',
                    options: {
                        width: '100%',
                        pieHole: 4 / 9
                    }
                }
            });

            /**
             * Create the second DataChart for top countries over the past 30 days.
             * It will be rendered inside an element with the id "chart-2-container".
             */
            var dataChart2 = new gapi.analytics.googleCharts.DataChart({
                query: {
                    metrics: 'ga:sessions',
                    dimensions: 'ga:country',
                    'start-date': '30daysAgo',
                    'end-date': 'yesterday',
                    'max-results': 6,
                    sort: '-ga:sessions'
                },
                chart: {
                    container: 'chart-2-container',
                    type: 'PIE',
                    options: {
                        width: '100%',
                        pieHole: 4 / 9
                    }
                }
            });

            /**
             * Update the first dataChart when the first view selecter is changed.
             */
            viewSelector1.on('change', function (ids) {
                dataChart1.set({ query: { ids: ids } }).execute();
            });

            /**
             * Update the second dataChart when the second view selecter is changed.
             */
            viewSelector2.on('change', function (ids) {
                dataChart2.set({ query: { ids: ids } }).execute();
            });

            var viewSelector = new gapi.analytics.ViewSelector({
                container: 'view-selector-container2'
            });

            // Render the view selector to the page.
            viewSelector.execute();

            /**
             * Create a table chart showing top browsers for users to interact with.
             * Clicking on a row in the table will update a second timeline chart with
             * data from the selected browser.
             */
            var mainChart = new gapi.analytics.googleCharts.DataChart({
                query: {
                    'dimensions': 'ga:browser',
                    'metrics': 'ga:sessions',
                    'sort': '-ga:sessions',
                    'max-results': '6'
                },
                chart: {
                    type: 'TABLE',
                    container: 'main-chart-container',
                    options: {
                        width: '100%'
                    }
                }
            });

            /**
             * Create a timeline chart showing sessions over time for the browser the
             * user selected in the main chart.
             */
            var breakdownChart = new gapi.analytics.googleCharts.DataChart({
                query: {
                    'dimensions': 'ga:date',
                    'metrics': 'ga:sessions',
                    'start-date': '7daysAgo',
                    'end-date': 'yesterday'
                },
                chart: {
                    type: 'LINE',
                    container: 'breakdown-chart-container',
                    options: {
                        width: '100%'
                    }
                }
            });

            /**
             * Store a refernce to the row click listener variable so it can be
             * removed later to prevent leaking memory when the chart instance is
             * replaced.
             */
            var mainChartRowClickListener;

            /**
             * Update both charts whenever the selected view changes.
             */
            viewSelector.on('change', function (ids) {
                var options = { query: { ids: ids } };

                // Clean up any event listeners registered on the main chart before
                // rendering a new one.
                if (mainChartRowClickListener) {
                    google.visualization.events.removeListener(mainChartRowClickListener);
                }

                mainChart.set(options).execute();
                breakdownChart.set(options);

                // Only render the breakdown chart if a browser filter has been set.
                if (breakdownChart.get().query.filters) breakdownChart.execute();
            });

            /**
             * Each time the main chart is rendered, add an event listener to it so
             * that when the user clicks on a row, the line chart is updated with
             * the data from the browser in the clicked row.
             */
            mainChart.on('success', function (response) {

                var chart = response.chart;
                var dataTable = response.dataTable;

                // Store a reference to this listener so it can be cleaned up later.
                mainChartRowClickListener = google.visualization.events
                    .addListener(chart, 'select', function (event) {

                        // When you unselect a row, the "select" event still fires
                        // but the selection is empty. Ignore that case.
                        if (!chart.getSelection().length) return;

                        var row = chart.getSelection()[0].row;
                        var browser = dataTable.getValue(row, 0);
                        var options = {
                            query: {
                                filters: 'ga:browser==' + browser
                            },
                            chart: {
                                options: {
                                    title: browser
                                }
                            }
                        };

                        breakdownChart.set(options).execute();
                    });
            });

        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <h2>
            <span ID="PageHeader" runat="server" Text="Site Statistics"></span>
        </h2>

        <div class="mb-3">
			<div id="embed-api-auth-container"></div>
		</div>

        <div class="mb-3">
			<div id="chart-container"></div>
			<div id="view-selector-container" style="display:none"></div>
        </div>

        <div class="mb-3">
            <div class="col-md-6">
				<div id="chart-1-container"></div>
				<div id="view-selector-1-container" style="display:none"></div>
			</div>
            <div class="col-md-6">
				<div id="chart-2-container"></div>
				<div id="view-selector-2-container" style="display:none"></div>
			</div>
		</div>

        <div class="mb-3">
			<div id="main-chart-container"></div>
			<div id="breakdown-chart-container"></div>
			<div id="view-selector-container2" style="display:none"></div>
        </div>
    </asp:Panel>
</asp:content>