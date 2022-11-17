var debugMode = true;
var geocoder;
var map;
var skipRestyling = false;
var dialogHTML = "";

window.default_confirm = window.confirm;
window.default_alert = window.alert;

window.alert = function (message, title) {
    if (message === undefined) {
        return window.default_alert(message);
    }
    try {
        bootbox.alert({
            title: title,
            message: message
        });
    } catch (ex) {
        return window.default_alert(message);
    }
};

window.confirm = function (message, success_callback) {
    if (success_callback === undefined) {
        return window.default_confirm(message);
    }

    try {
        bootbox.confirm(message, function (result) {
            if (result === true) {
                success_callback();
            }
        });
    } catch (ex) {
        return window.default_confirm(message);
    }
};

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function LogGoogleEvent(module, action, label) {
    ga("set", "checkProtocolTask", function () { });

    ga("send", {
        hitType: "event",
        eventCategory: module,
        eventAction: action,
        eventLabel: label
    });
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

function debugMsg(sMessage) {
    if (debugMode === true) {
        return sMessage;
    } else {
        return "";
    }
}

function changeDashYear() {
    if ($("#changeYearM").length === 0) {
        var currentYear = new Date().getFullYear();
        var min = new Date().getFullYear(),
            max = min + 2;
        min = min - 2;
        max = max - 2;
        var aa = min;

        var dialogDiv = '<div id="changeYearM" title="Change Year">';
        dialogDiv += '<select id="yearSelect" class="selectEntry">';
        for (var i = min; i <= max; i++) {
            aa = i - 1;
            dialogDiv += '<option value="' + aa + '">' + aa + "</option>";
        }
        dialogDiv += '<option value="' + currentYear + '">' + currentYear + "</option>";
        dialogDiv += "</select>";
        dialogDiv += "</div>";
        $("body").append(dialogDiv);
        restyleFormElements("#changeYearM");
    }
    openModal("changeYearM", 280, 140, '<input type="button" onclick="changeDashYearNow();" class="btn btn-primary" value="Change" />');
}

function changeDashYearNow() {
    var href = "";
    if (document.location.href.indexOf("?") >= 0) {
        href = document.location.href.split("?")[0];
    } else {
        href = document.location.href;
    }
    document.location.href = href + "?Year=" + $("#yearSelect").val();
}

function getIdentity() {
    var s = [];
    var hexDigits = "192839093455102372";
    for (var i = 0; i < 16; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    var uuid = s.join("");
    return Left(uuid, 15);
}

function loadHelp() {
    openModal("helpDialog", 300, 270);
}

function checkEnter(e) {
    var key;
    if (window.event) {
        key = window.event.keyCode; //IE
    } else {
        key = e.which;  //firefox
    }

    return (key !== 13);
}

function WYSIWYGFunctions(commandName, isAdmin, isTinyMCE) {
    var sQuery = "";

    if (isTinyMCE === true) {
        sQuery = "?Editor=TinyMCE";
    }

    if (commandName === "newestcontent") {
        if ($("#newestcontent").length === 0) {
            $("body").append('<div id="newestcontent" class="dynamicFunctionDiv" title="Insert Newest Content"><iframe src="' + config.imageBase + "js/newest_features.aspx" + sQuery + '" name="newestinsertframe" id="newestinsertframe" width="320" height="110" frameborder="0"></iframe></div>');
        }
        openModal("newestcontent", 340, 220, '<input type="button" onclick="newestinsertframe.insertFunction();" class="btn btn-primary" value="Insert" />');
    }

    if (commandName === "dynamicfunctions") {
        if ($("#dynamicfunctions").length === 0) {
            $("body").append('<div id="dynamicfunctions" class="dynamicFunctionDiv" title="Insert Dynamic Content"><iframe src="' + config.imageBase + "js/insert_functions.aspx" + sQuery + '" name="dynamicinsertframe" id="dynamicinsertframe" width="320" height="110" frameborder="0"></iframe></div>');
        }
        openModal("dynamicfunctions", 340, 220, '<input type="button" onclick="dynamicinsertframe.insertFunction();" class="btn btn-primary" value="Insert" />');
    }

    if (commandName === "rssfeed") {
        if ($("#rssfeed").length === 0) {
            $("body").append('<div id="rssfeed" class="dynamicFunctionDiv" title="Insert RSS Feed"><iframe src="' + config.imageBase + "js/rss_feed.aspx" + sQuery + '" name="rssinsertframe" id="rssinsertframe" width="320" height="130" frameborder="0"></iframe></div>');
        }
        openModal("rssfeed", 340, 240, '<input type="button" onclick="rssinsertframe.insertRSS();" class="btn btn-primary" value="Insert" />');
    }
}

function openImageBrowser() {
    if ($("#imgbrowser").length === 0) {
        $("body").append('<div id="imgbrowser" title="Browse / Upload Images"><iframe src="' + config.imageBase + 'spadmin/ImageManager/default.aspx" width="650" height="450"></iframe></div>');
    }
    openModal("imgbrowser", 300, 400);
}

function showResults(sId, pollId) {
    if ($("#PollOptions" + sId).is(":hidden") === true) {
        $("#PollOptions" + sId).show();
        $("#PollResults" + sId).hide();
        $("#resultsLink" + sId).html("View Results");
    } else {
        $.ajax({
            type: "GET",
            url: config.imageBase + "api/polls/results?PollID=" + pollId,
            dataType: "json",
            contentType: "application/json",
            error: function (xhr) {
                $("#PollResults" + sId).hide();
                alert("You must be logged in or do not have access to view this poll.");
            },
            success: function (result) {
                var outputHTML = "";
                $.each(result, function (i) {
                    outputHTML += result[i].PollOption + "<br/>";
                    outputHTML += '<div class="progress">';
                    outputHTML += '<div class="progress-bar" role="progressbar" aria-valuenow="' + result[i].Percentage + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + result[i].Percentage + '%;">' + result[i].Percentage + "%</div>";
                    outputHTML += "</div>";
                });
                $("#PollResults" + sId).css("padding", "10px");
                $("#PollResults" + sId).html(outputHTML);
            }
        });
        $("#PollOptions" + sId).hide();
        $("#PollResults" + sId).show();
        $("#resultsLink" + sId).html("View Poll");
    }
}

function castVote(PollID, OptionID, PortalID, sId) {
    var params = new Object();
    params.PollID = PollID;
    params.OptionID = OptionID;
    params.PortalID = PortalID;

    $.ajax({
        type: "POST",
        data: JSON.stringify(params),
        url: config.imageBase + "api/polls/vote",
        dataType: "json",
        contentType: "application/json",
        error: function (xhr) {
            alert("You must be logged into our system to cast a vote.");
            document.cookie = "returnUrl=" + config.siteBase + "poll/" + PollID;
            document.location.href = config.siteBase + "login.aspx";
        },

        success: function (response) {
            showResults(sId, PollID);
        }
    });
}

function customFormValidator(oSrc, args) {
    if (args.Value === "") {
        if ($("#error" + oSrc.getAttribute("id")).length === 0) {
            var sHTML = '<div class="alert alert-danger" role="alert">One or more fields are required.</div>';
            $("#ErrorMessage").html(sHTML);
        }
        args.IsValid = false;
    } else {
        if ($("#error" + oSrc.getAttribute("id")).length > 0) {
            $("#error" + oSrc.getAttribute("id")).remove();
        }
        args.IsValid = true;
    }
}

function saveFavorite(CatID, ModuleID, PageURL) {
    $.ajax({
        url: config.siteBase + "favorites_add.aspx?CatID=" + CatID + "&ModuleID=" + ModuleID + "&PageURL=" + PageURL,
        error: function (xhr, ajaxOptions, thrownError) {
            alert("There has been an error saving favorite.");
        },
        success: function (data) {
            $("#AddFavoritesMsg").html(data);
            try {
                $(":input[type='button'],:input[type='submit']").button();
            } catch (e) {
                return false;
            }
        }
    });
}

function openModal(divId, width, height, saveButton) {
    if ($("#" + divId + "modal").length === 0) {
        dialogHTML = $("#" + divId).html();
        var modalHTML = "";
        modalHTML += '<div class="modal fade" id="' + divId + 'modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">';
        modalHTML += '  <div class="modal-dialog modal-dialog-centered" style="width: ' + (width + 30) + 'px;" role="document">';
        modalHTML += '    <div class="modal-content">';
        modalHTML += '      <div class="modal-header">';
        modalHTML += '        <h5 class="modal-title" id="myModalLabel">' + $("#" + divId).attr("title") + '</h5>';
        modalHTML += '        <button type="button" class="close" data-dismiss="modal" aria-label="Close">';
        modalHTML += '          <span aria-hidden="true">&times;</span>';
        modalHTML += '        </button>';
        modalHTML += "      </div>";
        modalHTML += '      <div class="modal-body" style="height: ' + (height - 80) + 'px;">';
        modalHTML += "        " + dialogHTML;
        modalHTML += "      </div>";
        modalHTML += '      <div class="modal-footer">';
        modalHTML += '      <button type="button" class="btn btn-secondary" data-dismiss="' + divId + 'modal" onclick="closeDialog(\'' + divId + '\')">Close</button>';
        if (saveButton !== undefined) {
            modalHTML += "      " + saveButton;
        }
        modalHTML += "      </div>";
        modalHTML += "    </div>";
        modalHTML += "  </div>";
        modalHTML += "</div>";
        $("#" + divId).html(modalHTML);
        $("#" + divId).show();
        $("#" + divId + "modal").modal();
        $("#" + divId + "modal").on("hidden.bs.modal", function (e) {
            closeDialog(divId);
        });
    } else {
        $("#" + divId + "modal").modal("show");
    }
    return false;
}

function openDialog(id, width, height, saveButton) {
    openModal(id, width, height, saveButton);
    return false;
}

function closeDialog(divId) {
    $("#" + divId + "modal").modal("hide");
    return false;
}

function adminLoginDialog() {
    $(document).ready(function () {
        try {
            $("#LoginButton").addClass("btn btn-primary");
            var loginButtonHTML = $("#LoginButton").clone().wrap("<div>").parent().html();
            $("#LoginButton").remove();
            openModal("LoginDiv", 320, 310, loginButtonHTML);
            if ($("#LoginButton").length === 0) {
                $("#LoginDiv .modal-footer").append(loginButtonHTML);
            }
        } catch (e) {
            return false;
        }
    });
}

function fixMenus() {

    var subMenuHtml = '<li class="nav-item dropdown">';
    subMenuHtml += '<a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
    subMenuHtml += 'Member Menu';
    subMenuHtml += '</a>';
    subMenuHtml += '<div class="dropdown-menu" aria-labelledby="navbarDropdown">';
    subMenuHtml += '</div>';
    subMenuHtml += '</li>';

    if (window.innerWidth > 768) {
        $('.sitemenu3v').show();
        $("h4:contains('Member Menu')").show();
        $('.sitemenu1 .nav .dropdown').remove();
        $('.sitemenu3v .nav li').each(function (index) {
            $(this).find('a').attr('class', 'nav-link');
        });
    } else {
        if ($('.sitemenu3v .nav li').length > 0) {
            $('.sitemenu1 .nav').append(subMenuHtml);
            $('.sitemenu3v .nav li').each(function (index) {
                $(this).find('a').attr('class', 'dropdown-item').clone().appendTo('.sitemenu1 .nav .dropdown .dropdown-menu');
            });
            $('.sitemenu3v').hide();
            $("h4:contains('Member Menu')").hide();
        }
    }
}

$(document).ready(function () {

    fixMenus();

    $(window).on('resize', function () {
        fixMenus();
    });

    // GridView Code
    restyleGridView("#ManageGridView");
    if (skipRestyling === false) {
        restyleFormElements();
    }

    $(":input[type='text'],:input[type='password'],textarea").not(".ignore").each(function () {
        $(this).addClass("form-control");
        $(this).removeClass("textEntry");
        $(this).removeClass("textareaEntry");
    });
    $(".GridViewStyle :input[type='text']").each(function () {
        $(this).css("display", "inline-block");
        $(this).css("width", "auto");
    });

    $(":input[type='radio']").not(".ignore").each(function () {
        if ($(this).attr("class") !== "ui-helper-hidden-accessible") {
            $(this).addClass("radio-inline");
            $(this).next("label").css("display", "inline-block");
            $(this).next("label").css("margin-left", "10px");
        }
    });

    $("input[type='checkbox']").not(".ignore").each(function () {
        if ($(this).attr("class") !== "ui-helper-hidden-accessible") {
            $(this).addClass("checkbox-inline");
            $(this).css("height", "16px");
            $(this).css("width", "16px");
            $(this).css("margin", "0");
            $(this).next("label").css("display", "inline-block");
            $(this).next("label").css("margin-left", "8px");
        }
    });

    $(".phoneEntry").each(function () {
        $(this).wrap('<div class="input-group"></div>');
        $('<div class="input-group-btn"><button onclick="openPhone(\'' + $(this).val() + '\');" type="button" class="btn btn-default" aria-label="Left Align"><span class="glyphicon glyphicon-earphone"></span></button></div>').insertAfter(this);
    });

    $(".TableTitle td").addClass("well");
    $(".TableTitle td").css("padding", "5px 0 5px 5px");
    $(":input[type='button'],:input[type='submit']").addClass("btn btn-default");
    if (skipRestyling === false) {
        $("select").not(".ignore").each(function () {
            $(this).addClass("form-control");
            $(this).removeClass("selectEntry");
        });
        $(".GridViewStyle select").not(".ignore").each(function () {
            $(this).css("display", "inline-block");
            $(this).css("width", "auto");
        });
        $(".inlineBlock").each(function () {
            $(this).css("display", "inline-block");
            $(this).css("width", "auto");
        });
        $(".combo_common_nowidth").each(function () {
            $(this).css("display", "inline-block");
            $(this).css("width", "auto");
        });
        $(".inline-block").each(function () {
            $(this).css("display", "inline-block");
        });
    }

    if ($(".product-slider").length > 0) {
        $(".product-slider").slick({
            slidesToShow: 2,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 2500,
            responsive: [
                {
                    breakpoint: 1024,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2,
                        infinite: true,
                        dots: true
                    }
                },
                {
                    breakpoint: 600,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                    }
                },
                {
                    breakpoint: 480,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                    }
                }
            ]
        });
    }

    $(".pagecontent").css("top", $("nav.navbar").height() + 20);
});

