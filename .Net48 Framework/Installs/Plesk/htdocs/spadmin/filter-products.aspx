<%@ page language="C#" viewstatemode="Enabled" codebehind="filter-products.aspx.cs" inherits="wwwroot.filter_products" %>
<%@ Import Namespace="SepCommon" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head2" runat="server">

    <script type="text/javascript">
        var config = {
            base: '<%= this.GetInstallFolder(true) %>'
        };
    </script>
    <link type="text/css" rel="stylesheet" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js" integrity="sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI=" crossorigin="anonymous"></script>
    
    <script src="../js/bootbox.min.js" type="text/javascript"></script>
    <script src="../js/main.js" type="text/javascript"></script>
    <script src="../js/country.js" type="text/javascript"></script>

    <script type="text/javascript">
        function assignProduct(iRowOffset, sProductID, sProductName, sUnitPrice) {
            var newRowOffset = parseInt(iRowOffset) + 1;
            var row = document.createElement("tr");
            if (iRowOffset % 2 == 0) {
                row.setAttribute("class", "RowStyle");
            } else {
                row.setAttribute("class", "AltRowStyle");
            }
            var tdProductName = document.createElement("td");
            var tdUnitPrice = document.createElement("td");
            var tdQty = document.createElement("td");
            var tdTotalPrice = document.createElement("td");

            tdProductName.innerHTML = '<input type=\"hidden\" name=\"ProductID' +
                sProductID +
                '\" id=\"ProductID' +
                sProductID +
                '\" value=\"' +
                sProductID +
                '\" />' +
                sProductName;
            tdUnitPrice.innerHTML = sUnitPrice;
            tdQty.innerHTML = "1";
            tdTotalPrice.innerHTML = sUnitPrice;

            row.appendChild(tdProductName);
            row.appendChild(tdUnitPrice);
            row.appendChild(tdQty);
            row.appendChild(tdTotalPrice);

            window.parent.$("#AddProductIDs").val(window.parent.$("#AddProductIDs").val() + ',' + sProductID);
            window.parent.$("#AddProductName").val(window.parent.$("#AddProductName").val() + '||' + sProductName);
            window.parent.$("#AddUnitPrice").val(window.parent.$("#AddUnitPrice").val() + '||' + sUnitPrice);

            window.parent.$("#ManageGridView").append(row);

            parent.document.getElementById("AddProduct")
                .setAttribute("href", "javascript:openProductSearch('" + newRowOffset + "');");

            parent.closeDialog('FieldFilterDiv');
        }
        <%
            if (SepFunctions.ModuleActivated(41) == false)
            {
        %>
        $(document).ready(function () {
            parent.$("#ProductSearch").hide();
            parent.$("#SearchButton").hide();
        });
        <% } %>
    </script>
    <title>Filter Products</title>
</head>
<body>

    <form id="form1" runat="server">
        
        <div class="GridViewStyle" id="FilterGrid" runat="server">
            <asp:gridview id="ManageGridView" runat="server" autogeneratecolumns="False" allowsorting="True" clientidmode="Static"
                cssclass="GridViewStyle" showheader="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="javascript:assignProduct('<%= SepCommon.SepCore.Request.Item("RowOffset") %>', '<%#
                this.Eval("ProductID") %>', '<%#
                this.Eval("ProductName") %>', '<%#
                this.Eval("DisplayPrice") %>')"><%#
                this.Eval("ProductName") %></a>
                            <br />
                            <%#
                this.Eval("DisplayPrice") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:gridview>
        </div>

        <div id="CustomProduct" runat="server">

            <span id="failureNotification">
                <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
            </span>

            <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Add Custom Product</h4>
                <div class="form-group">
                    <label id="ProductNameLabel" clientidmode="Static" runat="server" for="ProductName">Product / Service Name:</label>
                    <input type="text" id="ProductName" runat="server" class="form-control" maxlength="100" />
                    <asp:customvalidator id="ProductNameRequired" runat="server" controltovalidate="ProductName"
                        clientvalidationfunction="customFormValidator" errormessage="Product Name is required."
                        validateemptytext="true" display="Dynamic">
                    </asp:customvalidator>
                </div>
                <div class="form-group">
                    <label id="UnitPriceLabel" clientidmode="Static" runat="server" for="UnitPrice">Unit Price:</label>
                    <input type="text" id="UnitPrice" runat="server" class="form-control" maxlength="100" />
                    <asp:customvalidator id="UnitPriceRequired" runat="server" controltovalidate="UnitPrice"
                        clientvalidationfunction="customFormValidator" errormessage="Unit Price is required."
                        validateemptytext="true" display="Dynamic">
                    </asp:customvalidator>
                </div>

                <hr class="mb-4" />

                <div class="mb-3">
                    <asp:button cssclass="btn btn-primary" id="AddButton" runat="server" text="Add" onclick="AddButton_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>