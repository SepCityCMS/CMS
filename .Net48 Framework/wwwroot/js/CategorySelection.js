function addCategory(catID, FieldID) {
    if (document.getElementById(FieldID).value.indexOf("|" + catID + "|") === -1) {
        if (document.getElementById(FieldID).value !== "") {
            document.getElementById(FieldID).value = document.getElementById(FieldID).value + ",";
        }
        document.getElementById(FieldID).value = document.getElementById(FieldID).value + "|" + catID + "|";
        var tblID = document.getElementById("selectedItems");
        var lastRow = tblID.rows.length;
        var row = tblID.insertRow(lastRow);
        row.id = "sel" + catID;
        var newCell = row.insertCell(0);
        if (Right(lastRow, 1) === 0 ||
            Right(lastRow, 1) === 2 ||
            Right(lastRow, 1) === 4 ||
            Right(lastRow, 1) === 6 ||
            Right(lastRow, 1) === 8) {
            newCell.className = "TableBody2";
        } else {
            newCell.className = "TableBody1";
        }
        newCell.innerHTML = document.getElementById("availtd" + catID).innerHTML;
        newCell.style.cursor = "pointer";
        newCell.id = "seltd" + catID;
        newCell.onclick = function() { removeCategory(catID); };
    } else {
        alert("Category Already Exists.");
    }
    removeCatRow("availableItems", "avail" + catID);
}

function removeCategory(catID, FieldID) {
    try {
        document.getElementById("SearchCatListHelp").style.display = "none";
    } catch (e) {
        // Do Nothing
    };
    document.getElementById(FieldID).value = document.getElementById(FieldID).value.replace(",|" + catID + "|", "");
    document.getElementById(FieldID).value = document.getElementById(FieldID).value.replace("|" + catID + "|", "");
    var tblID = document.getElementById("availableItems");
    var lastRow = tblID.rows.length;
    var row = tblID.insertRow(lastRow);
    row.id = "avail" + catID;
    var newCell = row.insertCell(0);
    if (Right(lastRow, 1) === 0 ||
        Right(lastRow, 1) === 2 ||
        Right(lastRow, 1) === 4 ||
        Right(lastRow, 1) === 6 ||
        Right(lastRow, 1) === 8) {
        newCell.className = "TableBody2";
    } else {
        newCell.className = "TableBody1";
    }
    newCell.innerHTML = document.getElementById("seltd" + catID).innerHTML;
    newCell.style.cursor = "pointer";
    newCell.id = "availtd" + catID;
    newCell.onclick = function() { addCategory(catID, FieldID); };
    removeCatRow("selectedItems", "sel" + catID);
}

function removeCatRow(tableID, rowID) {
    var lastRow = document.getElementById(tableID).rows.length;
    for (var i = 0; i < lastRow; i++) {
        try {
            if (document.getElementById(tableID).rows[i].id === rowID) {
                document.getElementById(tableID).deleteRow(i);
            }
        } catch (e) {
            // Do Nothing
        }
    }
}

function submitCatSearch(PortalID, FieldID) {
    var sSearchValue = $("#CatSearch").val();
    $.ajax({
            url: "../spadmin/categoryselection.aspx?Keywords=" +
                sSearchValue +
                "&PortalID=" +
                PortalID +
                "&FieldID=" +
                FieldID
        })
        .done(function(data) {
            $("#SearchCatList").html(data);
        });
    return false;
}

function submitCatKeyPress(e) {
    if (!e.which) {
        return e.keyCode;
    } else {
        return e.which;
    }
}