function openPhone(number) {
    if ($("#dialNumberDiv").length === 0) {
        $("body").append('<div id="dialNumberDiv" title="Dial Number"></div>');
        $("#dialNumberDiv")
            .append('<iframe style="width:100%; height: 450px;" id="UserSearchFrame" src="../twilio/softphone.aspx?Number=' + number + '" frameborder="0" />');
    }
    openModal("dialNumberDiv", 350, 560);
}

function restyleGridView(sid) {
    var captionHTML = "";

    if ($(sid + " caption").html() !== null) {
        captionHTML = $(sid + " caption").html();
    }

    $(sid).addClass("table table-striped table-bordered");

    if (captionHTML !== undefined) {
        if (captionHTML !== "") {
            $(sid + " caption").remove();
            $(sid).parent().prepend("<div>" + captionHTML + "</div>");
        }
    }

    $(sid + " tr th").css('white-space', 'nowrap');
    $(sid + " tr th a").css('display', 'inline-block');
    $(sid + " tr th a").css('width', '100%');
    $(sid + " tr th a").css('color', '#000000');
    $(sid + " tr th a").append('<i class="fa fa-arrows-v" style="margin-left:20px;"></i>');
}

function restyleFormElements(containername) {
    if (containername !== null) { containername = containername + " "; } else { containername = ""; }

    $(containername + ".selectEntry").each(function () {
        $(this).addClass("form-control");
    });

    $(".inline-block").each(function () {
        $(this).css("display", "inline-block");
    });
}

