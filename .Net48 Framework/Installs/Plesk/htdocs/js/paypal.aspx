<%@ page language="C#" CodeBehind="paypal.aspx.cs" Inherits="wwwroot.paypal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insert PayPal Button</title>
    <link type="text/css" rel="stylesheet" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js" integrity="sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI=" crossorigin="anonymous"></script>

    <script type="text/javascript" src="custom_dropdowns.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <p align="center">
        <b>Insert PayPal Item</b>
    </p>
    <table width="97%" align="center">
        <tr>
            <td>Your PayPal Email</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalEmail"/>
            </td>
        </tr><tr>
            <td>Item Name</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalItemName"/>
            </td>
        </tr><tr>
            <td>Item ID</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalItemID"/>
            </td>
        </tr><tr>
            <td>Sale Price</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalSalePrice"/>
            </td>
        </tr><tr>
            <td>Shipping Price</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalShippingPrice"/>
            </td>
        </tr><tr>
            <td>Ad'l Shipping</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalShippingPrice2"/>
            </td>
        </tr><tr>
            <td>Handling Fee</td>
            <td>
                <input type="text" style="width: 200px" name="PayPalHandling"/>
            </td>
        </tr>
    </table>
    <p align="center">
        <input type="button" onclick="windowClose('paypel');" value="Cancel"/> <input type="button" onclick="insertPayPal()" value="Insert"/>
    </p>
</form>
</body>
</html>