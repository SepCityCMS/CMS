jQuery.browser = {};
(function() {
    jQuery.browser.msie = false;
    jQuery.browser.version = 0;
    if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
        jQuery.browser.msie = true;
        jQuery.browser.version = RegExp.$1;
    }
})();

function insertHTML(inputText) {
    parent.tinymce.activeEditor.insertContent(inputText);
    parent.tinymce.activeEditor.windowManager.close();
}

function windowClose(dialogId) {
    parent.closeDialog(dialogId);
}

function insertPayPal() {
    insertHTML('<div class=\""PayPalCart\""><form action=\""https://www.paypal.com/cgi-bin/webscr\"" name=\""PayPal\"" target=\""_blank\"" method=\""post\""><input type=\""hidden\"" name=\""cmd\"" value=\""_cart\""><input type=\""hidden\"" name=\""business\"" value=\""' + document.getElementById("PayPalEmail").value + '\""><input type=\""hidden\"" name=\""item_name\"" value=\""' + document.getElementById("PayPalItemName").value + '\""><input type=\""hidden\"" name=\""item_number\"" value=\""' + document.getElementById("PayPalItemID").value + '\""><input type=\""hidden\"" name=\""amount\"" value=\""' + document.getElementById("PayPalSalePrice").value + '\""><input type=\""hidden\"" name=\""shipping\"" value=\""' + document.getElementById("PayPalShippingPrice").value + '\""><input type=\""hidden\"" name=\""shipping2\"" value=\""' + document.getElementById("PayPalShippingPrice2").value + '\""><input type=\""hidden\"" name=\""handling_cart\"" value=\""' + document.getElementById("PayPalHandling").value + '\""><input type=\""image\"" src=\""https://www.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif\"" name=\""add\""></form></div>');
    windowClose("paypal");
}

function insertFunction() {
    var text = "";
    var sValue = document.getElementById("dinsFeature").value;
    if (Right(sValue, 8) === "Simple]]") {
        text = Left(sValue, sValue.length - 8) + document.getElementById("dinsNum").value + "|Simple]]";
    } else if (Right(sValue, 8) === "Slider]]") {
        text = Left(sValue, sValue.length - 8) + document.getElementById("dinsNum").value + "|Slider]]";
    } else {
        text = sValue + document.getElementById("dinsNum").value + "]]";
    }
    insertHTML(text);
    windowClose("newestcontent");
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}