function showResultsChart(result) {
    var outputHTML = "";

    $.each(result, function (i) {
        outputHTML += result[i].PollOption + "<br/>";
        outputHTML += '<div class="progress">';
        outputHTML += '<div class="progress-bar" role="progressbar" aria-valuenow="' + result[i].Percentage + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + result[i].Percentage + '%;">' + result[i].Percentage + "%</div>";
        outputHTML += "</div>";
    });

    $("#PollResults").css("padding", "10px");
    $("#PollResults").html(outputHTML);
}

function showResultsChartRandom(result) {
    var outputHTML = "";

    $.each(result, function (i) {
        outputHTML += result[i].PollOption + "<br/>";
        outputHTML += '<div class="progress">';
        outputHTML += '<div class="progress-bar" role="progressbar" aria-valuenow="' + result[i].Percentage + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + result[i].Percentage + '%;">' + result[i].Percentage + "%</div>";
        outputHTML += "</div>";
    });

    $("#PollResultsRandom").css("padding", "10px");
    $("#PollResultsRandom").html(outputHTML);
}

function affiliateChart(result) {
    var outputHTML = "";

    $.each(result, function (i) {
        outputHTML += result[i].Level + "<br/>";
        outputHTML += '<div class="progress">';
        outputHTML += '<div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="' + result[i].TotalEarnings + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + result[i].TotalEarnings + 'px;">' + result[i].TotalEarnings + "</div>";
        outputHTML += '<div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="' + result[i].TotalVolume + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + (result[i].TotalVolume - result[i].TotalEarnings) + 'px;">' + result[i].TotalVolume + "</div>";
        outputHTML += "</div>";
    });

    $("#affiliateChart").css("padding", "10px");
    $("#affiliateChart").html(outputHTML);
}

function initMap() {
    var address = document.getElementById("idGoogleMapAddress").innerHTML;
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
        zoom: 12,
        center: latlng,
        mapTypeControl: true,
        mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
        navigationControl: true,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("idGoogleMapDiv"), myOptions);
    if (geocoder) {
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                if (status !== google.maps.GeocoderStatus.ZERO_RESULTS) {
                    map.setCenter(results[0].geometry.location);

                    var infowindow = new google.maps.InfoWindow(
                        {
                            content: "<b>" + address + "</b>",
                            size: new google.maps.Size(150, 50)
                        });

                    var marker = new google.maps.Marker({
                        position: results[0].geometry.location,
                        map: map,
                        title: address
                    });
                    google.maps.event.addListener(marker, "click", function () {
                        infowindow.open(map, marker);
                    });
                }
            }
        });
    }
}

function geocodeAddress(geocoder, resultsMap) {
    var address = document.getElementById("idGoogleMapAddress");
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === "OK") {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location
            });
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
}