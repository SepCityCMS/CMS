function ExecuteAction(goButton, sID) {

    try {
        if (document.getElementById("FilterDoAction").value === "") {
            alert("You must select an action from the dropdown.");
            return false;
        }
    } catch (ex) {
        RunExecuteAction(goButton, sID);
        return true;
    }

    try {
        if (Left(document.getElementById("FilterDoAction").value, 6) === "Delete") {
            if (sID === "ZoneID" || sID === "CatID") {
                confirm("Are you sure you wish to delete the selected content?\nAny related content will also be deleted.", function () {
                    RunExecuteAction(goButton, sID);
                    return true;
                });
                return false;
            } else {
                confirm("Are you sure you wish to delete the selected content?", function () {
                    RunExecuteAction(goButton, sID);
                    return true;
                });
                return false;
            }
        } else if (document.getElementById("FilterDoAction").value === "ResetMembers") {
            confirm("Are you sure you wish to reset the selected members?", function () {
                RunExecuteAction(goButton, sID);
                return true;
            });
            return false;
        }
    } catch (ex) {
        RunExecuteAction(goButton, sID);
        return true;
    }

    RunExecuteAction(goButton, sID);
    return true;

}

function RunExecuteAction(goButton, sID) {

    var allInputs = document.getElementsByTagName("input");
    var sUnqiueIDs = "";
    for (var i = 0; i < allInputs.length; i++) {
        if (allInputs[i].type === "checkbox" && Left(allInputs[i].id, sID.length) === sID) {
            if (allInputs[i].checked) {
                sUnqiueIDs += "," + allInputs[i].value;
                allInputs[i].checked = false;
            }
        }
    }
    sUniqueIDs = Right(sUnqiueIDs, sUnqiueIDs.length - 1);
    $("#UniqueIDs").val(sUniqueIDs);
    if ($('#FilterDoAction').val() !== "MoveClass" && $('#FilterDoAction').val() !== "AddAccessKey" && $('#FilterDoAction').val() !== "RemoveAccessKey" && $('#FilterDoAction').val() !== "AddGroup") {
        $(goButton).removeAttr("onclick");
        $(goButton).attr("onclick", "__doPostBack('" + $(goButton).attr("id").replace(/_/g, "$") + "','')");
        $(goButton).click();
    }

}
