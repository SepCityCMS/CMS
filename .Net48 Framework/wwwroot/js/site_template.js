function openDynamicFunctions(sHTML) {
    $("#DivDynHrefFrame").html(sHTML);
    openDialog("DivDynHrefFrame, 300, 400");
}

function openSaveWindow(sHTML) {
    $("#SaveHrefFrame").html(sHTML);
    openDialog("SaveHrefFrame", 300, 400);
}

function openTemplate() {
    $("#OpenTemplateFrame").attr("src", config.imageBase + "spadmin/site_template_open.aspx");
    openModal("OpenTempFrame", 450, 200);
}

function applyChanges() {

    var params = new Object();
    params.serializedData = $("#aspnetForm").serialize();

    $.ajax({
        type: "POST",
        url: config.imageBase + "api/sitetemplates/apply",
        data: JSON.stringify(params),
        dataType: "json",
        contentType: "application/json",
        success: function() {
            document.getElementById("TemplateFrame").src = config.imageBase + "spadmin/site_template_builder.aspx";
        },
        error: function(jqXHR, exception) {
            if (jqXHR.status === 0) {
                alert("Not connect.\n Verify Network.");
            } else if (jqXHR.status === 404) {
                alert("Requested page not found. [404]");
            } else if (jqXHR.status === 500) {
                alert("Internal Server Error [500].");
            } else if (exception === "parsererror") {
                alert("Requested JSON parse failed.");
            } else if (exception === "timeout") {
                alert("Time out error.");
            } else if (exception === "abort") {
                alert("Ajax request aborted.");
            } else {
                alert("Uncaught Error.\n" + jqXHR.responseText);
            }
        }
    });
}