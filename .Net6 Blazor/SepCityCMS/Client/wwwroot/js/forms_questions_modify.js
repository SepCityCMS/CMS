$("#NumOptions").val("0");

$(document)
    .ready(function() {
        $("#QuestionType")
            .change(function() {
                if ($(this).val() === "CB" || $(this).val() === "RB" || $(this).val() === "DD") {
                    $("#Option1Row").show();
                    $("#Option2Row").show();
                    $("#Option3Row").show();
                    $("#Option4Row").show();
                    $("#Option5Row").show();
                    $("#OptionAdd").show();
                } else {
                    $("#Option1Row").hide();
                    $("#Option2Row").hide();
                    $("#Option3Row").hide();
                    $("#Option4Row").hide();
                    $("#Option5Row").hide();
                    $("#OptionAdd").hide();
                }
            });

        if ($("#AnswerType").val() === "CB" || $("#AnswerType").val() === "RB" || $("#AnswerType").val() === "DD") {
            $("#Option1Row").show();
            $("#Option2Row").show();
            $("#Option3Row").show();
            $("#Option4Row").show();
            $("#Option5Row").show();
            $("#OptionAdd").show();
        }
    });

function addCustomOption() {

    var newNum = (parseInt($("#NumOptions").val()) + 1);
    $("#NumOptions").val(newNum);

    var sHTML = "";
    sHTML = '<p id="Option' + newNum + 'Row">';
    sHTML += '<label for="Option' + newNum + '" id="Option' + newNum + 'Label">Option ' + newNum + "</label>";
    sHTML += '<input name="ctl00$MainContent$Option' +
        newNum +
        '" type="text" maxlength="100" id="Option' +
        newNum +
        '" class="textEntry" />';
    sHTML += '<input type="hidden" name="ctl00$MainContent$OptionID' +
        newNum +
        '" id="OptionID' +
        newNum +
        '" value="' +
        getIdentity() +
        '" />';
    sHTML += "</p>";

    $("#OptionPanel").append(sHTML);

    return false;
}