<%@ page title="Custom Reports" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="custom_reports.aspx.cs" inherits="wwwroot.custom_reports" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function printReport(doAction) {
            window.open('custom_reports_print.aspx?DoAction=' +
                doAction +
                '&ReportID=' +
                document.getElementById('ReportID').value,
                '_blank');
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 982;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Custom Reports</h4>
                <div class="mb-3">
                    <label ID="ReportNameLabel" clientidmode="Static" runat="server" for="ReportID">Report Name:</label>
                    <select ID="ReportID" runat="server" class="form-control" ClientIDMode="Static">
                        <option value="(Select a Report)"></option>
                    </select>
                </div>
                <div class="mb-3">
                    <label ID="ShowTotalsLabel" clientidmode="Static" runat="server" for="ShowTotals" style="display:inline-block;">Show Totals:</label>
                    <asp:CheckBox ID="ShowTotals" runat="server" CssClass="checkboxEntry" />
                </div>
            </div>
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="PrintButton" runat="server" Text="Print" OnClientClick="printReport('Print')" /> <asp:Button CssClass="btn btn-success" ID="ExportButton" runat="server" Text="Export" OnClientClick="printReport('Export')" /></div>
        </div>
    </asp:Panel>
</asp:content>