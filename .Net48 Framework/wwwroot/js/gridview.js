function gridviewCheckAll(objRef) {

    $("input[type='checkbox']")
        .each(function () {
            if (objRef.checked) {
                $(this).prop("checked", true);
            } else {
                $(this).prop("checked", false);
            }
        });
}

function gridviewSelectRow(objRef) {

}

function submitSearch(e) {
    var key;
    if (window.event) {
        key = window.event.keyCode; //IE
    } else {
        key = e.which; //firefox      
    }

    return (key !== 13);
}

$(document).ready(function () {
    if (document.location.href.indexOf("/spadmin/") > -1) {
        if ($(".pagination-ys td").length > 0) {
            var pageHTML = $(".pagination-ys td").html();
            $(".pagination-ys").remove();
            $('<div class="pagination-ys">' + pageHTML + "</div>").insertAfter($("#ManageGridView").parent("div"));
            $("#ManageGridView").parent("div").height($(window).height() - $("h2").outerHeight() - $(".navbar.navbar-inverse").outerHeight() - $(".panel-heading").outerHeight() - $(".pagination").outerHeight() - 130);
            $("#ManageGridView").parent("div").css("overflow", "auto");
        }
    }
});